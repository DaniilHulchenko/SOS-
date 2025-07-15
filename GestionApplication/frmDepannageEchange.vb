Public Class frmDepannageEchange


    Public ReadOnly Property Debut() As String
        Get
            Return txtDebut.Text
        End Get
    End Property

    Public ReadOnly Property Fin() As String
        Get
            Return txtFin.Text
        End Get
    End Property

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnExecuter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExecuter.Click
        If Not (txtMotDePasse.Text = "prazine") Then
            MsgBox("Erreur de mot de passe !", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Erreur d'acces")
        Else
            Me.DialogResult = DialogResult.OK

        End If
    End Sub

End Class