// ****************************************************************************
// <copyright file="DSToolbarView.cs" company="DSoft Developments">
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
using Android.Graphics.Drawables;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.UI.Collections;
using DSoft.Datatypes.UI;
using DSoft.Themes.Toolbar;

namespace DSoft.UI.Views
{
	/// <summary>
	/// Toolbar View
	/// </summary>
	public class DSToolbarView : FrameLayout
	{		
		#region Fields
	
		private ToolbarMode mMode = ToolbarMode.Toolbar;
		private LinearLayout mContainer;
		private LinearLayout mContentView;		
		#endregion
		#region Properties
		
		/// <summary>
		/// Gets the content view.
		/// </summary>
		/// <value>The content view.</value>
		protected LinearLayout ContentView
		{
			get
			{
				return mContentView;
			}
		}
		
		/// <summary>
		/// Gets or sets the mode.
		/// </summary>
		/// <value>The mode.</value>
		public ToolbarMode Mode
		{
			get
			{
				return mMode;
			}
			set
			{
				mMode = value;
				
				//rebuild containerview
				if (mContainer != null)
				{
					if (mContentView != null)
					{
						mContainer.RemoveView (mContentView);
						mContentView = null;
					}
					
					RemoveView (mContainer);
					mContainer = null;
				}
				
				Initialize ();
			}
		}
		
		#endregion
		#region Constructors
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Views.DSToolbarView"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public DSToolbarView (IntPtr javaReference, JniHandleOwnership transfer) 
			: base(javaReference, transfer)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Views.DSToolbarView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public DSToolbarView (Context context) :
			base (context)
		{
			Initialize ();
		}
			
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Views.DSToolbarView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public DSToolbarView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Views.DSToolbarView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		/// <param name="defStyle">Def style.</param>
		public DSToolbarView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize ();
		}
		#endregion
		#region Methods
		private void Initialize ()
		{			
			LinearLayout container = new LinearLayout (Context);
			container.LayoutParameters = new ViewGroup.LayoutParams (LayoutParams.FillParent, LayoutParams.FillParent);
			container.Orientation = Orientation.Horizontal;
			
			mContainer = container;
			
			var lLayout = new LinearLayout (Context);
			var lytParams = new LinearLayout.LayoutParams (LayoutParams.FillParent, LayoutParams.FillParent);
			lytParams.SetMargins (0, 0, 0, 0);
			lLayout.LayoutParameters = lytParams;
			lLayout.Orientation = Orientation.Horizontal;
			//lLayout.SetBackgroundColor (Color.Red);
			
			if (DSToolbarTheme.CurrentTheme.Color != null)
			{
				var ptn = DSToolbarTheme.CurrentTheme.Color.ToAndroidColorDrawable ();
				lLayout.SetBackgroundDrawable(ptn);
			}
			
			
			mContentView = lLayout;
			mContainer.AddView (lLayout);
			
			this.AddView (mContainer);
		}
		#endregion
		#region Overrides
		/// <summary>
		/// Raises the measure event.
		/// </summary>
		/// <param name="widthMeasureSpec">Width measure spec.</param>
		/// <param name="heightMeasureSpec">Height measure spec.</param>
		protected override void OnMeasure (int widthMeasureSpec, int heightMeasureSpec)
		{
			base.OnMeasure (widthMeasureSpec, heightMeasureSpec);
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
			if (DSToolbarTheme.CurrentTheme.Color != null)
			{
				mContentView.SetBackgroundColor (DSToolbarTheme.CurrentTheme.Color.ToAndroidColor());
			}
			
			
			for(int i = 0 ; i < ChildCount ; i++)
			{
				GetChildAt (i).Layout (l, t, r, b);
            }
		}
		
		#endregion
	}
}

