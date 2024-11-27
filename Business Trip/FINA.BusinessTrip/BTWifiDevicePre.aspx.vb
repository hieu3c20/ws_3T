Imports Provider
Imports System.Data
Imports System.IO
Imports DevExpress.Web.ASPxGridView

Partial Public Class BTWifiDevicePre
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
        _username = CommonFunction._ToString(Session("UserName"))
        If Not IsPostBack Then
            InitForm()
            '            
            SetPreParams()
        Else
        End If
        '
        CommonFunction.CheckSessionMessage(Me)
        LoadForm()
    End Sub

    Private Sub InitForm()
        InitCountry()
    End Sub

    Protected Function GetAuthorizedAccounts() As String
        Dim builder As New StringBuilder()
        Dim dtEmployees = UserProvider.tbl_User_GetAuthorizedForWifiDevice(_username)
        builder.Append("[")
        For Each item As DataRow In dtEmployees.Rows
            builder.Append(String.Concat("{ value: '", item("EmployeeCode"), " - ", item("EmployeeName"), "', data: '", String.Format("{0}-{1}-{2}-{3}-{4}", item("EmployeeCode"), item("EmployeeName"), item("DivisionName"), item("DepartmentName"), CommonFunction._ToString(item("Mobile")).Replace("'", "")), "'},"))
        Next
        builder = New StringBuilder(builder.ToString().TrimEnd(","))
        builder.Append("]")
        Return builder.ToString()
    End Function

    Private Sub SetPreParams()
        Dim country As String = CommonFunction._ToString(Request.QueryString("country"))
        CommonFunction.SetCBOValue(ddlSCountry, country)
        Dim employeeCode As String = CommonFunction._ToString(Request.QueryString("ecode"))
        txtSEmployeeCode.Text = employeeCode
        Dim employeeName As String = CommonFunction._ToString(Request.QueryString("ename"))
        txtSEmployeeName.Text = employeeName
        Dim fromDate As String = CommonFunction._ToString(Request.QueryString("fromdate"))
        dteSFromDate.Text = fromDate
        Dim toDate As String = CommonFunction._ToString(Request.QueryString("todate"))
        dteSToDate.Text = toDate
        SetPreSearchCondition()
        If Request.QueryString("btno") IsNot Nothing Then
            LoadData()
        End If
    End Sub

    Private Sub InitCountry()
        Dim dt As DataTable = mCountryProvider.GetAll()
        CommonFunction.LoadDataToComboBox(ddlSCountry, dt, "Name", "Code", True, "All", "", "Code <> 'VN'")
        CommonFunction.LoadDataToComboBox(ddlWifiDeviceCountry, dt, "Name", "Code", True, "", "", "Code <> 'VN'")
    End Sub

    Private Sub LoadForm()
        LoadData()
    End Sub

    Private Sub SetPreSearchCondition()
        hSFromDate.Value = dteSFromDate.Text
        hSToDate.Value = dteSToDate.Text
        hSEmployeeCode.Value = txtSEmployeeCode.Text.Trim()
        hSEmployeeName.Value = txtSEmployeeName.Text.Trim()
        hSCountry.Value = ddlSCountry.SelectedValue
    End Sub

    Protected Sub btnSearch_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            SetPreSearchCondition()
            LoadData()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Sub LoadDataTable()
        Dim dateFrom As DateTime = CommonFunction._ToDateTime(hSFromDate.Value, "dd-MMM-yyyy")
        Dim dateTo As DateTime = CommonFunction._ToDateTime(hSToDate.Value, "dd-MMM-yyyy")
        Dim countryCode As String = hSCountry.Value
        Dim employeeCode As String = hSEmployeeCode.Value
        Dim employeeName As String = hSEmployeeName.Value
        _dsData = WifiDeviceProvider.WifiDevicePre_Search(countryCode, employeeCode, employeeName, dateFrom, dateTo, _username)
    End Sub

    Private Sub LoadData()
        LoadDataTable()
        LoadPrepared()
        LoadPending()
        LoadRejected()
        'LoadConfirmed()
        'LoadReturned()
    End Sub

    Private Sub LoadPrepared()
        Dim dtData As DataTable = _dsData.Tables(0)
        CommonFunction.LoadDataToGrid(grvPrepared, dtData, "Status = ''", "No")
    End Sub

    Private Sub LoadPending()
        Dim dtData As DataTable = _dsData.Tables(0)
        CommonFunction.LoadDataToGrid(grvPending, dtData, String.Format("Status in ('{0}', '{1}', '{2}')", WifiDeviceRequestStatus.pending.ToString(), WifiDeviceRequestStatus.confirmed.ToString(), WifiDeviceRequestStatus.returned.ToString()), "No")
    End Sub

    Private Sub LoadRejected()
        Dim dtData As DataTable = _dsData.Tables(0)
        CommonFunction.LoadDataToGrid(grvRejected, dtData, String.Format("Status = '{0}'", WifiDeviceRequestStatus.rejected.ToString()), "No")
    End Sub

    'Private Sub LoadConfirmed()
    '    Dim dtData As DataTable = _dsData.Tables(0)
    '    CommonFunction.LoadDataToGrid(grvConfirmed, dtData, String.Format("Status = '{0}'", WifiDeviceRequestStatus.confirmed.ToString()), "No")
    'End Sub

    'Private Sub LoadReturned()
    '    Dim dtData As DataTable = _dsData.Tables(0)
    '    CommonFunction.LoadDataToGrid(grvReturned, dtData, String.Format("Status = '{0}'", WifiDeviceRequestStatus.returned.ToString()), "No")
    'End Sub

    Private Sub EnableWifiForm(ByVal enable As Boolean)
        ddlWifiDeviceCountry.Enabled = enable
        dtWifiDeviceFromDate.ReadOnly = Not enable
        dtWifiDeviceToDate.ReadOnly = Not enable
        txtEmployeeCode.ReadOnly = Not enable
        '
        btnShowSubmitWifiDevice.Visible = enable
        panSubmitInfo.Visible = enable
        btnSaveWifiDevice.Visible = enable
    End Sub

    Private Sub ClearWifiForm()
        hWifiDeviceID.Value = ""
        ddlWifiDeviceCountry.SelectedValue = ""
        dtWifiDeviceFromDate.Date = DateTime.Now
        dtWifiDeviceToDate.Date = DateTime.Now
        txtEmployeeCode.Text = ""
        txtEmployeeName.Text = ""
        txtEmployeeDivision.Text = ""
        txtEmployeeDepartment.Text = ""
        '
        EnableWifiForm(True)
    End Sub

    Private Sub LoadWifiDeviceByID()
        EnableWifiForm(False)
        Dim id As Integer = CommonFunction._ToInt(hWifiDeviceID.Value)
        Dim dtData As DataTable = WifiDeviceProvider.WifiDevice_GetByID(CommonFunction._ToInt(id))
        If dtData.Rows.Count > 0 Then
            Dim drData As DataRow = dtData.Rows(0)
            Dim status As String = CommonFunction._ToString(drData("Status"))
            '
            EnableWifiForm(status.Length = 0 OrElse status.ToLower() = WifiDeviceRequestStatus.rejected.ToString())
            '
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
            txtEmployeeCode.Text = CommonFunction._ToString(drData("EmployeeCode"))
            txtEmployeeName.Text = CommonFunction._ToString(drData("EmployeeName"))
            txtEmployeeDivision.Text = CommonFunction._ToString(drData("EmployeeDivision"))
            txtEmployeeDepartment.Text = CommonFunction._ToString(drData("EmployeeDepartment"))
            CommonFunction.SetCBOValue(ddlWifiDeviceCountry, drData("CountryCode"))
        End If
    End Sub

    Protected Sub grvPrepared_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvPrepared.BeforeGetCallbackResult
        LoadDataTable()
        LoadPrepared()
    End Sub

    Protected Sub grvPending_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvPending.BeforeGetCallbackResult
        LoadDataTable()
        LoadPending()
    End Sub

    Protected Sub grvRejected_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvRejected.BeforeGetCallbackResult
        LoadDataTable()
        LoadRejected()
    End Sub

    'Protected Sub grvConfirmed_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvConfirmed.BeforeGetCallbackResult
    '    LoadDataTable()
    '    LoadConfirmed()
    'End Sub

    'Protected Sub grvReturned_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvReturned.BeforeGetCallbackResult
    '    LoadDataTable()
    '    LoadReturned()
    'End Sub

    Private Function GetWifiObject() As tblBTWifiDeviceInfo
        Dim obj As New tblBTWifiDeviceInfo()
        obj.ID = CommonFunction._ToInt(hWifiDeviceID.Value)
        obj.BTRegisterID = -1
        obj.FromDate = dtWifiDeviceFromDate.Date
        obj.ToDate = dtWifiDeviceToDate.Date
        obj.CountryCode = CommonFunction._ToString(ddlWifiDeviceCountry.SelectedValue)
        obj.EmployeeCode = txtEmployeeCode.Text.Trim()
        obj.EmployeeName = txtEmployeeName.Text.Trim()
        obj.EmployeeDivision = txtEmployeeDivision.Text.Trim()
        obj.EmployeeDepartment = txtEmployeeDepartment.Text.Trim()
        obj.CreatedBy = _username
        obj.UpdatedBy = _username
        obj.Status = ""
        Return obj
    End Function

    Private Function IsValidWifiDevice(ByVal obj As tblBTWifiDeviceInfo) As Boolean
        Dim isValid As Boolean = True
        dtWifiDeviceFromDate.Attributes("style") = ""
        dtWifiDeviceToDate.Attributes("style") = ""
        '
        If obj.ToDate < obj.FromDate Then
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

    Protected Sub btnAddNewWifiDevice_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnAddNewWifiDevice.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            ClearWifiForm()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnSaveWifiDevice_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveWifiDevice.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim obj As tblBTWifiDeviceInfo = GetWifiObject()
            '
            If obj.ID > 0 Then
                WifiDeviceProvider.WifiDevice_Update(obj)
            Else
                WifiDeviceProvider.WifiDevice_Insert(obj)                
            End If
            CommonFunction.SetProcessStatus(btnSearch, True)
            CommonFunction.ShowInfoMessage(panMessage, "Your data is saved successfully!")            
            LoadData()
        Catch ex As Exception
            CommonFunction.SetProcessStatus(btnSearch, False)
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnSubmit_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim obj As tblBTWifiDeviceInfo = GetWifiObject()
            obj.Status = WifiDeviceRequestStatus.pending.ToString()
            '            
            If Not IsValidWifiDevice(obj) Then
                CommonFunction.SetProcessStatus(btnSearch, False)
                Return
            End If
            obj.Comment = txtSubmitComment.Text.Trim()
            If obj.ID > 0 Then
                WifiDeviceProvider.WifiDevice_Update(obj)
            Else
                WifiDeviceProvider.WifiDevice_Insert(obj)
            End If
            CommonFunction.SetProcessStatus(btnSearch, True)
            'Send notice email
            If SendSubmitEmail() Then
                CommonFunction.ShowInfoMessage(panMessage, "Submitted!")
            Else
                CommonFunction.ShowInfoMessage(panMessage, "Submitted but fail to send notice emails! Please contact with administrator!")
            End If
            _objEmail.Dispose()
            LoadData()
        Catch ex As Exception
            CommonFunction.SetProcessStatus(btnSearch, False)
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnGetEmployeeInfo_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetEmployeeInfo.Click
        CommonFunction.SetPostBackStatus(btnGetEmployeeInfo)
        Try
            Dim info As String() = hEmployee.Value.Split("-")
            txtEmployeeCode.Text = info(0)
            txtEmployeeName.Text = info(1)
            txtEmployeeDivision.Text = info(2)
            txtEmployeeDepartment.Text = info(3)
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditWifiDevice_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try            
            LoadWifiDeviceByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteWifiDevice_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim id As Integer = CommonFunction._ToInt(hWifiDeviceID.Value)
            WifiDeviceProvider.WifiDevice_Delete(id.ToString())
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
            LoadData()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

