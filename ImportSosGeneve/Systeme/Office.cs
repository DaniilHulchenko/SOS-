using System;
using Microsoft.CSharp;
using System.IO;
using System.Diagnostics;
using System.Collections;
using Word;
using System.Threading;



namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de OutLookMail.
	/// </summary>
	public class OutLookMail
	{
		private Outlook.Application oApp;
		private Outlook._NameSpace oNameSpace;
		private Outlook.MAPIFolder oOutboxFolder;
		private Outlook.MAPIFolder oSentMailFolder;

		public OutLookMail()
		{
			oApp = new Outlook.ApplicationClass();
			oNameSpace	 = oApp.GetNamespace("MAPI");
			oNameSpace.Logon(null,null,false,false);
			oOutboxFolder = oNameSpace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderOutbox);
			oSentMailFolder = oNameSpace.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderSentMail);
		}

		public void AddToOutBox(string ToValue,string SubjectValue,string BodyValue,string[] Attachments)
		{
			object M=Type.Missing; 
			Outlook._MailItem oMailItem = (Outlook._MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
			oMailItem.To  = ToValue;
			oMailItem.Subject = SubjectValue;
			oMailItem.HTMLBody = BodyValue;	
			
			foreach(string s in Attachments)
			{
				
				oMailItem.Attachments.Add(s,M,M,"Piece jointe");
			}
			
			// Supply the state information required by the task.
			ThreadWithState tws = new ThreadWithState(oMailItem);
			// Create a thread to execute the task, and then
			// start the thread.
			Thread t = new Thread(new ThreadStart(tws.ThreadProc));
			t.Start();
			Console.WriteLine("Main thread does some work, then waits.");
			t.Join();
			Console.WriteLine("Independent task has completed; main thread ends.");  
			//oMailItem.SaveSentMessageFolder = oOutboxFolder;
			//oMailItem.SaveSentMessageFolder = oSentMailFolder;
			
			//oMailItem.Send();
		}
	}
	/// <summary>
	/// Description résumée de WordDoc.
	/// </summary>
	public class WordDoc
	{
		private Word.Application oApp;
		private string m_strFile="";

		public WordDoc(string FileName)
		{	
			try
			{
				oApp = new Word.ApplicationClass();
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
					Word.Paragraph para = oApp.Documents.get_Item(ref Ind).Paragraphs[i+1];
					if(para.Range.Text.IndexOf("@F211")>-1)
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
	public class ThreadWithState 
	{
		
		// State information used in the task.
		Outlook._MailItem oMyMailItem;
		
			
		
		// The constructor obtains the state information.
		public ThreadWithState(Outlook._MailItem oMailItem) 
		{
			oMyMailItem = oMailItem;
		}



		// The thread procedure performs the task, such as formatting 
		// and printing a document.
		public void ThreadProc() 
		{
			oMyMailItem.Send(); 
		}
	}
}
