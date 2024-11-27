Imports System.Web
Imports System.Web.Services
Imports Provider
Imports System.IO
Imports Microsoft.Office.Interop

Public Class AjaxHandle
    Implements System.Web.IHttpHandler, IRequiresSessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim action As String = context.Request.QueryString("action")
        Select Case action
            Case "attach"
                Attach()
            Case "delete-attachment"
                DeleteAttachment()
            Case "check-employee-code"
                CheckEmployeeCode()
            Case "get-exrate"
                GetExrate()
            Case "check-budget"
                CheckBudgetRemaining()
            Case "check-expense-budget"
                CheckExpenseBudgetRemaining()
            Case "check-advance-ora-status"
                CheckOraStatus(True)
            Case "check-expense-ora-status"
                CheckOraStatus(False)
            Case "get-ora-invoice-status"
                GetOraInvoiceStatus()
            Case "check-air-ticket-budget"
                CheckAirTicketBudget()
            Case "import-air-ticket"
                ImportAirTicket()
            Case "import-other-air-ticket"
                ImportOtherAirTicket()
            Case "check-airticket-confirmbudget"
                CheckAirTicketConfirmBudget()
            Case "hide-global-message"
                HideGlobalMessage()
        End Select
    End Sub

    Private Sub HideGlobalMessage()
        Dim messsage As String = ""
        Try
            If HttpContext.Current.Session("UserName") Is Nothing OrElse HttpContext.Current.Session("UserName").ToString().Trim().Length = 0 Then
                messsage = "session"
            Else
                HttpContext.Current.Session("HideExpireWarning") = "Y"
            End If
        Catch ex As Exception
            messsage = ex.Message
        End Try
        HttpContext.Current.Response.Write(messsage)
    End Sub

    Private Sub ImportOtherAirTicket()
        Dim messsage As String = ""
        Try
            If HttpContext.Current.Session("UserName") Is Nothing OrElse HttpContext.Current.Session("UserName").ToString().Trim().Length = 0 Then
                messsage = "session"
            Else
                Dim airPeriod As Integer = CommonFunction._ToInt(HttpContext.Current.Request.Params("airPeriod"))
                Dim supplier As String = HttpContext.Current.Request.Params("supplier")
                '
                If Not AirTicketProvider.CheckPeriodAndSupplier(airPeriod, supplier) Then
                    messsage = "closed"
                Else
                    Dim username As String = CommonFunction._ToString(HttpContext.Current.Session("UserName"))
                    Dim path As String = String.Format("/Import/{0}", username)
                    Dim Dir As New DirectoryInfo(HttpContext.Current.Server.MapPath(path))
                    If Not Dir.Exists() Then
                        Dir.Create()
                    Else
                        'delete old import files
                        For Each f As FileInfo In Dir.GetFiles()
                            Dim timeSpan As TimeSpan = DateTime.Now - f.CreationTime
                            If timeSpan.TotalDays >= 1 Then
                                Try
                                    f.Delete()
                                    f.Refresh()
                                Catch
                                End Try
                            End If
                        Next
                        Dir.Refresh()
                    End If
                    '
                    Dim attachmentFiles As HttpFileCollection = HttpContext.Current.Request.Files()
                    Dim errorCount As Integer = 0
                    'remove prev errors
                    AirTicketProvider.BTAirTicket_RemovePrevOtherError(username)
                    '
                    For i As Integer = 0 To attachmentFiles.Count - 1
                        Dim attFile As HttpPostedFile = attachmentFiles(i)
                        If attFile.FileName IsNot Nothing AndAlso attFile.FileName.Length > 0 Then
                            Dim fileName As String = attFile.FileName
                            If fileName.LastIndexOf("\") >= 0 Then
                                fileName = fileName.Substring(fileName.LastIndexOf("\") + 1)
                            End If
                            path = String.Format("{0}/{1}_{2}_{3}", path, username, DateTime.Now.ToString("yyMMddHHmmssfffff"), fileName)
                            Dim filePath As String = HttpContext.Current.Server.MapPath(path)
                            attFile.SaveAs(filePath)
                            Dim xlApp As Excel.Application = Nothing
                            Dim xlWorkBook As Excel.Workbook = Nothing
                            Dim xlWorkSheet As Excel.Worksheet = Nothing
                            Try
                                Dim misValue As Object = System.Reflection.Missing.Value
                                '
                                xlApp = New Excel.ApplicationClass()
                                xlWorkBook = xlApp.Workbooks.Open(filePath, 0, True, 5, "", "", True, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, 1, 0)
                                xlWorkSheet = xlWorkBook.Worksheets(1)
                                '
                                Dim dtData As New DataTable()
                                'get data table columns
                                Dim colIndex As Integer = 1
                                While True
                                    Try
                                        Dim colName As String = CommonFunction._ToString(xlWorkSheet.Cells(1, colIndex).Value)
                                        If colName.Trim().Length = 0 Then
                                            Exit While
                                        End If
                                        If colName.ToUpper().IndexOf("APT TAX") >= 0 Then
                                            colName = "apt_tax"
                                        ElseIf colName.ToUpper().IndexOf("VAT") >= 0 Then
                                            colName = "vat"
                                        Else
                                            colName = Regex.Replace(colName.ToLower(), "\s+", "_")
                                        End If
                                        dtData.Columns.Add(colName)
                                        colIndex += 1
                                    Catch ex As Exception
                                        messsage = "Import file template is incorrect!"
                                        Exit While
                                    End Try
                                End While
                                If messsage <> "" Then
                                    Exit For
                                End If
                                'get data table rows
                                Dim rowIndex As Integer = 2
                                While True
                                    Try
                                        Dim dr As DataRow = dtData.NewRow
                                        For index As Integer = 0 To dtData.Columns.Count - 1
                                            dr(index) = xlWorkSheet.Cells(rowIndex, index + 1).Value
                                        Next
                                        dtData.Rows.Add(dr)
                                        rowIndex += 1
                                        If CommonFunction._ToString(xlWorkSheet.Cells(rowIndex, 1).Value()).Trim().Length = 0 Then
                                            Exit While
                                        End If
                                    Catch ex As Exception
                                        Throw New Exception("Import file template is incorrect!")
                                    End Try
                                End While
                                'import data
                                For index As Integer = 0 To dtData.Rows.Count - 1
                                    Try
                                        Dim dr As DataRow = dtData.Rows(index)
                                        Dim remark As String = ""
                                        '
                                        Dim obj As New tblBTAirTicketInfo()
                                        obj.TicketNo = CommonFunction._ToString(dr("ticket_no")).Trim()
                                        If obj.TicketNo = "" Then
                                            remark = String.Concat("; ", "Ticket No is required!")
                                        End If
                                        obj.TicketDate = CommonFunction._ToDateTime(dr("date"))
                                        If obj.TicketDate = DateTime.MinValue Then
                                            remark = String.Concat("; ", "Ticket Date is required!")
                                        End If
                                        obj.AirLine = CommonFunction._ToString(dr("airl")).Trim()
                                        If obj.AirLine = "" Then
                                            remark = String.Concat("; ", "Airline is required!")
                                        End If
                                        obj.Passenger = CommonFunction._ToString(dr("passenger")).Trim()
                                        If obj.Passenger = "" Then
                                            remark = String.Concat("; ", "Passenger is required!")
                                        End If
                                        obj.Routing = CommonFunction._ToString(dr("routing")).Trim()
                                        If obj.Routing = "" Then
                                            remark = String.Concat("; ", "Routing is required!")
                                        End If
                                        obj.DepartureDate = CommonFunction._ToDateTime(dr("departure_date"))
                                        If obj.DepartureDate = DateTime.MinValue Then
                                            remark = String.Concat("; ", "DepartureDate is required!")
                                        End If
                                        obj.ReturnDate = CommonFunction._ToDateTime(dr("return_date"))
                                        obj.Fare = CommonFunction._ToMoney(dr("fare"))
                                        'If obj.Fare <= 0 Then
                                        '    remark = String.Concat("; ", "Fare is required!")
                                        'End If
                                        obj.VAT = CommonFunction._ToMoney(dr("vat"))
                                        'If obj.VAT < 0 Then
                                        '    remark = String.Concat("; ", "VAT is required!")
                                        'End If
                                        obj.APTTax = CommonFunction._ToMoney(dr("apt_tax"))
                                        'If obj.APTTax <= 0 Then
                                        '    remark = String.Concat("; ", "APTTax / HĐ HK is required!")
                                        'End If
                                        obj.SF = CommonFunction._ToMoney(dr("sf"))
                                        'If obj.SF <= 0 Then
                                        '    remark = String.Concat("; ", "SF is required!")
                                        'End If
                                        obj.NetPayment = CommonFunction._ToMoney(dr("net_payment"))
                                        If obj.NetPayment <= 0 Then
                                            remark = String.Concat("; ", "NetPayment is required!")
                                        End If
                                        obj.Currency = CommonFunction._ToString(dr("currency")).Trim()
                                        If obj.Currency = "" Then
                                            remark = String.Concat("; ", "Currency is required!")
                                        End If
                                        obj.Exrate = CommonFunction._ToDecimal(dr("exchange_rate"))
                                        If obj.Exrate <= 0 Then
                                            obj.Exrate = 1
                                        End If
                                        obj.BudgetCode = CommonFunction._ToString(dr("budget_code")).Replace(" ", "")
                                        If obj.BudgetCode = "" Then
                                            remark = String.Concat("; ", "Budget Code is required!")
                                        End If
                                        obj.Purpose = CommonFunction._ToString(dr("purpose")).Trim()
                                        If obj.Purpose = "" Then
                                            remark = String.Concat("; ", "Purpose is required!")
                                        End If
                                        obj.Requester = CommonFunction._ToString(dr("requester_code")).Trim()
                                        If obj.Requester = "" Then
                                            remark = String.Concat("; ", "Requester Code is required!")
                                        End If
                                        'obj.RequesterName = CommonFunction._ToString(dr("requester_name"))
                                        'If obj.RequesterName.Trim() = "" Then
                                        '    remark = String.Concat("; ", "Requester Name is required!")
                                        'End If
                                        'obj.RequesterDiv = CommonFunction._ToString(dr("requester_division"))
                                        'If obj.RequesterDiv.Trim() = "" Then
                                        '    remark = String.Concat("; ", "Requester Division is required!")
                                        'End If
                                        'obj.RequesterDept = CommonFunction._ToString(dr("requester_dept."))
                                        'If obj.RequesterDept.Trim() = "" Then
                                        '    remark = String.Concat("; ", "Requester Dept. is required!")
                                        'End If                                        
                                        'obj.RequesterPhone = CommonFunction._ToString(dr("requester_phone"))
                                        'If obj.RequesterPhone.Trim() = "" Then
                                        '    remark = String.Concat("; ", "Requester Phone is required!")
                                        'End If
                                        'obj.ICTRequest = (CommonFunction._ToString(dr("ict_request")).Trim() = "1")
                                        obj.AirPeriod = airPeriod
                                        obj.OraSupplier = supplier
                                        obj.CreatedBy = username
                                        '
                                        If remark.Trim().Length > 0 Then
                                            remark = remark.Substring(1).Trim()
                                        End If
                                        '
                                        errorCount = AirTicketProvider.BTAirTicket_Other_Import(obj, remark)
                                    Catch ex As Exception
                                        Throw New Exception("Import file template is incorrect!")
                                    End Try
                                Next
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
                                messsage = ex.Message
                            End Try
                        End If
                    Next
                    '
                    If messsage = "" Then
                        messsage = If(errorCount > 0, "fail", "success")
                    End If
                End If
            End If
        Catch ex As Exception
            messsage = ex.Message
        End Try
        HttpContext.Current.Response.Write(messsage)
    End Sub

    Private Sub CheckAirTicketConfirmBudget()
        Dim messsage As String
        Try
            If HttpContext.Current.Session("UserName") Is Nothing OrElse HttpContext.Current.Session("UserName").ToString().Trim().Length = 0 Then
                HttpContext.Current.Response.Write("session")
                Return
            End If
            '
            Dim period As Integer = CommonFunction._ToInt(HttpContext.Current.Request.QueryString("airPeriod"))
            Dim supplier As String = HttpContext.Current.Request.QueryString("supplier")
            Dim count As Integer = AirTicketProvider.CheckBudget(period, supplier)
            messsage = If(count > 0, count.ToString(), "all")
        Catch ex As Exception
            messsage = ex.Message
        End Try
        HttpContext.Current.Response.Write(messsage)
    End Sub

    Private Sub ImportAirTicket()
        Dim messsage As String = ""
        Try
            If HttpContext.Current.Session("UserName") Is Nothing OrElse HttpContext.Current.Session("UserName").ToString().Trim().Length = 0 Then
                messsage = "session"
            Else
                Dim airPeriod As Integer = CommonFunction._ToInt(HttpContext.Current.Request.Params("airPeriod"))
                Dim supplier As String = HttpContext.Current.Request.Params("supplier")
                '
                If Not AirTicketProvider.CheckPeriodAndSupplier(airPeriod, supplier) Then
                    messsage = "closed"
                Else
                    Dim username As String = CommonFunction._ToString(HttpContext.Current.Session("UserName"))
                    Dim path As String = String.Format("/Import/{0}", username)
                    Dim Dir As New DirectoryInfo(HttpContext.Current.Server.MapPath(path))
                    If Not Dir.Exists() Then
                        Dir.Create()
                    Else
                        'delete old import files
                        For Each f As FileInfo In Dir.GetFiles()
                            Dim timeSpan As TimeSpan = DateTime.Now - f.CreationTime
                            If timeSpan.TotalDays >= 1 Then
                                Try
                                    f.Delete()
                                    f.Refresh()
                                Catch
                                End Try
                            End If
                        Next
                        Dir.Refresh()
                    End If
                    '
                    Dim attachmentFiles As HttpFileCollection = HttpContext.Current.Request.Files()
                    Dim errorCount As Integer = 0
                    'remove prev errors
                    AirTicketProvider.BTAirTicket_RemovePrevError(username)
                    '
                    For i As Integer = 0 To attachmentFiles.Count - 1
                        Dim attFile As HttpPostedFile = attachmentFiles(i)
                        If attFile.FileName IsNot Nothing AndAlso attFile.FileName.Length > 0 Then
                            Dim fileName As String = attFile.FileName
                            If fileName.LastIndexOf("\") >= 0 Then
                                fileName = fileName.Substring(fileName.LastIndexOf("\") + 1)
                            End If
                            path = String.Format("{0}/{1}_{2}_{3}", path, username, DateTime.Now.ToString("yyMMddHHmmssfffff"), fileName)
                            Dim filePath As String = HttpContext.Current.Server.MapPath(path)
                            attFile.SaveAs(filePath)
                            Dim xlApp As Excel.Application = Nothing
                            Dim xlWorkBook As Excel.Workbook = Nothing
                            Dim xlWorkSheet As Excel.Worksheet = Nothing
                            Try
                                Dim misValue As Object = System.Reflection.Missing.Value
                                '
                                xlApp = New Excel.ApplicationClass()
                                xlWorkBook = xlApp.Workbooks.Open(filePath, 0, True, 5, "", "", True, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", False, False, 0, True, 1, 0)
                                xlWorkSheet = xlWorkBook.Worksheets(1)
                                '
                                Dim dtData As New DataTable()
                                'get data table columns
                                Dim colIndex As Integer = 1
                                While True
                                    Try
                                        Dim colName As String = CommonFunction._ToString(xlWorkSheet.Cells(1, colIndex).Value)
                                        If colName.Trim().Length = 0 Then
                                            Exit While
                                        End If
                                        If colName.ToUpper().IndexOf("APT TAX") >= 0 Then
                                            colName = "apt_tax"
                                        ElseIf colName.ToUpper().IndexOf("VAT") >= 0 Then
                                            colName = "vat"
                                        Else
                                            colName = Regex.Replace(colName.ToLower(), "\s+", "_")
                                        End If
                                        dtData.Columns.Add(colName)
                                        colIndex += 1
                                    Catch ex As Exception
                                        messsage = "Import file template is incorrect!"
                                        Exit While
                                    End Try
                                End While
                                If messsage <> "" Then
                                    Exit For
                                End If
                                'get data table rows
                                Dim rowIndex As Integer = 2
                                While True
                                    Try
                                        Dim dr As DataRow = dtData.NewRow
                                        For index As Integer = 0 To dtData.Columns.Count - 1
                                            dr(index) = xlWorkSheet.Cells(rowIndex, index + 1).Value
                                        Next
                                        dtData.Rows.Add(dr)
                                        rowIndex += 1
                                        If CommonFunction._ToString(xlWorkSheet.Cells(rowIndex, 1).Value()).Trim().Length = 0 Then
                                            Exit While
                                        End If
                                    Catch ex As Exception
                                        Throw New Exception("Import file template is incorrect!")
                                    End Try
                                End While
                                'import data
                                For index As Integer = 0 To dtData.Rows.Count - 1
                                    Try
                                        Dim dr As DataRow = dtData.Rows(index)
                                        Dim remark As String = ""
                                        '
                                        Dim obj As New tblBTAirTicketInfo()
                                        obj.TicketNo = CommonFunction._ToString(dr("ticket_no"))
                                        If obj.TicketNo.Trim() = "" Then
                                            remark = String.Concat("; ", "Ticket No is required!")
                                        End If
                                        obj.TicketDate = CommonFunction._ToDateTime(dr("date"))
                                        If obj.TicketDate = DateTime.MinValue Then
                                            remark = String.Concat("; ", "Ticket Date is required!")
                                        End If
                                        obj.AirLine = CommonFunction._ToString(dr("airl"))
                                        If obj.AirLine.Trim() = "" Then
                                            remark = String.Concat("; ", "Airline is required!")
                                        End If
                                        obj.Passenger = CommonFunction._ToString(dr("passenger"))
                                        If obj.Passenger.Trim() = "" Then
                                            remark = String.Concat("; ", "Passenger is required!")
                                        End If
                                        obj.Routing = CommonFunction._ToString(dr("routing"))
                                        If obj.Routing.Trim() = "" Then
                                            remark = String.Concat("; ", "Routing is required!")
                                        End If
                                        obj.DepartureDate = CommonFunction._ToDateTime(dr("departure_date"))
                                        If obj.DepartureDate = DateTime.MinValue Then
                                            remark = String.Concat("; ", "DepartureDate is required!")
                                        End If
                                        obj.ReturnDate = CommonFunction._ToDateTime(dr("return_date"))
                                        obj.Fare = CommonFunction._ToMoney(dr("fare"))
                                        'If obj.Fare <= 0 Then
                                        '    remark = String.Concat("; ", "Fare is required!")
                                        'End If
                                        obj.VAT = CommonFunction._ToMoney(dr("vat"))
                                        'If obj.VAT <= 0 Then
                                        '    remark = String.Concat("; ", "VAT is required!")
                                        'End If
                                        obj.APTTax = CommonFunction._ToMoney(dr("apt_tax"))
                                        'If obj.APTTax <= 0 Then
                                        '    remark = String.Concat("; ", "APTTax / HĐ HK is required!")
                                        'End If
                                        obj.SF = CommonFunction._ToMoney(dr("sf"))
                                        'If obj.SF <= 0 Then
                                        '    remark = String.Concat("; ", "SF is required!")
                                        'End If
                                        obj.NetPayment = CommonFunction._ToMoney(dr("net_payment"))
                                        If obj.NetPayment <= 0 Then
                                            remark = String.Concat("; ", "NetPayment is required!")
                                        End If
                                        obj.Currency = CommonFunction._ToString(dr("currency"))
                                        If obj.Currency.Trim() = "" Then
                                            remark = String.Concat("; ", "Currency is required!")
                                        End If
                                        obj.Exrate = CommonFunction._ToDecimal(dr("exchange_rate"))
                                        If obj.Exrate <= 0 Then
                                            obj.Exrate = 1
                                        End If
                                        obj.AirPeriod = airPeriod
                                        obj.OraSupplier = supplier
                                        obj.CreatedBy = username
                                        '
                                        If remark.Trim().Length > 0 Then
                                            remark = remark.Substring(1).Trim()
                                        End If
                                        '
                                        errorCount = AirTicketProvider.BTAirTicket_Import(obj, remark)
                                    Catch ex As Exception
                                        Throw New Exception("Import file template is incorrect!")
                                    End Try
                                Next
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
                                messsage = ex.Message
                            End Try
                        End If
                    Next
                    '
                    If messsage = "" Then
                        messsage = If(errorCount > 0, "fail", "success")
                    End If
                End If
            End If
        Catch ex As Exception
            messsage = ex.Message
        End Try
        HttpContext.Current.Response.Write(messsage)
    End Sub

    Private Sub CheckEmployeeCode()
        Dim messsage As String
        Try
            If HttpContext.Current.Session("UserName") Is Nothing OrElse HttpContext.Current.Session("UserName").ToString().Trim().Length = 0 Then
                messsage = "session"
            Else
                Dim empCode As String = CommonFunction._ToString(HttpContext.Current.Request.QueryString("code"))
                If Not UserProvider.tbl_User_IsAuthorizedAccount(empCode) Then
                    messsage = "invalid"
                ElseIf UserProvider.tbl_User_GetUserInfo_ByUserName(empCode) IsNot Nothing Then
                    messsage = "exist"
                Else
                    messsage = "null"
                End If
            End If
        Catch ex As Exception
            messsage = ex.Message
        End Try
        HttpContext.Current.Response.Write(messsage)
    End Sub

    Private Sub DeleteAttachment()
        Dim messsage As String
        Try
            If HttpContext.Current.Session("UserName") Is Nothing OrElse HttpContext.Current.Session("UserName").ToString().Trim().Length = 0 Then
                messsage = "session"
            Else
                Dim id As Integer = CommonFunction._ToInt(HttpContext.Current.Request("id"))
                BusinessTripProvider.BTRAttachment_Delete(id)
                messsage = "success"
            End If
        Catch ex As Exception
            messsage = ex.Message
        End Try
        HttpContext.Current.Response.Write(messsage)
    End Sub

    Private Sub Attach()
        Dim messsage As String
        Dim files As String = String.Empty
        Try
            If HttpContext.Current.Session("UserName") Is Nothing OrElse HttpContext.Current.Session("UserName").ToString().Trim().Length = 0 Then
                HttpContext.Current.Response.Write("session")
                Return
            End If
            '
            Dim username As String = CommonFunction._ToString(HttpContext.Current.Session("UserName"))
            Dim btID As Integer = CommonFunction._ToInt(HttpContext.Current.Request.Params("btID"))
            Dim type As String = HttpContext.Current.Request.Params("type")
            Dim attachmentFiles As HttpFileCollection = HttpContext.Current.Request.Files()
            For i As Integer = 0 To attachmentFiles.Count - 1
                Dim attFile As HttpPostedFile = attachmentFiles(i)
                If attFile.FileName IsNot Nothing AndAlso attFile.FileName.Length > 0 Then
                    Dim path As String = String.Format("/Attachments/{0}", username)
                    If Not Directory.Exists(HttpContext.Current.Server.MapPath(path)) Then
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path))
                    End If
                    Dim fileName As String = attFile.FileName
                    If fileName.LastIndexOf("\") >= 0 Then
                        fileName = fileName.Substring(fileName.LastIndexOf("\") + 1)
                    End If
                    path = String.Format("{0}/{1}_{2}_{3}", path, username, DateTime.Now.ToString("yyMMddHHmmssfffff"), fileName)
                    attFile.SaveAs(HttpContext.Current.Server.MapPath(path))
                    Dim obj As New tblBTRegisterAttachmentInfo()
                    obj.BTRegisterID = btID
                    obj.CreatedBy = username
                    obj.AttachmentPath = path
                    obj.AttachmentType = type
                    If type <> "other" AndAlso type <> "other-expense" Then
                        BusinessTripProvider.BTRAttachment_DeleteByType(btID, type)
                    End If
                    BusinessTripProvider.BTRAttachment_Insert(obj)
                End If
            Next
            messsage = "success"
            Dim dtData As DataTable = BusinessTripProvider.BTRAttachment_GetByType(btID, type)
            Dim builder As New StringBuilder()
            For Each item As DataRow In dtData.Rows
                Dim path As String = CommonFunction._ToString(item("AttachmentPath"))
                Dim fileName As String = path.Substring(path.LastIndexOf("/") + 1).Substring(25)
                Dim li As String = String.Format("<li><a href='{0}'>{1}</a><input type='button' class='grid-btn delete-btn' title='Remove' data-id='{2}' onclick='DeleteAttachment(this)'></li>", path, fileName, item("ID"))
                builder.Append(li)
            Next
            If builder.ToString().Trim().Length > 0 Then
                files = String.Format("<ol class='attachments'>{0}</ol>", builder.ToString())
            End If
        Catch ex As Exception
            messsage = ex.Message
        End Try
        Dim response As String = String.Concat("{message:""", messsage, """,files:""", files, """}")
        HttpContext.Current.Response.Write(response)
    End Sub

    Private Sub GetExrate()
        Dim messsage As String
        Try
            If HttpContext.Current.Session("UserName") Is Nothing OrElse HttpContext.Current.Session("UserName").ToString().Trim().Length = 0 Then
                HttpContext.Current.Response.Write("session")
                Return
            End If
            '
            Dim fromCurrency As String = HttpContext.Current.Request.QueryString("fromCurrency").Trim()
            Dim toCurrency As String = HttpContext.Current.Request.QueryString("toCurrency").Trim()
            Dim exchangeDate As DateTime
            Try
                exchangeDate = DateTime.ParseExact(HttpContext.Current.Request.QueryString("exchangeDate").Trim(), "dd-MMM-yyyy", Nothing)
            Catch ex As Exception
                exchangeDate = DateTime.MinValue
            End Try
            messsage = CommonFunction._ToString(ExpenseProvider.GetExrate(fromCurrency, toCurrency, exchangeDate))
        Catch ex As Exception
            messsage = ex.Message
        End Try
        HttpContext.Current.Response.Write(messsage)
    End Sub

    Private Sub CheckBudgetRemaining()
        Dim messsage As String = ""
        Try
            If HttpContext.Current.Session("UserName") Is Nothing OrElse HttpContext.Current.Session("UserName").ToString().Trim().Length = 0 Then
                HttpContext.Current.Response.Write("session")
                Return
            End If
            '
            Dim btID As Integer = CommonFunction._ToInt(HttpContext.Current.Request.QueryString("btID"))
            Dim budgetCode As Integer = CommonFunction._ToInt(HttpContext.Current.Request.QueryString("budgetCode"))
            'If budgetCode.Length = 0 Then
            '    budgetCode = Nothing
            'End If
            'messsage = CommonFunction._ToString(BusinessTripProvider.BTRegister_CheckBudgetRemaining(budgetCode, btID))
            '
            Dim btType As String = HttpContext.Current.Request.QueryString("bttype")
            Dim checkOraUser As String = HttpContext.Current.Request.QueryString("checkOraUser")
            If btType IsNot Nothing AndAlso btType.Trim().Length > 0 Then
                messsage = String.Concat(messsage, "|_|")
                If btType = "overnight" Then
                    Dim departureDate As DateTime = DateTime.ParseExact(HttpContext.Current.Request.QueryString("departureDate"), "dd-MMM-yyyy HH:mm", Nothing)
                    Dim returnDate As DateTime = DateTime.ParseExact(HttpContext.Current.Request.QueryString("returnDate"), "dd-MMM-yyyy HH:mm", Nothing)
                    Dim ds As DataSet = BusinessTripProvider.BTRegister_CheckOvernightBTDate(departureDate, returnDate, btID)

                    Dim noRequestAdvance As Boolean = HttpContext.Current.Request.QueryString("noRequestAdvance") = "T"

                    If returnDate <= departureDate Then
                        messsage = String.Concat(messsage, "invalid")
                    ElseIf CommonFunction._ToInt(ds.Tables(0).Rows(0)(0)) > 0 AndAlso Not noRequestAdvance Then
                        messsage = String.Concat(messsage, "out")
                    ElseIf CommonFunction._ToInt(ds.Tables(1).Rows(0)(0)) > 0 Then
                        messsage = String.Concat(messsage, "schedule-out")
                    ElseIf CommonFunction._ToInt(ds.Tables(2).Rows(0)(0)) > 0 Then
                        messsage = String.Concat(messsage, "conflict")
                    End If
                ElseIf btType = "oneday" Then
                    Dim ds As DataSet = BusinessTripProvider.BTRegister_CheckOnedayBTDate(btID)
                    If CommonFunction._ToInt(ds.Tables(0).Rows(0)(0)) > 0 Then
                        messsage = String.Concat(messsage, "conflict")
                    End If
                End If
            ElseIf checkOraUser = "y" Then
                messsage = String.Concat(messsage, "|_|")
                messsage = String.Concat(messsage, UserProvider.CheckOraMapping(CommonFunction._ToString(HttpContext.Current.Session("UserName"))))
            End If
        Catch ex As Exception
            messsage = ex.Message
        End Try
        HttpContext.Current.Response.Write(messsage)
    End Sub

    Private Sub CheckExpenseBudgetRemaining()
        Dim messsage As String = ""
        Try
            If HttpContext.Current.Session("UserName") Is Nothing OrElse HttpContext.Current.Session("UserName").ToString().Trim().Length = 0 Then
                HttpContext.Current.Response.Write("session")
                Return
            End If
            '
            Dim btID As Integer = CommonFunction._ToInt(HttpContext.Current.Request.QueryString("btID"))
            Dim budgetCode As Integer = CommonFunction._ToInt(HttpContext.Current.Request.QueryString("budgetCode").Trim())
            'If budgetCode.Length = 0 Then
            '    budgetCode = Nothing
            'End If
            'messsage = CommonFunction._ToString(BusinessTripProvider.BTRegister_CheckExpenseBudgetRemaining(budgetCode, btID))
            Dim btType As String = HttpContext.Current.Request.QueryString("bttype")
            Dim checkOraUser As String = HttpContext.Current.Request.QueryString("checkOraUser")
            If btType IsNot Nothing AndAlso btType.Trim().Length > 0 Then
                messsage = String.Concat(messsage, "|_|")
                If btType = "expense" Then
                    Dim departureDate As DateTime = DateTime.ParseExact(HttpContext.Current.Request.QueryString("departureDate"), "dd-MMM-yyyy HH:mm", Nothing)
                    Dim returnDate As DateTime = DateTime.ParseExact(HttpContext.Current.Request.QueryString("returnDate"), "dd-MMM-yyyy HH:mm", Nothing)
                    Dim ds As DataSet = ExpenseProvider.BTExpense_CheckOvernightBTDate(departureDate, returnDate, btID)

                    If returnDate <= departureDate Then
                        messsage = String.Concat(messsage, "invalid")
                    ElseIf CommonFunction._ToInt(ds.Tables(0).Rows(0)(0)) > 0 Then
                        messsage = String.Concat(messsage, "out")
                    ElseIf CommonFunction._ToInt(ds.Tables(1).Rows(0)(0)) > 0 Then
                        messsage = String.Concat(messsage, "other-out")
                    ElseIf CommonFunction._ToInt(ds.Tables(2).Rows(0)(0)) > 0 Then
                        messsage = String.Concat(messsage, "conflict")
                    End If
                ElseIf btType = "oneday" Then
                    Dim ds As DataSet = ExpenseProvider.BTExpense_CheckOnedayBTDate(btID)
                    If CommonFunction._ToInt(ds.Tables(0).Rows(0)(0)) > 0 Then
                        messsage = String.Concat(messsage, "conflict")
                    End If
                End If
            ElseIf checkOraUser = "y" Then
                messsage = String.Concat(messsage, "|_|")
                messsage = String.Concat(messsage, UserProvider.CheckOraMapping(CommonFunction._ToString(HttpContext.Current.Session("UserName"))))
            End If
        Catch ex As Exception
            messsage = ex.Message
        End Try
        HttpContext.Current.Response.Write(messsage)
    End Sub

    Private Sub CheckOraStatus(ByVal isAdvance As Boolean)
        Dim messsage As String
        Try
            If HttpContext.Current.Session("UserName") Is Nothing OrElse HttpContext.Current.Session("UserName").ToString().Trim().Length = 0 Then
                HttpContext.Current.Response.Write("session")
                Return
            End If
            '
            Dim btID As Integer = CommonFunction._ToInt(HttpContext.Current.Request.QueryString("btID"))
            If isAdvance Then
                If BusinessTripProvider.tbl_BT_Register_CheckAirTicket(btID) Then
                    messsage = "Can not reject this BT! There are some air tickets of this BT have already paid!"
                ElseIf BusinessTripProvider.tbl_BT_Register_CheckOracleStatus(btID) Then
                    messsage = "reject-allowed"
                Else
                    messsage = "Can not reject this BT! Please check oracle invoice status!"
                End If
            Else
                messsage = If(ExpenseProvider.tbl_BT_Expense_CheckOracleStatus(btID), "reject-allowed", "Can not reject this BT! Please check oracle invoice status!")
            End If
        Catch ex As Exception
            messsage = ex.Message
        End Try
        HttpContext.Current.Response.Write(messsage)
    End Sub

    Private Sub GetOraInvoiceStatus()
        Dim messsage As String
        Try
            If HttpContext.Current.Session("UserName") Is Nothing OrElse HttpContext.Current.Session("UserName").ToString().Trim().Length = 0 Then
                HttpContext.Current.Response.Write("session")
                Return
            End If
            '
            Dim btIDs As String = HttpContext.Current.Request.QueryString("btIDs")
            Dim type As String = HttpContext.Current.Request.QueryString("type")
            Dim dtData As DataTable = If(type = "a", BusinessTripProvider.GetOraInvoiceStatus(btIDs), ExpenseProvider.GetOraInvoiceStatus(btIDs))
            Dim ids As New StringBuilder("")
            Dim status As New StringBuilder("")
            For Each item As DataRow In dtData.Rows
                ids.Append(String.Format(",""{0}""", item("ImportID")))
                status.Append(String.Format(",""{0}""", item("OraStatus")))
            Next
            messsage = String.Concat("{""ids"":[", ids.ToString().Trim(","), "], ""status"":[", status.ToString().Trim(","), "]}")
        Catch ex As Exception
            messsage = ex.Message
        End Try
        HttpContext.Current.Response.Write(messsage)
    End Sub

    Private Sub CheckAirTicketBudget()
        Dim messsage As String
        Try
            If HttpContext.Current.Session("UserName") Is Nothing OrElse HttpContext.Current.Session("UserName").ToString().Trim().Length = 0 Then
                HttpContext.Current.Response.Write("session")
                Return
            End If
            '
            Dim period As Integer = CommonFunction._ToInt(HttpContext.Current.Request.QueryString("period"))
            Dim supplier As String = HttpContext.Current.Request.QueryString("supplier")
            messsage = AirTicketProvider.CheckBudgetString(period, supplier)
            messsage = String.Concat(messsage, "|_|")
            messsage = String.Concat(messsage, UserProvider.CheckOraMapping(CommonFunction._ToString(HttpContext.Current.Session("UserName"))))
        Catch ex As Exception
            messsage = ex.Message
        End Try
        HttpContext.Current.Response.Write(messsage)
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class