using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

	// Structure de graphique avec informations de légende

	public class Graphique
	{
		public Bitmap Img=null;
		public RectangleF[] ZonesInfo=null;
		public string[] Infos=null;
		public Color[] Couleurs=null;

		public enum TypeGraphique{Baton,Barre,Point,Courbe,Secteur};
		public Graphique.TypeGraphique MonTypeGraphique;

		public Graphique(Bitmap map,RectangleF[] rectangles,string[] legendes,Graphique.TypeGraphique TypeG)
		{
			this.Img = map;
			this.ZonesInfo = rectangles;
			this.Infos = legendes;			
			this.MonTypeGraphique = TypeG;
		}
		public Graphique(Bitmap map,Color[] Couleurs,string[] legendes,Graphique.TypeGraphique TypeG)
		{
			this.Img = map;
			this.Couleurs = Couleurs;
			this.Infos = legendes;			
			this.MonTypeGraphique = TypeG;
		}
	}
	/// <summary>
	/// Description résumée de Stats.
	/// Classe qui permet de générer des graphiques images à partir d'enregistrements sous
	/// forme de dataset
	/// </summary>
	public class Stats
	{
		public static string TitreGraphique="";
		public static bool AffichageLegende = false;
		public static bool AffichageValeurs=true;
		public static bool AfficheDelais=true;

				
		public static Graphique CreationGraphiqueBaton(System.Drawing.Size size,DataRow[] rows)
		{
			try
			{
				if(rows==null) return null;

				System.Collections.ArrayList Legende = new System.Collections.ArrayList();
				System.Collections.ArrayList Rectangles = new System.Collections.ArrayList();


				Bitmap map = new Bitmap(size.Width,size.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				Graphics g = Graphics.FromImage(map);
				g.Clear(Color.White);
			
				// Tracé de la légende horizontale :
				float MaxX = size.Width - 10;
				float MinX = 30;
				float MinY = size.Height - 30;
				float MaxY = 60;
				float UniteX = (MaxX - MinX) ;
				if(rows.Length>0) 
					UniteX = (MaxX - MinX) / rows.Length;
				else
					UniteX = (MaxX - MinX);
				float vv = (float)Math.Round(UniteX,2);
				if(vv<UniteX)
					UniteX = vv + 0.01f;
				else
					UniteX = vv;

				float UniteY = (MinY - MaxY) / 10;
			
				int MaxAppels = 0;
				for(int i=0;i<rows.Length;i++)
				{
					MaxAppels = Math.Max(MaxAppels,int.Parse(rows[i][1].ToString()));
				}

				float UnitesParAppel = (MinY - MaxY)/MaxAppels;

				Color CouleurBasse = Color.SkyBlue;
				Color CouleurMoyennee = Color.Blue;
				Color CouleurHaute = Color.SlateGray;

				// Axe des abcisses
				int j = 0;
				for(float i=MinX;i<MaxX;i+=UniteX)
				{
					g.DrawLine(Pens.Black,new Point((int)i,(int)MinY),new Point((int)i,(int)MinY-5));
					float Ecart = (int)i + UniteX / 2 - 6;
					float PtActuel = i;
					float PtSuivant  = i+UniteX;
					float Diff = PtSuivant - PtActuel;
					if(AffichageLegende)
						g.DrawString(rows[j][2].ToString(),new Font("Arial",8,FontStyle.Regular),Brushes.Black,(int)Ecart,(int)MinY);
					else
                        g.DrawString(rows[j][0].ToString().Split(':')[0],new Font("Arial",8,FontStyle.Regular),Brushes.Black,(int)Ecart,(int)MinY);
					float HauteurRectangle = float.Parse(rows[j][1].ToString()) * UnitesParAppel;

                    //****domi 21.2.2012   cas des valeurs = 0 ... qui font planter car un pixel ne peut être < à 0
                    if (HauteurRectangle == 0)
                        HauteurRectangle = 1;

                    if ((int)Diff == 0)
                        Diff = 1;

					RectangleF r = new RectangleF((int)i, MinY - HauteurRectangle,(int)Diff,HauteurRectangle);

					TimeSpan t1 = TimeSpan.FromMinutes(double.Parse(rows[j][3].ToString()));
					TimeSpan t2 = TimeSpan.FromMinutes(double.Parse(rows[j][4].ToString()));
					if(AfficheDelais)
						Legende.Add(rows[j][2].ToString() + "\r\n" + "Délai trajet: "  + t1.Hours + "h" + t1.Minutes + "\r\n" + "Délai Consult: " + t2.Hours + "h" + t2.Minutes);
					else
						Legende.Add(rows[j][2].ToString());
					Rectangles.Add(r);
					LinearGradientBrush m_Gradient;
					if(float.Parse(rows[j][1].ToString())<=MaxAppels*30/100)
						m_Gradient= new LinearGradientBrush(r,Color.White,CouleurBasse,LinearGradientMode.Horizontal);
					else if(float.Parse(rows[j][1].ToString())<=MaxAppels*70/100)
						m_Gradient= new LinearGradientBrush(r,Color.White,CouleurMoyennee,LinearGradientMode.Horizontal);
					else					
						m_Gradient= new LinearGradientBrush(r,Color.White,CouleurHaute,LinearGradientMode.Horizontal);
					g.FillRectangle(m_Gradient,r);
					if (AffichageValeurs) g.DrawString(rows[j][1].ToString(),new Font("Arial",8,FontStyle.Bold),Brushes.Black,(int)Ecart - 4,MinY - HauteurRectangle - 20);
					j++;
				}

				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;

				g.DrawString(TitreGraphique,new Font("Arial",12,FontStyle.Bold),Brushes.Red,new RectangleF(0,0,size.Width,60),sf);
			      
				g.Dispose();
				return new Graphique(map,(RectangleF[])Rectangles.ToArray(typeof(RectangleF)),(String[])Legende.ToArray(typeof(string)),Graphique.TypeGraphique.Baton);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}			
		}

		public static Graphique CreationGraphiqueMultipleBaton(System.Drawing.Size size,DataTable Enregistrement,DateTime DateStats,long max)
		{
			try
			{
				size = new Size(1000,800);

				System.Collections.ArrayList Legende = new System.Collections.ArrayList();
				System.Collections.ArrayList Rectangles = new System.Collections.ArrayList();

				Bitmap map = new Bitmap(size.Width,size.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				Graphics g = Graphics.FromImage(map);
				g.Clear(Color.White);
			
				float DebutX = 70;
				float DebutY = 100;

				float FinX = size.Width-10;
				float FinY = size.Height-10;

				float UniteX = (FinX-DebutX)/24;
				float UniteY = (FinY-DebutY)/10;
			
				StringFormat sf = new StringFormat();
				string legende="";

				int nLigneDataRow = 0;

				// Les Valeurs : 
				for(int i=9;i>=0;i--)
				{
					for(int j=0;j<24;j++)
					{
						try
						{
							sf.Alignment = StringAlignment.Center;
							sf.LineAlignment = StringAlignment.Center;
														
							int nb1 = int.Parse(Enregistrement.Rows[nLigneDataRow][2].ToString());
							int nb2 = int.Parse(Enregistrement.Rows[nLigneDataRow][3].ToString());
							int nb3 = int.Parse(Enregistrement.Rows[nLigneDataRow][4].ToString());

							float nb1f = int.Parse(Enregistrement.Rows[nLigneDataRow][2].ToString())*UniteY/max-5;
							float nb2f = int.Parse(Enregistrement.Rows[nLigneDataRow][3].ToString())*UniteY/max-5;
							float nb3f = int.Parse(Enregistrement.Rows[nLigneDataRow][4].ToString())*UniteY/max-5;

							if(nb1f==0) nb1f=0.1f;
							if(nb2f==0) nb2f=0.1f;
							if(nb3f==0) nb3f=0.1f;

							// nombre d'appels reçus sur le pabx
							RectangleF r1 = new RectangleF(DebutX + j*UniteX, DebutY + UniteY*i - nb1f,UniteX/3,nb1f);
							LinearGradientBrush m_Gradient;
							m_Gradient= new LinearGradientBrush(r1,Color.White,Color.Blue,LinearGradientMode.Horizontal);
							g.FillRectangle(m_Gradient,r1);
							g.DrawString(nb1.ToString(),new Font("Arial",6,FontStyle.Regular),Brushes.Black,new RectangleF(r1.Left,r1.Top+r1.Height,r1.Width,10),sf);

							// nombre d'appels enregistrés en base de donnée
							RectangleF r2 = new RectangleF(DebutX + j*UniteX + UniteX/3, DebutY + UniteY*i-nb2f,UniteX/3,nb2f);
							m_Gradient= new LinearGradientBrush(r2,Color.White,Color.Green,LinearGradientMode.Horizontal);
							g.FillRectangle(m_Gradient,r2);
							g.DrawString(nb2.ToString(),new Font("Arial",6,FontStyle.Regular),Brushes.Black,new RectangleF(r2.Left,r2.Top+r2.Height,r2.Width,10),sf);

							// nombre d'appels traités par un médecin
							RectangleF r3 = new RectangleF(DebutX + j*UniteX + UniteX*2/3, DebutY + UniteY*i-nb3f,UniteX/3,nb3f);
							m_Gradient= new LinearGradientBrush(r3,Color.White,Color.Orange,LinearGradientMode.Horizontal);
							g.FillRectangle(m_Gradient,r3);
							g.DrawString(nb3.ToString(),new Font("Arial",6,FontStyle.Regular),Brushes.Black,new RectangleF(r3.Left,r3.Top+r3.Height,r3.Width,10),sf);

							RectangleF RectangleMultiple = new RectangleF(r1.Left,DebutY + UniteY*i - UniteY,UniteX,UniteY);
							string strLegendeMultiple = "Appels sur PABX : " + nb1.ToString() + "\r\n";
							strLegendeMultiple += "Appels Pris : " + nb2.ToString() + "\r\n";
							strLegendeMultiple += "Appels Traités : " + nb3.ToString();

							Rectangles.Add(RectangleMultiple);
							Legende.Add(strLegendeMultiple);
						}
						catch(Exception exc1)
						{
							Console.WriteLine(exc1.Message);
						}

						nLigneDataRow++;
					}
				}

				// Légende des heures
				for(int j=0;j<24;j++)
				{
					//g.DrawLine(Pens.Black,new PointF(DebutX + UniteX*j,50), new PointF(DebutX+UniteX*j,60));
					g.DrawLine(Pens.LightGray,new PointF(DebutX + UniteX*j,50), new PointF(DebutX+UniteX*j,FinY));
					
					if(j<10)
						legende="0" + j;
					else
						legende=j.ToString();
					legende+=":00";
					
					sf.Alignment = StringAlignment.Near;
					sf.LineAlignment = StringAlignment.Center;
					g.DrawString(legende,new Font("Arial",10,FontStyle.Regular),Brushes.Black,new PointF(DebutX + UniteX*j,30),sf);
				}
				// Legende des jours
				for(int i=9;i>=0;i--)
				{
					legende = DateStats.AddDays(9-i).ToLongDateString().Split(' ')[0][0].ToString().ToUpper() + " " + DateStats.AddDays(9-i).Day + "/" + DateStats.AddDays(9-i).Month;

					//g.DrawLine(Pens.Cyan,new PointF(65,DebutY + UniteY*i), new PointF(70,DebutY + UniteY*i));
					g.DrawLine(Pens.LightGray,new PointF(65,DebutY + UniteY*i), new PointF(FinX,DebutY + UniteY*i));
					sf.Alignment = StringAlignment.Near;
					sf.LineAlignment = StringAlignment.Center;
					g.DrawString(legende,new Font("Arial",9,FontStyle.Regular),Brushes.Black,new PointF(10,DebutY + UniteY*i),sf);
				}

				
				g.Dispose();
				map.Save("c:\\test.png",System.Drawing.Imaging.ImageFormat.Png);

				return new Graphique(map,(RectangleF[])Rectangles.ToArray(typeof(RectangleF)),(String[])Legende.ToArray(typeof(string)),Graphique.TypeGraphique.Baton);
			
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}			
		}


		public static Graphique CreationGraphiqueBarre(System.Drawing.Size size, DataRow[] rows)
		{
			try
			{
				if(rows==null) return null;

				System.Collections.ArrayList Legende = new System.Collections.ArrayList();
				System.Collections.ArrayList Rectangles = new System.Collections.ArrayList();

				Bitmap map = new Bitmap(size.Width,size.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				Graphics g = Graphics.FromImage(map);
				g.Clear(Color.White);
			
				// Tracé de la légende horizontale :
				float MaxX = size.Width - 10;
				float MinX = 30;
				float MinY = size.Height - 30;
				float MaxY = 60;
				float UniteX = (MaxX - MinX) ;
				if(rows.Length>0) 
					UniteX = (MaxX - MinX) / rows.Length;
				else
					UniteX = (MaxX - MinX);
				float vv = (float)Math.Round(UniteX,2);
				if(vv<UniteX)
					UniteX = vv + 0.01f;
				else
					UniteX = vv;

				float UniteY = (MinY - MaxY) / 10;
			
				int MaxAppels = 0;
				for(int i=0;i<rows.Length;i++)
				{
					MaxAppels = Math.Max(MaxAppels,int.Parse(rows[i][1].ToString()));
				}

				float UnitesParAppel = (MinY - MaxY)/MaxAppels;

				Pen CouleurBasse = Pens.SkyBlue;
				Pen CouleurMoyennee = Pens.Blue;
				Pen CouleurHaute = Pens.SlateGray;
				
				// Axe des abcisses
				int j = 0;
				for(float i=MinX;i<MaxX;i+=UniteX)
				{
					g.DrawLine(Pens.Black,new Point((int)i,(int)MinY),new Point((int)i,(int)MinY-5));
					float Ecart = (int)i + UniteX / 2 - 6;
					float PtActuel = i;
					float PtSuivant  = i+UniteX;
					float Diff = PtSuivant - PtActuel;
					if(AffichageLegende)
						g.DrawString(rows[j][2].ToString(),new Font("Arial",8,FontStyle.Regular),Brushes.Black,(int)Ecart,(int)MinY);
					else
						g.DrawString(rows[j][0].ToString().Split(':')[0],new Font("Arial",8,FontStyle.Regular),Brushes.Black,(int)Ecart,(int)MinY);
					float HauteurRectangle = float.Parse(rows[j][1].ToString()) * UnitesParAppel;

                    //****domi 21.2.2012   cas des valeurs = 0
                    if (HauteurRectangle == 0)
                        HauteurRectangle = 1;

                    if ((int)Diff == 0)
                        Diff = 1;

                    RectangleF r = new RectangleF((int)i, MinY - HauteurRectangle,(int)Diff,HauteurRectangle);

					TimeSpan t1 = TimeSpan.FromMinutes(double.Parse(rows[j][3].ToString()));
					TimeSpan t2 = TimeSpan.FromMinutes(double.Parse(rows[j][4].ToString()));
					if(AfficheDelais)
						Legende.Add(rows[j][2].ToString() + "\r\n" + "Délai trajet: "  + t1.Hours + "h" + t1.Minutes + "\r\n" + "Durée consult: " + t2.Hours + "h" + t2.Minutes);
					else
						Legende.Add(rows[j][2].ToString());
					Rectangles.Add(r);
					double milieu = (int)i + (int)Diff/2;
					r = new RectangleF((int)milieu-5, MinY - HauteurRectangle,(int)milieu+5,HauteurRectangle);					
					if(float.Parse(rows[j][1].ToString())<=MaxAppels*30/100)
						g.DrawLine(CouleurBasse,(int)milieu,MinY-HauteurRectangle,(int)milieu,MinY);
					else if(float.Parse(rows[j][1].ToString())<=MaxAppels*70/100)
						g.DrawLine(CouleurMoyennee,(int)milieu,MinY-HauteurRectangle,(int)milieu,MinY);
					else					
						g.DrawLine(CouleurHaute,(int)milieu,MinY-HauteurRectangle,(int)milieu,MinY);
					
					if (AffichageValeurs) g.DrawString(rows[j][1].ToString(),new Font("Arial",8,FontStyle.Bold),Brushes.Black,(int)Ecart - 4,MinY - HauteurRectangle - 20);
					j++;
				}

				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;

				g.DrawString(TitreGraphique,new Font("Arial",12,FontStyle.Bold),Brushes.Red,new RectangleF(0,0,size.Width,60),sf);
			
				g.Dispose();
				return new Graphique(map,(RectangleF[])Rectangles.ToArray(typeof(RectangleF)),(String[])Legende.ToArray(typeof(string)),Graphique.TypeGraphique.Barre);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}			
		}

		public static Graphique CreationGraphiquePoint(System.Drawing.Size size, DataRow[] rows)
		{
			try
			{
				if(rows==null) return null;

				System.Collections.ArrayList Legende = new System.Collections.ArrayList();
				System.Collections.ArrayList Rectangles = new System.Collections.ArrayList();

				Bitmap map = new Bitmap(size.Width,size.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				Graphics g = Graphics.FromImage(map);
				g.Clear(Color.White);
			
				// Tracé de la légende horizontale :
				float MaxX = size.Width - 10;
				float MinX = 30;
				float MinY = size.Height - 30;
				float MaxY = 60;
				float UniteX = (MaxX - MinX) ;
				if(rows.Length>0) 
					UniteX = (MaxX - MinX) / rows.Length;
				else
					UniteX = (MaxX - MinX);
				float vv = (float)Math.Round(UniteX,2);
				if(vv<UniteX)
					UniteX = vv + 0.01f;
				else
					UniteX = vv;

				float UniteY = (MinY - MaxY) / 10;
			
				int MaxAppels = 0;
				for(int i=0;i<rows.Length;i++)
				{
					MaxAppels = Math.Max(MaxAppels,int.Parse(rows[i][1].ToString()));
				}

				float UnitesParAppel = (MinY - MaxY)/MaxAppels;

				Pen CouleurBasse = Pens.SkyBlue;
				Pen CouleurMoyennee = Pens.Blue;
				Pen CouleurHaute = Pens.SlateGray;


				// Axe des abcisses
				int j = 0;
				for(float i=MinX;i<MaxX;i+=UniteX)
				{
					g.DrawLine(Pens.Black,new Point((int)i,(int)MinY),new Point((int)i,(int)MinY-5));
					float Ecart = (int)i + UniteX / 2 - 6;
					float PtActuel = i;
					float PtSuivant  = i+UniteX;
					float Diff = PtSuivant - PtActuel;
					if(AffichageLegende)
						g.DrawString(rows[j][2].ToString(),new Font("Arial",8,FontStyle.Regular),Brushes.Black,(int)Ecart,(int)MinY);
					else
						g.DrawString(rows[j][0].ToString().Split(':')[0],new Font("Arial",8,FontStyle.Regular),Brushes.Black,(int)Ecart,(int)MinY);
					float HauteurRectangle = float.Parse(rows[j][1].ToString()) * UnitesParAppel;

                    //****domi 21.2.2012   cas des valeurs = 0
                    if (HauteurRectangle == 0)
                        HauteurRectangle = 1;

                    if ((int)Diff == 0)
                        Diff = 1;
                    
                    RectangleF r = new RectangleF((int)i, MinY - HauteurRectangle,(int)Diff,HauteurRectangle);

					TimeSpan t1 = TimeSpan.FromMinutes(double.Parse(rows[j][3].ToString()));
					TimeSpan t2 = TimeSpan.FromMinutes(double.Parse(rows[j][4].ToString()));
					if(AfficheDelais)
						Legende.Add(rows[j][2].ToString() + "\r\n" + "Délai trajet: "  + t1.Hours + "h" + t1.Minutes + "\r\n" + "Durée Consult: " + t2.Hours + "h" + t2.Minutes);
					else
						Legende.Add(rows[j][2].ToString());
					Rectangles.Add(r);
					double milieu = (int)i + (int)Diff/2;
										
					if(float.Parse(rows[j][1].ToString())<=MaxAppels*30/100)
						g.DrawEllipse(CouleurBasse,(int)milieu,MinY-HauteurRectangle,5,5);
					else if(float.Parse(rows[j][1].ToString())<=MaxAppels*70/100)
						g.DrawEllipse(CouleurMoyennee,(int)milieu,MinY-HauteurRectangle,5,5);
					else					
						g.DrawEllipse(CouleurHaute,(int)milieu,MinY-HauteurRectangle,5,5);
						
					
					if (AffichageValeurs) g.DrawString(rows[j][1].ToString(),new Font("Arial",8,FontStyle.Bold),Brushes.Black,(int)Ecart - 4,MinY - HauteurRectangle - 20);
					j++;
				}

				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;

				g.DrawString(TitreGraphique,new Font("Arial",12,FontStyle.Bold),Brushes.Red,new RectangleF(0,0,size.Width,60),sf);
			
				g.Dispose();
				return new Graphique(map,(RectangleF[])Rectangles.ToArray(typeof(RectangleF)),(String[])Legende.ToArray(typeof(string)),Graphique.TypeGraphique.Point );
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}			
		}

		public static Graphique CreationGraphiqueCourbe(System.Drawing.Size size, DataRow[] rows)
		{
			try
			{
				if(rows==null) return null;

				System.Collections.ArrayList Legende = new System.Collections.ArrayList();
				System.Collections.ArrayList Rectangles = new System.Collections.ArrayList();

				Bitmap map = new Bitmap(size.Width,size.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				Graphics g = Graphics.FromImage(map);
				g.Clear(Color.White);
			
				PointF AncienPoint=new PointF(0,0);
				PointF NouveauPoint=new PointF(0,0);

				// Tracé de la légende horizontale :
				float MaxX = size.Width - 10;
				float MinX = 30;
				float MinY = size.Height - 30;
				float MaxY = 60;
				float UniteX = (MaxX - MinX) ;
				if(rows.Length>0) 
					UniteX = (MaxX - MinX) / rows.Length;
				else
					UniteX = (MaxX - MinX);
				float vv = (float)Math.Round(UniteX,2);
				if(vv<UniteX)
					UniteX = vv + 0.01f;
				else
					UniteX = vv;

				float UniteY = (MinY - MaxY) / 10;
			
				int MaxAppels = 0;
				for(int i=0;i<rows.Length;i++)
				{
					MaxAppels = Math.Max(MaxAppels,int.Parse(rows[i][1].ToString()));
				}

				float UnitesParAppel = (MinY - MaxY)/MaxAppels;

				LinearGradientBrush m_Gradient= new LinearGradientBrush(new RectangleF(0,0,size.Width,size.Height),Color.Azure,Color.SteelBlue,LinearGradientMode.Vertical);
				ArrayList Liste = new ArrayList();
				Liste.Add(new PointF(MinX,MinY));

				// Axe des abcisses
				int j = 0;
				for(float i=MinX;i<MaxX;i+=UniteX)
				{
					g.DrawLine(Pens.Black,new Point((int)i,(int)MinY),new Point((int)i,(int)MinY-5));
					float Ecart = (int)i + UniteX / 2 - 6;
					float PtActuel = i;
					float PtSuivant  = i+UniteX;
					float Diff = PtSuivant - PtActuel;
					if(AffichageLegende)
						g.DrawString(rows[j][2].ToString(),new Font("Arial",8,FontStyle.Regular),Brushes.Black,(int)Ecart,(int)MinY);
					else
						g.DrawString(rows[j][0].ToString().Split(':')[0],new Font("Arial",8,FontStyle.Regular),Brushes.Black,(int)Ecart,(int)MinY);
					float HauteurRectangle = float.Parse(rows[j][1].ToString()) * UnitesParAppel;

                    //****domi 21.2.2012   cas des valeurs = 0
                    if (HauteurRectangle == 0)
                        HauteurRectangle = 1;

                    if ((int)Diff == 0)
                        Diff = 1;
                    
                    RectangleF r = new RectangleF((int)i, MinY - HauteurRectangle,(int)Diff,HauteurRectangle);

					TimeSpan t1 = TimeSpan.FromMinutes(double.Parse(rows[j][3].ToString()));
					TimeSpan t2 = TimeSpan.FromMinutes(double.Parse(rows[j][4].ToString()));
					if(AfficheDelais)
						Legende.Add(rows[j][2].ToString() + "\r\n" + "Délai trajet: "  + t1.Hours + "h" + t1.Minutes + "\r\n" + "Durée consult: " + t2.Hours + "h" + t2.Minutes);
					else
						Legende.Add(rows[j][2].ToString());
					Rectangles.Add(r);
					double milieu = (int)i + (int)Diff/2;
					
					
					NouveauPoint = new PointF((int)milieu,MinY-HauteurRectangle);
					if(i>MinX)
					{
						g.DrawLine(Pens.Black,AncienPoint,NouveauPoint);
						//g.DrawPolygon(Pens.Black,);
						//g.FillPolygon(m_Gradient,new PointF[] {new PointF(AncienPoint.X,MinY),AncienPoint,NouveauPoint,new PointF(NouveauPoint.X,MinY)});
					}
					Liste.Add(NouveauPoint);
					AncienPoint = NouveauPoint;

					if (AffichageValeurs) g.DrawString(rows[j][1].ToString(),new Font("Arial",8,FontStyle.Bold),Brushes.Black,(int)Ecart - 4,MinY - HauteurRectangle - 20);
					j++;
				}
				Liste.Add(new PointF(NouveauPoint.X,MinY));

				g.FillPolygon(m_Gradient,(PointF[])Liste.ToArray(typeof(PointF)));

				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;

				g.DrawString(TitreGraphique,new Font("Arial",12,FontStyle.Bold),Brushes.Red,new RectangleF(0,0,size.Width,60),sf);
			
				g.Dispose();
				return new Graphique(map,(RectangleF[])Rectangles.ToArray(typeof(RectangleF)),(String[])Legende.ToArray(typeof(string)),Graphique.TypeGraphique.Courbe);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}			
		}

		public static Graphique CreationGraphiqueSecteur(System.Drawing.Size size, DataRow[] rows)
		{
			try
			{
				if(rows==null) return null;

				Bitmap map = new Bitmap(size.Width,size.Height,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				Graphics g = Graphics.FromImage(map);
				g.Clear(Color.White);

				ArrayList Legende = new ArrayList();
				ArrayList Rectangles = new ArrayList();

				int Rayon = (Math.Min(size.Height,size.Width)-20)/2;
				int Diametre = Rayon*2;
				
				PointF Centre = new PointF(size.Width/2,size.Height/2);

				// Que représente 360° par rapport au nombre d'appels ?
				int NbAppels = 0;
				for(int i=0;i<rows.Length;i++)
				{
					NbAppels +=int.Parse(rows[i][1].ToString());
				}
				if(NbAppels==0) return null;
				
                float DegParAppel = (360/NbAppels);

				// le premier point du cercle : 
				// dessin du graphique
				float angle = 0;
				float sweep = 0;
 
				int Occ = 0;
				float X = 0;
				float Y = 0;

				ArrayList Couleurs = new ArrayList();
			
				for(int i=0; i < rows.Length; i++)
				{
					Random rand = new Random((int)DateTime.Now.Ticks);
					int Arg1 = rand.Next(0,255);
					int Arg2 = rand.Next(0,255);
					int Arg3 = rand.Next(0,255);


					Color color= Color.FromArgb(Arg1,Arg2,Arg3);		
					
					while(ExisteCouleur(Couleurs,color)==true)
					{
						Arg1 = rand.Next(0,255);
						Arg2 = rand.Next(0,255);
						Arg3 = rand.Next(0,255);
						color= Color.FromArgb(Arg1,Arg2,Arg3);		
					}

					Couleurs.Add(color);

												
					// dessin du secteur
					sweep = 
						(float)(360f * int.Parse(rows[i][1].ToString()) /NbAppels);
					RectangleF rect = new RectangleF(
						20,20,
						Diametre, 
						Diametre);
					g.FillPie(
						new SolidBrush(color), 
						rect.X,rect.Y,rect.Width,rect.Height, 
						angle, 
						sweep);
					g.DrawPie(
						new Pen(Color.Black), 
						rect.X,rect.Y,rect.Width,rect.Height, 
						angle, 
						sweep);					
					
					angle += sweep;

					// dessin de la légende
					
					int squareSize = 10;
					if(i==0) X = rect.X + Diametre + 20;			

					if(Y>=Diametre)
					{
						X+=Rayon/2;
						Occ = 0;						
					}
					

					Y = 30 +(squareSize +7)*Occ;

					RectangleF legendRect = new RectangleF(
						X, 
						Y, 
						squareSize, 
						squareSize);
					g.FillRectangle(
						new SolidBrush(color),
						legendRect); 

					// texte de la légende
					g.DrawString(
						rows[i][2].ToString(), 
						new Font("Vernada", 8), 
						Brushes.Black, 
						new PointF(legendRect.X + 15,
						legendRect.Y));				
				
					TimeSpan t1 = TimeSpan.FromMinutes(double.Parse(rows[i][3].ToString()));
					TimeSpan t2 = TimeSpan.FromMinutes(double.Parse(rows[i][4].ToString()));
					
					if(AfficheDelais)
                        Legende.Add(rows[i][2].ToString() + "\r\n" + "Délai trajet: "  + t1.Hours + "h" + t1.Minutes + "\r\n" + "Durée consult: " + t2.Hours + "h" + t2.Minutes);
					else
						Legende.Add(rows[i][2].ToString());
					

					Occ++;
					
				}	

				StringFormat sf = new StringFormat();
				sf.Alignment = StringAlignment.Center;

				g.DrawString(TitreGraphique,new Font("Arial",12,FontStyle.Bold),Brushes.Red,new RectangleF(0,0,size.Width,60),sf);
				g.Dispose();

				return new Graphique(map,(Color[])Couleurs.ToArray(typeof(Color)),(string[])Legende.ToArray(typeof(string)),Graphique.TypeGraphique.Secteur);
				//return new Graphique(map,new RectangleF[0],new string[0]);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}			
		}

		private static bool ExisteCouleur(ArrayList CouleursExistantes,Color couleur)
		{
			if(CouleursExistantes==null) return false;
			if(couleur.R==0 && couleur.G==0 && couleur.B==1) return true;

			for(int i=0;i<CouleursExistantes.Count;i++)
			{
				if(((Color)CouleursExistantes[i]).ToArgb()==couleur.ToArgb())
					return true;
			}
			
			return false;
		}
	}