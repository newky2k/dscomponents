// ****************************************************************************
// <copyright file="DSCalendarEventCollection.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.ObjectModel;

namespace DSoft.Datatypes.Calendar.Data.Collections
{
	public class DSCalendarEventCollection : Collection<DSCalendarEvent>
	{
		
		public DSCalendarEventCollection this [DateTime Data]
		{
			 get
			 {
			 	var startDate = new DateTime(Data.Year, Data.Month, Data.Day, 0,0,0);
			 	var endDate = new DateTime(Data.Year, Data.Month, Data.Day, 23,59,59);
			 	
			 	var results = new DSCalendarEventCollection()	;
			 	
			 	foreach (DSCalendarEvent anEvent in this)
			 	{
			 		bool include = false;
			 		
			 		if (anEvent.StartDate>=startDate && anEvent.StartDate<=endDate)
			 		{
			 			include = true;
			 		}
			 	
			 		if (!include)
			 		{
			 			//see if the date falls within the start and end dates of the 
			 			
			 			if (Data>=anEvent.StartDate && Data<=anEvent.EndDate)
				 		{
				 			include = true;
				 		}
			 			
			 		}
			 		if (include)
			 		{
			 			results.Add(anEvent);
			 		}
			 	}
			 	
			 	return results;
			 }
		}
	}
}

