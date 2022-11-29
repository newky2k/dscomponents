// ****************************************************************************
// <copyright file="DSDemoViewWithGridController.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.UI.Grid;
using DSComponentsSample.Data.Grid;
using DSoft.Datatypes.Grid.Data;
using DSoft.Themes.Grid;
using DSComponentsSample.Themes.Grid;

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

namespace DSComponentsSample.Controllers.Grid
{
	public class DSDemoViewWithGridController : UIViewController
	{
		private DSGridView mGridView;

		public DSDemoViewWithGridController () : base ()
		{
			this.Title = "UIViewController with Grid";
			this.TabBarItem.Image = UIImage.FromBundle ("second");

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.View.BackgroundColor = UIColor.White;

			var aFrame = this.View.Frame;

			aFrame.Height -= 50;

			if (iOSHelper.IsiOS7)
			{
				aFrame.Y += 64;
				aFrame.Height -= 64;
			}
			else
			{
				aFrame.Y = 0;
				aFrame.Height -= 44;
			}



			aFrame.Inflate (-5, -5);

			mGridView = new DSGridView (aFrame);

			//turn on showing of the selection
			mGridView.ShowSelection = true;

			//allow the scrolling to bounce
			mGridView.Bounces = false;

			//set the data source to be a DataSet with multiple datatables
			mGridView.DataSource = new ExampleDataSet ();

			//set the first database as the initial grid source
			mGridView.TableName = ((DSDataSet)mGridView.DataSource).Tables [1].Name;

			mGridView.Layer.CornerRadius = 5.0f;

			//set a theme on the control itself so that it doesn't use the global theme
			mGridView.Theme = new ItunesTheme ();

			this.View.Add (mGridView);

			         
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			if (this.ParentViewController.NavigationController != null)
			{
				var aButton = new UIBarButtonItem (UIBarButtonSystemItem.Action);

				aButton.Clicked += (object sender, EventArgs e) => {
					var alert = new UIActionSheet ("Switch DataSets");

					alert.AddButton ("Example 1");
					alert.AddButton ("Example 2");
					alert.AddButton ("Cancel");
					alert.CancelButtonIndex = 2;
					alert.Clicked += (object action, UIButtonEventArgs e2) => {	

						var curName = mGridView.TableName;
						var newName = String.Empty;

						switch (e2.ButtonIndex)
						{
							case 0:
								{
									newName = ((DSDataSet)mGridView.DataSource).Tables [0].Name;
								}
								break;
							case 1:
								{
									newName = ((DSDataSet)mGridView.DataSource).Tables [1].Name;
								}
								break;
						}

						if (String.IsNullOrWhiteSpace (newName))
							return;
						//check if the new name is different to the old name
						if (curName != newName)
						{
							mGridView.TableName = newName;
							mGridView.ReloadData ();
						}

					};

					alert.ShowFrom ((UIBarButtonItem)sender, true);
				};

				this.ParentViewController.NavigationItem.LeftBarButtonItem = null;
				this.ParentViewController.NavigationItem.RightBarButtonItems = new UIBarButtonItem[]{ aButton };
			}


		}
	}
}

