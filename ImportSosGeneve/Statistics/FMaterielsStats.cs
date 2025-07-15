using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using ImportSosGeneve.Statistics.Models;
using ClosedXML.Excel;
using System.Linq;
using System.Data.SqlClient;

namespace ImportSosGeneve.Statistics
{
    public partial class FMaterielsStats : Form
    {
        // MATERIAL CONSTANTS
        private const string MATERIAL_VALIUM = "Valium";
        private const string MATERIAL_MORPHINE = "Morphine";
        private const string MATERIAL_PETHIDINE = "Pethidine";
        private const string MATERIAL_FENTANYL = "Fentanyl";
        private const string MATERIAL_METHADONE = "Methadone";

        public FMaterielsStats()
        {
            InitializeComponent();

            cbMateriel.Items.AddRange(new string[]
            {
                MATERIAL_VALIUM,
                MATERIAL_MORPHINE,
                MATERIAL_PETHIDINE,
                MATERIAL_FENTANYL,
                MATERIAL_METHADONE
            });

        }


        /// <summary>
        /// Handles the click event for the "Get Statistics" button.
        /// Retrieves records for specified material within a specified date range from the database and saves the results to an Excel file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> instance containing the event data.</param>
        /// <remarks>
        /// This method checks if certain material is selected in the ComboBox. 
        /// If valid, it retrieves relevant records from the database and allows the user to specify a location to save the data in an Excel file using a SaveFileDialog.
        /// </remarks>
        /// <exception cref="System.Exception">Thrown if there is an error connecting to the database or writing to the Excel file.</exception>

