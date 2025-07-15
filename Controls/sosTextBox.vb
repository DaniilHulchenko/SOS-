<ToolboxBitmapAttribute(GetType(TextBox))> _
Public Class sosTextBox
    Inherits TextBox

    '---Récupération du caractère de séaration des décimales
    Private _SeparateurDecimales As Char = CType(Application.CurrentCulture.NumberFormat.NumberDecimalSeparator, Char)

    Public Shadows Enum TypeAffichage As Int32
        Normal
        Majuscule
        Minuscule
        PremiereLettreMajuscule
    End Enum

    <System.ComponentModel.Browsable(False), _
    DefaultValue(GetType(TypeAffichage), "Normal")> _
    Public Overloads Property CharacterCasing() As TypeAffichage
        Get
        End Get
        Set(ByVal value As TypeAffichage)
        End Set
    End Property

    Private _Affichage As TypeAffichage = TypeAffichage.Normal
    <Category("Sos Medecins"), _
    DefaultValue(GetType(TypeAffichage), "Normal"), _
    Description("Indique le type d'affichage de la donnée saisie")> _
    Public Property Affichage() As TypeAffichage
        Get
            Return _Affichage
        End Get
        Set(ByVal value As TypeAffichage)
            Select Case value
                Case TypeAffichage.Normal, TypeAffichage.PremiereLettreMajuscule
                    MyBase.CharacterCasing = Windows.Forms.CharacterCasing.Normal
                Case TypeAffichage.Majuscule
                    MyBase.CharacterCasing = Windows.Forms.CharacterCasing.Upper
                Case TypeAffichage.Minuscule
                    MyBase.CharacterCasing = Windows.Forms.CharacterCasing.Lower
            End Select

            _Affichage = value
        End Set
    End Property

    ''' <summary>
    ''' Type de la donnée saisie
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TypeSaisie As Int32
        Normal
        SaisieAlpha
        SaisieAlphaNumerique
        SaisieEntier
        SaisieDecimal
    End Enum

    Private _Saisie As TypeSaisie = TypeSaisie.Normal
    ''' <summary>
    ''' Indique le type de la donnée saisie
    ''' </summary>
    ''' <remarks></remarks>
    <Category("Sos Medecins"), _
    DefaultValue(GetType(TypeSaisie), "Normal"), _
    Description("Indique le type de la donnée saisie")> _
    Public Property Saisie() As TypeSaisie
        Get
            Return _Saisie
        End Get
        Set(ByVal value As TypeSaisie)
            _Saisie = value
        End Set
    End Property

    Private _PartieEntiere As Int32 = 0
    ''' <summary>
    ''' Nombre maximum de chiffres de la partie entière
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Sos Medecins"), _
    DefaultValue(0), _
    Description("Définit le nombre de chiffres de la partie entière")> _
    Public Property PartieEntiere() As Int32
        Get
            Return _PartieEntiere
        End Get
        Set(ByVal value As Int32)
            _PartieEntiere = value
        End Set
    End Property

    Private _PartieDecimale As Int32 = 0
    ''' <summary>
    ''' Nombre maximum de chiffres de la partie décimale
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("Sos Medecins"), _
    DefaultValue(0), _
    Description("Définit le nombre de chiffres de la partie décimale")> _
    Public Property PartieDecimale() As Int32
        Get
            Return _PartieDecimale
        End Get
        Set(ByVal value As Int32)
            _PartieDecimale = value
        End Set
    End Property

    Private _Espace As Boolean = True
    ''' <summary>
    ''' Indique si la saisie de l'espace est autorisée
    ''' </summary>
    ''' <remarks></remarks>
    <Category("Sos Medecins"), _
    DefaultValue(True), _
    Description("Indique si la saisie de l'espace est autorisée")> _
    Public Property Espace() As Boolean
        Get
            Return _Espace
        End Get
        Set(ByVal value As Boolean)
            _Espace = value
        End Set
    End Property

    Private _Negatif As Boolean = False
    ''' <summary>
    ''' Indique si la saisie de l'espace est autorisée
    ''' </summary>
    ''' <remarks></remarks>
    <Category("Sos Medecins"), _
    DefaultValue(False), _
    Description("Indique si une valeur négative est autorisée")> _
    Public Property Negatif() As Boolean
        Get
            Return _Negatif
        End Get
        Set(ByVal value As Boolean)
            _Negatif = value
        End Set
    End Property

    Protected Overrides Sub onKeyPress(ByVal e As KeyPressEventArgs)

        '--- Permet de passer aux champs de saisies suivant
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) And Me.Multiline = False Then
            Me.FindForm.SelectNextControl(Me, True, True, True, True)
            Exit Sub
        End If

        Select Case _Saisie
            Case TypeSaisie.SaisieEntier '---Entier
                Select Case Asc(e.KeyChar)
                    Case 48 To 57 '---[0-9]
                    Case 8
                        Exit Sub
                    Case Else
                        e.Handled = True
                End Select
            Case TypeSaisie.Normal
                Select Case _Affichage
                    Case TypeAffichage.PremiereLettreMajuscule
                        If (Me.SelectionStart = 0) Then
                            If (Me.SelectionLength = 0) Then
                                Me.Text = Me.Text.ToLower()
                            End If
                            e.KeyChar = Char.ToUpper(e.KeyChar)
                        End If
                End Select

            Case TypeSaisie.SaisieAlphaNumerique '---Alphanumérique
                Select Case Asc(e.KeyChar)
                    Case 97 To 122, 65 To 90, 48 To 57 '---[a-z],[A-Z],[0-9]
                        Select Case _Affichage
                            Case TypeAffichage.PremiereLettreMajuscule
                                If (Me.SelectionStart = 0) Then
                                    If (Me.SelectionLength = 0) Then
                                        Me.Text = Me.Text.ToLower()
                                    End If
                                    e.KeyChar = Char.ToUpper(e.KeyChar)
                                End If
                        End Select
                    Case 32 '---Espace
                        If Not _Espace Then
                            ' si l'espace non autorisée
                            e.Handled = True
                        End If
                    Case 8 '---Touche "Backspace"
                        Exit Sub
                    Case Else
                        e.Handled = True
                End Select

            Case TypeSaisie.SaisieAlpha '---Alpha
                Select Case Asc(e.KeyChar)
                    Case 97 To 122, 65 To 90 '---[a-z],[A-Z]
                        Select Case _Affichage
                            Case TypeAffichage.PremiereLettreMajuscule
                                If (Me.SelectionStart = 0) Then
                                    If (Me.SelectionLength = 0) Then
                                        Me.Text = Me.Text.ToLower()
                                    End If
                                    e.KeyChar = Char.ToUpper(e.KeyChar)
                                End If
                        End Select
                    Case 32 '---Espace
                        If Not _Espace Then
                            ' si l'espace non autorisée
                            e.Handled = True
                        End If
                    Case 8 '---Touche "Backspace"
                        Exit Sub
                    Case Else
                        e.Handled = True
                End Select

            Case TypeSaisie.SaisieDecimal '---Décimal
                Select Case Asc(e.KeyChar)
                    Case Asc(_SeparateurDecimales) ' [séparateur décimales]
                        If (Me.Text.LastIndexOf(_SeparateurDecimales) >= 0) Then
                            e.Handled = True
                        End If
                    Case 48 To 57 '---[0-9]
                        Dim z_intPosPoint As Int32 = Me.Text.LastIndexOf(_SeparateurDecimales)
                        If (Me.SelectionStart > z_intPosPoint) And z_intPosPoint >= 0 Then
                            ' on est apres le point
                            If (Me.Text.Length - Me.Text.Trim.IndexOf(_SeparateurDecimales)) > _PartieDecimale Then
                                e.Handled = True
                            End If
                        Else
                            If (z_intPosPoint < 0) Then
                                ' il n'y a pas de point
                                If Me.Text.Length >= _PartieEntiere And Me.SelectionLength <= 0 Then
                                    e.Handled = True
                                End If
                            Else
                                ' on est avant le point
                                If (Me.SelectionLength = 0) Then
                                    If z_intPosPoint >= _PartieEntiere Then
                                        e.Handled = True
                                    End If
                                End If
                            End If
                        End If
                    Case 8 '---Touche "Backspace"
                        If (Me.SelectionStart > 0) Then
                            Dim z_intPosPoint As Int32 = Me.Text.LastIndexOf(_SeparateurDecimales)

                            If Me.SelectionStart - 1 = z_intPosPoint Then
                                Me.Text = Me.Text.Substring(0, z_intPosPoint)
                                Me.SelectionStart = Me.Text.Length
                                e.Handled = True
                            End If
                        End If
                    Case 45 '--- touche "-"
                        If (Me.SelectionStart <> 0) Then
                            e.Handled = True
                        End If
                    Case Else
                        e.Handled = True
                End Select
        End Select
    End Sub

    Private Sub TextBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case _Saisie
            Case TypeSaisie.SaisieEntier '---Entier
            Case TypeSaisie.SaisieAlphaNumerique '---Alphanumérique
            Case TypeSaisie.SaisieAlpha '---Alpha
            Case TypeSaisie.SaisieDecimal '---Décimal
                Select Case e.KeyCode
                    Case Keys.Delete
                        If (Me.SelectionStart > 0) Then
                            Dim z_intPosPoint As Int32 = Me.Text.LastIndexOf(_SeparateurDecimales)

                            If Me.SelectionStart = z_intPosPoint Then
                                Me.Text = Me.Text.Substring(0, z_intPosPoint)
                                Me.SelectionStart = Me.Text.Length
                                e.Handled = True
                            End If
                        End If
                End Select
        End Select
    End Sub
End Class
