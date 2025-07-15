Imports SosMedecins.SmartRapport.DAL
Imports SosMedecins.Connexion

Public Class frmModificationSolde

    Private _drwFacture As dstElementsFacture.factureRow

    Public Sub New(ByVal p_dstElementsFacture As dstElementsFacture, ByVal p_lngNFacture As Long)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        lblAvertissement.Text = "Attention !!!" & vbCrLf & " vous allez modifier le solde de la facture"

        _drwFacture = p_dstElementsFacture.facture.FindByNFacture(p_lngNFacture)
        txtAncienSolde.Text = FormatCurrency(_drwFacture.Solde.ToString(), 2)
    End Sub

    Private Sub btnAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnnuler.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnValider_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValider.Click

        Dim z_dalTableFacture As New TableFacture
        _drwFacture.Solde = z_dalTableFacture.UpdateSolde(_drwFacture.NFacture, CType(txtNouveauSolde.Text, Double), Now.ToString())
        z_dalTableFacture = Nothing

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub txtNouveauSolde_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNouveauSolde.TextChanged
        If txtNouveauSolde.Text.Length > 0 Then
            btnValider.Enabled = True
        End If
    End Sub
End Class