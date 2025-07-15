using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImportSosGeneve.Rapport
{
    public partial class FCarteAVS : Form
    {
        string Chemin_Photo;
        
        public FCarteAVS(string CarteAvs)          
        {
           InitializeComponent();

           this.zoomImageViewer1.MouseWheel += new MouseEventHandler(zoomImageViewer1_MouseWheel);       //Domi 03.04.2013...Déclaration d'un évènement _MouseWheel

           Chemin_Photo = CarteAvs;
        }

        //*******************Domi 29.07.2014 ************Zoom sur l'image de la carte AVS
        //gestion de la roulette de la souris
        private void zoomImageViewer1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)  //sens de la roulette
            {
                zoomImageViewer1.Zoom += (float).1; //On augmente le zoom
            }
            else
            {
                if (zoomImageViewer1.Zoom > 0.2)     //On diminue le Zoom jusqu'à 0.2 max (après il n'y a plus d'image)     
                    zoomImageViewer1.Zoom -= (float).1;
            }
        }

        //Penser à donner le focus à la fenètre
        private void zoomImageViewer1_MouseEnter(object sender, EventArgs e)
        {
            if (zoomImageViewer1.Focused == false)
            {
                zoomImageViewer1.Focus();
            }
        }

        //Tourner l'image si elle est à l'envert
        private void bRotationImage_Click(object sender, EventArgs e)
        {
            Image img = zoomImageViewer1.Image;
            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
            zoomImageViewer1.Image = img;
        }      

        private void FCarteAVS_Load(object sender, EventArgs e)
        {
            try
            {
                zoomImageViewer1.Zoom = .3F;     //On défini la taille de l'image par défaut
                zoomImageViewer1.Image = Image.FromFile(Chemin_Photo);   //on affiche l'image de la carte AVS                                         
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur du chargement de l'image: " + ex);
            }          
        }

        private void bFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
