// ****************************************************************************
// <copyright file="DSRowProcessor.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Grid.Interfaces;
using DSoft.Datatypes.Grid.Data.Collections;
using DSoft.Datatypes.Grid.MetaData.Collections;

namespace DSoft.Datatypes.Grid.Shared
{
	/// <summary>
	/// Row processor
	/// </summary>
	public class DSRowProcessor : IDisposable
	{

		#region Fields

		private IDSGridCellViewCollection mCells;
		private int mRowIndex = -1;
		private bool mIsSelected;
		private IDSDataGridView mGridView;
		private CellStyle mStyle = CellStyle.Blank;
		private Action mViewInvalidatedAction;

		#endregion

		#region Events

		/// <summary>
		/// Occurs when on selection state changed.
		/// </summary>
		public event EventHandler OnSelectionStateChanged = delegate {};

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Grid.Shared.DSRowProcessor"/> class.
		/// </summary>
		/// <param name="viewInvalidatedAction">View invalidated action.</param>
		public DSRowProcessor(Action viewInvalidatedAction)
		{
			mViewInvalidatedAction = viewInvalidatedAction;
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the view invalidated action.
		/// </summary>
		/// <value>The view invalidated action.</value>
		public Action ViewInvalidatedAction
		{
			get
			{
				if (mViewInvalidatedAction == null)
					throw new Exception("ViewInvalidatedAction must be set");

				return mViewInvalidatedAction;
			}
			set {mViewInvalidatedAction = value;}
		}

		/// <summary>
		/// Gets or sets the grid view.
		/// </summary>
		/// <value>The grid view.</value>
		public IDSDataGridView GridView
		{
			get {return mGridView;}
			set {mGridView = value;}
		}
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

					ViewInvalidatedAction ();
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

					ViewInvalidatedAction ();
				}

			}
		}

		/// <summary>
		/// Gets the index of the real row.
		/// </summary>
		/// <value>The index of the real row.</value>
		public int RealRowIndex {
			get
			{
				return mRowIndex;
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

					ViewInvalidatedAction ();

					OnSelectionStateChanged(OnSelectionStateChanged, null);
				}

			}
		}

		/// <summary>
		/// Gets the columns.
		/// </summary>
		/// <value>The columns.</value>
		public DSGridViewCellInfoCollection Columns {
			get
			{
				return GridView.Processor.ColDefs;
			}
		}

		/// <summary>
		/// The type of the position.
		/// </summary>
		public RowPositionType PositionType { get; set; }

		/// <summary>
		/// Gets or sets the row identifier.
		/// </summary>
		/// <value>The row identifier.</value>
		public string RowId {get; set;}

		/// <summary>
		/// Calculates the position style.
		/// </summary>
		/// <returns>The position style.</returns>
		/// <param name="cell">Cell.</param>
		public CellPositionType CalculatePosStyle (IDSGridCellView cell)
		{
			//1) is top left
			if (cell.Processor.ColumnIndex == 0 && this.PositionType == RowPositionType.Top)
			{
				return CellPositionType.LeftTop;
			}
			else if (cell.Processor.ColumnIndex == 0 && this.PositionType == RowPositionType.Bottom)
			{
				return CellPositionType.LeftBottom;
			}
			else if (cell.Processor.ColumnIndex == 0)
			{
				return CellPositionType.LeftMiddle;
			}
			else if (cell.Processor.ColumnIndex == Columns.Count - 1 && this.PositionType == RowPositionType.Top)
			{
				return CellPositionType.RightTop;
			}
			else if (cell.Processor.ColumnIndex == Columns.Count - 1 && this.PositionType == RowPositionType.Bottom)
			{
				return CellPositionType.RightBottom;
			}
			else if (cell.Processor.ColumnIndex == Columns.Count - 1)
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

		/// <summary>
		/// Gets the cells.
		/// </summary>
		/// <value>The cells.</value>
		public IDSGridCellViewCollection Cells
		{
			get
			{
				if (mCells == null)
					mCells = new IDSGridCellViewCollection();

				return mCells;
			}
		}
		#endregion

		#region IDisposable implementation

		/// <summary>
		/// Releases all resource used by the <see cref="DSoft.Datatypes.Grid.Shared.DSRowProcessor"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the
		/// <see cref="DSoft.Datatypes.Grid.Shared.DSRowProcessor"/>. The <see cref="Dispose"/> method leaves the
		/// <see cref="DSoft.Datatypes.Grid.Shared.DSRowProcessor"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the
		/// <see cref="DSoft.Datatypes.Grid.Shared.DSRowProcessor"/> so the garbage collector can reclaim the memory that the
		/// <see cref="DSoft.Datatypes.Grid.Shared.DSRowProcessor"/> was occupying.</remarks>
		public void Dispose()
		{
			mGridView = null;
			mCells = null;
			mViewInvalidatedAction = null;
		}

		#endregion
	}
}

