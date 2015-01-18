// ****************************************************************************
// <copyright file="FeedDataTable.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Grid.Data;
using DSoft.Datatypes.Formatters;
using DSoft.Datatypes.Types;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

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

namespace DSComponentsSample.Data.Grid
{
	public class FeedDataTable  : DSDataTable
	{
	
		#region Properties
		public ObservableCollection<App> Apps { get; private set; }

		public bool HasData { 
			get
			{
				return (Apps.Count != 0);
			}
		}
		#endregion

		#region Constructors
		public FeedDataTable ()
		{
			Apps = new ObservableCollection<App> ();

			var dc1 = new DSDataColumn ("ICON");
			dc1.Caption = "";
			dc1.ReadOnly = true;
			dc1.DataType = typeof(UIImage);
			dc1.AllowSort = false;
			dc1.Width = 75.0f;
			dc1.Formatter = new DSImageFormatter (new DSSize (dc1.Width, dc1.Width)) {
				Margin = new DSInset (5.0f),
			};

			this.Columns.Add (dc1);


			// Create a column
			var dc2 = new DSDataColumn ("Name");
			dc2.Caption = "Name";
			dc2.ReadOnly = true;
			dc2.DataType = typeof(String);
			dc2.AllowSort = false;
			dc2.Width = 150;
			this.Columns.Add (dc2);

			// Create a column
			dc2 = new DSDataColumn ("Artist");
			dc2.Caption = "Artist";
			dc2.ReadOnly = true;
			dc2.DataType = typeof(String);
			dc2.AllowSort = false;
			dc2.Width = 200;
			this.Columns.Add (dc2);
		}

		#endregion

		#region Methods
		/// <summary>
		/// Clears the rows.
		/// </summary>
		public void ClearRows()
		{
			Rows.Clear();
		}
		#endregion

		#region Overrides
		/// <summary>
		/// Gets the row count.
		/// </summary>
		/// <returns>The row count.</returns>
		public override int GetRowCount ()
		{
			return Apps.Count;
		}

		/// <summary>
		/// Gets the row at the specified indexs
		/// </summary>
		/// <returns>The row.</returns>
		/// <param name="Index">Index.</param>
		public override DSDataRow GetRow(int Index)
		{
			DSDataRow aRow = null;
			var anApp = Apps [Index];

			if (Index < Rows.Count)
			{
				aRow = Rows [Index];
			}
			else
			{
				aRow = new DSDataRow ();
				Rows.Add (aRow);
			}
				
			aRow ["Name"] = anApp.Name;
			aRow ["Artist"] = anApp.Artist;
			aRow ["ICON"] = anApp.Image;

			return aRow;
		}
			
		#endregion
	}

	public class App
	{
		public string Artist { get; set; }

		public UIImage Image { get; set; }

		public Uri ImageUrl { get; set; }

		public string Name { get; set; }

		public Uri Url { get; set; }
	}

	public static class RssParser
	{
		// These are used to select the correct nodes and attributes from the Rss feed
		static readonly XName FeedElement = XName.Get ("feed", "http://www.w3.org/2005/Atom");
		static readonly XName EntryElement = XName.Get ("entry", "http://www.w3.org/2005/Atom");
		static readonly XName AppUrlElement = XName.Get ("id", "http://www.w3.org/2005/Atom");
		static readonly XName AppNameElement = XName.Get ("name", "http://itunes.apple.com/rss");
		static readonly XName ArtistElement = XName.Get ("artist", "http://itunes.apple.com/rss");
		static readonly XName ImageUrlElement = XName.Get ("image", "http://itunes.apple.com/rss");
		static readonly XName HeightAttribute = XName.Get ("height", "");

		public static List<App> Parse (string xml)
		{
			// Open the xml
			var doc = XDocument.Parse (xml);

			// We want to convert all the raw Xml nodes called 'entry' which
			// are in that namespace into instances of the 'App' class so they
			// can be displayed easily in the table.
			return doc.Element (FeedElement) // Select the 'feed' node.
					.Elements (EntryElement)     // Select all children with the name 'entry'.
					.Select (XmlElementToApp)    // Convert the 'entry' nodes to instances of the App class.
					.ToList ();                  // Return as a List<App>.
		}

		static App XmlElementToApp (XElement entry)
		{
			// The document may contain many image nodes. Select the one with
			// the largest resolution.
			var imageUrlNode = entry.Elements (ImageUrlElement)
				.Where (n => n.Attribute (HeightAttribute) != null)
				.OrderBy (node => int.Parse (node.Attribute (HeightAttribute).Value))
				.LastOrDefault ();

			// Parse the rest of the apps information from the XElement and
			// return the App instance.
			return new App {
				Name = entry.Element (AppNameElement).Value,
				Url = new Uri (entry.Element (AppUrlElement).Value),
				Artist = entry.Element (ArtistElement).Value,
				ImageUrl = new Uri (imageUrlNode.Value)
			};
		}
	}
}

