using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ImportSosGeneve
{
	public class frmFacDuplicata : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtEnvoye;
		private System.Windows.Forms.Button btnValider;
		private System.Windows.Forms.Button btnAnnuler;
		private System.Windows.Forms.TextBox txtDemande;
		private System.Windows.Forms.TextBox txtCommentaire;
		private System.Data.DataRow m_rowFacture=null;
		private long m_NFacture=-1;
		private bool m_bOk=false;
        private GroupBox groupBox1;
        private System.ComponentModel.Container components = null;

        public string TypeBVR = "Normal";     //Normal ou QRCode

        public frmFacDuplicata(System.Data.DataRow row)
		{			
			InitializeComponent();

			this.m_rowFacture = row;
			this.m_NFacture = long.Parse(row["NFacture"].ToString());
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFacDuplicata));
            this.txtDemande = new System.Windows.Forms.TextBox();
            this.txtEnvoye = new System.Windows.Forms.TextBox();
            this.txtCommentaire = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnValider = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDemande
            // 
            this.txtDemande.Location = new System.Drawing.Point(101, 15);
            this.txtDemande.Name = "txtDemande";
            this.txtDemande.Size = new System.Drawing.Size(292, 20);
            this.txtDemande.TabIndex = 1;
            this.txtDemande.Text = "Patient";
            this.txtDemande.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDemande_KeyUp);
            // 
            // txtEnvoye
            // 
            this.txtEnvoye.Location = new System.Drawing.Point(101, 41);
            this.txtEnvoye.Name = "txtEnvoye";
            this.txtEnvoye.Size = new System.Drawing.Size(292, 20);
            this.txtEnvoye.TabIndex = 3;
            this.txtEnvoye.Text = "Patient";
            // 
            // txtCommentaire
            // 
            this.txtCommentaire.Location = new System.Drawing.Point(101, 67);
            this.txtCommentaire.Name = "txtCommentaire";
            this.txtCommentaire.Size = new System.Drawing.Size(292, 20);
            this.txtCommentaire.TabIndex = 5;
            this.txtCommentaire.Text = "DUPLICATA";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Demandé par :";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Commentaire :";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Envoyé au :";
            // 
            // btnValider
            // 
            this.btnValider.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Ok;
            this.btnValider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnValider.Location = new System.Drawing.Point(237, 136);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(76, 34);
            this.btnValider.TabIndex = 1;
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Fermer;
            this.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuler.Location = new System.Drawing.Point(335, 136);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(76, 34);
            this.btnAnnuler.TabIndex = 2;
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDemande);
            this.groupBox1.Controls.Add(this.txtEnvoye);
            this.groupBox1.Controls.Add(this.txtCommentaire);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 98);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // frmFacDuplicata
            // 
            this.AcceptButton = this.btnValider;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.CancelButton = this.btnAnnuler;
            this.ClientSize = new System.Drawing.Size(460, 189);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnValider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFacDuplicata";
            this.Text = "Demande de Duplicata";
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
            //Enregistrement du duplicata :
            OutilsExt.OutilsSql.EnregistreDuplicata(long.Parse(m_rowFacture["NFacture"].ToString()),txtCommentaire.Text,txtDemande.Text,txtEnvoye.Text);
			m_bOk = true;
			this.Close();
			
			frmImpressionFacture imprFac = new frmImpressionFacture(m_rowFacture,2);
			imprFac.ShowDialog();
			imprFac.Dispose();
			imprFac=null;		
		}

		private void txtDemande_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Enter)
			{
				txtEnvoye.Focus();
			}
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
