
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Security.Cryptography
Imports System.Text

Namespace Provider
    Public Class UserProvider

#Region "OBJECT INFO"

        'Public Shared Function tbl_UsersGet(ByVal _Id As Object) As tbl_UsersInfo
        '    Return CBO.FillObject(SqlHelper.ExecuteReader(New Connections().SqlConn, "tbl_UsersGet", _Id), GetType(tbl_UsersInfo))
        'End Function

        'Public Shared Function tbl_Users_Insert(ByVal objtbl_Users As tbl_UsersInfo) As Integer
        '    Dim sp As New SqlDataProvider()
        '    Return SqlHelper.ExecuteScalar(New Connections().SqlConn, "tbl_Users_Insert", sp.GetNull(objtbl_Users.UserName), sp.GetNull(objtbl_Users.Password), sp.GetNull(objtbl_Users.isStatus), sp.GetNull(objtbl_Users.CreateDate))
        'End Function

        'Public Shared Sub tbl_Users_Update(ByVal objtbl_Users As tbl_UsersInfo)
        '    Dim sp As New SqlDataProvider()
        '    SqlHelper.ExecuteNonQuery(New Connections().SqlConn, "tbl_Users_Update", objtbl_Users.ID, sp.GetNull(objtbl_Users.UserName), sp.GetNull(objtbl_Users.Password), sp.GetNull(objtbl_Users.isStatus), sp.GetNull(objtbl_Users.CreateDate))
        'End Sub
#End Region

