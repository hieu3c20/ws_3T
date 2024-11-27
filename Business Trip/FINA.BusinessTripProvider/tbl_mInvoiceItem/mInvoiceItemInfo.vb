
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class mInvoiceItemInfo


#Region "Private Members"
        Private _InvoiceItemID As Integer
        Private _ItemName As String
        Private _Note As String
        Private _Status As Boolean

#End Region

#Region "Public Properties"
        Public Property InvoiceItemID() As Integer
            Get
                Return _InvoiceItemID
            End Get
            Set(ByVal value As Integer)
                _InvoiceItemID = value
            End Set
        End Property

        Public Property ItemName() As String
            Get
                Return _ItemName
            End Get
            Set(ByVal value As String)
                _ItemName = value
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


