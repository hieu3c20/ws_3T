
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class ReportInfo

#Region "Private Members"

        Private _DepartureFrom As DateTime
        Private _DepartureTo As DateTime
        Private _BranchID As Integer
        Private _ExceptBranch As Boolean
        Private _DivID As Integer
        Private _ExceptDiv As Boolean
        Private _DeptID As Integer
        Private _ExceptDept As Boolean
        Private _SecID As Integer
        Private _ExceptSec As Boolean
        Private _GroupID As Integer
        Private _ExceptGroup As Boolean
        Private _TeamID As Integer
        Private _ExceptTeam As Boolean
        Private _EmployeeCode As String
        Private _ExceptEmployeeCode As Boolean
        Private _Username As String
        Private _OraSupplier As String
        Private _AirPeriod As Integer
        Private _BTType As String
        Private _CountryID As Integer

#End Region

#Region "Public Properties"

        Public Property BTType() As String
            Get
                Return _BTType
            End Get
            Set(ByVal value As String)
                _BTType = value
            End Set
        End Property

        Public Property CountryID() As Integer
            Get
                Return _CountryID
            End Get
            Set(ByVal value As Integer)
                _CountryID = value
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

        Public Property EmployeeCode() As String
            Get
                Return _EmployeeCode
            End Get
            Set(ByVal value As String)
                _EmployeeCode = value
            End Set
        End Property

        Public Property ExceptEmployeeCode() As Boolean
            Get
                Return _ExceptEmployeeCode
            End Get
            Set(ByVal value As Boolean)
                _ExceptEmployeeCode = value
            End Set
        End Property

        Public Property DepartureFrom() As DateTime
            Get
                Return _DepartureFrom
            End Get
            Set(ByVal value As DateTime)
                _DepartureFrom = value
            End Set
        End Property

        Public Property DepartureTo() As DateTime
            Get
                Return _DepartureTo
            End Get
            Set(ByVal value As DateTime)
                _DepartureTo = value
            End Set
        End Property

        Public Property BranchID() As Integer
            Get
                Return _BranchID
            End Get
            Set(ByVal value As Integer)
                _BranchID = value
            End Set
        End Property

        Public Property ExceptBranch() As Boolean
            Get
                Return _ExceptBranch
            End Get
            Set(ByVal value As Boolean)
                _ExceptBranch = value
            End Set
        End Property

        Public Property DivID() As Integer
            Get
                Return _DivID
            End Get
            Set(ByVal value As Integer)
                _DivID = value
            End Set
        End Property

        Public Property ExceptDiv() As Boolean
            Get
                Return _ExceptDiv
            End Get
            Set(ByVal value As Boolean)
                _ExceptDiv = value
            End Set
        End Property

        Public Property DeptID() As Integer
            Get
                Return _DeptID
            End Get
            Set(ByVal value As Integer)
                _DeptID = value
            End Set
        End Property

        Public Property ExceptDept() As Boolean
            Get
                Return _ExceptDept
            End Get
            Set(ByVal value As Boolean)
                _ExceptDept = value
            End Set
        End Property

        Public Property SecID() As Integer
            Get
                Return _SecID
            End Get
            Set(ByVal value As Integer)
                _SecID = value
            End Set
        End Property

        Public Property ExceptSec() As Boolean
            Get
                Return _ExceptSec
            End Get
            Set(ByVal value As Boolean)
                _ExceptSec = value
            End Set
        End Property

        Public Property GroupID() As Integer
            Get
                Return _GroupID
            End Get
            Set(ByVal value As Integer)
                _GroupID = value
            End Set
        End Property

        Public Property ExceptGroup() As Boolean
            Get
                Return _ExceptGroup
            End Get
            Set(ByVal value As Boolean)
                _ExceptGroup = value
            End Set
        End Property

        Public Property TeamID() As Integer
            Get
                Return _TeamID
            End Get
            Set(ByVal value As Integer)
                _TeamID = value
            End Set
        End Property

        Public Property ExceptTeam() As Boolean
            Get
                Return _ExceptTeam
            End Get
            Set(ByVal value As Boolean)
                _ExceptTeam = value
            End Set
        End Property

        Public Property Username() As String
            Get
                Return _Username
            End Get
            Set(ByVal value As String)
                _Username = value
            End Set
        End Property

#End Region

    End Class

End Namespace


