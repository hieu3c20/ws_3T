Public Class CommonConstant

End Class

Public Enum HRStatus
    pending = 0
    completed = 1
    rejected = 2
End Enum

Public Enum GAStatus
    pending = 0
    completed = 1
    rejected = 2
End Enum

Public Enum FIStatus
    pending = 0
    checked = 1
    budget_rejected = 2
    budget_reconfirmed = 3
    completed = 4
    rejected = 5
    cancelled = 6
End Enum

Public Enum UserType
    HR = 1
    GA = 2
    FI = 3
End Enum

Public Enum RoleType
    Administrator = 0
    Normal = 1
    Finance = 2
    GA = 3
    HR = 4
    Finance_GA = 5
    Finance_Budget = 6
    TOFS_AIR_GA = 7
    IT = 8
End Enum

Public Enum SendEmailMode
    Dev = 0
    Test = 1
    None = 2
    User = 3
    UserTest = 4
End Enum

Public Enum AirticketRequestStatus
    Prepared = 0
    Submitted = 1
    Rejected = 2
    Approved = 3
End Enum

Public Enum WifiDeviceRequestStatus    
    pending = 0
    rejected = 1
    confirmed = 2
    returned = 3    
End Enum