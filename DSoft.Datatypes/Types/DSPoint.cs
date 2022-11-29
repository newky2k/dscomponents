// ****************************************************************************
// <copyright file="DSPoint.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

namespace DSoft.Datatypes.Types
{
	/// <summary>
	/// Point definition class
	/// </summary>
	public class DSPoint
	{
		#region Fields

		private float x;
		private float y;

		#endregion
		//
		// Static Fields
		//

		/// <summary>
		/// Gets the empty.
		/// </summary>
		/// <value>The empty.</value>
		public static DSPoint Empty
		{
			get
			{
				return new DSPoint (0f, 0f);
			}
		}

		//
		// Properties
		//

		/// <summary>
		/// Gets a value indicating whether this instance is empty.
		/// </summary>
		/// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
		public bool IsEmpty
		{
			get
			{
				return (double)this.X == 0.0 && (double)this.Y == 0.0;
			}
		}

		/// <summary>
		/// Gets or sets the X
		/// </summary>
		/// <value>The x.</value>
		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		/// <summary>
		/// Gets or sets the Y
		/// </summary>
		/// <value>The y.</value>
		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSPoint"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public DSPoint (float x, float y)
		{
			this.X = x;
			this.Y = y;
		}
	}
}

