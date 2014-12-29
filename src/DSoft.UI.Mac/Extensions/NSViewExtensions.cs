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

