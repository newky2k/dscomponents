// ****************************************************************************
// <copyright file="DSDSRectangle.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

namespace DSoft.Datatypes.Types
{
	/// <summary>
	/// Rectangle class
	/// </summary>
	public class DSRectangle
	{
		#region Static Fields

		/// <summary>
		/// Gets an Empty rectangle
		/// </summary>
		/// <value>The empty.</value>
		public static DSRectangle Empty {
			get
			{
				return new DSRectangle (0f, 0f, 0f, 0f);
			}
		}

		#endregion

		#region Fields

		private float x;
		private float y;
		private float width;
		private float height;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the bottom.
		/// </summary>
		/// <value>The bottom.</value>
		public float Bottom {
			get
			{
				return this.Y + this.Height;
			}
		}

		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height.</value>
		public float Height {
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is empty.
		/// </summary>
		/// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
		public bool IsEmpty {
			get
			{
				return this.Width <= 0f || this.Height <= 0f;
			}
		}

		/// <summary>
		/// Gets the left.
		/// </summary>
		/// <value>The left.</value>
		public float Left {
			get
			{
				return this.X;
			}
		}

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>The location.</value>
		public DSPoint Location {
			get
			{
				return new DSPoint (this.X, this.Y);
			}
			set
			{
				this.X = value.X;
				this.Y = value.Y;
			}
		}

		/// <summary>
		/// Gets the right.
		/// </summary>
		/// <value>The right.</value>
		public float Right {
			get
			{
				return this.X + this.Width;
			}
		}

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>The size.</value>
		public DSSize Size {
			get
			{
				return new DSSize (this.Width, this.Height);
			}
			set
			{
				this.Width = value.Width;
				this.Height = value.Height;
			}
		}

		/// <summary>
		/// Gets the top.
		/// </summary>
		/// <value>The top.</value>
		public float Top {
			get
			{
				return this.Y;
			}
		}

		/// <summary>
		/// Gets or sets the width.
		/// </summary>
		/// <value>The width.</value>
		public float Width {
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		/// <summary>
		/// Gets or sets the x.
		/// </summary>
		/// <value>The x.</value>
		public float X {
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
		/// Gets or sets the y.
		/// </summary>
		/// <value>The y.</value>
		public float Y {
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSRectangle"/> class.
		/// </summary>
		/// <param name="location">Location.</param>
		/// <param name="size">Size.</param>
		public DSRectangle (DSPoint location, DSSize size)
		{
			this.X = location.X;
			this.Y = location.Y;
			this.Width = size.Width;
			this.Height = size.Height;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Types.DSRectangle"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public DSRectangle (float x, float y, float width, float height)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		#endregion

		#region Methods

		/// <summary>
		/// From the Left,Top,Right and back
		/// </summary>
		/// <returns>The LTR.</returns>
		/// <param name="left">Left.</param>
		/// <param name="top">Top.</param>
		/// <param name="right">Right.</param>
		/// <param name="bottom">Bottom.</param>
		public static DSRectangle FromLTRB (float left, float top, float right, float bottom)
		{
			return new DSRectangle (left, top, right - left, bottom - top);
		}

		/// <summary>
		/// Applies the margin.
		/// </summary>
		/// <param name="Inset">Inset.</param>
		public void ApplyMargin (DSInset Inset)
		{
			x += Inset.Left;
			y += Inset.Top;
			height -= (Inset.Top + Inset.Bottom);
			width -= (Inset.Left + Inset.Right);
		}

		/// <summary>
		/// Centers the in rectangle.
		/// </summary>
		/// <param name="Target">Target.</param>
		public void CenterInRectangle (DSRectangle Target)
		{
			x = (Target.Size.Width / 2) - (this.Width / 2);
			y = (Target.Size.Height / 2) - (this.Height / 2);

		}

		#endregion
	}
}

