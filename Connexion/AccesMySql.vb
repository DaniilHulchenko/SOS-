Imports System.Data.Common
#Disable Warning BC40056 ' L'espace de noms ou le type spécifié dans les Imports 'MySql.Data' ne contient aucun membre public ou est introuvable. Vérifiez que l'espace de noms ou le type est défini et qu'il contient au moins un membre public. Vérifiez que le nom de l'élément importé n'utilise pas d'autres alias.
Imports MySql.Data
#Enable Warning BC40056 ' L'espace de noms ou le type spécifié dans les Imports 'MySql.Data' ne contient aucun membre public ou est introuvable. Vérifiez que l'espace de noms ou le type est défini et qu'il contient au moins un membre public. Vérifiez que le nom de l'élément importé n'utilise pas d'autres alias.

Public Class AccessMySql
    Implements AccesDonnees

#Region " Propriete "

#Disable Warning BC30002 ' Le type 'MySqlClient.MySqlConnection' n'est pas défini.
    Private _cnnConnection As MySqlClient.MySqlConnection
#Enable Warning BC30002 ' Le type 'MySqlClient.MySqlConnection' n'est pas défini.
#Disable Warning BC30002 ' Le type 'MySqlClient.MySqlTransaction' n'est pas défini.
    Private _trnTransaction As MySqlClient.MySqlTransaction
#Enable Warning BC30002 ' Le type 'MySqlClient.MySqlTransaction' n'est pas défini.

    Public ReadOnly Property ErreurCleDuplique() As Int64 Implements AccesDonnees.ErreurCleDuplique
        Get
            Return 2627
        End Get
    End Property

    Private _strConnectionString As String
    Public Property ConnectionString() As String Implements AccesDonnees.ConnectionString
        Get
            Return _strConnectionString
        End Get
        Set(ByVal Value As String)
            _strConnectionString = Value
        End Set
    End Property

    Private _blnAcceptChangesDuringFill As Boolean = True
    Property AcceptChangesDuringFill() As Boolean Implements AccesDonnees.AcceptChangesDuringFill
        Get
            Return _blnAcceptChangesDuringFill
        End Get
        Set(ByVal Value As Boolean)
            _blnAcceptChangesDuringFill = Value
        End Set
    End Property

    Public ReadOnly Property ServerName() As String Implements AccesDonnees.ServerName
        Get
            Return _cnnConnection.DataSource
        End Get
    End Property

    Public ReadOnly Property Database() As String Implements AccesDonnees.Database
        Get
            Return _cnnConnection.Database
        End Get
    End Property

    Public ReadOnly Property UserName() As String Implements AccesDonnees.UserName
        Get
            Return "Non implémenté"
        End Get
    End Property

    Public ReadOnly Property PassWord() As String Implements AccesDonnees.PassWord
        Get
            Return "Non implémenté"
        End Get
    End Property
#End Region

#Region " Construction "
    Public Sub New(ByVal p_strConnectionString As String)
        _strConnectionString = p_strConnectionString
        _cnnConnection = New MySqlClient.MySqlConnection(p_strConnectionString)
    End Sub

    Protected Overrides Sub Finalize()
        _cnnConnection = Nothing
        MyBase.Finalize()
    End Sub
#End Region

#Region " Gestion connexion "         '-----------------------------------------------------------------------
    Public Function OpenBDD() As Boolean Implements AccesDonnees.OpenBDD
        If Not (_cnnConnection.State = ConnectionState.Open) Then
            _cnnConnection.Open()
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub CloseBDD() Implements AccesDonnees.CloseBDD
        Try
            If _cnnConnection.State = ConnectionState.Open Then
                _cnnConnection.Close()
                _cnnConnection.Dispose()
            End If
        Catch

        End Try
    End Sub

    Public Sub BeginTransaction() Implements AccesDonnees.BeginTransaction
        _trnTransaction = _cnnConnection.BeginTransaction()
    End Sub

    Public Sub Commit() Implements AccesDonnees.Commit
        _trnTransaction.Commit()
        _trnTransaction = Nothing
    End Sub

    Public Sub RollBack() Implements AccesDonnees.RollBack
        _trnTransaction.Rollback()
        _trnTransaction = Nothing
    End Sub
#End Region

