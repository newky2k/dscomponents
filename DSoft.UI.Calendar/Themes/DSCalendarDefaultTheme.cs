// ****************************************************************************
// <copyright file="DSCalendarDefaultTheme.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using MonoTouch.UIKit;
using System.Drawing;
using DSoft.Datatypes.Calendar;
using DSoft.Datatypes.Calendar.Enums;
using DSoft.UI.Calendar.Data;
using DSoft.Datatypes.Calendar.Data;
using DSoft.UI.Calendar.Views;
using DSoft.UI.Calendar.Views.iOS;

namespace DSoft.UI.Calendar.Themes
{
	/// <summary>
	/// Default theme to look like the iPad iCal
	/// </summary>
	public class DSCalendarDefaultTheme : DSCalendarTheme
	{
		#region Properties
		
		#region Title/Date View Properties
		
		/// <summary>
		/// Gets the title view style.
		/// </summary>
		/// <value>The title view style.</value>
		public override CalendarTitleStyle TitleViewStyle
		{
			get
			{
				return CalendarTitleStyle.Visible;
			}
		}
		
		/// <summary>
		/// Gets the height of the title view.
		/// </summary>
		/// <value>The height of the title view.</value>
		public override float TitleViewHeight 
		{
			get
			{
				return 75.0f;
			}
		}
		
		/// <summary>
		/// Gets the title view font.
		/// </summary>
		/// <value>The title view font.</value>
		public override UIFont TitleViewFont 
		{
			get
			{
				return UIFont.SystemFontOfSize(46);
			}
		}
		
		/// <summary>
		/// Gets the color of the title view.
		/// </summary>
		/// <value>The color of the title view.</value>
		public override UIColor TitleViewColor 
		{
			get
			{
				return UIColor.Black;
			}
		}
		
		/// <summary>
		/// Gets the title label position.
		/// </summary>
		/// <value>The title label position.</value>
		public override PointF TitleLabelPosition 
		{
			get
			{
				return new PointF(65,10);
			}
			
		}
		
		
		#endregion
		
		#region Grid Properties
		
		/// <summary>
		/// Gets the day style for showing the days of the week
		/// </summary>
		/// <value>The day style.</value>
		public override CalendarDayStyle DayStyle 
		{
			get
			{
				return CalendarDayStyle.FirstRow;
			}
		}
		
		/// <summary>
		/// Gets the grid margin around the grid
		/// </summary>
		/// <value>The grid padding.</value>
		public override float GridMargin
		{
			get
			{
				return 20.0f;
			}
		}
		
		/// <summary>
		/// Gets the width of the grid border.
		/// </summary>
		/// <value>The width of the grid border.</value>
		public override float GridBorderWidth 
		{
			get
			{
				return 0.1f;
			}
		}
		
		/// <summary>
		/// Gets what day of the week is the start of the week
		/// </summary>
		/// <value>The week start.</value>
		public override CalendarWeekStart WeekStart 
		{
			get
			{
				return CalendarWeekStart.Monday;
			}
		}
		#endregion
		
		#region Cell Properties
		
		/// <summary>
		/// Gets the month display mode.
		/// </summary>
		/// <value>The month display mode.</value>
		public override CalendarMonthDisplayMode MonthDisplayMode
		{
			get
			{
				return CalendarMonthDisplayMode.FirstOnly;
			}
		}
		
		/// <summary>
		/// Gets the day location.
		/// </summary>
		/// <value>The day location.</value>
		public override CalendarDayLocation DayLocation 
		{
			get
			{
				return CalendarDayLocation.TopRight;
			}
		}
		/// <summary>
		/// Background color for the standard cell
		/// </summary>
		/// <value>The cell background.</value>
		public override MonoTouch.UIKit.UIColor CellBackground {
			get 
			{
				return UIColor.White;
			}
		}
		
		/// <summary>
		/// Gets the weekend cell background color
		/// </summary>
		/// <value>The cell weekend background.</value>
		public override MonoTouch.UIKit.UIColor WeekendCellBackground {
			get 
			{
				return new UIColor(251.0f/255.0f,251.0f/255.0f,251.0f/255.0f,1.0f);
			}
		}
		
		/// <summary>
		/// Gets the inactive weekend cell background color
		/// </summary>
		/// <value>The in active weekend cell background.</value>
		public override MonoTouch.UIKit.UIColor InActiveWeekendCellBackground {
			get 
			{
				return new UIColor(251.0f/255.0f,251.0f/255.0f,251.0f/255.0f,1.0f);
			}
		}
		
		/// <summary>
		/// Gets the today cell background.
		/// </summary>
		/// <value>The today cell background.</value>
		public override MonoTouch.UIKit.UIColor TodayCellBackground 
		{
			get 
			{
				return UIColor.White;
			}
		}
		
		/// <summary>
		/// Gets the inactive cell backgrond.
		/// </summary>
		/// <value>The in active cell backgrond.</value>
		public override UIColor InActiveCellBackgrond 
		{
			get
			{
				return CellBackground;
			}
		}
		/// <summary>
		/// Gets the color of the cell border.
		/// </summary>
		/// <value>The color of the cell border.</value>
		public override UIColor CellBorderColor 
		{
			get
			{
				return UIColor.Black;
			}
			
		}
		
		/// <summary>
		/// Gets the selected cell background color
		/// </summary>
		/// <value>The cell selected background.</value>
		public override UIColor CellSelectedBackground
		{
			get
			{
				return null;//UIColor.Black;
			}
			
		}
		#endregion
		
		#region Cell Text properties
		
