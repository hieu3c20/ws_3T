
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Security.Cryptography
Imports System.Text

Namespace Provider
    Public Class mTimeKeeperProvider

        Public Shared Function GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_TimeKeeperAll").Tables(0)
        End Function

        Public Shared Function m_TimeKeeper_GetByCode(ByVal Code As String) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_TimeKeeperByCode", sp.GetNull(Code)).Tables(0)
        End Function

        Public Shared Function m_TimeKeeper_ManageByCode(ByVal Code As String) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_TimeKeeperManageByCode", sp.GetNull(Code)).Tables(0)
        End Function

        Public Shared Function m_TimeKeeper_Choose(ByVal Code As String, ByVal Type As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "ChooseTimeKeeperByCode", sp.GetNull(Code), sp.GetNull(Type)).Tables(0)
        End Function

        Public Shared Function SearchEmployeeCode(ByVal Code As String, ByVal name As String, ByVal DivisionID As Integer, ByVal DepartmentID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "SearchEmployee", sp.GetNull(Code), sp.GetNull(name), sp.GetNull(DivisionID), sp.GetNull(DepartmentID)).Tables(0)
        End Function

        Public Shared Function tbl_TimeKeeper_Insert(ByVal objKeeper As mTimeKeeperInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "tbl_TimeKeeperInsert", _
                                           sp.GetNull(objKeeper.TimeKeeperCode), _
                                           sp.GetNull(objKeeper.EmployeeCode), _
                                           sp.GetNull(objKeeper.EmployeeName), _
                                           sp.GetNull(objKeeper.Note))
        End Function



        Public Shared Sub tbl_TimeKeeper_Delete(ByVal objKeeper As mTimeKeeperInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "tbl_TimeKeeperDelete", _
                                           sp.GetNull(objKeeper.TimeKeeperCode), _
                                           sp.GetNull(objKeeper.EmployeeCode))
        End Sub


    End Class
End Namespace