        private void bGetStatistics_Click(object sender, EventArgs e)
        {
            // Parse dates from DateTimePickers
            DateTime debutDate = dtDebutDate.Value;
            DateTime finalDate = dtFinalDate.Value;

            // Use switch expression to handle material selection
            switch (cbMateriel.SelectedItem?.ToString())
            {
                case MATERIAL_VALIUM:
                    ValiumHandler(debutDate, finalDate);
                    break;

                case MATERIAL_MORPHINE:
                    MaterielsHandler(MATERIAL_MORPHINE, debutDate, finalDate);
                    break;

                case MATERIAL_PETHIDINE:
                    MaterielsHandler(MATERIAL_PETHIDINE, debutDate, finalDate);
                    break;

                case MATERIAL_FENTANYL:
                    MaterielsHandler(MATERIAL_FENTANYL, debutDate, finalDate);
                    break;

                case MATERIAL_METHADONE:
                    MaterielsHandler(MATERIAL_METHADONE, debutDate, finalDate);
                    break;

                default:
                    // Show error if the selected item is not recognized
                    MessageBox.Show("Veuillez sélectionner un matériel valide ('Valium', 'Morphine', ou 'Pethidine').", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }


        public void MaterielsHandler(string materialColumn, DateTime debutDate, DateTime finalDate)
        {
            if (debutDate <= finalDate)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["Connection_Base"].ToString();
                List<MaterialRecord> records = new List<MaterialRecord>();

                string query = $@"
            SELECT 
                tm.Nom AS Medecin, 
                CONCAT(tper.Nom, ' ', tper.Prenom) AS Patient, 
                SUM(tc.{materialColumn}) AS Quantity
            FROM 
                tableconsultations tc 
            INNER JOIN 
                tableactes ta ON ta.Num = tc.CodeAppel
            INNER JOIN 
                tablemedecin tm ON tm.CodeIntervenant = ta.CodeIntervenant
            INNER JOIN 
                tablepatient tp ON ta.IndicePatient = tp.IdPatient
            INNER JOIN 
                tablepersonne tper ON tp.IdPersonne = tper.IdPersonne
            WHERE 
                tc.{materialColumn} <> 0
                AND ta.DAP BETWEEN @DateDeb AND @DateFin
            GROUP BY 
                tm.Nom, CONCAT(tper.Nom, ' ', tper.Prenom)
            ORDER BY 
                tm.Nom";

                try
                {
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (var command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@DateDeb", debutDate);
                            command.Parameters.AddWithValue("@DateFin", finalDate);

                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    records.Add(new MaterialRecord
                                    {
                                        Medecin = reader.IsDBNull(reader.GetOrdinal("Medecin")) ? string.Empty : reader.GetString(reader.GetOrdinal("Medecin")),
                                        Patient = reader.IsDBNull(reader.GetOrdinal("Patient")) ? string.Empty : reader.GetString(reader.GetOrdinal("Patient")),
                                        Quantity = reader.IsDBNull(reader.GetOrdinal("Quantity")) ? 0 : reader.GetInt32(reader.GetOrdinal("Quantity"))
                                    });
                                }
                            }
                        }
                    }

                    if (records.Count == 0)
                    {
                        MessageBox.Show("Les données n'existent pas entre les dates saisies", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xlsx";
                        saveFileDialog.Title = "Save Excel File";
                        saveFileDialog.FileName = $"{materialColumn}Statistics.xlsx";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = saveFileDialog.FileName;
                            SaveMaterialRecordsToExcel(records, filePath);
                            MessageBox.Show("Les données ont été enregistrées avec succès dans le fichier Excel !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Sauvegarde du fichier annulée.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur s'est produite: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("La date de début doit être antérieure ou égale à la date de fin.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void ValiumHandler(DateTime debutDate, DateTime finalDate)
        {
            // Validate date range
            if (debutDate <= finalDate)
            {
                try
                {
                    List<ValiumRecord> records = new List<ValiumRecord>();

                    // Set up the MariaDB connection
                    string connectionString = ConfigurationManager.ConnectionStrings["Connection_Base_Fiche"].ToString();

                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // SQL query with parameter placeholders
                        string query = @"
                        SELECT 
                            v.NomPatient, 
                            v.PrenomPatient,
                            v.DateC as DateC,
                            la.Num_Appel,
                            la.Libelle,
                            la.Categorie,
                            la.Qte as Qte
                        FROM 
                            listeamm la
                        INNER JOIN 
                            visite v ON la.Num_Appel = v.Num_Appel
                        WHERE 
                            la.IdTMM = 71  -- Filter for Valium
                            AND v.DateC BETWEEN @DateDeb AND @DateFin  -- Filter by consultation date
                        ORDER BY 
                            la.Num_Appel";

                        using (var command = new MySqlCommand(query, connection))
                        {
                            // Add parameters with the DateTime values
                            command.Parameters.AddWithValue("@DateDeb", debutDate);
                            command.Parameters.AddWithValue("@DateFin", finalDate);

                            // Execute the query and process results
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    records.Add(new ValiumRecord
                                    {
                                        NomPatient = !reader.IsDBNull(reader.GetOrdinal("NomPatient")) ? reader.GetString("NomPatient") : string.Empty,
                                        PrenomPatient = !reader.IsDBNull(reader.GetOrdinal("PrenomPatient")) ? reader.GetString("PrenomPatient") : string.Empty,
                                        DateC = !reader.IsDBNull(reader.GetOrdinal("DateC")) ? reader.GetDateTime("DateC") : (DateTime?)null,
                                        NumAppel = !reader.IsDBNull(reader.GetOrdinal("Num_Appel")) ? reader.GetInt64("Num_Appel").ToString() : string.Empty,
                                        Libelle = !reader.IsDBNull(reader.GetOrdinal("Libelle")) ? reader.GetString("Libelle") : string.Empty,
                                        Categorie = !reader.IsDBNull(reader.GetOrdinal("Categorie")) ? reader.GetString("Categorie") : string.Empty,
                                        Qte = !reader.IsDBNull(reader.GetOrdinal("Qte")) ? reader.GetInt32("Qte") : 0
                                    });
                                }
                            }
                        }
                    }

                    if (records.Count == 0)
                    {
                        MessageBox.Show("Les données n'existent pas entre les dates saisies", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Retrieve doctor information from the secondary database for unique Num_Appel values(FicheVisite db doesn`t has information about doctors)
                    var numAppelList = string.Join(",", records.Select(r => r.NumAppel).Distinct());

                    List<DoctorInfo> doctorInfos = new List<DoctorInfo>();
                    string queryDoctorInfo = @"
                        SELECT 
                            a.Num_Appel,
                            m.Nom AS MedecinNom,
                            m.Prenom AS MedecinPrenom
                        FROM 
                            appels a
                        INNER JOIN 
                            medecins m ON a.CodeMedecin = m.CodeMedecin
                        WHERE 
                            a.Num_Appel IN (" + numAppelList + ")";

                    using (var connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["Connection_Base_Regul"].ToString()))
                    {
                        connection.Open();
                        using (var command = new MySqlCommand(queryDoctorInfo, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var doctorInfo = new DoctorInfo
                                    {
                                        NumAppel = reader.GetInt64("Num_Appel").ToString(),
                                        MedecinNom = reader.GetString("MedecinNom"),
                                        MedecinPrenom = reader.GetString("MedecinPrenom")
                                    };
                                    doctorInfos.Add(doctorInfo);
                                }
                            }
                        }
                    }

                    // Merge doctor information with ValiumRecords
                    foreach (ValiumRecord record in records)
                    {
                        DoctorInfo doctor = doctorInfos.FirstOrDefault(d => d.NumAppel == record.NumAppel);
                        if (doctor != null)
                        {
                            record.MedecinNom = doctor.MedecinNom;
                            record.MedecinPrenom = doctor.MedecinPrenom;
                        }
                    }

                    // Open a SaveFileDialog to specify where to save the Excel file
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Excel Files|*.xlsx";
                        saveFileDialog.Title = "Save Excel File";
                        saveFileDialog.FileName = "ValiumStatistics.xlsx";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string filePath = saveFileDialog.FileName;

                            // Save records to the specified file path
                            SaveRecordsToExcel(records, filePath);
                            MessageBox.Show("Les données ont été enregistrées avec succès dans le fichier Excel !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Sauvegarde du fichier annulée.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur s'est produite: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Show error if the date range is invalid
                MessageBox.Show("La date de début doit être antérieure ou égale à la date de fin.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SaveMaterialRecordsToExcel(List<MaterialRecord> records, string filepath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Material Statistics");

                // Add headers
                worksheet.Cell(1, 1).Value = "Medecin";
                worksheet.Cell(1, 2).Value = "Patient";
                worksheet.Cell(1, 3).Value = "Quantity";

                // Add records to the worksheet
                for (int i = 0; i < records.Count; i++)
                {
                    var record = records[i];
                    int excelIndex = i + 2;

                    worksheet.Cell(excelIndex, 1).Value = record.Medecin;
                    worksheet.Cell(excelIndex, 2).Value = record.Patient;
                    worksheet.Cell(excelIndex, 3).Value = record.Quantity;
                }

                // Auto-fit columns for readability
                worksheet.Columns().AdjustToContents();

                // Save the workbook
                workbook.SaveAs(filepath);
            }
        }

        /// <summary>
        /// Saves a list of Valium records to an Excel file with a specified file path.
        /// </summary>
        /// <param name="records">A list of <see cref="ValiumRecord"/> objects containing data to be saved.</param>
        /// <param name="filePath">The file path where the Excel file should be saved.</param>
        /// <remarks>
        /// This method creates a new Excel workbook and adds a worksheet named "Statistics."
        /// The headers "NomPatient," "PrenomPatient," "DateC," "NumAppel," "Libelle," "Categorie," and "Qtt" are added to the first row.
        /// Each <see cref="ValiumRecord"/> in the list is then added to subsequent rows, with columns auto-fitted for readability.
        /// The workbook is saved to the specified file path.
        /// </remarks>
        /// <exception cref="System.IO.IOException">Thrown when there is an issue saving the file to the specified path.</exception>
        public void SaveRecordsToExcel(List<ValiumRecord> records, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Valium Statistics");

                // Add headers
                worksheet.Cell(1, 1).Value = "NomPatient";
                worksheet.Cell(1, 2).Value = "PrenomPatient";
                worksheet.Cell(1, 3).Value = "MedecinNom";
                worksheet.Cell(1, 4).Value = "MedecinPrenom";
                worksheet.Cell(1, 5).Value = "DateC";
                worksheet.Cell(1, 6).Value = "NumAppel";
                worksheet.Cell(1, 7).Value = "Libelle";
                worksheet.Cell(1, 8).Value = "Categorie";
                worksheet.Cell(1, 9).Value = "Qte";

                // Add records to the worksheet
                for (int i = 0; i < records.Count; i++)
                {
                    var record = records[i];
                    int excelIndex = i + 2;

                    worksheet.Cell(excelIndex, 1).Value = record.NomPatient;
                    worksheet.Cell(excelIndex, 2).Value = record.PrenomPatient;
                    worksheet.Cell(excelIndex, 3).Value = record.MedecinNom;
                    worksheet.Cell(excelIndex, 4).Value = record.MedecinPrenom;
                    worksheet.Cell(excelIndex, 5).Value = record.DateC;
                    worksheet.Cell(excelIndex, 6).Value = record.NumAppel;
                    worksheet.Cell(excelIndex, 7).Value = record.Libelle;
                    worksheet.Cell(excelIndex, 8).Value = record.Categorie;
                    worksheet.Cell(excelIndex, 9).Value = record.Qte;
                }

                // Auto-fit columns for better readability
                worksheet.Columns().AdjustToContents();

                // Save the workbook
                workbook.SaveAs(filePath);
            }
        }
    }
}
