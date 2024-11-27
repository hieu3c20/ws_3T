Imports Provider
Imports System.Data

Partial Public Class ChangePass
    Inherits System.Web.UI.Page

    Dim userName As String = String.Empty
    Dim fullName As String = String.Empty
    Dim strUserid As String = String.Empty
    Dim checkFirst As Boolean = False
    Dim _redirect As String = String.Empty

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Stop Caching in IE
        Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache)
        'Stop Caching in Firefox
        Response.Cache.SetNoStore()
        '
        HttpContext.Current.Session("HideExpireWarning") = "Y"
        '
        If Request.QueryString("uid") Is Nothing Then
            strUserid = ""
            userName = Session("UserName")
            fullName = Session("FullName")
            txtUserName.Text = userName
            txtFullName.Text = fullName
            txtMenu.Text = "Change Password"
            trFirstChange.Visible = False
            FillData(userName)
            _redirect = "~/User/UserProfile.aspx"
        Else
            txtMenu.Text = "Reset Password"
            trOldPass.Visible = False
            strUserid = Request.QueryString("uid").ToString()
            'Fill data   
            FillData(strUserid)
            _redirect = "" '"~/User/UserInfo.aspx"
        End If


        If Not IsPostBack Then

        Else
            txtOldPassword.Attributes("value") = txtOldPassword.Text
            txtPassWord.Attributes("value") = txtPassWord.Text
            txtRePassword.Attributes("value") = txtRePassword.Text
        End If

    End Sub

    Private Sub FillData(ByVal userName As String)
        Dim objUser As tbl_UsersInfo
        objUser = UserProvider.tbl_User_GetUserInfo_ByUserName(userName)
        If objUser IsNot Nothing Then
            txtUserName.Text = objUser.UserName
            txtFullName.Text = objUser.FullName
            txtDivision.Text = objUser.DivisionName
            txtBranch.Text = objUser.BranchName
            txtDepartment.Text = objUser.DepartmentName
            txtEmail.Text = objUser.TMVEmail
            userName = objUser.UserName
        End If
    End Sub

    Protected Sub btnChangePass_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChangePass.Click

        Try
            CommonFunction.SetPostBackStatus(btnChangePass)
            If strUserid = "" Then
                If Not ValidOldPassword() Then
                    txtMesOldPass.Text = "Current Password is invalid!"
                    Exit Sub
                Else
                    txtMesOldPass.Text = String.Empty
                End If
            Else
                checkFirst = chkChangePass.Checked
            End If

            If Request.QueryString("uid") Is Nothing Then
                If txtPassWord.Text.Trim = txtOldPassword.Text.Trim Then
                    txtMesPass.Text = "New password cannot be the same as the Old password"
                    Exit Sub
                Else
                    txtMesPass.Text = String.Empty
                End If
            End If

            If txtPassWord.Text.Trim = userName Then
                txtMesPass.Text = "Password cannot be the same as the User ID"
                Exit Sub
            Else
                txtMesPass.Text = String.Empty
            End If

            If txtPassWord.Text.Trim = String.Empty Then
                txtMesPass.Text = "Please enter New Password before change password!"
                Exit Sub
            Else
                txtMesPass.Text = String.Empty
            End If

            Dim sPattern As String = "\d"
            Dim oReg As Regex = New Regex(sPattern, RegexOptions.IgnoreCase)
            If oReg.IsMatch(txtPassWord.Text.Trim) = False Then
                txtMesPass.Text = "Password should contain mimimum one numeric character. Ex: 1,2 ,3 ..."
                Exit Sub
            Else
                txtMesPass.Text = String.Empty
            End If

            Dim sPatternNonAlphabet As String = "[a-zA-Z]"
            Dim oRegNonAlphabet As Regex = New Regex(sPatternNonAlphabet, RegexOptions.IgnoreCase)
            If oRegNonAlphabet.IsMatch(txtPassWord.Text.Trim) = False Then
                txtMesPass.Text = "Password should contain mimimum one non-alphanumeric character. Ex: a, b,c, A, B, C ..."
                Exit Sub
            Else
                txtMesPass.Text = String.Empty
            End If

            Dim sPatternAlphabet As String = "[^a-zA-Z_0-9]"
            Dim oRegAlphabet As Regex = New Regex(sPatternAlphabet, RegexOptions.IgnoreCase)
            If oRegAlphabet.IsMatch(txtPassWord.Text.Trim) = False Then
                txtMesPass.Text = "Password should contain mimimum one alphabet character. Ex:@, #,$,%..."
                Exit Sub
            Else
                txtMesPass.Text = String.Empty
            End If

            If txtPassWord.Text.Trim <> txtRePassword.Text.Trim Then
                txtMesPass.Text = "Password and Retype Password must the same value!"
                Exit Sub
            Else
                txtMesPass.Text = String.Empty
            End If

            'Update PassWord
            Dim strNewPass = CommonFunction.EncryptPassword(txtPassWord.Text.Trim)
            If strUserid = "" Then
                strUserid = Session("UserName")
            End If
            UserProvider.tbl_UserChangePass(strUserid, strNewPass, checkFirst)
            If _redirect <> "" Then
                'Thong bao thanh cong
                CommonFunction.SetSessionMessage("Password was updated successful!")
                Response.Redirect(_redirect)
            Else
                CommonFunction.ShowInfoMessage(panMessage, "Password was updated successful!")
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub


    Private Function ValidOldPassword() As Boolean
        Dim strOldPass = CommonFunction.EncryptPassword(txtOldPassword.Text.Trim)
        Dim sType As Boolean = UserProvider.CheckOldPassword(userName, strOldPass)
        Return sType
    End Function

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        If strUserid = "" Then
            Response.Redirect("UserProfile.aspx")
        Else
            Response.Redirect("UserInfo.aspx")
        End If

    End Sub
End Class