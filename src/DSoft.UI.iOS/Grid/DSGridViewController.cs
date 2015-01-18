// ****************************************************************************
// <copyright file="DSGridViewController.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.UI.Grid.Views;
using DSoft.Datatypes.Grid.Data.Interfaces;

#if __UNIFIED__
using UIKit;
using CoreGraphics;
using Foundation;
#else
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using System.Drawing;
#endif

namespace DSoft.UI.Grid
{
	/// <summary>
	/// A View Controller with a DSGridView
	/// </summary>
	public class DSGridViewController : UIViewController
	{
		#region Fields
		private bool mShowSelection;
		private bool mEnableBounce;
		private IDSDataSource mDatasource;

		#endregion
		#region Public Properties
		
		/// <summary>
		/// DataSource of the DataGrid
		/// </summary>
		public IDSDataSource DataSource 
		{ 
			get
			{
				if (mDatasource == null) throw new Exception("No Datasource set for this instance of DSGridViewController");

				return mDatasource;
			}
			set
			{
				mDatasource = value;

				if (m_GridView != null)
				{
					m_GridView.DataSource = mDatasource;
				}
			}
		}

		/// <summary>
		/// Highlight the selected row
		/// </summary>
		public bool ShowSelection
		{
			get
			{
				return mShowSelection;
			}
			set
			{
				mShowSelection =value;

				if (m_GridView != null)
				{
					m_GridView.ShowSelection = value;
				}
			}

		}

		/// <summary>
		/// Gets the grid view.
		/// </summary>
		/// <value>The grid view.</value>
		public DSGridView GridView
		{
			get
			{
				if (m_GridView == null) throw new Exception("The GridView has not been set on this instance of DSGridViewController");
				return m_GridView;
			}
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="DSoft.UI.Grid.DSGridViewController"/> enable bounce.
		/// </summary>
		/// <value><c>true</c> if enable bounce; otherwise, <c>false</c>.</value>
		public Boolean EnableBounce 
		{
			get
			{
				return mEnableBounce;
			} 
			set
			{
				mEnableBounce = value;
				
				if (m_GridView != null)
				{
					m_GridView.Bounces = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="DSoft.UI.Grid.DSGridViewController"/> disable navigation
		/// controller sizing.
		/// </summary>
		/// <value><c>true</c> if disable navigation controller sizing; otherwise, <c>false</c>.</value>
		public Boolean DisableNavigationControllerSizing {get; set;}

		#endregion

		#region Private Properties
		
		private DSGridView m_GridView;

		#endregion
	
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.DSGridViewController"/> class.
		/// </summary>
		public DSGridViewController ()
		{
			if (iOSHelper.IsiOS7)
			{
				this.AutomaticallyAdjustsScrollViewInsets = true;
			}

			DisableNavigationControllerSizing = true;

		}
		
		#endregion
		
		#region Overrides
		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			PrepareGridView ();

		}
		
		/// <summary>
		/// Views the did appear.
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
			
			m_GridView.ReloadData();

		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			if (!DisableNavigationControllerSizing)
			{
				if (iOSHelper.IsiOS7)
				{
					UpdateGridFromNavigationController();
				}
			}


		}

		public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillRotate(toInterfaceOrientation, duration);

			if (!DisableNavigationControllerSizing)
			{

				if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone && iOSHelper.IsiOS7)
				{
					if (toInterfaceOrientation == UIInterfaceOrientation.Portrait)
					{
						UpdateGridFrame(64);
					}
					else
					{
						var value = (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) ? 52 : 64;

						UpdateGridFrame(value);
					}
				}
			}

		}

		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate(fromInterfaceOrientation);

			if (!DisableNavigationControllerSizing)
			{
				if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone && iOSHelper.IsiOS7)
				{
					UpdateGridFromNavigationController();
				}
			}

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

		#region Private Functions
		/// <summary>
		/// Prepares the grid view.
		/// </summary>
		private void PrepareGridView ()
		{
			this.View.BackgroundColor = UIColor.Clear;
			m_GridView = new DSGridView();
			m_GridView.Frame = this.View.Bounds;
			m_GridView.AutoresizingMask = View.AutoresizingMask;
			m_GridView.ShowsHorizontalScrollIndicator = true;
			m_GridView.ShowsVerticalScrollIndicator = true;
			m_GridView.ShowSelection = ShowSelection;
			m_GridView.Bounces = mEnableBounce;
			m_GridView.DataSource = DataSource;
			m_GridView.OnSingleCellTap += OnSingleCellTap;
			m_GridView.OnDoubleCellTap += OnDoubleCellTap;
			this.View.AddSubview(m_GridView);
		}
			
		/// <summary>
		/// Updates the grid from navigation controller.
		/// </summary>
		private void UpdateGridFromNavigationController()
		{
			if (this.NavigationController != null)
			{
				var statusbar = 20.0f;

				var navController = this.NavigationController;
				var navControllerHieght = navController.Toolbar.Frame.Size.Height;
				var tbBarHeight = statusbar + navControllerHieght;

				UpdateGridFrame((float)tbBarHeight);

			}
		}

		/// <summary>
		/// Updates the grid frame.
		/// </summary>
		/// <param name="inset">Inset.</param>
		private void UpdateGridFrame(float inset)
		{
			var aFrame = this.View.Bounds;
			aFrame.Y += inset;
			aFrame.Height -= inset;

			m_GridView.Frame = aFrame;

		}

		#endregion
	}
}

