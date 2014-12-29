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

