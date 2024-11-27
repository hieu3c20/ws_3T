Imports Provider
Imports System.Data
Imports System.IO
Imports DevExpress.Web.ASPxGridView

Partial Public Class BTWifiDevice
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
        CommonFunction.CheckRole(RoleType.Administrator, RoleType.IT)
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
        InitSCountry()
    End Sub

    Private Sub SetPreParams()
        Dim btNo As String = CommonFunction._ToString(Request.QueryString("btno"))
        txtBusinessTripNo.Text = btNo
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

    Private Sub InitSCountry()
        Dim dt As DataTable = mCountryProvider.GetAll()
        CommonFunction.LoadDataToComboBox(ddlSCountry, dt, "Name", "Code", True, "All", "")
    End Sub

    Private Sub LoadForm()

    End Sub

    Private Sub SetPreSearchCondition()
        hSBTNo.Value = txtBusinessTripNo.Text.Trim()
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
        Dim btNo As String = hSBTNo.Value
        Dim dateFrom As DateTime = CommonFunction._ToDateTime(hSFromDate.Value, "dd-MMM-yyyy")
        Dim dateTo As DateTime = CommonFunction._ToDateTime(hSToDate.Value, "dd-MMM-yyyy")
        Dim countryCode As String = hSCountry.Value
        Dim employeeCode As String = hSEmployeeCode.Value
        Dim employeeName As String = hSEmployeeName.Value
        _dsData = WifiDeviceProvider.WifiDevice_Search(btNo, countryCode, employeeCode, employeeName, dateFrom, dateTo)
    End Sub

    Private Sub LoadData()
        LoadDataTable()
        LoadPending()
        LoadRejected()
        LoadConfirmed()
        LoadReturned()
    End Sub

    Private Sub LoadPending()
        Dim dtData As DataTable = _dsData.Tables(0)
        CommonFunction.LoadDataToGrid(grvPending, dtData, String.Format("Status = '{0}'", WifiDeviceRequestStatus.pending.ToString()), "No")
    End Sub

    Private Sub LoadRejected()
        Dim dtData As DataTable = _dsData.Tables(0)
        CommonFunction.LoadDataToGrid(grvRejected, dtData, String.Format("Status = '{0}'", WifiDeviceRequestStatus.rejected.ToString()), "No")
    End Sub

    Private Sub LoadConfirmed()
        Dim dtData As DataTable = _dsData.Tables(0)
        CommonFunction.LoadDataToGrid(grvConfirmed, dtData, String.Format("Status = '{0}'", WifiDeviceRequestStatus.confirmed.ToString()), "No")
    End Sub

    Private Sub LoadReturned()
        Dim dtData As DataTable = _dsData.Tables(0)
        CommonFunction.LoadDataToGrid(grvReturned, dtData, String.Format("Status = '{0}'", WifiDeviceRequestStatus.returned.ToString()), "No")
    End Sub

    Protected Sub grvPending_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvPending.BeforeGetCallbackResult
        LoadDataTable()
        LoadPending()
    End Sub

    Protected Sub grvRejected_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvRejected.BeforeGetCallbackResult
        LoadDataTable()
        LoadRejected()
    End Sub

    Protected Sub grvConfirmed_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvConfirmed.BeforeGetCallbackResult
        LoadDataTable()
        LoadConfirmed()
    End Sub

    Protected Sub grvReturned_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvReturned.BeforeGetCallbackResult
        LoadDataTable()
        LoadReturned()
    End Sub

    Protected Sub btnConfirm_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnConfirm.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim comment As String = txtConfirmComment.Text.Trim()
            Dim message As String = WifiDeviceProvider.WifiDevice_UpdateStatus(CommonFunction._ToInt(hID.Value), WifiDeviceRequestStatus.confirmed.ToString(), comment)
            If message.Trim().Length = 0 Then
                LoadData()
                'Send notice email
                If SendUserEmail() Then
                    CommonFunction.ShowInfoMessage(panMessage, "Confirmed!")
                Else
                    CommonFunction.ShowInfoMessage(panMessage, "Confirmed but fail to send notice emails! Please contact with administrator!")
                End If
                _objEmail.Dispose()
            Else
                CommonFunction.ShowErrorMessage(panMessage, message)
            End If
            hID.Value = ""
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnReject_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnReject.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim comment As String = txtRejectReason.Text.Trim()
            Dim message As String = WifiDeviceProvider.WifiDevice_UpdateStatus(CommonFunction._ToInt(hID.Value), WifiDeviceRequestStatus.rejected.ToString(), comment)
            If message.Trim().Length = 0 Then
                LoadData()
                'Send notice email
                If SendUserEmail() Then
                    CommonFunction.ShowInfoMessage(panMessage, "Rejected!")
                Else
                    CommonFunction.ShowInfoMessage(panMessage, "Rejected but fail to send notice emails! Please contact with administrator!")
                End If
                _objEmail.Dispose()
            Else
                CommonFunction.ShowErrorMessage(panMessage, message)
            End If
            hID.Value = ""
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnReturn_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnReturn.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim comment As String = ""
            Dim message As String = WifiDeviceProvider.WifiDevice_UpdateStatus(CommonFunction._ToInt(hID.Value), WifiDeviceRequestStatus.returned.ToString(), comment)
            If message.Trim().Length = 0 Then
                LoadData()
                'Send notice email
                If SendUserEmail() Then
                    CommonFunction.ShowInfoMessage(panMessage, "Set as Returned!")
                Else
                    CommonFunction.ShowInfoMessage(panMessage, "Set as Returned! but fail to send notice emails! Please contact with administrator!")
                End If
                _objEmail.Dispose()
            Else
                CommonFunction.ShowErrorMessage(panMessage, message)
            End If
            hID.Value = ""
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grv_HtmlRowPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) _
        Handles grvPending.HtmlRowPrepared, grvConfirmed.HtmlRowPrepared, grvRejected.HtmlRowPrepared, grvReturned.HtmlRowPrepared
        If e.RowType = GridViewRowType.Data Then
            Dim status As String = CommonFunction._ToString(e.GetValue("BTStatus"))
            If status = FIStatus.rejected.ToString() OrElse status = FIStatus.budget_rejected.ToString() Then
                e.Row.CssClass &= " rejected"
            ElseIf status = FIStatus.pending.ToString() Then
                e.Row.CssClass &= " not-found"
            ElseIf status = FIStatus.completed.ToString() Then
                e.Row.CssClass &= " completed"
            ElseIf status = FIStatus.budget_reconfirmed.ToString() Then
                e.Row.CssClass &= " waiting"
            ElseIf status = FIStatus.cancelled.ToString() Then
                e.Row.CssClass &= " cancelled"
            End If
        End If
    End Sub

