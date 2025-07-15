using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ImportSosGeneve.Commun
{
	/// <summary>
	/// Description résumée de ctrlJointDoc.
	/// </summary>
	public class ctrlJointDoc : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btJoint;
		private System.Windows.Forms.Button btVoir;
		private System.Windows.Forms.TextBox txtIdPatient;
		private System.Windows.Forms.GroupBox gbPatient;
		private System.Windows.Forms.Label lbNaissance;
		private System.Windows.Forms.Label lbPrenom;
		private System.Windows.Forms.Label lbNom;
		private System.Windows.Forms.Label lbDtConsult;
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctrlJointDoc()
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent

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

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.txtIdPatient = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btJoint = new System.Windows.Forms.Button();
            this.btVoir = new System.Windows.Forms.Button();
            this.gbPatient = new System.Windows.Forms.GroupBox();
            this.lbNaissance = new System.Windows.Forms.Label();
            this.lbPrenom = new System.Windows.Forms.Label();
            this.lbNom = new System.Windows.Forms.Label();
            this.lbDtConsult = new System.Windows.Forms.Label();
            this.gbPatient.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIdPatient
            // 
            this.txtIdPatient.Location = new System.Drawing.Point(88, 24);
            this.txtIdPatient.Name = "txtIdPatient";
            this.txtIdPatient.Size = new System.Drawing.Size(136, 20);
            this.txtIdPatient.TabIndex = 0;
            this.txtIdPatient.Text = "txtIdPatient";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "IdPatient";
            // 
            // btJoint
            // 
            this.btJoint.Location = new System.Drawing.Point(32, 80);
            this.btJoint.Name = "btJoint";
            this.btJoint.Size = new System.Drawing.Size(88, 32);
            this.btJoint.TabIndex = 2;
            this.btJoint.Text = "Joint";
            this.btJoint.Click += new System.EventHandler(this.btJoint_Click);
            // 
            // btVoir
            // 
            this.btVoir.Location = new System.Drawing.Point(32, 144);
            this.btVoir.Name = "btVoir";
            this.btVoir.Size = new System.Drawing.Size(88, 32);
            this.btVoir.TabIndex = 3;
            this.btVoir.Text = "Voir";
            // 
            // gbPatient
            // 
            this.gbPatient.Controls.Add(this.lbNaissance);
            this.gbPatient.Controls.Add(this.lbPrenom);
            this.gbPatient.Controls.Add(this.lbNom);
            this.gbPatient.Controls.Add(this.lbDtConsult);
            this.gbPatient.Location = new System.Drawing.Point(152, 56);
            this.gbPatient.Name = "gbPatient";
            this.gbPatient.Size = new System.Drawing.Size(208, 136);
            this.gbPatient.TabIndex = 16;
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
            // ctrlJointDoc
            // 
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.Controls.Add(this.gbPatient);
            this.Controls.Add(this.btVoir);
            this.Controls.Add(this.btJoint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIdPatient);
            this.Name = "ctrlJointDoc";
            this.Size = new System.Drawing.Size(376, 256);
            this.gbPatient.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void FichePatient(string[] pat)
		{
			if(pat==null)
			{
				lbDtConsult.Text="";
				lbNom.Text="";
				lbPrenom.Text="";
				lbNaissance.Text="";
				

			}
			else
			{
				if(pat[1]!="")
					lbDtConsult.Text = DateTime.Parse(pat[1]).ToString("dd/MM/yyyy HH:mm");
				else
					lbDtConsult.Text = DateTime.Parse(pat[0]).ToString("dd/MM/yyyy HH:mm");
				
				lbNom.Text=pat[2];
				lbPrenom.Text=pat[3];
				
				if(pat[4]!="")
					lbNaissance.Text = "Dt Naiss : " + DateTime.Parse(pat[4]).ToString("dd/MM/yyyy");
				else
					lbNaissance.Text="";
				
			}
		}

		private void txtIdPatient_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			btJoint.Visible = false;
			btVoir.Visible = false;
			btJoint.Tag=null;
			btVoir.Tag=null;

			if(e.KeyCode==Keys.Enter)
			{
				string[][] retour = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT a.DAP,a.DSL,pe.Nom,pe.Prenom,pe.DateNaissance from tableconsultations c inner join tableactes a on a.Num = c.CodeAppel inner join tablepatient pa on c.IndicePatient = pa.IdPatient inner join tablepersonne pe on pe.IdPersonne = pa.IdPersonne WHERE pa.IdPatient = '" + txtIdPatient.Text + "'");
				if(retour!=null && retour.Length==1)
				{
					FichePatient(retour[0]);
					long NPatient = long.Parse(txtIdPatient.Text);
					btJoint.Tag=NPatient;
					btJoint.Visible = true;

					/*string[][] retour2 = OutilsExt.OutilsSql.ExecuteCommandeAvecTabString("SELECT pa.IdPatient,jd.UrlDocJoint from patientjointdoc jd left join tablepatient pa on pa.IdPatient = jd.IdPatient where IdPatient = '" + txtIdPatient.Text + "'");
					if(retour2!=null && retour2.Length>0)
					{
						long NPatient = long.Parse(retour2[0][0]);
						btJoint.Tag=NPatient;
						btJoint.Visible = true;

						if(retour2[0][1]!="")
						{
							btVoir.Tag = retour2[0][1].Replace("|","\\");
							btVoir.Visible=true;
						}
					}
					else
					{
						MessageBox.Show("Désolé, aucune Patient n'existe pour cette IdPatient");
						return;
					}*/
				}
				else
				{
					FichePatient(null);
					MessageBox.Show("Désolé, le patient n'existe pas");
					return;
				}
			}
		}

		private void btJoint_Click(object sender, System.EventArgs e)
		{
			long NPatient = (long)btJoint.Tag;
			OpenFileDialog openFileDialog1 = new OpenFileDialog();
			openFileDialog1.Filter = "Tous les fichiers (*.*) | *.*";
			openFileDialog1.ShowDialog();
			string filename = openFileDialog1.FileName;
			bool reussite = OutilsExt.OutilsSql.ExecuteCommandeSansRetour("UPDATE patientjointdoc set UrlDocJoint = '" + filename.Replace("\\","|") + "' WHERE IdPatient = " + NPatient);
			if(reussite)
			{
				btVoir.Tag = filename;
				btVoir.Visible=true;				
			}
			else 
				MessageBox.Show("Erreur");
		}
	}
}
