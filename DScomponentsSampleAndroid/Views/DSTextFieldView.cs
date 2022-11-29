// ****************************************************************************
// <copyright file="DSTextFieldView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using DSoft.Datatypes.UI.Interfaces;
using DSoft.Datatypes.Types;

namespace DSComponentsSample.Views
{
	public class DSTextFieldView : View, IDSCustomView
	{
		#region IDSCustomView implementation

		public DSRectangle ViewFrame
		{
			get
			{
				return null;
			}
			set
			{

			}
		}

		public object Value
		{
			get
			{
				return null;
			}
			set
			{

			}
		}

		public bool IsReadOnly
		{
			get
			{
				return true;
			}
			set
			{

			}
		}

		public Action<object> UpdateAction
		{
			get
			{
				return null;
			}
			set
			{

			}
		}

		#endregion

		public DSTextFieldView(Context context) :
			base(context)
		{
			Initialize();
		}

		public DSTextFieldView(Context context, IAttributeSet attrs) :
			base(context, attrs)
		{
			Initialize();
		}

		public DSTextFieldView(Context context, IAttributeSet attrs, int defStyle) :
			base(context, attrs, defStyle)
		{
			Initialize();
		}

		void Initialize()
		{
		}
	}
}

