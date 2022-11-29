// ****************************************************************************
// <copyright file="DSNavigationBarView.cs" company="DSoft Developments">
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
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Graphics;
using DSoft.Datatypes.UI.Collections;
using DSoft.Datatypes.UI;
using Android.Graphics.Drawables;
using DSoft.Themes.Toolbar;

namespace DSoft.UI.Views
{	
	/// <summary>
	/// Navigation bar view
	/// </summary>
	public class DSNavigationBarView : DSToolbarView
	{
		#region Fields

		private TextView mTitleLabel;
		
		private LinearLayout mLeftArea;
		private LinearLayout mRightArea;
				
		private DSToolbarItemCollection mLeftButtons = new DSToolbarItemCollection();
		private DSToolbarItemCollection mRightButtons = new DSToolbarItemCollection();
		
		#endregion
		#region Properties
		
		/// <summary>
		/// Gets or sets the left button
		/// </summary>
		/// <value>The right button command.</value>
		public DSToolbarItem LeftButton
		{
			get
			{
				if (mLeftButtons == null || mLeftButtons.Count == 0)
					return null;
					
				return mLeftButtons [0];
			}
			set
			{
//				//Do
//				if (mLeftButtons.Contains (value))
//					return;
					
				if (mLeftButtons == null)
				{
					mLeftButtons = new DSToolbarItemCollection ();
				}
				
				if (mLeftButtons.Count != 0)
				{
					mLeftButtons.Clear ();
				}
				
				mLeftButtons.Add (value);
				
				BuildLeftArea ();
			}
		}
		
		/// <summary>
		/// Gets or sets the right button.
		/// </summary>
		/// <value>The right button.</value>
		public DSToolbarItem RightButton
		{
			get
			{
				if (mRightButtons == null || mRightButtons.Count == 0)
					return null;
					
				return mRightButtons [0];
			}
			set
			{
//				//Do
//				if (mLeftButtons.Contains (value))
//					return;
					
				if (mRightButtons == null)
				{
					mRightButtons = new DSToolbarItemCollection ();
				}
				
				if (mRightButtons.Count != 0)
				{
					mRightButtons.Clear ();
				}
				
				mRightButtons.Add (value);
				
				BuildRightArea ();
			}
		}
		
		/// <summary>
		/// Gets or sets the left buttons collection
		/// </summary>
		/// <value>The right buttons.</value>
		public DSToolbarItemCollection LeftButtons
		{
			get
			{
				return mLeftButtons;
			}
			set
			{
				mLeftButtons = value;
				BuildLeftArea ();
			}
		}
		
		/// <summary>
		/// Gets or sets the right buttons collection
		/// </summary>
		/// <value>The right buttons.</value>
		public DSToolbarItemCollection RightButtons
		{
			get
			{
				return mRightButtons;
			}
			set
			{
				mRightButtons = value;
				BuildRightArea ();
			}
		}
		
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public String Title
		{
			get
			{
				return mTitleLabel.Text;
			}
			set
			{
				mTitleLabel.Text = value;
			}
		}
		
		#endregion
		#region Constructors
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Views.DSNavigationBarView"/> class.
		/// </summary>
		/// <param name="javaReference">Java reference.</param>
		/// <param name="transfer">Transfer.</param>
		public DSNavigationBarView (IntPtr javaReference, JniHandleOwnership transfer) 
			: base(javaReference, transfer)
		{
			Setup ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Views.DSNavigationBarView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public DSNavigationBarView (Context context) :
			base (context)
		{
			Setup ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Views.DSNavigationBarView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public DSNavigationBarView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Setup ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Views.DSNavigationBarView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		/// <param name="defStyle">Def style.</param>
		public DSNavigationBarView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Setup ();
		}
		#endregion
		#region Methods
		private void Setup ()
		{			
			this.Mode = DSoft.Datatypes.Enums.ToolbarMode.Navigation;
			
			//set container for the left buttons
			var leftContainer = new LinearLayout (Context);
			leftContainer.LayoutParameters = new ViewGroup.LayoutParams (LayoutParams.WrapContent, LayoutParams.FillParent);
			leftContainer.Orientation = Orientation.Horizontal;
			mLeftArea = leftContainer;
			mLeftArea.SetBackgroundColor (Color.Transparent);
			ContentView.AddView (mLeftArea);
																	
			mTitleLabel = new TextView (Context, null, Android.Resource.Attribute.TextAppearanceMedium);
			mTitleLabel.Text = "Title";
			mTitleLabel.Gravity = GravityFlags.Center;

		
			mTitleLabel.SetBackgroundColor (Color.Transparent);

			var tvParams = new LinearLayout.LayoutParams(LayoutParams.FillParent, LayoutParams.FillParent,1.0f);
			
			var inset = Context.ToDevicePixels (4);
			
			tvParams.SetMargins (0, inset, 0, inset);
			mTitleLabel.LayoutParameters = tvParams;
			
			ContentView.AddView (mTitleLabel);
			
			var rightContainer = new LinearLayout (Context);
			rightContainer.LayoutParameters = new ViewGroup.LayoutParams (LayoutParams.WrapContent, LayoutParams.FillParent);
			rightContainer.Orientation = Orientation.Horizontal;
			mRightArea = rightContainer;
			mRightArea.SetBackgroundColor (Color.Transparent);
			ContentView.AddView (mRightArea);
			
		}
		
		/// <summary>
		/// Builds the left button area
		/// </summary>
		private void BuildLeftArea()
		{
			mLeftArea.RemoveAllViews ();
			
			var buttonInset = Context.ToDevicePixels (30);
					
			foreach (var item in mLeftButtons)
			{
				View aButton = null;
				if (item.Content is String) 
				{
					//add a standard text button
					//var aButton = new Button (Context);	
					Button button = new Button (Context);
					button.Text = item.Content as String;
					aButton = button;
					//
				} else if (item.Content is Bitmap) 
				{
					var imgButton = new ImageButton (Context);

					var draw = new BitmapDrawable (item.Content as Bitmap);
					imgButton.SetImageDrawable (draw);

					aButton = imgButton;
				}

				//aButton.SetBackgroundDrawable (null);	
				aButton.SetPadding (-buttonInset, 0, -buttonInset, 0);
				aButton.LayoutParameters = new ViewGroup.LayoutParams ( Context.ToDevicePixels (50), LayoutParams.FillParent);

				if (item.ClickCommand != null)
				{
					aButton.Click += (object sender, EventArgs e) => 
					{
						item.ClickCommand();
					};
				}

				mLeftArea.AddView (aButton);
			}
			
		}
		
		/// <summary>
		/// Builds the right button area
		/// </summary>
		private void BuildRightArea()
		{
			mRightArea.RemoveAllViews ();
			
			foreach (var item in mRightButtons)
			{
				if (item.Content is String)
				{
					//add a standard text button
					var aButton = new ImageButton (Context);
					//aButton.SetImageResource (Resource.Drawable.menu);
					//aButton.Text = item.Content as String;	
					aButton.SetBackgroundDrawable (null);	
					aButton.LayoutParameters = new ViewGroup.LayoutParams (LayoutParams.WrapContent, LayoutParams.FillParent);
					
					if (item.ClickCommand != null)
					{
						aButton.Click += (object sender, EventArgs e) => 
						{
							item.ClickCommand();
						};
					}
					
					mRightArea.AddView (aButton);
				}
			}
			
		}
		#endregion
	}
}

