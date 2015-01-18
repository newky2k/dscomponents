// ****************************************************************************
// <copyright file="IDSCalendarDelegate.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Calendar.Data;

namespace DSoft.Datatypes.Calendar.Interfaces
{
	/// <summary>
	/// DSCalendarView delegate interface
	/// </summary>
	public interface IDSCalendarDelegate
	{
		//did double touch cell
		void DidDoubleTapDayView(object sender, EventArgs args)	;
		
		//did double touch more event view
		void DidDoubleTapMoreEventsView(object sender, EventArgs args)	;
	}
}

