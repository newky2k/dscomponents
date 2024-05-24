// ****************************************************************************
// <copyright file="IDSCalendarDatasource.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Calendar.Data.Collections;
using DSoft.Datatypes.Calendar.Data;

namespace DSoft.Datatypes.Calendar.Interfaces
{
	/// <summary>
	/// DataSource for DSCalendarView
	/// </summary>
	public interface IDSCalendarDataSource
	{
		/// <summary>
		/// Returns the Events for a specific date
		/// </summary>
		/// <returns>The for date.</returns>
		/// <param name="aDate">A date.</param>
		DSCalendarEventCollection EventsForDates(DateTime StartDate, DateTime EndDate);
		
		/// <summary>
		/// Called when an event is double tapped
		/// </summary>
		/// <returns>The double tap event view.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="AnEvent">An event.</param>
		void DidDoubleTapEventView (object CalendarView, object sender, DSCalendarEvent AnEvent);
		
		/// <summary>
		/// Adds an event for specified date.
		/// </summary>
		/// <returns>The event for date.</returns>
		/// <param name="Date">Date.</param>
		void AddEventForDate (DateTime Date, object CalendarView, object sender);
		
		void HandleRotation (bool Visible);
		
	}
}

