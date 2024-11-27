Imports Provider
Imports System.Data
Imports System.DirectoryServices
Imports System.Net.Mail

Partial Public Class ResetPass
    Inherits System.Web.UI.Page

    Private _redirect As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Stop Caching in IE
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache)
        'Stop Caching in Firefox
        Response.Cache.SetNoStore()
        '
        'StartupJS.Text = "<script type='text/javascript'>alert('asdf')</script>"
        If Not IsPostBack Then
            'txtPassword.Text = ""
            'txtUserName.Text = ""
        End If
        _redirect = Request.QueryString("redirect")
        If _redirect Is Nothing OrElse _redirect.Trim().Length = 0 Then
            _redirect = "User\UserProfile.aspx"
        Else
            _redirect = _redirect.Replace(";begin;", "?").Replace(";and;", "&")
        End If
        '
        If Session("UserName") IsNot Nothing Then
            Response.Redirect(_redirect)
        End If
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogin.Click
        Try
            CommonFunction.SetPostBackStatus(btnLogin)
            CheckConnect()
            Dim strPassWord = String.Empty
            Dim dtUser As DataTable
            Dim mailTo As String = txtUserName.Text & "@toyotavn.com.vn"


            If ValidateActiveDirectoryLogin(txtUserName.Text, txtPassword.Text) = True Then

                'Lấy thông tin User Name
                dtUser = UserProvider.tbl_Users_Login_ByEmail(mailTo)

                'Update PassWord
                Dim strNew As String = getNewPass()
                Dim strNewPass = CommonFunction.EncryptPassword(strNew)
 
                'Gửi Email tới User để confirm to reset mật khẩu
                Dim from As String = ConfigurationManager.AppSettings("BTSSupportEmail").Replace("[", "<").Replace("]", ">")
                Dim cc As String = ""
                Dim bcc As String = "syhieuvt@toyotavn.com.vn"
                Dim subject As String = "[BTS system]: New Password information of {0} - Mật khẩu mới của {0}"
                Dim body As New StringBuilder()
                body.Append("<p><strong>Dear {0} - san</strong></p>")
                body.Append("<p>We would like to send you the account information to login Business Trip Online System (BTS) as bellow:")
                body.Append("<p style='margin-left: 30px'>* Username: <span style='color: red'>{1}</span> (This is your employee code)")
                body.Append("<br />* Password: <span style='color: red'>{2}</span>(You must change this default password when you login to BTS system for the first time)</p></li></ol>")
                body.Append("<p>If you need any support, please feel free to contact IT Dept. helpdesk:")
                body.Append("<p style='margin-left: 80px'>* Extension No: Head Office: (861) 2222")
                body.Append("<br />* Email: support@toyotavn.com.vn</p>")
                body.Append("<p><strong>Thanks & Best regards<br />BTS support team</strong></p>")
                '    
                Using srv As TMVEmailService.EmailService = New TMVEmailService.EmailService()
                    If dtUser.Rows.Count > 0 Then
                        For Each item As DataRow In dtUser.Rows
                            If mailTo.Trim().Length > 0 Then
                                Dim checkFirst As Boolean = True
                                UserProvider.tbl_UserChangePass(item("UserName"), strNewPass, checkFirst)
                                srv.SendEmail(from, mailTo, cc, bcc, String.Format(subject.ToString(), item("FullName")), String.Format(body.ToString(), item("FullName"), item("UserName"), strNew), "", "")
                            End If
                        Next
                        Response.Redirect("ConfirmResetPass.aspx")
                    Else
                        txtMessage.Text = "Chưa có tài khoản BTS. Liên hệ IT để hỗ trợ"
                    End If
                End Using

                'Tạo message confirm

            Else
                If txtUserName.Text = "" Then
                    txtMessage.Text = "Please input your Window User"
                Else
                    txtMessage.Text = "Incorrect Window User or Password!"
                End If
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Login.aspx")
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub


    Public Sub CheckConnect()
        If Not SqlDataProvider.ConnectDB() Then
            txtMessage.Text = "Fail to connect to the database! Please contact with administrator!"
        End If
    End Sub

    Private Function ValidateActiveDirectoryLogin(ByVal Username As String, ByVal Password As String) As Boolean
        Dim Success As Boolean = False
        Dim Entry As New System.DirectoryServices.DirectoryEntry("LDAP://" & "toyotavn.com.vn", Username, Password)
        Dim Searcher As New System.DirectoryServices.DirectorySearcher(Entry)
        Searcher.SearchScope = DirectoryServices.SearchScope.OneLevel
        Try
            Dim Results As System.DirectoryServices.SearchResult = Searcher.FindOne
            Success = Not (Results Is Nothing)
        Catch
            Success = False
        End Try
        Return Success
    End Function

    Private Function getNewPass() As String
        Dim s As String = "@#$%^&!AaBbCbDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789"
        Dim r As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To 8
            Dim idx As Integer = r.Next(0, 35)
            sb.Append(s.Substring(idx, 1))
        Next
        Return sb.ToString()

    End Function
End Class