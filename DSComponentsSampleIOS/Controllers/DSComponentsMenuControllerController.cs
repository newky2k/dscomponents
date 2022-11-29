// ****************************************************************************
// <copyright file="DSComponentsMenuControllerController.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Drawing;

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

namespace DSComponentsSample.Controllers
{
	public class DSComponentsMenuControllerController : UITableViewController
	{
		public DSComponentsMenuControllerController () : base (UITableViewStyle.Grouped)
		{
			this.Title = "DSComponents";
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Register the TableView's data source
			TableView.Source = new DSComponentsMenuControllerSource (this);
		}
	}
}

