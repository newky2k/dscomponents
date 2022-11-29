// ****************************************************************************
// <copyright file="DSTextFieldView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.UI.Interfaces;
using DSoft.Datatypes.Types;


#if __UNIFIED__
using UIKit;
using CoreGraphics;
using Foundation;
#else
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using System.Drawing;

using CGRect = global::System.Drawing.RectangleF;
using CGPoint = global::System.Drawing.PointF;
using CGSize = global::System.Drawing.SizeF;
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;
#endif

namespace DSComponentsSample.Views
{
	/// <summary>
	/// Sample custom view 
	/// </summary>
	public class DSTextFieldView : UIView, IDSCustomView
	{
		#region Fields

		private UITextField mTextField;
		private Action<object> mUpdateAction;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the view frame/rectangle
		/// </summary>
		/// <value>The view frame.</value>
		public DSRectangle ViewFrame {
			get
			{
				return this.Frame.ToDSRectangle ();
			}
			set
			{
				this.Frame = value.ToRectangleF ();

				this.SetNeedsLayout ();

			}
		}

		/// <summary>
		/// Gets or sets the value for the view
		/// </summary>
		/// <value>The value.</value>
		public object Value {
			get
			{
				return mTextField.Text;
			}
			set
			{
				mTextField.Text = value.ToString ();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the view is in readonly mode
		/// </summary>
		/// <value>true</value>
		/// <c>false</c>
		public bool IsReadOnly { 
			get
			{
				return !mTextField.Enabled;
			}
			set
			{
				mTextField.Enabled = !value;
			}
		}

		/// <summary>
		/// Gets or sets the update action.
		/// </summary>
		/// <value>The update action.</value>
		public Action<object> UpdateAction {
			get
			{
				return mUpdateAction;
			}
			set
			{
				mUpdateAction = value;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DSComponentsSample.Views.DSTextFieldView"/> class.
		/// </summary>
		public DSTextFieldView () : base ()
		{
			mTextField = new UITextField (CGRect.Empty);
			mTextField.WeakDelegate = this;

			this.Add (mTextField);
		}

		#endregion

		#region Overides

		public override void LayoutSubviews ()
		{
		
			mTextField.Frame = this.Bounds;

		}

		#endregion

		#region Delegate Functions

		[Export ("textFieldDidEndEditing:")]
		public void EditingEnded (UITextField textField)
		{
			this.EndEditing (true);

			if (mUpdateAction != null)
			{
				mUpdateAction (this.Value);
			}
		}

		[Export ("textFieldShouldReturn:")]
		public bool ShouldReturn (UITextField textField)
		{
			this.EndEditing (true);
			this.ResignFirstResponder ();
			return false;
		}

		#endregion
	}
}

