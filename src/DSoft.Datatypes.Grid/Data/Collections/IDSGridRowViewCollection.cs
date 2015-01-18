// ****************************************************************************
// <copyright file="IDSGridRowViewCollection.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.ObjectModel;
using DSoft.Datatypes.Grid.Interfaces;

namespace DSoft.Datatypes.Grid.Data.Collections
{
	/// <summary>
	/// Collection of IDSGridRowView objects
	/// </summary>
	public class IDSGridRowViewCollection : Collection<IDSGridRowView>
	{
		#region IDisposable implementation

		/// <summary>
		/// Releases all resource used by the <see cref="DSoft.Datatypes.Grid.Data.Collections.IDSGridRowViewCollection"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the
		/// <see cref="DSoft.Datatypes.Grid.Data.Collections.IDSGridRowViewCollection"/>. The <see cref="Dispose"/> method
		/// leaves the <see cref="DSoft.Datatypes.Grid.Data.Collections.IDSGridRowViewCollection"/> in an unusable state.
		/// After calling <see cref="Dispose"/>, you must release all references to the
		/// <see cref="DSoft.Datatypes.Grid.Data.Collections.IDSGridRowViewCollection"/> so the garbage collector can reclaim
		/// the memory that the <see cref="DSoft.Datatypes.Grid.Data.Collections.IDSGridRowViewCollection"/> was occupying.</remarks>
		public void Dispose ()
		{
			foreach (var item in Items)
			{
				item.TearDown ();
				item.DetachView();
			}

			this.Items.Clear ();
		}

		#endregion

		/// <summary>
		/// Returns the view with the row index
		/// </summary>
		/// <returns>The for row index.</returns>
		/// <param name="RowIndex">Row index.</param>
		public IDSGridRowView ViewForRowIndex (int RowIndex)
		{
			foreach (var item in Items)
			{
				if (item.Processor.RealRowIndex == RowIndex)
				{
					return item;
				}
			}

			return null;
		}
	}
}

