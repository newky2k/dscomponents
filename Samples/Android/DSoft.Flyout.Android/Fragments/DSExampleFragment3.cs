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
using DSoft.Datatypes.UI.Interfaces;

namespace DSoft.Flyout.Droid.Fragments
{
	public class DSExampleFragment3 : Fragment,IDSFlyoutContent
	{
		#region Fields 
		private String mTitle = "Example 3";
		#endregion
		
		#region IDSFlyoutContent implementation

		public string Title
		{
			get
			{
				return mTitle;
			}
			set
			{
				mTitle = value;
			}
		}

		#endregion
		
		#region Consructor
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Flyout.Android.Fragments.DSExampleFragment3"/> class.
		/// </summary>
		public DSExampleFragment3 ()
		{
			mTitle = "Example 3";
		}
		
		#endregion
		
		public override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Create your fragment here
		}
		
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);
			
			var aView = inflater.Inflate (Resource.Layout.example_layout, container,false);
			
			var aTestView = aView.FindViewById<TextView> (Resource.Id.txtExample);
			
			if (aTestView != null)
			{
				aTestView.Text = this.Title;
			}
			
			return aView;
		}
		
		public override void OnAttach (Activity activity)
		{
			base.OnAttach (activity);
			
		}
	}
}

