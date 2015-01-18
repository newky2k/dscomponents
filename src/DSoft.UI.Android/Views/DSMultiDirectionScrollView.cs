// ****************************************************************************
// <copyright file="DSMultiDirectionScrollView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using Java.Lang;
using Android.Views.Animations;
using DSoft.Datatypes.Types;

namespace DSoft.UI
{
	/// <summary>
	/// DS multi direction scroll view.
	/// </summary>
	internal class DSMultiDirectionScrollView : FrameLayout
	{
		#region Fields
		private const int ANIMATED_SCROLL_GAP = 250;
		private const float MAX_SCROLL_FACTOR = 0.5f;
		private long mLastScroll;
		private Rect mTempRect = new Rect ();
		private Scroller mScroller;
		
		/**
	     * Flag to indicate that we are moving focus ourselves. This is so the
	     * code that watches for focus changes initiated outside this TwoDScrollView
	     * knows that it does not have to do anything.
	     */
		private bool mTwoDScrollViewMovedFocus;
		/**
        * Position of the last motion event.
        */
		private float mLastMotionY;
		private float mLastMotionX;
		/**
        * True when the layout has changed but the traversal has not come through yet.
        * Ideally the view hierarchy would keep track of this for us.
        */
		private bool mIsLayoutDirty = true;
		/**
        * The child to give focus to in the event that a child has requested focus while the
        * layout is dirty. This prevents the scroll from being wrong if the child has not been
        * laid out before requesting focus.
        */
		private View mChildToScrollTo = null;
		/**
        * True if the user is currently dragging this TwoDScrollView around. This is
        * not the same as 'is being flinged', which can be checked by
        * mScroller.isFinished() (flinging begins when the user lifts his finger).
        */
		private bool mIsBeingDragged = false;
		/**
        * Determines speed during touch scrolling
        */
		private VelocityTracker mVelocityTracker;
		/**
        * Whether arrow scrolling is animated.
        */
		private int mTouchSlop;
		private int mMinimumVelocity;
		private int mMaximumVelocity;
		#endregion
		
