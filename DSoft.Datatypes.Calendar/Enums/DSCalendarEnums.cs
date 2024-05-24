// ****************************************************************************
// <copyright file="DSCalendarEnums.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

namespace DSoft.Datatypes.Calendar.Enums
{
	/// <summary>
	/// Calendar day style for configuring the appearnce of the day indicators
	/// </summary>
	public enum CalendarDayStyle {Header, FirstRow, None}
	
	/// <summary>
	/// Calendar tile style for configuring the appearance of the title/date view
	/// </summary>
	public enum CalendarTitleStyle {Visible, Hidden}
	
	/// <summary>
	/// Calendar day display mode.
	/// </summary>
	public enum CalendarDayDisplayMode {Full, Short, None}
	
	/// <summary>
	/// Calendar day location.
	/// </summary>
	public enum CalendarDayLocation {TopLeft, TopRight, BottomLeft, BottomRight}
	
	/// <summary>
	/// Calendar month show mode.
	/// </summary>
	public enum CalendarMonthDisplayMode {FirstOnly, OnChange, None}
	
	/// <summary>
	/// Calendar week start.
	/// </summary>
	public enum CalendarWeekStart {Sunday, Monday}
	
	public enum WeekDay {Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday}
	
	public enum WeekDayFromSunday {Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday}
	
	/// <summary>
	/// The type of event view to present for single or multi day events
	/// </summary>
	public enum DSEventType {Left, Middle, Right, Single}
	
	/// <summary>
	/// DS more events view positon.
	/// </summary>
	public enum DSMoreEventsViewPositon {TopLeft, TopFull, TopRight, BottomLeft, BottomFull, BottomRight}
}

