// ****************************************************************************
// <copyright file="DSCalendarCellInfo.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

namespace DSoft.UI.Calendar.Data
{
	/// <summary>
	/// DSCalendar cell information object
	/// </summary>
	internal class DSCalendarCellInfo
	{
		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>The date.</value>
		internal DateTime Date
		{
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether this instance is focus.
		/// </summary>
		/// <value><c>true</c> if this instance is focus; otherwise, <c>false</c>.</value>
		internal bool IsFocus {
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether this instance is current.
		/// </summary>
		/// <value><c>true</c> if this instance is current; otherwise, <c>false</c>.</value>
		internal bool IsCurrent
		{
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether this instance is on the first row.
		/// </summary>
		/// <value><c>true</c> if this instance is first row; otherwise, <c>false</c>.</value>
		internal bool IsFirstRow
		{
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets the show month.
		/// </summary>
		/// <value>The show month.</value>
		internal bool ShowMonth
		{
			get;
			set;
		}
	}
}

