using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImportSosGeneve.TA
{
    public partial class InterfGestionTA : Form
    {
        public InterfGestionTA()
        {
            InitializeComponent();
        }

        private void buttonNouveauAbonnement_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            
            //Vars
            DateTime DtDebut = DateTime.Parse(dateTimePicker1.Text.ToString());

            DateTime DtFin = DateTime.Parse(dateTimePicker2.Text.ToString());
            ImportSosGeneve.Donnees.gestionTA = new SosMedecins.SmartRapport.DAL.GestionTA();

            string SQL = "SELECT ROW_NUMBER()OVER(ORDER BY ta.DateCreationAbonnement ASC) AS IdAbonnement, ta.Archive, ta.IdContrat, p.Nom, p.Prenom, tc.NumeroCle as Cle,";
            SQL = SQL + " ta.DateCreationAbonnement as DateCreationAbon ";
            SQL = SQL + " from ta_abonnement ta, tablepersonne p, tablepatient pa, ta_abonnementcle tc";
            SQL = SQL + " where ta.IdPatient = pa.IdPatient";
            SQL = SQL + " and pa.IdPersonne = p.IdPersonne ";
            SQL = SQL + " and tc.IdAbonnement = ta.IdAbonnement ";
            SQL = SQL + " and ta.Archive = 0         ";
            SQL = SQL + " and ta.DateCreationAbonnement between  '" + DtDebut + "' and '" + DtFin + "'";
            SQL = SQL + " group by tc.IdAbonnement,  ta.IdContrat,  ta.Archive, p.Nom, p.Prenom, tc.NumeroCle, ta.DateCreationAbonnement";
            SQL = SQL + " order by ta.DateCreationAbonnement";


            OutilsExt.OutilsSql.RemplitDataTable(ImportSosGeneve.Donnees.gestionTA.Tables[0], SQL);
            
            frmGestionTA frmgest = new frmGestionTA(ImportSosGeneve.Donnees.gestionTA);
            
            frmgest.ShowDialog();
            frmgest.Dispose();
            frmgest = null;
            Cursor.Current = Cursors.Default;
        }

        private void buttonAbnAnnule_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Vars
            DateTime DtDebut = DateTime.Parse(dateTimePicker1.Text.ToString());

            DateTime DtFin = DateTime.Parse(dateTimePicker2.Text.ToString());
            
            ImportSosGeneve.Donnees.gestionTA = new SosMedecins.SmartRapport.DAL.GestionTA();


            string SQL = "SELECT ROW_NUMBER()OVER(ORDER BY ta.DateCreationAbonnement ASC) AS IdAbonnement, ta.Archive, p.Nom, p.Prenom, tc.NumeroCle as Cle , ta.DateCreationAbonnement as DateCreationAbon";
            SQL = SQL + " from ta_abonnement ta, tablepersonne p, tablepatient pa ,ta_abonnementcle tc, ta_abonnementjournal tj ";
            SQL = SQL + " where ta.IdPatient = pa.IdPatient";
            SQL = SQL + " and pa.IdPersonne = p.IdPersonne ";
            SQL = SQL + " and tc.IdAbonnement = ta.IdAbonnement";
            SQL = SQL + " and ta.IdAbonnement = tj.IdAbonnement";
            SQL = SQL + " and tj.TypeOp = 'Annulation'";
            SQL = SQL + " and DateOp between '" + DtDebut + "' and '" + DtFin + "'";
            SQL = SQL + " group by tj.IdAbonnement, tc.IdAbonnement,  ta.Archive, p.Nom, p.Prenom, tc.NumeroCle, ta.DateCreationAbonnement ";
            SQL = SQL + " order by ta.DateCreationAbonnement"; 
                      
            OutilsExt.OutilsSql.RemplitDataTable(ImportSosGeneve.Donnees.gestionTA.Tables[0], SQL);
            
            frmGestionTA frmgest = new frmGestionTA(ImportSosGeneve.Donnees.gestionTA);
            
            frmgest.ShowDialog();
            frmgest.Dispose();
            frmgest = null;
            Cursor.Current = Cursors.Default;
        }

        private void btnNoFax_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DateTime DtDebut = DateTime.Parse(dateTimePicker1.Text.ToString());
            DateTime DtFin = DateTime.Parse(dateTimePicker2.Text.ToString());
            
            ImportSosGeneve.Donnees.gestionTA = new SosMedecins.SmartRapport.DAL.GestionTA();
            
            string SQL = "SELECT ta.IdAbonnement, p.Nom, p.Prenom, tc.NumeroCle as Cle, ta.DateCreationAbonnement as DateCreationAbon  ";
            SQL += " FROM ta_abonnement ta, ta_abonnementcle tc, tablepersonne p, tablepatient pa ";
            SQL += " WHERE tc.IdAbonnement = ta.IdAbonnement ";
            SQL += " AND ta.Archive = 0 AND FaxFsasd = 0 AND p.IdPersonne = pa.IdPersonne AND ta.IdPatient = pa.IdPatient ";
            SQL += " AND ta.DateCreationAbonnement between '" + DtDebut + "' and '" + DtFin + "'";
            
            OutilsExt.OutilsSql.RemplitDataTable(ImportSosGeneve.Donnees.gestionTA.Tables[0], SQL);
            
            frmGestionTA frmgest = new frmGestionTA(ImportSosGeneve.Donnees.gestionTA);
            
            frmgest.ShowDialog();
            frmgest.Dispose();
            frmgest = null;
            Cursor.Current = Cursors.Default;
        }

        
    }
}