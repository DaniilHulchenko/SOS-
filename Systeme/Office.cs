using System;
using Microsoft.CSharp;
using System.IO;
using System.Diagnostics;
using System.Collections;
//using Word;
using System.Threading;


/// <summary>
/// Description résumée de WordDoc.
/// </summary>
public class WordDoc
{
    private Microsoft.Office.Interop.Word.Application oApp;
	private string m_strFile="";

	public WordDoc(string FileName)
	{	
		try
		{
            oApp = new Microsoft.Office.Interop.Word.Application();
			m_strFile = FileName;
		}
		catch(Exception exc1)
		{
			Console.WriteLine(exc1.Message);
		}
    }

	public void PrintToPrinter(string printerName)
	{		
		try
		{
			object M=Type.Missing; 
			object V= m_strFile; 
			object F = false;
			object I=false; 
			object Ind = 1;
			oApp.Documents.Open(ref V,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M);
			oApp.ActivePrinter = printerName;

			// Gestion de la police pour ActiveFax : 
			for(int i=0;i<oApp.Documents.get_Item(ref Ind).Paragraphs.Count;i++)
			{
                Microsoft.Office.Interop.Word.Paragraph para = oApp.Documents.get_Item(ref Ind).Paragraphs[i + 1];
				if((para.Range.Text.IndexOf("@F211")>-1) || (para.Range.Text.IndexOf("@F212")>-1))
				{
					para.Range.Font.Name = "ActiveFax Fixed";
					break;
				}
			}

			oApp.PrintOut(ref I,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M,ref M);
			oApp.Documents.Close(ref F,ref M,ref M);
            oApp.Quit(ref F,ref M,ref M);
			
		}
		catch(Exception exc1)
		{
			Console.WriteLine(exc1.Message);
		}
	}
}

