Imports Provider
Imports System.Data

Partial Public Class Login
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
            dtUser = UserProvider.tbl_Users_Login(txtUserName.Text.ToString())
            If dtUser.Rows.Count > 0 Then
                If txtPassword.Text.Trim = "" Then
                    txtMessage.Text = "Please enter password before login!"
                    Exit Sub
                Else
                    txtMessage.Text = String.Empty
                End If
                'Kiem tra co bi lock khong
                If Convert.ToBoolean(dtUser.Rows(0)("IsLocked").ToString()) Then
                    txtMessage.Text = "Account was Locked. Please contact System Administrator!"
                Else
                    'Neu khong bi lock
                    strPassWord = CommonFunction.EncryptPassword(txtPassword.Text.Trim)
                    If dtUser.Rows(0)("Password").ToString() = strPassWord Then
                        'Kiem tra ngay expriredDate
                        If Convert.ToDateTime(dtUser.Rows(0)("ExpriedDate").ToString()) < Date.Now Then
                            txtMessage.Text = "Your password was expired. Please contact System Administrator!"
                        Else
                            Session("UserName") = dtUser.Rows(0)("UserName").ToString()
                            Session("FullName") = dtUser.Rows(0)("FullName").ToString()
                            Session("RoleType") = dtUser.Rows(0)("RoleTypeName").ToString()
                            '
                            Session("Department") = dtUser.Rows(0)("DepartmentName").ToString()
                            Session("Division") = dtUser.Rows(0)("DivisionName").ToString()
                            Session("Branch") = dtUser.Rows(0)("BranchName").ToString()                                                        
                            'Update so lan tang bi fail (Disable After Fail = 0)
                            UserProvider.Update_DisableAfterLoginFail(txtUserName.Text.ToString(), True)
                            Response.Redirect(_redirect)
                        End If
                    Else
                        'tang so lan login fail len (=Disable After Fail+1)
                        UserProvider.Update_DisableAfterLoginFail(txtUserName.Text.ToString(), False)
                        Dim solan As String = UserProvider.tbl_Users_Login(txtUserName.Text.ToString()).Rows(0)("DisableAfterFailed").ToString()
                        Dim solanconlai As Integer = 5 - Convert.ToInt16(solan)
                        txtMessage.Text = "Invalid Password! You have " & solanconlai.ToString() & " consecutive failed login."
                    End If
                End If
            Else
                txtMessage.Text = "Invalid Username!"
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Public Sub CheckConnect()
        If Not SqlDataProvider.ConnectDB() Then
            txtMessage.Text = "Fail to connect to the database! Please contact with administrator!"
        End If
    End Sub

End Class