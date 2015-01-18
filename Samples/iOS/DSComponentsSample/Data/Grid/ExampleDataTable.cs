// ****************************************************************************
// <copyright file="ExampleDataTable.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Grid.Data;
using System.Collections.Generic;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Types;
using DSoft.Datatypes.Formatters;
using DSComponentsSample.Views;

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
	/// <summary>
	/// Example data table.
	/// </summary>
	public class ExampleDataTable : DSDataTable
	{
		#region Fields

		private DSBitmap[] mIcons;
		//private bool isUpSort;
//		private List<DSDataRow> mRows = new List<DSDataRow> ();

		private DSBitmap[] Icons {
			get
			{
				if (mIcons == null)
				{
					mIcons = new DSBitmap[] { UIImage.FromFile ("first.png").ToDSBitmap(), UIImage.FromFile ("second.png").ToDSBitmap() };
				}

				return mIcons;
			}
		}

		#endregion Fields

		#region Constructors
		public ExampleDataTable ()
		{

		}

		public ExampleDataTable (String Name) : base (Name)
		{

			var ColumnsDefs = new Dictionary<String,float> ();

			ColumnsDefs.Add ("Image", 30);
			ColumnsDefs.Add ("Ordered", 100);
			ColumnsDefs.Add ("ID", 100);
			ColumnsDefs.Add ("Date", 124);
			ColumnsDefs.Add ("Title", 150);
			ColumnsDefs.Add ("Description", 550);
			ColumnsDefs.Add ("Value", 100);

			foreach (var aKey in ColumnsDefs.Keys)
			{
				// Create a column
				var dc1 = new DSDataColumn (aKey);

				dc1.Caption = (aKey == "Ordered") ? "Record\nNumber" : aKey;
				dc1.ReadOnly = true;

				if (aKey.Equals ("Image"))
				{
					dc1.DataType = typeof(UIImage);
					dc1.AllowSort = false;
					dc1.Formatter = new DSImageFormatter (new DSSize (ColumnsDefs [aKey], ColumnsDefs [aKey]));
				}
				else if (aKey.Equals ("Ordered"))
				{
					dc1.DataType = typeof(Boolean);
					dc1.AllowSort = false;

//					var boolFormatter = new DSBooleanFormatter (DSoft.UI.Grid.Enums.BooleanFormatterStyle.Text, "Yes", "No");
//					boolFormatter.TextAlignment = DSoft.Datatypes.Enums.TextAlignment.Middle;

					var boolFormatter = new DSBooleanFormatter (BooleanFormatterStyle.Image);
					boolFormatter.Size = new DSSize (10, 10);

					dc1.Formatter = boolFormatter;
				}
				else if (aKey.Equals ("Title"))
				{
					//add a custom view to allow us to update the title
					//DSTextFieldView
					dc1.DataType = typeof(String);
					dc1.AllowSort = true;
					dc1.ReadOnly = false;

					dc1.Formatter = new DSViewFormatter<DSTextFieldView> ();
				}
				else if (aKey.Equals ("Value"))
				{
					dc1.DataType = typeof(String);
					dc1.AllowSort = true;

					//added a text formatter
					dc1.Formatter = new DSTextFormatter (TextAlignment.Left);
				}
				else
				{
					dc1.DataType = typeof(String);
					dc1.AllowSort = true;

				}

				dc1.Width = ColumnsDefs [aKey];

				this.Columns.Add (dc1);
			}

			//add row defs to keep row ids
			for(int loop = 0; loop < 1000; loop++)
			{
				var aRow = new DSDataRow ();
				aRow ["Title"] = @"Test";
				aRow ["ID"] = loop;

				Rows.Add (aRow);
			}
		}
		#endregion

		/*
		 * Below are the overrides for the DSDatatable 
		 * 
		 * As of version 2.6 the selection mechanism uses Row Id instead of indexes.  
		 * This allows it to retain the selection across coloumn sorting, which was not possible before
		 * 
		 * To ensure this functions as expected you can use the "Rows" property of DSDataTable, which would require onlu GetRow(int Index) to be overridden
		 * 
		 * If you want to use your own data source for the rows then you will need to implement all of the override function shown below
		 */
		/// <summary>
		/// Gets the row count.
		/// </summary>
		/// <returns>The row count.</returns>
		public override int GetRowCount ()
		{

			return Rows.Count;
		}

		/// <summary>
		/// Gets the row at the specified indexs
		/// </summary>
		/// <returns>The row.</returns>
		/// <param name="Index">Index.</param>
		public override DSDataRow GetRow (int Index)
		{
			DSDataRow aRow = null;

			if (Index < Rows.Count)
			{
				aRow = Rows [Index];
			}
			else
			{
				aRow = new DSDataRow ();
				aRow ["Title"] = @"Test";
				Rows.Add (aRow);
			}
				
			aRow ["Description"] = @"Some description would go here";
			aRow ["Date"] = DateTime.Now.ToShortDateString ();
			aRow ["Value"] = "10000.00";

			//see if even or odd to pick an image from the array
			var pos = Index % 2;
			aRow ["Image"] = Icons [pos];
			aRow ["Ordered"] = (pos == 0) ? true : false;

			return aRow;
		}

		/// <summary>
		/// Gets the row at the specified indexs
		/// </summary>
		/// <returns>The row.</returns>
		/// <param name="Index">Index.</param>
		/// <param name="RowId">Row identifier.</param>
		public override DSDataRow GetRow(string RowId)
		{
			return base.GetRow(RowId);
		}

		/// <summary>
		/// Return the index of the row with the matching ids
		/// </summary>
		/// <returns>The of row.</returns>
		/// <param name="RowId">Row identifier.</param>
		public override int IndexOfRow(string RowId)
		{
			return base.IndexOfRow(RowId);
		}
		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="RowIndex">Row index.</param>
		/// <param name="ColumnName">Column name.</param>
		public override DSDataValue GetValue (int RowIndex, string ColumnName)
		{
			return GetRow (RowIndex).Items [ColumnName];
		}

		/// <summary>
		/// Sets the value.
		/// </summary>
		/// <param name="RowIndex">Row index.</param>
		/// <param name="ColumnName">Column name.</param>
		/// <param name="Value">Value.</param>
		public override void SetValue (int RowIndex, string ColumnName, object Value)
		{
			GetRow (RowIndex).Items [ColumnName].Value = Value;
		}
	}
}

