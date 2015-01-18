// ****************************************************************************
// <copyright file="Delegates.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Grid.Data;

namespace DSoft.Datatypes.Grid
{
	/// <summary>
	/// Row selection changed handler.
	/// </summary>
	public delegate void RowSelectionChangedHandler (int RowIndex);
	/// <summary>
	/// Row selected handler delegate.  Sender will be of type DSGridRowView
	/// </summary>
	public delegate void RowSelectedHandlerDelegate (object sender);
	/// <summary>
	/// Cell tapped handler delegate. Sender will be of type DSGridCellView
	/// </summary>
	public delegate void CellTappedHandlerDelegate (object sender);
	/// <summary>
	/// Row selected delegate. Sender will be of type DSGridRowView
	/// </summary>
	public delegate void RowSelectedDelegate (object sender, int RowIndex);
}

