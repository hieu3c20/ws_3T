<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BTAirTicket.aspx.vb" Inherits="BTAirTicket"
    MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="HeadContent" runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link href="/js/jquery/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function AirTicketDateChange() {
            $("[id$=ddlAirCurrency]").change()
        }  
        function OtherAirTicketDateChange() {
            $("[id$=ddlOtherAirCurrency]").change()
        }      
        var _isGA = <%= _isGA.ToString().ToLower() %>
    </script>

</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    Air Ticket Management
</asp:Content>
<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <%--Preload images--%>
    <img src="images/inform-btn.png" style="display: none" alt="" />
    <img src="images/check-check.png" style="display: none" alt="" />
    <img src="images/check.png" style="display: none" alt="" />
    <img src="images/rad-check.png" style="display: none" alt="" />
    <img src="images/rad.png" style="display: none" alt="" />
    <img src="images/triangle.png" style="display: none" alt="" />
    <%--Message Panel--%>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="panMessage">
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--BT Status--%>
    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
        <ContentTemplate>
            <div class="currency bt-status" visible="false" runat="server" id="panStatus" style="overflow: hidden;
                transition: none; float: right; margin-top: -20px; text-transform: capitalize">
                <table class="grid-edit" style="margin: 0;">
                    <tr>
                        <td style="font-weight: bold;">
                            BTS Status: <a id="lblBTSStatus" runat="server" href="#" rel="show-more" onclick="return ShowHistory()">
                            </a>
                        </td>
                        <td style="font-weight: bold;" class="ora-status">
                            <span style="padding: 0px 10px">|</span>Oracle Status: <a id="lblORAStatus" runat="server"
                                href="#" onclick="showErrorOraMessage(this); return false"></a>
                        </td>
                    </tr>
                </table>
            </div>
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
                                <%--<tr>
                                    <td class="ui-panelgrid-cell" style="width: 130px">
                                        <label>
                                            Date From</label>
                                    </td>
                                    <td class="ui-panelgrid-cell date-tim e-picker validate-required">
                                        <dx:ASPxDateEdit ID="dteSDateFrom" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy">
                                        </dx:ASPxDateEdit>
                                        <asp:HiddenField runat="server" ID="hSDateFrom" />
                                    </td>
                                    <td class="ui-panelgrid-cell" style="width: 130px">
                                        <label>
                                            Date To</label>
                                    </td>
                                    <td class="ui-panelgrid-cell date-time-picker validate-required">
                                        <dx:ASPxDateEdit ID="dteSDateTo" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy">
                                        </dx:ASPxDateEdit>
                                        <asp:HiddenField runat="server" ID="hSDateTo" />
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td class="ui-panelgrid-cell" style="width: 130px">
                                        <label>
                                            Period</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:DropDownList runat="server" ID="ddlSAirPeriod" onchange="if(!_isGA){ HandleMessage(this); bindStartupEvents(this); }">
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="hSAirPeriod" />
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Supplier</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:DropDownList runat="server" ID="ddlSOraSupplier">
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="hSOraSupplier" />
                                    </td>
                                </tr>
                                <tr runat="server" id="trGeneral1" class="general-info">
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Airline</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtSAirline" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hSAirline" />
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Ticket No</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtSTicketNo" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hSTicketNo" />
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
                                            Employee Name</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtSEmployeeName" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hSEmployeeName" />
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
                                            <asp:Button ID="btnSearch" CssClass="btnSearch" runat="server" Text="" OnClientClick="btnCancelSub_Click($('[id$=btnCancelAirTicket]')[0]); btnCancelSub_Click($('[id$=btnCancelOtherAirTicket]')[0]); HandleMessage(this); bindStartupEvents(this)" />
                                            <i class="search"></i>Search</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <%--Air ticket--%>
        <div class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top active"
                role="tab">
                <span class="ui-icon"></span>Air Ticket Management</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content default-hide"
                role="tabpanel">
                <div class="HRTabControl">
                    <div class="HRTabNav">
                        <ul>
                            <li>Employee</li>
                            <li>Other</li>
                        </ul>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <div class="HRTabList">
                        <%--Employee--%>
                        <div class="HRTab">
                            <%--Buttons--%>
                            <div runat="server" id="btnShowAddAirTicket" style="float: left;">
                                <span id="btnAddAirTicket" style="text-align: center; font-weight: normal; padding-right: 14px;
                                    display: block;" class="btn inform" onclick="btnAddSub_Click(this)"><i class="add">
                                    </i><span>Add</span></span>
                            </div>
                            <div runat="server" id="btnShowImportAirTicket" style="float: left;">
                                <span id="btnImportAirTicket" style="text-align: center; font-weight: normal; padding-right: 14px;
                                    display: block;" class="btn inform"><i class="excel"></i><span>Import</span></span>
                            </div>
                            <div style="float: left">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <span id="btnShowImportAirTicketError" runat="server" style="text-align: center;
                                            font-weight: normal; padding-right: 14px;" class="btn inform view-error hide"><i
                                                class="triangel-error"></i><span>View Import Errors</span>
                                            <asp:Button runat="server" ID="btnViewImportAirTicketError" OnClientClick="CheckImportErrorStatus($('[id$=btnSearch]')[0], '');bindStartupEvents($('[id$=btnSearch]')[0]); HandleMessage($('[id$=btnSearch]')[0]);" />
                                        </span>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="bt-legend" style="float: right; padding: 5px 0 10px;">
                                <strong>BTS Legend of BT Approval:</strong> <span class="ora-pending" style="padding: 0 5px;
                                    border-right: 1px solid #ccc;">Pending</span> <span class="ora-done" style="padding: 0 5px;
                                        border-right: 1px solid #ccc;">Budget Checked</span> <span class="waiting" style="padding: 0 5px;
                                            border-right: 1px solid #ccc;">Confirm</span> <span class="ora-rejected" style="padding: 0 5px;
                                                border-right: 1px solid #ccc;">Rejected</span> <span class="ora-completed" style="padding: 0 5px;">
                                                    Completed</span>
                            </div>
                            <div style="clear: both">
                            </div>
                            <%--List--%>
                            <div class="ui-datatable ui-widget">
                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                    <ContentTemplate>
                                        <div style="margin-top: 10px; border: 1px solid transparent">
                                            <dx:ASPxGridView ID="grvBTAirTicket" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                                AutoGenerateColumns="false" Style="margin-top: 0">
                                                <SettingsText EmptyDataRow="No records found!" />
                                                <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="true" />
                                                <SettingsPager PageSize="100" NumericButtonCount="10">
                                                    <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All" Caption="Page Size"
                                                        Items="20, 30, 50, 100" />
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
                                                    <dx:GridViewDataColumn CellStyle-CssClass="btid" Width="45px" Settings-AllowAutoFilter="False"
                                                        FieldName="BTRegisterID" Caption="BT ID" />
                                                    <dx:GridViewDataDateColumn FieldName="TicketDate" Caption="Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                    <dx:GridViewDataColumn FieldName="Employee" Caption="Employee" Width="155px" />
                                                    <dx:GridViewDataColumn FieldName="TicketNo" Caption="Ticket No" />
                                                    <dx:GridViewDataColumn FieldName="Routing" Caption="Routing" />
                                                    <dx:GridViewBandColumn Caption="Net Payment">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="NetPayment_Domestic" Width="90px" Caption="Dome.air"
                                                                CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                            <dx:GridViewDataTextColumn FieldName="NetPayment" Width="70px" Caption="Int.air"
                                                                CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewDataImageColumn Width="70px" CellStyle-HorizontalAlign="Left">
                                                        <DataItemTemplate>
                                                            <% If Not _isGA Then%>
                                                            <input type="button" style="visibility: hidden; width: 8px; padding: 0px;" />
                                                            <% End If%>
                                                            <asp:Button ID="btnEdit" runat="server" ToolTip="View/Edit" CssClass='<%# If(Convert.ToBoolean(Eval("EnableForm")) AndAlso Convert.ToBoolean(Eval("AirEnable")), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                data-id='<%# Eval("ID") %>' OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])"
                                                                OnClick="btnEditAirTicket_OnClick"></asp:Button>
                                                            <input type="button" class="grid-btn delete-btn" title="Delete" runat="server" id="btnSubConfirmDelete"
                                                                visible='<%# Convert.ToBoolean(Eval("EnableForm")) AndAlso Convert.ToBoolean(Eval("AirEnable"))%>'
                                                                data-id='<%# Eval("ID") %>' onclick="btnDeleteAirTicketClick(this)" />
                                                            <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' Visible='<%# Convert.ToBoolean(Eval("EnableForm")) AndAlso Convert.ToBoolean(Eval("AirEnable"))%>'
                                                                ToolTip="Delete" runat="server" CssClass="hide" OnClick="btnDeleteAirTicket_OnClick"
                                                                OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])">
                                                            </asp:Button>
                                                            <asp:Button ID="btnViewBT" runat="server" ToolTip="View BT Information" CssClass="grid-btn info-btn"
                                                                data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnViewBTClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])"
                                                                OnClick="btnViewBT_OnClick"></asp:Button>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </dx:GridViewDataImageColumn>
                                                </Columns>
                                                <Templates>
                                                    <DetailRow>
                                                        <fieldset style="width: 98%; padding: 0; border: none; margin: 10px 0;">
                                                            <table class="tblRequestDetails grid-inside" style="table-layout: fixed">
                                                                <tr>
                                                                    <th rowspan="2" style="width: 100px">
                                                                        Airline
                                                                    </th>
                                                                    <th colspan="5">
                                                                        Price
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        Fare
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        VAT / Tax
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        ATP Tax / HĐ HK
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        SF
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        Currency
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left">
                                                                        <%#Eval("Airline")%>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <%#Eval("Fare", "{0:#,0.##}")%>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <%#Eval("VAT", "{0:#,0.##}")%>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <%#Eval("APTTax", "{0:#,0.##}")%>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <%#Eval("SF", "{0:#,0.##}")%>
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <%#Eval("Currency")%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </DetailRow>
                                                </Templates>
                                                <SettingsDetail ShowDetailRow="true" />
                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem FieldName="NetPayment_Domestic" DisplayFormat="{0:#,0.##}" SummaryType="Sum" />
                                                </TotalSummary>
                                            </dx:ASPxGridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <%--Edit form--%>
                            <fieldset class="add-edit-form" id="AirTicketForm">
                                <legend><span class="add-edit-action"></span>&nbsp;Air Ticket</legend>
                                <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hAirTicketID" />
                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                            <tbody>
                                                <tr>
                                                    <td class="ui-panelgrid-cell" style="width: 110px">
                                                        <label>
                                                            Date<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell date-time-picker validate-required air-date">
                                                        <dx:ASPxDateEdit ID="dteAirDate" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                                            ClientSideEvents-DateChanged="AirTicketDateChange" EditFormatString="dd-MMM-yyyy">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" style="width: 110px">
                                                        <label>
                                                            Airline<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:TextBox ID="txtAirline" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Ticket No<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:TextBox ID="txtAirTicketNo" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Routing<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:TextBox ID="txtAirRouting" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Fare</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell spin-edit air-payment">
                                                        <dx:ASPxSpinEdit ID="spiAirFare" runat="server" Height="21px" MinValue="0" MaxValue="1000000000000"
                                                            NumberType="Float" NullText="0" HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            VAT / Tax</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell spin-edit air-payment">
                                                        <dx:ASPxSpinEdit ID="spiAirVAT" runat="server" Height="21px" MinValue="0" MaxValue="1000000000000"
                                                            NumberType="Float" NullText="0" HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            APT Tax / HĐ HK</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell spin-edit air-payment">
                                                        <dx:ASPxSpinEdit ID="spiAirAPTTax" runat="server" Height="21px" MinValue="0" MaxValue="1000000000000"
                                                            NumberType="Float" NullText="0" HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            SF</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell spin-edit air-payment">
                                                        <dx:ASPxSpinEdit ID="spiAirSF" runat="server" Height="21px" MinValue="0" MaxValue="1000000000000"
                                                            NumberType="Float" NullText="0" HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Net Payment<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell spin-edit validate-required air-net-payment">
                                                        <dx:ASPxSpinEdit ID="spiAirNetPayment" runat="server" Height="21px" MinValue="0"
                                                            MaxValue="1000000000000" NumberType="Float" NullText="0" HorizontalAlign="Right"
                                                            DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" ReadOnly="true">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Currency<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:DropDownList runat="server" ID="ddlAirCurrency" data-exrate="air-exrate">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Exchange rate</label>
                                                        <br />
                                                        <asp:Label runat="server" ID="lblAirExrate"></asp:Label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell spin-edit exrate-value" data-exrate="air-exrate">
                                                        <dx:ASPxSpinEdit ID="spiAirExrate" runat="server" Height="21px" MinValue="1" MaxValue="1000000000000"
                                                            NumberType="Float" NullText="1" HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Period<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:DropDownList runat="server" ID="ddlAirPeriod">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Supplier<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:DropDownList runat="server" ID="ddlOraSupplier">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" style="width: 110px">
                                                        <label>
                                                            Passenger<span runat="server" id="spanPassenger"></span></label>
                                                    </td>
                                                    <td runat="server" id="tdPassenger" class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtPassenger" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell" style="width: 110px">
                                                        <label>
                                                            Requester Code<span runat="server" id="spanRequesterCode"></span></label>
                                                    </td>
                                                    <td runat="server" id="tdRequesterCode" class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="employee-code" data-hidden="hEmployeeInfo"
                                                            data-button="btnGetEmployeeInfo"></asp:TextBox>
                                                        <asp:HiddenField runat="server" ID="hEmployeeInfo" />
                                                        <asp:Button runat="server" ID="btnGetEmployeeInfo" CssClass="hide" OnClientClick="HandleMessage(this); bindStartupEvents(this)" />
                                                    </td>
                                                    <td class="ui-panelgrid-cell" style="width: 110px">
                                                        <label>
                                                            Requester Name<span runat="server" id="spanRequesterName"></span></label>
                                                    </td>
                                                    <td runat="server" id="tdRequesterName" class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtEmployeeName" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell" style="width: 110px">
                                                        <label>
                                                            Requester Division</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtEmployeeDiv" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" style="width: 110px">
                                                        <label>
                                                            Requester Department</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtEmployeeDept" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell" style="width: 120px">
                                                        <label>
                                                            Departure Date<span runat="server" id="spanDepartureDate"></span></label>
                                                    </td>
                                                    <td runat="server" id="tdDepartureDate" class="ui-panelgrid-cell date-time-picker">
                                                        <dx:ASPxDateEdit ID="dteDepartureDate" runat="server" ClientInstanceName="departureDate"
                                                            EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm">
                                                            <TimeSectionProperties Visible="true">
                                                                <TimeEditProperties EditFormatString="HH:mm" />
                                                            </TimeSectionProperties>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" style="width: 120px">
                                                        <label>
                                                            Return Date</label>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell date-time-picker">
                                                        <dx:ASPxDateEdit ID="dteReturnDate" runat="server" ClientInstanceName="returnDate"
                                                            EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm">
                                                            <TimeSectionProperties Visible="true">
                                                                <TimeEditProperties EditFormatString="HH:mm" />
                                                            </TimeSectionProperties>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Budget Code<span runat="server" id="spanEBudgetCode"></span></label>
                                                    </td>
                                                    <td runat="server" id="tdEBudgetCode" class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtEBudgetCode" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:CheckBox runat="server" Enabled="false" ID="chkEmpICTRequest" Text=" " CssClass="check-button"
                                                            Style="top: 2px;" />
                                                        <span style="position: relative; top: -3px;">ICT Request</span>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSaveAirTicket" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <ul id="AirTicketSummary" class="error-summary">
                                </ul>
                                <div class="action-pan">
                                    <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                        <ContentTemplate>
                                            <span runat="server" id="btnEShowConfirmBudget" visible="false">
                                                <input type="button" class="btn" value="Confirm Budget" onclick="showConfirmEConfirmBudget()" />
                                            </span>
                                            <asp:Button runat="server" ID="btnEConfirmBudget" Visible="false" Text="Confirm Budget"
                                                CssClass="btn hide" OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])" />
                                            <span runat="server" id="btnEShowRejectBudget" visible="false">
                                                <input type="button" class="btn secondary" value="Reject Budget" onclick="showConfirmERejectBudget()" />
                                            </span>
                                            <asp:Button runat="server" ID="btnERejectBudget" Visible="false" Text="Reject Budget"
                                                CssClass="btn hide" OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])" />
                                            <%----%>
                                            <span runat="server" id="btnShowSaveAirTicket">
                                                <input type="button" class="btn" value="Save" onclick="ValidateAirTicketForm(this)" />
                                            </span>
                                            <asp:Button runat="server" ID="btnSaveAirTicket" Text="Save" CssClass="btn hide"
                                                OnClientClick="HandleMessage(this); HandlePartialMessageBoard(this); bindStartupEvents(this)" />
                                            <asp:Button runat="server" ID="btnCancelAirTicket" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this)" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                        </div>
                        <%--Other--%>
                        <div class="HRTab">
                            <%--Buttons--%>
                            <div runat="server" id="btnShowAddOtherAirTicket" style="float: left;">
                                <span id="btnAddOtherAirTicket" style="text-align: center; font-weight: normal; padding-right: 14px;
                                    display: block;" class="btn inform" onclick="btnAddSub_Click(this)"><i class="add">
                                    </i><span>Add</span></span>
                            </div>
                            <div runat="server" id="btnShowImportOtherAirTicket" style="float: left;">
                                <span id="btnImportOtherAirTicket" style="text-align: center; font-weight: normal;
                                    padding-right: 14px; display: block;" class="btn inform"><i class="excel"></i><span>
                                        Import1</span></span>
                            </div>
                            <div style="float: left">
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <span id="btnShowImportOtherError" runat="server" style="text-align: center; font-weight: normal;
                                            padding-right: 14px;" class="btn inform view-error"><i class="triangel-error"></i>
                                            <span>View Import Errors</span>
                                            <asp:Button runat="server" ID="btnViewImportOtherError" OnClientClick="CheckImportErrorStatus($('[id$=btnSearch]')[0], 'o');bindStartupEvents($('[id$=btnSearch]')[0]); HandleMessage($('[id$=btnSearch]')[0]);" />
                                        </span>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="bt-legend" style="float: right; padding: 5px 0 10px;">
                                <strong>BTS Legend:</strong> <span class="ora-pending" style="padding: 0 5px; border-right: 1px solid #ccc;">
                                    Budget Unchecked</span> <span class="ora-done" style="padding: 0 5px;">Budget Checked</span>
                            </div>
                            <div style="clear: both">
                            </div>
                            <%--List--%>
                            <div class="ui-datatable ui-widget">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <div style="margin-top: 10px; border: 1px solid transparent">
                                            <dx:ASPxGridView ID="grvOtherAirTicket" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                                AutoGenerateColumns="false" Style="margin-top: 0">
                                                <SettingsText EmptyDataRow="No records found!" />
                                                <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="true" />
                                                <SettingsPager PageSize="100" NumericButtonCount="10">
                                                    <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All" Caption="Page Size"
                                                        Items="20, 30, 50, 100" />
                                                </SettingsPager>
                                                <Styles>
                                                    <AlternatingRow Enabled="True">
                                                    </AlternatingRow>
                                                </Styles>
                                                <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                                <Columns>
                                                    <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                                        FieldName="ID" Caption="ID" />
                                                    <dx:GridViewDataDateColumn FieldName="TicketDate" Caption="Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                    <dx:GridViewDataColumn FieldName="Passenger" Caption="Passenger" />
                                                    <dx:GridViewDataColumn FieldName="Requester" Caption="Requester" />
                                                    <dx:GridViewDataColumn FieldName="TicketNo" Caption="Ticket No" />
                                                    <dx:GridViewDataColumn FieldName="BudgetCode" Caption="Budget Code" />
                                                    <dx:GridViewBandColumn Caption="Net Payment">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="NetPayment_Domestic" Width="90px" Caption="Dome.air"
                                                                CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                            <dx:GridViewDataTextColumn FieldName="NetPayment" Width="70px" Caption="Int.air"
                                                                CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                        </Columns>
                                                    </dx:GridViewBandColumn>
                                                    <dx:GridViewDataImageColumn Width="40px" CellStyle-HorizontalAlign="Left">
                                                        <DataItemTemplate>
                                                            <asp:Button ID="btnEdit" runat="server" ToolTip="View/Edit" CssClass='<%# If(Convert.ToBoolean(Eval("EnableForm")) AndAlso Convert.ToBoolean(Eval("AirEnable")), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                data-id='<%# Eval("ID") %>' OnClientClick="btnEditOtherRequestClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])"
                                                                OnClick="btnEditOtherAirTicket_OnClick"></asp:Button>
                                                            <input type="button" class="grid-btn delete-btn" title="Delete" runat="server" id="btnSubConfirmDeleteOther"
                                                                visible='<%# Convert.ToBoolean(Eval("EnableForm")) AndAlso Convert.ToBoolean(Eval("AirEnable"))%>'
                                                                data-id='<%# Eval("ID") %>' onclick="btnDeleteOtherAirTicketClick(this)" />
                                                            <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' Visible='<%# Convert.ToBoolean(Eval("EnableForm")) AndAlso Convert.ToBoolean(Eval("AirEnable"))%>'
                                                                ToolTip="Delete" runat="server" CssClass="hide" OnClick="btnDeleteOtherAirTicket_OnClick"
                                                                OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])">
                                                            </asp:Button>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </dx:GridViewDataImageColumn>
                                                </Columns>
                                                <Templates>
                                                    <DetailRow>
                                                        <fieldset style="width: 98%; padding: 0; border: none; margin: 10px 0;">
                                                            <table class="tblRequestDetails grid-inside" style="table-layout: fixed">
                                                                <tr>
                                                                    <th rowspan="2">
                                                                        Routing
                                                                    </th>
                                                                    <th rowspan="2" style="width: 100px">
                                                                        Airline
                                                                    </th>
                                                                    <th colspan="5">
                                                                        Price
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: center">
                                                                        Fare
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        VAT
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        ATP Tax / HĐ HK
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        SF
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        Currency
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="text-align: left">
                                                                        <%#Eval("Routing")%>
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <%#Eval("Airline")%>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <%#Eval("Fare", "{0:#,0.##}")%>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <%#Eval("VAT", "{0:#,0.##}")%>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <%#Eval("APTTax", "{0:#,0.##}")%>
                                                                    </td>
                                                                    <td style="text-align: right">
                                                                        <%#Eval("SF", "{0:#,0.##}")%>
                                                                    </td>
                                                                    <td style="text-align: left">
                                                                        <%#Eval("Currency")%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </fieldset>
                                                    </DetailRow>
                                                </Templates>
                                                <SettingsDetail ShowDetailRow="true" />
                                                <TotalSummary>
                                                    <dx:ASPxSummaryItem FieldName="NetPayment_Domestic" DisplayFormat="{0:#,0.##}" SummaryType="Sum" />
                                                </TotalSummary>
                                            </dx:ASPxGridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <%--Edit form--%>
                            <fieldset class="add-edit-form" id="OtherAirTicketForm">
                                <legend><span class="add-edit-action"></span>&nbsp;Air Ticket</legend>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hOtherAirTicketID" />
                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                            <tbody>
                                                <tr>
                                                    <td class="ui-panelgrid-cell" style="width: 110px">
                                                        <label>
                                                            Date<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell date-time-picker validate-required air-date">
                                                        <dx:ASPxDateEdit ID="dteOtherDate" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                                            ClientSideEvents-DateChanged="OtherAirTicketDateChange" EditFormatString="dd-MMM-yyyy">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" style="width: 110px">
                                                        <label>
                                                            Airline<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:TextBox ID="txtOtherAirline" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Ticket No<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:TextBox ID="txtOtherTicketNo" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Routing<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:TextBox ID="txtOtherRouting" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell" style="width: 120px">
                                                        <label>
                                                            Departure Date<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell date-time-picker validate-required">
                                                        <dx:ASPxDateEdit ID="dteOtherDepartureDate" runat="server" ClientInstanceName="departureDate"
                                                            EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm">
                                                            <TimeSectionProperties Visible="true">
                                                                <TimeEditProperties EditFormatString="HH:mm" />
                                                            </TimeSectionProperties>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" style="width: 120px">
                                                        <label>
                                                            Return Date</label>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell date-time-picker">
                                                        <dx:ASPxDateEdit ID="dteOtherReturnDate" runat="server" ClientInstanceName="returnDate"
                                                            EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm">
                                                            <TimeSectionProperties Visible="true">
                                                                <TimeEditProperties EditFormatString="HH:mm" />
                                                            </TimeSectionProperties>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Fare</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell spin-edit other-air-payment">
                                                        <dx:ASPxSpinEdit ID="spiOtherFare" runat="server" Height="21px" MinValue="0" MaxValue="1000000000000"
                                                            NumberType="Float" NullText="0" HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            VAT / TAX</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell spin-edit other-air-payment">
                                                        <dx:ASPxSpinEdit ID="spiOtherVAT" runat="server" Height="21px" MinValue="0" MaxValue="1000000000000"
                                                            NumberType="Float" NullText="0" HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            APT Tax / HĐ HK</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell spin-edit other-air-payment">
                                                        <dx:ASPxSpinEdit ID="spiOtherAPTTax" runat="server" Height="21px" MinValue="0" MaxValue="1000000000000"
                                                            NumberType="Float" NullText="0" HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            SF</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell spin-edit other-air-payment">
                                                        <dx:ASPxSpinEdit ID="spiOtherSF" runat="server" Height="21px" MinValue="0" MaxValue="1000000000000"
                                                            NumberType="Float" NullText="0" HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Net Payment<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell spin-edit validate-required other-net-payment">
                                                        <dx:ASPxSpinEdit ID="spiOtherNetPayment" runat="server" Height="21px" MinValue="0"
                                                            MaxValue="1000000000000" NumberType="Float" NullText="0" HorizontalAlign="Right"
                                                            DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" ReadOnly="true">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Currency<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:DropDownList runat="server" ID="ddlOtherAirCurrency" data-exrate="other-air-exrate">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Exchange rate</label>
                                                        <br />
                                                        <asp:Label runat="server" ID="Label1"></asp:Label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell spin-edit exrate-value" data-exrate="other-air-exrate">
                                                        <dx:ASPxSpinEdit ID="spiOtherAirExrate" runat="server" Height="21px" MinValue="1"
                                                            MaxValue="1000000000000" NumberType="Float" NullText="1" HorizontalAlign="Right"
                                                            DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Passenger<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:TextBox ID="txtOtherPassenger" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Requester Code<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:TextBox ID="txtOtherRequesterCode" runat="server" CssClass="employee-code" data-hidden="hOtherRequester"
                                                            data-button="btnGetRequesterInfo"></asp:TextBox>
                                                        <asp:HiddenField runat="server" ID="hOtherRequester" />
                                                        <asp:Button runat="server" ID="btnGetRequesterInfo" CssClass="hide" OnClientClick="HandleMessage(this); bindStartupEvents(this)" />
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Requester Name<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:TextBox ID="txtOtherRequesterName" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Requester Division</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtOtherRequesterDivision" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Requester Department</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtOtherRequesterDepartment" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Requester Phone</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtOtherRequesterPhone" ReadOnly="true" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Period<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:DropDownList runat="server" ID="ddlOtherAirPeriod">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Supplier<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:DropDownList runat="server" ID="ddlOtherOraSupplier">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Budget Code<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:TextBox ID="txtOtherBudgetCode" runat="server"></asp:TextBox>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Purpose<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:TextBox ID="txtOtherPurpose" TextMode="MultiLine" ToolTip="Please enter event/project name for this air ticket"
                                                            data-tooltip="all" Rows="3" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td class="ui-panelgrid-cell">
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:CheckBox runat="server" ID="chkICTRequest" Text=" " CssClass="check-button"
                                                            Style="top: 2px;" />
                                                        <span style="position: relative; top: -3px;">ICT Request</span>
                                                    </td>
                                                </tr>--%>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSaveAirTicket" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <ul id="OtherAirTicketSummary" class="error-summary">
                                </ul>
                                <div class="action-pan">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <span runat="server" id="btnShowConfirmBudget" visible="false">
                                                <input type="button" class="btn" value="Confirm Budget" onclick="showConfirmConfirmBudget()" />
                                            </span>
                                            <asp:Button runat="server" ID="btnConfirmBudget" Visible="false" Text="Confirm Budget"
                                                CssClass="btn hide" OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])" />
                                            <span runat="server" id="btnShowRejectBudget" visible="false">
                                                <input type="button" class="btn secondary" value="Reject Budget" onclick="showConfirmRejectBudget()" />
                                            </span>
                                            <asp:Button runat="server" ID="btnRejectBudget" Visible="false" Text="Reject Budget"
                                                CssClass="btn hide" OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])" />
                                            <%----%>
                                            <span runat="server" id="btnShowSaveOtherAirTicket">
                                                <input type="button" class="btn" value="Save" onclick="ValidateOtherAirTicketForm(this)" />
                                            </span>
                                            <asp:Button runat="server" ID="btnSaveOtherAirTicket" Text="Save" CssClass="btn hide"
                                                OnClientClick="HandleMessage(this); bindStartupEvents(this)" />
                                            <asp:Button runat="server" ID="btnCancelOtherAirTicket" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this)" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
                <%--Actions--%>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <div id="divTransfer" class="action-pan no-transition" runat="server">
                            <span runat="server" id="btnShowSubmit" visible="false" class="btn" style="padding-left: 6px;
                                display: inline-block; padding-right: 20px;"><i class="approval-btn"></i><span style="padding-left: 10px;">
                                    Submit to FI Budget</span>
                                <input type="button" value="Submit" class="btn" onclick="showSubmitMessage()" /></span>
                            <span runat="server" id="btnShowConfirmAllBudget" visible="false" class="btn" style="padding-left: 6px;
                                display: inline-block; padding-right: 20px;"><i class="approval-btn"></i><span style="padding-left: 10px;">
                                    Confirm Budget</span>
                                <input type="button" value="Confirm Budget" class="btn" onclick="showConfirmBudgetMessage()" /></span>
                            <span runat="server" id="btnShowReconfirmAllBudget" visible="false" class="btn secondary"
                                style="padding-left: 6px; display: inline-block; padding-right: 20px;"><i class="reject-btn">
                                </i><span style="padding-left: 10px;">Re-Confirm GA</span>
                                <input type="button" value="Re-Confirm GA" class="btn secondary" onclick="showReConfirmGAMessage()" /></span>
                            <span runat="server" id="btnShowTransfer" visible="false" class="btn" style="padding-left: 6px;
                                padding-right: 20px; display: inline-block;"><i class="approval-btn"></i><span style="padding-left: 10px"
                                    runat="server" id="spanTransferLabel">Transfer to Oracle</span>
                                <input type="button" value="Transfer" class="btn" onclick="showApproveMessage()" /></span>
                            <span runat="server" id="btnShowReject" visible="false" class="btn secondary" style="padding-left: 6px;
                                display: inline-block;"><i class="reject-btn"></i><span>Reject</span>
                                <input type="button" value="Reject" class="btn" onclick="showRejectMessage()" /></span>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
    <%--Confirm Budget--%>
    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
        <ContentTemplate>
            <div id="panConfirmBudget" runat="server" visible="false" class="popup-container">
                <table style="width: 100%; height: 100%; margin: 0;">
                    <tr>
                        <td style="vertical-align: middle; border: none !important; padding: 0 !important;">
                            <table class="grid-edit" style="width: auto !important; margin: auto; background-color: #fff;
                                border-radius: 5px; box-shadow: 0 0 10px #fff; -webkit-box-shadow: 0 0 10px #fff;
                                -moz-box-shadow: 0 0 10px #fff;">
                                <tr>
                                    <td style="padding: 5px 30px 15px;">
                                        <h3 style="margin: 0; padding: 5px 0 3px; background-color: #fff; border-bottom: 1px solid #999;">
                                            Submit to Finance Accouting to check/process</h3>
                                        <div style="margin-top: 10px; font-weight: bold;">
                                            Comment:
                                        </div>
                                        <asp:TextBox ID="txtConfirmComment" class="reject-reason" Style="width: auto !important;
                                            margin-top: 5px;" TextMode="MultiLine" Rows="5" Columns="60" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 0 0 15px; text-align: center;">
                                        <asp:Button ID="btnConfirmAllBudget" runat="server" Text="Confirm Budget" Style="text-align: center;"
                                            CssClass="btn" OnClientClick="bindStartupEvents($('[id$=btnSearch]')[0]); HandleMessage($('[id$=btnSearch]')[0]);" />
                                        <input type="button" value="Cancel" onclick="hideConfirmBudgetMessage(); $('[id$=txtConfirmComment]').val('')"
                                            style="margin-left: 5px;" id="btnConfirmBudgetCancel" class="btn secondary" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Reconfirm Budget--%>
    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
        <ContentTemplate>
            <div id="panReconfirmBudget" runat="server" visible="false" class="popup-container">
                <table style="width: 100%; height: 100%; margin: 0;">
                    <tr>
                        <td style="vertical-align: middle; border: none !important; padding: 0 !important;">
                            <table class="grid-edit" style="width: auto !important; margin: auto; background-color: #fff;
                                border-radius: 5px; box-shadow: 0 0 10px #fff; -webkit-box-shadow: 0 0 10px #fff;
                                -moz-box-shadow: 0 0 10px #fff;">
                                <tr>
                                    <td style="padding: 5px 30px 15px;">
                                        <h3 style="margin: 0; padding: 5px 0 3px; background-color: #fff; border-bottom: 1px solid #999;">
                                            Send Email to GA to confirm budget</h3>
                                        <div style="margin-top: 10px; font-weight: bold;">
                                            Recommendation:
                                        </div>
                                        <asp:TextBox ID="txtReconfirmComment" class="reject-reason" Style="width: auto !important;
                                            margin-top: 5px;" TextMode="MultiLine" Rows="5" Columns="60" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 0 0 15px; text-align: center;">
                                        <asp:Button ID="btnReconfirmAllBudget" runat="server" Text="Reject" Style="text-align: center;"
                                            CssClass="btn" OnClientClick="if(!checkReconfirm()) return false; bindStartupEvents($('[id$=btnSearch]')[0]); HandleMessage($('[id$=btnSearch]')[0]);" />
                                        <input type="button" value="Cancel" onclick="hideReconfirmBudgetMessage(); $('[id$=txtRejectReason]').val('')"
                                            style="margin-left: 5px;" id="btnReconfirmBudgetCancel" class="btn secondary" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Submit to FI--%>
    <asp:UpdatePanel ID="UpdatePanel90" runat="server">
        <ContentTemplate>
            <div id="panSubmitInfo" runat="server" visible="false" class="popup-container">
                <table style="width: 100%; height: 100%; margin: 0;">
                    <tr>
                        <td style="vertical-align: middle; border: none !important; padding: 0 !important;">
                            <table class="grid-edit" style="width: auto !important; margin: auto; background-color: #fff;
                                border-radius: 5px; box-shadow: 0 0 10px #fff; -webkit-box-shadow: 0 0 10px #fff;
                                -moz-box-shadow: 0 0 10px #fff;">
                                <tr>
                                    <td style="padding: 5px 30px 15px;">
                                        <h3 style="margin: 0; padding: 5px 0 3px; background-color: #fff; border-bottom: 1px solid #999;">
                                            Submit to Finance Budget to check budget</h3>
                                        <div style="margin-top: 10px; font-weight: bold;">
                                            Comment:
                                        </div>
                                        <asp:TextBox ID="txtSubmitComment" class="reject-reason" Style="width: auto !important;
                                            margin-top: 5px;" TextMode="MultiLine" Rows="5" Columns="60" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 0 0 15px; text-align: center;">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" Style="text-align: center;"
                                            CssClass="btn" OnClientClick="bindStartupEvents($('[id$=btnSearch]')[0]); HandleMessage($('[id$=btnSearch]')[0]);" />
                                        <input type="button" value="Cancel" onclick="hideSubmitMessage(); $('[id$=txtSubmitComment]').val('')"
                                            style="margin-left: 5px;" id="btnSubmitCancel" class="btn secondary" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Tranfer to oracle--%>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <div id="tabApproveMessage" runat="server" class="popup-container">
                <table style="width: 100%; height: 100%; margin: 0;">
                    <tr>
                        <td style="vertical-align: middle; border: none !important; padding: 0 !important;">
                            <table class="grid-edit" style="width: auto !important; margin: auto; background-color: #fff;
                                border-radius: 5px; box-shadow: 0 0 10px #fff; -webkit-box-shadow: 0 0 10px #fff;
                                -moz-box-shadow: 0 0 10px #fff;">
                                <tr>
                                    <td style="padding: 5px 30px 15px;" colspan="2">
                                        <h3 style="margin: 0; padding: 5px 0 3px; background-color: #fff; border-bottom: 1px solid #999;">
                                            Transfer Air Tickets to Oracle (Invoice)</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 0px 40px 5px;" id="approve-message" colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 40px 0 40px;" colspan="2">
                                        <h4 style="margin: 0; padding: 5px 0 3px; background-color: #fff; text-decoration: underline;
                                            border: none;">
                                            Summary (VND)</h4>
                                        <table class="air-ticket-summary" style="margin-top: 0;">
                                            <tr>
                                                <th>
                                                    Ticket Qty
                                                </th>
                                                <th>
                                                    VAT
                                                </th>
                                                <th>
                                                    Net Payment
                                                </th>
                                                <th style="background-color: #dedede">
                                                    Total Amount
                                                </th>
                                            </tr>
                                            <tr>
                                                <td runat="server" id="tdAirQuantity" style="width: 10%">
                                                </td>
                                                <td runat="server" id="tdAirVAT" style="width: 18%">
                                                </td>
                                                <td runat="server" id="tdAirNet" style="width: 18%">
                                                </td>
                                                <td runat="server" id="tdAirNetPayment" style="background-color: #dedede; color: Red;
                                                    font-weight: bold; width: 18%">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 5px 0 40px;" class="date-time-picker">
                                        <h4 style="margin: 0; padding: 5px 0 3px; background-color: #fff; text-decoration: underline;
                                            border: none;">
                                            Invoice Date</h4>
                                        <dx:ASPxDateEdit ID="dteInvoiceDate" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td style="padding: 5px 40px 0 40px;" class="date-time-picker">
                                        <h4 style="margin: 0; padding: 5px 0 3px; background-color: #fff; text-decoration: underline;
                                            border: none;">
                                            GL Date</h4>
                                        <dx:ASPxDateEdit ID="dteGLDate" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy">
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 5px 0 40px;">
                                        <h4 style="margin: 0; padding: 4px 0 3px; background-color: #fff; text-decoration: underline;
                                            border: none;">
                                            Batch Name</h4>
                                        <asp:DropDownList runat="server" ID="ddlBatchName">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="padding: 5px 30px;">
                                        <ul id="approve-summary">
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 0 15px; text-align: center;" colspan="2">
                                        <asp:Button ID="btnTransferToOra" ToolTip="Transfer" runat="server" CssClass="btn"
                                            Text="Transfer" OnClientClick="if(!checkApprove()) return false; bindStartupEvents($('[id$=btnSearch]')[0]); HandleMessage($('[id$=btnSearch]')[0]);" />
                                        <input type="button" value="Cancel" onclick="hideApproveMessage();" style="margin-left: 5px;"
                                            id="btnApproveCancel" class="btn secondary" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Reject--%>
    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
        <ContentTemplate>
            <div id="panRejectInfo" runat="server" visible="false" class="popup-container">
                <table style="width: 100%; height: 100%; margin: 0;">
                    <tr>
                        <td style="vertical-align: middle; border: none !important; padding: 0 !important;">
                            <table class="grid-edit" style="width: auto !important; margin: auto; background-color: #fff;
                                border-radius: 5px; box-shadow: 0 0 10px #fff; -webkit-box-shadow: 0 0 10px #fff;
                                -moz-box-shadow: 0 0 10px #fff;">
                                <tr>
                                    <td style="padding: 5px 30px 15px;">
                                        <h3 style="margin: 0; padding: 5px 0 3px; background-color: #fff; border-bottom: 1px solid #999;">
                                            Reject to GA to update information</h3>
                                        <div style="margin-top: 10px; font-weight: bold;">
                                            Recommendation:
                                        </div>
                                        <asp:TextBox ID="txtRejectReason" class="reject-reason" Style="width: auto !important;
                                            margin-top: 5px;" TextMode="MultiLine" Rows="5" Columns="60" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 0 0 15px; text-align: center;">
                                        <asp:Button ID="btnReject" runat="server" Text="Reject" Style="text-align: center;"
                                            CssClass="btn" OnClientClick="if(!checkReject()) return false; bindStartupEvents($('[id$=btnSearch]')[0]); HandleMessage($('[id$=btnSearch]')[0]);" />
                                        <input type="button" value="Cancel" onclick="hideRejectMessage(); $('[id$=txtRejectReason]').val('')"
                                            style="margin-left: 5px;" id="btnRejectCancel" class="btn secondary" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--History--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="BTStatusHistory" style="position: fixed; top: 0; left: 0; width: 100%; height: 100%;
                background-color: rgba(0, 0, 0, 0.6); z-index: 9000; display: none; transition: none;">
                <div id="BTStatusContent" style="width: 800px; margin: 100px auto; transition: none;
                    background-color: #fff; padding: 10px 0 15px; border-radius: 5px;" class="popup-widget">
                    <h4 class="widget-title">
                        Status History</h4>
                    <div class="close-widget" onclick="HideHistory()">
                    </div>
                    <dx:ASPxGridView runat="server" ID="grvStatusHistory" SettingsPager-Mode="ShowAllRecords"
                        SettingsPager-PageSize="15" CssClass="scrolling-table status-table" Theme="Office2010Black"
                        AutoGenerateColumns="false" Width="96%" Style="margin: auto">
                        <SettingsText EmptyDataRow="No records found!" />
                        <Settings ShowVerticalScrollBar="true" ShowFilterRow="true" ShowFilterRowMenu="true" />
                        <Styles>
                            <AlternatingRow Enabled="True">
                            </AlternatingRow>
                        </Styles>
                        <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                        <Columns>
                            <dx:GridViewDataColumn Width="25px" FieldName="No" Caption="No" Settings-AllowAutoFilter="False" />
                            <dx:GridViewDataDateColumn Width="135px" FieldName="CreatedDate" Caption="Create Date"
                                PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy HH:mm}" />
                            <dx:GridViewDataColumn Width="180px" FieldName="FullName" Caption="Created By" />
                            <dx:GridViewBandColumn Caption="Status">
                                <Columns>
                                    <dx:GridViewDataColumn Width="80px" FieldName="FromStatus" Caption="From" />
                                    <dx:GridViewDataColumn Width="80px" FieldName="ToStatus" Caption="To" />
                                </Columns>
                            </dx:GridViewBandColumn>
                            <dx:GridViewDataColumn FieldName="Reason" Caption="Reason" />
                        </Columns>
                    </dx:ASPxGridView>
                    <div style="margin-top: 10px; text-align: center">
                        <input type="button" value="Close" onclick="HideHistory()" class="btn secondary" /></div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--View Error--%>
    <div id="oraErrorContainer" class="no-transition" style="display: none; position: absolute;
        z-index: 1001; background-color: #fff; padding: 10px 20px; border: 1px solid red;
        border-radius: 5px; color: Red; box-shadow: 0 1px 10px #aaa; -webkit-box-shadow: 0 1px 10px #999;
        -moz-box-shadow: 0 1px 10px #aaa; border-radius: 5px; -webkit-border-radius: 5px;
        -moz-border-radius: 5px; line-height: 1.5; right: 5px;">
        <div id="oraErrorDetails" style="max-width: 300px;">
        </div>
        <i style="position: absolute; width: 10px; height: 10px; bottom: -10px; right: 35px;
            background: url(/images/triangle.png) center center no-repeat transparent;">
        </i>
    </div>
    <%--Import Form--%>
    <div id="panImportAirTicket" style="display: none; transition: none; position: fixed;
        width: 100%; height: 100%; left: 0px; top: 0px; z-index: 9000; background-color: rgba(0, 0, 0, 0.7);">
        <div id="panImportAirTicketContent" style="width: 525px; transition: none; margin: 200px
            auto; position: relative; padding: 10px 20px; box-shadow: rgb(255, 255, 255) 0px
            0px 10px; border-radius: 5px; opacity: 1; background-color: rgb(255, 255, 255);">
            <div style="margin-bottom: 10px;">
                <fieldset style="padding: 10px 20px 20px;">
                    <legend style="color: #325EA2">Import air tickets</legend>
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hImportAirTicketType" />
                            <table class="grid-edit" style="margin: 0">
                                <tr>
                                    <td style="width: 180px; font-weight: bold;">
                                        Air Period:<br />
                                        <asp:DropDownList ID="ddlImportAirPeriod" runat="server" Style="margin-top: 5px;">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 180px; font-weight: bold; padding-top: 5px; padding-left: 16px">
                                        Supplier:<br />
                                        <asp:DropDownList ID="ddlImportOraSupplier" runat="server" Style="margin-top: 5px;">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; padding-top: 5px;" colspan="2">
                                        Import File: <a id="lnkDownloadImportTemplate" style="font-weight: normal; text-decoration: underline;
                                            display: none;" href="/Export/Template/import_airticket_template.xlt">(Download
                                            import template)</a> <a id="lnkDownloadOtherImportTemplate" style="font-weight: normal;
                                                text-decoration: underline; display: none;" href="/Export/Template/import_other_airticket_template.xlt">
                                                (Download import template)</a>
                                        <br />
                                        <input type="file" id="fImportAirTicket" style="margin-top: 5px; padding: 1px 0px;
                                            height: 26px; width: 478px;" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </div>
            <div style="margin-left: 20px; width: 300px; color: Red;">
                <ul id="ulImportErrorMessage" class="validate-error-container">
                </ul>
            </div>
            <div style="text-align: center">
                <span id="btnImportAirTicketOK" class="btn" style="margin-top: 10px;"><span>Import2</span></span>
                <span id="btnImportAirTicketCancel" class="btn secondary" style="margin-left: 5px;
                    margin-top: 10px;"><span>Cancel</span></span>
            </div>
        </div>
    </div>
    <%--Import Error--%>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <div id="panImportError" style="position: fixed; top: 0; left: 0; width: 100%; height: 100%;
                background-color: rgba(0, 0, 0, 0.6); z-index: 9000; display: none; transition: none;">
                <div id="panImportErrorContent" style="width: 90%; margin: 100px auto; transition: none;
                    background-color: #fff; padding: 10px 0 15px; border-radius: 5px;" class="popup-widget">
                    <h4 class="widget-title">
                        Air Ticket Import Errors List</h4>
                    <div class="close-widget" onclick="HideImportError('')">
                    </div>
                    <dx:ASPxGridView runat="server" ID="grvImportError" SettingsPager-Mode="ShowAllRecords"
                        SettingsPager-PageSize="15" CssClass="scrolling-table status-table" Theme="Office2010Black"
                        AutoGenerateColumns="false" Width="96%" Style="margin: auto">
                        <SettingsText EmptyDataRow="No records found!" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="300" ShowFilterRow="true"
                            ShowFilterRowMenu="true" />
                        <Styles>
                            <AlternatingRow Enabled="True">
                            </AlternatingRow>
                        </Styles>
                        <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                        <Columns>
                            <dx:GridViewDataColumn Width="25px" FieldName="No" Caption="No" Settings-AllowAutoFilter="False" />
                            <dx:GridViewDataColumn Width="130px" FieldName="TicketNo" Caption="Ticket No" />
                            <dx:GridViewDataDateColumn Width="100px" FieldName="TicketDate" Caption="Ticket Date"
                                PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" CellStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="Passenger" Caption="Passenger" />
                            <dx:GridViewDataColumn Width="55px" FieldName="AirLine" Caption="AirLine" />
                            <dx:GridViewDataColumn Width="" FieldName="Routing" Caption="Routing" />
                            <%--<dx:GridViewDataDateColumn Width="100px" FieldName="DepartureDate" Caption="Departure Date"
                                PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" CellStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataDateColumn Width="100px" FieldName="ReturnDate" Caption="Return Date"
                                PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" CellStyle-HorizontalAlign="Center" />--%>
                            <dx:GridViewDataColumn FieldName="Remark" Caption="Remark" />
                        </Columns>
                    </dx:ASPxGridView>
                    <div style="margin-top: 10px; text-align: center">
                        <input type="button" value="Close" onclick="HideImportError('')" class="btn secondary" /></div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Import Other Error--%>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <div id="panImportOtherError" style="position: fixed; top: 0; left: 0; width: 100%;
                height: 100%; background-color: rgba(0, 0, 0, 0.6); z-index: 9000; display: none;
                transition: none;">
                <div id="panImportOtherErrorContent" style="width: 90%; margin: 100px auto; transition: none;
                    background-color: #fff; padding: 10px 0 15px; border-radius: 5px;" class="popup-widget">
                    <h4 class="widget-title">
                        Air Ticket Import Errors List</h4>
                    <div class="close-widget" onclick="HideImportError('o')">
                    </div>
                    <dx:ASPxGridView runat="server" ID="grvImportOtherError" SettingsPager-Mode="ShowAllRecords"
                        SettingsPager-PageSize="15" CssClass="scrolling-table status-table" Theme="Office2010Black"
                        AutoGenerateColumns="false" Width="96%" Style="margin: auto">
                        <SettingsText EmptyDataRow="No records found!" />
                        <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="300" ShowFilterRow="true"
                            ShowFilterRowMenu="true" />
                        <Styles>
                            <AlternatingRow Enabled="True">
                            </AlternatingRow>
                        </Styles>
                        <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                        <Columns>
                            <dx:GridViewDataColumn Width="25px" FieldName="No" Caption="No" Settings-AllowAutoFilter="False" />
                            <dx:GridViewDataColumn Width="130px" FieldName="TicketNo" Caption="Ticket No" />
                            <dx:GridViewDataDateColumn Width="100px" FieldName="TicketDate" Caption="Ticket Date"
                                PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" CellStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataColumn FieldName="Passenger" Caption="Passenger" />
                            <dx:GridViewDataColumn FieldName="Requester" Caption="Requester" />
                            <dx:GridViewDataColumn Width="55px" FieldName="AirLine" Caption="AirLine" />
                            <dx:GridViewDataColumn Width="" FieldName="Routing" Caption="Routing" />
                            <%--<dx:GridViewDataDateColumn Width="100px" FieldName="DepartureDate" Caption="Departure Date"
                                PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" CellStyle-HorizontalAlign="Center" />
                            <dx:GridViewDataDateColumn Width="100px" FieldName="ReturnDate" Caption="Return Date"
                                PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" CellStyle-HorizontalAlign="Center" />--%>
                            <dx:GridViewDataColumn FieldName="Remark" Caption="Remark" />
                        </Columns>
                    </dx:ASPxGridView>
                    <div style="margin-top: 10px; text-align: center">
                        <input type="button" value="Close" onclick="HideImportError('o')" class="btn secondary" /></div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="ScriptContent" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">

    <script src="/js/jquery/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">
        var _authorizedAccounts = <%= GetAuthorizedAccounts() %>        
    </script>

    <script src="/js/bt-air-ticket.js" type="text/javascript"></script>

</asp:Content>
