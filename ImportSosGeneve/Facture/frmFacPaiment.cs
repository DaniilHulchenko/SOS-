using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de FacPaiment.
	/// </summary>
	public class frmFacPaiement : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtDate;
		private System.Windows.Forms.TextBox txtMontant;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtPayeCommentaire;
		private System.Windows.Forms.Button btnAnnuler;
		private System.Windows.Forms.Button btnValider;
		private System.Windows.Forms.ComboBox cbMoyen;
		private System.ComponentModel.Container components = null;
		private bool m_bOk = false;
		private System.Data.DataRow m_rowFacture=null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtDateSal;
        private GroupBox groupBox1;
		private System.Windows.Forms.Label label5;

		public frmFacPaiement(System.Data.DataRow row)
		{
			InitializeComponent();
			this.m_rowFacture	= row;
			txtDate.Text =  DateTime.Now.ToString("dd.MM.yyyy");	
			txtMontant.Text = m_rowFacture["Solde"].ToString();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFacPaiement));
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtMontant = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.btnValider = new System.Windows.Forms.Button();
            this.txtPayeCommentaire = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbMoyen = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDateSal = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(108, 19);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(87, 20);
            this.txtDate.TabIndex = 1;
            // 
            // txtMontant
            // 
            this.txtMontant.Location = new System.Drawing.Point(108, 71);
            this.txtMontant.Name = "txtMontant";
            this.txtMontant.Size = new System.Drawing.Size(87, 20);
            this.txtMontant.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 24);
            this.label3.TabIndex = 8;
            this.label3.Text = "Commentaire :";
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnuler.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Fermer;
            this.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuler.Location = new System.Drawing.Point(389, 184);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(76, 34);
            this.btnAnnuler.TabIndex = 2;
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // btnValider
            // 
            this.btnValider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValider.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Ok;
            this.btnValider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnValider.Location = new System.Drawing.Point(307, 184);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(76, 34);
            this.btnValider.TabIndex = 1;
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // txtPayeCommentaire
            // 
            this.txtPayeCommentaire.Location = new System.Drawing.Point(108, 97);
            this.txtPayeCommentaire.Multiline = true;
            this.txtPayeCommentaire.Name = "txtPayeCommentaire";
            this.txtPayeCommentaire.Size = new System.Drawing.Size(338, 56);
            this.txtPayeCommentaire.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(216, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "Moyen :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cbMoyen
            // 
            this.cbMoyen.Items.AddRange(new object[] {
            "CDM",
            "SBVR",
            "CCP",
            "CS",
            "BM",
            "AT",
            "AUCUN"});
            this.cbMoyen.Location = new System.Drawing.Point(278, 70);
            this.cbMoyen.Name = "cbMoyen";
            this.cbMoyen.Size = new System.Drawing.Size(168, 21);
            this.cbMoyen.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Montant :";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Date Paiment :";
            // 
            // txtDateSal
            // 
            this.txtDateSal.Location = new System.Drawing.Point(108, 45);
            this.txtDateSal.Name = "txtDateSal";
            this.txtDateSal.Size = new System.Drawing.Size(87, 20);
            this.txtDateSal.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Date Pour Salaire :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbMoyen);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDate);
            this.groupBox1.Controls.Add(this.txtPayeCommentaire);
            this.groupBox1.Controls.Add(this.txtDateSal);
            this.groupBox1.Controls.Add(this.txtMontant);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 165);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // frmFacPaiement
            // 
            this.AcceptButton = this.btnValider;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.CancelButton = this.btnAnnuler;
            this.ClientSize = new System.Drawing.Size(477, 230);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnValider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFacPaiement";
            this.Text = "Paiement isolé";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void btnAnnuler_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnValider_Click(object sender, System.EventArgs e)
		{
			DateTime DtSal = DateTime.Parse("01-01-1900");
			DateTime DtPaiement = DateTime.Parse("01-01-1900");
			if (txtDate.Text != "") 
				DtPaiement = DateTime.Parse(txtDate.Text+ " 00:00:01");
			if (txtDateSal.Text != "") 
				DtSal = DateTime.Parse(txtDateSal.Text+ " 00:00:01");
			if (txtMontant.Text == "") txtMontant.Text = "0";
			if (txtMontant.Text == "0") MessageBox.Show("Pas de Montant !! ");
			float fltMontant = float.Parse(txtMontant.Text);
			float fltTotal = float.Parse(txtMontant.Text);
			//Perte et Profit - Salary date is empty
			if (cbMoyen.Text == "AUCUN")
				DtSal = DateTime.Parse("01-01-1900");

            OutilsExt.OutilsSql.EnregistrePaiement(long.Parse(m_rowFacture["NFacture"].ToString()), fltMontant, fltTotal, txtPayeCommentaire.Text, cbMoyen.Text, DtPaiement, DtSal);
			//Put Perte et Profit for the solde
			if (float.Parse(m_rowFacture["Solde"].ToString())-float.Parse(txtMontant.Text)>0)
			{
				fltTotal = float.Parse(m_rowFacture["Solde"].ToString())-float.Parse(txtMontant.Text);
				DialogResult result = MessageBox.Show("Perte et Profit le Solde de "+ fltTotal.ToString() +" CHF?" ,"PP",MessageBoxButtons.YesNo,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1);
				if(result==DialogResult.Yes)
				{
                    DtSal = DateTime.Parse("01-01-1900");
                    OutilsExt.OutilsSql.EnregistreEtatFacture(long.Parse(m_rowFacture["NFacture"].ToString()), 6, DtPaiement, "Perte et Profit", "", "", fltTotal, DtSal);
                    OutilsExt.OutilsSql.ExecuteCommandeSansRetour("update facture set Solde = 0 WHERE NFacture = " + long.Parse(m_rowFacture["NFacture"].ToString()));
                    OutilsExt.OutilsSql.ExecuteCommandeSansRetour("update facture_status set FacDateAcquittee = '" + OutilsExt.OutilsSql.DateFormatMySql(DtPaiement) + "' WHERE NFacture = " + long.Parse(m_rowFacture["NFacture"].ToString()));

                    SosMedecins.SmartRapport.DAL.Fonction z_objFonction = new SosMedecins.SmartRapport.DAL.Fonction();
                    z_objFonction.EncaissementSurPlace(long.Parse(m_rowFacture["NFacture"].ToString()));
                }
			}
			m_bOk = true;
			this.Close();
		}

		public bool bOk
		{
			get
			{
				return m_bOk;
			}
		}
	}
}
