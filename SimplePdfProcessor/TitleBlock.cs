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
	public class TitleBlock
	{
		//public i4nt pageIndex {get; set;}
		int dX;
		int dY;
		int width; //width in millimeters
		int height; //height in millimeters
		double Width{ //width in points
			get
			{
				return width/0.3528;
			}
		}
		
		double Height{  //height in points
			get
			{
				return height/0.3528;
			}
		}
		
		List<int[]> TitleBlockFields; //here will be coordinates of rectangles to fill
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
			this.width = width;
			this.height = height;
			this.TitleBlockFields = new List<int[]>{
				new int[]{0, 0, this.width, this.height}
			};
		}
		
		public void Draw(PdfPage page)
		{
			if (page == null)
				throw new ArgumentNullException("page is null");
			foreach (int[] fields in TitleBlockFields)
			{
				if (fields.Length != 4)
				{
					throw new Exception("field must have 4 points!");
				}
				DrawOrientedRectangle(page, //TODO: where is base point??
			}
		}
		
		void DrawOrientedRectangle(PdfPage page, int X1, int Y1, int X2, int Y2)
		{
			if (page == null)
				throw new ArgumentNullException("page is null");
			double x1 = X1/0.3528;
			double y1 = Y1/0.3528;
			double x2 = X2/0.3528;
			double y2 = Y2/0.3528;
			XGraphics gfx = XGraphics.FromPdfPage(page);
        	switch (page.Orientation){
        		case PageOrientation.Portrait:
        			gfx.DrawRectangle(XBrushes.Black, x1, y1, x2, y2);
        			break;
        		case PageOrientation.Landscape:
        			gfx.DrawRectangle(XBrushes.Black, page.Width-y1, page.Height-x1, page.Width-y2, page.Height-x2);
        			break;
        		default:
        			throw new Exception("Invalid value for PageOrientation");
        	}
		}
	}
}
