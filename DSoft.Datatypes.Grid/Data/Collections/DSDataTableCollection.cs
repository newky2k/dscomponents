// ****************************************************************************
// <copyright file="DSDataTableCollection.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.ObjectModel;

namespace DSoft.Datatypes.Grid.Data.Collections
{
	/// <summary>
	/// Collection of tables
	/// </summary>
	public class DSDataTableCollection : Collection<DSDataTable>
	{
		#region "Properties"
		
		/// <summary>
		/// Indexer to access the tables based on table name
		/// </summary>
		/// <param name="key">Name of the table</param>
		/// <returns>DataTable</returns>
		public DSDataTable this[string key]
		{
			get
			{
				DSDataTable ret = null;
				foreach (DSDataTable dt in this)
				{
					if (dt.Name == key)
					{
						ret = dt;
						break; // Exit foreach
					}
				}
				return ret;
			}
		}
		
		#endregion
		
		#region "Methods"
		
		/// <summary>
		/// Adds a new table to the collection checking for duplicates in the name
		/// </summary>
		/// <param name="dt">New DataTable to add</param>
		public new void Add(DSDataTable dt)
		{
			foreach (DSDataTable curTable in this)
			{
				if (dt.Name == curTable.Name)
				{
					throw new Exception(String.Format("DataTableCollection: Table with name '{0}' already exists", dt.Name));
				}
			}
			base.Add(dt);
		}
		
		#endregion
	}
}

