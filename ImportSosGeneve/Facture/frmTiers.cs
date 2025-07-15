using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ImportSosGeneve
{
	public class frmTiers : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel1;
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
		private System.Windows.Forms.ComboBox cbTiers;
		private System.Windows.Forms.TextBox txtFindTiers;


		private DataRow m_SelectedRow=null;

		private CtrlDest.TypeOuverture m_TypeOuverture;

		public DateTime DateDebut;
		private System.Windows.Forms.Button button1;
		public DateTime DateFin;

		public frmTiers(CtrlDest.TypeOuverture mTypeOuverture)
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbTiers = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFindTiers = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nom Tiers :";
            // 
            // cbTiers
            // 
            this.cbTiers.Location = new System.Drawing.Point(64, 64);
            this.cbTiers.Name = "cbTiers";
            this.cbTiers.Size = new System.Drawing.Size(258, 21);
            this.cbTiers.TabIndex = 4;
            this.cbTiers.SelectedIndexChanged += new System.EventHandler(this.cbTiers_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(312, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Recherche d\'un tiers :";
            // 
            // txtFindTiers
            // 
            this.txtFindTiers.Location = new System.Drawing.Point(69, 32);
            this.txtFindTiers.Name = "txtFindTiers";
            this.txtFindTiers.Size = new System.Drawing.Size(252, 20);
            this.txtFindTiers.TabIndex = 2;
            this.txtFindTiers.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFindTiers_KeyUp);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Résultats :";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtAdresse);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtCommentaire);
            this.panel1.Controls.Add(this.txtFax);
            this.panel1.Controls.Add(this.txtTelephone);
            this.panel1.Controls.Add(this.txtNom);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(5, 96);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 271);
            this.panel1.TabIndex = 5;
            // 
            // txtAdresse
            // 
            this.txtAdresse.Location = new System.Drawing.Point(9, 199);
            this.txtAdresse.Multiline = true;
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.Size = new System.Drawing.Size(304, 56);
            this.txtAdresse.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(12, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "Adresse :";
            // 
            // txtCommentaire
            // 
            this.txtCommentaire.Location = new System.Drawing.Point(10, 98);
            this.txtCommentaire.Multiline = true;
            this.txtCommentaire.Name = "txtCommentaire";
            this.txtCommentaire.ReadOnly = true;
            this.txtCommentaire.Size = new System.Drawing.Size(304, 56);
            this.txtCommentaire.TabIndex = 7;
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(89, 51);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(120, 20);
            this.txtFax.TabIndex = 5;
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(89, 29);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(120, 20);
            this.txtTelephone.TabIndex = 3;
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(90, 5);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(218, 20);
            this.txtNom.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "Commentaire :";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Fax :";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(7, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Téléphone :";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 8);
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
            this.btnValider.Location = new System.Drawing.Point(160, 376);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(80, 34);
            this.btnValider.TabIndex = 7;
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnuler.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Fermer;
            this.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAnnuler.Location = new System.Drawing.Point(246, 376);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(80, 34);
            this.btnAnnuler.TabIndex = 8;
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.button1.Location = new System.Drawing.Point(11, 377);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 32);
            this.button1.TabIndex = 6;
            this.button1.Text = "Sauvegarde Destinataire Facture 10%";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmTiers
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(338, 422);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnValider);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFindTiers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbTiers);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmTiers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choix Tiers";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		private void RemplirListeTiers(DataSet ds)
		{
			FicheVierge();
			cbTiers.Items.Clear();
			cbTiers.Text="";
			for(int i=0;i<ds.Tables[0].Rows.Count;i++)
			{	
				ListItem item = new ListItem(ds.Tables[0].Rows[i],ds.Tables[0].Rows[i]["TiersNom"].ToString()+" "+ds.Tables[0].Rows[i]["Commune"].ToString());
				cbTiers.Items.Add(item);
			}
			if(cbTiers.Items.Count>0)
				cbTiers.SelectedIndex=0;
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
				txtNom.Text=row["TiersNom"].ToString();
				txtTelephone.Text=row["TiersTelephone"].ToString();
				txtFax.Text=row["TiersFax"].ToString();
				txtCommentaire.Text=row["TiersCommentaire"].ToString();
				txtAdresse.Text=row["NumDansRue"].ToString() + " " + row["Rue"].ToString() + " " + row["Np"].ToString() + " " + row["Commune"].ToString() + " "  ;
			}
		}

		public Dest GetDestinataire()
		{
			if(m_SelectedRow!=null)
			{
				Dest dest = new Dest();
				dest.m_TypeDestinataire = CtrlDest.TypeDestinataire.Tiers;
                dest.CodeDestinataireFacture = int.Parse(m_SelectedRow["NTiers"].ToString());
				dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromTiers(dest.CodeDestinataireFacture);

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
				DataSet  ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT h.NTiers,h.TiersNom,h.TiersTelephone,h.TiersFax,h.TiersCommentaire,a.NumDansRue,a.Rue,a.Np,a.Commune from tiers h inner join adresses a on a.NAdresse = h.NAdresse WHERE h.TiersNom like '%" + txtFindTiers.Text.Replace("'","''") + "%'");
				RemplirListeTiers(ds);				
			}
		}

		private void cbTiers_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cbTiers.SelectedIndex>-1)
			{
				RemplirFiche((DataRow)  ((ListItem)cbTiers.SelectedItem).objValue );
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			frmSelectionDate selectDate = new frmSelectionDate();
			selectDate.ShowDialog();
			this.DateDebut = selectDate.dt1.Value;
			this.DateFin = selectDate.dt2.Value;
			selectDate.Dispose();
			selectDate=null;
			this.Close();
		}
	}
}
