Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Collections
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports Microsoft.ApplicationBlocks.Data
Imports System.IO
Imports System.Drawing
Imports System.Reflection
Imports System.Net.Mail
Imports System.Drawing.Drawing2D
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System
Imports Microsoft.Win32
Imports System.Text
Imports System.Security.Cryptography
Imports DevExpress.Web.ASPxGridView


Namespace Provider
    Public Class CommonFunction
        Public Shared SHOW_MESSAGE As Boolean = True

        Public Shared Function GetMD5(ByVal input As String) As String
            Dim x As New System.Security.Cryptography.MD5CryptoServiceProvider
            Dim bs() As Byte = System.Text.Encoding.UTF8.GetBytes(input)
            bs = x.ComputeHash(bs)
            Dim s As New System.Text.StringBuilder()
            For Each b As Byte In bs
                s.Append(b.ToString("x2").ToLower())
            Next b
            Dim md5String As String = s.ToString()
            Return md5String
        End Function

        Public Shared Sub Info_SetValue(ByVal key As String, ByVal value As Object, ByVal dr As DataRow)
            Try
                If Not dr.Table.Columns.Contains(key) Then
                    dr.Table.Columns.Add(New DataColumn(key, GetType(Object)))
                End If
                If value Is Nothing Then
                    value = ""
                End If
                dr(key) = value
            Catch ex As Exception

            End Try
        End Sub

        Public Shared Function Info_GetValue(ByVal key As String, ByVal dr As DataRow) As Object
            Try
                If IsDBNull(dr(key)) Then
                    Return ""
                Else
                    Return dr(key)
                End If
            Catch ex As Exception
                Return ""
            End Try
        End Function

        Public Shared Function _ToString(ByVal obj As Object) As String
            Try
                If DBNull.Value.Equals(obj) Then
                    Return ""
                End If
                Return obj.ToString
            Catch ex As Exception
                Return ""
            End Try
        End Function

        Public Shared Function _ToUnSignString(ByVal obj As Object) As String
            Dim text As String = _ToString(obj)
            For i As Integer = 32 To 47
                text = text.Replace(Microsoft.VisualBasic.ChrW(i).ToString(), " ")
            Next
            text = Regex.Replace(text, "[^\w\.\- ]", " ")
            'text = Regex.Replace(text, "-{2,}", "-")
            text = Regex.Replace(text, "\s+", " ")
            Dim reg As New Regex("\p{IsCombiningDiacriticalMarks}+")
            Dim strFormD As String = text.Normalize(System.Text.NormalizationForm.FormD)
            Return reg.Replace(strFormD, String.Empty).Replace("đ", "d").Replace("Đ", "D")
        End Function

        Public Shared Function _ToBoolean(ByVal obj As Object) As Boolean
            Try
                Return Boolean.Parse(obj.ToString)
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Shared Function _ToInt(ByVal obj As Object) As Integer
            Try
                Return CInt(obj.ToString().Replace(",", ""))
            Catch ex As Exception
                Return -1
            End Try
        End Function

        Public Shared Function _ToUnsignInt(ByVal obj As Object) As Integer
            Try
                Dim result As Integer = CInt(obj.ToString().Replace(",", ""))
                Return If(result >= 0, result, 0)
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public Shared Function _ToUnsignIntWithNull(ByVal obj As Object) As Object
            Try
                Dim result As Object = CInt(obj.ToString().Replace(",", ""))
                Return If(CInt(result) > 0, result, Nothing)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Shared Function _ToDecimal(ByVal obj As Object) As Decimal
            Try
                Return CDec(obj.ToString().Replace(",", ""))
            Catch ex As Exception
                Return -1
            End Try
        End Function

        Public Shared Function _ToMoney(ByVal obj As Object) As Decimal
            Try
                Return CDec(obj.ToString().Replace(",", ""))
            Catch ex As Exception
                Return 0
            End Try
        End Function

        Public Shared Function _ToMoneyWithNull(ByVal obj As Object) As Object
            Try
                Dim result As Object = CDec(obj.ToString().Replace(",", ""))
                Return If(CDec(result) > 0, result, Nothing)
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Shared Function _ToDateTime(ByVal obj As Object, Optional ByVal format As String = Nothing) As DateTime
            Try
                Dim result As DateTime
                If format Is Nothing Then
                    result = CDate(obj)
                Else
                    result = DateTime.ParseExact(obj.ToString(), format, Nothing)
                End If
                Return result
            Catch ex As Exception
                Return DateTime.MinValue
            End Try
        End Function

        Public Shared Function _FormatMoney(ByVal obj As Object) As String
            Return String.Format("{0:#,0.##}", _ToMoney(obj))
        End Function

        Public Shared Sub SetCBOValue(ByVal ddl As DropDownList, ByVal value As Object, Optional ByVal findText As Boolean = False)
            ddl.ClearSelection()
            Dim item As ListItem = If(findText, ddl.Items.FindByText(_ToString(value)), ddl.Items.FindByValue(_ToString(value)))
            If item IsNot Nothing Then
                item.Selected = True
            End If
        End Sub

