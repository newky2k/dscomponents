// ****************************************************************************
// <copyright file="DSDataRowCollection.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;

namespace DSoft.Datatypes.Grid.Data.Collections
{
	/// <summary>
	/// Collection of rows in a DataTable
	/// </summary>
	public class DSDataRowCollection : Collection<DSDataRow>
	{
		/// <summary>
		/// Sort the row collection using the specified Column.
		/// </summary>
		/// <param name="Column">Column.</param>
		public void Sort(DSDataColumn Column)
		{
			var results = new List<DSDataRow> ();

			if (Column.UseDescendingSort) 
			{
				//Run the sort
				results = this.OrderByDescending (row => row[Column.ColumnName]).ToList();
			}
			else 
			{
				//Run the sort
				results = this.OrderBy(row => row[Column.ColumnName]).ToList();
			}

		
			this.Items.Clear ();


			foreach (var item in results) 
			{
				this.Items.Add(item);
			}

		}


	}
}

