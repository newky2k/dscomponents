// ****************************************************************************
// <copyright file="DSTypeExtensions.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Types;
using DSoft.Datatypes.Enums;
using System.IO;

#if __UNIFIED__
using UIKit;
using CoreGraphics;
using Foundation;
#else
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;

using System.Drawing;
using CGRect = global::System.Drawing.RectangleF;
using CGPoint = global::System.Drawing.PointF;
using CGSize = global::System.Drawing.SizeF;
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;
#endif


/// <summary>
/// Extensions for DS types
/// </summary>
public static class DSTypeExtensions
{
	/// <summary>
	/// Convert to sizef object
	/// </summary>
	/// <returns>SizeF</returns>
	/// <param name="Object">DSSize Object</param>
	public static CGSize ToSizeF(this DSSize Object)
	{
		return new CGSize ((nfloat)Object.Width, (nfloat)Object.Height);
	}

	/// <summary>
	/// Convert to a DSFont object
	/// </summary>
	/// <returns>The DS font.</returns>
	/// <param name="Font">Font.</param>
	public static DSFont ToDSFont(this UIFont Font)
	{
		var aFont = new DSFont (Font.FamilyName, (float)Font.PointSize, FontWeight.Normal);

		return aFont;
	}

	/// <summary>
	/// Convert to a UIFont object
	/// </summary>
	/// <returns>The user interface font.</returns>
	/// <param name="Font">Font.</param>
	public static UIFont ToUIFont(this DSFont Font)
	{
		if (String.IsNullOrWhiteSpace (Font.FontFamily)) 
		{
			return (Font.FontWeight == FontWeight.Normal) ? UIFont.SystemFontOfSize (Font.FontSize) : UIFont.BoldSystemFontOfSize (Font.FontSize);
		}

		return UIFont.FromName (Font.FontFamily, Font.FontSize);

	}

	/// <summary>
	/// Converts to a UIImage 
	/// </summary>
	/// <returns>The user interface image.</returns>
	/// <param name="Image">Image.</param>
	public static UIImage ToUIImage(this DSBitmap Image)
	{
		if (Image.ImageData != null || Image.ImageData.Length != 0) 
		{
			var imageData = NSData.FromArray(Image.ImageData);

			var theImage = UIImage.LoadFromData(imageData, UIScreen.MainScreen.Scale);

			return theImage;
		}

		return null;
	}

	/// <summary>
	/// Tos the DS bitmap.
	/// </summary>
	/// <returns>The DS bitmap.</returns>
	/// <param name="Image">Image.</param>
	public static DSBitmap ToDSBitmap(this UIImage Image)
	{
		var aMem = new MemoryStream ();
		Image.AsPNG ().AsStream ().CopyTo (aMem);

		return new DSBitmap (aMem.ToArray());
	}

	/// <summary>
	/// Converts to DSRectangle
	/// </summary>
	/// <returns>The DS rectangle.</returns>
	/// <param name="Frame">Frame.</param>
	public static DSRectangle ToDSRectangle(this CGRect Frame)
	{
		return new DSRectangle ((float)Frame.X, (float)Frame.Y, (float)Frame.Width, (float)Frame.Height);
	}

	/// <summary>
	/// Converts to RectangleF
	/// </summary>
	/// <returns>The rectangle f.</returns>
	/// <param name="Frame">Frame.</param>
	public static CGRect ToRectangleF(this DSRectangle Frame)
	{
		return new CGRect (Frame.X, Frame.Y, Frame.Width, Frame.Height);
	}
}

