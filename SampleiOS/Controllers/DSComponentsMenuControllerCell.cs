// ****************************************************************************
// <copyright file="DSComponentsMenuControllerCell.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using UIKit;
using CoreGraphics;
using Foundation;


namespace DSComponentsSample.Controllers
{
    public class DSComponentsMenuControllerCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("DSComponentsMenuControllerCell");

        public DSComponentsMenuControllerCell() : base(UITableViewCellStyle.Default, Key)
        {
            // TODO: add subviews to the ContentView, set various colors, etc.
            TextLabel.Text = "TextLabel";
        }
    }
}

