
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Security.Cryptography
Imports System.Text

Namespace Provider
    Public Class ExpenseProvider

#Region "tbl_BT_Expense"
        Public Shared Function tbl_BT_Expense_Recall(ByVal btID As Integer, ByVal modifiedBy As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                      "tbl_BT_Expense_Recall", _
                                      sp.GetNull(btID), _
                                      sp.GetNull(modifiedBy))).Trim()
        End Function

        Public Shared Function BTExpense_CheckOnedayBTDate(ByVal btid As Integer) As DataSet
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_Expense_CheckOnedayBTDate", _
                                            sp.GetNull(btid))
        End Function

        Public Shared Function BTExpense_Copy(ByVal btID As Integer, ByVal employees As String, _
                                               ByVal createdBy As String) As Integer
            Dim sp As New SqlDataProvider()
            Dim ds As DataSet = SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                      "tbl_BT_Expense_Copy", _
                                      sp.GetNull(btID), _
                                      sp.GetNull(employees), _
                                      sp.GetNull(createdBy))
            Return CommonFunction._ToUnsignInt(ds.Tables(ds.Tables.Count - 1).Rows(0)("count"))
        End Function

        Public Shared Sub BTExpense_Delete(ByVal ids As String)
            Dim sp As New SqlDataProvider()
            Dim dtAtt As DataTable = SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                      "tbl_BT_Expense_Delete", _
                                      sp.GetNull(ids)).Tables(0)
            DeleteAttFiles(dtAtt)
        End Sub

        Private Shared Sub DeleteAttFiles(ByVal dt As DataTable)
            For Each item As DataRow In dt.Rows
                Dim filePath As String = System.Web.HttpContext.Current.Server.MapPath(CommonFunction._ToString(item("AttachmentPath")))
                If IO.File.Exists(filePath) Then
                    IO.File.Delete(filePath)
                End If
            Next
        End Sub

        Public Shared Function UpdateMovingTime(ByVal BTRegisterID As Integer, _
                                                ByVal chkMovingTime As Boolean) As DataRow
            Dim dtData As DataTable = SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_Expense_UpdateMovingTime", _
                                            BTRegisterID, chkMovingTime).Tables(0)
            Return If(dtData.Rows.Count > 0, dtData.Rows(0), Nothing)
        End Function

        Public Shared Function BTExpense_ConfirmBudget(ByVal obj As tblBTRegisterInfo, ByVal comment As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                      "tbl_BT_Expense_ConfirmBudget", _
                                      sp.GetNull(obj.BTRegisterID), _
                                      sp.GetNull(obj.BudgetName), _
                                      sp.GetNull(obj.BudgetCode), _
                                      sp.GetNull(obj.ProjectBudgetCode), _
                                      sp.GetNull(comment), _
                                      sp.GetNull(obj.ModifiedBy), _
                                      sp.GetNull(obj.CheckAllBudget))).Trim()
        End Function

        Public Shared Function BTExpense_ConfirmRequester(ByVal obj As tblBTRegisterInfo, ByVal comment As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                      "tbl_BT_Expense_ConfirmRequester", _
                                      sp.GetNull(obj.BTRegisterID), _
                                      sp.GetNull(obj.BudgetName), _
                                      sp.GetNull(obj.BudgetCode), _
                                      sp.GetNull(obj.ProjectBudgetCode), _
                                      sp.GetNull(comment), _
                                      sp.GetNull(obj.ModifiedBy), _
                                      sp.GetNull(obj.CheckAllBudget))).Trim()
        End Function

        Public Shared Function BTExpense_RejectToBudget(ByVal btID As Integer, ByVal comment As String, ByVal modifiedBy As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                      "tbl_BT_Expense_RejectToBudget", _
                                      sp.GetNull(btID), _
                                      sp.GetNull(comment), _
                                      sp.GetNull(modifiedBy))).Trim()
        End Function
        'Update trang thai Status = Complete cho FI
        Public Shared Function tbl_BT_Expense_UpdateStatusFI(ByVal BTExpenseID As Integer, _
                                    ByVal Value As String, _
                                    ByVal CreatedBy As String, _
                                    ByVal Reason As String, _
                                    Optional ByVal supplierNo As String = Nothing, _
                                    Optional ByVal supplierSite As String = Nothing, _
                                    Optional ByVal glDate As DateTime = Nothing, _
                                    Optional ByVal batchName As Integer = Nothing, _
                                    Optional ByVal invoiceDate As Date = Nothing, _
                                    Optional ByVal extAmount As Decimal = Nothing, _
                                    Optional ByVal extExrate As Decimal = Nothing, _
                                    Optional ByVal creditExrate As Decimal = Nothing) As DataRow
            Dim sp As New SqlDataProvider()
            'Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, "tbl_BT_Expense_UpdateStatusFI", _
            '                          sp.GetNull(BTExpenseID), _
            '                          sp.GetNull(Value), _
            '                          sp.GetNull(CreatedBy), _
            '                          sp.GetNull(Reason), _
            '                          sp.GetNull(supplierNo), _
            '                          sp.GetNull(supplierSite), _
            '                          sp.GetNull(glDate), _
            '                          sp.GetNull(batchName), _
            '                          sp.GetNull(invoiceDate), _
            '                          sp.GetNull(extAmount), _
            '                          sp.GetNull(extExrate)))
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "tbl_BT_Expense_UpdateStatusFI"
                    sqlcm.Parameters.AddWithValue("@BTRegisterID", sp.GetNull(BTExpenseID))
                    sqlcm.Parameters.AddWithValue("@value", sp.GetNull(Value))
                    sqlcm.Parameters.AddWithValue("@createdBy", sp.GetNull(CreatedBy))
                    sqlcm.Parameters.AddWithValue("@Reason", sp.GetNull(Reason))
                    sqlcm.Parameters.AddWithValue("@SupplierNo", sp.GetNull(supplierNo))
                    sqlcm.Parameters.AddWithValue("@SupplierSite", sp.GetNull(supplierSite))
                    sqlcm.Parameters.AddWithValue("@GLDate", sp.GetNull(glDate))
                    sqlcm.Parameters.AddWithValue("@batchName", sp.GetNull(batchName))
                    sqlcm.Parameters.AddWithValue("@invoiceDate", sp.GetNull(invoiceDate))
                    sqlcm.Parameters.AddWithValue("@extAmount", sp.GetNull(extAmount))
                    sqlcm.Parameters.AddWithValue("@extExrate", sp.GetNull(extExrate))
                    sqlcm.Parameters.AddWithValue("@creditExrate", sp.GetNull(creditExrate))
                    Dim adapter As New SqlDataAdapter(sqlcm)
                    Dim dt As New DataTable()
                    adapter.Fill(dt)
                    Return If(dt.Rows.Count > 0, dt.Rows(0), dt.NewRow()) 'CommonFunction._ToString(sqlcm.ExecuteScalar())                    
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

        Public Shared Function tbl_BT_Expense_CheckOracleStatus(ByVal BTExpenseID As Integer) As Boolean
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, "tbl_BT_Expense_CheckStatusFI", BTExpenseID)) = 1
        End Function

        Public Shared Function GetOraInvoiceStatus(ByVal btIDs As String) As DataTable
            'Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "GetExpenseOraInvoiceStatus", btIDs).Tables(0)
            Dim sp As New SqlDataProvider()
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "GetExpenseOraInvoiceStatus"
                    sqlcm.Parameters.AddWithValue("@btID", sp.GetNull(btIDs))
                    Dim dtData As New DataTable()
                    Dim adapter As New SqlDataAdapter(sqlcm)
                    adapter.Fill(dtData)
                    Return dtData
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

        '
        Public Shared Sub BTExpense_Update(ByVal obj As tblBTExpenseInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_Expense_Update", _
                                      sp.GetNull(obj.BTExpenseID), _
                                      sp.GetNull(obj.IsSubmited), _
                                      sp.GetNull(obj.DepartureDate), _
                                      sp.GetNull(obj.ReturnDate), _
                                      sp.GetNull(obj.Purpose), _
                                      sp.GetNull(obj.ExchangeDate), _
                                      sp.GetNull(obj.ModifiedBy), _
                                      sp.GetNull(obj.IsMovingTimeAllowance), _
                                      sp.GetNull(obj.MovingTimeAllowanceVND), _
                                      sp.GetNull(obj.SubmitComment), _
                                      sp.GetNull(obj.BudgetCode), _
                                      sp.GetNull(obj.BudgetName), _
                                      sp.GetNull(obj.ProjectBudgetCode), _
                                      sp.GetNull(obj.CheckAllBudget), _
                                      sp.GetNull(obj.IsFirstTimeOverSea))
        End Sub
        '
        Public Shared Function BTExpense_GetByID(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Expense_GetByID", id).Tables(0)
        End Function

        Public Shared Function BTExpense_GetSummary(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BTExpense_GetSummary", id).Tables(0)
        End Function

        Public Shared Sub BTExpenseApproval_Insert(ByVal BTExpenseID As Integer, ByVal CreatedBy As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, "tbl_BT_Expense_Insert", _
                                            sp.GetNull(BTExpenseID), sp.GetNull(CreatedBy))
        End Sub

        Public Shared Function tbl_BT_Expense_SearchForView(ByVal btID As Integer, ByVal username As String) As DataTable
            Dim objPro As SqlDataProvider = New SqlDataProvider
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Expense_SearchForView", btID, objPro.GetNull(username)).Tables(0)
        End Function

        Public Shared Function tbl_BT_Expense_ViewByID(ByVal btID As Integer, ByVal username As String) As DataTable
            Dim objPro As SqlDataProvider = New SqlDataProvider
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Expense_ViewByID", btID, objPro.GetNull(username)).Tables(0)
        End Function

        Public Shared Function tbl_BT_Expense_Search(ByVal BTTypeID As String, ByVal LocationID As Integer, _
                                                 ByVal DivID As Integer, ByVal DepID As Integer, ByVal SecID As Integer, _
                                                 ByVal GropID As Integer, ByVal FullName As String, ByVal EmployeeCode As String, _
                                                 ByVal FromDate As Date, ByVal ToDate As Date, ByVal BudgetCode As String, ByVal username As String, Optional ByVal btNo As String = "", Optional ByVal btsStatus As String = "") As DataTable
            Dim objPro As SqlDataProvider = New SqlDataProvider
            'Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Expense_Search", _
            '                                objPro.GetNull(BTTypeID), _
            '                                objPro.GetNull(LocationID), _
            '                                objPro.GetNull(DivID), _
            '                                objPro.GetNull(DepID), _
            '                                objPro.GetNull(SecID), _
            '                                objPro.GetNull(GropID), _
            '                                objPro.GetNull(FullName), _
            '                                objPro.GetNull(EmployeeCode), _
            '                                objPro.GetNull(FromDate), _
            '                                objPro.GetNull(ToDate), _
            '                                objPro.GetNull(BudgetCode), _
            '                                "", _
            '                                objPro.GetNull(btNo), _
            '                                objPro.GetNull(username), "").Tables(0)
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "tbl_BT_Expense_Search"
                    sqlcm.Parameters.AddWithValue("@BTTypeID", objPro.GetNull(BTTypeID))
                    sqlcm.Parameters.AddWithValue("@LocationID", objPro.GetNull(LocationID))
                    sqlcm.Parameters.AddWithValue("@DivID", objPro.GetNull(DivID))
                    sqlcm.Parameters.AddWithValue("@DepID", objPro.GetNull(DepID))
                    sqlcm.Parameters.AddWithValue("@SecID", objPro.GetNull(SecID))
                    sqlcm.Parameters.AddWithValue("@GropID", objPro.GetNull(GropID))
                    sqlcm.Parameters.AddWithValue("@FullName", objPro.GetNull(FullName))
                    sqlcm.Parameters.AddWithValue("@EmployeeCode", objPro.GetNull(EmployeeCode))
                    sqlcm.Parameters.AddWithValue("@FromDate", objPro.GetNull(FromDate))
                    sqlcm.Parameters.AddWithValue("@ToDate", objPro.GetNull(ToDate))
                    sqlcm.Parameters.AddWithValue("@BudgetCode", objPro.GetNull(BudgetCode))
                    sqlcm.Parameters.AddWithValue("@TimeKeeper", "")
                    sqlcm.Parameters.AddWithValue("@BTNo", objPro.GetNull(btNo))
                    sqlcm.Parameters.AddWithValue("@username", objPro.GetNull(username))
                    sqlcm.Parameters.AddWithValue("@btType", "")
                    sqlcm.Parameters.AddWithValue("@btsStatus", objPro.GetNull(btsStatus))
                    Dim dtData As New DataTable()
                    Dim adapter As New SqlDataAdapter(sqlcm)
                    adapter.Fill(dtData)
                    Return dtData
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

        Public Shared Function tbl_BT_Expense_SearchFI(ByVal BTTypeID As String, ByVal LocationID As Integer, _
                                                 ByVal DivID As Integer, ByVal DepID As Integer, ByVal SecID As Integer, _
                                                 ByVal GropID As Integer, ByVal FullName As String, ByVal EmployeeCode As String, _
                                                 ByVal FromDate As Date, ByVal ToDate As Date, ByVal BudgetCode As String, ByVal username As String, Optional ByVal btNo As String = "", Optional ByVal btsStatus As String = "") As DataTable
            Dim objPro As SqlDataProvider = New SqlDataProvider           
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "tbl_BT_Expense_SearchFI"
                    sqlcm.Parameters.AddWithValue("@BTTypeID", objPro.GetNull(BTTypeID))
                    sqlcm.Parameters.AddWithValue("@LocationID", objPro.GetNull(LocationID))
                    sqlcm.Parameters.AddWithValue("@DivID", objPro.GetNull(DivID))
                    sqlcm.Parameters.AddWithValue("@DepID", objPro.GetNull(DepID))
                    sqlcm.Parameters.AddWithValue("@SecID", objPro.GetNull(SecID))
                    sqlcm.Parameters.AddWithValue("@GropID", objPro.GetNull(GropID))
                    sqlcm.Parameters.AddWithValue("@FullName", objPro.GetNull(FullName))
                    sqlcm.Parameters.AddWithValue("@EmployeeCode", objPro.GetNull(EmployeeCode))
                    sqlcm.Parameters.AddWithValue("@FromDate", objPro.GetNull(FromDate))
                    sqlcm.Parameters.AddWithValue("@ToDate", objPro.GetNull(ToDate))
                    sqlcm.Parameters.AddWithValue("@BudgetCode", objPro.GetNull(BudgetCode))                    
                    sqlcm.Parameters.AddWithValue("@BTNo", objPro.GetNull(btNo))
                    sqlcm.Parameters.AddWithValue("@username", objPro.GetNull(username))                    
                    sqlcm.Parameters.AddWithValue("@btsStatus", objPro.GetNull(btsStatus))
                    Dim dtData As New DataTable()
                    Dim adapter As New SqlDataAdapter(sqlcm)
                    adapter.Fill(dtData)
                    Return dtData
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

        Public Shared Function tbl_BT_Expense_Search(ByVal BTTypeID As String, ByVal FullName As String, ByVal EmployeeCode As String, ByVal BudgetCode As String, ByVal TimeKeeper As String, ByVal BTNo As String, ByVal departureFrom As DateTime, ByVal departureTo As DateTime, ByVal btType As String) As DataTable
            Dim objPro As SqlDataProvider = New SqlDataProvider
            'Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Expense_Search", _
            '                                objPro.GetNull(BTTypeID), -1, -1, -1, -1, -1, _
            '                                objPro.GetNull(FullName), _
            '                                objPro.GetNull(EmployeeCode), _
            '                                objPro.GetNull(departureFrom), _
            '                                objPro.GetNull(departureTo), _
            '                                objPro.GetNull(BudgetCode), _
            '                                objPro.GetNull(TimeKeeper), _
            '                                objPro.GetNull(BTNo), "", _
            '                                objPro.GetNull(btType)).Tables(0)
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "tbl_BT_Expense_Search"
                    sqlcm.Parameters.AddWithValue("@BTTypeID", objPro.GetNull(BTTypeID))
                    sqlcm.Parameters.AddWithValue("@LocationID", -1)
                    sqlcm.Parameters.AddWithValue("@DivID", -1)
                    sqlcm.Parameters.AddWithValue("@DepID", -1)
                    sqlcm.Parameters.AddWithValue("@SecID", -1)
                    sqlcm.Parameters.AddWithValue("@GropID", -1)
                    sqlcm.Parameters.AddWithValue("@FullName", objPro.GetNull(FullName))
                    sqlcm.Parameters.AddWithValue("@EmployeeCode", objPro.GetNull(EmployeeCode))
                    sqlcm.Parameters.AddWithValue("@FromDate", objPro.GetNull(departureFrom))
                    sqlcm.Parameters.AddWithValue("@ToDate", objPro.GetNull(departureTo))
                    sqlcm.Parameters.AddWithValue("@BudgetCode", objPro.GetNull(BudgetCode))
                    sqlcm.Parameters.AddWithValue("@TimeKeeper", objPro.GetNull(TimeKeeper))
                    sqlcm.Parameters.AddWithValue("@BTNo", objPro.GetNull(BTNo))
                    sqlcm.Parameters.AddWithValue("@username", "")
                    sqlcm.Parameters.AddWithValue("@btType", objPro.GetNull(btType))
                    Dim dtData As New DataTable()
                    Dim adapter As New SqlDataAdapter(sqlcm)
                    adapter.Fill(dtData)
                    Return dtData
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

        Public Shared Function GetExrate(ByVal fromCurrency As String, ByVal toCurrency As String, Optional ByVal exchangeDate As DateTime = Nothing) As Decimal
            Dim sp As SqlDataProvider = New SqlDataProvider
            Dim exrate As Decimal = CommonFunction._ToMoney(SqlHelper.ExecuteScalar(New Connections().SqlConn, "GetExrate", _
                                                                                    sp.GetNull(fromCurrency), _
                                                                                    sp.GetNull(toCurrency), _
                                                                                    sp.GetNull(exchangeDate)))
            If exrate < 1 Then
                exrate = 1
            End If
            Return exrate
        End Function

        Public Shared Function BTExpense_CheckOvernightBTDate(ByVal departureDate As DateTime, ByVal returnDate As DateTime, ByVal btid As Integer) As DataSet
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_Expense_CheckOvernightBTDate", _
                                            sp.GetNull(departureDate), _
                                            sp.GetNull(returnDate), _
                                            sp.GetNull(btid))
        End Function

        Public Shared Function CheckExtInvoice(ByVal btId As Integer) As DataRow
            Dim sp As SqlDataProvider = New SqlDataProvider
            Dim dtData As DataTable = SqlHelper.ExecuteDataset(New Connections().SqlConn, "CheckExtInvoice", sp.GetNull(btId)).Tables(0)
            Return If(dtData IsNot Nothing, dtData.Rows(0), dtData.NewRow())
        End Function
#End Region

#Region "tbl_BT_Expense_Request"
        Public Shared Sub BTExpenseRequest_Insert(ByVal obj As tblBTExpenseRequestInfo, Optional ByVal isMovingTimeAllowance As Boolean = False)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                    "tbl_BT_Expense_Request_Insert", _
                                    sp.GetNull(obj.BTExpenseID), _
                                    sp.GetNull(obj.Purpose), _
                                    sp.GetNull(obj.DDate), _
                                    sp.GetNull(obj.BreakfastAmount), _
                                    sp.GetNull(obj.LunchAmount), _
                                    sp.GetNull(obj.DinnerAmount), _
                                    sp.GetNull(obj.OtherAmount), _
                                    sp.GetNull(obj.Remark), _
                                    sp.GetNull(obj.AllowanceCurrency), _
                                    sp.GetNull(obj.AllowanceExrate), _
                                    sp.GetNull(obj.HotelAmount), _
                                    sp.GetNull(obj.HotelCurrency), _
                                    sp.GetNull(obj.HotelExrate), _
                                    sp.GetNull(obj.CreatedBy), _
                                    sp.GetNull(obj.CreditAmount), _
                                    sp.GetNull(obj.HotelCreditAmount), _
                                    sp.GetNull(obj.HotelExdate), _
                                    sp.GetNull(obj.DestinationID), _
                                    sp.GetNull(obj.oFromDate), _
                                    sp.GetNull(obj.oToDate), _
                                    sp.GetNull(obj.oTaxiTime), _
                                    sp.GetNull(obj.oMotobikeTime), _
                                    sp.GetNull(obj.oTaxiAmount), _
                                    sp.GetNull(obj.oMotobikeAmount), _
                                    sp.GetNull(obj.oCarRequest), _
                                    sp.GetNull(obj.oAirTicketRequest), _
                                    sp.GetNull(obj.oTrainTicketRequest), _
                                    sp.GetNull(isMovingTimeAllowance), _
                                    sp.GetNull(obj.oTaxiDesc), _
                                    sp.GetNull(obj.oMotobikeDesc))

        End Sub

        Public Shared Sub BTExpenseRequest_Update(ByVal obj As tblBTExpenseRequestInfo, Optional ByVal isMovingTimeAllowance As Boolean = False)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                    "tbl_BT_Expense_Request_Update", _
                                    sp.GetNull(obj.ID), _
                                    sp.GetNull(obj.Purpose), _
                                    sp.GetNull(obj.DDate), _
                                    sp.GetNull(obj.BreakfastAmount), _
                                    sp.GetNull(obj.LunchAmount), _
                                    sp.GetNull(obj.DinnerAmount), _
                                    sp.GetNull(obj.OtherAmount), _
                                    sp.GetNull(obj.Remark), _
                                    sp.GetNull(obj.AllowanceCurrency), _
                                    sp.GetNull(obj.AllowanceExrate), _
                                    sp.GetNull(obj.HotelAmount), _
                                    sp.GetNull(obj.HotelCurrency), _
                                    sp.GetNull(obj.HotelExrate), _
                                    sp.GetNull(obj.ModifiedBy), _
                                    sp.GetNull(obj.CreditAmount), _
                                    sp.GetNull(obj.HotelCreditAmount), _
                                    sp.GetNull(obj.HotelExdate), _
                                    sp.GetNull(obj.DestinationID), _
                                    sp.GetNull(obj.oFromDate), _
                                    sp.GetNull(obj.oToDate), _
                                    sp.GetNull(obj.oTaxiTime), _
                                    sp.GetNull(obj.oMotobikeTime), _
                                    sp.GetNull(obj.oTaxiAmount), _
                                    sp.GetNull(obj.oMotobikeAmount), _
                                    sp.GetNull(obj.oCarRequest), _
                                    sp.GetNull(obj.oAirTicketRequest), _
                                    sp.GetNull(obj.oTrainTicketRequest), _
                                    sp.GetNull(isMovingTimeAllowance), _
                                    sp.GetNull(obj.oTaxiDesc), _
                                    sp.GetNull(obj.oMotobikeDesc))

        End Sub

        Public Shared Function BTExpenseRequest_Search(ByVal btID As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Expense_Request_Search", btID).Tables(0)
        End Function

        Public Shared Function BTExpenseRequest_GetByID(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Expense_Request_GetByID", id).Tables(0)
        End Function

        Public Shared Sub BTExpenseRequest_Delete(ByVal ids As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_Expense_Request_Delete", _
                                      sp.GetNull(ids))
        End Sub

        Public Shared Function BTExpenseRequest_IsBTTimeConflict(ByVal btID As Integer, _
                                                                 ByVal requestID As Integer, _
                                                                 ByVal pdate As DateTime) As Boolean
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                            "tbl_BT_Expense_CheckBTTime", _
                                            sp.GetNull(btID), _
                                            sp.GetNull(requestID), _
                                            sp.GetNull(pdate))) > 0
        End Function

        Public Shared Function BTExpenseRequest_IsOneDayBTTimeConflict(ByVal btID As Integer, _
                                                                         ByVal requestID As Integer, _
                                                                         ByVal fromDate As DateTime, _
                                                                         ByVal toDate As DateTime) As Boolean
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                            "tbl_BT_Expense_CheckOneDayBTTime", _
                                            sp.GetNull(btID), _
                                            sp.GetNull(requestID), _
                                            sp.GetNull(fromDate), _
                                            sp.GetNull(toDate))) > 0
        End Function

        Public Shared Function BTExpenseRequest_CheckMovingTimeOneDay(ByVal requestID As Integer, _
                                                                      ByVal btID As Integer, _
                                                                 ByVal fromDate As DateTime) As Boolean
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                            "tbl_BT_Expense_CheckMovingTimeOneDay", _
                                            sp.GetNull(requestID), _
                                            sp.GetNull(btID), _
                                            sp.GetNull(fromDate))) = 0
        End Function
