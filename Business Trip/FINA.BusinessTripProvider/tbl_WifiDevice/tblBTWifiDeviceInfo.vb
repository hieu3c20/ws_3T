
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class tblBTWifiDeviceInfo

#Region "Private Members"

        Private _ID As Integer
        Private _CountryCode As String
        Private _BTRegisterID As Integer
        Private _FromDate As DateTime
        Private _ToDate As DateTime
        Private _IsReturned As Boolean
        Private _EmployeeCode As String
        Private _EmployeeName As String
        Private _EmployeeDivision As String
        Private _EmployeeDepartment As String
        Private _CreatedBy As String
        Private _CreateDate As DateTime
        Private _UpdatedBy As String
        Private _UpdateDate As DateTime
        Private _Status As String
        Private _Comment As String

#End Region

#Region "Public Properties"

        Public Property Comment() As String
            Get
                Return _Comment
            End Get
            Set(ByVal value As String)
                _Comment = value
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

        Public Property UpdatedBy() As String
            Get
                Return _UpdatedBy
            End Get
            Set(ByVal value As String)
                _UpdatedBy = value
            End Set
        End Property

        Public Property UpdateDate() As DateTime
            Get
                Return _UpdateDate
            End Get
            Set(ByVal value As DateTime)
                _UpdateDate = value
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

        Public Property CreateDate() As DateTime
            Get
                Return _CreateDate
            End Get
            Set(ByVal value As DateTime)
                _CreateDate = value
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

        Public Property BTRegisterID() As Integer
            Get
                Return _BTRegisterID
            End Get
            Set(ByVal value As Integer)
                _BTRegisterID = value
            End Set
        End Property

        Public Property IsReturned() As Boolean
            Get
                Return _IsReturned
            End Get
            Set(ByVal value As Boolean)
                _IsReturned = value
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

        Public Property CountryCode() As String
            Get
                Return _CountryCode
            End Get
            Set(ByVal value As String)
                _CountryCode = value
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

#End Region
    End Class

End Namespace


