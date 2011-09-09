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
using System.Collections.ObjectModel;

namespace SimplePdfProcessor
{
    /// <summary>
    /// Логика взаимодействия для MergeWindow.xaml
    /// </summary>
    public partial class MergeWindow : Window
    {
    	ObservableCollection<PDFDoc> inPdfs = new ObservableCollection<PDFDoc>(); //input pdf files
        PDFDoc outPdf; //output pdf file
        public MergeWindow()
        {
            InitializeComponent();
            outPdf = new PDFDoc();
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
                    inPdfs.Add(new PDFDoc(filename, PdfDocumentOpenMode.Import));
            }
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
        	foreach (PDFDoc doc in inPdfs)
        	{
        		foreach (PdfPage page in doc)
        		{
        			outPdf.AddPage(page);
        		}
        	}
        	DialogResult = outPdf.SavePDF((bool)checkShowFile.IsChecked);
        }

        private void btnRemoveFile_Click(object sender, RoutedEventArgs e)
        {
        	inPdfs.RemoveAt(lstMergeFiles.SelectedIndex);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Binding binding = new Binding();
            binding.Source = inPdfs;
            lstMergeFiles.SetBinding(ListBox.ItemsSourceProperty, binding);
        }
    }
}