// ****************************************************************************
// <copyright file="DSCalendarTheme.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using MonoTouch.UIKit;
using System.Drawing;
using DSoft.Datatypes.Calendar;
using DSoft.Datatypes.Calendar.Enums;
using DSoft.UI.Calendar.Data;
using DSoft.Datatypes.Calendar.Data;
using DSoft.UI.Calendar.Views;

namespace DSoft.UI.Calendar.Themes
{		
	/// <summary>
	/// Base calendar theme class
	/// </summary>
	public abstract class DSCalendarTheme
	{
		private static DSCalendarTheme mCurrent;
		
		/// <summary>
		/// Gets the current theme.
		/// </summary>
		/// <value>The current theme.</value>
		public static DSCalendarTheme CurrentTheme
		{
			get
			{
				if (mCurrent == null)
				{
					mCurrent = new DSCalendarDefaultTheme();
				}
				return mCurrent;
			}
		}
		
		/// <summary>
		/// Register a theme class as the current theme
		/// </summary>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static void Register<T>() where T : DSCalendarTheme,new()
		{
			var newType = new T();
				
			mCurrent = newType as DSCalendarTheme;
			
		}
		
		
		#region Properties
		
		#region Title/Date View Properties
		
		/// <summary>
		/// Gets the title view style.
		/// </summary>
		/// <value>The title view style.</value>
		public abstract CalendarTitleStyle TitleViewStyle {get;}
		
		/// <summary>
		/// Gets the height of the title view.
		/// </summary>
		/// <value>The height of the title view.</value>
		public abstract float TitleViewHeight {get;}
		
		/// <summary>
		/// Gets the title view font.
		/// </summary>
		/// <value>The title view font.</value>
		public abstract UIFont TitleViewFont {get;}
		
		/// <summary>
		/// Gets the color of the title view.
		/// </summary>
		/// <value>The color of the title view.</value>
		public abstract UIColor TitleViewColor {get;}
		
		/// <summary>
		/// Gets the title label position.
		/// </summary>
		/// <value>The title label position.</value>
		public abstract PointF TitleLabelPosition {get;}
		
		
		#endregion
		
		#region Grid Properties
		
		/// <summary>
		/// Gets the day style for showing the days of the week
		/// </summary>
		/// <value>The day style.</value>
		public abstract CalendarDayStyle DayStyle {get;}
		
		/// <summary>
		/// Gets the grid margin around the grid
		/// </summary>
		/// <value>The grid padding.</value>
		public abstract float GridMargin {get;}
		
		/// <summary>
		/// Gets the width of the grid border.
		/// </summary>
		/// <value>The width of the grid border.</value>
		public abstract float GridBorderWidth {get;}
		
		/// <summary>
		/// Gets what day of the week is the start of the week
		/// </summary>
		/// <value>The week start.</value>
		public abstract CalendarWeekStart WeekStart {get;}
		
		#endregion
		
		#region Cell Properties
		/// <summary>
		/// Background color for the standard cell
		/// </summary>
		/// <value>The cell background.</value>
		public abstract UIColor CellBackground {get;}
		
		/// <summary>
		/// Gets the weekend cell background.
		/// </summary>
		/// <value>The cell weekend background.</value>
		public abstract UIColor WeekendCellBackground {get;}
		
		/// <summary>
		/// Gets the inactive weekend cell background.
		/// </summary>
		/// <value>The in active weekend cell background.</value>
		public abstract UIColor InActiveWeekendCellBackground {get;}
		
		/// <summary>
		/// Gets the today cell background.
		/// </summary>
		/// <value>The today cell background.</value>
		public abstract UIColor TodayCellBackground {get;}
		
		/// <summary>
		/// Gets the inactive cell backgrond.
		/// </summary>
		/// <value>The in active cell backgrond.</value>
		public abstract UIColor InActiveCellBackgrond {get;}
		
		/// <summary>
		/// Gets the color of the cell border.
		/// </summary>
		/// <value>The color of the cell border.</value>
		public abstract UIColor CellBorderColor {get;}
		
		/// <summary>
		/// Gets the cell selected background.
		/// </summary>
		/// <value>The cell selected background.</value>
		public abstract UIColor CellSelectedBackground {get;}
		#endregion
		
		#region Cell Text properties
		
