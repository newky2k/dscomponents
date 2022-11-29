// ****************************************************************************
// <copyright file="ItunesTheme.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Themes.Grid;
using DSoft.Datatypes.Types;
using DSoft.Datatypes.Enums;

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

namespace DSComponentsSample.Themes.Grid
{
	public class ItunesTheme : DSGridDefaultTheme
	{
		public ItunesTheme () : base ()
		{
			//set default values
			HeaderBackground = DSColor.FromPatternImage (new UIImage ("header.png").ToDSBitmap ());
			HeaderHeight = 22.0f;
			HeaderTextForeground = DSColor.DarkGray;
			HeaderTextFont = DSFont.BoldFontOfSize (14.0f);

			RowHeight = 24.0f;
			CellTextFont = DSFont.NormalFontOfSize (14.0f);
			CellBorderStyle = BorderStyle.HorizontalOnly;

		}
	}
}

