// ****************************************************************************
// <copyright file="DSTextFieldView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using AppKit;
using DSoft.Datatypes.UI.Interfaces;

namespace DSComponentsSampleMac.Views
{
	public class DSTextFieldView : NSView, IDSCustomView
	{
		#region IDSCustomView implementation

		public DSoft.Datatypes.Types.DSRectangle ViewFrame
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public object Value
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public bool IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public Action<object> UpdateAction
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		#endregion

		public DSTextFieldView()
		{
		}
	}
}

