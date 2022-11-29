// ****************************************************************************
// <copyright file="DSGridRowViewCollection.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.ObjectModel;

namespace DSoft.UI.Mac.Grid.Views.Collections
{
	internal class DSGridRowViewCollection : Collection<DSGridRowView>, IDisposable
	{

		#region IDisposable implementation

		/// <summary>
		/// Releases all resource used by the <see cref="DSoft.UI.Grid.Views.Collections.DSGridRowViewCollection"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the
		/// <see cref="DSoft.UI.Grid.Views.Collections.DSGridRowViewCollection"/>. The <see cref="Dispose"/> method leaves the
		/// <see cref="DSoft.UI.Grid.Views.Collections.DSGridRowViewCollection"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the
		/// <see cref="DSoft.UI.Grid.Views.Collections.DSGridRowViewCollection"/> so the garbage collector can reclaim the
		/// memory that the <see cref="DSoft.UI.Grid.Views.Collections.DSGridRowViewCollection"/> was occupying.</remarks>
		public void Dispose ()
		{
			foreach (var item in Items)
			{
				item.TearDown ();
				item.RemoveFromSuperview ();
			}

			this.Items.Clear ();
		}

		#endregion

		/// <summary>
		/// Returns the view with the row index
		/// </summary>
		/// <returns>The for row index.</returns>
		/// <param name="RowIndex">Row index.</param>
		public DSGridRowView ViewForRowIndex (int RowIndex)
		{
			foreach (var item in Items)
			{
				if (item.RealRowIndex == RowIndex)
				{
					return item;
				}
			}

			return null;
		}
	}
}

