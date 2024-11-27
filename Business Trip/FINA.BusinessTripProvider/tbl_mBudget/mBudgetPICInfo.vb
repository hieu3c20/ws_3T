
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class mBudgetPICInfo

#Region "Private Members"

        Private _ID As Integer
        Private _PICName As String
        Private _PICEmail As String
        Private _Org As String

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

        Public Property PICName() As String
            Get
                Return _PICName
            End Get
            Set(ByVal value As String)
                _PICName = value
            End Set
        End Property

        Public Property PICEmail() As String
            Get
                Return _PICEmail
            End Get
            Set(ByVal value As String)
                _PICEmail = value
            End Set
        End Property

        Public Property Org() As String
            Get
                Return _Org
            End Get
            Set(ByVal value As String)
                _Org = value
            End Set
        End Property

#End Region

    End Class

End Namespace


