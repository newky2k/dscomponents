// ****************************************************************************
// <copyright file="DSCalendarLanguage.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;

namespace DSoft.Datatypes.Calendar.Language
{
	public abstract class DSCalendarLanguage
	{
		private static DSCalendarLanguage mLan;
		
		public static DSCalendarLanguage CurrentLanguage
		{
			get
			{
				if (mLan == null)
				{
					mLan = new DSCalendarEnglish();
				}
				return mLan;
			}
			set
			{
				mLan = value;
			}
		}
	
		public abstract List<string> DayStrings {get;}
		
		public abstract List<string> MonthStrings {get;}
		
		public virtual string ShortStringForMonth(int Month)
		{
			var mnt = MonthStrings[Month];
			
			if (mnt.Length <= 3) 
			{
				return mnt;
			}
			
			return mnt.Substring(0,3);
		}
		
		public virtual string ShortStringForDay(int Day)
		{
			var mnt = DayStrings[Day];
			
			if (mnt.Length <= 3) 
			{
				return mnt;
			}
			
			return mnt.Substring(0,3);
		}
	}
}

