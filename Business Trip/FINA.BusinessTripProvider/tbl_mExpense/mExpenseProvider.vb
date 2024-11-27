Imports Microsoft.ApplicationBlocks.Data

Namespace Provider
    Public Class mExpenseProvider

        Public Shared Function GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Expense_GetAll").Tables(0)
        End Function

        'Public Shared Function GetByCountry(ByVal country As Integer) As DataTable
        '    Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Destination_GetByCountry", country).Tables(0)
        'End Function

        Public Shared Function m_Expense_GetByID(ByVal ExpenseID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Expense_GetByID", sp.GetNull(ExpenseID)).Tables(0)
        End Function

        Public Shared Function m_Expense_Insert(ByVal objExp As mExpenseInfo) As String
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                "m_Expense_Insert", _
                sp.GetNull(objExp.TitleID), _
                sp.GetNull(objExp.BTType), _
                sp.GetNull(objExp.Breakfast), _
                sp.GetNull(objExp.Lunch), _
                sp.GetNull(objExp.Dinner), _
                sp.GetNull(objExp.OtherMeal), _
                sp.GetNull(objExp.Hotel), _
                sp.GetNull(objExp.Transportation), _
                sp.GetNull(objExp.Motobike), _
                sp.GetNull(objExp.Other), _
                sp.GetNull(objExp.Currency), _
                sp.GetNull(objExp.Note), _
                sp.GetNull(objExp.EffectiveDate))
        End Function

        Public Shared Function m_Expense_Upd(ByVal objExp As mExpenseInfo) As String
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                "m_Expense_Update", _
                sp.GetNull(objExp.TitleID), _
                sp.GetNull(objExp.BTType), _
                sp.GetNull(objExp.Breakfast), _
                sp.GetNull(objExp.Lunch), _
                sp.GetNull(objExp.Dinner), _
                sp.GetNull(objExp.OtherMeal), _
                sp.GetNull(objExp.Hotel), _
                sp.GetNull(objExp.Transportation), _
                sp.GetNull(objExp.Motobike), _
                sp.GetNull(objExp.Other), _
                sp.GetNull(objExp.Currency), _
                sp.GetNull(objExp.Note), _
                sp.GetNull(objExp.EffectiveDate), _
                sp.GetNull(objExp.ExpenseID))
            'sp.GetNull(objExp.DestinationGroupID), _
        End Function

        Public Shared Sub m_Expense_Del(ByVal ExpenseID As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_Expense_Del", sp.GetNull(ExpenseID))
        End Sub

        Public Shared Function m_Expense_GetNorm(ByVal btID As Integer, ByVal departureDate As DateTime) As DataTable 'ByVal employeeCode As String, ByVal destinationID As Integer, ByVal currency As String
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Expense_GetNorm", _
                                            sp.GetNull(btID), _
                                            sp.GetNull(departureDate)).Tables(0)
        End Function
    End Class
End Namespace

