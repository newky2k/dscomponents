// ****************************************************************************
// <copyright file="DSTouchView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace DSoft.UI.Calendar.Views
{
	/// <summary>
	/// Delegeare for touch event
	/// </summary>
	public delegate void TouchedDelegate();
	
	/// <summary>
	/// DSTouchView
	/// </summary>
	public class DSTouchView : UIView
	{
		/// <summary>
		/// Occurs when single tap.
		/// </summary>
		public event TouchedDelegate SingleTap = delegate {};
		
		/// <summary>
		/// Occurs when double tap.
		/// </summary>
		public event TouchedDelegate DoubleTap = delegate {};
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Calendar.Views.DSTouchView"/> class.
		/// </summary>
		/// <param name="Frame">Frame.</param>
		public DSTouchView (RectangleF Frame) : base(Frame)
		{
		
		}
		#endregion
		
		/// <summary>
		/// Toucheses the ended.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
		
			HandleTouches (touches, evt);
		}
		
		
		/// <summary>
		/// Handles the touches.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		private void HandleTouches(NSSet touches, UIEvent evt)
		{
			UITouch touch = touches.AnyObject as UITouch;
			
			if (touch != null) {
				switch (touch.TapCount) {
				case 1:
					{
						this.PerformSelector(new MonoTouch.ObjCRuntime.Selector("DidSingleTap"),null,0.2f);
					}
					break;
				case 2:
					{
						NSObject.CancelPreviousPerformRequest(this, new MonoTouch.ObjCRuntime.Selector("DidSingleTap"),null);
						this.PerformSelector(new MonoTouch.ObjCRuntime.Selector("DidDoubleTap"),null,0.01f);

					}
					break;
				}
			}
			
		}
		
		/// <summary>
		/// Dids the single tap.
		/// </summary>
		[Export("DidSingleTap")]
		protected void DidSingleTap()
		{
			SingleTap ();
		}
		
		/// <summary>
		/// Dids the double tap.
		/// </summary>
		[Export("DidDoubleTap")]
		protected void DidDoubleTap()
		{
			DoubleTap();
		}
		
	}
}

