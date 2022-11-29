// ****************************************************************************
// <copyright file="DSAbsoluteLayout.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using Android.Views;
using Android.Content;
using Android.Util;
using Android.Runtime;

namespace DSoft.UI.Views
{
	/// <summary>
	/// DS absolute layout.
	/// </summary>
	public class DSAbsoluteLayout : ViewGroup
	{
		#region Fields
		private int mPaddingLeft = 0;
		private int mPaddingRight = 0;
		private int mPaddingTop = 0;
		private int mPaddingBottom = 0;
		#endregion
		#region Constuctors

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Views.DSAbsoluteLayout"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public DSAbsoluteLayout (IntPtr javaReference, JniHandleOwnership transfer) 
			: base(javaReference, transfer)
		{

		}
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Views.DSAbsoluteLayout"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public DSAbsoluteLayout (Context context) : base (context)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Views.DSAbsoluteLayout"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public DSAbsoluteLayout (Context context, IAttributeSet attrs) 
			: base (context, attrs)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Views.DSAbsoluteLayout"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		/// <param name="defStyle">Def style.</param>
		public DSAbsoluteLayout (Context context, IAttributeSet attrs, int defStyle) 
			: base (context, attrs, defStyle)
		{
		}

		#endregion

		#region implemented abstract members of ViewGroup
		/// <summary>
		/// Raises the measure event.
		/// </summary>
		/// <param name="widthMeasureSpec">Width measure spec.</param>
		/// <param name="heightMeasureSpec">Height measure spec.</param>
		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			int count = ChildCount;
			int maxHeight = 0;
			int maxWidth = 0;
			// Find out how big everyone wants to be
			MeasureChildren (widthMeasureSpec, heightMeasureSpec);
			{
				// Find rightmost and bottom-most child
				for (int i = 0; i < count; i++)
				{
					View child = GetChildAt (i);
					if (child.Visibility != ViewStates.Gone)
					{
						int childRight;
						int childBottom;
						var lp = (DSAbsoluteLayout.DSAbsoluteLayoutParams)child.LayoutParameters;

						childRight = lp.x + child.MeasuredWidth;
						childBottom = lp.y + child.MeasuredHeight;
						maxWidth = System.Math.Max (maxWidth, childRight);
						maxHeight = System.Math.Max (maxHeight, childBottom);
					}
				}
			}
			// Account for padding too
			maxWidth += mPaddingLeft + mPaddingRight;
			maxHeight += mPaddingTop + mPaddingBottom;
			// Check against minimum height and width
			maxHeight = System.Math.Max (maxHeight, SuggestedMinimumHeight);
			maxWidth = System.Math.Max (maxWidth, SuggestedMinimumWidth);
			SetMeasuredDimension (ResolveSizeAndState (maxWidth, widthMeasureSpec, 0), ResolveSizeAndState
				(maxHeight, heightMeasureSpec, 0));
		}

		/// <Docs>Returns a set of default layout parameters.</Docs>
		/// <summary>
		/// Generates the default layout parameters.
		/// </summary>
		/// <returns>The default layout parameters.</returns>
		protected override ViewGroup.LayoutParams GenerateDefaultLayoutParams ()
		{
			return new DSAbsoluteLayout.DSAbsoluteLayoutParams (ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent, 0, 0);
		}

		/// <Docs>This is a new size or position for this view</Docs>
		/// <remarks>Called from layout when this view should
		///  assign a size and position to each of its children.
		/// 
		///  Derived classes with children should override
		///  this method and call layout on each of
		///  their children.</remarks>
		/// <format type="text/html">[Android Documentation]</format>
		/// <since version="Added in API level 1"></since>
		/// <summary>
		/// Raises the layout event.
		/// </summary>
		/// <param name="changed">If set to <c>true</c> changed.</param>
		/// <param name="l">L.</param>
		/// <param name="t">T.</param>
		/// <param name="r">The red component.</param>
		/// <param name="b">The blue component.</param>
		protected override void OnLayout (bool changed, int l, int t, int r, int b)
		{
			int count = ChildCount;
			{
				for (int i = 0; i < count; i++)
				{
					View child = GetChildAt (i);
					if (child.Visibility != ViewStates.Gone)
					{
						var lp = (DSAbsoluteLayout.DSAbsoluteLayoutParams)child.LayoutParameters;
						int childLeft = mPaddingLeft + lp.x;
						int childTop = mPaddingTop + lp.y;
						child.Layout (childLeft, childTop, childLeft + child.MeasuredWidth, childTop + child.MeasuredHeight);
					}
				}
			}
		}

