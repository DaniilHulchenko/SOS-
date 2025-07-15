using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ImportSosGeneve.Commun
{
	/// <summary>
	/// Description résumée de frmJointDoc.
	/// </summary>
	public class frmJointDoc : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox gbPatient;
		private System.Windows.Forms.Label lbNaissance;
		private System.Windows.Forms.Label lbPrenom;
		private System.Windows.Forms.Label lbNom;
		private System.Windows.Forms.Label lbDtConsult;
		private System.Windows.Forms.Button btVoir;
		private System.Windows.Forms.Button btJoint;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtIdPatient;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmJointDoc()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

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
            this.gbPatient = new System.Windows.Forms.GroupBox();
            this.lbNaissance = new System.Windows.Forms.Label();
            this.lbPrenom = new System.Windows.Forms.Label();
            this.lbNom = new System.Windows.Forms.Label();
            this.lbDtConsult = new System.Windows.Forms.Label();
            this.btVoir = new System.Windows.Forms.Button();
            this.btJoint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIdPatient = new System.Windows.Forms.TextBox();
            this.gbPatient.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbPatient
            // 
            this.gbPatient.Controls.Add(this.lbNaissance);
            this.gbPatient.Controls.Add(this.lbPrenom);
            this.gbPatient.Controls.Add(this.lbNom);
            this.gbPatient.Controls.Add(this.lbDtConsult);
            this.gbPatient.Location = new System.Drawing.Point(208, 80);
            this.gbPatient.Name = "gbPatient";
            this.gbPatient.Size = new System.Drawing.Size(208, 136);
            this.gbPatient.TabIndex = 4;
            this.gbPatient.TabStop = false;
            this.gbPatient.Text = "Patient";
            // 
            // lbNaissance
            // 
            this.lbNaissance.Location = new System.Drawing.Point(16, 112);
            this.lbNaissance.Name = "lbNaissance";
            this.lbNaissance.Size = new System.Drawing.Size(184, 16);
            this.lbNaissance.TabIndex = 3;
            // 
            // lbPrenom
            // 
            this.lbPrenom.Location = new System.Drawing.Point(16, 80);
            this.lbPrenom.Name = "lbPrenom";
            this.lbPrenom.Size = new System.Drawing.Size(184, 16);
            this.lbPrenom.TabIndex = 2;
            // 
            // lbNom
            // 
            this.lbNom.Location = new System.Drawing.Point(16, 56);
            this.lbNom.Name = "lbNom";
            this.lbNom.Size = new System.Drawing.Size(184, 16);
            this.lbNom.TabIndex = 1;
            // 
            // lbDtConsult
            // 
            this.lbDtConsult.Location = new System.Drawing.Point(16, 24);
            this.lbDtConsult.Name = "lbDtConsult";
            this.lbDtConsult.Size = new System.Drawing.Size(184, 16);
            this.lbDtConsult.TabIndex = 0;
            // 
            // btVoir
            // 
            this.btVoir.Location = new System.Drawing.Point(40, 152);
            this.btVoir.Name = "btVoir";
            this.btVoir.Size = new System.Drawing.Size(88, 32);
            this.btVoir.TabIndex = 3;
            this.btVoir.Text = "Voir";
            // 
            // btJoint
            // 
            this.btJoint.Location = new System.Drawing.Point(40, 104);
            this.btJoint.Name = "btJoint";
            this.btJoint.Size = new System.Drawing.Size(88, 32);
            this.btJoint.TabIndex = 2;
            this.btJoint.Text = "Joint";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(72, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "IdPatient";
            // 
            // txtIdPatient
            // 
            this.txtIdPatient.Location = new System.Drawing.Point(152, 16);
            this.txtIdPatient.Name = "txtIdPatient";
            this.txtIdPatient.Size = new System.Drawing.Size(136, 20);
            this.txtIdPatient.TabIndex = 1;
            this.txtIdPatient.Text = "txtIdPatient";
            // 
            // frmJointDoc
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(448, 266);
            this.Controls.Add(this.gbPatient);
            this.Controls.Add(this.btVoir);
            this.Controls.Add(this.btJoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIdPatient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmJointDoc";
            this.Text = "frmJointDoc";
            this.gbPatient.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	}
}
