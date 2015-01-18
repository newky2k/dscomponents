// ****************************************************************************
// <copyright file="DSMoreButtonMenuItem.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

namespace DSoft.Datatypes.UI
{
	/// <summary>
	/// More item to treat as a button
	/// </summary>
	public class DSMoreButtonMenuItem : DSMoreMenuItem
	{
		#region Fields
		private Action mCommand;
		#endregion
		
		#region Properties
		/// <summary>
		/// Gets or sets the command to be called when the item is tapped
		/// </summary>
		/// <value>The command.</value>
		public Action Command
		{
			get 
		    { 
		        return this.mCommand;
		    }
		    
			set 
		    { 
		        this.mCommand = value;
		        
		        PropertyDidChange("Command");
		    }
		}
		#endregion
		
		#region Constuctors
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.UI.DSMoreButtonMenuItem"/> class.
		/// </summary>
		public DSMoreButtonMenuItem() : base()
		{
			this.ItemType = DSoft.Datatypes.Enums.MoreMenuItemType.Action;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.UI.DSMoreButtonMenuItem"/> class.
		/// </summary>
		/// <param name="Title">Title.</param>
		public DSMoreButtonMenuItem(String Title) : this()
		{
			this.Title = Title;
		}
		#endregion	
	}
}

