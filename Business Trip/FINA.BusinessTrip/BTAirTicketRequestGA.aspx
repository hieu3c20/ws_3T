<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BTAirTicketRequestGA.aspx.vb"
    Inherits="BTAirTicketRequestGA" MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="HeadContent" runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link href="/js/jquery/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    Air Ticket Request Management
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
                <%--<span class="required" style="text-align: center">Please select Employee Code to get
                    information from system automatically. Some of them can not be modified.</span>--%>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hID" runat="server" />
                        <asp:HiddenField ID="hItemID" runat="server" />
                        <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                            <tbody>
                                <tr>
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
                                        <td class="ui-panelgrid-cell date-time-picker">
                                            <dx:ASPxDateEdit ID="dteSDepartureFrom" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                                EditFormatString="dd-MMM-yyyy">
                                            </dx:ASPxDateEdit>
                                            <asp:HiddenField runat="server" ID="hSDepartureFrom" />
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Departure Date To</label>
                                        </td>
                                        <td class="ui-panelgrid-cell date-time-picker">
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
                <span class="ui-icon"></span>Air Ticket Request Management</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content default-hide"
                role="tabpanel">
                <%--List--%>
                <div class="ui-datatable ui-widget">
                    <div class="HRTabControl">
                        <div class="HRTabNav">
                            <ul>
                                <li>Pending</li>
                                <li>Rejected</li>
                                <li>Approved</li>
                            </ul>
                            <div style="clear: both;">
                            </div>
                        </div>
                        <div class="HRTabList">
                            <div class="HRTab">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div style="margin-top: 10px; border: 1px solid transparent">
                                            <dx:ASPxGridView ID="grvSubmitted" runat="server" KeyFieldName="ID" Theme="Office2010Black"
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
                                                    <dx:GridViewDataColumn FieldName="EmployeeCode" Caption="Emp Code" Width="90px" />
                                                    <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Emp Name" />
                                                    <dx:GridViewDataColumn FieldName="From" Caption="From" />
                                                    <dx:GridViewDataColumn FieldName="To" Caption="To" />
                                                    <dx:GridViewDataDateColumn FieldName="DepartureDate" Caption="Departure" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy HH:mm}" />
                                                    <dx:GridViewDataDateColumn FieldName="ReturnDate" Caption="Return" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy HH:mm}" />
                                                    <dx:GridViewDataImageColumn Width="20px" CellStyle-HorizontalAlign="Left">
                                                        <DataItemTemplate>
                                                            <asp:Button ID="btnEdit" runat="server" ToolTip="View/Edit" CssClass='grid-btn viewDetails-btn'
                                                                data-id='<%# Eval("ID") %>' OnClientClick="btnEditOtherRequestClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])"
                                                                OnClick="btnEditOtherAirTicket_OnClick"></asp:Button>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </dx:GridViewDataImageColumn>
                                                </Columns>
                                                <Templates>
                                                    <DetailRow>
                                                        <fieldset style="width: 98%; padding: 0; border: none; margin: 10px 0;">
                                                            <legend style="margin-bottom: 5px; text-decoration: underline;">Relatives infomation</legend>
                                                            <table class="tblRequestDetails grid-inside" style="table-layout: fixed">
                                                                <tr>
                                                                    <th>
                                                                        Name
                                                                    </th>
                                                                    <th>
                                                                        Relationship
                                                                    </th>
                                                                    <th>
                                                                        From
                                                                    </th>
                                                                    <th>
                                                                        To
                                                                    </th>
                                                                    <th>
                                                                        Departure
                                                                    </th>
                                                                    <th>
                                                                        Return
                                                                    </th>
                                                                </tr>
                                                                <%#BuildGridDetails(Eval("ID"))%>
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
                            <div class="HRTab">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div style="margin-top: 10px; border: 1px solid transparent">
                                            <dx:ASPxGridView ID="grvRejected" runat="server" KeyFieldName="ID" Theme="Office2010Black"
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
                                                    <dx:GridViewDataColumn FieldName="EmployeeCode" Caption="Emp Code" Width="90px" />
                                                    <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Emp Name" />
                                                    <dx:GridViewDataColumn FieldName="From" Caption="From" />
                                                    <dx:GridViewDataColumn FieldName="To" Caption="To" />
                                                    <dx:GridViewDataDateColumn FieldName="DepartureDate" Caption="Departure" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy HH:mm}" />
                                                    <dx:GridViewDataDateColumn FieldName="ReturnDate" Caption="Return" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy HH:mm}" />
                                                    <dx:GridViewDataImageColumn Width="20px" CellStyle-HorizontalAlign="Left">
                                                        <DataItemTemplate>
                                                            <asp:Button ID="btnEdit" runat="server" ToolTip="View/Edit" CssClass='grid-btn viewDetails-btn'
                                                                data-id='<%# Eval("ID") %>' OnClientClick="btnEditOtherRequestClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])"
                                                                OnClick="btnEditOtherAirTicket_OnClick"></asp:Button>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </dx:GridViewDataImageColumn>
                                                </Columns>
                                                <Templates>
                                                    <DetailRow>
                                                        <fieldset style="width: 98%; padding: 0; border: none; margin: 10px 0;">
                                                            <legend style="margin-bottom: 5px; text-decoration: underline;">Relatives infomation</legend>
                                                            <table class="tblRequestDetails grid-inside" style="table-layout: fixed">
                                                                <tr>
                                                                    <th>
                                                                        Name
                                                                    </th>
                                                                    <th>
                                                                        Relationship
                                                                    </th>
                                                                    <th>
                                                                        From
                                                                    </th>
                                                                    <th>
                                                                        To
                                                                    </th>
                                                                    <th>
                                                                        Departure
                                                                    </th>
                                                                    <th>
                                                                        Return
                                                                    </th>
                                                                </tr>
                                                                <%#BuildGridDetails(Eval("ID"))%>
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
                            <div class="HRTab">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <div style="margin-top: 10px; border: 1px solid transparent">
                                            <dx:ASPxGridView ID="grvApproved" runat="server" KeyFieldName="ID" Theme="Office2010Black"
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
                                                    <dx:GridViewDataColumn FieldName="EmployeeCode" Caption="Emp Code" Width="90px" />
                                                    <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Emp Name" />
                                                    <dx:GridViewDataColumn FieldName="From" Caption="From" />
                                                    <dx:GridViewDataColumn FieldName="To" Caption="To" />
                                                    <dx:GridViewDataDateColumn FieldName="DepartureDate" Caption="Departure" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy HH:mm}" />
                                                    <dx:GridViewDataDateColumn FieldName="ReturnDate" Caption="Return" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy HH:mm}" />
                                                    <dx:GridViewDataImageColumn Width="20px" CellStyle-HorizontalAlign="Left">
                                                        <DataItemTemplate>
                                                            <asp:Button ID="btnEdit" runat="server" ToolTip="View/Edit" CssClass='grid-btn viewDetails-btn'
                                                                data-id='<%# Eval("ID") %>' OnClientClick="btnEditOtherRequestClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])"
                                                                OnClick="btnEditOtherAirTicket_OnClick"></asp:Button>
                                                        </DataItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                    </dx:GridViewDataImageColumn>
                                                </Columns>
                                                <Templates>
                                                    <DetailRow>
                                                        <fieldset style="width: 98%; padding: 0; border: none; margin: 10px 0;">
                                                            <legend style="margin-bottom: 5px; text-decoration: underline;">Relatives infomation</legend>
                                                            <table class="tblRequestDetails grid-inside" style="table-layout: fixed">
                                                                <tr>
                                                                    <th>
                                                                        Name
                                                                    </th>
                                                                    <th>
                                                                        Relationship
                                                                    </th>
                                                                    <th>
                                                                        From
                                                                    </th>
                                                                    <th>
                                                                        To
                                                                    </th>
                                                                    <th>
                                                                        Departure
                                                                    </th>
                                                                    <th>
                                                                        Return
                                                                    </th>
                                                                </tr>
                                                                <%#BuildGridDetails(Eval("ID"))%>
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
                        </div>
                    </div>
                </div>
                <%--Edit form--%>
                <fieldset class="add-edit-form" id="OtherAirTicketForm">
                    <legend><span class="add-edit-action"></span>&nbsp;Air Ticket Request</legend>
                    <asp:UpdatePanel ID="upAirTicketRequest" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hRequestID" />
                            <div style="text-align: right; padding-top: 5px; font-weight: bold;">
                                <span style="border-bottom: 1px solid #999; padding-left: 50px; padding-bottom: 3px;">
                                    Status: <a href="#" rel="show-more" onclick="return ShowHistory()">
                                        <asp:Label runat="server" ID="lblRequestStatus"></asp:Label></a>
                                    <asp:Label runat="server" ID="lblRequestMessage" Font-Bold="false"></asp:Label></span></div>
                            <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                <tbody>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Employee Code</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRequesterCode" runat="server" CssClass="employee-code"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hRequester" />
                                            <asp:Button runat="server" ID="btnGetRequesterInfo" CssClass="hide" OnClientClick="HandleMessage(this); bindStartupEvents(this)" />
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Employee Name</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRequesterName" ReadOnly="true" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Employee Division</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRequesterDivision" ReadOnly="true" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Employee Department</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRequesterDepartment" ReadOnly="true" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell" style="width: 100px">
                                            <label>
                                                From Country</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="ddlFromCountry" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td runat="server" id="tdRequestDestinationLabel" class="ui-panelgrid-cell">
                                            <label>
                                                From Destination</label>
                                        </td>
                                        <td runat="server" id="tdRequestDestinationControl" class="ui-panelgrid-cell" style="padding-top: 10px">
                                            <asp:DropDownList ID="ddlFromDestination" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell" style="width: 100px">
                                            <label>
                                                To Country</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="ddlToCountry" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td runat="server" id="td1" class="ui-panelgrid-cell">
                                            <label>
                                                To Destination</label>
                                        </td>
                                        <td runat="server" id="td2" class="ui-panelgrid-cell" style="padding-top: 10px">
                                            <asp:DropDownList ID="ddlToDestination" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell" style="width: 120px">
                                            <label>
                                                Departure Date</label>
                                        </td>
                                        <td class="ui-panelgrid-cell date-time-picker">
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
                                                Budget Code</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRequestBudgetCode" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Purpose</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtPurpose" TextMode="MultiLine" ToolTip="Please enter event/project name for this air ticket"
                                                data-tooltip="all" Rows="3" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div style="height: 0px; margin: 10px 0 15px; border-bottom: 3px double #ccc; position: relative">
                                <span style="position: absolute; left: 15px; top: -11px; background-color: #fff;
                                    padding: 5px 5px; font-weight: bold;">Relatives Information</span>
                            </div>
                            <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                <tbody>
                                    <tr>
                                        <td class="ui-panelgrid-cell" colspan="4">
                                            <asp:CheckBox runat="server" ID="chkRequestAirTicket" Text=" " CssClass="check-button"
                                                AutoPostBack="true" />
                                            <span style="position: relative; top: -4px; padding-right: 8px">Use the same destination
                                                and flight time as employee</span>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trRelativeFrom">
                                        <td class="ui-panelgrid-cell" style="width: 120px">
                                            <label>
                                                From Country</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="ddlRelativeFromCountry" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td runat="server" id="td3" class="ui-panelgrid-cell">
                                            <label>
                                                From Destination</label>
                                        </td>
                                        <td runat="server" id="td4" class="ui-panelgrid-cell" style="padding-top: 10px">
                                            <asp:DropDownList ID="ddlRelativeFromDestination" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trRelativeTo">
                                        <td class="ui-panelgrid-cell" style="width: 120px">
                                            <label>
                                                To Country</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="ddlRelativeToCountry" runat="server" AutoPostBack="true">
                                                <%--onchange="HandleMessage($('[id$=btnCancelOtherAirTicket]')[0]); bindStartupEvents($('[id$=btnCancelOtherAirTicket]')[0])"--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td runat="server" id="td5" class="ui-panelgrid-cell">
                                            <label>
                                                To Destination</label>
                                        </td>
                                        <td runat="server" id="td6" class="ui-panelgrid-cell" style="padding-top: 10px">
                                            <asp:DropDownList ID="ddlRelativeToDestination" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trRelativeTime">
                                        <td class="ui-panelgrid-cell" style="width: 120px">
                                            <label>
                                                Departure Date</label>
                                        </td>
                                        <td class="ui-panelgrid-cell date-time-picker">
                                            <dx:ASPxDateEdit ID="dteRelativeDepartureDate" runat="server" ClientInstanceName="departureDate"
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
                                            <dx:ASPxDateEdit ID="dteRelativeReturnDate" runat="server" ClientInstanceName="returnDate"
                                                EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm">
                                                <TimeSectionProperties Visible="true">
                                                    <TimeEditProperties EditFormatString="HH:mm" />
                                                </TimeSectionProperties>
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell" style="width: 120px">
                                            <label>
                                                Name</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRelativeName1" runat="server"></asp:TextBox>
                                        </td>
                                        <td runat="server" id="td7" class="ui-panelgrid-cell" style="width: 120px">
                                            <label>
                                                Relationship</label>
                                        </td>
                                        <td runat="server" id="td8" class="ui-panelgrid-cell" style="padding-top: 10px">
                                            <asp:DropDownList ID="ddlRelationship1" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Name</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRelativeName2" runat="server"></asp:TextBox>
                                        </td>
                                        <td runat="server" id="td9" class="ui-panelgrid-cell">
                                            <label>
                                                Relationship</label>
                                        </td>
                                        <td runat="server" id="td10" class="ui-panelgrid-cell" style="padding-top: 10px">
                                            <asp:DropDownList ID="ddlRelationship2" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Name</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRelativeName3" runat="server"></asp:TextBox>
                                        </td>
                                        <td runat="server" id="td11" class="ui-panelgrid-cell">
                                            <label>
                                                Relationship</label>
                                        </td>
                                        <td runat="server" id="td12" class="ui-panelgrid-cell" style="padding-top: 10px">
                                            <asp:DropDownList ID="ddlRelationship3" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table runat="server" id="tblAddMore" class="ui-panelgrid ui-widget grid-edit hide"
                                role="grid" style="margin-top: 0">
                                <tbody>
                                    <tr>
                                        <td class="ui-panelgrid-cell" style="width: 120px">
                                            <label>
                                                Name</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRelativeName4" runat="server"></asp:TextBox>
                                        </td>
                                        <td runat="server" id="td13" class="ui-panelgrid-cell" style="width: 120px">
                                            <label>
                                                Relationship</label>
                                        </td>
                                        <td runat="server" id="td14" class="ui-panelgrid-cell" style="padding-top: 10px">
                                            <asp:DropDownList ID="ddlRelationship4" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Name</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRelativeName5" runat="server"></asp:TextBox>
                                        </td>
                                        <td runat="server" id="td15" class="ui-panelgrid-cell">
                                            <label>
                                                Relationship</label>
                                        </td>
                                        <td runat="server" id="td16" class="ui-panelgrid-cell" style="padding-top: 10px">
                                            <asp:DropDownList ID="ddlRelationship5" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Name</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRelativeName6" runat="server"></asp:TextBox>
                                        </td>
                                        <td runat="server" id="td17" class="ui-panelgrid-cell">
                                            <label>
                                                Relationship</label>
                                        </td>
                                        <td runat="server" id="td18" class="ui-panelgrid-cell" style="padding-top: 10px">
                                            <asp:DropDownList ID="ddlRelationship6" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Name</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRelativeName7" runat="server"></asp:TextBox>
                                        </td>
                                        <td runat="server" id="td19" class="ui-panelgrid-cell">
                                            <label>
                                                Relationship</label>
                                        </td>
                                        <td runat="server" id="td20" class="ui-panelgrid-cell" style="padding-top: 10px">
                                            <asp:DropDownList ID="ddlRelationship7" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Name</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRelativeName8" runat="server"></asp:TextBox>
                                        </td>
                                        <td runat="server" id="td21" class="ui-panelgrid-cell">
                                            <label>
                                                Relationship</label>
                                        </td>
                                        <td runat="server" id="td22" class="ui-panelgrid-cell" style="padding-top: 10px">
                                            <asp:DropDownList ID="ddlRelationship8" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Name</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRelativeName9" runat="server"></asp:TextBox>
                                        </td>
                                        <td runat="server" id="td23" class="ui-panelgrid-cell">
                                            <label>
                                                Relationship</label>
                                        </td>
                                        <td runat="server" id="td24" class="ui-panelgrid-cell" style="padding-top: 10px">
                                            <asp:DropDownList ID="ddlRelationship9" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Name</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtRelativeName10" runat="server"></asp:TextBox>
                                        </td>
                                        <td runat="server" id="td25" class="ui-panelgrid-cell">
                                            <label>
                                                Relationship</label>
                                        </td>
                                        <td runat="server" id="td26" class="ui-panelgrid-cell" style="padding-top: 10px">
                                            <asp:DropDownList ID="ddlRelationship10" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div runat="server" id="divBtnAddMore" style="padding: 4px 10px">
                                <asp:HiddenField runat="server" ID="hAddMore" />
                                <a href="#" style="text-decoration: underline" onclick="$('[id$=tblAddMore]').slideDown(); $('[id$=hAddMore]').val('T'); $(this).parent().addClass('hide'); return false;">
                                    Click here to add more &raquo;</a></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <ul id="OtherAirTicketSummary" class="error-summary">
                    </ul>
                    <div class="action-pan">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <span runat="server" id="btnShowApprove" visible="false">
                                    <input type="button" class="btn" value="Approve" onclick="ConfirmApprove()" />
                                </span>
                                <asp:Button runat="server" ID="btnApprove" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents($('[id$=btnSearch]')[0])" />
                                <span runat="server" id="btnShowReject" visible="false">
                                    <input type="button" value="Reject" class="btn secondary" onclick="showRejectMessage()" /></span>
                                <asp:Button runat="server" ID="btnCancelOtherAirTicket" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
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
                                            Reject to HR to update information</h3>
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
</asp:Content>
<asp:Content ID="ScriptContent" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">

    <script src="/js/jquery/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">
        var _authorizedAccounts = <%= GetAuthorizedAccounts() %>        
    </script>

    <script src="/js/bt-air-ticket-request.js" type="text/javascript"></script>

</asp:Content>
