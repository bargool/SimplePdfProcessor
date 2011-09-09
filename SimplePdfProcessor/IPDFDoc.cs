/*
 * Created by SharpDevelop.
 * User: aleksey.nakoryakov
 * Date: 07.09.2011
 * Time: 17:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using PdfSharp.Pdf;

namespace SimplePdfProcessor
{
	/// <summary>
	/// Description of IPDFDoc.
	/// </summary>
	public interface IPDFDoc
	{
		int PagesCount { get; }
		
		void InsertPage(PdfPage page, int index);
		
		void AddPage(PdfPage page);
		
		void DeletePage(int index);
		
		PdfPage GetPage(int index);
	}
}
