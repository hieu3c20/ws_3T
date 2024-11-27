Imports Provider
Imports System.Data
Imports Microsoft.Office.Interop
Imports System.IO

Partial Public Class Report
    Inherits System.Web.UI.Page

    Protected _username As String = String.Empty
    Private _dtAuthorized As DataTable
    Private _rptNo As Integer = 0

    Protected ReadOnly Property RptNo() As Integer
        Get
            _rptNo += 1
            Return _rptNo
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CommonFunction.CheckSession()
        _username = CommonFunction._ToString(Session("UserName"))
        If Not IsPostBack Then
            InitForm()
        End If
    End Sub

    Private Sub InitForm()
        InitCommonCondition()
        '
        dteDepartureDateFrom.Date = New Date(DateTime.Now.Year, DateTime.Now.Month, 1)
        dteDepartureDateTo.Date = DateTime.Now
        '
        InitAirPeriod()
        InitOraSupplier()
        '
        InitBTType()
        InitCountry()
    End Sub

    Private Sub InitCommonCondition()
        Dim ds As DataSet = ReportProvider.GetInitReportCondition(_username)
        ds.Tables(1).TableName = "Team"
        ds.Tables(2).TableName = "Group"
        ds.Tables(3).TableName = "Section"
        ds.Tables(4).TableName = "Department"
        ds.Tables(5).TableName = "Division"
        ds.Tables(6).TableName = "Branch"
        CommonFunction.LoadDataToComboBox(ddlSection, ds.Tables("Section"), "SectionName", "SectionID", True)
        CommonFunction.LoadDataToComboBox(ddlDepartment, ds.Tables("Department"), "DepartmentName", "DepartmentID", True)
        CommonFunction.LoadDataToComboBox(ddlDivision, ds.Tables("Division"), "DivisionName", "DivisionID", True)
        CommonFunction.LoadDataToComboBox(ddlBranch, ds.Tables("Branch"), "BranchName", "BranchID", True)
    End Sub

    Private Sub InitAirPeriod()
        Dim dt As DataTable = AirTicketProvider.AirPeriod_GetAll()
        CommonFunction.LoadDataToComboBox(ddlAirPeriod, dt, "Name", "ID", True, "All", "")
    End Sub

    Private Sub InitOraSupplier()
        Dim dtBT As DataTable = mOraSupplierProvider.GetActive()
        CommonFunction.LoadDataToComboBox(ddlOraSupplier, dtBT, "SupplierName", "OraLink", True, "All", "")
    End Sub

    Private Sub InitBTType()
        Dim dt As DataTable = LookupProvider.GetByCode("BT_TYPE")
        CommonFunction.LoadLookupDataToComboBox(ddlBTType, dt, True, "All", "", "Value like 'overnight_%'")
    End Sub

    Private Sub InitCountry()
        Dim isDomestic As Boolean = ddlBTType.SelectedValue.IndexOf("domestic") >= 0
        Dim filter As String = If(ddlBTType.SelectedValue.Trim().Length = 0, "", If(Not isDomestic, "Code <> 'VN'", "Code = 'VN'"))
        Dim dt As DataTable = mCountryProvider.GetAll()
        CommonFunction.LoadDataToComboBox(ddlCountry, dt, "Name", "Code", True, "", "", filter)
        If isDomestic Then
            CommonFunction.SetCBOValue(ddlCountry, "VN")
            ddlCountry.Enabled = False
        Else
            ddlCountry.Enabled = True
        End If
    End Sub

    Protected Sub ddlBTType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlBTType.SelectedIndexChanged
        CommonFunction.SetPostBackStatus(ddlBTType)
        Try
            InitCountry()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Sub LoadForm()

    End Sub

    Protected Function GetAuthorizedAccounts() As String
        Dim builder As New StringBuilder()
        _dtAuthorized = ReportProvider.GetInitReportCondition(_username).Tables(0)
        builder.Append("[")
        For Each item As DataRow In _dtAuthorized.Rows
            builder.Append(String.Concat("{ value: '", item("EmployeeCode"), " - ", item("EmployeeName"), " ', data: '", item("EmployeeCode"), " - ", item("EmployeeName"), "'},"))
        Next
        builder = New StringBuilder(builder.ToString().TrimEnd(","))
        builder.Append("]")
        Return builder.ToString()
    End Function

    Protected Sub chkCondition_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles radBranchAll.CheckedChanged, _
            radBranchChoose.CheckedChanged, radBranchNoChoose.CheckedChanged, radDeptAll.CheckedChanged, radDeptChoose.CheckedChanged, _
            radDeptNoChoose.CheckedChanged, radDivAll.CheckedChanged, radDivChoose.CheckedChanged, radDivNoChoose.CheckedChanged, radEmpAll.CheckedChanged, _
            radEmpChoose.CheckedChanged, radEmpNoChoose.CheckedChanged, radSecAll.CheckedChanged, radSecChoose.CheckedChanged, radSecNoChoose.CheckedChanged
        CommonFunction.SetPostBackStatus(btnMessage)
        Try
            panReportCommon.Attributes("style") = panReportCommon.Attributes("style").Replace("display: none", "display: block").Replace("display:none", "display:block")
            '
            ddlBranch.Enabled = Not radBranchAll.Checked
            If radBranchAll.Checked Then
                ddlBranch.ClearSelection()
            End If
            ddlDivision.Enabled = Not radDivAll.Checked
            If radDivAll.Checked Then
                ddlDepartment.ClearSelection()
            End If
            ddlDepartment.Enabled = Not radDeptAll.Checked
            If radDeptAll.Checked Then
                ddlDepartment.ClearSelection()
            End If
            ddlSection.Enabled = Not radSecAll.Checked
            If radSecAll.Checked Then
                ddlSection.ClearSelection()
            End If
            txtEmployeeCode.Enabled = Not radEmpAll.Checked
            If radEmpAll.Checked Then
                txtEmployeeCode.Text = ""
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnCancelCommonReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelCommonReport.Click
        CommonFunction.SetPostBackStatus(btnMessage)
        Try
            panReportCommon.Attributes("style") = panReportCommon.Attributes("style").Replace("display: block", "display: none").Replace("display:block", "display:none")
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnOKCommonReport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnOKCommonReport.Click
        CommonFunction.SetPostBackStatus(btnMessage)
        Try
            panReportCommon.Attributes("style") = panReportCommon.Attributes("style").Replace("display: none", "display: block").Replace("display:none", "display:block")
            '
            Select Case hReportType.Value.ToLower()
                Case "advance"
                    AdvanceReport()
                Case "expense"
                    ExpenseReport()
                Case "advance-clear"
                    AdvanceClearReport()
                Case "noadvance-clear"
                    NoAdvanceClearReport()
                Case "airticket-compare"
                    AirTicketCompareReport()
                Case "wifi-device"
                    WifiDeviceReport()
            End Select
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Function GetReportConditions() As ReportInfo
        Dim obj As New ReportInfo
        obj.Username = _username
        obj.DepartureFrom = dteDepartureDateFrom.Date
        obj.DepartureTo = dteDepartureDateTo.Date
        obj.BranchID = If(radBranchAll.Checked, -1, CommonFunction._ToUnsignInt(ddlBranch.SelectedValue))
        obj.ExceptBranch = radBranchNoChoose.Checked
        obj.DivID = If(radDivAll.Checked, -1, CommonFunction._ToUnsignInt(ddlDivision.SelectedValue))
        obj.ExceptDiv = radDivNoChoose.Checked
        obj.DeptID = If(radDeptAll.Checked, -1, CommonFunction._ToUnsignInt(ddlDepartment.SelectedValue))
        obj.ExceptDept = radDeptNoChoose.Checked
        obj.SecID = If(radSecAll.Checked, -1, CommonFunction._ToUnsignInt(ddlSection.SelectedValue))
        obj.ExceptSec = radSecNoChoose.Checked
        obj.GroupID = -1
        obj.ExceptGroup = False
        obj.TeamID = -1
        obj.ExceptTeam = False
        obj.EmployeeCode = If(radEmpAll.Checked, "", If(txtEmployeeCode.Text.Trim().Length = 0, "0", txtEmployeeCode.Text.Split("-")(0).Trim()))
        obj.ExceptEmployeeCode = radEmpNoChoose.Checked
        'air ticket
        obj.AirPeriod = CommonFunction._ToInt(ddlAirPeriod.SelectedValue)
        obj.OraSupplier = ddlOraSupplier.SelectedValue
        'wifi device
        obj.BTType = ddlBTType.SelectedValue
        obj.CountryID = CommonFunction._ToInt(ddlCountry.SelectedValue)
        Return obj
    End Function

