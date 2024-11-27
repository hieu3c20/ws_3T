Imports Provider
Imports System.Data
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxGridView
Imports System.Collections
Imports DevExpress.Web.ASPxEditors

Partial Public Class CreateUser
    Inherits System.Web.UI.Page

    Dim userName As String = String.Empty
    Dim fullName As String = String.Empty
    Dim strUserid As String = String.Empty
    Dim isInsert As Boolean
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CommonFunction.CheckRole(RoleType.Administrator)
        CheckConnect()
        CommonFunction.CheckSessionMessage(Me)
        If Request.QueryString("uid") Is Nothing Then
            hChangePass.Visible = True
            strUserid = ""
        Else
            strUserid = Request.QueryString("uid").ToString()
            'btnGetUserInfo.Visible = False
            'btnGetUserInfo.Text = ""
            txtUserName.ReadOnly = True

            'btnCreateUser.Visible = False
            btnSearchEmpInfo.Visible = False
            hide_pass.Visible = False
            hChangePass.Visible = False
            btnGetUserInfo.Text = "ReLoad"
            loadText.InnerText = "Reload"
        End If
        If Not IsPostBack Then
            InitDropdownlist()
            InitDivision()
            If strUserid = "" Then
                'ddlRoleLevel.SelectedIndex = 0
                'GetAllEmployeeInfo()
            Else
                GetAllEmployeeInfo()
                'btnGetUserInfo.Text = "ReLoad"
            End If
        Else
            txtPassWord.Attributes("value") = txtPassWord.Text
            txtRePassword.Attributes("value") = txtRePassword.Text
        End If
        InitddlBranch()
        'LoadGridChooseEmployee("121401", 3)
    End Sub

    Public Sub GetAllEmployeeInfo()
        Try
            Dim strBranch As String = String.Empty
            Dim strBrConvert As String = String.Empty
            Dim objUser As tbl_UsersInfo
            objUser = UserProvider.tbl_User_GetUserInfo_ByUserName(strUserid)

            If objUser IsNot Nothing Then
                ' hBranch.Value = 

                LoadGridTimeKeeper(objUser.UserName)
                LoadGridChoose(objUser.UserName, 3)
                ' LoadGridChooseEmployee("121401", 3)
                txtUserName.Text = objUser.UserName
                txtFullName.Text = objUser.FullName
                txtDivision.Text = objUser.DivisionName
                txtDepartment.Text = objUser.DepartmentName
                txtSection.Text = objUser.SectionName
                txtEmailAddress.Text = objUser.TMVEmail
                txtTitle.Text = objUser.JobBand
                htxtUserName.Value = objUser.UserName
                txtMapUserID.Text = If(objUser.UserIDMapOra <= 0, "", objUser.UserIDMapOra.ToString())
                txtMapUserNameOra.Text = If(objUser.UserIDMapOra <= 0, "", objUser.UserNameMapOra.ToString())
                If objUser.Role_Type <> 0 Then
                    ddlRoleType.Items.FindByValue(objUser.Role_Type).Selected = True
                End If
                'InitddlManager()
                If objUser.TimeKeeper <> "0" Then
                    'ddlTimeKeeper.Items.FindByValue(objUser.TimeKeeper).Selected = True
                End If

                If objUser.Manager <> "0" Then
                    'ddlManager.Items.FindByValue(objUser.Manager).Selected = True
                End If

                If objUser.IsLocked = 0 Then
                    chkAccount.Checked = False
                Else
                    chkAccount.Checked = True
                End If

                If objUser.IsCreditCard = 0 Then
                    chkIsCreditCard.Checked = False
                Else
                    chkIsCreditCard.Checked = True
                End If

                If objUser.Role_Level <> "0" Then
                    'ddlRoleLevel.SelectedIndex = 0
                End If

                If objUser.Role_Type <> 1 Then
                    If objUser.BranchGroup <> "" Then
                        strBrConvert = UserProvider.tbl_getBranchByID(objUser.BranchGroup)
                        ddlBranch.Text = strBrConvert
                        hBranch.Value = objUser.BranchGroup
                    End If
                End If
            End If

        Catch ex As Exception
            CommonFunction.ShowStartupErrorMessage(Me, ex.Message)
        End Try
    End Sub

    Public Sub LoadGridTimeKeeper(ByVal EmployeeCode As String)
        Try
            Dim dtTimeKeeper As DataTable = mTimeKeeperProvider.m_TimeKeeper_GetByCode(EmployeeCode)
            grvTimeSheet.DataSource = dtTimeKeeper
            grvTimeSheet.DataBind()
            'For i = 0 To grvTimeSheet.VisibleRowCount - 1
            '    Dim chk As CheckBox = CType(grvTimeSheet.FindRowCellTemplateControl(i, grvTimeSheet.Columns(0), "chkSelect"), CheckBox)
            '    chk.Checked = True
            'Next
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub grvChoose_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvChoose.BeforeGetCallbackResult
        'LoadGridChoose(htxtUserName.Value, 3)
        LoadGridChoose(htxtUserName.Value, rdlFilterEmp.SelectedIndex + 1)
    End Sub

    Protected Sub grvChooseEmployee_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvChooseEmployee.BeforeGetCallbackResult
        btnSearchEmp_Click(Nothing, Nothing)
    End Sub

    Protected Sub grvKeeper_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvTimeSheet.BeforeGetCallbackResult
        LoadGridTimeKeeper(htxtUserName.Value)
    End Sub

    Protected Sub grvMapUser_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvMapUser.BeforeGetCallbackResult
        btnSearchEmployeeOra_Click(Nothing, Nothing)
    End Sub

    Public Sub LoadGridChoose(ByVal EmployeeCode As String, ByVal Type As Integer)
        Try
            Dim dtChoose As DataTable = mTimeKeeperProvider.m_TimeKeeper_Choose(EmployeeCode, Type)
            grvChoose.DataSource = dtChoose
            grvChoose.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub LoadGridChooseEmployee(ByVal EmployeeCode As String, ByVal Type As Integer)
        Try
            Dim dtChooseEmp As DataTable = mTimeKeeperProvider.m_TimeKeeper_Choose(EmployeeCode, Type)
            grvChooseEmployee.DataSource = dtChooseEmp
            grvChooseEmployee.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub InitDropdownlist()
        Try
            Dim dtRoleLevel As DataTable
            dtRoleLevel = RoleLevelProvider.Role_Level_GetAll()
            'ddlRoleLevel.DataSource = dtRoleLevel
            'ddlRoleLevel.DataValueField = "Role_Level_ID"
            'ddlRoleLevel.DataTextField = "LevelName"
            'ddlRoleLevel.DataBind()
            Dim dtRoleType As DataTable
            dtRoleType = RoleTypeProvider.Role_Type_GetAll()
            ddlRoleType.DataSource = dtRoleType
            ddlRoleType.DataValueField = "RoleTypeID"
            ddlRoleType.DataTextField = "RoleTypeName"
            ddlRoleType.DataBind()
        Catch ex As Exception
        End Try
    End Sub

    Public Sub InitddlBranch()
        Try
            Dim dtBranch As DataTable = BusinessTripProvider.tbl_BranchGetAll()
            Dim lb As ASPxListBox = (ddlBranch.FindControl("ddlBranch"))
            lb.DataSource = dtBranch
            lb.ValueField = "BranchID"
            lb.TextField = "BranchName"
            lb.DataBind()
            lb.Items.Insert(0, New ListEditItem("Tất cả", "0"))
            '
            Dim strBrConvert As String = CommonFunction._ToString(UserProvider.tbl_getBranchByID(hBranch.Value))
            ddlBranch.Text = strBrConvert
        Catch ex As Exception
            CommonFunction.ShowStartupErrorMessage(Me, ex.Message)
        End Try
    End Sub

