using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ImportSosGeneve
{
	/// <summary>
	/// Description résumée de CtrlServeur.
	/// </summary>
	public class CtrlServeur : System.Windows.Forms.UserControl
	{
		private frmGeneral m_frmgeneral=null;


		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ListView lstRapport;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.ComponentModel.IContainer components;

		public CtrlServeur(frmGeneral frm)
		{
			// Cet appel est requis par le Concepteur de formulaires Windows.Forms.
			InitializeComponent();

			this.m_frmgeneral = frm;

			// TODO : ajoutez les initialisations après l'appel à InitializeComponent
			this.timer1.Enabled = true;

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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.lstRapport = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dernier import des fiches";
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(8, 40);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(296, 264);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 312);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Envoi des rapports visés";
            // 
            // lstRapport
            // 
            this.lstRapport.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lstRapport.FullRowSelect = true;
            this.lstRapport.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstRapport.HideSelection = false;
            this.lstRapport.Location = new System.Drawing.Point(8, 336);
            this.lstRapport.Name = "lstRapport";
            this.lstRapport.Size = new System.Drawing.Size(296, 296);
            this.lstRapport.TabIndex = 3;
            this.lstRapport.UseCompatibleStateImageBehavior = false;
            this.lstRapport.View = System.Windows.Forms.View.Details;
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CtrlServeur
            // 
            this.BackColor = System.Drawing.Color.AntiqueWhite;
            this.Controls.Add(this.lstRapport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label1);
            this.Name = "CtrlServeur";
            this.Size = new System.Drawing.Size(312, 640);
            this.ResumeLayout(false);

		}
		#endregion

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			ChargementRapportToSend();
		}

		private void ChargementRapportToSend()
		{
			lstRapport.Items.Clear();
			string[][] ListeRapports = OutilsExt.OutilsSql.ListeRapportPourEnvoi();

			foreach(string[] s in ListeRapports)
			{
				if(s.Length>=4)
				{
					ListViewItem item = new ListViewItem(s[0]);
					item.SubItems.Add(s[1]);
					item.SubItems.Add(s[2]);
					item.SubItems.Add(s[3]);
					item.SubItems.Add(s[4]);
					if(s.Length==7)
					{
						item.SubItems.Add(s[5]);
						item.SubItems.Add(s[6]);
						
					}
					lstRapport.Items.Add(item);
				}
			}
		}
	}
}