#Region "Report"

    Private Sub AdvanceReport()
        Dim xlApp As Excel.Application = Nothing
        Dim xlWorkBook As Excel.Workbook = Nothing
        Dim xlWorkSheet As Excel.Worksheet = Nothing
        Try
            Dim tmp As String = Server.MapPath(String.Concat("\Export\Template\Report\advance_report.xlt"))
            '            
            Dim misValue As Object = System.Reflection.Missing.Value
            '
            xlApp = New Excel.ApplicationClass()
            xlWorkBook = xlApp.Workbooks.Open(tmp, 0, True, 5, "", "", True, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, 1, 0)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            'business
            Dim obj As ReportInfo = GetReportConditions()
            Dim rowIndex As Integer = 2
            xlWorkSheet.Cells(rowIndex, 3) = If(obj.DepartureFrom = DateTime.MinValue, "Begining", obj.DepartureFrom.ToString("dd-MMM-yyyy"))
            xlWorkSheet.Cells(rowIndex, 6) = If(obj.DepartureTo = DateTime.MinValue, "Today", obj.DepartureTo.ToString("dd-MMM-yyyy"))
            rowIndex += 1
            xlWorkSheet.Cells(rowIndex, 3) = If(radBranchAll.Checked, "All branch", If(radBranchNoChoose.Checked, "Except ", "") + If(ddlBranch.SelectedItem IsNot Nothing, ddlBranch.SelectedItem.Text, ""))
            xlWorkSheet.Cells(rowIndex, 6) = If(radDivAll.Checked, "All division", If(radDivNoChoose.Checked, "Except ", "") + If(ddlDivision.SelectedItem IsNot Nothing, ddlDivision.SelectedItem.Text, ""))
            rowIndex += 1
            xlWorkSheet.Cells(rowIndex, 3) = If(radDeptAll.Checked, "All department", If(radDeptNoChoose.Checked, "Except ", "") + If(ddlDepartment.SelectedItem IsNot Nothing, ddlDepartment.SelectedItem.Text, ""))
            xlWorkSheet.Cells(rowIndex, 6) = If(radSecAll.Checked, "All section", If(radSecNoChoose.Checked, "Except ", "") + If(ddlSection.SelectedItem IsNot Nothing, ddlSection.SelectedItem.Text, ""))
            '
            Dim ds As DataSet = ReportProvider.AdvanceReport(obj)
            ds.Tables(0).TableName = "Data"
            '
            rowIndex = 7
            For i As Integer = 0 To ds.Tables("Data").Rows.Count - 1
                Dim dr As DataRow = ds.Tables("Data").Rows(i)
                Dim colIndex As Integer = 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("No")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("BTNo")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("EmployeeCode")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("EmployeeName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("DepartureDate")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("ReturnDate")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Destination")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Purpose")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                rowIndex += 1
            Next
            '            
            Dim filePath As String = String.Concat("~/Export/Download/", _username)
            Dim dir As DirectoryInfo = New DirectoryInfo(Server.MapPath(filePath))
            If Not dir.Exists() Then
                dir.Create()
            End If
            filePath = String.Concat(filePath, "/", "advance_report_", _username, "_", DateTime.Now.ToString("yyyyMMddHHmmssfffff"), ".xls")
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
            btnMessage.Attributes("data-file-path") = filePath.Substring(1)
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

    Private Sub ExpenseReport()
        Dim xlApp As Excel.Application = Nothing
        Dim xlWorkBook As Excel.Workbook = Nothing
        Dim xlWorkSheet As Excel.Worksheet = Nothing
        Try
            Dim tmp As String = Server.MapPath(String.Concat("\Export\Template\Report\expense_report.xlt"))
            '            
            Dim misValue As Object = System.Reflection.Missing.Value
            '
            xlApp = New Excel.ApplicationClass()
            xlWorkBook = xlApp.Workbooks.Open(tmp, 0, True, 5, "", "", True, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, 1, 0)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            'business
            Dim obj As ReportInfo = GetReportConditions()
            Dim rowIndex As Integer = 2
            xlWorkSheet.Cells(rowIndex, 3) = If(obj.DepartureFrom = DateTime.MinValue, "Begining", obj.DepartureFrom.ToString("dd-MMM-yyyy"))
            xlWorkSheet.Cells(rowIndex, 6) = If(obj.DepartureTo = DateTime.MinValue, "Today", obj.DepartureTo.ToString("dd-MMM-yyyy"))
            rowIndex += 1
            xlWorkSheet.Cells(rowIndex, 3) = If(radBranchAll.Checked, "All branch", If(radBranchNoChoose.Checked, "Except ", "") + If(ddlBranch.SelectedItem IsNot Nothing, ddlBranch.SelectedItem.Text, ""))
            xlWorkSheet.Cells(rowIndex, 6) = If(radDivAll.Checked, "All division", If(radDivNoChoose.Checked, "Except ", "") + If(ddlDivision.SelectedItem IsNot Nothing, ddlDivision.SelectedItem.Text, ""))
            rowIndex += 1
            xlWorkSheet.Cells(rowIndex, 3) = If(radDeptAll.Checked, "All department", If(radDeptNoChoose.Checked, "Except ", "") + If(ddlDepartment.SelectedItem IsNot Nothing, ddlDepartment.SelectedItem.Text, ""))
            xlWorkSheet.Cells(rowIndex, 6) = If(radSecAll.Checked, "All section", If(radSecNoChoose.Checked, "Except ", "") + If(ddlSection.SelectedItem IsNot Nothing, ddlSection.SelectedItem.Text, ""))
            '
            Dim ds As DataSet = ReportProvider.ExpenseReport(obj)
            ds.Tables(0).TableName = "Data"
            '
            rowIndex = 7
            For i As Integer = 0 To ds.Tables("Data").Rows.Count - 1
                Dim dr As DataRow = ds.Tables("Data").Rows(i)
                Dim colIndex As Integer = 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("No")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("BTNo")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("EmployeeCode")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("EmployeeName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("DepartureDate")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("ReturnDate")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Destination")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Purpose")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                rowIndex += 1
            Next
            '            
            Dim filePath As String = String.Concat("~/Export/Download/", _username)
            Dim dir As DirectoryInfo = New DirectoryInfo(Server.MapPath(filePath))
            If Not dir.Exists() Then
                dir.Create()
            End If
            filePath = String.Concat(filePath, "/", "expense_report_", _username, "_", DateTime.Now.ToString("yyyyMMddHHmmssfffff"), ".xls")
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
            btnMessage.Attributes("data-file-path") = filePath.Substring(1)
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

    Private Sub AdvanceClearReport()
        Dim xlApp As Excel.Application = Nothing
        Dim xlWorkBook As Excel.Workbook = Nothing
        Dim xlWorkSheet As Excel.Worksheet = Nothing
        Try
            Dim tmp As String = Server.MapPath(String.Concat("\Export\Template\Report\advance_clear_report.xlt"))
            '            
            Dim misValue As Object = System.Reflection.Missing.Value
            '
            xlApp = New Excel.ApplicationClass()
            xlWorkBook = xlApp.Workbooks.Open(tmp, 0, True, 5, "", "", True, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, 1, 0)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            'business
            Dim obj As ReportInfo = GetReportConditions()
            Dim rowIndex As Integer = 2
            xlWorkSheet.Cells(rowIndex, 3) = If(obj.DepartureFrom = DateTime.MinValue, "Begining", obj.DepartureFrom.ToString("dd-MMM-yyyy"))
            xlWorkSheet.Cells(rowIndex, 6) = If(obj.DepartureTo = DateTime.MinValue, "Today", obj.DepartureTo.ToString("dd-MMM-yyyy"))
            rowIndex += 1
            xlWorkSheet.Cells(rowIndex, 3) = If(radBranchAll.Checked, "All branch", If(radBranchNoChoose.Checked, "Except ", "") + If(ddlBranch.SelectedItem IsNot Nothing, ddlBranch.SelectedItem.Text, ""))
            xlWorkSheet.Cells(rowIndex, 6) = If(radDivAll.Checked, "All division", If(radDivNoChoose.Checked, "Except ", "") + If(ddlDivision.SelectedItem IsNot Nothing, ddlDivision.SelectedItem.Text, ""))
            rowIndex += 1
            xlWorkSheet.Cells(rowIndex, 3) = If(radDeptAll.Checked, "All department", If(radDeptNoChoose.Checked, "Except ", "") + If(ddlDepartment.SelectedItem IsNot Nothing, ddlDepartment.SelectedItem.Text, ""))
            'xlWorkSheet.Cells(rowIndex, 6) = If(radSecAll.Checked, "All section", If(radSecNoChoose.Checked, "Except ", "") + If(ddlSection.SelectedItem IsNot Nothing, ddlSection.SelectedItem.Text, ""))
            '
            Dim ds As DataSet = ReportProvider.AdvanceClearReport(GetReportConditions())
            ds.Tables(0).TableName = "Data"
            ds.Tables(1).TableName = "Summary"
            '
            xlWorkSheet.Cells(rowIndex, 6) = CommonFunction._ToString(ds.Tables("Summary").Rows(0)("TotalRecords"))
            '
            rowIndex = 7
            For i As Integer = 0 To ds.Tables("Data").Rows.Count - 1
                Dim dr As DataRow = ds.Tables("Data").Rows(i)
                Dim colIndex As Integer = 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("No")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("BTNo")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Category")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Type")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("EmployeeCode")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("EmployeeName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Email")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("BranchName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("DivisionName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("DepartmentName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("ManagerName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("ManagerEmail")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("DepartureDate")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("ReturnDate")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("INVOICE_DATE")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("INVOICE_NUM")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("INVOICE_AMOUNT")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("CURRENCY")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("INVOICE_DESCRIPTION")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("INVOICE_PIC")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("SettleDate")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Overdue")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Remark")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                rowIndex += 1
            Next
            '            
            Dim filePath As String = String.Concat("~/Export/Download/", _username)
            Dim dir As DirectoryInfo = New DirectoryInfo(Server.MapPath(filePath))
            If Not dir.Exists() Then
                dir.Create()
            End If
            filePath = String.Concat(filePath, "/", "advance_clear_report_", _username, "_", DateTime.Now.ToString("yyyyMMddHHmmssfffff"), ".xls")
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
            btnMessage.Attributes("data-file-path") = filePath.Substring(1)
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

    Private Sub NoAdvanceClearReport()
        Dim xlApp As Excel.Application = Nothing
        Dim xlWorkBook As Excel.Workbook = Nothing
        Dim xlWorkSheet As Excel.Worksheet = Nothing
        Try
            Dim tmp As String = Server.MapPath(String.Concat("\Export\Template\Report\noadvance_clear_report.xlt"))
            '            
            Dim misValue As Object = System.Reflection.Missing.Value
            '
            xlApp = New Excel.ApplicationClass()
            xlWorkBook = xlApp.Workbooks.Open(tmp, 0, True, 5, "", "", True, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, 1, 0)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            'business
            Dim obj As ReportInfo = GetReportConditions()
            Dim rowIndex As Integer = 2
            xlWorkSheet.Cells(rowIndex, 3) = If(obj.DepartureFrom = DateTime.MinValue, "Begining", obj.DepartureFrom.ToString("dd-MMM-yyyy"))
            xlWorkSheet.Cells(rowIndex, 6) = If(obj.DepartureTo = DateTime.MinValue, "Today", obj.DepartureTo.ToString("dd-MMM-yyyy"))
            rowIndex += 1
            xlWorkSheet.Cells(rowIndex, 3) = If(radBranchAll.Checked, "All branch", If(radBranchNoChoose.Checked, "Except ", "") + If(ddlBranch.SelectedItem IsNot Nothing, ddlBranch.SelectedItem.Text, ""))
            xlWorkSheet.Cells(rowIndex, 6) = If(radDivAll.Checked, "All division", If(radDivNoChoose.Checked, "Except ", "") + If(ddlDivision.SelectedItem IsNot Nothing, ddlDivision.SelectedItem.Text, ""))
            rowIndex += 1
            xlWorkSheet.Cells(rowIndex, 3) = If(radDeptAll.Checked, "All department", If(radDeptNoChoose.Checked, "Except ", "") + If(ddlDepartment.SelectedItem IsNot Nothing, ddlDepartment.SelectedItem.Text, ""))
            'xlWorkSheet.Cells(rowIndex, 6) = If(radSecAll.Checked, "All section", If(radSecNoChoose.Checked, "Except ", "") + If(ddlSection.SelectedItem IsNot Nothing, ddlSection.SelectedItem.Text, ""))
            '
            Dim ds As DataSet = ReportProvider.AdvanceClearReport(GetReportConditions(), True)
            ds.Tables(0).TableName = "Data"
            ds.Tables(1).TableName = "Summary"
            '
            xlWorkSheet.Cells(rowIndex, 6) = CommonFunction._ToString(ds.Tables("Summary").Rows(0)("TotalRecords"))
            '
            rowIndex = 7
            For i As Integer = 0 To ds.Tables("Data").Rows.Count - 1
                Dim dr As DataRow = ds.Tables("Data").Rows(i)
                Dim colIndex As Integer = 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("No")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("BTNo")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Category")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Type")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("EmployeeCode")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("EmployeeName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Email")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("BranchName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("DivisionName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("DepartmentName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("ManagerName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("ManagerEmail")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("DepartureDate")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("ReturnDate")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Purpose")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("FinPIC")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("SettleDate")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Overdue")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Remark")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                rowIndex += 1
            Next
            '            
            Dim filePath As String = String.Concat("~/Export/Download/", _username)
            Dim dir As DirectoryInfo = New DirectoryInfo(Server.MapPath(filePath))
            If Not dir.Exists() Then
                dir.Create()
            End If
            filePath = String.Concat(filePath, "/", "noadvance_clear_report_", _username, "_", DateTime.Now.ToString("yyyyMMddHHmmssfffff"), ".xls")
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
            btnMessage.Attributes("data-file-path") = filePath.Substring(1)
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

    Private Sub AirTicketCompareReport()
        Dim xlApp As Excel.Application = Nothing
        Dim xlWorkBook As Excel.Workbook = Nothing
        Dim xlWorkSheet As Excel.Worksheet = Nothing
        Try
            Dim tmp As String = Server.MapPath(String.Concat("\Export\Template\Report\airticket_report.xlt"))
            '            
            Dim misValue As Object = System.Reflection.Missing.Value
            '
            xlApp = New Excel.ApplicationClass()
            xlWorkBook = xlApp.Workbooks.Open(tmp, 0, True, 5, "", "", True, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, 1, 0)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            'business
            Dim obj As ReportInfo = GetReportConditions()
            Dim rowIndex As Integer = 2
            xlWorkSheet.Cells(rowIndex, 3) = If(obj.DepartureFrom = DateTime.MinValue, "Begining", obj.DepartureFrom.ToString("dd-MMM-yyyy"))
            xlWorkSheet.Cells(rowIndex, 6) = If(obj.DepartureTo = DateTime.MinValue, "Today", obj.DepartureTo.ToString("dd-MMM-yyyy"))
            xlWorkSheet.Cells(rowIndex, 8) = If(ddlAirPeriod.SelectedValue = "", "All Period", If(ddlAirPeriod.SelectedItem IsNot Nothing, ddlAirPeriod.SelectedItem.Text, ""))
            rowIndex += 1
            xlWorkSheet.Cells(rowIndex, 3) = If(radBranchAll.Checked, "All Branch", If(radBranchNoChoose.Checked, "Except ", "") + If(ddlBranch.SelectedItem IsNot Nothing, ddlBranch.SelectedItem.Text, ""))
            xlWorkSheet.Cells(rowIndex, 6) = If(radDivAll.Checked, "All Division", If(radDivNoChoose.Checked, "Except ", "") + If(ddlDivision.SelectedItem IsNot Nothing, ddlDivision.SelectedItem.Text, ""))
            xlWorkSheet.Cells(rowIndex, 8) = If(ddlOraSupplier.SelectedValue = "", "All Supplier", If(ddlOraSupplier.SelectedItem IsNot Nothing, ddlOraSupplier.SelectedItem.Text, ""))
            rowIndex += 1
            xlWorkSheet.Cells(rowIndex, 3) = If(radDeptAll.Checked, "All Department", If(radDeptNoChoose.Checked, "Except ", "") + If(ddlDepartment.SelectedItem IsNot Nothing, ddlDepartment.SelectedItem.Text, ""))
            xlWorkSheet.Cells(rowIndex, 6) = If(radSecAll.Checked, "All Section", If(radSecNoChoose.Checked, "Except ", "") + If(ddlSection.SelectedItem IsNot Nothing, ddlSection.SelectedItem.Text, ""))
            '
            Dim ds As DataSet = ReportProvider.AirTicketReport(GetReportConditions())
            ds.Tables(0).TableName = "Data"
            ds.Tables(1).TableName = "Summary"
            '
            xlWorkSheet.Cells(rowIndex, 8) = CommonFunction._ToMoney(ds.Tables("Summary").Rows(0)("TotalAmount"))
            '
            rowIndex = 7
            For i As Integer = 0 To ds.Tables("Data").Rows.Count - 1
                Dim dr As DataRow = ds.Tables("Data").Rows(i)
                Dim colIndex As Integer = 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("No")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("TicketNo")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("TicketDate")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("AirLine")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("AirPeriodName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("SupplierName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Passenger")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("EmployeeCode")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("EmployeeName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("DivisionName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Routing")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Fare")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("VAT")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("APTTax")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("SF")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("NetPayment")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Currency")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Exrate")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("SFConverted")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("SFVAT")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("TotalVAT")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("NetPaymentConverted")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("BudgetCode")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Purpose")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("AirTicketType")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                rowIndex += 1
            Next
            '            
            Dim filePath As String = String.Concat("~/Export/Download/", _username)
            Dim dir As DirectoryInfo = New DirectoryInfo(Server.MapPath(filePath))
            If Not dir.Exists() Then
                dir.Create()
            End If
            filePath = String.Concat(filePath, "/", "air_ticket_report_", _username, "_", DateTime.Now.ToString("yyyyMMddHHmmssfffff"), ".xls")
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
            btnMessage.Attributes("data-file-path") = filePath.Substring(1)
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

    Private Sub WifiDeviceReport()
        Dim xlApp As Excel.Application = Nothing
        Dim xlWorkBook As Excel.Workbook = Nothing
        Dim xlWorkSheet As Excel.Worksheet = Nothing
        Try
            Dim tmp As String = Server.MapPath(String.Concat("\Export\Template\Report\wifi_report.xlt"))
            '            
            Dim misValue As Object = System.Reflection.Missing.Value
            '
            xlApp = New Excel.ApplicationClass()
            xlWorkBook = xlApp.Workbooks.Open(tmp, 0, True, 5, "", "", True, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, 1, 0)
            xlWorkSheet = xlWorkBook.Worksheets(1)
            'business
            Dim obj As ReportInfo = GetReportConditions()
            Dim rowIndex As Integer = 2
            xlWorkSheet.Cells(rowIndex, 3) = If(obj.DepartureFrom = DateTime.MinValue, "Begining", obj.DepartureFrom.ToString("dd-MMM-yyyy"))
            xlWorkSheet.Cells(rowIndex, 6) = If(obj.DepartureTo = DateTime.MinValue, "Today", obj.DepartureTo.ToString("dd-MMM-yyyy"))            
            rowIndex += 1
            xlWorkSheet.Cells(rowIndex, 3) = If(radBranchAll.Checked, "All Branch", If(radBranchNoChoose.Checked, "Except ", "") + If(ddlBranch.SelectedItem IsNot Nothing, ddlBranch.SelectedItem.Text, ""))
            xlWorkSheet.Cells(rowIndex, 6) = If(radDivAll.Checked, "All Division", If(radDivNoChoose.Checked, "Except ", "") + If(ddlDivision.SelectedItem IsNot Nothing, ddlDivision.SelectedItem.Text, ""))            
            rowIndex += 1
            xlWorkSheet.Cells(rowIndex, 3) = If(radDeptAll.Checked, "All Department", If(radDeptNoChoose.Checked, "Except ", "") + If(ddlDepartment.SelectedItem IsNot Nothing, ddlDepartment.SelectedItem.Text, ""))
            xlWorkSheet.Cells(rowIndex, 6) = If(radSecAll.Checked, "All Section", If(radSecNoChoose.Checked, "Except ", "") + If(ddlSection.SelectedItem IsNot Nothing, ddlSection.SelectedItem.Text, ""))
            '
            Dim ds As DataSet = ReportProvider.WifiReport(GetReportConditions())
            ds.Tables(0).TableName = "Data"
            '
            rowIndex = 7
            For i As Integer = 0 To ds.Tables("Data").Rows.Count - 1
                Dim dr As DataRow = ds.Tables("Data").Rows(i)
                Dim colIndex As Integer = 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("No")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("BTNo")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("EmployeeCode")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("EmployeeName")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("DepartureDate")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("ReturnDate")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Position")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Destination")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("Country")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("UnitPrice")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("VAT")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("AdvanceStatus")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                colIndex += 1
                xlWorkSheet.Cells(rowIndex, colIndex) = dr("ExpenseStatus")
                CType(xlWorkSheet.Cells(rowIndex, colIndex), Excel.Range).Borders.ColorIndex = 1
                rowIndex += 1
            Next
            '            
            Dim filePath As String = String.Concat("~/Export/Download/", _username)
            Dim dir As DirectoryInfo = New DirectoryInfo(Server.MapPath(filePath))
            If Not dir.Exists() Then
                dir.Create()
            End If
            filePath = String.Concat(filePath, "/", "wifi_device_report_", _username, "_", DateTime.Now.ToString("yyyyMMddHHmmssfffff"), ".xls")
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
            btnMessage.Attributes("data-file-path") = filePath.Substring(1)
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

#End Region
End Class