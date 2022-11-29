// ****************************************************************************
// <copyright file="DSGridViewActivity.cs" company="DSoft Developments">
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
using Android.Views;
using Android.Widget;
using DSoft.Datatypes.Grid;
using DSoft.Datatypes.Grid.Data.Interfaces;
using DSoft.UI.Grid.Views;

namespace DSoft.UI.Grid
{
	/// <summary>
	/// Activity with built in DSGridView
	/// </summary>
	[Activity (Label = "DSGridViewActivity")]			
	public class DSGridViewActivity : Activity
	{
		#region Fields

		private IDSDataGridView mGridView;
		//private bool mShowSelection;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the grid view.
		/// </summary>
		/// <value>The grid view.</value>
		public IDSDataGridView GridView {
			get
			{
				if (mGridView == null)
					throw new Exception ("Grid View has not yet been created");

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
				if (mGridView.Processor.DataSource == null)
					throw new Exception ("No Datasource set for this instance of DSGridViewController");

				return mGridView.Processor.DataSource;
			}
			set
			{
				if (mGridView != null)
				{
					mGridView.Processor.DataSource = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the name of the table.
		/// </summary>
		/// <value>The name of the table.</value>
		public string TableName {
			get
			{
				return GridView.Processor.TableName;
			}
			set
			{
				GridView.Processor.TableName = value;
			}
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Raises the create event.
		/// </summary>
		/// <param name="bundle">Bundle.</param>
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			PrepareGridView ();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Prepares the grid view.
		/// </summary>
		private void PrepareGridView ()
		{
			//this.View.BackgroundColor = UIColor.Clear;
			var aGridView = new DSGridView (this);
			aGridView.LayoutParameters = new ViewGroup.LayoutParams (FrameLayout.LayoutParams.FillParent, FrameLayout.LayoutParams.FillParent);

			mGridView = aGridView;
			this.SetContentView (aGridView);
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

