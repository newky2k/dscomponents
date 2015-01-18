// ****************************************************************************
// <copyright file="DSImageFormatter.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Base;
using DSoft.Datatypes.Types;

namespace DSoft.Datatypes.Formatters
{
	/// <summary>
	/// A formatter class for determining the layout options for an image
	/// </summary>
	public class DSImageFormatter : DSFormatter
	{

		#region Fields

		private DSSize mSize;
		private DSInset mMargin;

		#endregion

		#region Properties

		/// <summary>
		/// Margin around the image
		/// </summary>
		/// <value>The margin.</value>
		public DSInset Margin {
			get { return mMargin; }
			set { mMargin = value; }
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Formatters.DSImageFormatter"/> class.
		/// </summary>
		/// <param name="ImageSize">Image size.</param>
		public DSImageFormatter (DSSize ImageSize)
		{
			mSize = ImageSize;
		}

		#endregion

		#region implemented abstract members of DSFormatter

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>The size.</value>
		public override DSoft.Datatypes.Types.DSSize Size {
			get
			{
				return mSize;
			}
			set
			{
				mSize = value;
			}
		}

		#endregion

	}
}

