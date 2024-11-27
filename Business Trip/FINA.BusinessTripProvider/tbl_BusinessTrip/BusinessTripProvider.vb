Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Security.Cryptography
Imports System.Text

Namespace Provider
    Public Class BusinessTripProvider

#Region "DATA TABLE"
        'Bang tbl_BT_Register
        Public Shared Function tbl_BT_Register_GetAllByUserName(ByVal EmployeeCode As String, ByVal StatusID As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Registe_GetByEmpHR", EmployeeCode, StatusID).Tables(0)
        End Function

        Public Shared Sub tbl_BT_Register_UpdateStatusHR(ByVal BTRegisterID As Integer, ByVal Value As String, ByVal CreatedBy As String, ByVal Reason As String)
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, "tbl_BT_Register_UpdateStatusHR", BTRegisterID, Value, CreatedBy, Reason)
        End Sub

        Public Shared Sub tbl_BT_Register_DeleteRequestByBT(ByVal BTRegisterID As Integer)
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, "tbl_BT_Register_DeleteRequestByBT", BTRegisterID)
        End Sub

        Public Shared Function tbl_BT_Register_Recall(ByVal BTRegisterID As Integer, ByVal modifiedBy As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                      "tbl_BT_Register_Recall", _
                                      sp.GetNull(BTRegisterID), _
                                      sp.GetNull(modifiedBy))).Trim()
        End Function

        Public Shared Function tbl_BT_Register_UpdateStatusFI(ByVal BTRegisterID As Integer, _
                                                         ByVal Value As String, _
                                                         ByVal CreatedBy As String, _
                                                         ByVal Reason As String, _
                                                         Optional ByVal supplierNo As String = Nothing, _
                                                         Optional ByVal supplierSite As String = Nothing, _
                                                         Optional ByVal glDate As DateTime = Nothing, _
                                                         Optional ByVal batchName As Integer = Nothing, _
                                                         Optional ByVal invoiceDate As Date = Nothing) As DataRow
            Dim sp As New SqlDataProvider()
            'Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
            '        "tbl_BT_Register_UpdateStatusFI", _
            '        sp.GetNull(BTRegisterID), _
            '        sp.GetNull(Value), _
            '        sp.GetNull(CreatedBy), _
            '        sp.GetNull(Reason), _
            '        sp.GetNull(supplierNo), _
            '        sp.GetNull(supplierSite), _
            '        sp.GetNull(glDate), _
            '        sp.GetNull(batchName), _
            '        sp.GetNull(invoiceDate)))
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "tbl_BT_Register_UpdateStatusFI"
                    sqlcm.Parameters.AddWithValue("@BTRegisterID", sp.GetNull(BTRegisterID))
                    sqlcm.Parameters.AddWithValue("@value", sp.GetNull(Value))
                    sqlcm.Parameters.AddWithValue("@createdBy", sp.GetNull(CreatedBy))
                    sqlcm.Parameters.AddWithValue("@Reason", sp.GetNull(Reason))
                    sqlcm.Parameters.AddWithValue("@SupplierNo", sp.GetNull(supplierNo))
                    sqlcm.Parameters.AddWithValue("@SupplierSite", sp.GetNull(supplierSite))
                    sqlcm.Parameters.AddWithValue("@GLDate", sp.GetNull(glDate))
                    sqlcm.Parameters.AddWithValue("@BatchName", sp.GetNull(batchName))
                    sqlcm.Parameters.AddWithValue("@invoiceDate", sp.GetNull(invoiceDate))
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

        Public Shared Function check_Supplier_No(ByVal BTRegisterID As Integer, ByVal paymentType As String) As DataSet
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "checkSupplier_No", BTRegisterID, paymentType)
        End Function

        Public Shared Function UpdateMovingTime(ByVal BTRegisterID As Integer, _
                                                ByVal chkMovingTime As Boolean) As DataRow
            Dim dtData As DataTable = SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_Register_UpdateMovingTime", _
                                            BTRegisterID, chkMovingTime).Tables(0)
            Return If(dtData.Rows.Count > 0, dtData.Rows(0), Nothing)
        End Function

        Public Shared Function UpdateFirstTimeOversea(ByVal BTRegisterID As Integer, _
                                                ByVal chkFirstime As Boolean) As DataRow
            Dim dtData As DataTable = SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_Register_UpdateFirstTime", _
                                            BTRegisterID, chkFirstime).Tables(0)
            Return If(dtData.Rows.Count > 0, dtData.Rows(0), Nothing)
        End Function

        Public Shared Sub tbl_BT_Register_UpdateStatusGA(ByVal BTRegisterID As Integer, ByVal Value As String, ByVal CreatedBy As String, ByVal Reason As String)
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, "tbl_BT_Register_UpdateStatusGA", BTRegisterID, Value, CreatedBy, Reason)
        End Sub


        Public Shared Function tbl_BT_Register_SearchGA(ByVal BTTypeID As String, ByVal LocationID As Integer, _
                                                 ByVal DivID As Integer, ByVal DepID As Integer, ByVal SecID As Integer, _
                                                 ByVal GropID As Integer, ByVal FullName As String, ByVal EmployeeCode As String, _
                                                 ByVal FromDate As Date, ByVal ToDate As Date, ByVal Dep As String, _
                                                 ByVal Submit As Integer, ByVal BudgetCode As String, ByVal username As String, _
                                                 ByVal budgetChecked As Boolean) As DataTable
            Dim objPro As SqlDataProvider = New SqlDataProvider
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "tbl_BT_Register_SearchGA"
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
                    sqlcm.Parameters.AddWithValue("@Dep", objPro.GetNull(Dep))
                    sqlcm.Parameters.AddWithValue("@Submit", objPro.GetNull(Submit))
                    sqlcm.Parameters.AddWithValue("@BudgetCode", objPro.GetNull(BudgetCode))                    
                    sqlcm.Parameters.AddWithValue("@username", objPro.GetNull(username))
                    sqlcm.Parameters.AddWithValue("@budgetChecked", objPro.GetNull(budgetChecked))
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

        Public Shared Function tbl_BT_Register_Search(ByVal BTTypeID As String, _
                                                      ByVal FullName As String, _
                                                      ByVal EmployeeCode As String, _
                                                      ByVal BudgetCode As String, _
                                                      ByVal TimeKeeper As String, _
                                                      ByVal BTNo As String, _
                                                      ByVal departureFrom As DateTime, _
                                                      ByVal departureTo As DateTime, _
                                                      ByVal btType As String) As DataTable
            Dim objPro As SqlDataProvider = New SqlDataProvider
            'Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Register_Search", _
            '                                objPro.GetNull(BTTypeID), _
            '                                -1, -1, -1, -1, -1, _
            '                                objPro.GetNull(FullName), _
            '                                objPro.GetNull(EmployeeCode), _
            '                                objPro.GetNull(departureFrom), _
            '                                objPro.GetNull(departureTo), "", "", -1, _
            '                                objPro.GetNull(BudgetCode), _
            '                                objPro.GetNull(TimeKeeper), _
            '                                objPro.GetNull(BTNo), _
            '                                objPro.GetNull(btType), "", False).Tables(0)
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "tbl_BT_Register_Search"
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
                    sqlcm.Parameters.AddWithValue("@Dep", "")
                    sqlcm.Parameters.AddWithValue("@Submit", -1)
                    sqlcm.Parameters.AddWithValue("@BudgetCode", objPro.GetNull(BudgetCode))
                    sqlcm.Parameters.AddWithValue("@TimeKeeper", objPro.GetNull(TimeKeeper))
                    sqlcm.Parameters.AddWithValue("@BTNo", objPro.GetNull(BTNo))
                    sqlcm.Parameters.AddWithValue("@BTType", objPro.GetNull(btType))                    
                    sqlcm.Parameters.AddWithValue("@budgetChecked", False)                    
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
        Public Shared Function tbl_BT_Register_SearchFI(ByVal BTTypeID As String, ByVal LocationID As Integer, _
                                                 ByVal DivID As Integer, ByVal DepID As Integer, ByVal SecID As Integer, _
                                                 ByVal GropID As Integer, ByVal FullName As String, ByVal EmployeeCode As String, _
                                                 ByVal FromDate As Date, ByVal ToDate As Date, ByVal Dep As String, _
                                                 ByVal Submit As Integer, ByVal BudgetCode As String, ByVal username As String, _
                                                 ByVal budgetChecked As Boolean, _
                                                 Optional ByVal btNo As String = "", Optional ByVal btsStatus As String = "") As DataTable
            Dim objPro As SqlDataProvider = New SqlDataProvider            
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "tbl_BT_Register_Search_FI"
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
                    sqlcm.Parameters.AddWithValue("@Dep", objPro.GetNull(Dep))
                    sqlcm.Parameters.AddWithValue("@Submit", objPro.GetNull(Submit))
                    sqlcm.Parameters.AddWithValue("@BudgetCode", objPro.GetNull(BudgetCode))                    
                    sqlcm.Parameters.AddWithValue("@BTNo", objPro.GetNull(btNo))                    
                    sqlcm.Parameters.AddWithValue("@username", objPro.GetNull(username))
                    sqlcm.Parameters.AddWithValue("@budgetChecked", objPro.GetNull(budgetChecked))
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

        Public Shared Function tbl_BT_Register_SearchBudget(ByVal BTTypeID As String, ByVal LocationID As Integer, _
                                                 ByVal DivID As Integer, ByVal DepID As Integer, ByVal SecID As Integer, _
                                                 ByVal GropID As Integer, ByVal FullName As String, ByVal EmployeeCode As String, _
                                                 ByVal FromDate As Date, ByVal ToDate As Date, ByVal Dep As String, _
                                                 ByVal Submit As Integer, ByVal BudgetCode As String, ByVal username As String, _
                                                 ByVal budgetChecked As Boolean, _
                                                 Optional ByVal btNo As String = "", Optional ByVal btsStatus As String = "") As DataTable
            Dim objPro As SqlDataProvider = New SqlDataProvider
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "tbl_BT_Register_Search_Budget"
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
                    sqlcm.Parameters.AddWithValue("@Dep", objPro.GetNull(Dep))
                    sqlcm.Parameters.AddWithValue("@Submit", objPro.GetNull(Submit))
                    sqlcm.Parameters.AddWithValue("@BudgetCode", objPro.GetNull(BudgetCode))
                    sqlcm.Parameters.AddWithValue("@BTNo", objPro.GetNull(btNo))
                    sqlcm.Parameters.AddWithValue("@username", objPro.GetNull(username))
                    sqlcm.Parameters.AddWithValue("@budgetChecked", objPro.GetNull(budgetChecked))
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

        Public Shared Function tbl_BT_Register_SearchForView(ByVal btID As Integer, ByVal username As String) As DataTable
            Dim objPro As SqlDataProvider = New SqlDataProvider
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Register_SearchForView", btID, objPro.GetNull(username)).Tables(0)
        End Function

        Public Shared Function tbl_BT_Register_ViewByID(ByVal btID As Integer, ByVal username As String) As DataTable
            Dim objPro As SqlDataProvider = New SqlDataProvider
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Register_ViewByID", btID, objPro.GetNull(username)).Tables(0)
        End Function

        'Insert vao bang Tas_Set_Rest ben HR
        Public Shared Function InsTasSetRest(ByVal BTRegisterID As Integer) As Integer
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, "tbl_Tas_SetRest_Insert", BTRegisterID)
        End Function

        Public Shared Function DeleteTasSetRest(ByVal BTRegisterID As Integer) As Integer
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, "tbl_Tas_SetRest_Delete", BTRegisterID)
        End Function

        'Public Shared Function InsTas_SetRest(ByVal EmployeeCode As String, ByVal BTRegisterID As Integer) As Integer
        '    Return SqlHelper.ExecuteScalar(New Connections().SqlConn, "tbl_Tas_SetRest_Insert", EmployeeCode, BTRegisterID)
        'End Function

        'Bang tbl_BT_Register_Request
        Public Shared Function tbl_BT_Request_GetAllByUserName(ByVal EmployeeCode As String, ByVal StatusID As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Register_Request_GetByEmpHR", EmployeeCode, StatusID).Tables(0)
        End Function

        'Bang Business Trip Type
        Public Shared Function tbl_BT_Type()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "BusinessType_GetAll").Tables(0)
        End Function


        'Bang Division
        Public Shared Function tbl_DivisionGetAll()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "Division_GetAll").Tables(0)
        End Function

        'Bang Department
        Public Shared Function tbl_DepartmentGetByDivID(ByVal DivisionID As Integer)
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "Department_GetByDivID", DivisionID).Tables(0)
        End Function

        'Bang Section
        Public Shared Function tbl_SectionGetByDepID(ByVal DivisionID As Integer, ByVal DepartmentID As Integer)
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "Section_GetByDepID", DivisionID, DepartmentID).Tables(0)
        End Function

        'Bang Group
        Public Shared Function tbl_GroupGetBySecID(ByVal DivisionID As Integer, ByVal DepartmentID As Integer, ByVal SecID As Integer)
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "Group_GetBySecID", DivisionID, DepartmentID, SecID).Tables(0)
        End Function

        'Bang Branch
        Public Shared Function tbl_BranchGetAll()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "Location_GetAll").Tables(0)
        End Function

        Public Shared Function tbl_BT_Register_ClearRequest(ByVal btID As Integer, ByVal username As String) As String
            Dim objPro As SqlDataProvider = New SqlDataProvider
            'Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, "tbl_BT_Register_ClearRequest", btID, objPro.GetNull(username))).Trim()
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "tbl_BT_Register_ClearRequest"
                    sqlcm.Parameters.AddWithValue("@btID", objPro.GetNull(btID))
                    sqlcm.Parameters.AddWithValue("@username", objPro.GetNull(username))
                    Return CommonFunction._ToString(sqlcm.ExecuteScalar()).Trim()
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

        Public Shared Function tbl_BT_Register_CancelBT(ByVal btID As Integer, ByVal username As String) As String
            Dim objPro As SqlDataProvider = New SqlDataProvider
            'Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, "tbl_BT_Register_CancelBT", btID, objPro.GetNull(username))).Trim()
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "tbl_BT_Register_CancelBT"
                    sqlcm.Parameters.AddWithValue("@btID", objPro.GetNull(btID))
                    sqlcm.Parameters.AddWithValue("@username", objPro.GetNull(username))
                    Return CommonFunction._ToString(sqlcm.ExecuteScalar()).Trim()
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

