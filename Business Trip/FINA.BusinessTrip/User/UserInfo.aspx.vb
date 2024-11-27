Imports Provider
Imports System.Data

Partial Public Class UserInfo
    Inherits System.Web.UI.Page

    Dim userName As String = String.Empty
    Dim fullName As String = String.Empty
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CommonFunction.CheckRole(RoleType.Administrator)
        CommonFunction.CheckSessionMessage(Me)
        LoadGrid()
    End Sub

    'Protected Sub btnSearch_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
    '    txtBudgetCode.Text = "Code"
    '    LoadGrid()
    'End Sub

    Protected Sub LoadGrid()
        Dim dtUser As DataTable
        dtUser = UserProvider.tbl_UserGetAll()
        grvUserInfo.DataSource = dtUser
        grvUserInfo.DataBind()
    End Sub

    'Protected Sub grvUserInfo_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grvUserInfo.RowDataBound
    '    'If e.Row.Cells.Count > 3 AndAlso e.Row.Cells(6).Text.ToLower() = "no" Then
    '    '    e.Row.CssClass = "user-locked"
    '    'End If
    'End Sub

End Class