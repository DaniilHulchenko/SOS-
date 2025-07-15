using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace ImportSosGeneve
{
	public class CtrlNavigation : System.Windows.Forms.UserControl
	{
		public EventHandler BtnPrev_Click;
		public EventHandler BtnNext_Click;
		public EventHandler BtnLast_Click;

		private System.Windows.Forms.PictureBox btnLast;
		private System.Windows.Forms.PictureBox btnPrev;
		private System.Windows.Forms.PictureBox btnNext;
		private System.ComponentModel.Container components = null;

		public CtrlNavigation()
		{
			InitializeComponent();	
		
			btnPrev.Click+=new EventHandler(btnPrev_Click);
			btnNext.Click+=new EventHandler(btnNext_Click);
			btnLast.Click+=new EventHandler(btnLast_Click);
			
		}

		private void btnPrev_Click(object sender,System.EventArgs e)
		{
			BtnPrev_Click(null,null);
		}
		private void btnNext_Click(object sender,System.EventArgs e)
		{
			BtnNext_Click(null,null);
		}
		private void btnLast_Click(object sender,System.EventArgs e)
		{
			BtnLast_Click(null,null);
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

		#region Code généré par le Concepteur de composants
		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CtrlNavigation));
			this.btnLast = new System.Windows.Forms.PictureBox();
			this.btnPrev = new System.Windows.Forms.PictureBox();
			this.btnNext = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// btnLast
			// 
			this.btnLast.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.btnLast.Image = ((System.Drawing.Image)(resources.GetObject("btnLast.Image")));
			this.btnLast.Location = new System.Drawing.Point(64, 0);
			this.btnLast.Name = "btnLast";
			this.btnLast.Size = new System.Drawing.Size(24, 24);
			this.btnLast.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.btnLast.TabIndex = 0;
			this.btnLast.TabStop = false;
			// 
			// btnPrev
			// 
			this.btnPrev.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.btnPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.Image")));
			this.btnPrev.Location = new System.Drawing.Point(0, 0);
			this.btnPrev.Name = "btnPrev";
			this.btnPrev.Size = new System.Drawing.Size(32, 24);
			this.btnPrev.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.btnPrev.TabIndex = 1;
			this.btnPrev.TabStop = false;
			// 
			// btnNext
			// 
			this.btnNext.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
			this.btnNext.Location = new System.Drawing.Point(32, 0);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(32, 24);
			this.btnNext.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.btnNext.TabIndex = 2;
			this.btnNext.TabStop = false;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click_1);
			// 
			// CtrlNavigation
			// 
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.btnPrev);
			this.Controls.Add(this.btnLast);
			this.Name = "CtrlNavigation";
			this.Size = new System.Drawing.Size(88, 24);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnNext_Click_1(object sender, System.EventArgs e)
		{
		
		}
	}
}
