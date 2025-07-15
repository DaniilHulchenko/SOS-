Imports System.IO

Public Class frmLogIn
    Inherits System.Windows.Forms.Form

    Private _strConnexion As AccesDonnees
    Public Property Connexion() As AccesDonnees
        Get
            Return _strConnexion
        End Get
        Set(ByVal Value As AccesDonnees)
            _strConnexion = Value
        End Set
    End Property

    Private _strSelection As String
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents btValider As System.Windows.Forms.Button
    Private WithEvents btAnnuler As System.Windows.Forms.Button
    Public Property Selection() As String
        Get
            Return _strSelection
        End Get
        Set(ByVal Value As String)
            _strSelection = Value
        End Set
    End Property

    Private _drwDonneesRetour As DataRow
    Public Property DonneesRetour() As DataRow
        Get
            Return _drwDonneesRetour
        End Get
        Set(ByVal Value As DataRow)
            _drwDonneesRetour = Value
        End Set
    End Property

#Region " Code généré par le Concepteur Windows Form "

    Public Sub New()
        MyBase.New()

        'Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        'Ajoutez une initialisation quelconque après l'appel InitializeComponent()

    End Sub

    'La méthode substituée Dispose du formulaire pour nettoyer la liste des composants.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requis par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée en utilisant le Concepteur Windows Form.  
    'Ne la modifiez pas en utilisant l'éditeur de code.
    Friend WithEvents lblPassWord As System.Windows.Forms.Label
    Friend WithEvents LblUser As System.Windows.Forms.Label
    Friend WithEvents grpLogIn As System.Windows.Forms.GroupBox
    Friend WithEvents txtPassWord As System.Windows.Forms.TextBox
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogIn))
        Me.grpLogIn = New System.Windows.Forms.GroupBox()
        Me.lblPassWord = New System.Windows.Forms.Label()
        Me.LblUser = New System.Windows.Forms.Label()
        Me.txtPassWord = New System.Windows.Forms.TextBox()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btAnnuler = New System.Windows.Forms.Button()
        Me.btValider = New System.Windows.Forms.Button()
        Me.grpLogIn.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpLogIn
        '
        Me.grpLogIn.Controls.Add(Me.lblPassWord)
        Me.grpLogIn.Controls.Add(Me.LblUser)
        Me.grpLogIn.Controls.Add(Me.txtPassWord)
        Me.grpLogIn.Controls.Add(Me.txtUser)
        Me.grpLogIn.Location = New System.Drawing.Point(55, 32)
        Me.grpLogIn.Name = "grpLogIn"
        Me.grpLogIn.Size = New System.Drawing.Size(272, 96)
        Me.grpLogIn.TabIndex = 0
        Me.grpLogIn.TabStop = False
        '
        'lblPassWord
        '
        Me.lblPassWord.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPassWord.Location = New System.Drawing.Point(14, 59)
        Me.lblPassWord.Name = "lblPassWord"
        Me.lblPassWord.Size = New System.Drawing.Size(98, 14)
        Me.lblPassWord.TabIndex = 2
        Me.lblPassWord.Text = "Mot de passe :"
        '
        'LblUser
        '
        Me.LblUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUser.Location = New System.Drawing.Point(14, 26)
        Me.LblUser.Name = "LblUser"
        Me.LblUser.Size = New System.Drawing.Size(117, 21)
        Me.LblUser.TabIndex = 0
        Me.LblUser.Text = "Nom d'utilisateur :"
        '
        'txtPassWord
        '
        Me.txtPassWord.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassWord.Location = New System.Drawing.Point(135, 56)
        Me.txtPassWord.Name = "txtPassWord"
        Me.txtPassWord.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassWord.Size = New System.Drawing.Size(122, 22)
        Me.txtPassWord.TabIndex = 3
        '
        'txtUser
        '
        Me.txtUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUser.Location = New System.Drawing.Point(137, 23)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(122, 22)
        Me.txtUser.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(89, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = " "
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(128, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(159, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Version du 17.09.2012 "
        '
        'btAnnuler
        '
        Me.btAnnuler.BackColor = System.Drawing.Color.Transparent
        Me.btAnnuler.BackgroundImage = Global.SosMedecins.Connexion.My.Resources.Resources.bondCancel
        Me.btAnnuler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btAnnuler.FlatAppearance.BorderSize = 0
        Me.btAnnuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btAnnuler.ImageIndex = 4
        Me.btAnnuler.Location = New System.Drawing.Point(243, 147)
        Me.btAnnuler.Name = "btAnnuler"
        Me.btAnnuler.Size = New System.Drawing.Size(60, 60)
        Me.btAnnuler.TabIndex = 54
        Me.btAnnuler.UseVisualStyleBackColor = False
        '
        'btValider
        '
        Me.btValider.BackColor = System.Drawing.Color.Transparent
        Me.btValider.BackgroundImage = Global.SosMedecins.Connexion.My.Resources.Resources.brondValider
        Me.btValider.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btValider.FlatAppearance.BorderSize = 0
        Me.btValider.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btValider.ImageIndex = 2
        Me.btValider.Location = New System.Drawing.Point(92, 147)
        Me.btValider.Name = "btValider"
        Me.btValider.Size = New System.Drawing.Size(60, 60)
        Me.btValider.TabIndex = 53
        Me.btValider.UseVisualStyleBackColor = False
        '
        'frmLogIn
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(398, 223)
        Me.ControlBox = False
        Me.Controls.Add(Me.btAnnuler)
        Me.Controls.Add(Me.btValider)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grpLogIn)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLogIn"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SmartRapport V5"
        Me.grpLogIn.ResumeLayout(False)
        Me.grpLogIn.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region


    Private Function VerificationLogIn() As Boolean
        _strConnexion.OpenBDD()
        Try
            Dim z_strSql As String = _strSelection.Replace("%1", FormatSql.Format_String(txtUser.Text))
            z_strSql = z_strSql.Replace("%2", FormatSql.Format_String(txtPassWord.Text))

            Dim z_drr As DataTable = _strConnexion.ExecuteSql(Nothing, z_strSql)

            If (z_drr.Rows.Count > 0) Then

                _drwDonneesRetour = z_drr.Rows(0)

                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox("Erreur d'application - frmlogIn.VerificationLogIn" & vbCrLf & vbCrLf & ex.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Erreur")
        Finally
            _strConnexion.CloseBDD()
        End Try
    End Function



    Private Sub frmLogIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim exepath As String = Environment.GetCommandLineArgs()(0)

        Label2.Text = "Version du " & File.GetLastWriteTime(exepath).ToShortDateString()

        Console.WriteLine(File.GetLastWriteTime(exepath).ToShortDateString())

    End Sub

   
    Private Sub btValider_Click(sender As Object, e As EventArgs) Handles btValider.Click
        If (VerificationLogIn()) Then
            Me.DialogResult = DialogResult.Yes
        Else
            MsgBox("Nom d'utilisateur ou mot de passe incorrect !", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Erreur")
        End If
    End Sub

    Private Sub btAnnuler_Click(sender As Object, e As EventArgs) Handles btAnnuler.Click
        Me.DialogResult = DialogResult.Cancel
    End Sub

    
    Private Sub txtPassWord_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassWord.KeyDown
        If e.KeyCode = Keys.Enter Then
            btValider_Click(sender, e)
        End If
    End Sub
End Class
