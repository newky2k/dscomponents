// ****************************************************************************
// <copyright file="DSNetGridViewController.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.UI.Grid;
using DSComponentsSample.Data.Grid;
using System.Net;
using System.Threading.Tasks;
using System.Threading;

#if __UNIFIED__
using UIKit;
using CoreGraphics;
using Foundation;
#else
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using System.Drawing;
#endif

namespace DSComponentsSample.Controllers.Grid
{
	public class DSNetGridViewController : DSGridViewController
	{
		#region Fields

		static readonly Uri RssFeedUrl = new Uri ("http://phobos.apple.com/WebObjects/MZStoreServices.woa/ws/RSS/toppaidapplications/limit=25/xml");
		private FeedDataTable mDatasource = new FeedDataTable ();
		private UIAlertView mAlert = null;

		#endregion

		#region Constructors

		public DSNetGridViewController () : base ()
		{
			///Set the title and tab image
			this.Title = "Apple Top 100";
			this.TabBarItem.Image = UIImage.FromBundle ("first");

			//Allow selection an bouncing
			ShowSelection = true;
			EnableBounce = true;

			//Set the datasource of the grid view
			DataSource = mDatasource;

		}

		#endregion

		#region Overrides

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.View.BackgroundColor = UIColor.White;

			//old method
//			if (iOSHelper.IsiOS7)
//			{
//				var aFrame = this.GridView.Frame;
//				aFrame.Y += 64;
//				aFrame.Height -= 64;
//
//				this.GridView.Frame = aFrame;
//			}

			//new method
			DisableNavigationControllerSizing = false;
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			if (this.ParentViewController.NavigationController != null)
			{
				var aButton = new UIBarButtonItem (UIBarButtonSystemItem.Refresh);

				aButton.Clicked += (object sender, EventArgs e) => {
					ReloadData ();
				};

				this.ParentViewController.NavigationItem.LeftBarButtonItem = null;
				this.ParentViewController.NavigationItem.RightBarButtonItems = new UIBarButtonItem[]{ aButton };
			}
			if (!mDatasource.HasData && UIApplication.SharedApplication.NetworkActivityIndicatorVisible != true)
			{
				ReloadData ();
			}

		}

		#endregion

		#region Methods and Handlers

		private void DownloadCompleted (object sender, DownloadStringCompletedEventArgs e)
		{
			UIApplication.SharedApplication.BeginInvokeOnMainThread (() => {

				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

				mAlert.DismissWithClickedButtonIndex (0, true);

				if (e.Error != null)
				{
					DisplayError ("Warning", "The rss feed could not be downloaded: " + e.Error.Message);
				}
				else
				{
					try
					{
						//clear the selected items
						this.GridView.ClearSelectedItems();

						//Clear the rows
						mDatasource.ClearRows();
						mDatasource.Apps.Clear ();

						foreach (var v in RssParser.Parse (e.Result))
							mDatasource.Apps.Add (v);

						mAlert = new UIAlertView ("Fetching Icons...", "", null, null, null);
						mAlert.Show ();
						UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

						Task.Run (() => {

							foreach (var anApp in mDatasource.Apps)
							{
								byte[] data = null;

								using (var c = new GzipWebClient ())
								{
									data = c.DownloadData (anApp.ImageUrl);
								}


								anApp.Image = UIImage.LoadFromData (NSData.FromArray (data));
							}


						}).ContinueWith (prevTask => {

							mAlert.DismissWithClickedButtonIndex (0, true);
							UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

							GridView.ReloadData ();

						}, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext ());

					}
					catch
					{
						DisplayError ("Warning", "Malformed Xml was found in the Rss Feed.");
					}
				}
			});

		}

		private void DisplayError (string title, string errorMessage, params object[] formatting)
		{
			var alert = new UIAlertView (title, string.Format (errorMessage, formatting), null, "ok", null);
			alert.Show ();
		}

		private void ReloadData ()
		{
			mAlert = new UIAlertView ("Downloading...", "", null, null, null);
			mAlert.Show ();

			// Show the user that data is about to be downloaded
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

			using (var downloader = new GzipWebClient ())
			{
				downloader.DownloadStringCompleted += DownloadCompleted;
				downloader.DownloadStringAsync (RssFeedUrl);
			}

		}

		#endregion
	}

	public class GzipWebClient : WebClient
	{
		protected override WebRequest GetWebRequest (Uri address)
		{
			var request = base.GetWebRequest (address);
			if (request is HttpWebRequest)
				((HttpWebRequest)request).AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			return request;
		}
	}
}

