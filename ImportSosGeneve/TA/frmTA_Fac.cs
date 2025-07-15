using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de frmTA_Fac.
	/// </summary>
	public class frmTA_Fac : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmTA_Fac()
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
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(56, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 48);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create Factures";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmTA_Fac
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmTA_Fac";
            this.Text = "frmTA_Fac";
            this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			CreateFacutres();

		}
		public void CreateFacutres()
		{
			//find the active abbonements
			string sql = "SELECT ta1.IdAbonnement,ta1.DateCreationAbonnement,ta1.DateDebutFacturation,ta1.Periodicite,ta1.Ordre,ta1.Archive,ta2.*,tcle.NumeroCle,pa.TypeAbonnement,pa.TexteAbonnement,pa.IdPatient,pe.IdPersonne,pe.Tel,pe.Nom,pe.PRenom,pe.Sexe from ta_abonnement ta1 inner join ta_abonnementlieufacture ta2 on ta1.IdAbonnement = ta2.TF_IdAbonnement inner join ta_abonnementcle tcle on tcle.IdAbonnement = ta1.IdAbonnement  inner join tablepatient pa on pa.IdPAtient = ta1.IdPAtient inner join tablepersonne pe on pe.IdPErsonne = pa.IdPersonne   where Archive = 0";
			DataSet dt1= OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet(sql);
			//if the dataset is not empty
			if(dt1!=null)
			{
				//for each patient in the dataset
				for(int c=0;c<dt1.Tables[0].Rows.Count;c++)
				{
					
					string Cle = dt1.Tables[0].Rows[c]["NumeroCle"].ToString();
					//find the bills in table ta_factures
					string sql1 = "SELECT fa.* from ta_factures fa where NumeroCle = '" + Cle + "' order by Fin_période ASC";
					DataSet ds1= OutilsExt.OutilsSql.ExecuteCommandeAvecDataSet(sql1);

					//check if there are bills for the current patient
					if(ds1!=null)
					{
						//look for the last bill
						int i = ds1.Tables[0].Rows.Count;
						DateTime last = DateTime.Parse(dt1.Tables[0].Rows[c]["Fin_période"].ToString());
						//if last < today, create new bill
						if(last < DateTime.Now)
						{
						}
					}
					else
					{
						//create first bill
					}



				}
			}

		}

		
	}
}
