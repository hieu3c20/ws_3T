Imports Provider
Imports DevExpress.Web.ASPxGridView

Partial Public Class FiBudgetChecking
    Inherits System.Web.UI.Page
    Dim strUser As String = String.Empty
    Dim _dtData As DataTable = New DataTable()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CommonFunction.CheckSession()
        CommonFunction.CheckRole(RoleType.Administrator, RoleType.Finance_Budget)
        strUser = Session("UserName").ToString()
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
            Dim checkedPageSize As Integer = CommonFunction._ToUnsignInt(Request.QueryString("cpsize"))
            If checkedPageSize < 1 Then
                checkedPageSize = 100
            End If
            grvBTChecked.SettingsPager.PageSize = checkedPageSize
            Dim submitPageSize As Integer = CommonFunction._ToUnsignInt(Request.QueryString("spsize"))
            If submitPageSize < 1 Then
                submitPageSize = 100
            End If
            grvBTCompleted.SettingsPager.PageSize = submitPageSize
            Dim registerPage As Integer = CommonFunction._ToUnsignInt(Request.QueryString("rpage"))
            grvBTRegister.PageIndex = registerPage
            Dim rejectedPage As Integer = CommonFunction._ToUnsignInt(Request.QueryString("jpage"))
            grvBTRejected.PageIndex = rejectedPage
            Dim checkedPage As Integer = CommonFunction._ToUnsignInt(Request.QueryString("cpage"))
            grvBTChecked.PageIndex = checkedPage
            Dim submitPage As Integer = CommonFunction._ToUnsignInt(Request.QueryString("spage"))
            grvBTCompleted.PageIndex = submitPage
        End If
    End Sub

    Private Sub InitBusinessTrip()
        Dim dtBT As DataTable
        dtBT = LookupProvider.GetByCode("BT_Type")
        CommonFunction.LoadLookupDataToComboBox(ddlBTType, dtBT, True, "All", "")
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
        Dim btNo As String = String.Empty
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
        _dtData = BusinessTripProvider.tbl_BT_Register_SearchBudget(BTTypeID, LocationID, DivID, DepID, SecID, GropID, FullName, EmployeeCode, FromDate, ToDate, "", -1, "", strUser, False, btNo, btsStatus)
    End Sub

    Public Sub LoadDataGrid()
        GetDataTable()
        LoadRegister()
        LoadRejected()
        LoadChecked()
        LoadCompleted()
        LoadCancelled()
    End Sub

    Private Sub LoadRegister()
        CommonFunction.LoadDataToGrid(grvBTRegister, _dtData, "IsSubmited = 1 and FIStatus <> '" + FIStatus.checked.ToString() + "' and FIStatus <> '" + FIStatus.completed.ToString() + "' and FIStatus <> '" + FIStatus.rejected.ToString() + "' and FIStatus <> '" + FIStatus.cancelled.ToString() + "'", "No")
    End Sub

    Private Sub LoadRejected()
        CommonFunction.LoadDataToGrid(grvBTRejected, _dtData, "IsSubmited = 1 and FIStatus = '" + FIStatus.rejected.ToString() + "'", "No")
    End Sub

    Private Sub LoadChecked()
        CommonFunction.LoadDataToGrid(grvBTChecked, _dtData, "IsSubmited = 1 and FIStatus = '" + FIStatus.checked.ToString() + "'", "No")
    End Sub

    Private Sub LoadCompleted()
        CommonFunction.LoadDataToGrid(grvBTCompleted, _dtData, "IsSubmited = 1 and FIStatus = '" + FIStatus.completed.ToString() + "'", "No")
    End Sub

    Private Sub LoadCancelled()
        CommonFunction.LoadDataToGrid(grvBTCancelled, _dtData, "IsSubmited = 1 and FIStatus = '" + FIStatus.cancelled.ToString() + "'", "No")
    End Sub

    Protected Sub grvBTRegister_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTRegister.BeforeGetCallbackResult
        GetDataTable()
        LoadRegister()
    End Sub

    Protected Sub grvBTRejected_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTRejected.BeforeGetCallbackResult
        GetDataTable()
        LoadRejected()
    End Sub

    Protected Sub grvBTChecked_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTChecked.BeforeGetCallbackResult
        GetDataTable()
        LoadChecked()
    End Sub

    Protected Sub grvBTCompleted_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTCompleted.BeforeGetCallbackResult
        GetDataTable()
        LoadCompleted()
    End Sub

    Protected Sub grvBTCancelled_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTCancelled.BeforeGetCallbackResult
        GetDataTable()
        LoadCancelled()
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

    Protected Sub btnView_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            'Dim btn As Button = CType(sender, Button)
            Dim btID As Integer = CommonFunction._ToInt(hItemID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))            
            Dim dtRegister As DataTable = BusinessTripProvider.BTRegister_GetByID(btID)
            If dtRegister.Rows.Count > 0 Then
                Dim btType As String = CommonFunction._ToString(dtRegister.Rows(0)("BTType"))
                Dim postBackUrl As New StringBuilder()
                postBackUrl.Append(If(btType.IndexOf("oneday_") = 0, If(CommonFunction._ToInt(dtRegister.Rows(0)("IsOneDayExpense")) = 1, "~/BTOneDayExpenseDeclaration.aspx", "~/BTOneDayDeclaration.aspx"), "~/BTAdvanceDeclaration.aspx"))
                Dim params As String = String.Format( _
                    "btid={0}&bttype={1}&loc={2}&fdate={3}&tdate={4}&div={5}&dep={6}&sec={7}&group={8}&ecode={9}&ename={10}&rpage={11}&rpsize={12}&btno={13}&cpage={14}&cpsize={15}&spage={16}&spsize={17}&jpage={18}&jpsize={19}", _
                    btID, hBTType.Value, hLocation.Value, hFrom.Value, hTo.Value, hDivision.Value, hDept.Value, hSection.Value, hGroup.Value, hEmployeeCode.Value, hFullName.Value, grvBTRegister.PageIndex, grvBTRegister.SettingsPager.PageSize, _
                    hBTNo.Value, grvBTChecked.PageIndex, grvBTChecked.SettingsPager.PageSize, grvBTCompleted.PageIndex, grvBTCompleted.SettingsPager.PageSize, grvBTRejected.PageIndex, grvBTRejected.SettingsPager.PageSize)
                postBackUrl.Append(String.Format("?id={0}&back=FiBudgetChecking.aspx&params={1}", btID, params.Replace("&", ";amp;").Replace("=", ";eq;")))
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
            'Dim budgetChecked As String = e.GetValue("BudgetChecked")
            If status = FIStatus.rejected.ToString() OrElse status = FIStatus.budget_rejected.ToString() Then
                e.Row.CssClass &= " rejected"
            ElseIf status = FIStatus.budget_reconfirmed.ToString() Then
                e.Row.CssClass &= " waiting"
            End If
        End If
    End Sub

End Class