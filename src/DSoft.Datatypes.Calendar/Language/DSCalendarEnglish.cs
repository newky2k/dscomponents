// ****************************************************************************
// <copyright file="DSCalendarEnglish.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;

namespace DSoft.Datatypes.Calendar.Language
{
	public class DSCalendarEnglish : DSCalendarLanguage
	{		
		public override List<string> DayStrings
		{
			get
			{
				var result = new List<String>();
				
				result.Add("Monday");
				result.Add("Tuesday");
				result.Add("Wednesday");
				result.Add("Thursday");
				result.Add("Friday");
				result.Add("Saturday");
				result.Add("Sunday");
				
				return result;
			}
		}
		public override List<string> MonthStrings
		{
			get
			{
				var result = new List<String>();
				
				result.Add("January");
				result.Add("Feburary");
				result.Add("March");
				result.Add("April");
				result.Add("May");
				result.Add("June");
				result.Add("July");
				result.Add("August");
				result.Add("September");
				result.Add("October");
				result.Add("November");
				result.Add("December");
				
				return result;
			}
		}

	}
}

