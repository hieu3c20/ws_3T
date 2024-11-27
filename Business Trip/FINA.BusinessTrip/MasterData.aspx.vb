Imports Provider
Imports DevExpress.Web.ASPxEditors

Partial Public Class MasterData
    Inherits System.Web.UI.Page
    Dim pathFile As String
    Dim isGetFromExcelError As Boolean
    Dim isSuccess As Boolean
    Private dtError As Data.DataTable
    Dim dtExcel As New DataTable
    Dim dctEmployeeCode As New Dictionary(Of Integer, String)
    Dim dctOTDate As New Dictionary(Of Integer, String)
    Dim dctOT As New Dictionary(Of Integer, String)
    Dim dctOvertimetypeCode As New Dictionary(Of Integer, String)
    Dim dctNote As New Dictionary(Of Integer, String)

#Region "LOAD DATA"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CommonFunction.CheckSession()
        CommonFunction.CheckRole(RoleType.Administrator, RoleType.Finance, RoleType.Finance_GA, RoleType.Finance_Budget) 'RoleType.GA,
        SetRoleAuthorized()
        '
        If Not IsPostBack Then
            LoadGridCountry()
            LoadGridDes()
            LoadGridDesGroup()
            LoadGridTitleGroup()
            LoadGridSystem()
            LoadGridExpense()
            LoadGridBudget()
            LoadGridBudgetPIC()
            LoadGridInvoice()
            LoadGridAirPeriod()
            LoadGridSupplier()
            LoadGridBatchName()
            LoadGridDailyRate()
            LoadGridCompanyName()
            LoadGridCountryGroup()
            LoadGridAllowance()
            'InitOrg()                                    
        End If
    End Sub

    Private Sub SetRoleAuthorized()
        divAirTicket.Visible = False
        divBTPolicy.Visible = False
        divBudget.Visible = False
        divDailyExrate.Visible = False
        divDestination.Visible = False
        divInvoicing.Visible = False
        divOraInvBatchName.Visible = False
        divSystemParameter.Visible = False
        divTitleGroup.Visible = False
        '
        Dim role As String = CommonFunction._ToString(Session("RoleType")).ToLower()
        If role = RoleType.Administrator.ToString().ToLower() Then
            divSystemParameter.Visible = True
        End If
        '
        If role = RoleType.Administrator.ToString().ToLower() _
            OrElse role = RoleType.Finance.ToString().ToLower() _
            OrElse role = RoleType.Finance_GA.ToString().ToLower() Then
            divAirTicket.Visible = True
            divBTPolicy.Visible = True
            divDailyExrate.Visible = True
            divDestination.Visible = True
            divInvoicing.Visible = True
            divOraInvBatchName.Visible = True
            divTitleGroup.Visible = True
        End If
        '
        'If role = RoleType.GA.ToString().ToLower() OrElse role.ToLower() = RoleType.Top_GA.ToString().ToLower() Then
        '    divAirTicket.Visible = True           
        'End If
        '
        If role = RoleType.Administrator.ToString().ToLower() _
            OrElse role = RoleType.Finance_Budget.ToString().ToLower() Then
            divBudget.Visible = True
        End If
    End Sub

    Private Sub LoadGridCountry()
        Dim dtData As DataTable = mCountryProvider.GetAll()
        grvCountry.DataSource = dtData
        grvCountry.DataBind()
    End Sub

    Private Sub LoadGridDes()
        Dim dtDes As DataTable = mDestinationProvider.GetAll()
        grvDestination.DataSource = dtDes
        grvDestination.DataBind()
    End Sub

    Private Sub LoadGridDesGroup()
        Dim dtDesGroup As DataTable = mDestinationGroupProvider.GetAll()
        grvDesGroup.DataSource = dtDesGroup
        grvDesGroup.DataBind()
    End Sub

    Private Sub LoadGridTitleGroup()
        Dim dtTitleGroup As DataTable = mTitleGroupProvider.GetAll()
        grvTitleGroup.DataSource = dtTitleGroup
        grvTitleGroup.DataBind()
    End Sub

    Private Sub LoadGridSystem()
        Dim dtSys As DataTable = LookupProvider.GetAll()
        grvSystem.DataSource = dtSys
        grvSystem.DataBind()
    End Sub

    Private Sub LoadGridExpense()
        Dim dtExp As DataTable = mExpenseProvider.GetAll()
        grvExpense.DataSource = dtExp
        grvExpense.DataBind()
    End Sub

    Private Sub LoadGridBudget()
        Dim dtBud As DataTable = mBudgetProvider.GetAll()
        grvBudget.DataSource = dtBud
        grvBudget.DataBind()
    End Sub

    Private Sub LoadGridBudgetPIC()
        Dim dtBud As DataTable = mBudgetProvider.m_BudgetPIC_GetAll()
        grvBudgetPIC.DataSource = dtBud
        grvBudgetPIC.DataBind()
    End Sub

    Private Sub LoadGridInvoice()
        Dim dtItem As DataTable = mInvoiceItemProvider.GetAll()
        grvInvoiceItem.DataSource = dtItem
        grvInvoiceItem.DataBind()
    End Sub

    Private Sub LoadGridAirPeriod()
        Dim dtAir As DataTable = mAirPeriodProvider.GetAll()
        grvAirPeriod.DataSource = dtAir
        grvAirPeriod.DataBind()
    End Sub

    Private Sub LoadGridSupplier()
        Dim dtSup As DataTable = mOraSupplierProvider.GetAll()
        grvSupplier.DataSource = dtSup
        grvSupplier.DataBind()
    End Sub

    Private Sub LoadGetDataOraSupplier()
        Dim supplierName As String = txtSupplierName.Text.Trim()
        Dim supplierType As String = txtSupplierType.Text.Trim()
        Dim dtSup As DataTable = mOraSupplierProvider.GetDataOraSuplier(supplierName, supplierType)
        grvGetDataOraSupplier.DataSource = dtSup
        grvGetDataOraSupplier.DataBind()
    End Sub

    Private Sub LoadGridBatchName()
        Dim dtBat As DataTable = mBatchNameProvider.GetAll()
        grvBatchName.DataSource = dtBat
        grvBatchName.DataBind()
    End Sub

    Private Sub LoadGridDailyRate()
        Dim dtRate As DataTable = mOraDailyRateProvider.GetAll()
        grvDailyRate.DataSource = dtRate
        grvDailyRate.DataBind()
    End Sub

    Private Sub LoadGridCompanyName()
        Dim dtCom As DataTable = mCompanyProvider.GetAll()
        grvCompany.DataSource = dtCom
        grvCompany.DataBind()
    End Sub

    Private Sub LoadGridCountryGroup()
        Dim dtCountryGroup As DataTable = mCountryGroupProvider.GetAll()
        grvCountryGroup.DataSource = dtCountryGroup
        grvCountryGroup.DataBind()
    End Sub

    Private Sub LoadGridAllowance()
        Dim dtAll As DataTable = mMovingTimeAllowanceProvider.GetAll()
        grvAllowance.DataSource = dtAll
        grvAllowance.DataBind()
    End Sub

    Private Sub InitCurrency()
        Dim dt As DataTable = LookupProvider.GetByCode("POLICY_CURRENCY")
        CommonFunction.LoadLookupDataToComboBox(ddlCurrency, dt, True)
    End Sub

    Private Sub LoadCountryByID()
        Dim id As Integer = CommonFunction._ToInt(hCountryID.Value)
        Dim dtData As DataTable = mCountryProvider.m_Country_GetByID(id)
        If dtData.Rows.Count > 0 Then
            txtCountryCode.Text = dtData.Rows(0)("Code").ToString()
            txtCountryName.Text = dtData.Rows(0)("Name").ToString()
            CommonFunction.SetCBOValue(ddlCountryGroup, dtData.Rows(0)("GroupID"), False)
        End If
    End Sub

    Private Sub LoadDestinationByID()
        Dim id As Integer = CommonFunction._ToInt(hDestinationID.Value)
        Dim dtDes As DataTable = mDestinationProvider.m_Destination_GetByID(id)
        If dtDes.Rows.Count > 0 Then
            txtaDesName.Text = dtDes.Rows(0)("DestinationName").ToString()
            txtaDesNote.Text = dtDes.Rows(0)("Note").ToString()
            'cboCountry.Items.FindByValue(dtDes.Rows(0)("CountryID").ToString()).Selected = True
            CommonFunction.SetCBOValue(cboCountry, dtDes.Rows(0)("CountryID"), False)
            CommonFunction.SetCBOValue(ddlDesGroup, dtDes.Rows(0)("GroupID"), False)
            chkDestination.Checked = CommonFunction._ToBoolean(dtDes.Rows(0)("Status").ToString())
        End If
    End Sub

    Private Sub LoadDesGroupByID()
        Dim id As Integer = CommonFunction._ToInt(hDestinationGroupID.Value)
        Dim dtDesGroup As DataTable = mDestinationGroupProvider.m_Destination_Group_GetByID(id)
        If dtDesGroup.Rows.Count > 0 Then
            txtDesGroupName.Text = dtDesGroup.Rows(0)("GroupName").ToString()
            txtDesGroupDescription.Text = dtDesGroup.Rows(0)("Note").ToString()
            'cboCountry.Items.FindByValue(dtDes.Rows(0)("CountryID").ToString()).Selected = True
            ' CommonFunction.SetCBOValue(cboCountry, dtDes.Rows(0)("CountryID"), False)
            chkGroupStatus.Checked = CommonFunction._ToBoolean(dtDesGroup.Rows(0)("Status").ToString())
        End If
    End Sub

    Private Sub LoadTitleGroupByID()
        Dim id As Integer = CommonFunction._ToInt(hTitleGroupID.Value)
        Dim dtTitleGroup As DataTable = mTitleGroupProvider.m_Title_Group_GetByID(id)
        If dtTitleGroup.Rows.Count > 0 Then
            txtTitleGroupName.Text = dtTitleGroup.Rows(0)("Name").ToString()
            txtTitleGroupNote.Text = dtTitleGroup.Rows(0)("Note").ToString()
            ddlTitle.Text = dtTitleGroup.Rows(0)("GroupTitle").ToString()
            hTitleIDs.Value = dtTitleGroup.Rows(0)("TitleIDs").ToString()
            chkTitleGroup.Checked = CommonFunction._ToBoolean(dtTitleGroup.Rows(0)("Status").ToString())
        End If
    End Sub

    Private Sub LoadExpenseByID()
        Dim id As Integer = CommonFunction._ToInt(hExpense.Value)
        Dim dtExp As DataTable = mExpenseProvider.m_Expense_GetByID(id)
        If dtExp.Rows.Count > 0 Then
            txtExpBreakfast.Value = CommonFunction._ToMoneyWithNull(dtExp.Rows(0)("Breakfast"))
            txtExpDinner.Value = CommonFunction._ToMoneyWithNull(dtExp.Rows(0)("Dinner"))
            txtExpHotel.Value = CommonFunction._ToMoneyWithNull(dtExp.Rows(0)("Hotel"))
            txtExpLunch.Value = CommonFunction._ToMoneyWithNull(dtExp.Rows(0)("Lunch"))
            txtExpOtherMeal.Value = CommonFunction._ToMoneyWithNull(dtExp.Rows(0)("OtherMeal"))
            txtExpOther.Value = CommonFunction._ToMoneyWithNull(dtExp.Rows(0)("Other"))
            txtExpenseMotobike.Value = CommonFunction._ToMoneyWithNull(dtExp.Rows(0)("Motobike"))
            txtExpTransport.Value = CommonFunction._ToMoneyWithNull(dtExp.Rows(0)("Transportation"))
            txtGroupTitle.Text = CommonFunction._ToString(dtExp.Rows(0)("GroupTitle"))

            txtExpNote.Text = dtExp.Rows(0)("Note").ToString()
            dteExpenseEffectiveDate.Date = Date.Parse(dtExp.Rows(0)("EffectiveDate").ToString())
            'CommonFunction.SetCBOValue(ddlDestinationGroup, dtExp.Rows(0)("DestinationGroupID"), False)
            CommonFunction.SetCBOValue(ddlExpenseBTType, dtExp.Rows(0)("BTType"), False)
            ddlExpenseBTType.Enabled = False
            CommonFunction.SetCBOValue(ddlJobBand, dtExp.Rows(0)("TitleID"), False)
            CommonFunction.SetCBOValue(ddlCurrency, dtExp.Rows(0)("Currency"), False)
        End If
    End Sub

    Private Sub LoadSystemByID()
        Dim id As Integer = CommonFunction._ToInt(hSystemID.Value)
        Dim objSys As New tbl_LookupInfo
        objSys = LookupProvider.GetByID(id)
        If objSys IsNot Nothing Then
            txtCodeSys.Text = objSys.Code
            txtValueSys.Text = objSys.Value
            txtTextSys.Text = objSys.Text
            speOrder.Text = objSys.DisplayOrder
            chkActive.Checked = objSys.Active
        End If
    End Sub

    Private Sub LoadBudgetByID()
        Dim id As Integer = CommonFunction._ToInt(hBudget.Value)
        Dim dtBud As DataTable = mBudgetProvider.m_Budget_GetByID(id)

        If dtBud.Rows.Count > 0 Then
            txtBudgetCode.Text = dtBud.Rows(0)("BudgetCode").ToString()
            txtBudgetAmount.Value = CommonFunction._ToMoneyWithNull(dtBud.Rows(0)("Amount").ToString())
            txtBudgetName.Text = dtBud.Rows(0)("BudgetName").ToString()
            txtBudgetDes.Text = dtBud.Rows(0)("Description").ToString()
            chkBudgetActive.Checked = CommonFunction._ToBoolean(dtBud.Rows(0)("Active"))
            chkBudgetIsExecutive.Checked = CommonFunction._ToBoolean(dtBud.Rows(0)("IsExecutive"))
            txtOrg.Text = dtBud.Rows(0)("Org").ToString()
            CommonFunction.SetCBOValue(ddlDept, dtBud.Rows(0)("HRDepID"), False)
            CommonFunction.SetCBOValue(ddlBGType, dtBud.Rows(0)("Budget_Type"), False)
        End If
    End Sub

    Private Sub LoadBudgetPICByID()
        Dim id As Integer = CommonFunction._ToInt(hBudgetPIC.Value)
        Dim dtBud As DataTable = mBudgetProvider.m_BudgetPIC_GetByID(id)
        If dtBud.Rows.Count > 0 Then
            txtBudgetPICOrg.Text = dtBud.Rows(0)("Org").ToString()
            txtBudgetPICName.Text = dtBud.Rows(0)("PICName").ToString()
            txtBudgetPICEmail.Text = dtBud.Rows(0)("PICEmail").ToString()
        End If
    End Sub

    Private Sub LoadInvoiceItemByID()
        Dim id As Integer = CommonFunction._ToInt(hInvoiceItemID.Value)
        Dim dtItem As DataTable = mInvoiceItemProvider.m_InvoiceItem_GetByID(id)
        If dtItem.Rows.Count > 0 Then
            txtInvoiceItemName.Text = dtItem.Rows(0)("ItemName").ToString()
            txtInvoiceItemNote.Text = dtItem.Rows(0)("Note").ToString()
            chkInvItem.Checked = CommonFunction._ToBoolean(dtItem.Rows(0)("Status").ToString())
        End If
    End Sub

    Private Sub LoadAirPeriodByID()
        Dim id As Integer = CommonFunction._ToInt(hAirPeriodID.Value)
        Dim dtItem As DataTable = mAirPeriodProvider.m_AirPeriod_GetByID(id)
        If dtItem.Rows.Count > 0 Then
            txtAirName.Text = dtItem.Rows(0)("Name").ToString()
            txtAirDescription.Text = dtItem.Rows(0)("Description").ToString()
            chkAirActive.Checked = CommonFunction._ToBoolean(dtItem.Rows(0)("Active").ToString())
        End If
    End Sub

    Private Sub LoadBatchNameByID()
        Dim id As Integer = CommonFunction._ToInt(hBatchNameID.Value)
        Dim dtItem As DataTable = mBatchNameProvider.m_BatchName_GetByID(id)
        If dtItem.Rows.Count > 0 Then
            txtBatchName.Text = dtItem.Rows(0)("BatchName").ToString()
            txtBatchNameDescription.Text = dtItem.Rows(0)("Description").ToString()
            chkBatchNameActive.Checked = CommonFunction._ToBoolean(dtItem.Rows(0)("Active").ToString())
        End If
    End Sub

    Private Sub LoadDailyRateByID()
        Dim id As Integer = CommonFunction._ToInt(hDailyRateID.Value)
        Dim dtItem As DataTable = mOraDailyRateProvider.m_DailyRate_GetByID(id)
        If dtItem.Rows.Count > 0 Then
            CommonFunction.SetCBOValue(ddlFromCurrency, dtItem.Rows(0)("FROM_CURRENCY"), True)
            CommonFunction.SetCBOValue(ddlToCurrency, dtItem.Rows(0)("TO_CURRENCY"), True)
            speConversionRate.Text = dtItem.Rows(0)("CONVERSION_RATE").ToString()
            dteConversionDate.Date = Date.Parse(dtItem.Rows(0)("CONVERSION_DATE").ToString())
            chkDailyRateActive.Checked = CommonFunction._ToBoolean(dtItem.Rows(0)("Active").ToString())
        End If
    End Sub

    Private Sub LoadCompanyNameByID()
        Dim id As Integer = CommonFunction._ToInt(hCompanyID.Value)
        Dim dtItem As DataTable = mCompanyProvider.m_Company_GetByID(id)
        If dtItem.Rows.Count > 0 Then
            txtCompanyName.Text = dtItem.Rows(0)("Name").ToString()
            txtCompanyDescription.Text = dtItem.Rows(0)("Description").ToString()
            txtCompanyTaxCode.Text = dtItem.Rows(0)("TaxCode").ToString()
            chkCompanyCheck.Checked = CommonFunction._ToBoolean(dtItem.Rows(0)("Active").ToString())
        End If
    End Sub

    Private Sub LoadCountryGroupByID()
        Dim id As Integer = CommonFunction._ToInt(hCountryGroupID.Value)
        Dim dtCountryGroup As DataTable = mCountryGroupProvider.m_Country_Group_GetByID(id)
        If dtCountryGroup.Rows.Count > 0 Then
            txtCountryGroupName.Text = dtCountryGroup.Rows(0)("GroupName").ToString()
            txtCountryGroupDes.Text = dtCountryGroup.Rows(0)("Description").ToString()
            ChkCountryGroup.Checked = CommonFunction._ToBoolean(dtCountryGroup.Rows(0)("Status").ToString())
        End If
    End Sub

    Private Sub LoadAllowanceByID()
        Dim id As Integer = CommonFunction._ToInt(hAllowanceID.Value)
        Dim dtAll As DataTable = mMovingTimeAllowanceProvider.m_Allowance_GetByID(id)
        If dtAll.Rows.Count > 0 Then
            CommonFunction.SetCBOValue(ddlAllowanceCountryGroup, dtAll.Rows(0)("CountryGroup"))
            CommonFunction.SetCBOValue(ddlAllowanceCurrency, dtAll.Rows(0)("Currency"))
            speAllowanceAmount.Text = dtAll.Rows(0)("Amount").ToString()
            txtAllowanceDescription.Text = dtAll.Rows(0)("Description").ToString()
        End If
    End Sub

    Private Sub InitCountry()
        Dim dt As DataTable = mCountryProvider.GetAll
        CommonFunction.LoadDataToComboBox(cboCountry, dt, "Name", "ID", True, "Choose Country", "")
    End Sub

    Private Sub InitTitle()
        Dim dt As DataTable = LookupProvider.GetGroupTitle()
        CommonFunction.LoadDataToComboBox(ddlJobBand, dt, "Name", "TitleGroupID", True, "Choose Title Group", "")
    End Sub

    Public Sub InitGroupTitle()
        Try
            Dim dtGroupTitle As DataTable = mCountryProvider.GetTitleAll
            Dim lb As ASPxListBox = (ddlTitle.FindControl("ddlTitle"))
            lb.DataSource = dtGroupTitle
            lb.ValueField = "JobBandID"
            lb.TextField = "ShortName"
            lb.DataBind()
            lb.Items.Insert(0, New ListEditItem("Tất cả", "0"))
            '
            'Dim strBrConvert As String = CommonFunction._ToString(UserProvider.tbl_getBranchByID(hGroupTitle.Value))
            'ddlTitle.Text = strBrConvert
        Catch ex As Exception
            CommonFunction.ShowStartupErrorMessage(Me, ex.Message)
        End Try
    End Sub

    'Private Sub InitDestinationGroup()
    '    Dim dt As DataTable = mDestinationGroupProvider.GetAll()
    '    CommonFunction.LoadDataToComboBox(ddlDestinationGroup, dt, "GroupName", "GroupID", True, "Choose Group", "")
    'End Sub

    Private Sub InitCountryGroup()
        Dim dt As DataTable = mCountryGroupProvider.GetAll()
        CommonFunction.LoadDataToComboBox(ddlCountryGroup, dt, "GroupName", "ID", True, "Choose Group", "")
    End Sub

    Private Sub InitDesGroup()
        Dim dt As DataTable = mDestinationGroupProvider.GetAll()
        CommonFunction.LoadDataToComboBox(ddlDesGroup, dt, "GroupName", "GroupID", True, "Choose Group", "")
    End Sub

    Private Sub InitDepartment()
        Dim dt As DataTable = mBudgetProvider.DepartmentGetAll()
        CommonFunction.LoadDataToComboBox(ddlDept, dt, "DepartmentName", "DepartmentID", True, "Choose Department", "")
    End Sub

    'Private Sub InitOrg()
    '    Dim dt As DataTable = mBudgetProvider.DepartmentGetAll()
    '    CommonFunction.LoadDataToComboBox(ddlOrg, dt, "DepartmentName", "DepartmentID", True, "Choose Org", "")
    'End Sub

    Private Sub InitFromCurrency()
        Dim dtFrom As DataTable = mOraDailyRateProvider.GetActiveCurrency()
        CommonFunction.LoadDataToComboBox(ddlFromCurrency, dtFrom, "Text", "Value", True, "Please Choose Currency", "")
        CommonFunction.LoadDataToComboBox(ddlToCurrency, dtFrom, "Text", "Value", True, "Please Choose Currency", "")
    End Sub

    Private Sub InitAllowanceCurrency()
        Dim dt As DataTable = LookupProvider.GetByCode("POLICY_CURRENCY")
        CommonFunction.LoadLookupDataToComboBox(ddlAllowanceCurrency, dt, False)
    End Sub

    Private Sub InitGroupCountry()
        Dim dt As DataTable = mCountryGroupProvider.GetAll()
        CommonFunction.LoadDataToComboBox(ddlAllowanceCountryGroup, dt, "GroupName", "ID", True, "Choose Group Country", "")
    End Sub

