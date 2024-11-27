
Namespace Provider

    ''' <summary>
    ''' The Info class for tbl_Users
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class tbl_UsersInfo

#Region "Constructors"
        'Sub New()
        '    MyBase.New()
        'End Sub

#End Region

#Region "Private Members"
        Private _UserID As Integer
        Private _UserName As String = String.Empty
        Private _Password As String = String.Empty
        Private _FullName As String = String.Empty
        Private _IsLocked As Boolean
        Private _UpdateDate As DateTime
        Private _UpdateUserID As Integer
        Private _UpdatePassword As DateTime
        Private _PasswordHistory As String = String.Empty
        Private _IsNextLogon As Boolean
        Private _IsChangePassword As Boolean
        Private _PasswordChangeAfter As Integer
        Private _DisableAfterFailed As Integer
        Private _EffectiveDate As DateTime
        Private _ExpriedDate As DateTime
        Private _SendEmail As Boolean
        Private _NoPasswordHistory As Integer
        Private _DateLocked As DateTime
        Private _NoFailLogin As Integer
        Private _Role_Type As Integer
        Private _Role_Level As Integer
        Private _Manager As String = String.Empty
        Private _TimeKeeper As String = String.Empty
        Private _BranchName As String = String.Empty
        Private _DivisionName As String = String.Empty
        Private _DepartmentName As String = String.Empty
        Private _TMVEmail As String = String.Empty
        Private _Photograph As System.Byte()
        Private _JobBand As String = String.Empty
        Private _Mobile As String = String.Empty
        Private _Birthdate As Date
        Private _SexName As String = String.Empty
        Private _Ext As String = String.Empty
        Private _BranchID As Integer
        Private _DivisionID As Integer
        Private _DepartmentID As Integer
        Private _SectionID As Integer
        Private _SectionName As String = String.Empty
        Private _GroupID As Integer
        Private _GroupName As String = String.Empty
        Private _BranchGroup As String = String.Empty
        Private _IsCreditCard As Boolean
        Private _DepartmentShortName As String
        Private _RoleTypeName As String
        Private _UserIDMapOra As Integer
        Private _UserNameMapOra As String
#End Region

#Region "Constructors"
        ' initialization
        Public Sub New()
        End Sub

        'Public Sub New(ByVal userID As Integer, ByVal userName As String, ByVal passWord As String, _
        '                 ByVal isLocked As Boolean, ByVal updateDate As DateTime, ByVal updateUserID As Integer, _
        '                 ByVal updatePassword As DateTime, ByVal passwordHistory As String, ByVal isNextLogon As Boolean, ByVal passwordChangeAfter As Integer, _
        '                 ByVal disableAfterFailed As Integer, ByVal effectiveDate As DateTime, ByVal expriedDate As DateTime, _
        '                 ByVal sendEmail As Boolean, ByVal noPasswordHistory As Integer, ByVal dateLocked As DateTime, _
        '                 ByVal noFailLogin As Integer, ByVal role_Type As Integer, ByVal role_Level As Integer)
        '    Me.UserID = userID
        '    Me.UserName = userName
        '    Me.Password = passWord
        '    Me.IsLocked = isLocked
        '    Me.UpdateDate = updateDate
        '    Me.UpdateUserID = updateUserID
        '    Me.UpdatePassword = updatePassword
        '    Me.PasswordHistory = passwordHistory
        '    Me.IsNextLogon = isNextLogon
        '    Me.IsChangePassword = IsChangePassword
        '    Me.PasswordChangeAfter = passwordChangeAfter
        '    Me.DisableAfterFailed = disableAfterFailed
        '    Me.EffectiveDate = effectiveDate
        '    Me.ExpriedDate = expriedDate
        '    Me.SendEmail = sendEmail
        '    Me.NoPasswordHistory = noPasswordHistory
        '    Me.DateLocked = dateLocked
        '    Me.NoFailLogin = noFailLogin
        '    Me.Role_Type = role_Type
        '    Me.Role_Level = role_Level
        'End Sub
#End Region

