
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class mMovingTimeAllowanceInfo

#Region "Private Members"

        Private _ID As String
        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                _ID = value
            End Set
        End Property

        Private _CountryGroup As Integer
        Public Property CountryGroup() As Integer
            Get
                Return _CountryGroup
            End Get
            Set(ByVal value As Integer)
                _CountryGroup = value
            End Set
        End Property


        Private _Amount As Double
        Public Property Amount() As Double
            Get
                Return _Amount
            End Get
            Set(ByVal value As Double)
                _Amount = value
            End Set
        End Property

        Private _Currency As String
        Public Property Currency() As String
            Get
                Return _Currency
            End Get
            Set(ByVal value As String)
                _Currency = value
            End Set
        End Property


        Private _Description As String
        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

#End Region

#Region "Public Properties"        

#End Region

    End Class

End Namespace


