// ****************************************************************************
// <copyright file="DSEventView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using MonoTouch.UIKit;
using System.Drawing;
using DSoft.Datatypes.Calendar.Data;
using DSoft.Datatypes.Calendar.Enums;
using MonoTouch.Foundation;

namespace DSoft.UI.Calendar.Views
{
	/// <summary>
	/// Abstract view for display an event in the calenda
	/// </summary>
	public abstract class DSEventView : DSTouchView
	{
		#region Fields
		private DSCalendarEvent mEvent;
		private DSEventType mType;
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets the event.
		/// </summary>
		/// <value>The event.</value>
		public DSCalendarEvent Event
		{
			get
			{
				return mEvent;
			}
		}
		
		/// <summary>
		/// Gets the draw style of the event view depending on where the date of the cell falls withing the event date range
		/// </summary>
		/// <value>The type of the view.</value>
		public DSEventType ViewType
		{
			get
			{
				return mType;
			}
		}
		
		#endregion
		
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Calendar.Views.DSEventView"/> class.
		/// </summary>
		/// <param name="Frame">Frame.</param>
		/// <param name="AnEvent">An event.</param>
		/// <param name="ViewType">View type.</param>
		public DSEventView (RectangleF Frame, DSCalendarEvent AnEvent, DSEventType ViewType) : base(Frame)
		{
			Initialize (AnEvent, ViewType);
		}
				
		#endregion
		
		#region Private functions
		/// <summary>
		/// Initialize the specified AnEvent and ViewType.
		/// </summary>
		/// <param name="AnEvent">An event.</param>
		/// <param name="ViewType">View type.</param>
		private void Initialize(DSCalendarEvent AnEvent, DSEventType ViewType)
		{
			this.Opaque  = false;
			this.ClipsToBounds = true;
			
			mEvent = AnEvent;
			mType = ViewType;
			
			this.AutosizesSubviews = true;
			this.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;

			this.DoubleTap += () => 
			{
				if (this.Superview != null && this.Superview is DSCalendarEventsView)
				{
					((DSCalendarEventsView)this.Superview).DoubleTappedEvent (Event, this);
				}
			};
		}
		
		#endregion
	}
}

