Imports Provider
Imports System.Data

Partial Public Class FAQ
    Inherits System.Web.UI.Page
    Protected _no As Integer = 0
    Protected ReadOnly Property No() As Integer
        Get
            _no = _no + 1
            Return _no
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub InitForm()

    End Sub

    Private Sub LoadForm()

    End Sub

End Class