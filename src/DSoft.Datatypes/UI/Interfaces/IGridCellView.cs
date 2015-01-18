// ****************************************************************************
// <copyright file="IGridCellView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Types;

namespace DSoft.Datatypes.UI.Interfaces
{
	/// <summary>
	/// Inteface for custom view in DSGridView
	/// </summary>
	public interface IDSCustomView
	{
		/// <summary>
		/// Gets or sets the view frame/rectangle
		/// </summary>
		/// <value>The view frame.</value>
		DSRectangle ViewFrame { get; set; }

		/// <summary>
		/// Gets or sets the value for the view
		/// </summary>
		/// <value>The value.</value>
		object Value { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the view is in readonly mode
		/// </summary>
		/// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
		bool IsReadOnly { get; set; }

		/// <summary>
		/// Gets or sets the update action.
		/// </summary>
		/// <value>The update action.</value>
		Action<object> UpdateAction { get; set; }
	}
}

