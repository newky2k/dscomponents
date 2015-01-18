// ****************************************************************************
// <copyright file="DSTextFormatter.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Base;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Types;

namespace DSoft.Datatypes.Formatters
{
	/// <summary>
	/// Text formatter for DSDataColumn
	/// </summary>
	public class DSTextFormatter : DSFormatter
	{

		#region Properties

		/// <summary>
		/// Gets or sets the size of the column - Ignored in DSTextFormatter
		/// </summary>
		/// <value>The size.</value>
		public override DSSize Size {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the alignment.
		/// </summary>
		/// <value>The alignment.</value>
		public TextAlignment Alignment {
			get;
			set;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Formatters.DSTextFormatter"/> class.
		/// </summary>
		/// <param name="Alignment">Alignment.</param>
		public DSTextFormatter (TextAlignment Alignment)
		{
			this.Alignment = Alignment;
		}

		#endregion

	}
}