#Region "Export"
        Shared Function WORD_ReadContent(ByVal filepath As String) As String
            If Not File.Exists(filepath) Then
                Throw New Exception("Tệp tin không tồn tại.")
            End If

            Dim sw As New StreamReader(filepath, System.Text.Encoding.Unicode, False)
            Dim tmp As String = sw.ReadToEnd
            tmp = "<html><body>" & tmp.Replace("&nbsp;", " ").Replace("&quote;", "") & "</body> </html>"
            Try
                sw.Close()
            Catch ex As Exception

            End Try
            Return tmp
        End Function




#End Region

#Region "IMAGE"
        Public Shared Function CropImage(ByVal filepath As Object, ByVal width As Integer, ByVal height As Integer, ByVal px As Object, ByVal py As Object) As String
            Using oriImage = System.Drawing.Image.FromFile(filepath)
                Using bmp = New System.Drawing.Bitmap(width, height)
                    bmp.SetResolution(oriImage.HorizontalResolution, oriImage.VerticalResolution)
                    Using grp = Graphics.FromImage(bmp)
                        grp.SmoothingMode = SmoothingMode.AntiAlias
                        grp.InterpolationMode = InterpolationMode.HighQualityBicubic
                        grp.PixelOffsetMode = PixelOffsetMode.HighQuality
                        grp.DrawImage(oriImage, New System.Drawing.Rectangle(0, 0, width, height), px, py, width, height, System.Drawing.GraphicsUnit.Pixel)
                        oriImage.Dispose()
                        bmp.Save(filepath)
                    End Using
                End Using
            End Using
            Return ""
        End Function
#End Region

#Region "Notify"
        Public Shared Sub Notify_Error(ByVal _Label As Label, ByVal _Message As String)
            _Label.Text = _Message
            _Label.ForeColor = Color.Red
            _Label.Style.Remove("font-size")
            _Label.Style.Add("font-size", "11px")
        End Sub
        Public Shared Sub Notify_Error(ByVal _Label As Label, ByVal _Message As String, ByVal _Page As System.Web.UI.UserControl)
            _Label.Text = _Message
            _Label.ForeColor = Color.Red
            _Label.Style.Remove("font-size")
            _Label.Style.Add("font-size", "11px")
        End Sub
        Public Shared Sub Notify_Success(ByVal _Label As Label, ByVal _Message As String)
            Dim Script As String = "notify_success('" + _Message + "');"
            'DotNetNuke.UI.Utilities.ClientAPI.RegisterStartUpScript(_Label.Page, "xxx", "<script type='text/javascript'>jQuery(document).ready(function() {" + Script + "});</script>")
            '_Label.Text = _Message
            '_Label.ForeColor = Color.Lime
            '_Label.Style.Remove("font-size")
            '_Label.Style.Add("font-size", "11px")
        End Sub
        Public Shared Sub Notify_Warning(ByVal _Label As Label, ByVal _Message As String)
            _Label.Text = _Message
            _Label.Style.Remove("font-size")
            _Label.Style.Add("font-size", "11px")
            _Label.Style.Remove("color")
            _Label.Style.Add("color", "#56BBEA")
        End Sub


#End Region

        Public Shared Function Query(ByVal key As String, ByVal page As UserControl) As String
            Try
                Return page.Request(key).ToString()
            Catch ex As Exception
                Return ""
            End Try
        End Function
        Public Shared Function GetLanguage() As String
            Return System.Threading.Thread.CurrentThread.CurrentCulture.ToString()
        End Function
        Public Shared Function UrlReferrer(ByVal page As UserControl) As String
            Try
                Return page.Request.UrlReferrer.AbsoluteUri
            Catch ex As Exception
                Return ""
            End Try
        End Function

#Region "DDL"
        Public Shared Sub DDL_SelectedValue(ByVal ddl As DropDownList, ByVal value As String)
            Try
                If ddl.SelectedItem IsNot Nothing Then
                    ddl.SelectedItem.Selected = False
                End If
                ddl.SelectedValue = value
            Catch ex As Exception

            End Try
        End Sub
        Public Shared Function DDL_GetText(ByVal ddl As DropDownList, ByVal obj As Object) As String
            Try
                Return ddl.Items.FindByValue(obj.ToString).Text
            Catch ex As Exception
                Return ""
            End Try
        End Function
        Public Shared Sub DDL_BindData(ByVal ddl As DropDownList, ByVal source As [Object], ByVal dataTextField As String, ByVal dataValueField As String)
            ddl.DataTextField = dataTextField
            ddl.DataValueField = dataValueField
            ddl.DataSource = source
            ddl.DataBind()
        End Sub
        Public Shared Sub DDL_BindData(ByVal ddl As DropDownList, ByVal source As [Object], ByVal dataTextField As String, ByVal dataValueField As String, ByVal headertext As String, ByVal headervalue As String)
            ddl.Items.Clear()
            ddl.ClearSelection()
            ddl.SelectedIndex = -1
            ddl.SelectedValue = Nothing
            ddl.DataTextField = dataTextField
            ddl.DataValueField = dataValueField
            ddl.DataSource = source
            ddl.DataBind()
            If headertext IsNot Nothing Then
                ddl.Items.Insert(0, New ListItem(headertext, headervalue))
            End If
        End Sub
        Public Shared Sub DDL_BindData(ByVal ddl As DropDownList, ByVal source As ListItem(), ByVal headertext As String, ByVal headervalue As String)
            ddl.DataTextField = "Text"
            ddl.DataValueField = "Value"
            ddl.DataSource = source
            ddl.DataBind()
            If headertext IsNot Nothing Then
                ddl.Items.Insert(0, New ListItem(headertext, headervalue))
            End If
        End Sub
        Public Shared Sub DDL_BindData(ByVal ddl As DropDownList, ByVal source As ListItem())
            ddl.DataTextField = "Text"
            ddl.DataValueField = "Value"
            ddl.DataSource = source
            ddl.DataBind()
        End Sub
        Public Shared Sub LB_BindData(ByVal lb As ListBox, ByVal source As [Object], ByVal dataTextField As String, ByVal dataValueField As String)
            lb.DataTextField = dataTextField
            lb.DataValueField = dataValueField
            lb.DataSource = source
            lb.DataBind()
        End Sub
        Public Shared Sub LB_BindData(ByVal lb As ListBox, ByVal source As [Object], ByVal dataTextField As String, ByVal dataValueField As String, ByVal headertext As String, ByVal headervalue As String)
            lb.DataTextField = dataTextField
            lb.DataValueField = dataValueField
            lb.DataSource = source
            lb.DataBind()
            If headertext IsNot Nothing Then
                lb.Items.Insert(0, New ListItem(headertext, headervalue))
            End If
        End Sub
