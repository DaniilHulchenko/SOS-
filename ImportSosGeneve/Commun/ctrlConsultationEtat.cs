using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace ImportSosGeneve.Commun
{
    public partial class ctrlConsultationEtat : UserControl
    {

        private BindingSource _bseDonneesAppel;
        public BindingSource bseDonneesAppel
        {
            get
            {
                return _bseDonneesAppel;
            }
            set
            {
                _bseDonneesAppel = value;
                // affectation des champs
                if (_bseDonneesAppel != null)
                {
                    _bseDonneesAppel.PositionChanged += new EventHandler(Position_Changed);

                    if (lblDateAppel.DataBindings.Count == 0)
                    {
                        ChargeDonnees();
                    }
                }
            }
        }
        
        public ctrlConsultationEtat()
        {
            InitializeComponent();
        }

        private void Position_Changed(object sender, EventArgs e)
        {
            DataRowView z_drw = (DataRowView)_bseDonneesAppel.Current;

            //if (_bseDonneesAppel.Current == "0")
            if (z_drw.Row["AnnulationAppel"].ToString() == "0")
            {
                grpGeneral.BackColor = Color.LightSteelBlue;
                grpPriseEnCharge.BackColor = Color.LightSteelBlue;
                grpAnnulation.BackColor = Color.LightSteelBlue;
            }
            else
            {
                grpGeneral.BackColor = Color.RosyBrown;
                grpPriseEnCharge.BackColor = Color.RosyBrown;
                grpAnnulation.BackColor = Color.RosyBrown;
            }
        }

        private void ChargeDonnees()
        {
            //string valeurNulle = "______";
            lblDateAppel.DataBindings.Add("Text", _bseDonneesAppel, "DAP");
            lbMotif1.DataBindings.Add("Text", _bseDonneesAppel, "Motif1");

            //groupBox1.Text += " : Index " + row["Num"].ToString();
            //lblDateAppel.Text = "Appel du : " + DateTime.Parse(row["DAP"].ToString()).ToLongDateString() + " à " + DateTime.Parse(row["DAP"].ToString()).ToShortTimeString();
            //if (row["Motif1"].ToString() != "")
            //    lbMotif1.Text = row["Motif1"].ToString();
            //else
            //    lbMotif1.Text = valeurNulle;
            //if (row["Motif2"].ToString() != "")
            //    lbMotif2.Text = row["Motif2"].ToString();
            //else
            //    lbMotif2.Text = valeurNulle;
            lbUrgence.DataBindings.Add("Text", _bseDonneesAppel, "Urgence");

            //if (row["AnnulationAppel"].ToString() == "1")
            //{
            //    lblAnnulation.Text = "Annulé à  " + DateTime.Parse(row["DAN"].ToString()).ToShortTimeString();
            //    lblMotifAnnulation.Text = row["MotifAnnulation"].ToString();
            //    lblDevenirAnnulation.Text = row["DevenirAnnulation"].ToString();
            //}

            //if (row["NomMedecinSos"].ToString() != System.DBNull.Value.ToString())
            //    lblMedecin.Text = row["NomMedecinSos"].ToString();
            //else
            //    lblMedecin.Text = "Aucun médecin affecté";
            Binding binding = new Binding("Text", _bseDonneesAppel, "DRC");
            binding.Format += new ConvertEventHandler(Format_BindingDate);
            lbRCP.DataBindings.Add(binding);

            binding = new Binding("Text", _bseDonneesAppel, "DSL");
            binding.Format += new ConvertEventHandler(Format_BindingDate);
            lbSLL.DataBindings.Add(binding);

            binding = new Binding("Text", _bseDonneesAppel, "DFI");
            binding.Format += new ConvertEventHandler(Format_BindingDate);
            lbFIN.DataBindings.Add(binding);

            binding = new Binding("Text", _bseDonneesAppel, "DFI");
            binding.Format += new ConvertEventHandler(Format_BindingDate);
            txtFin.DataBindings.Add(binding);

            Binding bdgAge = new Binding("Text", _bseDonneesAppel, "UniteAge");
            bdgAge.Format += new ConvertEventHandler(Format_Age);
            lbAge.DataBindings.Add(bdgAge);

            Binding bdgDelai = new Binding("Text", _bseDonneesAppel, "DSL");
            bdgDelai.Format += new ConvertEventHandler(Format_Delai);
            lbDelai.DataBindings.Add(bdgDelai);
            
            
            //if (row["DFI"].ToString() != System.DBNull.Value.ToString() && row["DSL"].ToString() != System.DBNull.Value.ToString())
            //{
            //    TimeSpan duree = DateTime.Parse(row["DFI"].ToString()) - DateTime.Parse(row["DSL"].ToString());
            //    string nbHour = "";
            //    string nbMinute = "";
            //    if (duree.Hours < 10)
            //        nbHour = "0" + duree.Hours;
            //    else
            //        nbHour = duree.Hours.ToString();
            //    if (duree.Minutes < 10)
            //        nbMinute = "0" + duree.Minutes;
            //    else
            //        nbMinute = duree.Minutes.ToString();
            //    lbDuree.Text = nbHour + ":" + nbMinute;
            //}
        }

        private void Format_BindingDate(Object sender, ConvertEventArgs e)
        {
            if (e.Value.ToString() != string.Empty)
                e.Value = DateTime.Parse(e.Value.ToString()).ToString("dd/MM/yyyy");
            else
                e.Value = string.Empty;
        }

        private void Format_Age(Object sender, ConvertEventArgs e)
        {
            DataRowView z_drw = (DataRowView)_bseDonneesAppel.Current;

            if (e.Value.ToString() != string.Empty)
                //    lbAge.Text = row["Age"].ToString() + " " + row["UniteAge"].ToString();
                e.Value = z_drw["Age"].ToString() + " " + z_drw["UniteAge"].ToString();
            else
                e.Value = "???";
        }

        private void Format_Delai(Object sender, ConvertEventArgs e)
        {
            DataRowView z_drw = (DataRowView)_bseDonneesAppel.Current;

            if (z_drw["DSL"].ToString() != System.DBNull.Value.ToString() && z_drw["DRC"].ToString() != System.DBNull.Value.ToString())
            {
                TimeSpan delai = DateTime.Parse(z_drw["DSL"].ToString()) - DateTime.Parse(z_drw["DRC"].ToString());
                string nbHour = "";
                string nbMinute = "";

                if (delai.Hours < 10)
                    nbHour = "0" + delai.Hours;
                else
                    nbHour = delai.Hours.ToString();

                if (delai.Minutes < 10)
                    nbMinute = "0" + delai.Minutes;
                else
                    nbMinute = delai.Minutes.ToString();

                e.Value = nbHour + ":" + nbMinute;
            }
            else
                e.Value = "";
        }
    }
}
