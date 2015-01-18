// ****************************************************************************
// <copyright file="DSGridProcessor.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Linq;
using DSoft.Datatypes.Grid.Data.Collections;
using DSoft.Datatypes.Grid.Data;
using DSoft.Datatypes.Grid.Data.Interfaces;
using DSoft.Datatypes.Grid.MetaData.Collections;
using DSoft.Datatypes.Grid.MetaData;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Formatters;
using DSoft.Datatypes.Types;
using System.Collections.Generic;
using DSoft.Datatypes.Grid.Interfaces;

namespace DSoft.Datatypes.Grid.Shared
{
	/// <summary>
	/// DS grid processor.
	/// </summary>
	public class DSGridProcessor
	{
		#region Fields

		private IDSDataSource mDataSource;
		private String mTableName;
		private DSGridViewCellInfoCollection mColDefs;
		private Func<float> mThemeRowHeight;
		private Func<float> mThemeHeaderHeight;
		private Func<GridHeaderStyle> mThemeHeaderStyle;
		private float mRowHeight = -1;
		private List<String> mSelectedRowGuids = new List<String> ();
		private bool mEnableDeselection = false;
		private bool mEnableMulitSelect;
		#endregion

		#region Properties


		/// <summary>
		/// Gets the rows.
		/// </summary>
		/// <value>The rows.</value>
		public IDSGridRowViewCollection Rows
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the free rows.
		/// </summary>
		/// <value>The free rows.</value>
		public IDSGridRowViewCollection FreeRows
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the columns.
		/// </summary>
		/// <value>The columns.</value>
		public DSDataColumnCollection Columns {
			get
			{
				return CurrentTable.Columns;
			}
		}

		/// <summary>
		/// Gets or sets the data source.  If set rebuilds the grid
		/// </summary>
		/// <value>The data source.</value>
		public IDSDataSource DataSource { 
			get
			{
				if (mDataSource == null)
					throw new Exception ("No datasource set on the instance of DSGridView");

				return mDataSource;
			}
			set
			{
				mDataSource = value;
			
			}
		}

		/// <summary>
		/// Gets or sets the name of the table in the datasource, when datasource is a dataset.  Will rebuild the grid when set.
		/// </summary>
		/// <value>The name of the table.</value>
		public string TableName { 
			get
			{
				if (DataSource is DSDataSet)
				{
					if (String.IsNullOrEmpty (mTableName))
						throw new Exception ("TableName has not be set for the DSDataSet datasource in this instance of DSGridView");

					return mTableName;
				}
				else
				{
					return string.Empty;
				}
			}
			set
			{
				mTableName = value;

			}
		}

		/// <summary>
		/// Gets the current table.
		/// </summary>
		/// <value>The current table.</value>
		public DSDataTable CurrentTable {
			get
			{
				if (DataSource is DSDataTable)
				{
					return (DSDataTable)DataSource;
				}
				else if (DataSource is DSDataSet)
				{
					return ((DSDataSet)DataSource).Tables [TableName];
				}

				throw new Exception ("No recognised datasource found");
			}
		}
			
		/// <summary>
		/// Gets the col defs.
		/// </summary>
		/// <value>The col defs.</value>
		public DSGridViewCellInfoCollection ColDefs {
			get
			{
				if (mColDefs == null || mColDefs.Count == 0)
				{
					mColDefs = new DSGridViewCellInfoCollection ();

					float posX = 0.0f;
					float tallestCell = 0.0f;

					foreach (DSDataColumn col in Columns)
					{
						float ContentWidth = 0;

						if (col.Width != -1)
						{
							ContentWidth = col.Width;
						}
						else
						{

							ContentWidth = FindWidthOfColumn (col);
						}

						ContentWidth = FindWidthOfColumn (col);

						var cellInfo = new DSGridViewCellInfo ();
						cellInfo.xPosition = Columns.IndexOf (col);
						cellInfo.width = ContentWidth;
						cellInfo.height = ThemeRowHeight();
						cellInfo.IsReadOnly = col.ReadOnly;

						if (col.Formatter != null && !(col.Formatter is DSTextFormatter) && col.Formatter.Size != null)
						{
							cellInfo.height = col.Formatter.Size.Height;

						}
						cellInfo.y = 0.0f;
						cellInfo.x = posX;
						cellInfo.Formatter = col.Formatter;

						if (col.IsSortColumn)
						{
							cellInfo.SortStyle = (col.UseDescendingSort) ? SortIndicatorStyle.Descending : SortIndicatorStyle.Ascending;
						}

						cellInfo.Name = col.ColumnName;

						posX += ContentWidth;

						//see if the height is the tallest
						if (cellInfo.height > tallestCell)
							tallestCell = cellInfo.height;

						mColDefs.Add (cellInfo);
					}
				}

				return mColDefs;
			}
		}

