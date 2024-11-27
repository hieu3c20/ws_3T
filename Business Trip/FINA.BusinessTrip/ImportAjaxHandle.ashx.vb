Imports System.Web
Imports System.Web.Services
Imports Provider
Imports System.IO

Public Class ImportAjaxHandle
    Implements System.Web.IHttpHandler, IRequiresSessionState

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

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim action As String = context.Request.QueryString("action")
        Select Case action
            Case "import-budget"
                ImportExcelBudget()
        End Select
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Public Sub ImportExcelBudget()
        Dim messsageBud As String = String.Empty
        Dim files As String = String.Empty
        Try
            If HttpContext.Current.Session("UserName") Is Nothing OrElse HttpContext.Current.Session("UserName").ToString().Trim().Length = 0 Then
                HttpContext.Current.Response.Write("session")
                Return
            End If
            '
            Dim username As String = CommonFunction._ToString(HttpContext.Current.Session("UserName"))
            Dim strPath As String = CommonFunction.ImportTemplate()
            If strPath IsNot Nothing Then
                If CheckImport(strPath) IsNot Nothing Then
                    Exit Sub
                Else
                    'Import vao bang tam


                End If

            Else
                messsageBud = "File not found!"
            End If

        Catch ex As Exception
            messsageBud = ex.Message
        End Try
        HttpContext.Current.Response.Write(messsageBud)
    End Sub



    Private Function CheckImport(ByVal PathFile As String) As String
        dtExcel = GetDataFromSheet("Budget", PathFile)
        If dtExcel Is Nothing Then
            Return "File not found!"
        End If
        If Not isAllColumnName(dtExcel) Then
            'File excel dinh dang khong dung
            Return "File format is invalid!"
        End If
        Return Nothing
    End Function


    Private Function GetDataFromSheet(ByVal SheetName As String, ByVal PathFile As String) As DataTable
        Try
            Dim dt As New DataTable
            Dim strConn As String
            Dim da As OleDb.OleDbDataAdapter
            Dim ds As New DataSet

            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" & _
            "Data Source=" & pathFile & ";Extended Properties=""EXCEL 8.0;IMEX=1"""
            da = New OleDb.OleDbDataAdapter("SELECT * FROM [" & SheetName & "$]", strConn)
            da.Fill(ds)
            dt = ds.Tables(0)
            da.Dispose()
            Return dt
        Catch ex As Exception
            Throw New Exception("File is invalid")
        End Try
    End Function

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
            Dim i As Integer
            Dim j As Integer
            For i = 0 To dtExcel.Rows.Count - 1
                If IsDBNull(dtExcel.Rows(i)("EmployeeCode")) Or (dtExcel.Rows(i)("EmployeeCode")) Is Nothing Or _
                 IsDBNull(dtExcel.Rows(i)("OverTimeDate")) Or (dtExcel.Rows(i)("OverTimeDate")) Is Nothing Or _
                 IsDBNull(dtExcel.Rows(i)("OverTimeMinutes")) Or (dtExcel.Rows(i)("OverTimeMinutes")) Is Nothing Or _
                 IsDBNull(dtExcel.Rows(i)("OverTimeType")) Or (dtExcel.Rows(i)("OverTimeType")) Is Nothing Then
                    Dim dr As Data.DataRow
                    dr = dtError.NewRow
                    dr("EmployeeCode") = dtExcel.Rows(i)("EmployeeCode")
                    dr("OverTimeDate") = dtExcel.Rows(i)("OverTimeDate")
                    dr("OverTimeMinutes") = dtExcel.Rows(i)("OverTimeMinutes")
                    dr("OverTimeType") = dtExcel.Rows(i)("OverTimeType")
                    dr("Note") = dtExcel.Rows(i)("Note")
                    dr("Reason") = "EmployeeCode/OverTimeDate/OverTimeMinutes/OverTimeType không được để trống"
                    dtError.Rows.Add(dr)
                    Continue For
                End If
                If Not IsDate(dtExcel.Rows(i)("OverTimeDate")) Then
                    Dim dr As Data.DataRow
                    dr = dtError.NewRow
                    dr("EmployeeCode") = dtExcel.Rows(i)("EmployeeCode")
                    dr("OverTimeDate") = dtExcel.Rows(i)("OverTimeDate")
                    dr("OverTimeMinutes") = dtExcel.Rows(i)("OverTimeMinutes")
                    dr("OverTimeType") = dtExcel.Rows(i)("OverTimeType")
                    dr("Note") = dtExcel.Rows(i)("Note")
                    dr("Reason") = "Sai định dạng ngày OverTimeDate"
                    dtError.Rows.Add(dr)
                    Continue For
                End If




                If i = 0 Then
                    dctEmployeeCode.Add(i, dtExcel.Rows(i)("EmployeeCode").ToString.Trim)
                    dctOTDate.Add(i, dtExcel.Rows(i)("OverTimeDate").ToString.Trim)
                    dctOT.Add(i, dtExcel.Rows(i)("OverTimeMinutes").ToString.Trim)
                    dctOvertimetypeCode.Add(i, dtExcel.Rows(i)("OverTimeType").ToString.Trim)
                    dctNote.Add(i, dtExcel.Rows(i)("Note").ToString.Trim)
                Else
                    Dim blnExist As Boolean
                    blnExist = False
                    For j = 0 To dctEmployeeCode.Count - 1
                        If dtExcel.Rows(i)("EmployeeCode").ToString.Trim = dctEmployeeCode(j) _
                        And dtExcel.Rows(i)("OverTimeDate").ToString.Trim = dctOTDate(j) _
                        And dtExcel.Rows(i)("OverTimeMinutes").ToString.Trim = dctOT(j) _
                        And dtExcel.Rows(i)("OverTimeType").ToString.Trim = dctOvertimetypeCode(j) _
