
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class tblBTRegisterRequestDetailsInfo

#Region "Private Members"
        Private _ID As Integer
        Private _BTRegisterID As Integer
        Private _BTRegisterRequestID As Integer
        Private _BreakfastQty As Integer
        Private _LunchQty As Integer
        Private _DinnerQty As Integer
        Private _BreakfastUnit As Decimal
        Private _LunchUnit As Decimal
        Private _DinnerUnit As Decimal
        Private _Other As String
        Private _OtherQty As Integer
        Private _OtherUnit As Decimal
        Private _HotelQty As Integer
        Private _HotelUnit As Decimal
        Private _TotalAmount As Decimal
        Private _OtherMealQty As Integer
        Private _OtherMealUnit As Decimal
        Private _TaxiQty As Integer
        Private _TaxiAmount As Decimal
        Private _MotobikeQty As Integer
        Private _MotobikeAmount As Decimal
        Private _CarRequest As Boolean
        Private _TaxiDesc As String
        Private _MotobikeDesc As String
        Private _AirTicketRequest As Boolean
        Private _TrainTicketRequest As Boolean
#End Region

#Region "Public Properties"
        Public Property TaxiDesc() As String
            Get
                Return _TaxiDesc
            End Get
            Set(ByVal value As String)
                _TaxiDesc = value
            End Set
        End Property
        Public Property MotobikeDesc() As String
            Get
                Return _MotobikeDesc
            End Get
            Set(ByVal value As String)
                _MotobikeDesc = value
            End Set
        End Property
        Public Property AirTicketRequest() As Boolean
            Get
                Return _AirTicketRequest
            End Get
            Set(ByVal value As Boolean)
                _AirTicketRequest = value
            End Set
        End Property
        Public Property TrainTicketRequest() As Boolean
            Get
                Return _TrainTicketRequest
            End Get
            Set(ByVal value As Boolean)
                _TrainTicketRequest = value
            End Set
        End Property
        Public Property CarRequest() As Boolean
            Get
                Return _CarRequest
            End Get
            Set(ByVal value As Boolean)
                _CarRequest = value
            End Set
        End Property
        Public Property TaxiQty() As Integer
            Get
                Return _TaxiQty
            End Get
            Set(ByVal value As Integer)
                _TaxiQty = value
            End Set
        End Property
        Public Property MotobikeQty() As Integer
            Get
                Return _MotobikeQty
            End Get
            Set(ByVal value As Integer)
                _MotobikeQty = value
            End Set
        End Property
        Public Property TaxiAmount() As Decimal
            Get
                Return _TaxiAmount
            End Get
            Set(ByVal value As Decimal)
                _TaxiAmount = value
            End Set
        End Property
        Public Property MotobikeAmount() As Decimal
            Get
                Return _MotobikeAmount
            End Get
            Set(ByVal value As Decimal)
                _MotobikeAmount = value
            End Set
        End Property
        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property
        Public Property BTRegisterID() As Integer
            Get
                Return _BTRegisterID
            End Get
            Set(ByVal value As Integer)
                _BTRegisterID = value
            End Set
        End Property
        Public Property BTRegisterRequestID() As Integer
            Get
                Return _BTRegisterRequestID
            End Get
            Set(ByVal value As Integer)
                _BTRegisterRequestID = value
            End Set
        End Property
        Public Property BreakfastQty() As Integer
            Get
                Return _BreakfastQty
            End Get
            Set(ByVal value As Integer)
                _BreakfastQty = value
            End Set
        End Property
        Public Property LunchQty() As Integer
            Get
                Return _LunchQty
            End Get
            Set(ByVal value As Integer)
                _LunchQty = value
            End Set
        End Property
        Public Property DinnerQty() As Integer
            Get
                Return _DinnerQty
            End Get
            Set(ByVal value As Integer)
                _DinnerQty = value
            End Set
        End Property

        Public Property OtherMealQty() As Integer
            Get
                Return _OtherMealQty
            End Get
            Set(ByVal value As Integer)
                _OtherMealQty = value
            End Set
        End Property

        Public Property BreakfastUnit() As Decimal
            Get
                Return _BreakfastUnit
            End Get
            Set(ByVal value As Decimal)
                _BreakfastUnit = value
            End Set
        End Property
        Public Property LunchUnit() As Decimal
            Get
                Return _LunchUnit
            End Get
            Set(ByVal value As Decimal)
                _LunchUnit = value
            End Set
        End Property
        Public Property DinnerUnit() As Decimal
            Get
                Return _DinnerUnit
            End Get
            Set(ByVal value As Decimal)
                _DinnerUnit = value
            End Set
        End Property

        Public Property OtherMealUnit() As Decimal
            Get
                Return _OtherMealUnit
            End Get
            Set(ByVal value As Decimal)
                _OtherMealUnit = value
            End Set
        End Property

        Public Property Other() As String
            Get
                Return _Other
            End Get
            Set(ByVal value As String)
                _Other = value
            End Set
        End Property

        Public Property OtherQty() As Integer
            Get
                Return _OtherQty
            End Get
            Set(ByVal value As Integer)
                _OtherQty = value
            End Set
        End Property

        Public Property OtherUnit() As Decimal
            Get
                Return _OtherUnit
            End Get
            Set(ByVal value As Decimal)
                _OtherUnit = value
            End Set
        End Property

        Public Property HotelQty() As Integer
            Get
                Return _HotelQty
            End Get
            Set(ByVal value As Integer)
                _HotelQty = value
            End Set
        End Property

        Public Property HotelUnit() As Decimal
            Get
                Return _HotelUnit
            End Get
            Set(ByVal value As Decimal)
                _HotelUnit = value
            End Set
        End Property

        Public Property TotalAmount() As Decimal
            Get
                Return _TotalAmount
            End Get
            Set(ByVal value As Decimal)
                _TotalAmount = value
            End Set
        End Property
#End Region

    End Class

End Namespace


