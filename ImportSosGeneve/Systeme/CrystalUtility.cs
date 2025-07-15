using System;
using System.IO;
using CrystalDecisions;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using SosMedecins.SmartRapport.EtatsCrystal;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de CrystalUtility.
	/// </summary>
	public class CrystalUtility
	{
		public static ExportOptions OptionsExportationDefaut=null;

		public static RapportPatient MyReport=null;

		public CrystalUtility()
		{			
		}

		public static bool ExportReport(ReportDocument crReportDocument,string ExpType,string ExportPath,string filename)
		{
			if(OptionsExportationDefaut==null) OptionsExportationDefaut = crReportDocument.ExportOptions;

			if(ExportPath.Substring(ExportPath.Length-1,1)!="\\") ExportPath+="\\";
			string AncExportPath = ExportPath;
			if(ExpType=="htm")
				ExportPath+="\\" + filename;
			filename = filename + "." + ExpType;

			if(!System.IO.Directory.Exists(ExportPath)) System.IO.Directory.CreateDirectory(ExportPath);

			DiskFileDestinationOptions crDiskFileDestinationOptions = new DiskFileDestinationOptions();
			ExportOptions crExportOptions = crReportDocument.ExportOptions;
            crExportOptions.FormatOptions = OptionsExportationDefaut.FormatOptions;			
		
			if(System.IO.File.Exists(ExportPath + filename)) System.IO.File.Delete(ExportPath + filename);

			switch(ExpType)
			{
				case "rtf":
					crDiskFileDestinationOptions.DiskFileName = ExportPath + filename;
					crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
					crExportOptions.ExportFormatType = ExportFormatType.RichText;
					crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
					break;
				case "pdf":
					crDiskFileDestinationOptions.DiskFileName = ExportPath + filename ;
					crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
					crExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
					crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
					break;
				case "doc":
					crDiskFileDestinationOptions.DiskFileName = ExportPath + filename;
					crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
					crExportOptions.ExportFormatType = ExportFormatType.WordForWindows;
					crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
					break;
				case "xls":
					crDiskFileDestinationOptions.DiskFileName = ExportPath + filename;
					crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
					crExportOptions.ExportFormatType = ExportFormatType.Excel;
					crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
					break;
				case "htm":
					HTMLFormatOptions HTML40FormatOpts = new HTMLFormatOptions();
					crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
					crExportOptions.ExportFormatType = ExportFormatType.HTML40;
					HTML40FormatOpts.HTMLBaseFolderName = ExportPath;
					HTML40FormatOpts.HTMLFileName = filename;
					HTML40FormatOpts.HTMLEnableSeparatedPages = true;
					HTML40FormatOpts.HTMLHasPageNavigator = true;
					HTML40FormatOpts.FirstPageNumber = 1;
					HTML40FormatOpts.LastPageNumber = 3;
					crExportOptions.FormatOptions = HTML40FormatOpts;
					break;

				default:
					break;
			}

			try
			{
				crReportDocument.Export();

				if(ExpType=="htm")
				{
					string[] Rep = System.IO.Directory.GetDirectories(ExportPath);
					SosMedecins.SmartRapport.Systeme.OutilsExt.CopyDirectory(Rep[0],AncExportPath,true);
					System.IO.Directory.Delete(Rep[0],true);
				}

				return true;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		public static string GetHTML40FromReport(ReportDocument crReportDocument,string temp)
		{
			if(temp=="") return null;

			if(Directory.Exists(temp)) Directory.Delete(temp,true);

			ExportOptions crExportOptions = crReportDocument.ExportOptions;
			HTMLFormatOptions HTML40FormatOpts = new HTMLFormatOptions();
			crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
			crExportOptions.ExportFormatType = ExportFormatType.HTML40;
			HTML40FormatOpts.HTMLBaseFolderName = temp;
			HTML40FormatOpts.HTMLFileName = "HTML40.htm";
			HTML40FormatOpts.HTMLEnableSeparatedPages = true;
			HTML40FormatOpts.HTMLHasPageNavigator = true;
			HTML40FormatOpts.FirstPageNumber = 1;
			HTML40FormatOpts.LastPageNumber = 3;
			crExportOptions.FormatOptions = HTML40FormatOpts;

			try
			{
				string retour = "";
				crReportDocument.Export();

				string[] Rep = System.IO.Directory.GetDirectories(temp);
				string Fic = "";
				if(Rep.Length==1) 
				{
					Fic = Rep[0]+ "\\" + HTML40FormatOpts.HTMLFileName;
					StreamReader st = File.OpenText(Fic);
					retour = st.ReadToEnd();
					st.Close();
				}
				return retour;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}
		public static string GetHTML32FromReport(ReportDocument crReportDocument,string temp)
		{
			if(temp=="") return null;

			if(Directory.Exists(temp)) Directory.Delete(temp,true);

			ExportOptions crExportOptions = crReportDocument.ExportOptions;
			HTMLFormatOptions HTML32FormatOpts = new HTMLFormatOptions();
			crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
			crExportOptions.ExportFormatType = ExportFormatType.HTML32;
			HTML32FormatOpts.HTMLBaseFolderName = temp;
			HTML32FormatOpts.HTMLFileName = "HTML40.htm";
			HTML32FormatOpts.HTMLEnableSeparatedPages = true;
			HTML32FormatOpts.HTMLHasPageNavigator = true;
			HTML32FormatOpts.FirstPageNumber = 1;
			HTML32FormatOpts.LastPageNumber = 3;
			crExportOptions.FormatOptions = HTML32FormatOpts;

			try
			{
				string retour = "";
				crReportDocument.Export();

				string[] Rep = System.IO.Directory.GetDirectories(temp);
				string Fic = "";
				if(Rep.Length==1) 
				{
					Fic = Rep[0]+ "\\" + HTML32FormatOpts.HTMLFileName;
					StreamReader st = File.OpenText(Fic);
					retour = st.ReadToEnd();
					st.Close();
				}
				return retour;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}

        public static bool PrintReport(CrystalDecisions.CrystalReports.Engine.ReportClass crReportDocument, int nbCopie, string printerName)
        {
            try
            {
                CrystalDecisions.Shared.ReportPageRequestContext context = new CrystalDecisions.Shared.ReportPageRequestContext();
                int lastPage = crReportDocument.FormatEngine.GetLastPageNumber(context);

                //****************************
                switch (lastPage)
                {
                    case 2:

                        crReportDocument.PrintToPrinter(1, false, lastPage, lastPage);

                        crReportDocument.PrintOptions.PrinterName = printerName;
                        crReportDocument.PrintOptions.PaperSource = PaperSource.Middle;
                        crReportDocument.PrintToPrinter(1, false, 1, lastPage - 1);
                        break;
                    case 1:
                        crReportDocument.PrintOptions.PrinterName = printerName;
                        crReportDocument.PrintOptions.PaperSource = PaperSource.Lower;
                        crReportDocument.PrintToPrinter(1, false, lastPage, lastPage);
                        break;
                }


                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

		
            
        public static bool PrintReport(RapportPatient crReportDocument,int nbCopie,string printerName,bool WithLogo)
		{
			SetVisibleImage(crReportDocument,"Picture4",!WithLogo);
			//SetVisibleTexte(crReportDocument,"Text5",!WithLogo);

			return PrintReport(crReportDocument,nbCopie,printerName);
		}
        public static bool PrintReport1(RapportPatient crReportDocument, int nbCopie, string printerName, bool WithLogo)
        {
            SetinVisibleTexte(crReportDocument, "Text5", true);
            SetVisibleImage(crReportDocument, "Picture4", true);
           return PrintReport(crReportDocument, nbCopie, printerName);
        }
    
    
        //Pour l'envoie des rapports par mail
        public static bool MailReport(long IdRapport, CrystalDecisions.CrystalReports.Engine.ReportClass crReportDocument, string Mail, string printerName, string Destinataire)
        {
            try
            {
                TextObject text = (TextObject)crReportDocument.ReportDefinition.Sections[0].ReportObjects["Text1"];

                //on formate la chaine d'envoi:
                //F307 pour l'objet du mail, F201 nom du destinataire (pour activeFax client)
                //F212 Le mail (ne pas oublier d'aller voir dans Office.cs la fct° PrintToPrinter)
                //F502 1   Pour n'afficher que la 1ere page...

                text.Text = "@F307 Rapport Médical n°" + IdRapport.ToString() + "@@F201 " + Destinataire + "@@F212 " + Mail + "@@F502 1@";
                
                //ici on va exporter le .rpt en rtf, puis générer un .doc, avant de l'envoyer à l'imprimante
                if (OptionsExportationDefaut == null) OptionsExportationDefaut = crReportDocument.ExportOptions;
                
                DiskFileDestinationOptions crDiskFileDestinationOptions = new DiskFileDestinationOptions();
                ExportOptions crExportOptions = crReportDocument.ExportOptions;
                crExportOptions.FormatOptions = OptionsExportationDefaut.FormatOptions;

                crDiskFileDestinationOptions.DiskFileName = System.Windows.Forms.Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache + IdRapport + ".doc";
                crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                crExportOptions.ExportFormatType = ExportFormatType.EditableRTF;
                crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
                crReportDocument.Export();

                if (System.IO.File.Exists(crDiskFileDestinationOptions.DiskFileName))
                {
                    try
                    {
                        //on génère un document word
                        WordDoc doc = new WordDoc(crDiskFileDestinationOptions.DiskFileName);
                        
                        //on l"imprime"
                        doc.PrintToPrinter("ActiveFax");

                        //Puis on le suprime
                        System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache + IdRapport + ".doc");
                        doc = null;
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }
                
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool MailReport(long IdRapport, RapportPatient crReportDocument, string Mail, string printerName, string Destinataire, bool WithLogo)
        {
            SetVisibleImage(crReportDocument, "Picture4", !WithLogo);
            SetVisibleTexte(crReportDocument, "Text5", !WithLogo);
            return MailReport(IdRapport, crReportDocument, Mail, printerName, Destinataire);
        }

        //on fax les rapports
        public static bool FaxReport(long IdRapport, CrystalDecisions.CrystalReports.Engine.ReportClass crReportDocument, string Num, string printerName, string Destinataire)
        {
            try
            {
                TextObject text = (TextObject)crReportDocument.ReportDefinition.Sections[0].ReportObjects["Text1"];

                text.Text = "Rapport " + IdRapport + "@F201 " + Destinataire + "@@F211 " + Num.Replace(" ", "").Trim() + "@";

                if (OptionsExportationDefaut == null) OptionsExportationDefaut = crReportDocument.ExportOptions;

                DiskFileDestinationOptions crDiskFileDestinationOptions = new DiskFileDestinationOptions();
                ExportOptions crExportOptions = crReportDocument.ExportOptions;
                crExportOptions.FormatOptions = OptionsExportationDefaut.FormatOptions;

                crDiskFileDestinationOptions.DiskFileName = System.Windows.Forms.Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache + IdRapport + ".doc";
                crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                crExportOptions.ExportFormatType = ExportFormatType.EditableRTF;
                crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
                crReportDocument.Export();

                if (System.IO.File.Exists(crDiskFileDestinationOptions.DiskFileName))
                {
                    try
                    {
                        WordDoc doc = new WordDoc(crDiskFileDestinationOptions.DiskFileName);
                        doc.PrintToPrinter("ActiveFax");

                        System.IO.File.Delete(System.Windows.Forms.Application.StartupPath + SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli.Cache + IdRapport + ".doc");
                        doc = null;
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

		public static bool FaxReport(long IdRapport,RapportPatient crReportDocument,string Num,string printerName,string Destinataire,bool WithLogo)
		{
			SetVisibleImage(crReportDocument,"Picture4",!WithLogo);
			SetVisibleTexte(crReportDocument,"Text5",!WithLogo);
			return FaxReport(IdRapport,crReportDocument,Num,printerName,Destinataire);
		}

		private static void printDocument1_PrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs ev)
		{
			for(int i=0;i<MyReport.Section1.ReportObjects.Count;i++)
			{
				ReportObject objet = MyReport.Section1.ReportObjects[i];
				if(objet!=null && objet.GetType()==typeof(FieldObject))
				{
					FieldObject texte=  (FieldObject)objet;
					ev.Graphics.DrawString(texte.Name,new System.Drawing.Font("Arial",16,System.Drawing.FontStyle.Bold),System.Drawing.Brushes.Black,objet.Left,objet.Top);
				}				
			}
		}

		public static void MiseEnFormeObject(RapportPatient crDocument,string nom,System.Drawing.Font font,System.Drawing.Color couleur)
		{
			ReportObject objet = null;

            for (int i = 0; i < crDocument.Section1.ReportObjects.Count; i++)
                if (crDocument.Section1.ReportObjects[i].Name == nom) objet = crDocument.Section1.ReportObjects[i];
            for (int i = 0; i < crDocument.Section2.ReportObjects.Count; i++)
                if (crDocument.Section2.ReportObjects[i].Name == nom) objet = crDocument.Section2.ReportObjects[i];
            for (int i = 0; i < crDocument.Section3.ReportObjects.Count; i++)
                if (crDocument.Section3.ReportObjects[i].Name == nom) objet = crDocument.Section3.ReportObjects[i];
            for (int i = 0; i < crDocument.Section4.ReportObjects.Count; i++)
                if (crDocument.Section4.ReportObjects[i].Name == nom) objet = crDocument.Section4.ReportObjects[i];
            for (int i = 0; i < crDocument.Section5.ReportObjects.Count; i++)
                if (crDocument.Section5.ReportObjects[i].Name == nom) objet = crDocument.Section5.ReportObjects[i];
						
			if(objet!=null)
			{
				FieldObject texte=  (FieldObject)objet;
				texte.ApplyFont(font);
				texte.Color = couleur;
				crDocument.Refresh();
			}
		}

		public static void UpdateObject(RapportPatient crDocument,string nom,string texte)
		{
			ReportObject objet = null;

            for (int i = 0; i < crDocument.Section1.ReportObjects.Count; i++)
                if (crDocument.Section1.ReportObjects[i].Name == nom) objet = crDocument.Section1.ReportObjects[i];
            for (int i = 0; i < crDocument.Section2.ReportObjects.Count; i++)
                if (crDocument.Section2.ReportObjects[i].Name == nom) objet = crDocument.Section2.ReportObjects[i];
            for (int i = 0; i < crDocument.Section3.ReportObjects.Count; i++)
                if (crDocument.Section3.ReportObjects[i].Name == nom) objet = crDocument.Section3.ReportObjects[i];
            for (int i = 0; i < crDocument.Section4.ReportObjects.Count; i++)
                if (crDocument.Section4.ReportObjects[i].Name == nom) objet = crDocument.Section4.ReportObjects[i];
            for (int i = 0; i < crDocument.Section5.ReportObjects.Count; i++)
                if (crDocument.Section5.ReportObjects[i].Name == nom) objet = crDocument.Section5.ReportObjects[i];
						
			TextObject o = (TextObject)objet;
			o.Text = texte;
		}

		public static void StyleObject(RapportPatient crDocument,string nom,bool bUnderline,bool bBold, bool bItalic)
		{
			ReportObject objet = null;

            for (int i = 0; i < crDocument.Section1.ReportObjects.Count; i++)
                if (crDocument.Section1.ReportObjects[i].Name == nom) objet = crDocument.Section1.ReportObjects[i];
            for (int i = 0; i < crDocument.Section2.ReportObjects.Count; i++)
                if (crDocument.Section2.ReportObjects[i].Name == nom) objet = crDocument.Section2.ReportObjects[i];
            for (int i = 0; i < crDocument.Section3.ReportObjects.Count; i++)
                if (crDocument.Section3.ReportObjects[i].Name == nom) objet = crDocument.Section3.ReportObjects[i];
            for (int i = 0; i < crDocument.Section4.ReportObjects.Count; i++)
                if (crDocument.Section4.ReportObjects[i].Name == nom) objet = crDocument.Section4.ReportObjects[i];
            for (int i = 0; i < crDocument.Section5.ReportObjects.Count; i++)
                if (crDocument.Section5.ReportObjects[i].Name == nom) objet = crDocument.Section5.ReportObjects[i];
			
			int style = (int)System.Drawing.FontStyle.Regular;
			
			if(bUnderline) style+=4;
			if(bBold) style+=1;
			if(bItalic) style+=2;
			
			if(objet!=null)
			{
				if(objet.GetType()==typeof(FieldObject))
				{
					FieldObject o = (FieldObject)objet;
					System.Drawing.Font font = new System.Drawing.Font(o.Font.FontFamily,o.Font.Size,(System.Drawing.FontStyle)style);
					o.ApplyFont(font);				
				}
				else if(objet.GetType()==typeof(TextObject))
				{
					TextObject o = (TextObject)objet;
					System.Drawing.Font font = new System.Drawing.Font(o.Font.FontFamily,o.Font.Size,(System.Drawing.FontStyle)style);
					o.ApplyFont(font);				
				}
			}
		}

		public static void ChangePoliceSize(RapportPatient crDocument,string nom,int PoliceSize)
		{
			ReportObject objet = null;

            for (int i = 0; i < crDocument.Section1.ReportObjects.Count; i++)
                if (crDocument.Section1.ReportObjects[i].Name == nom) objet = crDocument.Section1.ReportObjects[i];
            for (int i = 0; i < crDocument.Section2.ReportObjects.Count; i++)
                if (crDocument.Section2.ReportObjects[i].Name == nom) objet = crDocument.Section2.ReportObjects[i];
            for (int i = 0; i < crDocument.Section3.ReportObjects.Count; i++)
                if (crDocument.Section3.ReportObjects[i].Name == nom) objet = crDocument.Section3.ReportObjects[i];
            for (int i = 0; i < crDocument.Section4.ReportObjects.Count; i++)
                if (crDocument.Section4.ReportObjects[i].Name == nom) objet = crDocument.Section4.ReportObjects[i];
            for (int i = 0; i < crDocument.Section5.ReportObjects.Count; i++)
                if (crDocument.Section5.ReportObjects[i].Name == nom) objet = crDocument.Section5.ReportObjects[i];
			
			if(objet!=null)
			{
				if(objet.GetType()==typeof(FieldObject))
				{
					FieldObject o = (FieldObject)objet;
					System.Drawing.Font font = new System.Drawing.Font(o.Font.FontFamily,PoliceSize,o.Font.Style);
					o.ApplyFont(font);				
				}
				else if(objet.GetType()==typeof(TextObject))
				{
					TextObject o = (TextObject)objet;
					System.Drawing.Font font = new System.Drawing.Font(o.Font.FontFamily,PoliceSize,o.Font.Style);
					o.ApplyFont(font);				
				}
			}
		}

		public static void ReLocateObject(RapportPatient crDocument,string nom, int X,int Y )
		{
			ReportObject objet = null;

            for (int i = 0; i < crDocument.Section1.ReportObjects.Count; i++)
                if (crDocument.Section1.ReportObjects[i].Name == nom) objet = crDocument.Section1.ReportObjects[i];
            for (int i = 0; i < crDocument.Section2.ReportObjects.Count; i++)
                if (crDocument.Section2.ReportObjects[i].Name == nom) objet = crDocument.Section2.ReportObjects[i];
            for (int i = 0; i < crDocument.Section3.ReportObjects.Count; i++)
                if (crDocument.Section3.ReportObjects[i].Name == nom) objet = crDocument.Section3.ReportObjects[i];
            for (int i = 0; i < crDocument.Section4.ReportObjects.Count; i++)
                if (crDocument.Section4.ReportObjects[i].Name == nom) objet = crDocument.Section4.ReportObjects[i];
            for (int i = 0; i < crDocument.Section5.ReportObjects.Count; i++)
                if (crDocument.Section5.ReportObjects[i].Name == nom) objet = crDocument.Section5.ReportObjects[i];
						
			if(X!=-1)
				objet.Left = X;
			if(Y!=-1)
				objet.Top = Y;
		}

		public static void FontInReport(RapportPatient crDocument,string commencant,System.Drawing.Font TheFont)
		{
			ReportObject objet;
			
			for(int i=0;i<crDocument.Section1.ReportObjects.Count;i++)
			{
				if(crDocument.Section1.ReportObjects[i].Name.IndexOf("Rap")>-1) 
				{
					objet= crDocument.Section1.ReportObjects[i];
					if(objet.GetType()==typeof(FieldObject))
					{
						((FieldObject)objet).ApplyFont(TheFont);					
					}
					else if(objet.GetType()==typeof(TextObject))
					{
						((TextObject)objet).ApplyFont(TheFont);
					}
				}
			}
			for(int i=0;i<crDocument.Section2.ReportObjects.Count;i++)
			{
				if(crDocument.Section2.ReportObjects[i].Name.IndexOf("Rap")>-1) 
				{
					objet= crDocument.Section2.ReportObjects[i];
					if(objet.GetType()==typeof(FieldObject))
					{
						((FieldObject)objet).ApplyFont(TheFont);
					}
					else if(objet.GetType()==typeof(TextObject))
					{
						((TextObject)objet).ApplyFont(TheFont);
					}
				}
			}
			for(int i=0;i<crDocument.Section3.ReportObjects.Count;i++)
			{
				if(crDocument.Section3.ReportObjects[i].Name.IndexOf("Rap")>-1) 
				{
					objet= crDocument.Section3.ReportObjects[i];
					if(objet.GetType()==typeof(FieldObject))
					{
						((FieldObject)objet).ApplyFont(TheFont);
					}
					else if(objet.GetType()==typeof(TextObject))
					{
						((TextObject)objet).ApplyFont(TheFont);
					}
				}
			}
			for(int i=0;i<crDocument.Section4.ReportObjects.Count;i++)
			{
				if(crDocument.Section4.ReportObjects[i].Name.IndexOf("Rap")>-1) 
				{
					objet= crDocument.Section4.ReportObjects[i];
					if(objet.GetType()==typeof(FieldObject))
					{
						((FieldObject)objet).ApplyFont(TheFont);
					}
					else if(objet.GetType()==typeof(TextObject))
					{
						((TextObject)objet).ApplyFont(TheFont);
					}
				}
			}
			for(int i=0;i<crDocument.Section5.ReportObjects.Count;i++)
			{
				if(crDocument.Section5.ReportObjects[i].Name.IndexOf("Rap")>-1) 
				{
					objet= crDocument.Section5.ReportObjects[i];
					if(objet.GetType()==typeof(FieldObject))
					{
						((FieldObject)objet).ApplyFont(TheFont);
					}
					else if(objet.GetType()==typeof(TextObject))
					{
						((TextObject)objet).ApplyFont(TheFont);
					}
				}
			}
			
		}

        public static void SetVisibleImage(CrystalDecisions.CrystalReports.Engine.ReportClass crReport, string Name, bool Invisible)
        {
            PictureObject picture = (PictureObject)crReport.ReportDefinition.Sections[0].ReportObjects[Name.ToLower()];
            picture.ObjectFormat.EnableSuppress = Invisible;

            picture = (PictureObject)crReport.ReportDefinition.Sections[3].ReportObjects[Name.ToLower()];
            picture.ObjectFormat.EnableSuppress = Invisible;
        }

        public static void SetVisibleTexte(CrystalDecisions.CrystalReports.Engine.ReportClass crReport, string Name, bool Invisible)
        {
            TextObject text = (TextObject)crReport.ReportDefinition.Sections[0].ReportObjects[Name.ToLower()];
        }
        public static void SetinVisibleTexte(CrystalDecisions.CrystalReports.Engine.ReportClass crReport, string Name, bool Invisible)
        {
            TextObject text = (TextObject)crReport.ReportDefinition.Sections[0].ReportObjects[Name.ToLower()];
            text.ObjectFormat.EnableSuppress = true;
            //crReport.ReportDefinition.Sections[0].ReportObjects[Name].Dispose();
        }
    
    }
}