#Region "Public Properties"
        Public Property UserNameMapOra() As String
            Get
                Return _UserNameMapOra
            End Get
            Set(ByVal Value As String)
                _UserNameMapOra = Value
            End Set
        End Property

        Public Property UserID() As Integer
            Get
                Return _UserID
            End Get
            Set(ByVal Value As Integer)
                _UserID = Value
            End Set
        End Property

        Public Property UserName() As String
            Get
                Return _UserName
            End Get
            Set(ByVal Value As String)
                _UserName = Value
            End Set
        End Property

        Public Property RoleTypeName() As String
            Get
                Return _RoleTypeName
            End Get
            Set(ByVal Value As String)
                _RoleTypeName = Value
            End Set
        End Property

        Public Property Password() As String
            Get
                Return _Password
            End Get
            Set(ByVal Value As String)
                _Password = Value
            End Set
        End Property

        Public Property FullName() As String
            Get
                Return _FullName
            End Get
            Set(ByVal Value As String)
                _FullName = Value
            End Set
        End Property

        Public Property DepartmentShortName() As String
            Get
                Return _DepartmentShortName
            End Get
            Set(ByVal Value As String)
                _DepartmentShortName = Value
            End Set
        End Property


        Public Property IsLocked() As Boolean
            Get
                Return _IsLocked
            End Get
            Set(ByVal Value As Boolean)
                _IsLocked = Value
            End Set
        End Property

        Public Property UpdateDate() As DateTime
            Get
                Return _UpdateDate
            End Get
            Set(ByVal Value As DateTime)
                _UpdateDate = Value
            End Set
        End Property

        Public Property UpdateUserID() As Integer
            Get
                Return _UpdateUserID
            End Get
            Set(ByVal Value As Integer)
                _UpdateUserID = Value
            End Set
        End Property

        Public Property UpdatePassword() As DateTime
            Get
                Return _UpdatePassword
            End Get
            Set(ByVal Value As DateTime)
                _UpdatePassword = Value
            End Set
        End Property

        Public Property PasswordHistory() As String
            Get
                Return _PasswordHistory
            End Get
            Set(ByVal Value As String)
                _PasswordHistory = Value
            End Set
        End Property

        Public Property IsNextLogon() As Boolean
            Get
                Return _IsNextLogon
            End Get
            Set(ByVal Value As Boolean)
                _IsNextLogon = Value
            End Set
        End Property

        Public Property IsChangePassword() As Boolean
            Get
                Return _IsChangePassword
            End Get
            Set(ByVal Value As Boolean)
                _IsChangePassword = Value
            End Set
        End Property


        Public Property PasswordChangeAfter() As Integer
            Get
                Return _PasswordChangeAfter
            End Get
            Set(ByVal Value As Integer)
                _PasswordChangeAfter = Value
            End Set
        End Property

        Public Property DisableAfterFailed() As Integer
            Get
                Return _DisableAfterFailed
            End Get
            Set(ByVal Value As Integer)
                _DisableAfterFailed = Value
            End Set
        End Property

        Public Property EffectiveDate() As DateTime
            Get
                Return _EffectiveDate
            End Get
            Set(ByVal Value As DateTime)
                _EffectiveDate = Value
            End Set
        End Property

        Public Property ExpriedDate() As DateTime
            Get
                Return _ExpriedDate
            End Get
            Set(ByVal Value As DateTime)
                _ExpriedDate = Value
            End Set
        End Property

        Public Property SendEmail() As Boolean
            Get
                Return _SendEmail
            End Get
            Set(ByVal Value As Boolean)
                _SendEmail = Value
            End Set
        End Property

        Public Property NoPasswordHistory() As Integer
            Get
                Return _NoPasswordHistory
            End Get
            Set(ByVal Value As Integer)
                _NoPasswordHistory = Value
            End Set
        End Property

        Public Property DateLocked() As DateTime
            Get
                Return _DateLocked
            End Get
            Set(ByVal Value As DateTime)
                _DateLocked = Value
            End Set
        End Property

        Public Property NoFailLogin() As Integer
            Get
                Return _NoFailLogin
            End Get
            Set(ByVal Value As Integer)
                _NoFailLogin = Value
            End Set
        End Property

        Public Property Role_Type() As Integer
            Get
                Return _Role_Type
            End Get
            Set(ByVal Value As Integer)
                _Role_Type = Value
            End Set
        End Property

        Public Property Role_Level() As Integer
            Get
                Return _Role_Level
            End Get
            Set(ByVal Value As Integer)
                _Role_Level = Value
            End Set
        End Property

        Public Property Manager() As String
            Get
                Return _Manager
            End Get
            Set(ByVal Value As String)
                _Manager = Value
            End Set
        End Property


        Public Property TimeKeeper() As String
            Get
                Return _TimeKeeper
            End Get
            Set(ByVal Value As String)
                _TimeKeeper = Value
            End Set
        End Property

        Public Property BranchName() As String
            Get
                Return _BranchName
            End Get
            Set(ByVal Value As String)
                _BranchName = Value
            End Set
        End Property

        Public Property DivisionName() As String
            Get
                Return _DivisionName
            End Get
            Set(ByVal Value As String)
                _DivisionName = Value
            End Set
        End Property

        Public Property DepartmentName() As String
            Get
                Return _DepartmentName
            End Get
            Set(ByVal Value As String)
                _DepartmentName = Value
            End Set
        End Property

        Public Property TMVEmail() As String
            Get
                Return _TMVEmail
            End Get
            Set(ByVal Value As String)
                _TMVEmail = Value
            End Set
        End Property

        Public Property Photograph() As System.Byte()
            Get
                Return _Photograph
            End Get
            Set(ByVal value As System.Byte())
                _Photograph = value
            End Set
        End Property


        Public Property JobBand() As String
            Get
                Return _JobBand
            End Get
            Set(ByVal Value As String)
                _JobBand = Value
            End Set
        End Property

        Public Property Mobile() As String
            Get
                Return _Mobile
            End Get
            Set(ByVal Value As String)
                _Mobile = Value
            End Set
        End Property

        Public Property Birthdate() As Date
            Get
                Return _Birthdate
            End Get
            Set(ByVal Value As Date)
                _Birthdate = Value
            End Set
        End Property

        Public Property SexName() As String
            Get
                Return _SexName
            End Get
            Set(ByVal Value As String)
                _SexName = Value
            End Set
        End Property

        Public Property Ext() As String
            Get
                Return _Ext
            End Get
            Set(ByVal Value As String)
                _Ext = Value
            End Set
        End Property


        Public Property BranchID() As Integer
            Get
                Return _BranchID
            End Get
            Set(ByVal Value As Integer)
                _BranchID = Value
            End Set
        End Property

        Public Property DivisionID() As Integer
            Get
                Return _DivisionID
            End Get
            Set(ByVal Value As Integer)
                _DivisionID = Value
            End Set
        End Property

        Public Property DepartmentID() As Integer
            Get
                Return _DepartmentID
            End Get
            Set(ByVal Value As Integer)
                _DepartmentID = Value
            End Set
        End Property

        Public Property SectionID() As Integer
            Get
                Return _SectionID
            End Get
            Set(ByVal Value As Integer)
                _SectionID = Value
            End Set
        End Property

        Public Property SectionName() As String
            Get
                Return _SectionName
            End Get
            Set(ByVal Value As String)
                _SectionName = Value
            End Set
        End Property

        Public Property GroupID() As String
            Get
                Return _GroupID
            End Get
            Set(ByVal Value As String)
                _GroupID = Value
            End Set
        End Property

        Public Property GroupName() As String
            Get
                Return _GroupName
            End Get
            Set(ByVal Value As String)
                _GroupName = Value
            End Set
        End Property


        Public Property BranchGroup() As String
            Get
                Return _BranchGroup
            End Get
            Set(ByVal Value As String)
                _BranchGroup = Value
            End Set
        End Property

        Public Property IsCreditCard() As Boolean
            Get
                Return _IsCreditCard
            End Get
            Set(ByVal Value As Boolean)
                _IsCreditCard = Value
            End Set
        End Property


        Public Property UserIDMapOra() As Integer
            Get
                Return _UserIDMapOra
            End Get
            Set(ByVal Value As Integer)
                _UserIDMapOra = Value
            End Set
        End Property

#End Region

    End Class

End Namespace


