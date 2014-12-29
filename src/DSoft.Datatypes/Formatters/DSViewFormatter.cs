using System;
using DSoft.Datatypes.Base;
using DSoft.Datatypes.UI.Interfaces;
using DSoft.Datatypes.Types;

namespace DSoft.Datatypes.Formatters
{
	/// <summary>
	/// Formatter with custom view class
	/// </summary>
	public abstract class DSViewFormatter : DSFormatter
	{
		/// <summary>
		/// Gets the view.
		/// </summary>
		/// <value>The view.</value>
		public virtual IDSCustomView View { get { return null; } }
	}

	/// <summary>
	/// Formatter with generic type for view
	/// </summary>
	public class DSViewFormatter<T> : DSViewFormatter where T : IDSCustomView, new()
	{
		/// <summary>
		/// Gets the view.
		/// </summary>
		/// <value>The view.</value>
		public override IDSCustomView View {
			get
			{
				return new T ();
			}
		}
	}
}

