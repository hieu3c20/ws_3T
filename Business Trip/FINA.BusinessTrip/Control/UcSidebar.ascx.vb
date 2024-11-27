Imports Provider
Partial Public Class UcSidebar
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CommonFunction.CheckSession()
        '        
        Dim assemply As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly()
        Dim assVersion As Version = assemply.GetName().Version
        Dim assDescriptionAttrs() As Object = assemply.GetCustomAttributes(GetType(Reflection.AssemblyDescriptionAttribute), False)
        Dim assDescription As Reflection.AssemblyDescriptionAttribute = If(assDescriptionAttrs.Length > 0, CType(assDescriptionAttrs(0), Reflection.AssemblyDescriptionAttribute), Nothing)
        Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(Reflection.AssemblyDescriptionAttribute), False)
        lblVersion.Text = If(assVersion Is Nothing, "1.0.0.0", assVersion.ToString())
        lblReleasedDate.Text = If(assDescription Is Nothing, "", assDescription.Description)
        '
        finMenu.Visible = False
        gaMenu.Visible = False
        hrMenu.Visible = False
        itMenu.Visible = False
        If Not IsPostBack Then
            Dim role As String = CommonFunction._ToString(Session("RoleType"))
            Select Case role.ToLower()
                Case RoleType.Administrator.ToString().ToLower()
                    finMenu.Visible = True
                    gaMenu.Visible = True
                    hrMenu.Visible = True
                    itMenu.Visible = True
                Case RoleType.Finance.ToString().ToLower()
                    finMenu.Visible = True
                    liBudgetChecking.Visible = False
                Case RoleType.Finance_Budget.ToString().ToLower()
                    finMenu.Visible = True
                    liAdvanceMgmt.Visible = False
                    liExpenseMgmt.Visible = False
                    liInvoicing.Visible = False
                    'liAirTicket.Visible = False
                Case RoleType.GA.ToString().ToLower(), RoleType.TOFS_AIR_GA.ToString().ToLower()
                    gaMenu.Visible = True
                Case RoleType.HR.ToString().ToLower()
                    hrMenu.Visible = True
                Case RoleType.IT.ToString().ToLower()
                    itMenu.Visible = True
                Case RoleType.Finance_GA.ToString().ToLower()
                    finMenu.Visible = True
                    gaMenu.Visible = True
                    liBudgetChecking.Visible = False
            End Select
            'Wifi device pre request
            Dim dtAuthorized As DataTable = UserProvider.tbl_User_GetAuthorizedForWifiDevice(CommonFunction._ToString(Session("UserName")))
            liWifiPreRequest.Visible = dtAuthorized.Rows.Count > 0
        End If
    End Sub

End Class