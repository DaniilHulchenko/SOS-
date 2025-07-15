Imports System.Data.Common
Imports System.Transactions

Public Class AccesGeneric
    Implements AccesDonnees

#Region " Propriete "

    Private _cnnConnection As DbConnection
    Private _trnTransaction As TransactionScope
    Private _dbProviderFactory As DbProviderFactory

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
    Public Sub New(ByVal p_strConnectionStrings As String, ByVal p_strProviderName As String)
        ' Récupération de toutes les chaines de connexion
        _strConnectionString = p_strConnectionStrings

        ' Création de la connection
        _cnnConnection = _dbProviderFactory.CreateConnection()
        _cnnConnection.ConnectionString = _strConnectionString

        ' Récupération de la classe fabrique suivant le provider
        _dbProviderFactory = DbProviderFactories.GetFactory(p_strProviderName)
    End Sub

    Protected Overrides Sub Finalize()
        _cnnConnection = Nothing
        MyBase.Finalize()
    End Sub
#End Region

#Region " Gestion connexion "         '-----------------------------------------------------------------------
    Public Function OpenBDD() As Boolean Implements AccesDonnees.OpenBDD
        Try

            If Not (_cnnConnection.State = ConnectionState.Open) Then
                _cnnConnection.Open()
            End If

        Catch eexception As Exception
            MsgBox("La base de données de l'application n'a pu être ouverte pour la raison suivante :" & vbCrLf & vbCrLf & eexception.Message, MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly, "Erreur Connection")
            Return False
        End Try

        Return True
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
        _trnTransaction = New TransactionScope()
    End Sub

    Public Sub Commit() Implements AccesDonnees.Commit
        _trnTransaction.Complete()
        _trnTransaction.Dispose()
        _trnTransaction = Nothing
    End Sub

    Public Sub RollBack() Implements AccesDonnees.RollBack
        _trnTransaction.Dispose()
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

        Dim z_Adapter As DbDataAdapter = _dbProviderFactory.CreateDataAdapter()
        z_Adapter.SelectCommand = p_comCommand

        z_Adapter.Fill(z_dtbDataTable)

        z_Adapter = Nothing
        ' retourne la valeur
        Return z_dtbDataTable
    End Function

    Public Function ExecuteScalar(ByVal p_strSql As String) As Object Implements AccesDonnees.ExecuteScalar

        Dim z_strValeurRetour As Object = Nothing

        Dim z_cmdSqlCmd As DbCommand = AddCommand(p_strSql)

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
        Dim z_comCommand As DbCommand = AddCommand(p_strSql)

        z_comCommand.CommandType = CommandType.Text

        ExecuteSansRetour(z_comCommand)
    End Sub

    Private Sub ExecuteSansRetour(ByVal p_comCommand As DbCommand) Implements AccesDonnees.ExecuteSansRetour
        Dim z_blnOpenLocal As Boolean = False

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
        Dim z_comCommand As DbCommand = _dbProviderFactory.CreateCommand()
        z_comCommand.CommandText = p_strTextCommand
        z_comCommand.Connection = _cnnConnection
        'Retourne la !!!
        Return z_comCommand
    End Function

    Public Function AddParametres(ByVal Nom As String, ByVal type As DbType) As DbParameter Implements AccesDonnees.AddParametres
        Throw New Exception("Fonctionnalité non implémentée")

        Return Nothing
    End Function

    Public Function NumeroErreur(ByVal ex As Exception) As Int64 Implements AccesDonnees.NumeroErreur

        Dim z_lng As Int64 = 0
        If (_dbProviderFactory.GetType() Is GetType(SqlClient.SqlClientFactory)) Then
            Dim z_exd As SqlClient.SqlException = CType(ex, SqlClient.SqlException)
            z_lng = z_exd.Number
        End If

        Return z_lng

    End Function
End Class
