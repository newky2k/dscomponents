// ****************************************************************************
// <copyright file="DSViewFormatter.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Base;
using DSoft.Datatypes.UI.Interfaces;
using DSoft.Datatypes.Types;

namespace DSoft.Datatypes.Formatters
{
	/// <summary>
	/// Formatter with custom view class
	/// </summary>
	public class DSViewFormatter : DSFormatter
	{
		private IDSCustomView mView; 
		/// <summary>
		/// Gets the view.
		/// </summary>
		/// <value>The view.</value>
		public virtual IDSCustomView View { get { return mView; } }

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Formatters.DSViewFormatter"/> class.
		/// </summary>
		public DSViewFormatter()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Formatters.DSViewFormatter"/> class.
		/// </summary>
		/// <param name="">.</param>
		public DSViewFormatter(IDSCustomView custView)
		{
			mView = custView;
		}
		#endregion
	}

	/// <summary>
	/// Formatter with generic type for view
	/// </summary>
	public class DSViewFormatter<T> : DSViewFormatter where T : IDSCustomView, new()
	{
		/// <summary>
		/// Gets the view.
		/// </summary>
		/// <value>The view.</value>
		public override IDSCustomView View {
			get
			{
				return new T ();
			}
		}
	}
}

