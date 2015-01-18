// ****************************************************************************
// <copyright file="DSInset.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

namespace DSoft.Datatypes.Types
{
	/// <summary>
	/// Inset class for margins and padding
	/// </summary>
	public class DSInset
	{

		#region Properties

		/// <summary>
		/// Gets or sets the top.
		/// </summary>
		/// <value>The top.</value>
		public float Top {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the bottom.
		/// </summary>
		/// <value>The bottom.</value>
		public float Bottom {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the left.
		/// </summary>
		/// <value>The left.</value>
		public float Left {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the right.
		/// </summary>
		/// <value>The right.</value>
		public float Right {
			get;
			set;
		}

		#endregion

		#region Fields

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSInset"/> class.
		/// </summary>
		public DSInset ()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSInset"/> class.
		/// </summary>
		/// <param name="Size">Set Uniform size for all side</param>
		public DSInset (float Size)
		{
			this.Left = Size;
			this.Right = Size;
			this.Top = Size;
			this.Bottom = Size;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSInset"/> class.
		/// </summary>
		/// <param name="Left">Left.</param>
		/// <param name="Top">Top.</param>
		/// <param name="Right">Right.</param>
		/// <param name="Bottom">Bottom.</param>
		public DSInset (float Left, float Top, float Right, float Bottom)
		{
			this.Left = Left;
			this.Right = Right;
			this.Top = Top;
			this.Bottom = Bottom;
		}

		#endregion

	}
}

