// ****************************************************************************
// <copyright file="DSMoreMenuItemCollection.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.ObjectModel;
using DSoft.Datatypes.Enums;

namespace DSoft.Datatypes.UI.Collections
{
	/// <summary>
	/// Collection of DSMoreMenuItems
	/// </summary>
	public class DSMoreMenuItemCollection : Collection<DSMoreMenuItem>
	{
		/// <summary>
		/// Gets or sets the menu title.
		/// </summary>
		/// <value>The title.</value>
		public String Title { get; set;}
		
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.UI.Collections.DSMoreMenuItemCollection"/> class.
		/// </summary>
		public DSMoreMenuItemCollection () : base()
		{
			
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.UI.Collections.DSMoreMenuItemCollection"/> class.
		/// </summary>
		/// <param name="Name">Name.</param>
		public DSMoreMenuItemCollection (String Name) : this()
		{
			this.Title = Name;
		}
		
		#endregion
		/// <summary>
		/// Counts the number of items that match the 
		/// </summary>
		/// <returns>The items.</returns>
		/// <param name="Type">Type.</param>
		public int CountItems(MoreMenuItemType Type)
		{
			var aCount = 0;
			
			foreach (var item in this.Items)
			{
				if (item.ItemType == Type)
				{
					aCount++;
				}
			}
			
			return aCount;
		}

		/// <summary>
		/// Finds the first instance of the Type
		/// </summary>
		/// <returns>The first.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public DSMoreMenuItem FindFirst<T>()
		{
			foreach (var item in this.Items) 
			{
				if (item is T) 
				{
					return item;
				}

			}

			return null;
		}

		
	}
}

