
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class tblBTInvoiceInfo


#Region "Private Members"
        Private _ID As Integer
        Private _BTRegisterID As Integer
        Private _STT As String
        Private _InvNo As String
        Private _InvDate As DateTime
        Private _SerialNo As String
        Private _SellerName As String
        Private _SellerTaxCode As String
        Private _NetCost As Decimal
        Private _TaxRate As Decimal
        Private _VAT As Decimal
        Private _Item As String
        Private _Supplier As String
        Private _CreatedBy As String
        Private _CreatedDate As DateTime
        Private _ModifiedBy As String
        Private _ModifiedDate As DateTime
        Private _IsCreditCard As Boolean
#End Region

#Region "Public Properties"
        Public Property IsCreditCard() As Boolean
            Get
                Return _IsCreditCard
            End Get
            Set(ByVal value As Boolean)
                _IsCreditCard = value
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

        Public Property ModifiedDate() As DateTime
            Get
                Return _ModifiedDate
            End Get
            Set(ByVal value As DateTime)
                _ModifiedDate = value
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

        Public Property ModifiedBy() As String
            Get
                Return _ModifiedBy
            End Get
            Set(ByVal value As String)
                _ModifiedBy = value
            End Set
        End Property

        Public Property Supplier() As String
            Get
                Return _Supplier
            End Get
            Set(ByVal value As String)
                _Supplier = value
            End Set
        End Property

        Public Property Item() As String
            Get
                Return _Item
            End Get
            Set(ByVal value As String)
                _Item = value
            End Set
        End Property

        Public Property TaxRate() As Decimal
            Get
                Return _TaxRate
            End Get
            Set(ByVal value As Decimal)
                _TaxRate = value
            End Set
        End Property

        Public Property VAT() As Decimal
            Get
                Return _VAT
            End Get
            Set(ByVal value As Decimal)
                _VAT = value
            End Set
        End Property

        Public Property NetCost() As Decimal
            Get
                Return _NetCost
            End Get
            Set(ByVal value As Decimal)
                _NetCost = value
            End Set
        End Property

        Public Property SellerTaxCode() As String
            Get
                Return _SellerTaxCode
            End Get
            Set(ByVal value As String)
                _SellerTaxCode = value
            End Set
        End Property

        Public Property InvDate() As DateTime
            Get
                Return _InvDate
            End Get
            Set(ByVal value As DateTime)
                _InvDate = value
            End Set
        End Property

        Public Property SerialNo() As String
            Get
                Return _SerialNo
            End Get
            Set(ByVal value As String)
                _SerialNo = value
            End Set
        End Property

        Public Property SellerName() As String
            Get
                Return _SellerName
            End Get
            Set(ByVal value As String)
                _SellerName = value
            End Set
        End Property

        Public Property ID() As String
            Get
                Return _ID
            End Get
            Set(ByVal value As String)
                _ID = value
            End Set
        End Property

        Public Property BTRegisterID() As String
            Get
                Return _BTRegisterID
            End Get
            Set(ByVal value As String)
                _BTRegisterID = value
            End Set
        End Property

        Public Property STT() As String
            Get
                Return _STT
            End Get
            Set(ByVal value As String)
                _STT = value
            End Set
        End Property

        Public Property InvNo() As String
            Get
                Return _InvNo
            End Get
            Set(ByVal value As String)
                _InvNo = value
            End Set
        End Property

#End Region

    End Class

End Namespace


