// ****************************************************************************
// <copyright file="LocalDSGridView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using Foundation;
using DSoft.UI.Mac.Grid;
using AppKit;

namespace DSComponentsSampleMac.Views
{
	[Register("LocalDSGridView")]
	public class LocalDSGridView : DSGridView
	{
		public LocalDSGridView(IntPtr ptr) 
			: base(ptr)
		{
		
		}
	}
}

