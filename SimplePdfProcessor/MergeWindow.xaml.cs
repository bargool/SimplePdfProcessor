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
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.IO;
using iTextSharp;
using System.Diagnostics;

namespace SimplePdfProcessor
{
    /// <summary>
    /// Логика взаимодействия для MergeWindow.xaml
    /// </summary>
    public partial class MergeWindow : Window
    {
        public MergeWindow()
        {
            InitializeComponent();
        }
        private void btnAddFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.Multiselect = true;
            dlg.Filter = "PDF files|*.pdf";
            if (true == dlg.ShowDialog())
            {
                foreach (string filename in dlg.FileNames)
                    lstMergeFiles.Items.Add(filename);
            }
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            List<PdfDocument> inputDocuments = new List<PdfDocument>();
            foreach (string filename in lstMergeFiles.Items)
            {
                PdfDocument pdfDoc = PDFDoc.OpenPDF(filename, PdfDocumentOpenMode.Import);
                if (pdfDoc == null)
                {
                    MessageBox.Show("При открытии файла произошла ошибка (возможно, он защищён)", "Ошибка!");
                    this.DialogResult = false;
                }
                inputDocuments.Add(pdfDoc);
            }

            PdfDocument outputDocument = new PdfDocument();

            foreach (PdfDocument pdfFile in inputDocuments)
            {
                for (int i = 0; i < pdfFile.PageCount; i++)
                {
                    outputDocument.AddPage(pdfFile.Pages[i]);
                }
            }
            DialogResult = PDFDoc.SavePdf(outputDocument, (bool)checkShowFile.IsChecked);
            //if (fname != "")
            //{
            //    DialogResult = true;
            //}
        }

        private void btnRemoveFile_Click(object sender, RoutedEventArgs e)
        {
            lstMergeFiles.Items.Remove(lstMergeFiles.SelectedItem);
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
        	//FIXME: moving up file in the list of files
            MessageBox.Show("Не реализовал пока!!");
            
            //string tempValue = "";
            //int index = lstMergeFiles.SelectedIndex;
            //tempValue = (string)lstMergeFiles.Items[index - 1];
            //lstMergeFiles.Items[index - 1] = lstMergeFiles.Items[index];
            //lstMergeFiles.Items[index] = tempValue;
            //lstMergeFiles.SelectedIndex = index - 1;
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
        	//FIXME: moving down file in the list of files
            MessageBox.Show("Не реализовал пока!!");
        }
    }
}