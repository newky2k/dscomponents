// ****************************************************************************
// <copyright file="DisposableList.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;

namespace DSoft.Datatypes
{
	/// <summary>
	/// A disposable list class for Disposable objects
	/// </summary>
	public class DisposableList<T> : List<T>, IDisposable where T : IDisposable
	{
		#region IDisposable implementation

		/// <summary>
		/// Releases all resource used by the object
		/// </summary>
		public void Dispose ()
		{
			foreach (var item in this) 
			{
				item.Dispose ();
			}

			this.Clear ();
		}

		#endregion



	}
}