#Region "Event Button"
    'Protected Sub btnCancel1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
    '    txtMesPass.Text = String.Empty
    '    txtMesRePass.Text = String.Empty
    '    txtPassWord.Text = String.Empty
    '    txtRePassword.Text = String.Empty
    '    Response.Redirect("BTApprovalRegister.aspx")
    'End Sub

    Protected Sub btnChooseOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChooseOK.Click
        'Dim i As Integer
        Dim insValue As Integer = 0
        Dim checkAll As Boolean = False

        'If btnGetUserInfo.Text = "Load" Then
        '    txtInvalidUser.Text = "You must get User Info!"
        '    userName = ""
        '    Exit Sub

        'Else
        Try
            userName = txtUserName.Text
            Dim employees As String = Request.Params("chkSelectEmployee")
            For Each emp As String In employees.Split(",")
                Dim EmployeeCode As String = emp.Split("/")(0)
                Dim EmployeeName As String = emp.Split("/")(1)
                Dim objTK As New mTimeKeeperInfo
                objTK.TimeKeeperCode = userName
                objTK.EmployeeCode = EmployeeCode
                objTK.EmployeeName = EmployeeName
                objTK.Note = ""
                mTimeKeeperProvider.tbl_TimeKeeper_Insert(objTK)
            Next
            'End If

            LoadGridTimeKeeper(userName)
            LoadGridChoose(userName, rdlFilterEmp.SelectedIndex + 1)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnChooseEmploye_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChooseEmp.Click
        CommonFunction.SetPostBackStatus(btnChooseEmp)
        Try
            CommonFunction.SetPostBackStatus(btnChooseEmp)
            Dim EmployeeCode As String
            EmployeeCode = hchooseEmpID.Value
            If EmployeeCode = "" Then
                CommonFunction.ShowErrorMessage(panMessage, "You must choose at least 1 record(s)")
            Else
                txtUserName.Text = EmployeeCode
                'btnGetUserInfo.Text = "ReLoad"
                'loadText.InnerText = "Reload"
                LoadUserInfo()
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnCreateUser_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreateUser.Click
        Try
            CommonFunction.SetPostBackStatus(btnCreateUser)
            If strUserid <> "" Then
                'truong hop update
                isInsert = False
                Save(isInsert)
            Else
                'If UserProvider.tbl_User_GetAllByUserName(txtUserName.Text).Rows.Count > 0 Then
                '    ' Check User ton tai chua
                '    CommonFunction.ShowErrorMessage(panMessage, "User existed! Please choose another user")
                '    Exit Sub
                'End If


                'truong hop insert moi
                If btnGetUserInfo.Text = "Load" Then
                    txtInvalidUser.Text = "You must get User Info!"
                ElseIf UserProvider.tbl_User_GetAllByUserName(txtUserName.Text).Rows.Count > 0 Then
                    ' Check User ton tai chua
                    CommonFunction.ShowErrorMessage(panMessage, "User existed! Please choose another user")
                    Exit Sub
                Else
                    txtInvalidUser.Text = String.Empty
                End If
                isInsert = True
                Save(isInsert)
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Public Sub Save(ByVal isInsert As Boolean)
        If checkCreateUser() Then
            'Insert New User
            Dim objUser As New tbl_UsersInfo
            objUser.UserName = txtUserName.Text.Trim
            objUser.FullName = txtFullName.Text
            objUser.Password = txtPassWord.Text.Trim
            objUser.Password = CommonFunction.EncryptPassword(objUser.Password)
            objUser.IsLocked = chkAccount.Checked
            If Session("UserName") IsNot Nothing Then
                objUser.UpdateUserID = Session("UserName").ToString()
            End If

            objUser.IsNextLogon = False
            objUser.IsChangePassword = chkChangePass.Checked
            'If ddlRoleLevel.SelectedIndex > 0 Then
            '    objUser.Role_Level = ddlRoleLevel.SelectedValue
            'End If
            objUser.Role_Level = 1
            objUser.Role_Type = ddlRoleType.SelectedValue
            objUser.BranchGroup = hBranch.Value
            objUser.IsCreditCard = chkIsCreditCard.Checked
            objUser.UserIDMapOra = CommonFunction._ToInt(txtMapUserID.Text)
            objUser.UserNameMapOra = txtMapUserNameOra.Text
            If isInsert = True Then
                Dim ID As Integer = UserProvider.tbl_UserInsert(objUser)
                If ID > 0 Then
                    CommonFunction.SetSessionMessage("Create New User Successfull!")
                    Response.Redirect("CreateUser.aspx?uid=" + objUser.UserName)
                End If
            Else
                'Update cho user
                UserProvider.tbl_UserUpdate(objUser)
                CommonFunction.ShowInfoMessage(panMessage, "Update User Successfull!")
            End If
        Else
            Exit Sub
        End If
    End Sub

    Protected Sub btnRemoveOK_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReject.Click
        'Dim i As Integer
        Dim insValue As Integer = 0
        Dim checkAll As Boolean = False

        If btnGetUserInfo.Text = "Load" Then
            txtInvalidUser.Text = "You must get User Info!"
            userName = ""
            Exit Sub
        Else
            userName = txtUserName.Text
            Dim employees As String = Request.Params("chkRemoveEmployee")
            For Each empCode As String In employees.Split(",")
                Dim EmployeeCode As String = empCode
                Dim objTK As New mTimeKeeperInfo
                objTK.TimeKeeperCode = userName
                objTK.EmployeeCode = EmployeeCode
                mTimeKeeperProvider.tbl_TimeKeeper_Delete(objTK)
            Next
        End If

        LoadGridTimeKeeper(userName)
        LoadGridChoose(userName, rdlFilterEmp.SelectedIndex + 1)
    End Sub

    Protected Sub btnSearchEmp_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearchEmployee.Click
        Try
            Dim dtEmployee As DataTable
            Dim DivID As Integer = -1
            DivID = CommonFunction._ToInt(cboDivision.SelectedValue)
            Dim DepID As Integer = -1
            DepID = CommonFunction._ToInt(cboDept.SelectedValue)
            dtEmployee = mTimeKeeperProvider.SearchEmployeeCode(txtEmpCodeSearch.Text.ToString(), txtEmpNameSearch.Text.ToString(), DivID, DepID)
            grvChooseEmployee.DataSource = dtEmployee
            grvChooseEmployee.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    '-------Search Employee in Oracle------
    Protected Sub btnSearchEmployeeOra_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMapUserSearch.Click
        Try
            Dim dtEmpSearch As DataTable
            Dim UserOra As String = Nothing
            Dim UserDes As String = Nothing
            UserOra = txtMapUserName.Text
            UserDes = txtMapUserDescription.Text

            dtEmpSearch = UserProvider.GetEmployeeOracToMap(UserOra, UserDes)
            grvMapUser.DataSource = dtEmpSearch
            grvMapUser.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnClearOraMap_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearOraMap.Click
        txtMapUserID.Text = ""
        txtMapUserNameOra.Text = ""
    End Sub
    '-------Choose Employee in Oracle------
    Protected Sub btnChooseEmpOra_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChooseEmpOra.Click
        CommonFunction.SetPostBackStatus(btnChooseEmpOra)
        Try
            CommonFunction.SetPostBackStatus(btnChooseEmpOra)
            Dim oraUserID As String = hchooseEmpMapOraID.Value
            Dim oraUserName As String = hchooseEmpMapOraName.Value
            If oraUserID = "" Then
                CommonFunction.ShowErrorMessage(panMessage, "You must choose at least 1 record(s)")
            Else
                txtMapUserID.Text = oraUserID
                txtMapUserNameOra.Text = oraUserName
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
        Response.Redirect("UserInfo.aspx")
        'Response.Redirect("BTApprovalRegister.aspx")
    End Sub

    Protected Sub cboDivision_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboDivision.SelectedIndexChanged
        Try
            Dim DivID As Integer = -1
            DivID = CommonFunction._ToInt(cboDivision.SelectedValue)
            InitddlDepartment(DivID)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub InitDivision()
        Dim dtDivision As DataTable
        dtDivision = BusinessTripProvider.tbl_DivisionGetAll()
        CommonFunction.LoadDataToComboBox(cboDivision, dtDivision, "DivisionName", "DivisionID", True, "All", "")
    End Sub

    Private Sub InitddlDepartment(ByVal DivisionID As Integer)
        Dim dtDepartment As DataTable
        dtDepartment = BusinessTripProvider.tbl_DepartmentGetByDivID(DivisionID)
        CommonFunction.LoadDataToComboBox(cboDept, dtDepartment, "DepartmentName", "DepartmentID", True, "All", "")
    End Sub

