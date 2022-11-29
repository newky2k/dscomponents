// ****************************************************************************
// <copyright file="DSDataRow.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Collections.ObjectModel;
using System.Collections;
using DSoft.Datatypes.Grid.Data.Collections;

namespace DSoft.Datatypes.Grid.Data
{
	/// <summary>
	/// Represents a row of data in a DataTable
	/// </summary>
	public class DSDataRow
	{
		#region Functions
		
		private DSDataValueCollection mValues;
		
		#endregion

		#region Properties

		/// <summary>
		/// Gets the row identifier.
		/// </summary>
		/// <value>The row identifier.</value>
		public string RowId {get; set;}

		/// <summary>
		/// Gets the items in the row as a collection of DSDataValue objects
		/// </summary>
		/// <value>The items.</value>
		public DSDataValueCollection Items
		{
			get
			{
				if (mValues == null)
				{
					mValues = new DSDataValueCollection ();
				}
				
				return mValues;
			}
		}
		
		
		/// <summary>
		/// Property indexer to access the items collection by key
		/// </summary>
		/// <param name="key">Key (column name)</param>
		/// <returns>Value of the corresponding cell</returns>
		public object this [string key] 
		{
			get {
				var item = Items [key];
				
				if (item == null) 
				{
					item = new DSDataValue ();
					item.ColumnName = key;
					
					Items.Add (item);
				}
				
				return item.Value;
				
				//return Items[key];
			}
			set
			{
				var item = Items [key];
				
				if (item == null)
				{
					item = new DSDataValue ();
					item.ColumnName = key;
					Items.Add (item);
				}
				
				item.Value = value;

			}
		}

		#endregion
		
		#region Constuctors
		
		/// <summary>
		/// Default constructor
		/// </summary>
		public DSDataRow()
		{
			this.RowId = Guid.NewGuid().ToString();
		}

	
		#endregion

		#region Functions
		/// <summary>
		/// Specifies equality between objects
		/// </summary>
		/// <param name="obj">Object to compare to</param>
		/// <returns>True or False</returns>
		public new bool Equals(object obj)
		{
			bool equal = false;
			if (obj is DSDataRow)
			{
				DSDataRow dest = obj as DSDataRow;
				if (this.Items.Count == dest.Items.Count)
				{
					equal = true;
					foreach (string key in this.Items.Keys)
					{
						equal = equal && dest.Items.ContainsKey(key) && this.Items[key].Equals(dest.Items[key]);
					}
				}
			}
			return equal;
		}

		#endregion

	}
}

