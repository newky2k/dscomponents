// ****************************************************************************
// <copyright file="DSDataValue.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.ComponentModel;

namespace DSoft.Datatypes.Grid.Data
{

	/// <summary>
	/// Stores the values for columns in a row
	/// </summary>
	public class DSDataValue : INotifyPropertyChanged
	{
		#region Fields

		private object mValue;

		#endregion
	
		#region Properties
		/// <summary>
		/// Gets or sets the name of the column.
		/// </summary>
		/// <value>The name of the column.</value>
		public String ColumnName { get; set;}
		
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>The value.</value>
		public object Value 
		{
			get
			{
				return mValue;
			}
			set
			{
				if (mValue == null)
				{
					mValue = value;
				}
				else if (!mValue.Equals(value))
				{
					OnPropertyChanged("Value");

					mValue = value;
				}
			}
		}
		#endregion

		#region INotifyPropertyChanged implementation

		/// <summary>
		/// Occurs when property changed.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged = delegate {};

		private void OnPropertyChanged(String Name)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(Name));
		}
		#endregion
	}
}

