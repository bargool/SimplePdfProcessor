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
		
		List<int[4]> TitleBlockFields;
		
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
		}
		
		public void Draw(PdfPage page)
		{
			if (page == null)
				throw new ArgumentNullException("page is null");
			
			double width = sizeX/0.3528;
        	double height = sizeY/0.3528;
        	double X;
        	double Y;
        	
        	XGraphics gfx = XGraphics.FromPdfPage(page);
        	switch (page.Orientation){
        		case PageOrientation.Landscape:
        			X = width;
        			Y = (double)page.Height;
        			gfx.DrawRectangle(XBrushes.Black, 0, Y-width, height, X);
        			break;
        		case PageOrientation.Portrait:
        			X = (double)page.Width;
        			Y = (double)page.Height;
        			gfx.DrawRectangle(XBrushes.Black, X-width, Y-height, X, Y);
        			break;
        		default:
        			throw new Exception("Invalid value for PageOrientation");
        	}
		}
	}
}