#Region "DATA TABLE"

        ' Login
        Public Shared Function tbl_Users_Login(ByVal User As String) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_Users_Login", User).Tables(0)
        End Function

        Public Shared Function tbl_Users_Login_ByEmail(ByVal Email As String) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_Users_Login_ByEmail", Email).Tables(0)
        End Function

        Public Shared Function tbl_Users_CheckExpireDate(ByVal User As String) As Integer
            Return CommonFunction._ToString(SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_User_CheckExpireDate", User).Tables(0).Rows(0)("RemainingDate"))
        End Function

        Public Shared Function tbl_Users_Login_CheckLock(ByVal User As String) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_Users_Login_CheckLock", User).Tables(0)
        End Function

        Public Shared Sub Update_DisableAfterLoginFail(ByVal User As String, ByVal Status As Boolean)
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, "tbl_Users_DisableAfterFail", User, Status)
        End Sub

        Public Shared Function tbl_User_GetAllByUserName(ByVal User As String) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_User_GetAllByUserName", User).Tables(0)
        End Function

        Public Shared Function isExpiredPassword(ByVal User As String) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_User_isExpiredPass", User).Tables(0)
        End Function

        Public Shared Function CheckOldPassword(ByVal UserName As String, ByVal Passsword As String) As Boolean
            Dim ds As DataSet
            ds = SqlHelper.ExecuteDataset(New Connections().SqlConn, "checkOldPassWord", UserName, Passsword)
            Return CType(ds.Tables(0).Rows(0).Item(0).ToString(), Boolean)
        End Function

        Public Shared Sub tbl_UserChangePass(ByVal UserName As String, ByVal Password As String, ByVal checkFirst As Boolean)
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, "tbl_Users_ChangePass_Udp", UserName, Password, checkFirst)
        End Sub

        'User 
        Public Shared Function tbl_User_GetUserInfo(ByVal UserName As String) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_User_GetInfo", UserName, Date.Now()).Tables(0)
        End Function

        Public Shared Function tbl_User_GetAuthorizedAccounts(ByVal UserName As String) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_User_GetAuthorizedAccounts", UserName).Tables(0)
        End Function

        Public Shared Function tbl_User_GetAuthorizedForAirTicket(ByVal UserName As String) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_User_GetAuthorizedForAirTicket", UserName).Tables(0)
        End Function

        Public Shared Function tbl_User_GetAuthorizedForWifiDevice(ByVal UserName As String) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_User_GetAuthorizedForWifiDevice", UserName).Tables(0)
        End Function

        Public Shared Function tbl_BT_User_ForAirTicketRequest() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_BT_User_ForAirTicketRequest").Tables(0)
        End Function

        Public Shared Function tbl_User_IsAuthorizedAccount(ByVal UserName As String) As Boolean
            Dim dtAuthorizedAccounts As DataTable = UserProvider.tbl_User_GetAuthorizedAccounts(CommonFunction._ToString(System.Web.HttpContext.Current.Session("UserName")))
            dtAuthorizedAccounts.PrimaryKey = New DataColumn() {dtAuthorizedAccounts.Columns("UserName")}
            Return dtAuthorizedAccounts.Rows.Find(UserName) IsNot Nothing
        End Function

        Public Shared Function tbl_User_GetUserInfo_ByUserID(ByVal userId As Integer) As tbl_UsersInfo
            Return SqlDataProvider.FillObject(SqlHelper.ExecuteReader(New Connections().SqlConn, "tbl_User_GetInfoByUserID", userId, Date.Now), GetType(tbl_UsersInfo))
        End Function

        Public Shared Function tbl_User_GetUserInfo_ByUserIDtb(ByVal userId As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_User_GetInfoByUserID", userId, Date.Now()).Tables(0)
        End Function

        Public Shared Function tbl_User_GetManagerByRole(ByVal RoleType As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_User_GetManagerByRole", RoleType).Tables(0)
        End Function

        Public Shared Function tbl_User_GetTimeKeeperByRole(ByVal RoleType As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_User_GetTimeKeeperByRole", RoleType).Tables(0)
        End Function

        Public Shared Function tbl_User_GetTimeKeeperByRoleType(ByVal RoleType As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_User_GetTimeKeeperByRole", RoleType).Tables(0)
        End Function

        Public Shared Function tbl_UserInsert(ByVal objUser As tbl_UsersInfo) As Integer
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, "tbl_User_Insert", _
                        CommonFunction.CheckNothing(objUser.UserName), CommonFunction.CheckNothing(objUser.Password), _
                        CommonFunction.CheckNothing(objUser.FullName), CommonFunction.CheckNothing(objUser.IsLocked), _
                        CommonFunction.CheckNothing(objUser.UpdateUserID), _
                        CommonFunction.CheckNothing(objUser.PasswordHistory), _
                        CommonFunction.CheckNothing(objUser.IsNextLogon), CommonFunction.CheckNothing(objUser.IsChangePassword), _
                        CommonFunction.CheckNothing(objUser.PasswordChangeAfter), CommonFunction.CheckNothing(objUser.DisableAfterFailed), _
                        CommonFunction.CheckNothing(objUser.SendEmail), CommonFunction.CheckNothing(objUser.NoPasswordHistory), _
                        CommonFunction.CheckNothing(objUser.NoFailLogin), _
                        CommonFunction.CheckNothing(objUser.Role_Type), CommonFunction.CheckNothing(objUser.Role_Level), _
                        CommonFunction.CheckNothing(objUser.Manager), CommonFunction.CheckNothing(objUser.TimeKeeper), _
                        CommonFunction.CheckNothing(objUser.BranchGroup), CommonFunction.CheckNothing(objUser.IsCreditCard), _
                        CommonFunction.CheckNothing(objUser.UserIDMapOra), _
                        CommonFunction.CheckNothing(objUser.UserNameMapOra))

        End Function


        Public Shared Sub tbl_UserUpdate(ByVal objUser As tbl_UsersInfo)
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, "tbl_User_Udp", _
                        CommonFunction.CheckNothing(objUser.UserName), CommonFunction.CheckNothing(objUser.Password), _
                        CommonFunction.CheckNothing(objUser.FullName), CommonFunction.CheckNothing(objUser.IsLocked), _
                        CommonFunction.CheckNothing(objUser.UpdateUserID), _
                        CommonFunction.CheckNothing(objUser.PasswordHistory), _
                        CommonFunction.CheckNothing(objUser.IsNextLogon), CommonFunction.CheckNothing(objUser.IsChangePassword), _
                        CommonFunction.CheckNothing(objUser.PasswordChangeAfter), CommonFunction.CheckNothing(objUser.DisableAfterFailed), _
                        CommonFunction.CheckNothing(objUser.SendEmail), CommonFunction.CheckNothing(objUser.NoPasswordHistory), _
                        CommonFunction.CheckNothing(objUser.NoFailLogin), _
                        CommonFunction.CheckNothing(objUser.Role_Type), CommonFunction.CheckNothing(objUser.Role_Level), _
                        CommonFunction.CheckNothing(objUser.Manager), CommonFunction.CheckNothing(objUser.TimeKeeper), _
                        CommonFunction.CheckNothing(objUser.BranchGroup), CommonFunction.CheckNothing(objUser.IsCreditCard), _
                        CommonFunction.CheckNothing(objUser.UserIDMapOra), _
                        CommonFunction.CheckNothing(objUser.UserNameMapOra))
        End Sub

        Public Shared Function tbl_UserGetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_User_GetAll").Tables(0)
        End Function

        Public Shared Function GetEmailInfoByBT(ByVal btID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_User_GetEmailInfoByBT", _
                                            sp.GetNull(btID)).Tables(0)
        End Function

        Public Shared Function GetEmailInfoByWifiDevice(ByVal ID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_BT_User_GetEmailInfoByWifiDevice", _
                                            sp.GetNull(ID)).Tables(0)
        End Function

        Public Shared Function tbl_User_GetUserInfo_ByUserName(ByVal userName As String) As tbl_UsersInfo
            Return SqlDataProvider.FillObject(SqlHelper.ExecuteReader(New Connections().SqlConn, "tbl_User_GetInfoByUserNamePicture", userName), GetType(tbl_UsersInfo))
        End Function

        Public Shared Function tbl_getBranchByID(ByVal BranchID As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, "tbl_Split_GetBranchNameByID", _
                                           sp.GetNull(BranchID)))
        End Function

        Public Shared Function CheckOraMapping(ByVal username As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                            "tbl_BT_User_CheckOraMapping", _
                                            sp.GetNull(username)))
        End Function

        Public Shared Function tbl_getEmployeeByBranch(ByVal BranchName As String) As String
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, "tbl_Get")
        End Function

        Public Shared Function CheckNothing(ByVal Value As Object, Optional ByVal isDateType As Boolean = False) As Object
            If Not isDateType Then
                If Value Is Nothing Then Return DBNull.Value
            Else
                If Value = Nothing Then Return DBNull.Value
            End If
            Return Value
        End Function


        Public Function GetBGTypeByID(ByVal BGTypeID As Decimal) As tbl_UsersInfo
            Return SqlDataProvider.FillObject(SqlHelper.ExecuteReader(New Connections().SqlConn, 1), GetType(tbl_UsersInfo))
        End Function

        Public Shared Function GetAllRole()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "Role_Type_GetAll").Tables(0)
        End Function

        Public Shared Function GetEmployeeByRole(ByVal RoleID As Integer)
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "GetEmployeeBy_RoleType", RoleID).Tables(0)
        End Function

        Public Shared Function GetEmployeeByRole1()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "GetEmployeeBy_RoleType1").Tables(0)
        End Function

        Public Shared Function GetEmployeeOracToMap(ByVal UserName As String, ByVal Des As String) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "GetUserFromOracToMapID", _
                                            sp.GetNull(UserName), sp.GetNull(Des)).Tables(0)
        End Function

        Public Shared Function GetActive() As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_User_GetActive").Tables(0)
        End Function

        Public Shared Function UserTestEmail_Get(ByVal btID As Integer, ByVal role As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "UserTestEmail_Get", _
                                           sp.GetNull(btID), _
                                           sp.GetNull(role)))
        End Function

        Public Shared Function UserTestEmail_Get(ByVal branch As String, ByVal role As String) As String
            Dim sp As New SqlDataProvider()
            Return CommonFunction._ToString(SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "UserTestEmail_GetByBranch", _
                                           sp.GetNull(branch), _
                                           sp.GetNull(role)))
        End Function

        Public Shared Function User_GetAllEmployees() As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                           "tbl_User_GetAllEmployees").Tables(0)
        End Function

        Public Shared Function GetAccountEmails() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_User_GetAccountEmails").Tables(0)
        End Function
#End Region

    End Class
End Namespace