		/// <summary>
		/// Generates the layout parameters.
		/// </summary>
		/// <returns>The layout parameters.</returns>
		/// <param name="attrs">Attrs.</param>
		public override LayoutParams GenerateLayoutParams (IAttributeSet attrs)
		{
			return new DSAbsoluteLayout.DSAbsoluteLayoutParams (Context, attrs);
		}

		/// <Docs>To be added.</Docs>
		/// <returns>To be added.</returns>
		/// <para tool="javadoc-to-mdoc"></para>
		/// <format type="text/html">[Android Documentation]</format>
		/// <since version="Added in API level 1"></since>
		/// <summary>
		/// Checks the layout parameters.
		/// </summary>
		/// <param name="p">P.</param>
		protected override bool CheckLayoutParams (LayoutParams p)
		{
			return p is DSAbsoluteLayout.DSAbsoluteLayoutParams;
		}

		/// <summary>
		/// Generates the layout parameters.
		/// </summary>
		/// <returns>The layout parameters.</returns>
		/// <param name="p">P.</param>
		protected override LayoutParams GenerateLayoutParams (LayoutParams p)
		{
			return new DSAbsoluteLayout.DSAbsoluteLayoutParams (p);
		}

		/// <Docs>Return true if the pressed state should be delayed for children or descendants of this
		///  ViewGroup.</Docs>
		/// <remarks>Return true if the pressed state should be delayed for children or descendants of this
		///  ViewGroup. Generally, this should be done for containers that can scroll, such as a List.
		///  This prevents the pressed state from appearing when the user is actually trying to scroll
		///  the content.
		/// 
		///  The default implementation returns true for compatibility reasons. Subclasses that do
		///  not scroll should generally override this method and return false.</remarks>
		/// <format type="text/html">[Android Documentation]</format>
		/// <since version="Added in API level 14"></since>
		/// <summary>
		/// Shoulds the state of the delay child pressed.
		/// </summary>
		/// <returns><c>true</c>, if delay child pressed state was shoulded, <c>false</c> otherwise.</returns>
		public override bool ShouldDelayChildPressedState ()
		{
			return false;
		}

		#endregion

		/// <summary>
		/// DSABsolute Layout parameters.
		/// </summary>
		internal class DSAbsoluteLayoutParams : ViewGroup.LayoutParams
		{
			/// <summary>The horizontal, or X, location of the child within the view group.</summary>
			/// <remarks>The horizontal, or X, location of the child within the view group.</remarks>
			public int x;
			/// <summary>The vertical, or Y, location of the child within the view group.</summary>
			/// <remarks>The vertical, or Y, location of the child within the view group.</remarks>
			public int y;

			public DSAbsoluteLayoutParams (int width, int height, int x, int y) : base (width, height)
			{
				this.x = x;
				this.y = y;
			}

			public DSAbsoluteLayoutParams (Context c, IAttributeSet attrs) : 
				base (c, attrs)
			{

				Android.Content.Res.TypedArray a = c.ObtainStyledAttributes (attrs, AbsoluteLayout_Layout);

				x = a.GetDimensionPixelOffset (AbsoluteLayout_Layout_layout_x, 0);
				y = a.GetDimensionPixelOffset (AbsoluteLayout_Layout_layout_y, 0);
				a.Recycle ();
			}

			/// <summary><inheritDoc></inheritDoc></summary>
			public DSAbsoluteLayoutParams (ViewGroup.LayoutParams source) : base (source)
			{
			}

			public static readonly int[] AbsoluteLayout_Layout = new int[] { unchecked((int)(
				    0x0101017f)), unchecked((int)(0x01010180))
			};
			public const int AbsoluteLayout_Layout_layout_x = 0;
			public const int AbsoluteLayout_Layout_layout_y = 1;
		}
	}
}

