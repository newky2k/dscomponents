// ****************************************************************************
// <copyright file="DSGridViewContainer.cs" company="DSoft Developments">
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
using DSoft.Themes.Grid;
using Android.Graphics;
using Android.Text;
using DSoft.Datatypes.Grid.Data.Interfaces;
using DSoft.Datatypes.Grid.Data;
using DSoft.Datatypes.Grid.Data.Collections;
using DSoft.Datatypes.Grid.MetaData.Collections;
using DSoft.Datatypes.Grid.MetaData;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Types;
using DSoft.UI.Views;
using DSoft.UI.Grid.Views;
using DSoft.UI.Grid.Views.Collections;
using DSoft.Datatypes.Grid.Shared;
using DSoft.Datatypes.Grid.Interfaces;
using DSoft.Datatypes.Grid;
using Java.Interop;

namespace DSoft.UI.Grid.Views
{
	/// <summary>
	/// DS grid view.
	/// </summary>
	internal class DSGridViewContainer : DSMultiDirectionScrollView
	{
		#region Fields
		private DSGridView mParentGrid;
		private DSAbsoluteLayout mContainer;
		private DSSize mContentSize;
		//private bool hasDrawn = false;
		//private DSGridTheme mTheme;
		//private int mTrialTapCount = 0;
		private int mRowStart = 0;
		#endregion

		#region Events

		/// <summary>
		/// Occurs when the grid view is scrolled
		/// </summary>
		public event EventHandler<DSPoint> OnDidScroll = delegate {};

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the size of the content.
		/// </summary>
		/// <value>The size of the content.</value>
		public DSSize ContentSize {
			get
			{
				if (mContentSize == null)
				{
					mContentSize = new DSSize (0, 0);

					if (mParentGrid.Processor.DataSource != null)
					{
						mContentSize = mParentGrid.Processor.CalculateSize ();
					}
				}
				return 	mContentSize;
			}
			set
			{
				mContentSize = value;
			}
		}
			
		#endregion

