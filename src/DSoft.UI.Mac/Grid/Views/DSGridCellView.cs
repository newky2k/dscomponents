// ****************************************************************************
// <copyright file="DSGridCellView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Grid.Data;
using AppKit;
using Foundation;
using ObjCRuntime;
using DSoft.Datatypes.Base;
using CoreGraphics;
using DSoft.UI.Mac.Extensions;
using DSoft.Datatypes.Formatters;

namespace DSoft.UI.Mac.Grid.Views
{
	/// <summary>
	/// A cell for use in the DSGridView
	/// </summary>
	public class DSGridCellView : NSView
	{
		#region Internal Properties

		#region Fields

		private bool mIsSelected;
		private CellStyle mStyle;
		private DSGridView mGridView;
		private DSGridRowView mRowView;
		private NSView mContentView;
		private SortIndicatorStyle mSortStyle;
		private int mRowIndex;
		private DSDataValue mValue;
		private NSColor BackgroundColor;
		#endregion

		/// <summary>
		/// Is this an odd cell
		/// </summary>
		internal bool IsOdd;

		/// <summary>
		/// gets if the cell is selected
		/// </summary>
		internal bool IsSelected { 
			get
			{
				return mIsSelected;
			} 
			set
			{
				if (mIsSelected != value)
				{
					mIsSelected = value;

					this.SetNeedsDisplay ();
				}
			}
		}

		/// <summary>
		/// The style of the cell
		/// </summary>
		internal CellStyle Style {
			get
			{
				return mStyle;
			}
			set
			{
				if (mStyle != value)
				{
					mStyle = value;

					this.SetNeedsDisplay ();
				}

			}
		}

		/// <summary>
		/// The name of the column.
		/// </summary>
		internal String ColumnName;
		/// <summary>
		/// The type of the position.
		/// </summary>
		internal CellPositionType PositionType;

		/// <summary>
		/// The sort style.
		/// </summary>
		internal SortIndicatorStyle SortStyle {
			get
			{
				return mSortStyle;
			}
			set
			{
				if (mSortStyle != value)
				{
					mSortStyle = value;

					this.SetNeedsDisplay ();
				}
			}
		}

