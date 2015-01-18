// ****************************************************************************
// <copyright file="DSCellProcessor.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Grid.Interfaces;
using DSoft.Datatypes.Grid.Data;
using DSoft.Datatypes.Base;

namespace DSoft.Datatypes.Grid.Shared
{
	/// <summary>
	/// Cell Processor
	/// </summary>
	public class DSCellProcessor : IDisposable
	{

		#region Fields

		private bool mIsSelected;
		private CellStyle mStyle;
		private IDSGridRowView mRowView;
		private SortIndicatorStyle mSortStyle;
		private int mRowIndex;
		private DSDataValue mValue;

		private IDSDataGridView mGridView;

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
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Grid.Shared.DSCellProcessor"/> class.
		/// </summary>
		/// <param name="viewInvalidatedAction">View invalidated action.</param>
		public DSCellProcessor(Action viewInvalidatedAction)
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
		/// Is this an odd cell
		/// </summary>
		public bool IsOdd;

		/// <summary>
		/// gets if the cell is selected
		/// </summary>
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

					ViewInvalidatedAction();

					OnSelectionStateChanged(OnSelectionStateChanged, null);
				}
			}
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

					ViewInvalidatedAction();
				}

			}
		}

		/// <summary>
		/// The name of the column.
		/// </summary>
		public String ColumnName;

		/// <summary>
		/// The type of the position.
		/// </summary>
		public CellPositionType PositionType;

		/// <summary>
		/// The sort style.
		/// </summary>
		public SortIndicatorStyle SortStyle {
			get
			{
				return mSortStyle;
			}
			set
			{
				if (mSortStyle != value)
				{
					mSortStyle = value;

					ViewInvalidatedAction();
				}
			}
		}

		/// <summary>
		/// Gets or sets the formatter.
		/// </summary>
		/// <value>The formatter.</value>
		public DSFormatter Formatter { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is read only.
		/// </summary>
		/// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
		public bool IsReadOnly { get; set; }

		/// <summary>
		/// Calculated index
		/// </summary>
		/// <value>The index.</value>
		public int Index 
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
		public DSDataValue ValueObject
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

		/// <summary>
		/// The Column Index of the cell within the grid
		/// </summary>
		public int ColumnIndex { get; set; }

		/// <summary>
		/// The Row Index of the cell within the grid
		/// </summary>
		public int RowIndex {
			get { return mRowIndex; }
			set
			{
				if (mRowIndex != value)
				{
					mRowIndex = value;

					ViewInvalidatedAction();
				}
			} 
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
		/// Gets or sets the grid row view.
		/// </summary>
		/// <value>The grid row view.</value>
		public IDSGridRowView GridRowView
		{
			get {return mRowView;}
			set {mRowView = value;}
		}

		#endregion

		/// <summary>
		/// Handles the property changed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandlePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			ViewInvalidatedAction();
		}

		/// <summary>
		/// Dids the single tap.
		/// </summary>
		/// <param name="cell">Cell.</param>
		public void DidSingleTap (IDSGridCellView cell)
		{
			if (GridView == null)
				return;

			if (Style == CellStyle.Header)
			{
				GridView.HandleOnHeaderCellTapped (cell);
			}
			else
			{

				GridView.HandleOnCellSingleTap (cell);
				GridView.HandleOnRowSingleSelect (GridRowView);
				GridView.HandleOnSelectedRowChanged (RowIndex);
			}

		}

		/// <summary>
		/// Dids the double tap.
		/// </summary>
		/// <param name="cell">Cell.</param>
		public void DidDoubleTap (IDSGridCellView cell)
		{
			if (GridView == null)
				return;

			GridView.HandleOnSelectedRowChanged (RowIndex);
			GridView.HandleOnCellDoubleTap (cell);
			GridView.HandleOnRowDoubleSelect (GridRowView);
		}

		/// <summary>
		/// Releases all resource used by the <see cref="DSoft.Datatypes.Grid.Shared.DSCellProcessor"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the
		/// <see cref="DSoft.Datatypes.Grid.Shared.DSCellProcessor"/>. The <see cref="Dispose"/> method leaves the
		/// <see cref="DSoft.Datatypes.Grid.Shared.DSCellProcessor"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the
		/// <see cref="DSoft.Datatypes.Grid.Shared.DSCellProcessor"/> so the garbage collector can reclaim the memory that the
		/// <see cref="DSoft.Datatypes.Grid.Shared.DSCellProcessor"/> was occupying.</remarks>
		public void Dispose()
		{
			mGridView = null;
			mRowView = null;
			mValue = null;
			mViewInvalidatedAction = null;
		}
	}


}

