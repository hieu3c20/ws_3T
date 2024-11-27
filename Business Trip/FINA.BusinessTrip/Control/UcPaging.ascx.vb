Imports Provider

Partial Public Class UcPaging
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub InitData()
        Style2()
    End Sub

    Protected Sub ControlCommand(ByVal sender As Object, ByVal e As CommandEventArgs)
        Try

            If e.CommandName = "_previous" Then
                CurrentPage = (Integer.Parse(CurrentPage) - 1).ToString
                Response.Redirect(Href())
                Return
            End If

            If e.CommandName = "_next" Then
                CurrentPage = (Integer.Parse(CurrentPage) + 1).ToString
                Response.Redirect(Href())
                Return
            End If

        Catch ex As Exception
            lblMessage.Text = ex.Message
        End Try

    End Sub

#Region "Properties"

    Public Property TongSoBanGhi() As String
        Get
            If _TongSoBanGhi.Value = "" Then
                Return "0"
            Else
                Return _TongSoBanGhi.Value
            End If
        End Get
        Set(ByVal value As String)
            _TongSoBanGhi.Value = value
        End Set
    End Property

    Public Property CurrentPage() As String
        Get
            If _CurrentPage.Value = "" Then
                Return "0"
            Else
                Return _CurrentPage.Value
            End If
        End Get
        Set(ByVal value As String)
            _CurrentPage.Value = value
        End Set
    End Property

    Public Property PageSize() As String
        Get
            If _PageSize.Value = "" Then
                Return "0"
            Else
                Return _PageSize.Value
            End If
        End Get
        Set(ByVal value As String)
            _PageSize.Value = value
        End Set
    End Property

    Public Property DefaultShowPageSize() As String
        Get
            If _DefaultShowPageSize.Value = "" Then
                Return "3"
            Else
                Return _DefaultShowPageSize.Value
            End If
        End Get
        Set(ByVal value As String)
            _DefaultShowPageSize.Value = value
        End Set
    End Property

    Public Property ShowPageSize() As String
        Get
            If _ShowPageSize.Value = "" Then
                Return "0"
            Else
                Return _ShowPageSize.Value
            End If
        End Get
        Set(ByVal value As String)
            _ShowPageSize.Value = value
        End Set
    End Property

    Public Property SoBanGhiTrongPage() As String
        Get
            If _SoBanGhiTrongPage.Value = "" Then
                Return "0"
            Else
                Return _SoBanGhiTrongPage.Value
            End If
        End Get
        Set(ByVal value As String)
            _SoBanGhiTrongPage.Value = value
        End Set
    End Property

#End Region

    Public Function Href() As String
        Return getUrl(CurrentPage)
    End Function

    Private Function getUrl(ByVal current As String) As String

        Dim specy As String = String.Empty
        Dim url As String = HttpContext.Current.Request.Url.AbsoluteUri
        'Dim path As String = HttpContext.Current.Request.Url.AbsolutePath
        'Dim host As String = HttpContext.Current.Request.Url.Host

        Dim urls As String() = url.Split("?")
        If urls.Length > 1 Then

            Dim params As String() = urls(1).Split("&")
            Dim tmpUrl As String = urls(0)
            specy = "?"
            For Each param As String In params

                If param.StartsWith("current") Or param.StartsWith("pagesize") Then
                    Continue For
                End If

                tmpUrl = tmpUrl + specy + param

                If specy = "?" Then
                    specy = "&"
                End If

            Next
            url = tmpUrl
        Else
            specy = "?"
        End If

        Return url + specy + "current=" + current + "&pagesize=" + PageSize

    End Function

    Public Sub Style1()
        lblTongSoBanGhi.Text = TongSoBanGhi
        'lblSoBanGhiTrongPage.Text = SoBanGhiTrongPage

        Dim totalPage As Integer
        Dim tmpTongso As Integer = Integer.Parse(TongSoBanGhi)
        Dim tmpPagesize As Integer = Integer.Parse(PageSize)

        totalPage = Math.Round(tmpTongso / tmpPagesize, 0)

        Dim listDrops As List(Of DropDownListInfo) = New List(Of DropDownListInfo)
        Dim listDrop As DropDownListInfo

        Dim tmpStartPage As Integer = 1

        If ShowPageSize = "0" Then
            ShowPageSize = DefaultShowPageSize
        End If

        If CurrentPage > Integer.Parse(ShowPageSize) Then
            ShowPageSize = Integer.Parse(ShowPageSize) + Integer.Parse(DefaultShowPageSize)
            tmpStartPage = Integer.Parse(ShowPageSize) - (Integer.Parse(DefaultShowPageSize))
        End If

        For i As Integer = tmpStartPage To totalPage Step 1


            listDrop = New DropDownListInfo()

            If i - 1 > Integer.Parse(ShowPageSize) Then
                Exit For
            ElseIf i - 1 = Integer.Parse(ShowPageSize) Then
                listDrop.Text = "..."
                listDrop.MaxShow = i.ToString()
            Else
                listDrop.Text = i.ToString()
            End If

            If CurrentPage = i.ToString() Then
                listDrop.CurrentCss = "current"
            Else
                listDrop.CurrentCss = ""
            End If

            listDrop.Value = i.ToString()
            listDrop.Href = getUrl(i.ToString())
            listDrops.Add(listDrop)
        Next

        rptCurrentPage.DataSource = listDrops
        rptCurrentPage.DataBind()
    End Sub

    Public Sub Style2()
        Dim totalPage As Integer
        Dim tmpTongso As Integer = Integer.Parse(TongSoBanGhi)
        Dim tmpPagesize As Integer = Integer.Parse(PageSize)
        Dim tmpCurrent As Integer = CInt(CurrentPage)

        If tmpTongso > 0 Then
            Dim recordFrom As Integer = (tmpCurrent - 1) * tmpPagesize + 1
            If recordFrom <= 0 Then recordFrom = 1
            Dim recordTo As Integer = tmpCurrent * tmpPagesize
            If recordTo > tmpTongso Then recordTo = tmpTongso
            lblBanGhiFrom.Text = recordFrom.ToString()
            lblBanGhiTo.Text = recordTo.ToString()
            lblTongSoBanGhi.Text = TongSoBanGhi
            plhShowing.Visible = True
            '
            If tmpPagesize > 0 Then
                totalPage = Math.Ceiling(tmpTongso / tmpPagesize) 'làm tròn lên
            Else
                totalPage = 1
            End If
        Else
            plhShowing.Visible = False
            '
            totalPage = 1
        End If

        Dim listDrops As List(Of DropDownListInfo) = New List(Of DropDownListInfo)
        Dim listDrop As DropDownListInfo = New DropDownListInfo()

        listDrop.CurrentCss = "current"
        listDrop.Text = CurrentPage
        listDrop.Value = CurrentPage
        listDrop.Href = getUrl(CurrentPage)
        listDrops.Add(listDrop)

        rptCurrentPage.DataSource = listDrops
        rptCurrentPage.DataBind()

        If CurrentPage = "1" Then
            _previous.CssClass = "paginate_button previous disabled"
            _previous.Enabled = False
        Else
            _previous.CssClass = "paginate_button previous"
            _previous.Enabled = True
        End If
        If Integer.Parse(CurrentPage) >= totalPage Then
            _next.CssClass = "paginate_button next disabled"
            _next.Enabled = False
        Else
            _next.CssClass = "paginate_button next"
            _next.Enabled = True
        End If

    End Sub

End Class
