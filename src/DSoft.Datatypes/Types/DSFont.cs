// ****************************************************************************
// <copyright file="DSFont.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Enums;

namespace DSoft.Datatypes.Types
{	
	/// <summary>
	/// Cross platform font class
	/// </summary>
	public class DSFont
	{
		#region Properties
		
		/// <summary>
		/// Gets or sets the size of the font.
		/// </summary>
		/// <value>The size of the font.</value>
		public float FontSize {get; set;}
		
		/// <summary>
		/// Gets or sets the font family.
		/// </summary>
		/// <value>The font family.</value>
		public string FontFamily {get; set;}
		
		/// <summary>
		/// Gets or sets the font weight.
		/// </summary>
		/// <value>The font weight.</value>
		public FontWeight FontWeight {get; set;}
		
		#endregion
	
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSFont"/> class.
		/// </summary>
		public DSFont ()
		{
		
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSFont"/> class.
		/// </summary>
		/// <param name="FontFamily">Font family.</param>
		/// <param name="FontSize">Font size.</param>
		/// <param name="FontWeight">Font weight.</param>
		public DSFont (String FontFamily, float FontSize, FontWeight FontWeight)
		{
			this.FontFamily = FontFamily;
			this.FontSize = FontSize;
			this.FontWeight = FontWeight;
			
		}
		#endregion

		#region Static Methods

		/// <summary>
		/// Normals the size of the font of.
		/// </summary>
		/// <returns>The font of size.</returns>
		/// <param name="Size">Size.</param>
		public static DSFont NormalFontOfSize(float Size)
		{
			var font = new DSFont ();

			font.FontWeight = FontWeight.Normal;
			font.FontSize = Size;
			return font;
		}

		/// <summary>
		/// Bolds the size of the font of.
		/// </summary>
		/// <returns>The font of size.</returns>
		/// <param name="Size">Size.</param>
		public static DSFont BoldFontOfSize(float Size)
		{
			var font = new DSFont ();

			font.FontWeight = FontWeight.Bold;
			font.FontSize = Size;
			return font;
		}
		#endregion

	}
}

