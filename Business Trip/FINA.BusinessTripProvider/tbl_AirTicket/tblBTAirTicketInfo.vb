
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class tblBTAirTicketInfo


#Region "Private Members"
        Private _ID As Integer
        Private _BTRegisterID As Integer
        Private _TicketDate As DateTime
        Private _AirLine As String
        Private _TicketNo As String
        Private _Routing As String
        Private _Fare As Decimal
        Private _VAT As Decimal
        Private _APTTax As Decimal
        Private _SF As Decimal
        Private _NetPayment As Decimal
        Private _Currency As String
        Private _Exrate As Decimal
        Private _CreatedBy As String
        Private _CreatedDate As DateTime
        Private _ModifiedBy As String
        Private _ModifiedDate As DateTime
        Private _AirPeriod As Integer
        Private _OraSupplier As String
        Private _DepartureDate As DateTime
        Private _ReturnDate As DateTime
        Private _Source As String
        Private _Passenger As String
        Private _BudgetCode As String
        Private _Purpose As String
        Private _Requester As String
        Private _RequesterDept As String
        Private _RequesterName As String
        Private _RequesterPhone As String
        Private _BudgetChecked As Boolean
        Private _RequesterDiv As String
        Private _ICTRequest As Boolean            

#End Region

#Region "Public Properties"
        Public Property ICTRequest() As Boolean
            Get
                Return _ICTRequest
            End Get
            Set(ByVal value As Boolean)
                _ICTRequest = value
            End Set
        End Property

        Public Property RequesterDiv() As String
            Get
                Return _RequesterDiv
            End Get
            Set(ByVal value As String)
                _RequesterDiv = value
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

        Public Property Purpose() As String
            Get
                Return _Purpose
            End Get
            Set(ByVal value As String)
                _Purpose = value
            End Set
        End Property

        Public Property Requester() As String
            Get
                Return _Requester
            End Get
            Set(ByVal value As String)
                _Requester = value
            End Set
        End Property

        Public Property RequesterDept() As String
            Get
                Return _RequesterDept
            End Get
            Set(ByVal value As String)
                _RequesterDept = value
            End Set
        End Property

        Public Property RequesterName() As String
            Get
                Return _RequesterName
            End Get
            Set(ByVal value As String)
                _RequesterName = value
            End Set
        End Property

        Public Property RequesterPhone() As String
            Get
                Return _RequesterPhone
            End Get
            Set(ByVal value As String)
                _RequesterPhone = value
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

        Public Property Source() As String
            Get
                Return _Source
            End Get
            Set(ByVal value As String)
                _Source = value
            End Set
        End Property

        Public Property Passenger() As String
            Get
                Return _Passenger
            End Get
            Set(ByVal value As String)
                _Passenger = value
            End Set
        End Property

        Public Property AirPeriod() As Integer
            Get
                Return _AirPeriod
            End Get
            Set(ByVal value As Integer)
                _AirPeriod = value
            End Set
        End Property

        Public Property OraSupplier() As String
            Get
                Return _OraSupplier
            End Get
            Set(ByVal value As String)
                _OraSupplier = value
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

        Public Property NetPayment() As Decimal
            Get
                Return _NetPayment
            End Get
            Set(ByVal value As Decimal)
                _NetPayment = value
            End Set
        End Property

        Public Property Exrate() As Decimal
            Get
                Return _Exrate
            End Get
            Set(ByVal value As Decimal)
                _Exrate = value
            End Set
        End Property

        Public Property VAT() As Decimal
            Get
                Return _VAT
            End Get
            Set(ByVal value As Decimal)
                _VAT = value
            End Set
        End Property

        Public Property APTTax() As Decimal
            Get
                Return _APTTax
            End Get
            Set(ByVal value As Decimal)
                _APTTax = value
            End Set
        End Property

        Public Property SF() As Decimal
            Get
                Return _SF
            End Get
            Set(ByVal value As Decimal)
                _SF = value
            End Set
        End Property

        Public Property Fare() As Decimal
            Get
                Return _Fare
            End Get
            Set(ByVal value As Decimal)
                _Fare = value
            End Set
        End Property

        Public Property TicketNo() As String
            Get
                Return _TicketNo
            End Get
            Set(ByVal value As String)
                _TicketNo = value
            End Set
        End Property

        Public Property Routing() As String
            Get
                Return _Routing
            End Get
            Set(ByVal value As String)
                _Routing = value
            End Set
        End Property

        Public Property AirLine() As String
            Get
                Return _AirLine
            End Get
            Set(ByVal value As String)
                _AirLine = value
            End Set
        End Property

        Public Property TicketDate() As DateTime
            Get
                Return _TicketDate
            End Get
            Set(ByVal value As DateTime)
                _TicketDate = value
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

        Public Property ModifiedDate() As DateTime
            Get
                Return _ModifiedDate
            End Get
            Set(ByVal value As DateTime)
                _ModifiedDate = value
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

        Public Property ModifiedBy() As String
            Get
                Return _ModifiedBy
            End Get
            Set(ByVal value As String)
                _ModifiedBy = value
            End Set
        End Property

        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                _ID = value
            End Set
        End Property

        Public Property BTRegisterID() As String
            Get
                Return _BTRegisterID
            End Get
            Set(ByVal value As String)
                _BTRegisterID = value
            End Set
        End Property

