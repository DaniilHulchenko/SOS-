using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
namespace ImportSosGeneve
{
    class LogMod
    {
        //objet de log qui sera unique (singleton)
        /// <summary>
        /// 
        /// </summary>
        private log4Raid objLog;

        //procédure qui sera appeler pour ecrire dans un fichier log (exemple)
        public void EcrireLog(string type, string texte)
        {
            if ((objLog == null))
            {//\\Sos_stockage\Compta\BVR\Postfinance
                //objLog = new log4Raid("\\\\192.168.0.12\\Compta\\BVR\\\\Postfinance\\logs", "Log", 0, "");
                objLog = new log4Raid(Environment.CurrentDirectory + "\\logs", "Log", 0, "");
                
            }
            objLog.EcrireLog(type, texte);
        }



        //classe de log
        private class log4Raid
        {
            //format de la date
            private string date_format;
            //chemin vers le dossier des logs
            private string path;
            //nom de base des fichiers de log
            private string base_name;
            //longeur max avant de changer de numéro de fichier
            private long file_length;

            public log4Raid(string p_path, string p_base_name,  // ERROR: Optional parameters aren't supported in C# 
                long p_file_length, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("dd/MM/yyyy hh:mm:ss")] // ERROR: Optional parameters aren't supported in C#
 string p_date_format)
            {
                path = p_path;
                base_name = p_base_name;
                file_length = p_file_length;
                date_format = p_date_format;
            }

            public void EcrireLog(string type, string texte)
            {
                try
                {
                    //recherche du nom de fichier
                    //liste des fichiers du repertoire
                    string[] fichiers = Directory.GetFiles(path, "*.txt");
                    //definition du nom du fichier
                    string nomFichier = "";
                    // trie des fichiers par ordre alphabétique
                    Array.Sort(fichiers);
                    //si on a au moins un fichiers
                    if ((fichiers.Length > 0))
                    {
                        // le nom du fichiers sera (temporairement) le dernier de la liste
                        //nomFichier = fichiers(fichiers.Length - 1);
                        nomFichier = fichiers[fichiers.Length - 1];
                        //récupération des informations du fichier
                        FileInfo fi = new FileInfo(nomFichier);
                        //si le fichier est supérieur à la taille max on remer le nom du fichier à vide
                        if ((fi.Length > file_length))
                        {
                            nomFichier = "";
                        }
                    }

                    // si le nom du fichier est vide on va créer un nouveau fichier
                    if (nomFichier == "")
                    {
                        nomFichier = path + "\\" + base_name + "_" + DateTime.Now.Date.ToShortDateString() + ".txt";
                        MessageBox.Show(path.ToString());
                        
                    }
                    //ouverture d'un lien vers le fichier
                    StreamWriter LogStream = new StreamWriter(nomFichier, true, System.Text.Encoding.UTF8);
                    //ecriture sur le fichier
                    LogStream.WriteLine(DateTime.Now+ ": " + type + " : " + texte);
                    //fermeture du lien vers le fichier
                    LogStream.Close();
                }
                catch (Exception ex)
                {
                    //erreur, impossible d'ecrire dans le fichier
                    Console.WriteLine("Erreur d'écriture dans les fichiers LOG : " + ex.Message);
                }
            }
        }
    }
}
  
