// ****************************************************************************
// <copyright file="ExampleDataSet.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Grid.Data;
using System.Collections.Generic;
using Android.Content;

namespace DSComponentsSample.Data.Grid
{
	public class ExampleDataSet : DSDataSet
	{

		public ExampleDataSet (Context EntryPoint)
		{
			this.Tables.Add(new ExampleDataTable(EntryPoint, "DT1"));
			this.Tables.Add(new ExampleDataTable2("DT2"));
		}

		/// <summary>
		/// Create a dicitionary of the available tables
		/// </summary>
		/// <returns>The dictionary.</returns>
		public List<String> TableDictionary
		{
			get
			{
				var dict = new List<String>();

				foreach (var aTable in Tables)
				{
					dict.Add(aTable.Name);
				}

				return dict;
			}

		}
	}
}

