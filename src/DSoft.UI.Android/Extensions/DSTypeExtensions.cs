// ****************************************************************************
// <copyright file="DSTypeExtensions.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Types;
using Android.Graphics.Drawables;
using System.Drawing;
using Android.Graphics;
using System.IO;

/// <summary>
/// Extension for DSTypes
/// </summary>
public static class DSTypeExtensions
{
	/// <summary>
	/// Converts the DSColor object to a Android.Graphics.Color object
	/// </summary>
	/// <returns></returns>
	/// <param name="Item"></param>
	public static Android.Graphics.Color ToAndroidColor(this DSColor Item)
	{		
		return new Android.Graphics.Color (Item.RedValue, Item.GreenValue, Item.BlueValue, Item.AlphaValue);
	}

	/// <summary>
	/// Converts the DSColor object to a Android.Graphics.Drawables.ColorDrawable object
	/// </summary>
	/// <returns>The android color drawable.</returns>
	/// <param name="Item">Item.</param>
	public static ColorDrawable ToAndroidColorDrawable(this DSColor Item)
	{
		var aColor = Item.ToAndroidColor ();

		return new ColorDrawable (aColor);
	}

	/// <summary>
	/// Converts to DSRectangle
	/// </summary>
	/// <returns>The DS rectangle.</returns>
	/// <param name="Frame">Frame.</param>
	public static DSRectangle ToDSRectangle(this RectangleF Frame)
	{
		return new DSRectangle (Frame.X, Frame.Y, Frame.Width, Frame.Height);
	}

	/// <summary>
	/// Converts to RectangleF
	/// </summary>
	/// <returns>The rectangle f.</returns>
	/// <param name="Frame">Frame.</param>
	public static RectangleF ToRectangleF(this DSRectangle Frame)
	{
		return new RectangleF (Frame.X, Frame.Y, Frame.Width, Frame.Height);
	}

	/// <summary>
	/// convert android bitmap to DSBitmap
	/// </summary>
	/// <returns>The DS bitmap.</returns>
	/// <param name="bmp">Bmp.</param>
	public static DSBitmap ToDSBitmap(this Bitmap bmp)
	{
		var stream = new MemoryStream();

		bmp.Compress(Bitmap.CompressFormat.Png, 100, stream);

		byte[] byteArray = stream.ToArray();

		var newBitmap = new DSBitmap(byteArray);
		return newBitmap;
	}

	/// <summary>
	/// Convert to android bitmap
	/// </summary>
	/// <returns>The bitmap.</returns>
	/// <param name="bmp">Bmp.</param>
	public static Bitmap ToBitmap(this DSBitmap bmp)
	{
		return BitmapFactory.DecodeByteArray(bmp.ImageData, 0, bmp.ImageData.Length);
	}
}

