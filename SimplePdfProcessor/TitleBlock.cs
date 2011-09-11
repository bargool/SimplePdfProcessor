/*
 * Created by SharpDevelop.
 * User: aleksey
 * Date: 09.09.2011
 * Time: 22:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace SimplePdfProcessor
{
	/// <summary>
	/// Description of TitleBlock.
	/// </summary>
	public abstract class TitleBlock
	{
		int dX;
		int dY;
		public int Width {get; set;} //width in millimeters
		public int Height {get; set;} //height in millimeters
		
		public List<int[]> TitleBlockFields; //here will be coordinates of rectangles to fill
		//Координаты должны быть относительно нижнего правого угла
		
		//constructors
		public TitleBlock()
			: this(5, 5) {}
		
		public TitleBlock(int dx, int dy)
			: this(dx, dy, 185, 15)	{}
		
		public TitleBlock(int dx, int dy, int width, int height)
		{
			dX = dx;
			dY = dy;
			this.Width = width;
			this.Height = height;
			FormTitleBlockFields();
		}
		
		public void Draw(PdfPage page)
		{
			if (page == null)
				throw new ArgumentNullException("page is null");
			XGraphics gfx = XGraphics.FromPdfPage(page);
			foreach (int[] fields in TitleBlockFields)
			{
				if (fields.Length != 4)
				{
					throw new Exception("field must have 4 points!");
				}
				DrawOrientedRectangle(page, gfx, fields[0], fields[1],
				                      fields[2], fields[3]);
			}
		}

		public abstract void FormTitleBlockFields();
//		{
//			this.TitleBlockFields = new List<int[]>
//			{
//				new int[]{0, 0, this.Width, this.Height}
//			};
//		}
		
		private void DrawOrientedRectangle(PdfPage page, XGraphics gfx,
		                                   int X, int Y, int fieldWidth, int fieldHeight)
		{
			if (page == null)
				throw new ArgumentNullException("page is null");
			
			double x = (X+dX+fieldWidth)/0.3528;
			double y = (Y+dY+fieldHeight)/0.3528;
			double fWidth = fieldWidth/0.3528;
			double fHeight = fieldHeight/0.3528;
        	switch (page.Orientation)
        	{
        		case PageOrientation.Portrait:
        			gfx.DrawRectangle(XBrushes.Black,
					                  page.Width-x, page.Height-y,
					                  fWidth, fHeight);
        			break;
        		case PageOrientation.Landscape:
        			gfx.DrawRectangle(XBrushes.Black,
        			                  y, page.Height-x,
        			                  fHeight, fWidth);
        			break;
        		default:
        			throw new Exception("Invalid value for PageOrientation");
        	}
		}
	}
}
