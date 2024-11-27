Imports Microsoft.ApplicationBlocks.Data

Namespace Provider
    Public Class mOraSupplierProvider

        Public Shared Function SearchSupplier(ByVal supplierName As String) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "", sp.GetNull(supplierName)).Tables(0)
        End Function

        Public Shared Function GetActive() As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_OraSupplier_GetActive").Tables(0)
        End Function

        Public Shared Function GetAll() As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_OraSupplier_GetAll").Tables(0)
        End Function


        Public Shared Function GetDataOraSuplier(ByVal name As String, ByVal type As String) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "Oracle_Supplier_GetAll", _
                                            sp.GetNull(name), _
                                            sp.GetNull(type)).Tables(0)
        End Function

        Public Shared Function m_OraSupplier_GetByID(ByVal OraSupplierID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_OraSupplier_GetByID", sp.GetNull(OraSupplierID)).Tables(0)
        End Function


        Public Shared Function m_OraSupplier_Insert(ByVal objItem As mOraSupplierInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "m_OraSupplier_Insert", _
                                           sp.GetNull(objItem.SupplierName), _
                                           sp.GetNull(objItem.Active), _
                                           sp.GetNull(objItem.OraLink))
        End Function

        Public Shared Sub m_OraSupplier_Upd(ByVal objItem As mOraSupplierInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_OraSupplier_Update", _
                                           sp.GetNull(objItem.SupplierName), _
                                           sp.GetNull(objItem.OraLink), _
                                           sp.GetNull(objItem.Active), _
                                           sp.GetNull(objItem.ID))
        End Sub

        Public Shared Sub m_OraSupplier_Del(ByVal OraSupplierID As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_OraSupplier_Del", sp.GetNull(OraSupplierID))
        End Sub
    End Class
End Namespace

