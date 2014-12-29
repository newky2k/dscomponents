using System;
using DSoft.Datatypes.Types;

namespace DSoft.Datatypes.Base
{
	/// <summary>
	/// Base formatter class for formatting the Cells
	/// </summary>
	public abstract class DSFormatter
	{
		/// <summary>
		/// Gets or sets the size of the column
		/// </summary>
		/// <value>The size.</value>
		public virtual DSSize Size { get; set; }
	}
}

