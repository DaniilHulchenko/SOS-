using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip; 

	public class ZipClass
	{
		public ZipClass(string[] args)
		{
					
			if (args.Length!=0)
			{	
				//Compactage
				if(args[1]=="1")
				{
					ZipFile(@args[0], "Techhead", 9);
				}
				//Decompactage
				if(args[1]=="2")
				{
					UnzipFile(@args[0],@"c:\\uncompress");
				}
				if(args[1]=="3")
				{
					ExplorZip(@args[0]);
				}
			}
		}
		public static void ExplorZip(string zipFile)
		{
		
			ZipFile zFile = new ZipFile(zipFile);

            foreach(ZipEntry eZip in zFile)
			{
				DateTime d= eZip.DateTime;
			}
		
		}
		public static void UnzipFile(string zipFile, string destinationDirectory )
		{ 			
			// On regarde bien que le fichier existe
			if (!File.Exists(zipFile)) throw new System.Exception(" Le fichier " + zipFile + " n'existe pas.");
			// On creer un repertoire pour dezipper l'archive 
			if (!Directory.Exists(destinationDirectory)) Directory.CreateDirectory(destinationDirectory);
			if (!destinationDirectory.EndsWith(Path.DirectorySeparatorChar.ToString())) destinationDirectory += Path.DirectorySeparatorChar;
			// ouvre le fichier zip

			ZipInputStream zipIStream = new ZipInputStream(File.OpenRead(zipFile));
			ZipEntry theEntry;
		
			//on boucle a travers chaque entre dans le fichier pour l'ecrire dans notre repertoire
			while ((theEntry = zipIStream.GetNextEntry()) != null) 
			{             	
				if (theEntry.IsDirectory) 
					Directory.CreateDirectory (destinationDirectory + theEntry.Name); 
				else
				{	
					// ecris chaque entry dans un fichier
					// write each entry out to a file
					int size = 2048; 				
					byte[] data = new byte[size]; 		
					FileStream fs = new FileStream(destinationDirectory + theEntry.Name, FileMode.Create);		
					while ((size = zipIStream.Read(data, 0, data.Length)) > 0) 
					{ 						
						fs.Write(data, 0, size); 	
					}
					//ferme le flux
					fs.Flush(); 				
					fs.Close(); 				
				}             
			}
			//ferme le flux
			zipIStream.Close(); 	
	
		}
		public static void ZipFile(string dirPath, string zipComment, int zipLvl)
		{
			string[] repertoires = Directory.GetDirectories(dirPath);
			
            foreach(string rep in repertoires)
			{
				string[] tabrep = rep.Split('\\');
				string nomrep = tabrep[tabrep.Length-1];
				
                if(nomrep=="ZIP") continue;    //On ne fait rien de ce qui suit car le dossier ZIP existe déjà
				
                DirectoryInfo di =new DirectoryInfo(rep+ "\\");
				
                if(di.Exists)
				{
					FileInfo[] fis = di.GetFiles();
					
					Directory.CreateDirectory(dirPath + "\\" + "ZIP");
					FileStream fZip =File.Create(dirPath + "\\ZIP\\" + nomrep + ".zip");
					//Le nom du fichier compresse que l'on veut creer
					//ZipOutputStream permet d'écrire des fichiers dans une archive Zip.
					//Les unes apres les autres 
					ZipOutputStream zipOStream = new ZipOutputStream(fZip);

					//taux de compression 0 pour seulement arciver -- 9 pour une meilleure compression
					zipOStream.SetLevel(zipLvl);
	
					//fixe le commentaire sur le fichier creer
					zipOStream.SetComment(zipComment);
		
					foreach(FileInfo fi in fis)
					{
						FileStream fs = File.OpenRead(di.FullName + fi.Name);
			
						// cree un buffer de byte de la taille du fichier
						byte[] tampon = new byte[fs.Length];
						fs.Read(tampon, 0, tampon.Length);
				
						//on cree un nouvel objet ZipEntry pour chaque fichier
						ZipEntry entry = new ZipEntry((fi.Name));
						// start a new zip entry.Clone et ferme automatiquement la precedente.
					
						zipOStream.PutNextEntry(entry);
						zipOStream.Write(tampon, 0, tampon.Length);
					}
					//finit le flux ZipOutputStream. 
			
					zipOStream.Finish();
					//puis ferme les flux
					zipOStream.Close();
					fZip.Close();					
				}
			}			
		}
	}
