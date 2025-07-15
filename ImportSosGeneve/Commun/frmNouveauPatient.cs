using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description r�sum�e de frmNouveauPatient.
	/// </summary>
	public class frmNouveauPatient : System.Windows.Forms.Form
	{
		/// <summary>
		/// Variable n�cessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmNouveauPatient(System.Data.DataRow appel)
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			System.Data.DataRow row = OutilsExt.OutilsSql.RecupereStructurePatient();
            CtrlPersonne personne = new CtrlPersonne(this,row,appel);
			this.Controls.Add(personne);
			personne.Top = 0;
			personne.Left = 0;
		}

		/// <summary>
		/// Nettoyage des ressources utilis�es.
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

		#region Code g�n�r� par le Concepteur Windows Form
		/// <summary>
		/// M�thode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette m�thode avec l'�diteur de code.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNouveauPatient));
            this.SuspendLayout();
            // 
            // frmNouveauPatient
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(536, 438);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNouveauPatient";
            this.Text = "Nouveau Patient";
            this.ResumeLayout(false);

		}
		#endregion
	}
}
