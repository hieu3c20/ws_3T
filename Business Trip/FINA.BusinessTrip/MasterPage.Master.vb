Imports Provider
Partial Public Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Stop Caching in IE
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache)
        'Stop Caching in Firefox
        Response.Cache.SetNoStore()
        '
        CommonFunction.CheckSession()
        checkFirstLogin()
        '
        CheckExpireDate()        
    End Sub

    Private Sub checkFirstLogin()
        If System.IO.Path.GetFileNameWithoutExtension(Request.Path) <> GetType(ChangePass).Name Then
            Dim dtUser As DataTable
            dtUser = UserProvider.tbl_User_GetAllByUserName(Session("UserName").ToString())
            If dtUser.Rows.Count > 0 Then
                If Convert.ToBoolean(dtUser.Rows(0)("IsChangePassword").ToString()) Then
                    Response.Redirect("~/User/ChangePass.aspx", True)
                End If
            End If
        End If
    End Sub

    Private Sub CheckExpireDate()
        Dim username As String = CommonFunction._ToString(Session("UserName"))
        Dim remaining As Integer = UserProvider.tbl_Users_CheckExpireDate(username)
        If remaining > 0 AndAlso remaining < 10 Then
            If CommonFunction._ToString(Session("HideExpireWarning") = "Y") Then
                Return
            End If
            lblMessage.Text = String.Format(LookupProvider.GetByCodeAndValue("GLOBAL", "expire-warning"), remaining)
            GlobalMessage.Visible = True
        End If
    End Sub

End Class