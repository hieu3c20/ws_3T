Imports Microsoft.ApplicationBlocks.Data

Namespace Provider
    Public Class mBatchNameProvider

        Public Shared Function GetActive() As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_BatchName_GetActive").Tables(0)
        End Function

        Public Shared Function GetAll() As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_BatchName_GetAll").Tables(0)
        End Function

        Public Shared Function m_BatchName_GetByID(ByVal BatchNameID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_BatchName_GetByID", sp.GetNull(BatchNameID)).Tables(0)
        End Function

        Public Shared Function m_BatchName_Insert(ByVal objItem As mBatchNameInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "m_BatchName_Insert", _
                                           sp.GetNull(objItem.BatchName), _
                                           sp.GetNull(objItem.Description), _
                                           sp.GetNull(objItem.Active))
        End Function

        Public Shared Sub m_BatchName_Upd(ByVal objItem As mBatchNameInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_BatchName_Update", _
                                           sp.GetNull(objItem.BatchName), _
                                           sp.GetNull(objItem.Description), _
                                           sp.GetNull(objItem.Active), _
                                           sp.GetNull(objItem.ID))
        End Sub

        Public Shared Sub m_BatchName_Del(ByVal BatchNameID As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_BatchName_Del", sp.GetNull(BatchNameID))
        End Sub
    End Class
End Namespace

