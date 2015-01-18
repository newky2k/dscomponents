// ****************************************************************************
// <copyright file="MetroEventView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.UI.Calendar.Data;
using System.Drawing;
using DSoft.Datatypes.Calendar.Data;
using MonoTouch.UIKit;
using DSoft.Datatypes.Calendar.Enums;
using DSoft.UI.Calendar.Helpers;
using MonoTouch.CoreGraphics;

namespace DSoft.UI.Calendar.Views.Metro
{
	/// <summary>
	/// Metro style event view.
	/// </summary>
	public class MetroEventView : DSEventView
	{
		#region Fields
		private UILabel mTitleLabel;
		#endregion
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Calendar.Views.Metro.MetroEventView"/> class.
		/// </summary>
		/// <param name="Frame">Frame.</param>
		/// <param name="AnEvent">An event.</param>
		/// <param name="ViewType">View type.</param>
		public MetroEventView (RectangleF Frame, DSCalendarEvent AnEvent, DSEventType ViewType) : base(Frame, AnEvent, ViewType)
		{
			this.BackgroundColor = UIColor.Clear;
			
			mTitleLabel = new UILabel(Rectangle.Empty);
			mTitleLabel.BackgroundColor = UIColor.Clear;
			mTitleLabel.TextColor = UIColor.White;
			mTitleLabel.TextAlignment = UITextAlignment.Left;
			mTitleLabel.Font = UIFont.SystemFontOfSize(14);
			mTitleLabel.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			this.AddSubview(mTitleLabel);
		}
		
		#endregion
		
		#region Overrides
		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw (RectangleF rect)
		{			
			DrawView(rect);
		}
		
		#endregion
		
		#region Functions
		private void DrawView(RectangleF rect)
		{
			var color = Event.EventColor.ToUIColor();
			
			color.SetFill();
			
			float offSet = -2;
			var fillRect = RectangleF.Inflate(rect,offSet,-1);
			
			switch (ViewType)
			{
				case DSEventType.Left:
				{
					fillRect = rect;
					fillRect.X = 2;
				}
					break;
				case DSEventType.Middle:
				{
					fillRect = rect;
				}
					break;
				case DSEventType.Right:
				{
					fillRect = rect;
					fillRect.Width -= 2;
				}
					break;
			}

			UIGraphics.GetCurrentContext().FillRect(fillRect);
			
			if (ViewType == DSEventType.Left || ViewType == DSEventType.Single)
			{
				var titleString = "";
				
				if (Event.IsAllDay)
				{
					titleString = Event.Title;
				}
				else
				{
					if (Event.IsAllDay)
					{
						titleString = Event.Title;
						
					}
					else
					{
						var hourString = String.Format ("{0}:{1}", Event.StartDate.Hour, Event.StartDate.Minute.ToString("00"));
						titleString = String.Format(@"{0}   {1}",hourString, Event.Title);
					}

				}
				
				mTitleLabel.Text = titleString;
			
				var titleFrame = this.Bounds;
				titleFrame.Inflate(-6.0f, -2.0f);
				
				var cleanedFrame = titleFrame.Integral ();
				mTitleLabel.Frame = cleanedFrame;
				
				
				//mTitleLabel.Center = new PointF ((int)Math.Round(mTitleLabel.Center.X), (int)Math.Round(mTitleLabel.Center.Y));
			}
		}
	
		#endregion
	}
}

