Friend Class frmParamImprimante

    Private _PrinterName As String = ""
    Public Property PrinterName() As String
        Get
            Return _PrinterName
        End Get
        Set(ByVal value As String)
            _PrinterName = value
            If (_PrinterName.Length > 0) Then
                gcbxImprimantes.SelectedItem = _PrinterName
            Else
                Dim z_prnDefault As New System.Drawing.Printing.PrinterSettings
                _PrinterName = z_prnDefault.PrinterName
                gcbxImprimantes.SelectedItem = _PrinterName
            End If
        End Set
    End Property

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        For Each z_Imp As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters

            ' System.Drawing.Printing.PrintAction()

            gcbxImprimantes.Items.Add(z_Imp)
        Next
    End Sub

    Private Sub gcmdAnnuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gcmdAnnuler.Click
        DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub gcmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gcmdOk.Click
        _PrinterName = gcbxImprimantes.SelectedItem.ToString


        DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class
