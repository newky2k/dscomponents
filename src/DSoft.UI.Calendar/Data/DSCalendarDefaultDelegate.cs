// ****************************************************************************
// <copyright file="DSCalendarDefaultDelegate.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Calendar.Interfaces;
using DSoft.Datatypes.Calendar.Data;

namespace DSoft.UI.Calendar.Data
{
	/// <summary>
	/// DS calendar default delegate.
	/// </summary>
	public class DSCalendarDefaultDelegate : IDSCalendarDelegate
	{
		#region IDSCalendarDelegate implementation
		
		/// <summary>
		/// Dids the double tap day view.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		public void DidDoubleTapDayView (object sender, EventArgs args)
		{
			Console.WriteLine ("Clicked a line");
		}
		
		/// <summary>
		/// Dids the double tap more events view.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		public void DidDoubleTapMoreEventsView (object sender, EventArgs args)
		{
			
		}
		#endregion
		
	}
}

