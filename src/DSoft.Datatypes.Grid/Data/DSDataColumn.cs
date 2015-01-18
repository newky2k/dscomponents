// ****************************************************************************
// <copyright file="DSDataColumn.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Base;

namespace DSoft.Datatypes.Grid.Data
{
	
	/// <summary>
	/// Represents a column of data in a DataTable
	/// </summary>
	public class DSDataColumn
	{
		#region Fields
		
		private DSFormatter mFormatter;
		
		#endregion
		#region "Properties"
		
		/// <summary>
		/// Gets or sets the formatter of the cell
		/// </summary>
		/// <value>The formatter.</value>
		public DSFormatter Formatter
		{
		 	get
		 	{
				return mFormatter;
		 	}
		 	set
		 	{
				mFormatter = value;
		 	}
		}
		
		/// <summary>
		/// Name of the column
		/// </summary>
		public string ColumnName { get; set; }
		
		/// <summary>
		/// Data type of the column
		/// </summary>
		public Type DataType { get; set; }
		
		/// <summary>
		/// Caption to be used in the column header
		/// </summary>
		public string Caption { get; set; }
		
		/// <summary>
		/// Allow the user to resize the column
		/// </summary>
		private bool AllowResize { get; set; }
		
		/// <summary>
		/// Allow the user to sort this column
		/// </summary>
		public bool AllowSort { get; set; }
		
		/// <summary>
		/// Allow the user to reorder this column
		/// </summary>
		private bool AllowReorder { get; set; }
		
		/// <summary>
		/// Column is read only
		/// </summary>
		public bool ReadOnly { get; set; }
		
		/// <summary>
		/// The display width of the column
		/// </summary>
		public float Width = -1;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is the column sorting is done on
		/// </summary>
		/// <value><c>true</c> if this instance is sort column; otherwise, <c>false</c>.</value>
		public Boolean IsSortColumn { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Data.DSDataColumn"/> should use ascending sort.
		/// </summary>
		/// <value><c>true</c> if use ascending sort; otherwise, <c>false</c>.</value>
		public Boolean UseDescendingSort { get; set; }

		#endregion
		

		#region Contructors
		/// <summary>
		/// Constructor with a column name.
		/// The caption (header) will be the same.
		/// Defaults to "String" type
		/// </summary>
		/// <param name="columnName">Name of the column</param>
		public DSDataColumn(string columnName)
		{
			this.ColumnName = columnName;
			// Default values
			this.Caption = columnName; // By default it will be the same unless we change it
			this.AllowResize = true;
			this.AllowSort = true;
			this.AllowReorder = true;
			this.ReadOnly = false;
			this.DataType = typeof(String);
			this.IsSortColumn = false;
			this.UseDescendingSort = false;
		}
		
		/// <summary>
		/// Constructor with a column name and a type.
		/// The caption (header) will be the same as the column name.
		/// </summary>
		/// <param name="columnName">Name of the column</param>
		/// <param name="columnType">Type of the column</param>
		public DSDataColumn(string columnName, Type columnType) : this(columnName)
		{
			this.DataType = columnType;
		}

		#endregion

		#region Methods
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="DSoft.Datatypes.Grid.Data.DSDataColumn"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="DSoft.Datatypes.Grid.Data.DSDataColumn"/>.</returns>
		public override string ToString ()
		{
			return string.Format ("[DSDataColumn: ColumnName={0}, DataType={1}, Caption={2}, AllowResize={3}, AllowSort={4}, AllowReorder={5}, ReadOnly={6}, IsSortColumn={7}, UseDescendingSort={8}]", ColumnName, DataType, Caption, AllowResize, AllowSort, AllowReorder, ReadOnly, IsSortColumn, UseDescendingSort);
		}

		#endregion

	}
}

