// ****************************************************************************
// <copyright file="DSDataValueCollection.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace DSoft.Datatypes.Grid.Data.Collections
{
	/// <summary>
	/// Collection of DSDataValue objects
	/// </summary>
	public class DSDataValueCollection : Collection<DSDataValue>
	{
		/// <summary>
		/// Gets the keys in the collection
		/// </summary>
		/// <value>The keys.</value>
		public List<String> Keys
		{
			get
			{
				var results = new List<String> ();
				
				foreach (var item in this.Items)
				{
					results.Add (item.ColumnName);
				}
				
				return results;
			}
		}

		/// <summary>
		/// Gets the <see cref="DSoft.Datatypes.Grid.Data.Collections.DSDataValueCollection"/> with the specified ColumnName.
		/// </summary>
		/// <param name="ColumnName">Column name.</param>
		public DSDataValue this [string ColumnName]
		{
			get
			{
				if (String.IsNullOrEmpty(ColumnName))
					return null;

				foreach (var item in this.Items)
				{
					if (item.ColumnName.ToLower ().Equals (ColumnName.ToLower ()))
						return item;
				}
				
				return null;
			}
		} 
		
		/// <summary>
		/// Check if the collection Contains the key.
		/// </summary>
		/// <returns><c>true</c>, if key was containsed, <c>false</c> otherwise.</returns>
		/// <param name="Key">Key.</param>
		public Boolean ContainsKey(String Key)
		{
			foreach (var aKey in Keys)
			{
				if (aKey.ToLower ().Equals (Key.ToLower ()))
					return true;
			}
			
			return false;
		}
	}
}

