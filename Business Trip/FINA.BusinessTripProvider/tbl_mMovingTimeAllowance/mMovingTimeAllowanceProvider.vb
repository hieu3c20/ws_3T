Imports Microsoft.ApplicationBlocks.Data

Namespace Provider
    Public Class mMovingTimeAllowanceProvider

        Public Shared Function m_Expense_GetPolicy(ByVal countryCode As String) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_MovingTimeAllowance_GetPolicy", sp.GetNull(countryCode)).Tables(0)
        End Function

        Public Shared Function GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Allowance_GetAll").Tables(0)
        End Function


        Public Shared Function m_Allowance_GetByID(ByVal ID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Allowance_GetByID", sp.GetNull(ID)).Tables(0)
        End Function

        Public Shared Function m_Allowance_Insert(ByVal objAll As mMovingTimeAllowanceInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "m_Allowance_Insert", _
                                           sp.GetNull(objAll.CountryGroup), _
                                           sp.GetNull(objAll.Amount), _
                                           sp.GetNull(objAll.Currency), _
                                           sp.GetNull(objAll.Description))

        End Function

        Public Shared Sub m_Allowance_Upd(ByVal objAll As mMovingTimeAllowanceInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_Allowance_Update", _
                                           sp.GetNull(objAll.CountryGroup), _
                                           sp.GetNull(objAll.Amount), _
                                           sp.GetNull(objAll.Currency), _
                                           sp.GetNull(objAll.Description), _
                                           sp.GetNull(objAll.ID))
        End Sub

        Public Shared Sub m_Allowance_Del(ByVal ID As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_Allowance_Del", sp.GetNull(ID))
        End Sub

    End Class
End Namespace

