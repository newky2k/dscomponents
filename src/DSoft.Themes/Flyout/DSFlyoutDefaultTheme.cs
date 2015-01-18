// ****************************************************************************
// <copyright file="DSFlyoutDefaultTheme.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Types;

namespace DSoft.Themes.Flyout
{
	/// <summary>
	/// Flyout Theme
	/// </summary>
	public class DSFlyoutDefaultTheme : DSFlyoutTheme
	{
		#region Fields
		private DSColor mMenuBackgroundColor;
		private DSColor mMenuTextColor;
		private DSColor mMenuHighlightedTextColor;
		private DSColor mMenuSelectedTextColor;
		private DSColor mMenuSegmentTextColor;
		private DSColor mMenuItemSelectedColor;
		private DSColor mMenuItemHighlightedColor;
		#endregion
		
		#region Properties

		#region Text
		/// <summary>
		/// Gets or sets the color of the menu text.
		/// </summary>
		/// <value>The color of the menu text.</value>
		public override DSColor MenuTextColor
		{
			get
			{
				if (mMenuTextColor == null)
				{
					mMenuTextColor = new DSColor (1.0f, 1.0f, 1.0f, 1.0f);
				}
				return mMenuTextColor;
			}
			set
			{
				mMenuTextColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the color of the menu text when selected
		/// </summary>
		/// <value>The color of the menu selected text.</value>
		public override DSColor MenuSelectedTextColor
		{
			get
			{
				if (mMenuSelectedTextColor == null)
				{
					mMenuSelectedTextColor = MenuTextColor;//new DSColor (0.0f, 1.0f, 0.0f, 1.0f);
				}
				return mMenuSelectedTextColor;
			}
			set
			{
				mMenuSelectedTextColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the color of the menu text when its highlighted.
		/// </summary>
		/// <value>The color of the menu text highlighted.</value>
		public override DSColor MenuHighlightedTextColor 
		{ 
			get 
			{
				if (mMenuHighlightedTextColor == null) 
				{
					mMenuHighlightedTextColor = MenuTextColor;//new DSColor (0.0f, 0.0f, 0.0f, 1.0f);
				}

				return mMenuHighlightedTextColor;
			}
			set 
			{
				mMenuHighlightedTextColor = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the color of the text for a segment header
		/// </summary>
		/// <value>The color of the menu segment text.</value>
		public override DSColor MenuSegmentTextColor
		{
			get
			{
				if (mMenuSegmentTextColor == null)
				{
					mMenuSegmentTextColor = new DSColor ("#7F8287");
				}
				return mMenuSegmentTextColor;
			}
			set
			{
				mMenuSegmentTextColor = value;
			}
		}

		#endregion

		#region Cell
		/// <summary>
		/// Returns the color of the background menu
		/// </summary>
		/// <value>The color of the menu background.</value>
		public override DSColor MenuBackgroundColor
		{
			get
			{ 
				if (mMenuBackgroundColor == null)
				{
					mMenuBackgroundColor = new DSColor ("#3D4046");
				}
				return mMenuBackgroundColor;
			}
			set
			{
				mMenuBackgroundColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the color of the menu item when selected.
		/// </summary>
		/// <value>The color of the menu item selected.</value>
		public override DSColor MenuSelectedBackgroundColor
		{
			get
			{
				if (mMenuItemSelectedColor == null)
				{
					mMenuItemSelectedColor = MenuBackgroundColor;//new DSColor (0.0f, 0.0f, 1.0f, 1.0f);
				}
				return mMenuItemSelectedColor;
			}
			set
			{
				mMenuItemSelectedColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the color of the menu item when highlighted.
		/// </summary>
		/// <value>The color of the menu item high lighted.</value>
		public override DSColor MenuHighLightedBackgroundColor
		{
			get
			{
				if (mMenuItemHighlightedColor == null)
				{
					mMenuItemHighlightedColor = MenuBackgroundColor;
					//mMenuItemHighlightedColor = new DSColor (1.0f, 0.0f, 0.0f, 1.0f);
				}
				return mMenuItemHighlightedColor;
			}
			set
			{
				mMenuItemHighlightedColor = value;
			}
		}

		#endregion
		#endregion



	}
}