		#region Constructors
		protected DSMultiDirectionScrollView (IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
		{
			Initialize ();
		}

		public DSMultiDirectionScrollView (Context context)
            : base(context)
		{
			Initialize ();
		}

		public DSMultiDirectionScrollView (Context context, IAttributeSet attrs)
            : base(context, attrs)
		{
			Initialize ();
		}

		public DSMultiDirectionScrollView (Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
		{
			Initialize ();
		}
		#endregion
		
		#region Properties

		/// <summary>
		/// Gets the content offset.
		/// </summary>
		/// <value>The content offset.</value>
		public DSPoint ContentOffset
		{
			get
			{
				return new DSPoint (mScroller.CurrX, mScroller.CurrY);
			}
		}

		/// <summary>
		/// Gets the frame.
		/// </summary>
		/// <value>The frame.</value>
		public DSRectangle Frame
		{
			get
			{
				return new DSRectangle (Left, Top, Width, Height);
			}
		}

		/// <summary>
		/// The maximum amount this scroll view will scroll in response to an arrow event.
		/// </summary>
		/// <value>The max scroll amount vertical.</value>
		public int MaxScrollAmountVertical
		{
			get
			{
				return (int)(MAX_SCROLL_FACTOR * Height);
			}
		}
		
		/// <summary>
		/// The maximum amount this scroll view will scroll in response to an arrow event.
		/// </summary>
		/// <value>The max scroll amount horizontal.</value>
		public int MaxScrollAmountHorizontal
		{
			get
			{
				return (int)(MAX_SCROLL_FACTOR * Width);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance can scroll.
		/// </summary>
		/// <value><c>true</c> if this instance can scroll; otherwise, <c>false</c>.</value>
		private bool CanScroll
		{
			get
			{
				View child = GetChildAt (0);
				if (child != null)
				{
					int childHeight = child.Height;
					int childWidth = child.Width;
					return (Height < childHeight + PaddingTop + PaddingBottom) ||
					(Width < childWidth + PaddingLeft + PaddingRight);
				}
				return false;
			}
		}

		#endregion
		
		#region Overrides
		public override void AddView (View child)
		{
			if (ChildCount > 0)
			{
				throw new IllegalStateException ("DHMultiDirectionScrollView can host only one direct child");
			}
			base.AddView (child);
		}

		public override void AddView (View child, int index)
		{
			if (ChildCount > 0)
			{
				throw new IllegalStateException ("DHMultiDirectionScrollView can host only one direct child");
			}
			base.AddView (child, index);
		}

		public override void AddView (View child, ViewGroup.LayoutParams p)
		{
			if (ChildCount > 0)
			{
				throw new IllegalStateException ("DHMultiDirectionScrollView can host only one direct child");
			}
			base.AddView (child, p);
		}

		public override void AddView (View child, int index, ViewGroup.LayoutParams p)
		{
			if (ChildCount > 0)
			{
				throw new IllegalStateException ("DHMultiDirectionScrollView can host only one direct child");
			}
			base.AddView (child, index, p);
		}		
		public override bool DispatchKeyEvent (KeyEvent e)
		{
			// Let the focused view and/or our descendants get the key first
			bool handled = base.DispatchKeyEvent (e);
			if (handled)
			{
				return true;
			}
			return ExecuteKeyEvent (e);
		}

		public override bool OnInterceptTouchEvent (MotionEvent ev)
		{
			/*
            * This method JUST determines whether we want to intercept the motion.
            * If we return true, onMotionEvent will be called and we do the actual
            * scrolling there.
            *
            * Shortcut the most recurring case: the user is in the dragging
            * state and he is moving his finger.  We want to intercept this
            * motion.
            */
			MotionEventActions action = ev.Action;
			if ((action == MotionEventActions.Move) && (mIsBeingDragged))
			{
				return true;
			}
			if (!CanScroll)
			{
				mIsBeingDragged = false;
				return false;
			}
			float y = ev.GetY ();
			float x = ev.GetX ();
			switch (action)
			{
				case MotionEventActions.Move:
                    /*
                    * mIsBeingDragged == false, otherwise the shortcut would have caught it. Check
                    * whether the user has moved far enough from his original down touch.
                    */
                    /*
                    * Locally do absolute value. mLastMotionY is set to the y value
                    * of the down event.
                    */
					int yDiff = (int)System.Math.Abs (y - mLastMotionY);
					int xDiff = (int)System.Math.Abs (x - mLastMotionX);
					if (yDiff > mTouchSlop || xDiff > mTouchSlop)
					{
						mIsBeingDragged = true;
					}
					break;

				case MotionEventActions.Down:
                    /* Remember location of down touch */
					mLastMotionY = y;
					mLastMotionX = x;

                    /*
                    * If being flinged and user touches the screen, initiate drag;
                    * otherwise don't.  mScroller.isFinished should be false when
                    * being flinged.
                    */
					mIsBeingDragged = !mScroller.IsFinished;
					break;

				case MotionEventActions.Cancel:
				case MotionEventActions.Up:
                    /* Release the drag */
					mIsBeingDragged = false;
					break;
			}

			/*
            * The only time we want to intercept motion events is if we are in the
            * drag mode.
            */
			return mIsBeingDragged;
		}

		public override bool OnTouchEvent (MotionEvent ev)
		{

			if (ev.Action == MotionEventActions.Down && ev.EdgeFlags != 0)
			{
				// Don't handle edge touches immediately -- they may actually belong to one of our
				// descendants.
				return false;
			}

			if (!CanScroll)
			{
				return false;
			}

			if (mVelocityTracker == null)
			{
				mVelocityTracker = VelocityTracker.Obtain ();
			}
			mVelocityTracker.AddMovement (ev);

			MotionEventActions action = ev.Action;
			float y = ev.GetY ();
			float x = ev.GetX ();

			switch (action)
			{
				case MotionEventActions.Down:
                    /*
                    * If being flinged and user touches, stop the fling. isFinished
                    * will be false if being flinged.
                    */
					if (!mScroller.IsFinished)
					{
						mScroller.AbortAnimation ();
					}

                    // Remember where the motion event started
					mLastMotionY = y;
					mLastMotionX = x;
					break;
				case MotionEventActions.Move:
                    // Scroll to follow the motion event
					int deltaX = (int)(mLastMotionX - x);
					int deltaY = (int)(mLastMotionY - y);
					mLastMotionX = x;
					mLastMotionY = y;

					if (deltaX < 0)
					{
						if (ScrollX < 0)
						{
							deltaX = 0;
						}
					}
					else if (deltaX > 0)
					{
						int rightEdge = Width - PaddingRight;
						int availableToScroll = GetChildAt (0).Right - ScrollX - rightEdge;
						if (availableToScroll > 0)
						{
							deltaX = System.Math.Min (availableToScroll, deltaX);
						}
						else
						{
							deltaX = 0;
						}
					}
					if (deltaY < 0)
					{
						if (ScrollY < 0)
						{
							deltaY = 0;
						}
					}
					else if (deltaY > 0)
					{
						int bottomEdge = Height - PaddingBottom;
						int availableToScroll = GetChildAt (0).Bottom - ScrollY - bottomEdge;
						if (availableToScroll > 0)
						{
							deltaY = System.Math.Min (availableToScroll, deltaY);
						}
						else
						{
							deltaY = 0;
						}
					}
					if (deltaY != 0 || deltaX != 0)
						ScrollBy (deltaX, deltaY);
					break;
				case MotionEventActions.Up:
					VelocityTracker velocityTracker = mVelocityTracker;
					velocityTracker.ComputeCurrentVelocity (1000, mMaximumVelocity);
					int initialXVelocity = (int)velocityTracker.XVelocity;
					int initialYVelocity = (int)velocityTracker.YVelocity;
					if ((System.Math.Abs (initialXVelocity) + System.Math.Abs (initialYVelocity) > mMinimumVelocity) && ChildCount > 0)
					{
						Fling (-initialXVelocity, -initialYVelocity);
					}
					if (mVelocityTracker != null)
					{
						mVelocityTracker.Recycle ();
						mVelocityTracker = null;
					}
					break;
			}
			return true;
		}
		
		protected override int ComputeVerticalScrollRange ()
		{
			int count = ChildCount;
			return count == 0 ? Height : (GetChildAt (0)).Bottom;
		}

		protected override int ComputeHorizontalScrollRange ()
		{
			int count = ChildCount;
			return count == 0 ? Width : (GetChildAt (0)).Right;
		}

		protected override void MeasureChild (View child, int parentWidthMeasureSpec, int parentHeightMeasureSpec)
		{
			ViewGroup.LayoutParams lp = child.LayoutParameters;
			int childWidthMeasureSpec;
			int childHeightMeasureSpec;

			childWidthMeasureSpec = GetChildMeasureSpec (parentWidthMeasureSpec, PaddingLeft + PaddingRight, lp.Width);
			childHeightMeasureSpec = MeasureSpec.MakeMeasureSpec (0, MeasureSpecMode.Unspecified);

			child.Measure (childWidthMeasureSpec, childHeightMeasureSpec);
		}

		protected override void MeasureChildWithMargins (View child, int parentWidthMeasureSpec, int widthUsed, int parentHeightMeasureSpec, int heightUsed)
		{
			MarginLayoutParams lp = (MarginLayoutParams)child.LayoutParameters;
			int childWidthMeasureSpec = GetChildMeasureSpec (parentWidthMeasureSpec,
			                                                         PaddingLeft + PaddingRight + lp.LeftMargin + lp.RightMargin + widthUsed, lp.Width);
			int childHeightMeasureSpec = MeasureSpec.MakeMeasureSpec (lp.TopMargin + lp.BottomMargin, MeasureSpecMode.Unspecified);

			child.Measure (childWidthMeasureSpec, childHeightMeasureSpec);
		}

		public override void ComputeScroll ()
		{
			if (mScroller.ComputeScrollOffset ())
			{
				// This is called at drawing time by ViewGroup.  We don't want to
				// re-show the scrollbars at this point, which scrollTo will do,
				// so we replicate most of scrollTo here.
				//
				//         It's a little odd to call onScrollChanged from inside the drawing.
				//
				//         It is, except when you remember that computeScroll() is used to
				//         animate scrolling. So unless we want to defer the onScrollChanged()
				//         until the end of the animated scrolling, we don't really have a
				//         choice here.
				//
				//         I agree.  The alternative, which I think would be worse, is to post
				//         something and tell the subclasses later.  This is bad because there
				//         will be a window where mScrollX/Y is different from what the app
				//         thinks it is.
				//
				int oldX = ScrollX;
				int oldY = ScrollY;
				int x = mScroller.CurrX;
				int y = mScroller.CurrY;
				if (ChildCount > 0)
				{
					View child = GetChildAt (0);
					ScrollTo (Clamp (x, Width - PaddingRight - PaddingLeft, child.Width),
					                        Clamp (y, Height - PaddingBottom - PaddingTop, child.Height));
				}
				else
				{
					ScrollTo (x, y);
				}
				if (oldX != ScrollX || oldY != ScrollY)
				{
					OnScrollChanged (ScrollX, ScrollY, oldX, oldY);
				}

				// Keep on drawing until the animation has finished.
				PostInvalidate ();
			}
		}
		
		public override bool RequestChildRectangleOnScreen (View child, Rect rectangle, bool immediate)
		{
			// offset into coordinate space of this scroll view
			rectangle.Offset (child.Left - child.ScrollX, child.Top - child.ScrollY);
			return ScrollToChildRect (rectangle, immediate);
		}

		public override void RequestLayout ()
		{
			mIsLayoutDirty = true;
			base.RequestLayout ();
		}

		protected override void OnLayout (bool changed, int l, int t, int r, int b)
		{
			base.OnLayout (changed, l, t, r, b);
			mIsLayoutDirty = false;
			// Give a child focus if it needs it
			if (mChildToScrollTo != null && IsViewDescendantOf (mChildToScrollTo, this))
			{
				ScrollToChild (mChildToScrollTo);
			}
			mChildToScrollTo = null;

			// Calling this with the present values causes it to re-clam them
			ScrollTo (ScrollX, ScrollY);
		}

		protected override void OnSizeChanged (int w, int h, int oldw, int oldh)
		{
			base.OnSizeChanged (w, h, oldw, oldh);

			View currentFocused = FindFocus ();
			if (null == currentFocused || this == currentFocused)
				return;

			// If the currently-focused view was visible on the screen when the
			// screen was at the old height, then scroll the screen to make that
			// view visible with the new screen height.
			currentFocused.GetDrawingRect (mTempRect);
			OffsetDescendantRectToMyCoords (currentFocused, mTempRect);
			int scrollDeltaX = ComputeScrollDeltaToGetChildRectOnScreen (mTempRect);
			int scrollDeltaY = ComputeScrollDeltaToGetChildRectOnScreen (mTempRect);
			DoScroll (scrollDeltaX, scrollDeltaY);
		}
		
		public override void RequestChildFocus (View child, View focused)
		{
			if (!mTwoDScrollViewMovedFocus)
			{
				if (!mIsLayoutDirty)
				{
					ScrollToChild (focused);
				}
				else
				{
					// The child may not be laid out yet, we can't compute the scroll yet
					mChildToScrollTo = focused;
				}
			}
			base.RequestChildFocus (child, focused);
		}
		#endregion
		
		#region Methods
		
				
		private void Initialize ()
		{
			mScroller = new Scroller (Context);
			Focusable = true;
			DescendantFocusability = Android.Views.DescendantFocusability.AfterDescendants;
			SetWillNotDraw (false);
			ViewConfiguration configuration = ViewConfiguration.Get (Context);
			mTouchSlop = configuration.ScaledTouchSlop;
			mMinimumVelocity = configuration.ScaledMinimumFlingVelocity;
			mMaximumVelocity = configuration.ScaledMaximumFlingVelocity;
		}


		/**
	     * You can call this function yourself to have the scroll view perform
	     * scrolling from a key event, just as if the event had been dispatched to
	     * it by the view hierarchy.
	     *
	     * @param event The key event to execute.
	     * @return Return true if the event was handled, else false.
	     */
		public bool ExecuteKeyEvent (KeyEvent e)
		{
			mTempRect.SetEmpty ();
			if (!CanScroll)
			{
				if (IsFocused)
				{
					View currentFocused = FindFocus ();
					if (currentFocused == this)
						currentFocused = null;
					View nextFocused = FocusFinder.Instance.FindNextFocus (this, currentFocused, FocusSearchDirection.Down);
					return nextFocused != null && nextFocused != this && nextFocused.RequestFocus (FocusSearchDirection.Down);
				}
				return false;
			}
			bool handled = false;
			if (e.Action == KeyEventActions.Down)
			{
				switch (e.KeyCode)
				{
					case Keycode.DpadUp:
                        //if (!e.IsAltPressed) {
                        //  handled = ArrowScroll( View.FOCUS_UP, false);
                        //} else {
                        //  handled = DullScroll(View.FOCUS_UP, false);
                        //}
						break;
					case Keycode.DpadDown:
                        //if (!e.IsAltPressed) {
                        //  handled = arrowScroll(View.FOCUS_DOWN, false);
                        //} else {
                        //  handled = fullScroll(View.FOCUS_DOWN, false);
                        //}
						break;
					case Keycode.DpadLeft:
                        //if (!e.IsAltPressed) {
                        //  handled = arrowScroll(View.FOCUS_LEFT, true);
                        //} else {
                        //  handled = fullScroll(View.FOCUS_LEFT, true);
                        //}
						break;
					case Keycode.DpadRight:
                        //if (!e.IsAltPressed) {
                        //  handled = arrowScroll(View.FOCUS_RIGHT, true);
                        //} else {
                        //  handled = fullScroll(View.FOCUS_RIGHT, true);
                        //}
						break;
				}
			}
			return handled;
		}

		/**
		  * Finds the next focusable component that fits in this View's bounds
		  * (excluding fading edges) pretending that this View's top is located at
		  * the parameter top.
		  *
		  * @param topFocus           look for a candidate is the one at the top of the bounds
		  *                           if topFocus is true, or at the bottom of the bounds if topFocus is
		  *                           false
		  * @param top                the top offset of the bounds in which a focusable must be
		  *                           found (the fading edge is assumed to start at this position)
		  * @param preferredFocusable the View that has highest priority and will be
		  *                           returned if it is within my bounds (null is valid)
		  * @return the next focusable component in the bounds or null if none can be
		  *         found
		  */
		private View findFocusableViewInMyBounds (bool topFocus, int top, bool leftFocus, int left, View preferredFocusable)
		{
			/*
            * The fading edge's transparent side should be considered for focus
            * since it's mostly visible, so we divide the actual fading edge length
            * by 2.
            */
			int verticalFadingEdgeLength = VerticalFadingEdgeLength / 2;
			int topWithoutFadingEdge = top + verticalFadingEdgeLength;
			int bottomWithoutFadingEdge = top + Height - verticalFadingEdgeLength;
			int horizontalFadingEdgeLength = HorizontalFadingEdgeLength / 2;
			int leftWithoutFadingEdge = left + horizontalFadingEdgeLength;
			int rightWithoutFadingEdge = left + Width - horizontalFadingEdgeLength;

			if ((preferredFocusable != null)
			             && (preferredFocusable.Top < bottomWithoutFadingEdge)
			             && (preferredFocusable.Bottom > topWithoutFadingEdge)
			             && (preferredFocusable.Left < rightWithoutFadingEdge)
			             && (preferredFocusable.Right > leftWithoutFadingEdge))
			{
				return preferredFocusable;
			}
			return FindFocusableViewInBounds (topFocus, topWithoutFadingEdge, bottomWithoutFadingEdge, leftFocus, leftWithoutFadingEdge, rightWithoutFadingEdge);
		}


		/// <summary>
		/// Finds the focusable view in bounds.
		/// </summary>
		/// <returns>The focusable view in bounds.</returns>
		/// <param name="topFocus">If set to <c>true</c> top focus.</param>
		/// <param name="top">Top.</param>
		/// <param name="bottom">Bottom.</param>
		/// <param name="leftFocus">If set to <c>true</c> left focus.</param>
		/// <param name="left">Left.</param>
		/// <param name="right">Right.</param>
		private View FindFocusableViewInBounds (bool topFocus, int top, int bottom, bool leftFocus, int left, int right)
		{
			IList<View> focusables = GetFocusables (FocusSearchDirection.Forward);
			View focusCandidate = null;

			/*
            * A fully contained focusable is one where its top is below the bound's
            * top, and its bottom is above the bound's bottom. A partially
            * contained focusable is one where some part of it is within the
            * bounds, but it also has some part that is not within bounds.  A fully contained
            * focusable is preferred to a partially contained focusable.
            */
			bool foundFullyContainedFocusable = false;

			int count = focusables.Count;
			for (int i = 0; i < count; i++)
			{
				View view = focusables [i];
				int viewTop = view.Top;
				int viewBottom = view.Bottom;
				int viewLeft = view.Left;
				int viewRight = view.Right;

				if (top < viewBottom && viewTop < bottom && left < viewRight && viewLeft < right)
				{
					/*
                    * the focusable is in the target area, it is a candidate for
                    * focusing
                    */
					bool viewIsFullyContained = (top < viewTop) && (viewBottom < bottom) && (left < viewLeft) && (viewRight < right);
					if (focusCandidate == null)
					{
						/* No candidate, take this one */
						focusCandidate = view;
						foundFullyContainedFocusable = viewIsFullyContained;
					}
					else
					{
						bool viewIsCloserToVerticalBoundary =
							(topFocus && viewTop < focusCandidate.Top) ||
							(!topFocus && viewBottom > focusCandidate.Bottom);
						bool viewIsCloserToHorizontalBoundary =
							(leftFocus && viewLeft < focusCandidate.Left) ||
							(!leftFocus && viewRight > focusCandidate.Right);
						if (foundFullyContainedFocusable)
						{
							if (viewIsFullyContained && viewIsCloserToVerticalBoundary && viewIsCloserToHorizontalBoundary)
							{
								/*
                                 * We're dealing with only fully contained views, so
                                 * it has to be closer to the boundary to beat our
                                 * candidate
                                 */
								focusCandidate = view;
							}
						}
						else
						{
							if (viewIsFullyContained)
							{
								/* Any fully contained view beats a partially contained view */
								focusCandidate = view;
								foundFullyContainedFocusable = true;
							}
							else if (viewIsCloserToVerticalBoundary && viewIsCloserToHorizontalBoundary)
							{
								/*
                                 * Partially contained view beats another partially
                                 * contained view if it's closer
                                 */
								focusCandidate = view;
							}
						}
					}
				}
			}
			return focusCandidate;
		}

		/**
		  * <p>Handles scrolling in response to a "home/end" shortcut press. This
		  * method will scroll the view to the top or bottom and give the focus
		  * to the topmost/bottommost component in the new visible area. If no
		  * component is a good candidate for focus, this scrollview reclaims the
		  * focus.</p>
		  *
		  * @param direction the scroll direction: {@link android.view.View#FOCUS_UP}
		  *                  to go the top of the view or
		  *                  {@link android.view.View#FOCUS_DOWN} to go the bottom
		  * @return true if the key event is consumed by this method, false otherwise
		  */
		public bool FullScroll (FocusSearchDirection direction, bool horizontal)
		{
			if (!horizontal)
			{
				bool down = direction == FocusSearchDirection.Down;
				int height = Height;
				mTempRect.Top = 0;
				mTempRect.Bottom = height;
				if (down)
				{
					int count = ChildCount;
					if (count > 0)
					{
						View view = GetChildAt (count - 1);
						mTempRect.Bottom = view.Bottom;
						mTempRect.Top = mTempRect.Bottom - height;
					}
				}
				return ScrollAndFocus (direction, mTempRect.Top, mTempRect.Bottom, 0, 0, 0);
			}
			else
			{
				bool right = direction == FocusSearchDirection.Down;
				int width = Width;
				mTempRect.Left = 0;
				mTempRect.Right = width;
				if (right)
				{
					int count = ChildCount;
					if (count > 0)
					{
						View view = GetChildAt (count - 1);
						mTempRect.Right = view.Bottom;
						mTempRect.Left = mTempRect.Right - width;
					}
				}
				return ScrollAndFocus (0, 0, 0, direction, mTempRect.Top, mTempRect.Bottom);
			}
		}

		/**
		  * <p>Scrolls the view to make the area defined by <code>top</code> and
		  * <code>bottom</code> visible. This method attempts to give the focus
		  * to a component visible in this area. If no component can be focused in
		  * the new visible area, the focus is reclaimed by this scrollview.</p>
		  *
		  * @param direction the scroll direction: {@link android.view.View#FOCUS_UP}
		  *                  to go upward
		  *                  {@link android.view.View#FOCUS_DOWN} to downward
		  * @param top       the top offset of the new area to be made visible
		  * @param bottom    the bottom offset of the new area to be made visible
		  * @return true if the key event is consumed by this method, false otherwise
		  */
		private bool ScrollAndFocus (FocusSearchDirection directionY, int top, int bottom, FocusSearchDirection directionX, int left, int right)
		{
			bool handled = true;
			int height = Height;
			int containerTop = ScrollY;
			int containerBottom = containerTop + height;
			bool up = directionY == FocusSearchDirection.Up;
			int width = Width;
			int containerLeft = ScrollX;
			int containerRight = containerLeft + width;
			bool leftwards = directionX == FocusSearchDirection.Up;
			View newFocused = FindFocusableViewInBounds (up, top, bottom, leftwards, left, right);
			if (newFocused == null)
			{
				newFocused = this;
			}
			if ((top >= containerTop && bottom <= containerBottom) || (left >= containerLeft && right <= containerRight))
			{
				handled = false;
			}
			else
			{
				int deltaY = up ? (top - containerTop) : (bottom - containerBottom);
				int deltaX = leftwards ? (left - containerLeft) : (right - containerRight);
				DoScroll (deltaX, deltaY);
			}
			if (newFocused != FindFocus () && newFocused.RequestFocus (directionY))
			{
				mTwoDScrollViewMovedFocus = true;
				mTwoDScrollViewMovedFocus = false;
			}
			return handled;
		}

		/**
		  * Handle scrolling in response to an up or down arrow click.
		  *
		  * @param direction The direction corresponding to the arrow key that was
		  *                  pressed
		  * @return True if we consumed the event, false otherwise
		  */
		public bool arrowScroll (FocusSearchDirection direction, bool horizontal)
		{
			View currentFocused = FindFocus ();
			if (currentFocused == this)
				currentFocused = null;
			View nextFocused = FocusFinder.Instance.FindNextFocus (this, currentFocused, direction);
			int maxJump = horizontal ? MaxScrollAmountHorizontal : MaxScrollAmountVertical;

			if (!horizontal)
			{
				if (nextFocused != null)
				{
					nextFocused.GetDrawingRect (mTempRect);
					OffsetDescendantRectToMyCoords (nextFocused, mTempRect);
					int scrollDelta = ComputeScrollDeltaToGetChildRectOnScreen (mTempRect);
					DoScroll (0, scrollDelta);
					nextFocused.RequestFocus (direction);
				}
				else
				{
					// no new focus
					int scrollDelta = maxJump;
					if (direction == FocusSearchDirection.Up && ScrollY < scrollDelta)
					{
						scrollDelta = ScrollY;
					}
					else if (direction == FocusSearchDirection.Down)
					{
						if (ChildCount > 0)
						{
							int daBottom = GetChildAt (0).Bottom;
							int screenBottom = ScrollY + Height;
							if (daBottom - screenBottom < maxJump)
							{
								scrollDelta = daBottom - screenBottom;
							}
						}
					}
					if (scrollDelta == 0)
					{
						return false;
					}
					DoScroll (0, direction == FocusSearchDirection.Down ? scrollDelta : -scrollDelta);
				}
			}
			else
			{
				if (nextFocused != null)
				{
					nextFocused.GetDrawingRect (mTempRect);
					OffsetDescendantRectToMyCoords (nextFocused, mTempRect);
					int scrollDelta = ComputeScrollDeltaToGetChildRectOnScreen (mTempRect);
					DoScroll (scrollDelta, 0);
					nextFocused.RequestFocus (direction);
				}
				else
				{
					// no new focus
					int scrollDelta = maxJump;
					if (direction == FocusSearchDirection.Up && ScrollY < scrollDelta)
					{
						scrollDelta = ScrollY;
					}
					else if (direction == FocusSearchDirection.Down)
					{
						if (ChildCount > 0)
						{
							int daBottom = GetChildAt (0).Bottom;
							int screenBottom = ScrollY + Height;
							if (daBottom - screenBottom < maxJump)
							{
								scrollDelta = daBottom - screenBottom;
							}
						}
					}
					if (scrollDelta == 0)
					{
						return false;
					}
					DoScroll (direction == FocusSearchDirection.Down ? scrollDelta : -scrollDelta, 0);
				}
			}
			return true;
		}

		/**
		  * Smooth scroll by a Y delta
		  *
		  * @param delta the number of pixels to scroll by on the Y axis
		  */
		private void DoScroll (int deltaX, int deltaY)
		{
			if (deltaX != 0 || deltaY != 0)
			{
				SmoothScrollBy (deltaX, deltaY);
			}
		}

		/**
		  * Like {@link View#scrollBy}, but scroll smoothly instead of immediately.
		  *
		  * @param dx the number of pixels to scroll by on the X axis
		  * @param dy the number of pixels to scroll by on the Y axis
		  */
		public void SmoothScrollBy (int dx, int dy)
		{
			long duration = AnimationUtils.CurrentAnimationTimeMillis () - mLastScroll;
			if (duration > ANIMATED_SCROLL_GAP)
			{
				mScroller.StartScroll (ScrollX, ScrollY, dx, dy);
				AwakenScrollBars (mScroller.Duration);
				Invalidate ();
			}
			else
			{
				if (!mScroller.IsFinished)
				{
					mScroller.AbortAnimation ();
				}
				ScrollBy (dx, dy);
			}
			mLastScroll = AnimationUtils.CurrentAnimationTimeMillis ();
		}

		/**
		  * Like {@link #scrollTo}, but scroll smoothly instead of immediately.
		  *
		  * @param x the position where to scroll on the X axis
		  * @param y the position where to scroll on the Y axis
		  */
		public void SmoothScrollTo (int x, int y)
		{
			SmoothScrollBy (x - ScrollX, y - ScrollY);
		}

		/**
		* Scrolls the view to the given child.
		*
		* @param child the View to scroll to
		*/
		private void ScrollToChild (View child)
		{
			child.GetDrawingRect (mTempRect);
			/* Offset from child's local coordinates to TwoDScrollView coordinates */
			OffsetDescendantRectToMyCoords (child, mTempRect);
			int scrollDelta = ComputeScrollDeltaToGetChildRectOnScreen (mTempRect);
			if (scrollDelta != 0)
			{
				ScrollBy (0, scrollDelta);
			}
		}

		/**
         * If rect is off screen, scroll just enough to get it (or at least the
         * first screen size chunk of it) on screen.
         *
         * @param rect      The rectangle.
         * @param immediate True to scroll immediately without animation
         * @return true if scrolling was performed
         */
		private bool ScrollToChildRect (Rect rect, bool immediate)
		{
			int delta = ComputeScrollDeltaToGetChildRectOnScreen (rect);
			bool scroll = delta != 0;
			if (scroll)
			{
				if (immediate)
				{
					ScrollBy (0, delta);
				}
				else
				{
					SmoothScrollBy (0, delta);
				}
			}
			return scroll;
		}

		/**
         * Compute the amount to scroll in the Y direction in order to get
         * a rectangle completely on the screen (or, if taller than the screen,
         * at least the first screen size chunk of it).
         *
         * @param rect The rect.
         * @return The scroll delta.
         */
		protected int ComputeScrollDeltaToGetChildRectOnScreen (Rect rect)
		{
			if (ChildCount == 0)
				return 0;
			int height = Height;
			int screenTop = ScrollY;
			int screenBottom = screenTop + height;
			int fadingEdge = VerticalFadingEdgeLength;
			// leave room for top fading edge as long as rect isn't at very top
			if (rect.Top > 0)
			{
				screenTop += fadingEdge;
			}

			// leave room for bottom fading edge as long as rect isn't at very bottom
			if (rect.Bottom < GetChildAt (0).Height)
			{
				screenBottom -= fadingEdge;
			}
			int scrollYDelta = 0;
			if (rect.Bottom > screenBottom && rect.Top > screenTop)
			{
				// need to move down to get it in view: move down just enough so
				// that the entire rectangle is in view (or at least the first
				// screen size chunk).
				if (rect.Height () > height)
				{
					// just enough to get screen size chunk on
					scrollYDelta += (rect.Top - screenTop);
				}
				else
				{
					// get entire rect at bottom of screen
					scrollYDelta += (rect.Bottom - screenBottom);
				}

				// make sure we aren't scrolling beyond the end of our content
				int bottom = GetChildAt (0).Bottom;
				int distanceToBottom = bottom - screenBottom;
				scrollYDelta = System.Math.Min (scrollYDelta, distanceToBottom);

			}
			else if (rect.Top < screenTop && rect.Bottom < screenBottom)
			{
				// need to move up to get it in view: move up just enough so that
				// entire rectangle is in view (or at least the first screen
				// size chunk of it).

				if (rect.Height () > height)
				{
					// screen size chunk
					scrollYDelta -= (screenBottom - rect.Bottom);
				}
				else
				{
					// entire rect at top
					scrollYDelta -= (screenTop - rect.Top);
				}

				// make sure we aren't scrolling any further than the top our content
				scrollYDelta = System.Math.Max (scrollYDelta, -ScrollY);
			}
			return scrollYDelta;
		}

		/**
         * When looking for focus in children of a scroll view, need to be a little
         * more careful not to give focus to something that is scrolled off screen.
         *
         * This is more expensive than the default {@link android.view.ViewGroup}
         * implementation, otherwise this behavior might have been made the default.
         */
		protected bool OnRequestFocusInDescendants (FocusSearchDirection direction, Rect previouslyFocusedRect)
		{
			// convert from forward / backward notation to up / down / left / right
			// (ugh).
			if (direction == FocusSearchDirection.Forward)
			{
				direction = FocusSearchDirection.Down;
			}
			else if (direction == FocusSearchDirection.Backward)
			{
				direction = FocusSearchDirection.Up;
			}

			View nextFocus = previouslyFocusedRect == null ?
            FocusFinder.Instance.FindNextFocus (this, null, direction) :
            FocusFinder.Instance.FindNextFocusFromRect (this,
			                                                    previouslyFocusedRect, direction);

			if (nextFocus == null)
			{
				return false;
			}

			return nextFocus.RequestFocus (direction, previouslyFocusedRect);
		}

		/**
         * Return true if child is an descendant of parent, (or equal to the parent).
         */
		private bool IsViewDescendantOf (View child, View parent)
		{
			if (child == parent)
			{
				return true;
			}

			IViewParent theParent = child.Parent;
			return (theParent is ViewGroup) && IsViewDescendantOf ((View)theParent, parent);
		}

		/**
         * Fling the scroll view
         *
         * @param velocityY The initial velocity in the Y direction. Positive
         *                  numbers mean that the finger/curor is moving down the screen,
         *                  which means we want to scroll towards the top.
         */
		public void Fling (int velocityX, int velocityY)
		{
			if (ChildCount > 0)
			{
				int height = Height - PaddingBottom - PaddingTop;
				int bottom = GetChildAt (0).Height;
				int width = Width - PaddingRight - PaddingLeft;
				int right = GetChildAt (0).Width;

				mScroller.Fling (ScrollX, ScrollY, velocityX, velocityY, 0, right - width, 0, bottom - height);

				bool movingDown = velocityY > 0;
				bool movingRight = velocityX > 0;

				View newFocused = findFocusableViewInMyBounds (movingRight, mScroller.FinalX, movingDown, mScroller.FinalY, FindFocus ());
				if (newFocused == null)
				{
					newFocused = this;
				}

				if (newFocused != FindFocus () && newFocused.RequestFocus (movingDown ? FocusSearchDirection.Down : FocusSearchDirection.Up))
				{
					mTwoDScrollViewMovedFocus = true;
					mTwoDScrollViewMovedFocus = false;
				}

				AwakenScrollBars (mScroller.Duration);
				Invalidate ();
			}
		}

		private int Clamp (int n, int my, int child)
		{
			if (my >= child || n < 0)
			{
				/* my >= child is this case:
                 *                    |--------------- me ---------------|
                 *     |------ child ------|
                 * or
                 *     |--------------- me ---------------|
                 *            |------ child ------|
                 * or
                 *     |--------------- me ---------------|
                 *                                  |------ child ------|
                 *
                 * n < 0 is this case:
                 *     |------ me ------|
                 *                    |-------- child --------|
                 *     |-- mScrollX --|
                 */
				return 0;
			}
			if ((my + n) > child)
			{
				/* this case:
                 *                    |------ me ------|
                 *     |------ child ------|
                 *     |-- mScrollX --|
                 */
				return child - my;
			}
			return n;
		}
		
		#endregion
	}
}

