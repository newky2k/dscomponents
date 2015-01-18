// ****************************************************************************
// <copyright file="OSXEventView.cs" company="DSoft Developments">
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

namespace DSoft.UI.Calendar.Views.OSX
{
	/// <summary>
	/// OSX event view.
	/// </summary>
	public class OSXEventView : DSEventView
	{
		private UILabel mTitleLabel;

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Calendar.Views.OSX.OSXEventView"/> class.
		/// </summary>
		/// <param name="Frame">Frame.</param>
		/// <param name="AnEvent">An event.</param>
		/// <param name="ViewType">View type.</param>
		public OSXEventView (RectangleF Frame, DSCalendarEvent AnEvent, DSEventType ViewType) : base(Frame, AnEvent, ViewType)
		{
			this.BackgroundColor = UIColor.Clear;
			
			mTitleLabel = new UILabel(RectangleF.Empty);
			mTitleLabel.BackgroundColor = UIColor.Clear;
			mTitleLabel.TextColor = UIColor.Black;
			mTitleLabel.Font = UIFont.SystemFontOfSize(12);
			this.AddSubview(mTitleLabel);
		}
		
		#endregion
		
		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);
			
			DrawView(rect);
		}
		
		/// <summary>
		/// Draws the view.
		/// </summary>
		/// <param name="rect">Rect.</param>
		private void DrawView(RectangleF rect)
		{
			mTitleLabel.Text = Event.Title;
			
			var titleFrame = this.Bounds;
			titleFrame.Inflate(-2.0f, -2.0f);
			
			mTitleLabel.Frame = titleFrame;
		}
	}
}

