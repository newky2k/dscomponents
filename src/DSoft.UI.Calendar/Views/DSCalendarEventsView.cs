// ****************************************************************************
// <copyright file="DSCalendarEventsView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using MonoTouch.UIKit;
using System.Drawing;
using DSoft.Datatypes.Calendar.Data.Collections;
using System.Collections.Generic;
using DSoft.UI.Calendar.Themes;
using DSoft.Datatypes.Calendar.Enums;
using DSoft.Datatypes.Calendar.Data;
using MonoTouch.CoreGraphics;

namespace DSoft.UI.Calendar.Views
{
	internal class DSCalendarEventsView : DSTouchView
	{
		#region Fields
		private DSCalendarEventCollection mEvents;
		private List<UIView> mViews;
		private DateTime mDate;
		private DSMoreEventsView mMoreEventsView;
		
		#endregion
		
		#region Properties
		
		internal DateTime CellDate
		{
			get
			{
				return mDate;
			}
			set
			{
				if (mDate != value)
				{
					mDate =  value;
					
					mViews = null;
				}

			}
		}
		
		internal int MoreItems
		{
			get
			{
				var count = 0;
				
				if (Events != null && Events.Count > DSCalendarTheme.CurrentTheme.MaxEvents)
				{
					count = Events.Count - (DSCalendarTheme.CurrentTheme.MaxEvents-1);
				}
				
				return count;
			}
		}
		internal bool ShouldShowMoreView
		{
			get
			{
				if (Events != null && (Events.Count > DSCalendarTheme.CurrentTheme.MaxEvents && mMoreEventsView != null))
				{
					if (DSCalendarTheme.CurrentTheme.MoreViewPosition == DSMoreEventsViewPositon.BottomFull)
					{
						return true;
					}
					
				}
				return false;
			}
		}
		
		private int NumberOfRowsToShow
		{
			get
			{
				var count = DSCalendarTheme.CurrentTheme.MaxEvents;
				
				if (ShouldShowMoreView) count = count - 1;
				
				return count;
			}
		}
		private List<UIView> Views
		{
			get
			{
				if (mViews == null)
				{
					mViews = new List<UIView>();
					
					int pos = 0;
					
					if (mEvents != null)
					{
						foreach (var evT in mEvents)
						{
							if (pos < NumberOfRowsToShow)
							{
								var count = evT.NumberOfDays;
								
								DSEventType evType = DSEventType.Single;
								
								if (count == 0)
								{
									evType = DSEventType.Single;
								}
								else
								{
									if (evT.StartDate.Date == CellDate.Date)
									{
										evType = DSEventType.Left;
									}
									else if (evT.EndDate.Date == CellDate.Date)
									{
										evType = DSEventType.Right;
									}
									else if (CellDate.Date > evT.StartDate && CellDate.Date < evT.EndDate)
									{
										evType = DSEventType.Middle;
									}
									
								}
								
							  	var aView = DSCalendarTheme.CurrentTheme.EventViewForEvent(evT, evType);
							
								if (aView != null)
								{
									mViews.Add(aView);
									this.AddSubview(aView);
								}
								
								pos++;
							}
							else
							{
							  continue;
							}
	
							
						}
					}

				}
				
				return mViews;
			}
			set
			{
				if (value == null && mViews != null)
				{
					foreach (var item in mViews)
					{
						item.RemoveFromSuperview();
					}
				}
				
				mViews = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the events for the cell
		/// </summary>
		/// <value>The events.</value>
		internal DSCalendarEventCollection Events
		{
			get
			{
				return mEvents;
			}
			set
			{
				if (mEvents != value)
				{
					mEvents = value;
					
					Views = null;
					
					if (mEvents.Count != 0)
					{
						this.SetNeedsDisplay();
					}
				}
			}
		}
		
		#endregion
		
		#region Constuctors
		public DSCalendarEventsView (RectangleF Frame) : base(Frame)
		{			
			
			Initialize ();
			
		}
		
		#endregion
		
		#region Override
		
		public override void LayoutSubviews ()
		{
			float posY = 0;
		
			float evHeight = this.Frame.Size.Height/DSCalendarTheme.CurrentTheme.MaxEvents;
			
			foreach (var evView in Views)
			{	
				evView.Frame = new RectangleF(0,posY, this.Frame.Width, evHeight).Integral();
				
				posY += evHeight;
			}
			
			if (mMoreEventsView != null && DSCalendarTheme.CurrentTheme.MoreViewPosition == DSMoreEventsViewPositon.BottomFull)
			{
				if (ShouldShowMoreView)
				{
					mMoreEventsView.RemainingItems = MoreItems;
				
					var topY = this.Frame.Size.Height - DSCalendarTheme.CurrentTheme.MoreEventsViewHeight;
				
					mMoreEventsView.Frame = new RectangleF(0,topY, this.Frame.Width, DSCalendarTheme.CurrentTheme.MoreEventsViewHeight).Integral();
				}
				else
				{
					mMoreEventsView.Frame = RectangleF.Empty;
				}

			}
		}
		
		#endregion
		
		#region Interal Functions
		
		private void Initialize()
		{
			this.BackgroundColor = UIColor.Clear;
			this.ClipsToBounds = true;
			
			if (DSCalendarTheme.CurrentTheme.MoreViewPosition == DSMoreEventsViewPositon.BottomFull)
			{
				mMoreEventsView = DSCalendarTheme.CurrentTheme.MoreEventsView();
				
				if (mMoreEventsView != null)
				{
					this.AddSubview(mMoreEventsView);
				}
			}
			
			this.SingleTap += () => 
			{
				if (this.Superview != null && this.Superview is DSCalendarCell)
				{
					((DSCalendarCell)this.Superview).SingleTouchEvent();
				}
			};
			this.DoubleTap += () => 
			{
				if (this.Superview != null && this.Superview is DSCalendarCell)
				{
					((DSCalendarCell)this.Superview).AddNewEvent();
				}

			};
		}
		
		internal void DoubleTappedEvent(DSCalendarEvent AnEvent, object sender)
		{
			if (this.Superview != null && this.Superview is DSCalendarCell)
			{
				((DSCalendarCell)this.Superview).DoubleTappedEvent (AnEvent, sender);
			}

		}
		
		#endregion
		
	}
}

