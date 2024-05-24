// ****************************************************************************
// <copyright file="UIColorExtension.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Types;

#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;

using CGRect = global::System.Drawing.RectangleF;
using CGPoint = global::System.Drawing.PointF;
using CGSize = global::System.Drawing.SizeF;
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;
#endif

/// <summary>
/// UIColor extension.
/// </summary>
public static class UIColorExtension
{
	/// <summary>
	/// Converts a DSColor to a UIColor object
	/// </summary>
	/// <returns>The color from DS color.</returns>
	/// <param name="aColor">A color.</param>
	public static UIColor ToUIColor(this DSColor aColor)
	{
		// if a pattern image has been set then create a pattern color from it
		if (aColor.PatternImage != null)
		{
			return UIColor.FromPatternImage (aColor.PatternImage.ToUIImage ());
		}

		var aRed = (float)aColor.RedValue / 255.0f;
		var aGreen = (float)aColor.GreenValue / 255.0f;
		var aBlue = (float)aColor.BlueValue / 255.0f;
		var aAlpha = (float)aColor.AlphaValue / 255.0f;

		return new UIColor(aRed,aGreen,aBlue,aAlpha);
	}

	/// <summary>
	/// Converts from a UIColor to a DSColor object
	/// </summary>
	/// <returns>The DS color.</returns>
	/// <param name="aColor">A color.</param>
	public static DSColor ToDSColor(this UIColor aColor)
	{

		nfloat red = 0.0f;
		nfloat blue = 0.0f;
		nfloat green = 0.0f;
		nfloat alpha = 0.0f;

		aColor.GetRGBA(out red,out green,out blue,out alpha);

		var aNewColor = new DSColor((float)red,(float)green,(float)blue,(float)alpha);

		return aNewColor;
	}
}

