// ****************************************************************************
// <copyright file="DSGridViewFragment.cs" company="DSoft Developments">
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
using DSoft.UI.Grid.Views;
using DSoft.Datatypes.Grid.Data.Interfaces;
using DSoft.Datatypes.Grid;

namespace DSoft.UI.Grid
{
	internal class DSGridViewFragment : Fragment
	{

		#region Fields

		private IDSDataGridView mGridView;
		private IDSDataSource mDatasource;
		private bool mShowSelection;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the grid view.
		/// </summary>
		/// <value>The grid view.</value>
		public IDSDataGridView GridView {
			get
			{
				return mGridView;
			}
			internal set
			{
				mGridView = value;
			}
		}

		/// <summary>
		/// DataSource of the DataGrid
		/// </summary>
		public IDSDataSource DataSource { 
			get
			{
				if (mDatasource == null)
					throw new Exception ("No Datasource set for this instance of DSGridViewController");

				return mDatasource;
			}
			set
			{
				mDatasource = value;

				if (mGridView != null)
				{
					mGridView.Processor.DataSource = mDatasource;
				}
			}
		}

		/// <summary>
		/// Highlight the selected row
		/// </summary>
		public bool ShowSelection {
			get
			{
				return mShowSelection;
			}
			set
			{
				mShowSelection = value;

				if (mGridView != null)
				{
					//mGridView.ShowSelection = value;
				}
			}

		}

		#endregion

		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}

		#region Methods

		private void PrepareGridView ()
		{
			//this.View.BackgroundColor = UIColor.Clear;
			var aGridView = new DSGridView (this.Activity);
			aGridView.LayoutParameters = new ViewGroup.LayoutParams (FrameLayout.LayoutParams.FillParent, FrameLayout.LayoutParams.FillParent);
			//mGridView.AutoresizingMask = View.AutoresizingMask;
			//mGridView.ShowsHorizontalScrollIndicator = true;
			//mGridView.ShowsVerticalScrollIndicator = true;
			//mGridView.ShowSelection = ShowSelection;
			//mGridView.Bounces = mEnableBounce;
			aGridView.DataSource = DataSource;
			//mGridView.OnSingleCellTap += OnSingleCellTap;
			//mGridView.OnDoubleCellTap += OnDoubleCellTap;
			//this.View.AddSubview(mGridView);

			mGridView = aGridView;
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Called when a Cell double tapped.
		/// </summary>
		/// <param name="sender">Sender.</param>
		public virtual void OnDoubleCellTap (object sender)
		{
			//DSGridCellView

		}

		/// <summary>
		/// Called when a Cell single tapped.
		/// </summary>
		/// <param name="sender">Sender.</param>
		public virtual void OnSingleCellTap (object sender)
		{
			//DSGridCellView

		}

		#endregion

	}
}

