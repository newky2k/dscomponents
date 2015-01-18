// ****************************************************************************
// <copyright file="IDSGridCellViewCollection.cs" company="DSoft Developments">
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
	/// Collection of IDSGridCellView objects
	/// </summary>
	public class IDSGridCellViewCollection : Collection<IDSGridCellView>
	{
		/// <summary>
		/// Gets the <see cref="DSoft.Datatypes.Grid.Data.Collections.IDSGridCellViewCollection"/> with the specified ColumnName.
		/// </summary>
		/// <param name="ColumnName">Column name.</param>
		public IDSGridCellView this [string ColumnName]
		{
			get
			{
				foreach (var item in Items)
				{
					if (item.Processor.ColumnName.ToLower().Equals(ColumnName.ToLower()))
					{
						return item;
					}
				}
				return null;
			}
		}

		/// <summary>
		/// Gets the <see cref="DSoft.Datatypes.Grid.Data.Collections.IDSGridCellViewCollection"/> with the specified Index.
		/// </summary>
		/// <param name="Index">Index.</param>
		public new IDSGridCellView this [int Index]
		{
			get
			{
				foreach (var item in Items)
				{
					if (item.Processor.ColumnIndex == Index)
					{
						return item;
					}
				}

				return null;
			}
		}


		#region IDisposable implementation
		/// <summary>
		/// Releases all resource used by the <see cref="DSoft.Datatypes.Grid.Data.Collections.IDSGridCellViewCollection"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the
		/// <see cref="DSoft.Datatypes.Grid.Data.Collections.IDSGridCellViewCollection"/>. The <see cref="Dispose"/> method
		/// leaves the <see cref="DSoft.Datatypes.Grid.Data.Collections.IDSGridCellViewCollection"/> in an unusable state.
		/// After calling <see cref="Dispose"/>, you must release all references to the
		/// <see cref="DSoft.Datatypes.Grid.Data.Collections.IDSGridCellViewCollection"/> so the garbage collector can reclaim
		/// the memory that the <see cref="DSoft.Datatypes.Grid.Data.Collections.IDSGridCellViewCollection"/> was occupying.</remarks>
		public void Dispose ()
		{
			foreach (var item in this.Items) 
			{
				item.TearDown ();
				item.DetachView ();
			}

			this.Items.Clear ();
		}

		#endregion
	}
}

