
Namespace Provider

    ''' <summary>
    ''' The Info class for tbl_Users
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class RoleLevel_Info

#Region "Private Members"
        Private _Role_Level_ID As Integer
        Private _LevelName As String = String.Empty
        Private _Note As String = String.Empty
#End Region

#Region "Constructors"
        ' initialization
        Public Sub New()
            '_Role_Level_ID = id
        End Sub

#End Region

#Region "Public Properties"
        Public Property Role_Level_ID() As Integer
            Get
                Return _Role_Level_ID
            End Get
            Set(ByVal Value As Integer)
                _Role_Level_ID = Value
            End Set
        End Property

        Public Property LevelName() As String
            Get
                Return _LevelName
            End Get
            Set(ByVal Value As String)
                _LevelName = Value
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


