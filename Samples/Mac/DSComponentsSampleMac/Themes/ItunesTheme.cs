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
using AppKit;
using DSoft.UI.Mac.Extensions;

namespace DSComponentsSampleMac.Themes
{
	public class ItunesTheme : DSGridDefaultTheme
	{
		public ItunesTheme () : base ()
		{
			//set default values
			HeaderBackground = DSColor.FromPatternImage (new NSImage ("header.png").ToDSBitmap ());
			HeaderHeight = 22.0f;
			HeaderTextForeground = DSColor.DarkGray;
			HeaderTextFont = DSFont.BoldFontOfSize (14.0f);

			RowHeight = 24.0f;
			CellTextFont = DSFont.NormalFontOfSize (14.0f);
			CellBorderStyle = BorderStyle.HorizontalOnly;

		}
	}
}

