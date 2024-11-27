Imports Provider
Imports System.Data
Imports System.IO
Imports DevExpress.Web.ASPxGridView

Partial Public Class BTInvoicing
    Inherits System.Web.UI.Page

    Protected _username As String = String.Empty
    Protected _dtData As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CommonFunction.CheckSession()
        CommonFunction.CheckRole(RoleType.Administrator, RoleType.Finance, RoleType.Finance_GA)
        _username = CommonFunction._ToString(Session("UserName"))
        If Not IsPostBack Then
            InitForm()
        Else
        End If
        CommonFunction.CheckSessionMessage(Me)
        LoadForm()
    End Sub

    Private Sub InitForm()
        InitInvoiceItem()
        SetPreParams()
    End Sub

    Private Sub SetPreParams()
        Dim btid As Integer = CommonFunction._ToInt(Request.QueryString("btid"))
        If btid > 0 Then
            Dim serial As String = CommonFunction._ToString(Request.QueryString("serial"))
            txtSSerialNo.Text = serial
            Dim invNo As String = CommonFunction._ToString(Request.QueryString("invno"))
            txtSInvoiceNo.Text = invNo
            Dim invFromDate As String = CommonFunction._ToString(Request.QueryString("invfdate"))
            dteSInvoiceDateFrom.Text = invFromDate
            Dim invToDate As String = CommonFunction._ToString(Request.QueryString("invtdate"))
            dteSInvoiceDateTo.Text = invToDate
            Dim seller As String = CommonFunction._ToString(Request.QueryString("seller"))
            txtSSellerName.Text = seller
            Dim supplier As String = CommonFunction._ToString(Request.QueryString("supplier"))
            txtSSupplier.Text = supplier
            Dim employeeCode As String = CommonFunction._ToString(Request.QueryString("ecode"))
            txtSEmployeeCode.Text = employeeCode
            Dim item As String = CommonFunction._ToString(Request.QueryString("item"))
            CommonFunction.SetCBOValue(ddlSItem, item)
            Dim departureFromDate As String = CommonFunction._ToString(Request.QueryString("depfdate"))
            dteSDepartureFrom.Text = departureFromDate
            Dim departureToDate As String = CommonFunction._ToString(Request.QueryString("deptdate"))
            dteSDepartureTo.Text = departureToDate
            SetPreSearchCondition()
            LoadBTInvoice()
            Dim pageSize As Integer = CommonFunction._ToUnsignInt(Request.QueryString("psize"))
            If pageSize < 1 Then
                pageSize = 100
            End If
            grvBTInvoice.SettingsPager.PageSize = pageSize
            Dim pageIndex As Integer = CommonFunction._ToUnsignInt(Request.QueryString("page"))
            grvBTInvoice.PageIndex = pageIndex
        End If
    End Sub

    Private Sub InitInvoiceItem()
        Dim dtData As DataTable = mInvoiceItemProvider.GetActive()
        CommonFunction.LoadDataToComboBox(ddlInvItem, dtData, "ItemName", "InvoiceItemID", True, "", "")
        CommonFunction.LoadDataToComboBox(ddlSItem, dtData, "ItemName", "InvoiceItemID", True, "All", "")
    End Sub

    Private Sub LoadForm()

    End Sub

    Private Sub EnableInvoiceForm(ByVal enable As Boolean)
        txtInvNo.ReadOnly = Not enable
        txtInvSellerName.ReadOnly = Not enable
        txtInvSellerTaxCode.ReadOnly = Not enable
        txtInvSerialNo.ReadOnly = Not enable
        txtInvSTT.ReadOnly = Not enable
        'txtInvSupplier.ReadOnly = Not enable
        spiInvNetCost.ReadOnly = Not enable
        'spiInvTaxRate.ReadOnly = Not enable
        spiInvVAT.ReadOnly = Not enable
        dteInvDate.ReadOnly = Not enable
        ddlInvItem.Enabled = enable
        chkInvoiceCredit.Enabled = enable
        '
        btnShowSaveInvoice.Visible = enable
        btnSaveInvoice.Visible = enable
    End Sub

    Private Sub LoadBTInvoice()
        Dim serialNo As String = hSSerialNo.Value
        Dim invoiceNo As String = hSInvoiceNo.Value
        Dim invoiceDateFrom As DateTime = CommonFunction._ToDateTime(hSInvoiceDateFrom.Value, "dd-MMM-yyyy")
        Dim invoiceDateTo As DateTime = CommonFunction._ToDateTime(hSInvoiceDateTo.Value, "dd-MMM-yyyy")
        Dim seller As String = hSSellerName.Value
        Dim supplier As String = hSSupplier.Value
        Dim item As String = hSItem.Value
        Dim employeeCode As String = hSEmployeeCode.Value
        Dim departureFrom As DateTime = CommonFunction._ToDateTime(hSDepartureFrom.Value, "dd-MMM-yyyy")
        Dim departureTo As DateTime = CommonFunction._ToDateTime(hSDepartureTo.Value, "dd-MMM-yyyy")
        Dim dtData As DataTable = InvoiceProvider.BTInvoice_Search(serialNo, invoiceNo, invoiceDateFrom, invoiceDateTo, seller, supplier, item, employeeCode, departureFrom, departureTo, _username)
        dtData.Columns.Add("TotalAmountFormated", GetType(String))
        For Each row As DataRow In dtData.Rows
            row("TotalAmountFormated") = String.Concat(CommonFunction._FormatMoney(row("Total")), " ", CommonFunction._ToString(row("Currency")))
        Next
        CommonFunction.LoadDataToGrid(grvBTInvoice, dtData)
    End Sub

    Private Sub LoadBTInvoiceByID()
        Dim id As Integer = CommonFunction._ToInt(hInvoiceID.Value)
        Dim dtData As DataTable = InvoiceProvider.BTInvoice_GetByID(CommonFunction._ToInt(id))
        If dtData.Rows.Count > 0 Then
            Dim drData As DataRow = dtData.Rows(0)
            txtInvSTT.Text = CommonFunction._ToString(drData("STT"))
            txtInvNo.Text = CommonFunction._ToString(drData("InvNo"))
            Dim invDate As DateTime = CommonFunction._ToDateTime(drData("InvDate"))
            If invDate <> DateTime.MinValue Then
                dteInvDate.Date = invDate
            Else
                dteInvDate.Value = Nothing
            End If
            txtInvSerialNo.Text = CommonFunction._ToString(drData("SerialNo"))
            txtInvSellerName.Text = CommonFunction._ToString(drData("SellerName"))
            txtInvSellerTaxCode.Text = CommonFunction._ToString(drData("SellerTaxCode"))
            spiInvNetCost.Value = CommonFunction._ToMoneyWithNull(drData("NetCost"))
            'spiInvTaxRate.Value = CommonFunction._ToMoneyWithNull(drData("TaxRate"))
            CommonFunction.SetCBOValue(ddlInvItem, drData("Item"))
            spiInvVAT.Value = CommonFunction._ToMoneyWithNull(drData("VAT"))
            spiInvTotal.Text = CommonFunction._FormatMoney(drData("Total"))
            lblInvNetCostCurrency.Text = CommonFunction._ToString(drData("Currency"))
            lblInvVATCurrency.Text = CommonFunction._ToString(drData("Currency"))
            lblInvTotalCurrency.Text = CommonFunction._ToString(drData("Currency"))
            'txtInvSupplier.Text = CommonFunction._ToString(drData("Supplier"))
            If CommonFunction._ToString(drData("PaymentType")) = "CC" Then
                chkInvoiceCredit.Checked = CommonFunction._ToBoolean(drData("IsCreditCard"))
                trInvoiceCredit.Visible = True
            Else
                chkInvoiceCredit.Checked = False
                trInvoiceCredit.Visible = False
            End If

        End If
    End Sub

    Private Sub ClearInvoiceForm()
        hInvoiceID.Value = ""
        dteInvDate.Date = DateTime.Now
        txtInvSTT.Text = ""
        txtInvNo.Text = ""
        txtInvSerialNo.Text = ""
        txtInvSellerName.Text = ""
        txtInvSellerTaxCode.Text = ""
        spiInvNetCost.Value = Nothing
        'spiInvTaxRate.Value = Nothing
        spiInvVAT.Value = Nothing
        spiInvTotal.Text = "0"
        lblInvNetCostCurrency.Text = ""
        lblInvVATCurrency.Text = ""
        lblInvTotalCurrency.Text = ""
        ddlInvItem.ClearSelection()
        'txtInvSupplier.Text = ""
        chkInvoiceCredit.Checked = False
        trInvoiceCredit.Visible = False
        '
        txtInvNo.CssClass = txtInvNo.CssClass.Replace(" validate-error", "")
    End Sub

    Private Function GetInvoiceObject() As tblBTInvoiceInfo
        Dim obj As New tblBTInvoiceInfo()
        obj.ID = CommonFunction._ToInt(hInvoiceID.Value)
        obj.BTRegisterID = CommonFunction._ToInt(hID.Value)
        obj.STT = txtInvSTT.Text.Trim()
        obj.InvNo = txtInvNo.Text.Trim()
        obj.InvDate = dteInvDate.Date
        obj.SerialNo = txtInvSerialNo.Text.Trim()
        obj.SellerName = txtInvSellerName.Text.Trim()
        obj.SellerTaxCode = txtInvSellerTaxCode.Text.Trim()
        obj.NetCost = CommonFunction._ToMoney(spiInvNetCost.Text.Trim())
        'obj.TaxRate = CommonFunction._ToMoney(spiInvTaxRate.Text.Trim())
        obj.VAT = CommonFunction._ToMoney(spiInvVAT.Text.Trim())
        obj.Item = If(ddlInvItem.SelectedValue.Trim().Length = 0, Nothing, ddlInvItem.SelectedValue)
        'obj.Supplier = txtInvSupplier.Text.Trim()
        obj.CreatedBy = _username
        obj.ModifiedBy = _username
        If trInvoiceCredit.Visible Then
            obj.IsCreditCard = chkInvoiceCredit.Checked
        Else
            obj.IsCreditCard = False
        End If
        Return obj
    End Function

    Private Sub SetPreSearchCondition()
        hSDepartureFrom.Value = dteSDepartureFrom.Text
        hSDepartureTo.Value = dteSDepartureTo.Text
        hSEmployeeCode.Value = txtSEmployeeCode.Text.Trim()
        hSInvoiceDateFrom.Value = dteSInvoiceDateFrom.Text
        hSInvoiceDateTo.Value = dteSInvoiceDateTo.Text
        hSInvoiceNo.Value = txtSInvoiceNo.Text.Trim()
        hSItem.Value = ddlSItem.SelectedValue
        hSSellerName.Value = txtSSellerName.Text.Trim()
        hSSerialNo.Value = txtSSerialNo.Text.Trim()
        hSSupplier.Value = txtSSupplier.Text.Trim()
    End Sub

    Protected Sub btnSearch_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            SetPreSearchCondition()
            LoadBTInvoice()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub grvBTInvoice_OnBeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles grvBTInvoice.BeforeGetCallbackResult
        LoadBTInvoice()
    End Sub

    Protected Sub btnSaveInvoice_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveInvoice.Click
        CommonFunction.SetPostBackStatus(btnSaveInvoice)
        Try
            Dim obj As tblBTInvoiceInfo = GetInvoiceObject()
            If Not IsValidInvoice(obj) Then
                CommonFunction.SetProcessStatus(btnSaveInvoice, False)
                Return
            End If
            If obj.ID > 0 Then
                InvoiceProvider.BTInvoice_Update(obj)
            Else
                InvoiceProvider.BTInvoice_Insert(obj)
            End If
            CommonFunction.SetProcessStatus(btnSaveInvoice, True)
            CommonFunction.ShowInfoMessage(panMessage, "Your data is saved successfully!")
            LoadBTInvoice()
        Catch ex As Exception
            CommonFunction.SetProcessStatus(btnSaveInvoice, False)
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Private Function IsValidInvoice(ByVal obj As tblBTInvoiceInfo) As Boolean
        Dim isValid As Boolean = True
        txtInvNo.CssClass = txtInvNo.CssClass.Replace(" validate-error", "")
        txtInvSellerName.CssClass = txtInvSellerName.CssClass.Replace(" validate-error", "")
        If InvoiceProvider.CheckNo(obj.ID, obj.InvNo, obj.SellerName) Then
            CommonFunction.ShowErrorMessage(panMessage, "Invoice no existed!")
            txtInvNo.CssClass &= " validate-error"
            txtInvSellerName.CssClass &= " validate-error"
            isValid = False
        End If
        Return isValid
    End Function

    Protected Sub btnCancelInvoice_OnClick(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelInvoice.Click
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btn)
        Try
            ClearInvoiceForm()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnEditInvoice_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            txtInvNo.CssClass = txtInvNo.CssClass.Replace(" validate-error", "")
            '
            'hInvoiceID.Value = btn.Attributes("data-id")
            LoadBTInvoice()
            Dim dtInv As DataTable = InvoiceProvider.BTInvoice_GetByID(CommonFunction._ToInt(hInvoiceID.Value))
            If dtInv.Rows.Count > 0 Then
                Dim enableForm As Boolean = CommonFunction._ToBoolean(dtInv.Rows(0)("EnableForm"))
                EnableInvoiceForm(enableForm)
                LoadBTInvoiceByID()
            Else
                ClearInvoiceForm()
                CommonFunction.ShowErrorMessage(panMessage, "Item not found!")
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnDeleteInvoice_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            Dim id As Integer = CommonFunction._ToInt(hInvoiceID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))
            InvoiceProvider.BTInvoice_Delete(id.ToString())
            CommonFunction.ShowInfoMessage(panMessage, "Your data is deleted successfully!")
            LoadBTInvoice()
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub

    Protected Sub btnViewBT_OnClick(ByVal sender As Object, ByVal e As EventArgs)
        CommonFunction.SetPostBackStatus(btnSearch)
        Try
            'Dim btn As Button = CType(sender, Button)
            Dim btID As Integer = CommonFunction._ToInt(hItemID.Value) 'CommonFunction._ToInt(btn.Attributes("data-id"))            
            Dim dtRegister As DataTable = BusinessTripProvider.BTRegister_GetByID(btID)
            If dtRegister.Rows.Count > 0 Then
                Dim btType As String = CommonFunction._ToString(dtRegister.Rows(0)("BTType"))
                Dim postBackUrl As New StringBuilder()
                postBackUrl.Append(If(btType.IndexOf("oneday_") = 0, "~/BTOneDayDeclaration.aspx", "~/BTExpenseDeclaration.aspx"))
                Dim params As String = String.Format( _
                    "btid={0}&serial={1}&invno={2}&invfdate={3}&invtdate={4}&seller={5}&supplier={6}&ecode={7}&item={8}&depfdate={9}&deptdate={10}&page={11}&psize={12}", _
                    btID, hSSerialNo.Value, hSInvoiceNo.Value, hSInvoiceDateFrom.Value, hSInvoiceDateTo.Value, hSSellerName.Value, hSSupplier.Value, hSEmployeeCode.Value, hSItem.Value, hSDepartureFrom.Value, hSDepartureTo.Value, grvBTInvoice.PageIndex, grvBTInvoice.SettingsPager.PageSize)
                postBackUrl.Append(String.Format("?id={0}&back=BTInvoicing.aspx&params={1}", btID, params.Replace("&", ";amp;").Replace("=", ";eq;")))
                Response.Redirect(postBackUrl.ToString())
            Else
                CommonFunction.ShowErrorMessage(panMessage, "Item not found!")
            End If
        Catch ex As Exception
            CommonFunction.ShowErrorMessage(panMessage, ex.Message)
        End Try
    End Sub
End Class