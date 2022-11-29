// ****************************************************************************
// <copyright file="EnumHelper.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Calendar;
using DSoft.Datatypes.Calendar.Enums;

namespace DSoft.UI.Calendar.Helpers
{
	internal class EnumHelper
	{
		internal static WeekDay ConvertToWeekDay(DayOfWeek Day)
		{
			switch (Day)
			{
				case DayOfWeek.Monday:
				{
					return WeekDay.Monday;
				}
				case DayOfWeek.Tuesday:
				{
					return WeekDay.Tuesday;
				}
				case DayOfWeek.Wednesday:
				{
					return WeekDay.Wednesday;
				}
				case DayOfWeek.Thursday:
				{
					return WeekDay.Thursday;
				}
				case DayOfWeek.Friday:
				{
					return WeekDay.Friday;
				}
				case DayOfWeek.Saturday:
				{
					return WeekDay.Saturday;
				}
				case DayOfWeek.Sunday:
				{
					return WeekDay.Sunday;
				}
				default:
				{
					return WeekDay.Monday;
				}
					
			}
			
		}
		
		internal static WeekDayFromSunday ConvertToWeekDayFromSunday(DayOfWeek Day)
		{
			switch (Day)
			{
				case DayOfWeek.Monday:
				{
					return WeekDayFromSunday.Monday;
				}
				case DayOfWeek.Tuesday:
				{
					return WeekDayFromSunday.Tuesday;
				}
				case DayOfWeek.Wednesday:
				{
					return WeekDayFromSunday.Wednesday;
				}
				case DayOfWeek.Thursday:
				{
					return WeekDayFromSunday.Thursday;
				}
				case DayOfWeek.Friday:
				{
					return WeekDayFromSunday.Friday;
				}
				case DayOfWeek.Saturday:
				{
					return WeekDayFromSunday.Saturday;
				}
				case DayOfWeek.Sunday:
				{
					return WeekDayFromSunday.Sunday;
				}
				default:
				{
					return WeekDayFromSunday.Monday;
				}
					
			}
			
		}
		
		internal static WeekDay ConvertSundayDayToDay(WeekDayFromSunday Day)
		{
			switch (Day)
			{
				case WeekDayFromSunday.Monday:
				{
					return WeekDay.Monday;
				}
				case WeekDayFromSunday.Tuesday:
				{
					return WeekDay.Tuesday;
				}
				case WeekDayFromSunday.Wednesday:
				{
					return WeekDay.Wednesday;
				}
				case WeekDayFromSunday.Thursday:
				{
					return WeekDay.Thursday;
				}
				case WeekDayFromSunday.Friday:
				{
					return WeekDay.Friday;
				}
				case WeekDayFromSunday.Saturday:
				{
					return WeekDay.Saturday;
				}
				case WeekDayFromSunday.Sunday:
				{
					return WeekDay.Sunday;
				}
				default:
				{
					return WeekDay.Monday;
				}
					
			}
		}
	}
}

