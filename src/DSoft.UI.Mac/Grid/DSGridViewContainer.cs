// ****************************************************************************
// <copyright file="DSGridViewContainer.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using AppKit;
using CoreGraphics;

namespace DSoft.UI.Mac.Grid
{
	public class DSGridViewContainer : NSView
	{
		public DSGridViewContainer()
		{
			Setup();
		}

		public DSGridViewContainer(CGRect Frame) : base (Frame)
		{
			Setup ();
		}

		public DSGridViewContainer(IntPtr handle) : base (handle)
		{
			Setup ();
		}

		private void Setup()
		{

		}

		public override void DrawRect(CGRect dirtyRect)
		{
			var context = NSGraphicsContext.CurrentContext.GraphicsPort;
			context.SetFillColor(NSColor.Red.CGColor); //White
			context.FillRect (dirtyRect);

		}
	}
}