#Region " Commandes avec retour "
    ''' <summary>Execute la procedure stockée passée dans la commande.
    ''' <param name='p_Command'>Commande à executer.</param>
    ''' </summary>
    Public Function ExecuteProcedureStockee(ByVal p_dtbTable As DataTable, ByVal p_comCommand As DbCommand) As DataTable Implements AccesDonnees.ExecuteProcedureStockee
        p_comCommand.CommandType = CommandType.StoredProcedure

        Return Execute(p_dtbTable, p_comCommand)
    End Function

    ''' <summary>Execute la commande.
    ''' <param name='p_Command'>Commande à executer.</param>
    ''' </summary>
    Public Function ExecuteSql(ByVal p_dtbTable As DataTable, ByVal p_comCommand As DbCommand) As DataTable Implements AccesDonnees.ExecuteSql
        p_comCommand.CommandType = CommandType.Text

        Return Execute(p_dtbTable, p_comCommand)
    End Function

    ''' <summary>Execute la commande.
    ''' <param name='p_Command'>Commande à executer.</param>
    ''' </summary>
    Public Function ExecuteSql(ByVal p_dtbTable As DataTable, ByVal p_strSql As String) As DataTable Implements AccesDonnees.ExecuteSql
        Dim z_comCommand As DbCommand = New MySql.Data.MySqlClient.MySqlCommand()

        z_comCommand.CommandText = p_strSql
        z_comCommand.CommandType = CommandType.Text

        Return Execute(p_dtbTable, z_comCommand)
    End Function

    ''' <summary>
    ''' Execute la commande passée.
    ''' </summary>
    ''' <param name="p_comCommand">Commande à executer.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Execute(ByVal p_dtbTable As DataTable, ByVal p_comCommand As DbCommand) As DataTable Implements AccesDonnees.Execute
        Dim z_dtbDataTable As DataTable
        If (p_dtbTable Is Nothing) Then
            z_dtbDataTable = New DataTable
        Else
            z_dtbDataTable = p_dtbTable
        End If

        p_comCommand.Connection = _cnnConnection
        If Not (_trnTransaction Is Nothing) Then
            p_comCommand.Transaction = _trnTransaction
        End If

        Dim z_Adapter As MySqlClient.MySqlDataAdapter = New MySqlClient.MySqlDataAdapter
        z_Adapter.AcceptChangesDuringFill = _blnAcceptChangesDuringFill

        z_Adapter.SelectCommand = DirectCast(p_comCommand, MySqlClient.MySqlCommand)

        z_Adapter.Fill(z_dtbDataTable)

        z_Adapter = Nothing
        ' retourne la valeur
        Return z_dtbDataTable
    End Function

    Public Function ExecuteScalar(ByVal p_strSql As String) As Object Implements AccesDonnees.ExecuteScalar

        Dim z_strValeurRetour As Object = Nothing

        Dim z_cmdSqlCmd As MySqlClient.MySqlCommand

        If _trnTransaction Is Nothing Then
            z_cmdSqlCmd = New MySqlClient.MySqlCommand(p_strSql, _cnnConnection)
        Else
            z_cmdSqlCmd = New MySqlClient.MySqlCommand(p_strSql, _cnnConnection, _trnTransaction)
        End If

        '--- Excution requete
        z_strValeurRetour = z_cmdSqlCmd.ExecuteScalar()

        z_cmdSqlCmd.Dispose()
        z_cmdSqlCmd = Nothing

        Return z_strValeurRetour
    End Function
#End Region

#Region " Commandes Sans retour "
    Public Sub ExecuteProcedureStockeeSansRetour(ByVal p_comCommand As DbCommand) Implements AccesDonnees.ExecuteProcedureStockeeSansRetour
        p_comCommand.CommandType = CommandType.StoredProcedure
        ExecuteSansRetour(p_comCommand)
    End Sub

    Public Sub ExecuteSqlSansRetour(ByVal p_comCommand As DbCommand) Implements AccesDonnees.ExecuteSqlSansRetour
        p_comCommand.CommandType = CommandType.Text
        ExecuteSansRetour(p_comCommand)
    End Sub

    Public Sub ExecuteSqlSansRetour(ByVal p_strSql As String) Implements AccesDonnees.ExecuteSqlSansRetour
        Dim z_comCommand As DbCommand = New MySqlClient.MySqlCommand()

        z_comCommand.CommandText = p_strSql
        z_comCommand.CommandType = CommandType.Text

        ExecuteSansRetour(z_comCommand)
    End Sub

    Private Sub ExecuteSansRetour(ByVal p_comCommand As DbCommand) Implements AccesDonnees.ExecuteSansRetour
        Dim z_blnOpenLocal As Boolean = False

        If Not (_trnTransaction Is Nothing) Then
            p_comCommand.Transaction = _trnTransaction
        End If

        Try
            p_comCommand.Connection = _cnnConnection
            If (_cnnConnection.State = System.Data.ConnectionState.Closed) Then
                z_blnOpenLocal = True
                _cnnConnection.Open()
            End If
            p_comCommand.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        Finally
            If (z_blnOpenLocal And _cnnConnection.State <> System.Data.ConnectionState.Closed) Then
                _cnnConnection.Close()
            End If
        End Try
    End Sub
#End Region

#Region " Accés -Mise à jour données "         '-----------------------------------------------------------------------

    '--- Retourne un DataReader aux projets appelant
    'Public Function RetourReader(ByVal p_Sql As String) As System.Data.IDataReader Implements AccesDonnees.RetourReader
    '    '--- Pas de gestion d'ereur. L'erreur sera gérée dans l'appelant.
    '    '    Permet d'avoir l'origine d'appel de l'erreur dans les traces

    '    Dim z_drrRead As MySqlClient.MySqlDataReader = Nothing
    '    Dim z_cmdSqlCmd As MySqlClient.MySqlCommand

    '    '--- Création commande requête
    '    If _trn Is Nothing Then
    '        z_cmdSqlCmd = New MySqlClient.MySqlCommand(p_Sql, _cnx)
    '    Else
    '        z_cmdSqlCmd = New MySqlClient.MySqlCommand(p_Sql, _cnx, _trn)
    '    End If

    '    '--- Excution requete
    '    z_drrRead = z_cmdSqlCmd.ExecuteReader

    '    z_cmdSqlCmd.Dispose()
    '    z_cmdSqlCmd = Nothing

    '    Return z_drrRead
    'End Function

    '--- Retourne un DataSet aux projets appelant
    'Public Function RetourDataset(ByVal p_strTable As String, ByVal p_strSql As String, ByRef p_dts As DataSet) As DataSet Implements AccesDonnees.RetourDataset

    '    '--- Pas de gestion d'ereur. L'erreur sera gérée dans l'appelant.
    '    '    Permet d'avoir l'origine d'appel de l'erreur dans les traces
    '    Dim z_dts As DataSet = Nothing

    '    '--- On regarde si on un dataset ds lequel on veut ajouter une table ou si création
    '    If p_dts Is Nothing Then
    '        z_dts = New DataSet
    '    Else
    '        z_dts = p_dts
    '    End If

    '    '--- Création commande requête
    '    Dim z_dar As New MySqlClient.MySqlDataAdapter

    '    z_dar.AcceptChangesDuringFill = _blnAcceptChangesDuringFill

    '    If _trn Is Nothing Then
    '        z_dar.SelectCommand = New MySqlClient.MySqlCommand(p_strSql, _cnnConnection)
    '    Else
    '        z_dar.SelectCommand = New MySqlClient.MySqlCommand(p_strSql, _cnnConnection, _trnTransaction)
    '    End If

    '    '--- Excution requete
    '    z_dar.Fill(z_dts, p_strTable)

    '    z_dar.Dispose()
    '    z_dar = Nothing

    '    Return z_dts

    'End Function

    'Public Sub ExecuteSqlSansRetour(ByVal p_strSql As String) Implements AccesDonnees.ExecuteSqlSansRetour
    '    '--- Pas de gestion d'ereur. L'erreur sera gérée dans l'appelant.
    '    '    Permet d'avoir l'origine d'appel de l'erreur dans les traces

    '    Dim z_cmdSqlCmd As MySqlClient.MySqlCommand

    '    Try
    '        '--- Création commande requête
    '        If _trnTransaction Is Nothing Then
    '            z_cmdSqlCmd = New MySqlClient.MySqlCommand(p_strSql, _cnnConnection)
    '        Else
    '            z_cmdSqlCmd = New MySqlClient.MySqlCommand(p_strSql, _cnnConnection, _trnTransaction)
    '        End If

    '        '--- Exécution
    '        z_cmdSqlCmd.ExecuteNonQuery()

    '        z_cmdSqlCmd.Dispose()
    '        z_cmdSqlCmd = Nothing

    '    Catch ex As Exception
    '        MsgBox("Erreur en exécution de requête." & vbCrLf & vbCrLf & "La mise à jour des données n'a pas été réalisée" & vbCrLf & vbCrLf & "Pour plus d'informations sur cette erreur consulter le message suivant.", MsgBoxStyle.OkOnly Or MsgBoxStyle.Critical, "Erreur Acces Base de données")
    '        '--- 
    '        Throw ex
    '    End Try
    'End Sub
#End Region

    Public Function AddCommand(ByVal p_strTextCommand As String) As DbCommand Implements AccesDonnees.AddCommand
        'Fabrique la commande
        Throw New Exception("Fonctionnalité non implémentée")
        'Retourne la !!!
        Return Nothing
    End Function

    Public Function AddParametres(ByVal Nom As String, ByVal type As DbType) As DbParameter Implements AccesDonnees.AddParametres
        Throw New Exception("Fonctionnalité non implémentée")

        Return Nothing
    End Function

    Public Function NumeroErreur(ByVal ex As Exception) As Int64 Implements AccesDonnees.NumeroErreur

        Dim z_lng As Int64 = 0
        If (ex.GetType Is GetType(MySqlClient.MySqlException)) Then
            Dim z_exd As MySqlClient.MySqlException = CType(ex, MySqlClient.MySqlException)
            z_lng = z_exd.Number
        End If

        Return z_lng

    End Function
End Class
