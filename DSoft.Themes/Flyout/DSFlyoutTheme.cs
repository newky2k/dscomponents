// ****************************************************************************
// <copyright file="DSFlyoutTheme.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Types;

namespace DSoft.Themes.Flyout
{
	/// <summary>
	/// Base flyout theme
	/// </summary>
	public abstract class DSFlyoutTheme
	{
		#region Fields
		private static DSFlyoutTheme mCurrentTheme;
		#endregion
		
		#region Static properties
		/// <summary>
		/// Gets or sets the current theme.
		/// </summary>
		/// <value>The current theme.</value>
		public static DSFlyoutTheme CurrentTheme
		{
			get 
		    { 
				if (mCurrentTheme == null)
					mCurrentTheme = new DSFlyoutDefaultTheme ();
					
		        return mCurrentTheme;
		    }
		    
			set 
		    { 
		        mCurrentTheme = value;
		    }
		}

		/// <summary>
		/// Register the theme as the current theme
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static void Register<T>() where T : DSFlyoutTheme, new()
		{
			CurrentTheme = new T ();
		}
		#endregion
		
		#region properties

		#region Text
		/// <summary>
		/// Gets or sets the color of the menu text.
		/// </summary>
		/// <value>The color of the menu text.</value>
		public abstract DSColor MenuTextColor { get; set;}

		/// <summary>
		/// Gets or sets the color of the menu text when selected
		/// </summary>
		/// <value>The color of the menu selected text.</value>
		public abstract DSColor MenuSelectedTextColor { get; set;}

		/// <summary>
		/// Gets or sets the color of the menu text when its highlighted.
		/// </summary>
		/// <value>The color of the menu text highlighted.</value>
		public abstract DSColor MenuHighlightedTextColor { get; set;}

		/// <summary>
		/// Gets or sets the color of the text for a segment header
		/// </summary>
		/// <value>The color of the menu segment text.</value>
		public abstract DSColor MenuSegmentTextColor { get; set;}

		#endregion

		#region Cell
		/// <summary>
		/// Returns the color of the background menu
		/// </summary>
		/// <value>The color of the menu background.</value>
		public abstract DSColor MenuBackgroundColor { get; set;}
				
		/// <summary>
		/// Gets or sets the color of the menu item when selected.
		/// </summary>
		/// <value>The color of the menu item selected.</value>
		public abstract DSColor MenuSelectedBackgroundColor {get; set;}
		
		/// <summary>
		/// Gets or sets the color of the menu item when highlighted.
		/// </summary>
		/// <value>The color of the menu item high lighted.</value>
		public abstract DSColor MenuHighLightedBackgroundColor { get; set;}
		#endregion
		#endregion
	}
}

