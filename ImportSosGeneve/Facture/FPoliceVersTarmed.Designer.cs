namespace ImportSosGeneve.Facture
{
    partial class FPoliceVersTarmed
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FPoliceVersTarmed));
            this.bCherche = new System.Windows.Forms.Button();
            this.bFermer = new System.Windows.Forms.Button();
            this.tBAdresseDest = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rBCantonal = new System.Windows.Forms.RadioButton();
            this.rBFederal = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // bCherche
            // 
            this.bCherche.BackgroundImage = global::ImportSosGeneve.Properties.Resources.bLoupeBleu1;
            this.bCherche.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bCherche.FlatAppearance.BorderSize = 0;
            this.bCherche.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCherche.Location = new System.Drawing.Point(289, 25);
            this.bCherche.Name = "bCherche";
            this.bCherche.Size = new System.Drawing.Size(48, 48);
            this.bCherche.TabIndex = 35;
            this.bCherche.UseVisualStyleBackColor = true;
            this.bCherche.Click += new System.EventHandler(this.bCherche_Click);
            // 
            // bFermer
            // 
            this.bFermer.BackgroundImage = global::ImportSosGeneve.Properties.Resources.brondValider;
            this.bFermer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bFermer.FlatAppearance.BorderSize = 0;
            this.bFermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFermer.Location = new System.Drawing.Point(289, 175);
            this.bFermer.Name = "bFermer";
            this.bFermer.Size = new System.Drawing.Size(57, 57);
            this.bFermer.TabIndex = 34;
            this.bFermer.UseVisualStyleBackColor = true;
            this.bFermer.Click += new System.EventHandler(this.bFermer_Click);
            // 
            // tBAdresseDest
            // 
            this.tBAdresseDest.Location = new System.Drawing.Point(12, 25);
            this.tBAdresseDest.Multiline = true;
            this.tBAdresseDest.Name = "tBAdresseDest";
            this.tBAdresseDest.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tBAdresseDest.Size = new System.Drawing.Size(251, 113);
            this.tBAdresseDest.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 16);
            this.label1.TabIndex = 37;
            this.label1.Text = "Adresse du destinataire de la facture";
            // 
            // rBCantonal
            // 
            this.rBCantonal.AutoSize = true;
            this.rBCantonal.Checked = true;
            this.rBCantonal.Location = new System.Drawing.Point(40, 175);
            this.rBCantonal.Name = "rBCantonal";
            this.rBCantonal.Size = new System.Drawing.Size(109, 20);
            this.rBCantonal.TabIndex = 38;
            this.rBCantonal.TabStop = true;
            this.rBCantonal.Text = "Tarif Cantonal";
            this.rBCantonal.UseVisualStyleBackColor = true;
            // 
            // rBFederal
            // 
            this.rBFederal.AutoSize = true;
            this.rBFederal.Location = new System.Drawing.Point(40, 201);
            this.rBFederal.Name = "rBFederal";
            this.rBFederal.Size = new System.Drawing.Size(103, 20);
            this.rBFederal.TabIndex = 39;
            this.rBFederal.Text = "Tarif Fédéral";
            this.rBFederal.UseVisualStyleBackColor = true;
            // 
            // FPoliceVersTarmed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(366, 254);
            this.ControlBox = false;
            this.Controls.Add(this.rBFederal);
            this.Controls.Add(this.rBCantonal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tBAdresseDest);
            this.Controls.Add(this.bCherche);
            this.Controls.Add(this.bFermer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FPoliceVersTarmed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facture police vers Tarmed";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bFermer;
        private System.Windows.Forms.Button bCherche;
        private System.Windows.Forms.TextBox tBAdresseDest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rBCantonal;
        private System.Windows.Forms.RadioButton rBFederal;
    }
}