#Region "Send Notice Email"
    Private _statusText As String = String.Empty
    Private _employee As String = String.Empty
    Private _country As String = String.Empty
    Private _btID As Integer
    Private Function GenNoticeBody(Optional ByVal link As String = "") As String
        Dim id As Integer = CommonFunction._ToInt(hWifiDeviceID.Value)
        Dim dtData As DataTable = WifiDeviceProvider.WifiDevice_GetByID(id)
        Dim drData As DataRow = If(dtData.Rows.Count > 0, dtData.Rows(0), dtData.NewRow())
        Dim comment As String = CommonFunction._ToString(drData("Comment")).Trim()
        _statusText = CommonFunction._ToString(drData("StatusText"))
        _employee = CommonFunction._ToString(drData("Employee"))
        _country = CommonFunction._ToString(drData("CountryName"))        
        '
        Dim eBody As New StringBuilder("<p><strong>To whom it may concern</strong></p>")
        eBody.Append("<p>This is notification from <strong>B</strong>usiness <strong>T</strong>rip Online <strong>S</strong>ystem (BTS).</p>")
        eBody.Append("<p>Regarding wifi device request, we would like to share the latest information to you as below:")
        eBody.Append(String.Format("<p><table><tr><td valign='top' width='210'><ul><li style='margin: 0;'><strong>Country:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", _country))        
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Employee:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", _employee))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Date:</strong></li></ul></td><td style='color: #0070c0'><strong>{0}</strong></td></tr>", String.Format("{0:dd-MMM-yyyy} -> {1:dd-MMM-yyyy}", drData("FromDate"), drData("ToDate"))))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Status:</strong></li></ul></td><td style='color: #0070c0'>{0}{1}</td></tr></table></p>", _statusText, If(comment.Length > 0, String.Format(" ({0})", comment), "")))
        eBody.Append("<p>If you are unable to check/process, You can refer to FAQ function from main menu of BTS system to view all Frequently Asked Question.</p>")
        eBody.Append("<p>Thank you for your cooperation.</p>")
        eBody.Append("<p>Regards,<br>BTS Support Team</p>")
        Return eBody.ToString()
    End Function

    Dim _objEmail As TMVEmailService.EmailService = Nothing
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
            Dim eSubject As String = String.Format("{0}[BTS Wifi Device]: {1} / {2} (Status: {3})", indicator, _employee, _country, _statusText)
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
        Dim viewLink As String = "BTAdvanceDeclaration.aspx?id={0}"
        Dim eBody As String = GenNoticeBody(viewLink)
        '
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToString(Session("Branch")), RoleType.Finance.ToString())
        Else
            Dim dtAuthorized As DataTable = BusinessTripProvider.BTRegister_GetAuthorizedUsersByCode(txtEmployeeCode.Text.Trim())
            Dim dvITBudget As DataView = dtAuthorized.DefaultView
            dvITBudget.RowFilter = String.Format("Role in ('{0}', '{1}')", RoleType.IT.ToString(), RoleType.Administrator.ToString())
            For Each dr As DataRowView In dvITBudget
                eTo = String.Concat(eTo, ", ", dr("TMVEmail"))
            Next
            If eTo.Length > 0 Then
                eTo = eTo.Substring(1).Trim()
            End If
        End If
        Return SendNoticeEmail(eTo, eBody)
    End Function
#End Region

End Class