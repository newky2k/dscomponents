using System;
using DSoft.Datatypes.Grid.Data;
using System.Collections.Generic;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Types;
using DSoft.Datatypes.Formatters;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Content.Res;
using Android.Content;

namespace DSComponentsSample.Data.Grid
{
	/// <summary>
	/// Example data table.
	/// </summary>
	public class ExampleDataTable : DSDataTable
	{

		#region Fields

		private DSBitmap[] mIcons;
		private bool isUpSort;
		private Context mEntryPoint;

		#endregion Fields

		private DSBitmap[] Icons {
			get
			{
				if (mIcons == null)
				{
					mIcons = new DSBitmap[] { 
						BitmapFactory.DecodeResource (mEntryPoint.Resources, Resource.Drawable.first).ToDSBitmap()
						, BitmapFactory.DecodeResource (mEntryPoint.Resources, Resource.Drawable.second).ToDSBitmap()
					};
				}

				return mIcons;
			}
		}

		private DSBitmap CheckBoxImage
		{
			get
			{
				var aImage = BitmapFactory.DecodeResource (mEntryPoint.Resources, Resource.Drawable.checkmark);

				return aImage.ToDSBitmap();
			}
		}

		public ExampleDataTable ()
		{

		}

		public ExampleDataTable (Context EntryPoint, String Name) : base (Name)
		{
			mEntryPoint = EntryPoint;

			var ColumnsDefs = new Dictionary<String,float> ();

			ColumnsDefs.Add ("Image", 30);
			ColumnsDefs.Add ("Ordered", 30);
			ColumnsDefs.Add ("ID", 100);
			ColumnsDefs.Add ("Date", 124);
			ColumnsDefs.Add ("Title", 150);
			ColumnsDefs.Add ("Description", 550);
			ColumnsDefs.Add ("Value", 100);

			foreach (var aKey in ColumnsDefs.Keys)
			{
				// Create a column
				var dc1 = new DSDataColumn (aKey);
				dc1.Caption = aKey;
				dc1.ReadOnly = true;

				if (aKey.Equals ("Image"))
				{
					dc1.DataType = typeof(DSBitmap);
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
					boolFormatter.TrueValue = CheckBoxImage;

					dc1.Formatter = boolFormatter;
				}
				else
				{
					dc1.DataType = typeof(String);
					dc1.AllowSort = true;

				}

				dc1.Width = ColumnsDefs [aKey];

				this.Columns.Add (dc1);
			}

		}

		public override void SortByColumn (int ColumnIndex)
		{
			base.SortByColumn (ColumnIndex);
			isUpSort = !isUpSort;
		}

		/// <summary>
		/// Gets the row count.
		/// </summary>
		/// <returns>The row count.</returns>
		public override int GetRowCount ()
		{
			return 100;
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
			var pos2 = (isUpSort) ? Index : 100 - Index;


			aRow ["ID"] = pos2;
			aRow ["Title"] = @"Test";
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
		/// Gets the value.
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="RowIndex">Row index.</param>
		/// <param name="ColumnName">Column name.</param>
		public override DSDataValue GetValue (int RowIndex, string ColumnName)
		{
			return GetRow (RowIndex).Items [ColumnName];
		}
	}
}

