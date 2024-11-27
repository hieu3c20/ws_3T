Imports System.Drawing
Imports Provider
Imports System.Data
Imports System.IO
Imports DevExpress.Web.ASPxGridView
Imports Microsoft.Office.Interop

Partial Public Class BTAdvanceDeclaration
    Inherits System.Web.UI.Page

    Protected _username As String = String.Empty
    Protected _dtData As DataTable
    Protected _enable As Boolean = True
    Protected _enableBudget As Boolean = False
    Protected _airEnable As Boolean = False
    Protected _isGA As Boolean = False
    Protected _isAdministrator As Boolean = False
    Protected _isDomestic As Boolean
    Protected _destination As String = String.Empty
    Protected _lastDestination As String = String.Empty
    Private _maAndAbove() As String
    'Private _dgmAndAbove() As String
    Private _gmAndAbove() As String
    Private _wifi() As String
    Private _sendEmailMode As String = CommonFunction._ToString(ConfigurationManager.AppSettings("SendEmailMode")).ToLower()
    Private _oldBudget As String = String.Empty
    Private _dtAuthorized As DataTable
    Private _isRejectToBudget As Boolean = False
    Private _dtBTData As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CommonFunction.CheckSession()
        '
        _maAndAbove = CommonFunction.StrSplit(CommonFunction._ToString(LookupProvider.GetByCodeAndValue("TITLES", "ma_above")).ToLower())
        '_dgmAndAbove = CommonFunction.StrSplit(CommonFunction._ToString(LookupProvider.GetByCodeAndValue("TITLES", "dgm_above")).ToLower())
        _gmAndAbove = CommonFunction.StrSplit(CommonFunction._ToString(LookupProvider.GetByCodeAndValue("TITLES", "gm_above")).ToLower())
        _wifi = CommonFunction.StrSplit(CommonFunction._ToString(LookupProvider.GetByCodeAndValue("TITLES", "wifi")).ToLower())
        '
        _username = CommonFunction._ToString(Session("UserName"))
        '
        _isRejectToBudget = False
        '
        txtBudgetCode.Text = If(ddlBudgetName.SelectedValue <> "", ddlBudgetName.SelectedValue.Split("-")(1), "")
        txtSelectBudgetCode.Text = If(ddlSelectBudgetName.SelectedValue <> "", ddlSelectBudgetName.SelectedValue.Split("-")(1), "")
        '
        _isDomestic = ddlBTType.SelectedValue.IndexOf("domestic") >= 0
        '
        Dim budgedCode As Integer = CommonFunction._ToInt(ddlBudgetName.SelectedValue.Split("-")(0))
        txtBudgetRemain.ForeColor = Color.Red
        txtBudgetRemain.Text = "Check by BCS" 'If(budgedCode = -1, "", String.Format("{0:#,0.##} VND", BusinessTripProvider.BTRegister_GetBudgetRemaining(budgedCode, CommonFunction._ToInt(hID.Value))))
        '
        If Not IsPostBack Then
            InitForm()
        Else
            Dim role As String = CommonFunction._ToString(Session("RoleType"))
            _isGA = role.ToLower() = RoleType.GA.ToString().ToLower() OrElse role.ToLower() = RoleType.TOFS_AIR_GA.ToString().ToLower() OrElse role.ToLower() = RoleType.Finance_GA.ToString().ToLower() OrElse role.ToLower() = RoleType.Administrator.ToString().ToLower()
            _isAdministrator = role.ToLower() = RoleType.Administrator.ToString().ToLower()
            '
            Dim btID As Integer = CommonFunction._ToInt(hID.Value)
            If btID > 0 Then
                _dtBTData = BusinessTripProvider.BTRegister_GetByID(btID)
            Else
                _dtBTData = Nothing
            End If
            If _dtBTData IsNot Nothing AndAlso _dtBTData.Rows.Count > 0 Then
                Dim drData As DataRow = _dtBTData.Rows(0)
                Dim isSubmited As Boolean = CommonFunction._ToBoolean(drData("IsSubmited"))
                Dim isBudgetChecked As Boolean = CommonFunction._ToBoolean(drData("BudgetChecked"))
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
                _enableBudget = (isOwner OrElse isEmp OrElse isTimeKeeper) AndAlso (finStatus = FIStatus.budget_rejected.ToString() OrElse finStatus = FIStatus.budget_reconfirmed.ToString())
                _airEnable = _isGA AndAlso isBudgetChecked AndAlso finStatus <> FIStatus.cancelled.ToString()
            Else
                _enable = True
                'spanBtnExportSchedule.Visible = False
                'spanExportBT.Visible = False
                _airEnable = False
            End If
            LoadBTRegisterAttachment(False)
        End If
        CommonFunction.CheckSessionMessage(Me)
        LoadForm()
    End Sub

    Private Sub InitForm()
        InitCurrency()
        InitBTType()
        'InitBudgetName()
        SetPreParams()
        'InitCountry()
        'InitDestination()
        InitBatchName()
        InitOraSupplier()
        InitEmployees()
        dteGLDate.Date = DateTime.Now
        dteInvoiceDate.Date = DateTime.Now
        'LoadExpenseNorm()
        txtSelectEmployeeCode.Attributes("data-code") = String.Format("{0} - {1}", _username, CommonFunction._ToString(Session("FullName"))) '_username        
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
        InitSearchBudgetName(True, chkBudgetAll.Checked)
        InitSelectBudgetName()
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
        _dtAuthorized = UserProvider.tbl_User_GetAuthorizedAccounts(_username)
        builder.Append("[")
        For Each item As DataRow In _dtAuthorized.Rows
            builder.Append(String.Concat("{ value: '", item("UserName"), " - ", item("FullName"), "', data: '", item("UserName"), "'},"))
        Next
        builder = New StringBuilder(builder.ToString().TrimEnd(","))
        builder.Append("]")
        Return builder.ToString()
    End Function

    Protected Function GetSelectAuthorizedAccounts() As String
        Dim builder As New StringBuilder()
        builder.Append("[")
        For Each item As DataRow In _dtAuthorized.Rows
            builder.Append(String.Concat("{ value: '", item("UserName"), " - ", item("FullName"), " ', data: '", item("UserName"), " - ", item("FullName"), "'},"))
        Next
        builder = New StringBuilder(builder.ToString().TrimEnd(","))
        builder.Append("]")
        Return builder.ToString()
    End Function

    Private Sub InitEmployees()
        Dim dtData As DataTable = mTimeKeeperProvider.m_TimeKeeper_GetByCode(_username)
        CommonFunction.LoadDataToGrid(grvChooseEmployee, dtData)
    End Sub

    Private Sub InitCurrency()
        Dim dtPolicy As DataTable = LookupProvider.GetByCode("POLICY_CURRENCY")
        CommonFunction.LoadLookupDataToComboBox(ddlCurrency, dtPolicy, False)
        'Dim dt As DataTable = LookupProvider.GetByCode("CURRENCY")
        CommonFunction.LoadLookupDataToComboBox(ddlAirCurrency, dtPolicy, False)
    End Sub

    Private Sub InitAirPeriod()
        Dim dt As DataTable = AirTicketProvider.AirPeriod_GetAll()
        CommonFunction.LoadDataToComboBox(ddlAirPeriod, dt, "Name", "ID", True, "", "")
    End Sub

    Private Sub InitBTType()
        Dim dt As DataTable = LookupProvider.GetByCode("BT_TYPE")
        CommonFunction.LoadLookupDataToComboBox(ddlBTType, dt, True, "All", "", "Value like 'overnight_%'")
        CommonFunction.LoadLookupDataToComboBox(ddlSelectBTType, dt, True, "", "", "Value like 'overnight_%'")
    End Sub

    Private Sub InitBudgetName()
        InitSearchBudgetName(True)
        InitSelectBudgetName()
    End Sub

    Private Sub InitSearchBudgetName(ByVal isSearch As Boolean, Optional ByVal all As Boolean = False, Optional ByVal getAll As Boolean = False)
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
        CommonFunction.LoadDataToComboBox(ddlBudgetName, dt, "BudgetItem", "BudgetCodeItem", dt.Rows.Count > 1, "")
        txtBudgetCode.Text = If(ddlBudgetName.SelectedValue <> "", ddlBudgetName.SelectedValue.Split("-")(1), "")
    End Sub

    Private Sub InitSelectBudgetName(Optional ByVal all As Boolean = False)
        Dim employeeCode = If(txtSelectEmployeeCode.Text.Trim().Length = 0, _username, txtSelectEmployeeCode.Text.Split("-")(0).Trim())
        Dim dt As DataTable
        If all Then
            dt = mBudgetProvider.GetOtherByType(employeeCode, ddlSelectBTType.SelectedValue, True)
        Else
            dt = mBudgetProvider.GetByDepartmentAndType(employeeCode, ddlSelectBTType.SelectedValue, True)
        End If
        CommonFunction.LoadDataToComboBox(ddlSelectBudgetName, dt, "BudgetItem", "BudgetCodeItem", dt.Rows.Count > 1, "", "")
        txtSelectBudgetCode.Text = If(ddlSelectBudgetName.SelectedValue <> "", ddlSelectBudgetName.SelectedValue.Split("-")(1), "")
    End Sub

    Private Sub InitCountry(Optional ByVal forAdd As Boolean = False)
        Dim dt As DataTable = mCountryProvider.GetAll()
        CommonFunction.LoadDataToComboBox(ddlDestinationCountry, dt, "Name", "Code", True, "", "", If(Not _isDomestic, "Code <> 'VN'", "Code = 'VN'"))
        CommonFunction.LoadDataToComboBox(ddlWifiDeviceCountry, dt, "Name", "Code", True, "", "", If(Not _isDomestic, "Code <> 'VN'", "Code = 'VN'"))
        If forAdd Then
            CommonFunction.LoadDataToComboBox(ddlSelectDestinationCountry, dt, "Name", "Code", True, "", "", If(Not _isDomestic, "Code <> 'VN'", "Code = 'VN'"))
            If _isDomestic Then
                CommonFunction.SetCBOValue(ddlSelectDestinationCountry, "VN")
                ddlSelectDestinationCountry.Enabled = False
            Else
                ddlSelectDestinationCountry.Enabled = True
            End If
        End If
    End Sub

    Private Sub InitDestination()
        Dim dt As DataTable = mDestinationProvider.GetByCountryCode(ddlDestinationCountry.SelectedValue)
        CommonFunction.LoadDataToComboBox(ddlDestinationLocation, dt, "Name", "DestinationID", True, "", "")
        CommonFunction.LoadDataToComboBox(ddlRequestDestination, dt, "Name", "DestinationID", True, "", "")
    End Sub

    Private Sub InitBatchName()
        Dim dtBT As DataTable = mBatchNameProvider.GetActive()
        CommonFunction.LoadDataToComboBox(ddlBatchName, dtBT, "BatchName", "ID", True, "", "")
    End Sub

    Private Sub InitOraSupplier()
        Dim dtBT As DataTable = mOraSupplierProvider.GetActive()
        CommonFunction.LoadDataToComboBox(ddlOraSupplier, dtBT, "SupplierName", "OraLink", True, "", "")
    End Sub

    Private Sub LoadTotalSummary(Optional ByVal reset As Boolean = False)
        If reset Then
            lblDailyAllowance.Text = "0"
            lblHotelExpense.Text = "0"
            lblMovingTimeAllowance.Text = "0"
            lblOther.Text = "0"
            lblTotalAdvance.Text = "0"
            lblTotalAdvance.ToolTip = ""
            hRequestGA.Value = ""
        Else
            'Total Summary
            Dim dtSummary As DataTable = BusinessTripProvider.BTRegister_GetSummary(CommonFunction._ToInt(hID.Value))
            If dtSummary.Rows.Count > 0 Then
                Dim drSummary As DataRow = dtSummary.Rows(0)
                lblDailyAllowance.Text = CommonFunction._FormatMoney(drSummary("DailyAllowance"))
                lblHotelExpense.Text = CommonFunction._FormatMoney(drSummary("HotelExpense"))
                lblMovingTimeAllowance.Text = CommonFunction._FormatMoney(drSummary("NMovingTimeAllowance"))
                lblOther.Text = CommonFunction._FormatMoney(drSummary("Other"))
                hOtherAmount.Value = CommonFunction._ToString(drSummary("OtherOnly"))
                lblTotalAdvance.Text = CommonFunction._FormatMoney(drSummary("TotalAdvanceRounded")) 'TotalAdvance
                'lblRequestGA.Text = CommonFunction._ToString(drSummary("RequestGA"))
                hRequestGA.Value = CommonFunction._ToString(drSummary("RequestGA"))
                '
                'Dim totalAdvance As Decimal = CommonFunction._ToMoney(drSummary("TotalAdvance"))
                'Dim totalAdvanceRounded As Decimal = CommonFunction._ToMoney(drSummary("TotalAdvanceRounded"))
                'If totalAdvance <> totalAdvanceRounded Then
                '    lblTotalAdvance.ToolTip = String.Format("This total value was rounded by finance rule (Actual value is {0})", CommonFunction._FormatMoney(totalAdvance))
                'Else
                '    lblTotalAdvance.ToolTip = ""
                'End If
            End If
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
        'BusinessTripProvider.BTRegister_DeleteDraft(_username)
        '
        GetDataTable()
        LoadRegister()
        LoadSubmitted()
        LoadRejected()
        LoadCompleted()
        LoadCancelled()
    End Sub

    Private Sub GetDataTable()
        Dim viewID As Integer = CommonFunction._ToInt(Request.QueryString("id"))
        If viewID > 0 Then
            _dtData = BusinessTripProvider.tbl_BT_Register_SearchForView(viewID, _username)
        Else
            Dim btNo As String = hsBTNo.Value
            Dim btType As String = hsBTType.Value 'ddlBTType.SelectedValue
            Dim fullName As String = hsFullName.Value
            Dim empCode As String = hsEmployeeCode.Value 'txtEmployeeCode.Text.Trim()
            Dim budgetCode As String = txtBudgetCode.Text.Trim() 'If(hsBudgetName.Value = "All", "", hsBudgetName.Value)
            Dim departureFrom As DateTime = dteDepartureFrom.Date
            Dim departureTo As DateTime = dteDepartureTo.Date

            _dtData = BusinessTripProvider.tbl_BT_Register_Search(btType, fullName, empCode, budgetCode, _username, btNo, departureFrom, departureTo, "overnight")
        End If
    End Sub

    Private Sub LoadRegister()
        CommonFunction.LoadDataToGrid(grvBTRegister, _dtData, "IsSubmited = 0", "No")
    End Sub

    Private Sub LoadSubmitted()
        CommonFunction.LoadDataToGrid(grvBTSubmitted, _dtData, "IsSubmited = 1 and FIStatus <> '" + FIStatus.completed.ToString() + "' and FIStatus <> '" + FIStatus.rejected.ToString() + "' and FIStatus <> '" + FIStatus.cancelled.ToString() + "'", "No")
    End Sub

    Private Sub LoadRejected()
        CommonFunction.LoadDataToGrid(grvBTRejected, _dtData, "IsSubmited = 1 and FIStatus = '" + FIStatus.rejected.ToString() + "'", "No")
    End Sub

    Private Sub LoadCompleted()
        CommonFunction.LoadDataToGrid(grvBTCompleted, _dtData, "IsSubmited = 1 and FIStatus = '" + FIStatus.completed.ToString() + "'", "No")
    End Sub

    Private Sub LoadCancelled()
        CommonFunction.LoadDataToGrid(grvBTCancelled, _dtData, "IsSubmited = 1 and FIStatus = '" + FIStatus.cancelled.ToString() + "'", "No")
    End Sub

    Private Sub LoadBTRegisterInfo(Optional ByVal reload As Boolean = True)
        If reload Then
            _dtBTData = BusinessTripProvider.BTRegister_GetByID(CommonFunction._ToInt(hID.Value))
        End If
        If _dtBTData IsNot Nothing AndAlso _dtBTData.Rows.Count > 0 Then
            Dim drData As DataRow = _dtBTData.Rows(0)
            'For send notice email
            _destination = CommonFunction._ToString(drData("Destination"))
            _lastDestination = CommonFunction._ToString(drData("LastDestination"))
            '
            CommonFunction.SetCBOValue(ddlCurrency, drData("Currency"))
            lblTransportationAndOtherCurrency.Text = If(ddlCurrency.SelectedItem IsNot Nothing, ddlCurrency.SelectedItem.Text, ddlCurrency.SelectedValue.ToUpper())
            Dim departureDate As DateTime = CommonFunction._ToDateTime(drData("DepartureDate"))
            If departureDate > DateTime.MinValue Then
                dteDepartureDate.Date = departureDate
            Else
                dteDepartureDate.Value = Nothing
            End If
            '
            LoadExpenseNorm()
            '
            Dim returnDate As DateTime = CommonFunction._ToDateTime(drData("ReturnDate"))
            If returnDate > DateTime.MinValue Then
                dteReturnDate.Date = returnDate
            Else
                dteReturnDate.Value = Nothing
            End If
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
            CommonFunction.SetCBOValue(ddlDestinationCountry, country)
            InitDestination()
            txtPurpose.Text = CommonFunction._ToString(drData("Purpose"))
            '
            chkRequestAirTicket.Checked = CommonFunction._ToBoolean(drData("AirTicket"))
            chkRequestTrainTicket.Checked = CommonFunction._ToBoolean(drData("TrainTicket"))
            chkRequestCar.Checked = CommonFunction._ToBoolean(drData("Car"))
            If chkRequestAirTicket.Checked Then
                'trExpectedDeparture.Visible = True
                'trExpectedReturn.Visible = True
                dteExpectedDepartureTime.Value = drData("ExpectedDepartureTime")
                txtExpectedDepartureFlightNo.Text = CommonFunction._ToString(drData("ExpectedDepartureFlightNo"))
                dteExpectedReturnTime.Value = drData("ExpectedReturnTime")
                txtExpectedReturnFlightNo.Text = CommonFunction._ToString(drData("ExpectedReturnFlightNo"))
            Else
                dteExpectedDepartureTime.Value = Nothing
                dteExpectedReturnTime.Value = Nothing
                txtExpectedDepartureFlightNo.Text = ""
                txtExpectedReturnFlightNo.Text = ""
            End If
            chkShowNoRequestAdvance.Checked = CommonFunction._ToBoolean(drData("NoRequestAdvance"))
            chkNoRequestAdvance.Checked = chkShowNoRequestAdvance.Checked
            If chkNoRequestAdvance.Checked Then
                tdRequestDestinationControl.Visible = True
                tdRequestDestinationLabel.Visible = True
                CommonFunction.SetCBOValue(ddlRequestDestination, drData("DestinationID"))
            Else
                ddlRequestDestination.ClearSelection()
            End If
            '
            txtEmployeeCode.Text = CommonFunction._ToString(drData("EmployeeCode"))
            '
            chkBudgetAll.Checked = CommonFunction._ToBoolean(drData("CheckAllBudget"))
            'LoadInfo(False)
            txtFullName.Text = CommonFunction._ToString(drData("EmployeeName"))
            txtEmail.Text = CommonFunction._ToString(drData("Email")) '
            txtLocation.Text = CommonFunction._ToString(drData("BranchName"))
            hLocationID.Value = CommonFunction._ToString(drData("BranchID"))
            txtDivision.Text = CommonFunction._ToString(drData("DivisionName"))
            hDivisionID.Value = CommonFunction._ToString(drData("DivisionID"))
            txtDepartment.Text = CommonFunction._ToString(drData("DepartmentName"))
            hDepartmentID.Value = CommonFunction._ToString(drData("DepartmentID"))
            txtSection.Text = CommonFunction._ToString(drData("SectionName"))
            hSectionID.Value = CommonFunction._ToString(drData("SectionID"))
            hGroup.Value = CommonFunction._ToString(drData("GroupName"))
            hGroupID.Value = CommonFunction._ToString(drData("GroupID"))
            txtPosition.Text = CommonFunction._ToString(drData("Position"))
            txtMobile.Text = CommonFunction._ToString(drData("Mobile"))
            '
            'hIsDGMAndAbove.Value = If(Array.IndexOf(_dgmAndAbove, txtPosition.Text.Trim().ToLower()) >= 0, "Y", "N")
            hIsGMAndAbove.Value = If(Array.IndexOf(_gmAndAbove, txtPosition.Text.Trim().ToLower()) >= 0, "Y", "N")
            '
            If Array.IndexOf(_maAndAbove, txtPosition.Text.Trim().ToLower()) >= 0 Then
                tdCredit.Visible = True
                chkCredit.Checked = CommonFunction._ToString(drData("PaymentType")) = "CC"
                'moving time allowance
                spanMovingTime.Visible = False
                chkMovingTimeAllowance.Checked = False
            Else
                tdCredit.Visible = False
                chkCredit.Checked = False
                '
                spanMovingTime.Visible = True
                chkMovingTimeAllowance.Checked = CommonFunction._ToMoney(drData("MovingTimeAllowance")) > 0
            End If
            '
            If Not _isDomestic Then
                'If txtEmployeeCode.Text.IndexOf("11") = 0 OrElse Array.IndexOf(_wifi, txtPosition.Text.Trim().ToLower()) >= 0 Then
                '    chkRequestWifi.Visible = True
                '    lblRequestWifi.Visible = True
                '    panWifiNote.Visible = True
                '    chkRequestWifi.Checked = CommonFunction._ToBoolean(drData("Wifi"))
                'Else
                '    chkRequestWifi.Visible = False
                '    lblRequestWifi.Visible = False
                '    panWifiNote.Visible = False
                '    chkRequestWifi.Checked = False
                'End If
                '
                chkFirstTimeOversea.Checked = CommonFunction._ToMoney(drData("FirstTimeOverSea")) > 0
                spanFirstTimeOversea.Visible = True
            Else
                'chkRequestWifi.Visible = False
                'lblRequestWifi.Visible = False
                'panWifiNote.Visible = False
                'chkRequestWifi.Checked = False
                '
                chkFirstTimeOversea.Checked = False
                spanFirstTimeOversea.Visible = False
            End If
            'chkFirstTimeChange()
            Dim firstTimeAmount As Decimal = CommonFunction._ToMoney(drData("FirstTimeOverSea"))
            If firstTimeAmount <= 0 Then
                firstTimeAmount = CommonFunction._ToMoney(LookupProvider.GetByCodeAndValue("PRE_PARAMS", "FIRST_TIME_OVERSEA"))
            End If
            spanFirstAmount.InnerText = String.Format("({0:#,0.##} USD)", firstTimeAmount)
            hFirstTime.Value = firstTimeAmount.ToString()
            'chkMovingTimeChange()
            Dim movingTimeAmount As Decimal = CommonFunction._ToMoney(drData("MovingTimeAllowance"))
            Dim movingTimeCurrency As String = CommonFunction._ToString(drData("MovingTimeCurrency"))
            If movingTimeAmount <= 0 Then
                Dim dtPolicy As DataTable = mMovingTimeAllowanceProvider.m_Expense_GetPolicy(ddlDestinationCountry.SelectedValue)
                If dtPolicy.Rows.Count > 0 Then
                    Dim drPolicy As DataRow = dtPolicy.Rows(0)
                    movingTimeAmount = CommonFunction._ToMoney(drPolicy("Amount"))
                    movingTimeCurrency = CommonFunction._ToString(drPolicy("Currency"))
                Else
                    movingTimeAmount = 0
                    movingTimeCurrency = If(_isDomestic, "VND", "USD")
                End If
            End If
            spanMovingTimeAmount.InnerText = String.Format("({0:#,0.##} {1})", movingTimeAmount, movingTimeCurrency)
            hMovingTime.Value = movingTimeAmount.ToString()
            'lnkHRStatus.Attributes("class") = CommonFunction._ToString(drData("HRStatus"))
            'lnkHRStatus.InnerText = CommonFunction._ToString(drData("HRStatusText"))
            'lnkGAStatus.Attributes("class") = CommonFunction._ToString(drData("GAStatus"))
            'lnkGAStatus.InnerText = CommonFunction._ToString(drData("GAStatusText"))
            lnkFINStatus.Attributes("class") = CommonFunction._ToString(drData("FIStatus"))
            lnkFINStatus.InnerText = CommonFunction._ToString(drData("FIStatusText"))
            lnkFINStatus.Attributes("title") = CommonFunction._ToString(drData("FIStatusDescription"))
            Dim comment As String = CommonFunction._ToString(drData("RejectReasonFI"))
            lblFINComment.Text = If(comment.Trim().Length > 0, String.Format("({0})", comment), "")
            '
            LoadBTRegisterHistory()
            '
            LoadTotalSummary()
            '
            Dim isSubmited As Boolean = CommonFunction._ToBoolean(drData("IsSubmited"))
            Dim isBudgetChecked As Boolean = CommonFunction._ToBoolean(drData("BudgetChecked"))
            Dim finStatus As String = CommonFunction._ToString(drData("FIStatus"))
            Dim isOwner As Boolean = CommonFunction._ToString(_dtBTData.Rows(0)("CreatedBy")) = _username
            Dim isEmp As Boolean = CommonFunction._ToString(_dtBTData.Rows(0)("EmployeeCode")) = _username
            Dim isTimeKeeper As Boolean = CommonFunction._ToString(_dtBTData.Rows(0)("TimeKeeper")).IndexOf(_username) >= 0
            '
            spanBtnExportSchedule.Visible = isOwner OrElse _isAdministrator OrElse isEmp OrElse isTimeKeeper OrElse _isGA
            spanExportSchedule1.Visible = isOwner OrElse _isAdministrator OrElse isEmp OrElse isTimeKeeper OrElse _isGA
            spanExportBT.Visible = isOwner OrElse _isAdministrator OrElse isEmp OrElse isTimeKeeper OrElse _isGA
            '
            _enable = (isOwner OrElse isEmp OrElse isTimeKeeper) AndAlso (Not isSubmited OrElse finStatus = FIStatus.rejected.ToString())
            _enableBudget = (isOwner OrElse isEmp OrElse isTimeKeeper) AndAlso (finStatus = FIStatus.budget_rejected.ToString() OrElse finStatus = FIStatus.budget_reconfirmed.ToString())
            _airEnable = _isGA AndAlso isBudgetChecked AndAlso finStatus <> FIStatus.cancelled.ToString()
            '
            Dim enableRecall As Boolean = (isOwner OrElse isEmp OrElse isTimeKeeper) AndAlso finStatus = FIStatus.pending.ToString()
            btnConfirmRecall.Visible = enableRecall
            btnRecall.Visible = enableRecall
            '
            btnCancel.Attributes("enable-form") = If(_enable, "true", "false")
            btnCancel.Attributes("enable-air-form") = If(_airEnable, "true", "false")
            '
            ShowInfoRows(True)
            EnableForm(_enable)
            '                        
            EnableInfoForm(False)
            '
            EnableApproveButtons()
            '
            CommonFunction.SetCBOValue(ddlBudgetName, drData("BudgetName"))
            txtBudgetCode.Text = CommonFunction._ToString(drData("BudgetCode"))
            txtProjectBudgetCode.Text = CommonFunction._ToString(drData("ProjectBudgetCode"))
            '
            Dim budgetName As String = If(ddlBudgetName.SelectedItem IsNot Nothing, ddlBudgetName.SelectedItem.Text, "")
            'If budgetName.Trim().Length > 0 Then
            '    budgetName = budgetName.Substring(budgetName.IndexOf("] -") + 3).Trim()
            'End If
            hOldBudget.Value = String.Format("{0}/{1}", txtBudgetCode.Text, budgetName)
            '
            Dim budgedCode As Integer = CommonFunction._ToInt(ddlBudgetName.SelectedValue.Split("-")(0))
            'txtBudgetRemain.Text = If(budgedCode = -1, "", String.Format("{0:#,0.##} VND", BusinessTripProvider.BTRegister_GetBudgetRemaining(budgedCode, CommonFunction._ToInt(hID.Value))))
        End If
    End Sub

    Private Sub EnableApproveButtons()
        btnShowApprove.Visible = False
        tabApproveMessage.Visible = False
        spanApproveLabel.InnerText = "Approve"
        btnApprove.Text = "Approve"
        btnShowRejectToBudget.Visible = False
        btnShowReject.Visible = False
        btnShowBudgetReject.Visible = False
        panRejectInfo.Visible = False
        btnShowConfirmBudget.Visible = False
        panConfirmBudget.Visible = False
        tdOraStatus.Visible = False
        btnBudgetReject.Visible = False
        btnReject.Visible = False
        btnRejectToBudget.Visible = False
        btnShowResetAdvance.Visible = False
        btnResetAdvance.Visible = False
        btnShowCancelBT.Visible = False
        btnCancelBT.Visible = False
        '
        'ddlBudgetName.Attributes("onchange") = "BudgetCodeChange(this, 'txtBudgetCode');"
        'ddlBudgetName.AutoPostBack = False
        '
        Dim role As String = CommonFunction._ToString(Session("RoleType"))
        If role.ToLower() = RoleType.Finance.ToString().ToLower() _
            OrElse role.ToLower() = RoleType.Administrator.ToString().ToLower() _
            OrElse role.ToLower() = RoleType.Finance_GA.ToString().ToLower() _
            OrElse role.ToLower() = RoleType.Finance_Budget.ToString().ToLower() Then
            '
            Dim viewID As Integer = CommonFunction._ToInt(Request.QueryString("id"))
            _dtData = BusinessTripProvider.tbl_BT_Register_ViewByID(viewID, _username)
            '
            If _dtData.Rows.Count > 0 Then
                Dim drData As DataRow = _dtData.Rows(0)
                Dim isSubmit As Boolean = CommonFunction._ToBoolean(drData("IsSubmited"))
                If isSubmit Then
                    Dim status As String = String.Empty
                    Dim oraStatus As String = String.Empty
                    Dim budgetChecked As Boolean = False
                    '
                    status = CommonFunction._ToString(drData("FIStatus"))
                    If status.ToLower() <> FIStatus.cancelled.ToString() Then
                        oraStatus = CommonFunction._ToString(drData("OraStatus"))
                        budgetChecked = CommonFunction._ToBoolean(drData("BudgetChecked"))
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
                        If status.ToLower() = FIStatus.pending.ToString() OrElse status.ToLower() = FIStatus.budget_reconfirmed.ToString() Then
                            If role.ToLower() = RoleType.Finance_Budget.ToString().ToLower() OrElse role.ToLower() = RoleType.Administrator.ToString().ToLower() Then
                                btnShowConfirmBudget.Visible = True
                                panConfirmBudget.Visible = True
                                If status.ToLower() <> FIStatus.budget_reconfirmed.ToString() Then
                                    btnShowBudgetReject.Visible = True
                                    btnBudgetReject.Visible = True
                                End If
                                btnReject.Visible = True
                                btnShowReject.Visible = True
                                panRejectInfo.Visible = True
                                '
                                ddlBudgetName.Enabled = True
                                ddlBudgetName.Attributes("onchange") = "BudgetCodeChange(this, 'txtBudgetCode'); HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))"
                                ddlBudgetName.AutoPostBack = True
                                chkBudgetAll.Enabled = True
                                txtProjectBudgetCode.ReadOnly = False
                                '
                                InitSearchBudgetName(False, chkBudgetAll.Checked)
                            End If
                        ElseIf budgetChecked AndAlso (status.ToLower() = FIStatus.checked.ToString() OrElse status.ToLower() = FIStatus.completed.ToString()) Then
                            If role.ToLower() <> RoleType.Finance_Budget.ToString().ToLower() Then
                                If status.ToLower() = FIStatus.completed.ToString() Then
                                    If CommonFunction._ToMoney(lblTotalAdvance.Text) > 0 Then
                                        btnShowResetAdvance.Visible = True
                                        btnResetAdvance.Visible = True
                                    End If
                                    btnShowCancelBT.Visible = True
                                    btnCancelBT.Visible = True
                                End If
                                If oraStatus.ToLower() <> "done" AndAlso oraStatus.ToLower() <> "paid" Then
                                    '
                                    btnShowApprove.Visible = True
                                    tabApproveMessage.Visible = True
                                    '
                                    Dim checkExpense As Integer = CommonFunction._ToUnsignInt(drData("CheckExpense"))
                                    If checkExpense = 0 Then
                                        btnShowReject.Visible = True
                                        panRejectInfo.Visible = True
                                        btnReject.Visible = True
                                        btnShowRejectToBudget.Visible = True
                                        btnRejectToBudget.Visible = True
                                    End If
                                    If status.ToLower() = FIStatus.completed.ToString() Then
                                        If CommonFunction._ToMoney(lblTotalAdvance.Text) = 0 Then
                                            btnShowApprove.Visible = False
                                            tabApproveMessage.Visible = False
                                        Else
                                            spanApproveLabel.InnerText = "Re-Approve"
                                            btnApprove.Text = "Re-Approve"
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub EnableForm(ByVal enable As Boolean)
        'ddlCurrency.Enabled = enable AndAlso ddlBTType.SelectedValue.IndexOf("domestic") < 0
        'chkCredit.Enabled = enable
        dteDepartureDate.ReadOnly = Not enable
        dteReturnDate.ReadOnly = Not enable
        txtPurpose.ReadOnly = Not enable
        'ddlDestinationCountry.Enabled = enable AndAlso Not _isDomestic        
        chkFirstTimeOversea.Enabled = enable AndAlso Not chkNoRequestAdvance.Checked
        'spiFirstTimeOverseaVND.ReadOnly = Not enable
        chkMovingTimeAllowance.Enabled = enable AndAlso Not chkNoRequestAdvance.Checked
        'spiMovingTimeAllowanceVND.ReadOnly = Not enable
        txtMobile.ReadOnly = Not enable
        btnFinish.Visible = enable OrElse _enableBudget
        btnShowSave.Visible = enable OrElse _enableBudget
        btnSubmit.Visible = enable OrElse _enableBudget
        btnShowSubmit.Visible = enable OrElse _enableBudget
        panSubmitInfo.Visible = enable OrElse _enableBudget
        chkRequestAirTicket.Enabled = enable
        chkRequestTrainTicket.Enabled = enable
        chkRequestCar.Enabled = enable
        'chkRequestWifi.Enabled = enable
        dteExpectedDepartureTime.ReadOnly = Not enable
        dteExpectedReturnTime.ReadOnly = Not enable
        txtExpectedDepartureFlightNo.ReadOnly = Not enable
        txtExpectedReturnFlightNo.ReadOnly = Not enable
        chkShowNoRequestAdvance.Enabled = enable
        chkNoRequestAdvance.Enabled = enable
        ddlRequestDestination.Enabled = enable
        'btnConfirmBudget.Visible = _budgetEnable
        'request                
        EnableRequestForm(enable AndAlso Not chkNoRequestAdvance.Checked)
        'airticket
        'EnableAirTicketForm(_airEnable)
        'schedule                
        EnableScheduleForm(enable)
        'wifi
        EnableWifiForm(enable)
        'attachment
        EnableAttachmentForm(enable OrElse _enableBudget)
    End Sub

    Private Sub EnableRequestForm(ByVal enable As Boolean)
        'dteRequestDate.ReadOnly = Not enable        
        ddlDestinationLocation.Enabled = enable
        dteFromDate.ReadOnly = Not enable
        dteToDate.ReadOnly = Not enable
        txtBreakfastQty.ReadOnly = Not enable
        'txtBreakfastUnit.ReadOnly = Not enable
        txtLunchQty.ReadOnly = Not enable
        'txtLunchUnit.ReadOnly = Not enable
        txtDinnerQty.ReadOnly = Not enable
        'txtDinnerUnit.ReadOnly = Not enable
        txtOtherMealQty.ReadOnly = Not enable
        'txtOtherMealUnit.ReadOnly = Not enable
        txtHotelQty.ReadOnly = Not enable
        txtHotelUnit.ReadOnly = Not enable
        txtOtherAmount.ReadOnly = Not enable
        txtOther.ReadOnly = Not enable
        'txaRemark.ReadOnly = Not enable
        '
        btnShowSaveRequest.Visible = enable
        btnSaveRequest.Visible = enable
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

    Private Sub EnableScheduleForm(ByVal enable As Boolean)
        dteScheduleDate.ReadOnly = Not enable
        txeFromTime.ReadOnly = Not enable
        txeToTime.ReadOnly = Not enable
        txtWorkingArea.ReadOnly = Not enable
        txtTask.ReadOnly = Not enable
        spiEstimateTransportationFee.ReadOnly = Not enable
        'chkAirTicket.Enabled = enable
        'chkTrainTicket.Enabled = enable
        'chkCar.Enabled = enable
        '
        btnShowSaveSchedule.Visible = enable
        btnSaveSchedule.Visible = enable
    End Sub

    Private Sub EnableWifiForm(ByVal enable As Boolean)
        enable = (enable AndAlso Not _isDomestic AndAlso (txtEmployeeCode.Text.IndexOf("11") = 0 OrElse Array.IndexOf(_wifi, txtPosition.Text.Trim().ToLower()) >= 0))
        ddlWifiDeviceCountry.Enabled = enable
        dtWifiDeviceFromDate.ReadOnly = Not enable
        dtWifiDeviceToDate.ReadOnly = Not enable
        '
        btnShowSaveWifiDevice.Visible = enable
        btnSaveWifiDevice.Visible = enable
    End Sub

    Private Sub EnableAttachmentForm(ByVal enable As Boolean)
        tdUploadCaption.Visible = enable
        tdUpload1.Visible = enable
        tdUpload2.Visible = enable
        tdUpload3.Visible = enable
        tdUploadDesc.Attributes("colspan") = If(enable, "2", "1")
        txtDescription.ReadOnly = Not enable
    End Sub

    Private Sub LoadBTRegisterHistory()
        Dim dtData As DataTable = BusinessTripProvider.BTRegisterHistory_Search(CommonFunction._ToInt(hID.Value))
        CommonFunction.LoadDataToGrid(grvFIStatusHistory, dtData, "[Type] = '" + UserType.FI.ToString() + "'", "No")
    End Sub

    Private Sub LoadBTRegisterRequest(Optional ByVal clear As Boolean = False)
        Dim dtData As DataTable = If(clear, Nothing, BusinessTripProvider.BTRegisterRequest_Search(CommonFunction._ToInt(hID.Value)))
        CommonFunction.LoadDataToGrid(grvBTRequest, dtData, "", Nothing, "EnableForm", _enable)
        '
        LoadTotalSummary()
    End Sub

    Private Sub LoadBTRegisterRequestByID()
        Dim id As Integer = CommonFunction._ToInt(hRequestID.Value)
        Dim dtData As DataTable = BusinessTripProvider.BTRegisterRequest_GetByID(id)
        If dtData.Rows.Count > 0 Then
            Dim drData As DataRow = dtData.Rows(0)
            'LoadExpenseNorm()
            CommonFunction.SetCBOValue(ddlDestinationLocation, drData("DestinationID"))
            Dim fromDate As DateTime = CommonFunction._ToDateTime(drData("FromDate"))
            Dim toDate As DateTime = CommonFunction._ToDateTime(drData("ToDate"))
            If fromDate <> DateTime.MinValue Then
                dteFromDate.Date = fromDate
            Else
                dteFromDate.Value = Nothing
            End If
            If toDate <> DateTime.MinValue Then
                dteToDate.Date = toDate
            Else
                dteToDate.Value = Nothing
            End If
            'txaRemark.Text = CommonFunction._ToString(drData("Remark"))
            'request details              
            'SetRequestAmount()
            txtBreakfastQty.Value = CommonFunction._ToUnsignIntWithNull(drData("BreakfastQty"))
            txtLunchQty.Value = CommonFunction._ToUnsignIntWithNull(drData("LunchQty"))
            txtDinnerQty.Value = CommonFunction._ToUnsignIntWithNull(drData("DinnerQty"))
            txtBreakfastUnit.Value = CommonFunction._ToMoneyWithNull(drData("BreakfastUnit"))
            txtLunchUnit.Value = CommonFunction._ToMoneyWithNull(drData("LunchUnit"))
            txtDinnerUnit.Value = CommonFunction._ToMoneyWithNull(drData("DinnerUnit"))
            txtOtherMealQty.Value = CommonFunction._ToMoneyWithNull(drData("OtherMealQty"))
            txtOtherMealUnit.Value = CommonFunction._ToMoneyWithNull(drData("OtherMealUnit"))
            txtOther.Text = CommonFunction._ToString(drData("Other"))
            txtOtherAmount.Value = CommonFunction._ToMoneyWithNull(drData("OtherUnit"))
            txtHotelQty.Value = CommonFunction._ToUnsignIntWithNull(drData("HotelQty"))
            txtHotelUnit.Value = CommonFunction._ToMoneyWithNull(drData("HotelUnit"))
            txtBreakfastAmount.Value = CommonFunction._ToMoneyWithNull(drData("BreakfastAmount"))
            txtLunchAmount.Value = CommonFunction._ToMoneyWithNull(drData("LunchAmount"))
            txtDinnerAmount.Value = CommonFunction._ToMoneyWithNull(drData("DinnerAmount"))
            txtOtherMealAmount.Value = CommonFunction._ToMoneyWithNull(drData("OtherMealAmount"))
            txtHotelAmount.Value = CommonFunction._ToMoneyWithNull(drData("HotelAmount"))
            txtTotalAmount.Value = CommonFunction._ToMoneyWithNull(drData("TotalAmount"))
            'If CommonFunction._ToMoney(txtOtherAmount.Text) > 0 Then
            '    trOtherExplaination.Attributes("style") = ""
            'Else
            '    trOtherExplaination.Attributes("style") = "display: none"
            'End If 
            '                                    
            dteFromDate.Attributes("style") = "float: left;"
            dteToDate.Attributes("style") = "float: left;"
            txtBreakfastQty.Attributes("style") = "height: 21px;"
            txtLunchQty.Attributes("style") = "height: 21px;"
            txtDinnerQty.Attributes("style") = "height: 21px;"
            txtOtherMealQty.Attributes("style") = "height: 21px;"
            txtHotelQty.Attributes("style") = "height: 21px;"
            txtBreakfastUnit.Attributes("style") = "height: 21px;"
            txtLunchUnit.Attributes("style") = "height: 21px;"
            txtDinnerUnit.Attributes("style") = "height: 21px;"
            txtOtherMealUnit.Attributes("style") = "height: 21px;"
            txtHotelUnit.Attributes("style") = "height: 21px;"
        End If
    End Sub

    Private Sub LoadBTRegisterSchedule(Optional ByVal clear As Boolean = False)
        Dim dtData As DataTable = If(clear, Nothing, BusinessTripProvider.BTRegisterSchedule_Search(CommonFunction._ToInt(hID.Value)))
        CommonFunction.LoadDataToGrid(grvBTSchedule, dtData, "", Nothing, "EnableForm", _enable)
    End Sub

    Private Sub LoadWifiDevice(Optional ByVal clear As Boolean = False)
        Dim dtData As DataTable = If(clear, Nothing, WifiDeviceProvider.WifiDevice_GetByBT(CommonFunction._ToInt(hID.Value)))
        CommonFunction.LoadDataToGrid(grvWifiDevice, dtData, "", Nothing, "EnableForm", _enable)
    End Sub

    Private Sub LoadBTRegisterScheduleByID()
        Dim id As Integer = CommonFunction._ToInt(hScheduleID.Value)
        Dim dtData As DataTable = BusinessTripProvider.BTRegisterSchedule_GetByID(CommonFunction._ToInt(id))
        If dtData.Rows.Count > 0 Then
            Dim drData As DataRow = dtData.Rows(0)
            Dim scheduleDate As DateTime = CommonFunction._ToDateTime(drData("FromTime"))
            If scheduleDate <> DateTime.MinValue Then
                dteScheduleDate.Date = scheduleDate
            Else
                dteScheduleDate.Value = Nothing
            End If
            txeFromTime.DateTime = CommonFunction._ToDateTime(drData("FromTime"))
            txeToTime.DateTime = CommonFunction._ToDateTime(drData("ToTime"))
            txtWorkingArea.Text = CommonFunction._ToString(drData("WorkingArea"))
            txtTask.Text = CommonFunction._ToString(drData("Task"))
            spiEstimateTransportationFee.Value = CommonFunction._ToMoneyWithNull(drData("EstimateTransportationFee"))
            'chkAirTicket.Checked = CommonFunction._ToBoolean(drData("AirTicket"))
            'chkTrainTicket.Checked = CommonFunction._ToBoolean(drData("TrainTicket"))
            'chkCar.Checked = CommonFunction._ToBoolean(drData("Car"))
        End If
    End Sub

    Private Sub LoadWifiDeviceByID()
        Dim id As Integer = CommonFunction._ToInt(hWifiDeviceID.Value)
        Dim dtData As DataTable = WifiDeviceProvider.WifiDevice_GetByID(CommonFunction._ToInt(id))
        If dtData.Rows.Count > 0 Then
            Dim drData As DataRow = dtData.Rows(0)
            Dim fromDate As DateTime = CommonFunction._ToDateTime(drData("FromDate"))
            If fromDate <> DateTime.MinValue Then
                dtWifiDeviceFromDate.Date = fromDate
            Else
                dtWifiDeviceFromDate.Value = Nothing
            End If
            Dim toDate As DateTime = CommonFunction._ToDateTime(drData("ToDate"))
            If toDate <> DateTime.MinValue Then
                dtWifiDeviceToDate.Date = toDate
            Else
                dtWifiDeviceToDate.Value = Nothing
            End If
            CommonFunction.SetCBOValue(ddlWifiDeviceCountry, drData("CountryCode"))
        End If
    End Sub

    Private Sub LoadBTRegisterAttachment(ByVal withDesc As Boolean)
        ClearAttachmentForm(withDesc)
        Dim dtData As DataTable = BusinessTripProvider.BTRAttachment_Search(CommonFunction._ToInt(hID.Value))

        Dim otherFiles As New StringBuilder()
        Dim desc As String = ""
        For Each item As DataRow In dtData.Rows
            Dim path As String = CommonFunction._ToString(item("AttachmentPath"))
            Dim fileName As String = path.Substring(path.LastIndexOf("/") + 1).Substring(25)
            Dim li As String = String.Format("<li style='font-weight: normal'><a href='{0}' target='_blank'>{1}</a>{2}</li>", path, fileName, If(_enable OrElse _enableBudget, String.Format("<input type='button' class='grid-btn delete-btn' title='Remove' data-id='{0}' onclick='DeleteAttachment(this)'>", item("ID")), ""))
            Select Case CommonFunction._ToString(item("AttachmentType"))
                Case "register"
                    'If withDesc Then txtAttRegisterDesc.Text = desc
                    Dim ltrRegister As New Literal()
                    ltrRegister.Text = String.Format("<ol class='attachments'>{0}</ol>", li)
                    panRegisterAttachments.Controls.Add(ltrRegister)
                    desc = CommonFunction._ToString(item("Description"))
                Case "schedule"
                    'If withDesc Then txtAttScheduleDesc.Text = desc
                    Dim ltrSchedule As New Literal()
                    ltrSchedule.Text = String.Format("<ol class='attachments'>{0}</ol>", li)
                    panScheduleAttachments.Controls.Add(ltrSchedule)
                    desc = CommonFunction._ToString(item("Description"))
                Case "other"
                    desc = CommonFunction._ToString(item("Description"))
                    otherFiles.Append(li)
            End Select
        Next
        If withDesc Then txtDescription.Text = desc
        '
        If otherFiles.ToString().Trim().Length > 0 Then
            Dim ltrOther As New Literal()
            ltrOther.Text = String.Format("<ol class='attachments'>{0}</ol>", otherFiles.ToString())
            panOthersAttachments.Controls.Add(ltrOther)
        End If
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
        Dim objUser As tbl_UsersInfo = UserProvider.tbl_User_GetUserInfo_ByUserName(txtEmployeeCode.Text.Trim())
        If objUser Is Nothing Then
            ClearInfoForm()
        Else
            txtFullName.Text = objUser.FullName
            txtEmail.Text = objUser.TMVEmail
            txtLocation.Text = objUser.BranchName
            hLocationID.Value = objUser.BranchID
            txtDivision.Text = objUser.DivisionName
            hDivisionID.Value = objUser.DivisionID
            txtDepartment.Text = objUser.DepartmentName
            hDepartmentID.Value = objUser.DepartmentID
            txtSection.Text = objUser.SectionName
            hSectionID.Value = objUser.SectionID
            hGroup.Value = objUser.GroupName
            hGroupID.Value = objUser.GroupID
            txtPosition.Text = objUser.JobBand
            txtMobile.Text = objUser.Mobile
            If Array.IndexOf(_maAndAbove, txtPosition.Text.Trim().ToLower()) >= 0 Then
                tdCredit.Visible = True
                chkCredit.Checked = True
                '
                spanMovingTime.Visible = False
            Else
                tdCredit.Visible = False
                chkCredit.Checked = False
                '
                spanMovingTime.Visible = True
            End If
        End If
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
        btnSearchEmpInfo.Visible = enable
        txtBusinessTripNo.ReadOnly = Not enable
        txtFullName.ReadOnly = Not enable
        '
        ddlBudgetName.Enabled = _enable OrElse _enableBudget
        ddlBudgetName.AutoPostBack = _enable OrElse _enableBudget
        If Not (_enable OrElse _enableBudget) Then
            ddlBudgetName.Attributes("onchange") = "BudgetCodeChange(this, 'txtBudgetCode');"
        End If
        chkBudgetAll.Enabled = _enable OrElse _enableBudget
        txtProjectBudgetCode.ReadOnly = Not _enable AndAlso Not _enableBudget
        '
        InitSearchBudgetName(Not (_enable OrElse _enableBudget), chkBudgetAll.Checked, Not (_enable OrElse _enableBudget))
        ''
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
        'If Not _enable AndAlso Not _enableBudget AndAlso show Then
        '    spanBudgetAll.Visible = False
        '    ddlBudgetName.Attributes("style") = ""
        '    'InitSearchBudgetName(True)
        'Else
        '    spanBudgetAll.Visible = True
        '    ddlBudgetName.Attributes("style") = "width: 160px !important"
        '    'InitSearchBudgetName(chkBudgetAll.Checked)
        'End If
    End Sub

    'Private Sub ClearBTForm()
    '    hID.Value = ""
    '    ddlCurrency.ClearSelection()
    '    lblTransportationAndOtherCurrency.Text = If(ddlCurrency.SelectedItem IsNot Nothing, ddlCurrency.SelectedItem.Text, ddlCurrency.SelectedValue.ToUpper())
    '    chkFirstTimeOversea.Checked = False
    '    'chkFirstTimeChange()
    '    chkMovingTimeAllowance.Checked = False
    '    'chkMovingTimeChange()
    '    dteDepartureDate.Date = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0)
    '    dteReturnDate.Date = New DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day, 16, 45, 0)
    '    ddlDestinationCountry.ClearSelection()
    '    InitDestination()
    '    txtPurpose.Text = ""
    '    chkRequestAirTicket.Checked = False
    '    chkRequestTrainTicket.Checked = False
    '    chkRequestCar.Checked = False
    '    'trExpectedDeparture.Visible = False
    '    'trExpectedReturn.Visible = False
    '    dteExpectedDepartureTime.Value = Nothing
    '    dteExpectedReturnTime.Value = Nothing
    '    txtExpectedDepartureFlightNo.Text = ""
    '    txtExpectedReturnFlightNo.Text = ""
    '    chkShowNoRequestAdvance.Checked = False
    '    chkNoRequestAdvance.Checked = False
    '    tdRequestDestinationControl.Visible = False
    '    tdRequestDestinationLabel.Visible = False
    '    InitBTStatus()
    '    LoadTotalSummary(True)
    '    '
    '    btnShowApprove.Visible = False
    '    tabApproveMessage.Visible = False
    '    btnShowReject.Visible = False
    '    btnShowBudgetReject.Visible = False
    '    panRejectInfo.Visible = False
    '    btnShowConfirmBudget.Visible = False
    '    panConfirmBudget.Visible = False
    '    tdOraStatus.Visible = False
    '    btnShowRejectToBudget.Visible = False
    'End Sub

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

    Private Sub ClearRequestForm()
        hRequestID.Value = ""
        ddlDestinationLocation.ClearSelection()
        'Dim dtDefault As DateTime = DateTime.Now.AddDays(1)
        'dteFromDate.Date = If(dteDepartureDate.Value Is Nothing, New DateTime(dtDefault.Year, dtDefault.Month, dtDefault.Day, 8, 0, 0), dteDepartureDate.Date)
        'dteToDate.Date = If(dteReturnDate.Value Is Nothing, New DateTime(dtDefault.Year, dtDefault.Month, dtDefault.Day, 16, 45, 0), dteReturnDate.Date)
        'txaRemark.Text = ""
        txtOther.Text = ""
        'txtBreakfastAmount.Value = 0
        'txtBreakfastQty.Value = Nothing
        txtBreakfastUnit.Value = Nothing
        'txtLunchAmount.Value = 0
        'txtLunchQty.Value = Nothing
        txtLunchUnit.Value = Nothing
        'txtDinnerAmount.Value = 0
        'txtDinnerQty.Value = Nothing
        txtDinnerUnit.Value = Nothing
        'txtOtherMealAmount.Value = 0
        'txtOtherMealQty.Value = Nothing
        txtOtherMealUnit.Value = Nothing
        txtOtherAmount.Value = Nothing
        'txtHotelAmount.Value = 0
        'txtHotelQty.Value = Nothing
        txtHotelUnit.Value = Nothing
        'txtTotalAmount.Value = 0
        txtBreakfastUnit.MaxValue = GetSpiMaxValue(0)
        txtLunchUnit.MaxValue = GetSpiMaxValue(0)
        txtDinnerUnit.MaxValue = GetSpiMaxValue(0)
        txtOtherMealUnit.MaxValue = GetSpiMaxValue(0)
        txtHotelUnit.MaxValue = GetSpiMaxValue(0)
        'txtOtherAmount.MaxValue = GetSpiMaxValue(0)
        'LoadExpenseNorm()
        'SetRequestAmount()
        '
        dteFromDate.Attributes("style") = "float: left;"
        dteToDate.Attributes("style") = "float: left;"
        txtBreakfastQty.Attributes("style") = "height: 21px;"
        txtLunchQty.Attributes("style") = "height: 21px;"
        txtDinnerQty.Attributes("style") = "height: 21px;"
        txtOtherMealQty.Attributes("style") = "height: 21px;"
        txtHotelQty.Attributes("style") = "height: 21px;"
        txtBreakfastUnit.Attributes("style") = "height: 21px;"
        txtLunchUnit.Attributes("style") = "height: 21px;"
        txtDinnerUnit.Attributes("style") = "height: 21px;"
        txtOtherMealUnit.Attributes("style") = "height: 21px;"
        txtHotelUnit.Attributes("style") = "height: 21px;"
    End Sub

    'Private Sub ClearScheduleForm()
    '    hScheduleID.Value = ""
    '    Dim dtDefault As DateTime = If(dteDepartureDate.Value Is Nothing, DateTime.Now, dteDepartureDate.Date)
    '    dteScheduleDate.Date = New DateTime(dtDefault.Year, dtDefault.Month, dtDefault.Day)
    '    txeFromTime.DateTime = New DateTime(dtDefault.Year, dtDefault.Month, dtDefault.Day, 8, 0, 0)
    '    txeToTime.DateTime = New DateTime(dtDefault.Year, dtDefault.Month, dtDefault.Day, 16, 45, 0)
    '    txtWorkingArea.Text = ""
    '    txtTask.Text = ""
    '    spiEstimateTransportationFee.Value = Nothing
    '    'chkAirTicket.Checked = False
    '    'chkTrainTicket.Checked = False
    '    'chkCar.Checked = False
    'End Sub

    Private Sub ClearAttachmentForm(Optional ByVal withDesc As Boolean = True)
        panOthersAttachments.Controls.Clear()
        panRegisterAttachments.Controls.Clear()
        panScheduleAttachments.Controls.Clear()
        If withDesc Then
            txtDescription.Text = ""
        End If
    End Sub

    Private Function GetObject() As tblBTRegisterInfo
        'Dim objUser As tbl_UsersInfo = UserProvider.tbl_User_GetUserInfo_ByUserName(txtEmployeeCode.Text.Trim())
        Dim obj As New tblBTRegisterInfo()
        obj.BTRegisterID = CommonFunction._ToInt(hID.Value)
        obj.EmployeeCode = txtEmployeeCode.Text.Trim()
        obj.EmployeeName = txtFullName.Text.Trim()
        obj.BTType = ddlBTType.SelectedValue
        obj.Location = txtLocation.Text.Trim()
        obj.LocationID = CommonFunction._ToInt(hLocationID.Value)
        obj.Division = txtDivision.Text.Trim()
        obj.DivisionID = CommonFunction._ToInt(hDivisionID.Value)
        obj.Department = txtDepartment.Text.Trim()
        obj.DepartmentID = CommonFunction._ToInt(hDepartmentID.Value)
        obj.Section = txtSection.Text.Trim()
        obj.SectionID = CommonFunction._ToInt(hSectionID.Value)
        obj.Group = hGroup.Value
        obj.GroupID = CommonFunction._ToInt(hGroupID.Value)
        obj.Position = txtPosition.Text.Trim()
        obj.BudgetName = ddlBudgetName.SelectedValue
        obj.BudgetCode = txtBudgetCode.Text.Trim()
        obj.BudgetCodeID = CommonFunction._ToInt(ddlBudgetName.SelectedValue.Split("-")(0))
        obj.IsSaved = True
        obj.IsSubmited = False
        obj.Currency = If(_isDomestic, "vnd", "usd") 'ddlCurrency.SelectedValue
        obj.PaymentType = If(chkCredit.Checked, "CC", "NC")
        obj.IsFirstTimeOverSea = chkFirstTimeOversea.Checked
        'obj.FirstTimeOverSeaVND = CommonFunction._ToMoney(spiFirstTimeOverseaVND.Text.Trim())
        obj.ModifiedBy = _username
        obj.CreatedBy = _username
        'obj.RequestDate = dteRequestDate.Date
        obj.Mobile = txtMobile.Text.Trim()
        obj.Email = txtEmail.Text.Trim()
        '
        obj.DepartureDate = dteDepartureDate.Date
        obj.ReturnDate = dteReturnDate.Date
        obj.CountryCode = ddlDestinationCountry.SelectedValue
        obj.Purpose = txtPurpose.Text.Trim()
        obj.IsMovingTimeAllowance = chkMovingTimeAllowance.Checked
        'obj.MovingTimeAllowanceVND = CommonFunction._ToMoney(spiMovingTimeAllowanceVND.Text.Trim())
        '
        obj.SubmitComment = txtSubmitComment.Text.Trim()
        obj.ProjectBudgetCode = txtProjectBudgetCode.Text.Trim()
        obj.CheckAllBudget = chkBudgetAll.Checked
        '
        obj.AirTicket = chkRequestAirTicket.Checked
        obj.TrainTicket = chkRequestTrainTicket.Checked
        obj.Car = chkRequestCar.Checked
        obj.Wifi = False 'chkRequestWifi.Checked
        If obj.AirTicket Then
            obj.ExpectedDepartureTime = dteExpectedDepartureTime.Date
            obj.ExpectedDepartureFlightNo = txtExpectedDepartureFlightNo.Text.Trim()
            obj.ExpectedReturnTime = dteExpectedReturnTime.Date
            obj.ExpectedReturnFlightNo = txtExpectedReturnFlightNo.Text.Trim()
        Else
            obj.ExpectedDepartureTime = DateTime.MinValue
            obj.ExpectedDepartureFlightNo = Nothing
            obj.ExpectedReturnTime = DateTime.MinValue
            obj.ExpectedReturnFlightNo = Nothing
        End If
        '
        obj.NoRequestAdvance = chkNoRequestAdvance.Checked
        If obj.NoRequestAdvance Then
            obj.DestinationID = CommonFunction._ToInt(ddlRequestDestination.SelectedValue)
        Else
            obj.DestinationID = -1
        End If
        '
        Return obj
    End Function

    Private Function GetRequestObject() As tblBTRegisterRequestInfo
        Dim obj As New tblBTRegisterRequestInfo()
        obj.ID = CommonFunction._ToInt(hRequestID.Value)
        obj.BTRegisterID = CommonFunction._ToInt(hID.Value)
        obj.CreatedBy = _username
        obj.FromDate = dteFromDate.Date
        obj.ModifiedBy = _username
        'obj.Remark = txaRemark.Text.Trim()
        obj.ToDate = dteToDate.Date
        obj.DestinationID = CommonFunction._ToInt(ddlDestinationLocation.SelectedValue)
        Return obj
    End Function

    Private Function GetRequestDetailsObject() As tblBTRegisterRequestDetailsInfo
        Dim obj As New tblBTRegisterRequestDetailsInfo()
        obj.BTRegisterID = CommonFunction._ToInt(hID.Value)
        obj.BreakfastQty = CommonFunction._ToUnsignInt(txtBreakfastQty.Text.Trim())
        obj.BreakfastUnit = CommonFunction._ToMoney(txtBreakfastUnit.Text.Trim())
        obj.DinnerQty = CommonFunction._ToUnsignInt(txtDinnerQty.Text.Trim())
        obj.DinnerUnit = CommonFunction._ToMoney(txtDinnerUnit.Text.Trim())
        obj.LunchQty = CommonFunction._ToUnsignInt(txtLunchQty.Text.Trim())
        obj.LunchUnit = CommonFunction._ToMoney(txtLunchUnit.Text.Trim())
        obj.OtherMealQty = CommonFunction._ToUnsignInt(txtOtherMealQty.Text.Trim())
        obj.OtherMealUnit = CommonFunction._ToMoney(txtOtherMealUnit.Text.Trim())
        obj.Other = txtOther.Text.Trim()
        'obj.OtherQty = 1
        obj.OtherUnit = CommonFunction._ToMoney(txtOtherAmount.Text.Trim())
        obj.HotelQty = CommonFunction._ToUnsignInt(txtHotelQty.Text.Trim())
        obj.HotelUnit = CommonFunction._ToMoney(txtHotelUnit.Text.Trim())
        obj.TotalAmount = CommonFunction._ToMoney(txtTotalAmount.Text.Trim())
        If CommonFunction._ToMoney(txtOtherAmount.Text.Trim()) = 0 Then
            obj.Other = ""
            txtOther.Text = ""
        End If
        Return obj
    End Function

    Private Function GetScheduleObject() As tblBTRegisterScheduleInfo
        Dim obj As New tblBTRegisterScheduleInfo()
        obj.ID = CommonFunction._ToInt(hScheduleID.Value)
        obj.BTRegisterID = CommonFunction._ToInt(hID.Value)
        obj.FromTime = New DateTime(dteScheduleDate.Date.Year, dteScheduleDate.Date.Month, dteScheduleDate.Date.Day, txeFromTime.DateTime.Hour, txeFromTime.DateTime.Minute, txeFromTime.DateTime.Second)
        obj.ToTime = New DateTime(dteScheduleDate.Date.Year, dteScheduleDate.Date.Month, dteScheduleDate.Date.Day, txeToTime.DateTime.Hour, txeToTime.DateTime.Minute, txeToTime.DateTime.Second)
        obj.WorkingArea = txtWorkingArea.Text.Trim()
        obj.Task = txtTask.Text.Trim()
        obj.EstimateTransportationFee = CommonFunction._ToMoney(spiEstimateTransportationFee.Text().Trim())
        obj.CreatedBy = _username
        obj.ModifiedBy = _username
        obj.AirTicket = False 'chkAirTicket.Checked
        obj.TrainTicket = False 'chkTrainTicket.Checked
        obj.Car = False 'chkCar.Checked
        Return obj
    End Function

    Private Function GetWifiObject() As tblBTWifiDeviceInfo
        Dim obj As New tblBTWifiDeviceInfo()
        obj.ID = CommonFunction._ToInt(hWifiDeviceID.Value)
        obj.BTRegisterID = CommonFunction._ToInt(hID.Value)
        obj.FromDate = dtWifiDeviceFromDate.Date
        obj.ToDate = dtWifiDeviceToDate.Date
        obj.CountryCode = CommonFunction._ToString(ddlWifiDeviceCountry.SelectedValue)
        obj.EmployeeCode = txtEmployeeCode.Text.Trim()
        obj.EmployeeName = txtFullName.Text.Trim()
        obj.EmployeeDepartment = txtDepartment.Text.Trim()
        obj.EmployeeDivision = txtDivision.Text.Trim()
        obj.CreatedBy = _username
        obj.UpdatedBy = _username
        obj.Status = ""
        Return obj
    End Function

    Private Sub UploadAttachments()
        'Dim attachmentFiles As HttpFileCollection = Request.Files()
        'For i As Integer = 0 To attachmentFiles.Count - 1
        '    Dim attFile As HttpPostedFile = attachmentFiles(i)
        '    If attFile.FileName IsNot Nothing AndAlso attFile.FileName.Length > 0 Then
        '        Dim path As String = String.Format("/Attachments/{0}", _username)
        '        If Not Directory.Exists(Server.MapPath(path)) Then
        '            Directory.CreateDirectory(Server.MapPath(path))
        '        End If
        '        path = String.Format("{0}/{1}_{2}_{3}", path, _username, DateTime.Now.ToString("yyMMddHHmmssfffff"), attFile.FileName)
        '        attFile.SaveAs(Server.MapPath(path))
        '        Dim obj As New tblBTRegisterAttachmentInfo()
        '        obj.BTRegisterID = CommonFunction._ToInt(hID.Value)
        '        obj.CreatedBy = _username
        '        obj.AttachmentPath = path
        '        Select Case attachmentFiles.Keys(i)
        '            Case "fRegister"
        '                obj.AttachmentType = "register"
        '                BusinessTripProvider.BTRAttachment_DeleteByType(CommonFunction._ToInt(hID.Value), "register")
        '            Case "fSchedule"
        '                obj.AttachmentType = "schedule"
        '                BusinessTripProvider.BTRAttachment_DeleteByType(CommonFunction._ToInt(hID.Value), "schedule")
        '            Case "fOthers"
        '                obj.AttachmentType = "other"
        '        End Select
        '        BusinessTripProvider.BTRAttachment_Insert(obj)
        '    End If
        'Next
        BusinessTripProvider.BTRAttachment_UpdateDesc(CommonFunction._ToInt(hID.Value), txtDescription.Text.Trim())
    End Sub

    Protected Sub btnSearch_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            SetPreSearchCondition()
            Dim postBackUrl As New StringBuilder()
            postBackUrl.Append("~/BTAdvanceDeclaration.aspx")
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
        LoadBTRegisterHistory()
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

    Protected Sub grvBTCancelled_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTCancelled.BeforeGetCallbackResult
        GetDataTable()
        LoadCancelled()
    End Sub

    Protected Sub grvBTRequest_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTRequest.BeforeGetCallbackResult
        LoadBTRegisterRequest()
    End Sub

    Protected Sub grvBTSchedule_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTSchedule.BeforeGetCallbackResult
        LoadBTRegisterSchedule()
    End Sub

    Protected Sub grvWifiDevice_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvWifiDevice.BeforeGetCallbackResult
        LoadWifiDevice()
    End Sub

    Protected Sub btnFinish_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnFinish.Click
        CommonFunction.SetPostBackStatus(btnFinish)
        Try
            Dim obj As tblBTRegisterInfo = GetObject()
            UploadAttachments()
            'hID.Value = ""
            BusinessTripProvider.BTRegister_Update(obj)
            '
            LoadBTRegisterInfo()
            '
            'LoadBTRegisterRequest()
            'LoadBTAirTicket()
            'LoadBTRegisterSchedule()
            LoadBTRegisterAttachment(True)
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
            Dim isFirstTimeSubmit As Boolean = (lnkFINStatus.InnerText.ToLower() = "prepared")
            Dim obj As tblBTRegisterInfo = GetObject()
            '
            obj.IsSubmited = True
            UploadAttachments()
            'hID.Value = ""
            BusinessTripProvider.BTRegister_Update(obj)
            '
            LoadBTRegisterInfo()
            '
            LoadBTRegisterRequest()
            'LoadBTAirTicket()
            LoadBTRegisterSchedule()
            LoadWifiDevice()
            LoadBTRegisterAttachment(True)
            'Send notice email        
            If SendSubmitEmail() Then
                SendUserEmail(isFirstTimeSubmit)
                'If isFirstTimeSubmit AndAlso Not _isDomestic AndAlso txtEmployeeCode.Text.IndexOf("11") = 0 Then
                '    SendITEmail()
                'End If
                'Sent execute's oversea trip to support
                'If Not _isDomestic AndAlso txtEmployeeCode.Text.IndexOf("11") = 0 Then
                '    SendITEmail()
                'End If
                CommonFunction.ShowInfoMessage(panMessage, "Your data is submited successfully!")
            Else
                CommonFunction.ShowInfoMessage(panMessage, "Your data is submited successfully but fail to send notice emails! Please contact with administrator!")
            End If
            'Send other budget notification
            If chkBudgetAll.Checked Then
                SendNoticeBudgetEmail()
            End If
            '                        
            _objEmail.Dispose()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnAdd_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        CommonFunction.SetPostBackStatus(btnAdd)
        Try
            'hID.Value = BusinessTripProvider.BTRegister_InsertTemp(_username)
            InitBTStatus()
            EnableInfoForm(False)
            ShowInfoRows(True)
            spanExportBT.Visible = True
            'pre init
            txtEmployeeCode.Text = txtSelectEmployeeCode.Text.Split("-")(0).Trim() 'txtSelectEmployeeCode.Text
            CommonFunction.SetCBOValue(ddlBTType, ddlSelectBTType.SelectedValue)
            chkBudgetAll.Checked = chkSelectBudgetAll.Checked
            InitSearchBudgetName(False, chkBudgetAll.Checked)
            CommonFunction.SetCBOValue(ddlBudgetName, ddlSelectBudgetName.SelectedValue)
            txtBudgetCode.Text = If(ddlBudgetName.SelectedValue <> "", ddlBudgetName.SelectedValue.Split("-")(1), "")
            txtProjectBudgetCode.Text = txtSelectProjectBudgetCode.Text
            CommonFunction.SetCBOValue(ddlDestinationCountry, ddlSelectDestinationCountry.SelectedValue)
            '
            _isDomestic = ddlBTType.SelectedValue.IndexOf("domestic") >= 0
            '
            'InitCountry()
            InitDestination()
            '          
            Dim budgedCode As Integer = CommonFunction._ToInt(ddlBudgetName.SelectedValue.Split("-")(0))
            'txtBudgetRemain.Text = If(budgedCode = -1, "", String.Format("{0:#,0.##} VND", BusinessTripProvider.BTRegister_GetBudgetRemaining(budgedCode, CommonFunction._ToInt(hID.Value))))
            '
            LoadInfo(False)
            '
            dteDepartureDate.Date = New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0)
            dteReturnDate.Date = New DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day, 16, 45, 0)
            If _isDomestic Then
                CommonFunction.SetCBOValue(ddlCurrency, "vnd")
                'ddlCurrency.Enabled = False
                spanFirstTimeOversea.Visible = False
                CommonFunction.SetCBOValue(ddlDestinationCountry, "VN")
                'ddlDestinationCountry.Enabled = False
                InitDestination()
                '
                'chkRequestWifi.Visible = False
                'lblRequestWifi.Visible = False
                'panWifiNote.Visible = False
            Else
                CommonFunction.SetCBOValue(ddlCurrency, "usd")
                spanFirstTimeOversea.Visible = True
                '
                'If txtEmployeeCode.Text.IndexOf("11") = 0 OrElse Array.IndexOf(_wifi, txtPosition.Text.Trim().ToLower()) >= 0 Then
                '    chkRequestWifi.Visible = True
                '    lblRequestWifi.Visible = True
                '    panWifiNote.Visible = True
                'Else
                '    chkRequestWifi.Visible = False
                '    lblRequestWifi.Visible = False
                '    panWifiNote.Visible = False
                'End If
            End If
            lblTransportationAndOtherCurrency.Text = If(ddlCurrency.SelectedItem IsNot Nothing, ddlCurrency.SelectedItem.Text, ddlCurrency.SelectedValue.ToUpper())
            chkFirstTimeOversea.Checked = False
            'chkRequestWifi.Checked = False
            'chkFirstTimeChange()                        
            Dim firstTimeAmount As Decimal = CommonFunction._ToMoney(LookupProvider.GetByCodeAndValue("PRE_PARAMS", "FIRST_TIME_OVERSEA"))
            spanFirstAmount.InnerText = String.Format("({0:#,0.##} USD)", firstTimeAmount)
            hFirstTime.Value = firstTimeAmount.ToString()
            hOtherAmount.Value = "0"
            '
            chkMovingTimeAllowance.Checked = False
            'chkMovingTimeChange()
            Dim movingTimeAmount As Decimal = 0
            Dim movingTimeCurrency As String = If(_isDomestic, "VND", "USD")
            Dim dtPolicy As DataTable = mMovingTimeAllowanceProvider.m_Expense_GetPolicy(ddlDestinationCountry.SelectedValue)
            If dtPolicy.Rows.Count > 0 Then
                Dim drPolicy As DataRow = dtPolicy.Rows(0)
                movingTimeAmount = CommonFunction._ToMoney(drPolicy("Amount"))
                movingTimeCurrency = CommonFunction._ToString(drPolicy("Currency"))
            End If
            spanMovingTimeAmount.InnerText = String.Format("({0:#,0.##} {1})", movingTimeAmount, movingTimeCurrency)
            '
            ddlBTType.Enabled = False
            '
            'hIsDGMAndAbove.Value = If(Array.IndexOf(_dgmAndAbove, txtPosition.Text.Trim().ToLower()) >= 0, "Y", "N")
            hIsGMAndAbove.Value = If(Array.IndexOf(_gmAndAbove, txtPosition.Text.Trim().ToLower()) >= 0, "Y", "N")
            hMovingTime.Value = movingTimeAmount.ToString()
            '            
            Dim obj As tblBTRegisterInfo = GetObject()
            Dim dtResult As DataTable = BusinessTripProvider.BTRegister_Insert(obj)
            hID.Value = dtResult.Rows(0)("ID")
            txtBusinessTripNo.Text = dtResult.Rows(0)("BTNo")
            '
            LoadExpenseNorm()
            '         
            btnCancel.Attributes("enable-form") = If(_enable, "true", "false")
            btnCancel.Attributes("enable-air-form") = If(_airEnable, "true", "false")
            EnableForm(True)
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteMany_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        CommonFunction.SetPostBackStatus(btnDelete)
        Try
            Dim deleteUsers As String = hDeleteUsers.Value
            If deleteUsers.Trim().Length > 0 Then
                BusinessTripProvider.BTRegister_Delete(deleteUsers)
                LoadDataList()
            End If
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnSaveRequest_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveRequest.Click
        CommonFunction.SetPostBackStatus(btnSaveRequest)
        Try
            dteFromDate.Attributes("style") = "float: left;"
            dteToDate.Attributes("style") = "float: left;"
            txtBreakfastQty.Attributes("style") = "height: 21px;"
            txtLunchQty.Attributes("style") = "height: 21px;"
            txtDinnerQty.Attributes("style") = "height: 21px;"
            txtOtherMealQty.Attributes("style") = "height: 21px;"
            txtHotelQty.Attributes("style") = "height: 21px;"
            txtBreakfastUnit.Attributes("style") = "height: 21px;"
            txtLunchUnit.Attributes("style") = "height: 21px;"
            txtDinnerUnit.Attributes("style") = "height: 21px;"
            txtOtherMealUnit.Attributes("style") = "height: 21px;"
            txtHotelUnit.Attributes("style") = "height: 21px;"
            '
            Dim requestObj As tblBTRegisterRequestInfo = GetRequestObject()
            If Not IsValidCommon(requestObj) Then
                CommonFunction.SetProcessStatus(btnSaveRequest, False)
                Return
            End If
            Dim requestDetailsObj As tblBTRegisterRequestDetailsInfo = GetRequestDetailsObject()
            If Not IsValidCommonPolicy(requestDetailsObj) Then
                CommonFunction.SetProcessStatus(btnSaveRequest, False)
                Return
            End If
            If requestObj.ID > 0 Then
                BusinessTripProvider.BTRegisterRequest_Update(requestObj, requestDetailsObj)
            Else
                BusinessTripProvider.BTRegisterRequest_Insert(requestObj, requestDetailsObj)
            End If
            CommonFunction.SetProcessStatus(btnSaveRequest, True)
            CommonFunction.ShowInfoMessage(panMessage, "Your data is saved successfully!")
            LoadBTRegisterRequest()
            LoadTotalSummary()
        Catch ex As Exception
            CommonFunction.SetProcessStatus(btnSaveRequest, False)
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Function IsValidCommon(ByVal obj As tblBTRegisterRequestInfo) As Boolean
        Dim isValid As Boolean = True

        If obj.ToDate <= obj.FromDate Then
            CommonFunction.ShowErrorMessage(panMessage, "To date must be greater than from date!")
            dteFromDate.Attributes("style") = "border-color: red; float: left;"
            dteToDate.Attributes("style") = "border-color: red; float: left;"
            isValid = False
        ElseIf BusinessTripProvider.BTRegisterRequest_IsBTTimeConflict(obj.BTRegisterID, obj.ID, obj.FromDate, obj.ToDate) Then
            CommonFunction.ShowErrorMessage(panMessage, "BT time conflict!")
            dteFromDate.Attributes("style") = "border-color: red; float: left;"
            dteToDate.Attributes("style") = "border-color: red; float: left;"
            isValid = False
        End If

        Return isValid
    End Function

    Private Function IsValidCommonPolicy(ByVal objDetails As tblBTRegisterRequestDetailsInfo) As Boolean
        Dim isValid As Boolean = True
        Dim fromDate As DateTime = dteFromDate.Date
        Dim toDate As DateTime = dteToDate.Date
        Dim dayCount As Integer = New Date(toDate.Year, toDate.Month, toDate.Day).Subtract(New Date(fromDate.Year, fromDate.Month, fromDate.Day)).TotalDays + 1
        If objDetails.BreakfastQty > dayCount Then
            txtBreakfastQty.Attributes("style") = "border: 1px solid red; height: 21px;"
            isValid = False
        End If
        If objDetails.LunchQty > dayCount Then
            txtLunchQty.Attributes("style") = "border: 1px solid red; height: 21px;"
            isValid = False
        End If
        If objDetails.DinnerQty > dayCount Then
            txtDinnerQty.Attributes("style") = "border: 1px solid red; height: 21px;"
            isValid = False
        End If
        If objDetails.OtherMealQty > dayCount Then
            txtOtherMealQty.Attributes("style") = "border: 1px solid red; height: 21px;"
            isValid = False
        End If
        If objDetails.HotelQty > dayCount Then
            txtHotelQty.Attributes("style") = "border: 1px solid red; height: 21px;"
            isValid = False
        End If
        If Not isValid Then
            CommonFunction.ShowErrorMessage(panMessage, "Request times is invalid!")
            'Else
            '    Dim dtNorm As DataTable = mExpenseProvider.m_Expense_GetNorm(txtEmployeeCode.Text.Trim(), CommonFunction._ToInt(ddlDestinationLocation.SelectedValue), ddlCurrency.SelectedValue)
            '    If dtNorm.Rows.Count > 0 Then
            '        Dim drNorm As DataRow = dtNorm.Rows(0)
            '        Dim BreakfastNorm As Decimal = GetSpiMaxValue(CommonFunction._ToMoney(drNorm("Breakfast")))
            '        Dim LunchNorm As Decimal = GetSpiMaxValue(CommonFunction._ToMoney(drNorm("Lunch")))
            '        Dim DinnerNorm As Decimal = GetSpiMaxValue(CommonFunction._ToMoney(drNorm("Dinner")))
            '        Dim OtherMealNorm As Decimal = GetSpiMaxValue(CommonFunction._ToMoney(drNorm("OtherMeal")))
            '        Dim HotelNorm As Decimal = GetSpiMaxValue(CommonFunction._ToMoney(drNorm("Hotel")))
            '        Dim OtherNorm As Decimal = GetSpiMaxValue(CommonFunction._ToMoney(drNorm("Other")))
            '        If objDetails.BreakfastUnit > BreakfastNorm Then
            '            txtBreakfastUnit.Attributes("style") = "border: 1px solid red; height: 21px;"
            '            isValid = False
            '        Else
            '            txtBreakfastUnit.Attributes("style") = "height: 21px;"
            '        End If
            '        If objDetails.LunchUnit > LunchNorm Then
            '            txtLunchUnit.Attributes("style") = "border: 1px solid red; height: 21px;"
            '            isValid = False
            '        End If
            '        If objDetails.DinnerUnit > DinnerNorm Then
            '            txtDinnerUnit.Attributes("style") = "border: 1px solid red; height: 21px;"
            '            isValid = False
            '        End If
            '        If objDetails.OtherMealUnit > OtherMealNorm Then
            '            txtOtherMealUnit.Attributes("style") = "border: 1px solid red; height: 21px;"
            '            isValid = False
            '        End If
            '        If objDetails.HotelUnit > HotelNorm Then
            '            If _isDomestic Then
            '                If Array.IndexOf(_gmAndAbove, txtPosition.Text.Trim().ToLower()) < 0 Then
            '                    txtHotelUnit.Attributes("style") = "border: 1px solid red; height: 21px;"
            '                    isValid = False
            '                End If
            '            End If
            '        End If
            '        If Not isValid Then
            '            CommonFunction.ShowErrorMessage(panMessage, "Request units is over policy!")
            '        End If
            '    End If
        End If
        Return isValid
    End Function

    'Protected Sub btnCancelRequest_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelRequest.Click
    '    Dim btn As Button = CType(sender, Button)
    '    CommonFunction.SetPostBackStatus(btn)
    '    Try
    '        ClearRequestForm()
    '    Catch ex As Exception
    '        CommonFunction.ShowErrorMessage(panMessage, ex.Message)
    '    End Try
    'End Sub

    Protected Sub btnSaveSchedule_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveSchedule.Click
        CommonFunction.SetPostBackStatus(btnSaveSchedule)
        Try
            Dim obj As tblBTRegisterScheduleInfo = GetScheduleObject()
            If Not IsValidSchedule(obj) Then
                CommonFunction.SetProcessStatus(btnSaveSchedule, False)
                Return
            End If
            '
            If obj.ID > 0 Then
                BusinessTripProvider.BTRegisterSchedule_Update(obj)
            Else
                BusinessTripProvider.BTRegisterSchedule_Insert(obj)
            End If
            CommonFunction.SetProcessStatus(btnSaveSchedule, True)
            CommonFunction.ShowInfoMessage(panMessage, "Your data is saved successfully!")
            LoadBTRegisterSchedule()
        Catch ex As Exception
            CommonFunction.SetProcessStatus(btnSaveSchedule, False)
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnSaveWifiDevice_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveWifiDevice.Click
        CommonFunction.SetPostBackStatus(btnSaveWifiDevice)
        Try
            Dim obj As tblBTWifiDeviceInfo = GetWifiObject()
            If Not IsValidWifiDevice(obj) Then
                CommonFunction.SetProcessStatus(btnSaveWifiDevice, False)
                Return
            End If
            '
            If obj.ID > 0 Then
                WifiDeviceProvider.WifiDevice_Update(obj)
            Else
                WifiDeviceProvider.WifiDevice_Insert(obj)
            End If
            CommonFunction.SetProcessStatus(btnSaveWifiDevice, True)
            CommonFunction.ShowInfoMessage(panMessage, "Your data is saved successfully!")
            LoadWifiDevice()
        Catch ex As Exception
            CommonFunction.SetProcessStatus(btnSaveWifiDevice, False)
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Function IsValidSchedule(ByVal obj As tblBTRegisterScheduleInfo) As Boolean
        Dim isValid As Boolean = True
        txeFromTime.Attributes("style") = "width: 100px !important; display: inline-block;"
        txeToTime.Attributes("style") = "width: 100px !important; display: inline-block;"
        '
        If obj.ToTime <= obj.FromTime Then
            CommonFunction.ShowErrorMessage(panMessage, "To time must be greater than from time!")
            txeFromTime.Attributes("style") = "border-color: red; width: 100px !important; display: inline-block;"
            txeToTime.Attributes("style") = "border-color: red; width: 100px !important; display: inline-block;"
            isValid = False
        End If

        Return isValid
    End Function

    Private Function IsValidWifiDevice(ByVal obj As tblBTWifiDeviceInfo) As Boolean
        Dim isValid As Boolean = True
        dtWifiDeviceFromDate.Attributes("style") = ""
        dtWifiDeviceToDate.Attributes("style") = ""
        '
        If obj.ToDate <= obj.FromDate Then
            CommonFunction.ShowErrorMessage(panMessage, "To Date must be greater than From Date!")
            dtWifiDeviceFromDate.Attributes("style") = "border-color: red"
            dtWifiDeviceToDate.Attributes("style") = "border-color: red"
            isValid = False
        ElseIf (Not WifiDeviceProvider.WifiDevice_CheckDate(obj)) Then
            CommonFunction.ShowErrorMessage(panMessage, "Request date is conflict!")
            dtWifiDeviceFromDate.Attributes("style") = "border-color: red"
            dtWifiDeviceToDate.Attributes("style") = "border-color: red"
            isValid = False
        End If

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

    Protected Sub btnEdit_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnAdd)
        Try
            'hID.Value = btn.Attributes("data-id")
            'LoadDataList()
            LoadBTRegisterInfo(False)
            '
            LoadBTRegisterRequest()
            LoadBTAirTicket()
            LoadBTRegisterSchedule()
            LoadWifiDevice()
            LoadBTRegisterAttachment(True)
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

    Protected Sub btnDelete_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnAdd)
        Try
            Dim btID As Integer = CommonFunction._ToInt(hID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            BusinessTripProvider.BTRegister_Delete(btID.ToString())
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
            LoadDataList()
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
                postBackUrl.Append("~/BTAdvanceDeclaration.aspx")
                postBackUrl.Append(String.Format( _
                    "?btno={0}&bttype={1}&fdate={2}&tdate={3}&ecode={4}&ename={5}&obudget={6}&bg={7}", _
                    hsBTNo.Value, hsBTType.Value, hsDepartureFrom.Value, hsDepartureTo.Value, _
                    hsEmployeeCode.Value, hsFullName.Value, hsBudgetAll.Value, hsBudgetName.Value))
                Response.Redirect(postBackUrl.ToString())
            End If
            '
            'BusinessTripProvider.BTRegister_DeleteDraftByID(CommonFunction._ToInt(hID.Value))
            'ClearBTForm()
            'EnableInfoForm(True)
            'ShowInfoRows(False)
            'LoadBTRegisterRequest(True)
            'ClearRequestForm()
            'LoadBTRegisterSchedule(True)
            ''ClearScheduleForm()
            'ClearAttachmentForm()
            'LoadBTAirTicket(True)
            'ClearAirTicketForm()
            ''
            'txtBusinessTripNo.Text = hsBTNo.Value
            'CommonFunction.SetCBOValue(ddlBTType, hsBTType.Value)
            'txtEmployeeCode.Text = hsEmployeeCode.Value
            'txtFullName.Text = hsFullName.Value
            'chkBudgetAll.Checked = hsBudgetAll.Value = "Y"
            'InitSearchBudgetName(True, chkBudgetAll.Checked)
            'CommonFunction.SetCBOValue(ddlBudgetName, hsBudgetName.Value)
            'ddlBudgetName.Enabled = True
            'chkBudgetAll.Enabled = True
            'txtBudgetCode.Text = If(ddlBudgetName.SelectedValue <> "", ddlBudgetName.SelectedValue.Split("-")(1), "")
            'dteDepartureFrom.Text = hsDepartureFrom.Value
            'dteDepartureTo.Text = hsDepartureTo.Value
            'btnSearch_OnClick(Nothing, Nothing)
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnRecall_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnRecall.Click
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            Dim btID As Integer = CommonFunction._ToInt(hID.Value)
            Dim message As String = BusinessTripProvider.tbl_BT_Register_Recall(btID, _username)
            If message.Length > 0 Then
                CommonFunction.ShowErrorMessage(panMessage, message)
            Else
                Session("RecallMessage") = "Your BT Approval was recalled successfully!"
                Dim postBackUrl As New StringBuilder()
                postBackUrl.Append("~/BTAdvanceDeclaration.aspx")
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

    Protected Sub btnEditRequest_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnAdd)
        Try
            hRequestID.Value = btn.Attributes("data-id")
            LoadBTRegisterRequestByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteRequest_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnAdd)
        Try
            Dim id As Integer = CommonFunction._ToInt(btn.Attributes("data-id"))
            BusinessTripProvider.BTRegisterRequest_Delete(id.ToString())
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
            LoadBTRegisterRequest()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditSchedule_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnAdd)
        Try
            hScheduleID.Value = btn.Attributes("data-id")
            LoadBTRegisterScheduleByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditWifiDevice_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnAdd)
        Try
            hWifiDeviceID.Value = btn.Attributes("data-id")
            LoadWifiDeviceByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteSchedule_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnAdd)
        Try
            Dim id As Integer = CommonFunction._ToInt(btn.Attributes("data-id"))
            BusinessTripProvider.BTRegisterSchedule_Delete(id.ToString())
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
            LoadBTRegisterSchedule()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteWifiDevice_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnAdd)
        Try
            Dim id As Integer = CommonFunction._ToInt(btn.Attributes("data-id"))
            WifiDeviceProvider.WifiDevice_Delete(id.ToString())
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
            LoadWifiDevice()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    'Protected Sub ddlCurrency_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlCurrency.SelectedIndexChanged
    '    CommonFunction.SetPostBackStatus(ddlCurrency)
    '    Try
    '        LoadExpenseNorm()
    '        chkFirstTimeChange()
    '        chkMovingTimeChange()
    '    Catch ex As Exception
    '        CommonFunction.ShowErrorMessage(panMessage, ex.Message)
    '    End Try
    'End Sub

    Protected Sub ddlSelectBTType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlSelectBTType.SelectedIndexChanged
        CommonFunction.SetPostBackStatus(ddlSelectBTType)
        Try
            _isDomestic = ddlSelectBTType.SelectedValue.IndexOf("domestic") >= 0
            '
            InitSelectBudgetName(chkSelectBudgetAll.Checked)
            txtSelectBudgetCode.Text = If(ddlSelectBudgetName.SelectedValue <> "", ddlSelectBudgetName.SelectedValue.Split("-")(1), "")
            '
            InitCountry(True)
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub chkBudgetAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkBudgetAll.CheckedChanged
        CommonFunction.SetPostBackStatus(btnCancel)
        InitSearchBudgetName((hID.Value.Trim().Length = 0 OrElse Not (_enable OrElse _enableBudget OrElse btnShowConfirmBudget.Visible = True)), chkBudgetAll.Checked)
        txtBudgetCode.Text = If(ddlBudgetName.SelectedValue <> "", ddlBudgetName.SelectedValue.Split("-")(1), "")
        Dim budgedCode As Integer = CommonFunction._ToInt(ddlBudgetName.SelectedValue.Split("-")(0))
        'txtBudgetRemain.Text = If(budgedCode = -1, "", String.Format("{0:#,0.##} VND", BusinessTripProvider.BTRegister_GetBudgetRemaining(budgedCode, CommonFunction._ToInt(hID.Value))))
        '
        'LoadDataList()
    End Sub

    Protected Sub chkSelectBudgetAll_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkSelectBudgetAll.CheckedChanged
        CommonFunction.SetPostBackStatus(btnCancel)
        If hSelectBudgetByEmp.Value = "Y" Then
            hSelectBudgetByEmp.Value = "N"
            chkSelectBudgetAll.Checked = Not chkSelectBudgetAll.Checked
        End If
        InitSelectBudgetName(chkSelectBudgetAll.Checked)
        txtSelectBudgetCode.Text = If(ddlSelectBudgetName.SelectedValue <> "", ddlSelectBudgetName.SelectedValue.Split("-")(1), "")
    End Sub

    Private Sub LoadBTAirTicket(Optional ByVal clear As Boolean = False)
        Dim dtData As DataTable = If(clear, Nothing, AirTicketProvider.BTAirTicket_Search(CommonFunction._ToInt(hID.Value)))
        CommonFunction.LoadDataToGrid(grvBTAirTicket, dtData, "", Nothing, "AirEnable", _airEnable)
    End Sub

    Private Sub LoadBTAirTicketByID()
        Dim id As Integer = CommonFunction._ToInt(hAirTicketID.Value)
        Dim dtData As DataTable = AirTicketProvider.BTAirTicket_GetByID(CommonFunction._ToInt(id))
        If dtData.Rows.Count > 0 Then
            Dim drData As DataRow = dtData.Rows(0)
            Dim oraStatus As Boolean = CommonFunction._ToBoolean(drData("EnableForm"))
            InitAirPeriod()
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
        dteAirDate.Date = If(dteDepartureDate.Value Is Nothing, DateTime.Now, dteDepartureDate.Date)
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
        InitAirPeriod()
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
                If lnkFINStatus.InnerText.ToLower() = FIStatus.completed.ToString() Then
                    AirTicketProvider.BTAirTicket_Insert(obj)
                Else
                    CommonFunction.SetProcessStatus(btnSaveAirTicket, False)
                    CommonFunction.ShowErrorMessage(panMessage, "This BT Request has not completed!")
                    Return
                End If
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

    Private Sub LoadExpenseNorm()
        Dim dtNorm As DataTable = mExpenseProvider.m_Expense_GetNorm(CommonFunction._ToInt(hID.Value), dteDepartureDate.Date) 'txtEmployeeCode.Text.Trim(), CommonFunction._ToInt(ddlDestinationLocation.SelectedValue), ddlCurrency.SelectedValue
        If dtNorm.Rows.Count > 0 Then
            Dim drNorm As DataRow = dtNorm.Rows(0)
            'txtBreakfastUnit.Value = CommonFunction._ToMoneyWithNull(drNorm("Breakfast"))
            'txtLunchUnit.Value = CommonFunction._ToMoneyWithNull(drNorm("Lunch"))
            'txtDinnerUnit.Value = CommonFunction._ToMoneyWithNull(drNorm("Dinner"))
            'txtOtherMealUnit.Value = CommonFunction._ToMoneyWithNull(drNorm("OtherMeal"))
            'txtHotelUnit.Value = If(chkCredit.Checked, Nothing, CommonFunction._ToMoneyWithNull(drNorm("Hotel")))
            'txtOtherAmount.Value = CommonFunction._ToMoneyWithNull(drNorm("Other"))
            hBreakfastUnit.Value = CommonFunction._ToMoney(drNorm("Breakfast"))
            hLunchUnit.Value = CommonFunction._ToMoney(drNorm("Lunch"))
            hDinnerUnit.Value = CommonFunction._ToMoney(drNorm("Dinner"))
            hOtherMealUnit.Value = CommonFunction._ToMoney(drNorm("OtherMeal"))
            hHotelUnit.Value = CommonFunction._ToMoney(drNorm("Hotel"))
            '
            'txtBreakfastUnit.MaxValue = GetSpiMaxValue(txtBreakfastUnit.Number)
            'txtLunchUnit.MaxValue = GetSpiMaxValue(txtLunchUnit.Number)
            'txtDinnerUnit.MaxValue = GetSpiMaxValue(txtDinnerUnit.Number)
            'txtOtherMealUnit.MaxValue = GetSpiMaxValue(txtOtherMealUnit.Number)
            'txtHotelUnit.MaxValue = GetSpiMaxValue(CommonFunction._ToMoney(drNorm("Hotel"))) 'GetSpiMaxValue(txtHotelUnit.Number)
            'If _isDomestic Then
            '    If Array.IndexOf(_gmAndAbove, txtPosition.Text.Trim().ToLower()) >= 0 Then
            '        txtHotelUnit.MaxValue = GetSpiMaxValue(0)
            '    End If
            'Else
            '    txtHotelUnit.MaxValue = GetSpiMaxValue(0)
            'End If
            'txtOtherAmount.MaxValue = GetSpiMaxValue(txtOtherAmount.Number)
        Else
            'txtBreakfastUnit.Value = Nothing
            'txtLunchUnit.Value = Nothing
            'txtDinnerUnit.Value = Nothing
            'txtOtherMealUnit.Value = Nothing
            'txtHotelUnit.Value = Nothing
            'txtOtherAmount.Value = Nothing
            hBreakfastUnit.Value = ""
            hLunchUnit.Value = ""
            hDinnerUnit.Value = ""
            hOtherMealUnit.Value = ""
            hHotelUnit.Value = ""
            '
            'txtBreakfastUnit.MaxValue = GetSpiMaxValue(0)
            'txtLunchUnit.MaxValue = GetSpiMaxValue(0)
            'txtDinnerUnit.MaxValue = GetSpiMaxValue(0)
            'txtOtherMealUnit.MaxValue = GetSpiMaxValue(0)
            'txtHotelUnit.MaxValue = GetSpiMaxValue(0)
            'txtOtherAmount.MaxValue = GetSpiMaxValue(0)
        End If
    End Sub

    Private Function GetSpiMaxValue(ByVal value As Decimal) As Decimal
        Return If(value < 1, 1000000000000, value)
    End Function

    'Protected Sub ddlDestinationLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlDestinationLocation.SelectedIndexChanged
    '    CommonFunction.SetPostBackStatus(ddlDestinationLocation)
    '    Try
    '        LoadExpenseNorm()
    '    Catch ex As Exception
    '        CommonFunction.ShowErrorMessage(panMessage, ex.Message)
    '    End Try
    'End Sub

    Protected Sub ddlBudgetName_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlBudgetName.SelectedIndexChanged
        CommonFunction.SetPostBackStatus(ddlBudgetName)
        Try
            Dim budgedCode As Integer = CommonFunction._ToInt(ddlBudgetName.SelectedValue.Split("-")(0))
            'txtBudgetRemain.Text = If(budgedCode = -1, "", String.Format("{0:#,0.##} VND", BusinessTripProvider.BTRegister_GetBudgetRemaining(budgedCode, CommonFunction._ToInt(hID.Value))))
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvBTSubmitted_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles grvBTSubmitted.HtmlRowPrepared, grvBTRejected.HtmlRowPrepared
        If e.RowType = GridViewRowType.Data Then
            Dim status As String = e.GetValue("FIStatus")
            If status = FIStatus.rejected.ToString() OrElse status = FIStatus.budget_rejected.ToString() Then
                e.Row.CssClass &= " rejected"
            ElseIf status = FIStatus.budget_reconfirmed.ToString() Then
                e.Row.CssClass &= " waiting"
            End If
        End If
    End Sub

    Protected Sub btnExportBT_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportBT.Click
        CommonFunction.SetPostBackStatus(btnExportBT)
        Dim xlApp As Excel.Application = Nothing
        Dim xlWorkBook As Excel.Workbook = Nothing
        Dim xlWorkSheet As Excel.Worksheet = Nothing
        Try
            Dim tmp As String = Server.MapPath(String.Concat("\Export\Template\", If(_isDomestic, "overnight_bt_template.xlt", "overnight_bt_international_template.xlt")))
            '            
            Dim misValue As Object = System.Reflection.Missing.Value
            '
            xlApp = New Excel.ApplicationClass()
            xlWorkBook = xlApp.Workbooks.Open(tmp, 0, True, 5, "", "", True, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, 1, 0)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            'business
            Dim ds As DataSet = ReportProvider.GetOverNightBT(CommonFunction._ToInt(hID.Value))
            ds.Tables(0).TableName = "General"
            ds.Tables(1).TableName = "Details"
            ds.Tables(2).TableName = "Transportation"
            ds.Tables(3).TableName = "Summary"
            '
            Dim rowIndex As Integer = 3
            'general info            
            Dim drGeneral As DataRow = ds.Tables("General").Rows(0)
            xlWorkSheet.Cells(rowIndex, 3) = drGeneral("BTNo")
            rowIndex = rowIndex + 2
            Dim employeeCode As String = drGeneral("EmployeeCode")
            xlWorkSheet.Cells(rowIndex, 3) = drGeneral("EmployeeName")
            xlWorkSheet.Cells(rowIndex, 13) = employeeCode
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 5) = drGeneral("Mobile")
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 4) = drGeneral("DepartmentName")
            xlWorkSheet.Cells(rowIndex, 12) = drGeneral("DivisionName")
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 4) = drGeneral("JobBand")
            'rowIndex = rowIndex + 1
            'xlWorkSheet.Cells(rowIndex, 7) = drGeneral("Budget")
            'request details
            rowIndex = rowIndex + 2
            Dim drDetails As DataRow = ds.Tables("Details").Rows(0)
            Dim drTransportation As DataRow = ds.Tables("Transportation").Rows(0)
            xlWorkSheet.Cells(rowIndex, 12) = drDetails("DepartureDate")
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 12) = drDetails("ReturnDate")
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 12) = drDetails("TotalDays")
            rowIndex = rowIndex + 2
            If CommonFunction._ToInt(drTransportation("AirTicketQty")) > 0 Then
                xlWorkSheet.Cells(rowIndex, 6) = "x"
                'expect air ticket
                Dim ExpectedDepartureTime As DateTime = CommonFunction._ToDateTime(drTransportation("ExpectedDepartureTime"))
                Dim ExpectedDepartureFlightNo As String = CommonFunction._ToString(drTransportation("ExpectedDepartureFlightNo"))
                Dim ExpectedReturnTime As DateTime = CommonFunction._ToDateTime(drTransportation("ExpectedReturnTime"))
                Dim ExpectedReturnFlightNo As String = CommonFunction._ToString(drTransportation("ExpectedReturnFlightNo"))
                Dim expectedDeparture As String = ""
                Dim expectedReturn As String = ""
                If ExpectedDepartureTime <> DateTime.MinValue Then
                    expectedDeparture = String.Concat(expectedDeparture, "; Time: ", ExpectedDepartureTime.ToString("dd-MMM-yyyy HH:mm"))
                End If
                If ExpectedDepartureFlightNo.Trim().Length > 0 Then
                    expectedDeparture = String.Concat(expectedDeparture, "; Flight No: ", ExpectedDepartureFlightNo.Trim())
                End If
                '
                If ExpectedReturnTime <> DateTime.MinValue Then
                    expectedReturn = String.Concat(expectedReturn, "; Time: ", ExpectedReturnTime.ToString("dd-MMM-yyyy HH:mm"))
                End If
                If ExpectedReturnFlightNo.Trim().Length > 0 Then
                    expectedReturn = String.Concat(expectedReturn, "; Flight No: ", ExpectedReturnFlightNo.Trim())
                End If
                '
                Dim expected As String = ""
                If expectedDeparture.Trim().Length > 0 Then
                    expectedDeparture = expectedDeparture.Substring(2)
                    expected = String.Concat("; Expected Departure - ", expectedDeparture)
                End If
                If expectedReturn.Trim().Length > 0 Then
                    expectedReturn = expectedReturn.Substring(2)
                    expected = String.Concat(expected, "; Expected Return - ", expectedReturn)
                End If
                '
                If expected.Trim().Length > 0 Then
                    expected = String.Concat("(", expected.Substring(2), ")")
                    xlWorkSheet.Cells(rowIndex, 11) = expected
                End If
            Else
                xlWorkSheet.Cells(rowIndex, 8) = "x"
            End If
            rowIndex = rowIndex + 1
            If CommonFunction._ToInt(drTransportation("TrainTicketQty")) > 0 Then
                xlWorkSheet.Cells(rowIndex, 6) = "x"
            Else
                xlWorkSheet.Cells(rowIndex, 8) = "x"
            End If
            rowIndex = rowIndex + 1
            If CommonFunction._ToInt(drTransportation("CarQty")) > 0 Then
                xlWorkSheet.Cells(rowIndex, 6) = "x"
            Else
                xlWorkSheet.Cells(rowIndex, 8) = "x"
            End If
            rowIndex = rowIndex + 2
            xlWorkSheet.Cells(rowIndex, 4) = drDetails("Purpose")
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 6) = drDetails("Destination")
            'summary            
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 6) = String.Concat("(", drGeneral("Currency"), ")")
            rowIndex = rowIndex + 2
            Dim drSummary As DataRow = ds.Tables("Summary").Rows(0)
            xlWorkSheet.Cells(rowIndex, 7) = drSummary("DailyAllowance")
            rowIndex = rowIndex + 2
            xlWorkSheet.Cells(rowIndex, 7) = drSummary("MovingTimeAllowance")
            rowIndex = rowIndex + 2
            xlWorkSheet.Cells(rowIndex, 7) = drSummary("HotelExpense")
            rowIndex = rowIndex + 2
            xlWorkSheet.Cells(rowIndex, 7) = drSummary("OtherExpense")
            rowIndex = rowIndex + 1
            Dim firstTimeAmount As Decimal = CommonFunction._ToMoney(drSummary("FirstTimeOversea"))
            If firstTimeAmount > 0 Then
                xlWorkSheet.Cells(rowIndex, 7) = String.Format("(First time Oversea Trip +{0} USD)", firstTimeAmount)
            End If
            rowIndex = rowIndex + 1
            xlWorkSheet.Cells(rowIndex, 7) = drSummary("TotalAdvance")
            '
            Dim filePath As String = String.Concat("~/Export/Download/", _username)
            Dim dir As DirectoryInfo = New DirectoryInfo(Server.MapPath(filePath))
            If Not dir.Exists() Then
                dir.Create()
            End If
            filePath = String.Concat(filePath, "/", "BT_OverNight_", employeeCode, "_", DateTime.Now.ToString("yyyyMMddHHmmssfffff"), ".xls")
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
            Dim ds As DataSet = ReportProvider.GetBTSchedule(CommonFunction._ToInt(hID.Value))
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
            'request details                   
            rowIndex = rowIndex + 3
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
                Dim estimateFee As Decimal = CommonFunction._ToMoney(drDetails("EstimateTransportationFee"))
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

    Protected Sub chkFirstTimeOversea_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkFirstTimeOversea.CheckedChanged
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            'chkFirstTimeChange(True)
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    'Private Sub chkFirstTimeChange(Optional ByVal loadSummary As Boolean = False)
    '    Dim btID As Integer = CommonFunction._ToInt(hID.Value)
    '    Dim drBT As DataRow = Nothing
    '    If btID > 0 Then
    '        drBT = BusinessTripProvider.UpdateFirstTimeOversea(btID, chkFirstTimeOversea.Checked)
    '    End If
    '    If chkFirstTimeOversea.Checked Then
    '        Dim firstTimeAmount As Decimal = 0
    '        Dim firstTimeAmountVND As Decimal = 0
    '        If drBT IsNot Nothing Then
    '            firstTimeAmount = CommonFunction._ToMoney(drBT("Amount"))
    '            firstTimeAmountVND = CommonFunction._ToMoney(drBT("AmountVND"))
    '        End If
    '        'If firstTimeAmount <= 0 Then
    '        '    firstTimeAmount = CommonFunction._ToMoney(LookupProvider.GetByCodeAndValue("PRE_PARAMS", "FIRST_TIME_OVERSEA"))
    '        'End If
    '        spanFirstAmount.InnerText = CommonFunction._FormatMoney(firstTimeAmount)
    '    End If
    '    '
    '    If loadSummary Then
    '        LoadTotalSummary()
    '    End If
    'End Sub

    'Protected Sub chkMovingTimeAllowance_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkMovingTimeAllowance.CheckedChanged
    '    CommonFunction.SetPostBackStatus(btnCancel)
    '    Try
    '        chkMovingTimeChange(True)
    '    Catch ex As Exception
    '        CommonFunction.ShowErrorMessage(panMessage, ex.Message)
    '    End Try
    'End Sub

    'Private Sub chkMovingTimeChange(Optional ByVal loadSummary As Boolean = False)
    '    Dim btID As Integer = CommonFunction._ToInt(hID.Value)
    '    Dim drBT As DataRow = Nothing
    '    If btID > 0 Then
    '        drBT = BusinessTripProvider.UpdateMovingTime(btID, chkMovingTimeAllowance.Checked)
    '    End If
    '    Dim movingTimeAmount As Decimal = 0
    '    Dim movingTimeCurrency As String = If(_isDomestic, "VND", "USD")
    '    Dim movingTimeAmountVND As Decimal = 0
    '    If drBT IsNot Nothing Then
    '        movingTimeAmount = CommonFunction._ToMoney(drBT("Amount"))
    '        movingTimeCurrency = CommonFunction._ToString(drBT("Currency"))
    '        movingTimeAmountVND = CommonFunction._ToMoney(drBT("AmountVND"))
    '    End If
    '    'If movingTimeAmount <= 0 Then
    '    '    Dim dtPolicy As DataTable = mMovingTimeAllowanceProvider.m_Expense_GetPolicy(ddlDestinationCountry.SelectedValue)
    '    '    If dtPolicy.Rows.Count > 0 Then
    '    '        Dim drPolicy As DataRow = dtPolicy.Rows(0)
    '    '        movingTimeAmount = CommonFunction._ToMoney(drPolicy("Amount"))
    '    '        movingTimeCurrency = CommonFunction._ToString(drPolicy("Currency"))
    '    '    Else
    '    '        movingTimeAmount = 0
    '    '        movingTimeCurrency = If(_isDomestic, "VND", "USD")
    '    '    End If
    '    'End If
    '    spanMovingTimeAmount.InnerText = String.Format("{0:#,0.##} {1}", movingTimeAmount, movingTimeCurrency)
    '    '
    '    If loadSummary Then
    '        LoadTotalSummary()
    '    End If
    'End Sub

    Protected Sub chkNoRequestAdvance_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkNoRequestAdvance.CheckedChanged
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            If chkNoRequestAdvance.Checked Then
                tdRequestDestinationControl.Visible = True
                tdRequestDestinationLabel.Visible = True
                '
                chkFirstTimeOversea.Checked = False
                chkFirstTimeOversea.Enabled = False
                'chkFirstTimeChange()
                chkMovingTimeAllowance.Checked = False
                chkMovingTimeAllowance.Enabled = False
                'chkMovingTimeChange()
                '
                grvBTRequest.DataSource = Nothing
                grvBTRequest.DataBind()
                EnableRequestForm(False)
                '
                LoadTotalSummary(True)
            Else
                tdRequestDestinationControl.Visible = False
                tdRequestDestinationLabel.Visible = False
                chkFirstTimeOversea.Enabled = True
                chkMovingTimeAllowance.Enabled = True
                LoadBTRegisterRequest()
                EnableRequestForm(True)
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnApprove_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnApprove.Click
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            dteGLDate.Attributes("style") = ""
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
                    Else
                        btnApprove.Visible = False
                        Dim supplierNo As String = CommonFunction._ToString(dtSupplier.Rows(0)("VendorNo"))
                        Dim supplierSite As String = CommonFunction._ToString(dtSupplierSite.Rows(0)("VendorSite"))
                        Dim dr As DataRow = BusinessTripProvider.tbl_BT_Register_UpdateStatusFI(BTRegisterID, FIStatus.completed.ToString(), _username, "", supplierNo, supplierSite, glDate, batchName, invoiceDate)
                        Dim message As String = CommonFunction._ToString(dr("MessageResult")).Trim()
                        Dim messageStatus As String = CommonFunction._ToString(dr("MessageStatus")).Trim()
                        If messageStatus.Length > 0 Then
                            CommonFunction.ShowErrorMessage(panMessage, messageStatus)
                        Else
                            tabApproveMessage.Attributes("style") = "display: none"
                            btnApprove.Visible = True
                            '
                            Dim isSendMail As Boolean = (btnApprove.Text = "Approve")
                            LoadBTRegisterInfo()
                            'LoadBTRegisterHistory()
                            Dim budgedCode As Integer = CommonFunction._ToInt(ddlBudgetName.SelectedValue.Split("-")(0))
                            'txtBudgetRemain.Text = If(budgedCode = -1, "", String.Format("{0:#,0.##} VND", BusinessTripProvider.BTRegister_GetBudgetRemaining(budgedCode, CommonFunction._ToInt(hID.Value))))
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
            'Dim oracleError As Boolean = BusinessTripProvider.tbl_BT_Register_CheckOracleStatus(BTRegisterID)
            'If oracleError Then

            'Else
            '    CommonFunction.ShowErrorMessage(panMessage, "Can not reject this BT! Please check oracle invoice status!")
            'End If
            Dim role As String = CommonFunction._ToString(Session("RoleType"))
            Select Case role.ToLower()
                Case RoleType.Finance.ToString().ToLower(), RoleType.Administrator.ToString().ToLower(), _
                    RoleType.Finance_GA.ToString().ToLower(), RoleType.Finance_Budget.ToString().ToLower()
                    Dim dr As DataRow = BusinessTripProvider.tbl_BT_Register_UpdateStatusFI(BTRegisterID, FIStatus.rejected.ToString(), _username, rejectReason)
                    Dim message As String = CommonFunction._ToString(dr("MessageResult")).Trim()
                    If message.Trim().Length > 0 Then
                        CommonFunction.ShowErrorMessage(panMessage, message)
                    Else
                        LoadBTRegisterInfo()
                        'LoadBTRegisterHistory()
                        Dim budgedCode As Integer = CommonFunction._ToInt(ddlBudgetName.SelectedValue.Split("-")(0))
                        'txtBudgetRemain.Text = If(budgedCode = -1, "", String.Format("{0:#,0.##} VND", BusinessTripProvider.BTRegister_GetBudgetRemaining(budgedCode, CommonFunction._ToInt(hID.Value))))
                        'Send notice email
                        If SendUserEmail() Then
                            CommonFunction.ShowInfoMessage(panMessage, "BT's Rejected!")
                        Else
                            CommonFunction.ShowInfoMessage(panMessage, "BT's Rejected but fail to send notice emails! Please contact with administrator!")
                        End If
                        _objEmail.Dispose()
                    End If
                    'Case RoleType.GA.ToString().ToLower()
                    '    BusinessTripProvider.tbl_BT_Register_UpdateStatusGA(BTRegisterID, GAStatus.rejected.ToString(), _username, rejectReason)
                    '    isRejected = True
                    'Case RoleType.HR.ToString().ToLower()
                    '    BusinessTripProvider.tbl_BT_Register_UpdateStatusHR(BTRegisterID, HRStatus.rejected.ToString(), _username, rejectReason)
                    '    isRejected = True
                Case Else
                    CommonFunction.ShowErrorMessage(panMessage, "Unauthorized!")
            End Select
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnConfirmBudget_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnConfirmBudget.Click
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            Dim obj As tblBTRegisterInfo = GetObject()
            Dim comment As String = txtConfirmComment.Text.Trim()
            Dim message As String = BusinessTripProvider.BTRegister_ConfirmBudget(obj, comment)
            If message.Length = 0 Then
                LoadBTRegisterInfo()
                'Send notice email
                If SendConfirmBudgetEmail() Then
                    SendUserEmail()
                    CommonFunction.ShowInfoMessage(panMessage, "Budget is confirmed successfully!")
                Else
                    CommonFunction.ShowInfoMessage(panMessage, "Budget is confirmed successfully but fail to send notice emails! Please contact with administrator!")
                End If
                'Send other budget notification
                If chkBudgetAll.Checked Then
                    SendNoticeBudgetEmail()
                End If
                _objEmail.Dispose()
            Else
                CommonFunction.ShowErrorMessage(panMessage, message)
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnBudgetReject_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnBudgetReject.Click
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            Dim BTRegisterID As Integer = CommonFunction._ToInt(hID.Value)
            Dim rejectReason As String = txtRejectReason.Text.Trim()
            txtRejectReason.Text = ""
            If rejectReason.Trim().Length = 0 Then
                Return
            End If
            Dim obj As tblBTRegisterInfo = GetObject()
            Dim message As String = BusinessTripProvider.BTRegister_ConfirmRequester(obj, rejectReason)
            If message.Length = 0 Then
                _oldBudget = hOldBudget.Value
                '
                LoadBTRegisterInfo()
                'LoadBTRegisterHistory()
                'Send notice email                
                If SendUserEmail() Then
                    CommonFunction.ShowInfoMessage(panMessage, "Success to send requester confirmation email!")
                Else
                    CommonFunction.ShowInfoMessage(panMessage, "Fail to send requester confirmation email! Please contact with administrator!")
                End If
                'Send other budget notification
                If chkBudgetAll.Checked Then
                    SendNoticeBudgetEmail()
                End If
                _objEmail.Dispose()
            Else
                CommonFunction.ShowErrorMessage(panMessage, message)
            End If
            'Dim role As String = CommonFunction._ToString(Session("RoleType"))
            'Dim isRejected As Boolean = False
            'Select Case role.ToLower()
            '    Case RoleType.Finance_Budget.ToString().ToLower(), RoleType.Administrator.ToString().ToLower()

            '        isRejected = True
            'End Select
            'If isRejected Then            
            'Else
            'CommonFunction.ShowErrorMessage(panMessage, "Unauthorized!")
            'End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnRejectToBudget_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnRejectToBudget.Click
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            Dim obj As tblBTRegisterInfo = GetObject()
            Dim comment As String = txtRejectReason.Text.Trim()
            Dim message As String = BusinessTripProvider.BTRegister_RejectToBudget(obj.BTRegisterID, comment, obj.ModifiedBy)
            If message.Trim().Length > 0 Then
                CommonFunction.ShowErrorMessage(panMessage, message)
            Else
                LoadBTRegisterInfo()
                'Send notice email
                _isRejectToBudget = True
                If SendSubmitEmail() Then
                    SendUserEmail()
                    CommonFunction.ShowInfoMessage(panMessage, "Reject to Finance Budget successfully!")
                Else
                    CommonFunction.ShowInfoMessage(panMessage, "Reject to Finance Budget successfully but fail to send notice emails! Please contact with administrator!")
                End If
                _objEmail.Dispose()
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnResetAdvance_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnResetAdvance.Click
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            Dim btID As Integer = CommonFunction._ToInt(hID.Value)
            Dim message As String = BusinessTripProvider.tbl_BT_Register_ClearRequest(btID, _username)
            If message.Length = 0 Then
                LoadBTRegisterInfo()
                LoadBTRegisterRequest()
                CommonFunction.ShowInfoMessage(panMessage, "Clear advance request (advance amount = 0) successfully!")
            Else
                CommonFunction.ShowErrorMessage(panMessage, message)
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelBT_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelBT.Click
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            Dim btID As Integer = CommonFunction._ToInt(hID.Value)
            Dim message As String = BusinessTripProvider.tbl_BT_Register_CancelBT(btID, _username)
            If message.Length = 0 Then
                LoadBTRegisterInfo()
                '
                If SendUserEmail() Then
                    CommonFunction.ShowInfoMessage(panMessage, "BT Approval has been cancelled!")
                Else
                    CommonFunction.ShowInfoMessage(panMessage, "BT Approval has been cancelled but fail to send notice emails! Please contact with administrator!")
                End If
            Else
                CommonFunction.ShowErrorMessage(panMessage, message)
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnCopy_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCopy.Click
        CommonFunction.SetPostBackStatus(btnCopy)
        Try
            Dim btID As Integer = CommonFunction._ToInt(hID.Value)
            Dim employees As String = hCopyUsers.Value 'Request.Params("employees")
            Dim count As Integer = BusinessTripProvider.BTRegister_Copy(btID, employees, _username)
            LoadDataList()
            CommonFunction.ShowInfoMessage(panMessage, String.Format("{0} BT(s) copied!", count)) 'employees.Split(",").Count()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvChooseEmployee_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvChooseEmployee.BeforeGetCallbackResult
        InitEmployees()
    End Sub

    'Protected Sub txtDateTime_DateChanged(ByVal sender As Object, ByVal e As EventArgs) Handles dteFromDate.DateChanged, dteToDate.DateChanged
    '    CommonFunction.SetPostBackStatus(btnCancel)
    '    Try
    '        SetRequestAmount()
    '    Catch ex As Exception
    '        CommonFunction.ShowErrorMessage(panMessage, ex.Message)
    '    End Try
    'End Sub

    'Private Sub SetRequestAmount()
    '    Dim breakfast As Integer = 0
    '    Dim lunch As Integer = 0
    '    Dim dinner As Integer = 0
    '    Dim other As Integer = 0
    '    Dim hotel As Integer = 0
    '    If dteFromDate.Text.Trim().Length > 0 AndAlso dteToDate.Text.Trim().Length > 0 Then
    '        Dim fromDate As DateTime = dteFromDate.Date
    '        Dim toDate As DateTime = dteToDate.Date
    '        Dim dayCount As Integer = New Date(toDate.Year, toDate.Month, toDate.Day).Subtract(New Date(fromDate.Year, fromDate.Month, fromDate.Day)).TotalDays + 1
    '        'Dim isDGMAndAbove As Boolean = False 'hIsDGMAndAbove.Value = "Y"
    '        Dim isGMAndAbove As Boolean = (hIsGMAndAbove.Value = "Y")
    '        breakfast = dayCount
    '        lunch = dayCount
    '        dinner = dayCount
    '        other = dayCount
    '        hotel = dayCount - 1
    '        If Not isGMAndAbove Then
    '            other = dayCount - 1
    '            '
    '            If CompareTime(fromDate, 8, 30) > 0 OrElse CompareTime(toDate, 8, 30) < 0 Then
    '                breakfast -= 1
    '            End If
    '            '
    '            If CompareTime(fromDate, 12, 30) > 0 OrElse CompareTime(toDate, 12, 30) < 0 Then
    '                lunch -= 1
    '            End If
    '            '
    '            If CompareTime(fromDate, 18, 0) > 0 OrElse CompareTime(toDate, 18, 0) < 0 Then
    '                dinner -= 1
    '            End If
    '        End If
    '    End If
    '    ''txtBreakfastQty.MaxValue = GetSpiMaxValue(breakfast)
    '    txtBreakfastQty.Value = If(breakfast > 0, breakfast, Nothing)
    '    'txtLunchQty.MaxValue = GetSpiMaxValue(lunch)
    '    txtLunchQty.Value = If(lunch > 0, lunch, Nothing)
    '    'txtDinnerQty.MaxValue = GetSpiMaxValue(dinner)
    '    txtDinnerQty.Value = If(dinner > 0, dinner, Nothing)
    '    'txtOtherMealQty.MaxValue = GetSpiMaxValue(other)
    '    txtOtherMealQty.Value = If(other > 0, other, Nothing)
    '    'txtHotelQty.MaxValue = GetSpiMaxValue(hotel)
    '    txtHotelQty.Value = If(hotel > 0, hotel, Nothing)
    'End Sub

    'Private Function CompareTime(ByVal theDate As DateTime, ByVal hours As Integer, ByVal min As Integer) As Integer
    '    Dim result As Integer
    '    If (theDate.Hour = hours AndAlso theDate.Minute > min) OrElse theDate.Hour > hours Then
    '        result = 1
    '    ElseIf theDate.Hour = hours AndAlso theDate.Minute = min Then
    '        result = 0
    '    Else
    '        result = -1
    '    End If
    '    Return result
    'End Function

    Protected Sub btnApproveCancel_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnApproveCancel.Click
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            tabApproveMessage.Attributes("style") = "display: none"
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDepartureDateChange_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnDepartureDateChange.Click
        CommonFunction.SetPostBackStatus(btnCancel)
        Try
            LoadExpenseNorm()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

