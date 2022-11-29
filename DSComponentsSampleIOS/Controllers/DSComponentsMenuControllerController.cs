// ****************************************************************************
// <copyright file="DSComponentsMenuControllerController.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Drawing;
using UIKit;
using CoreGraphics;
using Foundation;


namespace DSComponentsSample.Controllers
{
    public class DSComponentsMenuControllerController : UITableViewController
    {
        public DSComponentsMenuControllerController() : base(UITableViewStyle.Grouped)
        {
            this.Title = "DSComponents";
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Register the TableView's data source
            TableView.Source = new DSComponentsMenuControllerSource(this);
        }
    }
}

