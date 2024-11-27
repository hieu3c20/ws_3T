Imports Provider
Imports DevExpress.Web.ASPxGridView

Partial Public Class FiExpenseMgmt
    Inherits System.Web.UI.Page

    Private _username As String = String.Empty
    Private _btID As Integer
    Private _dtData As DataTable = New DataTable()
    Private _sendEmailMode As String = CommonFunction._ToString(ConfigurationManager.AppSettings("SendEmailMode")).ToLower()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CommonFunction.CheckSession()
        CommonFunction.CheckRole(RoleType.Administrator, RoleType.Finance, RoleType.Finance_GA)
        _username = CommonFunction._ToString(Session("UserName"))
        If Not IsPostBack Then
            InitForm()
        End If
    End Sub

#Region "InitForm"
    Private Sub InitForm()
        Try
            InitBusinessTrip()
            InitDivision()
            InitBranch()
            dtpFrom.Date = DateTime.Now.AddMonths(-6)
            cboDept.Items.Add(New ListItem("All", ""))
            cboSection.Items.Add(New ListItem("All", ""))
            cboGroup.Items.Add(New ListItem("All", ""))
            InitBatchName()
            dteGLDate.Date = DateTime.Now
            dteInvoiceDate.Date = DateTime.Now
            SetPreParams()
        Catch ex As Exception
            CommonFunction.ShowStartupErrorMessage(Me, ex.Message)
        End Try
    End Sub

    Private Sub SetPreParams()
        Dim btid As Integer = CommonFunction._ToInt(Request.QueryString("btid"))
        If btid > 0 Then
            Dim bttype As String = Request.QueryString("bttype")
            CommonFunction.SetCBOValue(ddlBTType, bttype)
            Dim location As String = CommonFunction._ToString(Request.QueryString("loc"))
            CommonFunction.SetCBOValue(cboLocation, location)
            Dim fromDate As String = CommonFunction._ToString(Request.QueryString("fdate"))
            dtpFrom.Text = fromDate
            Dim toDate As String = CommonFunction._ToString(Request.QueryString("tdate"))
            dtpTo.Text = toDate
            Dim division As String = CommonFunction._ToString(Request.QueryString("div"))
            CommonFunction.SetCBOValue(cboDivision, division)
            Dim department As String = CommonFunction._ToString(Request.QueryString("dep"))
            CommonFunction.SetCBOValue(cboSection, department)
            Dim section As String = CommonFunction._ToString(Request.QueryString("sec"))
            CommonFunction.SetCBOValue(cboSection, section)
            Dim group As String = CommonFunction._ToString(Request.QueryString("group"))
            CommonFunction.SetCBOValue(cboGroup, group)
            Dim employeeCode As String = CommonFunction._ToString(Request.QueryString("ecode"))
            txtEmployeeCode.Text = employeeCode
            Dim fullName As String = CommonFunction._ToString(Request.QueryString("ename"))
            txtFullName.Text = fullName
            Dim btNo As String = CommonFunction._ToString(Request.QueryString("btno"))
            txtBTNo.Text = btNo
            SetPreSearchCondition()
            LoadDataGrid()
            Dim registerPageSize As Integer = CommonFunction._ToUnsignInt(Request.QueryString("rpsize"))
            If registerPageSize < 1 Then
                registerPageSize = 100
            End If
            grvBTRegister.SettingsPager.PageSize = registerPageSize
            Dim rejectedPageSize As Integer = CommonFunction._ToUnsignInt(Request.QueryString("jpsize"))
            If rejectedPageSize < 1 Then
                rejectedPageSize = 100
            End If
            grvBTRejected.SettingsPager.PageSize = rejectedPageSize
            Dim submitPageSize As Integer = CommonFunction._ToUnsignInt(Request.QueryString("spsize"))
            If submitPageSize < 1 Then
                submitPageSize = 100
            End If
            grvBTSubmitted.SettingsPager.PageSize = submitPageSize
            Dim registerPage As Integer = CommonFunction._ToUnsignInt(Request.QueryString("rpage"))
            grvBTRegister.PageIndex = registerPage
            Dim rejectedPage As Integer = CommonFunction._ToUnsignInt(Request.QueryString("jpage"))
            grvBTRejected.PageIndex = rejectedPage
            Dim submitPage As Integer = CommonFunction._ToUnsignInt(Request.QueryString("spage"))
            grvBTSubmitted.PageIndex = submitPage
        End If
    End Sub

    Private Sub InitBusinessTrip()
        Dim dtBT As DataTable
        dtBT = LookupProvider.GetByCode("BT_Type")
        CommonFunction.LoadLookupDataToComboBox(ddlBTType, dtBT)
    End Sub

    Private Sub InitDivision()
        Dim dtDivision As DataTable
        dtDivision = BusinessTripProvider.tbl_DivisionGetAll()
        CommonFunction.LoadDataToComboBox(cboDivision, dtDivision, "DivisionName", "DivisionID", True, "All", "")
        InitddlDepartment()
    End Sub

    Private Sub InitddlDepartment()
        Dim DivisionID As Integer = CommonFunction._ToInt(cboDivision.SelectedValue)
        Dim dtDepartment As DataTable
        dtDepartment = BusinessTripProvider.tbl_DepartmentGetByDivID(DivisionID)
        CommonFunction.LoadDataToComboBox(cboDept, dtDepartment, "DepartmentName", "DepartmentID", True, "All", "")
        InitddlSection()
    End Sub

    Private Sub InitddlSection()
        Dim DivisionID As Integer = CommonFunction._ToInt(cboDivision.SelectedValue)
        Dim DepID As Integer = CommonFunction._ToInt(cboDept.SelectedValue)
        Dim dtSection As DataTable
        dtSection = BusinessTripProvider.tbl_SectionGetByDepID(DivisionID, DepID)
        CommonFunction.LoadDataToComboBox(cboSection, dtSection, "SectionName", "SectionID", True, "All", "")
        InitddlGroup()
    End Sub

    Private Sub InitddlGroup()
        Dim DivisionID As Integer = CommonFunction._ToInt(cboDivision.SelectedValue)
        Dim DepID As Integer = CommonFunction._ToInt(cboDept.SelectedValue)
        Dim SecID As Integer = CommonFunction._ToInt(cboSection.SelectedValue)
        Dim dtGroup As DataTable
        dtGroup = BusinessTripProvider.tbl_GroupGetBySecID(DivisionID, DepID, SecID)
        CommonFunction.LoadDataToComboBox(cboGroup, dtGroup, "GroupName", "GroupID", True, "All", "")
    End Sub

    Private Sub InitBranch()
        Dim dtBranch As DataTable
        dtBranch = BusinessTripProvider.tbl_BranchGetAll()
        cboLocation.DataSource = dtBranch
        cboLocation.DataValueField = "BranchID"
        cboLocation.DataTextField = "BranchName"
        cboLocation.DataBind()
        cboLocation.Items.Insert(0, New ListItem("All", ""))
    End Sub

    Private Sub InitBatchName()
        Dim dtBT As DataTable = mBatchNameProvider.GetActive()
        CommonFunction.LoadDataToComboBox(ddlBatchName, dtBT, "BatchName", "ID", True, "", "")
    End Sub

    Private Sub SetPreSearchCondition()
        hBTType.Value = ddlBTType.SelectedValue
        hLocation.Value = cboLocation.SelectedValue
        hFrom.Value = dtpFrom.Text
        hTo.Value = dtpTo.Text
        hDivision.Value = cboDivision.SelectedValue
        hDept.Value = cboDept.SelectedValue
        hSection.Value = cboSection.SelectedValue
        hGroup.Value = cboGroup.SelectedValue
        hEmployeeCode.Value = txtEmployeeCode.Text
        hFullName.Value = txtFullName.Text
        hBTNo.Value = txtBTNo.Text
        hBTSStatus.Value = ddlBTSStatus.SelectedValue
    End Sub
