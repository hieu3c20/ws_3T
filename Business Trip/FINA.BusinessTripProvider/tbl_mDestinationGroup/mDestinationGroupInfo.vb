
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class mDestinationGroupInfo


#Region "Private Members"
        Private _GroupID As Integer
        Private _GroupName As String
        Private _Note As String
        Private _Status As Boolean

#End Region

#Region "Public Properties"
        Public Property GroupID() As Integer
            Get
                Return _GroupID
            End Get
            Set(ByVal value As Integer)
                _GroupID = value
            End Set
        End Property

        Public Property GroupName() As String
            Get
                Return _GroupName
            End Get
            Set(ByVal value As String)
                _GroupName = value
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

        Public Property Status() As Boolean
            Get
                Return _Status
            End Get
            Set(ByVal value As Boolean)
                _Status = value
            End Set
        End Property

#End Region

    End Class

End Namespace


