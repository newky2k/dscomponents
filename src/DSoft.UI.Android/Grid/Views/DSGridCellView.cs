// ****************************************************************************
// <copyright file="DSGridCellView.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using Android.Content;
using Android.Util;
using Android.Widget;
using Android.Views;
using Android.Text;
using Android.Graphics;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Grid.Interfaces;
using DSoft.Datatypes.Grid.Shared;
using DSoft.Datatypes.Types;
using DSoft.Datatypes.Formatters;
using Android.OS;
using System.Threading;
using Android.App;

namespace DSoft.UI.Grid.Views
{
	/// <summary>
	/// DSGridCellView definition
	/// </summary>
	public class DSGridCellView : FrameLayout, IDSGridCellView
	{
	
		#region Fields
		private DSCellProcessor mProcessor;
		private View mContentView;
		Timer myTapHandler;
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
						if (this.Context is Activity)
						{
							((Activity)Context).RunOnUiThread(()=>
							{

								var backColor = DSColor.Clear;

								switch (Processor.Style)
								{
									case CellStyle.Blank:
										{
											backColor = GridView.Theme.BackgroundColor;
										}
										break;
									case CellStyle.Cell:
										{
											if (Processor.IsSelected)
											{
												backColor = GridView.Theme.CellBackgroundHighlight;

											}
											else
											{
												var alterColor = (GridView.Theme.CellBackground2 != null) ? GridView.Theme.CellBackground2
													: GridView.Theme.CellBackground;
												backColor = (Processor.IsOdd) ? GridView.Theme.CellBackground : alterColor;
											}
										}
										break;
									case CellStyle.Header:
										{
											backColor = GridView.Theme.HeaderBackground;
										}
										break;
								}

								this.SetBackgroundColor (backColor.ToAndroidColor());

								if (mContentView != null && mContentView is TextView)
								{
									var label = mContentView as TextView;

									if (Processor.Style == CellStyle.Header)
									{
										//label.TextAlignment = (UITextAlignment)GridView.Theme.HeaderTextAlignment;
										label.SetTextColor(GridView.Theme.HeaderTextForeground.ToAndroidColor ());
									}
									else
									{
										var alterColor = (GridView.Theme.CellTextForeground2 != null) ? GridView.Theme.CellTextForeground2.ToAndroidColor ()
											: GridView.Theme.CellTextForeground.ToAndroidColor ();
										var aColor = (Processor.IsOdd) ? GridView.Theme.CellTextForeground.ToAndroidColor () : alterColor;


										label.SetTextColor((Processor.IsSelected) ? GridView.Theme.CellTextHighlight.ToAndroidColor () : aColor);

										//label.TextAlignment = (UITextAlignment)boolFormatter.TextAlignment;
									}
										
								}
							});
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
		public DSGridView GridView
		{
			get
			{
				return Processor.GridView as DSGridView;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridCellView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public DSGridCellView (Context context) :
			base (context)
		{
		}
			
		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridCellView"/> class.
		/// </summary>
		/// <param name="GridView">Grid view.</param>
		/// <param name="RowView">Row view.</param>
		public DSGridCellView (DSGridView GridView, DSGridRowView RowView) 
			: this (GridView.Context)
		{
			Processor.GridView = GridView;
			Processor.GridRowView = RowView;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridCellView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		public DSGridCellView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.UI.Grid.Views.DSGridCellView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="attrs">Attrs.</param>
		/// <param name="defStyle">Def style.</param>
		public DSGridCellView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{

		}

		#endregion

		#region Methods

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		public void Initialize ()
		{
			this.RemoveAllViews();

			var backColor = DSColor.Clear;

			switch (Processor.Style)
			{
				case CellStyle.Blank:
					{
						backColor = GridView.Theme.BackgroundColor;
					}
					break;
				case CellStyle.Cell:
					{
						if (Processor.IsSelected)
						{
							backColor = GridView.Theme.CellBackgroundHighlight;

						}
						else
						{
							var alterColor = (GridView.Theme.CellBackground2 != null) ? GridView.Theme.CellBackground2
								: GridView.Theme.CellBackground;
							backColor = (Processor.IsOdd) ? GridView.Theme.CellBackground : alterColor;
						}
					}
					break;
				case CellStyle.Header:
					{
						backColor = GridView.Theme.HeaderBackground;
					}
					break;
			}
	
			this.SetBackgroundColor (backColor.ToAndroidColor());

			if (Processor.Style == CellStyle.Blank)
				return;
						
			if (Processor.Style != CellStyle.Blank && Processor.ValueObject.Value != null)
			{
				//update to work with different datatypes
				//check to see if the formatter is a DSViewFormatter
				if (Processor.Formatter != null && Processor.Formatter is DSViewFormatter)
				{
					var viewFormatter = Processor.Formatter as DSViewFormatter;

					if (!(viewFormatter.View is View))
						throw new Exception ("DSViewFormatter has type that isn't a View");

					var aView = viewFormatter.View as View;

					viewFormatter.View.Value = Processor.ValueObject.Value;

					var lp = new LinearLayout.LayoutParams (LayoutParams.WrapContent, LayoutParams.FillParent);

					if (Processor.Formatter.Size != null)
					{
						var width = Context.ToDevicePixels((int)Processor.Formatter.Size.Width);
						var height = Context.ToDevicePixels((int)Processor.Formatter.Size.Height);

						lp = new LinearLayout.LayoutParams (width, height);

					}

					aView.LayoutParameters = lp;
					viewFormatter.View.IsReadOnly = Processor.IsReadOnly;
					viewFormatter.View.UpdateAction = (obj) => {
						GridView.Processor.SetValue (Processor.Index, Processor.ColumnName, obj);
					};
					//aView.IsReadOnly = 
					mContentView = (View)aView;
					this.AddView (mContentView);

				}
				else if (Processor.ValueObject.Value is Bitmap || Processor.ValueObject.Value is DSBitmap)
				{
					var imgView = new ImageView(this.Context);

					Bitmap image = null;

					if (Processor.ValueObject.Value is Bitmap)
					{
						image = Processor.ValueObject.Value as Bitmap;
					}
					else if (Processor.ValueObject.Value is DSBitmap)
					{
						image = ((DSBitmap)Processor.ValueObject.Value).ToBitmap();
					}

					imgView.SetImageBitmap(image);

					var lp = new LinearLayout.LayoutParams (LayoutParams.WrapContent, LayoutParams.FillParent);

					if (Processor.Formatter.Size != null)
					{
						var width = Context.ToDevicePixels((int)Processor.Formatter.Size.Width);
						var height = Context.ToDevicePixels((int)Processor.Formatter.Size.Height);

						lp = new LinearLayout.LayoutParams (width, height, 1.0f);

					}

					lp.Gravity = GravityFlags.CenterHorizontal | GravityFlags.CenterVertical;


					if (Processor.Formatter != null && Processor.Formatter is DSImageFormatter)
					{
						var formta = Processor.Formatter as DSImageFormatter;

						if (formta.Margin != null)
						{
							//apply the margins to the frame
							var inset = formta.Margin;
							lp.SetMargins((int)inset.Left, (int)inset.Top,(int)inset.Right,(int)inset.Bottom);
						}
					}
//
					imgView.LayoutParameters = lp;

					var imgContainer = new LinearLayout(this.Context);
					var imgLp = new LinearLayout.LayoutParams(LayoutParams.FillParent,LayoutParams.FillParent);
					imgLp.Gravity = GravityFlags.Center;
					imgContainer.LayoutParameters = imgLp;

					imgContainer.AddView(imgView);

					mContentView = imgContainer;
					this.AddView (mContentView);
				}
				else if (Processor.ValueObject.Value is Boolean && Processor.Formatter != null && Processor.Formatter is DSBooleanFormatter)
				{
					var boolFormatter = Processor.Formatter as DSBooleanFormatter;

					if (boolFormatter.Style == BooleanFormatterStyle.Text)
					{
						var label = new TextView (this.Context);
						var lp = new LinearLayout.LayoutParams (LayoutParams.WrapContent, LayoutParams.FillParent);
						lp.Gravity = GravityFlags.DisplayClipHorizontal;

						label.LayoutParameters = lp;

						var fontSize = (Processor.Style == CellStyle.Header) ? GridView.Theme.HeaderTextFont.FontSize : GridView.Theme.CellTextFont.FontSize;
						label.SetTextSize (ComplexUnitType.Dip, fontSize);

						label.SetBackgroundColor (Color.Transparent);
						label.Text = ((bool)Processor.ValueObject.Value) ? boolFormatter.TrueValue.ToString() : boolFormatter.FalseValue.ToString();
						label.SetSingleLine (true); 
						label.Ellipsize = TextUtils.TruncateAt.End;


						TextAlignment textAlign;


						if (Processor.Style == CellStyle.Header)
						{
							textAlign = GridView.Theme.HeaderTextAlignment;
							label.SetTextColor(GridView.Theme.HeaderTextForeground.ToAndroidColor ());
						}
						else
						{
							var alterColor = (GridView.Theme.CellTextForeground2 != null) ? GridView.Theme.CellTextForeground2.ToAndroidColor ()
								: GridView.Theme.CellTextForeground.ToAndroidColor ();
							var aColor = (Processor.IsOdd) ? GridView.Theme.CellTextForeground.ToAndroidColor () : alterColor;


							label.SetTextColor((Processor.IsSelected) ? GridView.Theme.CellTextHighlight.ToAndroidColor () : aColor);

							textAlign = boolFormatter.TextAlignment;
						}


						var gavFlags = GravityFlags.CenterVertical | GravityFlags.Left;

						switch (textAlign)
						{
							case TextAlignment.Middle:
								{
									gavFlags = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;
								}
								break;
							case TextAlignment.Right:
								{
									gavFlags = GravityFlags.CenterVertical | GravityFlags.Right;
								}
								break;
						}
						label.Gravity = gavFlags;

						mContentView = label;
						this.AddView (mContentView);
					}
					else
					{
						var bValue = ((bool)Processor.ValueObject.Value) ? boolFormatter.TrueValue : boolFormatter.FalseValue;

						Bitmap image = null;

						if (bValue != null)
						{
							if (bValue is Bitmap)
							{
								image = bValue as Bitmap;
							}
							else if (bValue is DSBitmap)
							{
								image = ((DSBitmap)bValue).ToBitmap();
							}

						}

							
						if (image != null)
						{
							var imgView = new ImageView(this.Context);
							imgView.SetImageBitmap(image);

							var lp = new LinearLayout.LayoutParams (LayoutParams.WrapContent, LayoutParams.WrapContent);

							if (Processor.Formatter.Size != null)
							{
								var width = Context.ToDevicePixels((int)Processor.Formatter.Size.Width);
								var height = Context.ToDevicePixels((int)Processor.Formatter.Size.Height);

								lp = new LinearLayout.LayoutParams (width, height, 1.0f);

							}

							lp.Gravity = GravityFlags.CenterHorizontal | GravityFlags.CenterVertical;

							imgView.LayoutParameters = lp;

							var imgContainer = new LinearLayout(this.Context);
							var imgLp = new LinearLayout.LayoutParams(LayoutParams.FillParent,LayoutParams.FillParent);
							imgLp.Gravity = GravityFlags.Center;
							imgContainer.LayoutParameters = imgLp;

							imgContainer.AddView(imgView);

							mContentView = imgContainer;
							this.AddView (mContentView);
						}
					}
				}
				else
				{
					var label = new TextView (this.Context);
					var lp = new LinearLayout.LayoutParams (LayoutParams.WrapContent, LayoutParams.FillParent);
					lp.Gravity = GravityFlags.DisplayClipHorizontal;

					label.LayoutParameters = lp;

					label.SetTextSize (ComplexUnitType.Dip, GridView.Theme.CellTextFont.FontSize);

					label.SetBackgroundColor (Color.Transparent);
					label.Text = Processor.ValueObject.Value.ToString ();
					label.SetSingleLine (true); 
					label.Ellipsize = TextUtils.TruncateAt.End;


					TextAlignment textAlign;


					if (Processor.Style == CellStyle.Header)
					{
//						int numLines = Processor.ValueObject.Value.ToString ().Split ('\n').Length;
//						label.Lines = (label.Frame.Height >= 13) ? numLines : 1;

						textAlign = GridView.Theme.HeaderTextAlignment;

						if (Processor.Formatter != null && Processor.Formatter is DSTextFormatter)
						{
							textAlign = ((DSTextFormatter)Processor.Formatter).Alignment;

						}


						label.SetTextColor(GridView.Theme.HeaderTextForeground.ToAndroidColor ());
					}
					else
					{
						var alterColor = (GridView.Theme.CellTextForeground2 != null) ? GridView.Theme.CellTextForeground2.ToAndroidColor () : GridView.Theme.CellTextForeground.ToAndroidColor ();
						var aColor = (Processor.IsOdd) ? GridView.Theme.CellTextForeground.ToAndroidColor () : alterColor;

						label.SetTextColor((Processor.IsSelected) ? GridView.Theme.CellTextHighlight.ToAndroidColor () : aColor);

						textAlign = GridView.Theme.CellContentAlignment;

						if (Processor.Formatter != null && Processor.Formatter is DSTextFormatter)
						{
							textAlign = ((DSTextFormatter)Processor.Formatter).Alignment;

						}

					}

					//align the text
					var gavFlags = GravityFlags.CenterVertical | GravityFlags.Left;

					switch (textAlign)
					{
						case TextAlignment.Middle:
							{
								gavFlags = GravityFlags.CenterVertical | GravityFlags.CenterHorizontal;
							}
							break;
						case TextAlignment.Right:
							{
								gavFlags = GravityFlags.CenterVertical | GravityFlags.Right;
							}
							break;
					}
					label.Gravity = gavFlags;

					mContentView = label;
					this.AddView (mContentView);
				}

			}
				
			if (Processor.Style == CellStyle.Header && Processor.SortStyle != SortIndicatorStyle.None)
			{

				//draw the up indicator
				var anImage = (Processor.SortStyle == SortIndicatorStyle.Ascending) ? GridView.Theme.HeaderSortIndicatorUp : 
					GridView.Theme.HeaderSortIndicatorDown;


				if (anImage != null)
				{
					var aUIImage = anImage.ToBitmap ();

					var aView = new ImageView (this.Context);
					aView.SetImageBitmap(aUIImage);

					var lp = new FrameLayout.LayoutParams(this.Context.ToDevicePixels(16),this.Context.ToDevicePixels(8),GravityFlags.Right | GravityFlags.CenterVertical);
					lp.SetMargins(0,0,this.Context.ToDevicePixels(3),0);
					aView.LayoutParameters = lp;

					this.AddView (aView);
				}
			}

		}

		/// <summary>
		/// Tears down the view
		/// </summary>
		public void TearDown()
		{
			Processor.Dispose();
		}

		/// <summary>
		/// Detaches the view for its parent
		/// </summary>
		public void DetachView()
		{

		}
		#endregion

		#region Overrides
		/// <Docs>The motion event.</Docs>
		/// <returns>To be added.</returns>
		/// <para tool="javadoc-to-mdoc">Implement this method to handle touch screen motion events.</para>
		/// <format type="text/html">[Android Documentation]</format>
		/// <since version="Added in API level 1"></since>
		/// <summary>
		/// Raises the touch event event.
		/// </summary>
		/// <param name="ev">Ev.</param>
		public override bool OnTouchEvent (MotionEvent ev)
		{
			switch(ev.Action)
			{
				case MotionEventActions.Up:
					{
						if (myTapHandler != null)
						{
							myTapHandler.Change(Timeout.Infinite, Timeout.Infinite);
							myTapHandler.Dispose();
							myTapHandler = null;

							Processor.DidDoubleTap(this);
						}
						else
						{
							myTapHandler = new Timer((s)=>
							{
								if (myTapHandler != null)
								{
									myTapHandler.Change(Timeout.Infinite, Timeout.Infinite);

									myTapHandler = null;

									Processor.DidSingleTap(this);
								}


							},null,TimeSpan.FromMilliseconds(200.0f),TimeSpan.FromMilliseconds(200.0f));
						}
					}
					break;

			}
			//this.SetBackgroundColor (Android.Graphics.Color.Green);

			return true;
		}

		#endregion
	}
		
}