		/// <summary>
		/// Gets the color of the cell text for a standard day in the current month
		/// </summary>
		/// <value>The color of the cell text.</value>
		public override UIColor CellTextColor
		{
			get 
			{
				return UIColor.Black;
			}
		}
		
		/// <summary>
		/// Gets the color of the today cell text for today
		/// </summary>
		/// <value>The color of the today cell text.</value>
		public override UIColor TodayCellTextColor
		{
			get 
			{
				return UIColor.White;
			}
		}
		/// <summary>
		/// Gets the color of the inactive cell text for a standard day that isn't in the current month
		/// </summary>
		/// <value>The color of the in active cell text.</value>
		public override UIColor InActiveCellTextColor
		{
			get 
			{
				return UIColor.Gray;
			}
		}
		
		/// <summary>
		/// Gets the font for the text in a standard cell
		/// </summary>
		/// <value>The cell text font.</value>
		public override UIFont CellTextFont 
		{
			get
			{
				return UIFont.SystemFontOfSize(12);
			}
			
		}
		
		/// <summary>
		/// Gets the font for the text in a today cell
		/// </summary>
		/// <value>The today cell text font.</value>
		public override UIFont TodayCellTextFont
		{
			get
			{
				return UIFont.BoldSystemFontOfSize(14);
			}
			
		}
		
		/// <summary>
		/// Gets the color of the cell text for a standard day in the current month
		/// </summary>
		/// <value>The color of the cell text.</value>
		public override UIColor CellSelectedTextColor 
		{
			get { return UIColor.White;}
		}
		
		/// <summary>
		/// Gets the today cell header view
		/// </summary>
		/// <value>The today cell header view.</value>
		public override UIView TodayCellHeaderView
		{
			get
			{
				var aNewview = new UIView(RectangleF.Empty);
				aNewview.BackgroundColor = UIColor.Blue;
				
				var todayLabel = new UILabel(new RectangleF(2,0,200,20));
				todayLabel.Text = "Today";
				todayLabel.Font = TodayCellTextFont;
				todayLabel.BackgroundColor = UIColor.Clear;
				todayLabel.TextColor = TodayCellTextColor;
				todayLabel.TextAlignment = UITextAlignment.Left;
				
				aNewview.Add(todayLabel);
				
			    return aNewview;
			}
		}
		#endregion
		
		#region Header Cell Properties
		
		/// <summary>
		/// Gets the header hieght.
		/// </summary>
		/// <value>The header hieght.</value>
		public override float HeaderHeight 
		{	
			get
			{
				return 20;
			}
		}
		
		/// <summary>
		/// Gets the day display mode.
		/// </summary>
		/// <value>The day display mode.</value>
		public override CalendarDayDisplayMode HeaderDayDisplayMode 
		{
			get
			{
				return CalendarDayDisplayMode.Short;
			}
		}
		
		/// <summary>
		/// Gets the header cell background.
		/// </summary>
		/// <value>The header cell background.</value>
		public override UIColor HeaderCellBackground 
		{
			get
			{
				return UIColor.White;
			}
		}
		
		/// <summary>
		/// Gets the color of the cell border.
		/// </summary>
		/// <value>The color of the cell border.</value>
		public override UIColor HeaderCellBorderColor 
		{
			get
			{
				return UIColor.Black;
			}
			
		}
		
		/// <summary>
		/// Gets the color of the header cell text for a standard day in the current month
		/// </summary>
		/// <value>The color of the cell text.</value>
		public override UIColor HeaderCellTextColor
		{
			get 
			{
				return UIColor.Black;
			}
		}
		
		/// <summary>
		/// Gets the font for the header cell text
		/// </summary>
		/// <value>The header cell text font.</value>
		public override UIFont HeaderCellTextFont 
		{	
			get
			{
				return UIFont.SystemFontOfSize(14);
			}
			
		}
		
		/// <summary>
		/// Gets the header cell text alignment.
		/// </summary>
		/// <value>The header cell text alignment.</value>
		public override UITextAlignment HeaderCellTextAlignment 
		{
			get
			{
				return UITextAlignment.Center;
			}
		}
		#endregion
		
		#region Event Properties
		
		/// <summary>
		/// Gets the more events view position.
		/// </summary>
		/// <value>The more view position.</value>
		public override DSMoreEventsViewPositon MoreViewPosition 
		{
			get
			{
				return DSMoreEventsViewPositon.BottomFull;
			}
		}
		
		/// <summary>
		/// Gets the maximum number of events to show in the events view section of the cell
		/// </summary>
		/// <value>The max events.</value>
		public override int MaxEvents 
		{
			get
			{
				return 4;
			}
		}
		
		/// <summary>
		/// Hieght of the more events view, used to show the number of events that don't fit on the cell
		/// </summary>
		/// <value>The height of the more events view.</value>
		public override float MoreEventsViewHeight 
		{
			get
			{
				return 25.0f;
			}
		}
		
		#endregion
		#endregion
		
		#region Functions
		
		/// <summary>
		/// Get the EventView for the Event
		/// </summary>
		/// <returns>The view for event.</returns>
		/// <param name="Data">Data.</param>
		/// <param name="ViewType">View type.</param>
		public override DSEventView EventViewForEvent(DSCalendarEvent Data, DSEventType ViewType)
		{
			return new Views.iOS.iOSEventView(RectangleF.Empty,Data, ViewType);
		}
		
		/// <summary>
		/// Get the more events view
		/// </summary>
		/// <returns>The events view.</returns>
		public override DSMoreEventsView MoreEventsView()
		{
			return new iOSMoreEventsView(RectangleF.Empty);
		}
		
		#endregion
		
	}
}

