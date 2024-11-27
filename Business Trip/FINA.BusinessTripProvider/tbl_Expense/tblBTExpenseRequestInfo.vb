
Namespace Provider

    ''' <summary>
    ''' The Info class for tbl_Users
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class tblBTExpenseRequestInfo

#Region "Private Members"
        Private _ID As Integer
        Private _BTExpenseID As Integer
        Private _Purpose As String
        Private _Date As DateTime
        Private _BreakfastAmount As Decimal
        Private _LunchAmount As Decimal
        Private _DinnerAmount As Decimal
        Private _OtherAmount As Decimal
        Private _AllowanceCurrency As String
        Private _AllowanceExrate As Decimal
        Private _HotelAmount As Decimal
        Private _HotelCurrency As String
        Private _HotelExrate As Decimal
        Private _Remark As String
        Private _CreatedBy As String
        Private _CreatedDate As DateTime
        Private _ModifiedBy As String
        Private _ModifiedDate As DateTime
        Private _CreditAmount As Decimal
        Private _HotelCreditAmount As Decimal
        Private _HotelExdate As DateTime
        Private _DestinationID As Integer
        Private _oFromDate As DateTime
        Private _oToDate As DateTime
        Private _oTaxiTime As Integer
        Private _oMotobikeTime As Integer
        Private _oTaxiAmount As Decimal
        Private _oMotobikeAmount As Decimal
        Private _oCarRequest As Boolean
        Private _oAirTicketRequest As Boolean
        Private _oTrainTicketRequest As Boolean
        Private _oMovingTimeAllowance As Decimal
        Private _oMovingTimeAllowanceCurrency As String
        Private _oTaxiDesc As String
        Private _oMotobikeDesc As String
#End Region

