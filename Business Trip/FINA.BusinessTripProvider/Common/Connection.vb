Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Data

Namespace Provider

    Class Connections
        'http://www.codeproject.com/Questions/681165/How-to-access-connectionString-from-app-config-wit
        Public sqlcon As SqlConnection
        Public sqlcm As SqlCommand
        Protected sqlDr As SqlDataReader
        Private _sqlTrans As SqlTransaction

        Public ReadOnly Property SqlConn() As SqlConnection
            Get
                Return sqlcon
            End Get
        End Property

        Public ReadOnly Property SqlTrans() As SqlTransaction
            Get
                Return _sqlTrans
            End Get
        End Property

        Public Sub New()
            Dim Connectionstring As String = ConfigurationManager.ConnectionStrings("BusinessTripConnectionString").ConnectionString
            sqlcon = New SqlConnection(Connectionstring)
            sqlcon.Open()
        End Sub

        Public Sub New(ByVal ConnectionString As String)
            sqlcon = New SqlConnection(ConnectionString)
            sqlcon.Open()
        End Sub

        Public Function ExecuteReader() As SqlDataReader
            sqlcm.Connection = sqlcon
            sqlDr = sqlcm.ExecuteReader()
            Return sqlDr
        End Function

        Public Sub Close()
            sqlcon.Close()
        End Sub

        Public Sub BeginTransaction()
            _sqlTrans = sqlcon.BeginTransaction(IsolationLevel.ReadCommitted)
        End Sub

        Public Sub CommitTransaction()
            _sqlTrans.Commit()
            _sqlTrans.Dispose()
            _sqlTrans = Nothing
        End Sub

        Public Sub RollbackTransaction()
            _sqlTrans.Rollback()
            _sqlTrans.Dispose()
            _sqlTrans = Nothing
        End Sub
    End Class
End Namespace