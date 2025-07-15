using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ImportSosGeneve
{
	public class frmAssurance : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnValider;
		private System.Windows.Forms.Button btnAnnuler;
		private System.Windows.Forms.TextBox txtNom;
		private System.Windows.Forms.TextBox txtTelephone;
		private System.Windows.Forms.TextBox txtFax;
		private System.Windows.Forms.TextBox txtCommentaire;
		private System.Windows.Forms.Label label8;		
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox txtAdresse;
		private System.Windows.Forms.ComboBox cbAss;
		private System.Windows.Forms.TextBox txtFindAss;
		private CtrlDest.TypeOuverture m_TypeOuverture;
		public DateTime DateDebut;
		public DateTime DateFin;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
		private DataRow m_SelectedRow=null;

		public frmAssurance(CtrlDest.TypeOuverture mTypeOuverture)
		{
			InitializeComponent();
			m_TypeOuverture=mTypeOuverture;
			FicheVierge();
		}

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

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssurance));
            this.label1 = new System.Windows.Forms.Label();
            this.cbAss = new System.Windows.Forms.ComboBox();
            this.txtFindAss = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAdresse = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCommentaire = new System.Windows.Forms.TextBox();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnValider = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nom Assurance :";
            // 
            // cbAss
            // 
            this.cbAss.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAss.Location = new System.Drawing.Point(103, 45);
            this.cbAss.Name = "cbAss";
            this.cbAss.Size = new System.Drawing.Size(192, 21);
            this.cbAss.TabIndex = 2;
            this.cbAss.SelectedIndexChanged += new System.EventHandler(this.cbTiers_SelectedIndexChanged);
            // 
            // txtFindAss
            // 
            this.txtFindAss.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFindAss.Location = new System.Drawing.Point(103, 19);
            this.txtFindAss.Name = "txtFindAss";
            this.txtFindAss.Size = new System.Drawing.Size(192, 20);
            this.txtFindAss.TabIndex = 1;
            this.txtFindAss.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFindTiers_KeyUp);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Résultats :";
            // 
            // txtAdresse
            // 
            this.txtAdresse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdresse.Location = new System.Drawing.Point(103, 160);
            this.txtAdresse.Multiline = true;
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.Size = new System.Drawing.Size(192, 56);
            this.txtAdresse.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "Adresse :";
            // 
            // txtCommentaire
            // 
            this.txtCommentaire.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommentaire.Location = new System.Drawing.Point(103, 98);
            this.txtCommentaire.Multiline = true;
            this.txtCommentaire.Name = "txtCommentaire";
            this.txtCommentaire.Size = new System.Drawing.Size(192, 56);
            this.txtCommentaire.TabIndex = 7;
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(103, 72);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(120, 20);
            this.txtFax.TabIndex = 6;
            // 
            // txtTelephone
            // 
            this.txtTelephone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTelephone.Location = new System.Drawing.Point(103, 46);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(192, 20);
            this.txtTelephone.TabIndex = 5;
            // 
            // txtNom
            // 
            this.txtNom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNom.Location = new System.Drawing.Point(103, 20);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(192, 20);
            this.txtNom.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "Commentaire :";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "Fax :";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Téléphone :";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Nom :";
            // 
            // btnValider
            // 
            this.btnValider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValider.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Ok;
            this.btnValider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnValider.Location = new System.Drawing.Point(147, 323);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(80, 34);
            this.btnValider.TabIndex = 6;
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnuler.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Fermer;
            this.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuler.Location = new System.Drawing.Point(233, 323);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(80, 34);
            this.btnAnnuler.TabIndex = 7;
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtAdresse);
            this.groupBox1.Controls.Add(this.txtCommentaire);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtFax);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtTelephone);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtNom);
            this.groupBox1.Location = new System.Drawing.Point(12, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 225);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtFindAss);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbAss);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(301, 80);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Recherche d\'une assurance";
            // 
            // frmAssurance
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.CancelButton = this.btnAnnuler;
            this.ClientSize = new System.Drawing.Size(325, 369);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnValider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAssurance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choix Assurance";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		
		private void RemplirListeAssurance(DataSet ds)
		{
			FicheVierge();
			cbAss.Items.Clear();
			cbAss.Text="";
			for(int i=0;i<ds.Tables[0].Rows.Count;i++)
			{
				ListItem item = new ListItem(ds.Tables[0].Rows[i],ds.Tables[0].Rows[i]["AssNom"].ToString());
				cbAss.Items.Add(item);
			}
			if(cbAss.Items.Count>0)
				cbAss.SelectedIndex=0;
		}

		private void btnAnnuler_Click(object sender, System.EventArgs e)
		{
			m_SelectedRow=null;
			this.Close();
		}

		private void btnValider_Click(object sender, System.EventArgs e)
		{
			if(m_TypeOuverture==CtrlDest.TypeOuverture.ListeFacture)
			{
				frmSelectionDate selectDate = new frmSelectionDate();
				selectDate.ShowDialog();
				this.DateDebut = selectDate.dt1.Value;
				this.DateFin = selectDate.dt2.Value;
				selectDate.Dispose();
				selectDate=null;
			}

            this.Close();
		}

		private void FicheVierge()
		{
			txtNom.Text="";
			txtTelephone.Text="";
			txtFax.Text="";
			txtCommentaire.Text="";
			txtAdresse.Text="";
		}

		private void RemplirFiche(DataRow row)
		{
			if(row!=null)
			{
				m_SelectedRow=row;
				txtNom.Text=row["AssNom"].ToString();
				txtTelephone.Text=row["AssTelephone"].ToString();
				txtFax.Text=row["AssFax"].ToString();
				txtCommentaire.Text=row["AssCommentaire"].ToString();
				txtAdresse.Text=row["NumDansRue"].ToString() + " " + row["Rue"].ToString() + " " + row["Np"].ToString() + " " + row["Commune"].ToString() + " "  ;
			}
		}

		public Dest GetDestinataire()
		{
			if(m_SelectedRow!=null)
			{
				Dest dest = new Dest();
				dest.m_TypeDestinataire = CtrlDest.TypeDestinataire.Assurance;
                dest.CodeDestinataireFacture = int.Parse(m_SelectedRow["NAssurance"].ToString());
				dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromAssurance(dest.CodeDestinataireFacture);

				if(m_TypeOuverture==CtrlDest.TypeOuverture.ListeFacture)
				{
					dest.DateDebut = DateDebut;
					dest.DateFin = DateFin;
				}

				return dest;
			}
			else
				return null;
			
		}

		private void txtFindTiers_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				DataSet  ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT h.NAssurance,h.AssNom,h.AssTelephone,h.AssFax,h.AssCommentaire,a.NumDansRue,a.Rue,a.Np,a.Commune from assurances h inner join adresses a on a.NAdresse = h.NAdresse WHERE h.AssNom like '%" + txtFindAss.Text.Replace("'","''") + "%' AND AssEAN Is Not null");
				RemplirListeAssurance(ds);				
			}
		}

		private void cbTiers_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cbAss.SelectedIndex>-1)
			{
				RemplirFiche((DataRow)  ((ListItem)cbAss.SelectedItem).objValue );
			}
		}
	}
}
