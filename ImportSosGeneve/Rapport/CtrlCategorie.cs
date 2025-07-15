using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ImportSosGeneve
{
	public class CtrlCategorie : System.Windows.Forms.UserControl
	{

		private System.Windows.Forms.Label LblCategorie;
		private System.Windows.Forms.TextBox textBox1;		
		private System.ComponentModel.Container components = null;

		public CtrlCategorie()
		{
			InitializeComponent();
		}
		public CtrlCategorie(string LibelleCategorie,string Valeur)
		{
			InitializeComponent();
			this.LblCategorie.Text = LibelleCategorie + " : ";
            this.textBox1.Text = Valeur;	
		}

		public string StrLibelle
		{
			get
			{
				return LblCategorie.Text;
			}
		}
		
		public string StrValeur
		{
			get
			{
				return textBox1.Text;
			}			
		}
		
		public void DonneFocusToText()
		{
			textBox1.Focus();
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
			this.LblCategorie = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// LblCategorie
			// 
			this.LblCategorie.BackColor = System.Drawing.Color.Transparent;
			this.LblCategorie.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.LblCategorie.Location = new System.Drawing.Point(0, 0);
			this.LblCategorie.Name = "LblCategorie";
			this.LblCategorie.Size = new System.Drawing.Size(1021, 19);
			this.LblCategorie.TabIndex = 0;
			this.LblCategorie.Text = "label1";
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.BlanchedAlmond;
			this.textBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(-1, 19);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(1018, 96);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "textBox1";
			// 
			// CtrlCategorie
			// 
			this.BackColor = System.Drawing.Color.Bisque;
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.LblCategorie);
			this.Name = "CtrlCategorie";
			this.Size = new System.Drawing.Size(1017, 116);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
