// ****************************************************************************
// <copyright file="DSGridDefaultTheme.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Types;
using DSoft.Datatypes.Enums;

namespace DSoft.Themes.Grid
{
	/// <summary>
	/// Default DSGridView theme
	/// </summary>
	public class DSGridDefaultTheme : DSGridTheme
	{

		#region Fields

		private DSBitmap mUpSortIndicator;
		private DSBitmap mDownSortIndicator;
		private float mBorderWidth = 2.0f;
		private float mHeaderHeight = 44.0f;
		private float mCellBorderWidth = 0.5f;
		private float mRowHeight = 44.0f;
		private DSColor mBackgroundColor;
		private DSColor mBorderColor;
		private DSColor mHeaderBackground;
		private DSColor mHeaderTextForeground;
		private DSColor mCellBackground;
		private DSColor mCellBackground2;
		private DSColor mCellBackgroundHighlight;
		private DSColor mCellTextForeground;
		private DSColor mCellTextForeground2;
		private DSColor mCellTextHighlight;
		private GridHeaderStyle mHeaderStyle = GridHeaderStyle.Fixed;
		private TextAlignment mHeaderTextAlignment = TextAlignment.Left;
		private TextAlignment mCellContentAlignment = TextAlignment.Left;
		private BorderStyle mCellBorderStyle = BorderStyle.HorizontalOnly;
		private DSFont mHeaderTextFont;
		private DSFont mCellTextFont;

		#endregion

		#region Grid Properties

		/// <summary>
		/// Gets the width of the grid border.
		/// </summary>
		/// <value>The width of the grid border.</value>
		public override float BorderWidth { 
			get
			{
				return mBorderWidth;
			}
			set
			{
				mBorderWidth = value;
			}
		}

		/// <summary>
		/// Gets the color of the grid background.
		/// </summary>
		/// <value>The color of the grid background.</value>
		public override DSColor BackgroundColor { 
			get
			{
				if (mBackgroundColor == null)
				{
					mBackgroundColor = new DSColor (233.0f / 255.0f, 234.0f / 255.0f, 236.0f / 255.0f, 1);
				}
				return mBackgroundColor;
			}
			set
			{
				mBackgroundColor = value;
			}
		}

		/// <summary>
		/// Gets the color of the grid border.
		/// </summary>
		/// <value>The color of the grid border.</value>
		public override DSColor BorderColor { 
			get
			{
				if (mBorderColor == null)
				{
					mBorderColor = DSColor.LightGray;
				}
				return mBorderColor;
			}
			set
			{
				mBorderColor = value;
			}
		}

		#endregion

		#region Header Properties

		/// <summary>
		/// Gets the color of the header.
		/// </summary>
		/// <value>The color of the header.</value>
		public override DSColor HeaderBackground {
			get
			{

				if (mHeaderBackground == null)
				{
					mHeaderBackground = new DSColor (1.0f, 1.0f, 1.0f, 0.9f);
				}

				return mHeaderBackground;
			}
			set
			{
				mHeaderBackground = value;
			}
		}

		/// <summary>
		/// Gets the color of the header text.
		/// </summary>
		/// <value>The color of the header text.</value>
		public override DSColor HeaderTextForeground {
			get
			{

				if (mHeaderTextForeground == null)
				{
					mHeaderTextForeground = DSColor.LightGray;
				}

				return mHeaderTextForeground;
			}
			set
			{
				mHeaderTextForeground = value;
			}
		}

		/// <summary>
		/// Height of the Header Row/View
		/// </summary>
		/// <value>The height of the header.</value>
		public override float HeaderHeight {
			get
			{
				return mHeaderHeight;
			}
			set
			{
				mHeaderHeight = value;
			}
		}

		/// <summary>
		/// Gets the header style of the grid
		/// </summary>
		/// <value>The header style.</value>
		public override GridHeaderStyle HeaderStyle {
			get
			{
				return mHeaderStyle;
			}
			set
			{
				mHeaderStyle = value;
			}
		}

		/// <summary>
		/// Gets the cell text alignment.
		/// </summary>
		/// <value>The cell text alignment.</value>
		public override TextAlignment HeaderTextAlignment { 
			get
			{
				return mHeaderTextAlignment;
			} 
			set
			{
				mHeaderTextAlignment = value;
			}
		}

		/// <summary>
		/// Gets the header font.
		/// </summary>
		/// <value>The header font.</value>
		public override DSFont HeaderTextFont {
			get
			{
				if (mHeaderTextFont == null)
				{
					mHeaderTextFont = DSFont.BoldFontOfSize (16);
				}
				return mHeaderTextFont;
			}
			set
			{
				mHeaderTextFont = value;
			}
		}

