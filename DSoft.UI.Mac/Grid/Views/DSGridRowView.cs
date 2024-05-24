// ****************************************************************************
// <copyright file="DSGridRowView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Enums;
using DSoft.UI.Mac.Grid.Views.Collections;
using DSoft.Datatypes.Grid.Interfaces;
using AppKit;
using DSoft.Datatypes.Grid.MetaData.Collections;
using CoreGraphics;
using DSoft.UI.Mac.Extensions;

namespace DSoft.UI.Mac.Grid.Views
{
	/// <summary>
	/// DSGrid row view
	/// </summary>
	public class DSGridRowView : NSView, IDSGridRowView
	{
		#region Fields

		private DSGridCellViewCollection mCells;
		private int mRowIndex = -1;
		private bool mIsSelected;
		private DSGridView mGridView;
		private CellStyle mStyle = CellStyle.Blank;
		private NSColor BackgroundColor;
		#endregion

		#region Properties

		/// <summary>
		/// The style of the cell
		/// </summary>
		public CellStyle Style {
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
		/// Gets the index of the row.
		/// </summary>
		/// <value>The index of the row.</value>
		public int RowIndex {
			get
			{
				if (mRowIndex != 0 && Style == CellStyle.Cell)
				{
					return mRowIndex - 1;
				}
				return mRowIndex;
			}
			set
			{
				if (mRowIndex != value)
				{
					mRowIndex = value;

					this.SetNeedsDisplay ();
				}

			}
		}

		public int RealRowIndex {
			get
			{
				return mRowIndex;
			}
		}

		/// <summary>
		/// Gets the columns.
		/// </summary>
		/// <value>The columns.</value>
		private DSGridViewCellInfoCollection Columns {
			get
			{
				return mGridView.Processor.ColDefs;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is selected.
		/// </summary>
		/// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
		public bool IsSelected {
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
		/// The type of the position.
		/// </summary>
		public RowPositionType PositionType {get; set;}

		/// <summary>
		/// Gets or sets the row identifier.
		/// </summary>
		/// <value>The row identifier.</value>
		public string RowId {get; set;}
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
			mCells = new DSGridCellViewCollection ();
			mGridView = aView;
			//this.Opaque = true;
			this.BackgroundColor = NSColor.Clear;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridRowView"/> class.
		/// </summary>
		/// <param name="Index">Index.</param>
		/// <param name="aView">A view.</param>
		internal DSGridRowView (int Index, DSGridView aView) : this (aView)
		{
			this.RowIndex = Index;

		}

		#endregion

		#region Overrides

		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void DrawRect (CGRect rect)
		{
			base.DrawRect(rect);

			ReDraw ();
		}

		#endregion

		#region Private functions

		/// <summary>
		/// ReDraw the row view
		/// </summary>
		private void ReDraw ()
		{
			foreach (var cel in Columns)
			{
				var cell = mCells [cel.xPosition];

				if (cell == null)
				{
					cell = new DSGridCellView (this.mGridView, this);
					mCells.Add (cell);

				}

				cell.Style = this.Style;

				if (RowIndex != 0)
					cell.IsOdd = (RowIndex % 2) != 0;

				cell.ColumnIndex = cel.xPosition;
				cell.RowIndex = this.RowIndex;

				var aRect = cel.Frame.ToRectangleF ();
				aRect.Height = this.Frame.Height;
				cell.Frame = aRect.Integral ();
				cell.IsSelected = IsSelected;
				cell.IsReadOnly = cel.IsReadOnly;

				//work out the position style
				cell.ColumnName = cel.Name;
				cell.PositionType = CalculatePosStyle (cell);
				cell.Formatter = cel.Formatter;
				cell.SortStyle = cel.SortStyle;

				if (cell.Superview == null)
					this.AddSubview(cell, NSWindowOrderingMode.Below, this.Subviews[0]);
					//this.InsertSubview (cell, 0);


			}
		}

		/// <summary>
		/// Handles the cell single tapped.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void HandleCellSingleTapped (DSGridCellView sender)
		{
			mGridView.HandleOnSelectedRowChanged (RowIndex);
			mGridView.HandleOnCellSingleTap (sender);
			mGridView.HandleOnRowSingleSelect (this);

		}

		/// <summary>
		/// Handles the cell double tapped.
		/// </summary>
		/// <param name="sender">Sender.</param>
		void HandleCellDoubleTapped (DSGridCellView sender)
		{
			mGridView.HandleOnSelectedRowChanged (RowIndex);
			mGridView.HandleOnCellDoubleTap (sender);
			mGridView.HandleOnRowDoubleSelect (this);

		}

		/// <summary>
		/// Calculates the position style.
		/// </summary>
		/// <returns>The position style.</returns>
		/// <param name="cell">Cell.</param>
		private CellPositionType CalculatePosStyle (DSGridCellView cell)
		{
			//1) is top left
			if (cell.ColumnIndex == 0 && this.PositionType == RowPositionType.Top)
			{
				return CellPositionType.LeftTop;
			}
			else if (cell.ColumnIndex == 0 && this.PositionType == RowPositionType.Bottom)
			{
				return CellPositionType.LeftBottom;
			}
			else if (cell.ColumnIndex == 0)
			{
				return CellPositionType.LeftMiddle;
			}
			else if (cell.ColumnIndex == Columns.Count - 1 && this.PositionType == RowPositionType.Top)
			{
				return CellPositionType.RightTop;
			}
			else if (cell.ColumnIndex == Columns.Count - 1 && this.PositionType == RowPositionType.Bottom)
			{
				return CellPositionType.RightBottom;
			}
			else if (cell.ColumnIndex == Columns.Count - 1)
			{
				return CellPositionType.RightMiddle;
			}
			else if (this.PositionType == RowPositionType.Top)
			{
				return CellPositionType.CenterTop;
			}
			else if (this.PositionType == RowPositionType.Bottom)
			{
				return CellPositionType.CenterBottom;
			}
			else
			{
				return CellPositionType.CenterMiddle;
			}

			//return CellPositionType.LeftTop;
		}

		#endregion

		/// <summary>
		/// Tears down.
		/// </summary>
		public void TearDown ()
		{
			mCells.Dispose ();
			mGridView = null;

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

