Imports Microsoft.ApplicationBlocks.Data

Namespace Provider
    Public Class mAirPeriodProvider

        Public Shared Function GetActive() As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_AirPeriod_GetActive").Tables(0)
        End Function

        Public Shared Function GetAll() As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_AirPeriod_GetAll").Tables(0)
        End Function

        Public Shared Function m_AirPeriod_GetByID(ByVal AirPeriodID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_AirPeriod_GetByID", sp.GetNull(AirPeriodID)).Tables(0)
        End Function

        Public Shared Function m_AirPeriod_Insert(ByVal objItem As mAirPeriodInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "m_AirPeriod_Insert", _
                                           sp.GetNull(objItem.Name), _
                                           sp.GetNull(objItem.Description), _
                                           sp.GetNull(objItem.Active))
        End Function

        Public Shared Sub m_AirPeriod_Upd(ByVal objItem As mAirPeriodInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_AirPeriod_Update", _
                                           sp.GetNull(objItem.Name), _
                                           sp.GetNull(objItem.Description), _
                                           sp.GetNull(objItem.Active), _
                                           sp.GetNull(objItem.ID))
        End Sub

        Public Shared Sub m_AirPeriod_Del(ByVal AirPeriodID As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_AirPeriod_Del", sp.GetNull(AirPeriodID))
        End Sub

    End Class
End Namespace

