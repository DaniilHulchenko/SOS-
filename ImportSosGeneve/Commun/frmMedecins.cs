using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de frmMedecins.
	/// </summary>
	public class frmMedecins : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtPrenomMed;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtNomMed;
		private System.Windows.Forms.TextBox txtNIF;
		private System.Windows.Forms.TextBox txtMail;
		private System.Windows.Forms.ComboBox cbMed;
		private System.Windows.Forms.Button btnAnnuler;
        private System.Windows.Forms.Button btnValider;
		private System.Windows.Forms.TextBox txtEAN;
		private System.Windows.Forms.TextBox txtConcordat;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.TextBox txtCommentaireMed;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox chkMed;
        private GroupBox groupBox1;
        private Label label10;
        private TextBox tBoxRCC;

        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public DataTable DtMedecin = new DataTable();

        public frmMedecins()
		{
			
			InitializeComponent();
			InitializeData();
		}


        #region Code généré par le Concepteur Windows Form
        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMedecins));
            this.cbMed = new System.Windows.Forms.ComboBox();
            this.txtNomMed = new System.Windows.Forms.TextBox();
            this.txtPrenomMed = new System.Windows.Forms.TextBox();
            this.txtEAN = new System.Windows.Forms.TextBox();
            this.txtConcordat = new System.Windows.Forms.TextBox();
            this.txtNIF = new System.Windows.Forms.TextBox();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAnnuler = new System.Windows.Forms.Button();
            this.btnValider = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtCommentaireMed = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkMed = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tBoxRCC = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMed
            // 
            this.cbMed.Location = new System.Drawing.Point(106, 19);
            this.cbMed.Name = "cbMed";
            this.cbMed.Size = new System.Drawing.Size(198, 21);
            this.cbMed.TabIndex = 1;
            this.cbMed.SelectedIndexChanged += new System.EventHandler(this.cbMed_SelectedIndexChanged);
            // 
            // txtNomMed
            // 
            this.txtNomMed.Location = new System.Drawing.Point(106, 72);
            this.txtNomMed.Name = "txtNomMed";
            this.txtNomMed.Size = new System.Drawing.Size(198, 20);
            this.txtNomMed.TabIndex = 5;
            // 
            // txtPrenomMed
            // 
            this.txtPrenomMed.Location = new System.Drawing.Point(106, 98);
            this.txtPrenomMed.Name = "txtPrenomMed";
            this.txtPrenomMed.Size = new System.Drawing.Size(198, 20);
            this.txtPrenomMed.TabIndex = 7;
            // 
            // txtEAN
            // 
            this.txtEAN.Location = new System.Drawing.Point(106, 124);
            this.txtEAN.Name = "txtEAN";
            this.txtEAN.Size = new System.Drawing.Size(198, 20);
            this.txtEAN.TabIndex = 9;
            // 
            // txtConcordat
            // 
            this.txtConcordat.Location = new System.Drawing.Point(106, 148);
            this.txtConcordat.Name = "txtConcordat";
            this.txtConcordat.Size = new System.Drawing.Size(198, 20);
            this.txtConcordat.TabIndex = 11;
            // 
            // txtNIF
            // 
            this.txtNIF.Location = new System.Drawing.Point(106, 174);
            this.txtNIF.Name = "txtNIF";
            this.txtNIF.Size = new System.Drawing.Size(198, 20);
            this.txtNIF.TabIndex = 13;
            // 
            // txtMail
            // 
            this.txtMail.Enabled = false;
            this.txtMail.Location = new System.Drawing.Point(106, 200);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(198, 20);
            this.txtMail.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(6, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nom:";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(6, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 24);
            this.label2.TabIndex = 12;
            this.label2.Text = "N° NIF :";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(6, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "N° concordat :";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(6, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 24);
            this.label4.TabIndex = 8;
            this.label4.Text = "N° EAN :";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(6, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Prénom:";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(6, 203);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 24);
            this.label7.TabIndex = 14;
            this.label7.Text = "@ EMAIL :";
            // 
            // btnAnnuler
            // 
            this.btnAnnuler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnnuler.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Fermer;
            this.btnAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAnnuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnuler.Location = new System.Drawing.Point(248, 440);
            this.btnAnnuler.Name = "btnAnnuler";
            this.btnAnnuler.Size = new System.Drawing.Size(80, 34);
            this.btnAnnuler.TabIndex = 2;
            this.btnAnnuler.UseVisualStyleBackColor = false;
            this.btnAnnuler.Click += new System.EventHandler(this.btnAnnuler_Click);
            // 
            // btnValider
            // 
            this.btnValider.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValider.BackgroundImage = global::ImportSosGeneve.Properties.Resources.Ok;
            this.btnValider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnValider.Location = new System.Drawing.Point(162, 440);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(80, 34);
            this.btnValider.TabIndex = 1;
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 0;
            this.label6.Text = "Medecin :";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(6, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "Code Intervenant :";
            // 
            // txtCode
            // 
            this.txtCode.Enabled = false;
            this.txtCode.Location = new System.Drawing.Point(106, 46);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(198, 20);
            this.txtCode.TabIndex = 3;
            // 
            // txtCommentaireMed
            // 
            this.txtCommentaireMed.Location = new System.Drawing.Point(106, 285);
            this.txtCommentaireMed.Multiline = true;
            this.txtCommentaireMed.Name = "txtCommentaireMed";
            this.txtCommentaireMed.Size = new System.Drawing.Size(198, 56);
            this.txtCommentaireMed.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(6, 288);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 24);
            this.label9.TabIndex = 16;
            this.label9.Text = "Commentaire :";
            // 
            // chkMed
            // 
            this.chkMed.BackColor = System.Drawing.Color.Transparent;
            this.chkMed.Location = new System.Drawing.Point(106, 226);
            this.chkMed.Name = "chkMed";
            this.chkMed.Size = new System.Drawing.Size(88, 16);
            this.chkMed.TabIndex = 17;
            this.chkMed.Text = "Independant";
            this.chkMed.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tBoxRCC);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.chkMed);
            this.groupBox1.Controls.Add(this.cbMed);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCommentaireMed);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtMail);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtNIF);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtConcordat);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtEAN);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtPrenomMed);
            this.groupBox1.Controls.Add(this.txtNomMed);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 365);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // tBoxRCC
            // 
            this.tBoxRCC.Location = new System.Drawing.Point(106, 253);
            this.tBoxRCC.Name = "tBoxRCC";
            this.tBoxRCC.Size = new System.Drawing.Size(198, 20);
            this.tBoxRCC.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(8, 255);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 20);
            this.label10.TabIndex = 20;
            this.label10.Text = "N° RCC :";
            // 
            // frmMedecins
            // 
            this.AcceptButton = this.btnValider;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.CancelButton = this.btnAnnuler;
            this.ClientSize = new System.Drawing.Size(340, 486);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAnnuler);
            this.Controls.Add(this.btnValider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMedecins";
            this.Text = "Gestion des Medecins";
            this.Load += new System.EventHandler(this.frmMedecins_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion


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

		#region Dans les recherches

		private void InitializeData()
		{
            ChargelisteMed();
		}

		// On remplit la liste avec les médecins Sos
		private DataTable ChargelisteMed()
		{
            DtMedecin.Rows.Clear();

            //Connexion à la base SmartRapport
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection;

            try
            {
                //Table Motif
                string sqlstr0 = "SELECT CodeIntervenant,Nom,NomGeneve,PrenomGeneve,Concordat,EAN,NIF,Mail,Independant, RCC, Commentaire from tablemedecin order by Nom";
                cmd.CommandText = sqlstr0;

                cmd.Parameters.Clear();

                DtMedecin.Load(cmd.ExecuteReader());    //on execute
                cbMed.Items.Clear();      //On vide la liste

                foreach (DataRow row in DtMedecin.Rows)
                {
                    ListItem item = new ListItem(row, row["Nom"].ToString());
                    cbMed.Items.Add(item);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur lors de la recherche des médecins:" + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            return DtMedecin;
		}

		// On remplit la liste avec les médecins Sos
		private DataTable ChargelisteMed(string Code)
		{
            DtMedecin.Rows.Clear();

            //Connexion à la base SmartRapport
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
            SqlConnection dbConnection = new SqlConnection(connex);

            dbConnection.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbConnection;

            try
            {
                //Table Motif
                string sqlstr0 = "SELECT CodeIntervenant,Nom,NomGeneve,PrenomGeneve,Concordat,EAN,NIF,Mail,Independant, RCC, Commentaire " +
                                 " from tablemedecin WHERE CodeIntervenant = " + Code + " order by Nom";
                cmd.CommandText = sqlstr0;

                cmd.Parameters.Clear();

                DtMedecin.Load(cmd.ExecuteReader());    //on execute
                cbMed.Items.Clear();      //On vide la liste

                foreach (DataRow row in DtMedecin.Rows)
                {
                    ListItem item = new ListItem(row, row["Nom"].ToString());
                    cbMed.Items.Add(item);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur lors de la recherche des médecins:" + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //fermeture des connexions
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }

            return DtMedecin;
        }


		#endregion

	

		
		private void frmMedecins_Load(object sender, System.EventArgs e)
		{
			AfficheMedecinSos(null);
		}

		private void btnAnnuler_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void cbMed_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(cbMed.SelectedIndex > -1)
			{
				ListItem item = (ListItem)cbMed.SelectedItem;

                DataRow row = (DataRow)item.objValue;
                AfficheMedecinSos(row);               			
			}
		}

		//private void AfficheMedecinSos(string[] tableau)
        private void AfficheMedecinSos(DataRow row)
        {
			//if(tableau==null)
            if(row == null)
			{
				txtCode.Text="";
				txtNomMed.Text="";
				txtPrenomMed.Text="";
				txtMail.Text="";
				txtConcordat.Text="";
				txtEAN.Text="";
				txtNIF.Text="";
                tBoxRCC.Text = "";
				txtCommentaireMed.Text="";
				chkMed.Checked=false;
                tBoxRCC.Text = "";
			}
			else
			{               
                txtCode.Text = row["CodeIntervenant"].ToString();
				txtNomMed.Text = row["NomGeneve"].ToString();
				txtPrenomMed.Text = row["PrenomGeneve"].ToString();
                txtMail.Text = row["Mail"].ToString();
                txtConcordat.Text = row["Concordat"].ToString(); ;
				txtEAN.Text = row["EAN"].ToString();
				txtNIF.Text = row["NIF"].ToString();
                tBoxRCC.Text = row["RCC"].ToString();
				txtCommentaireMed.Text = row["Commentaire"].ToString();
                int indp = int.Parse(row["Independant"].ToString());
				if( indp==1) chkMed.Checked=true;
				if( indp==0) chkMed.Checked=false;
            }
        }

		private void btnValider_Click(object sender, System.EventArgs e)
		{
			//Enregistrement du médecin sos
			if(txtCode.Text=="") return;
			int indp =0;
			if (chkMed.Checked == true) indp = 1;
			if (chkMed.Checked == false) indp = 0;

			OutilsExt.OutilsSql.ExecuteCommandeSansRetour("UPDATE tablemedecin set NomGeneve ='" + txtNomMed.Text.Replace("'","''") +
                                                            "',PrenomGeneve ='" + txtPrenomMed.Text.Replace("'","''") + "',Concordat ='" + txtConcordat.Text.Replace("'","''") +
                                                            "',EAN ='" + txtEAN.Text.Replace("'","''") + "',NIF='" + txtNIF.Text.Replace("'","''") + "',Mail='" +
                                                            txtMail.Text.Replace("'","''") + "',Independant = '"+indp+"', RCC = '" + tBoxRCC.Text + "'," +
                                                            " Commentaire = '" + txtCommentaireMed.Text.Replace("'","''") + 
                                                            "'  WHERE CodeIntervenant = " + txtCode.Text);

            ChargelisteMed(txtCode.Text);
		}

	}
}
