// ****************************************************************************
// <copyright file="DSCalendarCell.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using MonoTouch.UIKit;
using System.Drawing;
using DSoft.Datatypes.Calendar;
using DSoft.Datatypes.Calendar.Language;
using DSoft.Datatypes.Calendar.Enums;
using DSoft.UI.Calendar.Data;
using DSoft.Datatypes.Calendar.Data.Collections;
using DSoft.UI.Calendar.Themes;
using DSoft.UI.Calendar.Helpers;
using MonoTouch.Foundation;
using DSoft.Datatypes.Calendar.Interfaces;
using DSoft.Datatypes.Calendar.Data;
using MonoTouch.CoreGraphics;

namespace DSoft.UI.Calendar.Views
{
	/// <summary>
	/// Calendar cell view
	/// </summary>
	public class DSCalendarCell : DSTouchView
	{
		#region Fields
		private DSCalendarCellInfo mCellInfo;
		private UILabel mRightLabel;
		private UIView mTodayHeader;
		private DSCalendarEventCollection mEvents;
		private DSCalendarEventsView mEventView;
		private DSMoreEventsView mMoreEventsView;
		private bool mSelected;
		
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets a value indicating whether this instance is weekend.
		/// </summary>
		/// <value><c>true</c> if this instance is weekend; otherwise, <c>false</c>.</value>
		private bool IsWeekend
		{
			get
			{
				switch(mCellInfo.Date.DayOfWeek)
				{
					case DayOfWeek.Sunday:
					case DayOfWeek.Saturday:
						return true;
					default:
						return false;
				}
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
					
					if (mEvents.Count != 0)
					{
						this.SetNeedsDisplay();
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the cell info.
		/// </summary>
		/// <value>The cell info.</value>
		internal DSCalendarCellInfo CellInfo
		{
			get
			{
				return mCellInfo;
			}
			set
			{
				mCellInfo = value;
				
				this.SetNeedsDisplay();
			}
		}
		
		/// <summary>
		/// Gets the more items.
		/// </summary>
		/// <value>The more items.</value>
		internal int MoreItems
		{
			get
			{
				var count = 0;
				
				if (Events != null && Events.Count > DSCalendarTheme.CurrentTheme.MaxEvents)
				{
					count = Events.Count;
				}
				
				return count;
			}
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether this instance is selected.
		/// </summary>
		/// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
		internal bool IsSelected
		{
			get
			{
				return mSelected;
			}
			set
			{
				if (mSelected != value)
				{
					mSelected = value;
					
					this.SetNeedsDisplay ();
				}
			}
		}
		#endregion
		
		#region Constuctors		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Calendar.Views.DSCalendarCell"/> class.
		/// </summary>
		/// <param name="Frame">Frame.</param>
		internal DSCalendarCell (RectangleF Frame) : base(Frame)
		{
			Initialize ();
			
		}
		#endregion
		
		#region Overrides
		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);
			
			DrawCell(rect);
			
		}
				
		#endregion
		
		#region Functions
		
		private void Initialize()
		{
			this.Opaque = false;
			this.BackgroundColor = UIColor.Clear;
			this.AutosizesSubviews = true;
			this.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			
			mRightLabel = new UILabel(RectangleF.Empty);
			mRightLabel.BackgroundColor = UIColor.Clear;
			mRightLabel.TextColor = UIColor.Gray;
			mRightLabel.TextAlignment = UITextAlignment.Right;
			mRightLabel.Font = DSCalendarTheme.CurrentTheme.CellTextFont;
			
			this.AddSubview(mRightLabel);
			
			mEventView = new DSCalendarEventsView(RectangleF.Empty);
			mEventView.BackgroundColor = UIColor.Clear;
			this.Add(mEventView);
			
			if (DSCalendarTheme.CurrentTheme.MoreViewPosition == DSMoreEventsViewPositon.TopRight)
			{
				mMoreEventsView = DSCalendarTheme.CurrentTheme.MoreEventsView();
				
				if (mMoreEventsView != null)
				{
					this.AddSubview(mMoreEventsView);
				}
			}
			
			this.SingleTap += () => 
			{
				SingleTouchEvent();
			};
			
			this.DoubleTap += () => 
			{
				
				AddNewEvent();
				
			};
		}
		
		/// <summary>
		/// Draws the cell
		/// </summary>
		/// <param name="rect">Rect.</param>
		private void DrawCell (RectangleF rect)
		{
			if (mCellInfo.IsFocus) {
				if (mTodayHeader == null)
					mTodayHeader = DSCalendarTheme.CurrentTheme.TodayCellHeaderView;
				
				if (mTodayHeader != null) {
					//set the frame
					mTodayHeader.Frame = new RectangleF (0, 0, this.Frame.Width, 20).Integral();
					
					if (mTodayHeader.Superview == null)
						this.InsertSubviewBelow (mTodayHeader, mRightLabel);
				}
				
			}

			
			var context = UIGraphics.GetCurrentContext ();
			
			UIColor fillColor = UIColor.White;
			
			var selectedColor = DSCalendarTheme.CurrentTheme.CellSelectedBackground;
			
			if (!IsSelected || selectedColor == null) {
				if (mCellInfo.IsFocus) {
					fillColor = DSCalendarTheme.CurrentTheme.TodayCellBackground;
				} else if (IsWeekend) {
					fillColor = (mCellInfo.IsCurrent) ? DSCalendarTheme.CurrentTheme.WeekendCellBackground : DSCalendarTheme.CurrentTheme.InActiveWeekendCellBackground;
				} else if (!mCellInfo.IsCurrent) {
					fillColor = DSCalendarTheme.CurrentTheme.InActiveCellBackgrond;
				} else {
					fillColor = DSCalendarTheme.CurrentTheme.CellBackground;
					
				}
			} else {
				fillColor = selectedColor;
			}
			
			fillColor.SetFill ();
			context.FillRect (rect);
			
			context.SetStrokeColor (DSCalendarTheme.CurrentTheme.CellBorderColor.CGColor);
			context.SetLineWidth (DSCalendarTheme.CurrentTheme.GridBorderWidth);
			context.StrokeRect (rect);
									
			mRightLabel.Text = String.Empty;
			//mRightLabel.BackgroundColor = UIColor.Red;
			//
			if (mCellInfo.IsFirstRow && DSCalendarTheme.CurrentTheme.DayStyle == CalendarDayStyle.FirstRow) {
				//get the day string for this row
				var aDay = EnumHelper.ConvertToWeekDay (mCellInfo.Date.DayOfWeek);
				
				var aString = DSCalendarLanguage.CurrentLanguage.ShortStringForDay ((int)aDay) + " ";
				
				mRightLabel.Text += aString;
			}
			mRightLabel.Text += mCellInfo.Date.Day.ToString ();
			
			if (mCellInfo.ShowMonth) {
				mRightLabel.Text += " " + DSCalendarEnglish.CurrentLanguage.ShortStringForMonth (mCellInfo.Date.Month - 1);
			}
			
			if (!IsSelected || selectedColor == null) 
			{
				mRightLabel.TextColor = (mCellInfo.IsFocus) ? DSCalendarTheme.CurrentTheme.TodayCellTextColor 
				: (mCellInfo.IsCurrent) ? DSCalendarTheme.CurrentTheme.CellTextColor : DSCalendarTheme.CurrentTheme.InActiveCellTextColor;
			}
			else
			{
				mRightLabel.TextColor = DSCalendarTheme.CurrentTheme.CellSelectedTextColor;
			}
			
			float aPos = 0;
			float yPos = 0;
			float offSet = 4;
			
			var fSize = this.StringSize(mRightLabel.Text,mRightLabel.Font,new SizeF(this.Frame.Width,20));
			
			var fHeight = fSize.Height;
			
			//is weekend
			switch (DSCalendarTheme.CurrentTheme.DayLocation)
			{
				case CalendarDayLocation.TopRight:
				{
					aPos = offSet;
					yPos = offSet;
					mRightLabel.TextAlignment = UITextAlignment.Right;
				}
					break;
				case CalendarDayLocation.BottomRight:
				{
					aPos = offSet;
					yPos = this.Frame.Height - (fHeight + offSet);
					mRightLabel.TextAlignment = UITextAlignment.Right;
				}
					break;
				case CalendarDayLocation.TopLeft:
				{
					aPos = offSet;//mLeftLabel.Frame.Left + mLeftLabel.Frame.Width;
					yPos = offSet;
					mRightLabel.TextAlignment = UITextAlignment.Left;
				}
					break;
				case CalendarDayLocation.BottomLeft:
				{
					aPos = offSet;//mLeftLabel.Frame.Left + mLeftLabel.Frame.Width;
					yPos = this.Frame.Height - (fHeight + offSet);
					mRightLabel.TextAlignment = UITextAlignment.Left;
				}
					break;
			}
			
			
			mRightLabel.Frame = new RectangleF(aPos,yPos,fSize.Width,fHeight).Integral();
			
			RectangleF eventFrame;// = null;
			
			if (yPos == offSet)
			{
				float top = ((mRightLabel.Frame.Top + mRightLabel.Frame.Height) + offSet) + 4;
				eventFrame = new RectangleF(0,top,this.Frame.Width,this.Frame.Height - (top + 2));
			}
			else
			{
				eventFrame = new RectangleF(0,offSet,this.Frame.Width,this.Frame.Height - (mRightLabel.Frame.Height + offSet));
			}
			
			mEventView.CellDate = mCellInfo.Date;
			mEventView.Events = mEvents;
			mEventView.Frame = eventFrame.Integral();
			
			if (DSCalendarTheme.CurrentTheme.MoreViewPosition == DSMoreEventsViewPositon.TopRight && MoreItems != 0)
			{
				if (mMoreEventsView != null)
				{
					var width = (this.Frame.Width - mRightLabel.Frame.Left)-4;
					
					mMoreEventsView.IsToday = mCellInfo.IsFocus;
					mMoreEventsView.RemainingItems = MoreItems;
					mMoreEventsView.Frame = new RectangleF(mRightLabel.Frame.Left,0,width, DSCalendarTheme.CurrentTheme.MoreEventsViewHeight).Integral();
				}
				
			}
			
		}
		
		#region Internal Functions

		internal void SingleTouchEvent()
		{
			this.IsSelected = true;
				
			if (this.Superview != null && this.Superview is DSCalendarGridView)
			{
				((DSCalendarGridView)this.Superview).SelectedChanged(this);
			}
		}
		internal void AddNewEvent ()
		{
			if (this.Superview != null && this.Superview is DSCalendarGridView)
			{
				var calendarGrid = (DSCalendarGridView)this.Superview;
				
				calendarGrid.DataSource.AddEventForDate(this.mCellInfo.Date,calendarGrid.Superview as DSCalendarView, this);
			}
		}
		
		/// <summary>
		/// Called when an event is double tapped.
		/// </summary>
		/// <param name="AnEvent">An event.</param>
		/// <param name="sender">Sender.</param>
		internal void DoubleTappedEvent(DSCalendarEvent AnEvent, object sender)
		{
			if (this.Superview != null && this.Superview is DSCalendarGridView)
			{
				((DSCalendarGridView)this.Superview).DoubleTappedEvent(AnEvent, this);
								
			}

		}
		#endregion
		#endregion
	}
}

