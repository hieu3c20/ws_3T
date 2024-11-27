<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BTInvoicing.aspx.vb" Inherits="BTInvoicing"
    MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="HeadContent" runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link href="/js/jquery/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    Invoice Management
</asp:Content>
<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <%--Preload images--%>
    <img src="images/inform-btn.png" style="display: none" alt="" />
    <img src="images/check-check.png" style="display: none" alt="" />
    <img src="images/check.png" style="display: none" alt="" />
    <img src="images/rad-check.png" style="display: none" alt="" />
    <img src="images/rad.png" style="display: none" alt="" />
    <%--Message Panel--%>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="panMessage">
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Main part--%>
    <div class="ui-accordion ui-widget ui-helper-reset ui-hidden-container" role="tablist">
        <%--Total error summary--%>
        <ul id="totalErrorSummary" class="total-error-summary hide">
        </ul>
        <%--Search form / General Information--%>
        <div class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top active"
                role="tab">
                <span class="ui-icon"></span><span id="general-title">Search Condition</span></h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content" role="tabpanel">
                <%--<span class="required" style="text-align: center">Please select Employee Code to get
                    information from system automatically. Some of them can not be modified.</span>--%>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hID" runat="server" />
                        <asp:HiddenField ID="hItemID" runat="server" />
                        <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                            <tbody>
                                <tr>
                                    <td class="ui-panelgrid-cell" style="width: 130px">
                                        <label>
                                            Serial No</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtSSerialNo" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hSSerialNo" />
                                    </td>
                                    <td class="ui-panelgrid-cell" style="width: 130px">
                                        <label>
                                            Invoice No</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtSInvoiceNo" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hSInvoiceNo" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Invoice Date From</label>
                                    </td>
                                    <td class="ui-panelgrid-cell date-time-picker validate-required">
                                        <dx:ASPxDateEdit ID="dteSInvoiceDateFrom" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy">
                                        </dx:ASPxDateEdit>
                                        <asp:HiddenField runat="server" ID="hSInvoiceDateFrom" />
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Invoice Date To</label>
                                    </td>
                                    <td class="ui-panelgrid-cell date-time-picker validate-required">
                                        <dx:ASPxDateEdit ID="dteSInvoiceDateTo" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy">
                                        </dx:ASPxDateEdit>
                                        <asp:HiddenField runat="server" ID="hSInvoiceDateTo" />
                                    </td>
                                </tr>
                                <tr runat="server" id="trGeneral1" class="general-info">
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Seller Name</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtSSellerName" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hSSellerName" />
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Supplier</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtSSupplier" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hSSupplier" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Employee Code</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtSEmployeeCode" runat="server" CssClass="employee-code" data-type="int"
                                            MaxLength="6"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hSEmployeeCode" />
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Item</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:DropDownList ID="ddlSItem" runat="server">
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="hSItem" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Departure Date From</label>
                                    </td>
                                    <td class="ui-panelgrid-cell date-time-picker validate-required">
                                        <dx:ASPxDateEdit ID="dteSDepartureFrom" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy">
                                        </dx:ASPxDateEdit>
                                        <asp:HiddenField runat="server" ID="hSDepartureFrom" />
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Departure Date To</label>
                                    </td>
                                    <td class="ui-panelgrid-cell date-time-picker validate-required">
                                        <dx:ASPxDateEdit ID="dteSDepartureTo" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy">
                                        </dx:ASPxDateEdit>
                                        <asp:HiddenField runat="server" ID="hSDepartureTo" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                        <span class="btn inform" id="btnSearchEmpInfo" runat="server" style="margin-left: 3px;
                                            text-align: center; margin-top: 20px; display: inline-block;">
                                            <asp:Button ID="btnSearch" CssClass="btnSearch" runat="server" Text="" OnClientClick="btnCancelSub_Click($('[id$=btnCancelInvoice]')[0]); HandleMessage(this); bindStartupEvents(this)" />
                                            <i class="search"></i>Search</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <%--Invoicing--%>
        <div class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top active"
                role="tab">
                <span class="ui-icon"></span>Invoice Management</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content default-hide"
                role="tabpanel">
                <%--List--%>
                <div class="ui-datatable ui-widget">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <div style="margin-top: 10px; border: 1px solid transparent">
                                <dx:ASPxGridView ID="grvBTInvoice" KeyFieldName="ID" runat="server" Theme="Office2010Black"
                                    AutoGenerateColumns="false">
                                    <SettingsText EmptyDataRow="No records found!" />
                                    <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
                                    <SettingsPager PageSize="100" NumericButtonCount="10">
                                        <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All" Caption="Page Size"
                                            Items="20, 30, 40, 50, 100" />
                                    </SettingsPager>
                                    <Styles>
                                        <AlternatingRow Enabled="True">
                                        </AlternatingRow>
                                    </Styles>
                                    <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                    <Columns>
                                        <%--<dx:GridViewDataColumn Width="20px" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <input type="checkbox" class="chkAll" onchange="CheckAll(this)" />
                                            </HeaderTemplate>
                                            <DataItemTemplate>
                                                <asp:CheckBox data-id='<%# Eval("ID") %>' runat="server" ID="chkSelect" CssClass="chkSelect"
                                                    onchange="CheckboxChecked(this)" />
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>--%>
                                        <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                            FieldName="ID" Caption="ID" />
                                        <dx:GridViewDataColumn CellStyle-CssClass="btid" Width="50px" Settings-AllowAutoFilter="False"
                                            FieldName="BTRegisterID" Caption="BT ID" />
                                        <dx:GridViewDataColumn FieldName="STT" Caption="STT" Width="80px" />
                                        <dx:GridViewBandColumn Caption="Invoice Information">
                                            <Columns>
                                                <dx:GridViewDataColumn FieldName="SerialNo" Caption="Serial No" Width="80px" />
                                                <dx:GridViewDataColumn FieldName="InvNo" Caption="Invoice No" Width="80px" />
                                                <dx:GridViewDataDateColumn FieldName="InvDate" Caption="Invoice Date" Width="90px"
                                                    PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                            </Columns>
                                        </dx:GridViewBandColumn>
                                        <dx:GridViewDataColumn FieldName="Item" Caption="Item" />
                                        <dx:GridViewDataTextColumn FieldName="NetCost" Width="90px" Caption="Net Cost" CellStyle-HorizontalAlign="Right"
                                            PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                        <dx:GridViewDataTextColumn FieldName="VAT" Width="80px" Caption="VAT" CellStyle-HorizontalAlign="Right"
                                            PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                        <dx:GridViewDataTextColumn FieldName="TotalAmountFormated" Width="110px" Caption="Total"
                                            CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                        <dx:GridViewDataImageColumn Width="60px" CellStyle-HorizontalAlign="Left">
                                            <DataItemTemplate>
                                                <asp:Button ID="btnEdit" runat="server" ToolTip="View/Edit" CssClass='<%# If(Convert.ToBoolean(Eval("EnableForm")), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                    data-id='<%# Eval("ID") %>' OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])"
                                                    OnClick="btnEditInvoice_OnClick"></asp:Button>
                                                <input type="button" class="grid-btn delete-btn" title="Delete" runat="server" id="btnSubConfirmDelete"
                                                    visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>' data-id='<%# Eval("ID") %>'
                                                    onclick="btnDeleteInvoiceClick(this)" />
                                                <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' Visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>'
                                                    ToolTip="Delete" runat="server" CssClass="hide" OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])"
                                                    OnClick="btnDeleteInvoice_OnClick"></asp:Button>
                                                <asp:Button ID="btnViewBT" runat="server" ToolTip="View BT Information" CssClass="grid-btn info-btn"
                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnViewBTClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent($('[id$=btnSearch]')[0])"
                                                    OnClick="btnViewBT_OnClick"></asp:Button>
                                            </DataItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </dx:GridViewDataImageColumn>
                                    </Columns>
                                    <Templates>
                                        <DetailRow>
                                            <fieldset style="width: 98%; padding: 0; border: none; margin: 10px 0;">
                                                <table class="tblRequestDetails grid-inside">
                                                    <tr>
                                                        <th>
                                                            Seller Name
                                                        </th>
                                                        <th style="width: 25%">
                                                            Seller Tax Code
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <%#Eval("SellerName")%>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <%#Eval("SellerTaxCode")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </DetailRow>
                                    </Templates>
                                    <SettingsDetail ShowDetailRow="true" />
                                </dx:ASPxGridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <%--Edit form--%>
                <fieldset class="add-edit-form" id="InvoiceForm">
                    <legend><span class="add-edit-action"></span>&nbsp;Invoice</legend>
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hInvoiceID" />
                            <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                <tbody>
                                    <tr>
                                        <td class="ui-panelgrid-cell" style="width: 110px">
                                            <label>
                                                STT<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell validate-required">
                                            <asp:TextBox ID="txtInvSTT" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="ui-panelgrid-cell" style="width: 120px">
                                            <label>
                                                Invoice No<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell validate-required">
                                            <asp:TextBox ID="txtInvNo" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Invoice Date<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell date-time-picker validate-required">
                                            <dx:ASPxDateEdit ID="dteInvDate" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                                EditFormatString="dd-MMM-yyyy">
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Serial No<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell validate-required">
                                            <asp:TextBox ID="txtInvSerialNo" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Seller Name<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell validate-required">
                                            <asp:TextBox ID="txtInvSellerName" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Seller Tax Code<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell validate-required">
                                            <asp:TextBox ID="txtInvSellerTaxCode" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Net Cost<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell spin-edit validate-required inv-cost inv-edit">
                                            <dx:ASPxSpinEdit ID="spiInvNetCost" runat="server" Height="21px" MinValue="0" MaxValue="1000000000000"
                                                NumberType="Float" NullText="0" HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}"
                                                Style="float: left">
                                            </dx:ASPxSpinEdit>
                                            <asp:Label runat="server" ID="lblInvNetCostCurrency" Style="float: left; padding-top: 7px;
                                                margin-left: 5px;"></asp:Label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                VAT</label>
                                        </td>
                                        <td class="ui-panelgrid-cell spin-edit inv-vat inv-edit">
                                            <dx:ASPxSpinEdit ID="spiInvVAT" runat="server" Height="21px" MinValue="0" MaxValue="1000000000000"
                                                NumberType="Float" NullText="0" HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}"
                                                Style="float: left">
                                            </dx:ASPxSpinEdit>
                                            <asp:Label runat="server" ID="lblInvVATCurrency" Style="float: left; padding-top: 7px;
                                                margin-left: 5px;"></asp:Label>
                                        </td>
                                        <%--<td class="ui-panelgrid-cell">
                                            <label>
                                                Tax Rate (%)<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell validate-required spin-edit tax-rate">
                                            <dx:ASPxSpinEdit ID="spiInvTaxRate" runat="server" Height="21px" MinValue="0" MaxValue="1000000000000"
                                                NumberType="Float" NullText="0" HorizontalAlign="Right" DecimalPlaces="0" DisplayFormatString="{0:#,0}">
                                            </dx:ASPxSpinEdit>
                                        </td>--%>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Total</label>
                                        </td>
                                        <td class="ui-panelgrid-cell spin-edit">
                                            <asp:TextBox ID="spiInvTotal" runat="server" ReadOnly="true" Style="text-align: right;
                                                float: left; width: 171px !important"></asp:TextBox>
                                            <asp:Label runat="server" ID="lblInvTotalCurrency" Style="float: left; padding-top: 7px;
                                                margin-left: 5px;"></asp:Label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Item<%--<span class="required">*</span>--%></label>
                                        </td>
                                        <td class="ui-panelgrid-cell<%-- validate-required--%>">
                                            <asp:DropDownList runat="server" ID="ddlInvItem">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <%--<tr>                                        
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Supplier<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell validate-required">
                                            <asp:TextBox ID="txtInvSupplier" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <tr runat="server" id="trInvoiceCredit">
                                        <td class="ui-panelgrid-cell" colspan="2">
                                            <asp:CheckBox runat="server" ID="chkInvoiceCredit" Text=" " CssClass="check-button" />
                                            <span style="position: relative; top: -4px; padding-right: 8px">Credit Card Invoice</span>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSaveInvoice" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <ul id="InvoiceSummary" class="error-summary">
                    </ul>
                    <div class="action-pan">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <span runat="server" id="btnShowSaveInvoice">
                                    <input type="button" class="btn" value="Save" onclick="ValidateInvoiceForm(this)" />
                                </span>
                                <asp:Button runat="server" ID="btnSaveInvoice" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); HandlePartialMessageBoard(this); bindStartupEvents(this)" />
                                <asp:Button runat="server" ID="btnCancelInvoice" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                    OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this)" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">

    <script src="/js/jquery/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="/js/bt-invoicing.js" type="text/javascript"></script>

</asp:Content>
