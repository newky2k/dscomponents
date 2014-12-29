using System;

#if __UNIFIED__
using UIKit;
#else
using MonoTouch.UIKit;
#endif


public static class iOSHelper
{
	/// <summary>
	/// Gets a value indicating if current OS is iOS7 or greater
	/// </summary>
	/// <value><c>true</c> if isi O s7; otherwise, <c>false</c>.</value>
	public static bool IsiOS7
	{
		get
		{
			return (new Version(UIDevice.CurrentDevice.SystemVersion) > new Version(6,2));
		}
	}
}

