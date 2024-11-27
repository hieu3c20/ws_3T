
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Security.Cryptography
Imports System.Text

Namespace Provider
    Public Class LookupProvider

        Public Shared Function GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_Lookup_GetAll").Tables(0)
        End Function

        Public Shared Function GetGroupTitle() As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "tbl_Lookup_GetGroup_Title").Tables(0)
        End Function

        Public Shared Function GetByID(ByVal id As Integer) As tbl_LookupInfo
            Return SqlDataProvider.FillObject(SqlHelper.ExecuteReader( _
                                              New Connections().SqlConn, _
                                              "tbl_Lookup_GetByID", id), GetType(tbl_LookupInfo))
        End Function

        Public Shared Function GetByCode(ByVal code As String) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_Lookup_GetByCode", _
                                            sp.GetNull(code)).Tables(0)
        End Function

        Public Shared Function GetByCodeAndType(ByVal code As String, ByVal BTType As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
                                            "tbl_Lookup_GetByCodeAndType", _
                                            sp.GetNull(code), sp.GetNull(BTType)).Tables(0)
        End Function

        Public Shared Function Insert(ByVal obj As tbl_LookupInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "tbl_Lookup_Insert", _
                                           sp.GetNull(obj.Code), _
                                           sp.GetNull(obj.Value), _
                                           sp.GetNull(obj.Text), _
                                           sp.GetNull(obj.DisplayOrder), _
                                           sp.GetNull(obj.Active))
        End Function

        Public Shared Function GetByCodeAndValue(ByVal code As String, ByVal value As String) As String
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "tbl_Lookup_GetByCodeAndValue", _
                                           sp.GetNull(code), _
                                           sp.GetNull(value))
        End Function

        Public Shared Sub Update(ByVal obj As tbl_LookupInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "tbl_Lookup_Update", _
                                           sp.GetNull(obj.Code), _
                                           sp.GetNull(obj.Value), _
                                           sp.GetNull(obj.Text), _
                                           sp.GetNull(obj.DisplayOrder), _
                                           sp.GetNull(obj.Active), _
                                           sp.GetNull(obj.ID))
        End Sub

        Public Shared Sub Delete(ByVal id As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "tbl_Lookup_Delete", _
                                           sp.GetNull(id))
        End Sub

    End Class
End Namespace

