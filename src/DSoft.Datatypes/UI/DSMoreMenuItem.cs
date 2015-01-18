// ****************************************************************************
// <copyright file="DSMoreMenuItem.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.ComponentModel;
using DSoft.Datatypes.Base;
using DSoft.Datatypes.Enums;

namespace DSoft.Datatypes.UI
{
	/// <summary>
	/// Menu item class, defaults to type of blank
	/// </summary>
	public class DSMoreMenuItem : ObservableClass
	{
		#region Fields
		private MoreMenuItemType mItemType;
		private String mTitle;
		private bool mHideMenuOnTap = true;
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets or sets the menu item type of the item.
		/// </summary>
		/// <value>The type of the item.</value>
		public MoreMenuItemType ItemType
		{
			get 
		    { 
		        return this.mItemType;
		    }
		    
			set 
		    { 
		        this.mItemType = value;
		        
		        PropertyDidChange("ItemType");
		    }
		}
		
		/// <summary>
		/// Gets or sets the title of the menu item
		/// </summary>
		/// <value>The title.</value>
		public String Title
		{
			get 
		    { 
		        return this.mTitle;
		    }
		    
			set 
		    { 
		        this.mTitle = value;
		        
		        PropertyDidChange("Title");
		    }
		}
		
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="DSoft.Datatypes.UI.DSMoreMenuItem"/> hide menu on tap.
		/// </summary>
		/// <value><c>true</c> if hide menu on tap; otherwise, <c>false</c>.</value>
		public bool HideMenuOnTap
		{
			get
			{
				return mHideMenuOnTap;
			}
			set
			{
				mHideMenuOnTap = value;
				
				PropertyDidChange("HideMenuOnTap");
			}
		}
		#endregion
		
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.UI.DSMoreMenuItem"/> class.
		/// </summary>
		public DSMoreMenuItem ()
		{
			this.ItemType = MoreMenuItemType.Blank;
			this.Title = String.Empty;
		}
		
		#endregion
		

	}
}