		/// <summary>
		/// Gets the number of rows.
		/// </summary>
		/// <value>The number of rows.</value>
		public int NumberOfRows {
			get
			{
				if (CurrentTable == null)
					return 0;

				var rows = CurrentTable.GetRowCount ();

				if (ThemeHeaderStyle() != GridHeaderStyle.None)
				{
					rows += 1;
				}

				return rows;
			}
		}

		/// <summary>
		/// Gets the height of the row.
		/// </summary>
		/// <value>The height of the row.</value>
		public float RowHeight {
			get
			{
				if (mRowHeight == -1)
				{
					float tallestCell = 0.0f; 
					//build content size
					foreach (DSDataColumn col in Columns)
					{
						if (col.Formatter != null && !(col.Formatter is DSTextFormatter) && col.Formatter.Size != null)
						{
							if (col.Formatter.Size.Height > tallestCell)
							{
								tallestCell = col.Formatter.Size.Height;
							}
						}

					}


					var thHieght = ThemeRowHeight();
					var rowhieght = (tallestCell > thHieght) ? tallestCell : thHieght;

					mRowHeight = rowhieght;
				}

				return mRowHeight;
			}
		}

		/// <summary>
		/// Gets or sets the height of the theme row function
		/// </summary>
		/// <value>The height of the theme row.</value>
		public Func<float> ThemeRowHeight
		{
			get
			{
				if (mThemeRowHeight == null)
					throw new Exception("You must set DSGridProcess.ThemeRowHeight");

				return mThemeRowHeight;
			}
			set
			{
				mThemeRowHeight = value;
			}
		}

		/// <summary>
		/// Gets or sets the height of the theme header.
		/// </summary>
		/// <value>The height of the theme header.</value>
		public Func<float> ThemeHeaderHeight
		{
			get
			{
				if (mThemeHeaderHeight == null)
					throw new Exception("You must set DSGridProcess.ThemeHeaderHeight");

				return mThemeHeaderHeight;
			}
			set
			{
				mThemeHeaderHeight = value;
			}
		}

