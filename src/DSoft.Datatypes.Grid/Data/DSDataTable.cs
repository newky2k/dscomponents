// ****************************************************************************
// <copyright file="DSDataTable.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Grid.Data.Interfaces;
using DSoft.Datatypes.Grid.Data.Collections;
using System.Collections.Generic;

namespace DSoft.Datatypes.Grid.Data
{
	/// <summary>
	/// Represents a DataTable
	/// </summary>
	public class DSDataTable : IDSDataSource
	{
		#region Private Fields

		private DSDataColumnCollection mColumns;
		private DSDataRowCollection mRows;

		#endregion

		#region Public Properties

		/// <summary>
		/// Name of the DataTable
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Collection of columns
		/// </summary>
		public DSDataColumnCollection Columns {
			get
			{
				return mColumns;
			}
		}

		/// <summary>
		/// Collection of rows
		/// </summary>
		public virtual DSDataRowCollection Rows { 
			get
			{
				if (mRows == null)
				{
					mRows = new DSDataRowCollection ();
				}
				return mRows;
			}
		}

		#endregion

		#region Contructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Data.DSDataTable"/> class.
		/// </summary>
		public DSDataTable ()
		{
			mColumns = new DSDataColumnCollection ();
			mRows = new DSDataRowCollection ();
		}

		/// <summary>
		/// Constructor of the DataTable with a name
		/// </summary>
		/// <param name="name">Name of the DataTable</param>
		public DSDataTable (string name) : this ()
		{
			this.Name = name;

		}

		#endregion

		#region Functions

		#endregion

		#region Methods

		/// <summary>
		/// Sorts the datatable by specified column.
		/// </summary>
		/// <param name="ColumnIndex">Column index.</param>
		public virtual void SortByColumn (int ColumnIndex)
		{
			//find the column and then sort by it
			var aColumn = mColumns [ColumnIndex];

			if (aColumn != null && aColumn.AllowSort)
			{
				mColumns.ResetSortedColumn (aColumn);
				mRows.Sort (aColumn);

			}

		}

		/// <summary>
		/// Gets the row count.
		/// </summary>
		/// <returns>The row count.</returns>
		public virtual int GetRowCount ()
		{
			return Rows.Count;
		}

		/// <summary>
		/// Gets the row at the specified indexs
		/// </summary>
		/// <returns>The row.</returns>
		/// <param name="Index">Index.</param>
		public virtual DSDataRow GetRow (int Index)
		{
			if (Rows.Count == 0)
				return null;

			return Rows [Index];
		}

		/// <summary>
		/// Gets the associated with the indexes
		/// </summary>
		/// <returns>The rows.</returns>
		/// <param name="Indexes">Indexes.</param>
		public DSDataRow[] GetRows(int[] Indexes)
		{
			var aRows = new List<DSDataRow>();

			foreach (var aIndex in Indexes)
			{
				var aRow = GetRow(aIndex);

				aRows.Add(aRow);
			}

			return aRows.ToArray();
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="RowIndex">Row index.</param>
		/// <param name="ColumnName">Column name.</param>
		public virtual DSDataValue GetValue (int RowIndex, String ColumnName)
		{
			return GetRow(RowIndex).Items[ColumnName];
			//return Rows [RowIndex].Items [ColumnName];
		}

		/// <summary>
		/// Sets the value.
		/// </summary>
		/// <param name="RowIndex">Row index.</param>
		/// <param name="ColumnName">Column name.</param>
		/// <param name="Value">Value.</param>
		public virtual void SetValue (int RowIndex, String ColumnName, object Value)
		{
			GetRow(RowIndex).Items[ColumnName].Value = Value;
			//Rows [RowIndex].Items [ColumnName].Value = Value;
		}

		/// <summary>
		/// Return the index of the row with the matching ids
		/// </summary>
		/// <returns>The of row.</returns>
		/// <param name="RowId">Row identifier.</param>
		public virtual int IndexOfRow(String RowId)
		{
			foreach (var aRow in Rows)
			{
				if (aRow.RowId.Equals(RowId))
					return Rows.IndexOf(aRow);
			}
			return -1;
		}


		/// <summary>
		/// Return the index of the rows with the matching ids
		/// </summary>
		/// <returns>The of rows.</returns>
		/// <param name="RowIds">Row identifiers.</param>
		public int[] IndexesOfRows(String[] RowIds)
		{
			var ids = new List<int>();

			foreach (var aId in RowIds)
			{
				ids.Add(IndexOfRow(aId));
			}

			return ids.ToArray();
		}

		/// <summary>
		/// Rows the with identifier.
		/// </summary>
		/// <returns>The with identifier.</returns>
		/// <param name="RowId">Row identifier.</param>
		public virtual DSDataRow GetRow(String RowId)
		{
			foreach (var aRow in Rows)
			{
				if (aRow.RowId.Equals(RowId))
					return aRow;
			}

			return null;
		}

		/// <summary>
		/// Return the the rows with the matching ids
		/// </summary>
		/// <returns>The rows.</returns>
		/// <param name="RowIds">Row identifiers.</param>
		public DSDataRow[] GetRows(String[] RowIds)
		{
			var results = new List<DSDataRow>();

			foreach (var aId in RowIds)
			{
				results.Add(GetRow(aId));
			}

			return results.ToArray();
		}
		#endregion
	}
}

