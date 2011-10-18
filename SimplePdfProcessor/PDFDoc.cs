using System;
using System.Collections;
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

	class PDFDoc: IPDFDoc, IEnumerable
	///<summary>
    ///	Abstraction for working with PDF
    ///</summary>
    {
		PdfDocument document; //document to work with
		PdfDocumentOpenMode openMode;
		string filename;
		public string FileName
		{
			get
			{
				return filename;
			}
		}
		public int PagesCount 
		{
			get
			{
				return document.PageCount;
			}
		}
		
		//Constructors
		public PDFDoc()
		{
			this.document = new PdfDocument();
//			this.openMode = PdfDocumentOpenMode.Modify;
		}
		
		public PDFDoc(string filename, PdfDocumentOpenMode openmode)
		{
			if (filename == null)
				throw new ArgumentNullException("filename is null");
			this.filename = filename;
			this.openMode = openmode;
			this.document = PDFDoc.LoadPDF(FileName, openMode);
		}
		
		//interfaces
		
		public IEnumerator GetEnumerator()
		{
			return document.Pages.GetEnumerator();
		}
		
		//methods
		
		public override string ToString()
		{
			return FileName;
		}

		
		public void InsertPage(PdfPage page, int index)
		{
			if (page == null)
				throw new ArgumentNullException("page is null");
			if (index < 0)
				throw new ArgumentOutOfRangeException("index have to be positive");
			document.Pages.Insert(index, page);
		}
		
		public void AddPage(PdfPage page)
		{
			document.Pages.Add(page);
		}
		
		public void DeletePage(int index)
		{
			document.Pages.RemoveAt(index);
		}
		
		public PdfPage GetPage(int index)
		{
			return document.Pages[index];
		}
		
		public bool OpenPDF(PdfDocumentOpenMode openmode)
		{
            //Открываем файл
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "PDF files|*.pdf";
            if (true == dlg.ShowDialog())
            {
                this.filename = dlg.FileName;
                this.openMode = openmode;
                this.document = PDFDoc.LoadPDF(filename, openmode);
                if (document == null)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
		}
		
		public bool SavePDF(bool showFile)
		{
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "PDF files|*.pdf";
            if (true == dlg.ShowDialog())
            {
            	this.filename = dlg.FileName;
            	if (filename.Substring(filename.Length-5)!=".pdf")
            	{
            		filename += ".pdf";
            	}
                this.document.Save(this.FileName);
                MessageBox.Show("Done!");
                if (showFile==true)
                    Process.Start(this.filename);
            }
            return dlg.FileName!=null;			
		}
		
        public static PdfDocument LoadPDF(string filename, PdfDocumentOpenMode openmode)
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
                {
                    MessageBox.Show("При открытии файла произошла ошибка (возможно, он защищён)", "Ошибка!");
                    return null; //TODO: open file with import mode and return with openmode
                    //otherwise, it seems file is v1.6 and higher
                    //trying to open via iTextSharp to 
                }
                return PdfReader.Open(GetCompartibleStream(filename), openmode);
            }
        }

        static MemoryStream GetCompartibleStream(string filename)
        ///<summary>open file via iTextSharp and return pdf v1.4 for PdfSharp</summary>
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

        public static bool WritePdf(PdfDocument pdfDoc, string filename)
        //save pdffile
        {
        	if (filename == null)
        		throw new ArgumentNullException("null filename");
            if (pdfDoc.PageCount == 0)
            {
                MessageBox.Show("Вы пытаетесь записать пустой pdf-файл!");
                return false;
            }
            try
            {
            	pdfDoc.Save(filename);            	
            }
            catch (Exception ex)
            {
            	throw new IOException("Проблемы с записью файла!");
            }
            return true;
        }
		
    }
}
