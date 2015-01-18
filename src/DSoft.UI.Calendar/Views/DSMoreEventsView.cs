// ****************************************************************************
// <copyright file="DSMoreEventsView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;

namespace DSoft.UI.Calendar.Views
{
	/// <summary>
	/// Abstract class for presenting the more events view controller
	/// </summary>
	public abstract class DSMoreEventsView : DSTouchView
	{
		#region Fields
		private int mRemainingItems;
		private bool mIsToday;
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets or sets the remaining items.
		/// </summary>
		/// <value>The remaining items.</value>
		internal int RemainingItems
		{
			get
			{
				return mRemainingItems;
			}
			set
			{
				if (mRemainingItems != value)
				{
					mRemainingItems = value;
					
					SetNeedsDisplay();
				}
			}
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether this instance is today.
		/// </summary>
		/// <value><c>true</c> if this instance is today; otherwise, <c>false</c>.</value>
		internal bool IsToday
		{
			get
			{
				return mIsToday;
			}
			set
			{
				mIsToday = value;
			}
		}
		
		#endregion
		
		#region Constuctors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Calendar.Views.DSMoreEventsView"/> class.
		/// </summary>
		/// <param name="Frame">Frame.</param>
		public DSMoreEventsView (RectangleF Frame) : base(Frame)
		{
			
			Initialize ();
			
		}
		#endregion
		
		#region Functions
		
		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize()
		{
			this.AutosizesSubviews = true;
			this.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			this.ClipsToBounds = true;
			this.Opaque = false;
			
			this.DoubleTap += () => 
			{
				Console.WriteLine("Add new item");
			};
		}
		
		
		#endregion
	}
}

