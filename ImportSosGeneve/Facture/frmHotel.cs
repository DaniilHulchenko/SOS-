using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de frmHotel.
	/// </summary>
	public class frmHotel : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbHotel;
		private System.Windows.Forms.TextBox txtFindHotel;
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
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox txtAdresse;


		private CtrlDest.TypeOuverture m_TypeOuverture;

		public DateTime DateDebut;
		public DateTime DateFin;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private PrintPreviewDialog printPreviewDialog1;


		private DataRow m_SelectedRow=null;

		public frmHotel(CtrlDest.TypeOuverture mTypeOuverture)
		{
			InitializeComponent();
			m_TypeOuverture=mTypeOuverture;

			FicheVierge();
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

		#region Code généré par le Concepteur Windows Form
		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHotel));
            this.label1 = new System.Windows.Forms.Label();
            this.cbHotel = new System.Windows.Forms.ComboBox();
            this.txtFindHotel = new System.Windows.Forms.TextBox();
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
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nom Hôtel :";
            // 
            // cbHotel
            // 
            this.cbHotel.Location = new System.Drawing.Point(87, 55);
            this.cbHotel.Name = "cbHotel";
            this.cbHotel.Size = new System.Drawing.Size(256, 21);
            this.cbHotel.TabIndex = 3;
            this.cbHotel.SelectedIndexChanged += new System.EventHandler(this.cbHotel_SelectedIndexChanged);
            // 
            // txtFindHotel
            // 
            this.txtFindHotel.Location = new System.Drawing.Point(87, 29);
            this.txtFindHotel.Name = "txtFindHotel";
            this.txtFindHotel.Size = new System.Drawing.Size(256, 20);
            this.txtFindHotel.TabIndex = 1;
            this.txtFindHotel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFindHotel_KeyUp);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Résultats :";
            // 
            // txtAdresse
            // 
            this.txtAdresse.Location = new System.Drawing.Point(87, 190);
            this.txtAdresse.Multiline = true;
            this.txtAdresse.Name = "txtAdresse";
            this.txtAdresse.Size = new System.Drawing.Size(256, 94);
            this.txtAdresse.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(6, 193);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "Adresse :";
            // 
            // txtCommentaire
            // 
            this.txtCommentaire.Location = new System.Drawing.Point(87, 91);
            this.txtCommentaire.Multiline = true;
            this.txtCommentaire.Name = "txtCommentaire";
            this.txtCommentaire.Size = new System.Drawing.Size(256, 93);
            this.txtCommentaire.TabIndex = 7;
            // 
            // txtFax
            // 
            this.txtFax.Location = new System.Drawing.Point(87, 65);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(120, 20);
            this.txtFax.TabIndex = 5;
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(87, 39);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(120, 20);
            this.txtTelephone.TabIndex = 3;
            // 
            // txtNom
            // 
            this.txtNom.Location = new System.Drawing.Point(87, 13);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(256, 20);
            this.txtNom.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(6, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "Commentaire :";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Fax :";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Téléphone :";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 16);
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
            this.btnValider.Location = new System.Drawing.Point(191, 408);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(80, 34);
            this.btnValider.TabIndex = 2;
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnuler.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Fermer;
            this.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuler.Location = new System.Drawing.Point(277, 408);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(80, 34);
            this.btnAnnuler.TabIndex = 3;
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbHotel);
            this.groupBox1.Controls.Add(this.txtFindHotel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 91);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recherche d\'un hôtel ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtAdresse);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtCommentaire);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtFax);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtTelephone);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtNom);
            this.groupBox2.Location = new System.Drawing.Point(8, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 295);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // frmHotel
            // 
            this.AcceptButton = this.btnValider;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.CancelButton = this.btnAnnuler;
            this.ClientSize = new System.Drawing.Size(369, 454);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnValider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHotel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choix Hotel";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void cbHotel_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cbHotel.SelectedIndex>-1)
			{
				RemplirFiche((DataRow)  ((ListItem)cbHotel.SelectedItem).objValue );
			}
		}

		private void txtFindHotel_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				DataSet  ds = OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet("SELECT h.NHotel,h.HotNom,h.HotTelephone,h.HotFax,h.HotComm,a.NumDansRue,a.Rue,a.Np,a.Commune from hotels h inner join adresses a on a.NAdresse = h.NAdresse WHERE h.HotNom like '%" + txtFindHotel.Text.Replace("'","''") + "%'");
				RemplirListeHotel(ds);				
			}
		}

		private void RemplirListeHotel(DataSet ds)
		{
			FicheVierge();
			cbHotel.Items.Clear();
			cbHotel.Text="";
			for(int i=0;i<ds.Tables[0].Rows.Count;i++)
			{
				ListItem item = new ListItem(ds.Tables[0].Rows[i],ds.Tables[0].Rows[i]["HotNom"].ToString());
				cbHotel.Items.Add(item);
			}
			if(cbHotel.Items.Count>0)
				cbHotel.SelectedIndex=0;
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
				m_SelectedRow = row;
				txtNom.Text=row["HotNom"].ToString();
				txtTelephone.Text=row["HotTelephone"].ToString();
				txtFax.Text=row["HotFax"].ToString();
				txtCommentaire.Text=row["HotComm"].ToString();
				txtAdresse.Text=row["NumDansRue"].ToString() + " " + row["Rue"].ToString() + " " + row["Np"].ToString() + " " + row["Commune"].ToString() + " "  ;
			}
		}

		public Dest GetDestinataire()
		{
			if(m_SelectedRow!=null)
			{
				Dest dest = new Dest();
				dest.m_TypeDestinataire = CtrlDest.TypeDestinataire.Hotel;
                dest.CodeDestinataireFacture = int.Parse(m_SelectedRow["NHotel"].ToString());
				dest.AdresseDestinataire = OutilsExt.OutilsSql.GetAdresseFromHotel(dest.CodeDestinataireFacture);

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
	}
}
