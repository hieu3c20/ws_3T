
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
        Private _DestinationID As Integer
        Private _Name As String
        Private _Note As String
#End Region

#Region "Public Properties"
        Public Property DestinationID() As String
            Get
                Return _DestinationID
            End Get
            Set(ByVal value As String)
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
#End Region

    End Class

End Namespace


