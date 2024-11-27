
Namespace Provider

    ''' <summary>
    ''' The Info class for tbl_BT_Register
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class tblBTRegisterInfo

#Region "Private Members"
        Private _BTRegisterID As Integer
        Private _EmployeeCode As String
        Private _EmployeeName As String
        Private _BTType As String
        Private _Location As String
        Private _LocationID As Integer
        Private _Department As String
        Private _DepartmentID As Integer
        Private _Division As String
        Private _DivisionID As Integer
        Private _Section As String
        Private _SectionID As Integer
        Private _Group As String
        Private _GroupID As Integer
        Private _Position As String
        Private _BudgetCode As String
        Private _BudgetName As String
        Private _IsSaved As Boolean
        Private _IsSubmited As Boolean
        Private _HRStatus As String
        Private _HRModifiedBy As String
        Private _HRModifiedDate As DateTime
        Private _GAStatus As String
        Private _GAModifiedBy As String
        Private _GAModifiedDate As DateTime
        Private _FIStatus As String
        Private _CreatedBy As String
        Private _CreatedDate As DateTime
        Private _ModifiedBy As String
        Private _ModifiedDate As DateTime
        Private _FIModifiedBy As String
        Private _FIModifiedDate As DateTime
        Private _Currency As String
        Private _RequestDate As DateTime
        Private _PaymentType As String
        Private _FirstTimeOverSea As Decimal
        Private _FirstTimeOverSeaVND As Decimal
        Private _IsFirstTimeOverSea As Boolean
        Private _Mobile As String
        Private _Email As String
        Private _CountryCode As String
        Private _Purpose As String
        Private _DepartureDate As DateTime
        Private _ReturnDate As DateTime
        Private _MovingTimeAllowance As Decimal
        Private _MovingTimeAllowanceVND As Decimal
        Private _IsMovingTimeAllowance As Boolean
        Private _SubmitComment As String
        Private _ProjectBudgetCode As String
        Private _CheckAllBudget As Boolean
        Private _BudgetChecked As Boolean
        Private _BudgetCodeID As Integer
        Private _NoRequestAdvance As Boolean
        Private _AirTicket As Boolean
        Private _TrainTicket As Boolean
        Private _Car As Boolean
        Private _Wifi As Boolean
        Private _ExpectedDepartureTime As DateTime
        Private _ExpectedDepartureFlightNo As String
        Private _ExpectedReturnTime As DateTime
        Private _ExpectedReturnFlightNo As String
        Private _DestinationID As Integer
#End Region

