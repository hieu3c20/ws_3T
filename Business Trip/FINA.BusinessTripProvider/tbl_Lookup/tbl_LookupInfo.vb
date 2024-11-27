
Namespace Provider

    ''' <summary>
    ''' The Info class for tbl_Lookup
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class tbl_LookupInfo


#Region "Private Members"
        Private _ID As Integer
        Private _Code As String
        Private _Value As String
        Private _Text As String
        Private _DisplayOrder As Integer
        Private _Active As Boolean
#End Region

#Region "Constructors"
        ' initialization
        Public Sub New()
        End Sub
#End Region

#Region "Public Properties"
        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property

        Public Property Code() As String
            Get
                Return _Code
            End Get
            Set(ByVal value As String)
                _Code = value
            End Set
        End Property


        Public Property Value() As String
            Get
                Return _Value
            End Get
            Set(ByVal value As String)
                _Value = value
            End Set
        End Property


        Public Property Text() As String
            Get
                Return _Text
            End Get
            Set(ByVal value As String)
                _Text = value
            End Set
        End Property


        Public Property DisplayOrder() As Integer
            Get
                Return _DisplayOrder
            End Get
            Set(ByVal value As Integer)
                _DisplayOrder = value
            End Set
        End Property

        Public Property Active() As Boolean
            Get
                Return _Active
            End Get
            Set(ByVal value As Boolean)
                _Active = value
            End Set
        End Property
#End Region

    End Class

End Namespace


