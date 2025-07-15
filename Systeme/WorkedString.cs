using System;

	/// <summary>
	/// Description résumée de WorkedString.
	/// </summary>
	public class WorkedString
	{
		public static string Facture_NoCompte = "01-30627-2";

		public WorkedString()
		{
			//
			// TODO : ajoutez ici la logique du constructeur
			//
		}

		public static bool FormatNumerique(bool AvecEspace,char c)
		{
			if((AvecEspace && (int)c==32) || (int)c==8 || (int)c==13) return true;

			if(!char.IsDigit(c)) 
				return false; 
			else return true;
		}
		public static bool FormatFlottant(bool AvecEspace,char c)
		{
			if((AvecEspace && (int)c==32) || (int)c==8 || (int)c==13 || (int)c==44) return true;

			if(!char.IsDigit(c)) 
				return false; 
			else return true;
		}
		public static bool FormatDate(char c)
		{
			if((int)c==8 || (int)c==13  || (int)c==47 || c=='.') return true;

			if(!char.IsDigit(c)) 
				return false; 
			else return true;
		}
		
		

		public static string GetAgeFormate(string age)
		{
			switch(age.ToLower())
			{
				case "a":
					return "ans";
				case "m":
					return "mois";
				case "s":
					return "semaines";
				default:
					return "ans";
			}
		}

		public static bool IsLong(string texte)
		{
			if(texte.Length==0) return false;

			foreach(char c in texte)
				if(!Char.IsDigit(c))
					return false;

			return true;
		}

		public static string GetPatientFormate(string sexe,bool Intime,bool DebutPhrase)
		{
			string retour  = "";

			switch(sexe.ToLower())
			{
				case "h":
					if(Intime) 
						retour+="ton " ;
					else
						retour+="votre ";
					retour+= "patient";
					break;
				case "m":
					if(Intime) 
						retour+="ton " ;
					else
						retour+="votre ";
					retour+= "patient";
					break;
				case "f":
					if(Intime) 
						retour+="ta " ;
					else
						retour+="votre ";
					retour+= "patiente";
					break;
				case "e":
					if(Intime) 
						retour+="ton " ;
					else
						retour+="votre ";
					retour+= "patient(e)";
					break;
				default:
					if(Intime) 
						retour+="ton " ;
					else
						retour+="votre ";
					retour+= "patient(e)";
					break;
			}

			if(DebutPhrase) retour = retour.Substring(0,1).ToUpper() + retour.Substring(1);
			return retour;
		}

			

		public static string GetDateFormatee(DateTime DateAppel)
		{
			string str = "";
			string mois = "";
			switch(DateAppel.Month)
			{
				case 1:
					mois = "janvier";
					break;
				case 2:
					mois = "février";
					break;
				case 3:
					mois = "mars";
					break;
				case 4:
					mois = "avril";
					break;
				case 5:
					mois = "mai";
					break;
				case 6:
					mois = "juin";
					break;
				case 7:
					mois = "juillet";
					break;
				case 8:
					mois = "août";
					break;
				case 9:
					mois = "septembre";
					break;
				case 10:
					mois = "octobre";
					break;
				case 11:
					mois = "novembre";
					break;
				case 12:
					mois = "décembre";
					break;
			}
			str = "le " + DateAppel.Day + " " + mois + " " + DateAppel.Year + " ";
			string moment = "";
			if(DateAppel.Hour>=23 || (DateAppel.Hour>=0 && DateAppel.Hour<5)) 
				moment = " dans la nuit";
			else if(DateAppel.Hour>5 && DateAppel.Hour < 12) 
				moment = " au matin";
			else if(DateAppel.Hour>12 && DateAppel.Hour < 18) 
				moment = " dans l'après-midi";
			else if(DateAppel.Hour>18 && DateAppel.Hour < 23) 
				moment = " au soir";
			str+=moment;			
			return str;
		}

		public static string GetSexeFormate(string sexe)
		{
			switch(sexe.ToLower())
			{
				case "h":
					return "Monsieur";
				case "m":
					return "Monsieur";
				case "f":
					return "Madame";
				case "e":
					//return "L'enfant";
					return "Aux parents de";
				case "s":
					return "";
				default:
					return "Madame,Monsieur";
			}
		}

		public static string GetAdresseFormatee(string Chez,string Rue,string Num,string Np,string Commune)
		{
			string retour =  "\r\n";
			if(Chez.Trim()!="")
				retour += "C/o " + Chez + "\r\n";
			
			string[] TabRue = Rue.Split(',');
			if(TabRue.Length==1)
				retour+=Rue;
			else if(TabRue.Length==2)
				retour+=TabRue[1] + " " + TabRue[0];
			else
				retour+=Rue;

			retour+=" " + Num;
			retour+="\r\n";
			retour+=Np ;

			if(Np!="") retour+= " ";

			string[] Tabcommune = Commune.Split(',');
			if(Tabcommune.Length==1)
				retour+=Commune;
			else if(Tabcommune.Length==2)
				retour+=Tabcommune[1] + " " + Tabcommune[0];
			else
				retour+=Commune;

			return retour;

		}

		private static string FormatTelEspace(string tel)
		{
			return tel.Substring(0,2) + " " + tel.Substring(2,2) + " " + tel.Substring(4,2) + " " +tel.Substring(6,2) + " " + tel.Substring(8,2); 
		}

		public static string GetFormuleConfrere(char sexe)
		{
			switch(sexe.ToString().ToLower())
			{
				case "h":
					return "Cher confrère";
				case "m":
					return "Cher confrère";
				case "f":
					return "Chère consoeur";
				default:
					return "Madame,Monsieur";
			}
		}

		public static bool ValiditeDate(string strDate)
		{
			try
			{
				DateTime.Parse(strDate);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static string FirstLetterUpper(string str)
		{
			if(str=="") return str;
			
			string retour = "";

			string[] tab = str.Split(' ');
			foreach(string s in tab)
				if(s!="")
					retour += s.Substring(0,1).ToUpper() + s.Remove(0,1).ToLower() + " ";
			retour = retour.Remove(retour.Length-1,1);

			return retour;
		}

		public static string FormatePreNom(string str)
		{
			if(str=="") return str;
			
			string retour="";
			for(int i=0;i<str.Length;i++)
			{
				if(i==0 || (str[i-1]=='-' || str[i-1]==' ')) 
					retour+=str[i].ToString().ToUpper();
				else
					retour+=str[i].ToString().ToLower();
			}			

			return retour;
		}

	
		public static string FormateNom(string Nom)
		{
			return Nom.ToUpper();
		}

		public static string ConvertStringToRtf(string depart)
		{				
			return "";
		}	

		public static string GetTTTFormatte(int Code)
		{
			switch(Code)
			{
				case 1:
					return "Maladie";
				case 2:
					return "Accident";
				case 3: 
					return "Examen";
				default:
					return "";
			}
		}

		public static string FormatMontantArrondi(float montant)
		{
			return FormatMontantArrondi((double)montant).Replace(",",".");
		}

		public static string FormatMontantArrondi(double montant)
		{
			double val=0;
			val = Math.Round(montant,2);
			if(val==0) return "0,00";
			string strVal = val.ToString();
			int indexOfPoint = strVal.IndexOfAny(new char[] {'.',','});
			if(indexOfPoint==-1)
				strVal+=",00";
			else if(indexOfPoint==strVal.Length-2)
				strVal+="0";

			int Centieme = int.Parse(strVal.Substring(strVal.Length-1,1));
			int Dixieme = int.Parse(strVal.Substring(strVal.Length-2,1));
			indexOfPoint =  strVal.IndexOfAny(new char[] {'.',','});
			int Unite = int.Parse(strVal.Substring(0,indexOfPoint));

			switch(Centieme)
			{
				case 1:
					Centieme=0;
					break;
				case 2:
					Centieme=0;
					break;
				case 3:
					Centieme=5;
					break;
				case 4:
					Centieme=5;
					break;
				case 6:
					Centieme=5;
					break;
				case 7:
					Centieme=5;
					break;
				case 8:
					Centieme=0;
					if(Dixieme==9)
					{
						Dixieme=0;
						Unite++;
					}
					else
						Dixieme++;
					break;					
				case 9:
					Centieme=0;
					if(Dixieme==9)
					{
						Dixieme=0;
						Unite++;
					}
					else
						Dixieme++;
					break;
				default:
					break;
			}

			return Unite + "." + Dixieme + Centieme;
		}		

		public static string GetFactureCode2(string CodeB)
		{
			return FormatteString(CodeB,"0 00000 00000 0000") + Facture_Modulo10(CodeB);
		}

		public static string GetFactureCodeB(long NFacture,long CodeIntervenant)
		{
			return FormatteString(NFacture.ToString(),"0000000") + "9" + FormatteString(CodeIntervenant.ToString(),"0000000"); 
		}

		public static string FormatteString(string myString, string format)
		{
			string retour="";

			if(myString.Length==0) return format;

			int IdxString = myString.Length-1;

			for(int i=0;i<format.Length;i++)
			{
				if(format[format.Length-1-i]!=' ' && IdxString>=0)
				{
					retour=myString.Substring(IdxString,1) + retour;
					IdxString--;
				}
				else
					retour = format[format.Length-1-i].ToString() + retour;
			}
			return retour;
		}
		public static string Modulo10(string P_serie)

		{
			int[][] tableau = new int[10][];

			for (int t = 0; t < 10; t++)

				tableau[t] = new int[10];

			int k = P_serie.Length+1;
			int[] report = new int[k];
			report[0] = 0;


			tableau[0][0] = 0;
			tableau[0][1] = 9;
			tableau[0][2] = 4;
			tableau[0][3] = 6;
			tableau[0][4] = 8;
			tableau[0][5] = 2;
			tableau[0][6] = 7;
			tableau[0][7] = 1;
			tableau[0][8] = 3;
			tableau[0][9] = 5;

			for(int i=1; i<10; i++)
				for(int j=0; j<10; j++)
				{
					tableau[i][j] = tableau[i-1][(j+1)%10];
				}

			for (int c = 0; c < k - 1; c++)
			{
				int chiffre = Convert.ToInt32(String.Format("{0}", P_serie[c]));
				report[c + 1] = tableau[report[c]][chiffre];
			}

			return String.Format("{0}", (10 - report[k-1]) % 10);
		}



		public static string Facture_Modulo10(string Serie)
		{
			//string retour="";
			int K,Chiffre,Errflag;

			K = Serie.Length;

			int[][] tableau = Tableau();

            int[] repor = new int[K+1];
			repor[0] = 0;

			if (K==0) return "";

			Errflag=0;

			char Car;
			for(int i=0;i<K;i++)
			{
				Car = Serie.Substring(i,1)[0];
				if(!Char.IsDigit(Car) && Car!=' ') Errflag = 1;
			}

			if(Errflag!=0)
			{
				if(Errflag==1)
					return "ERR:IL Y A UN CARACTERE DANS LA CHAINE A CODER";
				else
					return "ERR";
			}
			else
			{
				for(int i=0;i<K;i++)
				{
					Chiffre=int.Parse(Serie.Substring(i,1));
					repor[i+1] = tableau[repor[i]][Chiffre];
				}

				int z = 10 - repor[K-1];
				z = z%10;
				return z.ToString();
			}
		}

		public static int[][] Tableau()
		{
			int[][] tab = new int[10][];

            tab[0] = new int[10];
			tab[0][0] = 0;
			tab[0][1] = 9;
			tab[0][2] = 4;
			tab[0][3] = 6;
			tab[0][4] = 8;
			tab[0][5] = 2;
			tab[0][6] = 7;
			tab[0][7] = 1;
			tab[0][8] = 3;
			tab[0][9] = 5;

			for(int i=1;i<10;i++)
			{
				tab[i] = new int[10];
				for(int j=0;j<10;j++)
				{
					int k = j+1;
					tab[i][j] = tab[i-1][k%10];			
				}
			}

			return tab;
		}
	}