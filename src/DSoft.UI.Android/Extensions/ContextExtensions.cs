// ****************************************************************************
// <copyright file="ContextExtensions.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using Android.Util;
using Android.Views;
using Android.Content;
using Android.Runtime;

/// <summary>
/// Global Extensions for the Android Context
/// </summary>
public static class ContextExtensions
{
	private static readonly DisplayMetrics displayMetrics = new DisplayMetrics ();
	
	/// <summary>
	/// Gets the window manager for the context
	/// </summary>
	/// <returns>The window manager.</returns>
	/// <param name="Owner">Owner.</param>
	public static IWindowManager GetWindowManager(this Context Owner)
	{
		var wm = Owner.GetSystemService (Context.WindowService).JavaCast<IWindowManager> ();
		
		return wm;
	}
	
	/// <summary>
	/// Converts the dp value to the pixels on the device, based on the display density
	/// </summary>
	/// <returns>The device pixels.</returns>
	/// <param name="ctx">Context.</param>
	/// <param name="dp">Dp.</param>
	public static int ToDevicePixels (this Context ctx, int dp)
	{
		return ctx.ToDevicePixels (dp, 0.5f);
	}
	
	/// <summary>
	/// Converts the dp value to the pixels on the device, based on the display density
	/// </summary>
	/// <returns>The device pixels.</returns>
	/// <param name="ctx">Context.</param>
	/// <param name="dp">Dp.</param>
	/// <param name="offset">Addtional Offset</param>
	public static int ToDevicePixels (this Context ctx, int dp, float offset)
	{
		var wm = ctx.GetWindowManager ();
		wm.DefaultDisplay.GetMetrics (displayMetrics);

		var density = displayMetrics.Density;
		return (int)(dp * density + offset);
	}
	
}