#End Region

#Region "tbl_BT_Register"
        Public Shared Function CheckOraGLDate(ByVal glDate As DateTime) As Boolean
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, "ORA_CheckGLDate", glDate)) > 0
        End Function

        Public Shared Function GetOraInvoiceStatus(ByVal btIDs As String) As DataTable
            'Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "GetOraInvoiceStatus", btIDs).Tables(0)
            Dim sp As New SqlDataProvider()
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "GetOraInvoiceStatus"
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

        Public Shared Function BTRegister_CheckBudgetRemaining(ByVal budgetCode As Integer, ByVal btID As Integer) As Decimal
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToDecimal(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "tbl_BT_Register_CheckBudgetRemaining", _
                                           sp.GetNull(budgetCode), _
                                           sp.GetNull(btID)))
        End Function
        '
        Public Shared Function BTRegister_CheckExpenseBudgetRemaining(ByVal budgetCode As Integer, ByVal btID As Integer) As Decimal
            Dim sp As New SqlDataProvider()

            Return CommonFunction._ToDecimal(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "tbl_BT_Register_CheckExpenseBudgetRemaining", _
                                           sp.GetNull(budgetCode), _
                                           sp.GetNull(btID)))
        End Function
        '
        Public Shared Function BTRegister_GetBudgetRemaining(ByVal budgetCode As Integer, ByVal btID As Integer) As Decimal
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToDecimal(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "tbl_BT_Register_GetBudgetRemainingAmount", _
                                           sp.GetNull(budgetCode), _
                                           sp.GetNull(btID)))
        End Function
        '
        Public Shared Function BTRegister_GetExpenseBudgetRemaining(ByVal budgetCode As Integer, ByVal btID As Integer) As Decimal
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToDecimal(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "tbl_BT_Register_GetExpenseBudgetRemainingAmount", _
                                           sp.GetNull(budgetCode), _
                                           sp.GetNull(btID)))
        End Function
        '
        Public Shared Function BTRegister_InsertTemp(ByVal createdBy As String) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "tbl_BT_Register_InsertTemp", _
                                           sp.GetNull(createdBy))
        End Function

        Public Shared Function BTRegister_Insert(ByVal obj As tblBTRegisterInfo, Optional ByVal isOneDayExpense As Integer = 0) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                      "tbl_BT_Register_Insert", _
                                      sp.GetNull(obj.EmployeeCode), _
                                      sp.GetNull(obj.EmployeeName), _
                                      sp.GetNull(obj.BTType), _
                                      sp.GetNull(obj.Location), _
                                      sp.GetNull(obj.LocationID), _
                                      sp.GetNull(obj.Division), _
                                      sp.GetNull(obj.DivisionID), _
                                      sp.GetNull(obj.Department), _
                                      sp.GetNull(obj.DepartmentID), _
                                      sp.GetNull(obj.Section), _
                                      sp.GetNull(obj.SectionID), _
                                      sp.GetNull(obj.Group), _
                                      sp.GetNull(obj.GroupID), _
                                      sp.GetNull(obj.Position), _
                                      sp.GetNull(obj.BudgetCode), _
                                      sp.GetNull(obj.BudgetName), _
                                      sp.GetNull(obj.Currency), _
                                      sp.GetNull(obj.PaymentType), _
                                      sp.GetNull(obj.Mobile), _
                                      sp.GetNull(obj.Email), _
                                      sp.GetNull(obj.CreatedBy), _
                                      sp.GetNull(obj.DepartureDate), _
                                      sp.GetNull(obj.ReturnDate), _
                                      sp.GetNull(obj.ProjectBudgetCode), _
                                      sp.GetNull(obj.CheckAllBudget), _
                                      sp.GetNull(obj.CountryCode), _
                                      sp.GetNull(isOneDayExpense), _
                                      sp.GetNull(obj.BudgetCodeID), _
                                      sp.GetNull(obj.AirTicket), _
                                      sp.GetNull(obj.TrainTicket), _
                                      sp.GetNull(obj.Car), _
                                      sp.GetNull(obj.ExpectedDepartureTime), _
                                      sp.GetNull(obj.ExpectedDepartureFlightNo), _
                                      sp.GetNull(obj.ExpectedReturnTime), _
                                      sp.GetNull(obj.ExpectedReturnFlightNo), _
                                      sp.GetNull(obj.NoRequestAdvance), _
                                      sp.GetNull(obj.DestinationID)).Tables(0)
        End Function

        Public Shared Sub BTRegister_Update(ByVal obj As tblBTRegisterInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_Register_Update", _
                                      sp.GetNull(obj.BTRegisterID), _
                                      sp.GetNull(obj.EmployeeCode), _
                                      sp.GetNull(obj.EmployeeName), _
                                      sp.GetNull(obj.BTType), _
                                      sp.GetNull(obj.Location), _
                                      sp.GetNull(obj.LocationID), _
                                      sp.GetNull(obj.Division), _
                                      sp.GetNull(obj.DivisionID), _
                                      sp.GetNull(obj.Department), _
                                      sp.GetNull(obj.DepartmentID), _
                                      sp.GetNull(obj.Section), _
                                      sp.GetNull(obj.SectionID), _
                                      sp.GetNull(obj.Group), _
                                      sp.GetNull(obj.GroupID), _
                                      sp.GetNull(obj.Position), _
                                      sp.GetNull(obj.BudgetCode), _
                                      sp.GetNull(obj.BudgetName), _
                                      sp.GetNull(obj.IsSubmited), _
                                      sp.GetNull(obj.Currency), _
                                      sp.GetNull(obj.PaymentType), _
                                      sp.GetNull(obj.ModifiedBy), _
                                      sp.GetNull(obj.RequestDate), _
                                      sp.GetNull(obj.IsFirstTimeOverSea), _
                                      sp.GetNull(obj.FirstTimeOverSeaVND), _
                                      sp.GetNull(obj.Mobile), _
                                      sp.GetNull(obj.Email), _
                                      sp.GetNull(obj.DepartureDate), _
                                      sp.GetNull(obj.ReturnDate), _
                                      sp.GetNull(obj.Purpose), _
                                      sp.GetNull(obj.IsMovingTimeAllowance), _
                                      sp.GetNull(obj.MovingTimeAllowanceVND), _
                                      sp.GetNull(obj.SubmitComment), _
                                      sp.GetNull(obj.ProjectBudgetCode), _
                                      sp.GetNull(obj.CheckAllBudget), _
                                      sp.GetNull(obj.BudgetCodeID), _
                                      sp.GetNull(obj.AirTicket), _
                                      sp.GetNull(obj.TrainTicket), _
                                      sp.GetNull(obj.Car), _
                                      sp.GetNull(obj.Wifi), _
                                      sp.GetNull(obj.ExpectedDepartureTime), _
                                      sp.GetNull(obj.ExpectedDepartureFlightNo), _
                                      sp.GetNull(obj.ExpectedReturnTime), _
                                      sp.GetNull(obj.ExpectedReturnFlightNo), _
                                      sp.GetNull(obj.NoRequestAdvance), _
                                      sp.GetNull(obj.DestinationID))
        End Sub
        '
        Public Shared Sub BTRegister_DeleteDraft(ByVal username As String)
            Dim sp As New SqlDataProvider()
            Dim dtAtt As DataTable = SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                      "tbl_BT_Register_DeleteDraft", _
                                      sp.GetNull(username)).Tables(0)
            DeleteAttFiles(dtAtt)
        End Sub

        Public Shared Sub BTRegister_Delete(ByVal ids As String)
            Dim sp As New SqlDataProvider()
            Dim dtAtt As DataTable = SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                      "tbl_BT_Register_Delete", _
                                      sp.GetNull(ids)).Tables(0)
            DeleteAttFiles(dtAtt)
        End Sub

        Public Shared Sub BTRegister_DeleteDraftByID(ByVal id As Integer)
            Dim sp As New SqlDataProvider()
            Dim dtAtt As DataTable = SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                      "tbl_BT_Register_DeleteDraftByID", id).Tables(0)
            DeleteAttFiles(dtAtt)
        End Sub

        Public Shared Function BTRegister_GetByID(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Register_GetByID", id).Tables(0)
        End Function

        Public Shared Function BTRegister_GetAuthorizedUsers(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Register_GetAuthorizedUsers", id).Tables(0)
        End Function

        Public Shared Function BTRegister_GetAuthorizedUsersByCode(ByVal employeeCode As String) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Register_GetAuthorizedUsersByCode", employeeCode).Tables(0)
        End Function

        Private Shared Sub DeleteAttFiles(ByVal dt As DataTable)
            For Each item As DataRow In dt.Rows
                Dim filePath As String = System.Web.HttpContext.Current.Server.MapPath(CommonFunction._ToString(item("AttachmentPath")))
                If IO.File.Exists(filePath) Then
                    IO.File.Delete(filePath)
                End If
            Next
        End Sub

        Public Shared Function BTRegister_GetSummary(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BTRegister_GetSummary", id).Tables(0)
        End Function

        Public Shared Function tbl_BT_Register_CheckOracleStatus(ByVal BTRegisterID As Integer) As Boolean
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, "tbl_BT_Register_CheckStatusFI", BTRegisterID)) = 1
        End Function

        Public Shared Function BTRegister_ConfirmBudget(ByVal obj As tblBTRegisterInfo, ByVal comment As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                      "tbl_BT_Register_ConfirmBudget", _
                                      sp.GetNull(obj.BTRegisterID), _
                                      sp.GetNull(obj.BudgetName), _
                                      sp.GetNull(obj.BudgetCode), _
                                      sp.GetNull(obj.ProjectBudgetCode), _
                                      sp.GetNull(comment), _
                                      sp.GetNull(obj.ModifiedBy), _
                                      sp.GetNull(obj.CheckAllBudget), _
                                      sp.GetNull(obj.BudgetCodeID))).Trim()
        End Function

        Public Shared Function BTRegister_ConfirmRequester(ByVal obj As tblBTRegisterInfo, ByVal comment As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                      "tbl_BT_Register_ConfirmRequester", _
                                      sp.GetNull(obj.BTRegisterID), _
                                      sp.GetNull(obj.BudgetName), _
                                      sp.GetNull(obj.BudgetCode), _
                                      sp.GetNull(obj.ProjectBudgetCode), _
                                      sp.GetNull(comment), _
                                      sp.GetNull(obj.ModifiedBy), _
                                      sp.GetNull(obj.CheckAllBudget), _
                                      sp.GetNull(obj.BudgetCodeID))).Trim()
        End Function

        Public Shared Function BTRegister_RejectToBudget(ByVal btID As Integer, ByVal comment As String, ByVal modifiedBy As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                      "tbl_BT_Register_RejectToBudget", _
                                      sp.GetNull(btID), _
                                      sp.GetNull(comment), _
                                      sp.GetNull(modifiedBy)))
        End Function

        Public Shared Function BTRegister_Copy(ByVal btID As Integer, ByVal employees As String, _
                                               ByVal createdBy As String) As Integer
            Dim sp As New SqlDataProvider()
            Dim ds As DataSet = SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                      "tbl_BT_Register_Copy", _
                                      sp.GetNull(btID), _
                                      sp.GetNull(employees), _
                                      sp.GetNull(createdBy))
            Return CommonFunction._ToUnsignInt(ds.Tables(ds.Tables.Count - 1).Rows(0)("count"))
        End Function

        Public Shared Function BTRegister_CheckOnedayBTDate(ByVal btid As Integer) As DataSet
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_Register_CheckOnedayBTDate", _
                                            sp.GetNull(btid))
        End Function

        Public Shared Function BTRegister_CheckOvernightBTDate(ByVal departureDate As DateTime, ByVal returnDate As DateTime, ByVal btid As Integer) As DataSet
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_Register_CheckOvernightBTDate", _
                                            sp.GetNull(departureDate), _
                                            sp.GetNull(returnDate), _
                                            sp.GetNull(btid))
        End Function

        Public Shared Function tbl_BT_Register_CheckAirTicket(ByVal BTRegisterID As Integer) As Boolean
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                                                 "tbl_BT_Register_CheckAirTicket", _
                                                                 BTRegisterID)) > 0
        End Function

        Public Shared Function CheckOverseaExrate(ByVal BTRegisterID As Integer, ByVal invoiceDate As DateTime) As Boolean
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                                                 "CheckOverseaExrate", _
                                                                 sp.GetNull(BTRegisterID), _
                                                                 sp.GetNull(invoiceDate))) = 1
        End Function

