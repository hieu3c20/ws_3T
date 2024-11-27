Imports Provider
Imports System.Data
Imports System.IO
Imports DevExpress.Web.ASPxGridView

Partial Public Class BTAirTicketRequest
    Inherits System.Web.UI.Page

    Protected _username As String = String.Empty
    Protected _dtData As DataTable
    Protected _isGA As Boolean
    Protected _isTofsAirGA As Boolean
    Protected _isFI As Boolean
    Protected _isFIBudget As Boolean
    Private _sendEmailMode As String = CommonFunction._ToString(ConfigurationManager.AppSettings("SendEmailMode")).ToLower()
    Protected _dsData As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CommonFunction.CheckSession()
        CommonFunction.CheckRole(RoleType.Administrator, RoleType.HR)
        _username = CommonFunction._ToString(Session("UserName"))
        'Dim role As String = CommonFunction._ToString(Session("RoleType"))
        '_isGA = role.ToLower() = RoleType.GA.ToString().ToLower() OrElse role.ToLower() = RoleType.TOFS_AIR_GA.ToString().ToLower() OrElse role.ToLower() = RoleType.Finance_GA.ToString().ToLower() OrElse role.ToLower() = RoleType.Administrator.ToString().ToLower()
        '_isTofsAirGA = role.ToLower() = RoleType.TOFS_AIR_GA.ToString().ToLower() OrElse role.ToLower() = RoleType.Administrator.ToString().ToLower()
        '_isFI = role.ToLower() = RoleType.Finance.ToString().ToLower() OrElse role.ToLower() = RoleType.Finance_GA.ToString().ToLower() OrElse role.ToLower() = RoleType.Administrator.ToString().ToLower()
        '_isFIBudget = role.ToLower() = RoleType.Finance_Budget.ToString().ToLower() OrElse role.ToLower() = RoleType.Administrator.ToString().ToLower()
        If Not IsPostBack Then
            InitForm()
        Else
        End If
        '
        'If _isGA Then
        '    btnShowImportAirTicket.Visible = True
        '    btnShowImportOtherAirTicket.Visible = True
        '    btnShowAddOtherAirTicket.Visible = True
        '    btnShowImportAirTicketError.Visible = True
        '    btnShowImportOtherError.Visible = True
        '    Dim dsError As DataSet = AirTicketProvider.BTAirTicket_GetErrors(_username)
        '    CommonFunction.LoadDataToGrid(grvImportError, dsError.Tables(0))
        '    If dsError.Tables(0).Rows.Count > 0 Then
        '        btnShowImportAirTicketError.Attributes("class") = "btn inform view-error"
        '    Else
        '        btnShowImportAirTicketError.Attributes("class") = "btn inform view-error hide"
        '    End If
        '    '
        '    CommonFunction.LoadDataToGrid(grvImportOtherError, dsError.Tables(1))
        '    If dsError.Tables(1).Rows.Count > 0 Then
        '        btnShowImportOtherError.Attributes("class") = "btn inform view-error"
        '    Else
        '        btnShowImportOtherError.Attributes("class") = "btn inform view-error hide"
        '    End If
        'Else
        '    btnShowImportAirTicket.Visible = False
        '    btnShowImportOtherAirTicket.Visible = False
        '    btnShowAddOtherAirTicket.Visible = False
        '    btnShowImportAirTicketError.Visible = False
        '    btnShowImportOtherError.Visible = False
        'End If
        '
        CommonFunction.CheckSessionMessage(Me)
        LoadForm()
    End Sub

    Private Sub InitForm()
        Dim dtCountry As DataTable = mCountryProvider.GetAll()
        CommonFunction.LoadDataToComboBox(ddlFromCountry, dtCountry, "Name", "Code", True, "")
        CommonFunction.LoadDataToComboBox(ddlToCountry, dtCountry, "Name", "Code", True, "")
        CommonFunction.LoadDataToComboBox(ddlRelativeFromCountry, dtCountry, "Name", "Code", True, "")
        CommonFunction.LoadDataToComboBox(ddlRelativeToCountry, dtCountry, "Name", "Code", True, "")
        '
        InitRelative()
    End Sub

    Private Sub InitRelative()
        Dim dtRelative As DataTable = LookupProvider.GetByCode("RELATIVE")
        For i As Integer = 1 To 10
            Dim ddl As Web.UI.Control = upAirTicketRequest.FindControl(String.Concat("ddlRelationship", i))
            If ddl IsNot Nothing Then
                CommonFunction.LoadLookupDataToComboBox(CType(ddl, DropDownList), dtRelative, True, "")
            End If
        Next
    End Sub

    Private Sub InitFromDestination()
        Dim dt As DataTable = mDestinationProvider.GetByCountryCode(ddlFromCountry.SelectedValue)
        CommonFunction.LoadDataToComboBox(ddlFromDestination, dt, "Name", "DestinationID", True, "")
    End Sub

    Private Sub InitToDestination()
        Dim dt As DataTable = mDestinationProvider.GetByCountryCode(ddlToCountry.SelectedValue)
        CommonFunction.LoadDataToComboBox(ddlToDestination, dt, "Name", "DestinationID", True, "")
    End Sub

    Private Sub InitRelativeFromDestination()
        Dim dt As DataTable = mDestinationProvider.GetByCountryCode(ddlRelativeFromCountry.SelectedValue)
        CommonFunction.LoadDataToComboBox(ddlRelativeFromDestination, dt, "Name", "DestinationID", True, "")
    End Sub

    Private Sub InitRelativeToDestination()
        Dim dt As DataTable = mDestinationProvider.GetByCountryCode(ddlRelativeToCountry.SelectedValue)
        CommonFunction.LoadDataToComboBox(ddlRelativeToDestination, dt, "Name", "DestinationID", True, "")
    End Sub

    Protected Function GetAuthorizedAccounts() As String
        Dim builder As New StringBuilder()
        Dim dtEmployees As DataTable = UserProvider.tbl_BT_User_ForAirTicketRequest()
        builder.Append("[")
        For Each item As DataRow In dtEmployees.Rows
            builder.Append(String.Concat("{ value: '", item("EmployeeCode"), " - ", item("EmployeeName"), "', data: '", String.Format("{0}-{1}-{2}-{3}-{4}", item("EmployeeCode"), item("EmployeeName"), item("DivisionName"), item("DepartmentName"), CommonFunction._ToString(item("Mobile")).Replace("'", "")), "'},"))
        Next
        builder = New StringBuilder(builder.ToString().TrimEnd(","))
        builder.Append("]")
        Return builder.ToString()
    End Function

    Protected Function GetBudgetCodes() As String
        Dim builder As New StringBuilder()
        Dim dtData As DataTable = mBudgetProvider.GetActive()
        builder.Append("[")
        For Each item As DataRow In dtData.Rows
            builder.Append(String.Concat("{ value: '", item("BudgetCode"), "', data: '", item("BudgetCode"), "'},"))
        Next
        builder = New StringBuilder(builder.ToString().TrimEnd(","))
        builder.Append("]")
        Return builder.ToString()
    End Function

    Private Sub LoadForm()
        If hAddMore.Value = "T" Then
            tblAddMore.Attributes("class") = "ui-panelgrid ui-widget grid-edit"
            divBtnAddMore.Attributes("class") = "hide"
        Else
            tblAddMore.Attributes("class") = "ui-panelgrid ui-widget grid-edit hide"
            divBtnAddMore.Attributes("class") = ""
        End If
    End Sub

    Private Sub SetPreSearchCondition()
        hSDepartureFrom.Value = dteSDepartureFrom.Text
        hSDepartureTo.Value = dteSDepartureTo.Text
        hSEmployeeCode.Value = txtSEmployeeCode.Text.Trim()
        hSEmployeeName.Value = txtSEmployeeName.Text.Trim()
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
        Dim employeeCode As String = hSEmployeeCode.Value
        Dim employeeName As String = hSEmployeeName.Value
        Dim departureFrom As DateTime = CommonFunction._ToDateTime(hSDepartureFrom.Value, "dd-MMM-yyyy")
        Dim departureTo As DateTime = CommonFunction._ToDateTime(hSDepartureTo.Value, "dd-MMM-yyyy")
        _dsData = AirTicketProvider.BTAirTicketRequest_Search(employeeCode, employeeName, departureFrom, departureTo)
    End Sub

    Protected Function BuildGridDetails(ByVal masterId As Integer) As String
        LoadDataTable()
        Dim builder As New StringBuilder()
        For Each dr As Data.DataRow In _dsData.Tables(1).Select(String.Format("AirTicketRequestID = {0}", masterId))
            builder.Append(String.Format("<tr><td style='text-align: left'>{0}</td><td style='text-align: left'>{1}</td><td style='text-align: left'>{2}</td><td style='text-align: left'>{3}</td><td style='text-align: left'>{4}</td><td style='text-align: left'>{5}</td></tr>", _
                                         dr("Name"), dr("RelationshipName"), dr("From"), dr("To"), _
                                         If(CommonFunction._ToString(dr("DepartureDate")).Trim().Length = 0, "", CommonFunction._ToDateTime(dr("DepartureDate")).ToString("dd-MMM-yyyy HH:mm")), _
                                         If(CommonFunction._ToString(dr("ReturnDate")).Trim().Length = 0, "", CommonFunction._ToDateTime(dr("ReturnDate")).ToString("dd-MMM-yyyy HH:mm"))))
        Next
        Return builder.ToString()
    End Function

    Private Sub LoadBTAirTicket()
        LoadDataTable()
        Dim dtData As DataTable = _dsData.Tables(0)
        CommonFunction.LoadDataToGrid(grvPrepared, dtData, String.Format("Status is null or Status = '{0}'", AirticketRequestStatus.Prepared)) 'String.Concat(If(_isGA OrElse _isFIBudget, "", "BudgetChecked = 1 and "), "BTRegisterID is null")
        CommonFunction.LoadDataToGrid(grvRejected, dtData, String.Format("Status = '{0}'", AirticketRequestStatus.Rejected))
        CommonFunction.LoadDataToGrid(grvSubmitted, dtData, String.Format("Status = '{0}'", AirticketRequestStatus.Submitted))
        CommonFunction.LoadDataToGrid(grvApproved, dtData, String.Format("Status = '{0}'", AirticketRequestStatus.Approved))
    End Sub

    Protected Sub btnSubmit_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim obj As tblBTAirTicketRequestInfo = GetRequestObject()
            Dim objRelatives As List(Of tblBTAirTicketRelativeInfo) = GetRelativeList()
            'If Not IsValidRequest(obj) Then
            '    CommonFunction.SetProcessStatus(btnSaveOtherAirTicket, False)
            '    Return
            'End If
            AirTicketProvider.BTAirTicketRequest_Update(obj, objRelatives)
            '
            Dim comment As String = txtSubmitComment.Text.Trim()
            _status = AirticketRequestStatus.Submitted.ToString()
            Dim message As String = AirTicketProvider.BTAirTicketRequest_UpdateStatus(CommonFunction._ToInt(hRequestID.Value), _status, _username, comment)
            If message.Length > 0 Then
                CommonFunction.ShowErrorMessage(panMessage, message)
            Else
                LoadBTAirTicket()
                LoadBTOtherAirTicketByID()
                'Send notice email
                If SendSubmitEmail() Then
                    CommonFunction.ShowInfoMessage(panMessage, "Submited!")
                Else
                    CommonFunction.ShowInfoMessage(panMessage, "Submited but fail to send notice emails! Please contact with administrator!")
                End If
                _objEmail.Dispose()
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnRecall_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnRecall.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            _status = AirticketRequestStatus.Prepared.ToString()
            Dim message As String = AirTicketProvider.BTAirTicketRequest_Recall(CommonFunction._ToInt(hRequestID.Value), _status, _username)
            If message.Length > 0 Then
                CommonFunction.ShowErrorMessage(panMessage, message)
            Else
                LoadBTAirTicket()
                LoadBTOtherAirTicketByID()
                'Send notice email
                If SendSubmitEmail() Then
                    CommonFunction.ShowInfoMessage(panMessage, "Recalled!")
                Else
                    CommonFunction.ShowInfoMessage(panMessage, "Recalled but fail to send notice emails! Please contact with administrator!")
                End If
                _objEmail.Dispose()
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Sub EnableOtherAirTicketForm(ByVal status As String)
        Dim enable As Boolean = (status.Length = 0 _
                                 OrElse status = AirticketRequestStatus.Prepared.ToString() _
                                 OrElse status = AirticketRequestStatus.Rejected.ToString())
        txtRequesterCode.ReadOnly = Not enable
        txtPurpose.ReadOnly = Not enable
        txtRequestBudgetCode.ReadOnly = Not enable
        ddlFromCountry.Enabled = enable
        ddlFromDestination.Enabled = enable
        ddlToCountry.Enabled = enable
        ddlToDestination.Enabled = enable
        dteDepartureDate.ReadOnly = Not enable
        dteReturnDate.ReadOnly = Not enable
        '
        chkRequestAirTicket.Enabled = enable
        ddlRelativeFromCountry.Enabled = enable
        ddlRelativeFromDestination.Enabled = enable
        ddlRelativeToCountry.Enabled = enable
        ddlRelativeToDestination.Enabled = enable
        dteRelativeDepartureDate.ReadOnly = Not enable
        dteRelativeReturnDate.ReadOnly = Not enable
        If Not enable Then
            divBtnAddMore.Attributes("class") = "hide"
        End If
        For i As Integer = 1 To 10
            Dim txt As Web.UI.Control = upAirTicketRequest.FindControl(String.Concat("txtRelativeName", i))
            Dim ddl As Web.UI.Control = upAirTicketRequest.FindControl(String.Concat("ddlRelationship", i))
            If txt IsNot Nothing Then
                CType(txt, TextBox).ReadOnly = Not enable
            End If
            If ddl IsNot Nothing Then
                CType(ddl, DropDownList).Enabled = enable
            End If
        Next
        '
        btnShowSaveOtherAirTicket.Visible = enable
        btnSaveOtherAirTicket.Visible = enable
        '
        Dim enableSubmit As Boolean = (status = AirticketRequestStatus.Prepared.ToString() _
                                       OrElse status = AirticketRequestStatus.Rejected.ToString())
        btnShowSubmit.Visible = enableSubmit
        panSubmitInfo.Visible = enableSubmit
        '
        Dim enableRecall As Boolean = (status = AirticketRequestStatus.Submitted.ToString())
        btnConfirmRecall.Visible = enableRecall
        btnRecall.Visible = enableRecall
    End Sub

    Private Sub LoadBTOtherAirTicketByID()
        Dim id As Integer = CommonFunction._ToInt(hRequestID.Value)
        Dim dsData As DataSet = AirTicketProvider.BTAirTicketRequest_GetByID(id)
        Dim dtRequest As DataTable = dsData.Tables(0)
        Dim dtRelative As DataTable = dsData.Tables(1)
        Dim dtHistory As DataTable = dsData.Tables(2)
        If dtRequest.Rows.Count > 0 Then
            CommonFunction.LoadDataToGrid(grvStatusHistory, dtHistory, "", "No")
            '
            Dim drRequest As DataRow = dtRequest.Rows(0)
            _status = CommonFunction._ToString(drRequest("Status"))
            lblRequestStatus.Text = _status
            _statusMessage = CommonFunction._ToString(drRequest("Message"))
            lblRequestMessage.Text = If(_statusMessage.Trim().Length > 0, String.Format(" ({0})", _statusMessage.Trim()), "")
            txtRequesterCode.Text = CommonFunction._ToString(drRequest("EmployeeCode"))
            txtRequesterName.Text = CommonFunction._ToString(drRequest("EmployeeName"))
            txtRequesterDivision.Text = CommonFunction._ToString(drRequest("EmployeeDivision"))
            txtRequesterDepartment.Text = CommonFunction._ToString(drRequest("EmployeeDepartment"))
            txtPurpose.Text = CommonFunction._ToString(drRequest("Purpose"))
            txtRequestBudgetCode.Text = CommonFunction._ToString(drRequest("BudgetCode"))
            CommonFunction.SetCBOValue(ddlFromCountry, drRequest("FromCountry"))
            InitFromDestination()
            CommonFunction.SetCBOValue(ddlFromDestination, drRequest("FromDestination"))
            CommonFunction.SetCBOValue(ddlToCountry, drRequest("ToCountry"))
            InitToDestination()
            CommonFunction.SetCBOValue(ddlToDestination, drRequest("ToDestination"))
            dteDepartureDate.Value = drRequest("DepartureDate")
            dteReturnDate.Value = drRequest("ReturnDate")
            '
            Dim relCount As Integer = dtRelative.Rows.Count
            If relCount > 3 Then
                hAddMore.Value = "T"
                tblAddMore.Attributes("class") = "ui-panelgrid ui-widget grid-edit"
                divBtnAddMore.Attributes("class") = "hide"
            Else
                hAddMore.Value = "F"
                tblAddMore.Attributes("class") = "ui-panelgrid ui-widget grid-edit hide"
                divBtnAddMore.Attributes("class") = ""
            End If

            For i As Integer = 1 To 10
                Dim txt As Web.UI.Control = upAirTicketRequest.FindControl(String.Concat("txtRelativeName", i))
                Dim ddl As Web.UI.Control = upAirTicketRequest.FindControl(String.Concat("ddlRelationship", i))
                If txt IsNot Nothing Then
                    CType(txt, TextBox).Text = ""
                End If
                If ddl IsNot Nothing Then
                    CommonFunction.SetCBOValue(CType(ddl, DropDownList), "")
                End If
            Next
            If relCount = 0 Then
                chkRequestAirTicket.Checked = True
                ShowRelativeInfo(chkRequestAirTicket.Checked)
                CommonFunction.SetCBOValue(ddlRelativeFromCountry, "")
                InitRelativeFromDestination()
                CommonFunction.SetCBOValue(ddlRelativeFromDestination, "")
                CommonFunction.SetCBOValue(ddlRelativeToCountry, "")
                InitRelativeToDestination()
                CommonFunction.SetCBOValue(ddlRelativeToDestination, "")
                dteRelativeDepartureDate.Value = Nothing
                dteRelativeReturnDate.Value = Nothing
            Else
                For i As Integer = 0 To relCount - 1
                    Dim drRelative As DataRow = dtRelative.Rows(i)
                    If i = 0 Then
                        chkRequestAirTicket.Checked = CommonFunction._ToBoolean(drRelative("SameAsEmployee"))
                        ShowRelativeInfo(chkRequestAirTicket.Checked)
                        If chkRequestAirTicket.Checked Then
                            CommonFunction.SetCBOValue(ddlRelativeFromCountry, "")
                            InitRelativeFromDestination()
                            CommonFunction.SetCBOValue(ddlRelativeFromDestination, "")
                            CommonFunction.SetCBOValue(ddlRelativeToCountry, "")
                            InitRelativeToDestination()
                            CommonFunction.SetCBOValue(ddlRelativeToDestination, "")
                            dteRelativeDepartureDate.Value = Nothing
                            dteRelativeReturnDate.Value = Nothing
                        Else
                            CommonFunction.SetCBOValue(ddlRelativeFromCountry, drRelative("FromCountry"))
                            InitRelativeFromDestination()
                            CommonFunction.SetCBOValue(ddlRelativeFromDestination, drRelative("FromDestination"))
                            CommonFunction.SetCBOValue(ddlRelativeToCountry, drRelative("ToCountry"))
                            InitRelativeToDestination()
                            CommonFunction.SetCBOValue(ddlRelativeToDestination, drRelative("ToDestination"))
                            dteRelativeDepartureDate.Value = drRelative("DepartureDate")
                            dteRelativeReturnDate.Value = drRelative("ReturnDate")
                        End If
                    End If
                    Dim txt As Web.UI.Control = upAirTicketRequest.FindControl(String.Concat("txtRelativeName", i + 1))
                    Dim ddl As Web.UI.Control = upAirTicketRequest.FindControl(String.Concat("ddlRelationship", i + 1))
                    If txt IsNot Nothing Then
                        CType(txt, TextBox).Text = drRelative("Name")
                    End If
                    If ddl IsNot Nothing Then
                        CommonFunction.SetCBOValue(CType(ddl, DropDownList), drRelative("Relationship"))
                    End If
                Next
            End If
            EnableOtherAirTicketForm(_status)
        End If
    End Sub

    Private Sub ClearRequestForm()
        hRequestID.Value = ""
        lblRequestStatus.Text = "N/A"
        lblRequestMessage.Text = ""
        'dteOtherDate.Date = DateTime.Now                
        txtRequesterCode.Text = ""
        txtRequesterName.Text = ""
        txtRequesterDepartment.Text = ""
        txtRequesterDivision.Text = ""
        txtPurpose.Text = ""
        txtRequestBudgetCode.Text = ""
        CommonFunction.SetCBOValue(ddlFromCountry, "")
        InitFromDestination()
        CommonFunction.SetCBOValue(ddlFromDestination, "")
        CommonFunction.SetCBOValue(ddlToCountry, "")
        InitToDestination()
        CommonFunction.SetCBOValue(ddlToDestination, "")
        dteDepartureDate.Value = Nothing
        dteReturnDate.Value = Nothing
        '
        For i As Integer = 1 To 10
            Dim txt As Web.UI.Control = upAirTicketRequest.FindControl(String.Concat("txtRelativeName", i))
            Dim ddl As Web.UI.Control = upAirTicketRequest.FindControl(String.Concat("ddlRelationship", i))
            If txt IsNot Nothing Then
                CType(txt, TextBox).Text = ""
            End If
            If ddl IsNot Nothing Then
                CommonFunction.SetCBOValue(CType(ddl, DropDownList), "")
            End If
        Next
        chkRequestAirTicket.Checked = True
        ShowRelativeInfo(chkRequestAirTicket.Checked)
        CommonFunction.SetCBOValue(ddlRelativeFromCountry, "")
        InitRelativeFromDestination()
        CommonFunction.SetCBOValue(ddlRelativeFromDestination, "")
        CommonFunction.SetCBOValue(ddlRelativeToCountry, "")
        InitRelativeToDestination()
        CommonFunction.SetCBOValue(ddlRelativeToDestination, "")
        dteRelativeDepartureDate.Value = Nothing
        dteRelativeReturnDate.Value = Nothing
        hAddMore.Value = "F"
        tblAddMore.Attributes("class") = "ui-panelgrid ui-widget grid-edit hide"
        divBtnAddMore.Attributes("class") = ""
        '
        EnableOtherAirTicketForm("")
    End Sub

    Private Function GetRequestObject() As tblBTAirTicketRequestInfo
        Dim obj As New tblBTAirTicketRequestInfo()
        obj.ID = CommonFunction._ToInt(hRequestID.Value)
        obj.CreatedBy = _username
        obj.ModifiedBy = _username
        obj.EmployeeCode = txtRequesterCode.Text.Trim()
        obj.EmployeeDivision = txtRequesterDivision.Text.Trim()
        obj.EmployeeDepartment = txtRequesterDepartment.Text.Trim()
        obj.EmployeeName = txtRequesterName.Text.Trim()
        obj.FromCountry = ddlFromCountry.SelectedValue
        obj.FromDestination = CommonFunction._ToInt(ddlFromDestination.SelectedValue)
        obj.ToCountry = ddlToCountry.SelectedValue
        obj.ToDestination = CommonFunction._ToInt(ddlToDestination.SelectedValue)
        obj.DepartureDate = dteDepartureDate.Date
        obj.ReturnDate = dteReturnDate.Date
        obj.Purpose = txtPurpose.Text.Trim()
        obj.BudgetCode = txtRequestBudgetCode.Text.Trim()
        Return obj
    End Function

    Private Function GetRelativeList() As List(Of tblBTAirTicketRelativeInfo)
        Dim list As New List(Of tblBTAirTicketRelativeInfo)()
        For i As Integer = 1 To 10
            Dim obj As tblBTAirTicketRelativeInfo = Nothing
            Dim txt As Web.UI.Control = upAirTicketRequest.FindControl(String.Concat("txtRelativeName", i))
            Dim ddl As Web.UI.Control = upAirTicketRequest.FindControl(String.Concat("ddlRelationship", i))
            Dim relative As String = ""
            Dim relationShip As String = ""
            If txt IsNot Nothing Then
                relative = CType(txt, TextBox).Text.Trim()
            End If
            If ddl IsNot Nothing Then
                relationShip = CType(ddl, DropDownList).SelectedValue
            End If
            If relative.Length > 0 AndAlso relationShip.Length > 0 Then
                obj = New tblBTAirTicketRelativeInfo()
                obj.SameAsEmployee = chkRequestAirTicket.Checked
                If chkRequestAirTicket.Checked Then
                    obj.FromCountry = ddlFromCountry.SelectedValue
                    obj.ToCountry = ddlToCountry.SelectedValue
                    obj.FromDestination = CommonFunction._ToInt(ddlFromDestination.SelectedValue)
                    obj.ToDestination = CommonFunction._ToInt(ddlToDestination.SelectedValue)
                    obj.DepartureDate = dteDepartureDate.Date
                    obj.ReturnDate = dteReturnDate.Date
                Else
                    obj.FromCountry = ddlRelativeFromCountry.SelectedValue
                    obj.ToCountry = ddlRelativeToCountry.SelectedValue
                    obj.FromDestination = CommonFunction._ToInt(ddlRelativeFromDestination.SelectedValue)
                    obj.ToDestination = CommonFunction._ToInt(ddlRelativeToDestination.SelectedValue)
                    obj.DepartureDate = dteRelativeDepartureDate.Date
                    obj.ReturnDate = dteRelativeReturnDate.Date
                End If
                obj.Name = relative
                obj.Relationship = relationShip
            End If
            If obj IsNot Nothing Then
                list.Add(obj)
            End If
        Next
        Return list
    End Function

    Protected Sub grvPrepared_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) _
    Handles grvPrepared.BeforeGetCallbackResult, grvRejected.BeforeGetCallbackResult, _
        grvSubmitted.BeforeGetCallbackResult, grvApproved.BeforeGetCallbackResult
        LoadBTAirTicket()
    End Sub

    Protected Sub btnSaveOtherAirTicket_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveOtherAirTicket.Click
        CommonFunction.SetPostBackStatus(btnSaveOtherAirTicket)
        Try
            Dim obj As tblBTAirTicketRequestInfo = GetRequestObject()
            Dim objRelatives As List(Of tblBTAirTicketRelativeInfo) = GetRelativeList()
            If Not IsValidRequest(obj, objRelatives) Then
                CommonFunction.SetProcessStatus(btnSaveOtherAirTicket, False)
                Return
            End If
            If obj.ID > 0 Then
                AirTicketProvider.BTAirTicketRequest_Update(obj, objRelatives)
            Else
                obj.Status = AirticketRequestStatus.Prepared.ToString()
                Dim requestID As Decimal = AirTicketProvider.BTAirTicketRequest_Insert(obj, objRelatives)
                hRequestID.Value = requestID
                LoadBTOtherAirTicketByID()
            End If
            CommonFunction.SetProcessStatus(btnSaveOtherAirTicket, True)
            CommonFunction.ShowInfoMessage(panMessage, "Your data is saved successfully!")
            LoadBTAirTicket()
        Catch ex As Exception
            CommonFunction.SetProcessStatus(btnSaveOtherAirTicket, False)
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Function IsValidRequest(ByVal obj As tblBTAirTicketRequestInfo, ByVal objRelatives As List(Of tblBTAirTicketRelativeInfo)) As Boolean
        Dim isValid As Boolean = True
        dteDepartureDate.CssClass = dteDepartureDate.CssClass.Replace("validate-error", "")
        dteReturnDate.CssClass = dteReturnDate.CssClass.Replace("validate-error", "")
        If Not AirTicketProvider.BTAirTicketRequest_Validate(obj.ID, obj.EmployeeCode, obj.DepartureDate, obj.ReturnDate) Then
            CommonFunction.ShowErrorMessage(panMessage, "Request Date is conflict!")
            dteDepartureDate.CssClass &= " validate-error"
            dteReturnDate.CssClass &= " validate-error"
            isValid = False
        Else
            Dim isValidRelative As Boolean = True
            Dim relativeNames As String = "^"
            For i As Integer = 0 To objRelatives.Count - 1
                Dim r As tblBTAirTicketRelativeInfo = objRelatives(i)
                If Not AirTicketProvider.BTAirTicketRelative_Validate(r.AirTicketRequestID, r.Name, r.DepartureDate, r.ReturnDate) Then
                    relativeNames = String.Concat(relativeNames, ", ", r.Name)
                    isValidRelative = False
                End If
            Next
            If Not isValidRelative Then
                relativeNames = relativeNames.Replace("^, ", "")
                CommonFunction.ShowErrorMessage(panMessage, String.Format("Relative Request Date is conflict ({0})!", relativeNames))
                isValid = False
            End If
        End If
        Return isValid
    End Function

    Protected Sub btnCancelOtherAirTicket_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelOtherAirTicket.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btn)
        Try
            ClearRequestForm()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditOtherAirTicket_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            'txtTicketNo.CssClass = txtTicketNo.CssClass.Replace(" validate-error", "")            
            '
            'hAirTicketID.Value = btn.Attributes("data-id")                        
            LoadBTOtherAirTicketByID()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteOtherAirTicket_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim id As Integer = CommonFunction._ToInt(hRequestID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            AirTicketProvider.BTAirTicketRequest_Delete(id)
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
            LoadBTAirTicket()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnGetRequesterInfo_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnGetRequesterInfo.Click
        CommonFunction.SetPostBackStatus(btnGetRequesterInfo)
        Try
            Dim info As String() = hRequester.Value.Split("-")
            txtRequesterCode.Text = info(0)
            txtRequesterName.Text = info(1)
            txtRequesterDivision.Text = info(2)
            txtRequesterDepartment.Text = info(3)
            'txtRequesterPhone.Text = info(4)
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub ddlFromCoutry_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlFromCountry.SelectedIndexChanged
        'CommonFunction.SetPostBackStatus(btnCancelOtherAirTicket)
        InitFromDestination()
    End Sub

    Protected Sub ddlToCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlToCountry.SelectedIndexChanged
        'CommonFunction.SetPostBackStatus(btnCancelOtherAirTicket)
        InitToDestination()
    End Sub

    Protected Sub ddlRelativeFromCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlRelativeFromCountry.SelectedIndexChanged
        'CommonFunction.SetPostBackStatus(btnCancelOtherAirTicket)
        InitRelativeFromDestination()
    End Sub

    Protected Sub ddlRelativeToCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlRelativeToCountry.SelectedIndexChanged
        'CommonFunction.SetPostBackStatus(btnCancelOtherAirTicket)
        InitRelativeToDestination()
    End Sub

    Protected Sub chkRequestAirTicket_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkRequestAirTicket.CheckedChanged
        'CommonFunction.SetPostBackStatus(btnCancelOtherAirTicket)
        ShowRelativeInfo(chkRequestAirTicket.Checked)
    End Sub

    Private Sub ShowRelativeInfo(ByVal sameAsEmployee As Boolean)
        trRelativeFrom.Visible = Not sameAsEmployee
        trRelativeTo.Visible = Not sameAsEmployee
        trRelativeTime.Visible = Not sameAsEmployee
    End Sub

#Region "Send Notice Email"
    Private _status As String
    Private _statusMessage As String
    Private Function GenNoticeBody(Optional ByVal link As String = "") As String
        Dim fromDestination As String = ddlFromDestination.SelectedValue
        Dim toDestination As String = ddlToDestination.SelectedValue
        Dim sFrom As String = ""
        If fromDestination IsNot Nothing AndAlso fromDestination.Trim().Length > 0 Then
            sFrom = String.Concat(sFrom, ddlFromDestination.SelectedItem.Text, "/")
        End If
        sFrom = String.Concat(sFrom, ddlFromCountry.SelectedItem.Text)
        Dim sTo As String = ""
        If toDestination IsNot Nothing AndAlso toDestination.Trim().Length > 0 Then
            sTo = String.Concat(sTo, ddlToDestination.SelectedItem.Text, "/")
        End If
        sTo = String.Concat(sTo, ddlToCountry.SelectedItem.Text)
        '
        Dim eBody As New StringBuilder("<p><strong>To whom it may concern</strong></p>")
        eBody.Append("<p>This is notification from <strong>B</strong>usiness <strong>T</strong>rip Online <strong>S</strong>ystem (BTS).</p>")
        eBody.Append("<p>Regarding air ticket request, we would like to share the latest information to you as below:")
        eBody.Append(String.Format("<p><table><tr><td valign='top' width='210'><ul><li style='margin: 0;'><strong>Employee Code:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", txtRequesterCode.Text.Trim()))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Employee Name:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", txtRequesterName.Text.Trim()))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>From:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", sFrom))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>To:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", sTo))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Departure Date:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", If(dteDepartureDate.Value IsNot Nothing, dteDepartureDate.Date.ToString("dd-MMM-yyyy HH:mm"), "")))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Return Date:</strong></li></ul></td><td style='color: #0070c0'>{0}</td></tr>", If(dteReturnDate.Value IsNot Nothing, dteReturnDate.Date.ToString("dd-MMM-yyyy HH:mm"), "")))
        eBody.Append(String.Format("<tr><td valign='top'><ul><li style='margin: 0;'><strong>Status:</strong></li></ul></td><td style='color: #0070c0'>{0}{1}</td></tr></table></p>", _status, If(_statusMessage.Trim().Length > 0, String.Format(" ({0})", _statusMessage.Trim()), "")))
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
                eTo = "sycuongdx@toyotavn.com.vn, sudungnq@toyotavn.com.vn"
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
            Dim eSubject As String = String.Format("{0}[BTS Air Ticket Request]: {1} / {2} (Status: {3})", indicator, txtRequesterCode.Text.Trim(), txtRequesterName.Text.Trim(), _status)
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
            dvFI.RowFilter = String.Format("Role in ('{0}', '{1}', '{2}', '{3}')", RoleType.GA, RoleType.TOFS_AIR_GA, RoleType.Finance_GA, RoleType.Administrator.ToString())
            For Each dr As DataRowView In dvFI
                eTo = String.Concat(eTo, ", ", dr("TMVEmail"))
            Next
            If eTo.Length > 0 Then
                eTo = eTo.Substring(1).Trim()
            End If
        End If
        Dim viewLink As String = "BTAirTicketRequestGA.aspx" 'String.Format("BTAirTicket.aspx?btid=1&period={0}&supplier={1}", hSAirPeriod.Value, hSOraSupplier.Value)
        Dim eBody As String = GenNoticeBody(viewLink)
        Return SendNoticeEmail(eTo, eBody)
    End Function

    Private Function SendRecallEmail() As Boolean
        Dim eTo As String = ""
        If _sendEmailMode = SendEmailMode.UserTest.ToString().ToLower() Then
            eTo = UserProvider.UserTestEmail_Get(CommonFunction._ToString(Session("Branch")), RoleType.Finance.ToString())
        Else
            Dim dtAuthorized As DataTable = UserProvider.GetActive()
            Dim dvFI As DataView = dtAuthorized.DefaultView
            dvFI.RowFilter = String.Format("Role in ('{0}', '{1}', '{2}', '{3}')", RoleType.GA, RoleType.TOFS_AIR_GA, RoleType.Finance_GA, RoleType.Administrator.ToString())
            For Each dr As DataRowView In dvFI
                eTo = String.Concat(eTo, ", ", dr("TMVEmail"))
            Next
            If eTo.Length > 0 Then
                eTo = eTo.Substring(1).Trim()
            End If
        End If
        Dim viewLink As String = "BTAirTicketRequestGA.aspx" 'String.Format("BTAirTicket.aspx?btid=1&period={0}&supplier={1}", hSAirPeriod.Value, hSOraSupplier.Value)
        Dim eBody As String = GenNoticeBody(viewLink)
        Return SendNoticeEmail(eTo, eBody)
    End Function
#End Region

End Class