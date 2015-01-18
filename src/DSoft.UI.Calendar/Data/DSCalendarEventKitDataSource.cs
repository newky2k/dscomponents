// ****************************************************************************
// <copyright file="DSCalendarEventKitDataSource.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Calendar.Data;
using DSoft.Datatypes.Calendar.Data.Collections;
using DSoft.Datatypes.Calendar.Interfaces;
using DSoft.Datatypes.Types;
using MonoTouch.EventKit;
using MonoTouch.EventKitUI;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using DSoft.UI.Calendar.Helpers;
using DSoft.UI.Calendar.Views;
using System.Collections.Generic;
using System.Drawing;

namespace DSoft.UI.Calendar.Data
{
	/// <summary>
	/// Implementation of the Datasource interface
	/// </summary>
	public class DSCalendarEventKitDataSource : IDSCalendarDataSource
	{
		#region Fields
		private static EKEventStore mEventStore;
		private List<String> mVisibleCalendars;
		private DateTime? mCenterDate;
		private UIBarButtonItem mButton;
		internal UIPopoverController PopController;
		private DSCalendarView mCalendarView;
		#endregion
		
		#region Properties
		private static EKEventStore EventStore
		{
			get
			{
				if (mEventStore == null)
				{
					mEventStore = new EKEventStore ();
					
					//need to check os version
					if (new Version(UIDevice.CurrentDevice.SystemVersion) >= new Version(6,0,0,0))
					{
						mEventStore.RequestAccess (EKEntityType.Event, (bool granted, NSError e) => 
				        {
			                if (granted)
	                        {
	                        
	                        }
			                else
			                {
			                	new UIAlertView ( "Access Denied", "User Denied Access to Calendar Data", null,"ok", null).Show ();
			                }
				         });
					}

				}
				
				return mEventStore;
			}
		}
		
