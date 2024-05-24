// ****************************************************************************
// <copyright file="DSGridTheme.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Types;

namespace DSoft.Themes.Grid
{
	/// <summary>
	/// Base Grid View Theme
	/// </summary>
	public abstract class DSGridTheme
	{

		#region Static
		private static DSGridTheme mTheme;

		/// <summary>
		/// Gets or sets the current theme
		/// </summary>
		/// <value>The current.</value>
		public static DSGridTheme Current {
			get
			{
				if (mTheme == null)
				{
					mTheme = new DSGridDefaultTheme ();
				}
				return mTheme;
			}
			set
			{
				if (mTheme != value)
				{
					mTheme = value;

					OnThemeChanged (mTheme, new EventArgs ());
				}
			}

		}

		/// <summary>
		/// Register a Theme
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static void Register<T> () where T: DSGridTheme, new()
		{
			Current = new T ();
		}

		/// <summary>
		/// Called when the theme is changed
		/// </summary>
		public static event EventHandler OnThemeChanged = delegate {};

		#endregion

		#region Grid Properties

		/// <summary>
		/// Gets the width of the grid border.
		/// </summary>
		/// <value>The width of the grid border.</value>
		public abstract float BorderWidth { get; set; }

		/// <summary>
		/// Gets the color of the grid background.
		/// </summary>
		/// <value>The color of the grid background.</value>
		public abstract DSColor BackgroundColor { get; set; }

		/// <summary>
		/// Gets the color of the grid border.
		/// </summary>
		/// <value>The color of the grid border.</value>
		public abstract DSColor BorderColor { get; set; }

		#endregion

		#region Header Properties

		/// <summary>
		/// Height of the Header Row/View
		/// </summary>
		/// <value>The height of the header.</value>
		public abstract float HeaderHeight { get; set; }

		/// <summary>
		/// Gets the header style of the grid
		/// </summary>
		/// <value>The header style.</value>
		public abstract GridHeaderStyle HeaderStyle { get; set; }

		/// <summary>
		/// Gets the color of the header.
		/// </summary>
		/// <value>The color of the header.</value>
		public abstract DSColor HeaderBackground { get; set; }

		/// <summary>
		/// Gets the color of the header text.
		/// </summary>
		/// <value>The color of the header text.</value>
		public abstract DSColor HeaderTextForeground { get; set; }

		/// <summary>
		/// Gets the cell text alignment.
		/// </summary>
		/// <value>The cell text alignment.</value>
		public abstract TextAlignment HeaderTextAlignment  { get; set; }

		/// <summary>
		/// Gets the header font.
		/// </summary>
		/// <value>The header font.</value>
		public abstract DSFont HeaderTextFont { get; set; }

		/// <summary>
		/// Gets the UIImage to use for the sort indicator when pointing up
		/// </summary>
		/// <value>UIImage</value>
		public abstract DSBitmap HeaderSortIndicatorUp { get; set; }

		/// <summary>
		/// Gets the UIImage to use for the sort indicator when pointing down
		/// </summary>
		/// <value>UIImage</value>
		public abstract DSBitmap HeaderSortIndicatorDown { get; set; }

		#endregion

		#region Cell Properties

		/// <summary>
		/// Gets the cell border style.
		/// </summary>
		/// <value>The cell border style.</value>
		public abstract BorderStyle CellBorderStyle { get; set; }

		/// <summary>
		/// Gets the width of the cell border.
		/// </summary>
		/// <value>The width of the cell border.</value>
		public abstract float CellBorderWidth { get; set; }

		/// <summary>
		/// Gets the color of the cell background.
		/// </summary>
		/// <value>The color of the cell background.</value>
		public abstract DSColor CellBackground { get; set; }

		/// <summary>
		/// Gets the color of the cell background for the alternating row
		/// </summary>
		/// <value>The color of the cell background.</value>
		public abstract DSColor CellBackground2 { get; set; }

		/// <summary>
		/// Gets the color of the cell background when the row is highlighted.
		/// </summary>
		/// <value>The color of the cell highlight.</value>
		public abstract DSColor CellBackgroundHighlight { get; set; }

		/// <summary>
		/// Gets the color of the cell text.
		/// </summary>
		/// <value>The color of the cell text.</value>
		public abstract DSColor CellTextForeground { get; set; }

		/// <summary>
		/// Gets the cell text color for alternating row
		/// </summary>
		/// <value>The cell text color2.</value>
		public abstract DSColor CellTextForeground2 { get; set; }

		/// <summary>
		/// Gets the color of the cell text when the row is highlighted.
		/// </summary>
		/// <value>The color of the cell text highlight.</value>
		public abstract DSColor CellTextHighlight { get; set; }

		/// <summary>
		/// Gets the cell text alignment.
		/// </summary>
		/// <value>The cell text alignment.</value>
		public abstract TextAlignment CellContentAlignment  { get; set; }

		/// <summary>
		/// Gets the cell font.
		/// </summary>
		/// <value>The cell font.</value>
		public abstract DSFont CellTextFont { get; set; }

		#endregion

		#region Row Properties

		/// <summary>
		/// Gets the height of the row.
		/// </summary>
		/// <value>The height of the row.</value>
		public abstract float RowHeight { get; set; }

		#endregion

	}
}

