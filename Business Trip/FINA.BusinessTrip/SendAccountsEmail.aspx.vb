Imports Provider
Imports System.Data

Partial Public Class SendAccountsEmail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub InitForm()

    End Sub

    Private Sub LoadForm()

    End Sub

    Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSend.Click
        Dim from As String = ConfigurationManager.AppSettings("BTSSupportEmail").Replace("[", "<").Replace("]", ">")
        Dim cc As String = ""
        Dim bcc As String = "sudungnq@toyotavn.com.vn" 'syhieuvt@toyotavn.com.vn
        Dim subject As String = "[BTS system]: Your account information"
        Dim body As New StringBuilder()
        body.Append("<p><strong>Dear {0} - san</strong></p>")
        body.Append("<p>We would like to send you the account information to login Business Trip Online System (BTS) as bellow:")
        body.Append("<ol><li><strong>How to access BTS system:</strong>")
        body.Append("<p>You need to open Firefox or IE version 10 or above then:</p>")
        body.Append("<p style='margin-left: 30px'>* From <a style='color: #0563c1' href='http://tmv-net.com.vn/'><strong>http://tmv-net.com.vn/</strong></a> -> Click <span style='text-decoration: underline'>BTS</span> button from main menu")
        body.Append("<br /><strong>Or</strong><br />* Click <a style='color: #0563c1' href='http://tmv-bts.com.vn'><strong>http://tmv-bts.com.vn</strong></a></p></li>")
        body.Append("<li><strong>Account information:</strong>")
        body.Append("<p style='margin-left: 30px'>* Username: <span style='color: red'>{1}</span> (This is your employee code)")
        body.Append("<br />* Password: <span style='color: red'>{2}</span> (You must change this default password when you login to BTS system for the first time)</p></li></ol>")        
        body.Append("<p>If you need any support, please feel free to contact IT Dept. helpdesk:")
        body.Append("<p style='margin-left: 80px'>* Extension No: Head Office: (861) 2222")
        body.Append("<br />* Email: support@toyotavn.com.vn</p>")
        body.Append("<p>After you receive your account information, you can use BTS for all trips that departure <span style='color: red'>from 01-Mar-2016</span></p>")
        body.Append("<p><strong>Thanks & Best regards<br />BTS support team</strong></p>")
        '    
        Using srv As TMVEmailService.EmailService = New TMVEmailService.EmailService()
            Dim dtUser As DataTable = UserProvider.GetAccountEmails()
            Dim count As Integer = 0
            For Each item As DataRow In dtUser.Rows
                'Dim mailTo As String = "sudungnq@toyotavn.com.vn"
                Dim mailTo As String = item("Email").ToString()
                If mailTo.Trim().Length > 0 Then
                    srv.SendEmail(from, mailTo, cc, bcc, subject, String.Format(body.ToString(), item("EmployeeName"), item("EmployeeCode"), item("Password")), "", "")
                    count += 1
                End If
            Next
            lblMessage.Text = String.Format("{0} sent!", count)
        End Using
    End Sub

End Class