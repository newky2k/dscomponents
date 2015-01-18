// ****************************************************************************
// <copyright file="DSCalendarGridView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Drawing;
using DSoft.Datatypes.Calendar;
using DSoft.Datatypes.Calendar.Data;
using DSoft.Datatypes.Calendar.Data.Collections;
using DSoft.Datatypes.Calendar.Enums;
using DSoft.Datatypes.Calendar.Interfaces;
using DSoft.Datatypes.Calendar.Language;
using MonoTouch.UIKit;
using DSoft.UI.Calendar.Data;
using DSoft.UI.Calendar.Helpers;
using DSoft.UI.Calendar.Themes;
using MonoTouch.CoreGraphics;

namespace DSoft.UI.Calendar.Views
{
	/// <summary>
	/// DS calendar grid view.
	/// </summary>
	internal class DSCalendarGridView : UIView
	{
		#region Fields
		private int mColumns = 7;
		//private int mRows = 5;
		private DateTime? mCalendarDate;
		private List<object> mRowCells;
		private List<DateTime> mDates;
		private List<DSCalendarHeaderCell> mHeaderCells = new List<DSCalendarHeaderCell>();
		private IDSCalendarDataSource mDataSource;
		private DSCalendarEventCollection mData;
		private DateTime? mSelectedDate;
		#endregion
		
		#region Properties
		
		#region Internal Properties
		/// <summary>
		/// Gets or sets the calendar date.
		/// </summary>
		/// <value>The calendar date.</value>
		internal DateTime CalendarDate
		{
			get
			{
				if (mCalendarDate == null)
				{
					mCalendarDate = DateTime.Now;
				}
				return mCalendarDate.Value;
			}
			set
			{
				if (mCalendarDate != value)
				{
					mCalendarDate = value;
					mDates = null;
					mData = null;
					RowCells = null;
					this.SetNeedsLayout();
				}
								
			}
		}

		/// <summary>
		/// Gets or sets the data source.
		/// </summary>
		/// <value>The data source.</value>
		internal IDSCalendarDataSource DataSource
		{
			get
			{
				return mDataSource;
			}
			set
			{
				mDataSource = value;
				
				RowCells = null;
				mData = null;
				
				SetNeedsLayout();
			}
		}
		#endregion
		
		#region Private Properties
		private DateTime FirstDay
		{
			get
			{
				return new DateTime(CalendarDate.Year, CalendarDate.Month, 1);
			}
		}
		
		private DateTime LastDay
		{
			get
			{
				var totalDays = DateTime.DaysInMonth(CalendarDate.Year, CalendarDate.Month);
				
				return FirstDay.AddDays(totalDays-1);
			}
		}
		
		private int DayOfTheWeek
		{
			get
			{
				if (DSCalendarTheme.CurrentTheme.WeekStart == CalendarWeekStart.Sunday)
				{
					return (int)EnumHelper.ConvertToWeekDayFromSunday(FirstDay.DayOfWeek);
				}
				else
				{
					return (int)EnumHelper.ConvertToWeekDay(FirstDay.DayOfWeek);
				}
				
			}
		}
		
		private int NumberOfDaysInMonth
		{
			get
			{
				return DateTime.DaysInMonth(CalendarDate.Year, CalendarDate.Month);
			}
			
		}
		
		private int NumberOfRows
		{
			get
			{
				
				var posFromLeft = DayOfTheWeek;
				var totalCells = NumberOfDaysInMonth + posFromLeft;
				
				var rows2 = totalCells/7;
				var rows = totalCells % 7;
				
				var mRows = (rows == 0) ? rows2 : rows2 + 1;
				
				
				return mRows;
			}
		}
		
