// ****************************************************************************
// <copyright file="DSCalendarHeaderView.cs" company="DSoft Developments">
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
	/// <summary>
	/// Draws the header element
	/// </summary>
	internal class DSCalendarHeaderView : UIView
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
				mText  = value;
				
				this.SetNeedsDisplay();
			}
		}
		
		#endregion
		
		#region constuctors
		internal DSCalendarHeaderView (RectangleF Frame) : base(Frame)
		{
			Initialize ();
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
			
			DrawView (rect);
		}
		#endregion
		
		#region Functions
		
		private void Initialize()
		{
			this.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			this.AutosizesSubviews = true;
			
			this.Opaque = false;
			this.BackgroundColor = UIColor.Clear;
			
			mTextLabel = new UILabel(RectangleF.Empty);
			mTextLabel.BackgroundColor = UIColor.Clear;
			mTextLabel.TextColor = DSCalendarTheme.CurrentTheme.TitleViewColor;
			mTextLabel.Font = DSCalendarTheme.CurrentTheme.TitleViewFont;
			this.AddSubview(mTextLabel);

		}
		/// <summary>
		/// Draws the view.
		/// </summary>
		/// <param name="rect">Rect.</param>
		private void DrawView(RectangleF rect)
		{
			var aSize = this.StringSize(mText,mTextLabel.Font);
			
			mTextLabel.Frame = new RectangleF(DSCalendarTheme.CurrentTheme.TitleLabelPosition, aSize);
			mTextLabel.Text = mText;
		}
		#endregion
	}
}

