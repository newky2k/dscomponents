// ****************************************************************************
// <copyright file="DSCalendarViewController.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using MonoTouch.UIKit;
using System.Drawing;
using DSoft.UI.Calendar.Views;
using DSoft.Datatypes.Calendar.Interfaces;
using MonoTouch.Foundation;
using MonoTouch.EventKit;
using System.Collections.Generic;

namespace DSoft.UI.Calendar.ViewControlllers
{
	/// <summary>
	/// DSCalendarView controller
	/// </summary>
	public class DSCalendarViewController : UIViewController
	{
		#region Fields
		private IDSCalendarDataSource mDataSource;
		private DSCalendarView mCalendarView;
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets the calendar view.
		/// </summary>
		/// <value>The calendar view.</value>
		public DSCalendarView CalendarView
		{
			get
			{
				return mCalendarView;
			}
		}
		/// <summary>
		/// Get and Sets the DSCalendarView datasource
		/// </summary>
		/// <value>The data source.</value>
		public IDSCalendarDataSource DataSource
		{
			get
			{
				return mDataSource;
			}
			set
			{
				mDataSource = value;
			}
		}
		
		#endregion
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Calendar.ViewControlllers.DSCalendarViewController"/> class.
		/// </summary>
		public DSCalendarViewController ()
		{
			mCalendarView = new DSCalendarView(RectangleF.Empty, this);
			this.View.AddSubview(mCalendarView);
			
		}
		
		#endregion
		
		#region Overrides
		/// <summary>
		/// ViewWillAppear
		/// </summary>
		/// <param name="animated">If set to <c>true</c> animated.</param>
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			
			var calendarRect = this.View.Bounds;
			
			mCalendarView.Frame = calendarRect;
		}
		
		/// <summary>
		/// ViewDidLoad
		/// </summary>
		public override void ViewDidLoad ()
		{
			//load the datasource off the ui thread
			this.PerformSelector(new MonoTouch.ObjCRuntime.Selector("LazyLoadDataSource"),null,0.2f);
		}
		
		/// <summary>
		/// WillRotate
		/// </summary>
		/// <param name="toInterfaceOrientation">To interface orientation.</param>
		/// <param name="duration">Duration.</param>
		public override void WillRotate (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillRotate (toInterfaceOrientation, duration);
			
			this.DataSource.HandleRotation (false);
		}
		
		/// <summary>
		/// DidRotate
		/// </summary>
		/// <param name="fromInterfaceOrientation">From interface orientation.</param>
		public override void DidRotate (UIInterfaceOrientation fromInterfaceOrientation)
		{
			base.DidRotate (fromInterfaceOrientation);
			
			this.DataSource.HandleRotation (true);
		}
		
		#endregion
		
		#region Functions
		/// <summary>
		/// Loads the data source.
		/// </summary>
		[Export("LazyLoadDataSource")]
		protected void LoadDataSource()
		{
			mCalendarView.DataSource = DataSource;
		}
		
		#endregion
	}
}

