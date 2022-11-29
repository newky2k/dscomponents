// ****************************************************************************
// <copyright file="NSViewExtensions.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using AppKit;

namespace DSoft.UI.Mac.Extensions
{
	internal static class NSViewExtensions
	{
		internal static void SetNeedsDisplay(this NSView view)
		{
			view.SetNeedsDisplayInRect(view.Frame);
		}
	}
}

