Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient

Namespace Provider
    Public Class mBudgetProvider

        Public Shared Function DepartmentGetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "Department_GetAll").Tables(0)
        End Function

        Public Shared Function GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Budget_GetAll").Tables(0)
        End Function

        Public Shared Function GetActive() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Budget_GetActive").Tables(0)
        End Function

        Public Shared Function GetByDepartment(ByVal username As String, Optional ByVal active As Boolean = False) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Budget_GetByDepartment", username, active).Tables(0)
        End Function

        Public Shared Function GetByDepartmentAndType(ByVal username As String, ByVal type As String, Optional ByVal active As Boolean = False) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Budget_GetByDepartmentAndType", username, type, active).Tables(0)
        End Function

        Public Shared Function GetOtherByType(ByVal username As String, ByVal type As String, Optional ByVal active As Boolean = False) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Budget_GetOtherByType", username, type, active).Tables(0)
        End Function

        Public Shared Function GetOther(ByVal username As String, Optional ByVal active As Boolean = False) As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Budget_GetOther", username, active).Tables(0)
        End Function

        Public Shared Function m_Budget_GetByID(ByVal BudgetID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Budget_GetByID", sp.GetNull(BudgetID)).Tables(0)
        End Function

        Public Shared Function m_Budget_Insert(ByVal objBug As mBudgetInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "m_Budget_Insert", _
                                           sp.GetNull(objBug.BudgetCode), _
                                           sp.GetNull(objBug.BudgetName), _
                                           sp.GetNull(objBug.Amount), _
                                           sp.GetNull(objBug.Org), _
                                           sp.GetNull(objBug.Department), _
                                           sp.GetNull(objBug.Description), _
                                           sp.GetNull(objBug.HRDepID), _
                                           sp.GetNull(objBug.Active), _
                                           sp.GetNull(objBug.Budget_Type), _
                                           sp.GetNull(objBug.IsExecutive))
        End Function

        Public Shared Sub m_Budget_Upd(ByVal objBug As mBudgetInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_Budget_Update", _
                                           sp.GetNull(objBug.BudgetCode), _
                                           sp.GetNull(objBug.BudgetName), _
                                           sp.GetNull(objBug.Amount), _
                                           sp.GetNull(objBug.Org), _
                                           sp.GetNull(objBug.Department), _
                                           sp.GetNull(objBug.Description), _
                                           sp.GetNull(objBug.HRDepID), _
                                           sp.GetNull(objBug.BudgetID), _
                                           sp.GetNull(objBug.Active), _
                                           sp.GetNull(objBug.Budget_Type), _
                                           sp.GetNull(objBug.IsExecutive))
        End Sub

        Public Shared Sub m_Budget_Del(ByVal BudgetID As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_Budget_Del", sp.GetNull(BudgetID))
        End Sub

        Public Shared Sub m_Budget_GetOraData()
            'SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "GetOraBudget")
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "GetOraBudget"
                    sqlcm.ExecuteNonQuery()
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Sub

        Public Shared Function m_Budget_GetPICs(ByVal budgetCode As String) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_Budget_GetPICs", _
                                            sp.GetNull(budgetCode)).Tables(0)
        End Function

        Public Shared Function m_BudgetPIC_GetAll() As DataTable
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_BudgetPIC_GetAll").Tables(0)
        End Function

        Public Shared Function m_BudgetPIC_GetByID(ByVal BudgetPICID As Integer) As DataTable
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "m_BudgetPIC_GetByID", sp.GetNull(BudgetPICID)).Tables(0)
        End Function

        Public Shared Function m_BudgetPIC_Insert(ByVal objBug As mBudgetPICInfo) As Integer
            Dim sp As New SqlDataProvider()
            Return SqlHelper.ExecuteScalar(New Connections().SqlConn, _
                                           "m_BudgetPIC_Insert", _
                                           sp.GetNull(objBug.Org), _
                                           sp.GetNull(objBug.PICName), _
                                           sp.GetNull(objBug.PICEmail))
        End Function

        Public Shared Sub m_BudgetPIC_Upd(ByVal objBug As mBudgetPICInfo)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().SqlConn, _
                                           "m_BudgetPIC_Update", _
                                           sp.GetNull(objBug.ID), _
                                           sp.GetNull(objBug.Org), _
                                           sp.GetNull(objBug.PICName), _
                                           sp.GetNull(objBug.PICEmail))
        End Sub

        Public Shared Sub m_BudgetPIC_Del(ByVal id As Integer)
            Dim sp As New SqlDataProvider()
            SqlHelper.ExecuteNonQuery(New Connections().sqlcon, "m_BudgetPIC_Del", sp.GetNull(id))
        End Sub

    End Class
End Namespace