		/// <summary>
		/// Gets or sets the theme header style.
		/// </summary>
		/// <value>The theme header style.</value>
		public Func<GridHeaderStyle> ThemeHeaderStyle
		{
			get
			{
				if (mThemeHeaderStyle == null)
					throw new Exception("You must set DSGridProcess.ThemeHeaderStyle");

				return mThemeHeaderStyle;
			}
			set
			{
				mThemeHeaderStyle = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to enable multi-select, updates EnableDeselection with same value
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
		public bool EnableMulitSelect {
			get
			{
				return mEnableMulitSelect;
			}
			set
			{
				if (mEnableMulitSelect != value)
				{
					mEnableMulitSelect = value;
					EnableDeselection = value;

					ClearSelectedItems (true);
				}
			}
		}

		/// <summary>
		/// Gets or sets a value wether Deselection(by tap the same row) will be enabled.  Enabled by default when EnableMultiSelect is enabled
		/// </summary>
		/// <value><c>true</c> if enable deselection; otherwise, <c>false</c>.</value>
		public bool EnableDeselection {
			get
			{
				return mEnableDeselection;
			}
			set
			{
				if (mEnableDeselection != value)
				{
					mEnableDeselection = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the selected item. If multi-select enabled then it will return the first selected item
		/// </summary>
		/// <value>The selected item.</value>
		public DSDataRow SelectedItem {
			get
			{
				if (mSelectedRowGuids.Count == 0) 
					return null;


				return CurrentTable.GetRow (mSelectedRowGuids[0]);
			}
			set
			{
				if (value == null)
				{
					ClearSelectedItems(true);
				}
				else
				{
					UpdateSelection(value.RowId);
				}

			}
		}

		/// <summary>
		/// Gets or sets the selected items.
		/// </summary>
		/// <value>The selected items.</value>
		public DSDataRow[] SelectedItems {
			get
			{
				if (mSelectedRowGuids.Count == 0) 
					return null;


				return CurrentTable.GetRows(mSelectedRowGuids.ToArray());
			}
			set
			{
				if (value == null)
				{
					ClearSelectedItems(true);
				}
				else
				{
					var rowIds = from e in value
						select e.RowId;

					UpdateSelection(rowIds.ToArray());
				}
			}
		}

		/// <summary>
		/// Gets or sets the index of the selected. If multi-select enabled then it will return the index of the first
		/// selected item
		/// </summary>
		/// <value>The index of the selected.</value>
		public int SelectedIndex {
			get
			{
				return CurrentTable.IndexOfRow(mSelectedRowGuids[0]);
			}
			set
			{
				UpdateSelection (value);
			}
		}

		/// <summary>
		/// Gets or sets the selected indexes.
		/// </summary>
		/// <value>The selected indexes.</value>
		public int[] SelectedIndexes {
			get
			{
				return CurrentTable.IndexesOfRows(mSelectedRowGuids.ToArray());
			}
			set
			{
				UpdateSelection (value);
			}
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Grid.Shared.DSGridProcessor"/> class.
		/// </summary>
		public DSGridProcessor()
		{
			Rows = new IDSGridRowViewCollection();
			FreeRows = new IDSGridRowViewCollection();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Reset this Processor.
		/// </summary>
		public void Reset()
		{
			mColDefs = null;
			mRowHeight = -1;

			Rows.Dispose();
		}
		/// <summary>
		/// Finds the width of column.
		/// </summary>
		/// <returns>The width of column.</returns>
		/// <param name="Column">Column.</param>
		public float FindWidthOfColumn (DSDataColumn Column)
		{
			return Column.Width;
		}

		/// <summary>
		/// Calculates the scrollview size.
		/// </summary>
		public DSSize CalculateSize ()
		{
			float scrollContentWidth = 0;
			//build content size
			foreach (DSDataColumn col in Columns)
			{
				scrollContentWidth += FindWidthOfColumn (col);
			}

			//set one for the headers
			float ContentHeight = 0;
			if (this.ThemeHeaderStyle() != GridHeaderStyle.None)
			{
				ContentHeight = (this.ThemeHeaderHeight() - this.ThemeRowHeight());
			}

			ContentHeight += (NumberOfRows * RowHeight);

			return new DSSize (scrollContentWidth, ContentHeight);

		}

		/// <summary>
		/// Tops the X for row.
		/// </summary>
		/// <returns>The X for row.</returns>
		/// <param name="Index">Index.</param>
		public float TopYForRow (int Index)
		{
			if (Index == 0)
			{
				return 0.0f;
			}

			float topY = 0;
			if (this.ThemeHeaderStyle() != GridHeaderStyle.None)
			{
				topY = (this.ThemeHeaderHeight() - RowHeight);
			}

			topY += RowHeight * Index;

			return topY;
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="Index">Index.</param>
		/// <param name="CellName">Cell name.</param>
		public DSDataValue GetValue (int Index, String CellName)
		{
			if (Index == -1)
			{
				return new DSDataValue () { ColumnName = CellName, Value = Columns [CellName].Caption };
			}
			else
			{
				return CurrentTable.GetValue (Index, CellName);
			}
		}

		/// <summary>
		/// Sets the value for the row and column
		/// </summary>
		/// <param name="Index">Index.</param>
		/// <param name="CellName">Cell name.</param>
		/// <param name="value">Value.</param>
		public void SetValue (int Index, String CellName, object value)
		{
			if (Index != -1)
			{
				CurrentTable.SetValue (Index, CellName, value);
			}
		}

		/// <summary>
		/// Rebuilds the selection.
		/// </summary>
		public void RebuildSelection ()
		{

			foreach (IDSGridRowView aRow in Rows)
			{
				aRow.Processor.IsSelected = mSelectedRowGuids.Contains(aRow.Processor.RowId);
			}
		}

		/// <summary>
		/// Updates the selection.
		/// </summary>
		/// <param name="RowId">Row identifier.</param>
		public void UpdateSelection (String RowId)
		{

			if (EnableMulitSelect)
			{
				if (mSelectedRowGuids.Contains(RowId))
				{
					if (mEnableDeselection)
					{
						//remove on second tap in multi-select mode
						mSelectedRowGuids.Remove (RowId);
					}

				}
				else
				{
					mSelectedRowGuids.Add(RowId);
				}
			}
			else
			{


				if (!mEnableDeselection)
				{
					mSelectedRowGuids.Clear ();

					mSelectedRowGuids.Add (RowId);
				}
				else
				{
					if (mSelectedRowGuids.Contains(RowId))
					{
						mSelectedRowGuids.Clear ();

					}
					else
					{
						mSelectedRowGuids.Clear ();
						mSelectedRowGuids.Add (RowId);
					}
				}

			}

			RebuildSelection ();
		}

		/// <summary>
		/// Updates the selection
		/// </summary>
		/// <param name="index">Index.</param>
		public void UpdateSelection (int index)
		{
			var aRow = CurrentTable.GetRow(index);

			if (aRow != null)
			{
				UpdateSelection(aRow.RowId);
			}

		}

		/// <summary>
		/// Updates the selection with multi-select
		/// </summary>
		/// <param name="Indexes">Indexes.</param>
		public void UpdateSelection (String[] Indexes)
		{
			if (Indexes == null)
			{
				mSelectedRowGuids.Clear ();
				return;
			}

			if (!EnableMulitSelect && Indexes.Length > 0)
			{
				UpdateSelection (Indexes [0]);
				return;
			}


			mSelectedRowGuids.Clear ();
			mSelectedRowGuids.AddRange (Indexes);

			RebuildSelection ();

		}

		/// <summary>
		/// Updates the selection, with multi-select
		/// </summary>
		/// <param name="Indexes">Indexes.</param>
		public void UpdateSelection (int[] Indexes)
		{
			if (Indexes == null)
			{
				mSelectedRowGuids.Clear ();
				return;
			}

			var rows = CurrentTable.GetRows(Indexes);

			var rowIds = from e in rows
				select e.RowId;

			UpdateSelection(rowIds.ToArray());

		}

		/// <summary>
		/// Clears the selected items.
		/// </summary>
		public void ClearSelectedItems (bool rebuild)
		{
			mSelectedRowGuids.Clear ();

			if (rebuild)
			{
				RebuildSelection ();
			}

		}

		/// <summary>
		/// Finds the view for row.
		/// </summary>
		/// <returns>The view for row.</returns>
		/// <param name="Index">Index.</param>
		/// <param name="NewFunction">New function.</param>
		public IDSGridRowView FindViewForRow (int Index, Func<int,IDSGridRowView> NewFunction)
		{
			var aRow = Rows.ViewForRowIndex (Index);

			//see if a row is already available
			if (aRow == null)
			{
				//load one from the freecells collection
				if (FreeRows.Count != 0)
				{
					aRow = FreeRows [0];

					if (aRow != null)
					{
						aRow.Processor.RowIndex = Index;
						FreeRows.RemoveAt (0);
					}
				}

				if (aRow == null)
				{
					aRow = NewFunction(Index);

					//aRow = new DSGridRowView (Index, this);

				}

				//
				aRow.Processor.Style = CellStyle.Cell;

				//add to collection
				Rows.Add (aRow);
			}

			if (Index == 0)
				aRow.Processor.Style = (this.ThemeHeaderStyle() == GridHeaderStyle.Fixed) ? CellStyle.Blank : CellStyle.Header;

			//Work out the position of the row in the grid
			if (Index == 0)
				aRow.Processor.PositionType = RowPositionType.Top;
			else if (Index == NumberOfRows - 1)
			{
				aRow.Processor.PositionType = RowPositionType.Bottom;
			}
			else
			{
				aRow.Processor.PositionType = RowPositionType.Middle;
			}

			aRow.Processor.IsSelected = false;

			if (Index != 0)
			{
				var aDSRow = CurrentTable.GetRow(aRow.Processor.RowIndex);

				if (aDSRow != null)
				{
					aRow.Processor.RowId = aDSRow.RowId;
					aRow.Processor.IsSelected = mSelectedRowGuids.Contains(aRow.Processor.RowId);
				}

			}



			return aRow;
		}
			
		#endregion


	}
}

