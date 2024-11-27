
Namespace Provider

    ''' <summary>
    ''' The Info class for tbl_Users
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class tblBTRegisterRequestInfo

#Region "Private Members"
        Private _ID As Integer
        Private _BTRegisterID As Integer
        Private _DestinationID As Integer
        Private _Purpose As String
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _Remark As String
        Private _CreatedBy As String
        Private _CreatedDate As DateTime
        Private _ModifiedBy As String
        Private _ModifiedDate As DateTime
        'for one day trip        
        Private _RequestDate As DateTime
        Private _MovingTimeAllowance As Decimal
        Private _MovingTimeAllowanceCurrency As String
        Private _IsMovingTimeAllowance As Boolean
#End Region

#Region "Public Properties"
        Public Property MovingTimeAllowanceCurrency() As String
            Get
                Return _MovingTimeAllowanceCurrency
            End Get
            Set(ByVal value As String)
                _MovingTimeAllowanceCurrency = value
            End Set
        End Property
        Public Property IsMovingTimeAllowance() As Boolean
            Get
                Return _IsMovingTimeAllowance
            End Get
            Set(ByVal value As Boolean)
                _IsMovingTimeAllowance = value
            End Set
        End Property
        Public Property MovingTimeAllowance() As Decimal
            Get
                Return _MovingTimeAllowance
            End Get
            Set(ByVal value As Decimal)
                _MovingTimeAllowance = value
            End Set
        End Property
        Public Property RequestDate() As DateTime
            Get
                Return _RequestDate
            End Get
            Set(ByVal value As DateTime)
                _RequestDate = value
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

        Public Property Purpose() As String
            Get
                Return _Purpose
            End Get
            Set(ByVal value As String)
                _Purpose = value
            End Set
        End Property

        Public Property FromDate() As DateTime
            Get
                Return _FromDate
            End Get
            Set(ByVal value As DateTime)
                _FromDate = value
            End Set
        End Property

        Public Property ToDate() As DateTime
            Get
                Return _ToDate
            End Get
            Set(ByVal value As DateTime)
                _ToDate = value
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

        Public Property DestinationID() As Integer
            Get
                Return _DestinationID
            End Get
            Set(ByVal value As Integer)
                _DestinationID = value
            End Set
        End Property

#End Region

    End Class

End Namespace


