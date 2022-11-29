// ****************************************************************************
// <copyright file="DSGridViewCellInfo.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Base;
using DSoft.Datatypes.Types;

namespace DSoft.Datatypes.Grid.MetaData
{
	/// <summary>
	/// Meta data about a DSGridViewCell object
	/// </summary>
	public class DSGridViewCellInfo : IDisposable
	{
		#region Properties

		/// <summary>
		/// The X(Column) index of the grow
		/// </summary>
		public int xPosition;

		/// <summary>
		/// The frame to draw the column
		/// </summary>
		/// <value>The frame.</value>
		public DSRectangle Frame {
			get
			{
				return new DSRectangle (x, y, width, height);
			}
		}

		/// <summary>
		/// Gets or sets the x.
		/// </summary>
		/// <value>The x.</value>
		public float x {get; set;}

		/// <summary>
		/// Gets or sets the y.
		/// </summary>
		/// <value>The y.</value>
		public float y {get; set;}

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width.</value>
		public float width {get; set;}

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height.</value>
		public float height {get; set;}

		/// <summary>
		/// Style of the sort indicator
		/// </summary>
		public SortIndicatorStyle SortStyle;
		/// <summary>
		/// Formatter to apply to the cell
		/// </summary>
		public DSFormatter Formatter;

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public String Name { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is read only.
		/// </summary>
		/// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
		public bool IsReadOnly { get; set; }

		#endregion

		#region Constuctors

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Grid.MetaData.DSGridViewCellInfo"/> class.
		/// </summary>
		public DSGridViewCellInfo ()
		{
			SortStyle = SortIndicatorStyle.None;
		}

		#endregion

		#region IDisposable implementation
		/// <summary>
		/// Releases all resource used by the <see cref="DSoft.Datatypes.Grid.MetaData.DSGridViewCellInfo"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the
		/// <see cref="DSoft.Datatypes.Grid.MetaData.DSGridViewCellInfo"/>. The <see cref="Dispose"/> method leaves the
		/// <see cref="DSoft.Datatypes.Grid.MetaData.DSGridViewCellInfo"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the
		/// <see cref="DSoft.Datatypes.Grid.MetaData.DSGridViewCellInfo"/> so the garbage collector can reclaim the memory
		/// that the <see cref="DSoft.Datatypes.Grid.MetaData.DSGridViewCellInfo"/> was occupying.</remarks>
		public void Dispose ()
		{
			Formatter = null;
		}

		#endregion
	}
}

