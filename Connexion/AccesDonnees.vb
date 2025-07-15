Imports System.Data.Common
Imports System.Transactions

Public Interface AccesDonnees

    Property ConnectionString() As String
    ReadOnly Property ErreurCleDuplique() As Int64

    Function AddCommand(ByVal p_strTextCommand As String) As DbCommand
    Function AddParametres(ByVal Nom As String, ByVal type As DbType) As DbParameter
    Function NumeroErreur(ByVal ex As Exception) As Int64

    Sub BeginTransaction()
    Sub Commit()
    Sub RollBack()

    Function ExecuteProcedureStockee(ByVal p_dtbTable As DataTable, ByVal p_comCommand As DbCommand) As DataTable
    Function ExecuteSql(ByVal p_dtbTable As DataTable, ByVal p_comCommand As DbCommand) As DataTable
    Function ExecuteSql(ByVal p_dtbTable As DataTable, ByVal p_strSql As String) As DataTable
    Function Execute(ByVal p_dtbTable As DataTable, ByVal p_comCommand As DbCommand) As DataTable
    Function ExecuteScalar(ByVal p_strSql As String) As Object

    Sub ExecuteProcedureStockeeSansRetour(ByVal p_comCommand As DbCommand)
    Sub ExecuteSqlSansRetour(ByVal p_comCommand As DbCommand)
    Sub ExecuteSqlSansRetour(ByVal p_strSql As String)
    Sub ExecuteSansRetour(ByVal p_comCommand As DbCommand)

    ReadOnly Property ServerName() As String
    ReadOnly Property Database() As String
    ReadOnly Property UserName() As String
    ReadOnly Property PassWord() As String

    Property AcceptChangesDuringFill() As Boolean

    Function OpenBDD() As Boolean
    Sub CloseBDD()

    'Function RetourReader(ByVal p_Sql As String) As System.Data.IDataReader
    'Function RetourDataset(ByVal p_strTable As String, ByVal p_strSql As String, ByRef p_dts As DataSet) As DataSet
    'Sub ExecuteSql(ByVal p_strSql As String)

    'Function Format_Date(ByVal p_strDate As String) As String
    'Function Format_DateHeure(ByVal p_strDate As String) As String
    'Function Format_Heure(ByVal p_strDate As String) As String
    'Function Format_String(ByVal p_str As String) As String
    'Function Format_Bit(ByVal p_str As String) As String
    'Function Format_BoolToBit(ByVal p_bln As Boolean) As Byte
    'Function Format_Nombre(ByVal p_str As String) As String

End Interface
