using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.Web;
using System.Web.Services;

public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
{
	public TrustAllCertificatePolicy()
	{}

	public bool CheckValidationResult(ServicePoint sp,
		System.Security.Cryptography.X509Certificates.X509Certificate cert,WebRequest req, int problem)
	{
		return true;
    }
}

// **************************************************************
// classe permettant de créer un dégradé de couleur dans un spread
// **************************************************************
public class GradientCellType  : FarPoint.Win.Spread.CellType.TextCellType
{
	private Color topColor = Color.White;
	private Color bottomColor = Color.White;
	private StringAlignment _alignement = StringAlignment.Near;
		
	public Color TopColor
	{
		get { return topColor; }
		set { topColor = value; }
	}
		
	public Color BottomColor
	{
		get { return bottomColor; }
		set { bottomColor = value; }
	}

	public void setStringAlignement(StringAlignment alignement)
	{
		this._alignement = alignement;
	}

    public override void PaintCell(Graphics g, Rectangle r,
        FarPoint.Win.Spread.Appearance appearance,
        object value, bool isSelected,
        bool isLocked, float zoomFactor)
    {
        if (r.Width == 0 || r.Height == 0) return;

        Brush backColorBrush = new LinearGradientBrush(r, topColor, bottomColor,
            LinearGradientMode.Vertical);
        Brush foreColorBrush = new SolidBrush(appearance.ForeColor);
        g.FillRectangle(backColorBrush, r);

        StringFormat sf = new StringFormat();
        sf.Alignment = this._alignement;
        sf.LineAlignment = System.Drawing.StringAlignment.Center;


        if (value is string)
            g.DrawString((string)value, appearance.Font, foreColorBrush, new
                RectangleF(r.X, r.Y, r.Width, r.Height), sf);
        backColorBrush.Dispose();
        foreColorBrush.Dispose();
    }
}

// **************************************************************
// classe permettant de créer un dégradé de couleur dans un spread
// **************************************************************
public class BtnOnglet  : System.Windows.Forms.Button
{
	
	private Color topColor = Color.Yellow;
	private Color bottomColor = Color.Orange;
				
	public Color TopColor
	{
		get { return topColor; }
		set { topColor = value; }
	}
		
	public Color BottomColor
	{
		get { return bottomColor; }
		set { bottomColor = value; }
	}				
}
// **************************************************************
