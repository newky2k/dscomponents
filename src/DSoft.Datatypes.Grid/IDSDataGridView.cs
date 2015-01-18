// ****************************************************************************
// <copyright file="IDSDataGridView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Grid.Data;
using DSoft.Datatypes.Grid.Interfaces;
using DSoft.Datatypes.Grid.Shared;

namespace DSoft.Datatypes.Grid
{
	/// <summary>
	/// Cross platform IDSGridView platform for defining an shared API
	/// </summary>
	public interface IDSDataGridView
	{
		/// <summary>
		/// Gets or sets the processor.
		/// </summary>
		/// <value>The processor.</value>
		DSGridProcessor Processor {get;}

		/// <summary>
		/// Handles the on header cell tapped.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void HandleOnHeaderCellTapped (IDSGridCellView sender);

		/// <summary>
		/// Handles the on selected row changed.
		/// </summary>
		/// <param name="RowIndex">Row index.</param>
		void HandleOnSelectedRowChanged (int RowIndex);

		/// <summary>
		/// Handles the on cell single tap.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void HandleOnCellSingleTap (IDSGridCellView sender);

		/// <summary>
		/// Handles the on cell double tap.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void HandleOnCellDoubleTap (IDSGridCellView sender);

		/// <summary>
		/// Handles the on row double select.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void HandleOnRowDoubleSelect (IDSGridRowView sender);

		/// <summary>
		/// Handles the on row single select.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void HandleOnRowSingleSelect (IDSGridRowView sender);

	}
}

