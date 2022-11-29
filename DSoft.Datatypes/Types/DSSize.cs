// ****************************************************************************
// <copyright file="DSSize.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

namespace DSoft.Datatypes.Types
{
	/// <summary>
	/// Cross platform size class
	/// </summary>
	public class DSSize
	{
		/// <summary>
		/// Gets or sets the width
		/// </summary>
		/// <value>The width.</value>
		public float Width { get; set;}
		
		/// <summary>
		/// Gets or sets the hieght.
		/// </summary>
		/// <value>The hieght.</value>
		public float Height { get; set;}
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSSize"/> class.
		/// </summary>
		public DSSize ()
		{
			
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSSize"/> class.
		/// </summary>
		/// <param name="Width">Width.</param>
		/// <param name="Height">Height.</param>
		public DSSize (float Width, float Height)
		{
			this.Width = Width;
			this.Height = Height;
		}
		
		#endregion
		 
	}
}

