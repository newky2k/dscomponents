// ****************************************************************************
// <copyright file="ResourceHelper.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.IO;
using System.Reflection;

namespace DSoft.Themes
{
	internal class ResourceHelper
	{
		/// <summary>
		/// Loads the specified resource.
		/// </summary>
		/// <returns>The resource.</returns>
		/// <param name="Name">Name.</param>
		internal static MemoryStream LoadResource(String Name)
		{
			MemoryStream aMem = new MemoryStream();

			var assm = Assembly.GetExecutingAssembly ();

			var path = String.Format ("DSoft.Themes.Resources.{0}", Name);

			var aStream = assm.GetManifestResourceStream (path);

			aStream.CopyTo (aMem);

			return aMem;
		}
	}
}

