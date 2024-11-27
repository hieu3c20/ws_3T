Namespace Provider

    ''' <summary>
    ''' The Info class for tbl_Users
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class RoleType_Info

#Region "Private Members"
        Private _RoleTypeID As Integer
        Private _RoleTypeName As String = String.Empty
        Private _Note As String = String.Empty
#End Region

#Region "Constructors"
        ' initialization
        Public Sub New()
        End Sub
#End Region

#Region "Public Properties"
        Public Property RoleTypeID() As Integer
            Get
                Return _RoleTypeID
            End Get
            Set(ByVal Value As Integer)
                _RoleTypeID = Value
            End Set
        End Property

        Public Property RoleTypeName() As String
            Get
                Return _RoleTypeName
            End Get
            Set(ByVal Value As String)
                _RoleTypeName = Value
            End Set
        End Property

        Public Property Note() As String
            Get
                Return _Note
            End Get
            Set(ByVal Value As String)
                _Note = Value
            End Set
        End Property

#End Region

    End Class

End Namespace


