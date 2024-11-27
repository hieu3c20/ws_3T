
Namespace Provider

    ''' <summary>
    ''' The Info class for 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class tblBTExpenseScheduleInfo

#Region "Private Members"
        Private _ID As Integer
        Private _BTExpenseID As Integer
        Private _FromTime As DateTime
        Private _ToTime As DateTime
        Private _WorkingArea As String
        Private _Task As String
        Private _CreatedBy As String
        Private _CreatedDate As DateTime
        Private _ModifiedBy As String
        Private _ModifiedDate As DateTime
        Private _TransportationFee As Decimal
#End Region

#Region "Public Properties"
        Public Property TransportationFee() As Decimal
            Get
                Return _TransportationFee
            End Get
            Set(ByVal value As Decimal)
                _TransportationFee = value
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


