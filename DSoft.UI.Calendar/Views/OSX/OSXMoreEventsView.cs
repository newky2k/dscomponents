// ****************************************************************************
// <copyright file="OSXMoreEventsView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace DSoft.UI.Calendar.Views.OSX
{
	/// <summary>
	/// OSX more events view.
	/// </summary>
	public class OSXMoreEventsView : DSMoreEventsView
	{		
		private UILabel mTitleLabel;
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Calendar.Views.OSX.OSXMoreEventsView"/> class.
		/// </summary>
		/// <param name="Frame">Frame.</param>
		public OSXMoreEventsView (RectangleF Frame) : base(Frame)
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

