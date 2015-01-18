// ****************************************************************************
// <copyright file="DSMoreSegmentMenuItem.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

namespace DSoft.Datatypes.UI
{
	/// <summary>
	/// Segement or Section title menu item
	/// </summary>
	public class DSMoreSegmentMenuItem : DSMoreMenuItem
	{
		#region Fields
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.UI.DSMoreSegmentMenuItem"/> class.
		/// </summary>
		public DSMoreSegmentMenuItem ()
		{
			this.ItemType = DSoft.Datatypes.Enums.MoreMenuItemType.Segment;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.UI.DSMoreSegmentMenuItem"/> class.
		/// </summary>
		/// <param name="Name">Name.</param>
		public DSMoreSegmentMenuItem (String Name) : this()
		{
			this.Title = Name;
		}
		
		#endregion
	}
}

