using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ImportSosGeneve
{
	public delegate void dlgCtrlClosing();

	public class frmFacturation : Form
	{
		#region D�claration des variables

		private Container components = null;
		private CtrlFacturation m_Ctrl=null;
		private frmGeneral _frm=null;

		#endregion

		#region Contruction / Destruction du formulaire
        
		public frmFacturation(frmGeneral frm)
		{
			InitializeComponent();		
			this._frm = frm;
			m_Ctrl = new CtrlFacturation(_frm);
			this.Controls.Add(m_Ctrl);
			m_Ctrl.Dock = DockStyle.Fill;			
		}

		public frmFacturation(frmGeneral frm, System.Data.DataRow rowConsultation)
		{
			InitializeComponent();		
			this._frm = frm;
			m_Ctrl = new CtrlFacturation(_frm, rowConsultation);
			this.Controls.Add(m_Ctrl);
			m_Ctrl.Dock = DockStyle.Fill;						
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

		#region Code g�n�r� par le Concepteur Windows Form
		/// <summary>
		/// M�thode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette m�thode avec l'�diteur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // frmFacturation
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(1384, 894);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "frmFacturation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmFacturation_KeyUp);
            this.ResumeLayout(false);

		}
		#endregion		

		#region M�thodes publiques

		public void CloseFactu()
		{
			this.Controls.Remove(m_Ctrl);
			m_Ctrl.Dispose();
			this.Close();
			_frm.CloseFacture();
		}

		public void EnvoiCommande(Keys key)
		{
			m_Ctrl.EnvoiCommande(key);
		}

		private void frmFacturation_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			m_Ctrl.EnvoiCommande(e.KeyCode);			
		}

		#endregion
	}
}