Then

                            Dim dr As Data.DataRow
                            dr = dtError.NewRow
                            dr("EmployeeCode") = dtExcel.Rows(i)("EmployeeCode")
                            dr("OverTimeDate") = dtExcel.Rows(i)("OverTimeDate")
                            dr("OverTimeMinutes") = dtExcel.Rows(i)("OverTimeMinutes")
                            dr("OverTimeType") = dtExcel.Rows(i)("OverTimeType")
                            dr("Note") = dtExcel.Rows(i)("Note")
                            dr("Reason") = "Trùng bản ghi giống nhau"
                            dtError.Rows.Add(dr)

                            blnExist = True
                            Exit For
                        End If
                    Next
                    If Not blnExist Then
                        dctEmployeeCode.Add(dctEmployeeCode.Count, dtExcel.Rows(i)("EmployeeCode").ToString.Trim)
                        dctOTDate.Add(dctOTDate.Count, dtExcel.Rows(i)("OverTimeDate").ToString.Trim)
                        dctOT.Add(dctOT.Count, dtExcel.Rows(i)("OverTimeMinutes").ToString.Trim)
                        dctOvertimetypeCode.Add(dctOvertimetypeCode.Count, dtExcel.Rows(i)("OverTimeType").ToString.Trim)
                        dctNote.Add(dctNote.Count, dtExcel.Rows(i)("Note").ToString.Trim)

                    End If
                End If
            Next
        Catch ex As Exception
            ' MessageBox.Show("Có lỗi: " + ex.Message.ToString, "Import dữ liệu", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Function isAllColumnName(ByVal dt As DataTable) As Boolean
        If Not isFileStandart(dt, "BudgetName") Then Return False
        If Not isFileStandart(dt, "BudgetCode") Then Return False
        If Not isFileStandart(dt, "Amount") Then Return False
        If Not isFileStandart(dt, "Org") Then Return False
        If Not isFileStandart(dt, "Department") Then Return False
        If Not isFileStandart(dt, "Description") Then Return False
        Return True
    End Function


    Private Function isFileStandart(ByVal dt As DataTable, ByVal ColumnName As String) As Boolean
        Dim i As Integer
        For i = 0 To dt.Columns.Count - 1
            If dt.Columns(i).ColumnName = ColumnName Then
                Return True
            End If
        Next
        Return False
    End Function

End Class