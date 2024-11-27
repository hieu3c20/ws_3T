 
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data


Namespace Provider
    Public Class RoleLevelProvider

#Region "DATA TABLE"

        Public Shared Function Role_Level_GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "Role_Level_GetAll").Tables(0)
        End Function

        Public Shared Sub Role_Level_Ins()
            SqlHelper.ExecuteDataset(New Connections().SqlConn, "Role_Level_Ins")
        End Sub

        Public Shared Sub Role_Level_Udp()
            SqlHelper.ExecuteDataset(New Connections().SqlConn, "Role_Level_Udp")
        End Sub


#End Region

    End Class
End Namespace

