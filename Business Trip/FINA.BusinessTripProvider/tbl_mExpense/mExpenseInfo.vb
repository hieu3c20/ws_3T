
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class mExpenseInfo

#Region "Private Members"

        Private _ExpenseID As Integer
        Private _TitleID As Integer
        Private _DestinationGroupID As Integer
        Private _Breakfast As Double
        Private _Lunch As Double
        Private _Dinner As Double
        Private _OtherMeal As Double
        Private _Hotel As Double
        Private _Transportation As Double
        Private _Motobike As Double
        Private _Other As Double
        Private _Currency As String
        Private _Note As String
        Private _BTType As String
        Private _EffectiveDate As DateTime
        Private _ExpiredDate As DateTime
#End Region

#Region "Public Properties"
        Public Property EffectiveDate() As DateTime
            Get
                Return _EffectiveDate
            End Get
            Set(ByVal value As DateTime)
                _EffectiveDate = value
            End Set
        End Property

        Public Property ExpiredDate() As DateTime
            Get
                Return _ExpiredDate
            End Get
            Set(ByVal value As DateTime)
                _ExpiredDate = value
            End Set
        End Property

        Public Property BTType() As String
            Get
                Return _BTType
            End Get
            Set(ByVal value As String)
                _BTType = value
            End Set
        End Property

        Public Property ExpenseID() As Integer
            Get
                Return _ExpenseID
            End Get
            Set(ByVal value As Integer)
                _ExpenseID = value
            End Set
        End Property

        Public Property TitleID() As Integer
            Get
                Return _TitleID
            End Get
            Set(ByVal value As Integer)
                _TitleID = value
            End Set
        End Property

        Public Property DestinationGroupID() As Integer
            Get
                Return _DestinationGroupID
            End Get
            Set(ByVal value As Integer)
                _DestinationGroupID = value
            End Set
        End Property

        Public Property Breakfast() As Double
            Get
                Return _Breakfast
            End Get
            Set(ByVal value As Double)
                _Breakfast = value
            End Set
        End Property


        Public Property Lunch() As Double
            Get
                Return _Lunch
            End Get
            Set(ByVal value As Double)
                _Lunch = value
            End Set
        End Property

        Public Property Dinner() As Double
            Get
                Return _Dinner
            End Get
            Set(ByVal value As Double)
                _Dinner = value
            End Set
        End Property

        Public Property OtherMeal() As Double
            Get
                Return _OtherMeal
            End Get
            Set(ByVal value As Double)
                _OtherMeal = value
            End Set
        End Property


        Public Property Hotel() As Double
            Get
                Return _Hotel
            End Get
            Set(ByVal value As Double)
                _Hotel = value
            End Set
        End Property


        Public Property Transportation() As Double
            Get
                Return _Transportation
            End Get
            Set(ByVal value As Double)
                _Transportation = value
            End Set
        End Property

        Public Property Motobike() As Double
            Get
                Return _Motobike
            End Get
            Set(ByVal value As Double)
                _Motobike = value
            End Set
        End Property


        Public Property Other() As Double
            Get
                Return _Other
            End Get
            Set(ByVal value As Double)
                _Other = value
            End Set
        End Property

        Public Property Currency() As String
            Get
                Return _Currency
            End Get
            Set(ByVal value As String)
                _Currency = value
            End Set
        End Property

        Public Property Note() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                _Note = value
            End Set
        End Property

#End Region

    End Class

End Namespace


