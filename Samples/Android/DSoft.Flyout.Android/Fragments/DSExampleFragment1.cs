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
	public class DSExampleFragment1 : Fragment, IDSFlyoutContent
	{
		#region Fields 
		private String mTitle;
		
		#endregion
		
		#region Constructor
		
		public DSExampleFragment1 ()
		{
			mTitle = "Example 1";
		}
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

