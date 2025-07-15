//********************************************CLASSE POUR UN NOUVEAU COMPOSANT PERMETTANT DE ZOOMER SUR UNE IMAGE**************Domi 05.04.2013
//UTILISATION: déclarer dans une forme, puis, par exemple pour gestion avec la roulette
//Dans le constructeur de la From:
//this.zoomImageViewer1.MouseWheel += new MouseEventHandler(zoomImageViewer1_MouseWheel);  //nvl évenement de gestion de roulette
//Puis déclarer:
// private void zoomImageViewer1_MouseWheel(object sender, MouseEventArgs e)
// //sens de la roulette {if (e.Delta < 0)   {zoomImageViewer1.Zoom += (float).1;} //On augmente le zoom                  
//        else { if (zoomImageViewer1.Zoom > 0.2) zoomImageViewer1.Zoom -= (float).1;}}     //On diminue le Zoom jusqu'à 0.2 max (après il n'y a plus d'image)     
//                          
//private void zoomImageViewer1_MouseEnter(object sender, EventArgs e)      //Penser à donner le focus à la fenètre
//        { if (zoomImageViewer1.Focused == false)  zoomImageViewer1.Focus();}
//            
     
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ImportSosGeneve
{
    public class ZoomImageViewer : ScrollableControl  //Objet dérivé d'un ScrollableControl
{
	private Image _image;

	//Double buffer pour le control
    public ZoomImageViewer()
	{
		this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
		this.AutoScroll = true;
		this.Image = null;
		this.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
		this.Zoom = 1f;
	}
	
        //Nouvelles propriétés
	[Category("Appearance"), Description("Image à afficher")]
	public Image Image {
		get { return _image; }
		set {
			_image = value;
			UpdateScaleFactor();
			Invalidate();
		}
	}
	private float _zoom = 1f;
	[Category("Appearance"), Description("Le facteur de Zoom. inférieur à 1 pour réduire. supérieur à 1 pour augmenter.")]
	public float Zoom {
		get { return _zoom; }
		set {
			if (value < 0 || value < 1E-05) {
				value = 1E-05f;
			}
			_zoom = value;
			UpdateScaleFactor();
			Invalidate();
		}
	}
	private void UpdateScaleFactor()
	{
		if (_image == null) {
			this.AutoScrollMargin = this.Size;
		} else {
			this.AutoScrollMinSize = new Size(Convert.ToInt32(this._image.Width * _zoom + 0.5f), Convert.ToInt32(this._image.Height * _zoom + 0.5f));
		}
	}
	//Mise à jour de l'echelle
	private InterpolationMode _interpolationMode = InterpolationMode.High;
	[Category("Appearance"), Description("Mode d'interpolation utilisé pour lisser l'image")]
	public InterpolationMode InterpolationMode {
		get { return _interpolationMode; }
		set { _interpolationMode = value; }
	}
	protected override void OnPaintBackground(PaintEventArgs pevent)
	{
	}
	//On affiche le bond du controle
	protected override void OnPaint(PaintEventArgs e)
	{		
        //S'il n'y a pas d'image, ne rien faire
		if (_image == null) {
			base.OnPaintBackground(e);
			return;
		}
		//De temps en temps le 1er test foire, donc on gère l'erreur
		try 
        {
			int H = _image.Height;
		    //Renvoie un exception si pas d'image
		} 
        catch (Exception ex) 
        {
			base.OnPaintBackground(e);
            Console.WriteLine(ex.Message);
			return;
		}
		//Définie le Zoom de la matrice
		Matrix mx = new Matrix(_zoom, 0, 0, _zoom, 0, 0);
		mx.Translate(this.AutoScrollPosition.X / _zoom, this.AutoScrollPosition.Y / _zoom);
		e.Graphics.Transform = mx;
		e.Graphics.InterpolationMode = _interpolationMode;
		e.Graphics.DrawImage(_image, new Rectangle(0, 0, this._image.Width, this._image.Height), 0, 0, _image.Width, _image.Height, GraphicsUnit.Pixel);
		base.OnPaint(e);
	}
	
}
}


