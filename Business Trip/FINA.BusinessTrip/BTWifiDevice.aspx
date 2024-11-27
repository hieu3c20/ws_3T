<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BTWifiDevice.aspx.vb"
    Inherits="BTWifiDevice" MasterPageFile="~/MasterPage.Master" %>

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
                        <asp:HiddenField ID="hID" runat="server" />
                        <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                            <tbody>
                                <tr>
                                    <td class="ui-panelgrid-cell" style="width: 130px">
                                        <label>
                                            Business Trip No</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtBusinessTripNo" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hSBTNo" />
                                    </td>
                                    <td class="ui-panelgrid-cell">
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
                <div class="ui-datatable ui-widget">
                    <div class="ui-datatable-tablewrapper">
                        <div class="bt-legend" style="float: right; padding: 5px 0 10px;">
                            <strong>BTS Legend of BT Approval:</strong> <span class="ora-pending" style="padding: 0 5px;
                                border-right: 1px solid #ccc;">Pending</span> <span class="ora-done" style="padding: 0 5px;
                                    border-right: 1px solid #ccc;">Budget Checked</span> <span class="waiting" style="padding: 0 5px;
                                        border-right: 1px solid #ccc;">Confirm</span> <span class="ora-rejected" style="padding: 0 5px;
                                            border-right: 1px solid #ccc;">Rejected</span> <span class="ora-completed" style="padding: 0 5px;
                                                border-right: 1px solid #ccc;">Completed</span> <span class="ora-cancelled" style="padding: 0 5px;">
                                                    Cancelled</span>
                        </div>
                        <div style="clear: both">
                        </div>
                        <div class="HRTabControl">
                            <div class="HRTabNav">
                                <ul>
                                    <li>Pending</li>
                                    <li>Rejected</li>
                                    <li>Confirmed</li>
                                    <li>Returned</li>
                                </ul>
                                <div style="clear: both;">
                                </div>
                            </div>
                            <div class="HRTabList">
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
                                                        <dx:GridViewDataColumn FieldName="BTNo" Caption="BT No" Width="160px" />
                                                        <dx:GridViewDataColumn FieldName="EmployeeCode" Caption="Emp Code" Width="90px" />
                                                        <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Emp Name" Width="130px" />
                                                        <dx:GridViewDataColumn FieldName="Country" Caption="Country" />
                                                        <dx:GridViewDataDateColumn FieldName="FromDate" Caption="From Date" Width="90px"
                                                            PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                        <dx:GridViewDataDateColumn FieldName="ToDate" Caption="To Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                        <dx:GridViewDataImageColumn Width="40px">
                                                            <DataItemTemplate>
                                                                <input type="button" class="grid-btn confirm-btn" title="Confirm" runat="server"
                                                                    id="btnSubConfirmConfirm" onclick="showConfirmMessage(this)" />
                                                                <%------%>
                                                                <input type="button" class="grid-btn rejectC-btn" title="Reject" runat="server" id="btnSubConfirmReject"
                                                                    onclick="showRejectMessage(this)" />
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
                                                        <dx:GridViewDataColumn FieldName="BTNo" Caption="BT No" Width="160px" />
                                                        <dx:GridViewDataColumn FieldName="EmployeeCode" Caption="Emp Code" Width="90px" />
                                                        <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Emp Name" Width="130px" />
                                                        <dx:GridViewDataColumn FieldName="Country" Caption="Country" />
                                                        <dx:GridViewDataDateColumn FieldName="FromDate" Caption="From Date" Width="90px"
                                                            PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                        <dx:GridViewDataDateColumn FieldName="ToDate" Caption="To Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                        <dx:GridViewDataImageColumn Width="40px">
                                                            <DataItemTemplate>
                                                                <input type="button" class="grid-btn confirm-btn" title="Confirm" runat="server"
                                                                    id="btnSubConfirmConfirm" onclick="showConfirmMessage(this)" />
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
                                                        <dx:GridViewDataColumn FieldName="BTNo" Caption="BT No" Width="160px" />
                                                        <dx:GridViewDataColumn FieldName="EmployeeCode" Caption="Emp Code" Width="90px" />
                                                        <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Emp Name" Width="130px" />
                                                        <dx:GridViewDataColumn FieldName="Country" Caption="Country" />
                                                        <dx:GridViewDataDateColumn FieldName="FromDate" Caption="From Date" Width="90px"
                                                            PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                        <dx:GridViewDataDateColumn FieldName="ToDate" Caption="To Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                        <dx:GridViewDataImageColumn Width="40px">
                                                            <DataItemTemplate>
                                                                <input type="button" class="grid-btn rejectC-btn" title="Reject" runat="server" id="btnSubConfirmReject"
                                                                    onclick="showRejectMessage(this)" />
                                                                <%------%>
                                                                <input type="button" class="grid-btn ok-btn" title="Set as Returned" runat="server"
                                                                    id="btnSubConfirmReturn" onclick="showReturnMessage(this)" />
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
                                            <asp:Button ID="btnReturn" ToolTip="Set as Returned" runat="server" CssClass="hide"
                                                OnClientClick="HandleMessage($('[id$=btnSearch]')[0])"></asp:Button>
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
                                                        <dx:GridViewDataColumn FieldName="BTNo" Caption="BT No" Width="160px" />
                                                        <dx:GridViewDataColumn FieldName="EmployeeCode" Caption="Emp Code" Width="90px" />
                                                        <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Emp Name" Width="130px" />
                                                        <dx:GridViewDataColumn FieldName="Country" Caption="Country" />
                                                        <dx:GridViewDataDateColumn FieldName="FromDate" Caption="From Date" Width="90px"
                                                            PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                        <dx:GridViewDataDateColumn FieldName="ToDate" Caption="To Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
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
                            </div>
                        </div>
                    </div>
                </div>
                <%--Actions--%>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <%--<div id="divTransfer" class="action-pan no-transition" runat="server">
                            <span runat="server" id="btnShowSubmit" visible="false" class="btn" style="padding-left: 6px;
                                display: inline-block; padding-right: 20px;"><i class="approval-btn"></i><span style="padding-left: 10px;">
                                    Submit to FI Budget</span>
                                <input type="button" value="Confirm Budget" class="btn" onclick="showSubmitMessage()" /></span>
                            <span runat="server" id="btnShowReject" visible="false" class="btn secondary" style="padding-left: 6px;
                                display: inline-block;"><i class="reject-btn"></i><span>Reject</span>
                                <input type="button" value="Reject" class="btn" onclick="showRejectMessage()" /></span>
                        </div>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
    <%--Confirm--%>
    <asp:UpdatePanel ID="UpdatePanel90" runat="server">
        <ContentTemplate>
            <div id="panConfirmInfo" runat="server" class="popup-container">
                <table style="width: 100%; height: 100%; margin: 0;">
                    <tr>
                        <td style="vertical-align: middle; border: none !important; padding: 0 !important;">
                            <table class="grid-edit" style="width: auto !important; margin: auto; background-color: #fff;
                                border-radius: 5px; box-shadow: 0 0 10px #fff; -webkit-box-shadow: 0 0 10px #fff;
                                -moz-box-shadow: 0 0 10px #fff;">
                                <tr>
                                    <td style="padding: 5px 30px 15px;">
                                        <h3 style="margin: 0; padding: 5px 0 3px; background-color: #fff; border-bottom: 1px solid #999;">
                                            Confirm Wifi Device Request</h3>
                                        <div style="margin-top: 10px; font-weight: bold;">
                                            Comment:
                                        </div>
                                        <asp:TextBox ID="txtConfirmComment" class="reject-reason" Style="width: auto !important;
                                            margin-top: 5px;" TextMode="MultiLine" Rows="5" Columns="60" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 0 0 15px; text-align: center;">
                                        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" Style="text-align: center;"
                                            CssClass="btn" OnClientClick="HandleMessage($('[id$=btnSearch]')[0]);" />
                                        <input type="button" value="Cancel" onclick="hideConfirmMessage(); $('[id$=txtConfirmComment]').val('')"
                                            style="margin-left: 5px;" id="btnConfirmCancel" class="btn secondary" />
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
            <div id="panRejectInfo" runat="server" class="popup-container">
                <table style="width: 100%; height: 100%; margin: 0;">
                    <tr>
                        <td style="vertical-align: middle; border: none !important; padding: 0 !important;">
                            <table class="grid-edit" style="width: auto !important; margin: auto; background-color: #fff;
                                border-radius: 5px; box-shadow: 0 0 10px #fff; -webkit-box-shadow: 0 0 10px #fff;
                                -moz-box-shadow: 0 0 10px #fff;">
                                <tr>
                                    <td style="padding: 5px 30px 15px;">
                                        <h3 style="margin: 0; padding: 5px 0 3px; background-color: #fff; border-bottom: 1px solid #999;">
                                            Reject Wifi Device Request</h3>
                                        <div style="margin-top: 10px; font-weight: bold;">
                                            Comment:
                                        </div>
                                        <asp:TextBox ID="txtRejectReason" class="reject-reason" Style="width: auto !important;
                                            margin-top: 5px;" TextMode="MultiLine" Rows="5" Columns="60" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 0 0 15px; text-align: center;">
                                        <asp:Button ID="btnReject" runat="server" Text="Reject" Style="text-align: center;"
                                            CssClass="btn" OnClientClick="if(!checkReject()) return false; HandleMessage($('[id$=btnSearch]')[0]);" />
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
</asp:Content>
<asp:Content ID="ScriptContent" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">

    <script src="/js/jquery/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="/js/bt-wifi-device.js" type="text/javascript"></script>

</asp:Content>
