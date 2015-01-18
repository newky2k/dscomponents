// ****************************************************************************
// <copyright file="DSColor.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Globalization;

namespace DSoft.Datatypes.Types
{
	/// <summary>
	/// Cross platform color representation class
	/// </summary>
	public class DSColor
	{
		#region Public Properties
	
		/// <summary>
		/// Red value
		/// </summary>
		public int RedValue { get; set; }
		
		/// <summary>
		/// Green value
		/// </summary>
		public int GreenValue { get; set; }
		
		/// <summary>
		/// Blue value
		/// </summary>
		public int BlueValue { get; set; }
		
		/// <summary>
		/// Alpha value
		/// </summary>
		public int AlphaValue { get; set; }

		/// <summary>
		/// Gets or sets the pattern image.
		/// </summary>
		/// <value>The pattern image.</value>
		public DSBitmap PatternImage {get; set;}
		#endregion
			
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSColor"/> class.
		/// </summary>
		public DSColor ()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSColor"/> class.
		/// </summary>
		/// <param name="Red">Red 0 - 1.0</param>
		/// <param name="Green">Green 0 - 1.0</param>
		/// <param name="Blue">Blue 0 - 1.0</param>
		/// <param name="Alpha">Alpha 0 - 1.0</param>
		public DSColor (float Red, float Green, float Blue, float Alpha)
		{
			this.RedValue = (int)(255 * Red);
			this.GreenValue = (int)(255 * Green);
			this.BlueValue = (int)(255 * Blue);
			this.AlphaValue = (int)(255 * Alpha);
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSColor"/> class.
		/// </summary>
		/// <param name="Red">Red 0 - 1.0</param>
		/// <param name="Green">Green 0 - 1.0</param>
		/// <param name="Blue">Blue 0 - 1.0</param>
		public DSColor (float Red, float Green, float Blue) 
			: this(Red,Green,Blue,1.0f)
		{
			
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSColor"/> class.
		/// </summary>
		/// <param name="Red">Red 0-255</param>
		/// <param name="Green">Green 0-255</param>
		/// <param name="Blue">Blue 0-255</param>
		/// <param name="Alpha">Alpha 0-255</param>
		public DSColor (int Red, int Green, int Blue, int Alpha)
		{
			this.RedValue = Red;
			this.GreenValue = Green;
			this.BlueValue = Blue;
			this.AlphaValue = Alpha;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSColor"/> class.
		/// </summary>
		/// <param name="Hex">Hex value - 6 digits(RGB) or 8(ARGB)</param>
		public DSColor (String Hex)
		{
			if (!Hex.StartsWith ("#"))throw new Exception ("Invalid Hex format");

			var newString = Hex.Replace ("#", "");

			//get the length
			var length = newString.Length;

			int alpha, red, green, blue = 0;

			if (length == 8) 
			{
				//includes alpha
				var alphaString = newString.Substring (0, 2);
				var redString = newString.Substring (2, 2);
				var greenString = newString.Substring (4, 2);
				var blueString = newString.Substring (6, 2);

				alpha = int.Parse(alphaString, System.Globalization.NumberStyles.HexNumber);
				red = int.Parse(redString, System.Globalization.NumberStyles.HexNumber);
				green = int.Parse(greenString, System.Globalization.NumberStyles.HexNumber);
				blue = int.Parse(blueString, System.Globalization.NumberStyles.HexNumber);

			} else if (length == 6) 
			{
				//no alpha
				alpha = 255;
				var redString = newString.Substring (0, 2);
				var greenString = newString.Substring (2, 2);
				var blueString = newString.Substring (4, 2);

				red = int.Parse(redString, System.Globalization.NumberStyles.HexNumber);
				green = int.Parse(greenString, System.Globalization.NumberStyles.HexNumber);
				blue = int.Parse(blueString, System.Globalization.NumberStyles.HexNumber);
			} 
			else 
			{
				throw new Exception ("Invalid Hex format");
			}

			this.RedValue = red;
			this.GreenValue = green;
			this.BlueValue = blue;
			this.AlphaValue = alpha;
		}
		#endregion

		#region Static Methods

		/// <summary>
		/// From an image that will be a pattern
		/// </summary>
		/// <returns>The pattern image.</returns>
		/// <param name="Image">Image.</param>
		public static DSColor FromPatternImage(DSBitmap Image)
		{
			var newColor = new DSColor ();
			newColor.PatternImage = Image;
			return newColor;
			   
		}

		#endregion

		#region Colors
		/// <summary>
		/// Red Color
		/// </summary>
		/// <value>The red.</value>
		public static DSColor Red { get { return new DSColor (1.0f, 0.0f, 0.0f, 1.0f); } }

		/// <summary>
		/// Black Color
		/// </summary>
		/// <value>The black.</value>
		public static DSColor Black { get { return new DSColor (0.0f, 0.0f, 0.0f, 1.0f); } }

		/// <summary>
		/// Blue Color
		/// </summary>
		/// <value>The blue.</value>
		public static DSColor Blue { get { return new DSColor (0.00f,0.00f,1.00f,1.00f); } }

		/// <summary>
		/// Brown Color
		/// </summary>
		/// <value>The brown.</value>
		public static DSColor Brown { get { return new DSColor (0.60f,0.40f,0.20f,1.00f); } }

		/// <summary>
		/// Clear Color
		/// </summary>
		/// <value>The clear.</value>
		public static DSColor Clear { get { return new DSColor (0.00f,0.00f,0.00f,0.00f); } }

		/// <summary>
		/// Cyan Color
		/// </summary>
		/// <value>The cyan.</value>
		public static DSColor Cyan { get { return new DSColor (0.00f,1.00f,1.00f,1.00f); } }


		/// <summary>
		/// Dark Gray Color
		/// </summary>
		/// <value>The dark gray.</value>
		public static DSColor DarkGray { get { return new DSColor (0.33f,0.33f,0.33f,1.00f); } }

		/// <summary>
		/// Gets the color of the dark text.
		/// </summary>
		/// <value>The color of the dark text.</value>
		public static DSColor DarkTextColor { get { return new DSColor (1, 0, 0, 1); } }

		/// <summary>
		/// Gray Color
		/// </summary>
		/// <value>The gray.</value>
		public static DSColor Gray { get { return new DSColor (0.50f,0.50f,0.50f,1.00f); } }

		/// <summary>
		/// Green Color
		/// </summary>
		/// <value>The green.</value>
		public static DSColor Green { get { return new DSColor (0.00f,1.00f,0.00f,1.00f); } }

		/// <summary>
		/// Light Gray Color
		/// </summary>
		/// <value>The light gray.</value>
		public static DSColor LightGray { get { return new DSColor (0.67f,0.67f,0.67f,1.00f); } }

		/// <summary>
		/// Light Text Color
		/// </summary>
		/// <value>The color of the light text.</value>
		public static DSColor LightTextColor { get { return new DSColor (1.00f,1.00f,1.00f,0.60f); } }

		/// <summary>
		/// Magenta Color
		/// </summary>
		/// <value>The magenta.</value>
		public static DSColor Magenta { get { return new DSColor (1.00f,0.00f,1.00f,1.00f); } }

		/// <summary>
		/// Orange Color
		/// </summary>
		/// <value>The orange.</value>
		public static DSColor Orange { get { return new DSColor (1.00f,0.50f,0.00f,1.00f); } }

		/// <summary>
		/// Purple Color
		/// </summary>
		/// <value>The purple.</value>
		public static DSColor Purple { get { return new DSColor (0.50f,0.00f,0.50f,1.00f); } }

		/// <summary>
		/// White Color
		/// </summary>
		/// <value>The white.</value>
		public static DSColor White { get { return new DSColor (1.0f, 1.0f, 1.0f, 1.0f); } }

		/// <summary>
		/// Yellow Color
		/// </summary>
		/// <value>The yellow.</value>
		public static DSColor Yellow { get { return new DSColor (1.00f,1.00f,0.00f,1.00f); } }

		#endregion
	}

}

