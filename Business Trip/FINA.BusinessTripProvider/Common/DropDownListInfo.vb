
Namespace Provider

    Public Class DropDownListInfo

#Region "Private Members"

        Private _id As Integer
        Private _CurrentCss As String = String.Empty
        Private _text As String = String.Empty
        Private _value As String = String.Empty
        Private _maxShow As String = String.Empty
        Private _href As String = String.Empty

#End Region

#Region "Constructors"
        ' initialization
        Public Sub New()
        End Sub

        Public Sub New(ByVal id As Integer, ByVal text As String, ByVal value As String, ByVal maxShow As String, ByVal CurrentCss As String, ByVal Href As String)
            Me.ID = id
            Me.Text = text
            Me.Value = value
            Me.MaxShow = maxShow
            Me.CurrentCss = CurrentCss
            Me.Href = Href
        End Sub

#End Region

#Region "Public Properties"
        Public Property ID() As Integer
            Get
                Return _id
            End Get
            Set(ByVal Value As Integer)
                _id = Value
            End Set
        End Property

        Public Property Text() As String
            Get
                Return _text
            End Get
            Set(ByVal Value As String)
                _text = Value
            End Set
        End Property

        Public Property Value() As String
            Get
                Return _value
            End Get
            Set(ByVal Value As String)
                _value = Value
            End Set
        End Property

        Public Property MaxShow() As String
            Get
                If _maxShow = "" Then
                    Return "6"
                Else
                    Return _maxShow
                End If
            End Get
            Set(ByVal Value As String)
                _maxShow = Value
            End Set
        End Property

        Public Property CurrentCss() As String
            Get
                Return _CurrentCss
            End Get
            Set(ByVal Value As String)
                _CurrentCss = Value
            End Set
        End Property

        Public Property Href() As String
            Get
                Return _href
            End Get
            Set(ByVal Value As String)
                _href = Value
            End Set
        End Property

#End Region

    End Class

End Namespace

