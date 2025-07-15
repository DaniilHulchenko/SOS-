using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using SosMedecins.SmartRapport.Systeme;

namespace  SosMedecins.SmartRapport.Facturation

{
	public class frmFacAnnuler : System.Windows.Forms.Form
    {
        //public static MySql OutilsSql = null;
		private System.Windows.Forms.Button btnAnnuler;
		private System.Windows.Forms.Button btnValider;
		private System.ComponentModel.Container components = null;
		private bool m_bOk = false;
        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtComm;
        private RadioButton rdkAnnuler;
        private RadioButton rdkRediter;
		private System.Data.DataRow m_rowFacture=null;

		public frmFacAnnuler(System.Data.DataRow row)
		{
			InitializeComponent();
			this.m_rowFacture	= row;
		}
        /* public frmFacAnnuler()
          {
              InitializeComponent();
          }*/

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFacAnnuler));
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.btnValider = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtComm = new System.Windows.Forms.TextBox();
            this.rdkAnnuler = new System.Windows.Forms.RadioButton();
            this.rdkRediter = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAnnuler.BackgroundImage")));
            this.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuler.Location = new System.Drawing.Point(336, 102);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(76, 34);
            this.btnAnnuler.TabIndex = 24;
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // btnValider
            // 
            this.btnValider.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnValider.BackgroundImage")));
            this.btnValider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnValider.Location = new System.Drawing.Point(250, 102);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(80, 34);
            this.btnValider.TabIndex = 23;
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtComm);
            this.groupBox1.Controls.Add(this.rdkAnnuler);
            this.groupBox1.Controls.Add(this.rdkRediter);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 84);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Commentaire";
            // 
            // txtComm
            // 
            this.txtComm.Location = new System.Drawing.Point(102, 48);
            this.txtComm.Name = "txtComm";
            this.txtComm.Size = new System.Drawing.Size(292, 20);
            this.txtComm.TabIndex = 6;
            // 
            // rdkAnnuler
            // 
            this.rdkAnnuler.BackColor = System.Drawing.Color.Transparent;
            this.rdkAnnuler.Checked = true;
            this.rdkAnnuler.Location = new System.Drawing.Point(27, 19);
            this.rdkAnnuler.Name = "rdkAnnuler";
            this.rdkAnnuler.Size = new System.Drawing.Size(69, 26);
            this.rdkAnnuler.TabIndex = 4;
            this.rdkAnnuler.TabStop = true;
            this.rdkAnnuler.Text = "Annuler";
            this.rdkAnnuler.UseVisualStyleBackColor = false;
            // 
            // rdkRediter
            // 
            this.rdkRediter.BackColor = System.Drawing.Color.Transparent;
            this.rdkRediter.Location = new System.Drawing.Point(102, 22);
            this.rdkRediter.Name = "rdkRediter";
            this.rdkRediter.Size = new System.Drawing.Size(88, 20);
            this.rdkRediter.TabIndex = 5;
            this.rdkRediter.Text = "Ré-éditer";
            this.rdkRediter.UseVisualStyleBackColor = false;
            // 
            // frmFacAnnuler
            // 
            this.AcceptButton = this.btnValider;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.CancelButton = this.btnAnnuler;
            this.ClientSize = new System.Drawing.Size(424, 145);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnValider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFacAnnuler";
            this.Text = "Annuler une Facture";
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
			// Enregistrement de l'annulation :
            ImportSosGeneve.MySql outilsSql1 = new ImportSosGeneve.MySql(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli, Application.StartupPath + "\\Error.log");
            outilsSql1.EnregistreAnnulation(long.Parse(m_rowFacture["NFacture"].ToString()),txtComm.Text,rdkRediter.Checked);
            //OutilsSql.EnregistreAnnulation(long.Parse(m_rowFacture["NFacture"].ToString()),txtComm.Text,rdkRediter.Checked);

			if(rdkRediter.Checked)
			{
				//OutilsExt.OutilsSql.GetNewFactureWithNConsult(long.Parse(m_rowFacture["NConsultation"].ToString()));
                ImportSosGeneve.MySql outilsSql = new ImportSosGeneve.MySql(SosMedecins.SmartRapport.Systeme.OutilsExt.ParamAppli, Application.StartupPath + "\\Error.log");
                outilsSql.GetNewFactureWithNConsult(long.Parse(m_rowFacture["NConsultation"].ToString()));
                
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