#End Region

    Protected Sub btnSearch_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            SetPreSearchCondition()
            LoadDataGrid()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Sub GetDataTable()
        Dim LocationID, DivID, DepID, SecID, GropID As Integer
        Dim BTTypeID As String = String.Empty
        Dim FullName As String = String.Empty
        Dim EmployeeCode As String = String.Empty
        Dim FromDate As Date
        Dim ToDate As Date
        Dim btNo As String
        Dim btsStatus As String = String.Empty

        BTTypeID = hBTType.Value
        LocationID = CommonFunction._ToInt(hLocation.Value)
        DivID = CommonFunction._ToInt(hDivision.Value)
        DepID = CommonFunction._ToInt(hDept.Value)
        SecID = CommonFunction._ToInt(hSection.Value)
        GropID = CommonFunction._ToInt(hGroup.Value)
        FullName = hFullName.Value
        EmployeeCode = hEmployeeCode.Value
        FromDate = CommonFunction._ToDateTime(hFrom.Value, "dd-MMM-yyyy")
        ToDate = CommonFunction._ToDateTime(hTo.Value, "dd-MMM-yyyy")
        btNo = hBTNo.Value
        btsStatus = hBTSStatus.Value
        '
        _dtData = ExpenseProvider.tbl_BT_Expense_SearchFI(BTTypeID, LocationID, DivID, DepID, SecID, GropID, FullName, EmployeeCode, FromDate, ToDate, "", _username, btNo, btsStatus)
    End Sub

    Public Sub LoadDataGrid()
        GetDataTable()
        LoadRegister()
        LoadRejected()
        LoadSubmitted()
    End Sub

    Private Sub LoadRegister()
        CommonFunction.LoadDataToGrid(grvBTRegister, _dtData, "IsSubmited = 1 and BudgetChecked = 1 and FIStatus <> '" + FIStatus.completed.ToString() + "' and FIStatus <> '" + FIStatus.rejected.ToString() + "'", "No")
    End Sub

    Private Sub LoadRejected()
        CommonFunction.LoadDataToGrid(grvBTRejected, _dtData, "IsSubmited = 1 and BudgetChecked = 1 and FIStatus = '" + FIStatus.rejected.ToString() + "'", "No")
    End Sub

    Private Sub LoadSubmitted()
        CommonFunction.LoadDataToGrid(grvBTSubmitted, _dtData, "IsSubmited = 1 and BudgetChecked = 1 and FIStatus = '" + FIStatus.completed.ToString() + "'", "No")
    End Sub

    'Protected Sub btnApproval_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnApproval.Click
    '    CommonFunction.SetPostBackStatus(btnSearch)
    '    Dim i As Integer
    '    'Dim insValue As Integer = 0
    '    Dim checkAll As Boolean = False
    '    For i = 0 To grvBTRegister.VisibleRowCount - 1
    '        Dim chk As CheckBox = CType(grvBTRegister.FindRowCellTemplateControl(i, grvBTRegister.Columns(0), "chkSelect"), CheckBox)
    '        If chk IsNot Nothing Then
    '            If chk.Checked Then
    '                checkAll = True
    '                Dim BtRegisterID As Integer = CommonFunction._ToInt(chk.Attributes("data-id").ToString())
    '                BusinessTripProvider.tbl_BT_Register_UpdateStatusHR(BtRegisterID, HRStatus.completed.ToString(), strUser, "")
    '            End If
    '        End If
    '    Next
    '    CommonFunction.ShowInfoMessage(panMessage, "Approval suscessfully!")
    '    LoadDataGrid()
    'End Sub

    Protected Sub btnApprove_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnApprove.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            dteGLDate.Attributes("style") = ""
            spiExtensionAmount.Attributes("style") = ""
            approveMessage.InnerText = ""
            '
            If dteInvoiceDate.Text.Trim().Length = 0 OrElse dteGLDate.Text.Trim().Length = 0 Then
                Return
            End If
            _btID = CommonFunction._ToInt(hApproveBTRegisterID.Value)
            Dim PaymentType As String = If(radBankTransfer.Checked, "b", "c")
            Dim EmployeeCode As String = hApproveEmployeeCode.Value
            Dim invoiceDate As DateTime = dteInvoiceDate.Date
            Dim glDate As DateTime = dteGLDate.Date
            Dim batchName As Integer = CommonFunction._ToInt(ddlBatchName.SelectedValue)
            '
            Dim extAmount As Decimal = CommonFunction._ToMoney(spiExtensionAmount.Text)
            Dim drExt As DataRow = ExpenseProvider.CheckExtInvoice(_btID)
            Dim extLimitedAmount As Decimal = CommonFunction._ToMoney(drExt("ExtAmount"))
            '
            tabApproveMessage.Attributes("style") = "display: block"
            approveMessage.InnerText = hApproveMessage.Value
            'Kiem tra xem User da co ben oracle chua
            Dim dtVendor As DataSet = BusinessTripProvider.check_Supplier_No(_btID, PaymentType)
            Dim dtSupplier As DataTable = dtVendor.Tables(0)
            Dim dtSupplierSite As DataTable = dtVendor.Tables(1)
            If Not BusinessTripProvider.CheckOverseaExrate(_btID, invoiceDate) Then
                CommonFunction.ShowErrorMessage(panMessage, "Exchange rate's not found!")
            ElseIf dtSupplier.Rows.Count = 0 Then
                CommonFunction.ShowErrorMessage(panMessage, String.Format("Supplier ({0}) is not found! Please setup this employee as a supplier in Oracle!", EmployeeCode))
            ElseIf dtSupplier.Rows.Count >= 2 Then
                CommonFunction.ShowErrorMessage(panMessage, String.Format("Supplier ({0}) is invalid/double! Please re-check this employee in Oracle!", EmployeeCode))
            ElseIf dtSupplierSite.Rows.Count = 0 Then
                CommonFunction.ShowErrorMessage(panMessage, String.Format("Supplier ({0}) sites are not found! Please create sites for this employee in Oracle!", EmployeeCode))
                'ElseIf Not BusinessTripProvider.CheckOraGLDate(glDate) Then
                '    CommonFunction.ShowErrorMessage(panMessage, "GL period is not open!")
                '    dteGLDate.Attributes("style") = "border-color: red;"
            ElseIf extLimitedAmount > 0 AndAlso extAmount > extLimitedAmount Then
                CommonFunction.ShowErrorMessage(panMessage, "Extension Amount is invalid!")
                spiExtensionAmount.Attributes("style") = "border-color: red;"
            Else
                btnApprove.Visible = False
                Dim supplierNo As String = CommonFunction._ToString(dtSupplier.Rows(0)("VendorNo"))
                Dim supplierSite As String = CommonFunction._ToString(dtSupplierSite.Rows(0)("VendorSite"))
                Dim extExrate As Decimal = CommonFunction._ToMoney(spiExtensionExrate.Text)
                Dim creditExrate As Decimal = CommonFunction._ToMoney(spiCreditInvoiceExrate.Text)
                '
                Dim dr As DataRow = ExpenseProvider.tbl_BT_Expense_UpdateStatusFI(_btID, FIStatus.completed.ToString(), _username, "", supplierNo, supplierSite, glDate, batchName, invoiceDate, extAmount, extExrate, creditExrate)
                Dim message As String = CommonFunction._ToString(dr("MessageResult")).Trim()
                Dim messageStatus As String = CommonFunction._ToString(dr("MessageStatus")).Trim()
                If messageStatus.Length > 0 Then
                    CommonFunction.ShowErrorMessage(panMessage, messageStatus)
                Else
                    LoadDataGrid()
                    tabApproveMessage.Attributes("style") = "display: none"
                    btnApprove.Visible = True
                    Dim isDone As Boolean = True
                    If message.Trim().Length > 0 Then
                        isDone = False
                        CommonFunction.ShowErrorMessage(panMessage, String.Format("Oracle message: {0}", message))
                    Else
                        dteInvoiceDate.Date = DateTime.Now
                        dteGLDate.Date = DateTime.Now
                        ddlBatchName.ClearSelection()
                        radCash.Checked = True
                    End If
                    '
                    hApproveBTRegisterID.Value = ""
                    hApproveEmployeeCode.Value = ""
                    '
                    If hIsReApprove.Value = "F" Then
                        'Send notice email
                        If SendUserEmail() Then
                            If isDone Then
                                CommonFunction.ShowInfoMessage(panMessage, "BT's Approved!")
                            End If
                        Else
                            If isDone Then
                                CommonFunction.ShowInfoMessage(panMessage, "BT's Approved but fail to send notice emails! Please contact with administrator!")
                            End If
                        End If
                        _objEmail.Dispose()
                    Else
                        If isDone Then
                            CommonFunction.ShowInfoMessage(panMessage, "BT's Approved!")
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    'Protected Sub btnReject_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnReject.Click

    '    Dim i As Integer
    '    Dim EmployeeCode As String = String.Empty

    '    For i = 0 To grvBTSubmitted.VisibleRowCount
    '        Dim chk As CheckBox = CType(grvBTSubmitted.FindRowCellTemplateControl(i, grvBTSubmitted.Columns(1), "chkSelect"), CheckBox)
    '        If chk IsNot Nothing Then
    '            If chk.Checked Then
    '                Dim BtRegisterID As Integer = CommonFunction._ToInt(chk.Attributes("data-id").ToString())
    '                BusinessTripProvider.tbl_BT_Register_UpdateStatusHR(BtRegisterID, HRStatus.completed.ToString())
    '                'xoa du lieu trong bang TAS ben HR
    '                BusinessTripProvider.DeleteTasSetRest(BtRegisterID)
    '            End If
    '        End If
    '    Next
    '    LoadDataGrid()
    'End Sub

    Protected Sub btnRejectOK_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnRejectOK.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            _btID = CommonFunction._ToInt(hRejectBTRegisterID.Value)
            Dim rejectReason As String = txtRejectReason.Text.Trim()
            If rejectReason.Trim().Length = 0 Then
                Return
            End If
            'Dim oracleError As Boolean = ExpenseProvider.tbl_BT_Expense_CheckOracleStatus(_btID)
            'If oracleError Then

            'Else
            '    CommonFunction.ShowErrorMessage(panMessage, "Can not reject this BT! Please check oracle invoice status!")
            'End If
            Dim dr As DataRow = ExpenseProvider.tbl_BT_Expense_UpdateStatusFI(_btID, FIStatus.rejected.ToString(), _username, rejectReason)
            Dim message As String = CommonFunction._ToString(dr("MessageResult")).Trim()
            If message.Trim().Length > 0 Then
                CommonFunction.ShowErrorMessage(panMessage, message)
            Else
                LoadDataGrid()
                '                
                hRejectBTRegisterID.Value = ""
                txtRejectReason.Text = ""
                tabRejectMessage.Attributes("style") = "display: none"
                'Send notice email
                If SendUserEmail() Then
                    CommonFunction.ShowInfoMessage(panMessage, "BT's Rejected!")
                Else
                    CommonFunction.ShowInfoMessage(panMessage, "BT's Rejected but fail to send notice emails! Please contact with administrator!")
                End If
                _objEmail.Dispose()
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub cboDivision_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboDivision.SelectedIndexChanged
        Try
            InitddlDepartment()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub cboDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboDept.SelectedIndexChanged
        Try
            InitddlSection()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub cboSection_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboSection.SelectedIndexChanged
        Try
            InitddlGroup()
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub grvBTRegister_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTRegister.BeforeGetCallbackResult
        GetDataTable()
        LoadRegister()
    End Sub

    Protected Sub grvBTRejected_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTRejected.BeforeGetCallbackResult
        GetDataTable()
        LoadRejected()
    End Sub

    Protected Sub grvBTSubmitted_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTSubmitted.BeforeGetCallbackResult
        GetDataTable()
        LoadSubmitted()
    End Sub

    Protected Sub btnView_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            'Dim btn As Button = CType(sender, Button)
            Dim btID As Integer = CommonFunction._ToInt(hItemID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))            
            Dim dtExpense As DataTable = ExpenseProvider.BTExpense_GetByID(btID)
            If dtExpense.Rows.Count > 0 Then
                Dim btType As String = CommonFunction._ToString(dtExpense.Rows(0)("BTType"))
                Dim postBackUrl As New StringBuilder()
                postBackUrl.Append(If(btType.IndexOf("oneday_") = 0, "~/BTOneDayExpenseDeclaration.aspx", "~/BTExpenseDeclaration.aspx")) 'AndAlso CommonFunction._ToInt(dtExpense.Rows(0)("IsOneDayExpense")) = 1
                Dim params As String = String.Format( _
                    "btid={0}&bttype={1}&loc={2}&fdate={3}&tdate={4}&div={5}&dep={6}&sec={7}&group={8}&ecode={9}&ename={10}&rpage={11}&rpsize={12}&spage={13}&spsize={14}&btno={15}&jpage={16}&jpsize={17}", _
                    btID, hBTType.Value, hLocation.Value, hFrom.Value, hTo.Value, hDivision.Value, hDept.Value, hSection.Value, hGroup.Value, hEmployeeCode.Value, hFullName.Value, _
                    grvBTRegister.PageIndex, grvBTRegister.SettingsPager.PageSize, grvBTSubmitted.PageIndex, grvBTSubmitted.SettingsPager.PageSize, hBTNo.Value, grvBTRejected.PageIndex, grvBTRejected.SettingsPager.PageSize)
                postBackUrl.Append(String.Format("?id={0}&back=FiExpenseMgmt.aspx&params={1}", btID, params.Replace("&", ";amp;").Replace("=", ";eq;")))
                Response.Redirect(postBackUrl.ToString())
            Else
                CommonFunction.ShowErrorMessage(panMessage, "Item not found!")
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvBTRegister_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles grvBTRegister.HtmlRowPrepared, grvBTRejected.HtmlRowPrepared
        If e.RowType = GridViewRowType.Data Then
            Dim status As String = e.GetValue("FIStatus")
            If status = FIStatus.rejected.ToString() OrElse status = FIStatus.budget_rejected.ToString() Then
                e.Row.CssClass &= " rejected"
            ElseIf status = FIStatus.pending.ToString() Then
                e.Row.CssClass &= " not-found"
            ElseIf status = FIStatus.budget_reconfirmed.ToString() Then
                e.Row.CssClass &= " waiting"
            End If
        End If
    End Sub

    Protected Sub btnCheckExtInvoice_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCheckExtInvoice.Click
        CommonFunction.SetPostBackStatus(btnCheckExtInvoice)
        Try
            approveMessage.InnerText = hApproveMessage.Value
            Dim exrate As Decimal = 0
            Dim btID As Integer = CommonFunction._ToInt(hApproveBTRegisterID.Value)
            '
            Dim drExt As DataRow = ExpenseProvider.CheckExtInvoice(btID)
            Dim extAmount As Decimal = CommonFunction._ToMoney(drExt("ExtAmount"))
            If extAmount > 0 Then
                spiExtensionAmount.Number = extAmount Mod 100
                spiExtensionAmount.MaxValue = extAmount
                spiExtensionExrate.Number = ExpenseProvider.GetExrate("USD", "VND", dteInvoiceDate.Date)
                trExtensionInvoice.Visible = True
            Else
                spiExtensionAmount.Number = 0
                spiExtensionExrate.Number = 1
                trExtensionInvoice.Visible = False
            End If
            'credit            
            Dim dtExpense As DataTable = ExpenseProvider.BTExpense_GetByID(btID)
            Dim btType As String = CommonFunction._ToString(dtExpense.Rows(0)("BTType"))
            '
            Dim creditAmount As Decimal = CommonFunction._ToMoney(drExt("CreditAmount"))
            If btType.IndexOf("domestic") < 0 AndAlso creditAmount > 0 Then
                exrate = If(exrate > 0, exrate, ExpenseProvider.GetExrate("USD", "VND", dteInvoiceDate.Date))
                spiCreditInvoiceAmount.Number = creditAmount
                spiCreditInvoiceExrate.Number = exrate
                trCreditInvoice.Visible = True
            Else
                spiCreditInvoiceAmount.Number = 0
                spiCreditInvoiceExrate.Number = 1
                trCreditInvoice.Visible = False
            End If
            '
            tabApproveMessage.Attributes("style") = "display: block"
            dteInvoiceDate.Focus()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub dteInvoiceDate_DateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dteInvoiceDate.DateChanged
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim exrate As Decimal = ExpenseProvider.GetExrate("USD", "VND", dteInvoiceDate.Date)
            spiExtensionExrate.Number = exrate
            spiCreditInvoiceExrate.Number = exrate
            tabApproveMessage.Attributes("style") = "display: block;" + tabApproveMessage.Attributes("style")
            approveMessage.InnerText = hApproveMessage.Value
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnApproveCancel_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnApproveCancel.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            hApproveBTRegisterID.Value = ""
            tabApproveMessage.Attributes("style") = "display: none"
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