#End Region

#Region "JS"
        Public Shared Sub JS_Calendar(ByVal _Page As UserControl, ByVal _Control As Control)
            JS_Execute_StartupScript(_Page, "calendar", "jQuery( '#" + _Control.ClientID + "').datepicker();")
        End Sub

        Public Shared Sub JS_ShowError(ByVal uc As UserControl, ByVal message As String)
            Dim script As String = "fancyAlert('" & message.Replace("'", "") & "');"
            JS_ExecuteScript(uc, "error", script)
        End Sub

        Public Shared Sub JS_ShowMessageBox(ByVal uc As UserControl, ByVal message As String)
            Dim script As String = "fancyAlert('" & message.Replace("""", "").Replace("'", "") & "','');"
            JS_ExecuteScript(uc, "message", script)
        End Sub

        Public Shared Sub JS_ShowMessageBox(ByVal uc As UserControl, ByVal message As String, ByVal callback As String)
            Dim script As String = "fancyAlert('" & message.Replace("""", "").Replace("'", "") & "','" + callback + "');"
            JS_ExecuteScript(uc, "message", script)
        End Sub

        ''' <summary>
        ''' Do click on HTML element 
        ''' </summary>
        ''' <param name="uc"></param>
        ''' <param name="selector">jquery selector</param>
        Public Shared Sub JS_Click(ByVal uc As UserControl, ByVal selector As String)
            Dim script As String = "jQuery('" & selector & "').click();"
            JS_ExecuteScript(uc, "click", script)
        End Sub

        ''' <summary>
        ''' Display HTML tag with specific identify
        ''' </summary>
        ''' <param name="uc"></param>
        ''' <param name="id">Client ID</param>
        Public Shared Sub JS_ShowComponent(ByVal uc As UserControl, ByVal id As String)
            Dim script As String = "jQuery('#" & id & "').fadeIn();"
            JS_ExecuteScript(uc, "component", script)
        End Sub

        Public Shared Sub JS_ExecuteScript(ByVal uc As UserControl, ByVal key As String, ByVal script As String)
            'DotNetNuke.UI.Utilities.ClientAPI.RegisterStartUpScript(uc.Page, key, "<script type='text/javascript'>jQuery(document).ready(function() {" + script + "});</script>")
        End Sub

        Public Shared Sub JS_Execute_StartupScript(ByVal uc As UserControl, ByVal key As String, ByVal script As String)
            'DotNetNuke.UI.Utilities.ClientAPI.RegisterStartUpScript(uc.Page, key, "<script type='text/javascript'>jQuery(document).ready(function() {" + script + "});</script>")
        End Sub
#End Region


