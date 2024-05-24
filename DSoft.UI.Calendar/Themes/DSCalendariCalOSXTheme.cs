// ****************************************************************************
// <copyright file="DSCalendariCalOSXTheme.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Drawing;
using MonoTouch.UIKit;
using DSoft.UI.Calendar.Data;
using DSoft.Datatypes.Calendar.Data;
using DSoft.Datatypes.Calendar.Enums;
using DSoft.UI.Calendar.Views;
using DSoft.UI.Calendar.Views.OSX;

namespace DSoft.UI.Calendar.Themes
{
	/// <summary>
	/// iCalOSX theme.
	/// </summary>
	public class DSCalendariCalOSXTheme : DSCalendarDefaultTheme
	{
		/// <summary>
		/// Gets the today cell background.
		/// </summary>
		/// <value>The today cell background.</value>
		public override MonoTouch.UIKit.UIColor TodayCellBackground 
		{
			get 
			{
				return new UIColor(232.0f/255.0f,239.0f/255.0f,247.0f/255.0f,1.0f);
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
				return new UIColor(52.0f/255.0f,144.0f/255.0f,245.0f/255.0f,1.0f);
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
				return new PointF(20,10);
			}
			
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
				aNewview.BackgroundColor = UIColor.Clear;
				
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
		
		#region Functions
		
		/// <summary>
		/// Get the EventView for the Event
		/// </summary>
		/// <returns>The view for event.</returns>
		/// <param name="Data">Data.</param>
		/// <param name="ViewType">View type.</param>
		public override DSEventView EventViewForEvent(DSCalendarEvent Data, DSEventType ViewType)
		{
			return new Views.OSX.OSXEventView(RectangleF.Empty,Data, ViewType);
		}
		
		/// <summary>
		/// Get the more events view
		/// </summary>
		/// <returns>The events view.</returns>
		public override DSMoreEventsView MoreEventsView()
		{
			return new OSXMoreEventsView(RectangleF.Empty);
		}
		
		#endregion
	}
}

