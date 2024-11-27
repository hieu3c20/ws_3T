Imports Microsoft.ApplicationBlocks.Data


Namespace Provider
    Public Class mDestinationGroupProvider

        Public Shared Function GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Destination_Group_GetAll").Tables(0)
        End Function

        Public Shared Function m_Destination_Group_GetByID(ByVal DesID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Destination_Group_GetByID", sp.GetNull(DesID)).Tables(0)
        End Function

        Public Shared Function m_Destination_Group_Insert(ByVal objGroup As mDestinationGroupInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "m_Destination_Group_Insert", _
                                           sp.GetNull(objGroup.GroupName), _
                                           sp.GetNull(objGroup.Note), _
                                           sp.GetNull(objGroup.Status))

        End Function

        Public Shared Sub m_Destination_Group_Upd(ByVal objGroup As mDestinationGroupInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_Destination_Group_Update", _
                                           sp.GetNull(objGroup.GroupName), _
                                           sp.GetNull(objGroup.Note), _
                                           sp.GetNull(objGroup.Status), _
                                           sp.GetNull(objGroup.GroupID))
        End Sub

        Public Shared Sub m_Destination_GroupDel(ByVal GroupID As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_Destination_Group_Del", sp.GetNull(GroupID))
        End Sub


    End Class
End Namespace

