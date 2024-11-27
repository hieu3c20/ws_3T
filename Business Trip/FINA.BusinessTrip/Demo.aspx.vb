Imports Provider
Imports DevExpress.Web.ASPxGridView

Partial Public Class Demo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not IsPostBack Then

        '    Dim dtRole As New DataTable
        '    dtRole = UserProvider.GetAllRole()
        '    gridRole.DataSource = dtRole
        '    gridRole.DataBind()
        '    'Bind vao RoleDetail
        '    'Dim dtUser As New DataTable
        '    'dtUser = UserProvider.GetEmployeeByRole(1)

        'End If
        GenPasswords()
    End Sub

    Protected Sub GenPasswords()
        Dim alpha As String = "QWERTYUIOPASDFGHJKLZXCVBNM"
        Dim spectial As String = "!@#$%^&*"
        Dim number As String = "1234567890"
        Dim dtData As New DataTable()
        dtData.Columns.Add("Password")
        dtData.Columns.Add("PasswordEncrypted")

        For index As Integer = 1 To 1715
            Dim dr As DataRow = dtData.NewRow()
            Dim random As New Random()
            Dim password As New StringBuilder()
            password.Append(alpha(random.Next(0, alpha.Length - 1)))
            For i As Integer = 1 To 2
                password.Append(alpha.ToLower()(random.Next(0, alpha.Length - 1)))
            Next
            password.Append(spectial(random.Next(0, spectial.Length - 1)))
            For i As Integer = 1 To 4
                password.Append(number.ToLower()(random.Next(0, number.Length - 1)))
            Next
            dr("Password") = password.ToString()
            dr("PasswordEncrypted") = CommonFunction.EncryptPassword(password.ToString())
            dtData.Rows.Add(dr)
            Threading.Thread.Sleep(100)
        Next

        grvPassword.DataSource = dtData
        grvPassword.DataBind()
    End Sub

    'Protected Sub btnSearch_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCheck.Click
    '    'Dim a As Integer = grvBTSubmitted.

    'End Sub

    'Protected Sub ASPxGridView1_BeforePerformDataSelect(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim detailTable As New DataTable
    '    detailTable = UserProvider.GetEmployeeByRole1()
    '    Dim dv As DataView = New DataView(detailTable)
    '    Dim detailGridView As ASPxGridView = CType(sender, ASPxGridView)
    '    dv.RowFilter = "RoleTypeID = " & detailGridView.GetMasterRowKeyValue().ToString()
    '    detailGridView.DataSource = dv
    'End Sub

    'Protected Sub test_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles test.SelectedIndexChanged
    '    Response.Write(test.SelectedItem.Text)
    'End Sub
End Class