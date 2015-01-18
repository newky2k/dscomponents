// ****************************************************************************
// <copyright file="DSDemoGridViewController.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.UI.Grid;
using DSComponentsSample.Data.Grid;
using DSoft.UI.Grid.Views;
using DSoft.Themes.Grid;
using DSoft.Datatypes.Types;
using DSComponentsSample.Themes.Grid;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Grid.Data;

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
	public class DSDemoGridViewController : DSGridViewController
	{
		public DSDemoGridViewController () : base ()
		{
			///Set the title and tab image
			this.Title = "DSGridView Controller";
			this.TabBarItem.Image = UIImage.FromBundle ("first");

			//Allow selection an bouncing
			ShowSelection = true;
			EnableBounce = true;

			//Set the datasource of the grid view
			DataSource = new ExampleDataTable ("Items");

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.View.BackgroundColor = UIColor.White;

			//Old way
//			if (iOSHelper.IsiOS7)
//			{
//				var aFrame = this.GridView.Frame;
//				aFrame.Y += 64;
//				aFrame.Height -= 64;
//
//				this.GridView.Frame = aFrame;
//			}

			//new way
			DisableNavigationControllerSizing = false;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			if (this.ParentViewController.NavigationController != null)
			{
				var aButton = new UIBarButtonItem (UIBarButtonSystemItem.Action);

				aButton.Clicked += (object sender, EventArgs e) => {
					var alert = new UIActionSheet ("Themes");

					alert.AddButton ("Default");
					alert.AddButton ("New");
					alert.AddButton ("iTunes");
					alert.AddButton ("Cancel");
					alert.CancelButtonIndex = 3;
					alert.Clicked += (object action, UIButtonEventArgs e2) => {	
						DSGridTheme newtheme = null;

						switch (e2.ButtonIndex)
						{
							case 0:
								{
									//Use default
									newtheme = new DSGridDefaultTheme ();
								}
								break;
							case 1:
								{
									//Create a theme progratically
									newtheme = new DSGridDefaultTheme () {
										HeaderStyle = GridHeaderStyle.Standard,
										HeaderHeight = 75.0f,
										CellBackground = DSColor.Black,
										CellBackground2 = DSColor.Black,
										CellTextForeground = DSColor.White,
										CellTextForeground2 = DSColor.White,
										CellBackgroundHighlight = DSColor.Gray,
										CellTextHighlight = DSColor.Red,
									};
								}
								break;
							case 2:
								{
									//Use subclass
									newtheme = new ItunesTheme ();
								}
								break;
						}

						if (newtheme != null)
						{
							//set the current theme - grid will reload
							DSGridTheme.Current = newtheme;

							//reload the grid
							//GridView.ReloadData ();
						}
					};

					alert.ShowFrom ((UIBarButtonItem)sender, true);
				};
					
				var aButton2 = new UIBarButtonItem (UIBarButtonSystemItem.Action);

				aButton2.Clicked += (object sender, EventArgs e) => {
					var alert = new UIActionSheet ("Scroll To: 30");

					alert.AddButton ("Top");
					alert.AddButton ("Middle");
					alert.AddButton ("Bottom");
					alert.AddButton ("Cancel");
					alert.CancelButtonIndex = 3;
					alert.Clicked += (object action, UIButtonEventArgs e2) => {	
					
						switch (e2.ButtonIndex)
						{
							case 0:
								{
									//top
									this.GridView.SelectRow (30, Mode: ScrollToMode.Top);
								}
								break;
							case 1:
								{
									//Middle
									this.GridView.SelectRow (30, Mode: ScrollToMode.Middle, AdditonalOffset: 54.0f);
								}
								break;
							case 2:
								{
									//Bottom
									this.GridView.SelectRow (30, Mode: ScrollToMode.Bottom, AdditonalOffset: 54.0f);
								}
								break;
						}
								
					};

					alert.ShowFrom ((UIBarButtonItem)sender, true);
				};

				this.ParentViewController.NavigationItem.RightBarButtonItems = new UIBarButtonItem[] { aButton, aButton2 };

				var leftButton = new UIBarButtonItem (UIBarButtonSystemItem.Action);
				leftButton.Clicked += (object sender, EventArgs e) => {
					var alert = new UIActionSheet ("Options");

					var mutliSelectText = (GridView.EnableMulitSelect) ? "Disable Multi-select" : "Enable Multi-Select";
					var deselect = (GridView.EnableDeselection) ? "Disbale Deselection" : "Enable Deselection";

					alert.AddButton (mutliSelectText);
					alert.AddButton (deselect);
					alert.Add("Update Value");

					alert.AddButton ("Cancel");

					alert.CancelButtonIndex = 2;

					alert.Clicked += (object action, UIButtonEventArgs e2) => {	

						switch (e2.ButtonIndex)
						{
							case 0:
								{
									//enable/disable multi-select
									GridView.EnableMulitSelect = !GridView.EnableMulitSelect;
								}
								break;
							case 1:
								{
									//enable/disable deselection
									GridView.EnableDeselection = !GridView.EnableDeselection;
								}
								break;
							case 2:
								{
									//enable/disable deselection
									var dt = GridView.DataSource as DSDataTable;

									var dr = dt.GetRow(0);

									dr["Title"] = "Dude!";
								}
								break;
						}
					};

					alert.ShowFrom ((UIBarButtonItem)sender, true);

				};

				this.ParentViewController.NavigationItem.LeftItemsSupplementBackButton = true;
				this.ParentViewController.NavigationItem.LeftBarButtonItem = leftButton;
			}


		}

		public override void OnDoubleCellTap (object sender)
		{
			var aSender = (DSGridCellView)sender;
			var alert = new UIAlertView ("Double Tap", String.Format ("{0}:{1}", aSender.Processor.RowIndex.ToString (), aSender.Processor.ColumnIndex.ToString ()), null, "OK", null);

			alert.Show ();
		}

		public override void OnSingleCellTap (object sender)
		{
			var aSender = (DSGridCellView)sender;
			var alert = new UIAlertView ("Single Tap", String.Format ("{0}:{1}", aSender.Processor.RowIndex.ToString (), aSender.Processor.ColumnIndex.ToString ()), null, "OK", null);

			alert.Show ();

		}
	}
}

