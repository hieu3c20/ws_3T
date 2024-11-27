Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient


Namespace Provider
    Public Class ReportProvider

        Public Shared Function GetOneDayBT(ByVal btID As Integer) As DataSet
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "rptOneDayBT", btID)
        End Function

        Public Shared Function GetOneDayExpenseBT(ByVal btID As Integer) As DataSet
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "rptOneDayExpenseBT", btID)
        End Function

        Public Shared Function GetOverNightBT(ByVal btID As Integer) As DataSet
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "rptOverNightBT", btID)
        End Function

        Public Shared Function GetBTExpense(ByVal btID As Integer) As DataSet
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "rptBTExpense", btID)
        End Function

        Public Shared Function GetBTSchedule(ByVal btID As Integer) As DataSet
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "rptBTSchedule", btID)
        End Function

        Public Shared Function GetBTExpenseSchedule(ByVal btID As Integer) As DataSet
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "rptBTExpenseSchedule", btID)
        End Function

        Public Shared Function GetOneDayPayment(ByVal btID As Integer) As DataSet
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "rptBTOneDayPayment", btID)
        End Function

#Region "Report"

        Public Shared Function GetInitReportCondition(ByVal username As String) As DataSet
            Return SqlHelper.ExecuteDataset(New Connections().SqlConn, "GetInitReportCondition", username)
        End Function

        Public Shared Function AdvanceReport(ByVal obj As ReportInfo) As DataSet
            Dim sp As New SqlDataProvider()
            'Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
            '    "rptAdvanceReport", _
            '    sp.GetNull(obj.DepartureFrom), _
            '    sp.GetNull(obj.DepartureTo), _
            '    sp.GetNull(obj.BranchID), _
            '    sp.GetNull(obj.ExceptBranch), _
            '    sp.GetNull(obj.DivID), _
            '    sp.GetNull(obj.ExceptDiv), _
            '    sp.GetNull(obj.DeptID), _
            '    sp.GetNull(obj.ExceptDept), _
            '    sp.GetNull(obj.SecID), _
            '    sp.GetNull(obj.ExceptSec), _
            '    sp.GetNull(obj.GroupID), _
            '    sp.GetNull(obj.ExceptGroup), _
            '    sp.GetNull(obj.TeamhID), _
            '    sp.GetNull(obj.ExceptTeam), _
            '    sp.GetNull(obj.EmployeeCode), _
            '    sp.GetNull(obj.ExceptEmployeeCode), _
            '    sp.GetNull(obj.Username))
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "rptAdvanceReport"
                    sqlcm.Parameters.AddWithValue("@departureFrom", sp.GetNull(obj.DepartureFrom))
                    sqlcm.Parameters.AddWithValue("@departureTo", sp.GetNull(obj.DepartureTo))
                    sqlcm.Parameters.AddWithValue("@branchID", sp.GetNull(obj.BranchID))
                    sqlcm.Parameters.AddWithValue("@exceptBranch", sp.GetNull(obj.ExceptBranch))
                    sqlcm.Parameters.AddWithValue("@divisionID", sp.GetNull(obj.DivID))
                    sqlcm.Parameters.AddWithValue("@exceptDiv", sp.GetNull(obj.ExceptDiv))
                    sqlcm.Parameters.AddWithValue("@departmentID", sp.GetNull(obj.DeptID))
                    sqlcm.Parameters.AddWithValue("@exceptDep", sp.GetNull(obj.ExceptDept))
                    sqlcm.Parameters.AddWithValue("@sectionID", sp.GetNull(obj.SecID))
                    sqlcm.Parameters.AddWithValue("@exceptSec", sp.GetNull(obj.ExceptSec))
                    sqlcm.Parameters.AddWithValue("@groupID", sp.GetNull(obj.GroupID))
                    sqlcm.Parameters.AddWithValue("@exceptGroup", sp.GetNull(obj.ExceptGroup))
                    sqlcm.Parameters.AddWithValue("@teamID", sp.GetNull(obj.TeamID))
                    sqlcm.Parameters.AddWithValue("@exceptTeam", sp.GetNull(obj.ExceptTeam))
                    sqlcm.Parameters.AddWithValue("@employeeCode", sp.GetNull(obj.EmployeeCode))
                    sqlcm.Parameters.AddWithValue("@exceptEmp", sp.GetNull(obj.ExceptEmployeeCode))
                    sqlcm.Parameters.AddWithValue("@username", sp.GetNull(obj.Username))
                    Dim adapter As New SqlDataAdapter(sqlcm)
                    Dim ds As New DataSet()
                    adapter.Fill(ds)
                    Return ds
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

        Public Shared Function ExpenseReport(ByVal obj As ReportInfo) As DataSet
            Dim sp As New SqlDataProvider()
            'Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
            '    "rptExpenseReport", _
            '    sp.GetNull(obj.DepartureFrom), _
            '    sp.GetNull(obj.DepartureTo), _
            '    sp.GetNull(obj.BranchID), _
            '    sp.GetNull(obj.ExceptBranch), _
            '    sp.GetNull(obj.DivID), _
            '    sp.GetNull(obj.ExceptDiv), _
            '    sp.GetNull(obj.DeptID), _
            '    sp.GetNull(obj.ExceptDept), _
            '    sp.GetNull(obj.SecID), _
            '    sp.GetNull(obj.ExceptSec), _
            '    sp.GetNull(obj.GroupID), _
            '    sp.GetNull(obj.ExceptGroup), _
            '    sp.GetNull(obj.TeamID), _
            '    sp.GetNull(obj.ExceptTeam), _
            '    sp.GetNull(obj.EmployeeCode), _
            '    sp.GetNull(obj.ExceptEmployeeCode), _
            '    sp.GetNull(obj.Username))
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "rptExpenseReport"
                    sqlcm.Parameters.AddWithValue("@departureFrom", sp.GetNull(obj.DepartureFrom))
                    sqlcm.Parameters.AddWithValue("@departureTo", sp.GetNull(obj.DepartureTo))
                    sqlcm.Parameters.AddWithValue("@branchID", sp.GetNull(obj.BranchID))
                    sqlcm.Parameters.AddWithValue("@exceptBranch", sp.GetNull(obj.ExceptBranch))
                    sqlcm.Parameters.AddWithValue("@divisionID", sp.GetNull(obj.DivID))
                    sqlcm.Parameters.AddWithValue("@exceptDiv", sp.GetNull(obj.ExceptDiv))
                    sqlcm.Parameters.AddWithValue("@departmentID", sp.GetNull(obj.DeptID))
                    sqlcm.Parameters.AddWithValue("@exceptDep", sp.GetNull(obj.ExceptDept))
                    sqlcm.Parameters.AddWithValue("@sectionID", sp.GetNull(obj.SecID))
                    sqlcm.Parameters.AddWithValue("@exceptSec", sp.GetNull(obj.ExceptSec))
                    sqlcm.Parameters.AddWithValue("@groupID", sp.GetNull(obj.GroupID))
                    sqlcm.Parameters.AddWithValue("@exceptGroup", sp.GetNull(obj.ExceptGroup))
                    sqlcm.Parameters.AddWithValue("@teamID", sp.GetNull(obj.TeamID))
                    sqlcm.Parameters.AddWithValue("@exceptTeam", sp.GetNull(obj.ExceptTeam))
                    sqlcm.Parameters.AddWithValue("@employeeCode", sp.GetNull(obj.EmployeeCode))
                    sqlcm.Parameters.AddWithValue("@exceptEmp", sp.GetNull(obj.ExceptEmployeeCode))
                    sqlcm.Parameters.AddWithValue("@username", sp.GetNull(obj.Username))
                    Dim adapter As New SqlDataAdapter(sqlcm)
                    Dim ds As New DataSet()
                    adapter.Fill(ds)
                    Return ds
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

        Public Shared Function AdvanceClearReport(ByVal obj As ReportInfo, Optional ByVal noAdvance As Boolean = False) As DataSet
            Dim sp As New SqlDataProvider()
            'Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
            '    "rptAdvanceClearReport", _
            '    sp.GetNull(obj.DepartureFrom), _
            '    sp.GetNull(obj.DepartureTo), _
            '    sp.GetNull(obj.BranchID), _
            '    sp.GetNull(obj.ExceptBranch), _
            '    sp.GetNull(obj.DivID), _
            '    sp.GetNull(obj.ExceptDiv), _
            '    sp.GetNull(obj.DeptID), _
            '    sp.GetNull(obj.ExceptDept), _
            '    sp.GetNull(obj.SecID), _
            '    sp.GetNull(obj.ExceptSec), _
            '    sp.GetNull(obj.GroupID), _
            '    sp.GetNull(obj.ExceptGroup), _
            '    sp.GetNull(obj.TeamID), _
            '    sp.GetNull(obj.ExceptTeam), _
            '    sp.GetNull(obj.EmployeeCode), _
            '    sp.GetNull(obj.ExceptEmployeeCode), _
            '    sp.GetNull(obj.Username))
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "rptAdvanceClearReport"
                    sqlcm.Parameters.AddWithValue("@departureFrom", sp.GetNull(obj.DepartureFrom))
                    sqlcm.Parameters.AddWithValue("@departureTo", sp.GetNull(obj.DepartureTo))
                    sqlcm.Parameters.AddWithValue("@branchID", sp.GetNull(obj.BranchID))
                    sqlcm.Parameters.AddWithValue("@exceptBranch", sp.GetNull(obj.ExceptBranch))
                    sqlcm.Parameters.AddWithValue("@divisionID", sp.GetNull(obj.DivID))
                    sqlcm.Parameters.AddWithValue("@exceptDiv", sp.GetNull(obj.ExceptDiv))
                    sqlcm.Parameters.AddWithValue("@departmentID", sp.GetNull(obj.DeptID))
                    sqlcm.Parameters.AddWithValue("@exceptDep", sp.GetNull(obj.ExceptDept))
                    sqlcm.Parameters.AddWithValue("@sectionID", sp.GetNull(obj.SecID))
                    sqlcm.Parameters.AddWithValue("@exceptSec", sp.GetNull(obj.ExceptSec))
                    sqlcm.Parameters.AddWithValue("@groupID", sp.GetNull(obj.GroupID))
                    sqlcm.Parameters.AddWithValue("@exceptGroup", sp.GetNull(obj.ExceptGroup))
                    sqlcm.Parameters.AddWithValue("@teamID", sp.GetNull(obj.TeamID))
                    sqlcm.Parameters.AddWithValue("@exceptTeam", sp.GetNull(obj.ExceptTeam))
                    sqlcm.Parameters.AddWithValue("@employeeCode", sp.GetNull(obj.EmployeeCode))
                    sqlcm.Parameters.AddWithValue("@exceptEmp", sp.GetNull(obj.ExceptEmployeeCode))
                    sqlcm.Parameters.AddWithValue("@username", sp.GetNull(obj.Username))
                    sqlcm.Parameters.AddWithValue("@noAdvance", sp.GetNull(noAdvance))
                    Dim adapter As New SqlDataAdapter(sqlcm)
                    Dim ds As New DataSet()
                    adapter.Fill(ds)
                    Return ds
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

        Public Shared Function AirTicketReport(ByVal obj As ReportInfo) As DataSet
            Dim sp As New SqlDataProvider()
            'Return SqlHelper.ExecuteDataset(New Connections().SqlConn, _
            '    "rptAirTicketReport", _
            '    sp.GetNull(obj.DepartureFrom), _
            '    sp.GetNull(obj.DepartureTo), _
            '    sp.GetNull(obj.BranchID), _
            '    sp.GetNull(obj.ExceptBranch), _
            '    sp.GetNull(obj.DivID), _
            '    sp.GetNull(obj.ExceptDiv), _
            '    sp.GetNull(obj.DeptID), _
            '    sp.GetNull(obj.ExceptDept), _
            '    sp.GetNull(obj.SecID), _
            '    sp.GetNull(obj.ExceptSec), _
            '    sp.GetNull(obj.GroupID), _
            '    sp.GetNull(obj.ExceptGroup), _
            '    sp.GetNull(obj.TeamID), _
            '    sp.GetNull(obj.ExceptTeam), _
            '    sp.GetNull(obj.EmployeeCode), _
            '    sp.GetNull(obj.ExceptEmployeeCode), _
            '    sp.GetNull(obj.Username), _
            '    sp.GetNull(obj.AirPeriod), _
            '    sp.GetNull(obj.OraSupplier))
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "rptAirTicketReport"
                    sqlcm.Parameters.AddWithValue("@departureFrom", sp.GetNull(obj.DepartureFrom))
                    sqlcm.Parameters.AddWithValue("@departureTo", sp.GetNull(obj.DepartureTo))
                    sqlcm.Parameters.AddWithValue("@branchID", sp.GetNull(obj.BranchID))
                    sqlcm.Parameters.AddWithValue("@exceptBranch", sp.GetNull(obj.ExceptBranch))
                    sqlcm.Parameters.AddWithValue("@divisionID", sp.GetNull(obj.DivID))
                    sqlcm.Parameters.AddWithValue("@exceptDiv", sp.GetNull(obj.ExceptDiv))
                    sqlcm.Parameters.AddWithValue("@departmentID", sp.GetNull(obj.DeptID))
                    sqlcm.Parameters.AddWithValue("@exceptDep", sp.GetNull(obj.ExceptDept))
                    sqlcm.Parameters.AddWithValue("@sectionID", sp.GetNull(obj.SecID))
                    sqlcm.Parameters.AddWithValue("@exceptSec", sp.GetNull(obj.ExceptSec))
                    sqlcm.Parameters.AddWithValue("@groupID", sp.GetNull(obj.GroupID))
                    sqlcm.Parameters.AddWithValue("@exceptGroup", sp.GetNull(obj.ExceptGroup))
                    sqlcm.Parameters.AddWithValue("@teamID", sp.GetNull(obj.TeamID))
                    sqlcm.Parameters.AddWithValue("@exceptTeam", sp.GetNull(obj.ExceptTeam))
                    sqlcm.Parameters.AddWithValue("@employeeCode", sp.GetNull(obj.EmployeeCode))
                    sqlcm.Parameters.AddWithValue("@exceptEmp", sp.GetNull(obj.ExceptEmployeeCode))
                    sqlcm.Parameters.AddWithValue("@username", sp.GetNull(obj.Username))
                    sqlcm.Parameters.AddWithValue("@airPeriod", sp.GetNull(obj.AirPeriod))
                    sqlcm.Parameters.AddWithValue("@oraSupplier", sp.GetNull(obj.OraSupplier))
                    Dim adapter As New SqlDataAdapter(sqlcm)
                    Dim ds As New DataSet()
                    adapter.Fill(ds)
                    Return ds
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

        Public Shared Function WifiReport(ByVal obj As ReportInfo) As DataSet
            Dim sp As New SqlDataProvider()            
            Using sqlcm = New SqlCommand()
                Try
                    sqlcm.Connection = New Connections().SqlConn
                    sqlcm.CommandTimeout = 3600
                    sqlcm.CommandType = CommandType.StoredProcedure
                    sqlcm.CommandText = "rptWifiReport"
                    sqlcm.Parameters.AddWithValue("@departureFrom", sp.GetNull(obj.DepartureFrom))
                    sqlcm.Parameters.AddWithValue("@departureTo", sp.GetNull(obj.DepartureTo))
                    sqlcm.Parameters.AddWithValue("@branchID", sp.GetNull(obj.BranchID))
                    sqlcm.Parameters.AddWithValue("@exceptBranch", sp.GetNull(obj.ExceptBranch))
                    sqlcm.Parameters.AddWithValue("@divisionID", sp.GetNull(obj.DivID))
                    sqlcm.Parameters.AddWithValue("@exceptDiv", sp.GetNull(obj.ExceptDiv))
                    sqlcm.Parameters.AddWithValue("@departmentID", sp.GetNull(obj.DeptID))
                    sqlcm.Parameters.AddWithValue("@exceptDep", sp.GetNull(obj.ExceptDept))
                    sqlcm.Parameters.AddWithValue("@sectionID", sp.GetNull(obj.SecID))
                    sqlcm.Parameters.AddWithValue("@exceptSec", sp.GetNull(obj.ExceptSec))
                    sqlcm.Parameters.AddWithValue("@groupID", sp.GetNull(obj.GroupID))
                    sqlcm.Parameters.AddWithValue("@exceptGroup", sp.GetNull(obj.ExceptGroup))
                    sqlcm.Parameters.AddWithValue("@teamID", sp.GetNull(obj.TeamID))
                    sqlcm.Parameters.AddWithValue("@exceptTeam", sp.GetNull(obj.ExceptTeam))
                    sqlcm.Parameters.AddWithValue("@employeeCode", sp.GetNull(obj.EmployeeCode))
                    sqlcm.Parameters.AddWithValue("@exceptEmp", sp.GetNull(obj.ExceptEmployeeCode))
                    sqlcm.Parameters.AddWithValue("@username", sp.GetNull(obj.Username))
                    sqlcm.Parameters.AddWithValue("@bttype", sp.GetNull(obj.BTType))
                    sqlcm.Parameters.AddWithValue("@countryID", sp.GetNull(obj.CountryID))
                    Dim adapter As New SqlDataAdapter(sqlcm)
                    Dim ds As New DataSet()
                    adapter.Fill(ds)
                    Return ds
                Finally
                    Try
                        sqlcm.Connection.Close()
                        sqlcm.Connection.Dispose()
                    Catch
                    End Try
                End Try
            End Using
        End Function

#End Region

    End Class
End Namespace

