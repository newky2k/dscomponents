// ****************************************************************************
// <copyright file="TypeExtensions.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using MonoTouch.UIKit;
using DSoft.Datatypes.Types;

namespace DSoft.UI.Calendar.Helpers
{
	/// <summary>
	/// Functions to help convert from DSColor to UIColor and vice versa
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		/// Converts a DSColor to a UIColor object
		/// </summary>
		/// <returns>The color from DS color.</returns>
		/// <param name="aColor">A color.</param>
		public static UIColor ToUIColor(this DSColor aColor)
		{
			return new UIColor(aColor.Red,aColor.Green,aColor.Blue,aColor.Alpha);
		}
		
		/// <summary>
		/// Converts from a UIColor to a DSColor object
		/// </summary>
		/// <returns>The DS color.</returns>
		/// <param name="aColor">A color.</param>
		public static DSColor ToDSColor(this UIColor aColor)
		{
			var aNewColor = new DSColor();
			
			aColor.GetRGBA(out aNewColor.Red,out aNewColor.Green,out aNewColor.Blue,out aNewColor.Alpha);
			
			return aNewColor;
		}
	}
}

