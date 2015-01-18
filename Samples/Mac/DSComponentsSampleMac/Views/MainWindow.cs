// ****************************************************************************
// <copyright file="MainWindow.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

using Foundation;
using AppKit;

namespace DSComponentsSampleMac
{
	public partial class MainWindow : NSWindow
	{
		public MainWindow(IntPtr handle) : base(handle)
		{
		}

		[Export("initWithCoder:")]
		public MainWindow(NSCoder coder) : base(coder)
		{
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();
		}
	}
}