#Region "Send Notice Email"

    Private Function GenNoticeBody(Optional ByVal link As String = "", Optional ByVal notice As String = "") As String
        Dim eBody As New StringBuilder("<p><strong>To whom it may concern</strong></p>")
        eBody.Append("<p>This is notification from <strong>B</strong>usiness <strong>T</strong>rip Online <strong>S</strong>ystem (BTS).</p>")
        eBody.Append(String.Format("<p>Regarding Business Trip No. ""<strong>{0}</strong>"", we would like to share the latest information to you as below:</p>", txtBusinessTripNo.Text.Trim()))
        eBody.Append(String.Format("<p><table><tr><td valign='top' width='210'><ul><li style='margin: 0;'><strong>BT No:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", txtBusinessTripNo.Text.Trim()))
        Dim btTypeItem As ListItem = ddlBTType.SelectedItem
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>BT Type:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", If(btTypeItem IsNot Nothing, btTypeItem.Text, "")))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Employee Code/Name:</strong></li></ul></td><td style='color: #0070c0'><span style='color: red'>{0}</span>/<span style='color: red'>{1}</span></td></tr>", txtEmployeeCode.Text.Trim(), txtFullName.Text.Trim()))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Purpose/Destination:</strong></li></ul></td><td style='color: #0070c0'>{0}/{1}</td></tr>", txtPurpose.Text.Trim(), _destination))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Date From:</strong></li></ul></td><td><span style='color: #0070c0'>{0}</span> <strong>To:</strong> <span style='color: #0070c0'>{1}</span></td></tr>", dteDepartureDate.Date.ToString("dd-MMM-yyyy HH:mm"), dteReturnDate.Date.ToString("dd-MMM-yyyy HH:mm")))
        Dim budgetName As String = If(ddlBudgetName.SelectedItem IsNot Nothing, ddlBudgetName.SelectedItem.Text, "")
        'If budgetName.Trim().Length > 0 Then
        '    budgetName = budgetName.Substring(budgetName.IndexOf("] -") + 3).Trim()
        'End If
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Budget Code/Name:</strong></li></ul></td><td><span style='color: #0070c0'>{0}/{1}</span><span style='color: #333;'>{2}</span></td></tr>", txtBudgetCode.Text.Trim(), budgetName, If(_oldBudget.Trim().Length > 0, String.Format("<br />(Change from: {0})", _oldBudget), "")))
        'expect air ticket
        Dim expectedDeparture As String = ""
        If dteExpectedDepartureTime.Value IsNot Nothing Then
            expectedDeparture = String.Concat(expectedDeparture, "; Time: ", dteExpectedDepartureTime.Date.ToString("dd-MMM-yyyy HH:mm"))
        End If
        If txtExpectedDepartureFlightNo.Text.Trim().Length > 0 Then
            expectedDeparture = String.Concat(expectedDeparture, "; Flight No: ", txtExpectedDepartureFlightNo.Text.Trim())
        End If
        If expectedDeparture.Trim().Length > 0 Then
            expectedDeparture = expectedDeparture.Substring(2)
        End If
        '
        Dim expectedReturn As String = ""
        If dteExpectedReturnTime.Value IsNot Nothing Then
            expectedReturn = String.Concat(expectedReturn, "; Time: ", dteExpectedReturnTime.Date.ToString("dd-MMM-yyyy HH:mm"))
        End If
        If txtExpectedReturnFlightNo.Text.Trim().Length > 0 Then
            expectedReturn = String.Concat(expectedReturn, "; Flight No: ", txtExpectedReturnFlightNo.Text.Trim())
        End If
        If expectedReturn.Trim().Length > 0 Then
            expectedReturn = expectedReturn.Substring(2)
        End If
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Request to GA:</strong></li></ul></td><td><span style='color: red'>{0}</span><span style='color: #0070c0'>{1}{2}</span></td></tr>", If(hRequestGA.Value.Trim().Length > 0, hRequestGA.Value, "Don't have any request"), If(hRequestGA.Value.Trim().Length > 0 AndAlso expectedDeparture.Trim().Length > 0, String.Concat("<br />* Expected Departure - ", expectedDeparture), ""), If(hRequestGA.Value.Trim().Length > 0 AndAlso expectedReturn.Trim().Length > 0, String.Concat("<br />* Expected Return - ", expectedReturn), "")))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Amount:</strong></li></ul></td><td><span style='color: red'>{0} {1}</span></td></tr>", lblTotalAdvance.Text, ddlCurrency.SelectedItem.Text))
        Dim statusDesc As String = CommonFunction._ToString(lnkFINStatus.Attributes("title"))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Status:</strong></li></ul></td><td style='color: red'><strong>{0}</strong> {1}</td></tr>", lnkFINStatus.InnerText, If(statusDesc.Trim().Length > 0, String.Format("({0})", statusDesc), "")))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Comment:</strong></li></ul></td><td><span style='color: #0070c0'>{0}</span></td></tr>", If(lblFINComment.Text = "", "No comment", If(lblFINComment.Text.ToLower() = "(recall)", "This business trip was re-called successfully by requester", lblFINComment.Text))))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Updated by:</strong></li></ul></td><td style='color: #0070c0'>{0} - {1} ({2} Department | {3})</td></tr></table></p>", _username, CommonFunction._ToString(Session("FullName")), CommonFunction._ToString(Session("Department")), CommonFunction._ToString(Session("Division"))))
        eBody.Append(notice)
        eBody.Append(If(link IsNot Nothing AndAlso link <> "", String.Format("<p>Please <a href='{0}{1}'>click here</a> to check/process this information.</p>", ConfigurationManager.AppSettings("Domain"), link), ""))
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
            Dim eSubject As String = String.Format("{0}[BTS Advance]: {1} - {2}/{3} (Status: {4})", indicator, txtBusinessTripNo.Text.Trim(), txtFullName.Text.Trim(), _lastDestination, lnkFINStatus.InnerText)
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
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToInt(hID.Value), RoleType.Finance_Budget.ToString())
        Else
            Dim dtAuthorized As DataTable = BusinessTripProvider.BTRegister_GetAuthorizedUsers(CommonFunction._ToInt(hID.Value))
            Dim dvFIBudget As DataView = dtAuthorized.DefaultView
            dvFIBudget.RowFilter = String.Format("Role in ('{0}', '{1}')", RoleType.Finance_Budget.ToString(), RoleType.Administrator.ToString())
            For Each dr As DataRowView In dvFIBudget
                eTo = String.Concat(eTo, ", ", dr("TMVEmail"))
            Next
            If eTo.Length > 0 Then
                eTo = eTo.Substring(1).Trim()
            End If
        End If
        Dim viewLink As String = String.Format("BTAdvanceDeclaration.aspx?id={0}&back=FiBudgetChecking.aspx&params=btid;eq;{0};amp;bttype;eq;;amp;loc;eq;;amp;fdate;eq;;amp;tdate;eq;;amp;div;eq;;amp;dep;eq;;amp;sec;eq;;amp;group;eq;;amp;ecode;eq;;amp;ename;eq;;amp;btno;eq;", hID.Value)
        '
        Dim notice As New StringBuilder()
        If chkBudgetAll.Checked AndAlso Not _isRejectToBudget Then
            Dim budgedID As Integer = CommonFunction._ToInt(ddlBudgetName.SelectedValue.Split("-")(0))
            Dim dtBudget As DataTable = mBudgetProvider.m_Budget_GetByID(budgedID)
            notice.Append(String.Format("<p style='color: red'><strong>To {0} budget P.I.C:</strong><br>This business trip is using budget of your department. Please help us to cross-check this information and reply this email to CPM@toyotavn.com.vn to reject if you don't agree!<br>(System will accept this budget information after 2 days if you don't reply)</p>", dtBudget.Rows(0)("Department")))
        End If
        '
        Dim eBody As String = GenNoticeBody(viewLink, notice.ToString())
        Return SendNoticeEmail(eTo, eBody)
    End Function

    Private Function SendNoticeBudgetEmail() As Boolean
        Dim eTo As String = ""
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToInt(hID.Value), RoleType.Finance_Budget.ToString())
        Else
            Dim dtPic As DataTable = mBudgetProvider.m_Budget_GetPICs(txtBudgetCode.Text.Trim())
            For Each dr As DataRow In dtPic.Rows
                eTo = String.Concat(eTo, ", ", dr("PICEmail"))
            Next
            If eTo.Length > 0 Then
                eTo = eTo.Substring(1).Trim()
            End If
        End If
        '
        Dim notice As New StringBuilder()
        Dim budgedID As Integer = CommonFunction._ToInt(ddlBudgetName.SelectedValue.Split("-")(0))
        Dim dtBudget As DataTable = mBudgetProvider.m_Budget_GetByID(budgedID)
        notice.Append(String.Format("<p style='color: red'><strong>To {0} budget P.I.C:</strong><br>This business trip is using budget of your department. Please help us to cross-check this information and reply this email to CPM@toyotavn.com.vn to reject if you don't agree!<br>(System will accept this budget information after 2 days if you don't reply)</p>", dtBudget.Rows(0)("Department")))
        '
        Dim eBody As String = GenNoticeBody(Nothing, notice.ToString())
        Return SendNoticeEmail(eTo, eBody)
    End Function

    Private Function SendConfirmBudgetEmail() As Boolean
        Dim eTo As String = ""
        Dim eToGA As String = ""
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToInt(hID.Value), RoleType.Finance.ToString())
            eToGA = UserProvider.UserTestEmail_Get(CommonFunction._ToInt(hID.Value), RoleType.GA.ToString())
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
            '
            Dim dvGA As DataView = dtAuthorized.DefaultView
            dvGA.RowFilter = String.Format("Role in ('{0}', '{1}')", RoleType.GA.ToString(), RoleType.TOFS_AIR_GA.ToString())
            For Each dr As DataRowView In dvGA
                eToGA = String.Concat(eToGA, ", ", dr("TMVEmail"))
            Next
            If eToGA.Length > 0 Then
                eToGA = eToGA.Substring(1).Trim()
            End If
        End If
        Dim viewLinkGA As String = String.Format("BTAdvanceDeclaration.aspx?id={0}&back=GAAdvanceMgmt.aspx&params=btid;eq;{0};amp;bttype;eq;;amp;loc;eq;;amp;fdate;eq;;amp;tdate;eq;;amp;div;eq;;amp;dep;eq;;amp;sec;eq;;amp;group;eq;;amp;ecode;eq;;amp;ename;eq;", hID.Value)
        Dim eBodyGA As String = GenNoticeBody(viewLinkGA)
        '
        Dim viewLink As String = String.Format("BTAdvanceDeclaration.aspx?id={0}&back=FiAdvanceMgmt.aspx&params=btid;eq;{0};amp;bttype;eq;;amp;loc;eq;;amp;fdate;eq;;amp;tdate;eq;;amp;div;eq;;amp;dep;eq;;amp;sec;eq;;amp;group;eq;;amp;ecode;eq;;amp;ename;eq;;amp;btno;eq;", hID.Value)
        Dim eBody As String = GenNoticeBody(viewLink)
        Return SendNoticeEmail(eTo, eBody) AndAlso SendNoticeEmail(eToGA, eBodyGA)
    End Function

    Private Function SendUserEmail(Optional ByVal isFirstTimeSubmit As Boolean = False) As Boolean
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
        Dim viewLink As String = String.Format("BTAdvanceDeclaration.aspx?id={0}&back=BTAdvanceDeclaration.aspx", hID.Value)
        '
        Dim notice As New StringBuilder()
        If isFirstTimeSubmit Then
            Dim dtWifi As DataTable = WifiDeviceProvider.WifiDevice_GetByBT(CommonFunction._ToInt(hID.Value))
            If dtWifi.Rows.Count > 0 Then
                notice.Append("<p><strong>About wifi device requests</strong>: requests before 7 days will be confirmed automatically, otherwise need to be confirmed by IT deparment!</p>")
                SendWifiEmail()
            End If
        End If
        '
        Dim eBody As String = GenNoticeBody(viewLink, notice.ToString())
        Return SendNoticeEmail(eTo, eBody)
    End Function

    Private Function SendRecallEmail() As Boolean
        Dim eTo As String = ""
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToInt(hID.Value), RoleType.Finance_Budget.ToString())
        Else
            Dim dtAuthorized As DataTable = BusinessTripProvider.BTRegister_GetAuthorizedUsers(CommonFunction._ToInt(hID.Value))
            Dim dvFIBudget As DataView = dtAuthorized.DefaultView
            dvFIBudget.RowFilter = String.Format("Role in ('{0}', '{1}')", RoleType.Finance_Budget.ToString(), RoleType.Administrator.ToString())
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

    Private Function SendITEmail() As Boolean
        Dim eTo As String = ""
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToInt(hID.Value), RoleType.Normal.ToString())
        Else
            eTo = LookupProvider.GetByCodeAndValue("EMAIL_ADDRESS", "it_support")
        End If
        '
        Dim eBody As String = GenNoticeBody()
        Return SendNoticeEmail(eTo, eBody)
    End Function

    Private Function GenWifiNoticeBody(Optional ByVal link As String = "") As String
        Dim eBody As New StringBuilder("<p><strong>To whom it may concern</strong></p>")
        eBody.Append("<p>This is notification from <strong>B</strong>usiness <strong>T</strong>rip Online <strong>S</strong>ystem (BTS).</p>")
        eBody.Append("<p>Regarding wifi device request, we would like to share the latest information to you as below:")
        eBody.Append(String.Format("<p><table><tr><td valign='top' width='210'><ul><li style='margin: 0;'><strong>BT No:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", txtBusinessTripNo.Text.Trim()))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Employee:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr></table></p>", String.Format("{0}-{1}", txtEmployeeCode.Text.Trim(), txtFullName.Text.Trim())))
        eBody.Append(String.Format("<p>Please <a href='{0}{1}'>click here</a> to check/process this information.</p>", ConfigurationManager.AppSettings("Domain"), link))
        eBody.Append("<p>If you are unable to check/process, You can refer to FAQ function from main menu of BTS system to view all Frequently Asked Question.</p>")
        eBody.Append("<p>Thank you for your cooperation.</p>")
        eBody.Append("<p>Regards,<br>BTS Support Team</p>")
        Return eBody.ToString()
    End Function

    Private Function SendWifiNoticeEmail(ByVal eTo As String, ByVal eBody As String) As Boolean
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
            Dim employee As String = String.Format("{0}-{1}", txtEmployeeCode.Text.Trim(), txtFullName.Text.Trim())
            Dim bcc As String = ConfigurationManager.AppSettings("UserTestBCC")
            Dim eFrom As String = ConfigurationManager.AppSettings("BTSSupportEmail").Replace("[", "<").Replace("]", ">")
            Dim eSubject As String = String.Format("{0}[BTS Wifi Device]: {1} / {2}", indicator, txtBusinessTripNo.Text.Trim(), employee)
            Try
                isSent = _objEmail.SendEmail(eFrom, eTo, "", bcc, eSubject, eBody.ToString(), "", "")
            Catch ex As Exception
                isSent = False
            End Try
        End If
        Return isSent
    End Function

    Private Function SendWifiEmail() As Boolean
        Dim eTo As String = ""
        Dim viewLink As String = String.Format("BTWifiDevice.aspx?btno={0}", txtBusinessTripNo.Text.Trim())
        Dim eBody As String = GenWifiNoticeBody(viewLink)
        '
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToString(Session("Branch")), RoleType.Finance.ToString())
        Else
            Dim dtAuthorized As DataTable = BusinessTripProvider.BTRegister_GetAuthorizedUsers(CommonFunction._ToInt(hID.Value))
            Dim dvITBudget As DataView = dtAuthorized.DefaultView
            dvITBudget.RowFilter = String.Format("Role in ('{0}', '{1}')", RoleType.IT.ToString(), RoleType.Administrator.ToString())
            For Each dr As DataRowView In dvITBudget
                eTo = String.Concat(eTo, ", ", dr("TMVEmail"))
            Next
            If eTo.Length > 0 Then
                eTo = eTo.Substring(1).Trim()
            End If
        End If
        Return SendWifiNoticeEmail(eTo, eBody)
    End Function

#End Region

End Class