
Namespace Provider

    ''' <summary>
    ''' The Info class for 
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Class tblBTRegisterAttachmentInfo

#Region "Private Members"
        Private _ID As Integer
        Private _BTRegisterID As Integer
        Private _AttachmentType As String
        Private _AttachmentPath As String
        Private _Description As String
        Private _CreatedBy As String
        Private _CreatedDate As DateTime
        Private _ModifiedBy As String
        Private _ModifiedDate As DateTime
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

        Public Property BTRegisterID() As Integer
            Get
                Return _BTRegisterID
            End Get
            Set(ByVal value As Integer)
                _BTRegisterID = value
            End Set
        End Property

        Public Property AttachmentType() As String
            Get
                Return _AttachmentType
            End Get
            Set(ByVal value As String)
                _AttachmentType = value
            End Set
        End Property
        Public Property AttachmentPath() As String
            Get
                Return _AttachmentPath
            End Get
            Set(ByVal value As String)
                _AttachmentPath = value
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

    End Class

End Namespace


