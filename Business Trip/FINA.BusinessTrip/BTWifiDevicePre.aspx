<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BTWifiDevicePre.aspx.vb"
    Inherits="BTWifiDevicePre" MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="HeadContent" runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link href="/js/jquery/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    Wifi Device Management
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
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                            <tbody>
                                <tr>
                                    <td class="ui-panelgrid-cell" style="width: 130px">
                                        <label>
                                            Country</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:DropDownList runat="server" ID="ddlSCountry">
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="hSCountry" />
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
                                            Request Date From</label>
                                    </td>
                                    <td class="ui-panelgrid-cell date-time-picker validate-required">
                                        <dx:ASPxDateEdit ID="dteSFromDate" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy">
                                        </dx:ASPxDateEdit>
                                        <asp:HiddenField runat="server" ID="hSFromDate" />
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Request Date To</label>
                                    </td>
                                    <td class="ui-panelgrid-cell date-time-picker validate-required">
                                        <dx:ASPxDateEdit ID="dteSToDate" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy">
                                        </dx:ASPxDateEdit>
                                        <asp:HiddenField runat="server" ID="hSToDate" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                        <span class="btn inform" id="btnSearchEmpInfo" runat="server" style="margin-left: 3px;
                                            text-align: center; margin-top: 20px; display: inline-block;">
                                            <asp:Button ID="btnSearch" CssClass="btnSearch" runat="server" Text="" OnClientClick="HandleMessage(this)" />
                                            <i class="search"></i>Search</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <%--Wifi Device--%>
        <div class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top active"
                role="tab">
                <span class="ui-icon"></span>Wifi Device Management</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content default-hide"
                role="tabpanel">
                <div class="HRTabControl">
                    <div class="HRTabNav">
                        <ul>
                            <li>Prepared</li>
                            <li>Sent</li>
                            <li>Rejected</li>
                            <%--<li>Confirmed</li>
                            <li>Returned</li>--%>
                        </ul>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <div class="HRTabList">
                        <div class="HRTab">
                            <span id="btnAddWifiDevice" style="text-align: center; float: left; margin-top: 10px;
                                margin-bottom: 4px" class="btn inform add-btn" onclick="btnAddSub_Click(this); $('[id$=btnAddNewWifiDevice]').click()">
                                <i class="add"></i>Add</span>
                            <div style="clear: both">
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button runat="server" ID="btnAddNewWifiDevice" Text="Add" CssClass="btn hide"
                                        OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])" />
                                    <div style="margin-top: 10px; border: 1px solid transparent">
                                        <dx:ASPxGridView ID="grvPrepared" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                            AutoGenerateColumns="false" Style="margin-top: 0" SettingsBehavior-AllowSort="false">
                                            <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="true" />
                                            <SettingsPager PageSize="100" NumericButtonCount="10">
                                                <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All" Caption="Page Size"
                                                    Items="20, 30, 50, 100" />
                                            </SettingsPager>
                                            <SettingsDetail ShowDetailRow="true" />
                                            <SettingsText EmptyDataRow="No records found!" />
                                            <Styles>
                                                <AlternatingRow Enabled="True">
                                                </AlternatingRow>
                                            </Styles>
                                            <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                            <Columns>
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                                    FieldName="ID" Caption="ID" />
                                                <dx:GridViewDataColumn FieldName="EmployeeCode" Caption="Emp Code" Width="90px" />
                                                <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Emp Name" Width="130px" />
                                                <dx:GridViewDataColumn FieldName="Country" Caption="Country" />
                                                <dx:GridViewDataDateColumn FieldName="FromDate" Caption="From Date" Width="90px"
                                                    PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                <dx:GridViewDataDateColumn FieldName="ToDate" Caption="To Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" CssClass="grid-btn edit-btn" data-id='<%# Eval("ID") %>'
                                                            OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])"
                                                            OnClick="btnEditWifiDevice_OnClick"></asp:Button>
                                                        <input type="button" class="grid-btn delete-btn" title="Delete" runat="server" id="btnSubConfirmDelete"
                                                            onclick="btnDeleteWifiDeviceClick(this)" />
                                                        <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' ToolTip="Delete" runat="server"
                                                            CssClass="hide" OnClick="btnDeleteWifiDevice_OnClick" OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])" />
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                            <Templates>
                                                <DetailRow>
                                                    <fieldset style="width: 98%; padding: 0; border: none; margin: 10px 0;">
                                                        <table class="tblRequestDetails grid-inside" style="table-layout: fixed">
                                                            <tr>
                                                                <th>
                                                                    Comment
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <%#Eval("Comment")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </DetailRow>
                                            </Templates>
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem FieldName="EstimateTransportationFee" DisplayFormat="{0:#,0.##}"
                                                    SummaryType="Sum" />
                                            </TotalSummary>
                                        </dx:ASPxGridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="HRTab">
                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                <ContentTemplate>
                                    <div style="margin-top: 10px; border: 1px solid transparent">
                                        <dx:ASPxGridView ID="grvPending" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                            AutoGenerateColumns="false" Style="margin-top: 0" SettingsBehavior-AllowSort="false">
                                            <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="true" />
                                            <SettingsPager PageSize="100" NumericButtonCount="10">
                                                <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All" Caption="Page Size"
                                                    Items="20, 30, 50, 100" />
                                            </SettingsPager>
                                            <SettingsDetail ShowDetailRow="true" />
                                            <SettingsText EmptyDataRow="No records found!" />
                                            <Styles>
                                                <AlternatingRow Enabled="True">
                                                </AlternatingRow>
                                            </Styles>
                                            <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                            <Columns>
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                                    FieldName="ID" Caption="ID" />
                                                <dx:GridViewDataColumn FieldName="EmployeeCode" Caption="Emp Code" Width="90px" />
                                                <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Emp Name" Width="130px" />
                                                <dx:GridViewDataColumn FieldName="Country" Caption="Country" />
                                                <dx:GridViewDataDateColumn FieldName="FromDate" Caption="From Date" Width="90px"
                                                    PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                <dx:GridViewDataDateColumn FieldName="ToDate" Caption="To Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" CssClass="grid-btn viewDetails-btn" data-id='<%# Eval("ID") %>'
                                                            OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])"
                                                            OnClick="btnEditWifiDevice_OnClick"></asp:Button>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                            <Templates>
                                                <DetailRow>
                                                    <fieldset style="width: 98%; padding: 0; border: none; margin: 10px 0;">
                                                        <table class="tblRequestDetails grid-inside" style="table-layout: fixed">
                                                            <tr>
                                                                <th>
                                                                    Comment
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <%#Eval("Comment")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </DetailRow>
                                            </Templates>
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem FieldName="EstimateTransportationFee" DisplayFormat="{0:#,0.##}"
                                                    SummaryType="Sum" />
                                            </TotalSummary>
                                        </dx:ASPxGridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="HRTab">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div style="margin-top: 10px; border: 1px solid transparent">
                                        <dx:ASPxGridView ID="grvRejected" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                            AutoGenerateColumns="false" Style="margin-top: 0" SettingsBehavior-AllowSort="false">
                                            <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="true" />
                                            <SettingsPager PageSize="100" NumericButtonCount="10">
                                                <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All" Caption="Page Size"
                                                    Items="20, 30, 50, 100" />
                                            </SettingsPager>
                                            <SettingsDetail ShowDetailRow="true" />
                                            <SettingsText EmptyDataRow="No records found!" />
                                            <Styles>
                                                <AlternatingRow Enabled="True">
                                                </AlternatingRow>
                                            </Styles>
                                            <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                            <Columns>
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                                    FieldName="ID" Caption="ID" />
                                                <dx:GridViewDataColumn FieldName="EmployeeCode" Caption="Emp Code" Width="90px" />
                                                <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Emp Name" Width="130px" />
                                                <dx:GridViewDataColumn FieldName="Country" Caption="Country" />
                                                <dx:GridViewDataDateColumn FieldName="FromDate" Caption="From Date" Width="90px"
                                                    PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                <dx:GridViewDataDateColumn FieldName="ToDate" Caption="To Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" CssClass="grid-btn edit-btn" data-id='<%# Eval("ID") %>'
                                                            OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])"
                                                            OnClick="btnEditWifiDevice_OnClick"></asp:Button>
                                                        <input type="button" class="grid-btn delete-btn" title="Delete" runat="server" id="btnSubConfirmDelete"
                                                            onclick="btnDeleteWifiDeviceClick(this)" />
                                                        <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' ToolTip="Delete" runat="server"
                                                            CssClass="hide" OnClick="btnDeleteWifiDevice_OnClick" OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])" />
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                            <Templates>
                                                <DetailRow>
                                                    <fieldset style="width: 98%; padding: 0; border: none; margin: 10px 0;">
                                                        <table class="tblRequestDetails grid-inside" style="table-layout: fixed">
                                                            <tr>
                                                                <th>
                                                                    Comment
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <%#Eval("Comment")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </DetailRow>
                                            </Templates>
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem FieldName="EstimateTransportationFee" DisplayFormat="{0:#,0.##}"
                                                    SummaryType="Sum" />
                                            </TotalSummary>
                                        </dx:ASPxGridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <%--<div class="HRTab">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div style="margin-top: 10px; border: 1px solid transparent">
                                        <dx:ASPxGridView ID="grvConfirmed" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                            AutoGenerateColumns="false" Style="margin-top: 0" SettingsBehavior-AllowSort="false">
                                            <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="true" />
                                            <SettingsPager PageSize="100" NumericButtonCount="10">
                                                <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All" Caption="Page Size"
                                                    Items="20, 30, 50, 100" />
                                            </SettingsPager>
                                            <SettingsDetail ShowDetailRow="true" />
                                            <SettingsText EmptyDataRow="No records found!" />
                                            <Styles>
                                                <AlternatingRow Enabled="True">
                                                </AlternatingRow>
                                            </Styles>
                                            <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                            <Columns>
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                                    FieldName="ID" Caption="ID" />
                                                <dx:GridViewDataColumn FieldName="EmployeeCode" Caption="Emp Code" Width="90px" />
                                                <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Emp Name" Width="130px" />
                                                <dx:GridViewDataColumn FieldName="Country" Caption="Country" />
                                                <dx:GridViewDataDateColumn FieldName="FromDate" Caption="From Date" Width="90px"
                                                    PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                <dx:GridViewDataDateColumn FieldName="ToDate" Caption="To Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" CssClass="grid-btn viewDetails-btn" data-id='<%# Eval("ID") %>'
                                                            OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])"
                                                            OnClick="btnEditWifiDevice_OnClick"></asp:Button>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                            <Templates>
                                                <DetailRow>
                                                    <fieldset style="width: 98%; padding: 0; border: none; margin: 10px 0;">
                                                        <table class="tblRequestDetails grid-inside" style="table-layout: fixed">
                                                            <tr>
                                                                <th>
                                                                    Comment
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <%#Eval("Comment")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </DetailRow>
                                            </Templates>
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem FieldName="EstimateTransportationFee" DisplayFormat="{0:#,0.##}"
                                                    SummaryType="Sum" />
                                            </TotalSummary>
                                        </dx:ASPxGridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="HRTab">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div style="margin-top: 10px; border: 1px solid transparent">
                                        <dx:ASPxGridView ID="grvReturned" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                            AutoGenerateColumns="false" Style="margin-top: 0" SettingsBehavior-AllowSort="false">
                                            <Settings ShowFilterRow="true" ShowFilterRowMenu="true" ShowFooter="true" />
                                            <SettingsPager PageSize="100" NumericButtonCount="10">
                                                <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All" Caption="Page Size"
                                                    Items="20, 30, 50, 100" />
                                            </SettingsPager>
                                            <SettingsDetail ShowDetailRow="true" />
                                            <SettingsText EmptyDataRow="No records found!" />
                                            <Styles>
                                                <AlternatingRow Enabled="True">
                                                </AlternatingRow>
                                            </Styles>
                                            <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                            <Columns>
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                                    FieldName="ID" Caption="ID" />
                                                <dx:GridViewDataColumn FieldName="EmployeeCode" Caption="Emp Code" Width="90px" />
                                                <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Emp Name" Width="130px" />
                                                <dx:GridViewDataColumn FieldName="Country" Caption="Country" />
                                                <dx:GridViewDataDateColumn FieldName="FromDate" Caption="From Date" Width="90px"
                                                    PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                <dx:GridViewDataDateColumn FieldName="ToDate" Caption="To Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" CssClass="grid-btn viewDetails-btn" data-id='<%# Eval("ID") %>'
                                                            OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])"
                                                            OnClick="btnEditWifiDevice_OnClick"></asp:Button>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                            <Templates>
                                                <DetailRow>
                                                    <fieldset style="width: 98%; padding: 0; border: none; margin: 10px 0;">
                                                        <table class="tblRequestDetails grid-inside" style="table-layout: fixed">
                                                            <tr>
                                                                <th>
                                                                    Comment
                                                                </th>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">
                                                                    <%#Eval("Comment")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </fieldset>
                                                </DetailRow>
                                            </Templates>
                                            <TotalSummary>
                                                <dx:ASPxSummaryItem FieldName="EstimateTransportationFee" DisplayFormat="{0:#,0.##}"
                                                    SummaryType="Sum" />
                                            </TotalSummary>
                                        </dx:ASPxGridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>--%>
                    </div>
                </div>
                <%--Edit form--%>
                <fieldset class="add-edit-form" id="WifiDeviceForm">
                    <legend><span class="add-edit-action"></span>&nbsp;Wifi Device Request</legend>
                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hWifiDeviceID" />
                            <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                <tbody>
                                    <tr>
                                        <td class="ui-panelgrid-cell" style="width: 110px">
                                            <label>
                                                Country<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell validate-required">
                                            <asp:DropDownList ID="ddlWifiDeviceCountry" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Employee Code<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell validate-required">
                                            <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="employee-code" data-hidden="hEmployee"
                                                data-button="btnGetEmployeeInfo"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hEmployee" />
                                            <asp:Button runat="server" ID="btnGetEmployeeInfo" CssClass="hide" OnClientClick="HandleMessage(this); bindStartupEvents(this)" />
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Employee Name<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell validate-required">
                                            <asp:TextBox ID="txtEmployeeName" ReadOnly="true" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Employee Division</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtEmployeeDivision" ReadOnly="true" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Employee Department</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtEmployeeDepartment" ReadOnly="true" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                From Date<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell date-time-picker validate-required">
                                            <dx:ASPxDateEdit runat="server" ID="dtWifiDeviceFromDate" ClientInstanceName="dtWifiDeviceFromDate"
                                                EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy">
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                To Date<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell date-time-picker validate-required">
                                            <dx:ASPxDateEdit runat="server" ID="dtWifiDeviceToDate" ClientInstanceName="dtWifiDeviceToDate"
                                                EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy">
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSaveWifiDevice" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <ul id="WifiDeviceSummary" class="error-summary">
                    </ul>
                    <div class="action-pan">
                        <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                            <ContentTemplate>
                                <asp:Button runat="server" ID="btnSaveWifiDevice" Text="Save" CssClass="btn" OnClientClick="if(!ValidateWifiForm(this)){return false}; HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])" />
                                <span runat="server" id="btnShowSubmitWifiDevice">
                                    <input type="button" class="btn" value="Submit" onclick="if(ValidateWifiForm(this)){showSubmitMessage(this)}" />
                                </span>
                                <input type="button" id="btnCancelWifiDevice" value="Cancel" class="btn secondary btn-cancel-sub"
                                    onclick="btnCancelSub_Click(this);" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
    <%--Submit--%>
    <asp:UpdatePanel ID="UpdatePanel90" runat="server">
        <ContentTemplate>
            <div id="panSubmitInfo" runat="server" class="popup-container">
                <table style="width: 100%; height: 100%; margin: 0;">
                    <tr>
                        <td style="vertical-align: middle; border: none !important; padding: 0 !important;">
                            <table class="grid-edit" style="width: auto !important; margin: auto; background-color: #fff;
                                border-radius: 5px; box-shadow: 0 0 10px #fff; -webkit-box-shadow: 0 0 10px #fff;
                                -moz-box-shadow: 0 0 10px #fff;">
                                <tr>
                                    <td style="padding: 5px 30px 15px;">
                                        <h3 style="margin: 0; padding: 5px 0 3px; background-color: #fff; border-bottom: 1px solid #999;">
                                            Submit Wifi Device Request</h3>
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
                                            CssClass="btn" OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0]); HandlePartialMessageBoard($('[id$=btnSearch]')[0], $('[id$=btnShowSubmitWifiDevice]')[0])" />
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
</asp:Content>
<asp:Content ID="ScriptContent" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">

    <script src="/js/jquery/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">
        var _authorizedAccounts = <%= GetAuthorizedAccounts() %>        
    </script>

    <script src="/js/bt-wifi-device-pre.js" type="text/javascript"></script>

</asp:Content>
