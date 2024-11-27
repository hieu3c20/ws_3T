Imports Microsoft.ApplicationBlocks.Data


Namespace Provider
    Public Class InvoiceProvider
        Public Shared Sub BTInvoice_Insert(ByVal obj As tblBTInvoiceInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                    "tbl_BT_Invoice_Insert", _
                                    sp.GetNull(obj.BTRegisterID), _
                                    sp.GetNull(obj.STT), _
                                    sp.GetNull(obj.InvNo), _
                                    sp.GetNull(obj.InvDate), _
                                    sp.GetNull(obj.SerialNo), _
                                    sp.GetNull(obj.SellerName), _
                                    sp.GetNull(obj.SellerTaxCode), _
                                    sp.GetNull(obj.NetCost), _
                                    sp.GetNull(obj.TaxRate), _
                                    sp.GetNull(obj.Item), _
                                    sp.GetNull(obj.Supplier), _
                                    sp.GetNull(obj.CreatedBy), _
                                    sp.GetNull(obj.IsCreditCard), _
                                    sp.GetNull(obj.VAT))
        End Sub

        Public Shared Sub BTInvoice_Update(ByVal obj As tblBTInvoiceInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                    "tbl_BT_Invoice_Update", _
                                    sp.GetNull(obj.ID), _
                                    sp.GetNull(obj.STT), _
                                    sp.GetNull(obj.InvNo), _
                                    sp.GetNull(obj.InvDate), _
                                    sp.GetNull(obj.SerialNo), _
                                    sp.GetNull(obj.SellerName), _
                                    sp.GetNull(obj.SellerTaxCode), _
                                    sp.GetNull(obj.NetCost), _
                                    sp.GetNull(obj.TaxRate), _
                                    sp.GetNull(obj.Item), _
                                    sp.GetNull(obj.Supplier), _
                                    sp.GetNull(obj.ModifiedBy), _
                                    sp.GetNull(obj.IsCreditCard), _
                                    sp.GetNull(obj.VAT))
        End Sub

        Public Shared Function BTInvoice_Search(ByVal btID As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Invoice_Search", _
                                            btID, "", "", DBNull.Value, _
                                            DBNull.Value, "", "", "", "", _
                                            DBNull.Value, DBNull.Value, False, "").Tables(0)
        End Function

        Public Shared Function BTInvoice_Search(ByVal serialNo As String, _
                                                ByVal invoiceNo As String, _
                                                ByVal invoiceDateFrom As DateTime, _
                                                ByVal invoiceDateTo As DateTime, _
                                                ByVal seller As String, _
                                                ByVal supplier As String, _
                                                ByVal item As String, _
                                                ByVal employeeCode As String, _
                                                ByVal departureFrom As DateTime, _
                                                ByVal departureTo As DateTime, _
                                                ByVal username As String) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Invoice_Search", _
                                            -1, _
                                            sp.GetNull(serialNo), _
                                            sp.GetNull(invoiceNo), _
                                            sp.GetNull(invoiceDateFrom), _
                                            sp.GetNull(invoiceDateTo), _
                                            sp.GetNull(seller), _
                                            sp.GetNull(supplier), _
                                            sp.GetNull(item), _
                                            sp.GetNull(employeeCode), _
                                            sp.GetNull(departureFrom), _
                                            sp.GetNull(departureTo), _
                                            True, sp.GetNull(username)).Tables(0)
        End Function

        Public Shared Function BTInvoice_GetByID(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Invoice_GetByID", id).Tables(0)
        End Function

        Public Shared Sub BTInvoice_Delete(ByVal ids As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_Invoice_Delete", _
                                      sp.GetNull(ids))
        End Sub

        Public Shared Function CheckNo(ByVal id As Integer, ByVal no As String, ByVal seller As String) As Boolean
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "tbl_BT_Invoice_CheckNo", _
                                           sp.GetNull(id), _
                                           sp.GetNull(no), _
                                           sp.GetNull(seller))) > 0
        End Function
    End Class
End Namespace

