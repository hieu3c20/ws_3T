Imports Provider
Imports System.Data
Imports System.Drawing
Imports System.IO

Partial Public Class UserProfile
    Inherits System.Web.UI.Page

    Dim strUserName As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Stop Caching in IE
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache)
        'Stop Caching in Firefox
        Response.Cache.SetNoStore()
        '
        Try
            CommonFunction.CheckSessionMessage(Me)
            strUserName = Session("UserName")
            Dim objUser As New tbl_UsersInfo()
            objUser = UserProvider.tbl_User_GetUserInfo_ByUserName(strUserName)
            If objUser IsNot Nothing Then
                txtUserName.Text = objUser.UserName
                txtFullName.Text = objUser.FullName
                txtBranch.Text = objUser.BranchName
                txtDivision.Text = objUser.DivisionName
                txtDepartment.Text = objUser.DepartmentName
                txtEmailAddress.Text = objUser.TMVEmail
                txtBirthdate.Text = If(objUser.Birthdate = DateTime.MinValue, "xxx", objUser.Birthdate.ToString("dd-MMM-yyyy"))
                txtGender.Text = objUser.SexName
                txtJobband.Text = objUser.JobBand
                txtExt.Text = objUser.Ext
                txtMobile.Text = objUser.Mobile
                imgEmployee.ImageUrl = "~/imagehandle.ashx"
            Else
                CommonFunction.ShowStartupErrorMessage(Me, "User's not found! Please contact system administrator!")
            End If
        Catch ex As Exception
            CommonFunction.ShowStartupErrorMessage(Me, ex.Message)
        End Try
    End Sub

End Class