// ****************************************************************************
// <copyright file="DSMoreContentMenuItem.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

namespace DSoft.Datatypes.UI
{
	/// <summary>
	/// More menu item with a view to be displayed as a content
	/// </summary>
	public class DSMoreContentMenuItem : DSMoreMenuItem
	{
		#region Fields
		private object mContent;
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets the type of the content.
		/// </summary>
		/// <value>The type of the content.</value>
		public Type ContentType
		{
			get
			{
				return this.mContent.GetType();
			}
		}
		
		/// <summary>
		/// Gets or sets the boxed content object
		/// </summary>
		/// <value>The content.</value>
		public object Content
		{
			get 
		    { 
		        return this.mContent;
		    }
		    
			set 
		    { 
		        this.mContent = value;
		        
		        PropertyDidChange("Content");
		    }
		}
		#endregion
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.UI.DSMoreContentMenuItem"/> class.
		/// </summary>
		public DSMoreContentMenuItem () : base()
		{
			this.ItemType = DSoft.Datatypes.Enums.MoreMenuItemType.View;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.UI.DSMoreContentMenuItem"/> class.
		/// </summary>
		/// <param name="Content">Content.</param>
		public DSMoreContentMenuItem (object Content) : this()
		{
			this.Content = Content;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.UI.DSMoreContentMenuItem"/> class.
		/// </summary>
		/// <param name="Name">Name.</param>
		/// <param name="Content">Content.</param>
		public DSMoreContentMenuItem (String Name, object Content) : this(Content)
		{
			this.Title = Name;
		}
		#endregion
	}
	
	/// <summary>
	/// More menu item with a view to be displayed as a generic type
	/// </summary>
	public class DSMoreContentMenuItem<T> : DSMoreContentMenuItem
	{
		#region Properties
				
		/// <summary>
		/// Gets or sets the content as the Generic Type
		/// </summary>
		/// <value>The content of the typed.</value>
		public T TypedContent
		{
			get 
		    { 
		        return (T)this.Content;
		    }
		    
			set 
		    { 
		        this.Content = value;
		    }
		}
		#endregion
		#region Constructor
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.UI.DSMoreContentMenuItem"/> class.
		/// </summary>
		/// <param name="Content">Content.</param>
		public DSMoreContentMenuItem (T Content) : base(Content)
		{
			
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.UI.DSMoreContentMenuItem"/> class.
		/// </summary>
		/// <param name="Name">Name.</param>
		/// <param name="Content">Content.</param>
		public DSMoreContentMenuItem (String Name,T Content) : base(Name,Content)
		{
			
		}
		#endregion
	}
}