#End Region

#Region "tbl_BT_Register_Request"
        Public Shared Sub BTRegisterRequest_Insert(ByVal BTRRObj As tblBTRegisterRequestInfo, ByVal BTRRDObj As tblBTRegisterRequestDetailsInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_Register_Request_Insert", _
                                      sp.GetNull(BTRRObj.BTRegisterID), _
                                      sp.GetNull(BTRRObj.FromDate), _
                                      sp.GetNull(BTRRObj.ToDate), _
                                      sp.GetNull(BTRRObj.Remark), _
                                      sp.GetNull(BTRRObj.CreatedBy), _
                                      sp.GetNull(BTRRObj.RequestDate), _
                                      If(BTRRObj.DestinationID <= 0, DBNull.Value, CType(BTRRObj.DestinationID, Object)), _
                                      sp.GetNull(BTRRObj.Purpose), _
                                      sp.GetNull(BTRRObj.IsMovingTimeAllowance), _
                                      sp.GetNull(BTRRDObj.BreakfastQty), _
                                      sp.GetNull(BTRRDObj.LunchQty), _
                                      sp.GetNull(BTRRDObj.DinnerQty), _
                                      sp.GetNull(BTRRDObj.BreakfastUnit), _
                                      sp.GetNull(BTRRDObj.LunchUnit), _
                                      sp.GetNull(BTRRDObj.DinnerUnit), _
                                      sp.GetNull(BTRRDObj.OtherMealQty), _
                                      sp.GetNull(BTRRDObj.OtherMealUnit), _
                                      sp.GetNull(BTRRDObj.Other), _
                                      sp.GetNull(BTRRDObj.OtherQty), _
                                      sp.GetNull(BTRRDObj.OtherUnit), _
                                      sp.GetNull(BTRRDObj.HotelQty), _
                                      sp.GetNull(BTRRDObj.HotelUnit), _
                                      sp.GetNull(BTRRDObj.TotalAmount), _
                                      sp.GetNull(BTRRDObj.TaxiQty), _
                                      sp.GetNull(BTRRDObj.TaxiAmount), _
                                      sp.GetNull(BTRRDObj.MotobikeQty), _
                                      sp.GetNull(BTRRDObj.MotobikeAmount), _
                                      sp.GetNull(BTRRDObj.CarRequest), _
                                      sp.GetNull(BTRRDObj.TaxiDesc), _
                                      sp.GetNull(BTRRDObj.MotobikeDesc), _
                                      sp.GetNull(BTRRDObj.AirTicketRequest), _
                                      sp.GetNull(BTRRDObj.TrainTicketRequest))

        End Sub

        Public Shared Sub BTRegisterRequest_Update(ByVal BTRRObj As tblBTRegisterRequestInfo, ByVal BTRRDObj As tblBTRegisterRequestDetailsInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_Register_Request_Update", _
                                      sp.GetNull(BTRRObj.ID), _
                                      sp.GetNull(BTRRObj.FromDate), _
                                      sp.GetNull(BTRRObj.ToDate), _
                                      sp.GetNull(BTRRObj.Remark), _
                                      sp.GetNull(BTRRObj.ModifiedBy), _
                                      sp.GetNull(BTRRObj.RequestDate), _
                                      If(BTRRObj.DestinationID <= 0, DBNull.Value, CType(BTRRObj.DestinationID, Object)), _
                                      sp.GetNull(BTRRObj.Purpose), _
                                      sp.GetNull(BTRRObj.IsMovingTimeAllowance), _
                                      sp.GetNull(BTRRDObj.BreakfastQty), _
                                      sp.GetNull(BTRRDObj.LunchQty), _
                                      sp.GetNull(BTRRDObj.DinnerQty), _
                                      sp.GetNull(BTRRDObj.BreakfastUnit), _
                                      sp.GetNull(BTRRDObj.LunchUnit), _
                                      sp.GetNull(BTRRDObj.DinnerUnit), _
                                      sp.GetNull(BTRRDObj.OtherMealQty), _
                                      sp.GetNull(BTRRDObj.OtherMealUnit), _
                                      sp.GetNull(BTRRDObj.Other), _
                                      sp.GetNull(BTRRDObj.OtherQty), _
                                      sp.GetNull(BTRRDObj.OtherUnit), _
                                      sp.GetNull(BTRRDObj.HotelQty), _
                                      sp.GetNull(BTRRDObj.HotelUnit), _
                                      sp.GetNull(BTRRDObj.TotalAmount), _
                                      sp.GetNull(BTRRDObj.TaxiQty), _
                                      sp.GetNull(BTRRDObj.TaxiAmount), _
                                      sp.GetNull(BTRRDObj.MotobikeQty), _
                                      sp.GetNull(BTRRDObj.MotobikeAmount), _
                                      sp.GetNull(BTRRDObj.CarRequest), _
                                      sp.GetNull(BTRRDObj.TaxiDesc), _
                                      sp.GetNull(BTRRDObj.MotobikeDesc), _
                                      sp.GetNull(BTRRDObj.AirTicketRequest), _
                                      sp.GetNull(BTRRDObj.TrainTicketRequest))

        End Sub

        Public Shared Function BTRegisterRequest_Search(ByVal btID As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Register_Request_Search", btID).Tables(0)
        End Function

        Public Shared Function BTRegisterRequest_GetByID(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Register_Request_GetByID", id).Tables(0)
        End Function

        Public Shared Sub BTRegisterRequest_Delete(ByVal ids As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_Register_Request_Delete", _
                                      sp.GetNull(ids))

        End Sub

        Public Shared Function BTRegisterRequest_IsBTTimeConflict(ByVal btID As Integer, _
                                                                 ByVal requestID As Integer, _
                                                                 ByVal fromDate As DateTime, _
                                                                 ByVal toDate As DateTime) As Boolean
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                            "tbl_BT_Register_CheckBTTime", _
                                            sp.GetNull(btID), _
                                            sp.GetNull(requestID), _
                                            sp.GetNull(fromDate), _
                                            sp.GetNull(toDate))) > 0
        End Function

        Public Shared Function BTRegisterRequest_CheckMovingTimeOneDay(ByVal requestID As Integer, _
                                                                       ByVal btID As Integer, _
                                                                       ByVal fromDate As DateTime) As Boolean
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToInt(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                            "tbl_BT_Register_CheckMovingTimeOneDay", _
                                            sp.GetNull(requestID), _
                                            sp.GetNull(btID), _
                                            sp.GetNull(fromDate))) = 0
        End Function
