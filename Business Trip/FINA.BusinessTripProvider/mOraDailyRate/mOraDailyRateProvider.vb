Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Namespace Provider
    Public Class mOraDailyRateProvider
        'Public Shared Function SearchDailyRate(ByVal supplierName As String) As DataTable
        '    Dim sp As New SqlDataProvider()
        '    Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "", sp.GetNull(supplierName)).Tables(0)
        'End Function

        'Public Shared Function GetActive() As DataTable
        '    Dim sp As New SqlDataProvider()
        '    Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_OraSupplier_GetActive").Tables(0)
        'End Function

        Public Shared Function GetActiveCurrency() As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_CurrencyActive").Tables(0)
        End Function

        Public Shared Function GetAll() As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Currency_GetAll").Tables(0)
        End Function

        'Public Shared Function GetDataOraSuplier() As DataTable
        '    Dim sp As New SqlDataProvider()
        '    Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "Oracle_Supplier_GetAll").Tables(0)
        'End Function

        Public Shared Function m_DailyRate_GetByID(ByVal DailyRateID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_DailyRate_GetByID", sp.GetNull(DailyRateID)).Tables(0)
        End Function

        Public Shared Function m_DailyRate_Insert(ByVal objItem As mOraDailyRateInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "m_DailyRate_Insert", _
                                           sp.GetNull(objItem.FROM_CURRENCY), _
                                           sp.GetNull(objItem.TO_CURRENCY), _
                                           sp.GetNull(objItem.CONVERSION_DATE), _
                                           sp.GetNull(objItem.CONVERSION_RATE), _
                                           sp.GetNull(objItem.Active))
        End Function

        Public Shared Sub m_DailyRate_Upd(ByVal objItem As mOraDailyRateInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_DailyRate_Update", _
                                           sp.GetNull(objItem.FROM_CURRENCY), _
                                           sp.GetNull(objItem.TO_CURRENCY), _
                                           sp.GetNull(objItem.CONVERSION_DATE), _
                                           sp.GetNull(objItem.CONVERSION_RATE), _
                                           sp.GetNull(objItem.Active), _
                                           sp.GetNull(objItem.ID))
        End Sub

        Public Shared Sub m_DailyRate_Del(ByVal DailyRateID As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_DailyRate_Del", sp.GetNull(DailyRateID))
        End Sub

        Public Shared Sub GetOraDailyExrate()
            Dim sp As New SqlDataProvider()
            'SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "ORA_GL_DAILY_RATES_GetOraDailyExrate")
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "ORA_GL_DAILY_RATES_GetOraDailyExrate"
                    sqlcm.ExecuteNonQuery()
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Sub
    End Class
End Namespace

