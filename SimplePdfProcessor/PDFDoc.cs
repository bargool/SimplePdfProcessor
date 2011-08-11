using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using iTextSharp;
using System.IO;
using Microsoft.Win32;
using System.Windows;
using System.Diagnostics;

namespace SimplePdfProcessor
{
    class PDFDoc
    //Some static methods to use in program
    {
        public static PdfDocument OpenPDF(string filename, PdfDocumentOpenMode openmode)
        //opens PdfDocument
        {
            try
            {
                return PdfReader.Open(filename, openmode);
            }
            catch (PdfReaderException ex)
            {
                //if password protected - returns null
                if (ex.Message == "To modify the document the owner password is required")
                    return null; //TODO: open file with import mode and return with openmode
                //otherwise, it seems file is v1.6 and higher
                //trying to open via iTextSharp to 
                return PdfReader.Open(GetCompartibleStream(filename), openmode);

            }
        }

        static MemoryStream GetCompartibleStream(string filename)
        //open file via iTextSharp
        {
            MemoryStream outputStream = new MemoryStream();
            iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(filename);
            iTextSharp.text.pdf.PdfStamper pdfStamper = new iTextSharp.text.pdf.PdfStamper(reader, outputStream);
            pdfStamper.FormFlattening = true;
            pdfStamper.Writer.SetPdfVersion(iTextSharp.text.pdf.PdfWriter.PDF_VERSION_1_4);
            pdfStamper.Writer.CloseStream = false;
            pdfStamper.Close();
            return outputStream;
        }

        public static bool SavePdf(PdfDocument pdfDoc, bool showFile)
        //save pdffile and return it's name
        {
            if (pdfDoc.PageCount == 0)
            {
                MessageBox.Show("Вы пытаетесь записать пустой pdf-файл!");
                return false;
            }
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PDF files|*.pdf";
            if (true == dlg.ShowDialog())
            {
                pdfDoc.Save(dlg.FileName);
                MessageBox.Show("Done!");
                if (showFile==true)
                    Process.Start(dlg.FileName);
            }
            return dlg.FileName!=null;
        }
    }
}