#Region "Send Notice Email"

    Private _lastDestination As String = String.Empty
    Private _btNo As String = String.Empty
    Private _fiStatus As String = String.Empty
    Private _employeeName As String = String.Empty
    Dim departureDate As DateTime
    Dim returnDate As DateTime
    Dim statusDesc As String = ""
    Private Function GenNoticeBody(Optional ByVal link As String = "") As String
        Dim isOneday As Boolean = False
        Dim employeeCode As String = ""
        Dim purpose As String = ""
        Dim destination As String = ""
        Dim btType As String = ""
        Dim budgetName As String = ""
        Dim budgetCode As String = ""
        Dim btAmount As String = ""
        Dim comment As String = ""
        '
        Dim dtExpense As DataTable = ExpenseProvider.BTExpense_GetByID(_btID)
        If dtExpense.Rows.Count > 0 Then
            Dim dr As DataRow = dtExpense.Rows(0)
            isOneday = CommonFunction._ToString(dr("BTType")).IndexOf("oneday") >= 0
            _btNo = CommonFunction._ToString(dr("BTNo"))
            employeeCode = CommonFunction._ToString(dr("EmployeeCode"))
            _employeeName = CommonFunction._ToString(dr("EmployeeName"))
            btType = CommonFunction._ToString(dr("BTTypeName"))
            purpose = CommonFunction._ToString(dr("Purpose"))
            destination = CommonFunction._ToString(If(isOneday, dr("oDestination"), dr("Destination")))
            _lastDestination = CommonFunction._ToString(dr("LastDestination"))
            _fiStatus = CommonFunction._ToString(dr("FIStatusText"))
            departureDate = CommonFunction._ToDateTime(dr("DepartureDate"))
            returnDate = CommonFunction._ToDateTime(dr("ReturnDate"))
            statusDesc = CommonFunction._ToString(dr("FIStatusDescription"))
            budgetName = CommonFunction._ToString(dr("BudgetItem"))
            budgetCode = CommonFunction._ToString(dr("BudgetCode"))
            btAmount = String.Format("{0} {1}", CommonFunction._FormatMoney(dr("ExpenseAmount")), dr("BTCurrency"))
            comment = CommonFunction._ToString(dr("RejectReasonFI"))
            '
            Dim url = If(CommonFunction._ToString(dr("BTType")).IndexOf("oneday_") = 0 AndAlso CommonFunction._ToInt(dr("IsOneDayExpense")) = 1, "BTOneDayExpenseDeclaration.aspx", "BTExpenseDeclaration.aspx")
            link = String.Format(link, url)
        End If
        Dim eBody As New StringBuilder("<p><strong>To whom it may concern</strong></p>")
        eBody.Append("<p>This is notification from <strong>B</strong>usiness <strong>T</strong>rip Online <strong>S</strong>ystem (BTS).</p>")
        eBody.Append(String.Format("<p>Regarding Business Trip No. ""<strong>{0}</strong>"", we would like to share the latest information to you as below:</p>", _btNo))
        eBody.Append(String.Format("<p><table><tr><td valign='top' width='210'><ul><li style='margin: 0;'><strong>BT No:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", _btNo))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>BT Type:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", btType))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Employee Code/Name:</strong></li></ul></td><td style='color: #0070c0'><span style='color: red'>{0}</span>/<span style='color: red'>{1}</span></td></tr>", employeeCode, _employeeName))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Purpose/Destination:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", If(isOneday, destination, String.Format("{0}/{1}", purpose, destination))))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Date From:</strong></li></ul></td><td><span style='color: #0070c0'>{0}</span> <strong>To:</strong> <span style='color: #0070c0'>{1}</span></td></tr>", departureDate.ToString("dd-MMM-yyyy HH:mm"), returnDate.ToString("dd-MMM-yyyy HH:mm")))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Budget Code/Name:</strong></li></ul></td><td><span style='color: #0070c0'>{0}/{1}</span></td></tr>", budgetCode, budgetName))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Amount:</strong></li></ul></td><td><span style='color: red'>{0}</span></td></tr>", btAmount))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Status:</strong></li></ul></td><td style='color: red'><strong>{0}</strong> {1}</td></tr>", _fiStatus, If(statusDesc.Trim().Length > 0, String.Format("({0})", statusDesc), "")))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Comment:</strong></li></ul></td><td><span style='color: #0070c0'>{0}</span></td></tr>", If(comment = "", "No comment", comment)))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Updated by:</strong></li></ul></td><td style='color: #0070c0'>{0} - {1} ({2} Department | {3})</td></tr></table></p>", _username, CommonFunction._ToString(Session("FullName")), CommonFunction._ToString(Session("Department")), CommonFunction._ToString(Session("Division"))))
        eBody.Append(String.Format("<p>Please <a href='{0}{1}'>click here</a> to check/process this information.</p>", ConfigurationManager.AppSettings("Domain"), link))
        eBody.Append("<p>If you are unable to check/process, You can refer to FAQ function from main menu of BTS system to view all Frequently Asked Question.</p>")
        eBody.Append("<p>Thank you for your cooperation.</p>")
        eBody.Append("<p>Regards,<br>BTS Support Team</p>")
        Return eBody.ToString()
    End Function

    Private _objEmail As TMVEmailService.EmailService = Nothing
    Private Function SendNoticeEmail(ByVal eTo As String, ByVal eBody As String) As Boolean
        Dim isSent As Boolean = True
        Dim indicator As String = ""
        Select Case _sendEmailMode
            Case SendEmailMode.Dev.ToString().ToLower()
                eTo = "syhieuvt@toyotavn.com.vn" '"sudungnq@toyotavn.com.vn"
            Case SendEmailMode.Test.ToString().ToLower()
                eTo = ConfigurationManager.AppSettings("UserTestEmails")
            Case SendEmailMode.UserTest.ToString().ToLower()
                eTo = eTo
            Case SendEmailMode.User.ToString().ToLower()
                eTo = eTo
                indicator = ""
            Case Else
                eTo = ""
        End Select
        If eTo.Trim().Length > 0 Then
            If _objEmail Is Nothing Then
                _objEmail = New TMVEmailService.EmailService()
            End If
            Dim bcc As String = ConfigurationManager.AppSettings("UserTestBCC")
            Dim eFrom As String = ConfigurationManager.AppSettings("BTSSupportEmail").Replace("[", "<").Replace("]", ">")
            Dim eSubject As String = String.Format("{0}[BTS Expense]: {1} - {2}/{3} (Status: {4})", indicator, _btNo, _employeeName, _lastDestination, _fiStatus)
            Try
                isSent = _objEmail.SendEmail(eFrom, eTo, "", bcc, eSubject, eBody.ToString(), "", "")
            Catch ex As Exception
                isSent = False
            End Try
        End If
        Return isSent
    End Function

    Private Function SendUserEmail() As Boolean
        Dim eTo As String = ""
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(_btID, RoleType.Normal.ToString())
        Else
            Dim dtUser As DataTable = UserProvider.GetEmailInfoByBT(_btID)
            For Each dr As DataRow In dtUser.Rows
                eTo = String.Concat(eTo, ", ", dr("TMVEmail"))
            Next
            If eTo.Length > 0 Then
                eTo = eTo.Substring(1).Trim()
            End If
        End If
        Dim viewLink As String = String.Concat("{0}?id=", _btID, "&back={0}")
        Dim eBody As String = GenNoticeBody(viewLink)
        Return SendNoticeEmail(eTo, eBody)
    End Function
#End Region
End Class