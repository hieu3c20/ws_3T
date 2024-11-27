
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class mCountryGroupInfo


#Region "Private Members"
        Private _ID As Integer
        Private _GroupName As String
        Private _Desc As String
        Private _IsActive As Boolean

#End Region

#Region "Public Properties"
        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
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

        Public Property Desc() As String
            Get
                Return _Desc
            End Get
            Set(ByVal value As String)
                _Desc = value
            End Set
        End Property

        Public Property IsActive() As Boolean
            Get
                Return _IsActive
            End Get
            Set(ByVal value As Boolean)
                _IsActive = value
            End Set
        End Property

#End Region

    End Class

End Namespace