		private List<DateTime> Dates
		{
			get
			{
				if (mDates == null)
				{
					mDates = new List<DateTime>();
					
					var firstDay = new DateTime(CalendarDate.Year, CalendarDate.Month, 1);
					
					var posFromLeft = 0;
					
					if (DSCalendarTheme.CurrentTheme.WeekStart == CalendarWeekStart.Sunday)
					{
						posFromLeft = (int)EnumHelper.ConvertToWeekDayFromSunday(firstDay.DayOfWeek);
					}
					else
					{
						posFromLeft = (int)EnumHelper.ConvertToWeekDay(firstDay.DayOfWeek);
					}

					var totalDays = DateTime.DaysInMonth(CalendarDate.Year, CalendarDate.Month);

					//Days before	
					for (int loop = 0; loop < posFromLeft; loop++)
					{
						var dayMinus = posFromLeft - loop;
						
						var newDay = firstDay.AddDays(-dayMinus);
						
						mDates.Add(newDay);
					}
					
					//days of the month
					for (int loop = 0; loop < totalDays; loop++)
					{
						var newDay = firstDay.AddDays(loop);
						
						mDates.Add(newDay);
						
					}
					
					var LastDay = firstDay.AddDays(totalDays-1);
					
					var missingCells = (7 * NumberOfRows) - mDates.Count;
					
					for (int loop = 0; loop < missingCells; loop++)
					{
						var newDay = LastDay.AddDays(loop+1);
						mDates.Add(newDay);
					}
				
				}
				

				return mDates;
			}
		}
		private List<object> RowCells
		{
			get
			{
				if (mRowCells == null)
				{
					mRowCells = new List<object>();
					
					for (int loop = 0; loop < NumberOfRows; loop++)
					{
						//add the cells for the rows
						var aRowCells = new List<DSCalendarCell>();

						for (int innerloop = 0; innerloop <= mColumns; innerloop++)
						{
							var cell = new DSCalendarCell(RectangleF.Empty);
							aRowCells.Add(cell);
							this.AddSubview(cell);
						}
						
						mRowCells.Add(aRowCells);
					}
		
				}
				
				return mRowCells;
			}
			set
			{
				if (mRowCells != value)
				{
					//remove the old cells from view
					foreach (List<DSCalendarCell> aRow in mRowCells)
					{
						foreach (DSCalendarCell aCell in aRow)
						{
							aCell.RemoveFromSuperview ();
						}
						
					}
						
					mRowCells = value;
				}
			}
		}
		
		private DSCalendarEventCollection Data
		{
			get
			{
				if (mData == null)
				{
					mData  = new DSCalendarEventCollection();
					
					//build the dataitems
					if (DataSource != null)
					{
						mData = DataSource.EventsForDates(Dates[0], Dates[Dates.Count-1]);
					}
				}

				return mData;
			}
		}
		
		#endregion
		
		#region Public properties
		/// <summary>
		/// Gets the selected date.
		/// </summary>
		/// <value>The selected date.</value>
		public DateTime SelectedDate
		{
			get
			{
				if (mSelectedDate ==  null)
				{
					return DateTime.Now;
					
				}
				return mSelectedDate.Value;
			}
		}
		/// <summary>
	    /// Gets or sets the delegate.
	    /// </summary>
	    /// <value>The delegate.</value>

	   	#endregion
		#endregion
		
		#region Constuctor
		public DSCalendarGridView (RectangleF Frame) : base(Frame)
		{
			this.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			this.AutosizesSubviews = true;
			
			this.BackgroundColor = UIColor.White;
			
			if (DSCalendarTheme.CurrentTheme.DayStyle == CalendarDayStyle.Header)
			{
				//setup the header views
				for (int loop = 0; loop < mColumns; loop++)
				{
					var cell = new DSCalendarHeaderCell(RectangleF.Empty);
					mHeaderCells.Add(cell);
					this.AddSubview(cell);
				}
			}

						
		}
		
		#endregion
		
		#region Overrides
		/// <Docs>Lays out subviews.</Docs>
		/// <summary>
		/// Layouts the subviews.
		/// </summary>
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
						
			LayoutCalendar ();
		}
		
		#endregion
		
