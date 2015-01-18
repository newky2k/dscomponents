// ****************************************************************************
// <copyright file="DSCalendarEvent.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Types;

namespace DSoft.Datatypes.Calendar.Data
{
	/// <summary>
	/// Event object for the DSCalendarView
	/// </summary>
	public class DSCalendarEvent
	{
		#region Fields
		private DSColor mEventColor;
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Id of the event
		/// </summary>
		/// <value>The event I.</value>
		public string EventID { get; set;}
		
		/// <summary>
		/// Title of the event
		/// </summary>
		public String Title;
		
		/// <summary>
		/// The start date of the event
		/// </summary>
		public DateTime StartDate;
		
		/// <summary>
		/// The end date of the event
		/// </summary>
		public DateTime EndDate;
		
		/// <summary>
		/// The color of the event such as calendar color
		/// </summary>
		public DSColor EventColor
		{
			get
			{
				if (mEventColor == null)
				{
					mEventColor = new DSColor(55.0f/255.0f,122.0f/255.0f,230.0f/255.0f,1f);
				}
				
				return mEventColor;
			}
			set
			{
				mEventColor = value;
			}
		}
		
		/// <summary>
		/// The is all day.
		/// </summary>
		public bool IsAllDay;
		
		/// <summary>
		/// Gets the number of days.
		/// </summary>
		/// <value>The number of days.</value>
		public int NumberOfDays
		{
			get
			{
				var diff = EndDate - StartDate;
				
				return diff.Days;
			}
		}
		
		#endregion
		
		
	}
}