#Region "Public Properties"
        Public Property oTaxiDesc() As String
            Get
                Return _oTaxiDesc
            End Get
            Set(ByVal value As String)
                _oTaxiDesc = value
            End Set
        End Property
        Public Property oMotobikeDesc() As String
            Get
                Return _oMotobikeDesc
            End Get
            Set(ByVal value As String)
                _oMotobikeDesc = value
            End Set
        End Property
        Public Property oMovingTimeAllowanceCurrency() As String
            Get
                Return _oMovingTimeAllowanceCurrency
            End Get
            Set(ByVal value As String)
                _oMovingTimeAllowanceCurrency = value
            End Set
        End Property
        Public Property oMovingTimeAllowance() As Decimal
            Get
                Return _oMovingTimeAllowance
            End Get
            Set(ByVal value As Decimal)
                _oMovingTimeAllowance = value
            End Set
        End Property
        Public Property oAirTicketRequest() As Boolean
            Get
                Return _oAirTicketRequest
            End Get
            Set(ByVal value As Boolean)
                _oAirTicketRequest = value
            End Set
        End Property
        Public Property oTrainTicketRequest() As Boolean
            Get
                Return _oTrainTicketRequest
            End Get
            Set(ByVal value As Boolean)
                _oTrainTicketRequest = value
            End Set
        End Property
        Public Property oCarRequest() As Boolean
            Get
                Return _oCarRequest
            End Get
            Set(ByVal value As Boolean)
                _oCarRequest = value
            End Set
        End Property
        Public Property oMotobikeAmount() As Decimal
            Get
                Return _oMotobikeAmount
            End Get
            Set(ByVal value As Decimal)
                _oMotobikeAmount = value
            End Set
        End Property
        Public Property oTaxiAmount() As Decimal
            Get
                Return _oTaxiAmount
            End Get
            Set(ByVal value As Decimal)
                _oTaxiAmount = value
            End Set
        End Property
        Public Property oFromDate() As DateTime
            Get
                Return _oFromDate
            End Get
            Set(ByVal value As DateTime)
                _oFromDate = value
            End Set
        End Property
        Public Property oToDate() As DateTime
            Get
                Return _oToDate
            End Get
            Set(ByVal value As DateTime)
                _oToDate = value
            End Set
        End Property
        Public Property oTaxiTime() As Integer
            Get
                Return _oTaxiTime
            End Get
            Set(ByVal value As Integer)
                _oTaxiTime = value
            End Set
        End Property
        Public Property oMotobikeTime() As Integer
            Get
                Return _oMotobikeTime
            End Get
            Set(ByVal value As Integer)
                _oMotobikeTime = value
            End Set
        End Property
        Public Property DestinationID() As Integer
            Get
                Return _DestinationID
            End Get
            Set(ByVal value As Integer)
                _DestinationID = value
            End Set
        End Property
        Public Property HotelExdate() As DateTime
            Get
                Return _HotelExdate
            End Get
            Set(ByVal value As DateTime)
                _HotelExdate = value
            End Set
        End Property
        Public Property CreditAmount() As Decimal
            Get
                Return _CreditAmount
            End Get
            Set(ByVal value As Decimal)
                _CreditAmount = value
            End Set
        End Property

        Public Property HotelCreditAmount() As Decimal
            Get
                Return _HotelCreditAmount
            End Get
            Set(ByVal value As Decimal)
                _HotelCreditAmount = value
            End Set
        End Property

        Public Property BreakfastAmount() As Decimal
            Get
                Return _BreakfastAmount
            End Get
            Set(ByVal value As Decimal)
                _BreakfastAmount = value
            End Set
        End Property

        Public Property LunchAmount() As Decimal
            Get
                Return _LunchAmount
            End Get
            Set(ByVal value As Decimal)
                _LunchAmount = value
            End Set
        End Property

        Public Property DinnerAmount() As Decimal
            Get
                Return _DinnerAmount
            End Get
            Set(ByVal value As Decimal)
                _DinnerAmount = value
            End Set
        End Property

        Public Property OtherAmount() As Decimal
            Get
                Return _OtherAmount
            End Get
            Set(ByVal value As Decimal)
                _OtherAmount = value
            End Set
        End Property

        Public Property AllowanceCurrency() As String
            Get
                Return _AllowanceCurrency
            End Get
            Set(ByVal value As String)
                _AllowanceCurrency = value
            End Set
        End Property

        Public Property AllowanceExrate() As Decimal
            Get
                Return _AllowanceExrate
            End Get
            Set(ByVal value As Decimal)
                _AllowanceExrate = value
            End Set
        End Property

        Public Property HotelAmount() As Decimal
            Get
                Return _HotelAmount
            End Get
            Set(ByVal value As Decimal)
                _HotelAmount = value
            End Set
        End Property

        Public Property HotelCurrency() As String
            Get
                Return _HotelCurrency
            End Get
            Set(ByVal value As String)
                _HotelCurrency = value
            End Set
        End Property

        Public Property HotelExrate() As Decimal
            Get
                Return _HotelExrate
            End Get
            Set(ByVal value As Decimal)
                _HotelExrate = value
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

        Public Property BTExpenseID() As Integer
            Get
                Return _BTExpenseID
            End Get
            Set(ByVal value As Integer)
                _BTExpenseID = value
            End Set
        End Property

        Public Property Purpose() As String
            Get
                Return _Purpose
            End Get
            Set(ByVal value As String)
                _Purpose = value
            End Set
        End Property

        Public Property DDate() As DateTime
            Get
                Return _Date
            End Get
            Set(ByVal value As DateTime)
                _Date = value
            End Set
        End Property

        Public Property Remark() As String
            Get
                Return _Remark
            End Get
            Set(ByVal value As String)
                _Remark = value
            End Set
        End Property

        Public Property CreatedBy() As String
            Get
                Return _CreatedBy
            End Get
            Set(ByVal value As String)
                _CreatedBy = value
            End Set
        End Property

        Public Property CreatedDate() As DateTime
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As DateTime)
                _CreatedDate = value
            End Set
        End Property

        Public Property ModifiedBy() As String
            Get
                Return _ModifiedBy
            End Get
            Set(ByVal value As String)
                _ModifiedBy = value
            End Set
        End Property

        Public Property ModifiedDate() As DateTime
            Get
                Return _ModifiedDate
            End Get
            Set(ByVal value As DateTime)
                _ModifiedDate = value
            End Set
        End Property

#End Region

    End Class

End Namespace


