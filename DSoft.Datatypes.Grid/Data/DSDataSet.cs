// ****************************************************************************
// <copyright file="DSDataSet.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Grid.Data.Interfaces;
using DSoft.Datatypes.Grid.Data.Collections;

namespace DSoft.Datatypes.Grid.Data
{
	/// <summary>
	/// DSoft DataSet class
	/// </summary>
	public class DSDataSet : IDSDataSource
	{
		#region Properties
		
		/// <summary>
		/// Name of the DataSet
		/// </summary>
		public string Name { get; set; }
		
		/// <summary>
		/// Collection of DataTables
		/// </summary>
		public DSDataTableCollection Tables { get; set; }
		
		#endregion
		
		#region Contructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Data.DSDataSet"/> class.
		/// </summary>
		public DSDataSet ()
		{
			this.Tables = new DSDataTableCollection();
		}
		/// <summary>
		/// Constructor of the class
		/// </summary>
		/// <param name="name">Name of the DataSet</param>
		public DSDataSet(string name) : this()
		{
			this.Name = name;

		}

		#endregion
	
		#region Methods
		/// <summary>
		/// Gets the row count.
		/// </summary>
		/// <returns>The row count.</returns>
		/// <param name="TableName">Table name.</param>
		public virtual int GetRowCount(String TableName)
		{
			return Tables [TableName].GetRowCount ();
		}


		/// <summary>
		/// Gets the row.
		/// </summary>
		/// <returns>The row.</returns>
		/// <param name="Index">Index.</param>
		/// <param name="TableName">Table name.</param>
		public virtual DSDataRow GetRow(int Index, String TableName)
		{
			return Tables[TableName].GetRow(Index);
		}

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="RowIndex">Row index.</param>
		/// <param name="TableName">Table name.</param>
		/// <param name="ColumnName">Column name.</param>
		public virtual DSDataValue GetValue(int RowIndex, String TableName, String ColumnName)
		{
			return Tables [TableName].GetValue (RowIndex, ColumnName);
		}

		#endregion
	}
}

