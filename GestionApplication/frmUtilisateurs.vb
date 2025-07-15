Public Class frmUtilisateurs

    Dim _dstUtilisateurs As New DataSet

    Dim _bseUtilisateurs As New BindingSource
    Private _strConnexion As SosMedecins.Connexion.AccesDonnees

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _bseUtilisateurs.DataSource = _dstUtilisateurs

        dgvUtilisateurs.DataSource = _bseUtilisateurs
    End Sub

    Private Sub ChargeDonnees()
        txtCodeUtilisateur.DataBindings.Add("Text", _bseUtilisateurs, "CodeUtilisateur")
        txtCommentaire.DataBindings.Add("Text", _bseUtilisateurs, "Commentaire")
        txtEAN.DataBindings.Add("Text", _bseUtilisateurs, "EAN")
        txtInitiales.DataBindings.Add("Text", _bseUtilisateurs, "Initiale")
        txtMail.DataBindings.Add("Text", _bseUtilisateurs, "Mail")
        txtMotDePasse.DataBindings.Add("Text", _bseUtilisateurs, "Pass")
        txtNom.DataBindings.Add("Text", _bseUtilisateurs, "Nom")
        txtNomGeneve.DataBindings.Add("Text", _bseUtilisateurs, "NomGeneve")
        txtPrenomGeneve.DataBindings.Add("Text", _bseUtilisateurs, "PrenomGeneve")

        'cbxDroits
        'dtpDateDerConnexion

    End Sub

    Private Sub frmUtilisateurs_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _strConnexion = SosMedecins.SmartRapport.DAL.Variables.ConnexionBase

        _strConnexion.OpenBDD()
        Try
            Dim z_strSql As String = "Select CodeUtilisateur,Commentaire,EAN,Initiale,Mail,Pass,Nom, NomGeneve, PrenomGeneve from tableutilisateur"

            _dstUtilisateurs.Tables.Add(_strConnexion.ExecuteSql(Nothing, z_strSql))
            _bseUtilisateurs.DataMember = _dstUtilisateurs.Tables(0).TableName

        Catch ex As Exception
            MsgBox("Erreur d'application - frmUtilisateurs.frmUtilisateurs_Load" & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Erreur")
        Finally
            _strConnexion.CloseBDD()
        End Try

        ChargeDonnees()

        Cursor.Current = Cursors.Default
    End Sub
End Class