#End Region

    End Class

    Public Class tblBTAirTicketRequestInfo
        Private _ID As Integer
        Private _EmployeeCode As String
        Private _EmployeeName As String
        Private _EmployeeDivision As String
        Private _EmployeeDepartment As String
        Private _FromCountry As String
        Private _FromDestination As Integer
        Private _ToCountry As String
        Private _ToDestination As Integer
        Private _DepartureDate As Date
        Private _ReturnDate As Date
        Private _Purpose As String
        Private _CreatedBy As String
        Private _CreatedDate As Date
        Private _ModifiedBy As String
        Private _ModifiedDate As Date
        Private _Status As String
        Private _BudgetCode As String
        '
        Public Property BudgetCode() As String
            Get
                Return _BudgetCode
            End Get
            Set(ByVal value As String)
                _BudgetCode = value
            End Set
        End Property

        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
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

        Public Property CreatedDate() As Date
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As Date)
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

        Public Property ModifiedDate() As Date
            Get
                Return _ModifiedDate
            End Get
            Set(ByVal value As Date)
                _ModifiedDate = value
            End Set
        End Property

        Public Property DepartureDate() As Date
            Get
                Return _DepartureDate
            End Get
            Set(ByVal value As Date)
                _DepartureDate = value
            End Set
        End Property

        Public Property ReturnDate() As Date
            Get
                Return _ReturnDate
            End Get
            Set(ByVal value As Date)
                _ReturnDate = value
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

        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
            End Set
        End Property

        Public Property EmployeeCode() As String
            Get
                Return _EmployeeCode
            End Get
            Set(ByVal value As String)
                _EmployeeCode = value
            End Set
        End Property

        Public Property EmployeeName() As String
            Get
                Return _EmployeeName
            End Get
            Set(ByVal value As String)
                _EmployeeName = value
            End Set
        End Property

        Public Property EmployeeDivision() As String
            Get
                Return _EmployeeDivision
            End Get
            Set(ByVal value As String)
                _EmployeeDivision = value
            End Set
        End Property

        Public Property EmployeeDepartment() As String
            Get
                Return _EmployeeDepartment
            End Get
            Set(ByVal value As String)
                _EmployeeDepartment = value
            End Set
        End Property

        Public Property FromCountry() As String
            Get
                Return _FromCountry
            End Get
            Set(ByVal value As String)
                _FromCountry = value
            End Set
        End Property

        Public Property FromDestination() As Integer
            Get
                Return _FromDestination
            End Get
            Set(ByVal value As Integer)
                _FromDestination = value
            End Set
        End Property

        Public Property ToCountry() As String
            Get
                Return _ToCountry
            End Get
            Set(ByVal value As String)
                _ToCountry = value
            End Set
        End Property

        Public Property ToDestination() As Integer
            Get
                Return _ToDestination
            End Get
            Set(ByVal value As Integer)
                _ToDestination = value
            End Set
        End Property
    End Class

    Public Class tblBTAirTicketRelativeInfo
        Private _AirTicketRequestID As Integer
        Private _Name As String
        Private _Relationship As String
        Private _FromCountry As String
        Private _FromDestination As Integer
        Private _ToCountry As String
        Private _ToDestination As Integer
        Private _DepartureDate As Date
        Private _ReturnDate As Date
        Private _SameAsEmployee As Boolean
        '        
        Public Property AirTicketRequestID() As Integer
            Get
                Return _AirTicketRequestID
            End Get
            Set(ByVal value As Integer)
                _AirTicketRequestID = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property Relationship() As String
            Get
                Return _Relationship
            End Get
            Set(ByVal value As String)
                _Relationship = value
            End Set
        End Property

        Public Property FromCountry() As String
            Get
                Return _FromCountry
            End Get
            Set(ByVal value As String)
                _FromCountry = value
            End Set
        End Property

        Public Property FromDestination() As Integer
            Get
                Return _FromDestination
            End Get
            Set(ByVal value As Integer)
                _FromDestination = value
            End Set
        End Property

        Public Property ToCountry() As String
            Get
                Return _ToCountry
            End Get
            Set(ByVal value As String)
                _ToCountry = value
            End Set
        End Property

        Public Property ToDestination() As Integer
            Get
                Return _ToDestination
            End Get
            Set(ByVal value As Integer)
                _ToDestination = value
            End Set
        End Property

        Public Property DepartureDate() As Date
            Get
                Return _DepartureDate
            End Get
            Set(ByVal value As Date)
                _DepartureDate = value
            End Set
        End Property

        Public Property ReturnDate() As Date
            Get
                Return _ReturnDate
            End Get
            Set(ByVal value As Date)
                _ReturnDate = value
            End Set
        End Property

        Public Property SameAsEmployee() As Boolean
            Get
                Return _SameAsEmployee
            End Get
            Set(ByVal value As Boolean)
                _SameAsEmployee = value
            End Set
        End Property
    End Class

End Namespace


