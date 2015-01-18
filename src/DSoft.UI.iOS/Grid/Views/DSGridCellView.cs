// ****************************************************************************
// <copyright file="DSGridCellView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Base;
using DSoft.Datatypes.Types;
using DSoft.Datatypes.Grid.Data;
using DSoft.Datatypes.Enums;
using DSoft.Themes.Grid;
using DSoft.Datatypes.Formatters;
using DSoft.Datatypes.Grid.Interfaces;
using DSoft.Datatypes.Grid.Shared;



#if __UNIFIED__
using UIKit;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
#else
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

using System.Drawing;

using CGRect = global::System.Drawing.RectangleF;
using CGPoint = global::System.Drawing.PointF;
using CGSize = global::System.Drawing.SizeF;
using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;
#endif

namespace DSoft.UI.Grid.Views
{
	/// <summary>
	/// A cell for use in the DSGridView
	/// </summary>
	public class DSGridCellView : UIView, IDSGridCellView
	{
	
		#region Fields

		private DSCellProcessor mProcessor;
		private UIView mContentView;
		#endregion

		#region Properties

		/// <summary>
		/// Gets the processor for the cell view
		/// </summary>
		/// <value>The processor.</value>
		public DSCellProcessor Processor 
		{
			get
			{
				if (mProcessor == null)
				{
					mProcessor = new DSCellProcessor(()=>
					{
						this.SetNeedsDisplay ();
					});
				}

				return mProcessor;
			}
		}

		/// <summary>
		/// Gets the grid view.
		/// </summary>
		/// <value>The grid view.</value>
		private DSGridView GridView
		{
			get
			{
				return Processor.GridView as DSGridView;
			}
		}

		/// <summary>
		/// Gets the index of the row.
		/// </summary>
		/// <value>The index of the row.</value>
		public int RowIndex
		{
			get
			{
				return Processor.RowIndex;
			}
		}

		/// <summary>
		/// Gets the index of the column.
		/// </summary>
		/// <value>The index of the column.</value>
		public int ColumnIndex
		{
			get
			{
				return Processor.ColumnIndex;
			}
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridCellView"/> class.
		/// </summary>
		internal DSGridCellView () : base ()
		{
			this.Opaque = true;
			this.BackgroundColor = UIColor.Clear;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridCellView"/> class.
		/// </summary>
		/// <param name="aView">A view.</param>
		/// <param name="RowView">Row view.</param>
		internal DSGridCellView (DSGridView aView, DSGridRowView RowView) : this ()
		{
			this.Processor.GridRowView = RowView;
			this.Processor.GridView = aView;		

		}

		#endregion

		#region Overrides

		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw (CGRect rect)
		{
			base.Draw (rect);
			
			ReDraw (rect);
		}

		/// <summary>
		/// Toucheses the began.
		/// </summary>
		/// <param name="touches">Touches.</param>
		/// <param name="evt">Evt.</param>
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);
		
			CellWasTapped (touches, evt);
		
		}

		public override void TouchesCancelled (NSSet touches, UIEvent evt)
		{
			base.TouchesCancelled (touches, evt);

			//Fix issue with unneccesart calls to the tap handlers
			NSObject.CancelPreviousPerformRequest (this, new Selector ("DidSingleTap"), null);
		}

		#endregion

		#region Private Functions


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

			CGContext ctx = UIGraphics.GetCurrentContext ();

			switch (Processor.Style)
			{
				case CellStyle.Blank:
					{
						GridView.Theme.BackgroundColor.ToUIColor ().SetFill ();
					}
					break;
				case CellStyle.Cell:
					{
						if (Processor.IsSelected)
						{
							GridView.Theme.CellBackgroundHighlight.ToUIColor ().SetFill ();

						}
						else
						{
							var alterColor = (GridView.Theme.CellBackground2 != null) ? GridView.Theme.CellBackground2.ToUIColor () 
						                 : GridView.Theme.CellBackground.ToUIColor ();
							var aColor = (Processor.IsOdd) ? GridView.Theme.CellBackground.ToUIColor () : alterColor;
							aColor.SetFill ();
						}
					}
					break;
				case CellStyle.Header:
					{
						GridView.Theme.HeaderBackground.ToUIColor ().SetFill ();
					}
					break;
			}


			ctx.SetLineWidth (GridView.Theme.CellBorderWidth);
			GridView.Theme.BorderColor.ToUIColor ().SetStroke ();
			
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

			switch (GridView.Theme.CellBorderStyle)
			{
				case BorderStyle.Full:
					{
						if (Processor.RowIndex != 0 && Processor.Style != CellStyle.Blank)
						{
							//draw the upper line
							ctx.StrokeLineSegments (new CGPoint[]{ topLeftPoint, topRightPoint });
						}
						
						if (Processor.Style != CellStyle.Blank && Processor.ColumnIndex != 0)
						{
							ctx.StrokeLineSegments (new CGPoint[]{ topLeftPoint, bottomLeftPoint });
						}
						
						//ctx.StrokeRect(aRect);
					}
					break;
				case BorderStyle.HorizontalOnly:
					{
						if (Processor.RowIndex != 0 && Processor.Style != CellStyle.Blank)
						{
							//draw the upper line
							ctx.StrokeLineSegments (new CGPoint[]{ topLeftPoint, topRightPoint });
						}
						
					}
					break;
				case BorderStyle.VerticalOnly:
					{
						if (Processor.Style != CellStyle.Blank && Processor.ColumnIndex != 0)
						{
							ctx.StrokeLineSegments (new CGPoint[]{ topLeftPoint, bottomLeftPoint });
						}

					}
					break;
			}

