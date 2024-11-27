Imports Microsoft.ApplicationBlocks.Data


Namespace Provider
    Public Class mCountryProvider

        Public Shared Function GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Country_GetAll").Tables(0)
        End Function

        Public Shared Function GetDestinationAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Destination_GetAll").Tables(0)
        End Function

        Public Shared Function GetTitleAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Title_GetAll").Tables(0)
        End Function

        Public Shared Function m_Country_GetByID(ByVal id As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Country_GetByID", sp.GetNull(id)).Tables(0)
        End Function

        Public Shared Function m_Country_Insert(ByVal obj As mCountryInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "m_Country_Insert", _
                                           sp.GetNull(obj.Code), _
                                           sp.GetNull(obj.Name), _
                                           sp.GetNull(obj.GroupID))

        End Function

        Public Shared Sub m_Country_Update(ByVal obj As mCountryInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_Country_Update", _
                                           sp.GetNull(obj.ID), _
                                           sp.GetNull(obj.Code), _
                                           sp.GetNull(obj.Name), _
                                           sp.GetNull(obj.GroupID))
        End Sub

        Public Shared Sub m_Country_Delete(ByVal id As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_Country_Del", sp.GetNull(id))
        End Sub

    End Class
End Namespace

