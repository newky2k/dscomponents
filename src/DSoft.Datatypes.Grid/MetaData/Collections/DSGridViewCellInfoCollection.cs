// ****************************************************************************
// <copyright file="DSGridViewCellInfoCollection.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.ObjectModel;

namespace DSoft.Datatypes.Grid.MetaData.Collections
{
	/// <summary>
	/// Collection of DSGridViewCellInfo objects
	/// </summary>
	public class DSGridViewCellInfoCollection : Collection<DSGridViewCellInfo>, IDisposable
	{

//		/// <summary>
//		/// Returns the cells for a row
//		/// </summary>
//		/// <returns>The for row.</returns>
//		/// <param name="Row">Row.</param>
//		internal DSGridViewCellInfoCollection CellsForRow(int Row)
//		{
//			var newData = new DSGridViewCellInfoCollection();
//			
//			foreach (var item in this)
//			{
//				if (item.yPosition == Row)
//				{
//					newData.Add(item);
//				}
//			}
//			
//			return newData;
//		}


		#region IDisposable implementation
		/// <summary>
		/// Releases all resource used by the
		/// <see cref="DSoft.Datatypes.Grid.MetaData.Collections.DSGridViewCellInfoCollection"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the
		/// <see cref="DSoft.Datatypes.Grid.MetaData.Collections.DSGridViewCellInfoCollection"/>. The <see cref="Dispose"/>
		/// method leaves the <see cref="DSoft.Datatypes.Grid.MetaData.Collections.DSGridViewCellInfoCollection"/> in an
		/// unusable state. After calling <see cref="Dispose"/>, you must release all references to the
		/// <see cref="DSoft.Datatypes.Grid.MetaData.Collections.DSGridViewCellInfoCollection"/> so the garbage collector can
		/// reclaim the memory that the <see cref="DSoft.Datatypes.Grid.MetaData.Collections.DSGridViewCellInfoCollection"/>
		/// was occupying.</remarks>
		public void Dispose ()
		{
			foreach (var item in this.Items) 
			{
				item.Dispose ();
			}

			this.Items.Clear ();
		}
		#endregion
	}
}

