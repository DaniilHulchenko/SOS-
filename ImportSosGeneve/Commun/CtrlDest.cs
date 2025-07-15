using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ImportSosGeneve
{
	
	public class CtrlDest : System.Windows.Forms.UserControl
	{
		#region Déclaration de variables

		public enum TypeOuverture{DefautFacture,ListeFacture};
		public CtrlDest.TypeOuverture m_TypeOuverture;		
		public Dest DestinataireRetour=null;
		private FIP _fip=null;
		public enum TypeDestinataire{Idem=0,Hotel=1,Assurance=2,Commissariat=3,Tiers=4,AutrePrive=5};
		private Dest m_Dest=null;
		private string m_strAdressePatient="";

		// Controles du formulaire

		private System.Windows.Forms.GroupBox groupA;
		private System.Windows.Forms.RadioButton rdIdem;
		private System.Windows.Forms.RadioButton rdHotel;
		private System.Windows.Forms.RadioButton rdAssurance;
		private System.Windows.Forms.RadioButton rdTiers;
		private System.Windows.Forms.PictureBox btnSuivant;		
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.PictureBox BtnFermer;
		private System.Windows.Forms.RadioButton rdPrive;

		#endregion
		
		#region Construction / Destruction de la classe

		public CtrlDest(FIP fip,Dest dest,string AdressePatient)
		{
			InitializeComponent();
			this._fip = fip;
			m_TypeOuverture = CtrlDest.TypeOuverture.DefautFacture;
			this.m_Dest = dest;
			this.m_strAdressePatient = AdressePatient;
		}

		public CtrlDest(FIP fip,CtrlDest.TypeOuverture mTypeOuverture)
		{
			InitializeComponent();
			this._fip = fip;
			this.m_TypeOuverture = mTypeOuverture;
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

		#endregion

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CtrlDest));
            this.groupA = new System.Windows.Forms.GroupBox();
            this.rdPrive = new System.Windows.Forms.RadioButton();
            this.BtnFermer = new System.Windows.Forms.PictureBox();
            this.btnSuivant = new System.Windows.Forms.PictureBox();
            this.rdTiers = new System.Windows.Forms.RadioButton();
            this.rdAssurance = new System.Windows.Forms.RadioButton();
            this.rdHotel = new System.Windows.Forms.RadioButton();
            this.rdIdem = new System.Windows.Forms.RadioButton();
            this.groupA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnFermer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSuivant)).BeginInit();
            this.SuspendLayout();
            // 
            // groupA
            // 
            this.groupA.Controls.Add(this.rdPrive);
            this.groupA.Controls.Add(this.BtnFermer);
            this.groupA.Controls.Add(this.btnSuivant);
            this.groupA.Controls.Add(this.rdTiers);
            this.groupA.Controls.Add(this.rdAssurance);
            this.groupA.Controls.Add(this.rdHotel);
            this.groupA.Controls.Add(this.rdIdem);
            this.groupA.Location = new System.Drawing.Point(8, 8);
            this.groupA.Name = "groupA";
            this.groupA.Size = new System.Drawing.Size(208, 152);
            this.groupA.TabIndex = 0;
            this.groupA.TabStop = false;
            this.groupA.Text = "Choix Destinataire";
            // 
            // rdPrive
            // 
            this.rdPrive.Location = new System.Drawing.Point(24, 120);
            this.rdPrive.Name = "rdPrive";
            this.rdPrive.Size = new System.Drawing.Size(88, 24);
            this.rdPrive.TabIndex = 4;
            this.rdPrive.Text = "Autre privé";
            this.rdPrive.Visible = false;
            // 
            // BtnFermer
            // 
            this.BtnFermer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BtnFermer.Image = ((System.Drawing.Image)(resources.GetObject("BtnFermer.Image")));
            this.BtnFermer.Location = new System.Drawing.Point(104, 32);
            this.BtnFermer.Name = "BtnFermer";
            this.BtnFermer.Size = new System.Drawing.Size(88, 24);
            this.BtnFermer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BtnFermer.TabIndex = 60;
            this.BtnFermer.TabStop = false;
            this.BtnFermer.Click += new System.EventHandler(this.BtnFermer_Click);
            // 
            // btnSuivant
            // 
            this.btnSuivant.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnSuivant.Image = ((System.Drawing.Image)(resources.GetObject("btnSuivant.Image")));
            this.btnSuivant.Location = new System.Drawing.Point(104, 96);
            this.btnSuivant.Name = "btnSuivant";
            this.btnSuivant.Size = new System.Drawing.Size(88, 24);
            this.btnSuivant.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSuivant.TabIndex = 58;
            this.btnSuivant.TabStop = false;
            this.btnSuivant.Click += new System.EventHandler(this.btnSuivant_Click);
            // 
            // rdTiers
            // 
            this.rdTiers.Location = new System.Drawing.Point(24, 96);
            this.rdTiers.Name = "rdTiers";
            this.rdTiers.Size = new System.Drawing.Size(88, 24);
            this.rdTiers.TabIndex = 3;
            this.rdTiers.Text = "Tiers";
            // 
            // rdAssurance
            // 
            this.rdAssurance.Location = new System.Drawing.Point(24, 72);
            this.rdAssurance.Name = "rdAssurance";
            this.rdAssurance.Size = new System.Drawing.Size(80, 24);
            this.rdAssurance.TabIndex = 2;
            this.rdAssurance.Text = "Assurance";
            // 
            // rdHotel
            // 
            this.rdHotel.Location = new System.Drawing.Point(24, 48);
            this.rdHotel.Name = "rdHotel";
            this.rdHotel.Size = new System.Drawing.Size(80, 24);
            this.rdHotel.TabIndex = 1;
            this.rdHotel.Text = "Hôtel";
            // 
            // rdIdem
            // 
            this.rdIdem.Checked = true;
            this.rdIdem.Location = new System.Drawing.Point(24, 24);
            this.rdIdem.Name = "rdIdem";
            this.rdIdem.Size = new System.Drawing.Size(80, 24);
            this.rdIdem.TabIndex = 0;
            this.rdIdem.TabStop = true;
            this.rdIdem.Text = "Idem";
            // 
            // CtrlDest
            // 
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Controls.Add(this.groupA);
            this.Name = "CtrlDest";
            this.Size = new System.Drawing.Size(224, 168);
            this.groupA.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BtnFermer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSuivant)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		#region Evenements du formulaire

		private void btnSuivant_Click(object sender, System.EventArgs e)
		{		
			if(rdIdem.Checked == true )
			{
				DestinataireRetour = new Dest();
				DestinataireRetour.m_TypeDestinataire=TypeDestinataire.Idem;				
			}
			if(rdHotel.Checked == true )
			{
				frmHotel frm  = new frmHotel(m_TypeOuverture);
				frm.ShowDialog();
				DestinataireRetour = frm.GetDestinataire();
				frm.Dispose();
				frm=null;
			}
			if(rdAssurance.Checked == true )
			{
				frmAssurance frm  = new frmAssurance(m_TypeOuverture);
				frm.ShowDialog();
				DestinataireRetour = frm.GetDestinataire();
				frm.Dispose();
				frm=null;
			}
			if(rdTiers.Checked == true )
			{
				frmTiers frm  = new frmTiers(m_TypeOuverture);
				frm.ShowDialog();
				DestinataireRetour = frm.GetDestinataire();
				frm.Dispose();
				frm=null;
			}
			if(rdPrive.Checked == true )
			{
				FIP frm  = new FIP(_fip.m_frmgeneral);
				frm.m_TypeOuvertureDestinataire = m_TypeOuverture;
				frm.ShowDialog();
				DestinataireRetour = frm.GetDestinataire();
				frm.Dispose();
				frm=null;
			}

			_fip.DisposeCtrlDest(DestinataireRetour,m_TypeOuverture);
		}

		private void BtnFermer_Click(object sender, System.EventArgs e)
		{
			_fip.DisposeCtrlDest(null,m_TypeOuverture);
		}	
	
		#endregion
	}
}