		#region Private and Internal Properties

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
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridViewContainer"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public DSGridViewContainer(IntPtr javaReference, JniHandleOwnership transfer) 
			: base(javaReference, transfer)
		{
			Initialize ();
		}
			
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridViewContainer"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public DSGridViewContainer (Context context) :
			base (context)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridViewContainer"/> class.
		/// </summary>
		/// <param name="grdView">Grd view.</param>
		public DSGridViewContainer(DSGridView grdView) 
			: base(grdView.Context)
		{
			mParentGrid = grdView;

			Initialize();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridViewContainer"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public DSGridViewContainer (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridViewContainer"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		/// <param name="defStyle">Def style.</param>
		public DSGridViewContainer (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize ();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize ()
		{
			this.SetBackgroundColor (this.mParentGrid.Theme.BackgroundColor.ToAndroidColor ());
		
		}
			

		/// <summary>
		/// Builds the grid.
		/// </summary>
		internal void BuildGrid ()
		{
			//this.RemoveAllViews ();

			CleanUpViews();
	
			//need to calculate the width based on the columns within the datasource
			var container = new DSAbsoluteLayout (Context);
			container.SetBackgroundColor(Color.Transparent);


			var contSize = ContentSize;
			container.LayoutParameters = new ViewGroup.LayoutParams (Context.ToDevicePixels ((int)contSize.Width), Context.ToDevicePixels ((int)contSize.Height));
			mContainer = container;

			this.AddView (mContainer);

			var aView = new View(Context);
			aView.SetBackgroundColor(Color.Transparent);
			aView.LayoutParameters = new ViewGroup.LayoutParams (Context.ToDevicePixels ((int)contSize.Width), Context.ToDevicePixels ((int)contSize.Height));
			mContainer.AddView(aView);

			DrawViews();

		}

		/// <summary>
		/// Cleans up views.
		/// </summary>
		internal void CleanUpViews ()
		{
			if (mContainer != null)
				mContainer.RemoveAllViews();

			this.RemoveAllViews();

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


			for (int i = mRowStart; i < mParentGrid.Processor.NumberOfRows; i++)
			{
				//var height = (i == 0 && mParentGrid.Theme.HeaderStyle != GridHeaderStyle.None) ? mParentGrid.Theme.HeaderHeight : mParentGrid.Processor.RowHeight;
				//var top = mParentGrid.Processor.TopYForRow (i);

				availDefs.Add (i);
				var aRow = mParentGrid.Processor.Rows.ViewForRowIndex (i);

				if (aRow != null)
					rowsToKeep.Add (aRow);


				//				if (ShouldShowRow (top, height))
				//				{
				//					availDefs.Add (i);
				//					var aRow = Processor.Rows.ViewForRowIndex (i);
				//
				//					if (aRow != null)
				//						rowsToKeep.Add (aRow);
				//
				//				}
				//				else
				//				{
				//					if (IsAboveScreen (top, height))
				//					{
				//						continue;
				//					}
				//					else if (IsBelowScreen (top, height))
				//					{
				//						lastIndex = i;
				//						break;
				//					}
				//				}


			}

			if (rowsToKeep.Count != 0)
				mRowStart = (lastIndex - rowsToKeep.Count) - 5;

			//find the rows that are no longer need on screen
			var freeRows = (from freeRow in mParentGrid.Processor.Rows
				where !rowsToKeep.Contains (freeRow)
				select freeRow).ToList ();

			//remove them from the screen and add them to the free rows screen
			foreach (var item in freeRows)
			{
				mContainer.RemoveView (item as DSGridViewContainer);
				mParentGrid.Processor.FreeRows.Add (item);
			}

			mParentGrid.Processor.Rows.Clear ();

			mParentGrid.Processor.Rows = rowsToKeep;
			rowsToKeep = null;

			foreach (var index in availDefs)
			{
				//var aRow = Processor.FindViewForRow (index);
				var aRow = mParentGrid.Processor.FindViewForRow (index, (Index)=>
				{
					return new DSGridRowView (Index, this.mParentGrid);
				}) as DSGridRowView;

				var height = (index == 0 && mParentGrid.Theme.HeaderStyle != GridHeaderStyle.None) ? mParentGrid.Theme.HeaderHeight : mParentGrid.Processor.RowHeight;
				var top = mParentGrid.Processor.TopYForRow (index);
				//aRow.Columns = ColDefs;
				//aRow.Frame = new RectangleF (0, top, this.ContentSize.Width, height).Integral ();

				var lp = new DSAbsoluteLayout.DSAbsoluteLayoutParams (LayoutParams.FillParent, Context.ToDevicePixels ((int)height), 0,  Context.ToDevicePixels ((int)top));
				aRow.LayoutParameters = lp;

				if (aRow.Parent == null)
				{
					aRow.Initialize();

					mContainer.AddView (aRow, 0);
				}
			}

			//clear unused rows
			availDefs.Clear ();
			mParentGrid.Processor.FreeRows.Dispose ();

		}

			
		/// <summary>
		/// Determines whether this instance is above screen the specified aTop aHeight.
		/// </summary>
		/// <returns><c>true</c> if this instance is above screen the specified aTop aHeight; otherwise, <c>false</c>.</returns>
		/// <param name="aTop">A top.</param>
		/// <param name="aHeight">A height.</param>
		private bool IsAboveScreen (float aTop, float aHeight)
		{
			var cache = 10 * mParentGrid.Processor.RowHeight;
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
			var cache = 10 * mParentGrid.Processor.RowHeight;
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


		#endregion

		#region Overrides
					
		/// <Docs>Current horizontal scroll origin.</Docs>
		/// <param name="oldl">Previous horizontal scroll origin.</param>
		/// <summary>
		/// This is called in response to an internal scroll in this view (i.e., the
		///  view scrolled its own contents).
		/// </summary>
		/// <param name="l">L.</param>
		/// <param name="t">T.</param>
		/// <param name="oldt">Oldt.</param>
		protected override void OnScrollChanged (int l, int t, int oldl, int oldt)
		{

			if (t != oldt)
			{

				//this.RequestLayout ();
				//hasDrawn = false;



			}

			var dsPoint = new DSPoint(l,t);

			OnDidScroll(this, dsPoint);

			base.OnScrollChanged (l, t, oldl, oldt);
		}

		#endregion

	}
}

