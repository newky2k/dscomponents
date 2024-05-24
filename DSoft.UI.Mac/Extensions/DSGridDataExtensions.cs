// ****************************************************************************
// <copyright file="DSGridDataExtensions.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Data;
using DSoft.Datatypes.Grid.Data;

namespace System.Data
{
	/// <summary>
	/// Class extension for .Net Data objects to convert to DS data objects
	/// </summary>
	public static class DSDataExtensions
	{
		/// <summary>
		/// Convert a .Net DataSet object to a DSDataSet object
		/// </summary>
		/// <returns>The DS data set.</returns>
		/// <param name="Data">Data.</param>
		public static DSDataSet ToDSDataSet (this DataSet Data)
		{
			var newDS = new DSDataSet (Data.DataSetName);

			foreach (DataTable table in Data.Tables)
			{
				newDS.Tables.Add (table.ToDSDataTable ());
			}

			return newDS;
		}

		/// <summary>
		/// Convert a .Net DataTable object to a DSDataTable object
		/// </summary>
		/// <returns>The DS data set.</returns>
		/// <param name="Data">Data.</param>
		public static DSDataTable ToDSDataTable (this DataTable Data)
		{
			var newDT = new DSDataTable (Data.TableName);

			//add columns
			foreach (DataColumn column in Data.Columns)
			{
				newDT.Columns.Add (column.ToDSDataColumn ());
			}

			foreach (DataRow row in Data.Rows)
			{
				newDT.Rows.Add (row.ToDSDataRow ());

			}

			return newDT;
		}

		/// <summary>
		/// Convert a .Net DataColumn object to a DSDataColumn object
		/// </summary>
		/// <returns>The DS data column.</returns>
		/// <param name="Data">Data.</param>
		internal static DSDataColumn ToDSDataColumn (this DataColumn Data)
		{
			var result = new DSDataColumn (Data.ColumnName);

			result.ColumnName = Data.ColumnName;
			result.DataType = Data.DataType;
			result.Caption = Data.Caption;

			return result;
		}

		/// <summary>
		/// Convert a .Net DataRow object to a DSDataRow object
		/// </summary>
		/// <returns>The DS data row.</returns>
		/// <param name="Data">Data.</param>
		internal static DSDataRow ToDSDataRow (this DataRow Data)
		{
			var result = new DSDataRow ();

			foreach (DataColumn column in Data.Table.Columns)
			{
				result [column.ColumnName] = Data [column.ColumnName];

			}


			return result;
		}
	}
}

