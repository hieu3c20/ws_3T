
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.ApplicationBlocks.Data
Imports System.Security.Cryptography
Imports System.Text

Namespace Provider
    Public Class mDestinationProvider

        Public Shared Function GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Destination_GetAll").Tables(0)
        End Function

        Public Shared Function m_Destination_GetByID(ByVal DesID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Destination_GetByID", sp.GetNull(DesID)).Tables(0)
        End Function
        Public Shared Function m_Destination_Insert(ByVal objDes As mDestinationInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "m_Destination_Insert", _
                                           sp.GetNull(objDes.Name), _
                                           sp.GetNull(objDes.Note))
        End Function

        Public Shared Sub m_Destination_Upd(ByVal objDes As mDestinationInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_Destination_Update", _
                                           sp.GetNull(objDes.Name), _
                                           sp.GetNull(objDes.Note), _
                                           sp.GetNull(objDes.DestinationID))
        End Sub


    End Class
End Namespace

