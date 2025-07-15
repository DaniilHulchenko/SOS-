using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImportSosGeneve.Facture
{
    public partial class FacturePatient : Form
    {
        public FacturePatient(DataRow rowConsultation)
        {
            InitializeComponent();

            CtrlFacturation m_Ctrl = new CtrlFacturation(rowConsultation);
            this.Controls.Add(m_Ctrl);
            m_Ctrl.Dock = DockStyle.Fill;	
        }
    }
}