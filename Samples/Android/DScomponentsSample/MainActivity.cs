using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DSoft.UI.Grid;
using DSComponentsSample.Data.Grid;

namespace DScomponentsSample
{
	[Activity (Label = "DSComponentsSample", MainLauncher = false)]
	public class MainActivity : DSGridViewActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//set that data source and the table name
			DataSource = new ExampleDataSet (this);
			TableName = "DT1";
		
		}
	}
}


