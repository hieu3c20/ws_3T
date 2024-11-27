
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class mOraDailyRateInfo

#Region "Private Members"
        Private _ID As Integer
        Private _FROM_CURRENCY As String
        Private _TO_CURRENCY As String
        Private _CONVERSION_DATE As Date
        Private _CONVERSION_RATE As Double
        Private _Active As Boolean
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

        Public Property FROM_CURRENCY() As String
            Get
                Return _FROM_CURRENCY
            End Get
            Set(ByVal value As String)
                _FROM_CURRENCY = value
            End Set
        End Property

        Public Property TO_CURRENCY() As String
            Get
                Return _TO_CURRENCY
            End Get
            Set(ByVal value As String)
                _TO_CURRENCY = value
            End Set
        End Property

        Public Property CONVERSION_DATE() As Date
            Get
                Return _CONVERSION_DATE
            End Get
            Set(ByVal value As Date)
                _CONVERSION_DATE = value
            End Set
        End Property

        Public Property CONVERSION_RATE() As Double
            Get
                Return _CONVERSION_RATE
            End Get
            Set(ByVal value As Double)
                _CONVERSION_RATE = value
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


