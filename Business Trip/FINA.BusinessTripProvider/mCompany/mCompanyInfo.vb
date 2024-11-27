
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class mCompanyInfo

#Region "Private Members"
        Private _ID As Integer
        Private _Name As String
        Private _TaxCode As String
        Private _Description As String
        Private _Active As Boolean
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

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property TaxCode() As String
            Get
                Return _TaxCode
            End Get
            Set(ByVal value As String)
                _TaxCode = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
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

#End Region

    End Class

End Namespace


