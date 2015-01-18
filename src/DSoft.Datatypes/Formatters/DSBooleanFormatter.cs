// ****************************************************************************
// <copyright file="DSBooleanFormatter.cs" company="DSoft Developments">
//    Created By David Humphreys
//    Copyright © David Humphreys 2015
// </copyright>
// ****************************************************************************

using System;
using DSoft.Datatypes.Enums;
using DSoft.Datatypes.Base;
using DSoft.Datatypes.Types;

namespace DSoft.Datatypes.Formatters
{
	/// <summary>
	/// Formatter class for Boolean Cells
	/// </summary>
	public class DSBooleanFormatter : DSFormatter
	{
		#region Fields

		private DSSize mSize;
		private BooleanFormatterStyle mStyle;
		private TextAlignment mTextAlignment;
		private object mTrueValue;
		private object mFalseValue;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the true value either the text to appear or name of the image to show
		/// </summary>
		/// <value>The true value.</value>
		public object TrueValue {
			get
			{
				if (mTrueValue == null)
				{
					switch (Style)
					{
						case BooleanFormatterStyle.Text:
							{
								return "True";
							}
						case BooleanFormatterStyle.Image:
							{
								return "checkmark.png";
							}	
						default:
							{
								return string.Empty;
							}
					}
				}

				return mTrueValue;
			}
			set
			{
				mTrueValue = value;
			}
		}

		/// <summary>
		/// Gets or sets the false value either the text to appear or name of the image to show
		/// </summary>
		/// <value>The false value.</value>
		public object FalseValue {
			get
			{
				if (mFalseValue == null)
				{
					switch (Style)
					{
						case BooleanFormatterStyle.Text:
							{
								return "False";
							}
						case BooleanFormatterStyle.Image:
							{
								return string.Empty;
							}	
						default:
							{
								return string.Empty;
							}
					}
				}

				return mFalseValue;
			}
			set
			{
				mFalseValue = value;
			}
		}

		/// <summary>
		/// Gets or sets the style of the boolean column
		/// </summary>
		/// <value>The style.</value>
		public BooleanFormatterStyle Style { 
			get
			{
				return mStyle;
			} 
			set
			{
				mStyle = value;
			}
		}

		/// <summary>
		/// Gets or sets the text alignment when style is Text
		/// </summary>
		/// <value>The text alignment.</value>
		public TextAlignment TextAlignment {
			get
			{
				return mTextAlignment;
			}
			set
			{
				mTextAlignment = value;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Formatters.DSBooleanFormatter"/> class.
		/// </summary>
		/// <param name="Style">Style.</param>
		/// <param name="TrueValue">True value.</param>
		/// <param name="FalseValue">False value.</param>
		public DSBooleanFormatter (BooleanFormatterStyle Style, object TrueValue, object FalseValue) : this (Style)
		{

			mTrueValue = TrueValue;
			mFalseValue = FalseValue;


		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DSoft.Datatypes.Formatters.DSBooleanFormatter"/> class.
		/// </summary>
		/// <param name="Style">Style.</param>
		public DSBooleanFormatter (BooleanFormatterStyle Style)
		{
			mStyle = Style;
			mSize = new DSSize (20, 20);
		}

		#endregion

		#region implemented abstract members of DSFormatter

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>The size.</value>
		public override DSoft.Datatypes.Types.DSSize Size {
			get
			{
				return mSize;
			}
			set
			{
				mSize = value;
			}
		}

		#endregion
	}
}

