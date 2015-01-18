// ****************************************************************************
// <copyright file="IDSGridRowView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Grid.Shared;

namespace DSoft.Datatypes.Grid.Interfaces
{
	/// <summary>
	/// Cross-platform Interface for the DSGridRowView
	/// </summary>
	public interface IDSGridRowView
	{
		/// <summary>
		/// Gets the processor.
		/// </summary>
		/// <value>The processor.</value>
		DSRowProcessor Processor {get;}

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

