// ****************************************************************************
// <copyright file="DSCalendarHeaderCell.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using MonoTouch.UIKit;
using System.Drawing;
using DSoft.UI.Calendar.Themes;


namespace DSoft.UI.Calendar.Views
{
	internal class DSCalendarHeaderCell : UIView
	{
	
		#region Fields
		private String mText;
		private UILabel mTextLabel;
		
		#endregion
		
		#region Properties
		internal String Text
		{
			get 
			{
				return mText;
			}
			set
			{
				mText = value;
					
				this.SetNeedsDisplay();
			}
		}
		
		#endregion
		
		#region Constuctor
		internal DSCalendarHeaderCell(RectangleF Frame) : base(Frame)
		{
			this.Opaque = false;
			this.BackgroundColor = UIColor.Clear;
			
			mTextLabel = new UILabel(RectangleF.Empty);
			mTextLabel.BackgroundColor = UIColor.Clear;
			mTextLabel.TextColor = DSCalendarTheme.CurrentTheme.HeaderCellTextColor;
			mTextLabel.TextAlignment = DSCalendarTheme.CurrentTheme.HeaderCellTextAlignment;
			mTextLabel.Font = DSCalendarTheme.CurrentTheme.HeaderCellTextFont;
			this.AddSubview(mTextLabel);
		}
		
		#endregion
		
		#region Overrides
		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);
			
			DrawCell(rect);
		}
		
		#endregion
		
		#region Functions
		private void DrawCell(RectangleF rect)
		{
			var context = UIGraphics.GetCurrentContext();
			
			DSCalendarTheme.CurrentTheme.HeaderCellBackground.SetFill();
			context.FillRect(rect);
			
			context.SetStrokeColor(DSCalendarTheme.CurrentTheme.HeaderCellBorderColor.CGColor);
			context.SetLineWidth(DSCalendarTheme.CurrentTheme.GridBorderWidth);
			context.StrokeRect(rect);
			
			mTextLabel.Frame = RectangleF.Inflate(this.Bounds,-4,0);
			mTextLabel.Text = mText;
			mTextLabel.Font = DSCalendarTheme.CurrentTheme.HeaderCellTextFont;
		}
		
		#endregion
	}
}

