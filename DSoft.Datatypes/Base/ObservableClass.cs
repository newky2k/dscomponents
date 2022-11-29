// ****************************************************************************
// <copyright file="ObservableClass.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.ComponentModel;

namespace DSoft.Datatypes.Base
{
	/// <summary>
	/// Observable class with notification methods
	/// </summary>
	public class ObservableClass : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation

		/// <summary>
		/// Occurs when property changed.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged = delegate {};

		#endregion
		
		/// <summary>
		/// Property has changed
		/// </summary>
		/// <param name="Name">Name.</param>
		protected void PropertyDidChange(String Name)
		{
			PropertyChanged (this, new PropertyChangedEventArgs (Name));
		}
	}
}

