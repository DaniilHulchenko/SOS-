using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace SosMedecins.SmartRapport.Systeme
{
    public class OutilsExt
    {
        //public static Attente.Attente AttentActuelle = null;
        public static Parametrage ParamAppli = null;
      
        public static System.Net.CookieContainer Cookies = null;

        public static void CopyDirectory(string sourcePath, string destinationPath, bool recurse)
        {
            String[] files;
            if (destinationPath[destinationPath.Length - 1] != Path.DirectorySeparatorChar)
                destinationPath += Path.DirectorySeparatorChar;
            if (!Directory.Exists(destinationPath)) Directory.CreateDirectory(destinationPath);
            files = Directory.GetFileSystemEntries(sourcePath);
            foreach (string element in files)
            {
                if (recurse)
                {
                    // copy sub directories (recursively) 
                    if (Directory.Exists(element))
                        CopyDirectory(element, destinationPath + Path.GetFileName(element), recurse);
                    // copy files in directory 
                    else
                        File.Copy(element, destinationPath + Path.GetFileName(element), true);
                }
                else
                {
                    // only copy files in directory 
                    if (!Directory.Exists(element))
                        File.Copy(element, destinationPath + Path.GetFileName(element), true);
                }
            }
        }

        public static void StockeEtape(string filename, string valeur)
        {
            StreamWriter writer = File.AppendText(filename);
            writer.WriteLine(DateTime.Now.ToString() + "-" + valeur);
            writer.Close();
        }
       
    }
}