#Region "Public Properties"
        Public Property DestinationID() As Integer
            Get
                Return _DestinationID
            End Get
            Set(ByVal value As Integer)
                _DestinationID = value
            End Set
        End Property

        Public Property NoRequestAdvance() As Boolean
            Get
                Return _NoRequestAdvance
            End Get
            Set(ByVal value As Boolean)
                _NoRequestAdvance = value
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

        Public Property Wifi() As Boolean
            Get
                Return _Wifi
            End Get
            Set(ByVal value As Boolean)
                _Wifi = value
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

        Public Property AirTicket() As Boolean
            Get
                Return _AirTicket
            End Get
            Set(ByVal value As Boolean)
                _AirTicket = value
            End Set
        End Property

        Public Property ExpectedDepartureTime() As DateTime
            Get
                Return _ExpectedDepartureTime
            End Get
            Set(ByVal value As DateTime)
                _ExpectedDepartureTime = value
            End Set
        End Property

        Public Property ExpectedDepartureFlightNo() As String
            Get
                Return _ExpectedDepartureFlightNo
            End Get
            Set(ByVal value As String)
                _ExpectedDepartureFlightNo = value
            End Set
        End Property

        Public Property ExpectedReturnTime() As DateTime
            Get
                Return _ExpectedReturnTime
            End Get
            Set(ByVal value As DateTime)
                _ExpectedReturnTime = value
            End Set
        End Property

        Public Property ExpectedReturnFlightNo() As String
            Get
                Return _ExpectedReturnFlightNo
            End Get
            Set(ByVal value As String)
                _ExpectedReturnFlightNo = value
            End Set
        End Property

        Public Property BudgetCodeID() As Integer
            Get
                Return _BudgetCodeID
            End Get
            Set(ByVal value As Integer)
                _BudgetCodeID = value
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

        Public Property BudgetChecked() As Boolean
            Get
                Return _BudgetChecked
            End Get
            Set(ByVal value As Boolean)
                _BudgetChecked = value
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

        Public Property CountryCode() As String
            Get
                Return _CountryCode
            End Get
            Set(ByVal value As String)
                _CountryCode = value
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

        Public Property DepartureDate() As DateTime
            Get
                Return _DepartureDate
            End Get
            Set(ByVal value As DateTime)
                _DepartureDate = value
            End Set
        End Property

        Public Property ReturnDate() As DateTime
            Get
                Return _ReturnDate
            End Get
            Set(ByVal value As DateTime)
                _ReturnDate = value
            End Set
        End Property

        Public Property Email() As String
            Get
                Return _Email
            End Get
            Set(ByVal value As String)
                _Email = value
            End Set
        End Property

        Public Property Mobile() As String
            Get
                Return _Mobile
            End Get
            Set(ByVal value As String)
                _Mobile = value
            End Set
        End Property

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

        Public Property FirstTimeOverSeaVND() As Decimal
            Get
                Return _FirstTimeOverSeaVND
            End Get
            Set(ByVal value As Decimal)
                _FirstTimeOverSeaVND = value
            End Set
        End Property

        Public Property BTRegisterID() As Integer
            Get
                Return _BTRegisterID
            End Get
            Set(ByVal Value As Integer)
                _BTRegisterID = Value
            End Set
        End Property

        Public Property EmployeeCode() As String
            Get
                Return _EmployeeCode
            End Get
            Set(ByVal Value As String)
                _EmployeeCode = Value
            End Set
        End Property

        Public Property BTType() As String
            Get
                Return _BTType
            End Get
            Set(ByVal Value As String)
                _BTType = Value
            End Set
        End Property

        Public Property Location() As String
            Get
                Return _Location
            End Get
            Set(ByVal Value As String)
                _Location = Value
            End Set
        End Property

        Public Property LocationID() As Integer
            Get
                Return _LocationID
            End Get
            Set(ByVal Value As Integer)
                _LocationID = Value
            End Set
        End Property

        Public Property Section() As String
            Get
                Return _Section
            End Get
            Set(ByVal Value As String)
                _Section = Value
            End Set
        End Property

        Public Property SectionID() As Integer
            Get
                Return _SectionID
            End Get
            Set(ByVal Value As Integer)
                _SectionID = Value
            End Set
        End Property

        Public Property Group() As String
            Get
                Return _Group
            End Get
            Set(ByVal Value As String)
                _Group = Value
            End Set
        End Property

        Public Property GroupID() As Integer
            Get
                Return _GroupID
            End Get
            Set(ByVal Value As Integer)
                _GroupID = Value
            End Set
        End Property

        Public Property Department() As String
            Get
                Return _Department
            End Get
            Set(ByVal Value As String)
                _Department = Value
            End Set
        End Property

        Public Property DepartmentID() As Integer
            Get
                Return _DepartmentID
            End Get
            Set(ByVal Value As Integer)
                _DepartmentID = Value
            End Set
        End Property

        Public Property Division() As String
            Get
                Return _Division
            End Get
            Set(ByVal Value As String)
                _Division = Value
            End Set
        End Property

        Public Property DivisionID() As Integer
            Get
                Return _DivisionID
            End Get
            Set(ByVal Value As Integer)
                _DivisionID = Value
            End Set
        End Property

        Public Property Position() As String
            Get
                Return _Position
            End Get
            Set(ByVal Value As String)
                _Position = Value
            End Set
        End Property

        Public Property BudgetCode() As String
            Get
                Return _BudgetCode
            End Get
            Set(ByVal Value As String)
                _BudgetCode = Value
            End Set
        End Property

        Public Property BudgetName() As String
            Get
                Return _BudgetName
            End Get
            Set(ByVal Value As String)
                _BudgetName = Value
            End Set
        End Property

        Public Property IsSaved() As Boolean
            Get
                Return _IsSaved
            End Get
            Set(ByVal Value As Boolean)
                _IsSaved = Value
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

        Public Property HRStatus() As String
            Get
                Return _HRStatus
            End Get
            Set(ByVal Value As String)
                _HRStatus = Value
            End Set
        End Property

        Public Property HRModifiedBy() As String
            Get
                Return _HRModifiedBy
            End Get
            Set(ByVal Value As String)
                _HRModifiedBy = Value
            End Set
        End Property

        Public Property HRModifiedDate() As DateTime
            Get
                Return _HRModifiedDate
            End Get
            Set(ByVal Value As DateTime)
                _HRModifiedDate = Value
            End Set
        End Property

        Public Property RequestDate() As DateTime
            Get
                Return _RequestDate
            End Get
            Set(ByVal Value As DateTime)
                _RequestDate = Value
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

        Public Property Currency() As String
            Get
                Return _Currency
            End Get
            Set(ByVal Value As String)
                _Currency = Value
            End Set
        End Property


        Public Property EmployeeName() As String
            Get
                Return _EmployeeName
            End Get
            Set(ByVal Value As String)
                _EmployeeName = Value
            End Set
        End Property

        Public Property PaymentType() As String
            Get
                Return _PaymentType
            End Get
            Set(ByVal Value As String)
                _PaymentType = Value
            End Set
        End Property
#End Region

#Region "Constructors"

        Public Sub New()
        End Sub

#End Region

    End Class

End Namespace


