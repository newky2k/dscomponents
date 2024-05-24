// ****************************************************************************
// <copyright file="DSToolbarDefaultTheme.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Types;

namespace DSoft.Themes.Toolbar
{
	/// <summary>
	/// Default Toolbar theme
	/// </summary>
	public class DSToolbarDefaultTheme : DSToolbarTheme
	{
		#region Fields
		private DSColor mColor;
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public override DSColor Color
		{
			get 
		    { 
				if (mColor == null) 
				{
					mColor = new DSColor ("#3B5998");

				}
		        return this.mColor;
		    }
		    
			set 
		    { 
		        this.mColor = value;
		    }
		}
		
		#endregion
	}
}

