// ****************************************************************************
// <copyright file="DSCalendarView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Drawing;
using DSoft.Datatypes.Calendar;
using DSoft.Datatypes.Calendar.Enums;
using DSoft.Datatypes.Calendar.Interfaces;
using DSoft.Datatypes.Calendar.Language;
using MonoTouch.UIKit;
using DSoft.UI.Calendar.Data;
using DSoft.UI.Calendar.Themes;
using MonoTouch.CoreGraphics;

namespace DSoft.UI.Calendar.Views
{
	/// <summary>
	/// DSCalendarView view
	/// </summary>
	public class DSCalendarView : UIView
	{
	
		#region Fields
		private DSCalendarHeaderView mCalendarHeader;
		private DSCalendarGridView mCalendarGrid;
		private DateTime? mCalendarDate;
		
		private IDSCalendarDataSource mDatasource;
		private IDSCalendarDelegate mDelegate;
		private UIViewController mOwnerViewController;
		#endregion
	   
		#region Property
	   
	   /// <summary>
	   /// Gets the calendar grid.
	   /// </summary>
	   /// <value>The calendar grid.</value>
	   public UIView CalendarGrid
	   {
	   		get
	   		{
				return mCalendarGrid;
	   		}
	   }
	   /// <summary>
	   /// Gets or sets the calendar date.
	   /// </summary>
	   /// <value>The calendar date.</value>
	   public DateTime CalendarDate
	   {
	   		get
	   		{
	   			if (mCalendarDate == null)
	   			{
					mCalendarDate = DateTime.Now;

	   			}
	   			
				return mCalendarDate.Value;
	   		}
	   		set
	   		{
	   			if (mCalendarDate.Value != value)
	   			{
	   				mCalendarDate = value;
	   			
					mCalendarGrid.CalendarDate = value;
	   				ReloadData();
	   			}
	   			
	   		}
	   }
	   /// <summary>
	   /// Gets or sets the data source.
	   /// </summary>
	   /// <value>The data source.</value>
		public IDSCalendarDataSource DataSource
	   {
	   		get
	   		{
	   			return mDatasource;
	   		}
	   		set
	   		{
	   			if (mDatasource != value)
	   			{
	   				mDatasource = value;
	   				
	   				if (mCalendarGrid != null)
	   				{
	   					mCalendarGrid.DataSource = mDatasource;
	   				}
	   				
					this.SetNeedsLayout ();
	   			}
	   			
	   		}
	   }
	   
	   /// <summary>
	   /// Gets or sets the delegate.
	   /// </summary>
	   /// <value>The delegate.</value>
	   	public IDSCalendarDelegate Delegate
	   	{
	   		get
	   		{
	   			if (mDelegate == null)
	   			{
					mDelegate = new DSCalendarDefaultDelegate ();
	   			}
				return mDelegate;
	   		}
	   		set
	   		{
	   			if (mDelegate != value)
	   			{
	   				mDelegate = value;
	   				
					
	   			}
				
	   		}
	   	}
	   	
	   	/// <summary>
	   	/// Gets the parent view controller.
	   	/// </summary>
	   	/// <value>The parent view controller.</value>
	   	public UIViewController ParentViewController
	   	{
	   		get
	   		{
				return mOwnerViewController;
	   		}
	   	}
	   	
	   	/// <summary>
	   	/// Gets the selected date.
	   	/// </summary>
	   	/// <value>The selected date.</value>
	   	public DateTime SelectedDate
	   	{
	   		get
	   		{
				return mCalendarGrid.SelectedDate;
	   		}
	   	}
		#endregion
	   
		#region Constuctors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Calendar.Views.DSCalendarView"/> class.
		/// </summary>
		/// <param name="Frame">Frame.</param>
		/// <param name="VC">V.</param>
		public DSCalendarView(RectangleF Frame, UIViewController VC) : base(Frame)
		{
			Initialize (Frame, VC);
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
			
			SetupView();

		}
		
		#endregion
		
		#region Private
		
		/// <summary>
		/// Initialize the specified Frame and VC.
		/// </summary>
		/// <param name="Frame">Frame.</param>
		/// <param name="VC">V.</param>
		private void Initialize(RectangleF Frame, UIViewController VC)
		{
			mOwnerViewController = VC;
						
			this.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
			this.AutosizesSubviews = true;
			
			this.BackgroundColor = DSCalendarTheme.CurrentTheme.CellBackground;
			
			mCalendarHeader = new DSCalendarHeaderView(RectangleF.Empty);	
			mCalendarHeader.Text = String.Format("{0} {1}", DSCalendarEnglish.CurrentLanguage.MonthStrings[CalendarDate.Month-1], CalendarDate.Year)	;
			this.AddSubview(mCalendarHeader);
			
			mCalendarGrid = new DSCalendarGridView(RectangleF.Empty);
			mCalendarGrid.CalendarDate = CalendarDate;
			this.AddSubview(mCalendarGrid);
			
		}
		
		/// <summary>
		/// Setups the view.
		/// </summary>
		private void SetupView()
		{
			var headerHieght = (DSCalendarTheme.CurrentTheme.TitleViewStyle == CalendarTitleStyle.Visible) ? DSCalendarTheme.CurrentTheme.TitleViewHeight : 0;
			var headerFrame = new RectangleF(0,0,this.Bounds.Width, headerHieght).Integral();
			mCalendarHeader.Frame = headerFrame;
			
			var gridFrame = this.Bounds;
			
			var padding = DSCalendarTheme.CurrentTheme.GridMargin;
			var topPadding = (headerHieght == 0) ? padding : 0.0f;
			gridFrame.Y = (headerHieght == 0) ? padding : headerFrame.Height;
			gridFrame.Height =  gridFrame.Height-(headerFrame.Height+(padding + topPadding));
			
			var innerFrame = RectangleF.Inflate(gridFrame, -padding,0).Integral();
			mCalendarGrid.Frame = innerFrame;
			//mCalendarGrid.DataSource = mDatasource;
			
			mCalendarHeader.Text = String.Format("{0} {1}", DSCalendarEnglish.CurrentLanguage.MonthStrings[CalendarDate.Month-1], CalendarDate.Year)	;
		}
		
		#endregion

		#region Functions
		/// <summary>
		/// Reloads the data.
		/// </summary>
		public void ReloadData ()
		{
			//throw new NotImplementedException ();
			this.SetNeedsLayout();
			mCalendarGrid.DataSource = mDatasource;
		}
		
		/// <summary>
		/// Cells for date.
		/// </summary>
		/// <returns>The for date.</returns>
		/// <param name="Date">Date.</param>
		public UIView CellForDate (DateTime Date)
		{
			return ((DSCalendarGridView)CalendarGrid).CellForDate (Date);
		}
		
		#endregion
	}
}

