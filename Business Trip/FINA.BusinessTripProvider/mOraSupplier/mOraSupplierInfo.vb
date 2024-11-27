
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class mOraSupplierInfo

#Region "Private Members"
        Private _ID As Integer
        Private _SupplierName As String
        Private _Active As Boolean
        Private _OraLink As String
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

        Public Property SupplierName() As String
            Get
                Return _SupplierName
            End Get
            Set(ByVal value As String)
                _SupplierName = value
            End Set
        End Property

        Public Property Active() As Boolean
            Get
                Return _Active
            End Get
            Set(ByVal value As Boolean)
                _Active = value
            End Set
        End Property

        Public Property OraLink() As String
            Get
                Return _OraLink
            End Get
            Set(ByVal value As String)
                _OraLink = value
            End Set
        End Property

#End Region

    End Class

End Namespace


