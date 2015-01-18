// ****************************************************************************
// <copyright file="DSDataColumnCollection.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.ObjectModel;

namespace DSoft.Datatypes.Grid.Data.Collections
{
	/// <summary>
	/// Collection of columns in a DataTable
	/// </summary>
	public class DSDataColumnCollection : Collection<DSDataColumn>
	{
		#region "Properties"
		
		/// <summary>
		/// Indexer to access the columns based on column name
		/// </summary>
		/// <param name="key">Name of the column</param>
		/// <returns>DataColumns</returns>
		public DSDataColumn this[string key]
		{
			get
			{
				DSDataColumn ret = null;
				foreach (DSDataColumn dc in this)
				{
					if (dc.ColumnName == key)
					{
						ret = dc;
						break; // Exit foreach
					}
				}
				return ret;
			}
		}
		
		#endregion
		
		#region "Methods"
		
		/// <summary>
		/// Adds a new column to the collection checking for duplicates in the name
		/// </summary>
		/// <param name="dc">New column to add</param>
		public new void Add(DSDataColumn dc)
		{
			foreach (DSDataColumn curColumn in this)
			{
				if (dc.ColumnName == curColumn.ColumnName)
				{
					throw new Exception(String.Format("DataColumnCollection: Column with name '{0}' already exists", dc.ColumnName));
				}
			}
			base.Add(dc);
		}

		/// <summary>
		/// Resets the current sorted column and sets it to the new column
		/// </summary>
		/// <param name="dc">Dc.</param>
		public void ResetSortedColumn(DSDataColumn dc)
		{
			foreach (DSDataColumn curColumn in this) 
			{

				if (curColumn.Equals(dc))
				{
					//check to see if it already is the sort column, then change the order to the opposite
					curColumn.UseDescendingSort = (curColumn.IsSortColumn == true) ? !curColumn.UseDescendingSort : false;
					curColumn.IsSortColumn = true;
				}
				else
				{
					curColumn.IsSortColumn = false;
				}
			}
		}

		/// <summary>
		/// Returns the current sort columns
		/// </summary>
		/// <returns>The column.</returns>
		public DSDataColumn SortColumn()
		{
			foreach (DSDataColumn curColumn in this) 
			{
				if (curColumn.IsSortColumn) return curColumn;
			}

			return null;
		}

		#endregion
	}
}