#Region "Get / Set form inform mation"
        Public Shared Function Form_GetInformation(ByVal _GroupControl As Control, ByVal dr As DataRow) As DataRow
            For Each _Control As Control In _GroupControl.Controls
                Try
                    Dim _Filed As String = _Control.ID
                    If _Filed Is Nothing Then
                        Continue For
                    End If
                    If _Filed.StartsWith("_") Then
                        Dim value As String = ""
                        If TypeOf _Control Is TextBox Then
                            value = DirectCast(_Control, TextBox).Text
                        End If
                        If TypeOf _Control Is DropDownList Then
                            value = DirectCast(_Control, DropDownList).SelectedValue
                        End If
                        'If TypeOf _Control Is TextEditor Then
                        '    Try
                        '        value = DirectCast(_Control, TextEditor).Text
                        '    Catch ex As Exception
                        '        value = ""
                        '    End Try
                        'End If
                        If TypeOf _Control Is Literal Then
                            value = DirectCast(_Control, Literal).Text
                        End If
                        If TypeOf _Control Is Label Then
                            value = DirectCast(_Control, Label).Text
                        End If
                        If TypeOf _Control Is HiddenField Then
                            value = DirectCast(_Control, HiddenField).Value
                        End If
                        If TypeOf _Control Is CheckBox Then
                            value = DirectCast(_Control, CheckBox).Checked
                        End If
                        Dim colname As String = _Filed.Substring(1, _Filed.Length - 1)
                        CommonFunction.Info_SetValue(colname, value, dr)
                    End If
                Catch ex As Exception

                End Try
            Next
            Return dr
        End Function

        Public Shared Function Form_GetInformation(ByVal _GroupControl As Control) As DataRow
            Dim dt As New DataTable()
            Dim dr As DataRow
            For Each _Control As Control In _GroupControl.Controls
                Dim _Filed As String = _Control.ID
                If _Filed Is Nothing Then
                    Continue For
                End If
                If _Filed.StartsWith("_") Then
                    dt.Columns.Add(_Filed.Substring(1, _Filed.Length - 1), GetType(String))
                End If
            Next
            dr = dt.NewRow()
            For Each _Control As Control In _GroupControl.Controls
                Dim _Filed As String = _Control.ID
                If _Filed Is Nothing Then
                    Continue For
                End If
                If _Filed.StartsWith("_") Then
                    Dim value As String = ""
                    If TypeOf _Control Is TextBox Then
                        value = DirectCast(_Control, TextBox).Text
                    End If
                    If TypeOf _Control Is DropDownList Then
                        value = DirectCast(_Control, DropDownList).SelectedValue
                    End If
                    'If TypeOf _Control Is TextEditor Then
                    '    Try
                    '        value = DirectCast(_Control, TextEditor).Text
                    '    Catch ex As Exception
                    '        value = ""
                    '    End Try
                    'End If
                    If TypeOf _Control Is Literal Then
                        value = DirectCast(_Control, Literal).Text
                    End If
                    If TypeOf _Control Is Label Then
                        value = DirectCast(_Control, Label).Text
                    End If
                    If TypeOf _Control Is HiddenField Then
                        value = DirectCast(_Control, HiddenField).Value
                    End If
                    If TypeOf _Control Is CheckBox Then
                        value = DirectCast(_Control, CheckBox).Checked
                    End If
                    dr(_Filed.Substring(1, _Filed.Length - 1)) = value
                End If
            Next
            Return dr
        End Function

        Public Shared Function Form_GetInformation(ByVal _GroupControl As Control, ByVal _Prefix As String) As DataRow
            Dim dt As New DataTable()
            Dim dr As DataRow
            For Each _Control As Control In _GroupControl.Controls
                Dim _Filed As String = _Control.ID
                If _Filed Is Nothing Then
                    Continue For
                End If
                If _Filed.StartsWith(_Prefix) Then
                    dt.Columns.Add(_Filed.Substring(_Prefix.Length, _Filed.Length - _Prefix.Length), GetType(String))
                End If
            Next
            dr = dt.NewRow()
            For Each _Control As Control In _GroupControl.Controls
                Dim _Filed As String = _Control.ID
                If _Filed Is Nothing Then
                    Continue For
                End If
                If _Filed.StartsWith(_Prefix) Then
                    Dim value As String = ""
                    If TypeOf _Control Is TextBox Then
                        value = DirectCast(_Control, TextBox).Text
                    End If
                    If TypeOf _Control Is DropDownList Then
                        value = DirectCast(_Control, DropDownList).SelectedValue
                    End If
                    'If TypeOf _Control Is TextEditor Then
                    '    Try
                    '        value = DirectCast(_Control, TextEditor).Text
                    '    Catch ex As Exception
                    '        value = ""
                    '    End Try
                    'End If
                    If TypeOf _Control Is Literal Then
                        value = DirectCast(_Control, Literal).Text
                    End If
                    If TypeOf _Control Is Label Then
                        value = DirectCast(_Control, Label).Text
                    End If
                    If TypeOf _Control Is HiddenField Then
                        value = DirectCast(_Control, HiddenField).Value
                    End If
                    dr(_Filed.Substring(_Prefix.Length, _Filed.Length - _Prefix.Length)) = value
                End If
            Next
            Return dr
        End Function

        Public Shared Function Form_SetInformation(ByVal _GroupControl As Control, ByVal dr As DataRow) As DataRow
            If dr Is Nothing Then
                Return dr
            End If
            For Each _Control As Control In _GroupControl.Controls
                Dim _Filed As String = _Control.ID
                If _Filed Is Nothing Then
                    Continue For
                End If
                If _Filed.StartsWith("_") Then
                    Dim _Column As String = _Filed.Substring(1, _Filed.Length - 1)
                    Dim value As String = ""
                    If Not dr.Table.Columns.Contains(_Column) Then
                        Continue For
                    End If
                    Dim obj As Object = dr(_Column)
                    If obj Is Nothing Then
                        Continue For
                    End If
                    value = obj.ToString()
                    If TypeOf obj Is DateTime Then
                        value = CType(obj, DateTime).ToString()
                    End If
                    If TypeOf _Control Is TextBox Then
                        DirectCast(_Control, TextBox).Text = value
                    End If
                    If TypeOf _Control Is DropDownList Then
                        Dim li As ListItem = DirectCast(_Control, DropDownList).Items.FindByValue(value)
                        If li IsNot Nothing Then
                            DirectCast(_Control, DropDownList).SelectedItem.Selected = False
                            li.Selected = True
                        End If
                    End If
                    'If TypeOf _Control Is TextEditor Then
                    '    DirectCast(_Control, TextEditor).Text = value
                    'End If
                    If TypeOf _Control Is Label Then
                        DirectCast(_Control, Label).Text = value
                    End If
                    If TypeOf _Control Is Literal Then
                        DirectCast(_Control, Literal).Text = value
                    End If
                    If TypeOf _Control Is HiddenField Then
                        DirectCast(_Control, HiddenField).Value = value
                    End If
                    If TypeOf _Control Is CheckBox Then
                        If value.Trim = "" Then
                            DirectCast(_Control, CheckBox).Checked = "False"
                        Else
                            DirectCast(_Control, CheckBox).Checked = value
                        End If

                    End If
                End If
            Next
            Return dr
        End Function
        Public Shared Function Form_SetInformation(ByVal _GroupControl As Control, ByVal dr As DataRow, ByVal _Prefix As String) As DataRow
            If dr Is Nothing Then
                Return dr
            End If
            For Each _Control As Control In _GroupControl.Controls
                Dim _Filed As String = _Control.ID
                If _Filed Is Nothing Then
                    Continue For
                End If
                If _Filed.StartsWith(_Prefix) Then
                    Dim _Column As String = _Filed.Substring(_Prefix.Length, _Filed.Length - _Prefix.Length)
                    Dim value As String = ""
                    If Not dr.Table.Columns.Contains(_Column) Then
                        Continue For
                    End If
                    Dim obj As Object = dr(_Column)
                    If obj Is Nothing Then
                        Continue For
                    End If
                    value = obj.ToString()
                    If TypeOf obj Is DateTime Then
                        value = CType(obj, DateTime).ToString()
                    End If
                    If TypeOf _Control Is TextBox Then
                        DirectCast(_Control, TextBox).Text = value
                    End If
                    If TypeOf _Control Is DropDownList Then
                        Dim li As ListItem = DirectCast(_Control, DropDownList).Items.FindByValue(value)
                        If li IsNot Nothing Then
                            li.Selected = True
                        End If
                    End If
                    'If TypeOf _Control Is TextEditor Then
                    '    DirectCast(_Control, TextEditor).Text = value
                    'End If
                    If TypeOf _Control Is Label Then
                        DirectCast(_Control, Label).Text = value
                    End If
                    If TypeOf _Control Is Literal Then
                        DirectCast(_Control, Literal).Text = value
                    End If
                    If TypeOf _Control Is HiddenField Then
                        DirectCast(_Control, HiddenField).Value = value
                    End If
                End If
            Next
            Return dr
        End Function

        Public Shared Sub Form_ClearInformation(ByVal _GroupControl As Control)
            For Each _Control As Control In _GroupControl.Controls
                Dim temp As Object = _Control.ID
                If temp Is Nothing Then
                    Continue For
                End If
                If Not temp.ToString.StartsWith("_") Then
                    Continue For
                End If

                If TypeOf _Control Is TextBox Then
                    DirectCast(_Control, TextBox).Text = ""
                    Continue For
                End If

                If TypeOf _Control Is DropDownList Then
                    Continue For
                End If

                If TypeOf _Control Is HiddenField Then
                    DirectCast(_Control, HiddenField).Value = ""
                    Continue For
                End If

                If TypeOf _Control Is Label Then
                    DirectCast(_Control, Label).Text = ""
                    Continue For
                End If

                If TypeOf _Control Is Literal Then
                    DirectCast(_Control, Literal).Text = ""
                    Continue For
                End If
                If TypeOf _Control Is CheckBox Then
                    DirectCast(_Control, CheckBox).Checked = False
                    Continue For
                End If
                'If TypeOf _Control Is TextEditor Then
                '    DirectCast(_Control, TextEditor).Text = "&lt;br&gt;"
                'End If
            Next
        End Sub
