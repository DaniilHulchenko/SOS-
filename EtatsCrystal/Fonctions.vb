Public Class Fonctions
    Shared Sub ChangeConnection(ByVal p_crpReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal p_objAccesDonnees As SosMedecins.Connexion.AccesDonnees)
        Dim z_crpTableConnexion As CrystalDecisions.Shared.TableLogOnInfo

        Dim z_crpConnexion As New CrystalDecisions.Shared.ConnectionInfo

        z_crpConnexion.ServerName = p_objAccesDonnees.ServerName
        'z_crpConnexion.DatabaseName = _gAccesDonnees.Database
        z_crpConnexion.IntegratedSecurity = False
        z_crpConnexion.UserID = p_objAccesDonnees.UserName
        z_crpConnexion.Password = p_objAccesDonnees.PassWord

        For Each z_crpTable As CrystalDecisions.CrystalReports.Engine.Table In p_crpReport.Database.Tables
            z_crpTableConnexion = z_crpTable.LogOnInfo
            z_crpTableConnexion.ConnectionInfo = z_crpConnexion
            z_crpTable.ApplyLogOnInfo(z_crpTableConnexion)
        Next

        For Each z_crpObjet As CrystalDecisions.CrystalReports.Engine.ReportDocument In p_crpReport.Subreports
            For Each z_crpTable As CrystalDecisions.CrystalReports.Engine.Table In p_crpReport.Subreports(z_crpObjet.Name).Database.Tables
                z_crpTableConnexion = z_crpTable.LogOnInfo
                z_crpTableConnexion.ConnectionInfo = z_crpConnexion
                z_crpTable.ApplyLogOnInfo(z_crpTableConnexion)
            Next
        Next
    End Sub

    Public Sub AffectDataSet(ByVal p_crpReport As CrystalDecisions.CrystalReports.Engine.ReportDocument, ByVal p_dstDataSet As DataSet)
        p_crpReport.SetDataSource(p_dstDataSet)

        For Each z_crpObjet As CrystalDecisions.CrystalReports.Engine.ReportDocument In p_crpReport.Subreports
            z_crpObjet.SetDataSource(p_dstDataSet)
        Next
    End Sub
End Class
