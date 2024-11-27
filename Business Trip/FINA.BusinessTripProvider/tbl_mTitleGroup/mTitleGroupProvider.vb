Imports Microsoft.ApplicationBlocks.Data


Namespace Provider
    Public Class mTitleGroupProvider

        Public Shared Function GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Title_Group_GetAll").Tables(0)
        End Function

        Public Shared Function CheckTitleGroup(ByVal GroupTitle As String) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Title_Check_Group", sp.GetNull(GroupTitle)).Tables(0)
        End Function

        Public Shared Function m_Title_Group_GetByID(ByVal TitleGroID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Title_Group_GetByID", sp.GetNull(TitleGroID)).Tables(0)
        End Function

        Public Shared Function m_Title_Group_Insert(ByVal objTitle As mTitleGroupInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "m_Title_Group_Insert", _
                                           sp.GetNull(objTitle.Name), _
                                           sp.GetNull(objTitle.GroupTitle), _
                                           sp.GetNull(objTitle.Note), _
                                           sp.GetNull(objTitle.Status), _
                                           sp.GetNull(objTitle.TitleIDs))

        End Function

        Public Shared Sub m_Title_Group_Upd(ByVal objTitle As mTitleGroupInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_Title_Group_Update", _
                                           sp.GetNull(objTitle.Name), _
                                           sp.GetNull(objTitle.GroupTitle), _
                                           sp.GetNull(objTitle.Note), _
                                           sp.GetNull(objTitle.Status), _
                                           sp.GetNull(objTitle.TitleIDs), _
                                           sp.GetNull(objTitle.TitleGroupID))
        End Sub

        Public Shared Sub m_Title_Group_Del(ByVal TitleGroupID As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_Title_Group_Del", sp.GetNull(TitleGroupID))
        End Sub

    End Class
End Namespace