#End Region

#Region "IMG"
        Public Shared Function IMG_FormatImage(ByVal endcodeimgage As String) As String
            Return HttpUtility.HtmlDecode(endcodeimgage).Replace("<p>", "").Replace("</p>", "")
        End Function

        Public Shared Function IMG_FormatImage(ByVal endcodeimgage As Object) As String
            Dim temp As String = ""
            If endcodeimgage IsNot Nothing Then
                temp = endcodeimgage.ToString()
            End If
            Return IMG_FormatImage(temp)
        End Function
#End Region

#Region "Date"
        Public Shared Function DATE_Format(ByVal format__1 As String, ByVal obj As Object) As String
            Try
                Return DirectCast(obj, DateTime).ToString(format__1)
            Catch ex As Exception
                Return ""
            End Try
        End Function

        Public Shared Function DATE_Format(ByVal format__1 As String, ByVal obj As Object, ByVal _Default As Object) As String
            Dim str As String = _Default
            Try
                str = DirectCast(obj, DateTime).ToString(format__1)
                If str.StartsWith("01/01/0001") Then
                    str = _Default
                End If
            Catch ex As Exception

            End Try
            Return str
        End Function

        Public Shared Function DATE_Format(ByVal obj As Object) As String
            Try
                Return String.Format("{0:dd/MM/yyyy HH:mm}", obj)
            Catch ex As Exception
                Return ""
            End Try
        End Function

        Public Shared Function DATE_Format_DDMMYYYY(ByVal obj As Object) As String
            Try
                Return String.Format("{0:dd/MM/yyyy}", obj)
            Catch ex As Exception
                Return ""
            End Try
        End Function