		/// <summary>
		/// Gets the UIImage to use for the sort indicator when pointing up
		/// </summary>
		/// <value>UIImage</value>
		public override DSBitmap HeaderSortIndicatorUp {
			get
			{
				if (mUpSortIndicator == null)
				{
					mUpSortIndicator = new DSBitmap (ResourceHelper.LoadResource ("upArrow.png").ToArray ());
				}

				return mUpSortIndicator;
			}
			set
			{
				mUpSortIndicator = value;
			}
		}

		/// <summary>
		/// Gets the UIImage to use for the sort indicator when pointing down
		/// </summary>
		/// <value>UIImage</value>
		public override DSBitmap HeaderSortIndicatorDown {
			get
			{
				if (mDownSortIndicator == null)
				{
					mDownSortIndicator = new DSBitmap (ResourceHelper.LoadResource ("downArrow.png").ToArray ());
				}

				return mDownSortIndicator;
			}
			set
			{
				mDownSortIndicator = value;
			}
		}

		#endregion

		#region Cell Properties

		/// <summary>
		/// Gets the color of the cell background.
		/// </summary>
		/// <value>The color of the cell background.</value>
		public override DSColor CellBackground {
			get
			{
				if (mCellBackground == null)
					mCellBackground = DSColor.Clear;
				return mCellBackground;
			}
			set
			{
				mCellBackground = value;
			}
		}

		/// <summary>
		/// Gets the color of the cell background for the alternating row
		/// </summary>
		/// <value>The color of the cell background.</value>
		public override DSColor CellBackground2 {
			get
			{
				if (mCellBackground2 == null)
					mCellBackground2 = new DSColor (241.0f / 255.0f, 244.0f / 255.0f, 247.0f / 255.0f, 1.0f);

				return mCellBackground2;
			}
			set
			{
				mCellBackground2 = value;
			}
		}

		/// <summary>
		/// Gets the color of the cell background when the row is highlighted.
		/// </summary>
		/// <value>The color of the cell highlight.</value>
		public override DSColor CellBackgroundHighlight {
			get
			{
				if (mCellBackgroundHighlight == null)
					mCellBackgroundHighlight = new DSColor (60.0f / 255.0f, 119.0f / 255.0f, 212.0f / 255.0f, 1.0f);
				return mCellBackgroundHighlight;
			}
			set
			{
				mCellBackgroundHighlight = value;
			}
		}

		/// <summary>
		/// Gets the color of the cell text.
		/// </summary>
		/// <value>The color of the cell text.</value>
		public override DSColor CellTextForeground {
			get
			{
				if (mCellTextForeground == null)
					mCellTextForeground = DSColor.Black;
				return mCellTextForeground;
			}
			set
			{
				mCellTextForeground = value;
			}

		}

		/// <summary>
		/// Gets the cell text color for alternating row
		/// </summary>
		/// <value>The cell text color2.</value>
		public override DSColor CellTextForeground2 { 
			get
			{ 
				if (mCellTextForeground2 == null)
					mCellTextForeground2 = CellTextForeground;
				return  mCellTextForeground2; 
			}
			set
			{
				mCellTextForeground2 = value;
			}
		}

		/// <summary>
		/// Gets the color of the cell text when the row is highlighted.
		/// </summary>
		/// <value>The color of the cell text highlight.</value>
		public override DSColor CellTextHighlight {
			get
			{
				if (mCellTextHighlight == null)
					mCellTextHighlight = DSColor.White;
				return mCellTextHighlight;
			}
			set
			{
				mCellTextHighlight = value;
			}
		}

		/// <summary>
		/// Gets the cell border style.
		/// </summary>
		/// <value>The cell border style.</value>
		public override BorderStyle CellBorderStyle {
			get
			{
				return mCellBorderStyle;
			}
			set
			{
				mCellBorderStyle = value;
			}
		}

		/// <summary>
		/// Gets the width of the cell border.
		/// </summary>
		/// <value>The width of the cell border.</value>
		public override float CellBorderWidth { 
			get
			{
				return mCellBorderWidth;
			}
			set
			{
				mCellBorderWidth = value;
			}
		}

		/// <summary>
		/// Gets the cell text alignment.
		/// </summary>
		/// <value>The cell text alignment.</value>
		public override TextAlignment CellContentAlignment { 
			get
			{
				return mCellContentAlignment;
			}
			set
			{
				mCellContentAlignment = value;
			}
		}

		/// <summary>
		/// Gets the cell font.
		/// </summary>
		/// <value>The cell font.</value>
		public override DSFont CellTextFont {
			get
			{
				if (mCellTextFont == null)
					mCellTextFont = DSFont.NormalFontOfSize (16);

				return mCellTextFont; 
			}
			set
			{
				mCellTextFont = value;
			}
		}

		#endregion

		#region Row Properties

		/// <summary>
		/// Gets the height of the row.
		/// </summary>
		/// <value>The height of the row.</value>
		public override float RowHeight { 
			get
			{
				return mRowHeight;
			}
			set
			{
				mRowHeight = value;
			}
		}

		#endregion

	}
}

