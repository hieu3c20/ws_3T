 
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace Provider
    Public Class RoleTypeProvider

#Region "DATA TABLE"

        Public Shared Function Role_Type_GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "Role_Type_GetAll").Tables(0)
        End Function

        Public Shared Sub Role_Type_Ins()
            SqlHelper.ExecuteDataset(New Connections().SqlConn, "Role_Type_Ins")
        End Sub

        Public Shared Sub Role_Type_Udp()
            SqlHelper.ExecuteDataset(New Connections().SqlConn, "Role_Type_Udp")
        End Sub


#End Region

    End Class
End Namespace

