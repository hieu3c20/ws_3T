
Namespace Provider

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class mBudgetInfo

#Region "Private Members"

        Private _BudgetID As Integer
        Private _BudgetName As String
        Private _BudgetCode As String
        Private _Amount As Double
        Private _Org As String
        Private _Department As String
        Private _Description As String
        Private _HRDepID As Integer
        Private _Year As Integer
        Private _Active As Boolean
        Private _Budget_Type As String
        Private _IsExecutive As Boolean
#End Region

#Region "Public Properties"

        Public Property IsExecutive() As Boolean
            Get
                Return _IsExecutive
            End Get
            Set(ByVal value As Boolean)
                _IsExecutive = value
            End Set
        End Property

        Public Property Budget_Type() As String
            Get
                Return _Budget_Type
            End Get
            Set(ByVal value As String)
                _Budget_Type = value
            End Set
        End Property

        Public Property Year() As Integer
            Get
                Return _Year
            End Get
            Set(ByVal value As Integer)
                _Year = value
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

        Public Property HRDepID() As Integer
            Get
                Return _HRDepID
            End Get
            Set(ByVal value As Integer)
                _HRDepID = value
            End Set
        End Property

        Public Property BudgetID() As Integer
            Get
                Return _BudgetID
            End Get
            Set(ByVal value As Integer)
                _BudgetID = value
            End Set
        End Property

        Public Property BudgetName() As String
            Get
                Return _BudgetName
            End Get
            Set(ByVal value As String)
                _BudgetName = value
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

        Public Property Amount() As Double
            Get
                Return _Amount
            End Get
            Set(ByVal value As Double)
                _Amount = value
            End Set
        End Property

        Public Property BudgetCode() As String
            Get
                Return _BudgetCode
            End Get
            Set(ByVal value As String)
                _BudgetCode = value
            End Set
        End Property

        Public Property Department() As String
            Get
                Return _Department
            End Get
            Set(ByVal value As String)
                _Department = value
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

#End Region

    End Class

End Namespace


