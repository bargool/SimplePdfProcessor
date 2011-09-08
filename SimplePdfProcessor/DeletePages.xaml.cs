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
using System.Diagnostics;

namespace SimplePdfProcessor
{
    /// <summary>
    /// Логика взаимодействия для DeletePages.xaml
    /// </summary>
    public partial class DeletePages : Window
    {
    	PDFDoc pdf = null;
        public DeletePages()
        {
            InitializeComponent();
        }

        private void btnGO_Click(object sender, RoutedEventArgs e)
        {
            int fromPage;
            int toPage;

            bool isValidFrom = int.TryParse(txtFromPage.Text, out fromPage);
            bool isValidTo = int.TryParse(txtToPage.Text, out toPage);
            if ((!isValidFrom || !isValidTo)||(fromPage > toPage || fromPage<1))
            {
                MessageBox.Show("Требуются корректные номера страниц!");
                return;
            }
            if (toPage > pdf.PagesCount)
            {
                toPage = pdf.PagesCount;
            }
            // Удаляем i раз страницу "С" (при удалении следующая занимает место удаленной ведь!)
            for (int i = fromPage-1; i < toPage; i++)
            {
                pdf.DeletePage(fromPage-1);
            }
            DialogResult = pdf.SavePDF(true);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        	pdf = new PDFDoc();
            //Открываем файл
            bool openResult = pdf.OpenPDF(PdfDocumentOpenMode.Modify);
            if (openResult)
            {
            	lblPageAmount.Content += pdf.PagesCount.ToString;
            }
            else
            {
            	this.DialogResult=false;
            }
        }

    }
}
