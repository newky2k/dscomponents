// ****************************************************************************
// <copyright file="GenericsHelper.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;

namespace DSoft.Datatypes.Helpers
{
	/// <summary>
	/// Helper class for generic types
	/// </summary>
	public static class GenericsHelper
	{
		/// <summary>
		/// Instatiate the type with the specified Params.
		/// </summary>
		/// <param name="Params">Parameters.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Instatiate<T> (object[] Params)
		{
			Type constructedType = typeof(T);

			if (Params == null)
			{
				Params = new object[]{ };
			}

			var obj = (T)Activator.CreateInstance (constructedType, Params);

			return (obj == null) ? default(T) : obj;

		}
	}
}

