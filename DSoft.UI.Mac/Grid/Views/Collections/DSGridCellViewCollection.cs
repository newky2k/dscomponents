// ****************************************************************************
// <copyright file="DSGridCellViewCollection.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.ObjectModel;

namespace DSoft.UI.Mac.Grid.Views.Collections
{
	/// <summary>
	/// Collection of DSGridCellView
	/// </summary>
	public class DSGridCellViewCollection : Collection<DSGridCellView>, IDisposable
	{
		/// <summary>
		/// Gets the <see cref="DSoft.UI.Grid.Views.Collections.DSGridCellViewCollection"/> with the specified ColumnName.
		/// </summary>
		/// <param name="ColumnName">Column name.</param>
		public DSGridCellView this [string ColumnName]
		{
			get
			{
				foreach (var item in Items)
				{
					if (item.ColumnName.ToLower().Equals(ColumnName.ToLower()))
					{
						return item;
					}
				}
				return null;
			}
		}

		/// <summary>
		/// Gets the <see cref="DSoft.UI.Grid.Views.Collections.DSGridCellViewCollection"/> with the specified Index.
		/// </summary>
		/// <param name="Index">Index.</param>
		public new DSGridCellView this [int Index]
		{
			get
			{
				foreach (var item in Items)
				{
					if (item.ColumnIndex == Index)
					{
						return item;
					}
				}

				return null;
			}
		}


		#region IDisposable implementation
		public void Dispose ()
		{
			foreach (var item in this.Items) 
			{
				item.TearDown ();
				item.RemoveFromSuperview ();
			}

			this.Items.Clear ();
		}
		#endregion
	}
}

