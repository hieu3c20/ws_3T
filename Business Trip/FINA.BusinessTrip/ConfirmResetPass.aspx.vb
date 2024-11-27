Imports Provider
Imports System.Data
Imports System.DirectoryServices
Imports System.Net.Mail

Partial Public Class ConfirmResetPass
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

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Try
            Response.Redirect("Login.aspx")
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

End Class