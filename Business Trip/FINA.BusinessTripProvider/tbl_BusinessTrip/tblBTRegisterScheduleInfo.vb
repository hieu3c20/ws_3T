
Namespace Provider

    ''' <summary>
    ''' The Info class for 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class tblBTRegisterScheduleInfo

#Region "Private Members"
        Private _ID As Integer
        Private _BTRegisterID As Integer
        Private _FromTime As DateTime
        Private _ToTime As DateTime
        Private _WorkingArea As String
        Private _Task As String
        Private _EstimateTransportationFee As Decimal
        Private _CreatedBy As String
        Private _CreatedDate As DateTime
        Private _ModifiedBy As String
        Private _ModifiedDate As DateTime
        Private _AirTicket As Boolean
        Private _TrainTicket As Boolean
        Private _Car As Boolean
#End Region

#Region "Public Properties"
        Public Property AirTicket() As Boolean
            Get
                Return _AirTicket
            End Get
            Set(ByVal value As Boolean)
                _AirTicket = value
            End Set
        End Property
        Public Property TrainTicket() As Boolean
            Get
                Return _TrainTicket
            End Get
            Set(ByVal value As Boolean)
                _TrainTicket = value
            End Set
        End Property
        Public Property Car() As Boolean
            Get
                Return _Car
            End Get
            Set(ByVal value As Boolean)
                _Car = value
            End Set
        End Property
        Public Property EstimateTransportationFee() As Decimal
            Get
                Return _EstimateTransportationFee
            End Get
            Set(ByVal value As Decimal)
                _EstimateTransportationFee = value
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
        Public Property FromTime() As DateTime
            Get
                Return _FromTime
            End Get
            Set(ByVal value As DateTime)
                _FromTime = value
            End Set
        End Property
        Public Property ToTime() As DateTime
            Get
                Return _ToTime
            End Get
            Set(ByVal value As DateTime)
                _ToTime = value
            End Set
        End Property

        Public Property WorkingArea() As String
            Get
                Return _WorkingArea
            End Get
            Set(ByVal value As String)
                _WorkingArea = value
            End Set
        End Property

        Public Property Task() As String
            Get
                Return _Task
            End Get
            Set(ByVal value As String)
                _Task = value
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

#Region "Constructors"

        Public Sub New()
        End Sub

#End Region

    End Class

End Namespace


