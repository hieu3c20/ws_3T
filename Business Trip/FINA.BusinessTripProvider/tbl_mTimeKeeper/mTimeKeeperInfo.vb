
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class mTimeKeeperInfo


#Region "Private Members"
        Private _TimeKeeperID As Integer
        Private _TimeKeeperCode As String
        Private _EmployeeCode As String
        Private _EmployeeName As String
        Private _Note As String
#End Region

#Region "Public Properties"
        Public Property TimeKeeperID() As Integer
            Get
                Return _TimeKeeperID
            End Get
            Set(ByVal value As Integer)
                _TimeKeeperID = value
            End Set
        End Property

        Public Property TimeKeeperCode() As String
            Get
                Return _TimeKeeperCode
            End Get
            Set(ByVal value As String)
                _TimeKeeperCode = value
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

        Public Property Note() As String
            Get
                Return _Note
            End Get
            Set(ByVal value As String)
                _Note = value
            End Set
        End Property
#End Region

    End Class

End Namespace


