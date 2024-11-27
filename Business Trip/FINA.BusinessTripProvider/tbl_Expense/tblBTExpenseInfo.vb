
Namespace Provider

    ''' <summary>
    ''' The Info class for tbl_BT_Register
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class tblBTExpenseInfo

#Region "Private Members"
        Private _BTExpenseID As Integer
        Private _IsSubmited As Boolean
        Private _GAStatus As String
        Private _GAModifiedBy As String
        Private _GAModifiedDate As DateTime
        Private _FIStatus As String
        Private _FIModifiedBy As String
        Private _FIModifiedDate As DateTime
        Private _CreatedBy As String
        Private _CreatedDate As DateTime
        Private _ModifiedBy As String
        Private _ModifiedDate As DateTime
        Private _RejectReasonGA As String
        Private _RejectReasonFI As String

        Private _DepartureDate As DateTime
        Private _ReturnDate As DateTime
        Private _CountryCode As String
        Private _Purpose As String
        Private _ExchangeDate As DateTime
        Private _Currency As String
        Private _TitleGroup As String

        Private _MovingTimeAllowance As Decimal
        Private _MovingTimeAllowanceVND As Decimal
        Private _IsMovingTimeAllowance As Boolean

        Private _SubmitComment As String

        Private _BudgetCode As String
        Private _BudgetName As String
        Private _CheckAllBudget As Boolean
        Private _ProjectBudgetCode As String

        Private _FirstTimeOverSea As Decimal
        Private _IsFirstTimeOverSea As Boolean
#End Region

#Region "Public Properties"
        Public Property IsFirstTimeOverSea() As Boolean
            Get
                Return _IsFirstTimeOverSea
            End Get
            Set(ByVal value As Boolean)
                _IsFirstTimeOverSea = value
            End Set
        End Property

        Public Property FirstTimeOverSea() As Decimal
            Get
                Return _FirstTimeOverSea
            End Get
            Set(ByVal value As Decimal)
                _FirstTimeOverSea = value
            End Set
        End Property

        Public Property CheckAllBudget() As Boolean
            Get
                Return _CheckAllBudget
            End Get
            Set(ByVal value As Boolean)
                _CheckAllBudget = value
            End Set
        End Property
        Public Property ProjectBudgetCode() As String
            Get
                Return _ProjectBudgetCode
            End Get
            Set(ByVal value As String)
                _ProjectBudgetCode = value
            End Set
        End Property

        Public Property BudgetName() As String
            Get
                Return _BudgetName
            End Get
            Set(ByVal value As String)
                _BudgetName = value
            End Set
        End Property

        Public Property BudgetCode() As String
            Get
                Return _BudgetCode
            End Get
            Set(ByVal value As String)
                _BudgetCode = value
            End Set
        End Property

        Public Property SubmitComment() As String
            Get
                Return _SubmitComment
            End Get
            Set(ByVal value As String)
                _SubmitComment = value
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

        Public Property MovingTimeAllowanceVND() As Decimal
            Get
                Return _MovingTimeAllowanceVND
            End Get
            Set(ByVal value As Decimal)
                _MovingTimeAllowanceVND = value
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
        Public Property BTExpenseID() As Integer
            Get
                Return _BTExpenseID
            End Get
            Set(ByVal Value As Integer)
                _BTExpenseID = Value
            End Set
        End Property

        Public Property IsSubmited() As Boolean
            Get
                Return _IsSubmited
            End Get
            Set(ByVal Value As Boolean)
                _IsSubmited = Value
            End Set
        End Property

        Public Property CountryCode() As String
            Get
                Return _CountryCode
            End Get
            Set(ByVal Value As String)
                _CountryCode = Value
            End Set
        End Property

        Public Property Purpose() As String
            Get
                Return _Purpose
            End Get
            Set(ByVal Value As String)
                _Purpose = Value
            End Set
        End Property

        Public Property DepartureDate() As DateTime
            Get
                Return _DepartureDate
            End Get
            Set(ByVal Value As DateTime)
                _DepartureDate = Value
            End Set
        End Property

        Public Property ReturnDate() As DateTime
            Get
                Return _ReturnDate
            End Get
            Set(ByVal Value As DateTime)
                _ReturnDate = Value
            End Set
        End Property

        Public Property GAStatus() As String
            Get
                Return _GAStatus
            End Get
            Set(ByVal Value As String)
                _GAStatus = Value
            End Set
        End Property

        Public Property GAModifiedBy() As String
            Get
                Return _GAModifiedBy
            End Get
            Set(ByVal Value As String)
                _GAModifiedBy = Value
            End Set
        End Property

        Public Property GAModifiedDate() As DateTime
            Get
                Return _GAModifiedDate
            End Get
            Set(ByVal Value As DateTime)
                _GAModifiedDate = Value
            End Set
        End Property

        Public Property FIStatus() As String
            Get
                Return _FIStatus
            End Get
            Set(ByVal Value As String)
                _FIStatus = Value
            End Set
        End Property

        Public Property FIModifiedBy() As String
            Get
                Return _FIModifiedBy
            End Get
            Set(ByVal Value As String)
                _FIModifiedBy = Value
            End Set
        End Property

        Public Property FIModifiedDate() As DateTime
            Get
                Return _FIModifiedDate
            End Get
            Set(ByVal Value As DateTime)
                _FIModifiedDate = Value
            End Set
        End Property

        Public Property CreatedBy() As String
            Get
                Return _CreatedBy
            End Get
            Set(ByVal Value As String)
                _CreatedBy = Value
            End Set
        End Property

        Public Property CreatedDate() As DateTime
            Get
                Return _CreatedDate
            End Get
            Set(ByVal Value As DateTime)
                _CreatedDate = Value
            End Set
        End Property

        Public Property ModifiedBy() As String
            Get
                Return _ModifiedBy
            End Get
            Set(ByVal Value As String)
                _ModifiedBy = Value
            End Set
        End Property

        Public Property ModifiedDate() As DateTime
            Get
                Return _ModifiedDate
            End Get
            Set(ByVal Value As DateTime)
                _ModifiedDate = Value
            End Set
        End Property

        Public Property ExchangeDate() As DateTime
            Get
                Return _ExchangeDate
            End Get
            Set(ByVal Value As DateTime)
                _ExchangeDate = Value
            End Set
        End Property

        Public Property RejectReasonGA() As String
            Get
                Return _RejectReasonGA
            End Get
            Set(ByVal Value As String)
                _RejectReasonGA = Value
            End Set
        End Property

        Public Property RejectReasonFI() As String
            Get
                Return _RejectReasonFI
            End Get
            Set(ByVal Value As String)
                _RejectReasonFI = Value
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

        Public Property TitleGroup() As String
            Get
                Return _TitleGroup
            End Get
            Set(ByVal value As String)
                _TitleGroup = value
            End Set
        End Property

#End Region

#Region "Constructors"
        Public Sub New()
        End Sub
#End Region
    End Class

End Namespace


