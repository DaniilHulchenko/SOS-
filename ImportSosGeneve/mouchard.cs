using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ImportSosGeneve
{
    class mouchard
    {

        public static bool evenement(string message, string utilisateur)
        {
            //on ecrit l'evènement dans la base
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base_IP"].ToString();       //stokage des périodes dans la base      
                SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config

                //on ouvre la connexion
                dbConnection.Open();    

                SqlCommand cmd = dbConnection.CreateCommand();       //Pour l'insert (avec transaction)              
           //     SqlTransaction transaction;

            //    transaction = dbConnection.BeginTransaction("Transac_AjoutEvenement");                 //Démarre une transaction locale     

                try
                {                    
                    cmd.Connection = dbConnection;
                //    cmd.Transaction = transaction;          //On démarre une transaction


                    string sqlstr0 = "select max(id) from Evenements";       //recup du plus grand Id   

                    cmd.CommandText = sqlstr0;

                    DataSet DSResult0 = new DataSet();       //on déclare le DataSet pour recevoir les diverses données

                    DSResult0.Tables.Add("Resultats");      //on déclare une table pour cet ensemble de donnée
                    DSResult0.Tables["Resultats"].Load(cmd.ExecuteReader());       //on execute               

                    int idEvenement = 0;

                    try
                    {                        
                        idEvenement = int.Parse(DSResult0.Tables["Resultats"].Rows[0][0].ToString()) + 1;
                    }
                    catch (Exception)
                    {
                        idEvenement = 1;  //C'est null
                    }
                    


                    //on définit la requette d'ajout 
                    String sql = "INSERT INTO Evenements (Id, DateEvenement, Evenement, Utilisateur)";
                    sql += " VALUES (@Id, @Date, @Evenement, @utilisateur)";
                    cmd.CommandText = sql;

                    // Ajout des paramètres
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Id", idEvenement);
                    cmd.Parameters.AddWithValue("Date", DateTime.Now);
                    cmd.Parameters.AddWithValue("Evenement", message.ToString());
                    cmd.Parameters.AddWithValue("Utilisateur", utilisateur.ToString());

                    //execution de la requette
                    cmd.ExecuteNonQuery();

                    //on valide la transaction
                  //  transaction.Commit();
                    Console.WriteLine("Transaction réussie.");


                }
                 catch (Exception ex)
                {
                    Console.WriteLine("Exception Validation: {0}", ex.GetType());
                    Console.WriteLine("  Message: {0}", ex.Message);

                    //On essai de faire un Rollback
                    try
                    {
                      //  transaction.Rollback();
                        return (false);
                    }
                    catch (Exception ex2)
                    {
                        //on gère ici toutes les erreurs qui on pu survenir sur le serveur pour empêcher le Roolback...commme par exemple une connexion fermée...                       
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                        return (false);
                    }
                }
                finally
                {
                    // En cas de pépins...fermeture de la connexion et reinitialisation des variables
                    if (dbConnection.State == System.Data.ConnectionState.Open)
                        dbConnection.Close();
                }
                        
            return (true);
        }


        public static bool Delete_evenement(DateTime debut, DateTime fin)
        {
            //on efface les evènements dans la base
            string connex = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();       //stokage des périodes dans la base      
            SqlConnection dbConnection = new SqlConnection(connex);      //Chaine de connection récupérée dans le app.config

            //on ouvre la connexion
            dbConnection.Open();

            SqlCommand cmd = dbConnection.CreateCommand();       //Pour l'insert (avec transaction)              
            SqlTransaction transaction;

            transaction = dbConnection.BeginTransaction("TransacSupprEvenements");                 //Démarre une transaction locale     

            try
            {

                cmd.Connection = dbConnection;
                cmd.Transaction = transaction;          //On démarre une transaction                

                //on définit la requette d'ajout 
                String sql = "DELETE FROM Evenements WHERE DateEvenement between @DateDeb and @DateFin";

                cmd.CommandText = sql;

                // Ajout des paramètres
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("DateDeb", debut.Date);
                cmd.Parameters.AddWithValue("DateFin", fin.Date);

                //execution de la requette
                cmd.ExecuteNonQuery();

                //on valide la transaction
                transaction.Commit();
                Console.WriteLine("Transaction réussie.");


            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Validation: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);

                //On essai de faire un Rollback
                try
                {
                    transaction.Rollback();
                    return (false);
                }
                catch (Exception ex2)
                {
                    //on gère ici toutes les erreurs qui on pu survenir sur le serveur pour empêcher le Roolback...commme par exemple une connexion fermée...                       
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                    return (false);
                }
            }
            finally
            {
                // En cas de pépins...fermeture de la connexion et reinitialisation des variables
                if (dbConnection.State == System.Data.ConnectionState.Open)
                    dbConnection.Close();
            }

            return (true);
        }




    }

}