		/// <summary>
		/// Gets or sets the formatter.
		/// </summary>
		/// <value>The formatter.</value>
		internal DSFormatter Formatter { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is read only.
		/// </summary>
		/// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
		internal bool IsReadOnly { get; set; }

		/// <summary>
		/// Calculated index
		/// </summary>
		/// <value>The index.</value>
		private int Index 
		{
			get
			{
				//get the row index, -1 if it's a header style
				return (this.Style == CellStyle.Header) ? -1 : this.RowIndex;
			}
		}
		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>The value.</value>
		private DSDataValue ValueObject
		{
			get
			{


				var aValue = mGridView.Processor.GetValue (Index, this.ColumnName);

				if (aValue !=  null && aValue != mValue)
				{
					if (mValue != null)
					{
						mValue.PropertyChanged -= HandlePropertyChanged;
					}

					mValue = aValue;

					mValue.PropertyChanged += HandlePropertyChanged;

				}

				return mValue;
			}
		}


		#endregion

		#region Public Properties

		/// <summary>
		/// The Column Index of the cell within the grid
		/// </summary>
		public int ColumnIndex { get; internal set; }

		/// <summary>
		/// The Row Index of the cell within the grid
		/// </summary>
		public int RowIndex {
			get { return mRowIndex; }
			internal set
			{
				if (mRowIndex != value)
				{
					mRowIndex = value;

					this.SetNeedsDisplay ();
				}
			} 
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridCellView"/> class.
		/// </summary>
		internal DSGridCellView () : base ()
		{

			this.BackgroundColor = NSColor.Clear;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridCellView"/> class.
		/// </summary>
		/// <param name="aView">A view.</param>
		/// <param name="RowView">Row view.</param>
		internal DSGridCellView (DSGridView aView, DSGridRowView RowView) : this ()
		{
			this.mRowView = RowView;
			this.mGridView = aView;		

		}

		#endregion

		#region Overrides

		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void DrawRect (CGRect rect)
		{
			base.DrawRect (rect);

			ReDraw (rect);
		}
			
	
		public override void TouchesBeganWithEvent(NSEvent theEvent)
		{
			base.TouchesBeganWithEvent(theEvent);

			CellWasTapped (null, theEvent);
		}

		public override void TouchesCancelledWithEvent(NSEvent theEvent)
		{
			base.TouchesCancelledWithEvent(theEvent);

			NSObject.CancelPreviousPerformRequest (this, new Selector ("DidSingleTap"), null);
		}

		#endregion

		#region Private Functions

		/// <summary>
		/// Handles the property changed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandlePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			this.SetNeedsDisplay();
		}

		/// <summary>
		/// Draw the contents of the Cell
		/// </summary>
		/// <param name="rect">Rect.</param>
		private void ReDraw (CGRect rect)
		{
			foreach (var aView in this.Subviews)
			{
				aView.RemoveFromSuperview ();
			}

			mContentView = null;
		
			CGContext ctx = NSGraphicsContext.CurrentContext.GraphicsPort;


			switch (Style)
			{
				case CellStyle.Blank:
					{
						mGridView.Theme.BackgroundColor.ToNSColor ().SetFill ();
					}
					break;
				case CellStyle.Cell:
					{
						if (IsSelected)
						{
							mGridView.Theme.CellBackgroundHighlight.ToNSColor ().SetFill ();

						}
						else
						{
							var alterColor = (mGridView.Theme.CellBackground2 != null) ? mGridView.Theme.CellBackground2.ToNSColor () 
								: mGridView.Theme.CellBackground.ToNSColor ();
							var aColor = (IsOdd) ? mGridView.Theme.CellBackground.ToNSColor () : alterColor;
							aColor.SetFill ();
						}
					}
					break;
				case CellStyle.Header:
					{
						mGridView.Theme.HeaderBackground.ToNSColor ().SetFill ();
					}
					break;
			}


			ctx.SetLineWidth (mGridView.Theme.CellBorderWidth);
			mGridView.Theme.BorderColor.ToNSColor ().SetStroke ();

			CGPath _path = new CGPath ();

			var aRect = new CGRect (rect.X, rect.Y, rect.Width - 0, rect.Height - 0);

			_path.AddRect (aRect);

			_path.CloseSubpath ();

			ctx.AddPath (_path);
			ctx.DrawPath (CGPathDrawingMode.Fill);

			var topLeftPoint = new CGPoint (0, 0);
			var topRightPoint = new CGPoint (aRect.Width, 0);
			var bottomLeftPoint = new CGPoint (0, aRect.Height);
			var bottomRightPoint = new CGPoint (aRect.Width, aRect.Height);

			switch (mGridView.Theme.CellBorderStyle)
			{
				case BorderStyle.Full:
					{
						if (this.RowIndex != 0 && Style != CellStyle.Blank)
						{
							//draw the upper line
							ctx.StrokeLineSegments (new CGPoint[]{ topLeftPoint, topRightPoint });
						}

						if (Style != CellStyle.Blank && this.ColumnIndex != 0)
						{
							ctx.StrokeLineSegments (new CGPoint[]{ topLeftPoint, bottomLeftPoint });
						}

						//ctx.StrokeRect(aRect);
					}
					break;
				case BorderStyle.HorizontalOnly:
					{
						if (this.RowIndex != 0 && Style != CellStyle.Blank)
						{
							//draw the upper line
							ctx.StrokeLineSegments (new CGPoint[]{ topLeftPoint, topRightPoint });
						}

					}
					break;
				case BorderStyle.VerticalOnly:
					{
						if (Style != CellStyle.Blank && this.ColumnIndex != 0)
						{
							ctx.StrokeLineSegments (new CGPoint[]{ topLeftPoint, bottomLeftPoint });
						}

					}
					break;
			}

			ctx.SetAllowsAntialiasing (true);


			//do nothing else if the row is blank
			if (this.Style == CellStyle.Blank)
				return;

			//			//get the row index, -1 if it's a header style
			//			var index = (this.Style == CellStyle.Header) ? -1 : this.RowIndex;
			//
			//			var Value = mGridView.GetValue (index, this.ColumnName).Value;

			if (Style != CellStyle.Blank && ValueObject.Value != null)
			{
				//update to work with different datatypes
				//check to see if the formatter is a DSViewFormatter
				if (Formatter != null && Formatter is DSViewFormatter)
				{
					var viewFormatter = Formatter as DSViewFormatter;

					var aView = viewFormatter.View;

					if (!(aView is NSView))
						throw new Exception ("DSViewFormatter has type that isn't a UIView");


					aView.Value = ValueObject.Value;
					var aFrame = rect;

					if (Formatter.Size != null)
					{
						aFrame.Width = Formatter.Size.Width;
						aFrame.Height = Formatter.Size.Height;
					}

					aView.ViewFrame = aFrame.ToDSRectangle ();
					aView.IsReadOnly = this.IsReadOnly;
					aView.UpdateAction = (obj) => {
						mGridView.Processor.SetValue (Index, this.ColumnName, obj);
					};
					//aView.IsReadOnly = 
					mContentView = (NSView)aView;
					this.AddSubview (mContentView);

				}
				else if (ValueObject.Value is NSImage)
				{
					var aImage = (NSImage)ValueObject.Value;

					mContentView = new NSImageView(CGRect.Empty);
					((NSImageView)mContentView).Image = aImage;

					var aFrame = mContentView.Frame;

					if (Formatter != null)
					{

						aFrame.Width = Formatter.Size.Width;
						aFrame.Height = Formatter.Size.Height;

						//anView.Frame = aFrame;
					}

					//try a centralize the image
					var xPos = (this.Frame.Width / 2) - (aFrame.Size.Width / 2);
					var yPos = (this.Frame.Height / 2) - (aFrame.Size.Height / 2);
					aFrame.X = xPos;
					aFrame.Y = yPos;

					if (Formatter != null && Formatter is DSImageFormatter)
					{
						var formta = Formatter as DSImageFormatter;

						if (formta.Margin != null)
						{
							//apply the margins to the frame
							var mRect = aFrame.ToDSRectangle ();

							mRect.ApplyMargin (formta.Margin);

							aFrame = mRect.ToRectangleF ();
						}
					}

					mContentView.Frame = aFrame.Integral ();
					this.AddSubview (mContentView);
				}
				else if (ValueObject.Value is Boolean && Formatter != null && Formatter is DSBooleanFormatter)
				{
					var boolFormatter = Formatter as DSBooleanFormatter;

					if (boolFormatter.Style == BooleanFormatterStyle.Text)
					{
						var label = new NSTextField (rect.Inset (5, 5));
						label.BackgroundColor = NSColor.Clear;
						label.Font = (Style == CellStyle.Header) ? mGridView.Theme.HeaderTextFont.ToUIFont () : mGridView.Theme.CellTextFont.ToUIFont ();
						label.StringValue = ((bool)ValueObject.Value) ? boolFormatter.TrueValue : boolFormatter.FalseValue;

						if (Style == CellStyle.Header)
						{
							label.Alignment = (NSTextAlignment)mGridView.Theme.HeaderTextAlignment;
							label.TextColor = mGridView.Theme.HeaderTextForeground.ToNSColor ();
						}
						else
						{
							var alterColor = (mGridView.Theme.CellTextForeground2 != null) ? mGridView.Theme.CellTextForeground2.ToNSColor ()
								: mGridView.Theme.CellTextForeground.ToNSColor ();
							var aColor = (IsOdd) ? mGridView.Theme.CellTextForeground.ToNSColor () : alterColor;

							label.TextColor = (IsSelected) ? mGridView.Theme.CellTextHighlight.ToNSColor () : aColor;

							label.Alignment = (NSTextAlignment)boolFormatter.TextAlignment;
						}

						mContentView = label;
						this.AddSubview (mContentView);
					}
					else
					{
						var fileName = ((bool)ValueObject.Value) ? boolFormatter.TrueValue : boolFormatter.FalseValue;

						if (!String.IsNullOrWhiteSpace (fileName))
						{
							var anImage = new NSImage (fileName);

							var anView = new NSImageView (CGRect.Empty);
							anView.Image = anImage;

							var aFrame = anView.Frame;

							if (Formatter != null)
							{

								aFrame.Width = Formatter.Size.Width;
								aFrame.Height = Formatter.Size.Height;
							}

							//try a centralize the image
							var xPos = (this.Frame.Width / 2) - (aFrame.Size.Width / 2);
							var yPos = (this.Frame.Height / 2) - (aFrame.Size.Height / 2);
							aFrame.X = xPos;
							aFrame.Y = yPos;

							anView.Frame = aFrame.Integral ();

							mContentView = anView;
							this.AddSubview (mContentView);
						}
					}
				}
				else
				{

					var label = new NSTextField (rect.Inset (5, 5));
					label.BackgroundColor = this.BackgroundColor;
					label.Font = (Style == CellStyle.Header) ? mGridView.Theme.HeaderTextFont.ToUIFont () 
						: mGridView.Theme.CellTextFont.ToUIFont ();
					label.StringValue = ValueObject.Value.ToString ();

					//label.LineBreakMode = UILineBreakMode.TailTruncation;

					if (Style == CellStyle.Header)
					{
						int numLines = ValueObject.Value.ToString ().Split ('\n').Length;
						//label.Lines = (label.Frame.Height >= 13) ? numLines : 1;

						var textAlignment = mGridView.Theme.HeaderTextAlignment;

						if (Formatter != null && Formatter is DSTextFormatter)
						{
							textAlignment = ((DSTextFormatter)Formatter).Alignment;

						}
						label.Alignment = (NSTextAlignment)textAlignment;
						label.TextColor = mGridView.Theme.HeaderTextForeground.ToNSColor ();
					}
					else
					{
						var alterColor = (mGridView.Theme.CellTextForeground2 != null) ? mGridView.Theme.CellTextForeground2.ToNSColor () : mGridView.Theme.CellTextForeground.ToNSColor ();
						var aColor = (IsOdd) ? mGridView.Theme.CellTextForeground.ToNSColor () : alterColor;

						label.TextColor = (IsSelected) ? mGridView.Theme.CellTextHighlight.ToNSColor () : aColor;

						var textAlignment = mGridView.Theme.CellContentAlignment;

						if (Formatter != null && Formatter is DSTextFormatter)
						{
							textAlignment = ((DSTextFormatter)Formatter).Alignment;

						}

						label.Alignment = (NSTextAlignment)textAlignment;
					}

					mContentView = label;
					this.AddSubview (mContentView);
				}

			}

			//Set the border
			if (Style != CellStyle.Blank)
			{
				ctx.SetLineWidth (mGridView.Theme.BorderWidth);
				mGridView.Theme.BorderColor.ToNSColor ().SetStroke ();

				switch (PositionType)
				{
					case CellPositionType.LeftTop:
						{
							ctx.StrokeLineSegments (new CGPoint[]{ topLeftPoint, topRightPoint });
							ctx.StrokeLineSegments (new CGPoint[]{ topLeftPoint, bottomLeftPoint });
						}
						break;
					case CellPositionType.LeftMiddle:
						{
							ctx.StrokeLineSegments (new CGPoint[]{ topLeftPoint, bottomLeftPoint });
						}
						break;
					case CellPositionType.LeftBottom:
						{
							ctx.StrokeLineSegments (new CGPoint[]{ bottomLeftPoint, bottomRightPoint });
							ctx.StrokeLineSegments (new CGPoint[]{ topLeftPoint, bottomLeftPoint });
						}
						break;
					case CellPositionType.CenterTop:
						{
							ctx.StrokeLineSegments (new CGPoint[]{ topLeftPoint, topRightPoint });
						}
						break;
					case CellPositionType.CenterMiddle:
						{

						}
						break;
					case CellPositionType.CenterBottom:
						{
							ctx.StrokeLineSegments (new CGPoint[]{ bottomLeftPoint, bottomRightPoint });
						}
						break;
					case CellPositionType.RightTop:
						{
							ctx.StrokeLineSegments (new CGPoint[]{ topLeftPoint, topRightPoint });
							ctx.StrokeLineSegments (new CGPoint[]{ topRightPoint, bottomRightPoint });
						}
						break;
					case CellPositionType.RightMiddle:
						{
							ctx.StrokeLineSegments (new CGPoint[]{ topRightPoint, bottomRightPoint });
						}
						break;
					case CellPositionType.RightBottom:
						{
							ctx.StrokeLineSegments (new CGPoint[]{ bottomLeftPoint, bottomRightPoint });
							ctx.StrokeLineSegments (new CGPoint[]{ topRightPoint, bottomRightPoint });
						}
						break;
				}
			}

			if (Style == CellStyle.Header && SortStyle != SortIndicatorStyle.None)
			{

				//draw the up indicator
				var anImage = (SortStyle == SortIndicatorStyle.Ascending) ? mGridView.Theme.HeaderSortIndicatorUp : 
					mGridView.Theme.HeaderSortIndicatorDown;


				if (anImage != null)
				{
					var aUIImage = anImage.ToUIImage ();

					var aView = new NSImageView ();
					aView.Image = aUIImage;


					var aLeft = this.Bounds.Width - 22;
					var atop = (this.Bounds.Height / 2) - 5;

					aView.Frame = new CGRect (aLeft, atop, 16, 8).Integral ();

					this.AddSubview (aView);
				}
			}
		}

		/// <summary>
		/// Handle touches ended event
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		private void CellWasTapped (NSSet touches, NSEvent evt)
		{
			//evt.
			var touch = touches.AnyObject as NSObject;


//			if (touch != null)
//			{
//				//make sure the touch was in this view
//				if (true)
//				{
//					switch (touch.TapCount)
//					{
//						case 1:
//							{
//								if (mGridView.EnableDoubleTap)
//								{
//									this.PerformSelector (new Selector ("DidSingleTap"), null, DSGridView.DoubleTapTimeout);
//								}
//								else
//								{
//									DidSingleTap ();
//								}
//
//							}
//							break;
//						case 2:
//							{
//								if (mGridView.EnableDoubleTap)
//								{
//									NSObject.CancelPreviousPerformRequest (this, new Selector ("DidSingleTap"), null);
//									this.PerformSelector (new Selector ("DidDoubleTap"), null, 0.01f);
//								}
//								//
//
//							}
//							break;
//					}
//				}
//
//			}
		}

		[Export ("DidSingleTap")]
		private void DidSingleTap ()
		{
			if (mGridView == null)
				return;

			if (Style == CellStyle.Header)
			{
				mGridView.HandleOnHeaderCellTapped (this);
			}
			else
			{

				mGridView.HandleOnCellSingleTap (this);
				mGridView.HandleOnRowSingleSelect (mRowView);
				mGridView.HandleOnSelectedRowChanged (RowIndex);
			}

		}

		[Export ("DidDoubleTap")]
		private void DidDoubleTap ()
		{
			if (mGridView == null)
				return;

			mGridView.HandleOnSelectedRowChanged (RowIndex);
			mGridView.HandleOnCellDoubleTap (this);
			mGridView.HandleOnRowDoubleSelect (mRowView);
		}

		/// <summary>
		/// Tears down.
		/// </summary>
		public void TearDown ()
		{
			if (mContentView != null)
			{
				mContentView.RemoveFromSuperview ();
			}

			mContentView = null;
			mGridView = null;
			mRowView = null;
			Formatter = null;

		}

		#endregion
	}
}

