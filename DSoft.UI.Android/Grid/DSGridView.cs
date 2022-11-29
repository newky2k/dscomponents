// ****************************************************************************
// <copyright file="DSGridView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using DSoft.Datatypes.Grid;
using DSoft.Datatypes.Grid.Shared;
using DSoft.Themes.Grid;
using DSoft.Datatypes.Grid.Interfaces;
using DSoft.Datatypes.Grid.Data.Interfaces;
using DSoft.Datatypes.Grid.Data;
using DSoft.Datatypes.Enums;
using DSoft.UI.Grid.Views;
using DSoft.UI.Views;
using DSoft.Datatypes.Types;

namespace DSoft.UI.Grid
{
	/// <summary>
	/// DSGridView for Android
	/// </summary>
	public class DSGridView : DSAbsoluteLayout, IDSDataGridView
	{
		#region Fields
		private DSGridProcessor mProcessor;
		private bool hasDrawn = false;
		private DSGridTheme mTheme;
		private int mTrialTapCount = 0;
		private DSGridViewContainer mRowsView;
		private DSGridRowView mHeaderView;
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

		#region Properties
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
		/// Gets the processor.
		/// </summary>
		/// <value>The processor.</value>
		public  DSGridProcessor Processor
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
					if (hasDrawn)
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
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.DSGridView"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public DSGridView(IntPtr javaReference, JniHandleOwnership transfer) 
			: base(javaReference, transfer)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.DSGridView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public DSGridView(Context context) :
			base(context)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.DSGridView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public DSGridView(Context context, IAttributeSet attrs) :
			base(context, attrs)
		{
			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.DSGridView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		/// <param name="defStyle">Def style.</param>
		public DSGridView(Context context, IAttributeSet attrs, int defStyle) :
			base(context, attrs, defStyle)
		{
			Initialize();
		}

		#endregion

		#region Overrides
		/// <Docs>This is a new size or position for this view</Docs>
		/// <remarks>Called from layout when this view should
		///  assign a size and position to each of its children.
		/// 
		///  Derived classes with children should override
		///  this method and call layout on each of
		///  their children.</remarks>
		/// <format type="text/html">[Android Documentation]</format>
		/// <since version="Added in API level 1"></since>
		/// <summary>
		/// Raises the layout event.
		/// </summary>
		/// <param name="changed">If set to <c>true</c> changed.</param>
		/// <param name="l">L.</param>
		/// <param name="t">T.</param>
		/// <param name="r">The red component.</param>
		/// <param name="b">The blue component.</param>
		protected override void OnLayout (bool changed, int l, int t, int r, int b)
		{
			base.OnLayout (changed, l, t, r, b);

			if (!hasDrawn)
			{
				hasDrawn = true;

				BuildGrid ();


			}
		}
		#endregion

		#region Methods

		/// <summary>
		/// Reloads the data.
		/// </summary>
		/// <param name="clearSelection">If set to <c>true</c> clear selection.</param>
		public void ReloadData (bool clearSelection = true)
		{
			if (!hasDrawn)
				return;

			if (clearSelection)
			{

				Processor.ClearSelectedItems(false);
			}


			Processor.Reset();

			GC.Collect ();


			if (hasDrawn)
			{
				//clean down variables
				//mContentSize = null;

				if (this.Context is Activity)
				{
					((Activity)this.Context).RunOnUiThread(()=>
					{
						BuildGrid ();
					});
				}


			}

		}

		private void BuildGrid ()
		{
	
			DrawHeaderRow ();

			mRowsView.BuildGrid();
		}

		/// <summary>
		/// Draws the header row.
		/// </summary>
		private void DrawHeaderRow ()
		{			
			if (this.Theme.HeaderStyle == GridHeaderStyle.Fixed)
			{
				if (mHeaderView == null)
				{
					mHeaderView = new DSGridRowView (this);
					mHeaderView.Processor.RowIndex = 0;
					mHeaderView.Processor.Style = CellStyle.Header;

					//var hWidth = Processor.CalculateSize().Width;

					//mHeaderView.LayoutParameters = new DSAbsoluteLayout.DSAbsoluteLayoutParams (hWidth, Context.ToDevicePixels ((int)this.Theme.HeaderHeight), 0, 0);
				}

				var hWidth = Processor.CalculateSize().Width;
				mHeaderView.LayoutParameters = new DSAbsoluteLayout.DSAbsoluteLayoutParams (Context.ToDevicePixels ((int)hWidth), Context.ToDevicePixels ((int)this.Theme.HeaderHeight), 0, 0);

				mHeaderView.Initialize();

				if (mHeaderView.Parent == null)
					this.AddView (mHeaderView);

			}
		}

		private void Initialize()
		{
			this.SetBackgroundColor (this.Theme.BackgroundColor.ToAndroidColor ());

			mRowsView = new DSGridViewContainer(this);

			mRowsView.OnDidScroll += HandleOnDidScroll;
			this.AddView(mRowsView);
		}

		/// <summary>
		/// Handles the on did scroll.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="offSet">Off set.</param>
		internal void HandleOnDidScroll (object sender, DSPoint offSet)
		{
			if (mHeaderView != null)
			{
				var hWidth = Processor.CalculateSize().Width;
				mHeaderView.LayoutParameters = new DSAbsoluteLayout.DSAbsoluteLayoutParams (Context.ToDevicePixels ((int)hWidth), Context.ToDevicePixels ((int)this.Theme.HeaderHeight), -(int)offSet.X, 0);
			}
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

		#endregion

		#region Event Handlers

		/// <summary>
		/// Handles the trial tap.
		/// </summary>
		private void HandleTrialTap ()
		{
			if (mTrialTapCount == 0 || mTrialTapCount > 5)
			{
				if (this.Context is Activity)
				{
					((Activity)this.Context).RunOnUiThread(()=>
					{
						var dlgAlert = (new AlertDialog.Builder (this.Context)).Create ();
						dlgAlert.SetMessage ("This is a trial version of DSoft Developments DSGridView control.  Please visit the Xamarin component store to purchase the full version.");
						dlgAlert.SetTitle ("Trial Mode");
						dlgAlert.SetButton("OK", (s,e)=>{});
						dlgAlert.Show ();

						mTrialTapCount = 1;
					});
				}

			}

			mTrialTapCount++;
		}

		/// <summary>
		/// Handles the on header cell tapped.
		/// </summary>
		/// <param name="sender">Sender.</param>
		public void HandleOnHeaderCellTapped (IDSGridCellView sender)
		{
			Processor.CurrentTable.SortByColumn (sender.Processor.ColumnIndex);

			ReloadData (false);

			Processor.RebuildSelection();
		}

		/// <summary>
		/// Handles the on selected row changed.
		/// </summary>
		/// <param name="RowIndex">Row index.</param>
		public void HandleOnSelectedRowChanged (int RowIndex)
		{
			Processor.UpdateSelection (RowIndex);
		}

		/// <summary>
		/// Handles the on cell single tap.
		/// </summary>
		/// <param name="sender">Sender.</param>
		public void HandleOnCellSingleTap (IDSGridCellView sender)
		{
			OnSingleCellTap (sender);
		}

		/// <summary>
		/// Handles the on cell double tap.
		/// </summary>
		/// <param name="sender">Sender.</param>
		public void HandleOnCellDoubleTap (IDSGridCellView sender)
		{
			OnDoubleCellTap (sender);
		}

		/// <summary>
		/// Handles the on row double select.
		/// </summary>
		/// <param name="sender">Sender.</param>
		public void HandleOnRowDoubleSelect (IDSGridRowView sender)
		{
			if (IsTrial)
				HandleTrialTap ();

			if (sender.Processor.RowIndex > -1)
			{
				OnRowDoubleTapped (sender, sender.Processor.RowIndex);
			}
		}

		/// <summary>
		/// Handles the on row single select.
		/// </summary>
		/// <param name="sender">Sender.</param>
		public void HandleOnRowSingleSelect (IDSGridRowView sender)
		{
			if (IsTrial)
				HandleTrialTap ();

			if (sender.Processor.RowIndex >= 0)
			{

				OnRowSelect (sender, sender.Processor.RowIndex);
			}


		}

		#endregion
	}
}