		#region functions
		#region Private functions
		/// <summary>
		/// Layouts the calendar.
		/// </summary>
		private void LayoutCalendar()
		{
			var cellWidth = this.Bounds.Size.Width/(float)mColumns;
			float headerHeight = (DSCalendarTheme.CurrentTheme.DayStyle == CalendarDayStyle.Header) ? DSCalendarTheme.CurrentTheme.HeaderHeight : 0;
			float left = 0.0f;
			int loop = 0;
			
			if (DSCalendarTheme.CurrentTheme.DayStyle == CalendarDayStyle.Header)
			{
				while (loop < mColumns)
				{
					var cell = mHeaderCells[loop];
					cell.Frame = new RectangleF(left,0,cellWidth, headerHeight).Integral();
					
					var intDay = 0;
					
					if (DSCalendarTheme.CurrentTheme.WeekStart == CalendarWeekStart.Sunday)
					{
						intDay = (int)EnumHelper.ConvertSundayDayToDay((WeekDayFromSunday)loop);
					}
					else
					{
						intDay = loop;
					}
					
					switch (DSCalendarTheme.CurrentTheme.HeaderDayDisplayMode)
					{
						case CalendarDayDisplayMode.Full:
						{
							cell.Text = DSCalendarEnglish.CurrentLanguage.DayStrings[intDay];
						}
							break;
						case CalendarDayDisplayMode.Short:
						{
							cell.Text = DSCalendarEnglish.CurrentLanguage.ShortStringForDay(intDay);
						}
							break;
						default:
						{
							cell.Text = String.Empty;
						}
							break;
					}
					
					left += cellWidth;
					loop++;
				}
			}
			
			float cellHeight = (this.Bounds.Size.Height-headerHeight)/(float)NumberOfRows;
				
			var today = DateTime.Now;
			var cleanedToday  = new DateTime(today.Year, today.Month, today.Day);
			
			var datePos = 0;
			foreach (List<DSCalendarCell> row in RowCells)
			{
				left = 0.0f;
				loop = 0;
				while (loop < mColumns)
				{
					var aDate = Dates[datePos];
					
					var aCellInfo = new DSCalendarCellInfo() {Date = aDate};
					aCellInfo.IsFirstRow = (RowCells.IndexOf(row) == 0);
					aCellInfo.IsFocus = (aCellInfo.Date == cleanedToday);
					
					if ((aDate >= FirstDay) && (aDate <= LastDay))
					{
						aCellInfo.IsCurrent = true;
					} 
					
					switch (DSCalendarTheme.CurrentTheme.MonthDisplayMode)
						{
							case CalendarMonthDisplayMode.FirstOnly:
							{
								if (aDate.Equals(FirstDay))
								{
									aCellInfo.ShowMonth = true;
								}
								
							}
								break;
							case CalendarMonthDisplayMode.OnChange:
							{
								if ((datePos == 0 && loop == 0) || aDate.Equals(FirstDay) || (aDate > LastDay && aDate.Day == 1))
								{
									aCellInfo.ShowMonth = true;
								}
								
							}
								break;
						}
					
					var cell = row[loop];
			
					cell.Events = Data[aDate];
					
					
					cell.Frame = new RectangleF(left,headerHeight,cellWidth, cellHeight).Integral();
					cell.CellInfo = aCellInfo;
					
					if (aCellInfo.Date == mSelectedDate)
						cell.IsSelected = true;
						
					left += cellWidth;
					loop++;
					datePos++;
				}
				headerHeight += cellHeight;
			}
		}
		
		#endregion
		
		#region Internal functions
		
		internal void SelectedChanged(object sender)
		{
			foreach (var aCel in this.Subviews)
			{
				if (aCel is DSCalendarCell)
				{
					if (aCel != sender)
					{
						((DSCalendarCell)aCel).IsSelected = false;
					}
				}

			}
			
			mSelectedDate = ((DSCalendarCell)sender).CellInfo.Date;
		}
		internal void DoubleTappedEvent(DSCalendarEvent AnEvent, object sender)
		{
			if (this.Superview != null && this.Superview is DSCalendarView)
			{				
				var grid = this.Superview as DSCalendarView;
				
				if (grid.DataSource != null)
				{
					grid.DataSource.DidDoubleTapEventView (this.Superview, sender, AnEvent);
				}
				
			}
		}
		
		#endregion
		
		internal UIView CellForDate (DateTime Date)
		{
			foreach (var aCel in this.Subviews) 
			{
				if (aCel is DSCalendarCell) 
				{
					if (((DSCalendarCell)aCel).CellInfo != null)
					{
						if (((DSCalendarCell)aCel).CellInfo.Date == Date)
						return aCel;
					}
				}


			}
			
			return null;
		}
		#endregion
	}
}