#End Region

    Private Sub InitForm()

    End Sub

    Private Sub LoadForm()

    End Sub

#Region "Event Click"

    '-----------Country Master-----------
    Protected Sub btnSavecountry_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveCountry.Click
        CommonFunction.SetPostBackStatus(btnSaveCountry)
        Try
            Dim obj As New mCountryInfo
            obj.Name = txtCountryName.Text.Trim()
            obj.Code = txtCountryCode.Text.Trim()
            obj.GroupID = CommonFunction._ToInt(ddlCountryGroup.SelectedValue())

            If hCountryID.Value <> "" Then
                obj.ID = CommonFunction._ToInt(hCountryID.Value)
                mCountryProvider.m_Country_Update(obj)
                CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                CommonFunction.SetProcessStatus(btnSaveCountry, True)
                LoadGridCountry()
            Else
                Dim i As Integer = 0
                i = mCountryProvider.m_Country_Insert(obj)
                If i > 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveCountry, True)
                    LoadGridCountry()
                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditcountry_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveCountry)
        Try
            InitCountryGroup()
            LoadCountryByID()
            LoadGridCountry()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeletecountry_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveCountry)
        Try
            Dim id As Integer = CommonFunction._ToInt(hCountryID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            mCountryProvider.m_Country_Delete(id)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveCountry, True)
            LoadGridCountry()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try

    End Sub

    Protected Sub btnCancelcountry_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelCountry.Click
        CommonFunction.SetPostBackStatus(btnCancelCountry)
        Try
            txtCountryCode.Text = String.Empty
            txtCountryName.Text = String.Empty
            hCountryID.Value = String.Empty
            InitCountryGroup()
            'cboCountry.ClearSelection()            
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvCountry_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvCountry.BeforeGetCallbackResult
        LoadGridCountry()
    End Sub

    '-----------Destination Master-----------
    Protected Sub btnSaveDestination_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveDestination.Click
        CommonFunction.SetPostBackStatus(btnSaveDestination)
        Try
            Dim objDes As New mDestinationInfo
            objDes.Name = txtaDesName.Text
            objDes.Note = txtaDesNote.Text
            objDes.Status = chkDestination.Checked
            objDes.CountryID = CommonFunction._ToInt(cboCountry.SelectedValue())
            objDes.GroupID = CommonFunction._ToInt(ddlDesGroup.SelectedValue())

            If hDestinationID.Value <> "" Then
                objDes.DestinationID = CommonFunction._ToInt(hDestinationID.Value)
                mDestinationProvider.m_Destination_Upd(objDes)
                CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                CommonFunction.SetProcessStatus(btnSaveDestination, True)
                LoadGridDes()
            Else
                Dim i As Integer = 0
                i = mDestinationProvider.m_Destination_Insert(objDes)
                'Throw New Exception("aaaaaaaaa")
                If i > 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveDestination, True)
                    LoadGridDes()

                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditDestination_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveDestination)
        Try
            'hDestinationID.Value = btn.Attributes("data-id")
            InitCountry()
            InitDesGroup()
            LoadDestinationByID()
            LoadGridDes()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteDestination_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveDestination)
        Try
            Dim DesID As Integer = CommonFunction._ToInt(hDestinationID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            mDestinationProvider.m_Destination_Del(DesID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveDestination, True)
            LoadGridDes()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try

    End Sub

    Protected Sub btnCancelDestination_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelDestination.Click
        CommonFunction.SetPostBackStatus(btnCancelDestination)
        Try
            txtaDesName.Text = String.Empty
            txtaDesNote.Text = String.Empty
            hDestinationID.Value = String.Empty
            InitCountry()
            InitDesGroup()
            'cboCountry.ClearSelection()
            chkDestination.Checked = True
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvDes_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvDestination.BeforeGetCallbackResult
        LoadGridDes()
    End Sub

    '-----------System Master-----------
    Protected Sub btnSaveSystem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveSystem.Click
        CommonFunction.SetPostBackStatus(btnSaveSystem)
        Try
            Dim objSys As New tbl_LookupInfo
            objSys.Code = txtCodeSys.Text
            objSys.Value = txtValueSys.Text
            objSys.Text = txtTextSys.Text
            objSys.Active = chkActive.Checked
            objSys.DisplayOrder = speOrder.Value
            If hSystemID.Value <> "" Then
                objSys.ID = CommonFunction._ToInt(hSystemID.Value)
                LookupProvider.Update(objSys)
                CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                CommonFunction.SetProcessStatus(btnSaveSystem, True)
                LoadGridSystem()
            Else
                Dim i As Integer = 0
                i = LookupProvider.Insert(objSys)

                If i > 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveSystem, True)
                    LoadGridSystem()
                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditSystem_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveSystem)
        Try
            'hSystemID.Value = btn.Attributes("data-id")
            LoadSystemByID()
            LoadGridSystem()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteSystem_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveSystem)
        Try
            Dim SystemID As Integer = CommonFunction._ToInt(hSystemID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            LookupProvider.Delete(SystemID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveDestination, True)
            LoadGridSystem()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelSystemPara_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelSystem.Click
        CommonFunction.SetPostBackStatus(btnCancelSystem)
        Try
            txtCodeSys.Text = String.Empty
            txtTextSys.Text = String.Empty
            txtValueSys.Text = String.Empty
            chkActive.Checked = True
            speOrder.Value = 0
            hSystemID.Value = String.Empty
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvSystem_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvSystem.BeforeGetCallbackResult
        LoadGridSystem()
    End Sub

    '-----------Expense Master-----------
    Protected Sub btnSaveExpense_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveExpense.Click
        CommonFunction.SetPostBackStatus(btnSaveExpense)
        Try
            Dim objExp As New mExpenseInfo
            'objExp.DestinationGroupID = CommonFunction._ToInt(ddlDestinationGroup.SelectedValue())
            objExp.BTType = ddlExpenseBTType.SelectedValue
            objExp.TitleID = CommonFunction._ToInt(ddlJobBand.SelectedValue())
            objExp.Breakfast = CommonFunction._ToMoney(txtExpBreakfast.Text.Trim())
            objExp.Lunch = CommonFunction._ToMoney(txtExpLunch.Text.Trim())
            objExp.Dinner = CommonFunction._ToMoney(txtExpDinner.Text.Trim())
            objExp.OtherMeal = CommonFunction._ToMoney(txtExpOtherMeal.Text.Trim())
            objExp.Hotel = CommonFunction._ToMoney(txtExpHotel.Text.Trim())
            objExp.Transportation = CommonFunction._ToMoney(txtExpTransport.Text.Trim())
            objExp.Motobike = CommonFunction._ToMoney(txtExpenseMotobike.Text.Trim())
            objExp.Other = CommonFunction._ToMoney(txtExpOther.Text.Trim())
            objExp.Note = txtExpNote.Text
            objExp.Currency = ddlCurrency.Text
            objExp.EffectiveDate = dteExpenseEffectiveDate.Date

            If hExpense.Value <> "" Then
                objExp.ExpenseID = CommonFunction._ToInt(hExpense.Value)
                Dim message = mExpenseProvider.m_Expense_Upd(objExp)
                If message.Trim().Length = 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                    CommonFunction.SetProcessStatus(btnSaveExpense, True)
                    LoadGridExpense()
                Else
                    CommonFunction.ShowErrorMessage(panMessage, message)
                    CommonFunction.SetProcessStatus(btnSaveExpense, False)
                End If
            Else
                Dim message = mExpenseProvider.m_Expense_Insert(objExp)
                If message.Trim().Length = 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveExpense, True)
                    LoadGridExpense()
                Else
                    CommonFunction.ShowErrorMessage(panMessage, message)
                    CommonFunction.SetProcessStatus(btnSaveExpense, False)
                End If
            End If
            InitTitle()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditExpense_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveExpense)
        Try
            'hExpense.Value = btn.Attributes("data-id")
            'InitDestinationGroup()
            InitTitle()
            InitCurrency()
            LoadExpenseByID()
            LoadGridExpense()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteExpense_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveExpense)

        Try
            Dim ExpenseID As Integer = CommonFunction._ToInt(hExpense.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            mExpenseProvider.m_Expense_Del(ExpenseID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveExpense, True)
            LoadGridExpense()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try

    End Sub

    Protected Sub btnCancelExpense_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelExpense.Click
        CommonFunction.SetPostBackStatus(btnCancelExpense)
        Try
            hExpense.Value = ""
            txtExpBreakfast.Value = Nothing
            txtExpDinner.Value = Nothing
            txtExpHotel.Value = Nothing
            txtExpLunch.Value = Nothing
            txtExpOtherMeal.Value = Nothing
            txtExpNote.Text = String.Empty
            txtExpOther.Value = Nothing
            txtExpTransport.Value = Nothing
            dteExpenseEffectiveDate.Date = DateTime.Now
            'InitDestinationGroup()
            InitTitle()
            InitCurrency()
            'ddlDestinationGroup.ClearSelection()
            ddlExpenseBTType.ClearSelection()
            ddlExpenseBTType.Enabled = True
            'ddlJobBand.ClearSelection()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvExpense_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvExpense.BeforeGetCallbackResult
        LoadGridExpense()
    End Sub

    Protected Sub ddlExpenseBTType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlExpenseBTType.SelectedIndexChanged
        Try
            Dim bttype As String = ddlExpenseBTType.SelectedValue
            Dim currency As String = ""
            If bttype = "d" Then
                currency = "vnd"
            ElseIf bttype = "o" Then
                currency = "usd"
            End If
            CommonFunction.SetCBOValue(ddlCurrency, currency)
        Catch ex As Exception
        End Try
    End Sub

    '-----------Budget Master-----------
    Protected Sub btnSaveBudget_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveBudget.Click
        CommonFunction.SetPostBackStatus(btnSaveBudget)
        Try
            Dim objBud As New mBudgetInfo
            objBud.BudgetCode = txtBudgetCode.Text
            objBud.Amount = txtBudgetAmount.Value
            objBud.BudgetName = txtBudgetName.Text
            objBud.Description = txtBudgetDes.Text
            objBud.Active = chkBudgetActive.Checked
            objBud.Budget_Type = ddlBGType.SelectedValue

            objBud.Department = If(ddlDept.SelectedItem IsNot Nothing, ddlDept.SelectedItem.Text, "")
            objBud.Org = txtOrg.Text.Trim()
            objBud.HRDepID = CommonFunction._ToInt(ddlDept.SelectedValue)
            objBud.IsExecutive = chkBudgetIsExecutive.Checked

            If hBudget.Value <> "" Then
                objBud.BudgetID = CommonFunction._ToInt(hBudget.Value)
                mBudgetProvider.m_Budget_Upd(objBud)
                CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                CommonFunction.SetProcessStatus(btnSaveBudget, True)
                LoadGridBudget()
            Else
                Dim i As Integer = 0
                i = mBudgetProvider.m_Budget_Insert(objBud)
                If i > 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveBudget, True)
                    LoadGridBudget()
                End If
            End If

        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditBudget_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveBudget)
        Try
            'hBudget.Value = btn.Attributes("dataBudget-id")
            InitDepartment()
            LoadBudgetByID()
            LoadGridBudget()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteBudget_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveBudget)
        Try
            Dim BudgetID As Integer = CommonFunction._ToInt(hBudget.Value) 'CommonFunction._ToInt(btn.Attributes("dataBudget-id"))
            mBudgetProvider.m_Budget_Del(BudgetID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveBudget, True)
            LoadGridBudget()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelBudget_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelBudget.Click
        CommonFunction.SetPostBackStatus(btnCancelBudget)
        Try
            hBudget.Value = String.Empty
            txtBudgetAmount.Value = Nothing
            txtBudgetCode.Text = String.Empty
            txtBudgetName.Text = String.Empty
            txtBudgetDes.Text = String.Empty
            chkBudgetActive.Checked = True
            chkBudgetIsExecutive.Checked = False
            InitDepartment()
            'ddlDept.ClearSelection()
            'ddlOrg.ClearSelection()
            ddlBGType.ClearSelection()
            txtOrg.Text = ""
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvBudget_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBudget.BeforeGetCallbackResult
        LoadGridBudget()
    End Sub

    Protected Sub btnGetOraBudget_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetOraBudget.Click
        CommonFunction.SetPostBackStatus(btnGetOraBudget)
        Try
            mBudgetProvider.m_Budget_GetOraData()
            LoadGridBudget()
            LoadGridBudgetPIC()
            CommonFunction.ShowInfoMessage(panMessage, "Get oracle budget successfully!")
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    '-----------Budget PIC Master-----------
    Protected Sub btnSaveBudgetPIC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveBudgetPIC.Click
        CommonFunction.SetPostBackStatus(btnSaveBudgetPIC)
        Try
            Dim obj As New mBudgetPICInfo
            obj.Org = txtBudgetPICOrg.Text.Trim()
            obj.PICName = txtBudgetPICName.Text.Trim()
            obj.PICEmail = txtBudgetPICEmail.Text.Trim()

            If hBudgetPIC.Value <> "" Then
                obj.ID = CommonFunction._ToInt(hBudgetPIC.Value)
                mBudgetProvider.m_BudgetPIC_Upd(obj)
                CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                CommonFunction.SetProcessStatus(btnSaveBudgetPIC, True)
                LoadGridBudgetPIC()
            Else
                Dim i As Integer = 0
                i = mBudgetProvider.m_BudgetPIC_Insert(obj)
                If i > 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveBudgetPIC, True)
                    LoadGridBudgetPIC()
                End If
            End If

        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditBudgetPIC_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveBudgetPIC)
        Try
            'hBudgetPIC.Value = btn.Attributes("dataBudgetPIC-id")
            LoadBudgetPICByID()
            LoadGridBudgetPIC()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteBudgetPIC_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveBudgetPIC)
        Try
            Dim BudgetPICID As Integer = CommonFunction._ToInt(hBudgetPIC.Value) 'CommonFunction._ToInt(btn.Attributes("dataBudgetPIC-id"))
            mBudgetProvider.m_BudgetPIC_Del(BudgetPICID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveBudgetPIC, True)
            LoadGridBudgetPIC()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelBudgetPIC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelBudgetPIC.Click
        CommonFunction.SetPostBackStatus(btnCancelBudgetPIC)
        Try
            hBudgetPIC.Value = String.Empty
            txtBudgetPICOrg.Text = ""
            txtBudgetPICEmail.Text = String.Empty
            txtBudgetPICName.Text = String.Empty
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvBudgetPIC_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBudgetPIC.BeforeGetCallbackResult
        LoadGridBudgetPIC()
    End Sub

    '-----------Invoice Item Master-----------
    Protected Sub btnSaveItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveItem.Click
        CommonFunction.SetPostBackStatus(btnSaveItem)
        Try
            Dim objItem As New mInvoiceItemInfo
            objItem.ItemName = txtInvoiceItemName.Text
            objItem.Note = txtInvoiceItemNote.Text
            objItem.Status = chkInvItem.Checked

            If hInvoiceItemID.Value <> "" Then
                objItem.InvoiceItemID = CommonFunction._ToInt(hInvoiceItemID.Value)
                mInvoiceItemProvider.m_InvoiceItem_Upd(objItem)
                CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                CommonFunction.SetProcessStatus(btnSaveItem, True)
                LoadGridInvoice()
            Else
                Dim i As Integer = 0
                i = mInvoiceItemProvider.m_InvoiceItem_Insert(objItem)
                If i > 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveItem, True)
                    LoadGridInvoice()

                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditItem_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveItem)
        Try
            'hInvoiceItemID.Value = btn.Attributes("data-id")
            LoadInvoiceItemByID()
            LoadGridInvoice()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteItem_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveItem)

        Try
            Dim InvItemID As Integer = CommonFunction._ToInt(hInvoiceItemID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            mInvoiceItemProvider.m_InvoiceItem_Del(InvItemID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveItem, True)
            LoadGridInvoice()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try

    End Sub

    Protected Sub btnCancelItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelItem.Click
        CommonFunction.SetPostBackStatus(btnCancelItem)
        Try
            txtInvoiceItemName.Text = String.Empty
            txtInvoiceItemNote.Text = String.Empty
            chkInvItem.Checked = True
            hInvoiceItemID.Value = String.Empty
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvInvoiceItem_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvInvoiceItem.BeforeGetCallbackResult
        LoadGridInvoice()
    End Sub

    '-----------Destination Group Master-----------
    Protected Sub btnSaveDesGroup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveDesGroup.Click
        CommonFunction.SetPostBackStatus(btnSaveDesGroup)
        Try
            Dim objDesGroup As New mDestinationGroupInfo
            objDesGroup.GroupName = txtDesGroupName.Text
            objDesGroup.Note = txtDesGroupDescription.Text
            objDesGroup.Status = chkGroupStatus.Checked

            If hDestinationGroupID.Value <> "" Then
                objDesGroup.GroupID = CommonFunction._ToInt(hDestinationGroupID.Value)
                mDestinationGroupProvider.m_Destination_Group_Upd(objDesGroup)
                CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                CommonFunction.SetProcessStatus(btnSaveDesGroup, True)
                LoadGridDesGroup()
            Else
                Dim i As Integer = 0
                i = mDestinationGroupProvider.m_Destination_Group_Insert(objDesGroup)
                If i > 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveDesGroup, True)
                    LoadGridDesGroup()
                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditDesGroup_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveDesGroup)
        Try
            'hDestinationGroupID.Value = btn.Attributes("data-id")
            LoadDesGroupByID()
            LoadGridDesGroup()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteDesGroup_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveDesGroup)
        Try
            Dim DesGroupID As Integer = CommonFunction._ToInt(hDestinationGroupID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            mDestinationGroupProvider.m_Destination_GroupDel(DesGroupID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveDesGroup, True)
            LoadGridDesGroup()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try

    End Sub

    Protected Sub btnCancelDesGroup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelDesGroup.Click
        CommonFunction.SetPostBackStatus(btnCancelDesGroup)
        Try
            txtDesGroupName.Text = String.Empty
            txtDesGroupDescription.Text = String.Empty
            hDestinationGroupID.Value = String.Empty
            chkGroupStatus.Checked = True
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvDesGroup1_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvDesGroup.BeforeGetCallbackResult
        LoadGridDesGroup()
    End Sub

    '-----------Title Group Master-----------
    Protected Sub btnSaveTitleGroup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveTitleGroup.Click
        CommonFunction.SetPostBackStatus(btnSaveTitleGroup)
        Try
            Dim strBranch As String = String.Empty
            Dim strBrConvert As String = String.Empty

            Dim objTitleGroup As New mTitleGroupInfo
            objTitleGroup.Name = txtTitleGroupName.Text
            objTitleGroup.Note = txtTitleGroupNote.Text
            objTitleGroup.Status = chkGroupStatus.Checked
            objTitleGroup.GroupTitle = ddlTitle.Text

            objTitleGroup.TitleIDs = hTitleIDs.Value


            'If objTitleGroup.GroupTitle <> "" Then
            '    strBrConvert = UserProvider.tbl_getBranchByID(objTitleGroup.GroupTitle)
            '    ddlTitle.Text = strBrConvert
            '    hTitleGroupID.Value = objTitleGroup.GroupTitle
            'End If

            If hTitleGroupID.Value <> "" Then
                objTitleGroup.TitleGroupID = CommonFunction._ToInt(hTitleGroupID.Value)
                mTitleGroupProvider.m_Title_Group_Upd(objTitleGroup)
                CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                CommonFunction.SetProcessStatus(btnSaveTitleGroup, True)
                LoadGridTitleGroup()
            Else
                Dim i As Integer = 0
                i = mTitleGroupProvider.m_Title_Group_Insert(objTitleGroup)
                If i > 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveDesGroup, True)
                    LoadGridTitleGroup()
                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditTitleGroup_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveTitleGroup)
        Try
            'hTitleGroupID.Value = btn.Attributes("data-id")
            InitGroupTitle()
            LoadTitleGroupByID()
            LoadGridTitleGroup()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteTitleGroup_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveTitleGroup)
        Try
            Dim TitleGroupID As Integer = CommonFunction._ToInt(hTitleGroupID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            mTitleGroupProvider.m_Title_Group_Del(TitleGroupID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveTitleGroup, True)
            LoadGridTitleGroup()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try

    End Sub

    Protected Sub btnCancelTitleGroup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelTitleGroup.Click
        CommonFunction.SetPostBackStatus(btnCancelTitleGroup)
        Try
            txtTitleGroupName.Text = String.Empty
            txtTitleGroupNote.Text = String.Empty
            hTitleIDs.Value = String.Empty
            hTitleGroupID.Value = String.Empty
            chkTitleGroup.Checked = True
            InitGroupTitle()
            ddlTitle.Text = ""
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvTitleGroup_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvTitleGroup.BeforeGetCallbackResult
        LoadGridTitleGroup()
    End Sub

    '-----------Air Period -----------
    Protected Sub btnSaveAirPeriod_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveAirPeriod.Click
        CommonFunction.SetPostBackStatus(btnSaveAirPeriod)
        Try
            Dim objAir As New mAirPeriodInfo
            objAir.Name = txtAirName.Text
            objAir.Description = txtAirDescription.Text
            objAir.Active = chkAirActive.Checked


            If hAirPeriodID.Value <> "" Then
                objAir.ID = CommonFunction._ToInt(hAirPeriodID.Value)
                mAirPeriodProvider.m_AirPeriod_Upd(objAir)
                CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                CommonFunction.SetProcessStatus(btnSaveAirPeriod, True)
                LoadGridAirPeriod()
            Else
                Dim i As Integer = 0
                i = mAirPeriodProvider.m_AirPeriod_Insert(objAir)
                If i > 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveAirPeriod, True)
                    LoadGridAirPeriod()

                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditAirPeriod_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveAirPeriod)
        Try
            'hAirPeriodID.Value = btn.Attributes("data-id")
            LoadAirPeriodByID()
            LoadGridAirPeriod()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteAirPeriod_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveAirPeriod)
        Try
            Dim ID As Integer = CommonFunction._ToInt(hAirPeriodID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            mAirPeriodProvider.m_AirPeriod_Del(ID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveAirPeriod, True)
            LoadGridAirPeriod()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try

    End Sub

    Protected Sub btnCancelAirPeriod_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelAirPeriod.Click
        CommonFunction.SetPostBackStatus(btnCancelAirPeriod)
        Try
            txtAirName.Text = String.Empty
            txtAirDescription.Text = String.Empty
            chkAirActive.Checked = True
            hAirPeriodID.Value = String.Empty
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvAirPeriod_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvAirPeriod.BeforeGetCallbackResult
        LoadGridAirPeriod()
    End Sub

    '-----------Supplier -----------
    Protected Sub btnSaveSupplier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveSupplier.Click
        CommonFunction.SetPostBackStatus(btnSaveSupplier)
        Try
            Dim supplierNos As String = hSelectSuppliers.Value
            For Each sup As String In supplierNos.Split(";")
                Dim obj As New mOraSupplierInfo()
                obj.Active = True
                obj.OraLink = sup.Split("|")(0)
                obj.SupplierName = sup.Split("|")(1)
                mOraSupplierProvider.m_OraSupplier_Insert(obj)
            Next
            CommonFunction.ShowInfoMessage(panMessage, "Your data is saved successfully!")
            CommonFunction.SetProcessStatus(btnSaveSupplier, True)
            LoadGridSupplier()
            LoadGetDataOraSupplier()
        Catch ex As Exception
            CommonFunction.SetProcessStatus(btnSaveSupplier, False)
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteSupplier_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveSupplier)
        Try
            Dim ID As Integer = CommonFunction._ToInt(hOraSupplierID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            mOraSupplierProvider.m_OraSupplier_Del(ID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveSupplier, True)
            LoadGridSupplier()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try

    End Sub

    Protected Sub btnCancelSupplier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelSupplier.Click
        CommonFunction.SetPostBackStatus(btnCancelSupplier)
        Try

        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnSearchSupplier_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearchSupplier.Click
        CommonFunction.SetPostBackStatus(btnSearchSupplier)
        Try
            LoadGetDataOraSupplier()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvSupplier_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvSupplier.BeforeGetCallbackResult
        LoadGridSupplier()
    End Sub

    Protected Sub grvGetDataOraSupplier_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvGetDataOraSupplier.BeforeGetCallbackResult
        LoadGetDataOraSupplier()
    End Sub

    '-----------Batch Name -----------
    Protected Sub btnSaveBatchName_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveBatchName.Click
        CommonFunction.SetPostBackStatus(btnSaveBatchName)
        Try
            Dim objBatch As New mBatchNameInfo
            objBatch.BatchName = txtBatchName.Text
            objBatch.Description = txtBatchNameDescription.Text
            objBatch.Active = chkBatchNameActive.Checked


            If hBatchNameID.Value <> "" Then
                objBatch.ID = CommonFunction._ToInt(hBatchNameID.Value)
                mBatchNameProvider.m_BatchName_Upd(objBatch)
                CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                CommonFunction.SetProcessStatus(btnSaveBatchName, True)
                LoadGridBatchName()
            Else
                Dim i As Integer = 0
                i = mBatchNameProvider.m_BatchName_Insert(objBatch)
                If i > 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveBatchName, True)
                    LoadGridBatchName()

                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditBatchName_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveBatchName)
        Try
            'hBatchNameID.Value = btn.Attributes("data-id")
            LoadBatchNameByID()
            LoadGridBatchName()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteBatchName_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveBatchName)

        Try
            Dim ID As Integer = CommonFunction._ToInt(hBatchNameID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            mBatchNameProvider.m_BatchName_Del(ID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveBatchName, True)
            LoadGridBatchName()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try

    End Sub

    Protected Sub btnCancelBatchName_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelBatchName.Click
        CommonFunction.SetPostBackStatus(btnCancelBatchName)
        Try
            txtBatchName.Text = String.Empty
            txtBatchNameDescription.Text = String.Empty
            chkBatchNameActive.Checked = True
            hBatchNameID.Value = String.Empty

        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvBatchName_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBatchName.BeforeGetCallbackResult
        LoadGridBatchName()
    End Sub

    '-----------Daily Rates -----------
    Protected Sub btnSaveDailyRate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveDailyRate.Click
        CommonFunction.SetPostBackStatus(btnSaveDailyRate)
        Try
            Dim objRate As New mOraDailyRateInfo
            objRate.FROM_CURRENCY = ddlFromCurrency.SelectedItem.Text
            objRate.TO_CURRENCY = ddlToCurrency.SelectedItem.Text
            objRate.CONVERSION_DATE = dteConversionDate.Date
            objRate.CONVERSION_RATE = speConversionRate.Value

            objRate.Active = chkDailyRateActive.Checked


            If hDailyRateID.Value <> "" Then
                objRate.ID = CommonFunction._ToInt(hDailyRateID.Value)
                mOraDailyRateProvider.m_DailyRate_Upd(objRate)
                CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                CommonFunction.SetProcessStatus(btnSaveBatchName, True)
                LoadGridDailyRate()
            Else
                Dim i As Integer = 0
                i = mOraDailyRateProvider.m_DailyRate_Insert(objRate)
                If i > 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveDailyRate, True)
                    LoadGridDailyRate()

                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditDailyRate_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveDailyRate)
        Try
            'hDailyRateID.Value = btn.Attributes("data-id")
            InitFromCurrency()
            LoadDailyRateByID()
            LoadGridDailyRate()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteDailyRate_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveDailyRate)

        Try
            Dim ID As Integer = CommonFunction._ToInt(hDailyRateID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            mOraDailyRateProvider.m_DailyRate_Del(ID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveDailyRate, True)
            LoadGridDailyRate()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try

    End Sub

    Protected Sub btnCancelDailyRate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelDailyRate.Click
        CommonFunction.SetPostBackStatus(btnCancelDailyRate)
        Try
            dteConversionDate.Date = Date.Now
            speConversionRate.Value = Nothing
            chkDailyRateActive.Checked = True
            hDailyRateID.Value = String.Empty
            InitFromCurrency()
            'ddlFromCurrency.ClearSelection()
            'ddlToCurrency.ClearSelection()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnGetOraExrate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetOraExrate.Click
        CommonFunction.SetPostBackStatus(btnGetOraExrate)
        Try
            mOraDailyRateProvider.GetOraDailyExrate()
            LoadGridDailyRate()
            CommonFunction.ShowInfoMessage(panMessage, "Get oracle daily exchange rates successfully!")
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvDailyRate_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvDailyRate.BeforeGetCallbackResult
        LoadGridDailyRate()
    End Sub

    '-----------Company Name -----------
    Protected Sub btnSaveCompany_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveCompany.Click
        CommonFunction.SetPostBackStatus(btnSaveCompany)
        Try
            Dim objCompany As New mCompanyInfo
            objCompany.Name = txtCompanyName.Text
            objCompany.TaxCode = txtCompanyTaxCode.Text
            objCompany.Description = txtCompanyDescription.Text
            objCompany.Active = chkCompanyCheck.Checked


            If hCompanyID.Value <> "" Then
                objCompany.ID = CommonFunction._ToInt(hCompanyID.Value)
                mCompanyProvider.m_Company_Upd(objCompany)
                CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                CommonFunction.SetProcessStatus(btnSaveCompany, True)
                LoadGridCompanyName()
            Else
                Dim i As Integer = 0
                i = mCompanyProvider.m_Company_Insert(objCompany)
                If i > 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveCompany, True)
                    LoadGridCompanyName()

                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditCompanyName_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveCompany)
        Try
            'hCompanyID.Value = btn.Attributes("data-id")
            LoadCompanyNameByID()
            LoadGridCompanyName()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteCompanyName_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveCompany)

        Try
            Dim ID As Integer = CommonFunction._ToInt(hCompanyID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            mCompanyProvider.m_Company_Del(ID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveCompany, True)
            LoadGridCompanyName()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try

    End Sub

    Protected Sub btnCancelCompanyName_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelCompany.Click
        CommonFunction.SetPostBackStatus(btnCancelCompany)
        Try
            txtCompanyName.Text = String.Empty
            txtCompanyDescription.Text = String.Empty
            txtCompanyTaxCode.Text = String.Empty
            chkBatchNameActive.Checked = True
            hCompanyID.Value = String.Empty

        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvCompanyName_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvCompany.BeforeGetCallbackResult
        LoadGridCompanyName()
    End Sub

    '-----------Country Group Master-----------
    Protected Sub btnSaveCountryGroup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveCountryGroup.Click
        CommonFunction.SetPostBackStatus(btnSaveCountryGroup)
        Try
            Dim objCountryGroup As New mCountryGroupInfo
            objCountryGroup.GroupName = txtCountryGroupName.Text
            objCountryGroup.Desc = txtCountryGroupDes.Text
            objCountryGroup.IsActive = ChkCountryGroup.Checked

            If hCountryGroupID.Value <> "" Then
                objCountryGroup.ID = CommonFunction._ToInt(hCountryGroupID.Value)
                mCountryGroupProvider.m_Country_Group_Upd(objCountryGroup)
                CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                CommonFunction.SetProcessStatus(btnSaveCountryGroup, True)
                LoadGridCountryGroup()
            Else
                Dim i As Integer = 0
                i = mCountryGroupProvider.m_Country_Group_Insert(objCountryGroup)
                If i > 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveCountryGroup, True)
                    LoadGridCountryGroup()
                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditCountryGroup_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveCountryGroup)
        Try
            'hCountryGroupID.Value = btn.Attributes("data-id")
            LoadCountryGroupByID()
            LoadGridCountryGroup()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteCountryGroup_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveCountryGroup)
        Try
            Dim CountryGroupID As Integer = CommonFunction._ToInt(hCountryGroupID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            mCountryGroupProvider.m_Country_GroupDel(CountryGroupID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveCountryGroup, True)
            LoadGridCountryGroup()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try

    End Sub

    Protected Sub btnCancelCountryGroup_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelCountryGroup.Click
        CommonFunction.SetPostBackStatus(btnCancelCountryGroup)
        Try
            txtCountryGroupName.Text = String.Empty
            txtCountryGroupDes.Text = String.Empty
            hCountryGroupID.Value = String.Empty
            ChkCountryGroup.Checked = True
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvCountryGroup_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvCountryGroup.BeforeGetCallbackResult
        LoadGridCountryGroup()
    End Sub

    '-----------Moving Allowance Master-----------
    Protected Sub btnSaveAllowance_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveAllowance.Click
        CommonFunction.SetPostBackStatus(btnSaveAllowance)
        Try
            Dim obj As New mMovingTimeAllowanceInfo
            obj.CountryGroup = CommonFunction._ToInt(ddlAllowanceCountryGroup.SelectedValue())
            obj.Currency = ddlAllowanceCurrency.SelectedValue
            obj.Amount = CommonFunction._ToMoney(speAllowanceAmount.Value)
            obj.Description = txtAllowanceDescription.Text

            If hAllowanceID.Value <> "" Then
                obj.ID = CommonFunction._ToInt(hAllowanceID.Value)
                mMovingTimeAllowanceProvider.m_Allowance_Upd(obj)
                CommonFunction.ShowInfoMessage(panMessage, "Updated successfully!")
                CommonFunction.SetProcessStatus(btnSaveAllowance, True)
                LoadGridAllowance()
            Else
                Dim i As Integer = 0
                i = mMovingTimeAllowanceProvider.m_Allowance_Insert(obj)
                If i > 0 Then
                    CommonFunction.ShowInfoMessage(panMessage, "Created successfully!")
                    CommonFunction.SetProcessStatus(btnSaveAllowance, True)
                    LoadGridAllowance()
                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditAllowance_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveAllowance)
        Try
            'hAllowanceID.Value = btn.Attributes("data-id")
            InitAllowanceCurrency()
            InitGroupCountry()
            LoadAllowanceByID()
            LoadGridAllowance()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteAllowance_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSaveAllowance)
        Try
            Dim AllowanceID As Integer = CommonFunction._ToInt(hAllowanceID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            mMovingTimeAllowanceProvider.m_Allowance_Del(AllowanceID)
            CommonFunction.ShowInfoMessage(panMessage, "Deleted successfully!")
            CommonFunction.SetProcessStatus(btnSaveAllowance, True)
            LoadGridAllowance()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelAllowance_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelAllowance.Click
        CommonFunction.SetPostBackStatus(btnCancelAllowance)
        Try
            speAllowanceAmount.Value = Nothing
            hAllowanceID.Value = String.Empty
            InitAllowanceCurrency()
            InitGroupCountry()
            'ddlAllowanceCountryGroup.ClearSelection()
            'ddlAllowanceCurrency.ClearSelection()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvAllowance_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvAllowance.BeforeGetCallbackResult
        LoadGridAllowance()
    End Sub

    ' Index Changed
    Protected Sub cboTitle_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlJobBand.SelectedIndexChanged
        Try
            Dim TitleID As Integer = -1
            TitleID = CommonFunction._ToInt(ddlJobBand.SelectedValue)
            Dim dt As DataTable
            dt = mTitleGroupProvider.m_Title_Group_GetByID(TitleID)
            If dt IsNot Nothing Then
                txtGroupTitle.Text = CommonFunction._ToString(dt.Rows(0)("GroupTitle"))
            Else
                txtGroupTitle.Text = String.Empty
            End If
        Catch ex As Exception
        End Try
    End Sub

    'Protected Sub btnImportSub_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoadBudget.Click
    '    LoadGridBudget()
    'End Sub

    Private Sub LoadDataToAllArray()
        Try
            dctEmployeeCode.Clear()
            dctOT.Clear()
            dctOTDate.Clear()
            dctOvertimetypeCode.Clear()
            dctNote.Clear()
            dtError = New Data.DataTable
            dtError.Columns.Add("BudgetName")
            dtError.Columns.Add("BudgetCode")
            dtError.Columns.Add("Amount")
            dtError.Columns.Add("Org")
            dtError.Columns.Add("Department")
            dtError.Columns.Add("Description")
            dtError.Columns.Add("Reason")
            'Dim i As Integer
            'Dim j As Integer
            '            For i = 0 To dtExcel.Rows.Count - 1
            '                If IsDBNull(dtExcel.Rows(i)("EmployeeCode")) Or (dtExcel.Rows(i)("EmployeeCode")) Is Nothing Or _
            '                 IsDBNull(dtExcel.Rows(i)("OverTimeDate")) Or (dtExcel.Rows(i)("OverTimeDate")) Is Nothing Or _
            '                 IsDBNull(dtExcel.Rows(i)("OverTimeMinutes")) Or (dtExcel.Rows(i)("OverTimeMinutes")) Is Nothing Or _
            '                 IsDBNull(dtExcel.Rows(i)("OverTimeType")) Or (dtExcel.Rows(i)("OverTimeType")) Is Nothing Then
            '                    Dim dr As Data.DataRow
            '                    dr = dtError.NewRow
            '                    dr("EmployeeCode") = dtExcel.Rows(i)("EmployeeCode")
            '                    dr("OverTimeDate") = dtExcel.Rows(i)("OverTimeDate")
            '                    dr("OverTimeMinutes") = dtExcel.Rows(i)("OverTimeMinutes")
            '                    dr("OverTimeType") = dtExcel.Rows(i)("OverTimeType")
            '                    dr("Note") = dtExcel.Rows(i)("Note")
            '                    dr("Reason") = "EmployeeCode/OverTimeDate/OverTimeMinutes/OverTimeType không được để trống"
            '                    dtError.Rows.Add(dr)
            '                    Continue For
            '                End If
            '                If Not IsDate(dtExcel.Rows(i)("OverTimeDate")) Then
            '                    Dim dr As Data.DataRow
            '                    dr = dtError.NewRow
            '                    dr("EmployeeCode") = dtExcel.Rows(i)("EmployeeCode")
            '                    dr("OverTimeDate") = dtExcel.Rows(i)("OverTimeDate")
            '                    dr("OverTimeMinutes") = dtExcel.Rows(i)("OverTimeMinutes")
            '                    dr("OverTimeType") = dtExcel.Rows(i)("OverTimeType")
            '                    dr("Note") = dtExcel.Rows(i)("Note")
            '                    dr("Reason") = "Sai định dạng ngày OverTimeDate"
            '                    dtError.Rows.Add(dr)
            '                    Continue For
            '                End If




            '                If i = 0 Then
            '                    dctEmployeeCode.Add(i, dtExcel.Rows(i)("EmployeeCode").ToString.Trim)
            '                    dctOTDate.Add(i, dtExcel.Rows(i)("OverTimeDate").ToString.Trim)
            '                    dctOT.Add(i, dtExcel.Rows(i)("OverTimeMinutes").ToString.Trim)
            '                    dctOvertimetypeCode.Add(i, dtExcel.Rows(i)("OverTimeType").ToString.Trim)
            '                    dctNote.Add(i, dtExcel.Rows(i)("Note").ToString.Trim)
            '                Else
            '                    Dim blnExist As Boolean
            '                    blnExist = False
            '                    For j = 0 To dctEmployeeCode.Count - 1
            '                        If dtExcel.Rows(i)("EmployeeCode").ToString.Trim = dctEmployeeCode(j) _
            '                        And dtExcel.Rows(i)("OverTimeDate").ToString.Trim = dctOTDate(j) _
            '                        And dtExcel.Rows(i)("OverTimeMinutes").ToString.Trim = dctOT(j) _
            '                        And dtExcel.Rows(i)("OverTimeType").ToString.Trim = dctOvertimetypeCode(j) _
            'Then

            '                            Dim dr As Data.DataRow
            '                            dr = dtError.NewRow
            '                            dr("EmployeeCode") = dtExcel.Rows(i)("EmployeeCode")
            '                            dr("OverTimeDate") = dtExcel.Rows(i)("OverTimeDate")
            '                            dr("OverTimeMinutes") = dtExcel.Rows(i)("OverTimeMinutes")
            '                            dr("OverTimeType") = dtExcel.Rows(i)("OverTimeType")
            '                            dr("Note") = dtExcel.Rows(i)("Note")
            '                            dr("Reason") = "Trùng bản ghi giống nhau"
            '                            dtError.Rows.Add(dr)

            '                            blnExist = True
            '                            Exit For
            '                        End If
            '                    Next
            '                    If Not blnExist Then
            '                        dctEmployeeCode.Add(dctEmployeeCode.Count, dtExcel.Rows(i)("EmployeeCode").ToString.Trim)
            '                        dctOTDate.Add(dctOTDate.Count, dtExcel.Rows(i)("OverTimeDate").ToString.Trim)
            '                        dctOT.Add(dctOT.Count, dtExcel.Rows(i)("OverTimeMinutes").ToString.Trim)
            '                        dctOvertimetypeCode.Add(dctOvertimetypeCode.Count, dtExcel.Rows(i)("OverTimeType").ToString.Trim)
            '                        dctNote.Add(dctNote.Count, dtExcel.Rows(i)("Note").ToString.Trim)

            '                    End If
            '                End If
            'Next
        Catch ex As Exception
            ' MessageBox.Show("Có lỗi: " + ex.Message.ToString, "Import dữ liệu", MessageBoxButtons.OK)
        End Try
    End Sub

#End Region
End Class