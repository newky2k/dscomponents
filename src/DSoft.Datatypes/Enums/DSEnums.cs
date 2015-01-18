// ****************************************************************************
// <copyright file="DSEnums.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

namespace DSoft.Datatypes.Enums
{
	/// <summary>
	/// Font weight.
	/// </summary>
	public enum FontWeight
	{
		/// <summary>
		/// Normal
		/// </summary>
		Normal,
		/// <summary>
		/// Bold
		/// </summary>
		Bold
	}

	/// <summary>
	/// Text alignment.
	/// </summary>
	public enum TextAlignment
	{
		/// <summary>
		/// Left
		/// </summary>
		Left = 0,
		/// <summary>
		/// Middle
		/// </summary>
		Middle,
		/// <summary>
		/// Right
		/// </summary>
		Right
	}

	/// <summary>
	/// More menu item type.
	/// </summary>
	public enum MoreMenuItemType
	{
		/// <summary>
		/// Item the links to content
		/// </summary>
		View,
		/// <summary>
		/// Item that executes code
		/// </summary>
		Action,
		/// <summary>
		/// Segement or Section Header
		/// </summary>
		Segment,
		/// <summary>
		/// Blank spacer
		/// </summary>
		Blank
	}

	/// <summary>
	/// Flyout menu state.
	/// </summary>
	public enum FlyoutMenuState
	{
		/// <summary>
		/// Menu is closed
		/// </summary>
		Closed,
		/// <summary>
		/// Left menu is visible
		/// </summary>
		LeftOpen,
		/// <summary>
		/// Right menu is visible
		/// </summary>
		RightOpen}
	;

	/// <summary>
	/// Toolbar operation mode
	/// </summary>
	public enum ToolbarMode
	{
		/// <summary>
		/// Toolbar mode, no bottom highlight on android
		/// </summary>
		Toolbar,
		/// <summary>
		/// Navigation mode, bottom highligh on android
		/// </summary>
		Navigation,
	}

	/// <summary>
	/// Border style.
	/// </summary>
	public enum BorderStyle
	{
		/// <summary>
		///  No border
		/// </summary>
		None,
		/// <summary>
		/// All Sides
		/// </summary>
		Full,
		/// <summary>
		/// Top and Bottom only
		/// </summary>
		HorizontalOnly,
		/// <summary>
		/// Left and Right only
		/// </summary>
		VerticalOnly
	}

	/// <summary>
	/// Grid header style.
	/// </summary>
	public enum GridHeaderStyle
	{
		/// <summary>
		/// Standard scrollable column headers
		/// </summary>
		Standard,
		/// <summary>
		/// Fixed column headers
		/// </summary>
		Fixed,
		/// <summary>
		/// Column Headers not visible
		/// </summary>
		None
	}

	/// <summary>
	/// Determines the style of the cell
	/// </summary>
	public enum CellStyle
	{
		/// <summary>
		/// Cell style
		/// </summary>
		Cell,
		/// <summary>
		/// Header style
		/// </summary>
		Header,
		/// <summary>
		/// Empty style
		/// </summary>
		Blank,
		/// <summary>
		/// Row Selector Style
		/// </summary>
		RowSelector,
	}

	/// <summary>
	/// The cell position type in the grid
	/// </summary>
	public enum CellPositionType
	{
		/// <summary>
		/// Top left
		/// </summary>
		LeftTop,
		/// <summary>
		/// Middle Left
		/// </summary>
		LeftMiddle,
		/// <summary>
		/// Bottom Left
		/// </summary>
		LeftBottom,
		/// <summary>
		/// Top Center
		/// </summary>
		CenterTop,
		/// <summary>
		/// Center
		/// </summary>
		CenterMiddle,
		/// <summary>
		/// Bottom Center
		/// </summary>
		CenterBottom,
		/// <summary>
		/// Top Right
		/// </summary>
		RightTop,
		/// <summary>
		/// Right Center
		/// </summary>
		RightMiddle,
		/// <summary>
		/// Bottom Right
		/// </summary>
		RightBottom
	}

	/// <summary>
	/// Row position type.
	/// </summary>
	public enum RowPositionType
	{
		/// <summary>
		/// Top Row
		/// </summary>
		Top,
		/// <summary>
		/// Middle Rows
		/// </summary>
		Middle,
		/// <summary>
		/// Bottom Row
		/// </summary>
		Bottom
	}

	/// <summary>
	/// Cell sort indicator style.
	/// </summary>
	public enum CellSortIndicatorStyle
	{
		/// <summary>
		/// No Indicator
		/// </summary>
		None,
		/// <summary>
		/// Ascending
		/// </summary>
		Up,
		/// <summary>
		/// Descending
		/// </summary>
		Down
	}

	/// <summary>
	/// Boolean formatter style.
	/// </summary>
	public enum BooleanFormatterStyle
	{
		/// <summary>
		/// Show as text
		/// </summary>
		Text,
		/// <summary>
		/// Show using images for true and false values
		/// </summary>
		Image
	}

	/// <summary>
	/// Style of the sort indicator
	/// </summary>
	public enum SortIndicatorStyle
	{
		/// <summary>
		/// No indicator
		/// </summary>
		None,
		/// <summary>
		/// Ascending
		/// </summary>
		Ascending,
		/// <summary>
		/// Descending
		/// </summary>
		Descending
	}

	/// <summary>
	/// Mode for scrolling to a selected row
	/// </summary>
	public enum ScrollToMode
	{
		/// <summary>
		/// Don't scroll
		/// </summary>
		None,
		/// <summary>
		/// Scroll to top
		/// </summary>
		Top,
		/// <summary>
		/// Scroll to middle
		/// </summary>
		Middle,
		/// <summary>
		/// Scroll to bottom
		/// </summary>
		Bottom,
	}
}

