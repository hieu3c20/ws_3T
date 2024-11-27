Imports Microsoft.ApplicationBlocks.Data


Namespace Provider
    Public Class mCountryGroupProvider

        Public Shared Function GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Country_Group_GetAll").Tables(0)
        End Function

        Public Shared Function m_Country_Group_GetByID(ByVal CountryGroupID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Country_Group_GetByID", sp.GetNull(CountryGroupID)).Tables(0)
        End Function

        Public Shared Function m_Country_Group_Insert(ByVal objGroup As mCountryGroupInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "m_Country_Group_Insert", _
                                           sp.GetNull(objGroup.GroupName), _
                                           sp.GetNull(objGroup.Desc), _
                                           sp.GetNull(objGroup.IsActive))

        End Function

        Public Shared Sub m_Country_Group_Upd(ByVal objGroup As mCountryGroupInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_Country_Group_Update", _
                                           sp.GetNull(objGroup.GroupName), _
                                           sp.GetNull(objGroup.Desc), _
                                           sp.GetNull(objGroup.IsActive), _
                                           sp.GetNull(objGroup.ID))
        End Sub

        Public Shared Sub m_Country_GroupDel(ByVal ID As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_Country_Group_Del", sp.GetNull(ID))
        End Sub


    End Class
End Namespace

