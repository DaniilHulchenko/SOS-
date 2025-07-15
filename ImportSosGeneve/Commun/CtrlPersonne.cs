using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de CtrlPersonne.
	/// </summary>
	public class CtrlPersonne : System.Windows.Forms.UserControl
	{
		#region Déclaration des variables

		private DataRow m_Personne=null;
		private DataRow m_Appel=null;
		private frmNouveauPatient _frm=null;

		// Controles du formulaire

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox txtDossier;
		private System.Windows.Forms.LinkLabel lnkValidation;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox txtNom;
        private System.Windows.Forms.TextBox txtPrenom;
		private System.Windows.Forms.TextBox txtNaissance;
		private System.Windows.Forms.TextBox txtNumRue;
		private System.Windows.Forms.TextBox txtRue;
		private System.Windows.Forms.TextBox txtNp;
		private System.Windows.Forms.TextBox txtCommune;
		private System.Windows.Forms.TextBox txtBatiment;
		private System.Windows.Forms.TextBox txtEscalier;
		private System.Windows.Forms.TextBox txtEtage;
		private System.Windows.Forms.TextBox txtDigicode;
		private System.Windows.Forms.TextBox txtInterphone;
		private System.Windows.Forms.TextBox txtPorte;
		private System.Windows.Forms.TextBox txtCommentaire;
		private System.Windows.Forms.Label label17;
        private MaskedTextBox EMaskTel1;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion

		#region Construction / Destruction du formulaire

		public CtrlPersonne()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent
		}

		public CtrlPersonne(frmNouveauPatient frm, DataRow row, DataRow Appel)
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent
			m_Personne = row;
			m_Appel = Appel;

			_frm=frm;

			// on affiche la personne
			m_Personne["Tel"]=m_Appel["TelPatient"].ToString()  ;
			m_Personne["NumeroDansRue"]=m_Appel["NumeroDansRue"].ToString()  ;
			m_Personne["Rue"]=m_Appel["Rue"].ToString() ;
			m_Personne["CodePostal"]=m_Appel["CodePostal"].ToString()  ;
			m_Personne["Commune"]=m_Appel["Commune"].ToString() ;
			m_Personne["Batiment"]=m_Appel["Batiment"].ToString() ;
			m_Personne["Escalier"]=m_Appel["Escalier"].ToString()  ;
			m_Personne["Etage"]=m_Appel["Etage"].ToString()  ;
			m_Personne["Digicode"]=m_Appel["Digicode"].ToString()  ;
			m_Personne["Internom"]=m_Appel["Internom"].ToString()  ;
			m_Personne["Porte"]=m_Appel["Porte"].ToString();

			AffichePersonne(m_Personne);
		}

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDossier = new System.Windows.Forms.TextBox();
            this.lnkValidation = new System.Windows.Forms.LinkLabel();
            this.label16 = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.txtPrenom = new System.Windows.Forms.TextBox();
            this.txtNaissance = new System.Windows.Forms.TextBox();
            this.txtNumRue = new System.Windows.Forms.TextBox();
            this.txtRue = new System.Windows.Forms.TextBox();
            this.txtNp = new System.Windows.Forms.TextBox();
            this.txtCommune = new System.Windows.Forms.TextBox();
            this.txtBatiment = new System.Windows.Forms.TextBox();
            this.txtEscalier = new System.Windows.Forms.TextBox();
            this.txtEtage = new System.Windows.Forms.TextBox();
            this.txtDigicode = new System.Windows.Forms.TextBox();
            this.txtInterphone = new System.Windows.Forms.TextBox();
            this.txtPorte = new System.Windows.Forms.TextBox();
            this.txtCommentaire = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.EMaskTel1 = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nom :";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Prénom :";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tél :";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Date de naissance  :";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Numéro de rue :";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Rue :";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "NP :";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(104, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "Commune :";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 224);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 16);
            this.label9.TabIndex = 19;
            this.label9.Text = "Batiment :";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(8, 248);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 16);
            this.label10.TabIndex = 23;
            this.label10.Text = "Escalier :";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(8, 272);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 16);
            this.label11.TabIndex = 27;
            this.label11.Text = "Etage :";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(144, 224);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 16);
            this.label12.TabIndex = 21;
            this.label12.Text = "Digicode :";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(144, 248);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 16);
            this.label13.TabIndex = 25;
            this.label13.Text = "Interphone :";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(144, 272);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(96, 16);
            this.label14.TabIndex = 29;
            this.label14.Text = "Porte :";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(8, 312);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(176, 16);
            this.label15.TabIndex = 31;
            this.label15.Text = "Commentaire (255 ca.) :";
            // 
            // txtDossier
            // 
            this.txtDossier.Location = new System.Drawing.Point(256, 40);
            this.txtDossier.Multiline = true;
            this.txtDossier.Name = "txtDossier";
            this.txtDossier.Size = new System.Drawing.Size(264, 360);
            this.txtDossier.TabIndex = 5;
            // 
            // lnkValidation
            // 
            this.lnkValidation.Location = new System.Drawing.Point(8, 392);
            this.lnkValidation.Name = "lnkValidation";
            this.lnkValidation.Size = new System.Drawing.Size(240, 16);
            this.lnkValidation.TabIndex = 33;
            this.lnkValidation.TabStop = true;
            this.lnkValidation.Text = "Valider la création";
            this.lnkValidation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnkValidation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkValidation_LinkClicked);
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(256, 16);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(256, 16);
            this.label16.TabIndex = 2;
            this.label16.Text = "Dossier du patient :  (accessible par PDA)";
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(68, 13);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(155, 20);
            this.txtNom.TabIndex = 1;
            // 
            // txtPrenom
            // 
            this.txtPrenom.Location = new System.Drawing.Point(68, 37);
            this.txtPrenom.Name = "txtPrenom";
            this.txtPrenom.Size = new System.Drawing.Size(155, 20);
            this.txtPrenom.TabIndex = 4;
            // 
            // txtNaissance
            // 
            this.txtNaissance.Location = new System.Drawing.Point(112, 84);
            this.txtNaissance.Name = "txtNaissance";
            this.txtNaissance.Size = new System.Drawing.Size(111, 20);
            this.txtNaissance.TabIndex = 10;
            // 
            // txtNumRue
            // 
            this.txtNumRue.Location = new System.Drawing.Point(94, 108);
            this.txtNumRue.Name = "txtNumRue";
            this.txtNumRue.Size = new System.Drawing.Size(80, 20);
            this.txtNumRue.TabIndex = 12;
            // 
            // txtRue
            // 
            this.txtRue.Location = new System.Drawing.Point(40, 141);
            this.txtRue.Name = "txtRue";
            this.txtRue.Size = new System.Drawing.Size(203, 20);
            this.txtRue.TabIndex = 14;
            // 
            // txtNp
            // 
            this.txtNp.Location = new System.Drawing.Point(14, 186);
            this.txtNp.Name = "txtNp";
            this.txtNp.Size = new System.Drawing.Size(74, 20);
            this.txtNp.TabIndex = 17;
            // 
            // txtCommune
            // 
            this.txtCommune.Location = new System.Drawing.Point(106, 186);
            this.txtCommune.Name = "txtCommune";
            this.txtCommune.Size = new System.Drawing.Size(136, 20);
            this.txtCommune.TabIndex = 18;
            // 
            // txtBatiment
            // 
            this.txtBatiment.Location = new System.Drawing.Point(72, 221);
            this.txtBatiment.Name = "txtBatiment";
            this.txtBatiment.Size = new System.Drawing.Size(56, 20);
            this.txtBatiment.TabIndex = 20;
            // 
            // txtEscalier
            // 
            this.txtEscalier.Location = new System.Drawing.Point(72, 244);
            this.txtEscalier.Name = "txtEscalier";
            this.txtEscalier.Size = new System.Drawing.Size(56, 20);
            this.txtEscalier.TabIndex = 24;
            // 
            // txtEtage
            // 
            this.txtEtage.Location = new System.Drawing.Point(72, 267);
            this.txtEtage.Name = "txtEtage";
            this.txtEtage.Size = new System.Drawing.Size(56, 20);
            this.txtEtage.TabIndex = 28;
            // 
            // txtDigicode
            // 
            this.txtDigicode.Location = new System.Drawing.Point(206, 220);
            this.txtDigicode.Name = "txtDigicode";
            this.txtDigicode.Size = new System.Drawing.Size(47, 20);
            this.txtDigicode.TabIndex = 22;
            // 
            // txtInterphone
            // 
            this.txtInterphone.Location = new System.Drawing.Point(206, 246);
            this.txtInterphone.Name = "txtInterphone";
            this.txtInterphone.Size = new System.Drawing.Size(47, 20);
            this.txtInterphone.TabIndex = 26;
            // 
            // txtPorte
            // 
            this.txtPorte.Location = new System.Drawing.Point(205, 268);
            this.txtPorte.Name = "txtPorte";
            this.txtPorte.Size = new System.Drawing.Size(48, 20);
            this.txtPorte.TabIndex = 30;
            // 
            // txtCommentaire
            // 
            this.txtCommentaire.Location = new System.Drawing.Point(8, 332);
            this.txtCommentaire.Name = "txtCommentaire";
            this.txtCommentaire.Size = new System.Drawing.Size(242, 20);
            this.txtCommentaire.TabIndex = 32;
            // 
            // label17
            // 
            this.label17.ForeColor = System.Drawing.Color.Red;
            this.label17.Location = new System.Drawing.Point(40, 64);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 16);
            this.label17.TabIndex = 7;
            this.label17.Text = "000 000 00 00";
            // 
            // EMaskTel1
            // 
            this.EMaskTel1.BackColor = System.Drawing.Color.White;
            this.EMaskTel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EMaskTel1.ForeColor = System.Drawing.Color.SlateBlue;
            this.EMaskTel1.Location = new System.Drawing.Point(121, 59);
            this.EMaskTel1.Mask = "000 000 00 00";
            this.EMaskTel1.Name = "EMaskTel1";
            this.EMaskTel1.Size = new System.Drawing.Size(102, 21);
            this.EMaskTel1.TabIndex = 34;
            // 
            // CtrlPersonne
            // 
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.Controls.Add(this.EMaskTel1);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.txtCommentaire);
            this.Controls.Add(this.txtPorte);
            this.Controls.Add(this.txtInterphone);
            this.Controls.Add(this.txtDigicode);
            this.Controls.Add(this.txtEtage);
            this.Controls.Add(this.txtEscalier);
            this.Controls.Add(this.txtBatiment);
            this.Controls.Add(this.txtCommune);
            this.Controls.Add(this.txtNp);
            this.Controls.Add(this.txtRue);
            this.Controls.Add(this.txtNumRue);
            this.Controls.Add(this.txtNaissance);
            this.Controls.Add(this.txtPrenom);
            this.Controls.Add(this.txtNom);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.lnkValidation);
            this.Controls.Add(this.txtDossier);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CtrlPersonne";
            this.Size = new System.Drawing.Size(528, 416);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Méthodes privées

		// Affichage d'une personne
		private void AffichePersonne(DataRow row)
		{
			txtNom.Text = row["Nom"].ToString();
			txtPrenom.Text = row["Prenom"].ToString();
			txtNaissance.Text = row["DateNaissance"].ToString();
			EMaskTel1.Text = row["Tel"].ToString();
			txtNumRue.Text = row["NumeroDansRue"].ToString();
			txtRue.Text = row["Rue"].ToString();
			txtNp.Text = row["CodePostal"].ToString();
			txtCommune.Text = row["Commune"].ToString();
			txtBatiment.Text = row["Batiment"].ToString();
			txtEscalier.Text = row["Escalier"].ToString();
			txtEtage.Text = row["Etage"].ToString();
			txtDigicode.Text = row["Digicode"].ToString();
			txtInterphone.Text = row["Internom"].ToString();
			txtPorte.Text = row["Porte"].ToString();
			txtCommentaire.Text = row["TexteSup"].ToString();
			txtDossier.Text = row["SuiviPatient"].ToString();
		}

		#endregion

		#region Evenements du formulaire

		private void lnkValidation_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if(!WorkedString.ValiditeDate(txtNaissance.Text) && txtNaissance.Text!="")
			{
				MessageBox.Show("Champs Date de naissance non valide");
				txtNaissance.Focus();
				return;
			}

			if(txtNom.Text=="")
			{
				MessageBox.Show("Champs Nom Obligatoire");
				return;
			}
			if(txtPrenom.Text=="")
			{
				MessageBox.Show("Champs Prénom Obligatoire");
				return;
			}
			if(EMaskTel1.Text=="")
			{
				MessageBox.Show("Champs Tel Obligatoire");
				return;
			}

		//	if(txtTel.Text.Length!=14 || txtTel.Text.Split(' ').Length!=5)
		//	{
		//		MessageBox.Show("Champs Tel Format 000 000 00 00");
		//		return;
		//	}

			m_Personne["Nom"] =txtNom.Text ;
			m_Personne["Prenom"]=txtPrenom.Text  ;
			m_Personne["DateNaissance"]=txtNaissance.Text  ;
			m_Personne["Tel"]=EMaskTel1.Text  ;
			m_Personne["NumeroDansRue"]=txtNumRue.Text  ;
			m_Personne["Rue"]=txtRue.Text ;
			m_Personne["CodePostal"]=txtNp.Text  ;
			m_Personne["Commune"]=txtCommune.Text ;
			m_Personne["Batiment"]=txtBatiment.Text ;
			m_Personne["Escalier"]=txtEscalier.Text  ;
			m_Personne["Etage"]=txtEtage.Text  ;
			m_Personne["Digicode"]=txtDigicode.Text  ;
			m_Personne["Internom"]=txtInterphone.Text  ;
			m_Personne["Porte"]=txtPorte.Text ;
			m_Personne["TexteSup"]=txtCommentaire.Text  ;
			m_Personne["SuiviPatient"]=txtDossier.Text  ;

            if (m_Personne["IdPersonne"].ToString() == "")
            {
                SosMedecins.SmartRapport.DAL.Fonction z_dalFonction = new SosMedecins.SmartRapport.DAL.Fonction();
                z_dalFonction.CreationPatient(m_Personne, m_Appel);
            }

			this._frm.Close();
		}

		#endregion

	}
}
