/*
 * Created by SharpDevelop.
 * User: aleksey.nakoryakov
 * Date: 09/02/2011
 * Time: 16:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace SimplePdfProcessor
{
	/// <summary>
	/// Interaction logic for RectangleWindow.xaml
	/// </summary>
	public partial class RectangleWindow : Window
	{
        string filename; //имя обрабатываемого файла
        PdfDocument pdfDoc = null;

		public RectangleWindow()
		{
			InitializeComponent();
		}

        private void btnGO_Click(object sender, RoutedEventArgs e)
        {
            int SizeX;
            int SizeY;

            bool isValidFrom = int.TryParse(txtSizeX.Text, out SizeX);
            bool isValidTo = int.TryParse(txtSizeY.Text, out SizeY);
            if ((!isValidFrom || !isValidTo)||(SizeX < 1)||(SizeY < 1))
            {
                MessageBox.Show("Требуются корректные размеры!");
                return;
            }
            
            foreach (PdfPage page in pdfDoc.Pages)
            {
            	DrawRectangle(page, SizeX, SizeY);
            }
            
            DialogResult = PDFDoc.SavePdf(pdfDoc, true);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Открываем файл
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "PDF files|*.pdf";
            if (true == dlg.ShowDialog())
            {
                filename = dlg.FileName;
                pdfDoc = PDFDoc.OpenPDF(filename, PdfDocumentOpenMode.Modify);
                if (pdfDoc == null)
                {
                    MessageBox.Show("При открытии файла произошла ошибка (возможно, он защищён)", "Ошибка!");
                    this.DialogResult = false;
                }
            }
            else
            {
                this.DialogResult = false;
            }

        }
        private void DrawRectangle(PdfPage page, int sizeX, int sizeY)
        {
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