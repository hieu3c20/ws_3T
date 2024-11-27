Imports Microsoft.ApplicationBlocks.Data


Namespace Provider
    Public Class mDestinationProvider

        Public Shared Function GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Destination_GetAll").Tables(0)
        End Function

        Public Shared Function GetByCountry(ByVal country As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Destination_GetByCountry", country).Tables(0)
        End Function

        Public Shared Function GetByCountryCode(ByVal countryCode As String) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Destination_GetByCountryCode", countryCode).Tables(0)
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
                                           sp.GetNull(objDes.Note), _
                                           sp.GetNull(objDes.CountryID), _
                                           sp.GetNull(objDes.GroupID), _
                                           sp.GetNull(objDes.Status))

        End Function

        Public Shared Sub m_Destination_Upd(ByVal objDes As mDestinationInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_Destination_Update", _
                                           sp.GetNull(objDes.Name), _
                                           sp.GetNull(objDes.Note), _
                                           sp.GetNull(objDes.CountryID), _
                                           sp.GetNull(objDes.GroupID), _
                                           sp.GetNull(objDes.Status), _
                                           sp.GetNull(objDes.DestinationID))
        End Sub

        Public Shared Sub m_Destination_Del(ByVal DesID As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_Destination_Del", sp.GetNull(DesID))
        End Sub
    End Class
End Namespace

