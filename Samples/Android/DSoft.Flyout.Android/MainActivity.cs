using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.Graphics;
using DSoft.UI.Flyout;
using DSoft.Datatypes.UI.Collections;
using DSoft.Datatypes.UI;
using DSoft.Flyout.Droid.Fragments;
using DSoft.Themes.Flyout;
using DSoft.Datatypes.Types;
using DSoft.Themes.Toolbar;


namespace DSoft.Flyout.Droid
{
	[Activity (Label = "DSoft.Flyout.Droid", MainLauncher = true,Theme = "@android:style/Theme.Holo.NoActionBar")]
	public class MainActivity : DSFlyoutActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Register a toolbar theme
			DSToolbarTheme.Register<DSToolbarDefaultTheme> ();

			//Set the current flyout theme
			DSFlyoutTheme.Register<DSFlyoutDefaultTheme> ();

			//Build the menu
			var mItems = new DSMoreMenuItemCollection ("Menu");

			//add a section
			mItems.Add (new DSMoreSegmentMenuItem ("Features"));

			//add some fully typed content pages - Type must inherit from IDSFlyoutContent and Fragment
			mItems.Add (new DSMoreFragmentMenuItem<DSExampleFragment1> ());

			//add a Android typed generic fragment item
			// note that you have to specify the title manually
			mItems.Add (new DSMoreContentMenuItem<Fragment> (new DSExampleFragment2 ())
				{
					Title = "Example Two",
				});

			// Add standar
			mItems.Add (new DSMoreContentMenuItem ("Example III",new DSExampleFragment3 ()));

			//add another section
			mItems.Add (new DSMoreSegmentMenuItem ("Tasks"));

			//add a button the executes code rather than show content
			mItems.Add (new DSMoreButtonMenuItem ("Log out") 
			{
				HideMenuOnTap = false,
				Command = () => 
				{
					Console.WriteLine("Logged out!");
				},	
			});
			
			this.MenuItems = mItems;

			//disable autoselect of the first tab
			this.AutoSelectInitialContent = false;
		}

		public override void OnAttachedToWindow ()
		{
			base.OnAttachedToWindow ();

			//select the middle item
			this.CurrentItem = this.MenuItems [2] as DSMoreContentMenuItem<Fragment>;
		}
	}
}