#End Region

#Region "tbl_BT_Expense_Other"
        Public Shared Sub BTExpenseOther_Insert(ByVal obj As tblBTExpenseOtherInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                    "tbl_BT_Expense_Other_Insert", _
                                    sp.GetNull(obj.BTExpenseID), _
                                    sp.GetNull(obj.DDate), _
                                    sp.GetNull(obj.Expense), _
                                    sp.GetNull(obj.Amount), _
                                    sp.GetNull(obj.Currency), _
                                    sp.GetNull(obj.Exrate), _
                                    sp.GetNull(obj.CreatedBy), _
                                    sp.GetNull(obj.CreditAmount))
        End Sub

        Public Shared Sub BTExpenseOther_Update(ByVal obj As tblBTExpenseOtherInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                    "tbl_BT_Expense_Other_Update", _
                                    sp.GetNull(obj.ID), _
                                    sp.GetNull(obj.DDate), _
                                    sp.GetNull(obj.Expense), _
                                    sp.GetNull(obj.Amount), _
                                    sp.GetNull(obj.Currency), _
                                    sp.GetNull(obj.Exrate), _
                                    sp.GetNull(obj.ModifiedBy), _
                                    sp.GetNull(obj.CreditAmount))
        End Sub

        Public Shared Function BTExpenseOther_Search(ByVal btID As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Expense_Other_Search", btID).Tables(0)
        End Function

        Public Shared Function BTExpenseOther_GetByID(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Expense_Other_GetByID", id).Tables(0)
        End Function

        Public Shared Sub BTExpenseOther_Delete(ByVal ids As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_Expense_Other_Delete", _
                                      sp.GetNull(ids))
        End Sub
#End Region

#Region "tbl_BT_Expense_Schedule"
        Public Shared Sub BTExpenseSchedule_Insert(ByVal obj As tblBTExpenseScheduleInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_Expense_Schedule_Insert", _
                                      sp.GetNull(obj.BTExpenseID), _
                                      sp.GetNull(obj.FromTime), _
                                      sp.GetNull(obj.ToTime), _
                                      sp.GetNull(obj.WorkingArea), _
                                      sp.GetNull(obj.Task), _
                                      sp.GetNull(obj.CreatedBy), _
                                      sp.GetNull(obj.TransportationFee))

        End Sub

        Public Shared Sub BTExpenseSchedule_Update(ByVal obj As tblBTExpenseScheduleInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_Expense_Schedule_Update", _
                                      sp.GetNull(obj.ID), _
                                      sp.GetNull(obj.FromTime), _
                                      sp.GetNull(obj.ToTime), _
                                      sp.GetNull(obj.WorkingArea), _
                                      sp.GetNull(obj.Task), _
                                      sp.GetNull(obj.ModifiedBy), _
                                      sp.GetNull(obj.TransportationFee))

        End Sub

        Public Shared Function BTExpenseSchedule_Search(ByVal btID As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Expense_Schedule_Search", btID).Tables(0)
        End Function

        Public Shared Function BTExpenseSchedule_GetByID(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Expense_Schedule_GetByID", id).Tables(0)
        End Function

        Public Shared Sub BTExpenseSchedule_Delete(ByVal ids As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_Expense_Schedule_Delete", _
                                      sp.GetNull(ids))

        End Sub
#End Region

#Region "tbl_BT_History"
        Public Shared Function BTExpenseHistory_Search(ByVal btID As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BTExpenseHistory_Search", btID).Tables(0)
        End Function
#End Region

    End Class
End Namespace

