
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class mCountryInfo

#Region "Private Members"

        Private _ID As Integer
        Private _Name As String
        Private _Code As String
        Private _GroupID As Integer

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

        Public Property ID() As Integer
            Get
                Return _ID
            End Get
            Set(ByVal value As Integer)
                _ID = value
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

        Public Property Code() As String
            Get
                Return _Code
            End Get
            Set(ByVal value As String)
                _Code = value
            End Set
        End Property

#End Region
    End Class

End Namespace


