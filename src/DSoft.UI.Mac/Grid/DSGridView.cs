// ****************************************************************************
// <copyright file="DSGridView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using Foundation;
using DSoft.Datatypes.Grid;
using AppKit;
using DSoft.Datatypes.Grid.Data.Interfaces;
using DSoft.Themes.Grid;
using DSoft.Datatypes.Grid.Shared;
using DSoft.UI.Mac.Grid.Views;
using DSoft.Datatypes.Grid.Data;
using CoreGraphics;
using DSoft.Datatypes.Enums;
using DSoft.UI.Mac.Extensions;
using System.Linq;
using System.Collections.Generic;
using DSoft.Datatypes.Grid.Data.Collections;

namespace DSoft.UI.Mac.Grid
{
	/// <summary>
	/// A Scrollable Grid View 
	/// </summary>
	[Register ("DSGridView")]
	public class DSGridView : NSScrollView, IDSDataGridView
	{
		#region Static fields and properties

		private static float mDTapTimeout = 0.2f;

		/// <summary>
		/// Gets or sets the double tap timeout. Used to set the response time of the first tap when double tap is enabled
		/// </summary>
		/// <value>The double tap timeout.</value>
		public static float DoubleTapTimeout {
			get
			{
				return mDTapTimeout;
			}
			set
			{
				mDTapTimeout = value;
			}
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when on single cell tap.
		/// </summary>
		public event CellTappedHandlerDelegate OnSingleCellTap = delegate {};
		/// <summary>
		/// Occurs when on double cell tap.
		/// </summary>
		public event CellTappedHandlerDelegate OnDoubleCellTap = delegate {};
		/// <summary>
		/// Occurs when a row is selected
		/// </summary>
		public event RowSelectedDelegate OnRowSelect = delegate {};
		/// <summary>
		/// Occurs when a row is double tapped
		/// </summary>
		public event RowSelectedDelegate OnRowDoubleTapped = delegate {};

		#endregion

		#region Public Properties
		/// <summary>
		/// Gets or sets the data source.  If set rebuilds the grid
		/// </summary>
		/// <value>The data source.</value>
		public IDSDataSource DataSource { 
			get
			{
				return Processor.DataSource;
			}
			set
			{
				Processor.DataSource = value;

				//force redraw
				ReloadData ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to enable multi-select, updates EnableDeselection with same value
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
		public bool EnableMulitSelect {
			get
			{
				return Processor.EnableMulitSelect;
			}
			set
			{
				Processor.EnableMulitSelect =  value;
			}
		}


		/// <summary>
		/// Toggle row highlighting
		/// </summary>
		public bool ShowSelection;

		/// <summary>
		/// Gets or sets the name of the table in the datasource, when datasource is a dataset.  Will rebuild the grid when set.
		/// </summary>
		/// <value>The name of the table.</value>
		public string TableName { 
			get
			{
				return Processor.TableName;
			}
			set
			{
				Processor.TableName = value;

				if (Processor.DataSource is DSDataSet)
				{
					//update the grid
					ReloadData ();
				}

			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="DSoft.UI.Grid.DSGridView"/> enable double tap.
		/// </summary>
		/// <value><c>true</c> if enable double tap; otherwise, <c>false</c>.</value>
		internal bool EnableDoubleTap {
			get
			{
				if (OnRowDoubleTapped != null)
					return true;

				return false;
			} 

		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="DSoft.UI.Grid.DSGridView"/> content top inset.
		/// </summary>
		/// <value><c>true</c> if content top inset; otherwise, <c>false</c>.</value>
		public int ContentTopInset {
			get
			{
				if (mTopInset == -1)
				{
					mTopInset = 0;
				}

				return mTopInset;
			}
			set
			{
				mTopInset = value;

				Setup ();
			}
		}

		/// <summary>
		/// Gets or sets the theme for the instance of DSGridView.  If none set it will use the global theme
		/// </summary>
		/// <value>The theme.</value>
		public DSGridTheme Theme {
			get
			{
				if (mTheme == null)
					return DSGridTheme.Current;

				return mTheme;
			}
			set
			{
				if (mTheme != value)
				{
					mTheme = value;

					//reload the control
					if (isDrawn)
						ReloadData ();

					//if setting null then readded the global them changer event handler
					if (value == null)
					{
						DSGridTheme.OnThemeChanged += HandleOnThemeChanged;
					}
					else
					{
						//remove it if we are setting a specific theme
						DSGridTheme.OnThemeChanged -= HandleOnThemeChanged;
					}

				}
			}
		}

		/// <summary>
		/// Gets or sets the selected item. If multi-select enabled then it will return the first selected item
		/// </summary>
		/// <value>The selected item.</value>
		public DSDataRow SelectedItem {
			get
			{
				return Processor.SelectedItem;
			}
			set
			{
				Processor.SelectedItem = value;

			}
		}

		/// <summary>
		/// Gets or sets the selected items.
		/// </summary>
		/// <value>The selected items.</value>
		public DSDataRow[] SelectedItems {
			get
			{
				return Processor.SelectedItems;
			}
			set
			{
				Processor.SelectedItems = value;
			}
		}

		/// <summary>
		/// Gets or sets the index of the selected. If multi-select enabled then it will return the index of the first
		/// selected item
		/// </summary>
		/// <value>The index of the selected.</value>
		public int SelectedIndex {
			get
			{
				return Processor.SelectedIndex;
			}
			set
			{
				Processor.SelectedIndex = value;
			}
		}

		/// <summary>
		/// Gets or sets the selected indexes.
		/// </summary>
		/// <value>The selected indexes.</value>
		public int[] SelectedIndexes {
			get
			{
				return Processor.SelectedIndexes;
			}
			set
			{
				Processor.SelectedIndexes = value;
			}
		}

		/// <summary>
		/// Gets or sets a value wether Deselection(by tap the same row) will be enabled.  Enabled by default when EnableMultiSelect is enabled
		/// </summary>
		/// <value><c>true</c> if enable deselection; otherwise, <c>false</c>.</value>
		public bool EnableDeselection {
			get
			{
				return Processor.EnableDeselection;
			}
			set
			{
				Processor.EnableDeselection = value;
			}
		}

		public CGPoint ContentOffset
		{
			get
			{
				return this.DocumentVisibleRect.Location;
			}
			set
			{
				if (this.DocumentView is NSView)
				{
					var aView = this.DocumentView as NSView;

					aView.ScrollPoint(value);
				}
			}
		}

		#endregion

		#region private Fields
		private DSGridProcessor mProcessor;

		private bool isDrawn = false;
		private int mTopInset = -1;
		private DSGridRowView m_HeaderView;

		private int mTrialTapCount = 0;
		private int mRowStart = 0;

		private DSGridTheme mTheme;


		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.DSGridView"/> class.
		/// </summary>
		public DSGridView ()
		{
			Setup ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.DSGridView"/> class.
		/// </summary>
		/// <param name="Frame">Frame.</param>
		public DSGridView (CGRect Frame) : base (Frame)
		{
			Setup ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.DSGridView"/> class.
		/// </summary>
		/// <param name="handle">Handle.</param>
		public DSGridView (IntPtr handle) : base (handle)
		{
			Setup ();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void DrawRect (CGRect rect)
		{

			base.DrawRect (rect);

			ReDraw ();

		}


		/// <Docs>Lays out subviews.</Docs>
		/// <summary>
		/// Layouts the subviews.
		/// </summary>
		public  void LayoutSubviews ()
		{
//			base.LayoutSubviews ();
//
//			if (this.ContentInset.Top != 0)
//			{
//				this.ContentInset = new UIEdgeInsets (0, 0, 0, 0);
//			}


			DrawViews ();
		}

		/// <summary>
		/// Reloads the data.
		/// </summary>
		public void ReloadData (bool clearSelection = true)
		{
			if (clearSelection)
			{

				Processor.ClearSelectedItems(false);
			}

			mRowStart = 0;



			if (m_HeaderView != null)
			{
				m_HeaderView.RemoveFromSuperview ();
				m_HeaderView = null;
			}

			Processor.Reset();

			GC.Collect ();

			this.SetNeedsDisplay ();

		}

		/// <summary>
		/// Selects the specified row
		/// </summary>
		/// <param name="Index">row Index</param>
		/// <param name="Animated">If set to <c>true</c> animated, default(true)</param>
		/// <param name="Mode">Scroll Mode, default(None)</param>
		/// <param name="AdditonalOffset">Additonal offset(default(0)</param>
		public void SelectRow (int Index, bool Animated = true, ScrollToMode Mode = ScrollToMode.None, float AdditonalOffset = 0)
		{
			Processor.UpdateSelection (Index);

			nfloat top = -1;

			switch (Mode)
			{
				case ScrollToMode.None:
					{
						top = this.ContentOffset.Y;
					}
					break;
				case ScrollToMode.Top:
					{
						top = Processor.TopYForRow (Index);
					}
					break;
				case ScrollToMode.Middle:
					{
						top = Processor.TopYForRow (Index);
						var bottomOffset = (this.Frame.Height - Processor.RowHeight) + this.Theme.HeaderHeight;
						bottomOffset = (((bottomOffset - this.Frame.Top) - 20) - AdditonalOffset);
						top -= ((bottomOffset / 2) - (Processor.RowHeight / 2));
					}
					break;
				case ScrollToMode.Bottom:
					{
						top = Processor.TopYForRow (Index);
						var bottomOffset = (this.Frame.Height - Processor.RowHeight) + this.Theme.HeaderHeight;
						top -= (((bottomOffset - this.Frame.Top) - 20) - AdditonalOffset);
					}
					break;
			}

			this.ContentOffset = new CGPoint (0, top);
		}

		/// <summary>
		/// Clears the selected items.
		/// </summary>
		public void ClearSelectedItems()
		{
			Processor.ClearSelectedItems(true);
		}

		#endregion

		#region Private Properties

		/// <summary>
		/// Gets a value indicating whether this instance is trial.
		/// </summary>
		/// <value><c>true</c> if this instance is trial; otherwise, <c>false</c>.</value>
		private bool IsTrial {
			get
			{
				#if TRIAL 
				return true;
				#else
				return false;
				#endif
			}
		}

		/// <summary>
		/// Gets the processor.
		/// </summary>
		/// <value>The processor.</value>
		internal DSGridProcessor Processor
		{
			get
			{
				if (mProcessor == null)
				{
					mProcessor = new DSGridProcessor();
					mProcessor.ThemeRowHeight = ()=>
					{
						return Theme.RowHeight;
					};

					mProcessor.ThemeHeaderStyle = ()=>
					{
						return Theme.HeaderStyle;
					};

					mProcessor.ThemeHeaderHeight = ()=>
					{
						return Theme.HeaderHeight;
					};
				}
				return mProcessor;
			}
		}
		#endregion

		#region Private Methods

		/// <summary>
		/// Setup this instance.
		/// </summary>
		private void Setup ()
		{
			this.DocumentView = new DSGridViewContainer();

			this.BackgroundColor = this.Theme.BackgroundColor.ToNSColor ();

			//this.Scrolled += HandleHandleScrolled;
			this.HasHorizontalScroller = true;
			this.HasVerticalScroller = true;
			//this.ClipsToBounds = true;
			//			this.mEnableMulitSelect = true;
			//			this.mEnableDeselection = false;
			DSGridTheme.OnThemeChanged += HandleOnThemeChanged;



		}

		/// <summary>
		/// Handles the on theme changed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleOnThemeChanged (object sender, EventArgs e)
		{
			if (mTheme == null)
			{
				ReloadData ();
			}
		}

		/// <summary>
		/// Res the draw.
		/// </summary>
		private void ReDraw ()
		{
			isDrawn = true;

			((NSView)this.DocumentView).SetFrameSize(Processor.CalculateSize ().ToSizeF ());
 
			CleanUpViews ();

			DrawViews ();
			DrawHeaderRow ();
		}

		/// <summary>
		/// Cleans up views.
		/// </summary>
		private void CleanUpViews ()
		{
			//remove subviews
			foreach (var view in this.Subviews)
			{
				view.RemoveFromSuperview ();

				if (view is DSGridRowView)
				{
					((DSGridRowView)view).IsSelected = false;
				}
			}
		}

		/// <summary>
		/// Draws the header row.
		/// </summary>
		private void DrawHeaderRow ()
		{			
			if (this.Theme.HeaderStyle == GridHeaderStyle.Fixed)
			{
				if (m_HeaderView == null)
				{
					m_HeaderView = new DSGridRowView (this);
					m_HeaderView.RowIndex = 0;
					m_HeaderView.Style = CellStyle.Header;

					m_HeaderView.Frame = new CGRect (0, this.ContentOffset.Y, this.ContentSize.Width, this.Theme.HeaderHeight).Integral ();

				}

				m_HeaderView.Frame = new CGRect (m_HeaderView.Frame.Left, this.ContentOffset.Y, this.ContentSize.Width, this.Theme.HeaderHeight).Integral ();
				m_HeaderView.SetNeedsDisplay ();

				if (m_HeaderView.Superview == null)
					this.AddSubview (m_HeaderView);
			}
		}

		/// <summary>
		/// Handles the handle scrolled.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleHandleScrolled (object sender, EventArgs e)
		{
			if (this.Theme.HeaderStyle == GridHeaderStyle.Fixed)
				DrawHeaderRow ();
		}

		/// <summary>
		/// Draws the views.
		/// </summary>
		private void DrawViews ()
		{
			var availDefs = new List<int> ();

			var rowsToKeep = new IDSGridRowViewCollection ();
			int lastIndex = 0;

			if (mRowStart < 0)
				mRowStart = 0;


			for (int i = mRowStart; i < Processor.NumberOfRows; i++)
			{
				var height = (i == 0 && this.Theme.HeaderStyle != GridHeaderStyle.None) ? this.Theme.HeaderHeight : Processor.RowHeight;
				var top = Processor.TopYForRow (i);

				if (ShouldShowRow (top, height))
				{
					availDefs.Add (i);

					var aRow = Processor.Rows.ViewForRowIndex (i) as DSGridRowView;

					if (aRow != null)
						rowsToKeep.Add (aRow);

				}
				else
				{
					if (IsAboveScreen (top, height))
					{
						continue;
					}
					else if (IsBelowScreen (top, height))
					{
						lastIndex = i;
						break;
					}
				}


			}

			if (rowsToKeep.Count != 0)
				mRowStart = (lastIndex - rowsToKeep.Count) - 5;

			//find the rows that are no longer need on screen
			var freeRows = (from freeRow in Processor.Rows
				where !rowsToKeep.Contains (freeRow)
				select freeRow).ToList ();

			//remove them from the screen and add them to the free rows screen
			foreach (var item in freeRows)
			{
				item.DetachView ();
				Processor.FreeRows.Add (item);
			}

			Processor.Rows.Clear ();

			Processor.Rows = rowsToKeep;
			rowsToKeep = null;

			foreach (var index in availDefs)
			{
				var aRow = Processor.FindViewForRow (index, (Index)=>
				{
					return new DSGridRowView (Index, this);
				}) as DSGridRowView;

				var height = (index == 0 && this.Theme.HeaderStyle != GridHeaderStyle.None) ? this.Theme.HeaderHeight : Processor.RowHeight;
				var top = Processor.TopYForRow (index);
				//aRow.Columns = ColDefs;
				aRow.Frame = new CGRect (0, top, this.ContentSize.Width, height).Integral ();

				if (aRow.Superview == null)
				{
					this.AddSubview(aRow, NSWindowOrderingMode.Below, (NSView)this.Subviews[0]);
					//this.InsertSubview (aRow, 0);
				}
			}

			//clear unused rows
			availDefs.Clear ();
			Processor.FreeRows.Dispose ();

		}

		/// <summary>
		/// Determines whether this instance is above screen the specified aTop aHeight.
		/// </summary>
		/// <returns><c>true</c> if this instance is above screen the specified aTop aHeight; otherwise, <c>false</c>.</returns>
		/// <param name="aTop">A top.</param>
		/// <param name="aHeight">A height.</param>
		private bool IsAboveScreen (float aTop, float aHeight)
		{
			var cache = 10 * Processor.RowHeight;
			var result = (aTop + aHeight < (this.ContentOffset.Y - cache));
			return result;
		}

		/// <summary>
		/// Determines whether this instance is below screen the specified aTop aHeight.
		/// </summary>
		/// <returns><c>true</c> if this instance is below screen the specified aTop aHeight; otherwise, <c>false</c>.</returns>
		/// <param name="aTop">A top.</param>
		/// <param name="aHeight">A height.</param>
		private bool IsBelowScreen (float aTop, float aHeight)
		{
			var cache = 10 * Processor.RowHeight;
			var result = (aTop > (this.ContentOffset.Y + this.Frame.Height) + cache);
			return result;
		}

		/// <summary>
		/// Checks to see if the row should show
		/// </summary>
		/// <returns><c>true</c>, if show row was shoulded, <c>false</c> otherwise.</returns>
		/// <param name="aTop">A top.</param>
		/// <param name="aHeight">A height.</param>
		private bool ShouldShowRow (float aTop, float aHeight)
		{
			return (!(IsAboveScreen (aTop, aHeight)
				|| IsBelowScreen (aTop, aHeight)));

		}

		/// <summary>
		/// Handles the trial tap.
		/// </summary>
		private void HandleTrialTap ()
		{
			if (mTrialTapCount == 0 || mTrialTapCount > 5)
			{
//				var alertView = new UIAlertView ("Trial Mode"
//					, "This is a trial version of DSofts DSGridView control.\n  Please visit the Xamarin component store to purchase the full version."
//					, null, "OK", null);
//
//				alertView.Show ();

				mTrialTapCount = 1;
			}

			mTrialTapCount++;
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Handles the on header cell tapped.
		/// </summary>
		/// <param name="sender">Sender.</param>
		internal void HandleOnHeaderCellTapped (DSGridCellView sender)
		{
			Processor.CurrentTable.SortByColumn (sender.ColumnIndex);

			ReloadData (false);

			Processor.RebuildSelection();
		}

		/// <summary>
		/// Handles the on selected row changed.
		/// </summary>
		/// <param name="RowIndex">Row index.</param>
		internal void HandleOnSelectedRowChanged (int RowIndex)
		{
			Processor.UpdateSelection (RowIndex);
		}

		/// <summary>
		/// Handles the on cell single tap.
		/// </summary>
		/// <param name="sender">Sender.</param>
		internal void HandleOnCellSingleTap (DSGridCellView sender)
		{
			this.CommitEditing();
			//this.EndEditing (true);

			OnSingleCellTap (sender);
		}

		/// <summary>
		/// Handles the on cell double tap.
		/// </summary>
		/// <param name="sender">Sender.</param>
		internal void HandleOnCellDoubleTap (DSGridCellView sender)
		{
			OnDoubleCellTap (sender);
		}

		/// <summary>
		/// Handles the on row double select.
		/// </summary>
		/// <param name="sender">Sender.</param>
		internal void HandleOnRowDoubleSelect (DSGridRowView sender)
		{
			if (IsTrial)
				HandleTrialTap ();

			if (sender.RowIndex > -1)
			{
				OnRowDoubleTapped (sender, sender.RowIndex);
			}
		}

		/// <summary>
		/// Handles the on row single select.
		/// </summary>
		/// <param name="sender">Sender.</param>
		internal void HandleOnRowSingleSelect (DSGridRowView sender)
		{
			if (IsTrial)
				HandleTrialTap ();

			if (sender.RowIndex >= 0)
			{

				OnRowSelect (sender, sender.RowIndex);
			}


		}

		#endregion
	}
}

