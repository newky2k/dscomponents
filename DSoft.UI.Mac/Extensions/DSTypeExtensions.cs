// ****************************************************************************
// <copyright file="DSTypeExtensions.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Types;
using CoreGraphics;
using AppKit;
using DSoft.Datatypes.Enums;
using Foundation;
using System.IO;

namespace DSoft.UI.Mac.Extensions
{
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
		public static DSFont ToDSFont(this NSFont Font)
		{
			var aFont = new DSFont (Font.FamilyName, (float)Font.PointSize, FontWeight.Normal);

			return aFont;
		}

		/// <summary>
		/// Convert to a UIFont object
		/// </summary>
		/// <returns>The user interface font.</returns>
		/// <param name="Font">Font.</param>
		public static NSFont ToUIFont(this DSFont Font)
		{
			if (String.IsNullOrWhiteSpace (Font.FontFamily)) 
			{
				return (Font.FontWeight == FontWeight.Normal) ? NSFont.SystemFontOfSize (Font.FontSize) : NSFont.BoldSystemFontOfSize (Font.FontSize);
			}


			return NSFont.FromFontName (Font.FontFamily, Font.FontSize);

		}

		/// <summary>
		/// Converts to a UIImage 
		/// </summary>
		/// <returns>The user interface image.</returns>
		/// <param name="Image">Image.</param>
		public static NSImage ToUIImage(this DSBitmap Image)
		{
			if (Image.ImageData != null || Image.ImageData.Length != 0) 
			{
				var imageData = NSData.FromArray(Image.ImageData);

				var theImage = new NSImage(imageData);

				return theImage;
			}

			return null;
		}

		/// <summary>
		/// Tos the DS bitmap.
		/// </summary>
		/// <returns>The DS bitmap.</returns>
		/// <param name="Image">Image.</param>
		public static DSBitmap ToDSBitmap(this NSImage Image)
		{
			var aMem = new MemoryStream ();

			CGRect rect = CGRect.Empty;
			var cgImage = Image.AsCGImage(ref rect,null,null);
			var bitmap = new NSBitmapImageRep(cgImage);
			bitmap.Size = Image.Size;

			var data = bitmap.RepresentationUsingTypeProperties(NSBitmapImageFileType.Png,null);
			data.AsStream().CopyTo (aMem);

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
}

