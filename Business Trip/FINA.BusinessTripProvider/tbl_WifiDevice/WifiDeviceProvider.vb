Imports Microsoft.ApplicationBlocks.Data


Namespace Provider
    Public Class WifiDeviceProvider

        Public Shared Function WifiDevice_Insert(ByVal obj As tblBTWifiDeviceInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                      "tbl_BT_WifiDevice_Insert", _
                                      sp.GetNull(obj.BTRegisterID), _
                                      sp.GetNull(obj.FromDate), _
                                      sp.GetNull(obj.ToDate), _
                                      sp.GetNull(obj.CountryCode), _
                                      sp.GetNull(obj.EmployeeCode), _
                                      sp.GetNull(obj.EmployeeName), _
                                      sp.GetNull(obj.EmployeeDivision), _
                                      sp.GetNull(obj.EmployeeDepartment), _
                                      sp.GetNull(obj.CreatedBy), _
                                      sp.GetNull(obj.Status), _
                                      sp.GetNull(obj.Comment)))

        End Function

        Public Shared Sub WifiDevice_Update(ByVal obj As tblBTWifiDeviceInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_WifiDevice_Update", _
                                      sp.GetNull(obj.ID), _
                                      sp.GetNull(obj.FromDate), _
                                      sp.GetNull(obj.ToDate), _
                                      sp.GetNull(obj.CountryCode), _
                                      sp.GetNull(obj.EmployeeCode), _
                                      sp.GetNull(obj.EmployeeName), _
                                      sp.GetNull(obj.EmployeeDivision), _
                                      sp.GetNull(obj.EmployeeDepartment), _
                                      sp.GetNull(obj.UpdatedBy), _
                                      sp.GetNull(obj.Status), _
                                      sp.GetNull(obj.Comment))

        End Sub

        Public Shared Function WifiDevice_Search(ByVal btNo As String, ByVal countryCode As String, ByVal empCode As String, _
            ByVal empName As String, ByVal fromDate As DateTime, ByVal toDate As DateTime) As DataSet
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_WifiDevice_Search", _
                                            sp.GetNull(btNo), _
                                            sp.GetNull(countryCode), _
                                            sp.GetNull(empCode), _
                                            sp.GetNull(empName), _
                                            sp.GetNull(fromDate), _
                                            sp.GetNull(toDate))
        End Function

        Public Shared Function WifiDevicePre_Search(ByVal countryCode As String, ByVal empCode As String, _
            ByVal empName As String, ByVal fromDate As DateTime, ByVal toDate As DateTime, ByVal timeKeeper As String) As DataSet
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_WifiDevicePre_Search", _
                                            sp.GetNull(countryCode), _
                                            sp.GetNull(empCode), _
                                            sp.GetNull(empName), _
                                            sp.GetNull(fromDate), _
                                            sp.GetNull(toDate), _
                                            sp.GetNull(timeKeeper))
        End Function

        Public Shared Function WifiDevice_GetByBT(ByVal btid As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_WifiDevice_GetByBT", btid).Tables(0)
        End Function

        Public Shared Function WifiDevice_GetByID(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_WifiDevice_GetByID", id).Tables(0)
        End Function

        Public Shared Sub WifiDevice_Delete(ByVal ids As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_WifiDevice_Delete", _
                                      sp.GetNull(ids))

        End Sub

        Public Shared Function WifiDevice_CheckDate(ByVal obj As tblBTWifiDeviceInfo) As Boolean
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                            "tbl_BT_WifiDevice_CheckDate", _
                                            sp.GetNull(obj.EmployeeCode), _
                                            sp.GetNull(obj.ID), _
                                            sp.GetNull(obj.FromDate), _
                                            sp.GetNull(obj.ToDate))) = 0
        End Function

        Public Shared Function WifiDevice_UpdateStatus(ByVal id As Integer, ByVal status As String, ByVal comment As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                      "tbl_BT_WifiDevice_UpdateStatus", _
                                      sp.GetNull(id), _
                                      sp.GetNull(status), _
                                      sp.GetNull(comment)))
        End Function

    End Class
End Namespace

