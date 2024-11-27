 
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace Provider
    Partial Public Class SqlDataProvider

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

        'Public Shared Function tbl_Users_Login(ByVal User As String, ByVal Pass As String) As DataTable
        '    Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_Users_Login", User, Pass).Tables(0)
        'End Function

        'Public Shared Function tbl_Users_Get(ByVal iD As Object) As DataRow
        '    Dim dt As DataTable = SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_Users_Get", iD).Tables(0)
        '    If dt.Rows.Count <= 0 Then
        '        Return Nothing
        '    End If
        '    Return dt.Rows(0)
        'End Function

        'Public Shared Function tbl_Users_Gets(ByVal iD As String) As DataTable
        '    Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_Users_Gets", iD).Tables(0)
        'End Function

        'Public Shared Sub tbl_Users_Delete(ByVal iD As String)
        '    SqlHelper.ExecuteNonQuery(New Connections().SqlConn, "tbl_Users_Delete", iD)
        'End Sub

        'Public Shared Sub tbl_Analytics_ActiveViewCount()
        '    SqlHelper.ExecuteNonQuery(New Connections().SqlConn, "tbl_Analytics_ActiveViewCount")
        'End Sub

        'Public Shared Function tbl_Users_Search( _
        ' ByVal PageSize As Integer _
        ' , ByVal PageIndex As Integer) As DataTable
        '    Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_Users_Search", _
        '     PageSize _
        '     , PageIndex).Tables(0)
        'End Function
#End Region

    End Class
End Namespace

