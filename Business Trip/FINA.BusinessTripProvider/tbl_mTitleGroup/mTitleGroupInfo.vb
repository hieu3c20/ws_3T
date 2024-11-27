
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class mTitleGroupInfo


#Region "Private Members"
        Private _TitleGroupID As Integer
        Private _Name As String
        Private _GroupTitle As String
        Private _Note As String
        Private _Status As Boolean
        Private _TitleIDs As String

#End Region

#Region "Public Properties"
        Public Property TitleGroupID() As Integer
            Get
                Return _TitleGroupID
            End Get
            Set(ByVal value As Integer)
                _TitleGroupID = value
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

        Public Property GroupTitle() As String
            Get
                Return _GroupTitle
            End Get
            Set(ByVal value As String)
                _GroupTitle = value
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

        Public Property TitleIDs() As String
            Get
                Return _TitleIDs
            End Get
            Set(ByVal value As String)
                _TitleIDs = value
            End Set
        End Property

#End Region

    End Class

End Namespace