			ctx.SetAllowsAntialiasing (true);


			//do nothing else if the row is blank
			if (Processor.Style == CellStyle.Blank)
				return;

//			//get the row index, -1 if it's a header style
//			var index = (this.Style == CellStyle.Header) ? -1 : this.RowIndex;
//
//			var Value = GridView.GetValue (index, this.ColumnName).Value;

			if (Processor.Style != CellStyle.Blank && Processor.ValueObject.Value != null)
			{
				//update to work with different datatypes
				//check to see if the formatter is a DSViewFormatter
				if (Processor.Formatter != null && Processor.Formatter is DSViewFormatter)
				{
					var viewFormatter = Processor.Formatter as DSViewFormatter;

					var aView = viewFormatter.View;

					if (!(aView is UIView))
						throw new Exception ("DSViewFormatter has type that isn't a UIView");


					aView.Value = Processor.ValueObject.Value;
					var aFrame = rect;

					if (Processor.Formatter.Size != null)
					{
						aFrame.Width = Processor.Formatter.Size.Width;
						aFrame.Height = Processor.Formatter.Size.Height;
					}

					aView.ViewFrame = aFrame.ToDSRectangle ();
					aView.IsReadOnly = Processor.IsReadOnly;
					aView.UpdateAction = (obj) => {
						GridView.Processor.SetValue (Processor.Index, Processor.ColumnName, obj);
					};
					//aView.IsReadOnly = 
					mContentView = (UIView)aView;
					this.AddSubview (mContentView);

				}
				else if (Processor.ValueObject.Value is UIImage || Processor.ValueObject.Value is DSBitmap ) 
				{
					UIImage image = null;

					if (Processor.ValueObject.Value is UIImage)
					{
						image = (UIImage)Processor.ValueObject.Value;
					}
					else if (Processor.ValueObject.Value is DSBitmap)
					{
						image = ((DSBitmap)Processor.ValueObject.Value).ToUIImage();
					}

					mContentView = new UIImageView (image);

					var aFrame = mContentView.Frame;
					
					if (Processor.Formatter != null)
					{
						
						aFrame.Width = Processor.Formatter.Size.Width;
						aFrame.Height = Processor.Formatter.Size.Height;
						
						//anView.Frame = aFrame;
					}
					
					//try a centralize the image
					var xPos = (this.Frame.Width / 2) - (aFrame.Size.Width / 2);
					var yPos = (this.Frame.Height / 2) - (aFrame.Size.Height / 2);
					aFrame.X = xPos;
					aFrame.Y = yPos;

					if (Processor.Formatter != null && Processor.Formatter is DSImageFormatter)
					{
						var formta = Processor.Formatter as DSImageFormatter;

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
				else if (Processor.ValueObject.Value is Boolean && Processor.Formatter != null && Processor.Formatter is DSBooleanFormatter)
				{
					var boolFormatter = Processor.Formatter as DSBooleanFormatter;
					
					if (boolFormatter.Style == BooleanFormatterStyle.Text)
					{
						UILabel label = new UILabel (rect.Inset (5, 5));
						label.BackgroundColor = UIColor.Clear;
						label.Font = (Processor.Style == CellStyle.Header) ? GridView.Theme.HeaderTextFont.ToUIFont () : GridView.Theme.CellTextFont.ToUIFont ();
						label.Text = ((bool)Processor.ValueObject.Value) ? boolFormatter.TrueValue.ToString() : boolFormatter.FalseValue.ToString();
						
						if (Processor.Style == CellStyle.Header)
						{
							label.TextAlignment = (UITextAlignment)GridView.Theme.HeaderTextAlignment;
							label.TextColor = GridView.Theme.HeaderTextForeground.ToUIColor ();
						}
						else
						{
							var alterColor = (GridView.Theme.CellTextForeground2 != null) ? GridView.Theme.CellTextForeground2.ToUIColor ()
						                 : GridView.Theme.CellTextForeground.ToUIColor ();
							var aColor = (Processor.IsOdd) ? GridView.Theme.CellTextForeground.ToUIColor () : alterColor;
		
							label.TextColor = (Processor.IsSelected) ? GridView.Theme.CellTextHighlight.ToUIColor () : aColor;
		
							label.TextAlignment = (UITextAlignment)boolFormatter.TextAlignment;
						}
						
						mContentView = label;
						this.AddSubview (mContentView);
					}
					else
					{
						var bValue = ((bool)Processor.ValueObject.Value) ? boolFormatter.TrueValue : boolFormatter.FalseValue;


						UIImage image = null;

						if (bValue != null)
						{
							if (bValue is String)
							{
								var fileName = bValue as String;

								if (!String.IsNullOrWhiteSpace (fileName))
								{
									image = new UIImage (fileName);
								}
							}
							else if (bValue is UIImage)
							{
								image = (UIImage)bValue;
							}
							else if (bValue is DSBitmap)
							{
								image = ((DSBitmap)bValue).ToUIImage();
							}

	
						}

						if (image != null)
						{
							var anView = new UIImageView (image);

							var aFrame = anView.Frame;

							if (Processor.Formatter != null)
							{

								aFrame.Width = Processor.Formatter.Size.Width;
								aFrame.Height = Processor.Formatter.Size.Height;
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
					var label = new UILabel (rect.Inset (5, 5));
					label.BackgroundColor = this.BackgroundColor;
					label.Font = (Processor.Style == CellStyle.Header) ? GridView.Theme.HeaderTextFont.ToUIFont () 
				             : GridView.Theme.CellTextFont.ToUIFont ();
					label.Text = Processor.ValueObject.Value.ToString ();
					label.LineBreakMode = UILineBreakMode.TailTruncation;

					if (Processor.Style == CellStyle.Header)
					{
						int numLines = Processor.ValueObject.Value.ToString ().Split ('\n').Length;
						label.Lines = (label.Frame.Height >= 13) ? numLines : 1;

						var textAlignment = GridView.Theme.HeaderTextAlignment;

						if (Processor.Formatter != null && Processor.Formatter is DSTextFormatter)
						{
							textAlignment = ((DSTextFormatter)Processor.Formatter).Alignment;

						}
						label.TextAlignment = (UITextAlignment)textAlignment;
						label.TextColor = GridView.Theme.HeaderTextForeground.ToUIColor ();
					}
					else
					{
						var alterColor = (GridView.Theme.CellTextForeground2 != null) ? GridView.Theme.CellTextForeground2.ToUIColor () : GridView.Theme.CellTextForeground.ToUIColor ();
						var aColor = (Processor.IsOdd) ? GridView.Theme.CellTextForeground.ToUIColor () : alterColor;
	
						label.TextColor = (Processor.IsSelected) ? GridView.Theme.CellTextHighlight.ToUIColor () : aColor;
	
						var textAlignment = GridView.Theme.CellContentAlignment;

						if (Processor.Formatter != null && Processor.Formatter is DSTextFormatter)
						{
							textAlignment = ((DSTextFormatter)Processor.Formatter).Alignment;

						}

						label.TextAlignment = (UITextAlignment)textAlignment;
					}
					
					mContentView = label;
					this.AddSubview (mContentView);
				}

			}

			//Set the border
			if (Processor.Style != CellStyle.Blank)
			{
				ctx.SetLineWidth (GridView.Theme.BorderWidth);
				GridView.Theme.BorderColor.ToUIColor ().SetStroke ();
				
				switch (Processor.PositionType)
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
				
			if (Processor.Style == CellStyle.Header && Processor.SortStyle != SortIndicatorStyle.None)
			{
				
				//draw the up indicator
				var anImage = (Processor.SortStyle == SortIndicatorStyle.Ascending) ? GridView.Theme.HeaderSortIndicatorUp : 
			                  GridView.Theme.HeaderSortIndicatorDown;


				if (anImage != null)
				{
					var aUIImage = anImage.ToUIImage ();

					//if iOS7 or above set as template image
					if (iOSHelper.IsiOS7)
					{
						aUIImage = aUIImage.ImageWithRenderingMode (UIImageRenderingMode.AlwaysTemplate);

					}

					UIImageView aView = new UIImageView (aUIImage);

					//if iOS7 tint the image with the header text color
					if (iOSHelper.IsiOS7)
						aView.TintColor = GridView.Theme.HeaderTextForeground.ToUIColor ();

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
		private void CellWasTapped (NSSet touches, UIEvent evt)
		{
			UITouch touch = touches.AnyObject as UITouch;


			if (touch != null)
			{
				//make sure the touch was in this view
				if (true)
				{
					switch (touch.TapCount)
					{
						case 1:
							{
								if (GridView.EnableDoubleTap)
								{
									this.PerformSelector (new Selector ("DidSingleTap"), null, DSGridView.DoubleTapTimeout);
								}
								else
								{
									DidSingleTap ();
								}

							}
							break;
						case 2:
							{
								if (GridView.EnableDoubleTap)
								{
									NSObject.CancelPreviousPerformRequest (this, new Selector ("DidSingleTap"), null);
									this.PerformSelector (new Selector ("DidDoubleTap"), null, 0.01f);
								}
								//

							}
							break;
					}
				}

			}
		}

		[Export ("DidSingleTap")]
		private void DidSingleTap ()
		{
			Processor.DidSingleTap(this);
		}

		[Export ("DidDoubleTap")]
		private void DidDoubleTap ()
		{
			Processor.DidDoubleTap(this);
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


		}

		public void DetachView()
		{
			this.RemoveFromSuperview();
		}
		#endregion
	}
}

