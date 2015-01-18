// ****************************************************************************
// <copyright file="DSToolbarTheme.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Types;

namespace DSoft.Themes.Toolbar
{
	/// <summary>
	/// Toolbar theme
	/// </summary>
	public abstract class DSToolbarTheme
	{
		#region Static methods
		private static DSToolbarTheme mCurrentTheme;
		
		/// <summary>
		/// Gets or sets the current theme.
		/// </summary>
		/// <value>The current theme.</value>
		public static DSToolbarTheme CurrentTheme
		{
			get
			{
				if (mCurrentTheme == null)
				{
					mCurrentTheme = new DSToolbarDefaultTheme ();
				}
				return mCurrentTheme;
			}	
			set
			{
				mCurrentTheme = value;
			}
		}

		/// <summary>
		/// Register theme as the current theme
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static void Register<T>() where T : DSToolbarTheme, new() 
		{
			CurrentTheme = new T ();
		}
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets or sets the color of the toolbar
		/// </summary>
		/// <value>The color.</value>
		public abstract DSColor Color { get; set;}
		
		#endregion
	}
}

