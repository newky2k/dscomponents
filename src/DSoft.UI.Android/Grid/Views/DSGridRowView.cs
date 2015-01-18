// ****************************************************************************
// <copyright file="DSGridRowView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Text;
using Android.Graphics;
using DSoft.Datatypes.Grid.MetaData.Collections;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Grid.Interfaces;
using DSoft.Datatypes.Grid.Shared;
using Android.App;

namespace DSoft.UI.Grid.Views
{
	/// <summary>
	/// DSGridRowView
	/// </summary>
	public class DSGridRowView : LinearLayout, IDSGridRowView
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
						if (this.Context is Activity)
						{
							((Activity)Context).RunOnUiThread(()=>
							{
								Invalidate();
							});
						}

					});

					mProcessor.OnSelectionStateChanged += (object sender, EventArgs e) => 
					{
						foreach (var item in Processor.Cells)
						{
							item.Processor.IsSelected = this.Processor.IsSelected;
						}
					};
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

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridRowView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public DSGridRowView (Context context) :
			base (context)
		{
			//Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridRowView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public DSGridRowView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			//Initialize ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridRowView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		/// <param name="defStyle">Def style.</param>
		public DSGridRowView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			//Initialize ();
		}
			
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridRowView"/> class.
		/// </summary>
		/// <param name="GridView">Grid view.</param>
		public DSGridRowView (DSGridView GridView) : base (GridView.Context)
		{
			Processor.GridView = GridView;

			//Initialize ();
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

		#region Methods

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public void Initialize ()
		{
			this.SetBackgroundColor (Color.Transparent);
			this.Orientation = Orientation.Horizontal;


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

				var lp = new LinearLayout.LayoutParams (Context.ToDevicePixels ((int)cel.width), LayoutParams.FillParent);
				lp.Gravity = GravityFlags.DisplayClipHorizontal;
				cell.LayoutParameters = lp;

				cell.Processor.IsSelected = Processor.IsSelected;
				cell.Processor.IsReadOnly = cel.IsReadOnly;

				//work out the position style
				cell.Processor.ColumnName = cel.Name;
				cell.Processor.PositionType = Processor.CalculatePosStyle (cell);
				cell.Processor.Formatter = cel.Formatter;
				cell.Processor.SortStyle = cel.SortStyle;

				cell.Initialize();

				if (cell.Parent != null)
				{
					if (cell.Parent is ViewGroup)
					{
						var par = cell.Parent as ViewGroup;

						par.RemoveView(cell);
					}

				}
				this.AddView (cell);


			}

		}

		/// <summary>
		/// Tears down the view
		/// </summary>
		public void TearDown()
		{

		}

		/// <summary>
		/// Detaches the view for its parent
		/// </summary>
		public void DetachView()
		{

		}
			
		#endregion

	}
}

