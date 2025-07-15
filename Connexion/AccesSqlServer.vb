Imports System.Data.Common
Imports System.Data.SqlClient

Public Class AccesSqlServer
    Implements AccesDonnees

#Region " Variables Privées "
    Private _trnTransaction As SqlClient.SqlTransaction = Nothing
#End Region

#Region " Propriétés Privées "
    ''' <summary>
    ''' Provider d'acces aux données
    ''' </summary>
    ''' 
    Private _cnnConnection As SqlClient.SqlConnection

    'Private _dbProviderFactory As DbProviderFactory
    'Public ReadOnly Property dbProviderFactory() As DbProviderFactory
    '    Get
    '        Return _dbProviderFactory
    '    End Get
    'End Property

    Public ReadOnly Property ErreurCleDuplique() As Int64 Implements AccesDonnees.ErreurCleDuplique
        Get
            Return 2627
        End Get
    End Property
#End Region

#Region " Propriétés Public "
    ''' <summary>
    ''' Chaine de connexion
    ''' </summary>
    Private _ConnectionString As String
    Public Property ConnectionString() As String Implements AccesDonnees.ConnectionString
        Get
            Return _ConnectionString
        End Get
        Set(ByVal value As String)
            _ConnectionString = value
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
            Dim z_str As String = _ConnectionString.Substring(_ConnectionString.IndexOf("User Id=") + "User Id=".Length)
            If z_str.Contains(";") Then
                z_str = z_str.Substring(0, z_str.IndexOf(";"))
            End If

            Return z_str
        End Get
    End Property

    Public ReadOnly Property PassWord() As String Implements AccesDonnees.PassWord
        Get
            Dim z_str As String = _ConnectionString.Substring(_ConnectionString.IndexOf("Password=") + "Password=".Length)
            If z_str.Contains(";") Then
                z_str = z_str.Substring(0, z_str.IndexOf(";"))
            End If

            Return z_str
        End Get
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
#End Region

#Region " Constructeur de la classe "
    Public Sub New(ByVal p_strConnectionStrings As String)
        ' Récupération de toutes les chaines de connexion
        _ConnectionString = p_strConnectionStrings
        Connection()
    End Sub
#End Region

#Region " Gestion de la connexion "
    ''' <summary>
    ''' Crée une connexion 
    ''' </summary>
    Public Sub Connection()
        Connection(_ConnectionString)
    End Sub
    ''' <summary>
    ''' Crée une connexion 
    ''' </summary>
    Public Sub Connection(ByVal p_strConnectionString As String)
        ' Création de la connection
        _cnnConnection = New SqlClient.SqlConnection(p_strConnectionString)
    End Sub

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
            End If
        Catch

        End Try
    End Sub
#End Region

#Region " Transaction : Ouvre et Termine une transaction "
    Public Sub BeginTransaction() Implements AccesDonnees.BeginTransaction
        If (_cnnConnection.State = System.Data.ConnectionState.Closed) Then
            _cnnConnection.Open()
        End If
        _trnTransaction = _cnnConnection.BeginTransaction()
    End Sub

    Public Sub Commit() Implements AccesDonnees.Commit
        _trnTransaction.Commit()
        _trnTransaction.Dispose()
        _cnnConnection.Close()
    End Sub

    Public Sub RollBack() Implements AccesDonnees.RollBack
        _trnTransaction.Rollback()
        _trnTransaction.Dispose()
        _cnnConnection.Close()
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
        Dim z_comCommand As DbCommand = New SqlCommand()

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

        If (_cnnConnection Is Nothing) Then
            Connection()
        End If

        p_comCommand.Connection = _cnnConnection
        If Not (_trnTransaction Is Nothing) Then
            p_comCommand.Transaction = _trnTransaction
        End If


        Dim z_Adapter As SqlDataAdapter = New SqlDataAdapter
        z_Adapter.AcceptChangesDuringFill = _blnAcceptChangesDuringFill

        z_Adapter.SelectCommand = DirectCast(p_comCommand, SqlCommand)

        z_Adapter.Fill(z_dtbDataTable)

        z_Adapter = Nothing
        ' retourne la valeur
        Return z_dtbDataTable
    End Function

    Public Function ExecuteScalar(ByVal p_strSql As String) As Object Implements AccesDonnees.ExecuteScalar

        Dim z_strValeurRetour As Object = Nothing

        Dim z_cmdSqlCmd As SqlCommand

        If _trnTransaction Is Nothing Then
            z_cmdSqlCmd = New SqlCommand(p_strSql, _cnnConnection)
        Else
            z_cmdSqlCmd = New SqlCommand(p_strSql, _cnnConnection, _trnTransaction)
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
        Dim z_comCommand As DbCommand = New SqlCommand()

        z_comCommand.CommandText = p_strSql
        z_comCommand.CommandType = CommandType.Text

        ExecuteSansRetour(z_comCommand)
    End Sub

    Private Sub ExecuteSansRetour(ByVal p_comCommand As DbCommand) Implements AccesDonnees.ExecuteSansRetour
        Dim z_blnOpenLocal As Boolean = False
        If (_cnnConnection Is Nothing) Then
            Connection()
        End If

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

    Public Function AddCommand(ByVal p_strTextCommand As String) As DbCommand Implements AccesDonnees.AddCommand
        'Fabrique la commande
        Dim z_comCommand As DbCommand = New SqlCommand
        z_comCommand.CommandText = p_strTextCommand
        z_comCommand.Connection = _cnnConnection
        'Retourne la !!!
        Return z_comCommand
    End Function

    Public Function AddParametres(ByVal Nom As String, ByVal type As DbType) As DbParameter Implements AccesDonnees.AddParametres
        Dim z_prmParametre As DbParameter = New SqlParameter

        z_prmParametre.ParameterName = Nom
        z_prmParametre.DbType = type
        z_prmParametre.Value = DBNull.Value

        Return z_prmParametre
    End Function

    Public Function NumeroErreur(ByVal ex As Exception) As Int64 Implements AccesDonnees.NumeroErreur

        Dim z_lng As Int64 = 0
        If (ex.GetType Is GetType(SqlClient.SqlException)) Then
            Dim z_exd As SqlClient.SqlException = CType(ex, SqlClient.SqlException)
            z_lng = z_exd.Number
        End If

        Return z_lng
    End Function

End Class