		/// <summary>
		/// Gets the month display mode.
		/// </summary>
		/// <value>The month display mode.</value>
		public abstract CalendarMonthDisplayMode MonthDisplayMode {get;}
		
		/// <summary>
		/// Gets the day location.
		/// </summary>
		/// <value>The day location.</value>
		public abstract CalendarDayLocation DayLocation {get;}
		
		/// <summary>
		/// Gets the color of the cell text for a standard day in the current month
		/// </summary>
		/// <value>The color of the cell text.</value>
		public abstract UIColor CellTextColor {get;}
		
		/// <summary>
		/// Gets the color of the today cell text for today
		/// </summary>
		/// <value>The color of the today cell text.</value>
		public abstract UIColor TodayCellTextColor {get;}
		
		/// <summary>
		/// Gets the color of the inactive cell text for a standard day that isn't in the current month
		/// </summary>
		/// <value>The color of the in active cell text.</value>
		public abstract UIColor InActiveCellTextColor {get;}
		
		/// <summary>
		/// Gets the font for the text in a standard cell
		/// </summary>
		/// <value>The cell text font.</value>
		public abstract UIFont CellTextFont {get;}
		
		/// <summary>
		/// Gets the font for the text in a today cell
		/// </summary>
		/// <value>The today cell text font.</value>
		public abstract UIFont TodayCellTextFont {get;}
		
		/// <summary>
		/// Gets the today cell header view.
		/// </summary>
		/// <value>The today cell header view.</value>
		public abstract UIView TodayCellHeaderView {get;}
		
		/// <summary>
		/// Gets the color of the cell text for a standard day in the current month
		/// </summary>
		/// <value>The color of the cell text.</value>
		public abstract UIColor CellSelectedTextColor {get;}
		#endregion
		
		#region Header Cell Properties
		
		/// <summary>
		/// Gets the header hieght.
		/// </summary>
		/// <value>The header hieght.</value>
		public abstract float HeaderHeight {get;}
		
		/// <summary>
		/// Gets the day display mode.
		/// </summary>
		/// <value>The day display mode.</value>
		public abstract CalendarDayDisplayMode HeaderDayDisplayMode {get;}
		
		/// <summary>
		/// Gets the header cell background.
		/// </summary>
		/// <value>The header cell background.</value>
		public abstract UIColor HeaderCellBackground {get;}
		
		/// <summary>
		/// Gets the color of the cell border.
		/// </summary>
		/// <value>The color of the cell border.</value>
		public abstract UIColor HeaderCellBorderColor {get;}
		
		/// <summary>
		/// Gets the color of the header cell text for a standard day in the current month
		/// </summary>
		/// <value>The color of the cell text.</value>
		public abstract UIColor HeaderCellTextColor {get;}
		
		/// <summary>
		/// Gets the font for the header cell text
		/// </summary>
		/// <value>The header cell text font.</value>
		public abstract UIFont HeaderCellTextFont {get;}
		
		/// <summary>
		/// Gets the header cell text alignment.
		/// </summary>
		/// <value>The header cell text alignment.</value>
		public abstract UITextAlignment HeaderCellTextAlignment {get;}
		#endregion
		
		#region Event Properties
		
		/// <summary>
		/// Gets the more events view position.
		/// </summary>
		/// <value>The more view position.</value>
		public abstract DSMoreEventsViewPositon MoreViewPosition {get;}
		
	
		/// <summary>
		/// Gets the maximum number of events to show in the events view section of the cell
		/// </summary>
		/// <value>The max events.</value>
		public abstract int MaxEvents {get;}
		
		/// <summary>
		/// Hieght of the more events view, used to show the number of events that don't fit on the cell
		/// </summary>
		/// <value>The height of the more events view.</value>
		public abstract float MoreEventsViewHeight {get;}
		
		
		#endregion
		#endregion
		
		#region Functions
		/// <summary>
		/// Get the EventView for the Event
		/// </summary>
		/// <returns>The view for event.</returns>
		/// <param name="Data">Data.</param>
		/// <param name="ViewType">View type.</param>
		public abstract DSEventView EventViewForEvent(DSCalendarEvent Data, DSEventType ViewType);
		
		/// <summary>
		/// Get the more events view
		/// </summary>
		/// <returns>The events view.</returns>
		public abstract DSMoreEventsView MoreEventsView();
		
		#endregion
	}
}

