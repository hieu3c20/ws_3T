
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class mDestinationInfo


#Region "Private Members"
        Private _DestinationID As Integer
        Private _Name As String
        Private _Note As String
        Private _CountryID As Integer
        Private _GroupID As Integer
        Private _Status As Boolean

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

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
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

        Public Property CountryID() As Integer
            Get
                Return _CountryID
            End Get
            Set(ByVal value As Integer)
                _CountryID = value
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