		/// <summary>
		/// Gets the calendars.
		/// </summary>
		/// <value>The calendars.</value>
		public static List<String> Calendars
		{
			get
			{	var lstString = new List<String> ();
			
				EKCalendar[] calendars = null;
				if (new Version (UIDevice.CurrentDevice.SystemVersion) >= new Version (6, 0, 0, 0)) 
				{
					calendars = EventStore.GetCalendars(EKEntityType.Event);
				}
				else
				{
					calendars = EventStore.Calendars;
				}
				
				foreach (EKCalendar cal in calendars)
				{
					lstString.Add (cal.Title);
				}
				
				return lstString;
			}
		}
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Calendar.Data.DSCalendarEventKitDataSource"/> class.
		/// </summary>
		public DSCalendarEventKitDataSource () :this(Calendars)
		{
			
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Calendar.Data.DSCalendarEventKitDataSource"/> class with visible calendar list
		/// </summary>
		/// <param name="VisibleCalendars">Visible calendars.</param>
		public DSCalendarEventKitDataSource (List<String> VisibleCalendars)
		{
			mVisibleCalendars = VisibleCalendars;
		}
		
		#endregion
		
		#region Functions
		
		
		/// <summary>
		/// Returns the Events for a specific date range
		/// </summary>
		/// <returns>The for date.</returns>>
		/// <param name="StartDate">Start date.</param>
		/// <param name="EndDate">End date.</param>
		public DSCalendarEventCollection EventsForDates (DateTime StartDate, DateTime EndDate)
		{
			var results = new DSCalendarEventCollection ();
		
			
			NSPredicate query = EventStore.PredicateForEvents (StartDate, EndDate, EKCalendarsInList(mVisibleCalendars));
			
			EKEvent[] events = EventStore.EventsMatching (query);
			
			if (events != null) 
			{
			
				foreach (EKEvent anEvent in events) 
				{
					
					var aDsEvent = new DSCalendarEvent ();
					
					aDsEvent.EventID = anEvent.EventIdentifier;
					aDsEvent.Title = anEvent.Title;
					
					aDsEvent.StartDate = anEvent.StartDate;
					aDsEvent.EndDate = anEvent.EndDate;
					
					if (anEvent.TimeZone == null)
					{
						aDsEvent.StartDate = aDsEvent.StartDate.ToLocalTime();
						aDsEvent.EndDate = aDsEvent.EndDate.ToLocalTime();
						
					}
					
					aDsEvent.EventColor = new UIColor (anEvent.Calendar.CGColor).ToDSColor ();
					
					aDsEvent.IsAllDay = anEvent.AllDay;
					
					
					results.Add (aDsEvent);
					
				}
			}
						
			return results;
		}
		
		/// <summary>
		/// Adds an event for specified date.
		/// </summary>
		/// <returns>The event for date.</returns>
		/// <param name="Date">Date.</param>
		/// <param name="CalendarView">Calendar view.</param>
		/// <param name="sender">Sender.</param>
		public void AddEventForDate (DateTime Date, object CalendarView, object sender)
		{
			//create a new event
			var anNewEvent = EKEvent.FromStore (EventStore);
			anNewEvent.TimeZone = NSTimeZone.LocalTimeZone;
			anNewEvent.StartDate = Date;
			anNewEvent.EndDate = Date.AddDays (1).AddMinutes (-1);
			anNewEvent.AllDay = true;
			anNewEvent.Calendar = EventStore.DefaultCalendarForNewEvents;

			ShowEventEditor (anNewEvent, CalendarView as DSCalendarView, sender);
			
		}
		/// <summary>
		/// Called when an event is double tapped
		/// </summary>
		/// <returns>The double tap event view.</returns>
		/// <param name="sender">Sender.</param>
		/// <param name="AnEvent">An event.</param>
		/// <param name="CalendarView">Calendar view.</param>
		public void DidDoubleTapEventView (object CalendarView, object sender, DSCalendarEvent AnEvent)
		{
				
			if (CalendarView is DSCalendarView)
			{
				EKEvent mySavedEvent = EventStore.EventFromIdentifier ( AnEvent.EventID );
					
				if (mySavedEvent != null)
				{
					ShowEventEditor (mySavedEvent, CalendarView as DSCalendarView,sender);
				}
					
				var parentVC = CalendarView as DSCalendarView;
				
				if (parentVC.ParentViewController != null)
				{
					

				}
				
			}

		}
		
		/// <summary>
		/// Limits the calendar list to those specified
		/// </summary>
		/// <returns>The calendars in list.</returns>
		/// <param name="VisibleCalendars">Visible calendars.</param>
		private EKCalendar[] EKCalendarsInList (List<String> VisibleCalendars)
		{
			var visCals = new List<EKCalendar> ();
			
			EKCalendar[] calendars = null;
			if (new Version (UIDevice.CurrentDevice.SystemVersion) >= new Version (6, 0, 0, 0)) 
			{
				calendars = EventStore.GetCalendars(EKEntityType.Event);
			}
			else
			{
				calendars = EventStore.Calendars;
			}
				
			foreach (EKCalendar cal in calendars)
			{
				if (VisibleCalendars.Contains(cal.Title))
				{
					visCals.Add (cal);
				}
			}
			
			return visCals.ToArray ();
			
			
		}
		
		/// <summary>
		/// Shows the event editor.
		/// </summary>
		/// <param name="TheEvent">The event.</param>
		/// <param name="CalendarView">Calendar view.</param>
		/// <param name="targetView">Target view.</param>
		private void ShowEventEditor(EKEvent TheEvent, DSCalendarView CalendarView, object targetView)
		{
			if (targetView is DSCalendarCell)
			{
				mCenterDate = ((DSCalendarCell)targetView).CellInfo.Date;
			}
			else if (targetView is UIBarButtonItem)
			{
				mButton = (UIBarButtonItem)targetView;
			}
			
			mCalendarView = CalendarView;
				
			var eventController = new MonoTouch.EventKitUI.EKEventEditViewController ();
			eventController.ModalPresentationStyle = UIModalPresentationStyle.FormSheet;
			eventController.EventStore = EventStore;
			eventController.Event = TheEvent;

			eventController.EditViewDelegate = new CreateEventEditViewDelegate ( eventController, CalendarView, this );
					
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
			{
				//use the popoverview to present it
				if (PopController != null)
				{
					//dissmis the existing one
					PopController.Dismiss (true);
				}
				
				PopController = new UIPopoverController (eventController);
				PopController.PopoverContentSize = eventController.ContentSizeForViewInPopover;
				PopController.DidDismiss += (object sender, EventArgs e) => {PopController = null;};
				if (targetView is UIBarButtonItem)
				{
					PopController.PresentFromBarButtonItem ((UIBarButtonItem)targetView, UIPopoverArrowDirection.Any, true);
				}
				else if (targetView is UIView)
				{
					
					var aView = targetView as UIView;
					
					var inset = new RectangleF (aView.Frame.Left, aView.Frame.Top, aView.Frame.Size.Width, 25);
					
					//if (targetView is DSEventView) inset = aView.ConvertRectToView (aView.Frame, mCalendarView.CalendarGrid);
					PopController.PresentFromRect (inset, mCalendarView.CalendarGrid, UIPopoverArrowDirection.Any, true);
				}
				
			}
			else
			{
				//present modally on the phone
				if (CalendarView.ParentViewController != null)
				{

					CalendarView.ParentViewController.PresentViewController ( eventController, true, null );
				}
			}

				

		}
		
		/// <summary>
		/// Handles the rotation.
		/// </summary>
		/// <param name="Visible">If set to <c>true</c> visible.</param>
		public void HandleRotation (bool Visible)
		{
			if (PopController != null)
			{
				if (Visible) 
				{	
					if (mButton != null)
					{
						PopController.PresentFromBarButtonItem(mButton,  UIPopoverArrowDirection.Any, true);
						
					}
					else if (mCenterDate != null)
					{
						UIView aView = mCalendarView.CellForDate (mCenterDate.Value);

						var inset = new RectangleF (aView.Frame.Left, aView.Frame.Top, (aView.Frame.Size.Width == 0 ) ? 25 : aView.Frame.Size.Width, 25);
						PopController.PresentFromRect (inset, mCalendarView.CalendarGrid, UIPopoverArrowDirection.Any, true);
					}
					
				}
				else
				{
					PopController.Dismiss (true);
				}
			}
			
		}
		
		internal void ClearViews()
		{
			mCenterDate = null;
			mCalendarView = null;
		}
		#endregion
		
		#region Delegate Class
		/// <summary>
		/// Delegate class
		/// </summary>
		protected class CreateEventEditViewDelegate : MonoTouch.EventKitUI.EKEventEditViewDelegate
		{
		        // we need to keep a reference to the controller so we can dismiss it
		        /// <summary>
		        /// The event controller.
		        /// </summary>
		        protected EKEventEditViewController eventController;
				/// <summary>
				/// The m calendar view.
				/// </summary>
				protected DSCalendarView mCalendarView;
				/// <summary>
				/// The datasource.
				/// </summary>
				protected DSCalendarEventKitDataSource mDatasource;
				
				#region Constructors
				/// <summary>
				/// Initializes a new instance of the CreateEventEditViewDelegate class.
				/// </summary>
				/// <param name="eventController">Event controller.</param>
				/// <param name="CalendarView">Calendar view.</param>
				/// <param name="DataSource">Data source.</param>
		        public CreateEventEditViewDelegate (EKEventEditViewController eventController, DSCalendarView CalendarView, DSCalendarEventKitDataSource DataSource)
		        {
		                // save our controller reference
		                this.eventController = eventController;
		                this.mCalendarView = CalendarView;
		                this.mDatasource = DataSource;
		        }
				#endregion
		     
		     /// <Docs>To be added.</Docs>
		     /// <summary>
		     /// Completion Handler
		     /// </summary>
		     /// <param name="controller">Controller.</param>
		     /// <param name="action">Action.</param>
		     public override void Completed (MonoTouch.EventKitUI.EKEventEditViewController controller, EKEventEditViewAction action)
			{
				if (mDatasource.PopController != null)
				{
					mDatasource.PopController.Dismiss(true);
					mDatasource.PopController = null;
					
					mDatasource.ClearViews ();
				}
				else
				{
					eventController.DismissViewController (true, null);
				}
				
		        
				switch (action) 
				{
			
				case EKEventEditViewAction.Canceled:
					break;
				case EKEventEditViewAction.Deleted:
					{
			        	//reload the calendar view
						mCalendarView.ReloadData ();
					}
					break;
				case EKEventEditViewAction.Saved:
					{
						
						NSError e;
						eventController.EventStore.SaveEvent (controller.Event, EKSpan.ThisEvent, out e);

						//reload the calendar view
						mCalendarView.ReloadData ();
					}
					break;
				}
		    }
		}
		
		#endregion
	}
	
}