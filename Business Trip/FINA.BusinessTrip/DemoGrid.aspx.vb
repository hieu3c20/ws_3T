Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxGridView
Imports System.Collections
Imports DevExpress.Web.ASPxEditors
Imports Provider
Partial Public Class DemoGrid
    Inherits System.Web.UI.Page

    Private ds As DataSet = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'If Not IsPostBack Then
        '    'Dim dtBranch As DataTable = BusinessTripProvider.tbl_BranchGetAll()

        '    'Dim lb As ASPxListBox = sender

        '    'lb.DataSource = dtBranch
        '    'lb.ValueField = "BranchID"
        '    'lb.TextField = "Name"

        '    'lb.DataBind()
        'End If

     
        Dim dtBranch As DataTable = BusinessTripProvider.tbl_BranchGetAll()
        Dim lb As ASPxListBox = (ddlBranchEdit.FindControl("ddlBranch"))
        lb.DataSource = dtBranch
        lb.ValueField = "BranchID"
        lb.TextField = "BranchName"
        lb.DataBind()


        'Dim strCount As String

        Dim obj As Object
        obj = lb

        Dim dt As DataTable = CType(lb.DataSource, DataTable)

        'If dt Then
        For j As Integer = 0 To dt.Rows.Count - 1

        Next





        Dim BranchID As String = "1,3,5"

        ddlBranchEdit.Text = "NIC HCM"
        'Dim BraArr As String() = BranchID.Split(",")

        'For i As Integer = 0 To BraArr.Length - 1
        '    Dim ch As String
        '    ch = BraArr(i).ToString()
        '    'For j As Integer = 0 To lb.Co
        'Next

    End Sub



    Protected Sub btnTest_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTest.Click
        Dim str As String = String.Empty

        str = ddlBranchEdit.Value
        str = hTest.Value
    End Sub
End Class