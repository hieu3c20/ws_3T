
Namespace Provider

    ''' <summary>
    ''' The Info class for 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class tblBTExpenseOtherInfo

#Region "Private Members"
        Private _ID As Integer
        Private _BTExpenseID As Integer
        Private _DDate As DateTime        
        Private _Expense As String
        Private _Amount As Decimal
        Private _Currency As String
        Private _Exrate As Decimal
        Private _CreatedBy As String
        Private _CreatedDate As DateTime
        Private _ModifiedBy As String
        Private _ModifiedDate As DateTime
        Private _CreditAmount As Decimal

#End Region

#Region "Public Properties"
        Public Property CreditAmount() As Decimal
            Get
                Return _CreditAmount
            End Get
            Set(ByVal value As Decimal)
                _CreditAmount = value
            End Set
        End Property

        Public Property Amount() As Decimal
            Get
                Return _Amount
            End Get
            Set(ByVal value As Decimal)
                _Amount = value
            End Set
        End Property

        Public Property Exrate() As Decimal
            Get
                Return _Exrate
            End Get
            Set(ByVal value As Decimal)
                _Exrate = value
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
        Public Property BTExpenseID() As Integer
            Get
                Return _BTExpenseID
            End Get
            Set(ByVal value As Integer)
                _BTExpenseID = value
            End Set
        End Property
        Public Property DDate() As DateTime
            Get
                Return _DDate
            End Get
            Set(ByVal value As DateTime)
                _DDate = value
            End Set
        End Property

        Public Property Expense() As String
            Get
                Return _Expense
            End Get
            Set(ByVal value As String)
                _Expense = value
            End Set
        End Property

        Public Property Currency() As String
            Get
                Return _Currency
            End Get
            Set(ByVal value As String)
                _Currency = value
            End Set
        End Property

        Public Property CreatedBy() As String
            Get
                Return _CreatedBy
            End Get
            Set(ByVal value As String)
                _CreatedBy = value
            End Set
        End Property

        Public Property CreatedDate() As DateTime
            Get
                Return _CreatedDate
            End Get
            Set(ByVal value As DateTime)
                _CreatedDate = value
            End Set
        End Property

        Public Property ModifiedBy() As String
            Get
                Return _ModifiedBy
            End Get
            Set(ByVal value As String)
                _ModifiedBy = value
            End Set
        End Property

        Public Property ModifiedDate() As DateTime
            Get
                Return _ModifiedDate
            End Get
            Set(ByVal value As DateTime)
                _ModifiedDate = value
            End Set
        End Property
#End Region

#Region "Constructors"

        Public Sub New()
        End Sub

#End Region

    End Class

End Namespace


