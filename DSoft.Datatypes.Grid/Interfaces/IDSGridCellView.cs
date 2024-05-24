// ****************************************************************************
// <copyright file="IDSGridCellView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Grid.Shared;

namespace DSoft.Datatypes.Grid.Interfaces
{
	/// <summary>
	/// Interface for grid cell view.
	/// </summary>
	public interface IDSGridCellView
	{
		/// <summary>
		/// Gets the processor for the cell view
		/// </summary>
		/// <value>The processor.</value>
		DSCellProcessor Processor {get;}

		/// <summary>
		/// Tears down the view
		/// </summary>
		void TearDown();

		/// <summary>
		/// Detaches the view for its parent
		/// </summary>
		void DetachView();
	}
}

