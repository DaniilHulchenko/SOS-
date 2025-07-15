using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using ImportSosGeneve.Commun.Models;

namespace ImportSosGeneve.Commun
{
    public partial class AjoutMedecin : Form
    {
        private const int SERVICE_NUMBER = 104;
        private const int CPTFACM = 0;
        private const string CONCORDAT = "C 1476.25";

        public AjoutMedecin()
        {
            InitializeComponent();
        }

        private void btAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateChildren(ValidationConstraints.Enabled))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();

                    MedecinInsert medecin = new MedecinInsert
                    {
                        CodeIntervenant = Convert.ToInt16(tbCodeMedecin.Text),
                        Nom = $"{tbNom.Text.ToUpper()} {tbPrenom.Text}",
                        Initiale = tbInitiale.Text,
                        Service = SERVICE_NUMBER, // ALL MEDECINS HAVE VALUE 104
                        Archive = cbArchive.Checked ? 1 : 0,
                        Mail = tbMail.Text,
                        NomGeneve = tbNom.Text.ToUpper(),
                        PrenomGeneve = tbPrenom.Text,
                        Concordat = CONCORDAT, // ALL MEDECINS HAVE VALUE C 1476.25
                        EAN = tbEAN.Text,
                        NIF = null, // ALL MEDECINS DON`T HAVE VALUE
                        Independant = cbIndependant.Checked ? '1' : '0',
                        Commentaire = string.IsNullOrEmpty(rtbCommentaire.Text) ? null : rtbCommentaire.Text,
                        Desactive = cbDesactive.Checked ? 1 : 0,
                        MedInterne = cbMedInterne.Checked ? 1 : 0,
                        DateMajCpt = dateTimePicker1.Value,
                        CptFacM = CPTFACM, // ALL MEDECINS HAVE VALUE 0
                        RCC = null
                    };



                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string sql = @"
                    INSERT INTO [dbo].[tablemedecin]
                        ([CodeIntervenant]
                        ,[Nom]
                        ,[Initiale]
                        ,[Service]
                        ,[Archive]
                        ,[Mail]
                        ,[NomGeneve]
                        ,[PrenomGeneve]
                        ,[Concordat]
                        ,[EAN]
                        ,[NIF]
                        ,[Independant]
                        ,[Commentaire]
                        ,[Desactive]
                        ,[MedInterne]
                        ,[DateMajCpt]
                        ,[CptFactM]
                        ,[RCC])
                    VALUES
                        (@CodeIntervenant
                        ,@Nom
                        ,@Initiale
                        ,@Service
                        ,@Archive
                        ,@Mail
                        ,@NomGeneve
                        ,@PrenomGeneve
                        ,@Concordat
                        ,@EAN
                        ,@NIF
                        ,@Independant
                        ,@Commentaire
                        ,@Desactive
                        ,@MedInterne
                        ,@DateMajCpt
                        ,@CptFactM
                        ,@RCC)";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            // Set parameters for the SQL command
                            command.Parameters.AddWithValue("@CodeIntervenant", medecin.CodeIntervenant);
                            command.Parameters.AddWithValue("@Nom", medecin.Nom);
                            command.Parameters.AddWithValue("@Initiale", medecin.Initiale);
                            command.Parameters.AddWithValue("@Service", medecin.Service);
                            command.Parameters.AddWithValue("@Archive", medecin.Archive);
                            command.Parameters.AddWithValue("@Mail", medecin.Mail);
                            command.Parameters.AddWithValue("@NomGeneve", medecin.NomGeneve);
                            command.Parameters.AddWithValue("@PrenomGeneve", medecin.PrenomGeneve);
                            command.Parameters.AddWithValue("@Concordat", medecin.Concordat);
                            command.Parameters.AddWithValue("@EAN", medecin.EAN);
                            command.Parameters.AddWithValue("@NIF", (object)DBNull.Value);
                            command.Parameters.AddWithValue("@Independant", medecin.Independant);
                            command.Parameters.AddWithValue("@Commentaire", medecin.Commentaire ?? (object)DBNull.Value);
                            command.Parameters.AddWithValue("@Desactive", medecin.Desactive);
                            command.Parameters.AddWithValue("@MedInterne", medecin.MedInterne);
                            command.Parameters.AddWithValue("@DateMajCpt", medecin.DateMajCpt);
                            command.Parameters.AddWithValue("@CptFactM", medecin.CptFacM);
                            command.Parameters.AddWithValue("@RCC", (object)DBNull.Value);

                            // Open connection and execute the command
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    // Optionally, show a success message or perform any additional logic
                    MessageBox.Show("Enregistrement ajouté avec succès!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                

        }

        private void tbCodeMedecin_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void tbNom_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(tbNom, e);
        }

        private void tbPrenom_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(tbPrenom, e);
        }

        private void tbInitiale_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBox(tbInitiale, e);
        }

        private void tbMail_Validating(object sender, CancelEventArgs e)
        {
            string email = tbMail.Text;

            // Check if the input is empty
            if (string.IsNullOrWhiteSpace(email))
            {
                errorProvider1.SetError(tbMail, "Email cannot be empty.");
                e.Cancel = true; // Prevent focus from leaving the TextBox
                return;
            }

            // Validate the email format using a regular expression
            if (!IsValidEmail(email))
            {
                errorProvider1.SetError(tbMail, "Invalid email format.");
                e.Cancel = true; // Prevent focus from leaving the TextBox
                return;
            }

            // Clear any previous error
            errorProvider1.SetError(tbMail, null);
        }

        private void ValidateTextBox(TextBox textBox, CancelEventArgs e)
        {
            // Get the text from the TextBox
            string input = textBox.Text;

            // Check if the input is empty
            if (string.IsNullOrWhiteSpace(input))
            {
                errorProvider1.SetError(textBox, "Field cannot be empty.");
                e.Cancel = true; // Prevent focus from leaving the TextBox
                return;
            }

            // Check if the input contains only letters
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    errorProvider1.SetError(textBox, "Field must contain only letters.");
                    e.Cancel = true; // Prevent focus from leaving the TextBox
                    return;
                }
            }

            // Optionally, trim the input to remove any leading or trailing spaces
            textBox.Text = input.Trim();
            errorProvider1.SetError(textBox, null);
        }

        private bool IsValidEmail(string email)
        {
            // Regular expression for validating an email address
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, pattern);
        }

        private void tbCodeMedecin_Validating(object sender, CancelEventArgs e)
        {
            string input = tbCodeMedecin.Text;

            // Check if the input is empty
            if (string.IsNullOrWhiteSpace(input))
            {
                errorProvider1.SetError(tbCodeMedecin, "Field cannot be empty.");
                e.Cancel = true; // Prevent focus from leaving the TextBox
                return;
            }

            errorProvider1.SetError(tbCodeMedecin, null);
        }

        private void tbEAN_Validating(object sender, CancelEventArgs e)
        {
            string input = tbEAN.Text;

            // Check if the input is empty
            if (string.IsNullOrWhiteSpace(input))
            {
                errorProvider1.SetError(tbEAN, "Field cannot be empty.");
                e.Cancel = true; // Prevent focus from leaving the TextBox
                return;
            }

            errorProvider1.SetError(tbEAN, null);
        }
    }
}
