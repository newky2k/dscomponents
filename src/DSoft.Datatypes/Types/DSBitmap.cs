// ****************************************************************************
// <copyright file="DSBitmap.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.IO;

namespace DSoft.Datatypes.Types
{
	/// <summary>
	/// Bitmap class
	/// </summary>
	public class DSBitmap
	{
		#region Properties
		/// <summary>
		/// Gets or sets the image data.
		/// </summary>
		/// <value>The image data.</value>
		public Byte[] ImageData { get; set; }
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSBitmap"/> class.
		/// </summary>
		public DSBitmap ()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSBitmap"/> class.
		/// </summary>
		/// <param name="Data">Data.</param>
		public DSBitmap (Byte[] Data)
		{
			ImageData = Data;
		}

		#endregion
	}
}

