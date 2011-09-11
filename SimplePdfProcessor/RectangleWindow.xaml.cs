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
		PDFDoc pdf = null;

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
            
            foreach (PdfPage page in pdf)
            {
            	BigTitleBlock title = new BigTitleBlock(5, 5, SizeX, SizeY);
            	title.Draw(page);
            }
            
            DialogResult = pdf.SavePDF(true);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        	pdf = new PDFDoc();
            bool openResult = pdf.OpenPDF(PdfDocumentOpenMode.Modify);
            if (!openResult) this.DialogResult=false;
        }
	}
}