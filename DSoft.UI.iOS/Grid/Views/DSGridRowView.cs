// ****************************************************************************
// <copyright file="DSGridRowView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.UI.Grid.Views.Collections;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Grid.MetaData.Collections;
using DSoft.Datatypes.Grid.Interfaces;
using DSoft.Datatypes.Grid.Shared;

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

namespace DSoft.UI.Grid.Views
{
	/// <summary>
	/// DSGrid row view
	/// </summary>
	public class DSGridRowView : UIView, IDSGridRowView
	{
		#region Fields
		private DSRowProcessor mProcessor;
		#endregion

		#region Properties

		/// <summary>
		/// Gets the processor.
		/// </summary>
		/// <value>The processor.</value>
		public DSRowProcessor Processor
		{
			get
			{
				if (mProcessor == null)
				{
					mProcessor = new DSRowProcessor(()=>
					{
						this.SetNeedsDisplay();
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
		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridRowView"/> class.
		/// </summary>
		internal DSGridRowView ()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridRowView"/> class.
		/// </summary>
		/// <param name="aView">A view.</param>
		internal DSGridRowView (DSGridView aView)
		{
			Processor.GridView = aView;
			this.Opaque = true;
			this.BackgroundColor = UIColor.Clear;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridRowView"/> class.
		/// </summary>
		/// <param name="Index">Index.</param>
		/// <param name="aView">A view.</param>
		internal DSGridRowView (int Index, DSGridView aView) : this (aView)
		{
			this.Processor.RowIndex = Index;

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

			ReDraw ();
		}

		#endregion

		#region Private functions

		/// <summary>
		/// ReDraw the row view
		/// </summary>
		private void ReDraw ()
		{
			foreach (var cel in Processor.Columns)
			{
				var cell = Processor.Cells [cel.xPosition] as DSGridCellView;
				
				if (cell == null)
				{
					cell = new DSGridCellView (GridView, this);
					Processor.Cells.Add (cell);

				}
					
				cell.Processor.Style = this.Processor.Style;
				
				if (Processor.RowIndex != 0)
					cell.Processor.IsOdd = (Processor.RowIndex % 2) != 0;
				
				cell.Processor.ColumnIndex = cel.xPosition;
				cell.Processor.RowIndex = this.Processor.RowIndex;

				var aRect = cel.Frame.ToRectangleF ();
				aRect.Height = this.Frame.Height;
				cell.Frame = aRect.Integral ();

				cell.Processor.IsSelected = Processor.IsSelected;
				cell.Processor.IsReadOnly = cel.IsReadOnly;

				//work out the position style
				cell.Processor.ColumnName = cel.Name;
				cell.Processor.PositionType = Processor.CalculatePosStyle (cell);
				cell.Processor.Formatter = cel.Formatter;
				cell.Processor.SortStyle = cel.SortStyle;

				if (cell.Superview == null)
					this.InsertSubview (cell, 0);

				
			}
		}
						
		#endregion

		/// <summary>
		/// Tears down.
		/// </summary>
		public void TearDown ()
		{

			Processor.Dispose();

		}

		/// <summary>
		/// Detaches the view for its parent
		/// </summary>
		public void DetachView()
		{
			if (this.Superview != null)
			{
				this.RemoveFromSuperview();
			}
		}
	}
}