#Region "Send Notice Email"
    Private _statusText As String = String.Empty
    Private _employee As String = String.Empty
    Private _country As String = String.Empty
    Private _btID As Integer
    Private Function GenNoticeBody(Optional ByVal link As String = "") As String
        Dim id As Integer = CommonFunction._ToInt(hID.Value)
        Dim dtData As DataTable = WifiDeviceProvider.WifiDevice_GetByID(id)
        Dim drData As DataRow = If(dtData.Rows.Count > 0, dtData.Rows(0), dtData.NewRow())
        Dim comment As String = CommonFunction._ToString(drData("Comment")).Trim()
        _statusText = CommonFunction._ToString(drData("StatusText"))
        _employee = CommonFunction._ToString(drData("Employee"))
        _country = CommonFunction._ToString(drData("CountryName"))
        _btID = CommonFunction._ToInt(drData("BTRegisterID"))
        '
        Dim eBody As New StringBuilder("<p><strong>To whom it may concern</strong></p>")
        eBody.Append("<p>This is notification from <strong>B</strong>usiness <strong>T</strong>rip Online <strong>S</strong>ystem (BTS).</p>")
        eBody.Append("<p>Regarding wifi device request, we would like to share the latest information to you as below:")
        eBody.Append(String.Format("<p><table>{0}", If(_btID > 0, String.Format("<tr><td valign='top' width='210'><ul><li style='margin: 0;'><strong>BT No:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", drData("BTNo")), "")))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Country:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", _country))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Employee:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", _employee))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Date:</strong></li></ul></td><td style='color: #0070c0'><strong>{0}</strong></td></tr>", String.Format("{0:dd-MMM-yyyy} -> {1:dd-MMM-yyyy}", drData("FromDate"), drData("ToDate"))))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Status:</strong></li></ul></td><td style='color: #0070c0'>{0}{1}</td></tr></table></p>", _statusText, If(comment.Length > 0, String.Format(" ({0})", comment), "")))
        If _btID > 0 Then
            eBody.Append(String.Format("<p>Please <a href='{0}{1}'>click here</a> to check/process this information.</p>", ConfigurationManager.AppSettings("Domain"), String.Format(link, _btID)))
        End If
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

    Private Function SendUserEmail() As Boolean
        Dim eTo As String = ""
        Dim viewLink As String = "BTAdvanceDeclaration.aspx?id={0}"
        Dim eBody As String = GenNoticeBody(viewLink)
        '
        If _btID <= 0 Then
            Return True
        End If
        '
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToString(Session("Branch")), RoleType.Finance.ToString())
        Else
            Dim dtUser As DataTable = UserProvider.GetEmailInfoByBT(_btID)
            For Each dr As DataRow In dtUser.Rows
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