#End Region

#Region "checkInfo"
    Public Sub CheckConnect()
        If Not SqlDataProvider.ConnectDB() Then
        End If
    End Sub

    Public Function checkCreateUser() As Boolean
        Dim check As Boolean = False
        Dim Pass As Boolean = False

        If strUserid = "" Then
            'Kiem tra password
            If txtPassWord.Text.Trim = String.Empty Then
                txtMesPass.Text = "Please enter New Password before change password!"
                check = True
                Pass = True
            ElseIf txtPassWord.Text.Trim = txtUserName.Text Then
                txtMesPass.Text = "Password cannot be the same as the User ID"
                check = True
                Pass = True
            Else
                txtMesPass.Text = String.Empty
            End If


            Dim sPattern As String = "\d"
            Dim oReg As Regex = New Regex(sPattern, RegexOptions.IgnoreCase)
            If oReg.IsMatch(txtPassWord.Text.Trim) = False And Pass = False Then
                txtMesPass.Text = "Password should contain mimimum one numeric character. Ex: 1,2 ,3 ..."
                check = True
                Pass = True
            ElseIf Pass = False Then
                txtMesPass.Text = String.Empty
            End If

            Dim sPatternNonAlphabet As String = "[a-zA-Z]"
            Dim oRegNonAlphabet As Regex = New Regex(sPatternNonAlphabet, RegexOptions.IgnoreCase)
            If oRegNonAlphabet.IsMatch(txtPassWord.Text.Trim) = False And Pass = False Then
                txtMesPass.Text = "Password should contain mimimum one non-alphanumeric character. Ex: a, b,c, A, B, C ..."
                check = True
                Pass = True
            ElseIf Pass = False Then
                txtMesPass.Text = String.Empty
            End If

            Dim sPatternAlphabet As String = "[^a-zA-Z_0-9]"
            Dim oRegAlphabet As Regex = New Regex(sPatternAlphabet, RegexOptions.IgnoreCase)
            If oRegAlphabet.IsMatch(txtPassWord.Text.Trim) = False And Pass = False Then
                txtMesPass.Text = "Password should contain mimimum one alphabet character. Ex:@, #,$,%..."
                check = True
                Pass = True
            ElseIf Pass = False Then
                txtMesPass.Text = String.Empty
            End If

            If txtPassWord.Text.Trim <> txtRePassword.Text.Trim And Pass = False Then
                txtMesPass.Text = "Password and Retype Password must the same value!"
                check = True
                Pass = True
            ElseIf Pass = False Then
                txtMesPass.Text = String.Empty
            End If
        End If


        'Kiem tra RoleType va LevelType
        If ddlRoleType.SelectedValue Is Nothing OrElse ddlRoleType.SelectedValue = "" Then
            txtMesRoleType.Text = "Please choose Role Type!"
            check = True
        Else
            txtMesRoleType.Text = String.Empty
        End If

        'If ddlTimeKeeper.SelectedIndex = 0 Then
        '    txtMesTimeKeeper.Text = "Please choose TimeKeeper!"
        '    check = True
        'Else
        '    txtMesTimeKeeper.Text = String.Empty
        'End If


        If check = False Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

    Protected Sub LoadUserInfo()
        If btnGetUserInfo.Text = "Load" Then
            userName = txtUserName.Text.Trim.ToString()
            Dim dtUser As New DataTable
            dtUser = UserProvider.tbl_User_GetUserInfo(userName)
            If dtUser.Rows.Count > 0 Then
                htxtUserName.Value = userName
                txtInvalidUser.Text = String.Empty
                txtFullName.Text = dtUser.Rows(0)("EmployeeName").ToString()
                txtDivision.Text = dtUser.Rows(0)("DivisionName").ToString()
                txtDepartment.Text = dtUser.Rows(0)("DepartmentName").ToString()
                txtEmailAddress.Text = IIf(String.IsNullOrEmpty(dtUser.Rows(0)("TMVEmail").ToString()), "No Email", dtUser.Rows(0)("TMVEmail").ToString())
                txtTitle.Text = dtUser.Rows(0)("JobBand").ToString()
                btnGetUserInfo.Text = "Reload"
                loadText.InnerText = "Reload"
                btnCreateUser.Visible = True
                txtUserName.ReadOnly = True
                'txtMapUserID.Text = dtUser.Rows(0)("USER_ID").ToString()
                LoadGridTimeKeeper(userName)
                LoadGridChoose(userName, 3)
            Else
                'Thong bao khong co du lieu phu hop
                txtInvalidUser.Text = "Employee's not found! Please enter correct employee code!"
                btnGetUserInfo.Text = "Load"
                loadText.InnerText = "Load"
                btnCreateUser.Visible = False
                txtUserName.ReadOnly = False
            End If
        Else
            txtUserName.ReadOnly = False
            btnGetUserInfo.Text = "Load"
            loadText.InnerText = "Load"
            btnCreateUser.Visible = False
        End If
    End Sub

    Protected Sub rdlFilterEmp_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rdlFilterEmp.SelectedIndexChanged
        'CommonFunction.SetPostBackStatus(btnGetUserInfo)
        LoadGridChoose(htxtUserName.Value, rdlFilterEmp.SelectedIndex + 1)
        'If  = 0 Then
        '    'CommonFunction.ShowStartupErrorMessage(Me, "a")

        'ElseIf rdlFilterEmp.SelectedIndex = 1 Then
        '    LoadGridChoose(htxtUserName.Value, 2)
        'Else
        '    LoadGridChoose(htxtUserName.Value, 3)
        '    'CommonFunction.ShowStartupErrorMessage(Me, "b")
        'End If
    End Sub
End Class