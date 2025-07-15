Public Class uscGestion
    Private _drwFacture As DataRow

#Region " Arrangement "
    Private _dtbArrangement As DataTable

    Private Sub ChargeArrangement(ByVal p_drwFacture As DataRow)
        Dim z_blnConnect As Boolean = SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.OpenBDD()
        Try
            _dtbArrangement = SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.ExecuteSql(Nothing, SosMedecins.SmartRapport.DAL.RequetesSelect.facture_arrangement.Nfacture.Replace("%NFacture%", p_drwFacture("NFacture").ToString()))
        Finally
            If (z_blnConnect) Then
                SosMedecins.SmartRapport.DAL.Variables.ConnexionBase.CloseBDD()
            End If
        End Try
        AfficheArrangement()
    End Sub

    Private Sub AfficheArrangement()
        btnArrangement.Enabled = True
        If (_dtbArrangement.Rows.Count > 0) Then
            btnArrangement.Text = "Modifier"

            txtArrangementDate.Text = CType(_dtbArrangement.Rows(0)("DateCreation"), Date).ToString("dd.MM.yyyy")
            txtArrangementUser.Tag = _dtbArrangement.Rows(0)("CodeUtilisateur").ToString()
            txtArrangementUser.Text = _dtbArrangement.Rows(0)("Nom").ToString()
            txtArrangement.Text = _dtbArrangement.Rows(0)("Commentaire").ToString()
        Else
            btnArrangement.Text = "Ajouter"

            txtArrangementDate.Text = ""
            txtArrangementUser.Tag = ""
            txtArrangementUser.Text = ""
            txtArrangement.Text = ""
        End If
    End Sub


    Private Sub btnArrangement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArrangement.Click
        Dim z_frmArrangement As New frmArrangement(_dtbArrangement, _drwFacture("NFacture").ToString())
        '
        z_frmArrangement.ShowDialog()
        z_frmArrangement.Dispose()
        z_frmArrangement = Nothing
        '
        AfficheArrangement()
    End Sub
#End Region



End Class
