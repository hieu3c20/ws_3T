Imports Provider
Imports DevExpress.Web.ASPxGridView

Partial Public Class HrManagement
    Inherits System.Web.UI.Page
    Dim strUser As String = String.Empty
    Dim dtBTSearch As DataTable
    Dim dtRegister As DataTable
    Dim dtSubmited As DataTable
    Dim _dtData As New DataTable()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CommonFunction.CheckSession()
        CommonFunction.CheckRole(RoleType.Administrator, RoleType.HR)
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
            SetPreSearchCondition()
            LoadDataGrid()
            Dim registerPageSize As Integer = CommonFunction._ToUnsignInt(Request.QueryString("rpsize"))
            If registerPageSize < 1 Then
                registerPageSize = 20
            End If
            grvBTRegister.SettingsPager.PageSize = registerPageSize
            Dim submitPageSize As Integer = CommonFunction._ToUnsignInt(Request.QueryString("spsize"))
            If submitPageSize < 1 Then
                submitPageSize = 20
            End If
            grvBTSubmitted.SettingsPager.PageSize = submitPageSize
            Dim registerPage As Integer = CommonFunction._ToUnsignInt(Request.QueryString("rpage"))
            grvBTRegister.PageIndex = registerPage
            Dim submitPage As Integer = CommonFunction._ToUnsignInt(Request.QueryString("spage"))
            grvBTSubmitted.PageIndex = submitPage
        End If
    End Sub

    Private Sub InitBusinessTrip()
        Dim dtBT As DataTable
        'dtBT = BusinessTripProvider.tbl_BT_Type()
        dtBT = LookupProvider.GetByCode("BT_Type")
        CommonFunction.LoadLookupDataToComboBox(ddlBTType, dtBT, True, "All", "")
        'ddlBTType.DataSource = dtBT
        'ddlBTType.DataValueField = "ID"
        'ddlBTType.DataTextField = "Value"
        'ddlBTType.DataBind()
        'ddlBTType.Items.Insert(0, New ListItem("Choose Business Type ...", ""))
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
        'CommonFunction.LoadLookupDataToComboBox(cboLocation, dtBranch)
        cboLocation.DataSource = dtBranch
        cboLocation.DataValueField = "BranchID"
        cboLocation.DataTextField = "BranchName"
        cboLocation.DataBind()
        cboLocation.Items.Insert(0, New ListItem("All", ""))

    End Sub

    'Private Sub LoadGrid()
    '    Dim dtRe As New DataTable()
    '    dtRe = BusinessTripProvider.tbl_BT_Register_GetAllByUserName(strUser, 4)
    '    grvBTRegister.DataSource = dtRe
    '    grvBTRegister.DataBind()
    '    dtRegister = dtRe

    '    Dim dtRequest As New DataTable()
    '    dtRequest = BusinessTripProvider.tbl_BT_Register_GetAllByUserName(strUser, 7)
    '    grvBTSubmitted.DataSource = dtRequest
    '    grvBTSubmitted.DataBind()
    '    dtSubmited = dtRequest
    'End Sub

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
    End Sub
#End Region

