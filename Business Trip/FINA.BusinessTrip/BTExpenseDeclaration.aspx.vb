Imports Provider
Imports System.Data
Imports System.IO
Imports DevExpress.Web.ASPxGridView
Imports Microsoft.Office.Interop
Imports System.Drawing

Partial Public Class BTExpenseDeclaration
    Inherits System.Web.UI.Page

    Protected _username As String = String.Empty
    Protected _dtData As DataTable
    Protected _enable As Boolean = True
    Protected _invEnable As Boolean = False
    Protected _airEnable As Boolean = False
    Protected _isFI As Boolean = False
    Protected _isGA As Boolean = False
    Protected _isAdministrator As Boolean = False
    Protected _isDomestic As Boolean
    Protected _destination As String = String.Empty
    Protected _lastDestination As String = String.Empty
    Private _maAndAbove() As String
    Private _gmAndAbove() As String
    Private _sendEmailMode As String = CommonFunction._ToString(ConfigurationManager.AppSettings("SendEmailMode")).ToLower()
    Private _advanceMovingTime As Decimal = 0
    Private _advanceMovingTimeCurrency As String = ""
    Private _advanceFirstTime As Decimal = 0
    Private _dtBTData As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CommonFunction.CheckSession()
        '
        _maAndAbove = CommonFunction.StrSplit(CommonFunction._ToString(LookupProvider.GetByCodeAndValue("TITLES", "ma_above")).ToLower())
        _gmAndAbove = CommonFunction.StrSplit(CommonFunction._ToString(LookupProvider.GetByCodeAndValue("TITLES", "gm_above")).ToLower())
        '
        _username = CommonFunction._ToString(Session("UserName"))
        txtBudgetCode.Text = If(ddlBudgetName.SelectedValue <> "", ddlBudgetName.SelectedValue.Split("-")(1), "")
        '
        Dim lItem As ListItem = ddlCommonCurrency.SelectedItem
        If lItem IsNot Nothing Then
            lblCommonCCCurrency.Text = lItem.Text
        End If
        Dim hItem As ListItem = ddlHotelCurrency.SelectedItem
        If hItem IsNot Nothing Then
            lblHotelCCCurrency.Text = hItem.Text
        End If
        Dim oItem As ListItem = ddlExpenseOtherCurrency.SelectedItem
        If oItem IsNot Nothing Then
            lblOtherCCCurrency.Text = oItem.Text
        End If
        '
        _isDomestic = ddlBTType.SelectedValue.IndexOf("domestic") >= 0
        '
        Dim budgedCode As Integer = CommonFunction._ToInt(ddlBudgetName.SelectedValue.Split("-")(0))
        txtBudgetRemain.ForeColor = Color.Red
        txtBudgetRemain.Text = "Check by BCS" 'If(budgedCode = -1, "", String.Format("{0:#,0.##} VND", BusinessTripProvider.BTRegister_GetExpenseBudgetRemaining(budgedCode, CommonFunction._ToInt(hID.Value))))
        '
        If Not IsPostBack Then
            InitForm()
        Else
            Dim role As String = CommonFunction._ToString(Session("RoleType"))
            _isFI = role.ToLower() = RoleType.Finance.ToString().ToLower() OrElse role.ToLower() = RoleType.Finance_GA.ToString().ToLower() OrElse role.ToLower() = RoleType.Administrator.ToString().ToLower()
            _isGA = role.ToLower() = RoleType.GA.ToString().ToLower() OrElse role.ToLower() = RoleType.TOFS_AIR_GA.ToString().ToLower() OrElse role.ToLower() = RoleType.Finance_GA.ToString().ToLower() OrElse role.ToLower() = RoleType.Administrator.ToString().ToLower()
            _isAdministrator = role.ToLower() = RoleType.Administrator.ToString().ToLower()
            '
            Dim btID As Integer = CommonFunction._ToInt(hID.Value)
            If btID > 0 Then
                _dtBTData = ExpenseProvider.BTExpense_GetByID(btID)
            Else
                _dtBTData = Nothing
            End If
            If _dtBTData IsNot Nothing AndAlso _dtBTData.Rows.Count > 0 Then
                Dim drData As DataRow = _dtBTData.Rows(0)
                '
                Dim isSubmited As Boolean = CommonFunction._ToBoolean(drData("IsSubmited"))
                Dim finStatus As String = CommonFunction._ToString(drData("FIStatus"))
                'Dim gasStatus As String = CommonFunction._ToString(drData("GAStatus"))
                Dim isOwner As Boolean = CommonFunction._ToString(_dtBTData.Rows(0)("CreatedBy")) = _username
                Dim isEmp As Boolean = CommonFunction._ToString(_dtBTData.Rows(0)("EmployeeCode")) = _username
                Dim isTimeKeeper As Boolean = CommonFunction._ToString(_dtBTData.Rows(0)("TimeKeeper")).IndexOf(_username) >= 0
                '
                spanBtnExportSchedule.Visible = isOwner OrElse _isAdministrator OrElse isEmp OrElse isTimeKeeper OrElse _isGA
                spanExportSchedule1.Visible = isOwner OrElse _isAdministrator OrElse isEmp OrElse isTimeKeeper OrElse _isGA
                spanExportBT.Visible = isOwner OrElse _isAdministrator OrElse isEmp OrElse isTimeKeeper OrElse _isGA
                '
                _enable = (isOwner OrElse isEmp OrElse isTimeKeeper) AndAlso (Not isSubmited OrElse finStatus = FIStatus.rejected.ToString())
                _invEnable = _isFI AndAlso finStatus <> FIStatus.completed.ToString() AndAlso finStatus <> FIStatus.rejected.ToString()
                _airEnable = _isGA
            Else
                _enable = True
                'spanExportBT.Visible = False
                _invEnable = _isFI
                _airEnable = False
            End If
            LoadBTRegisterAttachment(False)
        End If
        '                
        CommonFunction.CheckSessionMessage(Me)
        LoadForm()
    End Sub

    Private Sub InitForm()
        InitCurrency()
        InitBTType()
        'InitBudgetName(True)
        SetPreParams()
        'InitCountry()
        'InitDestination()
        InitInvoiceItem()
        InitBatchName()
        InitOraSupplier()
        dteGLDate.Date = DateTime.Now
        dteInvoiceDate.Date = DateTime.Now
        LoadDataList()
        'LoadInfo()
        ShowInfoRows(False)
    End Sub

    Private Sub SetPreParams()
        Dim btNo As String = CommonFunction._ToString(Request.QueryString("btno"))
        txtBusinessTripNo.Text = btNo
        Dim bttype As String = Request.QueryString("bttype")
        CommonFunction.SetCBOValue(ddlBTType, bttype)
        Dim employeeCode As String = CommonFunction._ToString(Request.QueryString("ecode"))
        txtEmployeeCode.Text = employeeCode
        Dim fullName As String = CommonFunction._ToString(Request.QueryString("ename"))
        txtFullName.Text = fullName
        Dim fromDate As String = CommonFunction._ToString(Request.QueryString("fdate"))
        dteDepartureFrom.Text = fromDate
        Dim toDate As String = CommonFunction._ToString(Request.QueryString("tdate"))
        dteDepartureTo.Text = toDate
        chkBudgetAll.Checked = (CommonFunction._ToString(Request.QueryString("obudget")) = "Y")
        InitBudgetName(True, chkBudgetAll.Checked)
        Dim budget As String = Request.QueryString("bg")
        CommonFunction.SetCBOValue(ddlBudgetName, budget)
        txtBudgetCode.Text = If(ddlBudgetName.SelectedValue <> "", ddlBudgetName.SelectedValue.Split("-")(1), "")
        '
        SetPreSearchCondition()
    End Sub

    Private Sub LoadForm()

    End Sub

    Protected Function GetAuthorizedAccounts() As String
        Dim builder As New StringBuilder()
        Dim dt As DataTable = UserProvider.tbl_User_GetAuthorizedAccounts(_username)
        builder.Append("[")
        For Each item As DataRow In dt.Rows
            builder.Append(String.Concat("{ value: '", item("UserName"), " - ", item("FullName"), "', data: '", item("UserName"), "'},"))
        Next
        builder = New StringBuilder(builder.ToString().TrimEnd(","))
        builder.Append("]")
        Return builder.ToString()
    End Function

    Private Sub InitCurrency()
        Dim dtPolicy As DataTable = LookupProvider.GetByCode("POLICY_CURRENCY")
        CommonFunction.LoadLookupDataToComboBox(ddlCurrency, dtPolicy, False)
        Dim dt As DataTable = LookupProvider.GetByCode("CURRENCY")
        CommonFunction.LoadLookupDataToComboBox(ddlCommonCurrency, dt, False)
        CommonFunction.LoadLookupDataToComboBox(ddlHotelCurrency, dt, False)
        CommonFunction.LoadLookupDataToComboBox(ddlExpenseOtherCurrency, dt, False)
        CommonFunction.LoadLookupDataToComboBox(ddlAirCurrency, dtPolicy, False)
    End Sub

    Private Sub InitAirPeriod(ByVal status As Boolean)
        Dim dt As DataTable = AirTicketProvider.AirPeriod_GetAll()
        CommonFunction.LoadDataToComboBox(ddlAirPeriod, dt, "Name", "ID", True, "", "")
    End Sub

    Private Sub InitBTType()
        Dim dt As DataTable = LookupProvider.GetByCode("BT_TYPE")
        CommonFunction.LoadLookupDataToComboBox(ddlBTType, dt, True, "All", "", "Value like 'overnight_%'")
    End Sub

    Private Sub InitBudgetName(ByVal isSearch As Boolean, Optional ByVal all As Boolean = False, Optional ByVal getAll As Boolean = False)
        Dim dt As DataTable
        If getAll Then
            dt = mBudgetProvider.GetAll()
        Else
            If isSearch Then
                If all Then
                    dt = mBudgetProvider.GetOther(_username)
                Else
                    dt = mBudgetProvider.GetByDepartment(_username)
                End If
            Else
                If all Then
                    dt = mBudgetProvider.GetOtherByType(txtEmployeeCode.Text.Trim(), ddlBTType.SelectedValue, True)
                Else
                    dt = mBudgetProvider.GetByDepartmentAndType(txtEmployeeCode.Text.Trim(), ddlBTType.SelectedValue, True)
                End If
            End If
        End If
        'If all Then
        '    dt = mBudgetProvider.GetOther(If(isSearch, _username, txtEmployeeCode.Text.Trim()))
        'Else
        '    dt = mBudgetProvider.GetByDepartment(If(isSearch, _username, txtEmployeeCode.Text.Trim()))
        'End If
        CommonFunction.LoadDataToComboBox(ddlBudgetName, dt, "BudgetItem", "BudgetCodeItem", True, "")
        txtBudgetCode.Text = If(ddlBudgetName.SelectedValue <> "", ddlBudgetName.SelectedValue.Split("-")(1), "")
    End Sub

    Private Sub InitCountry()
        Dim dt As DataTable = mCountryProvider.GetAll()
        'CommonFunction.LoadDataToComboBox(ddlDestinationCountry, dt, "Name", "Code", True, "", "", If(Not _isDomestic, "Code <> 'VN'", "Code = 'VN'"))
        CommonFunction.LoadDataToComboBox(ddlExpenseDestinationCountry, dt, "Name", "Code", True, "", "", If(Not _isDomestic, "Code <> 'VN'", "Code = 'VN'"))
    End Sub

    'Private Sub InitDestination()
    '    Dim dt As DataTable = mDestinationProvider.GetByCountryCode(ddlDestinationCountry.SelectedValue)
    '    CommonFunction.LoadDataToComboBox(ddlDestinationLocation, dt, "Name", "DestinationID", True, "", "")
    'End Sub

    Private Sub InitExpenseDestination()
        Dim dt As DataTable = mDestinationProvider.GetByCountryCode(ddlExpenseDestinationCountry.SelectedValue)
        CommonFunction.LoadDataToComboBox(ddlExpenseDestinationLocation, dt, "Name", "DestinationID", True, "", "")
    End Sub

    Private Sub InitInvoiceItem()
        Dim dtData As DataTable = mInvoiceItemProvider.GetActive()
        CommonFunction.LoadDataToComboBox(ddlInvItem, dtData, "ItemName", "InvoiceItemID", True, "", "")
    End Sub

    Private Sub InitBatchName()
        Dim dtBT As DataTable = mBatchNameProvider.GetActive()
        CommonFunction.LoadDataToComboBox(ddlBatchName, dtBT, "BatchName", "ID", True, "", "")
    End Sub

    Private Sub InitOraSupplier()
        Dim dtBT As DataTable = mOraSupplierProvider.GetActive()
        CommonFunction.LoadDataToComboBox(ddlOraSupplier, dtBT, "SupplierName", "OraLink", True, "", "")
    End Sub

    'Private Sub LoadTotalSummary()
    '    'Total Summary
    '    Dim dtSummary As DataTable = BusinessTripProvider.BTRegister_GetSummary(CommonFunction._ToInt(hID.Value))
    '    If dtSummary.Rows.Count > 0 Then
    '        Dim drSummary As DataRow = dtSummary.Rows(0)
    '        lblDailyAllowance.Text = CommonFunction._FormatMoney(drSummary("DailyAllowance"))
    '        lblHotelExpense.Text = CommonFunction._FormatMoney(drSummary("HotelExpense"))
    '        lblMovingTimeAllowance.Text = CommonFunction._FormatMoney(drSummary("NMovingTimeAllowance"))
    '        lblOther.Text = CommonFunction._FormatMoney(drSummary("Other"))
    '        lblTotalAdvance.Text = CommonFunction._FormatMoney(drSummary("TotalAdvanceRounded")) 'TotalAdvance
    '        '
    '        Dim totalAdvance As Decimal = CommonFunction._ToMoney(drSummary("TotalAdvance"))
    '        Dim totalAdvanceRounded As Decimal = CommonFunction._ToMoney(drSummary("TotalAdvanceRounded"))
    '        If totalAdvance <> totalAdvanceRounded Then
    '            lblTotalAdvance.ToolTip = String.Format("This total value was rounded by finance rule (Actual value is {0})", CommonFunction._FormatMoney(totalAdvance))
    '        Else
    '            lblTotalAdvance.ToolTip = String.Format("This total value was rounded by finance rule")
    '        End If
    '        'lblRequestGA.Text = CommonFunction._ToString(drSummary("RequestGA"))
    '    End If
    'End Sub

    Private Sub LoadExpenseTotalSummary()
        Dim dtSummary As DataTable = ExpenseProvider.BTExpense_GetSummary(CommonFunction._ToInt(hID.Value))
        If dtSummary.Rows.Count > 0 Then
            Dim drSummary As DataRow = dtSummary.Rows(0)
            Dim btType As String = ddlBTType.SelectedValue
            Dim isDomestic As Boolean = btType.IndexOf("_domestic") >= 0
            lblExpenseDailyAllowance.Text = CommonFunction._FormatMoney(drSummary("DailyAllowance"))
            lblExpenseHotelExpense.Text = CommonFunction._FormatMoney(drSummary("HotelExpense"))
            lblExpenseMovingTimeAllowance.Text = CommonFunction._FormatMoney(drSummary("MovingTimeAllowance"))
            lblExpenseOtherExpense.Text = CommonFunction._FormatMoney(drSummary("OtherAmount"))
            hExpenseOtherAmount.Value = CommonFunction._ToString(drSummary("OtherOnly"))
            lblExpenseTotalExpense.Text = CommonFunction._FormatMoney(drSummary("TotalExpense"))
            lblCashAdvance.Text = CommonFunction._FormatMoney(drSummary("TotalAdvance"))
            lblCreditAdvance.Text = CommonFunction._FormatMoney(drSummary("TotalCredit"))
            lblDisparity.Text = CommonFunction._FormatMoney(drSummary("Disparity"))
            '
            'Dim disparity As Decimal = CommonFunction._ToMoney(drSummary("oDisparity"))
            'Dim disparityRounded As Decimal = CommonFunction._ToMoney(drSummary("Disparity"))
            'If disparity <> disparityRounded Then
            '    lblDisparity.ToolTip = String.Format("This disparity value was rounded by finance rule (Actual value is {0})", CommonFunction._FormatMoney(disparity))
            'Else
            '    lblDisparity.ToolTip = ""
            'End If
        End If
    End Sub

    Private Sub LoadInfo(Optional ByVal loadGrid As Boolean = True)
        'If txtEmployeeCode.Text.Trim().Length = 0 OrElse UserProvider.tbl_User_IsAuthorizedAccount(txtEmployeeCode.Text.Trim()) Then
        '    LoadUserInfo()
        '    If loadGrid Then
        '        LoadDataList()
        '    End If
        'Else
        '    ClearInfoForm()
        'End If
        LoadUserInfo()
        If loadGrid Then
            LoadDataList()
        End If
    End Sub

    Private Sub LoadDataList()
        'Delete draft registers
        BusinessTripProvider.BTRegister_DeleteDraft(_username)
        '
        GetDataTable()
        LoadRegister()
        LoadSubmitted()
        LoadRejected()
        LoadCompleted()
    End Sub

    Private Sub GetDataTable()
        Dim viewID As Integer = CommonFunction._ToInt(Request.QueryString("id"))
        If viewID > 0 Then
            _dtData = ExpenseProvider.tbl_BT_Expense_SearchForView(viewID, _username)
        Else
            Dim btNo As String = hsBTNo.Value
            Dim btType As String = hsBTType.Value
            Dim fullName As String = hsFullName.Value
            Dim empCode As String = hsEmployeeCode.Value
            Dim budgetCode As String = txtBudgetCode.Text.Trim() 'If(hsBudgetName.Value = "All", "", hsBudgetName.Value)
            Dim departureFrom As DateTime = dteDepartureFrom.Date
            Dim departureTo As DateTime = dteDepartureTo.Date

            _dtData = ExpenseProvider.tbl_BT_Expense_Search(btType, fullName, empCode, budgetCode, _username, btNo, departureFrom, departureTo, "overnight")
        End If
    End Sub

    Private Sub LoadRegister()
        CommonFunction.LoadDataToGrid(grvBTRegister, _dtData, "IsSubmited = 0", "No")
    End Sub

    Private Sub LoadSubmitted()
        CommonFunction.LoadDataToGrid(grvBTSubmitted, _dtData, "IsSubmited = 1 and FIStatus <> '" + FIStatus.completed.ToString() + "' and FIStatus <> '" + FIStatus.rejected.ToString() + "'", "No")
    End Sub

    Private Sub LoadRejected()
        CommonFunction.LoadDataToGrid(grvBTRejected, _dtData, "IsSubmited = 1 and FIStatus = '" + FIStatus.rejected.ToString() + "'", "No")
    End Sub

    Private Sub LoadCompleted()
        CommonFunction.LoadDataToGrid(grvBTCompleted, _dtData, "IsSubmited = 1 and FIStatus = '" + FIStatus.completed.ToString() + "'", "No")
    End Sub

    Private Sub LoadBTRegisterInfo()
        Dim dtData As DataTable = BusinessTripProvider.BTRegister_GetByID(CommonFunction._ToInt(hID.Value))
        If dtData.Rows.Count > 0 Then
            Dim drData As DataRow = dtData.Rows(0)
            CommonFunction.SetCBOValue(ddlCurrency, drData("Currency"))
            Dim currencyItem As ListItem = ddlCurrency.SelectedItem
            If currencyItem IsNot Nothing Then
                lblInvNetCostCurrency.Text = currencyItem.Text
                lblInvVATCurrency.Text = currencyItem.Text
                lblInvTotalCurrency.Text = currencyItem.Text
            End If
            'Dim departureDate As DateTime = CommonFunction._ToDateTime(drData("DepartureDate"))
            'If departureDate > DateTime.MinValue Then
            '    dteDepartureDate.Date = departureDate
            'Else
            '    dteDepartureDate.Value = Nothing
            'End If
            'Dim returnDate As DateTime = CommonFunction._ToDateTime(drData("ReturnDate"))
            'If returnDate > DateTime.MinValue Then
            '    dteReturnDate.Date = returnDate
            'Else
            '    dteReturnDate.Value = Nothing
            'End If
            '
            txtBusinessTripNo.Text = CommonFunction._ToString(drData("BTNo"))
            CommonFunction.SetCBOValue(ddlBTType, drData("BTType"))
            '
            _isDomestic = ddlBTType.SelectedValue.IndexOf("domestic") >= 0
            '
            InitCountry()
            Dim country As String = CommonFunction._ToString(drData("CountryCode"))
            If _isDomestic AndAlso country.Trim().Length = 0 Then
                country = "VN"
            End If
            'CommonFunction.SetCBOValue(ddlDestinationCountry, country)
            'InitDestination()
            'txtPurpose.Text = CommonFunction._ToString(drData("Purpose"))
            '
            'chkRequestAirTicket.Checked = CommonFunction._ToBoolean(drData("AirTicket"))
            'chkRequestTrainTicket.Checked = CommonFunction._ToBoolean(drData("TrainTicket"))
            'chkRequestCar.Checked = CommonFunction._ToBoolean(drData("Car"))
            'If chkRequestAirTicket.Checked Then
            '    trExpectedDeparture.Visible = True
            '    trExpectedReturn.Visible = True
            '    dteExpectedDepartureTime.Value = drData("ExpectedDepartureTime")
            '    txtExpectedDepartureFlightNo.Text = CommonFunction._ToString(drData("ExpectedDepartureFlightNo"))
            '    dteExpectedReturnTime.Value = drData("ExpectedReturnTime")
            '    txtExpectedReturnFlightNo.Text = CommonFunction._ToString(drData("ExpectedReturnFlightNo"))
            'Else
            '    dteExpectedDepartureTime.Value = Nothing
            '    dteExpectedReturnTime.Value = Nothing
            '    txtExpectedDepartureFlightNo.Text = ""
            '    txtExpectedReturnFlightNo.Text = ""
            'End If
            'chkShowNoRequestAdvance.Checked = CommonFunction._ToBoolean(drData("NoRequestAdvance"))
            'chkNoRequestAdvance.Checked = chkShowNoRequestAdvance.Checked
            'If chkNoRequestAdvance.Checked Then
            '    tdRequestDestinationControl.Visible = True
            '    tdRequestDestinationLabel.Visible = True
            '    CommonFunction.SetCBOValue(ddlRequestDestination, drData("DestinationID"))
            'Else
            '    ddlRequestDestination.ClearSelection()
            'End If
            '
            txtEmployeeCode.Text = CommonFunction._ToString(drData("EmployeeCode"))
            '
            chkBudgetAll.Checked = CommonFunction._ToBoolean(drData("CheckAllBudget"))
            '
            InitBudgetName(False, chkBudgetAll.Checked, True)
            CommonFunction.SetCBOValue(ddlBudgetName, drData("BudgetName"))
            txtBudgetCode.Text = CommonFunction._ToString(drData("BudgetCode"))
            txtProjectBudgetCode.Text = CommonFunction._ToString(drData("ProjectBudgetCode"))
            '
            Dim budgedCode As Integer = CommonFunction._ToInt(ddlBudgetName.SelectedValue.Split("-")(0))
            'txtBudgetRemain.Text = If(budgedCode = -1, "", String.Format("{0:#,0.##} VND", BusinessTripProvider.BTRegister_GetExpenseBudgetRemaining(budgedCode, CommonFunction._ToInt(hID.Value))))
            'LoadInfo(False)
            txtFullName.Text = CommonFunction._ToString(drData("EmployeeName"))
            txtEmail.Text = CommonFunction._ToString(drData("Email")) '
            txtLocation.Text = CommonFunction._ToString(drData("BranchName"))
            txtDivision.Text = CommonFunction._ToString(drData("DivisionName"))
            txtDepartment.Text = CommonFunction._ToString(drData("DepartmentName"))
            txtSection.Text = CommonFunction._ToString(drData("SectionName"))
            txtPosition.Text = CommonFunction._ToString(drData("Position"))
            txtMobile.Text = CommonFunction._ToString(drData("Mobile"))
            '
            hIsGMAndAbove.Value = If(Array.IndexOf(_maAndAbove, txtPosition.Text.Trim().ToLower()) >= 0, "Y", "N")
            '
            If Array.IndexOf(_maAndAbove, txtPosition.Text.Trim().ToLower()) >= 0 Then
                tdCredit.Visible = True
                chkCredit.Checked = CommonFunction._ToString(drData("PaymentType")) = "CC"
                '
                'spanMovingTime.Visible = False
                'chkMovingTimeAllowance.Checked = False
            Else
                tdCredit.Visible = False
                chkCredit.Checked = False
                '
                'spanMovingTime.Visible = True
                'chkMovingTimeAllowance.Checked = CommonFunction._ToMoney(drData("MovingTimeAllowance")) > 0
                _advanceMovingTime = CommonFunction._ToMoney(drData("MovingTimeAllowance"))
                _advanceMovingTimeCurrency = CommonFunction._ToString(drData("MovingTimeCurrency"))
            End If
            '
            If Not _isDomestic Then
                _advanceFirstTime = CommonFunction._ToMoney(drData("FirstTimeOverSea"))
                'chkFirstTimeOversea.Checked = CommonFunction._ToMoney(drData("FirstTimeOverSea")) > 0
                'spanFirstTimeOversea.Visible = True
                'Else
                '    chkFirstTimeOversea.Checked = False
                '    spanFirstTimeOversea.Visible = False
            End If
            'chkFirstTimeChange()
            'chkMovingTimeChange()
            '            
            'LoadTotalSummary()
        End If
    End Sub

    'Private Sub chkMovingTimeChange()
    '    If chkMovingTimeAllowance.Checked Then
    '        Dim drBT As DataRow = BusinessTripProvider.BTRegister_GetByID(CommonFunction._ToInt(hID.Value)).Rows(0)
    '        Dim movingTimeAmount As Decimal = CommonFunction._ToMoney(drBT("MovingTimeAllowance"))
    '        Dim movingTimeCurrency As String = CommonFunction._ToString(drBT("MovingTimeCurrency"))
    '        If movingTimeAmount <= 0 Then
    '            Dim dtPolicy As DataTable = mMovingTimeAllowanceProvider.m_Expense_GetPolicy(ddlDestinationCountry.SelectedValue)
    '            If dtPolicy.Rows.Count > 0 Then
    '                Dim drPolicy As DataRow = dtPolicy.Rows(0)
    '                movingTimeAmount = CommonFunction._ToMoney(drPolicy("Amount"))
    '                movingTimeCurrency = CommonFunction._ToString(drPolicy("Currency"))
    '            Else
    '                movingTimeAmount = 0
    '                movingTimeCurrency = If(_isDomestic, "VND", "USD")
    '            End If
    '        End If
    '        spanMovingTimeAmount.InnerText = String.Format("{0:#,0.##} {1}", movingTimeAmount, movingTimeCurrency)
    '        spanMovingTime1.Visible = True
    '        If movingTimeCurrency.ToUpper() = "USD" Then
    '            spanMovingTime1.Attributes("style") = spanMovingTime1.Attributes("style").ToLower().Replace("width: 90px", "width: 70px")
    '        Else
    '            spanMovingTime1.Attributes("style") = spanMovingTime1.Attributes("style").ToLower().Replace("width: 70px", "width: 90px")
    '        End If
    '        If movingTimeCurrency.ToUpper() = "USD" AndAlso ddlCurrency.SelectedValue = "vnd" Then
    '            Dim movingTimeAmountVND As Decimal = CommonFunction._ToMoney(drBT("MovingTimeAllowanceVND"))
    '            If movingTimeAmountVND <= 0 Then
    '                movingTimeAmountVND = movingTimeAmount * ExpenseProvider.GetExrate("usd", "vnd")
    '            End If
    '            spiMovingTimeAllowanceVND.Value = CommonFunction._ToMoneyWithNull(movingTimeAmountVND)
    '            spanMovingTime2.Visible = True
    '            spanMovingTime3.Visible = True
    '            spanMovingTime4.Visible = True
    '        Else
    '            spanMovingTime2.Visible = False
    '            spanMovingTime3.Visible = False
    '            spanMovingTime4.Visible = False
    '        End If
    '    Else
    '        spanMovingTime1.Visible = False
    '        spanMovingTime2.Visible = False
    '        spanMovingTime3.Visible = False
    '        spanMovingTime4.Visible = False
    '    End If
    'End Sub

    Private Sub LoadBTExpenseInfo(Optional ByVal reload As Boolean = True)
        If reload Then
            _dtBTData = ExpenseProvider.BTExpense_GetByID(CommonFunction._ToInt(hID.Value))
        End If
        If _dtBTData IsNot Nothing AndAlso _dtBTData.Rows.Count > 0 Then
            Dim drData As DataRow = _dtBTData.Rows(0)
            'For send notice email
            _destination = CommonFunction._ToString(drData("Destination"))
            _lastDestination = CommonFunction._ToString(drData("LastDestination"))
            '
            Dim departureDate As DateTime = CommonFunction._ToDateTime(drData("DepartureDate"))
            If departureDate <> DateTime.MinValue Then
                dteExpenseDepartureDate.Date = departureDate
            Else
                dteExpenseDepartureDate.Value = Nothing
            End If
            '
            LoadExpenseNorm(True)
            '
            Dim returnDate As DateTime = CommonFunction._ToDateTime(drData("ReturnDate"))
            If returnDate <> DateTime.MinValue Then
                dteExpenseReturnDate.Date = returnDate
            Else
                dteExpenseReturnDate.Value = Nothing
            End If
            Dim country As String = CommonFunction._ToString(drData("CountryCode"))
            If _isDomestic AndAlso country.Trim().Length = 0 Then
                country = "VN"
            End If
            CommonFunction.SetCBOValue(ddlExpenseDestinationCountry, country)
            InitExpenseDestination()
            txtExpensePurpose.Text = CommonFunction._ToString(drData("Purpose"))
            '
            lnkFINStatus.Attributes("class") = CommonFunction._ToString(drData("FIStatus"))
            lnkFINStatus.InnerText = CommonFunction._ToString(drData("FIStatusText"))
            lnkFINStatus.Attributes("title") = CommonFunction._ToString(drData("FIStatusDescription"))
            Dim comment As String = CommonFunction._ToString(drData("RejectReasonFI"))
            lblFINComment.Text = If(comment.Trim().Length > 0, String.Format("({0})", comment), "")
            '
            If Array.IndexOf(_maAndAbove, txtPosition.Text.Trim().ToLower()) >= 0 Then
                spanExpenseMovingTime.Visible = False
                chkExpenseMovingTimeAllowance.Checked = False
            Else
                spanExpenseMovingTime.Visible = True
                chkExpenseMovingTimeAllowance.Checked = CommonFunction._ToMoney(drData("MovingTimeAllowance")) > 0
            End If
            '            
            If Not _isDomestic Then
                chkExpenseFirstTimeOversea.Checked = CommonFunction._ToMoney(drData("FirstTimeOverSea")) > 0
                spanExpenseFirstTimeOversea.Visible = True
            Else
                chkExpenseFirstTimeOversea.Checked = False
                spanExpenseFirstTimeOversea.Visible = False
            End If
            'chkFirstTimeChange()
            Dim firstTimeAmount As Decimal = CommonFunction._ToMoney(drData("FirstTimeOverSea"))
            If firstTimeAmount <= 0 Then
                If _advanceFirstTime <= 0 Then
                    firstTimeAmount = CommonFunction._ToMoney(LookupProvider.GetByCodeAndValue("PRE_PARAMS", "FIRST_TIME_OVERSEA"))
                Else
                    firstTimeAmount = _advanceFirstTime
                End If
            End If
            spanExpenseFirstAmount.InnerText = String.Format("({0:#,0.##} USD)", firstTimeAmount)
            hExpenseFirstTime.Value = firstTimeAmount.ToString()
            'chkExpenseMovingTimeChange()
            Dim movingTimeAmount As Decimal = CommonFunction._ToMoney(drData("MovingTimeAllowance"))
            Dim movingTimeCurrency As String = CommonFunction._ToString(drData("MovingTimeCurrency"))
            If movingTimeAmount <= 0 Then
                If _advanceMovingTime <= 0 Then
                    Dim dtPolicy As DataTable = mMovingTimeAllowanceProvider.m_Expense_GetPolicy(ddlExpenseDestinationCountry.SelectedValue)
                    If dtPolicy.Rows.Count > 0 Then
                        Dim drPolicy As DataRow = dtPolicy.Rows(0)
                        movingTimeAmount = CommonFunction._ToMoney(drPolicy("Amount"))
                        movingTimeCurrency = CommonFunction._ToString(drPolicy("Currency"))
                    Else
                        movingTimeAmount = 0
                        movingTimeCurrency = If(_isDomestic, "VND", "USD")
                    End If
                Else
                    movingTimeAmount = _advanceMovingTime
                    movingTimeCurrency = _advanceMovingTimeCurrency
                End If
            End If
            spanExpenseMovingTimeAmount.InnerText = String.Format("({0:#,0.##} {1})", movingTimeAmount, movingTimeCurrency)
            hExpenseMovingTime.Value = movingTimeAmount.ToString()
            '
            ShowOverseaDetails(Not _isDomestic)
            ShowCreditDetails(chkCredit.Checked)
            '
            LoadBTExpenseHistory()
            '
            LoadExpenseTotalSummary()
            '            
            Dim isSubmited As Boolean = CommonFunction._ToBoolean(drData("IsSubmited"))
            Dim finStatus As String = CommonFunction._ToString(drData("FIStatus"))
            'Dim gasStatus As String = CommonFunction._ToString(drData("GAStatus"))
            Dim isOwner As Boolean = CommonFunction._ToString(_dtBTData.Rows(0)("CreatedBy")) = _username
            Dim isEmp As Boolean = CommonFunction._ToString(_dtBTData.Rows(0)("EmployeeCode")) = _username
            Dim isTimeKeeper As Boolean = CommonFunction._ToString(_dtBTData.Rows(0)("TimeKeeper")).IndexOf(_username) >= 0
            '            
            spanBtnExportSchedule.Visible = isOwner OrElse _isAdministrator OrElse isEmp OrElse isTimeKeeper OrElse _isGA
            spanExportSchedule1.Visible = isOwner OrElse _isAdministrator OrElse isEmp OrElse isTimeKeeper OrElse _isGA
            spanExportBT.Visible = isOwner OrElse _isAdministrator OrElse isEmp OrElse isTimeKeeper OrElse _isGA
            '
            _enable = (isOwner OrElse isEmp OrElse isTimeKeeper) AndAlso (Not isSubmited OrElse finStatus = FIStatus.rejected.ToString())
            _invEnable = _isFI AndAlso finStatus <> FIStatus.completed.ToString() AndAlso finStatus <> FIStatus.cancelled.ToString()
            _airEnable = _isGA
            '
            Dim enableRecall As Boolean = (isOwner OrElse isEmp OrElse isTimeKeeper) AndAlso finStatus = FIStatus.pending.ToString()
            btnConfirmRecall.Visible = enableRecall
            btnRecall.Visible = enableRecall
            '
            btnCancel.Attributes("enable-form") = If(_enable, "true", "false")
            btnCancel.Attributes("enable-invoice-form") = If(_invEnable, "true", "false")
            btnCancel.Attributes("enable-air-form") = If(_airEnable, "true", "false")
            EnableForm(_enable)
            '
            EnableApproveButtons()
        End If
    End Sub

    Private Sub ShowOverseaDetails(ByVal enable As Boolean)
        'tdCommonConverted.Visible = enable
        'tdCommonConvertedCaption.Visible = enable
        'tdCommonExrate.Visible = enable
        'tdCommonExrateCaption.Visible = enable
        tdHotelConverted.Visible = enable
        tdHotelConvertedCaption.Visible = enable
        tdHotelExchangeDate.Visible = enable
        tdHotelExchangeDateCaption.Visible = enable
        tdHotelExrate.Visible = enable
        tdHotelExrateCaption.Visible = enable
        trOtherExchange.Visible = enable
    End Sub

    Private Sub ShowCreditDetails(ByVal enable As Boolean)
        trCommonCredit.Visible = enable
        trHotelCredit.Visible = enable
        trOtherCredit.Visible = enable
    End Sub

    Private Sub EnableApproveButtons()
        btnShowApprove.Visible = False
        tabApproveMessage.Visible = False
        btnCheckExtInvoice.Visible = False
        spanApproveLabel.InnerText = "Approve"
        btnApprove.Text = "Approve"
        btnShowReject.Visible = False
        panRejectInfo.Visible = False
        '
        Dim role As String = CommonFunction._ToString(Session("RoleType"))
        If role.ToLower() = RoleType.Finance.ToString().ToLower() _
            OrElse role.ToLower() = RoleType.Administrator.ToString().ToLower() _
            OrElse role.ToLower() = RoleType.Finance_GA.ToString().ToLower() Then
            '
            Dim viewID As Integer = CommonFunction._ToInt(Request.QueryString("id"))
            _dtData = ExpenseProvider.tbl_BT_Expense_ViewByID(viewID, _username)
            '
            If _dtData.Rows.Count > 0 Then
                Dim drData As DataRow = _dtData.Rows(0)
                Dim isSubmit As Boolean = CommonFunction._ToBoolean(drData("IsSubmited"))
                If isSubmit Then
                    Dim status As String = String.Empty
                    Dim oraStatus As String = String.Empty
                    '
                    status = CommonFunction._ToString(drData("FIStatus"))
                    oraStatus = CommonFunction._ToString(drData("OraStatus"))
                    '
                    lnkOraStatus.Attributes("class") = If(oraStatus = "n/a", "", oraStatus)
                    lnkOraStatus.InnerText = If(oraStatus = "deleted", "NO ORACLE INVOICE", oraStatus.ToUpper())
                    tdOraStatus.Visible = True
                    If oraStatus.ToLower() = "error" Then
                        lnkOraStatus.Attributes("data-message") = CommonFunction._ToString(drData("ReasonError"))
                    Else
                        lnkOraStatus.Attributes.Remove("data-message")
                    End If
                    '
                    If status.ToLower() <> FIStatus.rejected.ToString() AndAlso oraStatus.ToLower() <> "done" AndAlso oraStatus.ToLower() <> "paid" Then
                        btnShowReject.Visible = True
                        panRejectInfo.Visible = True
                        btnShowApprove.Visible = True
                        btnCheckExtInvoice.Visible = True
                        tabApproveMessage.Visible = True
                        If status.ToLower() = FIStatus.completed.ToString() Then
                            spanApproveLabel.InnerText = "Re-Approve"
                            btnApprove.Text = "Re-Approve"
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub EnableForm(ByVal enable As Boolean)
        dteExpenseDepartureDate.ReadOnly = Not enable
        dteExpenseReturnDate.ReadOnly = Not enable
        'ddlExpenseDestinationCountry.Enabled = enable AndAlso Not _isDomestic        
        txtExpensePurpose.ReadOnly = Not enable
        chkExpenseMovingTimeAllowance.Enabled = enable
        chkExpenseFirstTimeOversea.Enabled = enable
        'spiExpenseMovingTimeAllowanceVND.ReadOnly = Not enable
        btnFinish.Visible = enable
        btnShowSave.Visible = enable
        btnSubmit.Visible = enable
        btnShowSubmit.Visible = enable
        panSubmitInfo.Visible = enable
        'request                
        EnableRequestForm(enable)
        'other
        EnableOtherForm(enable)
        'schedule
        EnableScheduleForm(enable)
        'attachment
        EnableAttachmentForm(enable)
        'invoice
        EnableInvoiceForm(_invEnable)
        'air
        'EnableAirTicketForm(_airEnable)
    End Sub

    Private Sub EnableRequestForm(ByVal enable As Boolean)
        ddlExpenseDestinationLocation.Enabled = enable
        dteDate.ReadOnly = Not enable
        'spiBreakfastAmount.ReadOnly = Not enable
        'spiLunchAmount.ReadOnly = Not enable
        'spiDinnerAmount.ReadOnly = Not enable
        'spiOtherAmount.ReadOnly = Not enable
        'ddlCommonCurrency.Enabled = enable AndAlso Not _isDomestic
        'spiCommonExrate.ReadOnly = Not enable
        'txtRemark.ReadOnly = Not enable
        spiHotelAmount.ReadOnly = Not enable
        ddlHotelCurrency.Enabled = enable AndAlso Not _isDomestic
        spiHotelExrate.ReadOnly = Not enable
        spiCommonCCAmount.ReadOnly = Not enable
        'chkExpenseMovingTimeAllowance.Enabled = enable
        'chkExpenseFirstTimeOversea.Enabled = enable
        spiHotelCCAmount.ReadOnly = Not enable
        chkCommonCCAmount.Enabled = enable
        chkHotelCCAmount.Enabled = enable
        dteHotelExchangeDate.ReadOnly = Not enable
        chkBreakfastAmount.Enabled = enable
        chkLunchAmount.Enabled = enable
        chkDinnerAmount.Enabled = enable
        chkOtherAmount.Enabled = enable
        '
        btnShowSaveRequest.Visible = enable
        btnSaveRequest.Visible = enable
    End Sub

    Private Sub EnableOtherForm(ByVal enable As Boolean)
        dteExpenseOtherDate.ReadOnly = Not enable
        txtOtherExpense.ReadOnly = Not enable
        ddlExpenseOtherCurrency.Enabled = enable AndAlso Not _isDomestic
        spiExpenseOtherExrate.ReadOnly = Not enable
        spiExpenseOtherAmount.ReadOnly = Not enable
        spiOtherCCAmount.ReadOnly = Not enable
        chkOtherCCAmount.Enabled = enable
        '
        btnShowSaveOther.Visible = enable
        btnSaveOther.Visible = enable
    End Sub

    Private Sub EnableInvoiceForm(ByVal enable As Boolean)
        txtInvNo.ReadOnly = Not enable
        txtInvSellerName.ReadOnly = Not enable
        txtInvSellerTaxCode.ReadOnly = Not enable
        txtInvSerialNo.ReadOnly = Not enable
        txtInvSTT.ReadOnly = Not enable
        'txtInvSupplier.ReadOnly = Not enable
        spiInvNetCost.ReadOnly = Not enable
        'spiInvTaxRate.ReadOnly = Not enable
        spiInvVAT.ReadOnly = Not enable
        dteInvDate.ReadOnly = Not enable
        ddlInvItem.Enabled = enable
        chkInvoiceCredit.Enabled = enable
        '
        btnShowSaveInvoice.Visible = enable
        btnSaveInvoice.Visible = enable
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
        '
        btnShowSaveAirTicket.Visible = enable
        btnSaveAirTicket.Visible = enable
    End Sub

    Private Sub EnableAttachmentForm(ByVal enable As Boolean)
        tdUploadCaption.Visible = enable
        tdUpload1.Visible = enable
        tdUpload2.Visible = enable
        tdUploadDesc.Attributes("colspan") = If(enable, "2", "1")
        txtExpenseDescription.ReadOnly = Not enable
    End Sub

    Private Sub LoadBTExpenseHistory()
        Dim dtData As DataTable = ExpenseProvider.BTExpenseHistory_Search(CommonFunction._ToInt(hID.Value))
        CommonFunction.LoadDataToGrid(grvFIStatusHistory, dtData, "[Type] = '" + UserType.FI.ToString() + "'", "No")
    End Sub

    'Private Sub LoadBTRegisterRequest()
    '    Dim dtData As DataTable = BusinessTripProvider.BTRegisterRequest_Search(CommonFunction._ToInt(hID.Value))
    '    CommonFunction.LoadDataToGrid(grvBTRequest, dtData)
    '    '
    '    LoadTotalSummary()
    'End Sub

    Private Sub LoadBTExpenseRequest()
        Dim dtData As DataTable = ExpenseProvider.BTExpenseRequest_Search(CommonFunction._ToInt(hID.Value))
        dtData.Columns.Add("TotalAmountFormated", GetType(String))
        dtData.Columns.Add("HotelAmountFormated", GetType(String))
        dtData.Columns.Add("MovingTimeAmountFormated", GetType(String))
        For Each item As DataRow In dtData.Rows
            item("TotalAmountFormated") = String.Concat(CommonFunction._FormatMoney(item("TotalAmount")), " ", CommonFunction._ToString(item("AllowanceCurrency")))
            item("HotelAmountFormated") = String.Concat(CommonFunction._FormatMoney(item("HotelAmount")), " ", CommonFunction._ToString(item("HotelCurrency")))
        Next
        CommonFunction.LoadDataToGrid(grvCommonExpense, dtData, "", Nothing, "EnableForm", _enable)
        '
        LoadExpenseTotalSummary()
    End Sub

    'Private Sub LoadBTRegisterRequestByID()
    '    Dim id As Integer = CommonFunction._ToInt(hRequestID.Value)
    '    Dim dtData As DataTable = BusinessTripProvider.BTRegisterRequest_GetByID(id)
    '    If dtData.Rows.Count > 0 Then
    '        Dim drData As DataRow = dtData.Rows(0)
    '        CommonFunction.SetCBOValue(ddlDestinationLocation, drData("DestinationID"))
    '        Dim fromDate As DateTime = CommonFunction._ToDateTime(drData("FromDate"))
    '        Dim toDate As DateTime = CommonFunction._ToDateTime(drData("ToDate"))
    '        If fromDate <> DateTime.MinValue Then
    '            dteFromDate.Date = fromDate
    '        Else
    '            dteFromDate.Value = Nothing
    '        End If
    '        If toDate <> DateTime.MinValue Then
    '            dteToDate.Date = toDate
    '        Else
    '            dteToDate.Value = Nothing
    '        End If
    '        txaRemark.Text = CommonFunction._ToString(drData("Remark"))
    '        'request details            
    '        txtBreakfastQty.InnerText = CommonFunction._FormatMoney(drData("BreakfastQty"))
    '        txtLunchQty.InnerText = CommonFunction._FormatMoney(drData("LunchQty"))
    '        txtDinnerQty.InnerText = CommonFunction._FormatMoney(drData("DinnerQty"))
    '        txtBreakfastUnit.InnerText = CommonFunction._FormatMoney(drData("BreakfastUnit"))
    '        txtLunchUnit.InnerText = CommonFunction._FormatMoney(drData("LunchUnit"))
    '        txtDinnerUnit.InnerText = CommonFunction._FormatMoney(drData("DinnerUnit"))
    '        txtOtherMealQty.InnerText = CommonFunction._FormatMoney(drData("OtherMealQty"))
    '        txtOtherMealUnit.InnerText = CommonFunction._FormatMoney(drData("OtherMealUnit"))
    '        txtOther.Text = CommonFunction._ToString(drData("Other"))
    '        txtOtherAmount.Value = CommonFunction._ToMoneyWithNull(drData("OtherUnit"))
    '        txtHotelQty.InnerText = CommonFunction._FormatMoney(drData("HotelQty"))
    '        txtHotelUnit.InnerText = CommonFunction._FormatMoney(drData("HotelUnit"))
    '        txtBreakfastAmount.InnerText = CommonFunction._FormatMoney(drData("BreakfastAmount"))
    '        txtLunchAmount.InnerText = CommonFunction._FormatMoney(drData("LunchAmount"))
    '        txtDinnerAmount.InnerText = CommonFunction._FormatMoney(drData("DinnerAmount"))
    '        txtOtherMealAmount.InnerText = CommonFunction._FormatMoney(drData("OtherMealAmount"))
    '        txtHotelAmount.InnerText = CommonFunction._FormatMoney(drData("HotelAmount"))
    '        txtTotalAmount.InnerText = CommonFunction._FormatMoney(drData("TotalAmount"))
    '    End If
    'End Sub

    Private Sub LoadBTExpenseRequestByID()
        Dim id As Integer = CommonFunction._ToInt(hExpenseRequestID.Value)
        Dim dtData As DataTable = ExpenseProvider.BTExpenseRequest_GetByID(id)
        If dtData.Rows.Count > 0 Then
            Dim drData As DataRow = dtData.Rows(0)
            CommonFunction.SetCBOValue(ddlExpenseDestinationLocation, drData("DestinationID"))
            'LoadExpenseNorm(True)
            Dim ddate As DateTime = CommonFunction._ToDateTime(drData("Date"))
            If ddate <> DateTime.MinValue Then
                dteDate.Date = ddate
            Else
                dteDate.Value = Nothing
            End If
            'spiBreakfastAmount.Value = CommonFunction._ToMoneyWithNull(drData("BreakfastAmount"))
            Dim breakfast As Decimal = CommonFunction._ToMoney(drData("BreakfastAmount"))
            hBreakfastAmount.Value = breakfast.ToString()
            chkBreakfastAmount.Checked = breakfast > 0
            'spiLunchAmount.Value = CommonFunction._ToMoneyWithNull(drData("LunchAmount"))
            Dim lunch As Decimal = CommonFunction._ToMoney(drData("LunchAmount"))
            hLunchAmount.Value = lunch.ToString()
            chkLunchAmount.Checked = lunch > 0
            'spiDinnerAmount.Value = CommonFunction._ToMoneyWithNull(drData("DinnerAmount"))
            Dim dinner As Decimal = CommonFunction._ToMoney(drData("DinnerAmount"))
            hDinnerAmount.Value = dinner.ToString()
            chkDinnerAmount.Checked = dinner > 0
            'spiOtherAmount.Value = CommonFunction._ToMoneyWithNull(drData("OtherAmount"))
            Dim other As Decimal = CommonFunction._ToMoney(drData("OtherAmount"))
            hOtherAmount.Value = other.ToString()
            chkOtherAmount.Checked = other > 0
            'spiCommonTotalAmount.Value = CommonFunction._ToMoneyWithNull(drData("TotalAmount"))
            CommonFunction.SetCBOValue(ddlCommonCurrency, drData("AllowanceCurrency"))
            Dim lItem As ListItem = ddlCommonCurrency.SelectedItem
            If lItem IsNot Nothing Then
                lblCommonCCCurrency.Text = lItem.Text
            End If
            'spiCommonExrate.Value = CommonFunction._ToMoneyWithNull(drData("AllowanceExrate"))
            'spiCommonTotalConverted.Value = CommonFunction._ToMoneyWithNull(drData("TotalAmountConverted"))
            'txtRemark.Text = CommonFunction._ToString(drData("Remark"))
            spiHotelAmount.Value = CommonFunction._ToMoneyWithNull(drData("HotelAmount"))
            CommonFunction.SetCBOValue(ddlHotelCurrency, drData("HotelCurrency"))
            Dim hItem As ListItem = ddlHotelCurrency.SelectedItem
            If hItem IsNot Nothing Then
                lblHotelCCCurrency.Text = hItem.Text
            End If
            spiHotelExrate.Value = CommonFunction._ToMoneyWithNull(drData("HotelExrate"))
            spiHotelTotalConverted.Value = CommonFunction._ToMoneyWithNull(drData("HotelAmountConverted"))
            'If CommonFunction._ToMoney(spiOtherAmount.Text.Trim()) > 0 Then
            '    trExpenseOtherExplaination.Attributes("style") = ""
            'Else
            '    trExpenseOtherExplaination.Attributes("style") = "display: none"
            'End If
            spiCommonCCAmount.Value = CommonFunction._ToMoneyWithNull(drData("CreditAmount"))
            If spiCommonCCAmount.Number > 0 Then
                chkCommonCCAmount.Checked = True
                'spiCommonCCAmount.Visible = True
                'lblCommonCCCurrency.Visible = True
            Else
                chkCommonCCAmount.Checked = False
                'lblCommonCCCurrency.Visible = False
                'spiCommonCCAmount.Visible = False
            End If
            spiHotelCCAmount.Value = CommonFunction._ToMoneyWithNull(drData("HotelCreditAmount"))
            If spiHotelCCAmount.Number > 0 Then
                chkHotelCCAmount.Checked = True
                'spiHotelCCAmount.Visible = True
                'lblHotelCCCurrency.Visible = True
            Else
                chkHotelCCAmount.Checked = False
                'spiHotelCCAmount.Visible = False
                'lblHotelCCCurrency.Visible = False
            End If
            Dim hotelExdate As DateTime = CommonFunction._ToDateTime(drData("HotelExdate"))
            If hotelExdate <> DateTime.MinValue Then
                dteHotelExchangeDate.Date = hotelExdate
            Else
                dteHotelExchangeDate.Value = Nothing
            End If
            '
            dteDate.Attributes("style") = "float: left"
            lblCommonCCMessage.Text = ""
            lblCommonCCMessage.Visible = False
            lblHotelCCMessage.Text = ""
            lblHotelCCMessage.Visible = False
            spiCommonCCAmount.Attributes("style") = "float: left; width: 50px !important;"
            spiHotelCCAmount.Attributes("style") = "float: left; width: 50px !important;"
        End If
    End Sub

    'Private Sub LoadBTRegisterSchedule()
    '    Dim dtData As DataTable = BusinessTripProvider.BTRegisterSchedule_Search(CommonFunction._ToInt(hID.Value))
    '    CommonFunction.LoadDataToGrid(grvBTSchedule, dtData)
    'End Sub

    'Private Sub LoadBTRegisterScheduleByID()
    '    Dim id As Integer = CommonFunction._ToInt(hScheduleID.Value)
    '    Dim dtData As DataTable = BusinessTripProvider.BTRegisterSchedule_GetByID(CommonFunction._ToInt(id))
    '    If dtData.Rows.Count > 0 Then
    '        Dim drData As DataRow = dtData.Rows(0)
    '        Dim scheduleDate As DateTime = CommonFunction._ToDateTime(drData("FromTime"))
    '        If scheduleDate <> DateTime.MinValue Then
    '            dteScheduleDate.Date = scheduleDate
    '        Else
    '            dteScheduleDate.Value = Nothing
    '        End If
    '        txeFromTime.DateTime = CommonFunction._ToDateTime(drData("FromTime"))
    '        txeToTime.DateTime = CommonFunction._ToDateTime(drData("ToTime"))
    '        txtWorkingArea.Text = CommonFunction._ToString(drData("WorkingArea"))
    '        txtTask.Text = CommonFunction._ToString(drData("Task"))
    '        spiEstimateTransportationFee.Value = CommonFunction._ToMoneyWithNull(drData("EstimateTransportationFee"))
    '        'chkAirTicket.Checked = CommonFunction._ToBoolean(drData("AirTicket"))
    '        'chkTrainTicket.Checked = CommonFunction._ToBoolean(drData("TrainTicket"))
    '        'chkCar.Checked = CommonFunction._ToBoolean(drData("Car"))
    '    End If
    'End Sub

    Private Sub LoadBTExpenseOther()
        Dim dtData As DataTable = ExpenseProvider.BTExpenseOther_Search(CommonFunction._ToInt(hID.Value))
        CommonFunction.LoadDataToGrid(grvBTExpenseOther, dtData, "", Nothing, "EnableForm", _enable)
        '
        LoadExpenseTotalSummary()
    End Sub

    Private Sub LoadBTExpenseOtherByID()
        Dim id As Integer = CommonFunction._ToInt(hExpenseOtherID.Value)
        Dim dtData As DataTable = ExpenseProvider.BTExpenseOther_GetByID(CommonFunction._ToInt(id))
        If dtData.Rows.Count > 0 Then
            Dim drData As DataRow = dtData.Rows(0)
            Dim scheduleDate As DateTime = CommonFunction._ToDateTime(drData("Date"))
            If scheduleDate <> DateTime.MinValue Then
                dteExpenseOtherDate.Date = scheduleDate
            Else
                dteExpenseOtherDate.Value = Nothing
            End If
            txtOtherExpense.Text = CommonFunction._ToString(drData("Expense"))
            CommonFunction.SetCBOValue(ddlExpenseOtherCurrency, drData("Currency"))
            Dim oItem As ListItem = ddlExpenseOtherCurrency.SelectedItem
            If oItem IsNot Nothing Then
                lblOtherCCCurrency.Text = oItem.Text
            End If
            spiExpenseOtherExrate.Value = CommonFunction._ToMoneyWithNull(drData("Exrate"))
            spiExpenseOtherAmount.Value = CommonFunction._ToMoneyWithNull(drData("Amount"))
            txtExpenseOtherAmountConverted.Text = CommonFunction._FormatMoney(drData("AmountConverted"))
            spiOtherCCAmount.Value = CommonFunction._ToMoneyWithNull(drData("CreditAmount"))
            If spiOtherCCAmount.Number > 0 Then
                chkOtherCCAmount.Checked = True
                'spiOtherCCAmount.Visible = True
                'lblOtherCCCurrency.Visible = True
            Else
                chkOtherCCAmount.Checked = False
                'spiOtherCCAmount.Visible = False
                'lblOtherCCCurrency.Visible = False
            End If
            lblOtherCCMessage.Text = ""
            lblOtherCCMessage.Visible = False
            spiOtherCCAmount.Attributes("style") = "float: left; width: 50px !important;"
        End If
    End Sub

    Private Sub LoadBTRegisterAttachment(ByVal withDesc As Boolean)
        ClearAttachmentForm(withDesc)
        Dim dtData As DataTable = BusinessTripProvider.BTRAttachment_Search(CommonFunction._ToInt(hID.Value))

        Dim otherFiles As New StringBuilder()
        Dim otherExpenseFiles As New StringBuilder()
        Dim desc As String = ""
        Dim expendDesc As String = ""
        For Each item As DataRow In dtData.Rows
            Dim path As String = CommonFunction._ToString(item("AttachmentPath"))
            Dim fileName As String = path.Substring(path.LastIndexOf("/") + 1).Substring(25)
            Dim attType As String = CommonFunction._ToString(item("AttachmentType"))
            Dim li As String = String.Format("<li style='font-weight: normal'><a href='{0}' target='_blank'>{1}</a>{2}</li>", path, fileName, If(_enable, If(attType = "expense" OrElse attType = "other-expense", String.Format("<input type='button' class='grid-btn delete-btn' title='Remove' data-id='{0}' onclick='DeleteAttachment(this)'>", item("ID")), ""), ""))
            Select Case attType
                'Case "register"
                '    'If withDesc Then txtAttRegisterDesc.Text = desc
                '    Dim ltrRegister As New Literal()
                '    ltrRegister.Text = String.Format("<ol class='attachments'>{0}</ol>", li)
                '    panRegisterAttachments.Controls.Add(ltrRegister)
                '    desc = CommonFunction._ToString(item("Description"))
                'Case "schedule"
                '    'If withDesc Then txtAttScheduleDesc.Text = desc
                '    Dim ltrSchedule As New Literal()
                '    ltrSchedule.Text = String.Format("<ol class='attachments'>{0}</ol>", li)
                '    panScheduleAttachments.Controls.Add(ltrSchedule)
                '    desc = CommonFunction._ToString(item("Description"))
                Case "expense"
                    'If withDesc Then txtAttScheduleDesc.Text = desc
                    Dim ltrExpense As New Literal()
                    ltrExpense.Text = String.Format("<ol class='attachments'>{0}</ol>", li)
                    panExpenseAttachments.Controls.Add(ltrExpense)
                    expendDesc = CommonFunction._ToString(item("Description"))
                Case "other"
                    desc = CommonFunction._ToString(item("Description"))
                    otherFiles.Append(li)
                Case "other-expense"
                    expendDesc = CommonFunction._ToString(item("Description"))
                    otherExpenseFiles.Append(li)
            End Select
        Next
        If withDesc Then
            'txtDescription.Text = desc
            txtExpenseDescription.Text = expendDesc
        End If
        '
        'If otherFiles.ToString().Trim().Length > 0 Then
        '    Dim ltrOther As New Literal()
        '    ltrOther.Text = String.Format("<ol class='attachments'>{0}</ol>", otherFiles.ToString())
        '    panOthersAttachments.Controls.Add(ltrOther)
        'End If
        If otherExpenseFiles.ToString().Trim().Length > 0 Then
            Dim ltrOther As New Literal()
            ltrOther.Text = String.Format("<ol class='attachments'>{0}</ol>", otherExpenseFiles.ToString())
            panOtherExpenseAttachments.Controls.Add(ltrOther)
        End If
    End Sub

    Private Sub LoadBTInvoice()
        Dim dtData As DataTable = InvoiceProvider.BTInvoice_Search(CommonFunction._ToInt(hID.Value))
        dtData.Columns.Add("TotalAmountFormated", GetType(String))
        For Each row As DataRow In dtData.Rows
            row("TotalAmountFormated") = String.Concat(CommonFunction._FormatMoney(row("Total")), " ", CommonFunction._ToString(row("Currency")))
        Next
        CommonFunction.LoadDataToGrid(grvBTInvoice, dtData, "", Nothing, "EnableInvForm", _invEnable)
    End Sub

    Private Sub LoadBTInvoiceByID()
        Dim id As Integer = CommonFunction._ToInt(hInvoiceID.Value)
        Dim dtData As DataTable = InvoiceProvider.BTInvoice_GetByID(CommonFunction._ToInt(id))
        If dtData.Rows.Count > 0 Then
            Dim drData As DataRow = dtData.Rows(0)
            txtInvSTT.Text = CommonFunction._ToString(drData("STT"))
            txtInvNo.Text = CommonFunction._ToString(drData("InvNo"))
            Dim invDate As DateTime = CommonFunction._ToDateTime(drData("InvDate"))
            If invDate <> DateTime.MinValue Then
                dteInvDate.Date = invDate
            Else
                dteInvDate.Value = Nothing
            End If
            txtInvSerialNo.Text = CommonFunction._ToString(drData("SerialNo"))
            txtInvSellerName.Text = CommonFunction._ToString(drData("SellerName"))
            txtInvSellerTaxCode.Text = CommonFunction._ToString(drData("SellerTaxCode"))
            spiInvNetCost.Value = CommonFunction._ToMoneyWithNull(drData("NetCost"))
            'spiInvTaxRate.Value = CommonFunction._ToMoneyWithNull(drData("TaxRate"))
            CommonFunction.SetCBOValue(ddlInvItem, drData("Item"))
            spiInvVAT.Value = CommonFunction._ToMoneyWithNull(drData("VAT"))
            spiInvTotal.Text = CommonFunction._FormatMoney(drData("Total"))
            'txtInvSupplier.Text = CommonFunction._ToString(drData("Supplier"))            
            chkInvoiceCredit.Checked = CommonFunction._ToBoolean(drData("IsCreditCard"))
            trInvoiceCredit.Visible = chkCredit.Checked
            'chkInvoiceCredit.Visible = chkCredit.Checked
        End If
    End Sub

    Private Sub ClearInvoiceForm()
        hInvoiceID.Value = ""
        dteInvDate.Date = If(dteExpenseDepartureDate.Value IsNot Nothing, dteExpenseDepartureDate.Date, DateTime.Now)
        txtInvSTT.Text = ""
        txtInvNo.Text = ""
        txtInvSerialNo.Text = ""
        txtInvSellerName.Text = ""
        txtInvSellerTaxCode.Text = ""
        spiInvNetCost.Value = Nothing
        'spiInvTaxRate.Value = Nothing
        spiInvVAT.Value = Nothing
        spiInvTotal.Text = "0"
        ddlInvItem.ClearSelection()
        'txtInvSupplier.Text = ""
        chkInvoiceCredit.Checked = False
        trInvoiceCredit.Visible = chkCredit.Checked
        '
        txtInvNo.CssClass = txtInvNo.CssClass.Replace(" validate-error", "")
    End Sub

    Private Sub SetPreSearchCondition()
        hsBTNo.Value = txtBusinessTripNo.Text
        hsBTType.Value = ddlBTType.SelectedValue
        hsEmployeeCode.Value = txtEmployeeCode.Text
        hsFullName.Value = txtFullName.Text
        hsBudgetName.Value = ddlBudgetName.SelectedValue
        hsDepartureFrom.Value = dteDepartureFrom.Text
        hsDepartureTo.Value = dteDepartureTo.Text
        hsBudgetAll.Value = If(chkBudgetAll.Checked, "Y", "N")
    End Sub

    Private Sub LoadUserInfo()
        ClearInfoForm()
        'Dim objUser As tbl_UsersInfo = UserProvider.tbl_User_GetUserInfo_ByUserName(txtEmployeeCode.Text.Trim())
        'If objUser Is Nothing Then
        '    ClearInfoForm()
        'Else
        '    txtFullName.Text = objUser.FullName
        '    txtEmail.Text = objUser.TMVEmail
        '    txtLocation.Text = objUser.BranchName
        '    txtDivision.Text = objUser.DivisionName
        '    txtDepartment.Text = objUser.DepartmentName
        '    txtSection.Text = objUser.SectionName
        '    txtPosition.Text = objUser.JobBand
        '    txtMobile.Text = objUser.Mobile
        '    chkCredit.Checked = objUser.IsCreditCard
        'End If
    End Sub

    Private Sub ClearInfoForm()
        txtFullName.Text = ""
        txtEmail.Text = ""
        txtLocation.Text = ""
        txtDivision.Text = ""
        txtDepartment.Text = ""
        txtSection.Text = ""
        txtMobile.Text = ""
        txtPosition.Text = ""
        '
        '_dtData = Nothing
        'LoadRegister()
        'LoadSubmitted()
        'LoadCompleted()
    End Sub

    Private Sub EnableInfoForm(ByVal enable As Boolean)
        ddlBTType.Enabled = enable
        txtEmployeeCode.ReadOnly = Not enable
        ddlBudgetName.Enabled = enable
        btnSearchEmpInfo.Visible = enable
        txtMobile.ReadOnly = Not enable
        txtFullName.ReadOnly = Not enable
        txtBusinessTripNo.ReadOnly = Not enable
    End Sub

    Private Sub ShowInfoRows(ByVal show As Boolean)
        'trGeneral1.Visible = show
        trGeneral2.Visible = show
        trGeneral3.Visible = show
        trGeneral4.Visible = show
        trGeneral5.Visible = show
        trGeneral6.Visible = show
        '
        trGeneral0.Visible = Not show
        '        
        chkBudgetAll.Enabled = Not show
        If show Then
            'spanBudgetAll.Visible = False
            'ddlBudgetName.Attributes("style") = ""
            InitBudgetName(False, chkBudgetAll.Checked)
        Else
            'spanBudgetAll.Visible = True
            'ddlBudgetName.Attributes("style") = "width: 160px !important"
            InitBudgetName(True, chkBudgetAll.Checked)
        End If
    End Sub

    Private Sub ClearBTForm()
        hID.Value = ""
        dteExpenseDepartureDate.Date = DateTime.Now
        dteExpenseReturnDate.Date = DateTime.Now
        ddlExpenseDestinationCountry.ClearSelection()
        InitExpenseDestination()
        txtExpensePurpose.Text = ""
        chkExpenseMovingTimeAllowance.Checked = False
        chkExpenseFirstTimeOversea.Checked = False
        'chkExpenseMovingTimeChange()
        '        
        InitBTStatus()
    End Sub

    Private Sub LoadExpenseNorm(Optional ByVal withHotel As Boolean = False)
        Dim dtNorm As DataTable = mExpenseProvider.m_Expense_GetNorm(CommonFunction._ToInt(hID.Value), dteExpenseDepartureDate.Date) ', CommonFunction._ToInt(ddlExpenseDestinationLocation.SelectedValue)
        If dtNorm.Rows.Count > 0 Then
            'spiBreakfastAmount.Value = If(chkBreakfastAmount.Checked, CommonFunction._ToMoneyWithNull(dtNorm.Rows(0)("Breakfast")), Nothing)
            'spiLunchAmount.Value = If(chkLunchAmount.Checked, CommonFunction._ToMoneyWithNull(dtNorm.Rows(0)("Lunch")), Nothing)
            'spiDinnerAmount.Value = If(chkDinnerAmount.Checked, CommonFunction._ToMoneyWithNull(dtNorm.Rows(0)("Dinner")), Nothing)
            'spiOtherAmount.Value = If(chkOtherAmount.Checked, CommonFunction._ToMoneyWithNull(dtNorm.Rows(0)("OtherMeal")), Nothing)
            '
            Dim breakfast As Decimal = CommonFunction._ToMoney(dtNorm.Rows(0)("Breakfast"))
            Dim lunch As Decimal = CommonFunction._ToMoney(dtNorm.Rows(0)("Lunch"))
            Dim dinner As Decimal = CommonFunction._ToMoney(dtNorm.Rows(0)("Dinner"))
            Dim other As Decimal = CommonFunction._ToMoney(dtNorm.Rows(0)("OtherMeal"))
            hBreakFastUnit.Value = breakfast.ToString()
            hLunchUnit.Value = lunch.ToString()
            hDinnerUnit.Value = dinner.ToString()
            hOtherUnit.Value = other.ToString()
            '
            Dim currency As String = CommonFunction._ToString(dtNorm.Rows(0)("Currency"))
            CommonFunction.SetCBOValue(ddlCommonCurrency, currency)
            Dim lItem As ListItem = ddlCommonCurrency.SelectedItem
            If lItem IsNot Nothing Then
                lblCommonCCCurrency.Text = lItem.Text
            End If
            '
            'spiBreakfastAmount.MaxValue = GetSpiMaxValue(breakfast)
            'spiLunchAmount.MaxValue = GetSpiMaxValue(lunch)
            'spiDinnerAmount.MaxValue = GetSpiMaxValue(dinner)
            'spiOtherAmount.MaxValue = GetSpiMaxValue(other)
            '
            If withHotel Then
                hHotelUnit.Value = CommonFunction._ToMoney(dtNorm.Rows(0)("Hotel")).ToString()
                'spiHotelAmount.Value = CommonFunction._ToMoneyWithNull(dtNorm.Rows(0)("Hotel"))
                CommonFunction.SetCBOValue(ddlHotelCurrency, currency)
                Dim hItem As ListItem = ddlHotelCurrency.SelectedItem
                If hItem IsNot Nothing Then
                    lblHotelCCCurrency.Text = hItem.Text
                End If
                'spiHotelAmount.MaxValue = GetSpiMaxValue(spiHotelAmount.Number)
                'If _isDomestic Then
                '    If Array.IndexOf(_gmAndAbove, txtPosition.Text.Trim().ToLower()) >= 0 Then
                '        spiHotelAmount.MaxValue = GetSpiMaxValue(0)
                '    End If
                'Else
                '    spiHotelAmount.MaxValue = GetSpiMaxValue(0)
                'End If
            End If
        Else
            hBreakFastUnit.Value = "0"
            hLunchUnit.Value = "0"
            hDinnerUnit.Value = "0"
            hOtherUnit.Value = "0"
            '
            'spiBreakfastAmount.MaxValue = GetSpiMaxValue(0)
            'spiLunchAmount.MaxValue = GetSpiMaxValue(0)
            'spiDinnerAmount.MaxValue = GetSpiMaxValue(0)
            'spiOtherAmount.MaxValue = GetSpiMaxValue(0)
            '
            If withHotel Then
                spiHotelAmount.Value = Nothing
                spiHotelAmount.MaxValue = GetSpiMaxValue(0)
            End If
        End If
    End Sub

    Private Function GetSpiMaxValue(ByVal value As Decimal) As Decimal
        Return If(value < 1, 1000000000000, value)
    End Function

    Private Sub ClearRequestForm()
        hExpenseRequestID.Value = ""
        ddlExpenseDestinationLocation.ClearSelection()
        'dteDate.Date = If(dteExpenseDepartureDate.Value Is Nothing, DateTime.Now, dteExpenseDepartureDate.Date)
        'chkBreakfastAmount.Checked = True
        'chkLunchAmount.Checked = True
        'chkDinnerAmount.Checked = True
        'chkOtherAmount.Checked = True
        'LoadExpenseNorm(True)
        'ddlCommonCurrency.ClearSelection()
        'spiCommonTotalConverted.Value = 0
        'txtRemark.Text = ""
        'ddlHotelCurrency.ClearSelection()
        'dteHotelExchangeDate.Date = If(dteExpenseReturnDate.Value Is Nothing, DateTime.Now, dteExpenseReturnDate.Date)
        'spiHotelTotalConverted.Value = 0
        'spiCommonCCAmount.Value = Nothing
        'spiCommonCCAmount.Visible = False
        'spiHotelCCAmount.Value = Nothing
        'spiHotelCCAmount.Visible = False
        'chkCommonCCAmount.Checked = False
        'chkHotelCCAmount.Checked = False
        '
        dteDate.Attributes("style") = "float: left; width: 50px !important;"
        lblCommonCCMessage.Text = ""
        lblCommonCCMessage.Visible = False
        'lblCommonCCCurrency.Text = ""
        'lblCommonCCCurrency.Visible = False
        lblHotelCCMessage.Text = ""
        lblHotelCCMessage.Visible = False
        'lblHotelCCCurrency.Text = ""
        'lblHotelCCCurrency.Visible = False
        spiCommonCCAmount.Attributes("style") = "float: left; width: 50px !important;"
        spiHotelCCAmount.Attributes("style") = "float: left; width: 50px !important;"
        '        
        'CommonFunction.SetCBOValue(ddlCommonCurrency, ddlCurrency.SelectedValue)
        'Dim lItem As ListItem = ddlCommonCurrency.SelectedItem
        'If lItem IsNot Nothing Then
        '    lblCommonCCCurrency.Text = lItem.Text
        'End If
        'CommonFunction.SetCBOValue(ddlHotelCurrency, ddlCurrency.SelectedValue)
        'Dim hItem As ListItem = ddlHotelCurrency.SelectedItem
        'If hItem IsNot Nothing Then
        '    lblHotelCCCurrency.Text = hItem.Text
        'End If
        'spiCommonExrate.Value = ExpenseProvider.GetExrate("USD", ddlCommonCurrency.SelectedValue, dteDate.Date)
        spiHotelExrate.Value = 1
    End Sub

    Private Sub ClearOtherForm()
        hExpenseOtherID.Value = ""
        'dteExpenseOtherDate.Date = If(dteExpenseDepartureDate.Value IsNot Nothing, dteExpenseDepartureDate.Date, DateTime.Now)
        'txtOtherExpense.Text = ""
        'ddlExpenseOtherCurrency.ClearSelection()
        'spiExpenseOtherAmount.Value = Nothing
        'txtExpenseOtherAmountConverted.Text = "0"
        'spiOtherCCAmount.Value = Nothing
        'spiOtherCCAmount.Visible = False
        'chkOtherCCAmount.Checked = False
        lblOtherCCMessage.Text = ""
        lblOtherCCMessage.Visible = False
        'lblOtherCCCurrency.Visible = False
        spiOtherCCAmount.Attributes("style") = "float: left; width: 50px !important;"
        '        
        'CommonFunction.SetCBOValue(ddlExpenseOtherCurrency, ddlCurrency.SelectedValue)
        'Dim oItem As ListItem = ddlExpenseOtherCurrency.SelectedItem
        'If oItem IsNot Nothing Then
        '    lblOtherCCCurrency.Text = oItem.Text
        'End If
        spiExpenseOtherExrate.Value = 1
    End Sub

    Private Sub InitBTStatus()
        'lnkHRStatus.Attributes("class") = ""
        'lnkHRStatus.InnerText = "Prepared"
        'lnkGAStatus.Attributes("class") = ""
        'lnkGAStatus.InnerText = "Prepared"
        lnkFINStatus.Attributes("class") = ""
        lnkFINStatus.InnerText = "Prepared"
        lnkFINStatus.Attributes("title") = ""
        lblFINComment.Text = ""
    End Sub

    Private Sub ClearAttachmentForm(Optional ByVal withDesc As Boolean = True)
        'panOthersAttachments.Controls.Clear()
        'panRegisterAttachments.Controls.Clear()
        'panScheduleAttachments.Controls.Clear()
        panExpenseAttachments.Controls.Clear()
        panOtherExpenseAttachments.Controls.Clear()
        'txtDescription.Text = ""
        If withDesc Then
            txtExpenseDescription.Text = ""
        End If
    End Sub

    Private Function GetObject() As tblBTExpenseInfo
        Dim obj As New tblBTExpenseInfo()
        obj.BTExpenseID = CommonFunction._ToInt(hID.Value)
        obj.IsSubmited = False
        obj.DepartureDate = dteExpenseDepartureDate.Date
        obj.ReturnDate = dteExpenseReturnDate.Date
        obj.CountryCode = ddlExpenseDestinationCountry.SelectedValue
        obj.Purpose = txtExpensePurpose.Text.Trim()
        obj.IsMovingTimeAllowance = chkExpenseMovingTimeAllowance.Checked
        obj.IsFirstTimeOverSea = chkExpenseFirstTimeOversea.Checked
        'obj.MovingTimeAllowanceVND = CommonFunction._ToMoney(spiExpenseMovingTimeAllowanceVND.Text.Trim())
        obj.CreatedBy = _username
        obj.ModifiedBy = _username
        obj.SubmitComment = txtSubmitComment.Text.Trim()
        Return obj
    End Function

    Private Function GetRequestObject() As tblBTExpenseRequestInfo
        Dim obj As New tblBTExpenseRequestInfo()
        obj.BTExpenseID = CommonFunction._ToInt(hID.Value)
        obj.ID = CommonFunction._ToInt(hExpenseRequestID.Value)
        obj.DestinationID = CommonFunction._ToInt(ddlExpenseDestinationLocation.SelectedValue)
        obj.DDate = dteDate.Date
        obj.BreakfastAmount = CommonFunction._ToMoney(spiBreakfastAmount.Text.Trim())
        obj.LunchAmount = CommonFunction._ToMoney(spiLunchAmount.Text.Trim())
        obj.DinnerAmount = CommonFunction._ToMoney(spiDinnerAmount.Text.Trim())
        obj.OtherAmount = CommonFunction._ToMoney(spiOtherAmount.Text.Trim())
        obj.AllowanceCurrency = ddlCommonCurrency.SelectedValue
        obj.AllowanceExrate = 1 'CommonFunction._ToMoney(spiCommonExrate.Text.Trim())
        'If obj.AllowanceExrate = 0 Then
        '    obj.AllowanceExrate = 1
        'End If
        'obj.Remark = txtRemark.Text.Trim()
        'If obj.OtherAmount = 0 Then
        '    obj.Remark = ""
        '    txtRemark.Text = ""
        'End If
        obj.HotelAmount = CommonFunction._ToMoney(spiHotelAmount.Text.Trim())
        obj.HotelCurrency = ddlHotelCurrency.SelectedValue
        obj.HotelExrate = CommonFunction._ToMoney(spiHotelExrate.Text.Trim())
        If obj.HotelExrate = 0 Then
            obj.HotelExrate = 1
        End If
        obj.CreatedBy = _username
        obj.ModifiedBy = _username
        obj.CreditAmount = If(chkCommonCCAmount.Checked, CommonFunction._ToMoney(spiCommonCCAmount.Text.Trim()), 0)
        obj.HotelCreditAmount = If(chkHotelCCAmount.Checked, CommonFunction._ToMoney(spiHotelCCAmount.Text.Trim()), 0)
        obj.HotelExdate = dteHotelExchangeDate.Date
        Return obj
    End Function

    Private Function GetOtherObject() As tblBTExpenseOtherInfo
        Dim obj As New tblBTExpenseOtherInfo()
        obj.BTExpenseID = CommonFunction._ToInt(hID.Value)
        obj.ID = CommonFunction._ToInt(hExpenseOtherID.Value)
        obj.DDate = dteExpenseOtherDate.Date
        obj.Expense = CommonFunction._ToString(txtOtherExpense.Text.Trim())
        obj.Currency = ddlExpenseOtherCurrency.SelectedValue
        obj.Exrate = CommonFunction._ToMoney(spiExpenseOtherExrate.Text.Trim())
        If obj.Exrate = 0 Then
            obj.Exrate = 1
        End If
        obj.Amount = CommonFunction._ToMoney(spiExpenseOtherAmount.Text.Trim())
        obj.CreatedBy = _username
        obj.ModifiedBy = _username
        obj.CreditAmount = If(chkOtherCCAmount.Checked, CommonFunction._ToMoney(spiOtherCCAmount.Text.Trim()), 0)
        Return obj
    End Function

    Private Function GetInvoiceObject() As tblBTInvoiceInfo
        Dim obj As New tblBTInvoiceInfo()
        obj.ID = CommonFunction._ToInt(hInvoiceID.Value)
        obj.BTRegisterID = CommonFunction._ToInt(hID.Value)
        obj.STT = txtInvSTT.Text.Trim()
        obj.InvNo = txtInvNo.Text.Trim()
        obj.InvDate = dteInvDate.Date
        obj.SerialNo = txtInvSerialNo.Text.Trim()
        obj.SellerName = txtInvSellerName.Text.Trim()
        obj.SellerTaxCode = txtInvSellerTaxCode.Text.Trim()
        obj.NetCost = CommonFunction._ToMoney(spiInvNetCost.Text.Trim())
        'obj.TaxRate = Math.Round(CommonFunction._ToMoney(spiInvTaxRate.Text.Trim()))
        obj.VAT = CommonFunction._ToMoney(spiInvVAT.Text.Trim())
        obj.Item = If(ddlInvItem.SelectedValue.Trim().Length = 0, Nothing, ddlInvItem.SelectedValue)
        'obj.Supplier = txtInvSupplier.Text.Trim()
        obj.CreatedBy = _username
        obj.ModifiedBy = _username
        obj.IsCreditCard = If(chkCredit.Checked, chkInvoiceCredit.Checked, False)
        Return obj
    End Function

    Private Sub UploadAttachments()
        BusinessTripProvider.BTRAttachment_UpdateExpenseDesc(CommonFunction._ToInt(hID.Value), txtExpenseDescription.Text.Trim())
    End Sub

    Protected Sub btnSearch_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            SetPreSearchCondition()
            Dim postBackUrl As New StringBuilder()
            postBackUrl.Append("~/BTExpenseDeclaration.aspx")
            postBackUrl.Append(String.Format( _
                "?btno={0}&bttype={1}&fdate={2}&tdate={3}&ecode={4}&ename={5}&obudget={6}&bg={7}", _
                hsBTNo.Value, hsBTType.Value, hsDepartureFrom.Value, hsDepartureTo.Value, _
                hsEmployeeCode.Value, hsFullName.Value, hsBudgetAll.Value, hsBudgetName.Value))
            Response.Redirect(postBackUrl.ToString())
            'LoadDataList()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvFIStatusHistory_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvFIStatusHistory.BeforeGetCallbackResult
        LoadBTExpenseHistory()
    End Sub

    Protected Sub grvBTRegister_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTRegister.BeforeGetCallbackResult
        GetDataTable()
        LoadRegister()
    End Sub

    Protected Sub grvBTSubmitted_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTSubmitted.BeforeGetCallbackResult
        GetDataTable()
        LoadSubmitted()
    End Sub

    Protected Sub grvBTRejected_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTRejected.BeforeGetCallbackResult
        GetDataTable()
        LoadRejected()
    End Sub

    Protected Sub grvBTCompleted_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTCompleted.BeforeGetCallbackResult
        GetDataTable()
        LoadCompleted()
    End Sub

    'Protected Sub grvBTRequest_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTRequest.BeforeGetCallbackResult
    '    LoadBTRegisterRequest()
    'End Sub

    Protected Sub grvCommonExpense_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvCommonExpense.BeforeGetCallbackResult
        LoadBTExpenseRequest()
    End Sub

    'Protected Sub grvBTSchedule_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTSchedule.BeforeGetCallbackResult
    '    LoadBTRegisterSchedule()
    'End Sub

    Protected Sub grvBTExpenseOther_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTExpenseOther.BeforeGetCallbackResult
        LoadBTExpenseOther()
    End Sub

    Protected Sub grvBTInvoice_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTInvoice.BeforeGetCallbackResult
        LoadBTInvoice()
    End Sub

    Protected Sub btnFinish_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnFinish.Click
        CommonFunction.SetPostBackStatus(btnFinish)
        Try
            Dim obj As tblBTExpenseInfo = GetObject()
            UploadAttachments()
            'hID.Value = ""
            ExpenseProvider.BTExpense_Update(obj)
            '
            LoadBTExpenseInfo()
            '            
            'LoadBTExpenseRequest()
            'LoadBTAirTicket()
            'LoadBTExpenseSchedule()
            'LoadBTExpenseOther()
            LoadBTRegisterAttachment(True)
            'LoadBTInvoice()
            '
            CommonFunction.ShowInfoMessage(panMessage, "Your data is saved successfully!")
            'CommonFunction.SetSesstionMessage("Saved successfully!")
            'Response.Redirect(Request.Path)
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnSubmit_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            Dim obj As tblBTExpenseInfo = GetObject()
            obj.IsSubmited = True
            UploadAttachments()
            'hID.Value = ""
            ExpenseProvider.BTExpense_Update(obj)
            '
            LoadBTExpenseInfo()
            '            
            LoadBTExpenseRequest()
            'LoadBTAirTicket()
            LoadBTExpenseSchedule()
            LoadBTExpenseOther()
            LoadBTRegisterAttachment(True)
            'LoadBTInvoice()
            'Send notice email
            If SendSubmitEmail() Then
                SendUserEmail()
                CommonFunction.ShowInfoMessage(panMessage, "Your data is submited successfully!")
            Else
                CommonFunction.ShowInfoMessage(panMessage, "Your data is submited successfully but fail to send notice emails! Please contact with administrator!")
            End If
            _objEmail.Dispose()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEdit_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            'hID.Value = btn.Attributes("data-id")
            EnableInfoForm(False)
            ShowInfoRows(True)
            'LoadInfo(False)            
            'LoadDataList()
            LoadBTRegisterInfo()
            LoadBTExpenseInfo(False)
            '
            'LoadBTRegisterRequest()
            LoadBTExpenseRequest()
            LoadBTAirTicket()
            LoadBTExpenseSchedule()
            LoadBTExpenseOther()
            'LoadBTRegisterSchedule()
            LoadBTRegisterAttachment(True)
            LoadBTInvoice()
            '
            Dim recallMessage As String = CommonFunction._ToString(Session("RecallMessage"))
            If recallMessage.Trim().Length > 0 Then
                Session.Remove("RecallMessage")
                CommonFunction.ShowInfoMessage(panMessage, recallMessage)
                '
                SendRecallEmail()
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btn)
        Try
            'for view only
            Dim backUrl As String = CommonFunction._ToString(Request.QueryString("back"))
            If backUrl.Trim.Length > 0 Then
                Dim params As String = CommonFunction._ToString(Request.QueryString("params")).Replace(";eq;", "=").Replace(";amp;", "&")
                Response.Redirect(String.Format("~/{0}{1}", backUrl, If(params.Trim().Length > 0, String.Concat("?", params), "")), True)
            Else
                Dim postBackUrl As New StringBuilder()
                postBackUrl.Append("~/BTExpenseDeclaration.aspx")
                postBackUrl.Append(String.Format( _
                    "?btno={0}&bttype={1}&fdate={2}&tdate={3}&ecode={4}&ename={5}&obudget={6}&bg={7}", _
                    hsBTNo.Value, hsBTType.Value, hsDepartureFrom.Value, hsDepartureTo.Value, _
                    hsEmployeeCode.Value, hsFullName.Value, hsBudgetAll.Value, hsBudgetName.Value))
                Response.Redirect(postBackUrl.ToString())
            End If
            '
            'BusinessTripProvider.BTRegister_DeleteDraftByID(CommonFunction._ToInt(hID.Value))
            'ClearBTForm()
            'ClearRequestForm()
            'ClearOtherForm()
            'EnableInfoForm(True)
            ''                        
            'ClearAttachmentForm()
            ''
            'txtBusinessTripNo.Text = hsBTNo.Value
            'CommonFunction.SetCBOValue(ddlBTType, hsBTType.Value)
            'txtEmployeeCode.Text = hsEmployeeCode.Value
            'txtFullName.Text = hsFullName.Value
            'chkBudgetAll.Checked = hsBudgetAll.Value = "Y"
            'ShowInfoRows(False)
            ''InitBudgetName(True, chkBudgetAll.Checked)
            'CommonFunction.SetCBOValue(ddlBudgetName, hsBudgetName.Value)
            'txtBudgetCode.Text = If(ddlBudgetName.SelectedValue <> "", ddlBudgetName.SelectedValue.Split("-")(1), "")
            'dteDepartureFrom.Text = hsDepartureFrom.Value
            'dteDepartureTo.Text = hsDepartureTo.Value
            'btnSearch_OnClick(Nothing, Nothing)
            '            
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnRecall_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnRecall.Click
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            Dim btID As Integer = CommonFunction._ToInt(hID.Value)
            Dim message As String = ExpenseProvider.tbl_BT_Expense_Recall(btID, _username)
            If message.Length > 0 Then
                CommonFunction.ShowErrorMessage(panMessage, message)
            Else
                Session("RecallMessage") = "Your BT Expense Declaration was recalled successfully!"
                Dim postBackUrl As New StringBuilder()
                postBackUrl.Append("~/BTExpenseDeclaration.aspx")
                postBackUrl.Append(String.Format("?id={0}&btno={1}&bttype={2}&fdate={3}&tdate={4}&ecode={5}&ename={6}&obudget={7}&bg={8}", _
                    hID.Value, hsBTNo.Value, hsBTType.Value, hsDepartureFrom.Value, hsDepartureTo.Value, _
                    hsEmployeeCode.Value, hsFullName.Value, hsBudgetAll.Value, hsBudgetName.Value))
                Response.Redirect(postBackUrl.ToString())
                'CommonFunction.ShowInfoMessage(panMessage, "Recalled successfully!")
                'LoadBTRegisterInfo()
                ''
                'LoadBTRegisterRequest()
                'LoadBTAirTicket()
                'LoadBTRegisterSchedule()
                'LoadBTRegisterAttachment(True)
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    'Protected Sub btnEditRequest_OnClick(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim btn As Button = CType(sender, Button)
    '    CommonFunction.SetPostBackStatus(btnCancel)
    '    Try
    '        hRequestID.Value = btn.Attributes("data-id")
    '        LoadBTRegisterRequestByID()
    '    Catch ex As Exception
    '        CommonFunction.ShowErrorMessage(panMessage, ex.Message)
    '    End Try
    'End Sub

    Protected Sub btnDeleteRequest_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            Dim id As Integer = CommonFunction._ToInt(btn.Attributes("data-id"))
            ExpenseProvider.BTExpenseRequest_Delete(id.ToString())
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
            LoadBTExpenseRequest()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditExpenseRequest_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            hExpenseRequestID.Value = btn.Attributes("data-id")
            LoadBTExpenseRequestByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    'Protected Sub btnEditSchedule_OnClick(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim btn As Button = CType(sender, Button)
    '    CommonFunction.SetPostBackStatus(btnCancel)
    '    Try
    '        hScheduleID.Value = btn.Attributes("data-id")
    '        LoadBTRegisterScheduleByID()
    '    Catch ex As Exception
    '        CommonFunction.ShowErrorMessage(panMessage, ex.Message)
    '    End Try
    'End Sub

    Protected Sub btnEditExpenseOther_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            hExpenseOtherID.Value = btn.Attributes("data-id")
            LoadBTExpenseOtherByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteOther_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            Dim id As Integer = CommonFunction._ToInt(btn.Attributes("data-id"))
            ExpenseProvider.BTExpenseOther_Delete(id.ToString())
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
            LoadBTExpenseOther()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnSaveInvoice_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveInvoice.Click
        CommonFunction.SetPostBackStatus(btnSaveInvoice)
        Try
            Dim obj As tblBTInvoiceInfo = GetInvoiceObject()
            If Not IsValidInvoice(obj) Then
                CommonFunction.SetProcessStatus(btnSaveInvoice, False)
                Return
            End If
            If obj.ID > 0 Then
                InvoiceProvider.BTInvoice_Update(obj)
            Else
                InvoiceProvider.BTInvoice_Insert(obj)
            End If
            CommonFunction.SetProcessStatus(btnSaveInvoice, True)
            CommonFunction.ShowInfoMessage(panMessage, "Your data is saved successfully!")
            LoadBTInvoice()
        Catch ex As Exception
            CommonFunction.SetProcessStatus(btnSaveInvoice, False)
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Function IsValidInvoice(ByVal obj As tblBTInvoiceInfo) As Boolean
        Dim isValid As Boolean = True
        txtInvNo.CssClass = txtInvNo.CssClass.Replace(" validate-error", "")
        txtInvSellerName.CssClass = txtInvSellerName.CssClass.Replace(" validate-error", "")
        If InvoiceProvider.CheckNo(obj.ID, obj.InvNo, obj.SellerName) Then
            CommonFunction.ShowErrorMessage(panMessage, "Invoice no existed!")
            txtInvNo.CssClass &= " validate-error"
            txtInvSellerName.CssClass &= " validate-error"
            isValid = False
        End If
        Return isValid
    End Function

    Protected Sub btnCancelInvoice_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelInvoice.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btn)
        Try
            ClearInvoiceForm()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditInvoice_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            txtInvNo.CssClass = txtInvNo.CssClass.Replace(" validate-error", "")
            '
            hInvoiceID.Value = btn.Attributes("data-id")
            LoadBTInvoiceByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteInvoice_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            Dim id As Integer = CommonFunction._ToInt(btn.Attributes("data-id"))
            InvoiceProvider.BTInvoice_Delete(id.ToString())
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
            LoadBTInvoice()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    'Protected Sub btnCancelRequest_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelRequest.Click
    '    CommonFunction.SetPostBackStatus(btnCancelRequest)
    '    Try
    '        ClearRequestForm()
    '    Catch ex As Exception
    '        CommonFunction.ShowErrorMessage(panMessage, ex.Message)
    '    End Try
    'End Sub

    Protected Sub btnSaveRequest_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveRequest.Click
        CommonFunction.SetPostBackStatus(btnSaveRequest)
        Try
            Dim requestObj As tblBTExpenseRequestInfo = GetRequestObject()
            If Not IsValidRequest(requestObj) Then
                CommonFunction.SetProcessStatus(btnSaveRequest, False)
                Return
            End If
            If requestObj.ID > 0 Then
                ExpenseProvider.BTExpenseRequest_Update(requestObj)
            Else
                ExpenseProvider.BTExpenseRequest_Insert(requestObj)
            End If
            CommonFunction.SetProcessStatus(btnSaveRequest, True)
            CommonFunction.ShowInfoMessage(panMessage, "Your data is saved successfully!")
            LoadBTExpenseRequest()
        Catch ex As Exception
            CommonFunction.SetProcessStatus(btnSaveRequest, False)
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Function IsValidRequest(ByVal requestObj As tblBTExpenseRequestInfo) As Boolean
        dteDate.Attributes("style") = "float: left"
        lblCommonCCMessage.Text = ""
        lblCommonCCMessage.Visible = False
        lblHotelCCMessage.Text = ""
        lblHotelCCMessage.Visible = False
        spiCommonCCAmount.Attributes("style") = "float: left; width: 50px !important;"
        spiHotelCCAmount.Attributes("style") = "float: left; width: 50px !important;"
        '
        Dim isValid As Boolean = True
        If ExpenseProvider.BTExpenseRequest_IsBTTimeConflict(requestObj.BTExpenseID, requestObj.ID, requestObj.DDate) Then
            CommonFunction.ShowErrorMessage(panMessage, "Expense date conflict!")
            dteDate.Attributes("style") = "border-color: red; float: left; width: 50px !important;" '.Border.BorderColor = Drawing.Color.Red
            isValid = False
        Else
            Dim mealAmount As Decimal = (requestObj.BreakfastAmount + requestObj.LunchAmount + requestObj.DinnerAmount + requestObj.OtherAmount)
            Dim creditAmount As Decimal = requestObj.CreditAmount
            If creditAmount > mealAmount Then
                lblCommonCCMessage.Text = "Daily allowance credit amount can not be greater than daily allowance amount!"
                lblCommonCCMessage.Visible = True
                spiCommonCCAmount.Attributes("style") = "border-color: red; float: left; width: 50px !important;" '.Border.BorderColor = Drawing.Color.Red
                isValid = False
            End If
            '
            Dim hotelAmount As Decimal = requestObj.HotelAmount
            Dim hotelCreditAmount As Decimal = requestObj.HotelCreditAmount
            If hotelCreditAmount > hotelAmount Then
                lblHotelCCMessage.Text = "Hotel credit amount can not be greater than hotel amount!"
                lblHotelCCMessage.Visible = True
                spiHotelCCAmount.Attributes("style") = "border-color: red; float: left; width: 50px !important;" '.Border.BorderColor = Drawing.Color.Red
                isValid = False
            End If
        End If
        Return isValid
    End Function

    'Protected Sub btnCancelOther_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelOther.Click
    '    CommonFunction.SetPostBackStatus(btnCancelOther)
    '    Try
    '        ClearOtherForm()
    '    Catch ex As Exception
    '        CommonFunction.ShowErrorMessage(panMessage, ex.Message)
    '    End Try
    'End Sub

    Protected Sub btnSaveOther_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveOther.Click
        CommonFunction.SetPostBackStatus(btnSaveOther)
        Try
            Dim obj As tblBTExpenseOtherInfo = GetOtherObject()
            If Not IsValidOther(obj) Then
                CommonFunction.SetProcessStatus(btnSaveOther, False)
                Return
            End If
            If obj.ID > 0 Then
                ExpenseProvider.BTExpenseOther_Update(obj)
            Else
                ExpenseProvider.BTExpenseOther_Insert(obj)
            End If
            CommonFunction.SetProcessStatus(btnSaveOther, True)
            CommonFunction.ShowInfoMessage(panMessage, "Your data is saved successfully!")
            LoadBTExpenseOther()
        Catch ex As Exception
            CommonFunction.SetProcessStatus(btnSaveOther, False)
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Function IsValidOther(ByVal obj As tblBTExpenseOtherInfo) As Boolean
        lblOtherCCMessage.Visible = False
        spiOtherCCAmount.Attributes("style") = "float: left; width: 50px !important;" '.Border.BorderColor = Drawing.Color.FromArgb(204, 204, 204)
        '
        Dim isValid As Boolean = True
        If obj.CreditAmount > obj.Amount Then
            lblOtherCCMessage.Text = "Credit amount can not be greater than total amount!"
            lblOtherCCMessage.Visible = True
            spiOtherCCAmount.Attributes("style") = "border-color: red; float: left; width: 50px !important;" '.Border.BorderColor = Drawing.Color.Red
            isValid = False
        End If
        Return isValid
    End Function

    Protected Sub chkBudgetAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkBudgetAll.CheckedChanged
        CommonFunction.SetPostBackStatus(btnCancel)
        InitBudgetName(hID.Value.Trim().Length = 0, chkBudgetAll.Checked)
        txtBudgetCode.Text = If(ddlBudgetName.SelectedValue <> "", ddlBudgetName.SelectedValue.Split("-")(1), "")
        '
        'LoadDataList()
    End Sub

    Private Sub LoadBTAirTicket()
        Dim dtData As DataTable = AirTicketProvider.BTAirTicket_Search(CommonFunction._ToInt(hID.Value))
        CommonFunction.LoadDataToGrid(grvBTAirTicket, dtData, "", Nothing, "AirEnable", _airEnable)
    End Sub

    Private Sub LoadBTAirTicketByID()
        Dim id As Integer = CommonFunction._ToInt(hAirTicketID.Value)
        Dim dtData As DataTable = AirTicketProvider.BTAirTicket_GetByID(CommonFunction._ToInt(id))
        If dtData.Rows.Count > 0 Then
            Dim drData As DataRow = dtData.Rows(0)
            Dim oraStatus As Boolean = CommonFunction._ToBoolean(drData("EnableForm"))
            InitAirPeriod(oraStatus)
            EnableAirTicketForm(oraStatus AndAlso _airEnable)
            Dim airDate As DateTime = CommonFunction._ToDateTime(drData("TicketDate"))
            If airDate <> DateTime.MinValue Then
                dteAirDate.Date = airDate
            Else
                dteAirDate.Value = Nothing
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
        End If
    End Sub

    Private Sub ClearAirTicketForm()
        hAirTicketID.Value = ""
        dteAirDate.Date = If(dteExpenseDepartureDate.Value IsNot Nothing, dteExpenseDepartureDate.Date, DateTime.Now)
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
        InitAirPeriod(True)
        'ddlAirPeriod.ClearSelection()
        ddlOraSupplier.ClearSelection()
        EnableAirTicketForm(_airEnable)
        '
        txtAirTicketNo.CssClass = txtAirTicketNo.CssClass.Replace(" validate-error", "")
        ddlAirPeriod.CssClass = ddlAirPeriod.CssClass.Replace(" validate-error", "")
        ddlOraSupplier.CssClass = ddlOraSupplier.CssClass.Replace(" validate-error", "")
    End Sub

    Private Function GetAirTicketObject() As tblBTAirTicketInfo
        Dim obj As New tblBTAirTicketInfo()
        obj.ID = CommonFunction._ToInt(hAirTicketID.Value)
        obj.BTRegisterID = CommonFunction._ToInt(hID.Value)
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
            If obj.ID > 0 Then
                AirTicketProvider.BTAirTicket_Update(obj)
            Else
                AirTicketProvider.BTAirTicket_Insert(obj)
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
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            txtAirTicketNo.CssClass = txtAirTicketNo.CssClass.Replace(" validate-error", "")
            ddlAirPeriod.CssClass = ddlAirPeriod.CssClass.Replace(" validate-error", "")
            ddlOraSupplier.CssClass = ddlOraSupplier.CssClass.Replace(" validate-error", "")
            '
            hAirTicketID.Value = btn.Attributes("data-id")
            LoadBTAirTicketByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteAirTicket_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            Dim id As Integer = CommonFunction._ToInt(btn.Attributes("data-id"))
            AirTicketProvider.BTAirTicket_Delete(id.ToString())
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
            LoadBTAirTicket()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvBTSubmitted_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles grvBTSubmitted.HtmlRowPrepared, grvBTRejected.HtmlRowPrepared
        If e.RowType = GridViewRowType.Data Then
            Dim status As String = e.GetValue("FIStatus")
            If status = FIStatus.rejected.ToString() Then
                e.Row.CssClass &= " rejected"
            End If
        End If
    End Sub

    Protected Sub btnExportBT_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportBT.Click
        CommonFunction.SetPostBackStatus(btnExportBT)
        Dim xlApp As Excel.Application = Nothing
        Dim xlWorkBook As Excel.Workbook = Nothing
        Dim xlWorkSheet As Excel.Worksheet = Nothing
        Try
            Dim btType As String = ddlBTType.SelectedValue
            Dim isDomestic As Boolean = btType.IndexOf("_domestic") >= 0
            Dim tmp As String = Server.MapPath(String.Concat("\Export\Template\", If(isDomestic, "bt_expense_template.xlt", "bt_expense_international_template.xlt")))
            '            
            Dim misValue As Object = System.Reflection.Missing.Value
            '
            xlApp = New Excel.ApplicationClass()
            xlWorkBook = xlApp.Workbooks.Open(tmp, 0, True, 5, "", "", True, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, 1, 0)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            'business
            Dim ds As DataSet = ReportProvider.GetBTExpense(CommonFunction._ToInt(hID.Value))
            ds.Tables(0).TableName = "General"
            ds.Tables(1).TableName = "Details"
            ds.Tables(2).TableName = "DetailsSummary"
            ds.Tables(3).TableName = "Other"
            ds.Tables(4).TableName = "OtherSummary"
            ds.Tables(5).TableName = "Summary"
            ds.Tables(6).TableName = "Exrate"
            '
            Dim rowIndex As Integer = 3
            'general info            
            Dim drGeneral As DataRow = ds.Tables("General").Rows(0)
            xlWorkSheet.Cells(rowIndex, 3) = drGeneral("BTNo")
            rowIndex = rowIndex + 1
            Dim employeeCode As String = drGeneral("EmployeeCode")
            xlWorkSheet.Cells(rowIndex, 3) = drGeneral("EmployeeName")
            xlWorkSheet.Cells(rowIndex, 8) = employeeCode
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 5) = drGeneral("Mobile")
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 3) = drGeneral("DepartmentName")
            xlWorkSheet.Cells(rowIndex, 7) = drGeneral("DivisionName")
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 3) = drGeneral("JobBand")
            'rowIndex = rowIndex + 1
            'xlWorkSheet.Cells(rowIndex, 5) = drGeneral("Budget")
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 8) = drGeneral("DepartureDate")
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 8) = drGeneral("ReturnDate")
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 7) = drGeneral("Purpose")
            'request details
            rowIndex = rowIndex + 4
            Dim nextRowIndex = rowIndex + 8
            Dim dtDetails As DataTable = ds.Tables("Details")
            If dtDetails.Rows.Count > 8 Then
                nextRowIndex = nextRowIndex + dtDetails.Rows.Count - 8
                Dim currentRow As Excel.Range = CType(xlWorkSheet.Rows(rowIndex + 1), Excel.Range)
                For i As Integer = 1 To dtDetails.Rows.Count - 8
                    currentRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown, currentRow.Copy(Type.Missing))
                Next
            End If

            For i As Integer = 0 To dtDetails.Rows.Count - 1
                Dim colIndex As Integer = 1
                Dim drDetails As DataRow = dtDetails.Rows(i)
                xlWorkSheet.Cells(rowIndex, colIndex) = drDetails("Date")
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = drDetails("BreakfastAmount")
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = drDetails("LunchAmount")
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = drDetails("DinnerAmount")
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = drDetails("OtherAmount")
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = drDetails(If(isDomestic, "MealAmount", "MealAmountConverted"))
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = drDetails("MovingTimeAllowance")
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = drDetails("HotelCurrency")
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = drDetails("HotelAmount")
                colIndex += 1
                If Not isDomestic Then
                    xlWorkSheet.Cells(rowIndex, colIndex) = drDetails("HotelAmountConverted")
                End If
                rowIndex = rowIndex + 1
            Next
            Dim drDetailsSummary As DataRow = ds.Tables("DetailsSummary").Rows(0)
            rowIndex = nextRowIndex
            xlWorkSheet.Cells(rowIndex, 6) = drDetailsSummary(If(isDomestic, "TotalMealAmount", "TotalMealAmountConverted"))
            xlWorkSheet.Cells(rowIndex, 7) = drDetailsSummary("MovingTimeAllowance")
            If isDomestic Then
                xlWorkSheet.Cells(rowIndex, 9) = drDetailsSummary("TotalHotelAmount")
            Else
                xlWorkSheet.Cells(rowIndex, 10) = drDetailsSummary("TotalHotelAmountConverted")
            End If
            'other details
            rowIndex = rowIndex + 3
            nextRowIndex = rowIndex + 9
            Dim dtOther As DataTable = ds.Tables("Other")
            If dtOther.Rows.Count > 9 Then
                nextRowIndex = nextRowIndex + dtOther.Rows.Count - 9
                Dim currentRow As Excel.Range = CType(xlWorkSheet.Rows(rowIndex + 1), Excel.Range)
                For i As Integer = 1 To dtOther.Rows.Count - 9
                    currentRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown, currentRow.Copy(Type.Missing))
                Next
            End If

            For i As Integer = 0 To dtOther.Rows.Count - 1
                Dim colIndex As Integer = 1
                Dim drOther As DataRow = dtOther.Rows(i)
                xlWorkSheet.Cells(rowIndex, colIndex) = drOther("Date")
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = drOther("Expense")
                colIndex += 6
                xlWorkSheet.Cells(rowIndex, colIndex) = drOther("Currency")
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = drOther("Amount")
                colIndex += 1
                If Not isDomestic Then
                    xlWorkSheet.Cells(rowIndex, colIndex) = drOther("AmountConverted")
                End If
                rowIndex = rowIndex + 1
            Next
            Dim drOtherSummary As DataRow = ds.Tables("OtherSummary").Rows(0)
            rowIndex = nextRowIndex
            If isDomestic Then
                xlWorkSheet.Cells(rowIndex, 9) = drOtherSummary("TotalAmount")
            Else
                xlWorkSheet.Cells(rowIndex, 10) = drOtherSummary("TotalAmountConverted")
            End If
            'summary            
            rowIndex = rowIndex + 2
            Dim drSummary As DataRow = ds.Tables("Summary").Rows(0)
            xlWorkSheet.Cells(rowIndex, 6) = drSummary("TotalExpense")
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 6) = drSummary("TotalAdvance")
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 6) = drSummary("CreditAmount")
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 6) = drSummary("Disparity")
            'exrate
            rowIndex = rowIndex + 10
            Dim dtExrate As DataTable = ds.Tables("Exrate")
            If dtExrate.Rows.Count > 2 Then
                Dim currentRow As Excel.Range = CType(xlWorkSheet.Rows(rowIndex + 1), Excel.Range)
                For i As Integer = 1 To dtOther.Rows.Count - 2
                    currentRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown, currentRow.Copy(Type.Missing))
                Next
            End If

            For i As Integer = 0 To dtExrate.Rows.Count - 1
                Dim drExrate As DataRow = dtExrate.Rows(0)
                xlWorkSheet.Cells(rowIndex, 8) = String.Format("1 {0}:", drExrate("Currency"))
                xlWorkSheet.Cells(rowIndex, 9) = drExrate("Exrate")
                xlWorkSheet.Cells(rowIndex, 10) = " VND"
                rowIndex = rowIndex + 1
            Next

            '
            Dim filePath As String = String.Concat("~/Export/Download/", _username)
            Dim dir As DirectoryInfo = New DirectoryInfo(Server.MapPath(filePath))
            If Not dir.Exists() Then
                dir.Create()
            End If
            filePath = String.Concat(filePath, "/", "BT_Expense_", employeeCode, "_", DateTime.Now.ToString("yyyyMMddHHmmssfffff"), ".xls")
            'delete old download file
            For Each f As FileInfo In dir.GetFiles()
                Dim timeSpan As TimeSpan = DateTime.Now - f.CreationTime
                If timeSpan.TotalDays >= 1 Then
                    Try
                        f.Delete()
                        f.Refresh()
                    Catch
                    End Try
                End If
            Next
            dir.Refresh()
            '
            xlWorkBook.SaveAs(Server.MapPath(filePath), Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, False, False, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlUserResolution, True, misValue, misValue, misValue)
            btnExportBT.Attributes("data-file-path") = filePath.Substring(1)
            '            
            Try
                xlWorkBook.Close(False)
            Catch
            End Try
            Try
                xlApp.Quit()
            Catch
            End Try
            CommonFunction.ReleaseObject(xlWorkSheet)
            CommonFunction.ReleaseObject(xlWorkBook)
            CommonFunction.ReleaseObject(xlApp)
        Catch ex As Exception
            Try
                xlWorkBook.Close(False)
            Catch
            End Try
            Try
                xlApp.Quit()
            Catch
            End Try
            CommonFunction.ReleaseObject(xlWorkSheet)
            CommonFunction.ReleaseObject(xlWorkBook)
            CommonFunction.ReleaseObject(xlApp)
            '
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    'Private Sub chkFirstTimeChange()
    '    If chkFirstTimeOversea.Checked Then
    '        Dim drBT As DataRow = BusinessTripProvider.BTRegister_GetByID(CommonFunction._ToInt(hID.Value)).Rows(0)
    '        Dim firstTimeAmount As Decimal = CommonFunction._ToMoney(drBT("FirstTimeOverSea"))
    '        If firstTimeAmount <= 0 Then
    '            firstTimeAmount = CommonFunction._ToMoney(LookupProvider.GetByCodeAndValue("PRE_PARAMS", "FIRST_TIME_OVERSEA"))
    '        End If
    '        spanFirstAmount.InnerText = firstTimeAmount
    '        spanFirstTimeOversea1.Visible = True
    '        If ddlCurrency.SelectedValue = "vnd" Then
    '            Dim firstTimeAmountVND As Decimal = CommonFunction._ToMoney(drBT("FirstTimeOverSeaVND"))
    '            If firstTimeAmountVND <= 0 Then
    '                firstTimeAmountVND = firstTimeAmount * ExpenseProvider.GetExrate("usd", "vnd")
    '            End If
    '            spiFirstTimeOverseaVND.Value = CommonFunction._ToMoneyWithNull(firstTimeAmountVND)
    '            spanFirstTimeOversea2.Visible = True
    '            spanFirstTimeOversea3.Visible = True
    '            spanFirstTimeOversea4.Visible = True
    '        Else
    '            spanFirstTimeOversea2.Visible = False
    '            spanFirstTimeOversea3.Visible = False
    '            spanFirstTimeOversea4.Visible = False
    '        End If
    '    Else
    '        spanFirstTimeOversea1.Visible = False
    '        spanFirstTimeOversea2.Visible = False
    '        spanFirstTimeOversea3.Visible = False
    '        spanFirstTimeOversea4.Visible = False
    '    End If
    'End Sub

    Protected Sub btnApprove_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnApprove.Click
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            dteGLDate.Attributes("style") = ""
            spiExtensionAmount.Attributes("style") = ""
            approveMessage.InnerText = ""
            '
            Dim BTRegisterID As Integer = CommonFunction._ToInt(hID.Value)
            Dim role As String = CommonFunction._ToString(Session("RoleType"))
            Select Case role.ToLower()
                Case RoleType.Finance.ToString().ToLower(), RoleType.Administrator.ToString().ToLower(), RoleType.Finance_GA.ToString().ToLower()
                    If dteInvoiceDate.Text.Trim().Length = 0 OrElse dteGLDate.Text.Trim().Length = 0 Then
                        Return
                    End If
                    Dim EmployeeCode As String = txtEmployeeCode.Text.Trim()
                    Dim PaymentType As String = If(radBankTransfer.Checked, "b", "c")
                    Dim invoiceDate As DateTime = dteInvoiceDate.Date
                    Dim glDate As DateTime = dteGLDate.Date
                    Dim batchName As Integer = CommonFunction._ToInt(ddlBatchName.SelectedValue)
                    '
                    Dim extAmount As Decimal = CommonFunction._ToMoney(spiExtensionAmount.Text)
                    Dim drExt As DataRow = ExpenseProvider.CheckExtInvoice(BTRegisterID)
                    Dim extLimitedAmount As Decimal = CommonFunction._ToMoney(drExt("ExtAmount"))
                    '
                    tabApproveMessage.Attributes("style") = "display: block"
                    approveMessage.InnerText = hApproveMessage.Value
                    'Kiem tra xem User da co ben oracle chua
                    Dim dtVendor As DataSet = BusinessTripProvider.check_Supplier_No(BTRegisterID, PaymentType)
                    Dim dtSupplier As DataTable = dtVendor.Tables(0)
                    Dim dtSupplierSite As DataTable = dtVendor.Tables(1)
                    If Not BusinessTripProvider.CheckOverseaExrate(BTRegisterID, invoiceDate) Then
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
                        '
                        Dim supplierNo As String = CommonFunction._ToString(dtSupplier.Rows(0)("VendorNo"))
                        Dim supplierSite As String = CommonFunction._ToString(dtSupplierSite.Rows(0)("VendorSite"))
                        Dim extExrate As Decimal = CommonFunction._ToMoney(spiExtensionExrate.Text)
                        Dim creditExrate As Decimal = CommonFunction._ToMoney(spiCreditInvoiceExrate.Text)
                        '
                        Dim dr As DataRow = ExpenseProvider.tbl_BT_Expense_UpdateStatusFI(BTRegisterID, FIStatus.completed.ToString(), _username, "", supplierNo, supplierSite, glDate, batchName, invoiceDate, extAmount, extExrate, creditExrate)
                        Dim message As String = CommonFunction._ToString(dr("MessageResult")).Trim()
                        Dim messageStatus As String = CommonFunction._ToString(dr("MessageStatus")).Trim()
                        If messageStatus.Length > 0 Then
                            CommonFunction.ShowErrorMessage(panMessage, messageStatus)
                        Else
                            tabApproveMessage.Attributes("style") = "display: none"
                            btnApprove.Visible = True
                            '
                            Dim isSendMail As Boolean = btnApprove.Text = "Approve"
                            LoadBTExpenseInfo()
                            'LoadBTExpenseHistory()
                            Dim budgedCode As Integer = CommonFunction._ToInt(ddlBudgetName.SelectedValue.Split("-")(0))
                            'txtBudgetRemain.Text = If(budgedCode = -1, "", String.Format("{0:#,0.##} VND", BusinessTripProvider.BTRegister_GetExpenseBudgetRemaining(budgedCode, CommonFunction._ToInt(hID.Value))))
                            '
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
                            If isSendMail Then
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
                Case Else
                    CommonFunction.ShowErrorMessage(panMessage, "Unauthorized!")
            End Select
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnReject_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnReject.Click
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            Dim BTRegisterID As Integer = CommonFunction._ToInt(hID.Value)
            Dim rejectReason As String = txtRejectReason.Text.Trim()
            txtRejectReason.Text = ""
            If rejectReason.Trim().Length = 0 Then
                Return
            End If
            'Dim oracleError As Boolean = ExpenseProvider.tbl_BT_Expense_CheckOracleStatus(BTRegisterID)
            'If oracleError Then

            'Else
            '    CommonFunction.ShowErrorMessage(panMessage, "Can not reject this BT! Please check oracle invoice status!")
            'End If
            Dim role As String = CommonFunction._ToString(Session("RoleType"))
            Select Case role.ToLower()
                Case RoleType.Finance.ToString().ToLower(), RoleType.Administrator.ToString().ToLower(), RoleType.Finance_GA.ToString().ToLower()
                    Dim dr As DataRow = ExpenseProvider.tbl_BT_Expense_UpdateStatusFI(BTRegisterID, FIStatus.rejected.ToString(), _username, rejectReason)
                    Dim message As String = CommonFunction._ToString(dr("MessageResult")).Trim()
                    If message.Trim().Length > 0 Then
                        CommonFunction.ShowErrorMessage(panMessage, message)
                    Else
                        'EnableApproveButtons()
                        LoadBTExpenseInfo()
                        'LoadBTExpenseHistory()
                        Dim budgedCode As Integer = CommonFunction._ToInt(ddlBudgetName.SelectedValue.Split("-")(0))
                        'txtBudgetRemain.Text = If(budgedCode = -1, "", String.Format("{0:#,0.##} VND", BusinessTripProvider.BTRegister_GetExpenseBudgetRemaining(budgedCode, CommonFunction._ToInt(hID.Value))))
                        'Send notice email
                        If SendUserEmail() Then
                            CommonFunction.ShowInfoMessage(panMessage, "BT's Rejected!")
                        Else
                            CommonFunction.ShowInfoMessage(panMessage, "BT's Rejected but fail to send notice emails! Please contact with administrator!")
                        End If
                        _objEmail.Dispose()
                    End If
                Case Else
                    CommonFunction.ShowErrorMessage(panMessage, "Unauthorized!")
            End Select
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    'Protected Sub chkCommonCCAmount_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkCommonCCAmount.CheckedChanged
    '    CommonFunction.SetPostBackStatus(btnCancel)
    '    If chkCommonCCAmount.Checked Then
    '        spiCommonCCAmount.Visible = True
    '        lblCommonCCCurrency.Visible = True
    '        Dim breakfast As Decimal = CommonFunction._ToMoney(spiBreakfastAmount.Text.Trim())
    '        Dim lunch As Decimal = CommonFunction._ToMoney(spiLunchAmount.Text)
    '        Dim dinner As Decimal = CommonFunction._ToMoney(spiDinnerAmount.Text)
    '        Dim other As Decimal = CommonFunction._ToMoney(spiOtherAmount.Text)
    '        spiCommonCCAmount.Value = CommonFunction._ToMoneyWithNull(breakfast + lunch + dinner + other)
    '        spiCommonCCAmount.Attributes("style") = "float: left; width: 50px !important;"
    '    Else
    '        spiCommonCCAmount.Visible = False
    '        lblCommonCCCurrency.Visible = False
    '        lblCommonCCMessage.Visible = False
    '        spiCommonCCAmount.Value = Nothing
    '    End If
    'End Sub

    'Protected Sub chkHotelCCAmount_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkHotelCCAmount.CheckedChanged
    '    CommonFunction.SetPostBackStatus(btnCancel)
    '    If chkHotelCCAmount.Checked Then
    '        spiHotelCCAmount.Visible = True
    '        lblHotelCCCurrency.Visible = True
    '        spiHotelCCAmount.Value = CommonFunction._ToMoneyWithNull(spiHotelAmount.Text.Trim())
    '        spiHotelCCAmount.Attributes("style") = "float: left; width: 50px !important;"
    '    Else
    '        spiHotelCCAmount.Visible = False
    '        lblHotelCCCurrency.Visible = False
    '        lblHotelCCMessage.Visible = False
    '        spiHotelCCAmount.Value = Nothing
    '    End If
    'End Sub

    'Protected Sub chkOtherCCAmount_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkOtherCCAmount.CheckedChanged
    '    CommonFunction.SetPostBackStatus(btnCancel)
    '    If chkOtherCCAmount.Checked Then
    '        spiOtherCCAmount.Visible = True
    '        lblOtherCCCurrency.Visible = True
    '        spiOtherCCAmount.Value = CommonFunction._ToMoneyWithNull(spiExpenseOtherAmount.Text.Trim())
    '        spiOtherCCAmount.Attributes("style") = "float: left; width: 50px !important;"
    '    Else
    '        spiOtherCCAmount.Visible = False
    '        lblOtherCCCurrency.Visible = False
    '        lblOtherCCMessage.Visible = False
    '        spiOtherCCAmount.Value = Nothing
    '    End If
    'End Sub

    'Protected Sub chkExpenseMovingTimeAllowance_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkExpenseMovingTimeAllowance.CheckedChanged
    '    CommonFunction.SetPostBackStatus(btnCancel)
    '    Try
    '        chkExpenseMovingTimeChange(True)
    '    Catch ex As Exception
    '        CommonFunction.ShowErrorMessage(panMessage, ex.Message)
    '    End Try
    'End Sub

    'Private Sub chkExpenseMovingTimeChange(Optional ByVal loadSummary As Boolean = False)
    '    Dim btID As Integer = CommonFunction._ToInt(hID.Value)
    '    '
    '    Dim drBT As DataRow = Nothing
    '    If btID > 0 Then
    '        drBT = ExpenseProvider.UpdateMovingTime(btID, chkExpenseMovingTimeAllowance.Checked)
    '    End If
    '    If chkExpenseMovingTimeAllowance.Checked Then
    '        Dim movingTimeAmount As Decimal = 0
    '        Dim movingTimeCurrency As String = If(_isDomestic, "VND", "USD")
    '        Dim movingTimeAmountVND As Decimal = 0
    '        If drBT IsNot Nothing Then
    '            movingTimeAmount = CommonFunction._ToMoney(drBT("Amount"))
    '            movingTimeCurrency = CommonFunction._ToString(drBT("Currency"))
    '            movingTimeAmountVND = CommonFunction._ToMoney(drBT("AmountVND"))
    '        End If
    '        'If movingTimeAmount <= 0 Then
    '        '    Dim dtPolicy As DataTable = mMovingTimeAllowanceProvider.m_Expense_GetPolicy(ddlExpenseDestinationCountry.SelectedValue)
    '        '    If dtPolicy.Rows.Count > 0 Then
    '        '        Dim drPolicy As DataRow = dtPolicy.Rows(0)
    '        '        movingTimeAmount = CommonFunction._ToMoney(drPolicy("Amount"))
    '        '        movingTimeCurrency = CommonFunction._ToString(drPolicy("Currency"))
    '        '    Else
    '        '        movingTimeAmount = 0
    '        '        movingTimeCurrency = If(_isDomestic, "VND", "USD")
    '        '    End If
    '        'End If
    '        spanExpenseMovingTimeAmount.InnerText = String.Format("{0:#,0.##} {1}", movingTimeAmount, movingTimeCurrency)
    '        '    spanExpenseMovingTime1.Visible = True
    '        '    If movingTimeCurrency.ToUpper() = "USD" Then
    '        '        spanExpenseMovingTime1.Attributes("style") = spanExpenseMovingTime1.Attributes("style").ToLower().Replace("width: 90px", "width: 70px")
    '        '    Else
    '        '        spanExpenseMovingTime1.Attributes("style") = spanExpenseMovingTime1.Attributes("style").ToLower().Replace("width: 70px", "width: 90px")
    '        '    End If
    '        '    If movingTimeCurrency.ToUpper() = "USD" AndAlso ddlCurrency.SelectedValue = "vnd" Then
    '        '        'Dim movingTimeAmountVND As Decimal = CommonFunction._ToMoney(drBT("MovingTimeAllowanceVND"))
    '        '        'If movingTimeAmountVND <= 0 Then
    '        '        '    movingTimeAmountVND = movingTimeAmount * ExpenseProvider.GetExrate("usd", "vnd")
    '        '        'End If
    '        '        spiExpenseMovingTimeAllowanceVND.Value = CommonFunction._ToMoneyWithNull(movingTimeAmountVND)
    '        '        spanExpenseMovingTime2.Visible = True
    '        '        spanExpenseMovingTime3.Visible = True
    '        '        spanExpenseMovingTime4.Visible = True
    '        '    Else
    '        '        spanExpenseMovingTime2.Visible = False
    '        '        spanExpenseMovingTime3.Visible = False
    '        '        spanExpenseMovingTime4.Visible = False
    '        '    End If
    '        'Else
    '        '    spanExpenseMovingTime1.Visible = False
    '        '    spanExpenseMovingTime2.Visible = False
    '        '    spanExpenseMovingTime3.Visible = False
    '        '    spanExpenseMovingTime4.Visible = False
    '    End If
    '    '
    '    If loadSummary Then
    '        LoadExpenseTotalSummary()
    '    End If
    'End Sub

    'Protected Sub ddlExpenseDestinationLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlExpenseDestinationLocation.SelectedIndexChanged
    '    CommonFunction.SetPostBackStatus(ddlExpenseDestinationLocation)
    '    Try
    '        LoadExpenseNorm(True)
    '    Catch ex As Exception
    '        CommonFunction.ShowErrorMessage(panMessage, ex.Message)
    '    End Try
    'End Sub

    'Protected Sub chkAmount_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkBreakfastAmount.CheckedChanged, chkLunchAmount.CheckedChanged, chkDinnerAmount.CheckedChanged, chkOtherAmount.CheckedChanged
    '    CommonFunction.SetPostBackStatus(btnCancel)
    '    Try
    '        LoadExpenseNorm()
    '    Catch ex As Exception
    '        CommonFunction.ShowErrorMessage(panMessage, ex.Message)
    '    End Try
    'End Sub

    Protected Sub dteInvoiceDate_DateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dteInvoiceDate.DateChanged
        CommonFunction.SetPostBackStatus(btnCancel)
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
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            tabApproveMessage.Attributes("style") = "display: none"
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnCheckExtInvoice_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCheckExtInvoice.Click
        CommonFunction.SetPostBackStatus(btnCheckExtInvoice)
        Try
            approveMessage.InnerText = hApproveMessage.Value
            Dim exrate As Decimal = 0
            'ext
            Dim drExt As DataRow = ExpenseProvider.CheckExtInvoice(CommonFunction._ToInt(hID.Value))
            Dim extAmount As Decimal = CommonFunction._ToMoney(drExt("ExtAmount"))
            If extAmount > 0 Then
                exrate = ExpenseProvider.GetExrate("USD", "VND", dteInvoiceDate.Date)
                spiExtensionAmount.Number = extAmount Mod 100
                spiExtensionAmount.MaxValue = extAmount
                spiExtensionExrate.Number = exrate
                trExtensionInvoice.Visible = True
            Else
                spiExtensionAmount.Number = 0
                spiExtensionExrate.Number = 1
                trExtensionInvoice.Visible = False
            End If
            'credit
            Dim creditAmount As Decimal = CommonFunction._ToMoney(drExt("CreditAmount"))
            If Not _isDomestic AndAlso creditAmount > 0 Then
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

    Private Sub EnableScheduleForm(ByVal enable As Boolean)
        dteExpenseScheduleDate.ReadOnly = Not enable
        txeExpenseFromTime.ReadOnly = Not enable
        txeExpenseToTime.ReadOnly = Not enable
        txtExpenseWorkingArea.ReadOnly = Not enable
        txtExpenseTask.ReadOnly = Not enable
        spiTransportationFee.ReadOnly = Not enable
        '
        btnShowSaveSchedule.Visible = enable
        btnSaveSchedule.Visible = enable
    End Sub

    Private Sub LoadBTExpenseSchedule()
        Dim dtData As DataTable = ExpenseProvider.BTExpenseSchedule_Search(CommonFunction._ToInt(hID.Value))
        CommonFunction.LoadDataToGrid(grvBTExpenseSchedule, dtData, "", Nothing, "EnableForm", _enable)
    End Sub

    Private Sub LoadBTExpenseScheduleByID()
        Dim id As Integer = CommonFunction._ToInt(hExpenseScheduleID.Value)
        Dim dtData As DataTable = ExpenseProvider.BTExpenseSchedule_GetByID(CommonFunction._ToInt(id))
        If dtData.Rows.Count > 0 Then
            Dim drData As DataRow = dtData.Rows(0)
            Dim scheduleDate As DateTime = CommonFunction._ToDateTime(drData("FromTime"))
            If scheduleDate <> DateTime.MinValue Then
                dteExpenseScheduleDate.Date = scheduleDate
            Else
                dteExpenseScheduleDate.Value = Nothing
            End If
            txeExpenseFromTime.DateTime = CommonFunction._ToDateTime(drData("FromTime"))
            txeExpenseToTime.DateTime = CommonFunction._ToDateTime(drData("ToTime"))
            txtExpenseWorkingArea.Text = CommonFunction._ToString(drData("WorkingArea"))
            txtExpenseTask.Text = CommonFunction._ToString(drData("Task"))
            spiTransportationFee.Value = CommonFunction._ToMoneyWithNull(drData("TransportationFee"))
        End If
    End Sub

    'Private Sub ClearScheduleForm()
    '    hExpenseScheduleID.Value = ""
    '    Dim dtDefault As DateTime = If(dteExpenseDepartureDate.Value Is Nothing, DateTime.Now, dteExpenseDepartureDate.Date)
    '    dteExpenseScheduleDate.Date = New DateTime(dtDefault.Year, dtDefault.Month, dtDefault.Day)
    '    txeExpenseFromTime.DateTime = New DateTime(dtDefault.Year, dtDefault.Month, dtDefault.Day, 8, 0, 0)
    '    txeExpenseToTime.DateTime = New DateTime(dtDefault.Year, dtDefault.Month, dtDefault.Day, 16, 45, 0)
    '    txtExpenseWorkingArea.Text = ""
    '    txtExpenseTask.Text = ""
    '    spiTransportationFee.Value = Nothing
    'End Sub

    Private Function GetScheduleObject() As tblBTExpenseScheduleInfo
        Dim obj As New tblBTExpenseScheduleInfo()
        obj.ID = CommonFunction._ToInt(hExpenseScheduleID.Value)
        obj.BTExpenseID = CommonFunction._ToInt(hID.Value)
        obj.FromTime = New DateTime(dteExpenseScheduleDate.Date.Year, dteExpenseScheduleDate.Date.Month, dteExpenseScheduleDate.Date.Day, txeExpenseFromTime.DateTime.Hour, txeExpenseFromTime.DateTime.Minute, txeExpenseFromTime.DateTime.Second)
        obj.ToTime = New DateTime(dteExpenseScheduleDate.Date.Year, dteExpenseScheduleDate.Date.Month, dteExpenseScheduleDate.Date.Day, txeExpenseToTime.DateTime.Hour, txeExpenseToTime.DateTime.Minute, txeExpenseToTime.DateTime.Second)
        obj.WorkingArea = txtExpenseWorkingArea.Text.Trim()
        obj.Task = txtExpenseTask.Text.Trim()
        obj.TransportationFee = CommonFunction._ToMoney(spiTransportationFee.Text().Trim())
        obj.CreatedBy = _username
        obj.ModifiedBy = _username
        Return obj
    End Function

    Protected Sub grvBTExpenseSchedule_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTExpenseSchedule.BeforeGetCallbackResult
        LoadBTExpenseSchedule()
    End Sub

    Protected Sub btnSaveSchedule_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveSchedule.Click
        CommonFunction.SetPostBackStatus(btnSaveSchedule)
        Try
            Dim obj As tblBTExpenseScheduleInfo = GetScheduleObject()
            If Not IsValidSchedule(obj) Then
                CommonFunction.SetProcessStatus(btnSaveSchedule, False)
                Return
            End If
            '
            If obj.ID > 0 Then
                ExpenseProvider.BTExpenseSchedule_Update(obj)
            Else
                ExpenseProvider.BTExpenseSchedule_Insert(obj)
            End If
            CommonFunction.SetProcessStatus(btnSaveSchedule, True)
            CommonFunction.ShowInfoMessage(panMessage, "Your data is saved successfully!")
            LoadBTExpenseSchedule()
        Catch ex As Exception
            CommonFunction.SetProcessStatus(btnSaveSchedule, False)
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Function IsValidSchedule(ByVal obj As tblBTExpenseScheduleInfo) As Boolean
        Dim isValid As Boolean = True
        txeExpenseFromTime.Attributes("style") = "width: 100px !important; display: inline-block;"
        txeExpenseToTime.Attributes("style") = "width: 100px !important; display: inline-block;"
        '
        If obj.ToTime <= obj.FromTime Then
            CommonFunction.ShowErrorMessage(panMessage, "To time must be greater than from time!")
            txeExpenseFromTime.Attributes("style") = "border-color: red; width: 100px !important; display: inline-block;"
            txeExpenseToTime.Attributes("style") = "border-color: red; width: 100px !important; display: inline-block;"
            isValid = False
        End If
        '
        Return isValid
    End Function

    'Protected Sub btnCancelSchedule_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelSchedule.Click
    '    Dim btn As Button = CType(sender, Button)
    '    CommonFunction.SetPostBackStatus(btn)
    '    Try
    '        ClearScheduleForm()
    '    Catch ex As Exception
    '        CommonFunction.ShowErrorMessage(panMessage, ex.Message)
    '    End Try
    'End Sub

    Protected Sub btnEditExpenseSchedule_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            hExpenseScheduleID.Value = btn.Attributes("data-id")
            LoadBTExpenseScheduleByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteSchedule_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            Dim id As Integer = CommonFunction._ToInt(btn.Attributes("data-id"))
            ExpenseProvider.BTExpenseSchedule_Delete(id.ToString())
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
            LoadBTExpenseSchedule()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub BtnExportSchedule_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles BtnExportSchedule.Click, btnExportSchedule1.Click
        CommonFunction.SetPostBackStatus(CType(sender, Button))
        Dim xlApp As Excel.Application = Nothing
        Dim xlWorkBook As Excel.Workbook = Nothing
        Dim xlWorkSheet As Excel.Worksheet = Nothing
        Try
            Dim tmp As String = Server.MapPath("\Export\Template\bt-schedule-template.xlt")
            '            
            Dim misValue As Object = System.Reflection.Missing.Value
            '
            xlApp = New Excel.ApplicationClass()
            xlWorkBook = xlApp.Workbooks.Open(tmp, 0, True, 5, "", "", True, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, 1, 0)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            'business
            Dim ds As DataSet = ReportProvider.GetBTExpenseSchedule(CommonFunction._ToInt(hID.Value))
            ds.Tables(0).TableName = "General"
            ds.Tables(1).TableName = "Details"
            ds.Tables(2).TableName = "Summary"
            '
            Dim rowIndex As Integer = 5
            'general info            
            Dim drGeneral As DataRow = ds.Tables("General").Rows(0)
            Dim employeeCode As String = drGeneral("EmployeeCode")
            xlWorkSheet.Cells(rowIndex, 2) = drGeneral("EmployeeName")
            xlWorkSheet.Cells(rowIndex, 4) = drGeneral("DivisionName")
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 2) = employeeCode
            xlWorkSheet.Cells(rowIndex, 4) = drGeneral("DepartmentName")
            '
            rowIndex = rowIndex + 2
            xlWorkSheet.Cells(rowIndex, 4) = "TRANSPORTATION FEE"
            'request details                   
            rowIndex = rowIndex + 1
            Dim nextRowIndex = rowIndex + 20
            Dim dtDetails As DataTable = ds.Tables("Details")
            If dtDetails.Rows.Count > 20 Then
                nextRowIndex = nextRowIndex + dtDetails.Rows.Count - 20
                Dim currentRow As Excel.Range = CType(xlWorkSheet.Rows(rowIndex + 1), Excel.Range)
                For i As Integer = 1 To dtDetails.Rows.Count - 20
                    currentRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown, currentRow.Copy(Type.Missing))
                Next
            End If

            For i As Integer = 0 To dtDetails.Rows.Count - 1
                Dim colIndex As Integer = 1
                Dim drDetails As DataRow = dtDetails.Rows(i)
                xlWorkSheet.Cells(rowIndex, colIndex) = drDetails("Date")
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = drDetails("Time")
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = drDetails("WorkingArea")
                colIndex += 1
                Dim estimateFee As Decimal = CommonFunction._ToMoney(drDetails("TransportationFee"))
                xlWorkSheet.Cells(rowIndex, colIndex) = If(estimateFee > 0, estimateFee.ToString(), "")
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = drDetails("Task")
                rowIndex = rowIndex + 1
            Next
            '
            rowIndex = nextRowIndex
            Dim drSummary As DataRow = ds.Tables("Summary").Rows(0)
            xlWorkSheet.Cells(rowIndex, 4) = drSummary("TotalEstimate")
            '
            Dim filePath As String = String.Concat("~/Export/Download/", _username)
            Dim dir As DirectoryInfo = New DirectoryInfo(Server.MapPath(filePath))
            If Not dir.Exists() Then
                dir.Create()
            End If
            dir.Refresh()
            filePath = String.Concat(filePath, "/", "BT_Schedule_", employeeCode, "_", DateTime.Now.ToString("yyyyMMddHHmmssfffff"), ".xls")
            '
            xlWorkBook.SaveAs(Server.MapPath(filePath), Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, False, False, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlUserResolution, True, misValue, misValue, misValue)
            CType(sender, Button).Attributes("data-file-path") = filePath.Substring(1)
            '            
            xlWorkBook.Close(False)
            xlApp.Quit()
            CommonFunction.ReleaseObject(xlWorkSheet)
            CommonFunction.ReleaseObject(xlWorkBook)
            CommonFunction.ReleaseObject(xlApp)
        Catch ex As Exception
            Try
                xlWorkBook.Close(False)
            Catch
            End Try
            Try
                xlApp.Quit()
            Catch
            End Try
            CommonFunction.ReleaseObject(xlWorkSheet)
            CommonFunction.ReleaseObject(xlWorkBook)
            CommonFunction.ReleaseObject(xlApp)
            '
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDepartureDateChange_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnDepartureDateChange.Click
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            LoadExpenseNorm(True)
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

