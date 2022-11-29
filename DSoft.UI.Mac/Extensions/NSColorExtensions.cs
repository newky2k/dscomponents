// ****************************************************************************
// <copyright file="NSColorExtensions.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Types;
using AppKit;

namespace DSoft.UI.Mac.Extensions
{
	public static class NSColorExtensions
	{
		/// <summary>
		/// Converts a DSColor to a UIColor object
		/// </summary>
		/// <returns>The color from DS color.</returns>
		/// <param name="aColor">A color.</param>
		public static NSColor ToNSColor(this DSColor aColor)
		{
			// if a pattern image has been set then create a pattern color from it
			if (aColor.PatternImage != null)
			{
				return NSColor.FromPatternImage (aColor.PatternImage.ToUIImage ());
			}

			var aRed = (nfloat)aColor.RedValue / 255.0f;
			var aGreen = (nfloat)aColor.GreenValue / 255.0f;
			var aBlue = (nfloat)aColor.BlueValue / 255.0f;
			var aAlpha = (nfloat)aColor.AlphaValue / 255.0f;

			return NSColor.FromDeviceRgba(aRed,aGreen,aBlue,aAlpha);
		}

		/// <summary>
		/// Converts from a UIColor to a DSColor object
		/// </summary>
		/// <returns>The DS color.</returns>
		/// <param name="aColor">A color.</param>
		public static DSColor ToDSColor(this NSColor aColor)
		{

			nfloat red = 0.0f;
			nfloat blue = 0.0f;
			nfloat green = 0.0f;
			nfloat alpha = 0.0f;

			aColor.GetRgba(out red,out green,out blue,out alpha);

			var aNewColor = new DSColor((float)red,(float)green,(float)blue,(float)alpha);

			return aNewColor;
		}
	}
}