#End Region

#Region "Number"
        Public Shared Function NUMBER_Format(ByVal obj As Object) As String
            Try
                Dim result As String = String.Format("{0:#,##}", Double.Parse(obj.ToString))
                If result.Trim = "" Then
                    Return 0
                End If
                Return result.Replace(",", ".")
            Catch ex As Exception

            End Try
            Return ""
        End Function

        Public Shared Function NUMBER_Format(ByVal obj As Object, ByVal _Multil As Object) As String
            Try
                Dim result As String = String.Format("{0:#,##}", Double.Parse(obj.ToString) * _Multil)
                If result.Trim = "" Then
                    Return 0
                End If
                Return result.Replace(",", ".")
            Catch ex As Exception

            End Try
            Return ""
        End Function

        Public Shared Function ConvertHourByNumber(ByVal obj As Object) As String
            Try
                Dim result As String = "000" + obj.ToString()
                result = result.Substring(result.Length - 4, 4)
                Return result.Insert(2, ":")
            Catch ex As Exception

            End Try
            Return ""
        End Function
#End Region
        Public Shared Function ConvertTiengVietCoDauThanhKhongDauV2(ByVal sTiengVietCoDau As String) As String
            Dim r As Regex = New Regex("[ĂÂẠÃẢÀÁăâạãảàáẶẴẲẰẮặẵẳằắẬẪẨẦẤậẫẩầấ]")
            sTiengVietCoDau = r.Replace(sTiengVietCoDau, "a")

            r = New Regex("[ÊẸẼẺÈÉêẹỄẽẾỂếễềỆệèểỀéẻ]")
            sTiengVietCoDau = r.Replace(sTiengVietCoDau, "e")

            r = New Regex("[ỊỈĨÌÍịĩỉìí]")
            sTiengVietCoDau = r.Replace(sTiengVietCoDau, "i")

            r = New Regex("[ỰỮỬỪỨựữửừứ]")
            sTiengVietCoDau = r.Replace(sTiengVietCoDau, "u")

            r = New Regex("[ỴỸỶỲÝỹỵỷỳý]")
            sTiengVietCoDau = r.Replace(sTiengVietCoDau, "y")

            r = New Regex("[đĐĐ]")
            sTiengVietCoDau = r.Replace(sTiengVietCoDau, "d")

            r = New Regex("[ỢỠỞỜỚợỡởờớỘÔỔỒỐộỗổồốƠỎÕÒÓỌÔôọóòỏõơ]")
            sTiengVietCoDau = r.Replace(sTiengVietCoDau, "o")

            r = New Regex("[ỰỮỬỪỨựữửừứƯỤŨỦÙÚưụũủùú]")
            sTiengVietCoDau = r.Replace(sTiengVietCoDau, "u")
            '--------------------------------- 
            sTiengVietCoDau = sTiengVietCoDau.Trim

            r = New Regex("[():/,?.]")
            sTiengVietCoDau = r.Replace(sTiengVietCoDau, "-")

            sTiengVietCoDau = sTiengVietCoDau.Replace("  ", " ")
            'sTiengVietCoDau = sTiengVietCoDau.Replace("""", "-")
            'sTiengVietCoDau = sTiengVietCoDau.Replace("--", "-")

            Return sTiengVietCoDau
        End Function

#Region "Current URL & Parameter"

        Public Shared Function getUrl(ByVal splitParam As String) As String

            Dim specy As String = String.Empty
            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
            'Dim path As String = HttpContext.Current.Request.Url.AbsolutePath
            'Dim host As String = HttpContext.Current.Request.Url.Host

            Dim urls As String() = url.Split("?")
            If urls.Length > 1 Then

                Dim params As String() = urls(1).Split("&")
                Dim tmpUrl As String = urls(0)
                specy = "?"
                For Each param As String In params

                    If param.StartsWith(splitParam) Then
                        Continue For
                    End If

                    tmpUrl = tmpUrl + specy + param

                    If specy = "?" Then
                        specy = "&"
                    End If

                Next
                url = tmpUrl
            Else
                specy = "?"
            End If

            Return url + specy

        End Function

        Public Shared Function getUrl(ByVal current As String, ByVal pagesize As String) As String

            Dim specy As String = String.Empty
            Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
            'Dim path As String = HttpContext.Current.Request.Url.AbsolutePath
            'Dim host As String = HttpContext.Current.Request.Url.Host

            Dim urls As String() = url.Split("?")
            If urls.Length > 1 Then

                Dim params As String() = urls(1).Split("&")
                Dim tmpUrl As String = urls(0)
                specy = "?"
                For Each param As String In params

                    If param.StartsWith("current") Or param.StartsWith("pagesize") Then
                        Continue For
                    End If

                    tmpUrl = tmpUrl + specy + param

                    If specy = "?" Then
                        specy = "&"
                    End If

                Next
                url = tmpUrl
            Else
                specy = "?"
            End If

            Return url + specy + "current=" + current + "&pagesize=" + pagesize

        End Function

