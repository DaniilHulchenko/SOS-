using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de frmModifRapport.
	/// </summary>
	public class frmModifRapport : System.Windows.Forms.Form
    {
		private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.Button btnValider;
		private System.Windows.Forms.Label LblSauvegardeRapport;
        private GroupBox groupBox1;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label1;
        private TextBox txtSing;
        private TextBox txtSalut;
        private TextBox txtIntro;
        private TextBox txtBonjour;
        private TextBox txtConc;
        private TextBox txtTete;
        private TextBox txtNRapport;

        /// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmModifRapport(string EnTete,string EnDest, string EnConc, string EnBonjour, string EnIntro, string EnSalut, string EnSing, long NoRapport)
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
			txtTete.Text = EnTete;
			txtNRapport.Text = NoRapport.ToString();
			txtConc.Text = EnConc;
			txtBonjour.Text = EnBonjour;
			txtIntro.Text = EnIntro;
			txtSalut.Text = EnSalut;
			txtSing.Text = EnSing;

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
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
            this.btnValider = new System.Windows.Forms.Button();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.LblSauvegardeRapport = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSing = new System.Windows.Forms.TextBox();
            this.txtSalut = new System.Windows.Forms.TextBox();
            this.txtIntro = new System.Windows.Forms.TextBox();
            this.txtBonjour = new System.Windows.Forms.TextBox();
            this.txtConc = new System.Windows.Forms.TextBox();
            this.txtTete = new System.Windows.Forms.TextBox();
            this.txtNRapport = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnValider
            // 
            this.btnValider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValider.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Fermer;
            this.btnValider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnValider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValider.Location = new System.Drawing.Point(620, 385);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(80, 34);
            this.btnValider.TabIndex = 1;
            this.btnValider.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnuler.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Ok;
            this.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuler.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnnuler.Location = new System.Drawing.Point(534, 385);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(80, 34);
            this.btnAnnuler.TabIndex = 2;
            this.btnAnnuler.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // LblSauvegardeRapport
            // 
            this.LblSauvegardeRapport.Location = new System.Drawing.Point(184, 440);
            this.LblSauvegardeRapport.Name = "LblSauvegardeRapport";
            this.LblSauvegardeRapport.Size = new System.Drawing.Size(312, 16);
            this.LblSauvegardeRapport.TabIndex = 17;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSing);
            this.groupBox1.Controls.Add(this.txtSalut);
            this.groupBox1.Controls.Add(this.txtIntro);
            this.groupBox1.Controls.Add(this.txtBonjour);
            this.groupBox1.Controls.Add(this.txtConc);
            this.groupBox1.Controls.Add(this.txtTete);
            this.groupBox1.Location = new System.Drawing.Point(11, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(689, 367);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 316);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 24);
            this.label7.TabIndex = 10;
            this.label7.Text = "Signature";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 278);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 24);
            this.label6.TabIndex = 8;
            this.label6.Text = "Salutation";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "Introduction";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 24);
            this.label4.TabIndex = 4;
            this.label4.Text = "Bonjour";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "Concerne";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "EnTete";
            // 
            // txtSing
            // 
            this.txtSing.Location = new System.Drawing.Point(178, 321);
            this.txtSing.Multiline = true;
            this.txtSing.Name = "txtSing";
            this.txtSing.Size = new System.Drawing.Size(190, 32);
            this.txtSing.TabIndex = 11;
            // 
            // txtSalut
            // 
            this.txtSalut.Location = new System.Drawing.Point(178, 283);
            this.txtSalut.Multiline = true;
            this.txtSalut.Name = "txtSalut";
            this.txtSalut.Size = new System.Drawing.Size(505, 32);
            this.txtSalut.TabIndex = 9;
            // 
            // txtIntro
            // 
            this.txtIntro.Location = new System.Drawing.Point(178, 213);
            this.txtIntro.Multiline = true;
            this.txtIntro.Name = "txtIntro";
            this.txtIntro.Size = new System.Drawing.Size(505, 64);
            this.txtIntro.TabIndex = 7;
            // 
            // txtBonjour
            // 
            this.txtBonjour.Location = new System.Drawing.Point(178, 135);
            this.txtBonjour.Multiline = true;
            this.txtBonjour.Name = "txtBonjour";
            this.txtBonjour.Size = new System.Drawing.Size(505, 72);
            this.txtBonjour.TabIndex = 5;
            // 
            // txtConc
            // 
            this.txtConc.Location = new System.Drawing.Point(178, 49);
            this.txtConc.Multiline = true;
            this.txtConc.Name = "txtConc";
            this.txtConc.Size = new System.Drawing.Size(505, 80);
            this.txtConc.TabIndex = 3;
            // 
            // txtTete
            // 
            this.txtTete.Location = new System.Drawing.Point(178, 19);
            this.txtTete.Multiline = true;
            this.txtTete.Name = "txtTete";
            this.txtTete.Size = new System.Drawing.Size(505, 24);
            this.txtTete.TabIndex = 1;
            // 
            // txtNRapport
            // 
            this.txtNRapport.Location = new System.Drawing.Point(392, 920);
            this.txtNRapport.Name = "txtNRapport";
            this.txtNRapport.Size = new System.Drawing.Size(104, 20);
            this.txtNRapport.TabIndex = 29;
            this.txtNRapport.Visible = false;
            // 
            // frmModifRapport
            // 
            this.AcceptButton = this.btnValider;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.CancelButton = this.btnAnnuler;
            this.ClientSize = new System.Drawing.Size(712, 431);
            this.Controls.Add(this.txtNRapport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LblSauvegardeRapport);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnValider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmModifRapport";
            this.Text = "Modification de Rapport";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnValider_Click(object sender, System.EventArgs e)
		{
			long NRapport = long.Parse(txtNRapport.Text);
			bool reusite = false;
            reusite = OutilsExt.OutilsSql.ExecuteCommandeSansRetour("UPDATE tablerapports set RapEnTete='" + txtTete.Text.Replace("'", "''") + "',RapConcerne = '" + txtConc.Text.Replace("'", "''") + "',RapSignature = '" + txtSing.Text.Replace("'", "''") + "' WHERE NRapport = " + NRapport);
            reusite = OutilsExt.OutilsSql.ExecuteCommandeSansRetour("UPDATE tablerapportdestine set RapBonjour='" + txtBonjour.Text.Replace("'", "''") + "',RapIntroduction = '" + txtIntro.Text.Replace("'", "''") + "',RapSalutation = '" + txtSalut.Text.Replace("'", "''") + "' WHERE NRapport = " + NRapport);
				
			if (reusite == true)
			{
				LblSauvegardeRapport.Text = "Sauvegarde réussie";
			}			
			else
			{
				LblSauvegardeRapport.Text = "Sauvegarde échouée!!!";
			}
		}

		private void btnAnnuler_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
