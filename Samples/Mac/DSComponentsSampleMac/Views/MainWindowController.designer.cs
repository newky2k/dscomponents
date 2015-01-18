// ****************************************************************************
// <copyright file="MainWindowController.designer.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using Foundation;
using System.CodeDom.Compiler;

namespace DSComponentsSampleMac
{
	[Register ("MainWindowController")]
	partial class MainWindowController
	{
		[Outlet]
		DSComponentsSampleMac.Views.LocalDSGridView grdView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (grdView != null) {
				grdView.Dispose ();
				grdView = null;
			}
		}
	}
}