#Region "Event"
    Protected Sub btnSearch_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click

        Try
            SetPreSearchCondition()
            LoadDataGrid()
        Catch ex As Exception
            CommonFunction.ShowStartupErrorMessage(Me, ex.Message)
        End Try
    End Sub

    Private Sub GetDataTable()
        Dim LocationID, DivID, DepID, SecID, GropID As Integer
        Dim BTTypeID As String = String.Empty
        Dim FullName As String = String.Empty
        Dim EmployeeCode As String = String.Empty
        Dim FromDate As Date
        Dim ToDate As Date
        'Dim dtSub As DataTable

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

        '_dtData = BusinessTripProvider.tbl_BT_Register_Search(BTTypeID, LocationID, DivID, DepID, SecID, GropID, FullName, EmployeeCode, FromDate, ToDate, "", "", -1, "", strUser, True)
    End Sub

    Public Sub LoadDataGrid()
        GetDataTable()
        LoadRegister()
        LoadSubmitted()
    End Sub

    Private Sub LoadRegister()
        CommonFunction.LoadDataToGrid(grvBTRegister, _dtData, "IsSubmited = 1 and HRStatus <> '" + HRStatus.completed.ToString() + "'", "No")
    End Sub

    Private Sub LoadSubmitted()
        CommonFunction.LoadDataToGrid(grvBTSubmitted, _dtData, "IsSubmited = 1 and HRStatus = '" + HRStatus.completed.ToString() + "'", "No")
    End Sub

    'Protected Sub btnApproval_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnApproval.Click
    '    CommonFunction.SetPostBackStatus(btnApproval)
    '    Dim i As Integer
    '    Dim insValue As Integer = 0
    '    Dim checkAll As Boolean = False
    '    For i = 0 To grvBTRegister.VisibleRowCount - 1
    '        Dim chk As CheckBox = CType(grvBTRegister.FindRowCellTemplateControl(i, grvBTRegister.Columns(0), "chkSelect"), CheckBox)
    '        If chk IsNot Nothing Then
    '            If chk.Checked Then
    '                checkAll = True
    '                Dim BtRegisterID As Integer = CommonFunction._ToInt(chk.Attributes("data-id").ToString())
    '                BusinessTripProvider.tbl_BT_Register_UpdateStatusHR(BtRegisterID, HRStatus.completed.ToString(), strUser, "")
    '                'Insert into HR
    '                insValue = BusinessTripProvider.InsTasSetRest(BtRegisterID)
    '            End If
    '        End If
    '    Next
    '    CommonFunction.ShowInfoMessage(panMessage, "Approval suscessfully!")
    '    LoadDataGrid()
    'End Sub

    Protected Sub btnApprovalOK_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim BTRegisterID As Integer            
            BTRegisterID = CommonFunction._ToInt(CType(sender, Button).Attributes("data-id"))
            BusinessTripProvider.tbl_BT_Register_UpdateStatusHR(BTRegisterID, HRStatus.completed.ToString(), strUser, "")
            'Insert into HR
            BusinessTripProvider.InsTasSetRest(BTRegisterID)
            LoadDataGrid()
            '
            CommonFunction.ShowInfoMessage(panMessage, "BT's Approved!")
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

    Protected Sub grvBTRegister_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTRegister.BeforeGetCallbackResult
        GetDataTable()
        LoadRegister()
    End Sub

    Protected Sub grvBTSubmitted_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTSubmitted.BeforeGetCallbackResult
        GetDataTable()
        LoadSubmitted()
    End Sub

    Protected Sub btnRejectOK_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnRejectOK.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim BTRegisterID As Integer = CommonFunction._ToInt(hRejectBTRegisterID.Value)
            Dim rejectReason As String = txtRejectReason.Text.Trim()
            hRejectBTRegisterID.Value = ""
            txtRejectReason.Text = ""
            tabRejectMessage.Attributes("style") = "display: none"
            If rejectReason.Trim().Length = 0 Then
                Return
            End If
            BusinessTripProvider.tbl_BT_Register_UpdateStatusHR(BTRegisterID, HRStatus.rejected.ToString(), strUser, rejectReason)
            'Xoa du lieu trong bang TAS
            If hRejectTypeID.Value = "1" Then
                BusinessTripProvider.DeleteTasSetRest(BTRegisterID)
            End If
            LoadDataGrid()
            '
            CommonFunction.ShowInfoMessage(panMessage, "BT's Rejected!")
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

    Protected Sub btnView_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim btn As Button = CType(sender, Button)
            Dim btID As Integer = CommonFunction._ToInt(btn.Attributes("data-id"))
            Dim dtRegister As DataTable = BusinessTripProvider.BTRegister_GetByID(btID)
            If dtRegister.Rows.Count > 0 Then
                Dim btType As String = CommonFunction._ToString(dtRegister.Rows(0)("BTType"))
                Dim postBackUrl As New StringBuilder()
                postBackUrl.Append(If(btType.IndexOf("oneday_") = 0, "~/BTOneDayDeclaration.aspx", "~/BTAdvanceDeclaration.aspx"))
                Dim params As String = String.Format( _
                    "btid={0}&bttype={1}&loc={2}&fdate={3}&tdate={4}&div={5}&dep={6}&sec={7}&group={8}&ecode={9}&ename={10}&rpage={11}&rpsize={12}&spage={13}&spsize={14}", _
                    btID, hBTType.Value, hLocation.Value, hFrom.Value, hTo.Value, hDivision.Value, hDept.Value, hSection.Value, hGroup.Value, hEmployeeCode.Value, hFullName.Value, grvBTRegister.PageIndex, grvBTRegister.SettingsPager.PageSize, grvBTSubmitted.PageIndex, grvBTSubmitted.SettingsPager.PageSize)
                postBackUrl.Append(String.Format("?id={0}&back=HRManagement.aspx&params={1}", btID, params.Replace("&", ";amp;").Replace("=", ";eq;")))
                Response.Redirect(postBackUrl.ToString())
            Else
                CommonFunction.ShowErrorMessage(panMessage, "Item not found!")
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvBTRegister_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles grvBTRegister.HtmlRowPrepared
        If e.RowType = GridViewRowType.Data Then
            Dim status As String = e.GetValue("HRStatus")
            If status = HRStatus.rejected.ToString() Then
                e.Row.CssClass &= " rejected"
            End If
        End If
    End Sub
#End Region

End Class