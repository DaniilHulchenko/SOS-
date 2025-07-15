Public Class frmParamImpression

    <Category("Sos Medecin"), _
    DefaultValue(1), _
    Description("Contient le nobre de page maximum a imprimer")> _
    Property NombreDePageMaximum() As Decimal
        Get
            Return nudDe.Maximum
        End Get
        Set(ByVal value As Decimal)
            nudDe.Maximum = value
            nudA.Maximum = value
            nudA.Value = value
        End Set
    End Property

    Public Property NombreCopie() As Int32
        Get
            Return CType(nudCopie.Value, Int32)
        End Get
        Set(ByVal value As Int32)
            nudCopie.Value = value
        End Set
    End Property

    Public Property PageDebut() As Int32
        Get
            If rbnToute.Checked Then
                Return 0
            Else
                Return CType(nudDe.Value, Int32)
            End If
        End Get
        Set(ByVal value As Int32)
            nudDe.Value = value
        End Set
    End Property

    Public Property PageFin() As Int32
        Get
            If rbnToute.Checked Then
                Return 0
            Else
                Return CType(nudA.Value, Int32)
            End If
        End Get
        Set(ByVal value As Int32)
            nudA.Value = value
        End Set
    End Property

    Private Sub rbnSelection_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbnSelection.CheckedChanged
        If rbnSelection.Checked Then
            lblPage.Enabled = True
            nudDe.Enabled = True
            lblA.Enabled = True
            nudA.Enabled = True
        Else
            lblPage.Enabled = False
            nudDe.Enabled = False
            lblA.Enabled = False
            nudA.Enabled = False
        End If
    End Sub

    Private Sub btnImprimer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImprimer.Click
        DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnnuler.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub nudDe_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles nudDe.ValueChanged
        nudA.Minimum = nudDe.Value
    End Sub
End Class