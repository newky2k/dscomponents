// ****************************************************************************
// <copyright file="DSComponentsMenuControllerCell.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

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
	public class DSComponentsMenuControllerCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("DSComponentsMenuControllerCell");

		public DSComponentsMenuControllerCell () : base (UITableViewCellStyle.Default, Key)
		{
			// TODO: add subviews to the ContentView, set various colors, etc.
			TextLabel.Text = "TextLabel";
		}
	}
}

