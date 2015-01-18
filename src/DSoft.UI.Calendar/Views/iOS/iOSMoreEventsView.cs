// ****************************************************************************
// <copyright file="iOSMoreEventsView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Drawing;
using MonoTouch.UIKit;
using DSoft.UI.Calendar.Themes;

namespace DSoft.UI.Calendar.Views.iOS
{
	/// <summary>
	/// iOS style more events view to show that there are more events than can fit on screen
	/// </summary>
	public class iOSMoreEventsView : DSMoreEventsView
	{
		#region Fields
		private UILabel mTitleLabel;
		#endregion
		
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Calendar.Views.iOS.iOSMoreEventsView"/> class.
		/// </summary>
		/// <param name="Frame">Frame.</param>
		public iOSMoreEventsView (RectangleF Frame) : base(Frame)
		{
			this.BackgroundColor = UIColor.Clear;
			
			mTitleLabel = new UILabel(RectangleF.Empty);
			mTitleLabel.BackgroundColor = UIColor.Clear;
			mTitleLabel.TextColor = UIColor.DarkGray;
			mTitleLabel.Font = UIFont.SystemFontOfSize(12);
			mTitleLabel.Text = "Test";
			
			this.AddSubview(mTitleLabel);
		}
		
		#endregion
			
		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw (RectangleF rect)
		{
			mTitleLabel.Frame = RectangleF.Inflate(this.Bounds, -10, 0);
			mTitleLabel.TextColor = (IsToday == true) ? DSCalendarTheme.CurrentTheme.TodayCellTextColor : DSCalendarTheme.CurrentTheme.CellTextColor;
			mTitleLabel.Text = String.Format("{0} more...", RemainingItems.ToString());
			
		}
		
		/// <Docs>Lays out subviews.</Docs>
		/// <summary>
		/// Layouts the subviews.
		/// </summary>
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();
			
			
		}
	}
}

