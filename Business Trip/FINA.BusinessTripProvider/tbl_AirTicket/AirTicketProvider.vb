Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient


Namespace Provider
    Public Class AirTicketProvider

        Public Shared Function BTAirTicket_Other_Import(ByVal obj As tblBTAirTicketInfo, ByVal remark As String) As Integer
            Dim sp As New SqlDataProvider()
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "tbl_BT_AirTicket_Other_Import"
                    sqlcm.Parameters.AddWithValue("@Passenger", sp.GetNull(obj.Passenger))
                    sqlcm.Parameters.AddWithValue("@DepartureDate", sp.GetNull(obj.DepartureDate))
                    sqlcm.Parameters.AddWithValue("@ReturnDate", sp.GetNull(obj.ReturnDate))
                    sqlcm.Parameters.AddWithValue("@TicketDate", sp.GetNull(obj.TicketDate))
                    sqlcm.Parameters.AddWithValue("@AirLine", sp.GetNull(obj.AirLine))
                    sqlcm.Parameters.AddWithValue("@TicketNo", sp.GetNull(obj.TicketNo))
                    sqlcm.Parameters.AddWithValue("@Routing", sp.GetNull(obj.Routing))
                    sqlcm.Parameters.AddWithValue("@Fare", sp.GetNull(obj.Fare))
                    sqlcm.Parameters.AddWithValue("@VAT", sp.GetNull(obj.VAT))
                    sqlcm.Parameters.AddWithValue("@APTTax", sp.GetNull(obj.APTTax))
                    sqlcm.Parameters.AddWithValue("@SF", sp.GetNull(obj.SF))
                    sqlcm.Parameters.AddWithValue("@NetPayment", sp.GetNull(obj.NetPayment))
                    sqlcm.Parameters.AddWithValue("@Currency", sp.GetNull(obj.Currency))
                    sqlcm.Parameters.AddWithValue("@Exrate", sp.GetNull(obj.Exrate))
                    sqlcm.Parameters.AddWithValue("@CreatedBy", sp.GetNull(obj.CreatedBy))
                    sqlcm.Parameters.AddWithValue("@AirPeriod", sp.GetNull(obj.AirPeriod))
                    sqlcm.Parameters.AddWithValue("@Supplier", sp.GetNull(obj.OraSupplier))
                    sqlcm.Parameters.AddWithValue("@Remark", sp.GetNull(remark))
                    sqlcm.Parameters.AddWithValue("@BudgetCode", sp.GetNull(obj.BudgetCode))
                    sqlcm.Parameters.AddWithValue("@Purpose", sp.GetNull(obj.Purpose))
                    sqlcm.Parameters.AddWithValue("@Requester", sp.GetNull(obj.Requester))
                    sqlcm.Parameters.AddWithValue("@ictRequest", sp.GetNull(obj.ICTRequest))
                    'sqlcm.Parameters.AddWithValue("@RequesterDept", sp.GetNull(obj.RequesterDept))
                    'sqlcm.Parameters.AddWithValue("@RequesterName", sp.GetNull(obj.RequesterName))
                    'sqlcm.Parameters.AddWithValue("@RequesterPhone", sp.GetNull(obj.RequesterPhone))
                    'sqlcm.Parameters.AddWithValue("@RequesterDiv", sp.GetNull(obj.RequesterDiv))
                    Return CommonFunction._ToInt(sqlcm.ExecuteScalar())
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

        Public Shared Function BTAirTicket_Import(ByVal obj As tblBTAirTicketInfo, ByVal remark As String) As Integer
            Dim sp As New SqlDataProvider()
            'Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
            '                        "tbl_BT_AirTicket_Import", _
            '                        sp.GetNull(obj.Passenger), _
            '                        sp.GetNull(obj.DepartureDate), _
            '                        sp.GetNull(obj.ReturnDate), _
            '                        sp.GetNull(obj.TicketDate), _
            '                        sp.GetNull(obj.AirLine), _
            '                        sp.GetNull(obj.TicketNo), _
            '                        sp.GetNull(obj.Routing), _
            '                        sp.GetNull(obj.Fare), _
            '                        sp.GetNull(obj.VAT), _
            '                        sp.GetNull(obj.APTTax), _
            '                        sp.GetNull(obj.SF), _
            '                        sp.GetNull(obj.NetPayment), _
            '                        sp.GetNull(obj.Currency), _
            '                        sp.GetNull(obj.Exrate), _
            '                        sp.GetNull(obj.CreatedBy), _
            '                        sp.GetNull(obj.AirPeriod), _
            '                        sp.GetNull(obj.OraSupplier), _
            '                        sp.GetNull(remark)))
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "tbl_BT_AirTicket_Import"
                    sqlcm.Parameters.AddWithValue("@Passenger", sp.GetNull(obj.Passenger))
                    sqlcm.Parameters.AddWithValue("@DepartureDate", sp.GetNull(obj.DepartureDate))
                    sqlcm.Parameters.AddWithValue("@ReturnDate", sp.GetNull(obj.ReturnDate))
                    sqlcm.Parameters.AddWithValue("@TicketDate", sp.GetNull(obj.TicketDate))
                    sqlcm.Parameters.AddWithValue("@AirLine", sp.GetNull(obj.AirLine))
                    sqlcm.Parameters.AddWithValue("@TicketNo", sp.GetNull(obj.TicketNo))
                    sqlcm.Parameters.AddWithValue("@Routing", sp.GetNull(obj.Routing))
                    sqlcm.Parameters.AddWithValue("@Fare", sp.GetNull(obj.Fare))
                    sqlcm.Parameters.AddWithValue("@VAT", sp.GetNull(obj.VAT))
                    sqlcm.Parameters.AddWithValue("@APTTax", sp.GetNull(obj.APTTax))
                    sqlcm.Parameters.AddWithValue("@SF", sp.GetNull(obj.SF))
                    sqlcm.Parameters.AddWithValue("@NetPayment", sp.GetNull(obj.NetPayment))
                    sqlcm.Parameters.AddWithValue("@Currency", sp.GetNull(obj.Currency))
                    sqlcm.Parameters.AddWithValue("@Exrate", sp.GetNull(obj.Exrate))
                    sqlcm.Parameters.AddWithValue("@CreatedBy", sp.GetNull(obj.CreatedBy))
                    sqlcm.Parameters.AddWithValue("@AirPeriod", sp.GetNull(obj.AirPeriod))
                    sqlcm.Parameters.AddWithValue("@Supplier", sp.GetNull(obj.OraSupplier))
                    sqlcm.Parameters.AddWithValue("@Remark", sp.GetNull(remark))
                    Return CommonFunction._ToInt(sqlcm.ExecuteScalar())
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

        Public Shared Sub BTAirTicket_RemovePrevError(ByVal username As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                    "tbl_BT_AirTicket_RemovePrevError", sp.GetNull(username))
        End Sub

        Public Shared Sub BTAirTicket_RemovePrevOtherError(ByVal username As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                    "tbl_BT_AirTicket_RemovePrevOtherError", sp.GetNull(username))
        End Sub

        Public Shared Function BTAirTicket_Insert(ByVal obj As tblBTAirTicketInfo) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                    "tbl_BT_AirTicket_Insert", _
                                    sp.GetNull(obj.BTRegisterID), _
                                    sp.GetNull(obj.TicketDate), _
                                    sp.GetNull(obj.AirLine), _
                                    sp.GetNull(obj.TicketNo), _
                                    sp.GetNull(obj.Routing), _
                                    sp.GetNull(obj.Fare), _
                                    sp.GetNull(obj.VAT), _
                                    sp.GetNull(obj.APTTax), _
                                    sp.GetNull(obj.SF), _
                                    sp.GetNull(obj.NetPayment), _
                                    sp.GetNull(obj.Currency), _
                                    sp.GetNull(obj.Exrate), _
                                    sp.GetNull(obj.CreatedBy), _
                                    sp.GetNull(obj.AirPeriod), _
                                    sp.GetNull(obj.OraSupplier), _
                                    sp.GetNull(obj.Passenger), _
                                    sp.GetNull(obj.BudgetCode), _
                                    sp.GetNull(obj.Purpose), _
                                    sp.GetNull(obj.Requester), _
                                    sp.GetNull(obj.RequesterDept), _
                                    sp.GetNull(obj.RequesterName), _
                                    sp.GetNull(obj.RequesterPhone), _
                                    sp.GetNull(obj.RequesterDiv), _
                                    sp.GetNull(obj.ICTRequest), _
                                    sp.GetNull(obj.DepartureDate), _
                                    sp.GetNull(obj.ReturnDate)))
        End Function

        Public Shared Function BTAirTicket_Update(ByVal obj As tblBTAirTicketInfo) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                    "tbl_BT_AirTicket_Update", _
                                    sp.GetNull(obj.ID), _
                                    sp.GetNull(obj.TicketDate), _
                                    sp.GetNull(obj.AirLine), _
                                    sp.GetNull(obj.TicketNo), _
                                    sp.GetNull(obj.Routing), _
                                    sp.GetNull(obj.Fare), _
                                    sp.GetNull(obj.VAT), _
                                    sp.GetNull(obj.APTTax), _
                                    sp.GetNull(obj.SF), _
                                    sp.GetNull(obj.NetPayment), _
                                    sp.GetNull(obj.Currency), _
                                    sp.GetNull(obj.Exrate), _
                                    sp.GetNull(obj.ModifiedBy), _
                                    sp.GetNull(obj.AirPeriod), _
                                    sp.GetNull(obj.OraSupplier), _
                                    sp.GetNull(obj.Passenger), _
                                    sp.GetNull(obj.BudgetCode), _
                                    sp.GetNull(obj.Purpose), _
                                    sp.GetNull(obj.Requester), _
                                    sp.GetNull(obj.RequesterDept), _
                                    sp.GetNull(obj.RequesterName), _
                                    sp.GetNull(obj.RequesterPhone), _
                                    sp.GetNull(obj.RequesterDiv), _
                                    sp.GetNull(obj.ICTRequest), _
                                    sp.GetNull(obj.DepartureDate), _
                                    sp.GetNull(obj.ReturnDate)))
        End Function

        Public Shared Function BTAirTicket_Search(ByVal btID As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_AirTicket_Search", _
                                            btID, -1, "", "", "", "", DBNull.Value, DBNull.Value, "", False, "", True, False).Tables(0)
        End Function

        Public Shared Function BTAirTicket_Search(ByVal period As Integer, _
                                                ByVal airline As String, _
                                                ByVal ticketNo As String, _
                                                ByVal employeeCode As String, _
                                                ByVal employeeName As String, _
                                                ByVal departureFrom As DateTime, _
                                                ByVal departureTo As DateTime, _
                                                ByVal username As String, _
                                                ByVal supplier As String, _
                                                ByVal showAll As Boolean, _
                                                ByVal budgetChecked As Boolean) As DataSet
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_AirTicket_Search", _
                                            -1, _
                                            sp.GetNull(period), _
                                            sp.GetNull(airline), _
                                            sp.GetNull(ticketNo), _
                                            sp.GetNull(employeeCode), _
                                            sp.GetNull(employeeName), _
                                            sp.GetNull(departureFrom), _
                                            sp.GetNull(departureTo), _
                                            sp.GetNull(username), _
                                            True, _
                                            sp.GetNull(supplier), _
                                            sp.GetNull(showAll), _
                                            sp.GetNull(budgetChecked))
        End Function

        Public Shared Function BTAirTicket_GetByID(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_AirTicket_GetByID", id).Tables(0)
        End Function

        Public Shared Function CheckPeriodAndSupplier(ByVal period As Integer, ByVal supplier As String) As Boolean
            Return CommonFunction._ToBoolean(SqlHelper.ExecuteScalar(New Connections().SqlConn, "CheckPeriodAndSupplier", period, supplier))
        End Function

        Public Shared Function CheckNo(ByVal id As Integer, ByVal no As String) As Boolean
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "tbl_BT_AirTicket_CheckNo", _
                                           sp.GetNull(id), _
                                           sp.GetNull(no))) > 0
        End Function

        Public Shared Function CheckBudget(ByVal period As Integer, ByVal supplier As String) As Integer
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "tbl_BT_AirTicket_CheckBudget", _
                                           sp.GetNull(period), _
                                           sp.GetNull(supplier)))
        End Function

        Public Shared Function CheckBudgetString(ByVal period As Integer, ByVal supplier As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "tbl_BT_AirTicket_CheckBudget_Long", _
                                           sp.GetNull(period), _
                                           sp.GetNull(supplier)))
        End Function

        Public Shared Sub BTAirTicket_Delete(ByVal ids As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_AirTicket_Delete", _
                                      sp.GetNull(ids))
        End Sub

        Public Shared Function TransferToOracle(ByVal period As Integer, _
                                          ByVal userName As String, _
                                          ByVal supplier As String, _
                                          ByVal site As String, _
                                          ByVal gldate As DateTime, _
                                          ByVal batchName As Integer, _
                                          ByVal invoiceDate As Date) As String
            Dim sp As New SqlDataProvider()
            'Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().sqlcon, _
            '        "TransferAirticketToOracle", _
            '        sp.GetNull(period), _
            '        sp.GetNull(userName), _
            '        sp.GetNull(supplier), _
            '        sp.GetNull(site), _
            '        sp.GetNull(gldate), _
            '        sp.GetNull(batchName), _
            '        sp.GetNull(invoiceDate)))
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "TransferAirticketToOracle"
                    sqlcm.Parameters.AddWithValue("@Period", sp.GetNull(period))
                    sqlcm.Parameters.AddWithValue("@createdBy", sp.GetNull(userName))
                    sqlcm.Parameters.AddWithValue("@SupplierNo", sp.GetNull(supplier))
                    sqlcm.Parameters.AddWithValue("@SupplierSite", sp.GetNull(site))
                    sqlcm.Parameters.AddWithValue("@GLDate", sp.GetNull(gldate))
                    sqlcm.Parameters.AddWithValue("@batchName", sp.GetNull(batchName))
                    sqlcm.Parameters.AddWithValue("@invoiceDate", sp.GetNull(invoiceDate))
                    Return CommonFunction._ToString(sqlcm.ExecuteScalar())
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

        Public Shared Function BTAirTicket_CheckSupplierSite(ByVal supplier As String) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_AirTicket_CheckSupplierSite", supplier).Tables(0)
        End Function

        Public Shared Sub BTAirTicket_GAApprove(ByVal period As Integer, ByVal supplier As String, _
                                                ByVal comment As String, ByVal createdBy As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                    "tbl_BT_AirTicket_GAApprove", _
                                    sp.GetNull(period), _
                                    sp.GetNull(supplier), _
                                    sp.GetNull(comment), _
                                    sp.GetNull(createdBy))
        End Sub

        Public Shared Sub BTAirTicket_FIReject(ByVal period As Integer, ByVal supplier As String, _
                                                ByVal comment As String, ByVal createdBy As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                    "tbl_BT_AirTicket_FIReject", _
                                    sp.GetNull(period), _
                                    sp.GetNull(supplier), _
                                    sp.GetNull(comment), _
                                    sp.GetNull(createdBy))
        End Sub

        Public Shared Function BTAirTicket_History(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_AirTicket_History_Get", id).Tables(0)
        End Function

        Public Shared Function BTAirTicket_GetErrors(ByVal username As String) As DataSet
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BTAirTicket_GetErrors", sp.GetNull(username))
        End Function

        Public Shared Sub BTAirTicket_ConfirmBudget(ByVal id As Integer, ByVal budgetCode As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                    "tbl_BT_AirTicket_ConfirmBudget", _
                                    sp.GetNull(id), _
                                    sp.GetNull(budgetCode))
        End Sub

        Public Shared Sub BTAirTicket_RejectBudget(ByVal id As Integer, ByVal budgetCode As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                    "tbl_BT_AirTicket_RejectBudget", _
                                    sp.GetNull(id), _
                                    sp.GetNull(budgetCode))
        End Sub

        Public Shared Sub BTAirTicket_ConfirmBudgetAll(ByVal period As Integer, ByVal supplier As String, _
                                                ByVal comment As String, ByVal createdBy As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                    "tbl_BT_AirTicket_ConfirmBudgetAll", _
                                    sp.GetNull(period), _
                                    sp.GetNull(supplier), _
                                    sp.GetNull(comment), _
                                    sp.GetNull(createdBy))
        End Sub

        Public Shared Sub BTAirTicket_RejectBudgetAll(ByVal period As Integer, ByVal supplier As String, _
                                                ByVal comment As String, ByVal createdBy As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                    "tbl_BT_AirTicket_RejectBudgetAll", _
                                    sp.GetNull(period), _
                                    sp.GetNull(supplier), _
                                    sp.GetNull(comment), _
                                    sp.GetNull(createdBy))
        End Sub

#Region "m_AirPeriod"

        Public Shared Function AirPeriod_GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_AirPeriod_GetAll").Tables(0)
        End Function

        Public Shared Function AirPeriod_GetSubmited() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_AirPeriod_GetSubmited").Tables(0)
        End Function

        Public Shared Function OraSupplier_GetSubmited(ByVal period As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_OraSupplier_GetSubmited", period).Tables(0)
        End Function

        Public Shared Function AirPeriod_GetByID(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_AirPeriod_GetByID", id).Tables(0)
        End Function

#End Region

#Region "tbl_BT_AirTicketCheck"
        Public Shared Function AirTicketCheck(ByVal period As Integer, ByVal supplier As String) As DataSet
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_AirTicketCheck_Check", period, supplier)
        End Function

#End Region

#Region "Air ticket request"
        Public Shared Function BTAirTicketRequest_Insert(ByVal requestObj As tblBTAirTicketRequestInfo, ByVal relativeObjs As List(Of tblBTAirTicketRelativeInfo)) As Decimal
            Dim objConn As New Connections()
            Try
                objConn.BeginTransaction()
                Dim sp As New SqlDataProvider()
                Dim requestID As Integer = CommonFunction._ToInt(SqlHelper.ExecuteScalar(objConn.SqlTrans, _
                                        "tbl_BT_AirTicket_Request_Insert", _
                                        sp.GetNull(requestObj.EmployeeCode), _
                                        sp.GetNull(requestObj.EmployeeName), _
                                        sp.GetNull(requestObj.EmployeeDivision), _
                                        sp.GetNull(requestObj.EmployeeDepartment), _
                                        sp.GetNull(requestObj.FromCountry), _
                                        sp.GetNull(requestObj.FromDestination), _
                                        sp.GetNull(requestObj.ToCountry), _
                                        sp.GetNull(requestObj.ToDestination), _
                                        sp.GetNull(requestObj.DepartureDate), _
                                        sp.GetNull(requestObj.ReturnDate), _
                                        sp.GetNull(requestObj.Purpose), _
                                        sp.GetNull(requestObj.CreatedBy), _
                                        sp.GetNull(requestObj.Status), _
                                        sp.GetNull(requestObj.BudgetCode)))

                '
                For Each obj As tblBTAirTicketRelativeInfo In relativeObjs
                    SqlHelper.ExecuteNonQuery(objConn.SqlTrans, _
                                              "tbl_BT_AirTicket_Relative_Insert", _
                                              sp.GetNull(requestID), _
                                              sp.GetNull(obj.Name), _
                                              sp.GetNull(obj.Relationship), _
                                              sp.GetNull(obj.FromCountry), _
                                              sp.GetNull(obj.FromDestination), _
                                              sp.GetNull(obj.ToCountry), _
                                              sp.GetNull(obj.ToDestination), _
                                              sp.GetNull(obj.DepartureDate), _
                                              sp.GetNull(obj.ReturnDate), _
                                              sp.GetNull(obj.SameAsEmployee))
                Next
                '
                objConn.CommitTransaction()
                Return requestID
            Catch ex As Exception
                objConn.RollbackTransaction()
                Throw ex
            End Try
        End Function

        Public Shared Sub BTAirTicketRequest_Update(ByVal requestObj As tblBTAirTicketRequestInfo, ByVal relativeObjs As List(Of tblBTAirTicketRelativeInfo))
            Dim objConn As New Connections()
            Try
                objConn.BeginTransaction()
                Dim sp As New SqlDataProvider()
                SqlHelper.ExecuteNonQuery(objConn.SqlTrans, _
                                        "tbl_BT_AirTicket_Request_Update", _
                                        sp.GetNull(requestObj.ID), _
                                        sp.GetNull(requestObj.EmployeeCode), _
                                        sp.GetNull(requestObj.EmployeeName), _
                                        sp.GetNull(requestObj.EmployeeDivision), _
                                        sp.GetNull(requestObj.EmployeeDepartment), _
                                        sp.GetNull(requestObj.FromCountry), _
                                        sp.GetNull(requestObj.FromDestination), _
                                        sp.GetNull(requestObj.ToCountry), _
                                        sp.GetNull(requestObj.ToDestination), _
                                        sp.GetNull(requestObj.DepartureDate), _
                                        sp.GetNull(requestObj.ReturnDate), _
                                        sp.GetNull(requestObj.Purpose), _
                                        sp.GetNull(requestObj.ModifiedBy), _
                                        sp.GetNull(requestObj.BudgetCode))
                '
                For Each obj As tblBTAirTicketRelativeInfo In relativeObjs
                    SqlHelper.ExecuteNonQuery(objConn.SqlTrans, _
                                              "tbl_BT_AirTicket_Relative_Insert", _
                                              sp.GetNull(requestObj.ID), _
                                              sp.GetNull(obj.Name), _
                                              sp.GetNull(obj.Relationship), _
                                              sp.GetNull(obj.FromCountry), _
                                              sp.GetNull(obj.FromDestination), _
                                              sp.GetNull(obj.ToCountry), _
                                              sp.GetNull(obj.ToDestination), _
                                              sp.GetNull(obj.DepartureDate), _
                                              sp.GetNull(obj.ReturnDate), _
                                              sp.GetNull(obj.SameAsEmployee))
                Next
                '
                objConn.CommitTransaction()
            Catch ex As Exception
                objConn.RollbackTransaction()
                Throw ex
            End Try
        End Sub

        Public Shared Function BTAirTicketRequest_Search(ByVal employeeCode As String, ByVal employeeName As String, _
                                      ByVal departureDateFrom As Date, ByVal departureDateTo As Date) As DataSet
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_AirTicket_Request_Search", _
                                            sp.GetNull(employeeCode), _
                                            sp.GetNull(employeeName), _
                                            sp.GetNull(departureDateFrom), _
                                            sp.GetNull(departureDateTo))
        End Function

        Public Shared Function BTAirTicketRequest_GetByID(ByVal requestID As Integer) As DataSet
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_AirTicket_Request_GetByID", _
                                            sp.GetNull(requestID))
        End Function

        Public Shared Sub BTAirTicketRequest_Delete(ByVal id As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_AirTicket_Request_Delete", _
                                      sp.GetNull(id))
        End Sub

        Public Shared Function BTAirTicketRequest_UpdateStatus(ByVal requestID As Integer, ByVal status As String, _
                                                          ByVal createdBy As String, ByVal reason As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                      "tbl_BT_AirTicket_Request_UpdateStatus", _
                                      sp.GetNull(requestID), _
                                      sp.GetNull(status), _
                                      sp.GetNull(createdBy), _
                                      sp.GetNull(reason)))
        End Function

        Public Shared Function BTAirTicketRequest_Recall(ByVal requestID As Integer, ByVal status As String, _
                                                          ByVal createdBy As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                      "tbl_BT_AirTicket_Request_Recall", _
                                      sp.GetNull(requestID), _
                                      sp.GetNull(status), _
                                      sp.GetNull(createdBy)))
        End Function

        Public Shared Function BTAirTicketRequest_Validate(ByVal requestID As Integer, ByVal employeeCode As String, _
                                                          ByVal departureDate As Date, ByVal returnDate As Date) As Boolean
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                      "tbl_BT_AirTicket_Request_Validate", _
                                      sp.GetNull(requestID), _
                                      sp.GetNull(employeeCode), _
                                      sp.GetNull(departureDate), _
                                      sp.GetNull(returnDate))) = 0
        End Function

        Public Shared Function BTAirTicketRelative_Validate(ByVal requestID As Integer, ByVal name As String, _
                                                          ByVal departureDate As Date, ByVal returnDate As Date) As Boolean
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                      "tbl_BT_AirTicket_Relative_Validate", _
                                      sp.GetNull(requestID), _
                                      sp.GetNull(name), _
                                      sp.GetNull(departureDate), _
                                      sp.GetNull(returnDate))) = 0
        End Function
#End Region
    End Class
End Namespace

