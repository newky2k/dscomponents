// ****************************************************************************
// <copyright file="DSToolbarItem.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

namespace DSoft.Datatypes.UI
{
	/// <summary>
	/// Toolbar item
	/// </summary>
	public class DSToolbarItem
	{
		/// <summary>
		/// Gets or sets the content of the button e.g. Text or Image
		/// </summary>
		/// <value>The content.</value>
		public object Content { get; set;}
		/// <summary>
		/// Gets or sets the click command.
		/// </summary>
		/// <value>The click command.</value>
		public Action ClickCommand { get; set;}
	}
}

