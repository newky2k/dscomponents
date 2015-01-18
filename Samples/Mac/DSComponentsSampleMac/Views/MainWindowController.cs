// ****************************************************************************
// <copyright file="MainWindowController.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

using Foundation;
using AppKit;
using DSComponentsSampleMac.Data.Grid;
using DSoft.Datatypes.Grid.Data;
using DSComponentsSampleMac.Themes;

namespace DSComponentsSampleMac
{
	public partial class MainWindowController : NSWindowController
	{
		public MainWindowController(IntPtr handle) : base(handle)
		{
		}

		[Export("initWithCoder:")]
		public MainWindowController(NSCoder coder) : base(coder)
		{
		}

		public MainWindowController() : base("MainWindow")
		{
		}

		public override void AwakeFromNib()
		{
			base.AwakeFromNib();


			//turn on showing of the selection
			grdView.ShowSelection = true;

			//allow the scrolling to bounce
			//grdView.Bounces = false;

			//set the data source to be a DataSet with multiple datatables
			grdView.DataSource = new ExampleDataSet ();

			//set the first database as the initial grid source
			grdView.TableName = ((DSDataSet)grdView.DataSource).Tables [1].Name;

			//grdView.Layer.CornerRadius = 5.0f;

			//set a theme on the control itself so that it doesn't use the global theme
			grdView.Theme = new ItunesTheme ();

			grdView.DrawRect(grdView.Frame);
		}

		public new MainWindow Window
		{
			get { return (MainWindow)base.Window; }
		}
	}
}