#Region "Send Notice Email"

    Private Function GenNoticeBody(Optional ByVal link As String = "") As String
        Dim eBody As New StringBuilder("<p><strong>To whom it may concern</strong></p>")
        eBody.Append("<p>This is notification from <strong>B</strong>usiness <strong>T</strong>rip Online <strong>S</strong>ystem (BTS).</p>")
        eBody.Append(String.Format("<p>Regarding Business Trip No. ""<strong>{0}</strong>"", we would like to share the latest information to you as below:</p>", txtBusinessTripNo.Text.Trim()))
        eBody.Append(String.Format("<p><table><tr><td valign='top' width='210'><ul><li style='margin: 0;'><strong>BT No:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", txtBusinessTripNo.Text.Trim()))
        Dim btTypeItem As ListItem = ddlBTType.SelectedItem
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>BT Type:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", If(btTypeItem IsNot Nothing, btTypeItem.Text, "")))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Employee Code/Name:</strong></li></ul></td><td style='color: #0070c0'><span style='color: red'>{0}</span>/<span style='color: red'>{1}</span></td></tr>", txtEmployeeCode.Text.Trim(), txtFullName.Text.Trim()))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Purpose/Destination:</strong></li></ul></td><td style='color: #0070c0'>{0}/{1}</td></tr>", txtExpensePurpose.Text.Trim(), _destination))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Date From:</strong></li></ul></td><td><span style='color: #0070c0'>{0}</span> <strong>To:</strong> <span style='color: #0070c0'>{1}</span></td></tr>", dteExpenseDepartureDate.Date.ToString("dd-MMM-yyyy HH:mm"), dteExpenseReturnDate.Date.ToString("dd-MMM-yyyy HH:mm")))
        Dim budgetName As String = If(ddlBudgetName.SelectedItem IsNot Nothing, ddlBudgetName.SelectedItem.Text, "")
        'If budgetName.Trim().Length > 0 Then
        '    budgetName = budgetName.Substring(budgetName.IndexOf("] -") + 3).Trim()
        'End If
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Budget Code/Name:</strong></li></ul></td><td><span style='color: #0070c0'>{0}/{1}</span></td></tr>", txtBudgetCode.Text.Trim(), budgetName))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Amount:</strong></li></ul></td><td><span style='color: red'>{0} {1}</span></td></tr>", lblExpenseTotalExpense.Text, ddlCurrency.SelectedItem.Text))
        Dim statusDesc As String = CommonFunction._ToString(lnkFINStatus.Attributes("title"))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Status:</strong></li></ul></td><td style='color: red'><strong>{0}</strong> {1}</td></tr>", lnkFINStatus.InnerText, If(statusDesc.Trim().Length > 0, String.Format("({0})", statusDesc), "")))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Comment:</strong></li></ul></td><td><span style='color: #0070c0'>{0}</span></td></tr>", If(lblFINComment.Text = "", "No comment", If(lblFINComment.Text.ToLower() = "(recall)", "This business trip was re-called successfully by requester", lblFINComment.Text))))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Updated by:</strong></li></ul></td><td style='color: #0070c0'>{0} - {1} ({2} Department | {3})</td></tr></table></p>", _username, CommonFunction._ToString(Session("FullName")), CommonFunction._ToString(Session("Department")), CommonFunction._ToString(Session("Division"))))
        eBody.Append(If(link IsNot Nothing AndAlso link <> "", String.Format("<p>Please <a href='{0}{1}'>click here</a> to check/process this information.</p>", ConfigurationManager.AppSettings("Domain"), link), ""))
        eBody.Append("<p>If you are unable to check/process, You can refer to FAQ function from main menu of BTS system to view all Frequently Asked Question.</p>")
        eBody.Append("<p>Thank you for your cooperation.</p>")
        eBody.Append("<p>Regards,<br>BTS Support Team</p>")
        Return eBody.ToString()
    End Function
    '
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
            Dim eSubject As String = String.Format("{0}[BTS Expense]: {1} - {2}/{3} (Status: {4})", indicator, txtBusinessTripNo.Text.Trim(), txtFullName.Text.Trim(), _lastDestination, lnkFINStatus.InnerText)
            Try
                isSent = _objEmail.SendEmail(eFrom, eTo, "", bcc, eSubject, eBody, "", "")
            Catch ex As Exception
                isSent = False
            End Try
        End If
        Return isSent
    End Function

    Private Function SendSubmitEmail() As Boolean
        Dim eTo As String = ""
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToInt(hID.Value), RoleType.Finance.ToString())
        Else
            Dim dtAuthorized As DataTable = BusinessTripProvider.BTRegister_GetAuthorizedUsers(CommonFunction._ToInt(hID.Value))
            Dim dvFIBudget As DataView = dtAuthorized.DefaultView
            dvFIBudget.RowFilter = String.Format("Role in ('{0}', '{1}', '{2}')", RoleType.Finance.ToString(), RoleType.Finance_GA.ToString(), RoleType.Administrator.ToString())
            For Each dr As DataRowView In dvFIBudget
                eTo = String.Concat(eTo, ", ", dr("TMVEmail"))
            Next
            If eTo.Length > 0 Then
                eTo = eTo.Substring(1).Trim()
            End If
        End If
        Dim viewLink As String = String.Format("BTExpenseDeclaration.aspx?id={0}&back=FiExpenseMgmt.aspx&params=btid;eq;{0};amp;bttype;eq;;amp;loc;eq;;amp;fdate;eq;;amp;tdate;eq;;amp;div;eq;;amp;dep;eq;;amp;sec;eq;;amp;group;eq;;amp;ecode;eq;;amp;ename;eq;;amp;btno;eq;", hID.Value)
        Dim eBody As String = GenNoticeBody(viewLink)
        Return SendNoticeEmail(eTo, eBody)
    End Function

    Private Function SendUserEmail() As Boolean
        Dim eTo As String = ""
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToInt(hID.Value), RoleType.Normal.ToString())
        Else
            Dim dtUser As DataTable = UserProvider.GetEmailInfoByBT(CommonFunction._ToInt(hID.Value))
            For Each dr As DataRow In dtUser.Rows
                eTo = String.Concat(eTo, ", ", dr("TMVEmail"))
            Next
            If eTo.Length > 0 Then
                eTo = eTo.Substring(1).Trim()
            End If
        End If
        Dim viewLink As String = String.Format("BTExpenseDeclaration.aspx?id={0}&back=BTExpenseDeclaration.aspx", hID.Value)
        Dim eBody As String = GenNoticeBody(viewLink)
        Return SendNoticeEmail(eTo, eBody)
    End Function

    Private Function SendRecallEmail() As Boolean
        Dim eTo As String = ""
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToInt(hID.Value), RoleType.Finance.ToString())
        Else
            Dim dtAuthorized As DataTable = BusinessTripProvider.BTRegister_GetAuthorizedUsers(CommonFunction._ToInt(hID.Value))
            Dim dvFIBudget As DataView = dtAuthorized.DefaultView
            dvFIBudget.RowFilter = String.Format("Role in ('{0}', '{1}', '{2}')", RoleType.Finance.ToString(), RoleType.Finance_GA.ToString(), RoleType.Administrator.ToString())
            For Each dr As DataRowView In dvFIBudget
                eTo = String.Concat(eTo, ", ", dr("TMVEmail"))
            Next
            If eTo.Length > 0 Then
                eTo = eTo.Substring(1).Trim()
            End If
        End If
        '
        Dim eBody As String = GenNoticeBody()
        Return SendNoticeEmail(eTo, eBody)
    End Function

#End Region
End Class