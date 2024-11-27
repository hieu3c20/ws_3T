Imports Microsoft.ApplicationBlocks.Data

Namespace Provider
    Public Class mCompanyProvider

        Public Shared Function GetActive() As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_CompanyName_GetActive").Tables(0)
        End Function

        Public Shared Function GetAll() As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_CompanyName_GetAll").Tables(0)
        End Function

        Public Shared Function m_Company_GetByID(ByVal BatchNameID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_CompanyName_GetByID", sp.GetNull(BatchNameID)).Tables(0)
        End Function

        Public Shared Function m_Company_Insert(ByVal objItem As mCompanyInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "m_CompanyName_Insert", _
                                           sp.GetNull(objItem.Name), _
                                           sp.GetNull(objItem.TaxCode), _
                                           sp.GetNull(objItem.Description), _
                                           sp.GetNull(objItem.Active))
        End Function

        Public Shared Sub m_Company_Upd(ByVal objItem As mCompanyInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_CompanyName_Update", _
                                           sp.GetNull(objItem.Name), _
                                           sp.GetNull(objItem.TaxCode), _
                                           sp.GetNull(objItem.Description), _
                                           sp.GetNull(objItem.Active), _
                                           sp.GetNull(objItem.ID))
        End Sub

        Public Shared Sub m_Company_Del(ByVal CompanyID As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_CompanyName_Del", sp.GetNull(CompanyID))
        End Sub
    End Class
End Namespace

