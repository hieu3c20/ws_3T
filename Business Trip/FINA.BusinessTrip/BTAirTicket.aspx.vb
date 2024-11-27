Imports Provider
Imports System.Data
Imports System.IO
Imports DevExpress.Web.ASPxGridView

Partial Public Class BTAirTicket
    Inherits System.Web.UI.Page

    Protected _username As String = String.Empty
    Protected _dtData As DataTable
    Protected _isGA As Boolean
    Protected _isTofsAirGA As Boolean
    Protected _isFI As Boolean
    Protected _isFIBudget As Boolean
    Private _sendEmailMode As String = CommonFunction._ToString(ConfigurationManager.AppSettings("SendEmailMode")).ToLower()
    Private _dsData As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CommonFunction.CheckSession()
        CommonFunction.CheckRole(RoleType.Administrator, RoleType.Finance, RoleType.Finance_Budget, RoleType.GA, RoleType.TOFS_AIR_GA, RoleType.Finance_GA)
        _username = CommonFunction._ToString(Session("UserName"))
        Dim role As String = CommonFunction._ToString(Session("RoleType"))
        _isGA = role.ToLower() = RoleType.GA.ToString().ToLower() OrElse role.ToLower() = RoleType.TOFS_AIR_GA.ToString().ToLower() OrElse role.ToLower() = RoleType.Finance_GA.ToString().ToLower() OrElse role.ToLower() = RoleType.Administrator.ToString().ToLower()
        _isTofsAirGA = role.ToLower() = RoleType.TOFS_AIR_GA.ToString().ToLower() OrElse role.ToLower() = RoleType.Administrator.ToString().ToLower()
        _isFI = role.ToLower() = RoleType.Finance.ToString().ToLower() OrElse role.ToLower() = RoleType.Finance_GA.ToString().ToLower() OrElse role.ToLower() = RoleType.Administrator.ToString().ToLower()
        _isFIBudget = role.ToLower() = RoleType.Finance_Budget.ToString().ToLower() OrElse role.ToLower() = RoleType.Administrator.ToString().ToLower()
        If Not IsPostBack Then
            InitForm()
            '
            divTransfer.Visible = False
            SetPreParams()
        Else
        End If
        '
        If _isGA Then
            btnShowImportAirTicket.Visible = True
            btnShowImportOtherAirTicket.Visible = True
            btnShowAddOtherAirTicket.Visible = True
            btnShowImportAirTicketError.Visible = True
            btnShowImportOtherError.Visible = True
            Dim dsError As DataSet = AirTicketProvider.BTAirTicket_GetErrors(_username)
            CommonFunction.LoadDataToGrid(grvImportError, dsError.Tables(0))
            If dsError.Tables(0).Rows.Count > 0 Then
                btnShowImportAirTicketError.Attributes("class") = "btn inform view-error"
            Else
                btnShowImportAirTicketError.Attributes("class") = "btn inform view-error hide"
            End If
            '
            CommonFunction.LoadDataToGrid(grvImportOtherError, dsError.Tables(1))
            If dsError.Tables(1).Rows.Count > 0 Then
                btnShowImportOtherError.Attributes("class") = "btn inform view-error"
            Else
                btnShowImportOtherError.Attributes("class") = "btn inform view-error hide"
            End If
        Else
            btnShowImportAirTicket.Visible = False
            btnShowImportOtherAirTicket.Visible = False
            btnShowAddOtherAirTicket.Visible = False
            btnShowImportAirTicketError.Visible = False
            btnShowImportOtherError.Visible = False
        End If
        '
        CommonFunction.CheckSessionMessage(Me)
        LoadForm()
    End Sub

    Private Sub InitForm()
        InitCurrency()
        InitSAirPeriod()
        InitAirPeriod()
        InitSOraSupplier()
        InitOraSupplier()
        dteGLDate.Date = DateTime.Now
        dteInvoiceDate.Date = DateTime.Now
        InitBatchName()
        'InitMonth()
        'InitYear()     
        If Not _isGA Then
            ddlSAirPeriod.AutoPostBack = True
        Else
            ddlSAirPeriod.AutoPostBack = False
        End If
    End Sub

    'Private Sub InitMonth()
    '    For i As Integer = 1 To 12
    '        ddlMonth.Items.Add(i)
    '    Next
    'End Sub

    'Private Sub InitYear()
    '    Dim iYear As Integer
    '    iYear = DateTime.Now.Year
    '    For i As Integer = iYear - 20 To iYear + 1
    '        ddlYear.Items.Insert(0, i)
    '    Next
    'End Sub

    Protected Function GetAuthorizedAccounts() As String
        Dim builder As New StringBuilder()
        Dim dtEmployees = UserProvider.tbl_User_GetAuthorizedForAirTicket(_username)
        builder.Append("[")
        For Each item As DataRow In dtEmployees.Rows
            builder.Append(String.Concat("{ value: '", item("EmployeeCode"), " - ", item("EmployeeName"), "', data: '", String.Format("{0}-{1}-{2}-{3}-{4}", item("EmployeeCode"), item("EmployeeName"), item("DivisionName"), item("DepartmentName"), CommonFunction._ToString(item("Mobile")).Replace("'", "")), "'},"))
        Next
        builder = New StringBuilder(builder.ToString().TrimEnd(","))
        builder.Append("]")
        Return builder.ToString()
    End Function

    Private Sub SetPreParams()
        Dim btid As Integer = CommonFunction._ToInt(Request.QueryString("btid"))
        If btid > 0 Then
            'Dim fromDate As String = CommonFunction._ToString(Request.QueryString("fdate"))
            'dteSDateFrom.Text = fromDate
            'Dim toDate As String = CommonFunction._ToString(Request.QueryString("tdate"))
            'dteSDateTo.Text = toDate
            Dim period As String = CommonFunction._ToString(Request.QueryString("period"))
            CommonFunction.SetCBOValue(ddlSAirPeriod, period)
            Dim airline As String = CommonFunction._ToString(Request.QueryString("airline"))
            txtSAirline.Text = airline
            Dim ticketNo As String = CommonFunction._ToString(Request.QueryString("ticket"))
            txtSTicketNo.Text = ticketNo
            Dim employeeCode As String = CommonFunction._ToString(Request.QueryString("ecode"))
            txtSEmployeeCode.Text = employeeCode
            Dim employeeName As String = CommonFunction._ToString(Request.QueryString("ename"))
            txtSEmployeeName.Text = employeeName
            Dim departureFromDate As String = CommonFunction._ToString(Request.QueryString("depfdate"))
            dteSDepartureFrom.Text = departureFromDate
            Dim departureToDate As String = CommonFunction._ToString(Request.QueryString("deptdate"))
            dteSDepartureTo.Text = departureToDate
            Dim supplier As String = CommonFunction._ToString(Request.QueryString("supplier"))
            CommonFunction.SetCBOValue(ddlSOraSupplier, supplier)
            SetPreSearchCondition()
            LoadBTAirTicket()
            Dim pageSize As Integer = CommonFunction._ToUnsignInt(Request.QueryString("psize"))
            If pageSize < 1 Then
                pageSize = 100
            End If
            grvBTAirTicket.SettingsPager.PageSize = pageSize
            Dim pageIndex As Integer = CommonFunction._ToUnsignInt(Request.QueryString("page"))
            grvBTAirTicket.PageIndex = pageIndex
            Dim otherPageSize As Integer = CommonFunction._ToUnsignInt(Request.QueryString("opsize"))
            If otherPageSize < 1 Then
                otherPageSize = 100
            End If
            grvOtherAirTicket.SettingsPager.PageSize = otherPageSize
            Dim otherPageIndex As Integer = CommonFunction._ToUnsignInt(Request.QueryString("opage"))
            grvOtherAirTicket.PageIndex = otherPageIndex
        End If
    End Sub

    Private Sub InitCurrency()
        Dim dt As DataTable = LookupProvider.GetByCode("POLICY_CURRENCY") 'CURRENCY
        CommonFunction.LoadLookupDataToComboBox(ddlAirCurrency, dt, False)
        CommonFunction.LoadLookupDataToComboBox(ddlOtherAirCurrency, dt, False)
    End Sub

    Private Sub InitSAirPeriod()
        Dim dt As DataTable = If(_isGA, AirTicketProvider.AirPeriod_GetAll(), AirTicketProvider.AirPeriod_GetSubmited())
        CommonFunction.LoadDataToComboBox(ddlSAirPeriod, dt, "Name", "ID", True, "All", "")
    End Sub

    Private Sub InitAirPeriod()
        Dim dt As DataTable = AirTicketProvider.AirPeriod_GetAll()
        CommonFunction.LoadDataToComboBox(ddlAirPeriod, dt, "Name", "ID", True, "", "")
        CommonFunction.LoadDataToComboBox(ddlImportAirPeriod, dt, "Name", "ID", True, "", "")
        CommonFunction.LoadDataToComboBox(ddlOtherAirPeriod, dt, "Name", "ID", True, "", "")
    End Sub

    Private Sub InitSOraSupplier()
        Dim dtBT As DataTable = If(_isGA, mOraSupplierProvider.GetActive(), AirTicketProvider.OraSupplier_GetSubmited(CommonFunction._ToInt(ddlSAirPeriod.SelectedValue)))
        CommonFunction.LoadDataToComboBox(ddlSOraSupplier, dtBT, "SupplierName", "OraLink", True, "All", "")
    End Sub

    Private Sub InitOraSupplier()
        Dim dtBT As DataTable = mOraSupplierProvider.GetActive()
        CommonFunction.LoadDataToComboBox(ddlOraSupplier, dtBT, "SupplierName", "OraLink", True, "", "")
        CommonFunction.LoadDataToComboBox(ddlImportOraSupplier, dtBT, "SupplierName", "OraLink", True, "", "")
        CommonFunction.LoadDataToComboBox(ddlOtherOraSupplier, dtBT, "SupplierName", "OraLink", True, "", "")
    End Sub

    Private Sub InitBatchName()
        Dim dtBT As DataTable = mBatchNameProvider.GetActive()
        CommonFunction.LoadDataToComboBox(ddlBatchName, dtBT, "BatchName", "ID", True, "", "")
    End Sub

    Private Sub LoadForm()

    End Sub

    Private Sub EnableAirTicketForm(ByVal enable As Boolean)
        dteAirDate.ReadOnly = Not enable
        txtAirline.ReadOnly = Not enable
        txtAirTicketNo.ReadOnly = Not enable
        txtAirRouting.ReadOnly = Not enable
        spiAirFare.ReadOnly = Not enable
        spiAirVAT.ReadOnly = Not enable
        spiAirAPTTax.ReadOnly = Not enable
        spiAirSF.ReadOnly = Not enable
        ddlAirCurrency.Enabled = enable
        spiAirExrate.ReadOnly = Not enable
        ddlAirPeriod.Enabled = enable
        ddlOraSupplier.Enabled = enable
        txtPassenger.ReadOnly = Not (enable AndAlso chkEmpICTRequest.Checked)
        txtEmployeeCode.ReadOnly = Not (enable AndAlso chkEmpICTRequest.Checked)
        dteDepartureDate.ReadOnly = Not (enable AndAlso chkEmpICTRequest.Checked)
        dteReturnDate.ReadOnly = Not (enable AndAlso chkEmpICTRequest.Checked)
        txtEBudgetCode.ReadOnly = True 'Not (enable AndAlso chkEmpICTRequest.Checked)
        spanEBudgetCode.Attributes("class") = ""
        spanEBudgetCode.InnerText = ""
        tdEBudgetCode.Attributes("class") = "ui-panelgrid-cell"
        If enable AndAlso chkEmpICTRequest.Checked Then
            spanPassenger.Attributes("class") = "required"
            spanPassenger.InnerText = "*"
            tdPassenger.Attributes("class") = "ui-panelgrid-cell validate-required"
            spanRequesterCode.Attributes("class") = "required"
            spanRequesterCode.InnerText = "*"
            tdRequesterCode.Attributes("class") = "ui-panelgrid-cell validate-required"
            spanRequesterName.Attributes("class") = "required"
            spanRequesterName.InnerText = "*"
            tdRequesterName.Attributes("class") = "ui-panelgrid-cell validate-required"
            spanDepartureDate.Attributes("class") = "required"
            spanDepartureDate.InnerText = "*"
            tdDepartureDate.Attributes("class") = "ui-panelgrid-cell date-time-picker validate-required"
        Else
            spanPassenger.Attributes("class") = ""
            spanPassenger.InnerText = ""
            tdPassenger.Attributes("class") = "ui-panelgrid-cell"
            spanRequesterCode.Attributes("class") = ""
            spanRequesterCode.InnerText = ""
            tdRequesterCode.Attributes("class") = "ui-panelgrid-cell"
            spanRequesterName.Attributes("class") = ""
            spanRequesterName.InnerText = ""
            tdRequesterName.Attributes("class") = "ui-panelgrid-cell"
            spanDepartureDate.Attributes("class") = ""
            spanDepartureDate.InnerText = ""
            tdDepartureDate.Attributes("class") = "ui-panelgrid-cell date-time-picker"
        End If
        '
        btnShowSaveAirTicket.Visible = enable
        btnSaveAirTicket.Visible = enable
    End Sub

    Private Sub SetPreSearchCondition()
        hSDepartureFrom.Value = dteSDepartureFrom.Text
        hSDepartureTo.Value = dteSDepartureTo.Text
        hSEmployeeCode.Value = txtSEmployeeCode.Text.Trim()
        hSEmployeeName.Value = txtSEmployeeName.Text.Trim()
        hSAirline.Value = txtSAirline.Text.Trim()
        'hSDateFrom.Value = dteSDateFrom.Text
        'hSDateTo.Value = dteSDateTo.Text
        hSAirPeriod.Value = ddlSAirPeriod.SelectedValue
        hSTicketNo.Value = txtSTicketNo.Text.Trim()
        hSOraSupplier.Value = ddlSOraSupplier.SelectedValue
    End Sub

    Protected Sub btnSearch_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            SetPreSearchCondition()
            LoadBTAirTicket()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Sub LoadDataTable()
        'Dim dateFrom As DateTime = CommonFunction._ToDateTime(hSDateFrom.Value, "dd-MMM-yyyy")
        'Dim dateTo As DateTime = CommonFunction._ToDateTime(hSDateTo.Value, "dd-MMM-yyyy")
        Dim period As Integer = CommonFunction._ToInt(hSAirPeriod.Value)
        Dim airline As String = hSAirline.Value
        Dim ticketNo As String = hSTicketNo.Value
        Dim employeeCode As String = hSEmployeeCode.Value
        Dim employeeName As String = hSEmployeeName.Value
        Dim departureFrom As DateTime = CommonFunction._ToDateTime(hSDepartureFrom.Value, "dd-MMM-yyyy")
        Dim departureTo As DateTime = CommonFunction._ToDateTime(hSDepartureTo.Value, "dd-MMM-yyyy")
        Dim supplier As String = hSOraSupplier.Value
        _dsData = AirTicketProvider.BTAirTicket_Search(period, airline, ticketNo, employeeCode, employeeName, departureFrom, departureTo, _username, supplier, If(_isGA, True, False), True)
    End Sub

    Private Sub LoadSummary()
        '
        divTransfer.Visible = False
        btnShowTransfer.Visible = False
        tabApproveMessage.Visible = False
        spanTransferLabel.InnerText = "Transfer to Oracle"
        btnTransferToOra.Text = "Transfer"
        btnShowReject.Visible = False
        panRejectInfo.Visible = False
        btnShowSubmit.Visible = False
        panSubmitInfo.Visible = False
        panConfirmBudget.Visible = False
        btnShowConfirmAllBudget.Visible = False
        panReconfirmBudget.Visible = False
        btnShowReconfirmAllBudget.Visible = False
        '
        panStatus.Visible = False
        Dim dtData As DataTable = _dsData.Tables(0)
        If ddlSAirPeriod.SelectedValue.Trim().Length > 0 AndAlso ddlSOraSupplier.SelectedValue.Trim().Length > 0 AndAlso dtData.Rows.Count > 0 Then
            Dim hasAmount As Boolean = False
            For Each item As DataRow In dtData.Rows
                If CommonFunction._ToMoney(item("NetPayment_Domestic")) > 0 Then
                    hasAmount = True
                    Exit For
                End If
            Next
            Dim dsCheck As DataSet = AirTicketProvider.AirTicketCheck(CommonFunction._ToInt(hSAirPeriod.Value), hSOraSupplier.Value)
            Dim dtCheck As DataTable = dsCheck.Tables(0)
            Dim dtStatus As DataTable = dsCheck.Tables(1)
            If dtCheck.Rows.Count > 0 Then
                Dim drCheck As DataRow = dtCheck.Rows(0)
                If _isFI Then
                    If CommonFunction._ToBoolean(drCheck("FICanApprove")) AndAlso hasAmount Then
                        btnShowTransfer.Visible = True
                        tabApproveMessage.Visible = True
                        divTransfer.Visible = True
                    End If
                    If CommonFunction._ToBoolean(drCheck("FICanReject")) Then
                        btnShowReject.Visible = True
                        panRejectInfo.Visible = True
                        divTransfer.Visible = True
                        '
                        btnShowTransfer.Visible = True
                        tabApproveMessage.Visible = True
                        divTransfer.Visible = True
                        If dtStatus.Rows.Count > 0 AndAlso CommonFunction._ToString(dtStatus.Rows(0)("ORAStatus")).ToLower() = "error" Then
                            spanTransferLabel.InnerText = "Re-Transfer to Oracle"
                            btnTransferToOra.Text = "Re-Transfer"
                        End If
                    End If
                End If
                If _isTofsAirGA Then
                    If CommonFunction._ToBoolean(drCheck("GACanApprove")) AndAlso hasAmount Then
                        btnShowSubmit.Visible = True
                        panSubmitInfo.Visible = True
                        divTransfer.Visible = True
                    End If
                End If
                If _isFIBudget Then
                    If CommonFunction._ToBoolean(drCheck("FIBudgetCanApprove")) AndAlso hasAmount Then
                        btnShowConfirmAllBudget.Visible = True
                        panConfirmBudget.Visible = True
                        btnShowReconfirmAllBudget.Visible = True
                        panReconfirmBudget.Visible = True
                        divTransfer.Visible = True
                    End If
                End If
            End If
            'status            
            If dtStatus.Rows.Count > 0 Then
                Dim btsStatus As String = CommonFunction._ToString(dtStatus.Rows(0)("BTSStatus"))
                Dim oraStatus As String = CommonFunction._ToString(dtStatus.Rows(0)("ORAStatus"))
                lblBTSStatus.InnerText = btsStatus
                lblBTSStatus.Attributes("class") = If(btsStatus = "n/a", "", btsStatus)
                lblORAStatus.Attributes("class") = If(oraStatus = "n/a", "", oraStatus)
                lblORAStatus.InnerText = If(oraStatus = "deleted", "NO ORACLE INVOICE", oraStatus.ToUpper())
                panStatus.Visible = True
            End If
            'history
            Dim dtHistory As DataTable = dsCheck.Tables(2)
            CommonFunction.LoadDataToGrid(grvStatusHistory, dtHistory, "", "No")
            'ora message
            Dim dtOra As DataTable = dsCheck.Tables(3)
            If dtOra.Rows.Count > 0 AndAlso lblORAStatus.InnerText.ToLower() = "error" Then
                lblORAStatus.Attributes("data-message") = CommonFunction._ToString(dtOra.Rows(0)("Reason"))
            Else
                lblORAStatus.Attributes.Remove("data-message")
            End If
            'transfer
            Dim dtSummary As DataTable = _dsData.Tables(1)
            Dim drSummary As DataRow = dtSummary.Rows(0)
            tdAirQuantity.InnerText = String.Format("{0:#,0}", CommonFunction._ToInt(drSummary("Quantity")))
            tdAirVAT.InnerText = String.Format("{0:#,0}", CommonFunction._ToMoney(drSummary("VAT")))
            tdAirNet.InnerText = String.Format("{0:#,0}", CommonFunction._ToMoney(drSummary("Net")))
            tdAirNetPayment.InnerText = String.Format("{0:#,0}", CommonFunction._ToMoney(drSummary("NetPayment")))
        End If
    End Sub

    Private Sub LoadBTAirTicket()
        LoadDataTable()
        Dim dtData As DataTable = _dsData.Tables(0)
        CommonFunction.LoadDataToGrid(grvBTAirTicket, dtData, String.Concat(If(_isGA OrElse _isFIBudget, "", "BudgetChecked = 1 and "), "BTRegisterID is not null or ICTRequest = 1"), Nothing, "AirEnable", _isGA)
        CommonFunction.LoadDataToGrid(grvOtherAirTicket, dtData, String.Concat(If(_isGA OrElse _isFIBudget, "", "BudgetChecked = 1 and "), "BTRegisterID is null and (ICTRequest is null or ICTRequest = 0)"), Nothing, "AirEnable", _isGA)
        LoadSummary()
    End Sub

    Private Sub LoadBTAirTicketByID()
        Dim id As Integer = CommonFunction._ToInt(hAirTicketID.Value)
        Dim dtData As DataTable = AirTicketProvider.BTAirTicket_GetByID(CommonFunction._ToInt(id))
        If dtData.Rows.Count > 0 Then
            Dim drData As DataRow = dtData.Rows(0)
            Dim oraStatus As Boolean = CommonFunction._ToBoolean(drData("EnableForm"))
            '
            Dim airDate As DateTime = CommonFunction._ToDateTime(drData("TicketDate"))
            If airDate <> DateTime.MinValue Then
                dteAirDate.Date = airDate
            End If
            txtAirline.Text = CommonFunction._ToString(drData("AirLine"))
            txtAirTicketNo.Text = CommonFunction._ToString(drData("TicketNo"))
            txtAirRouting.Text = CommonFunction._ToString(drData("Routing"))
            spiAirFare.Value = CommonFunction._ToMoneyWithNull(drData("Fare"))
            spiAirVAT.Value = CommonFunction._ToMoneyWithNull(drData("VAT"))
            spiAirAPTTax.Value = CommonFunction._ToMoneyWithNull(drData("APTTax"))
            spiAirSF.Value = CommonFunction._ToMoneyWithNull(drData("SF"))
            CommonFunction.SetCBOValue(ddlAirCurrency, drData("Currency"))
            spiAirExrate.Value = CommonFunction._ToMoneyWithNull(drData("Exrate"))
            spiAirNetPayment.Value = CommonFunction._ToMoneyWithNull(drData("NetPayment"))
            CommonFunction.SetCBOValue(ddlAirPeriod, drData("AirPeriod"))
            CommonFunction.SetCBOValue(ddlOraSupplier, drData("OraSupplier"))
            txtPassenger.Text = CommonFunction._ToString(drData("BTPassenger"))
            txtEmployeeCode.Text = CommonFunction._ToString(drData("RequesterCode"))
            txtEmployeeName.Text = CommonFunction._ToString(drData("RequesterName"))
            txtEmployeeDiv.Text = CommonFunction._ToString(drData("RequesterDiv"))
            txtEmployeeDept.Text = CommonFunction._ToString(drData("RequesterDept"))
            chkEmpICTRequest.Checked = CommonFunction._ToBoolean(drData("ICTRequest"))
            txtEBudgetCode.Text = CommonFunction._ToString(drData("BudgetCode"))
            dteDepartureDate.Value = drData("DepartureDate")
            dteReturnDate.Value = drData("ReturnDate")
            '
            EnableAirTicketForm(oraStatus AndAlso _isGA)
            '
            Dim enableBudget As Boolean = CommonFunction._ToBoolean(drData("EnableBudget"))
            If enableBudget AndAlso _isFIBudget AndAlso chkEmpICTRequest.Checked Then
                If CommonFunction._ToBoolean(drData("BudgetChecked")) Then
                    btnEShowConfirmBudget.Visible = False
                    btnEConfirmBudget.Visible = False
                    btnEShowRejectBudget.Visible = True
                    btnERejectBudget.Visible = True
                    txtEBudgetCode.ReadOnly = True
                Else
                    btnEShowConfirmBudget.Visible = True
                    btnEConfirmBudget.Visible = True
                    btnEShowRejectBudget.Visible = False
                    btnERejectBudget.Visible = False
                    txtEBudgetCode.ReadOnly = False
                    spanEBudgetCode.Attributes("class") = "required"
                    spanEBudgetCode.InnerText = "*"
                    tdEBudgetCode.Attributes("class") = "ui-panelgrid-cell validate-required"
                End If
            Else
                'txtEBudgetCode.ReadOnly = True
                btnEShowConfirmBudget.Visible = False
                btnEConfirmBudget.Visible = False
                btnEShowRejectBudget.Visible = False
                btnERejectBudget.Visible = False
            End If
        End If
    End Sub

    Private Sub ClearAirTicketForm()
        hAirTicketID.Value = ""
        hItemID.Value = ""
        dteAirDate.Date = DateTime.Now
        txtAirline.Text = ""
        txtAirTicketNo.Text = ""
        txtAirRouting.Text = ""
        spiAirFare.Value = Nothing
        spiAirVAT.Value = Nothing
        spiAirAPTTax.Value = Nothing
        spiAirSF.Value = Nothing
        ddlAirCurrency.ClearSelection()
        spiAirExrate.Value = ExpenseProvider.GetExrate(ddlAirCurrency.SelectedValue, "VND", dteAirDate.Date)
        spiAirNetPayment.Value = Nothing
        ddlAirPeriod.ClearSelection()
        ddlOraSupplier.ClearSelection()
        txtPassenger.Text = ""
        txtEmployeeCode.Text = ""
        txtEmployeeName.Text = ""
        txtEmployeeDiv.Text = ""
        txtEmployeeDept.Text = ""
        chkEmpICTRequest.Checked = True
        txtEBudgetCode.Text = ""
        dteDepartureDate.Value = Nothing
        dteReturnDate.Value = Nothing
        EnableAirTicketForm(_isGA)
        '
        txtAirTicketNo.CssClass = txtAirTicketNo.CssClass.Replace(" validate-error", "")
        ddlAirPeriod.CssClass = ddlAirPeriod.CssClass.Replace(" validate-error", "")
        ddlOraSupplier.CssClass = ddlOraSupplier.CssClass.Replace(" validate-error", "")
        '
        btnEShowConfirmBudget.Visible = False
        btnEConfirmBudget.Visible = False
        btnEShowRejectBudget.Visible = False
        btnERejectBudget.Visible = False
    End Sub

    Private Function GetAirTicketObject() As tblBTAirTicketInfo
        Dim obj As New tblBTAirTicketInfo()
        obj.ID = CommonFunction._ToInt(hAirTicketID.Value)
        obj.BTRegisterID = CommonFunction._ToInt(hItemID.Value)
        obj.TicketDate = dteAirDate.Date
        obj.AirLine = txtAirline.Text.Trim()
        obj.TicketNo = txtAirTicketNo.Text.Trim()
        obj.Routing = txtAirRouting.Text.Trim()
        obj.Fare = CommonFunction._ToMoney(spiAirFare.Text.Trim())
        obj.VAT = CommonFunction._ToMoney(spiAirVAT.Text.Trim())
        obj.APTTax = CommonFunction._ToMoney(spiAirAPTTax.Text.Trim())
        obj.SF = CommonFunction._ToMoney(spiAirSF.Text.Trim())
        obj.Currency = ddlAirCurrency.SelectedValue
        obj.Exrate = CommonFunction._ToMoney(spiAirExrate.Text.Trim())
        If obj.Exrate = 0 Then
            obj.Exrate = 1
        End If
        obj.NetPayment = CommonFunction._ToMoney(spiAirNetPayment.Text.Trim())
        obj.CreatedBy = _username
        obj.ModifiedBy = _username
        obj.AirPeriod = CommonFunction._ToInt(ddlAirPeriod.SelectedValue)
        obj.OraSupplier = ddlOraSupplier.SelectedValue
        obj.Requester = txtEmployeeCode.Text.Trim()
        obj.RequesterName = txtEmployeeName.Text.Trim()
        obj.RequesterDept = txtEmployeeDept.Text.Trim()
        obj.RequesterDiv = txtEmployeeDiv.Text.Trim()
        obj.ICTRequest = chkEmpICTRequest.Checked
        obj.BudgetCode = If(obj.ICTRequest, txtEBudgetCode.Text.Trim(), "")
        obj.Passenger = If(obj.ICTRequest, txtPassenger.Text.Trim(), "")
        obj.DepartureDate = dteDepartureDate.Date
        obj.ReturnDate = dteReturnDate.Date
        Return obj
    End Function

    Protected Sub grvBTAirTicket_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTAirTicket.BeforeGetCallbackResult
        LoadBTAirTicket()
    End Sub

    Protected Sub btnSaveAirTicket_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveAirTicket.Click
        CommonFunction.SetPostBackStatus(btnSaveAirTicket)
        Try
            Dim obj As tblBTAirTicketInfo = GetAirTicketObject()
            If Not IsValidAirTicket(obj) Then
                CommonFunction.SetProcessStatus(btnSaveAirTicket, False)
                Return
            End If
            Dim message As String = ""
            If obj.ID > 0 Then
                message = AirTicketProvider.BTAirTicket_Update(obj)
            Else
                message = AirTicketProvider.BTAirTicket_Insert(obj)
            End If
            If message.Trim().Length > 0 Then
                CommonFunction.SetProcessStatus(btnSaveAirTicket, False)
                CommonFunction.ShowErrorMessage(panMessage, message)
                Return
            End If
            CommonFunction.SetProcessStatus(btnSaveAirTicket, True)
            CommonFunction.ShowInfoMessage(panMessage, "Your data is saved successfully!")
            LoadBTAirTicket()
        Catch ex As Exception
            CommonFunction.SetProcessStatus(btnSaveAirTicket, False)
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Function IsValidAirTicket(ByVal obj As tblBTAirTicketInfo) As Boolean
        Dim isValid As Boolean = True
        txtAirTicketNo.CssClass = txtAirTicketNo.CssClass.Replace(" validate-error", "")
        ddlAirPeriod.CssClass = ddlAirPeriod.CssClass.Replace(" validate-error", "")
        ddlOraSupplier.CssClass = ddlOraSupplier.CssClass.Replace(" validate-error", "")
        If AirTicketProvider.CheckNo(obj.ID, obj.TicketNo) Then
            CommonFunction.ShowErrorMessage(panMessage, "Ticket no existed!")
            txtAirTicketNo.CssClass &= " validate-error"
            isValid = False
        ElseIf Not AirTicketProvider.CheckPeriodAndSupplier(obj.AirPeriod, obj.OraSupplier) Then
            CommonFunction.ShowErrorMessage(panMessage, "This period and supplier are closed!")
            ddlAirPeriod.CssClass &= " validate-error"
            ddlOraSupplier.CssClass &= " validate-error"
            isValid = False
        End If
        Return isValid
    End Function

    Protected Sub btnCancelAirTicket_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelAirTicket.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btn)
        Try
            ClearAirTicketForm()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditAirTicket_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            txtAirTicketNo.CssClass = txtAirTicketNo.CssClass.Replace(" validate-error", "")
            ddlAirPeriod.CssClass = ddlAirPeriod.CssClass.Replace(" validate-error", "")
            ddlOraSupplier.CssClass = ddlOraSupplier.CssClass.Replace(" validate-error", "")
            '
            'hAirTicketID.Value = btn.Attributes("data-id")            
            LoadBTAirTicket()
            LoadBTAirTicketByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteAirTicket_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim id As Integer = CommonFunction._ToInt(hAirTicketID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            AirTicketProvider.BTAirTicket_Delete(id.ToString())
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
            LoadBTAirTicket()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnViewBT_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            'Dim btn As Button = CType(sender, Button)
            Dim btID As Integer = CommonFunction._ToInt(hItemID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))            
            Dim dtRegister As DataTable = BusinessTripProvider.BTRegister_GetByID(btID)
            Dim dtSubmit As DataTable = ExpenseProvider.BTExpense_GetByID(btID)
            Dim detailsPage = ""
            If dtSubmit.Rows.Count > 0 Then
                Dim btType As String = CommonFunction._ToString(dtSubmit.Rows(0)("BTType"))
                If CommonFunction._ToBoolean(dtSubmit.Rows(0)("IsSubmited")) Then
                    detailsPage = If(btType.IndexOf("oneday_") = 0, "~/BTOneDayExpenseDeclaration.aspx", "~/BTExpenseDeclaration.aspx")
                End If
            End If
            If dtRegister.Rows.Count > 0 Then
                Dim btType As String = CommonFunction._ToString(dtRegister.Rows(0)("BTType"))
                If detailsPage.Trim().Length = 0 Then
                    detailsPage = If(btType.IndexOf("oneday_") = 0, "~/BTOneDayDeclaration.aspx", "~/BTAdvanceDeclaration.aspx")
                End If
                Dim postBackUrl As New StringBuilder()
                postBackUrl.Append(detailsPage)
                Dim params As String = String.Format( _
                    "btid={0}&period={1}&airline={2}&ticket={3}&ecode={4}&ename={5}&depfdate={6}&deptdate={7}&page={8}&psize={9}&supplier={10}&opage={11}&opsize={12}", _
                    btID, hSAirPeriod.Value, hSAirline.Value, hSTicketNo.Value, hSEmployeeCode.Value, hSEmployeeName.Value, hSDepartureFrom.Value, hSDepartureTo.Value, _
                    grvBTAirTicket.PageIndex, grvBTAirTicket.SettingsPager.PageSize, hSOraSupplier.Value, grvOtherAirTicket.PageIndex, grvOtherAirTicket.SettingsPager.PageSize)
                postBackUrl.Append(String.Format("?id={0}&back=BTAirTicket.aspx&params={1}", btID, params.Replace("&", ";amp;").Replace("=", ";eq;")))
                Response.Redirect(postBackUrl.ToString())
            Else
                CommonFunction.ShowErrorMessage(panMessage, "Item not found!")
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    'Click Transfer data to Orc
    Protected Sub btnTransferToOra_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnTransferToOra.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            dteGLDate.Attributes("style") = ""
            '
            If dteInvoiceDate.Text.Trim().Length = 0 OrElse dteGLDate.Text.Trim().Length = 0 Then
                Return
            End If
            Dim invoiceDate As DateTime = dteInvoiceDate.Date
            Dim glDate As DateTime = dteGLDate.Date
            Dim batchName As Integer = CommonFunction._ToInt(ddlBatchName.SelectedValue)
            Dim supplier As String = hSOraSupplier.Value
            Dim period As Integer = CommonFunction._ToInt(hSAirPeriod.Value)
            tabApproveMessage.Attributes("style") = "display: block"
            'Kiem tra xem User da co ben oracle chua                        
            Dim dtSupplierSite As DataTable = AirTicketProvider.BTAirTicket_CheckSupplierSite(supplier)
            If dtSupplierSite.Rows.Count = 0 Then
                CommonFunction.ShowErrorMessage(panMessage, "Supplier sites are not found! Please check Oracle!")
                'ElseIf Not BusinessTripProvider.CheckOraGLDate(glDate) Then
                '    CommonFunction.ShowErrorMessage(panMessage, "GL period is not open!")
                '    dteGLDate.Attributes("style") = "border-color: red;"
            Else
                btnTransferToOra.Visible = False
                Dim supplierSite As String = CommonFunction._ToString(dtSupplierSite.Rows(0)("VendorSite"))
                Dim message As String = AirTicketProvider.TransferToOracle(period, _username, supplier, supplierSite, glDate, batchName, invoiceDate)
                LoadBTAirTicket()
                tabApproveMessage.Attributes("style") = "display: none"
                btnTransferToOra.Visible = True
                If message.Trim().Length > 0 Then
                    CommonFunction.ShowErrorMessage(panMessage, String.Format("Oracle message: {0}", message))
                Else
                    CommonFunction.ShowInfoMessage(panMessage, "Transfer Completed!")
                    '                
                    dteInvoiceDate.Date = DateTime.Now
                    dteGLDate.Date = DateTime.Now
                    ddlBatchName.ClearSelection()
                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnSubmit_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim comment As String = txtSubmitComment.Text.Trim()
            AirTicketProvider.BTAirTicket_GAApprove(CommonFunction._ToInt(hSAirPeriod.Value), hSOraSupplier.Value, comment, _username)
            LoadBTAirTicket()
            'Send notice email
            If SendSubmitEmail() Then
                CommonFunction.ShowInfoMessage(panMessage, "Submited!")
            Else
                CommonFunction.ShowInfoMessage(panMessage, "Submited but fail to send notice emails! Please contact with administrator!")
            End If
            _objEmail.Dispose()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnReject_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnReject.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim comment As String = txtRejectReason.Text.Trim()
            AirTicketProvider.BTAirTicket_FIReject(CommonFunction._ToInt(hSAirPeriod.Value), hSOraSupplier.Value, comment, _username)
            LoadBTAirTicket()
            'Send notice email
            If SendRejectEmail() Then
                CommonFunction.ShowInfoMessage(panMessage, "Rejected!")
            Else
                CommonFunction.ShowInfoMessage(panMessage, "Rejected but fail to send notice emails! Please contact with administrator!")
            End If
            _objEmail.Dispose()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub ddlSAirPeriod_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSAirPeriod.SelectedIndexChanged
        CommonFunction.SetPostBackStatus(ddlSAirPeriod)
        Try
            If Not _isGA Then
                InitSOraSupplier()
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvBTAirTicket_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles grvBTAirTicket.HtmlRowPrepared
        If e.RowType = GridViewRowType.Data Then
            Dim status As String = e.GetValue("FIStatus")
            If status = FIStatus.rejected.ToString() OrElse status = FIStatus.budget_rejected.ToString() Then
                e.Row.CssClass &= " rejected"
            ElseIf status = FIStatus.pending.ToString() Then
                e.Row.CssClass &= " not-found"
            ElseIf status = FIStatus.completed.ToString() Then
                e.Row.CssClass &= " completed"
            ElseIf status = FIStatus.budget_reconfirmed.ToString() Then
                e.Row.CssClass &= " waiting"
            End If
        End If
    End Sub

    Protected Sub grvOtherAirTicket_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles grvOtherAirTicket.HtmlRowPrepared
        If e.RowType = GridViewRowType.Data Then
            Dim status As Boolean = CommonFunction._ToBoolean(e.GetValue("OtherBudgetChecked"))
            If Not status Then
                e.Row.CssClass &= " not-found"
            End If
        End If
    End Sub

    Protected Sub btnViewImportAirTicketError_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnViewImportAirTicketError.Click, btnViewImportOtherError.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try

        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    'Other
    Private Sub EnableOtherAirTicketForm(ByVal enable As Boolean)
        dteOtherDate.ReadOnly = Not enable
        txtOtherAirline.ReadOnly = Not enable
        txtOtherTicketNo.ReadOnly = Not enable
        txtOtherRouting.ReadOnly = Not enable
        spiOtherFare.ReadOnly = Not enable
        spiOtherVAT.ReadOnly = Not enable
        spiOtherAPTTax.ReadOnly = Not enable
        spiOtherSF.ReadOnly = Not enable
        'spiOtherNetPayment.ReadOnly = Not enable
        ddlOtherAirPeriod.Enabled = enable
        ddlOtherOraSupplier.Enabled = enable
        txtOtherPassenger.ReadOnly = Not enable
        txtOtherRequesterCode.ReadOnly = Not enable
        'txtOtherRequesterName.ReadOnly = Not enable
        'txtOtherRequesterDepartment.ReadOnly = Not enable
        'txtOtherRequesterPhone.ReadOnly = Not enable
        txtOtherBudgetCode.ReadOnly = Not enable
        txtOtherPurpose.ReadOnly = Not enable
        'chkICTRequest.Enabled = enable
        ddlOtherAirCurrency.Enabled = enable
        spiOtherAirExrate.ReadOnly = Not enable
        dteOtherDepartureDate.Enabled = enable
        dteOtherReturnDate.Enabled = enable
        '
        btnShowSaveOtherAirTicket.Visible = enable
        btnSaveOtherAirTicket.Visible = enable
        '        
        btnShowConfirmBudget.Visible = False
        btnConfirmBudget.Visible = False
        btnShowRejectBudget.Visible = False
        btnRejectBudget.Visible = False
    End Sub

    Private Sub LoadBTOtherAirTicketByID()
        Dim id As Integer = CommonFunction._ToInt(hOtherAirTicketID.Value)
        Dim dtData As DataTable = AirTicketProvider.BTAirTicket_GetByID(CommonFunction._ToInt(id))
        If dtData.Rows.Count > 0 Then
            Dim drData As DataRow = dtData.Rows(0)
            Dim oraStatus As Boolean = CommonFunction._ToBoolean(drData("EnableForm"))
            EnableOtherAirTicketForm(oraStatus AndAlso _isGA)
            '
            Dim enableBudget As Boolean = CommonFunction._ToBoolean(drData("EnableBudget"))
            If enableBudget AndAlso _isFIBudget Then
                If CommonFunction._ToBoolean(drData("BudgetChecked")) Then
                    btnShowConfirmBudget.Visible = False
                    btnConfirmBudget.Visible = False
                    btnShowRejectBudget.Visible = True
                    btnRejectBudget.Visible = True
                    txtOtherBudgetCode.ReadOnly = True
                Else
                    btnShowConfirmBudget.Visible = True
                    btnConfirmBudget.Visible = True
                    btnShowRejectBudget.Visible = False
                    btnRejectBudget.Visible = False
                    txtOtherBudgetCode.ReadOnly = False
                End If
            Else
                'txtOtherBudgetCode.ReadOnly = True
                btnShowConfirmBudget.Visible = False
                btnConfirmBudget.Visible = False
                btnShowRejectBudget.Visible = False
                btnRejectBudget.Visible = False
            End If
            '
            Dim airDate As DateTime = CommonFunction._ToDateTime(drData("TicketDate"))
            If airDate <> DateTime.MinValue Then
                dteOtherDate.Date = airDate
            End If
            txtOtherAirline.Text = CommonFunction._ToString(drData("AirLine"))
            txtOtherTicketNo.Text = CommonFunction._ToString(drData("TicketNo"))
            txtOtherRouting.Text = CommonFunction._ToString(drData("Routing"))
            spiOtherFare.Value = CommonFunction._ToMoneyWithNull(drData("Fare"))
            spiOtherVAT.Value = CommonFunction._ToMoneyWithNull(drData("VAT"))
            spiOtherAPTTax.Value = CommonFunction._ToMoneyWithNull(drData("APTTax"))
            spiOtherSF.Value = CommonFunction._ToMoneyWithNull(drData("SF"))
            spiOtherNetPayment.Value = CommonFunction._ToMoneyWithNull(drData("NetPayment"))
            CommonFunction.SetCBOValue(ddlOtherAirPeriod, drData("AirPeriod"))
            CommonFunction.SetCBOValue(ddlOtherOraSupplier, drData("OraSupplier"))
            txtOtherPassenger.Text = CommonFunction._ToString(drData("Passenger"))
            txtOtherRequesterCode.Text = CommonFunction._ToString(drData("RequesterCode"))
            txtOtherRequesterName.Text = CommonFunction._ToString(drData("RequesterName"))
            txtOtherRequesterDivision.Text = CommonFunction._ToString(drData("RequesterDiv"))
            txtOtherRequesterDepartment.Text = CommonFunction._ToString(drData("RequesterDept"))
            txtOtherRequesterPhone.Text = CommonFunction._ToString(drData("RequesterPhone"))
            txtOtherBudgetCode.Text = CommonFunction._ToString(drData("BudgetCode"))
            txtOtherPurpose.Text = CommonFunction._ToString(drData("Purpose"))
            'chkICTRequest.Checked = CommonFunction._ToBoolean(drData("ICTRequest"))
            CommonFunction.SetCBOValue(ddlOtherAirCurrency, drData("Currency"))
            spiOtherAirExrate.Value = CommonFunction._ToMoneyWithNull(drData("Exrate"))
            dteOtherDepartureDate.Value = drData("DepartureDate")
            dteOtherReturnDate.Value = drData("ReturnDate")
        End If
    End Sub

    Private Sub ClearOtherAirTicketForm()
        hOtherAirTicketID.Value = ""
        dteOtherDate.Date = DateTime.Now
        txtOtherAirline.Text = ""
        txtOtherTicketNo.Text = ""
        txtOtherRouting.Text = ""
        spiOtherFare.Value = Nothing
        spiOtherVAT.Value = Nothing
        spiOtherAPTTax.Value = Nothing
        spiOtherSF.Value = Nothing
        spiOtherNetPayment.Value = Nothing
        ddlOtherAirPeriod.ClearSelection()
        ddlOtherOraSupplier.ClearSelection()
        txtOtherPassenger.Text = ""
        txtOtherRequesterCode.Text = ""
        txtOtherRequesterName.Text = ""
        txtOtherRequesterDepartment.Text = ""
        txtOtherRequesterDivision.Text = ""
        txtOtherRequesterPhone.Text = ""
        txtOtherBudgetCode.Text = ""
        txtOtherPurpose.Text = ""
        dteOtherDepartureDate.Value = Nothing
        dteOtherReturnDate.Value = Nothing
        'chkICTRequest.Checked = False
        ddlOtherAirCurrency.ClearSelection()
        spiOtherAirExrate.Value = ExpenseProvider.GetExrate(ddlOtherAirCurrency.SelectedValue, "VND", dteOtherDate.Date)
        '
        txtOtherTicketNo.CssClass = txtOtherTicketNo.CssClass.Replace(" validate-error", "")
        ddlOtherAirPeriod.CssClass = ddlOtherAirPeriod.CssClass.Replace(" validate-error", "")
        ddlOtherOraSupplier.CssClass = ddlOtherOraSupplier.CssClass.Replace(" validate-error", "")
        '
        EnableOtherAirTicketForm(_isGA)
        '
        btnShowConfirmBudget.Visible = False
        btnConfirmBudget.Visible = False
        btnShowRejectBudget.Visible = False
        btnRejectBudget.Visible = False
    End Sub

    Private Function GetOtherAirTicketObject() As tblBTAirTicketInfo
        Dim obj As New tblBTAirTicketInfo()
        obj.ID = CommonFunction._ToInt(hOtherAirTicketID.Value)
        obj.TicketDate = dteOtherDate.Date
        obj.AirLine = txtOtherAirline.Text.Trim()
        obj.TicketNo = txtOtherTicketNo.Text.Trim()
        obj.Routing = txtOtherRouting.Text.Trim()
        obj.Fare = CommonFunction._ToMoney(spiOtherFare.Text.Trim())
        obj.VAT = CommonFunction._ToMoney(spiOtherVAT.Text.Trim())
        obj.APTTax = CommonFunction._ToMoney(spiOtherAPTTax.Text.Trim())
        obj.SF = CommonFunction._ToMoney(spiOtherSF.Text.Trim())
        obj.NetPayment = CommonFunction._ToMoney(spiOtherNetPayment.Text.Trim())
        obj.CreatedBy = _username
        obj.ModifiedBy = _username
        obj.AirPeriod = CommonFunction._ToInt(ddlOtherAirPeriod.SelectedValue)
        obj.OraSupplier = ddlOtherOraSupplier.SelectedValue
        obj.Passenger = txtOtherPassenger.Text.Trim()
        obj.Requester = txtOtherRequesterCode.Text.Trim()
        obj.RequesterDiv = txtOtherRequesterDivision.Text.Trim()
        obj.RequesterDept = txtOtherRequesterDepartment.Text.Trim()
        obj.RequesterName = txtOtherRequesterName.Text.Trim()
        obj.RequesterPhone = txtOtherRequesterPhone.Text.Trim()
        obj.BudgetCode = txtOtherBudgetCode.Text.Trim()
        obj.Purpose = txtOtherPurpose.Text.Trim()
        obj.Currency = ddlOtherAirCurrency.SelectedValue
        obj.Exrate = CommonFunction._ToMoney(spiOtherAirExrate.Text.Trim())
        If obj.Exrate = 0 Then
            obj.Exrate = 1
        End If
        'obj.ICTRequest = chkICTRequest.Checked
        obj.DepartureDate = dteOtherDepartureDate.Date
        obj.ReturnDate = dteOtherReturnDate.Date
        Return obj
    End Function

    Protected Sub grvOtherAirTicket_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvOtherAirTicket.BeforeGetCallbackResult
        LoadBTAirTicket()
    End Sub

    Protected Sub btnSaveOtherAirTicket_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveOtherAirTicket.Click
        CommonFunction.SetPostBackStatus(btnSaveOtherAirTicket)
        Try
            Dim obj As tblBTAirTicketInfo = GetOtherAirTicketObject()
            If Not IsValidOtherAirTicket(obj) Then
                CommonFunction.SetProcessStatus(btnSaveOtherAirTicket, False)
                Return
            End If
            Dim message As String = ""
            If obj.ID > 0 Then
                message = AirTicketProvider.BTAirTicket_Update(obj)
            Else
                message = AirTicketProvider.BTAirTicket_Insert(obj)
            End If
            If message.Trim().Length > 0 Then
                CommonFunction.SetProcessStatus(btnSaveOtherAirTicket, False)
                CommonFunction.ShowErrorMessage(panMessage, message)
                Return
            End If
            CommonFunction.SetProcessStatus(btnSaveOtherAirTicket, True)
            CommonFunction.ShowInfoMessage(panMessage, "Your data is saved successfully!")
            LoadBTAirTicket()
        Catch ex As Exception
            CommonFunction.SetProcessStatus(btnSaveOtherAirTicket, False)
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Function IsValidOtherAirTicket(ByVal obj As tblBTAirTicketInfo) As Boolean
        Dim isValid As Boolean = True
        txtOtherTicketNo.CssClass = txtOtherTicketNo.CssClass.Replace(" validate-error", "")
        ddlOtherAirPeriod.CssClass = ddlOtherAirPeriod.CssClass.Replace(" validate-error", "")
        ddlOtherOraSupplier.CssClass = ddlOtherOraSupplier.CssClass.Replace(" validate-error", "")
        If AirTicketProvider.CheckNo(obj.ID, obj.TicketNo) Then
            CommonFunction.ShowErrorMessage(panMessage, "Ticket no existed!")
            txtOtherTicketNo.CssClass &= " validate-error"
            isValid = False
        ElseIf Not AirTicketProvider.CheckPeriodAndSupplier(obj.AirPeriod, obj.OraSupplier) Then
            CommonFunction.ShowErrorMessage(panMessage, "This period and supplier are closed!")
            ddlOtherAirPeriod.CssClass &= " validate-error"
            ddlOtherOraSupplier.CssClass &= " validate-error"
            isValid = False
        End If
        Return isValid
    End Function

    Protected Sub btnCancelOtherAirTicket_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelOtherAirTicket.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btn)
        Try
            ClearOtherAirTicketForm()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditOtherAirTicket_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            txtOtherTicketNo.CssClass = txtOtherTicketNo.CssClass.Replace(" validate-error", "")
            ddlOtherAirPeriod.CssClass = ddlOtherAirPeriod.CssClass.Replace(" validate-error", "")
            ddlOtherOraSupplier.CssClass = ddlOtherOraSupplier.CssClass.Replace(" validate-error", "")
            '
            'hAirTicketID.Value = btn.Attributes("data-id")            
            LoadBTAirTicket()
            LoadBTOtherAirTicketByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteOtherAirTicket_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim id As Integer = CommonFunction._ToInt(hOtherAirTicketID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            AirTicketProvider.BTAirTicket_Delete(id.ToString())
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
            LoadBTAirTicket()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmBudget_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnConfirmBudget.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim obj As tblBTAirTicketInfo = GetOtherAirTicketObject()
            AirTicketProvider.BTAirTicket_ConfirmBudget(obj.ID, obj.BudgetCode)
            CommonFunction.ShowInfoMessage(panMessage, "Budget is confirmed successfully!")
            LoadBTAirTicket()
            LoadBTOtherAirTicketByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEConfirmBudget_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnEConfirmBudget.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim obj As tblBTAirTicketInfo = GetAirTicketObject()
            AirTicketProvider.BTAirTicket_ConfirmBudget(obj.ID, obj.BudgetCode)
            CommonFunction.ShowInfoMessage(panMessage, "Budget is confirmed successfully!")
            LoadBTAirTicket()
            LoadBTAirTicketByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnRejectBudget_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnRejectBudget.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim obj As tblBTAirTicketInfo = GetOtherAirTicketObject()
            AirTicketProvider.BTAirTicket_RejectBudget(obj.ID, obj.BudgetCode)
            CommonFunction.ShowInfoMessage(panMessage, "Budget is rejected successfully!")
            LoadBTAirTicket()
            LoadBTOtherAirTicketByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnERejectBudget_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnERejectBudget.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim obj As tblBTAirTicketInfo = GetAirTicketObject()
            AirTicketProvider.BTAirTicket_RejectBudget(obj.ID, obj.BudgetCode)
            CommonFunction.ShowInfoMessage(panMessage, "Budget is rejected successfully!")
            LoadBTAirTicket()
            LoadBTAirTicketByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmAllBudget_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnConfirmAllBudget.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim supplier As String = hSOraSupplier.Value
            Dim period As Integer = CommonFunction._ToInt(hSAirPeriod.Value)
            Dim comment As String = txtConfirmComment.Text.Trim()
            AirTicketProvider.BTAirTicket_ConfirmBudgetAll(period, supplier, comment, _username)
            LoadBTAirTicket()
            'Send notice email
            If SendConfirmBudgetEmail() Then
                CommonFunction.ShowInfoMessage(panMessage, "Budgets are confirmed successfully!")
            Else
                CommonFunction.ShowInfoMessage(panMessage, "Budgets are confirmed but fail to send notice emails! Please contact with administrator!")
            End If
            _objEmail.Dispose()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnReconfirmAllBudget_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnReconfirmAllBudget.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim supplier As String = hSOraSupplier.Value
            Dim period As Integer = CommonFunction._ToInt(hSAirPeriod.Value)
            Dim comment As String = txtReconfirmComment.Text.Trim()
            AirTicketProvider.BTAirTicket_RejectBudgetAll(period, supplier, comment, _username)
            LoadBTAirTicket()
            'Send notice email
            If SendReConfirmBudgetEmail() Then
                CommonFunction.ShowInfoMessage(panMessage, "Budgets are re-confirmed successfully!")
            Else
                CommonFunction.ShowInfoMessage(panMessage, "Budgets are re-confirmed but fail to send notice emails! Please contact with administrator!")
            End If
            _objEmail.Dispose()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnGetRequesterInfo_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetRequesterInfo.Click
        CommonFunction.SetPostBackStatus(btnGetRequesterInfo)
        Try
            Dim info As String() = hOtherRequester.Value.Split("-")
            txtOtherRequesterCode.Text = info(0)
            txtOtherRequesterName.Text = info(1)
            txtOtherRequesterDivision.Text = info(2)
            txtOtherRequesterDepartment.Text = info(3)
            txtOtherRequesterPhone.Text = info(4)
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnGetEmployeeInfo_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetEmployeeInfo.Click
        CommonFunction.SetPostBackStatus(btnGetEmployeeInfo)
        Try
            Dim info As String() = hEmployeeInfo.Value.Split("-")
            txtEmployeeCode.Text = info(0)
            txtEmployeeName.Text = info(1)
            txtEmployeeDiv.Text = info(2)
            txtEmployeeDept.Text = info(3)
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub
#Region "Send Notice Email"
    Private _period As String = String.Empty
    Private _supplier As String = String.Empty
    Private _fiStatus As String = String.Empty
    Private Function GenNoticeBody(Optional ByVal link As String = "") As String
        Dim periodItem As ListItem = ddlSAirPeriod.SelectedItem
        _period = If(periodItem IsNot Nothing, periodItem.Text, "")
        Dim supplierItem As ListItem = ddlSOraSupplier.SelectedItem
        _supplier = If(supplierItem IsNot Nothing, supplierItem.Text, "")
        _fiStatus = lblBTSStatus.InnerText
        If _fiStatus.Trim().Length > 1 Then
            _fiStatus = String.Concat(_fiStatus.ToUpperInvariant(0), _fiStatus.Substring(1))
        End If
        '
        Dim eBody As New StringBuilder("<p><strong>To whom it may concern</strong></p>")
        eBody.Append("<p>This is notification from <strong>B</strong>usiness <strong>T</strong>rip Online <strong>S</strong>ystem (BTS).</p>")
        eBody.Append("<p>Regarding air ticket invoice, we would like to share the latest information to you as below:")
        eBody.Append(String.Format("<p><table><tr><td valign='top' width='210'><ul><li style='margin: 0;'><strong>Air ticket period:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", _period))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Air ticket supplier:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", _supplier))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Status:</strong></li></ul></td><td style='color: red'><strong>{0}</strong></td></tr>", _fiStatus))
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
            Dim eSubject As String = String.Format("{0}[BTS Air Ticket]: {1} / {2} (Status: {3})", indicator, _period, _supplier, _fiStatus)
            Try
                isSent = _objEmail.SendEmail(eFrom, eTo, "", bcc, eSubject, eBody.ToString(), "", "")
            Catch ex As Exception
                isSent = False
            End Try
        End If
        Return isSent
    End Function

    Private Function SendSubmitEmail() As Boolean
        Dim eTo As String = ""
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToString(Session("Branch")), RoleType.Finance.ToString())
        Else
            Dim dtAuthorized As DataTable = UserProvider.GetActive()
            Dim dvFI As DataView = dtAuthorized.DefaultView
            dvFI.RowFilter = String.Format("Role in ('{0}', '{1}')", RoleType.Finance_Budget.ToString(), RoleType.Administrator.ToString())
            For Each dr As DataRowView In dvFI
                eTo = String.Concat(eTo, ", ", dr("TMVEmail"))
            Next
            If eTo.Length > 0 Then
                eTo = eTo.Substring(1).Trim()
            End If
        End If
        Dim viewLink As String = String.Format("BTAirTicket.aspx?btid=1&period={0}&supplier={1}", hSAirPeriod.Value, hSOraSupplier.Value)
        Dim eBody As String = GenNoticeBody(viewLink)
        Return SendNoticeEmail(eTo, eBody)
    End Function

    Private Function SendConfirmBudgetEmail() As Boolean
        Dim eTo As String = ""
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToString(Session("Branch")), RoleType.Finance.ToString())
        Else
            Dim dtAuthorized As DataTable = UserProvider.GetActive()
            Dim dvFI As DataView = dtAuthorized.DefaultView
            dvFI.RowFilter = String.Format("Role in ('{0}', '{1}', '{2}', '{3}')", RoleType.TOFS_AIR_GA.ToString(), RoleType.Finance.ToString(), RoleType.Finance_GA.ToString(), RoleType.Administrator.ToString())
            For Each dr As DataRowView In dvFI
                eTo = String.Concat(eTo, ", ", dr("TMVEmail"))
            Next
            If eTo.Length > 0 Then
                eTo = eTo.Substring(1).Trim()
            End If
        End If
        Dim viewLink As String = String.Format("BTAirTicket.aspx?btid=1&period={0}&supplier={1}", hSAirPeriod.Value, hSOraSupplier.Value)
        Dim eBody As String = GenNoticeBody(viewLink)
        Return SendNoticeEmail(eTo, eBody)
    End Function

    Private Function SendReConfirmBudgetEmail() As Boolean
        Dim eTo As String = ""
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToString(Session("Branch")), RoleType.Finance.ToString())
        Else
            Dim dtAuthorized As DataTable = UserProvider.GetActive()
            Dim dvFI As DataView = dtAuthorized.DefaultView
            dvFI.RowFilter = String.Format("Role in ('{0}', '{1}', '{2}')", RoleType.TOFS_AIR_GA.ToString(), RoleType.Finance_GA.ToString(), RoleType.Administrator.ToString())
            For Each dr As DataRowView In dvFI
                eTo = String.Concat(eTo, ", ", dr("TMVEmail"))
            Next
            If eTo.Length > 0 Then
                eTo = eTo.Substring(1).Trim()
            End If
        End If
        Dim viewLink As String = String.Format("BTAirTicket.aspx?btid=1&period={0}&supplier={1}", hSAirPeriod.Value, hSOraSupplier.Value)
        Dim eBody As String = GenNoticeBody(viewLink)
        Return SendNoticeEmail(eTo, eBody)
    End Function

    Private Function SendRejectEmail() As Boolean
        Dim eTo As String = ""
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToString(Session("Branch")), RoleType.GA.ToString())
        Else
            Dim dtAuthorized As DataTable = UserProvider.GetActive()
            Dim dvGA As DataView = dtAuthorized.DefaultView
            dvGA.RowFilter = String.Format("Role in ('{0}', '{1}', '{2}')", RoleType.TOFS_AIR_GA.ToString(), RoleType.Finance_GA.ToString(), RoleType.Administrator.ToString())
            For Each dr As DataRowView In dvGA
                eTo = String.Concat(eTo, ", ", dr("TMVEmail"))
            Next
            If eTo.Length > 0 Then
                eTo = eTo.Substring(1).Trim()
            End If
        End If
        Dim viewLink As String = String.Format("BTAirTicket.aspx?btid=1&period={0}&supplier={1}", hSAirPeriod.Value, hSOraSupplier.Value)
        Dim eBody As String = GenNoticeBody(viewLink)
        Return SendNoticeEmail(eTo, eBody)
    End Function
#End Region

End Class