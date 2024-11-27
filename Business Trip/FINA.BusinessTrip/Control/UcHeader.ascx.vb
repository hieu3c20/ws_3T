Imports Provider
Partial Public Class UcHeader
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblFullName.Text = Session("FullName")
        lblUserName.Text = Session("UserName")
        liAccountMenu.Visible = False
        masterMenu.Visible = False
        Dim role As String = CommonFunction._ToString(Session("RoleType"))
        If role.ToLower() = RoleType.Administrator.ToString().ToLower() Then
            liAccountMenu.Visible = True
        End If
        'OrElse role.ToLower() = RoleType.GA.ToString().ToLower() _
        'OrElse role.ToLower() = RoleType.Top_GA.ToString().ToLower() _
        If role.ToLower() = RoleType.Administrator.ToString().ToLower() _
            OrElse role.ToLower() = RoleType.Finance.ToString().ToLower() _
            OrElse role.ToLower() = RoleType.Finance_GA.ToString().ToLower() _
            OrElse role.ToLower() = RoleType.Finance_Budget.ToString().ToLower() Then
            masterMenu.Visible = True
        End If
    End Sub

End Class