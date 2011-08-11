using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using PdfSharp;
using PdfSharp.Drawing;
using System.Diagnostics;

namespace SimplePdfProcessor
{
    // <summary>
    // Логика взаимодействия для RotatePagesWindow.xaml
    // </summary>
    public partial class RotatePagesWindow : Window
    {
        string filename; //имя обрабатываемого файла
        PdfDocument pdfDoc = null;

        public RotatePagesWindow()
        {
            InitializeComponent();
        }

        private void radiobutton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ActivitySelected();
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

        private void btnGO_Click(object sender, RoutedEventArgs e)
        {
            ActivitySelected();
        }

        private void ActivitySelected()
        {
            int rotateAngle = 0;
            if (true == rdClockwise.IsChecked)
            {
                rotateAngle = 90;
            }
            if (true == rdCounterClockwise.IsChecked)
            {
                rotateAngle = 270;
            }
            if (true == rdFlip.IsChecked)
            {
                rotateAngle = 180;
            }
            foreach (PdfPage page in pdfDoc.Pages)
            {
                page.Rotate = rotateAngle;
            }
            DialogResult = PDFDoc.SavePdf(pdfDoc, true);
        }
    }
}
