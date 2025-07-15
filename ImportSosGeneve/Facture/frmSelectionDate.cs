using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de frmSelectionDate.
	/// </summary>
	public class frmSelectionDate : System.Windows.Forms.Form
	{
		private System.Windows.Forms.LinkLabel lnkValider;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.DateTimePicker dt1;
		public System.Windows.Forms.DateTimePicker dt2;
		private System.Windows.Forms.LinkLabel lnkValider10;
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSelectionDate()
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();

			//
			// TODO : ajoutez le code du constructeur après l'appel à InitializeComponent
			//
		}
		public frmSelectionDate(DateTime debut, DateTime fin)
		{
			//
			// Requis pour la prise en charge du Concepteur Windows Forms
			//
			InitializeComponent();
			dt1.Value = debut;
			dt2.Value = fin;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectionDate));
            this.lnkValider = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dt1 = new System.Windows.Forms.DateTimePicker();
            this.dt2 = new System.Windows.Forms.DateTimePicker();
            this.lnkValider10 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lnkValider
            // 
            this.lnkValider.BackColor = System.Drawing.Color.Transparent;
            this.lnkValider.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkValider.Location = new System.Drawing.Point(123, 73);
            this.lnkValider.Name = "lnkValider";
            this.lnkValider.Size = new System.Drawing.Size(96, 24);
            this.lnkValider.TabIndex = 5;
            this.lnkValider.TabStop = true;
            this.lnkValider.Text = "Valider";
            this.lnkValider.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnkValider.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkValider_LinkClicked);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Début :";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fin :";
            // 
            // dt1
            // 
            this.dt1.CustomFormat = "";
            this.dt1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt1.Location = new System.Drawing.Point(75, 13);
            this.dt1.Name = "dt1";
            this.dt1.Size = new System.Drawing.Size(86, 20);
            this.dt1.TabIndex = 1;
            // 
            // dt2
            // 
            this.dt2.CustomFormat = "";
            this.dt2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt2.Location = new System.Drawing.Point(75, 43);
            this.dt2.Name = "dt2";
            this.dt2.Size = new System.Drawing.Size(86, 20);
            this.dt2.TabIndex = 3;
            this.dt2.Value = new System.DateTime(2022, 1, 1, 16, 4, 0, 0);
            // 
            // lnkValider10
            // 
            this.lnkValider10.BackColor = System.Drawing.Color.Transparent;
            this.lnkValider10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkValider10.Location = new System.Drawing.Point(7, 76);
            this.lnkValider10.Name = "lnkValider10";
            this.lnkValider10.Size = new System.Drawing.Size(105, 24);
            this.lnkValider10.TabIndex = 4;
            this.lnkValider10.TabStop = true;
            this.lnkValider10.Text = "Destinataire 10%";
            this.lnkValider10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnkValider10.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkValider10_LinkClicked);
            // 
            // frmSelectionDate
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(229, 115);
            this.ControlBox = false;
            this.Controls.Add(this.lnkValider10);
            this.Controls.Add(this.dt2);
            this.Controls.Add(this.dt1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkValider);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSelectionDate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sélection des dates";
            this.ResumeLayout(false);

		}
		#endregion

		private void lnkValider_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.Close();
		}

		private void lnkValider10_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.Close();
		}


	}
}
