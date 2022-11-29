// ****************************************************************************
// <copyright file="MetroMoreEventsView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Drawing;
using MonoTouch.UIKit;
using DSoft.UI.Calendar.Themes;

namespace DSoft.UI.Calendar.Views.Metro
{
	/// <summary>
	/// Metro style more events view to show that there are more events than can fit on screen
	/// </summary>
	public class MetroMoreEventsView : DSMoreEventsView
	{
		#region Fields
		private UILabel mTitleLabel;
		private DSTouchView mTouchYView;
		#endregion
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Calendar.Views.Metro.MetroMoreEventsView"/> class.
		/// </summary>
		/// <param name="Frame">Frame.</param>
		public MetroMoreEventsView (RectangleF Frame) : base(Frame)
		{
			this.BackgroundColor = UIColor.Clear;
			
			mTitleLabel = new UILabel(RectangleF.Empty);
			mTitleLabel.BackgroundColor = UIColor.Clear;
			mTitleLabel.TextColor = UIColor.DarkGray;
			mTitleLabel.Font = UIFont.SystemFontOfSize(12);
			mTitleLabel.Text = "Test";
			mTitleLabel.TextAlignment = UITextAlignment.Right;
			mTitleLabel.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleWidth;
			this.AddSubview(mTitleLabel);
			
			mTouchYView = new DSTouchView (RectangleF.Empty);
			mTouchYView.Opaque = true;
			mTouchYView.BackgroundColor = UIColor.Red;
			this.AddSubview(mTouchYView);
			
			mTouchYView.DoubleTap += () => {Console.WriteLine("Dude!");};
		}
		
		#endregion
			
		#region Constuctors
		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw (RectangleF rect)
		{
			mTitleLabel.Frame = RectangleF.Inflate(this.Bounds, -10, 0);
			mTitleLabel.TextColor = (IsToday == true) ? DSCalendarTheme.CurrentTheme.TodayCellTextColor : DSCalendarTheme.CurrentTheme.CellTextColor;
			mTitleLabel.Text = String.Format("{0} events", RemainingItems.ToString());
			
			mTouchYView.Frame = RectangleF.Inflate(this.Bounds, -10, 0);
			
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
			
			
		}
		#endregion
	}
}

