Imports Microsoft.ApplicationBlocks.Data


Namespace Provider
    Public Class mInvoiceItemProvider

        Public Shared Function GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_InvoiceItem_GetAll").Tables(0)
        End Function

        Public Shared Function GetActive() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_InvoiceItem_GetActive").Tables(0)
        End Function

        Public Shared Function m_InvoiceItem_GetByID(ByVal InvItemID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_InvoiceItem_GetByID", sp.GetNull(InvItemID)).Tables(0)
        End Function

        Public Shared Function m_InvoiceItem_Insert(ByVal objItem As mInvoiceItemInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "m_InvoiceItem_Insert", _
                                           sp.GetNull(objItem.ItemName), _
                                           sp.GetNull(objItem.Note), _
                                           sp.GetNull(objItem.Status))
        End Function

        Public Shared Sub m_InvoiceItem_Upd(ByVal objItem As mInvoiceItemInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_InvoiceItem_Update", _
                                           sp.GetNull(objItem.ItemName), _
                                           sp.GetNull(objItem.Note), _
                                           sp.GetNull(objItem.Status), _
                                           sp.GetNull(objItem.InvoiceItemID))
        End Sub

        Public Shared Sub m_InvoiceItem_Del(ByVal InvItemID As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_InvoiceItem_Del", sp.GetNull(InvItemID))
        End Sub


    End Class
End Namespace

