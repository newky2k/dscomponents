// ****************************************************************************
// <copyright file="DSComponentsMenuControllerSource.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSComponentsSample.Controllers.Grid;

#if __UNIFIED__
using UIKit;
using CoreGraphics;
using Foundation;
#else
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using System.Drawing;

using CGRect = global::System.Drawing.RectangleF;
using CGPoint = global::System.Drawing.PointF;
using CGSize = global::System.Drawing.SizeF;
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;
#endif

namespace DSComponentsSample.Controllers
{
	public class DSComponentsMenuControllerSource : UITableViewSource
	{
		private UIViewController mParentVC;

		public DSComponentsMenuControllerSource (UIViewController ParentVC)
		{
			mParentVC = ParentVC;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			// TODO: return the actual number of sections
			return 1;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			// TODO: return the actual number of items in the section
			return 1;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (DSComponentsMenuControllerCell.Key) as DSComponentsMenuControllerCell;
			if (cell == null)
				cell = new DSComponentsMenuControllerCell ();
			
			// TODO: populate the cell with the appropriate data based on the indexPath
			cell.TextLabel.Text = "DSGridView";
			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow (indexPath, true);

			var aVC = BuildGridDemo ();

			mParentVC.NavigationController.PushViewController (aVC, true);

		}

		private UIViewController BuildGridDemo ()
		{
			var tabs = new UITabBarController ();
			tabs.Title = "DSGridView";

			var aView = new DSDemoGridViewController ();

			var aView2 = new DSDemoViewWithGridController ();

			var aView3 = new DSNetGridViewController ();

			tabs.ViewControllers = new UIViewController[]{ aView, aView2, aView3 };
		
			return tabs;
		}
	}
}

