// ****************************************************************************
// <copyright file="LayoutActivity.cs" company="DSoft Developments">
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
using DSoft.UI.Grid;
using SampleAndroid.Data.Grid;
using Android;

namespace SampleAndroid
{
	[Activity (Label = "SampleAndroid", MainLauncher = true)]			
	public class LayoutActivity : Activity
	{
		DSGridView mDataGrid;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.activity_main);

			mDataGrid = this.FindViewById<DSGridView>(Resource.Id.myDataGrid);

			if (mDataGrid != null)
			{
				mDataGrid.DataSource = new ExampleDataSet (this);
				mDataGrid.TableName = "DT1";
			}

		}

    }
}