#End Region

        Public Shared Function StrSplit(ByVal str As String, Optional ByVal splitters() As Char = Nothing) As String()
            If splitters Is Nothing OrElse splitters.Length = 0 Then
                splitters = New Char() {CChar(","), CChar(";")}
            End If
            Dim result() As String = str.Split(splitters)
            For i As Integer = 0 To result.Length - 1
                result(i) = result(i).Trim()
            Next
            Return result
        End Function

        Public Shared Sub SetSessionMessage(ByVal message As String)
            HttpContext.Current.Session("Message") = message
        End Sub

        Public Shared Sub SetSessionErrorMessage(ByVal message As String)
            HttpContext.Current.Session("ErrorMessage") = message
        End Sub

        Public Shared Sub CheckSessionMessage(ByVal page As Web.UI.Page)
            Dim messageSession As Object = HttpContext.Current.Session("Message")
            Dim errorMessageSession As Object = HttpContext.Current.Session("ErrorMessage")
            If messageSession IsNot Nothing AndAlso messageSession.ToString().Trim().Length > 0 Then
                ShowStartupInfoMessage(page, messageSession.ToString())
                HttpContext.Current.Session.Remove("Message")
            ElseIf errorMessageSession IsNot Nothing AndAlso errorMessageSession.ToString().Trim().Length > 0 Then
                ShowStartupErrorMessage(page, errorMessageSession.ToString())
                HttpContext.Current.Session.Remove("ErrorMessage")
            End If
        End Sub

        Public Shared Sub SetPostBackStatus(ByVal ctrl As WebControl) ', Optional ByVal messageOnly As Boolean = True
            ctrl.Attributes("data-status") = "done"
            'If Not messageOnly Then
            '    ctrl.Attributes("data-postback") = "done"
            '    ctrl.Attributes("data-partial") = "done"
            'End If
        End Sub

        Public Shared Sub SetProcessStatus(ByVal ctrl As WebControl, Optional ByVal success As Boolean = True)
            ctrl.Attributes("data-process") = If(success, "success", "fail")
        End Sub

        Public Shared Function TruncString(ByVal str As String, ByVal length As Integer, Optional ByVal suffix As String = "...")
            If str.Length > length Then
                str = str.Substring(0, length) & suffix
            End If
            Return str
        End Function

        Private Shared Function BuildPushMessage(ByVal message As String, ByVal type As String) As String
            message = TruncString(message, 120)
            Return "<div id='PushMessage' style='display: none; transition: none; position: fixed; top: 150px; left: 134px; width: 100%; text-align: center; z-index: 8000'><span id='PushMessageContent' style='white-space: nowrap; padding: 8px 25px; color: #fff; border-radius: 15px; font-size: 1.2em; -webkit-border-radius: 15px; -moz-border-radius: 15px; box-shadow: 0 0 10px #666; -webkit-box-shadow: 0 0 10px #666; -moz-box-shadow: 0 0 10px #666; background-color: " + If(type = "info", "#2DB63A", "#CB3737") + "; '>" + message + "</span></div>"
        End Function

        Public Shared Sub ShowInfoMessage(ByVal container As Panel, ByVal message As String)
            Dim div As Literal = New Literal()
            div.Text = BuildPushMessage(message, "info")
            container.Controls.Add(div)
        End Sub

        Public Shared Sub ShowErrorMessage(ByVal container As Panel, ByVal message As String)
            Dim div As Literal = New Literal()
            div.Text = BuildPushMessage(message, "error")
            container.Controls.Add(div)
        End Sub

        Public Shared Sub ShowStartupInfoMessage(ByVal page As Web.UI.Page, ByVal message As String)
            message = message.Replace("""", "'").Replace(Environment.NewLine, " ")
            Dim script As String = String.Concat("<script type='text/javascript'>$(document).ready(function(){ShowInfoMessage(""", message, """)})</script>")
            RegisterStartupClientScript(page, "info-message", script)
        End Sub

        Public Shared Sub ShowStartupErrorMessage(ByVal page As Web.UI.Page, ByVal message As String)
            message = message.Replace("""", "'").Replace(Environment.NewLine, " ")
            Dim script As String = String.Concat("<script type='text/javascript'>$(document).ready(function(){ShowErrorMessage(""", message, """)})</script>")
            RegisterStartupClientScript(page, "error-message", script)
        End Sub

        Public Shared Sub RegisterStartupClientScript(ByVal page As Web.UI.Page, ByVal key As String, ByVal script As String)
            If Not page.ClientScript.IsStartupScriptRegistered(key) Then
                page.ClientScript.RegisterStartupScript(page.GetType(), key, script, False)
            End If
        End Sub

        Public Shared Function GetNull(ByVal Field As Object) As Object
            If Field Is Nothing Then
                Return DBNull.Value
            Else
                Return Field
            End If
        End Function

        Public Shared Function CheckNothing(ByVal Value As Object, Optional ByVal isDateType As Boolean = False) As Object
            If Not isDateType Then
                If Value Is Nothing Then Return DBNull.Value
            Else
                If Value = Nothing Then Return DBNull.Value
            End If
            Return Value
        End Function

        Public Shared Function CheckDBNull(ByVal value As Object) As Object
            If value Is Nothing Or value = Nothing Then
                Return DBNull.Value
            End If
            Return value
        End Function


        Public Shared Function EncryptPassword(ByVal Password As String) As String
            Dim salt As String = "Mu1Ig2cnhuugFbWQpRmo4g=="
            Return EncodePassword(Password, 1, salt)
        End Function

        Private Shared Function EncodePassword(ByVal pass As String, ByVal passwordFormat As Integer, ByVal salt As String) As String
            If passwordFormat = 0 Then
                ' MembershipPasswordFormat.Clear
                Return pass
            End If

            Dim bIn As Byte() = Encoding.Unicode.GetBytes(pass)
            Dim bSalt As Byte() = Convert.FromBase64String(salt)
            Dim bAll As Byte() = New Byte(bSalt.Length + (bIn.Length - 1)) {}
            Dim bRet As Byte() = Nothing

            Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length)
            Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length)
            If passwordFormat = 1 Then
                ' MembershipPasswordFormat.Hashed
                Dim s As HashAlgorithm = HashAlgorithm.Create("SHA1")
                bRet = s.ComputeHash(bAll)
            Else
                bRet = Nothing  'EncryptPassword(bAll)
            End If

            Return Convert.ToBase64String(bRet)
        End Function

        Public Shared Sub LoadDataToComboBox(ByVal ddl As DropDownList, ByVal dt As DataTable, ByVal textField As String, ByVal valueField As String, Optional ByVal hasAll As Boolean = False, Optional ByVal allText As String = "All", Optional ByVal allValue As String = "", Optional ByVal filter As String = "")
            Dim dv As DataView = dt.DefaultView
            dv.RowFilter = filter
            ddl.DataSource = dv
            ddl.DataTextField = textField
            ddl.DataValueField = valueField
            ddl.DataBind()
            If hasAll Then
                ddl.Items.Insert(0, New ListItem(allText, allValue))
            End If
        End Sub

        Public Shared Sub LoadLookupDataToComboBox(ByVal ddl As DropDownList, ByVal dt As DataTable, Optional ByVal hasAll As Boolean = True, Optional ByVal allText As String = "All", Optional ByVal allValue As String = "", Optional ByVal filter As String = "")
            LoadDataToComboBox(ddl, dt, "Text", "Value", hasAll, allText, allValue, filter)
        End Sub

        Public Shared Sub LoadDataToGrid(ByVal grv As ASPxGridView, ByVal dt As DataTable, Optional ByVal filter As String = "", Optional ByVal noField As String = Nothing, Optional ByVal statusCol As String = Nothing, Optional ByVal stt As Boolean = True)
            If dt Is Nothing Then
                grv.DataSource = Nothing
                grv.DataBind()
                Return
            End If
            Dim dtData = dt.Copy()
            If statusCol IsNot Nothing AndAlso statusCol.Trim().Length > 0 Then
                dtData.Columns.Add(statusCol)
                For Each item As DataRow In dtData.Rows
                    item(statusCol) = stt
                Next
            End If
            Dim dv As DataView = dtData.DefaultView
            dv.RowFilter = filter
            If noField IsNot Nothing AndAlso noField.Trim().Length > 0 Then
                For index As Integer = 0 To dv.Count - 1
                    dv.Item(index)(noField) = index + 1
                Next
            End If
            grv.DataSource = dv
            grv.DataBind()
        End Sub

        Public Shared Sub CheckSession()
            Dim context As HttpContext = HttpContext.Current
            If context.Session("UserName") Is Nothing OrElse context.Session("UserName").ToString().Trim().Length = 0 Then
                'context.Response.Redirect("~/Logout.aspx", True)
                context.Session.Clear()
                Dim redirect As String = String.Format("~/Login.aspx?redirect={0}", context.Request.RawUrl.Replace("?", ";begin;").Replace("&", ";and;"))
                context.Response.Redirect(redirect, True)
            End If
        End Sub

        Public Shared Function ImportTemplate() As String
            Try
                Dim username As String = CommonFunction._ToString(HttpContext.Current.Session("UserName"))
                Dim attachmentFiles As HttpFileCollection = HttpContext.Current.Request.Files()
                Dim attFile As HttpPostedFile = attachmentFiles(0)

                If attFile.FileName IsNot Nothing AndAlso attFile.FileName.Length > 0 Then
                    Dim path As String = "/Import/"
                    If Not Directory.Exists(HttpContext.Current.Server.MapPath(path)) Then
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path))
                    End If
                    path = String.Format("{0}/{1}_{2}_{3}", path, username, DateTime.Now.ToString("yyMMddHHmmssfffff"), attFile.FileName)
                    path = HttpContext.Current.Server.MapPath(path)
                    attFile.SaveAs(path)
                    Return path
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Shared Sub CheckRole(ByVal ParamArray roles() As RoleType)
            Dim role As String = _ToString(HttpContext.Current.Session("RoleType"))
            For i As Integer = 0 To roles.Length - 1
                If roles(i).ToString().ToLower() = role.ToLower() Then
                    Return
                End If
            Next
            HttpContext.Current.Response.Redirect("~/Logout.aspx", True)
        End Sub

        Public Shared Sub ReleaseObject(ByVal obj As Object)
            Try
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
                obj = Nothing
            Catch ex As Exception
                obj = Nothing
            Finally
                GC.Collect()
            End Try
        End Sub

    End Class
End Namespace