#End Region

#Region "tbl_BT_Register_Schedule"
        Public Shared Sub BTRegisterSchedule_Insert(ByVal obj As tblBTRegisterScheduleInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_Register_Schedule_Insert", _
                                      sp.GetNull(obj.BTRegisterID), _
                                      sp.GetNull(obj.FromTime), _
                                      sp.GetNull(obj.ToTime), _
                                      sp.GetNull(obj.WorkingArea), _
                                      sp.GetNull(obj.Task), _
                                      sp.GetNull(obj.EstimateTransportationFee), _
                                      sp.GetNull(obj.CreatedBy), _
                                      sp.GetNull(obj.AirTicket), _
                                      sp.GetNull(obj.TrainTicket), _
                                      sp.GetNull(obj.Car))

        End Sub

        Public Shared Sub BTRegisterSchedule_Update(ByVal obj As tblBTRegisterScheduleInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_Register_Schedule_Update", _
                                      sp.GetNull(obj.ID), _
                                      sp.GetNull(obj.FromTime), _
                                      sp.GetNull(obj.ToTime), _
                                      sp.GetNull(obj.WorkingArea), _
                                      sp.GetNull(obj.Task), _
                                      sp.GetNull(obj.EstimateTransportationFee), _
                                      sp.GetNull(obj.ModifiedBy), _
                                      sp.GetNull(obj.AirTicket), _
                                      sp.GetNull(obj.TrainTicket), _
                                      sp.GetNull(obj.Car))

        End Sub

        Public Shared Function BTRegisterSchedule_Search(ByVal btID As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Register_Schedule_Search", btID).Tables(0)
        End Function

        Public Shared Function BTRegisterSchedule_GetByID(ByVal id As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_Register_Schedule_GetByID", id).Tables(0)
        End Function

        Public Shared Sub BTRegisterSchedule_Delete(ByVal ids As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BT_Register_Schedule_Delete", _
                                      sp.GetNull(ids))

        End Sub
#End Region

#Region "tbl_BT_Register_Attachment"
        Public Shared Sub BTRAttachment_Insert(ByVal obj As tblBTRegisterAttachmentInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                      "tbl_BTRAttachment_Insert", _
                                      sp.GetNull(obj.BTRegisterID), _
                                      sp.GetNull(obj.AttachmentType), _
                                      sp.GetNull(obj.AttachmentPath), _
                                      sp.GetNull(obj.Description), _
                                      sp.GetNull(obj.CreatedBy))

        End Sub

        Public Shared Function BTRAttachment_Search(ByVal btID As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BTRAttachment_Search", btID).Tables(0)
        End Function

        Public Shared Function BTRAttachment_GetByType(ByVal btID As Integer, ByVal type As String) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BTRAttachment_GetByType", btID, sp.GetNull(type)).Tables(0)
        End Function

        Public Shared Function BTRAttachment_GetByID(ByVal ID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BTRAttachment_GetByID", ID).Tables(0)
        End Function

        Public Shared Sub BTRAttachment_DeleteByType(ByVal btID As Integer, ByVal type As String)
            Dim sp As New SqlDataProvider()
            Dim dtAtt As DataTable = SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BTRAttachment_DeleteByType", btID, sp.GetNull(type)).Tables(0)
            DeleteAttFiles(dtAtt)
        End Sub

        Public Shared Sub BTRAttachment_Delete(ByVal id As Integer)
            Dim dtAtt As DataTable = SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BTRAttachment_Delete", id).Tables(0)
            DeleteAttFiles(dtAtt)
        End Sub

        Public Shared Sub BTRAttachment_UpdateDesc(ByVal btID As Integer, ByVal otherDesc As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, "tbl_BTRAttachment_UpdateDesc", btID, sp.GetNull(otherDesc))
        End Sub
        Public Shared Sub BTRAttachment_UpdateExpenseDesc(ByVal btID As Integer, ByVal otherDesc As String)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, "tbl_BTRAttachment_UpdateExpenseDesc", btID, sp.GetNull(otherDesc))
        End Sub
#End Region

#Region "tbl_BT_History"
        Public Shared Function BTRegisterHistory_Search(ByVal btID As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BTHistory_Search", btID).Tables(0)
        End Function
#End Region

    End Class
End Namespace

