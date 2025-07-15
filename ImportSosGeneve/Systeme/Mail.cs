using System;
using System.Web.Mail;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de Mail.
	/// </summary>
	public class Mail
	{	
		public MailMessage m_Mail=null;

		public Mail(string[] Destinataire,string sujet,string objet,string[] attachement)
		{
			MailMessage mess = new MailMessage();
			mess.From = OutilsExt.ParamAppli.m_Utilisateur.EMail;
			foreach(string s in Destinataire)
			{
				mess.To+=s + ";";
			}
			mess.To = mess.To.Remove(mess.To.Length-1,1);

			mess.Subject = sujet;
			mess.BodyFormat	 = MailFormat.Text;
			mess.Body = objet;

			for(int i=0;i<attachement.Length;i++)
			{
				MailAttachment att = new MailAttachment(attachement[i]);
				mess.Attachments.Add(att);				
			}		

			m_Mail = mess;
		}

		public bool EnvoiMail()
		{	
			if(m_Mail==null) return false;
			int Etape = 0;
			try
			{				
				OutLookMail mail = new OutLookMail();
				Etape++;
				string[] Attach = new string[m_Mail.Attachments.Count];
				Etape++;
				
				for(int i=0;i<m_Mail.Attachments.Count;i++)
				{
					MailAttachment att  = (MailAttachment)m_Mail.Attachments[i];
					Attach[i] = att.Filename;
					
				}
				
				mail.AddToOutBox(m_Mail.To,m_Mail.Subject,m_Mail.Body,Attach);
				Etape++;
				return true;
			}
			catch(Exception ex)
			{
				
				System.Windows.Forms.MessageBox.Show(Etape.ToString());
				System.Windows.Forms.MessageBox.Show(ex.Message);
				return false;
			}
		}
	}

	public class ServeurMail
	{
		public string AdrServeurSmtp;
		public string AdrEmetteur;
		public int PortServeurSmtp;

		public ServeurMail(string Serveur, int Port,string Emetteur)
		{
			this.AdrEmetteur = Emetteur;
			this.AdrServeurSmtp = Serveur;
			this.PortServeurSmtp = Port;
		}
	}
}
