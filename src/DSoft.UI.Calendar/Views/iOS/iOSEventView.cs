// ****************************************************************************
// <copyright file="iOSEventView.cs" company="DSoft Developments">
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

namespace DSoft.UI.Calendar.Views.iOS
{
	/// <summary>
	/// iOS style event view.
	/// </summary>
	public class iOSEventView : DSEventView
	{
		#region Fields
		private UILabel mTitleLabel;
		#endregion
		
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Calendar.Views.iOS.iOSEventView"/> class.
		/// </summary>
		/// <param name="Frame">Frame.</param>
		/// <param name="AnEvent">An event.</param>
		/// <param name="ViewType">View type.</param>
		public iOSEventView (RectangleF Frame, DSCalendarEvent AnEvent, DSEventType ViewType) : base(Frame, AnEvent, ViewType)
		{
			this.BackgroundColor = UIColor.Clear;
			
			mTitleLabel = new UILabel(RectangleF.Empty);
			mTitleLabel.BackgroundColor = UIColor.Clear;
			mTitleLabel.TextColor = UIColor.Black;
			mTitleLabel.Font = UIFont.SystemFontOfSize(12);
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
			base.Draw (rect);
			
			DrawView(rect);
		}
		
		#endregion
		
		#region Functions
		private void DrawView(RectangleF rect)
		{
			mTitleLabel.Text = Event.Title;
			
			var titleFrame = this.Bounds;
			titleFrame.Inflate(-2.0f, -2.0f);
			
			mTitleLabel.Frame = titleFrame;
		}
		
		#endregion
	}
}

