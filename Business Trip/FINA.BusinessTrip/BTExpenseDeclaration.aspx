<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BTExpenseDeclaration.aspx.vb"
    Inherits="BTExpenseDeclaration" MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="HeadContent" runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link href="/js/jquery/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function CommonDateChange() {
            $('[id$=ddlCommonCurrency]').change()
        }

        function HotelExdateChange() {
            $('[id$=ddlHotelCurrency]').change()
        }

        function AirTicketDateChange() {
            $('[id$=ddlAirCurrency]').change()
        }

        function OtherDateChange() {
            $('[id$=ddlExpenseOtherCurrency]').change()
        }

        function InvoiceDateChanged() {
            $("#btnInvoiceDateChange").click()
        }

        var currentDepartureDate
        function DepartureDateChanged() {
            if ($("[id$=grvCommonExpense]").find("tr.dxgvDataRow_Office2010Black").size()) {
                ShowErrorMessage("Policy has changed. In order to process this settle, please delete all old allowance records that was created before.", 15000)
                dteExpenseDepartureDate.SetDate(currentDepartureDate)
            }
            else {
                currentDepartureDate = dteExpenseDepartureDate.GetDate()                
                $("[id$=btnDepartureDateChange]").click()
            }
        }
    </script>

</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    Overnight Trip Expense Management
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
    <div class="currency bt-status hide" style="overflow: hidden; transition: none; float: left;
        margin-top: 9px;">
        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
            <ContentTemplate>
                <table class="grid-edit" style="margin: 0;">
                    <tr>
                        <%--<td style="padding: 0px; font-weight: bold;">
                            HR Status: <a id="lnkHRStatus" runat="server" href="#" rel="show-more" class="">
                            </a>
                        </td>
                        <td style="padding: 0px 0px 0px 20px; font-weight: bold;">
                            GA Status: <a id="lnkGAStatus" runat="server" href="#" rel="show-more" class="">
                            </a>
                        </td>--%>
                        <td style="font-weight: bold;">
                            Status: <a id="lnkFINStatus" runat="server" href="#" rel="show-more" onclick="return ShowHistory('FI')">
                            </a>
                            <asp:Label runat="server" ID="lblFINComment" Style="font-weight: normal;"></asp:Label>
                        </td>
                        <td style="font-weight: bold;" runat="server" id="tdOraStatus" visible="false">
                            <span style="padding: 0px 10px">|</span>Oracle Status: <a id="lnkOraStatus" runat="server"
                                href="#" onclick="showErrorOraMessage(this); return false;"></a>
                        </td>
                    </tr>
                </table>
                <div id="BTStatusHistory" style="position: fixed; top: 0; left: 0; width: 100%; height: 100%;
                    background-color: rgba(0, 0, 0, 0.6); z-index: 9000; display: none; transition: none;">
                    <div id="BTStatusContent" style="width: 800px; margin: 100px auto; transition: none;
                        background-color: #fff; padding: 10px 0 15px; border-radius: 5px;" class="popup-widget">
                        <h4 class="widget-title">
                            Status History</h4>
                        <div class="close-widget" onclick="HideHistory()">
                        </div>
                        <dx:ASPxGridView runat="server" ID="grvFIStatusHistory" SettingsPager-Mode="ShowAllRecords"
                            SettingsPager-PageSize="15" CssClass="scrolling-table status-table FI" Theme="Office2010Black"
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
    </div>
    <%--Currency-Payment Type--%>
    <div class="currency hide" style="overflow: hidden; transition: none; float: right;
        margin-top: 8px;">
        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
                <table class="grid-edit" style="margin: 0;">
                    <tr>
                        <td style="padding: 0px 0px 0px 20px;" runat="server" id="tdCredit">
                            <span style="position: relative; top: -3px; font-weight: bold; color: red">Credit Card</span>
                            <asp:CheckBox runat="server" ID="chkCredit" Enabled="false" Text=" " CssClass="check-button"
                                Style="top: 2px;" />
                        </td>
                        <td style="padding: 0px 0px 0px 20px;">
                            <span style="position: relative; top: 1px; font-weight: bold; color: red">Currency</span>
                            <asp:DropDownList ID="ddlCurrency" Enabled="false" runat="server" Style="padding: 3px 5px !important;
                                width: auto !important">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="clear: both">
    </div>
    <%--Main part--%>
    <div class="ui-accordion ui-widget ui-helper-reset ui-hidden-container" role="tablist">
        <%--Total error summary--%>
        <ul id="totalErrorSummary" class="total-error-summary hide">
        </ul>
        <%--Search form / General Information--%>
        <div class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                role="tab">
                <span class="ui-icon"></span><span id="general-title">Search Condition</span></h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content default-hide hide"
                role="tabpanel">
                <%--<span class="required" style="text-align: center">Please select Employee Code to get
                    information from system automatically. Some of them can not be modified.</span>--%>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hID" runat="server" />
                        <asp:HiddenField runat="server" ID="hIsGMAndAbove" />
                        <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                            <tbody>
                                <tr>
                                    <td class="ui-panelgrid-cell" style="width: 160px">
                                        <label>
                                            Business Trip No</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtBusinessTripNo" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hsBTNo" />
                                    </td>
                                    <td class="ui-panelgrid-cell" style="width: 130px">
                                        <label>
                                            Business Trip Type</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:DropDownList ID="ddlBTType" runat="server">
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="hsBTType" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Employee Code</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtEmployeeCode" runat="server" MaxLength="6" CssClass="employee-code"
                                            data-type="int"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hsEmployeeCode" />
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Full Name</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hsFullName" />
                                    </td>
                                </tr>
                                <tr runat="server" id="trGeneral2" class="general-info">
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Location</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtLocation" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Division</label>
                                    </td>
                                    <td role="gridcell" class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtDivision" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="trGeneral3" class="general-info">
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Department</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtDepartment" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Section</label>
                                    </td>
                                    <td role="gridcell" class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtSection" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="trGeneral4" class="general-info">
                                    <td role="gridcell" class="ui-panelgrid-cell">
                                        <label>
                                            Title</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtPosition" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Email</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtEmail" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="trGeneral0">
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Departure From</label>
                                    </td>
                                    <td class="ui-panelgrid-cell date-time-picker validate-required">
                                        <dx:ASPxDateEdit ID="dteDepartureFrom" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy">
                                        </dx:ASPxDateEdit>
                                        <asp:HiddenField runat="server" ID="hsDepartureFrom" />
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Departure To</label>
                                    </td>
                                    <td class="ui-panelgrid-cell date-time-picker validate-required">
                                        <dx:ASPxDateEdit ID="dteDepartureTo" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy">
                                        </dx:ASPxDateEdit>
                                        <asp:HiddenField runat="server" ID="hsDepartureTo" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Budget Name</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:DropDownList ID="ddlBudgetName" runat="server" onchange="BudgetCodeChange(this, 'txtBudgetCode'); HandleMessage(this); bindStartupEvents(this)"
                                            Style="width: 160px !important">
                                        </asp:DropDownList>
                                        <asp:HiddenField runat="server" ID="hsBudgetName" />
                                        <span runat="server" id="spanBudgetAll" style="margin-left: 12px; position: relative;
                                            top: 3px;">
                                            <asp:CheckBox runat="server" ID="chkBudgetAll" class="check-button" Text=" " AutoPostBack="true"
                                                onchange="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]);"
                                                Style="position: relative; top: 4px;" />
                                            <span style="padding-top: 5px">Other</span> </span>
                                        <asp:HiddenField runat="server" ID="hsBudgetAll" />
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Budget Code</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtBudgetCode" runat="server" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="trGeneral6" class="general-info">
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Project Budget Code</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtProjectBudgetCode" ReadOnly="true" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="trGeneral5" class="general-info">
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Estimate Budget Remaining</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtBudgetRemain" runat="server" ReadOnly="true" Style="text-align: right"></asp:TextBox>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Mobile No</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: center;">
                                        <span class="btn inform" id="btnSearchEmpInfo" runat="server" style="margin-left: 3px;
                                            text-align: center; margin-top: 20px; display: inline-block;">
                                            <asp:Button ID="btnSearch" CssClass="btnSearch" runat="server" Text="" />
                                            <i class="search"></i>Search</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <%--List of BT--%>
        <div id="panGetInfo" class="no-transition">
            <div class="tab-container">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top active"
                    role="tab">
                    <span class="ui-icon"></span>Expense Information</h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content" role="tabpanel">
                    <div class="ui-datatable ui-widget">
                        <div class="ui-datatable-tablewrapper">
                            <div class="HRTabControl">
                                <div class="HRTabNav">
                                    <ul>
                                        <li>Prepared</li>
                                        <li>Submitted</li>
                                        <li>Rejected</li>
                                        <li>Completed</li>
                                    </ul>
                                    <div style="clear: both;">
                                    </div>
                                </div>
                                <div class="HRTabList">
                                    <%--Register BT--%>
                                    <div class="HRTab">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="grvBTRegister" KeyFieldName="BTRegisterID" runat="server" Theme="Office2010Black"
                                                    AutoGenerateColumns="false">
                                                    <SettingsText EmptyDataRow="No records found!" />
                                                    <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
                                                    <SettingsPager PageSize="10" NumericButtonCount="10">
                                                        <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All" Caption="Page Size"
                                                            Items="10, 20, 30, 50, 100" />
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
                                                                <asp:CheckBox data-id='<%# Eval("BTRegisterID") %>' data-EmpCode='<%# Eval("EmployeeCode") %>'
                                                                    runat="server" ID="chkSelect" CssClass="chkSelect" onchange="CheckboxChecked(this)" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>--%>
                                                        <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                                            FieldName="BTRegisterID" Caption="ID" />
                                                        <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Right" Width="50px" FieldName="EmployeeCode"
                                                            Caption="Employee Code">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataColumn FieldName="FullName" Caption="Employee Name" Width="130px" />
                                                        <dx:GridViewBandColumn Caption="Departure">
                                                            <Columns>
                                                                <dx:GridViewDataDateColumn Width="85px" FieldName="FromDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                                <dx:GridViewDataColumn Width="50px" FieldName="FromTime" Caption="Time" />
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewBandColumn Caption="Return">
                                                            <Columns>
                                                                <dx:GridViewDataDateColumn Width="85px" FieldName="ToDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                                <dx:GridViewDataColumn Width="50px" FieldName="ToTime" Caption="Time" />
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewDataColumn FieldName="Purpose" Caption="Destination/Purpose" />
                                                        <dx:GridViewDataImageColumn Width="1px">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" ToolTip="View/Edit" CssClass='<%# If(Eval("IsOwner").ToString() = "1", "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnEditBTClick(this); HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0])"
                                                                    OnClick="btnEdit_OnClick"></asp:Button>
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataImageColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <%--Submited BT--%>
                                    <div class="HRTab deny-delete">
                                        <%--<div class="bt-legend" style="float: right; padding: 5px 0 10px;">
                                            <strong>BTS Legend:</strong> <span class="ora-done" style="padding: 0 5px;">Pending</span>
                                        </div>
                                        <div style="clear: both">
                                        </div>--%>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="grvBTSubmitted" KeyFieldName="BTRegisterID" runat="server" Theme="Office2010Black"
                                                    AutoGenerateColumns="false">
                                                    <SettingsText EmptyDataRow="No records found!" />
                                                    <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
                                                    <SettingsPager PageSize="10" NumericButtonCount="10">
                                                        <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All" Caption="Page Size"
                                                            Items="10, 20, 30, 50, 100" />
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
                                                                <asp:CheckBox data-id='<%# Eval("BTRegisterID") %>' data-EmpCode='<%# Eval("EmployeeCode") %>'
                                                                    runat="server" ID="chkSelect" CssClass="chkSelect" onchange="CheckboxChecked(this)" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>--%>
                                                        <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                                            FieldName="BTRegisterID" Caption="ID" />
                                                        <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Right" Width="50px" FieldName="EmployeeCode"
                                                            Caption="Employee Code">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataColumn FieldName="FullName" Caption="Employee Name" Width="110px" />
                                                        <dx:GridViewBandColumn Caption="Departure">
                                                            <Columns>
                                                                <dx:GridViewDataDateColumn Width="85px" FieldName="FromDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                                <dx:GridViewDataColumn Width="50px" FieldName="FromTime" Caption="Time" />
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewBandColumn Caption="Return">
                                                            <Columns>
                                                                <dx:GridViewDataDateColumn Width="85px" FieldName="ToDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                                <dx:GridViewDataColumn Width="50px" FieldName="ToTime" Caption="Time" />
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewDataColumn FieldName="Purpose" Caption="Destination/Purpose" />
                                                        <dx:GridViewDataImageColumn Width="1px">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" ToolTip="View/Edit" CssClass='<%# If(Eval("IsOwner").ToString() = "1" AndAlso Eval("FIStatus").ToString() = FIStatus.rejected.ToString(), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnEditBTClick(this); HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0])"
                                                                    OnClick="btnEdit_OnClick"></asp:Button>
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataImageColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <%--Rejected BT--%>
                                    <div class="HRTab deny-delete">
                                        <%--<div class="bt-legend" style="float: right; padding: 5px 0 10px;">
                                            <strong>BTS Legend:</strong> <span class="ora-rejected" style="padding: 0 5px">Rejected</span>
                                        </div>
                                        <div style="clear: both">
                                        </div>--%>
                                        <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="grvBTRejected" KeyFieldName="BTRegisterID" runat="server" Theme="Office2010Black"
                                                    AutoGenerateColumns="false">
                                                    <SettingsText EmptyDataRow="No records found!" />
                                                    <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
                                                    <SettingsPager PageSize="10" NumericButtonCount="10">
                                                        <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All" Caption="Page Size"
                                                            Items="10, 20, 30, 50, 100" />
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
                                                                <asp:CheckBox data-id='<%# Eval("BTRegisterID") %>' data-EmpCode='<%# Eval("EmployeeCode") %>'
                                                                    runat="server" ID="chkSelect" CssClass="chkSelect" onchange="CheckboxChecked(this)" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>--%>
                                                        <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                                            FieldName="BTRegisterID" Caption="ID" />
                                                        <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Right" Width="50px" FieldName="EmployeeCode"
                                                            Caption="Employee Code">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataColumn FieldName="FullName" Caption="Employee Name" Width="110px" />
                                                        <dx:GridViewBandColumn Caption="Departure">
                                                            <Columns>
                                                                <dx:GridViewDataDateColumn Width="85px" FieldName="FromDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                                <dx:GridViewDataColumn Width="50px" FieldName="FromTime" Caption="Time" />
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewBandColumn Caption="Return">
                                                            <Columns>
                                                                <dx:GridViewDataDateColumn Width="85px" FieldName="ToDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                                <dx:GridViewDataColumn Width="50px" FieldName="ToTime" Caption="Time" />
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewDataColumn FieldName="Purpose" Caption="Destination/Purpose" />
                                                        <dx:GridViewDataImageColumn Width="1px">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" ToolTip="View/Edit" CssClass='<%# If(Eval("IsOwner").ToString() = "1" AndAlso Eval("FIStatus").ToString() = FIStatus.rejected.ToString(), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnEditBTClick(this); HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0])"
                                                                    OnClick="btnEdit_OnClick"></asp:Button>
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataImageColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <%--Completed BT--%>
                                    <div class="HRTab deny-delete">
                                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="grvBTCompleted" KeyFieldName="BTRegisterID" runat="server" Theme="Office2010Black"
                                                    AutoGenerateColumns="false">
                                                    <SettingsText EmptyDataRow="No records found!" />
                                                    <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
                                                    <SettingsPager PageSize="10" NumericButtonCount="10">
                                                        <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All" Caption="Page Size"
                                                            Items="10, 20, 30, 50, 100" />
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
                                                                <asp:CheckBox data-id='<%# Eval("BTRegisterID") %>' data-EmpCode='<%# Eval("EmployeeCode") %>'
                                                                    runat="server" ID="chkSelect" CssClass="chkSelect" onchange="CheckboxChecked(this)" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>--%>
                                                        <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                                            FieldName="BTRegisterID" Caption="ID" />
                                                        <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Right" Width="50px" FieldName="EmployeeCode"
                                                            Caption="Employee Code">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataColumn FieldName="FullName" Caption="Employee Name" Width="110px" />
                                                        <dx:GridViewBandColumn Caption="Departure">
                                                            <Columns>
                                                                <dx:GridViewDataDateColumn Width="85px" FieldName="FromDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                                <dx:GridViewDataColumn Width="50px" FieldName="FromTime" Caption="Time" />
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewBandColumn Caption="Return">
                                                            <Columns>
                                                                <dx:GridViewDataDateColumn Width="85px" FieldName="ToDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                                <dx:GridViewDataColumn Width="50px" FieldName="ToTime" Caption="Time" />
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewDataColumn FieldName="Purpose" Caption="Destination/Purpose" />
                                                        <dx:GridViewDataImageColumn Width="1px">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" ToolTip="View" CssClass="grid-btn viewDetails-btn"
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnEditBTClick(this); HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0])"
                                                                    OnClick="btnEdit_OnClick"></asp:Button>
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataImageColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--Add/Edit form--%>
        <div id="panRegister" class="no-transition" style="display: none;">
            <%--General--%>
            <div class="tab-container">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top active"
                    role="tab">
                    <span class="ui-icon"></span>Travelling Time &amp; Purpose</h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content" role="tabpanel"
                    style="padding: 10px 1em;">
                    <%--<div class="HRTabControl edit-form">
                        <div class="HRTabNav">
                            <ul>
                                <li>Expense</li>
                                <li>Advance (refer)</li>
                            </ul>
                            <div style="clear: both;">
                            </div>
                        </div>
                        <div class="HRTabList" style="padding: 0 1.2em; border: 1px solid #ccc; border-top: none;">--%>
                    <%--Expense Tab--%>
                    <%--<div class="HRTab">--%>
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                        <ContentTemplate>
                            <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                <tbody>
                                    <tr>
                                        <td class="ui-panelgrid-cell" style="width: 120px">
                                            <label>
                                                Departure Date<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell date-time-picker validate-required">
                                            <dx:ASPxDateEdit ID="dteExpenseDepartureDate" ClientInstanceName="dteExpenseDepartureDate"
                                                runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm"
                                                ClientSideEvents-DateChanged="DepartureDateChanged">
                                                <TimeSectionProperties Visible="true">
                                                    <TimeEditProperties EditFormatString="HH:mm" />
                                                </TimeSectionProperties>
                                            </dx:ASPxDateEdit>
                                            <asp:Button runat="server" Text="Cancel" OnClientClick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))"
                                                ID="btnDepartureDateChange" class="btn secondary hide" />
                                        </td>
                                        <td class="ui-panelgrid-cell" style="width: 120px">
                                            <label>
                                                Return Date<span class="required">*</span></label>
                                        </td>
                                        <td role="gridcell" class="ui-panelgrid-cell date-time-picker validate-required">
                                            <dx:ASPxDateEdit ID="dteExpenseReturnDate" ClientInstanceName="dteExpenseReturnDate"
                                                runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm">
                                                <TimeSectionProperties Visible="true">
                                                    <TimeEditProperties EditFormatString="HH:mm" />
                                                </TimeSectionProperties>
                                            </dx:ASPxDateEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell" style="width: 100px">
                                            <label>
                                                Country<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell validate-required">
                                            <asp:DropDownList ID="ddlExpenseDestinationCountry" runat="server" Enabled="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Purpose<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell validate-required" colspan="3">
                                            <asp:TextBox ID="txtExpensePurpose" runat="server" TextMode="MultiLine" Rows="3"
                                                Style="width: 97% !important" ToolTip="If your business trip is for a project or an event , you need to specify what project or event. e.g. Project XXX or Support project XXX of ABC department"
                                                data-tooltip="all"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--</div>--%>
                    <%--Advance Tab--%>
                    <%--<div class="HRTab">
                                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="HiddenField2" />
                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                            <tbody>
                                                <tr>
                                                    <td class="ui-panelgrid-cell validate-required" colspan="2" style="padding-bottom: 10px">
                                                        <asp:CheckBox runat="server" Enabled="false" ID="chkShowNoRequestAdvance" Text=" "
                                                            CssClass="check-button" onchange="ConfirmNoRequest()" />
                                                        <label style="position: relative; top: -5px;">
                                                            No request advance (Advance amount = 0)
                                                        </label>
                                                        <asp:CheckBox runat="server" ID="chkNoRequestAdvance" Enabled="false" Text=" " CssClass="hide" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell" style="width: 120px">
                                                        <label>
                                                            Departure Date<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell date-time-picker validate-required">
                                                        <dx:ASPxDateEdit ID="dteDepartureDate" runat="server" ReadOnly="true" EditFormat="Custom"
                                                            DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm">
                                                            <TimeSectionProperties Visible="true">
                                                                <TimeEditProperties EditFormatString="HH:mm" />
                                                            </TimeSectionProperties>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" style="width: 120px">
                                                        <label>
                                                            Return Date<span class="required">*</span></label>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell date-time-picker validate-required">
                                                        <dx:ASPxDateEdit ID="dteReturnDate" runat="server" ReadOnly="true" EditFormat="Custom"
                                                            DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm">
                                                            <TimeSectionProperties Visible="true">
                                                                <TimeEditProperties EditFormatString="HH:mm" />
                                                            </TimeSectionProperties>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell" style="width: 100px">
                                                        <label>
                                                            Country<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required">
                                                        <asp:DropDownList runat="server" ID="ddlDestinationCountry" Enabled="false">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td runat="server" id="tdRequestDestinationLabel" visible="false" class="ui-panelgrid-cell">
                                                        <label>
                                                            Destination<span class="required">*</span></label>
                                                    </td>
                                                    <td runat="server" id="tdRequestDestinationControl" visible="false" class="ui-panelgrid-cell validate-required"
                                                        style="padding-top: 10px">
                                                        <asp:DropDownList ID="ddlRequestDestination" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Purpose<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell validate-required" colspan="3">
                                                        <asp:TextBox ID="txtPurpose" runat="server" ReadOnly="true" TextMode="MultiLine"
                                                            Rows="3" Style="width: 97% !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Request to GA</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" colspan="3">
                                                        <asp:CheckBox runat="server" ID="chkRequestAirTicket" Text=" " CssClass="check-button"
                                                            Enabled="false" />
                                                        <span style="position: relative; top: -4px; padding-right: 8px">Air Ticket</span>
                                                        <asp:CheckBox runat="server" ID="chkRequestTrainTicket" Text=" " CssClass="check-button"
                                                            Enabled="false" />
                                                        <span style="position: relative; top: -4px; padding-right: 8px;">Train Ticket</span>
                                                        <asp:CheckBox runat="server" ID="chkRequestCar" Text=" " CssClass="check-button"
                                                            Enabled="false" />
                                                        <span style="position: relative; top: -4px;">Car</span>
                                                        <asp:HiddenField runat="server" ID="hRequestGA" />
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trExpectedDeparture" visible="false">
                                                    <td class="ui-panelgrid-cell">
                                                    </td>
                                                    <td class="ui-panelgrid-cell date-time-picker">
                                                        Expected Departure Time:
                                                        <dx:ASPxDateEdit ID="dteExpectedDepartureTime" runat="server" ReadOnly="true" EditFormat="Custom"
                                                            DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm">
                                                            <TimeSectionProperties Visible="true">
                                                                <TimeEditProperties EditFormatString="HH:mm" />
                                                            </TimeSectionProperties>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" colspan="2">
                                                        Expected Departure Flight No:<br />
                                                        <asp:TextBox ID="txtExpectedDepartureFlightNo" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trExpectedReturn" visible="false">
                                                    <td class="ui-panelgrid-cell">
                                                    </td>
                                                    <td class="ui-panelgrid-cell date-time-picker">
                                                        Expected Return Time:
                                                        <dx:ASPxDateEdit ID="dteExpectedReturnTime" runat="server" ReadOnly="true" EditFormat="Custom"
                                                            DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm">
                                                            <TimeSectionProperties Visible="true">
                                                                <TimeEditProperties EditFormatString="HH:mm" />
                                                            </TimeSectionProperties>
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" colspan="2">
                                                        Expected Return Flight No:<br />
                                                        <asp:TextBox ID="txtExpectedReturnFlightNo" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>--%>
                </div>
            </div>
            <%--Advance Request--%>
            <div class="tab-container">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                    role="tab">
                    <span class="ui-icon"></span>Expense Declaration</h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content default-hide hide"
                    role="tabpanel" style="position: relative; padding: 10px 1em;">
                    <%--Expense Tab--%>
                    <%--Summary--%>
                    <div class="ui-datatable ui-widget">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <div>
                                    <table style="margin: 0; background-color: #f9f9f9;" table-layout="fixed">
                                        <tr>
                                            <th style="border: 1px solid #ccc; padding: 5px 5px" colspan="5">
                                                A. Expense
                                            </th>
                                            <th style="border: 1px solid #ccc; padding: 5px 5px" rowspan="2">
                                                B. Advance
                                                <br />
                                                (Cash)
                                            </th>
                                            <th style="border: 1px solid #ccc; padding: 5px 5px" rowspan="2">
                                                C. Credit Card
                                            </th>
                                            <th style="border: 1px solid #ccc; padding: 5px 5px; color: red" rowspan="2">
                                                D. Disparity<br />
                                                (D) = (B + C) - A
                                            </th>
                                        </tr>
                                        <tr>
                                            <td style="border: 1px solid #ccc; padding: 5px 5px; text-align: center">
                                                Daily Allowance<br />
                                                (A1)
                                            </td>
                                            <td style="border: 1px solid #ccc; padding: 5px 5px; text-align: center">
                                                Hotel<br />
                                                (A2)
                                            </td>
                                            <td style="border: 1px solid #ccc; padding: 5px 5px; text-align: center">
                                                Moving Time Allowance<br />
                                                (A3)
                                            </td>
                                            <td style="border: 1px solid #ccc; padding: 5px 5px; text-align: center">
                                                Transportation &amp; Other<br />
                                                (A4)
                                            </td>
                                            <td style="border: 1px solid #ccc; padding: 5px 5px; background-color: #ededed; color: red;
                                                text-align: center">
                                                Total<br />
                                                (A) = A1 + A2 + A3 + A4
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right; padding: 5px 5px">
                                                <asp:Label runat="server" CssClass="total-summary" ID="lblExpenseDailyAllowance"
                                                    Text="0"></asp:Label>
                                            </td>
                                            <td style="text-align: right; padding: 5px 5px">
                                                <asp:Label runat="server" CssClass="total-summary" ID="lblExpenseHotelExpense" Text="0"></asp:Label>
                                            </td>
                                            <td style="text-align: right; padding: 5px 5px">
                                                <asp:Label runat="server" CssClass="total-summary" ID="lblExpenseMovingTimeAllowance"
                                                    Text="0"></asp:Label>
                                            </td>
                                            <td style="text-align: right; padding: 5px 5px">
                                                <asp:Label runat="server" CssClass="total-summary" ID="lblExpenseOtherExpense" Text="0"></asp:Label>
                                            </td>
                                            <td style="text-align: right; padding: 5px 5px; background-color: #ededed; font-weight: bold;
                                                color: red">
                                                <asp:Label runat="server" CssClass="total-summary" ID="lblExpenseTotalExpense" Text="0"></asp:Label>
                                            </td>
                                            <td style="text-align: right; padding: 5px 5px">
                                                <asp:Label runat="server" CssClass="total-summary" ID="lblCashAdvance" Text="0"></asp:Label>
                                            </td>
                                            <td style="text-align: right; padding: 5px 5px">
                                                <asp:Label runat="server" CssClass="total-summary" ID="lblCreditAdvance" Text="0"></asp:Label>
                                            </td>
                                            <td style="text-align: right; padding: 5px 5px; background-color: #dcdcdc; font-weight: bold;
                                                color: red">
                                                <asp:Label runat="server" CssClass="total-summary" ID="lblDisparity" Text="0" data-tooltip="all"
                                                    title=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <%--Expense Info--%>
                    <div class="HRTabControl edit-form" style="margin-top: 10px; margin-bottom: 10px;">
                        <div class="HRTabNav">
                            <ul>
                                <li id="tabExpenseCommon">Allowance</li>
                                <li id="tabExpenseOther">Transportation &amp; Other</li>
                            </ul>
                            <div style="clear: both;">
                            </div>
                        </div>
                        <%--Expense tabs--%>
                        <div class="HRTabList">
                            <%--Common--%>
                            <div class="HRTab" style="position: relative;">
                                <%--Global Request--%>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hExpenseMovingTime" />
                                        <asp:HiddenField runat="server" ID="hExpenseOtherAmount" />
                                        <asp:HiddenField runat="server" ID="hExpenseFirstTime" />
                                        <div style="float: right; margin-top: 5px;">
                                            <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="width: 750px;
                                                margin-top: 7px;">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: auto">
                                                            &nbsp;
                                                        </td>
                                                        <%--Moving Time Allowance--%>
                                                        <td runat="server" id="spanExpenseMovingTime" class="ui-panelgrid-cell" style="text-align: right;
                                                            width: 250px; padding-top: 2px; padding-right: 2px">
                                                            <asp:CheckBox runat="server" ID="chkExpenseMovingTimeAllowance" AutoPostBack="false"
                                                                Text=" " CssClass="check-button" onchange="CalculateSummary()" />
                                                            <label style="position: relative; top: -5px;">
                                                                Moving Time Allowance <span runat="server" id="spanExpenseMovingTimeAmount" style="color: red">
                                                                </span>
                                                            </label>
                                                        </td>
                                                        <%--First Time Oversea--%>
                                                        <td runat="server" id="spanExpenseFirstTimeOversea" class="ui-panelgrid-cell" style="text-align: right;
                                                            width: 210px; padding-top: 2px; padding-right: 2px">
                                                            <asp:CheckBox runat="server" ID="chkExpenseFirstTimeOversea" AutoPostBack="false"
                                                                Text=" " CssClass="check-button" onchange="CalculateSummary()" />
                                                            <label style="position: relative; top: -5px;">
                                                                First Time Oversea <span runat="server" id="spanExpenseFirstAmount" style="color: red">
                                                                </span>
                                                            </label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <span id="btnAddCommonExpense" style="margin-top: 10px; text-align: center;" class="btn inform add-btn"
                                    onclick="btnAddSub_Click(this)"><i class="add"></i>Add</span>
                                <%--List--%>
                                <div class="ui-datatable ui-widget">
                                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                        <ContentTemplate>
                                            <div style="margin-top: 10px; border: 1px solid transparent">
                                                <dx:ASPxGridView ID="grvCommonExpense" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                                    AutoGenerateColumns="false" Style="margin-top: 0px; table-layout: fixed" SettingsBehavior-AllowSort="false">
                                                    <SettingsText EmptyDataRow="No records found!" />
                                                    <Styles>
                                                        <AlternatingRow Enabled="True">
                                                        </AlternatingRow>
                                                    </Styles>
                                                    <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                                    <Columns>
                                                        <dx:GridViewDataColumn Width="25px" Settings-AllowAutoFilter="False" FieldName="No"
                                                            Caption="No" />
                                                        <dx:GridViewDataDateColumn FieldName="Date" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                        <dx:GridViewBandColumn Caption="Daily Allowance">
                                                            <Columns>
                                                                <dx:GridViewDataTextColumn FieldName="BreakfastAmount" Width="90" Caption="Breakfast"
                                                                    CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                                <dx:GridViewDataTextColumn FieldName="LunchAmount" Width="90" Caption="Lunch" CellStyle-HorizontalAlign="Right"
                                                                    PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                                <dx:GridViewDataTextColumn FieldName="DinnerAmount" Width="90" Caption="Dinner" CellStyle-HorizontalAlign="Right"
                                                                    PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                                <dx:GridViewDataTextColumn FieldName="OtherAmount" Width="90" Caption="Other" CellStyle-HorizontalAlign="Right"
                                                                    PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                                <dx:GridViewDataTextColumn FieldName="TotalAmountFormated" Width="110" Caption="Total"
                                                                    CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}"
                                                                    CellStyle-BackColor="#eeeeee" />
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewDataTextColumn FieldName="HotelAmountFormated" Width="110" Caption="Hotel"
                                                            CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                        <dx:GridViewDataImageColumn Width="55px">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" CssClass='<%# If(Convert.ToBoolean(Eval("EnableForm")), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("ID") %>' OnClick="btnEditExpenseRequest_OnClick" OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))">
                                                                </asp:Button>
                                                                <input type="button" class="grid-btn delete-btn" title="Delete" runat="server" visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>'
                                                                    onclick="btnDeleteRequestClick(this)" />
                                                                <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' Visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>'
                                                                    ToolTip="Delete" runat="server" CssClass="hide" OnClick="btnDeleteRequest_OnClick"
                                                                    OnClientClick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))">
                                                                </asp:Button>
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataImageColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%--Edit form--%>
                                <fieldset class="add-edit-form" id="ExpenseAdvanceForm">
                                    <legend><span class="add-edit-action"></span>&nbsp;Allowance Information</legend>
                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                        <ContentTemplate>
                                            <asp:HiddenField runat="server" ID="hExpenseRequestID" />
                                            <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                                <tbody>
                                                    <tr>
                                                        <td class="ui-panelgrid-cell" style="width: 110px">
                                                            <label>
                                                                Location<span class="required">*</span></label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell validate-required">
                                                            <asp:DropDownList ID="ddlExpenseDestinationLocation" AutoPostBack="false" runat="server">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="ui-panelgrid-cell" style="width: 100px">
                                                            <label>
                                                                Date<span class="required">*</span></label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell date-time-picker validate-required">
                                                            <dx:ASPxDateEdit ID="dteDate" ClientInstanceName="dteDate" runat="server" EditFormat="Custom"
                                                                DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy">
                                                            </dx:ASPxDateEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="ui-panelgrid-cell">
                                                            <label>
                                                                Daily Allowance</label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell" colspan="3">
                                                            <asp:HiddenField runat="server" ID="hBreakFastUnit" />
                                                            <asp:HiddenField runat="server" ID="hLunchUnit" />
                                                            <asp:HiddenField runat="server" ID="hDinnerUnit" />
                                                            <asp:HiddenField runat="server" ID="hOtherUnit" />
                                                            <asp:HiddenField runat="server" ID="hHotelUnit" />
                                                            <asp:HiddenField runat="server" ID="hBreakfastAmount" />
                                                            <asp:HiddenField runat="server" ID="hLunchAmount" />
                                                            <asp:HiddenField runat="server" ID="hDinnerAmount" />
                                                            <asp:HiddenField runat="server" ID="hOtherAmount" />
                                                            <table class="tblRequestDetails grid-inside" style="table-layout: fixed;">
                                                                <tr class="tr-header tr-request">
                                                                    <td style="text-align: center">
                                                                        <asp:CheckBox runat="server" AutoPostBack="false" onchange="CalculateRequest()" ID="chkBreakfastAmount"
                                                                            Text=" " CssClass="check-button" Style="top: 2px;" />
                                                                        <span style="position: relative; top: -3px; left: -5px;">Breakfast</span>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:CheckBox runat="server" AutoPostBack="false" onchange="CalculateRequest()" ID="chkLunchAmount"
                                                                            Text=" " CssClass="check-button" Style="top: 2px;" />
                                                                        <span style="position: relative; top: -3px; left: -5px;">Lunch</span>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:CheckBox runat="server" AutoPostBack="false" onchange="CalculateRequest()" ID="chkDinnerAmount"
                                                                            Text=" " CssClass="check-button" Style="top: 2px;" />
                                                                        <span style="position: relative; top: -3px; left: -5px;">Dinner</span>
                                                                    </td>
                                                                    <td style="text-align: center">
                                                                        <asp:CheckBox runat="server" AutoPostBack="false" onchange="CalculateRequest()" ID="chkOtherAmount"
                                                                            Text=" " CssClass="check-button" Style="top: 2px;" />
                                                                        <span style="position: relative; top: -3px; left: -5px;">Other</span>
                                                                    </td>
                                                                    <td style="text-align: center; background-color: #eee;">
                                                                        Total
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        Actual Currency
                                                                    </td>
                                                                    <%--<td style="text-align: center; display: none;" runat="server" id="tdCommonExrateCaption">
                                                                        Ex. Rate<br />
                                                                        <asp:Label runat="server" CssClass="exrate-caption" data-exrate="common-exrate" ID="lblCommonExrate"
                                                                            Style="font-weight: normal"></asp:Label>
                                                                    </td>
                                                                    <td runat="server" id="tdCommonConvertedCaption" style="text-align: center; background-color: #eee !important;
                                                                        display: none;">
                                                                        Converted<br />
                                                                        <span style="font-weight: normal">(USD)</span>
                                                                    </td>--%>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-input daily-allowance-amount expense-amount">
                                                                        <dx:ASPxSpinEdit ID="spiBreakfastAmount" ClientInstanceName="spiBreakfastAmount"
                                                                            runat="server" Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float"
                                                                            NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" SpinButtons-ShowIncrementButtons="false"
                                                                            ReadOnly="true">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="td-input expense-amount">
                                                                        <dx:ASPxSpinEdit ID="spiLunchAmount" ClientInstanceName="spiLunchAmount" runat="server"
                                                                            Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float" NullText="0"
                                                                            DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" SpinButtons-ShowIncrementButtons="false"
                                                                            ReadOnly="true">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="td-input expense-amount">
                                                                        <dx:ASPxSpinEdit ID="spiDinnerAmount" ClientInstanceName="spiDinnerAmount" runat="server"
                                                                            Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float" NullText="0"
                                                                            DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" SpinButtons-ShowIncrementButtons="false"
                                                                            ReadOnly="true">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="td-input expense-amount other-amount">
                                                                        <dx:ASPxSpinEdit ID="spiOtherAmount" ClientInstanceName="spiOtherAmount" runat="server"
                                                                            Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float" NullText="0"
                                                                            DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" SpinButtons-ShowIncrementButtons="false"
                                                                            ReadOnly="true">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="td-input total-amount" style="background-color: #eee">
                                                                        <dx:ASPxSpinEdit ID="spiCommonTotalAmount" ReadOnly="true" runat="server" Height="21px"
                                                                            MinValue="0" MaxValue="1000000000000" NumberType="Float" NullText="0" DecimalPlaces="2"
                                                                            DisplayFormatString="{0:#,0.##}" SpinButtons-ShowIncrementButtons="false" Number="0">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="td-input">
                                                                        <asp:DropDownList runat="server" ID="ddlCommonCurrency" CssClass="exrate-currency"
                                                                            Enabled="false" data-exrate="common-exrate" Style="border: none; margin: 0; width: 100% !important"
                                                                            onchange="$('[id$=lblCommonCCCurrency]').text(this.options[this.selectedIndex].text)">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <%--<td runat="server" id="tdCommonExrate" class="td-input expense-exrate exrate-value"
                                                                        data-exrate="common-exrate" style="display: none">
                                                                        <dx:ASPxSpinEdit ID="spiCommonExrate" runat="server" Height="21px" MinValue="1" MaxValue="1000000000000"
                                                                            NumberType="Float" NullText="1" DecimalPlaces="6" DisplayFormatString="{0:#,0.##}">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td runat="server" id="tdCommonConverted" class="td-input total-converted" style="background-color: #eee;
                                                                        display: none">
                                                                        <dx:ASPxSpinEdit ID="spiCommonTotalConverted" ReadOnly="true" runat="server" Height="21px"
                                                                            MinValue="0" MaxValue="1000000000000" NumberType="Float" NullText="0" DecimalPlaces="2"
                                                                            DisplayFormatString="{0:#,0.##}" SpinButtons-ShowIncrementButtons="false" Number="0">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>--%>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trCommonCredit">
                                                        <td class="ui-panelgrid-cell">
                                                        </td>
                                                        <td class="ui-panelgrid-cell spin-edit cc-edit" colspan="3">
                                                            <div style="float: left; padding-top: 3px; width: 99px;">
                                                                <asp:CheckBox runat="server" AutoPostBack="false" onchange="chkCommonCCAmountChanged()"
                                                                    ID="chkCommonCCAmount" Text=" " CssClass="check-button" Style="top: 2px;" />
                                                                <span style="position: relative; top: -3px; left: -5px;">Credit Card</span>
                                                            </div>
                                                            <dx:ASPxSpinEdit ID="spiCommonCCAmount" runat="server" ClientInstanceName="spiCommonCCAmount"
                                                                CssClass="common-credit" Style="float: left; width: 50px !important; display: none;"
                                                                MinValue="0" MaxValue="1000000000000" NumberType="Float" NullText="0" DecimalPlaces="2"
                                                                HorizontalAlign="Right" DisplayFormatString="{0:#,0.##}">
                                                            </dx:ASPxSpinEdit>
                                                            <asp:Label runat="server" ID="lblCommonCCCurrency" CssClass="common-credit" Style="float: left;
                                                                padding-top: 7px; margin-left: 5px; display: none;"></asp:Label>
                                                            <asp:Label runat="server" ID="lblCommonCCMessage" CssClass="common-credit" Style="float: left;
                                                                padding-top: 7px; margin-left: 10px; color: Red; display: none;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="ui-panelgrid-cell">
                                                            <label>
                                                                Hotel Expense</label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell" colspan="3">
                                                            <table class="tblRequestDetails grid-inside" style="table-layout: fixed;">
                                                                <tr class="tr-header">
                                                                    <td style="text-align: center;">
                                                                        Amount
                                                                    </td>
                                                                    <td style="text-align: center;">
                                                                        Actual Currency
                                                                    </td>
                                                                    <td runat="server" id="tdHotelExchangeDateCaption" style="text-align: center;">
                                                                        Invoice Date
                                                                    </td>
                                                                    <td runat="server" id="tdHotelExrateCaption" style="text-align: center;">
                                                                        Exchange Rate<br />
                                                                        <asp:Label runat="server" ID="lblHotelExrate" CssClass="exrate-caption" data-exrate="hotel-exrate"
                                                                            Style="font-weight: normal"></asp:Label>
                                                                    </td>
                                                                    <td runat="server" id="tdHotelConvertedCaption" style="text-align: center; background-color: #eee !important">
                                                                        Converted<br />
                                                                        <span style="font-weight: normal">(USD)</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="td-input hotel-amount expense-amount">
                                                                        <dx:ASPxSpinEdit ID="spiHotelAmount" runat="server" Height="21px" MinValue="0" MaxValue="1000000000000"
                                                                            NumberType="Float" NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td class="td-input">
                                                                        <asp:DropDownList runat="server" ID="ddlHotelCurrency" CssClass="exrate-currency"
                                                                            data-exrate="hotel-exrate" Style="border: none; margin: 0; width: 100% !important"
                                                                            onchange="$('[id$=lblHotelCCCurrency]').text(this.options[this.selectedIndex].text)">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td runat="server" id="tdHotelExchangeDate" class="td-input date-time-edit">
                                                                        <dx:ASPxDateEdit ID="dteHotelExchangeDate" ClientInstanceName="dteHotelExchangeDate"
                                                                            ClientSideEvents-DateChanged="HotelExdateChange" Style="width: 100% !important"
                                                                            runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy">
                                                                        </dx:ASPxDateEdit>
                                                                    </td>
                                                                    <td runat="server" id="tdHotelExrate" class="td-input expense-exrate exrate-value"
                                                                        data-exrate="hotel-exrate">
                                                                        <dx:ASPxSpinEdit ID="spiHotelExrate" runat="server" Height="21px" MinValue="1" MaxValue="1000000000000"
                                                                            NumberType="Float" NullText="1" DecimalPlaces="6" DisplayFormatString="{0:#,0.##}">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td runat="server" id="tdHotelConverted" class="td-input total-converted" style="background-color: #eee">
                                                                        <dx:ASPxSpinEdit ID="spiHotelTotalConverted" ReadOnly="true" runat="server" Height="21px"
                                                                            MinValue="0" MaxValue="1000000000000" NumberType="Float" NullText="0" DecimalPlaces="2"
                                                                            DisplayFormatString="{0:#,0.##}" SpinButtons-ShowIncrementButtons="false" Number="0">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trHotelCredit">
                                                        <td class="ui-panelgrid-cell">
                                                        </td>
                                                        <td class="ui-panelgrid-cell spin-edit cc-edit" colspan="3">
                                                            <div style="float: left; padding-top: 3px; width: 99px;">
                                                                <asp:CheckBox runat="server" AutoPostBack="false" onchange="chkHotelCCAmountChanged()"
                                                                    ID="chkHotelCCAmount" Text=" " CssClass="check-button" Style="top: 2px;" />
                                                                <span style="position: relative; top: -3px; left: -5px;">Credit Card</span>
                                                            </div>
                                                            <dx:ASPxSpinEdit ID="spiHotelCCAmount" ClientInstanceName="spiHotelCCAmount" CssClass="hotel-credit"
                                                                runat="server" Style="float: left; width: 50px !important; display: none;" MinValue="0"
                                                                MaxValue="1000000000000" NumberType="Float" NullText="0" DecimalPlaces="2" HorizontalAlign="Right"
                                                                DisplayFormatString="{0:#,0.##}">
                                                            </dx:ASPxSpinEdit>
                                                            <asp:Label runat="server" ID="lblHotelCCCurrency" CssClass="hotel-credit" Style="float: left;
                                                                padding-top: 7px; margin-left: 5px; display: none;"></asp:Label>
                                                            <asp:Label runat="server" ID="lblHotelCCMessage" CssClass="hotel-credit" Style="float: left;
                                                                padding-top: 7px; margin-left: 10px; color: Red; display: none;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSaveRequest" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <ul id="ExpenseAdvanceSummary" class="error-summary">
                                    </ul>
                                    <div class="action-pan">
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                            <ContentTemplate>
                                                <span runat="server" id="btnShowSaveRequest">
                                                    <input type="button" class="btn" value="Save" onclick="ValidateAdvanceForm(this)" />
                                                </span>
                                                <asp:Button runat="server" ID="btnSaveRequest" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                                <input type="button" id="btnCancelRequest" value="Cancel" class="btn secondary btn-cancel-sub"
                                                    onclick="btnCancelSub_Click(this); ClearRequestForm();" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </fieldset>
                            </div>
                            <%--Transportation & Other--%>
                            <div class="HRTab">
                                <span id="Span2" style="margin-top: 10px; text-align: center;" class="btn inform add-btn"
                                    onclick="btnAddSub_Click(this)"><i class="add"></i>Add</span>
                                <%--List--%>
                                <div class="ui-datatable ui-widget">
                                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                        <ContentTemplate>
                                            <div style="margin-top: 10px; border: 1px solid transparent">
                                                <dx:ASPxGridView ID="grvBTExpenseOther" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                                    AutoGenerateColumns="false" Style="margin-top: 0px" SettingsBehavior-AllowSort="false">
                                                    <SettingsText EmptyDataRow="No records found!" />
                                                    <Styles>
                                                        <AlternatingRow Enabled="True">
                                                        </AlternatingRow>
                                                    </Styles>
                                                    <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                                    <Columns>
                                                        <dx:GridViewDataColumn Width="25px" Settings-AllowAutoFilter="False" FieldName="No"
                                                            Caption="No" />
                                                        <dx:GridViewDataDateColumn Width="100px" FieldName="Date" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                        <dx:GridViewDataColumn FieldName="Expense" Caption="Expense" />
                                                        <dx:GridViewDataTextColumn FieldName="Amount" Width="120px" Caption="Amount" CellStyle-HorizontalAlign="Right"
                                                            PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                        <dx:GridViewDataColumn FieldName="Currency" Width="90px" Caption="Currency" />
                                                        <dx:GridViewDataImageColumn Width="40px">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" CssClass='<%# If(Convert.ToBoolean(Eval("EnableForm")), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("ID") %>' OnClick="btnEditExpenseOther_OnClick" OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))">
                                                                </asp:Button>
                                                                <input type="button" class="grid-btn delete-btn" title="Delete" runat="server" visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>'
                                                                    onclick="btnDeleteOtherClick(this)" />
                                                                <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' Visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>'
                                                                    ToolTip="Delete" runat="server" CssClass="hide" OnClick="btnDeleteOther_OnClick"
                                                                    OnClientClick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))">
                                                                </asp:Button>
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataImageColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%--Edit form--%>
                                <fieldset class="add-edit-form" id="ExpenseOtherForm">
                                    <legend><span class="add-edit-action"></span>&nbsp;Other Information</legend>
                                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                        <ContentTemplate>
                                            <asp:HiddenField runat="server" ID="hExpenseOtherID" />
                                            <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                                <tbody>
                                                    <tr>
                                                        <td class="ui-panelgrid-cell" style="width: 150px;">
                                                            <label>
                                                                Date<span class="required">*</span></label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell date-time-picker validate-required">
                                                            <dx:ASPxDateEdit ID="dteExpenseOtherDate" ClientInstanceName="dteExpenseOtherDate"
                                                                runat="server" EditFormat="Custom" ClientSideEvents-DateChanged="OtherDateChange"
                                                                DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy">
                                                            </dx:ASPxDateEdit>
                                                        </td>
                                                        <td style="width: 140px;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="ui-panelgrid-cell">
                                                            <label>
                                                                Expense<span class="required">*</span></label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell validate-required" colspan="3" style="padding-bottom: 0">
                                                            <asp:TextBox ID="txtOtherExpense" runat="server" TextMode="MultiLine" Rows="3" Style="width: 97% !important"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td role="gridcell" class="ui-panelgrid-cell">
                                                            <label>
                                                                Amount<span class="required">*</span></label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell spin-edit expense-other-amount validate-required">
                                                            <dx:ASPxSpinEdit ID="spiExpenseOtherAmount" runat="server" Height="21px" MinValue="0"
                                                                MaxValue="1000000000000" NumberType="Float" NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}"
                                                                HorizontalAlign="Right">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                        <td class="ui-panelgrid-cell">
                                                            <label>
                                                                Actual Currency<span class="required">*</span></label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell validate-required">
                                                            <asp:DropDownList runat="server" ID="ddlExpenseOtherCurrency" CssClass="exrate-currency"
                                                                data-exrate="expense-other-exrate" onchange="$('[id$=lblOtherCCCurrency]').text(this.options[this.selectedIndex].text)">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trOtherCredit">
                                                        <td class="ui-panelgrid-cell">
                                                        </td>
                                                        <td class="ui-panelgrid-cell spin-edit cc-edit" colspan="3">
                                                            <div style="float: left; padding-top: 3px; width: 99px;">
                                                                <asp:CheckBox runat="server" AutoPostBack="false" onchange="chkOtherCCAmountChanged()"
                                                                    ID="chkOtherCCAmount" Text=" " CssClass="check-button" Style="top: 2px;" />
                                                                <span style="position: relative; top: -3px; left: -5px;">Credit Card</span>
                                                            </div>
                                                            <dx:ASPxSpinEdit ID="spiOtherCCAmount" ClientInstanceName="spiOtherCCAmount" CssClass="other-credit"
                                                                runat="server" Style="float: left; width: 50px !important; display: none;" MinValue="0"
                                                                MaxValue="1000000000000" NumberType="Float" NullText="0" DecimalPlaces="2" HorizontalAlign="Right"
                                                                DisplayFormatString="{0:#,0.##}">
                                                            </dx:ASPxSpinEdit>
                                                            <asp:Label runat="server" ID="lblOtherCCCurrency" CssClass="other-credit" Style="float: left;
                                                                padding-top: 7px; margin-left: 5px; display: none;"></asp:Label>
                                                            <asp:Label runat="server" ID="lblOtherCCMessage" CssClass="other-credit" Style="float: left;
                                                                padding-top: 7px; margin-left: 10px; color: Red; display: none;"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="trOtherExchange">
                                                        <td role="gridcell" class="ui-panelgrid-cell">
                                                            <label>
                                                                Exchange Rate<span class="required">*</span></label>
                                                            <br />
                                                            <asp:Label runat="server" ID="lblExpenseOtherExrate" CssClass="exrate-caption" data-exrate="expense-other-exrate"
                                                                Style="font-weight: normal"></asp:Label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell spin-edit expense-other-exrate validate-required exrate-value"
                                                            data-exrate="expense-other-exrate">
                                                            <dx:ASPxSpinEdit ID="spiExpenseOtherExrate" runat="server" Height="21px" MinValue="1"
                                                                MaxValue="1000000000000" NumberType="Float" NullText="1" DecimalPlaces="6" DisplayFormatString="{0:#,0.##}"
                                                                HorizontalAlign="Right">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                        <td role="gridcell" class="ui-panelgrid-cell" style="width: 100px">
                                                            <label>
                                                                Amount Converted<br />
                                                                (USD)</label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell spin-edit expense-other-amount-converted">
                                                            <asp:TextBox runat="server" ID="txtExpenseOtherAmountConverted" Style="text-align: right"
                                                                ReadOnly="true" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSaveOther" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <ul id="ExpenseOtherSummary" class="error-summary">
                                    </ul>
                                    <div class="action-pan">
                                        <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                                            <ContentTemplate>
                                                <span runat="server" id="btnShowSaveOther">
                                                    <input type="button" class="btn" value="Save" onclick="ValidateOtherForm(this)" />
                                                </span>
                                                <asp:Button runat="server" ID="btnSaveOther" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                                <input type="button" id="btnCancelOther" value="Cancel" class="btn secondary btn-cancel-sub"
                                                    onclick="btnCancelSub_Click(this); ClearOtherForm()" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
                <%--Advance Tab--%>
                <%--<div class="HRTab">
                                <%--Summary--
                                <div class="ui-datatable ui-widget">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                            <div>
                                                <table style="background-color: #f9f9f9; margin: 0;">
                                                    <tr>
                                                        <th style="border: 1px solid #ccc; padding: 5px 5px" colspan="5">
                                                            Advance Request
                                                        </th>
                                                        <%--<th style="border: 1px solid #ccc; padding: 5px 5px" rowspan="2">
                                                            Request to GA
                                                        </th>--
                                                    </tr>
                                                    <tr>
                                                        <td style="border: 1px solid #ccc; padding: 5px 5px; text-align: center;">
                                                            Daily Allowance<br />
                                                            (1)
                                                        </td>
                                                        <td style="border: 1px solid #ccc; padding: 5px 5px; text-align: center;">
                                                            Hotel<br />
                                                            (2)
                                                        </td>
                                                        <td style="border: 1px solid #ccc; padding: 5px 5px; text-align: center;">
                                                            Moving Time Allowance<br />
                                                            (3)
                                                        </td>
                                                        <td style="border: 1px solid #ccc; padding: 5px 5px; text-align: center;">
                                                            Transportation &amp; Other<br />
                                                            (4)
                                                        </td>
                                                        <th style="border: 1px solid #ccc; padding: 5px 5px; background-color: #ededed; color: red">
                                                            Total<br />
                                                            (5) = (1) + (2) + (3) + (4)
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right; padding: 5px 5px">
                                                            <asp:Label runat="server" CssClass="total-summary" ID="lblDailyAllowance" Text="0"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right; padding: 5px 5px">
                                                            <asp:Label runat="server" CssClass="total-summary" ID="lblHotelExpense" Text="0"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right; padding: 5px 5px">
                                                            <asp:Label runat="server" CssClass="total-summary" ID="lblMovingTimeAllowance" Text="0"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right; padding: 5px 5px">
                                                            <asp:Label runat="server" CssClass="total-summary" ID="lblOther" Text="0"></asp:Label>
                                                        </td>
                                                        <td style="text-align: right; padding: 5px 5px; background-color: #ededed; font-weight: bold;
                                                            color: red">
                                                            <asp:Label runat="server" CssClass="total-summary" ID="lblTotalAdvance" Text="0"></asp:Label>
                                                        </td>
                                                        <%--<td style="padding: 5px 5px">
                                                            <asp:Label runat="server" CssClass="total-summary" ID="lblRequestGA"></asp:Label>
                                                        </td>--
                                                    </tr>
                                                </table>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%--Advance Info--
                                <div class="HRTabControl edit-form" style="margin-top: 10px; margin-bottom: 10px;">
                                    <div class="HRTabNav">
                                        <ul>
                                            <li>Common</li>
                                            <li>Schedule</li>
                                        </ul>
                                        <div style="clear: both;">
                                        </div>
                                    </div>
                                    <%--Advance tabs--
                                    <div class="HRTabList">
                                        <%--Common--
                                        <div class="HRTab" style="position: relative;">
                                            <%--Global Request--
                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                    <div style="float: right; margin-top: 5px;">
                                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="width: 750px;
                                                            margin-top: 7px;">
                                                            <tbody>
                                                                <tr>
                                                                    <td style="width: auto">
                                                                        &nbsp;
                                                                    </td>
                                                                    <%--Moving Time Allowance--
                                                                    <td runat="server" id="spanMovingTime" class="ui-panelgrid-cell" style="text-align: right;
                                                                        width: 220px; padding-top: 2px; padding-right: 2px">
                                                                        <asp:CheckBox runat="server" Enabled="false" ID="chkMovingTimeAllowance" AutoPostBack="true"
                                                                            Text=" " CssClass="check-button" onchange="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))" /><label
                                                                                style="position: relative; top: -5px;">
                                                                                Moving Time Allowance
                                                                            </label>
                                                                    </td>
                                                                    <td runat="server" id="spanMovingTime1" class="ui-panelgrid-cell" style="color: red;
                                                                        width: 90px; text-align: right; padding: 0 0 5px 0;">
                                                                        +<span runat="server" id="spanMovingTimeAmount"></span>
                                                                    </td>
                                                                    <td runat="server" id="spanMovingTime2" style="color: red; width: 1px; text-align: center;
                                                                        padding: 0 1px 5px 1px;">
                                                                        =
                                                                    </td>
                                                                    <td runat="server" id="spanMovingTime3" class="ui-panelgrid-cell spin-edit first-time"
                                                                        style="width: 1%; padding: 0px 2px; color: Red;">
                                                                        <dx:ASPxSpinEdit ID="spiMovingTimeAllowanceVND" runat="server" ReadOnly="true" Width="100px"
                                                                            Style="position: relative; top: -1px; display: inline-block;" MinValue="0" MaxValue="1000000000000"
                                                                            NumberType="Float" NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}"
                                                                            Increment="100">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td runat="server" id="spanMovingTime4" class="ui-panelgrid-cell" style="width: 1px;
                                                                        padding: 0 2px 5px 3px; color: Red;">
                                                                        VND
                                                                    </td>
                                                                    <%--First Time Oversea--
                                                                    <td runat="server" id="spanFirstTimeOversea" class="ui-panelgrid-cell" style="text-align: right;
                                                                        width: 180px; padding-top: 2px; padding-right: 2px">
                                                                        <asp:CheckBox runat="server" ID="chkFirstTimeOversea" Enabled="false" AutoPostBack="true"
                                                                            Text=" " CssClass="check-button" onchange="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))" /><label
                                                                                style="position: relative; top: -5px;">
                                                                                First Time Oversea
                                                                            </label>
                                                                    </td>
                                                                    <td runat="server" id="spanFirstTimeOversea1" class="ui-panelgrid-cell" style="color: red;
                                                                        width: 75px; text-align: right; padding: 0 0 5px 0;">
                                                                        +<span runat="server" id="spanFirstAmount"></span> USD
                                                                    </td>
                                                                    <td runat="server" id="spanFirstTimeOversea2" style="color: red; width: 1px; text-align: center;
                                                                        padding: 0 1px 5px 1px;">
                                                                        =
                                                                    </td>
                                                                    <td runat="server" id="spanFirstTimeOversea3" class="ui-panelgrid-cell spin-edit first-time"
                                                                        style="width: 1%; padding: 0px 2px; color: Red;">
                                                                        <dx:ASPxSpinEdit ID="spiFirstTimeOverseaVND" runat="server" ReadOnly="true" Width="100px"
                                                                            Style="position: relative; top: -1px; display: inline-block;" MinValue="0" MaxValue="1000000000000"
                                                                            NumberType="Float" NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}"
                                                                            Increment="100">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td runat="server" id="spanFirstTimeOversea4" class="ui-panelgrid-cell" style="width: 1px;
                                                                        padding: 0 2px 5px 3px; color: Red;">
                                                                        VND
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <%--List--
                                            <div class="ui-datatable ui-widget">
                                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                                    <ContentTemplate>
                                                        <div style="margin-top: 10px; border: 1px solid transparent">
                                                            <dx:ASPxGridView ID="grvBTRequest" runat="server" KeyFieldName="RequestID" Theme="Office2010Black"
                                                                AutoGenerateColumns="false" Style="margin-top: 0px" SettingsBehavior-AllowSort="false">
                                                                <SettingsText EmptyDataRow="No records found!" />
                                                                <Styles>
                                                                    <AlternatingRow Enabled="True">
                                                                    </AlternatingRow>
                                                                </Styles>
                                                                <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                                                <Columns>
                                                                    <dx:GridViewDataColumn Width="25px" Settings-AllowAutoFilter="False" FieldName="No"
                                                                        Caption="No" />
                                                                    <dx:GridViewBandColumn Caption="From">
                                                                        <Columns>
                                                                            <dx:GridViewDataDateColumn Width="90px" FieldName="FromDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}"
                                                                                CellStyle-HorizontalAlign="Center" />
                                                                            <dx:GridViewDataColumn Width="60px" FieldName="FromTime" Caption="Time" CellStyle-HorizontalAlign="Center" />
                                                                        </Columns>
                                                                    </dx:GridViewBandColumn>
                                                                    <dx:GridViewBandColumn Caption="To">
                                                                        <Columns>
                                                                            <dx:GridViewDataDateColumn Width="90px" FieldName="ToDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}"
                                                                                CellStyle-HorizontalAlign="Center" />
                                                                            <dx:GridViewDataColumn Width="60px" FieldName="ToTime" Caption="Time" CellStyle-HorizontalAlign="Center" />
                                                                        </Columns>
                                                                    </dx:GridViewBandColumn>
                                                                    <dx:GridViewDataTextColumn FieldName="EstimateFee" Width="100px" Caption="Estimate<br />Total Amount"
                                                                        CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                                    <dx:GridViewDataColumn FieldName="Destination" Caption="Destination" Width="120px" />
                                                                    <dx:GridViewDataColumn FieldName="Remark" Caption="Remark" />
                                                                    <dx:GridViewDataImageColumn Width="40px">
                                                                        <DataItemTemplate>
                                                                            <asp:Button ID="btnEdit" runat="server" CssClass='grid-btn viewDetails-btn' data-id='<%# Eval("RequestID") %>'
                                                                                OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))"
                                                                                OnClick="btnEditRequest_OnClick"></asp:Button>
                                                                        </DataItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </dx:GridViewDataImageColumn>
                                                                </Columns>
                                                                <Templates>
                                                                    <DetailRow>
                                                                        <fieldset style="width: 98%; padding: 0; border: none; margin: 10px 0;">
                                                                            <table class="tblRequestDetails grid-inside" style="table-layout: fixed">
                                                                                <tr>
                                                                                    <th rowspan="2" style="width: 80px">
                                                                                        Item
                                                                                    </th>
                                                                                    <th colspan="5">
                                                                                        Daily Allowance
                                                                                    </th>
                                                                                    <th rowspan="2">
                                                                                        Hotel
                                                                                    </th>
                                                                                    <th rowspan="2">
                                                                                        Other
                                                                                    </th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="text-align: center;">
                                                                                        Breakfast
                                                                                    </td>
                                                                                    <td style="text-align: center;">
                                                                                        Lunch
                                                                                    </td>
                                                                                    <td style="text-align: center;">
                                                                                        Dinner
                                                                                    </td>
                                                                                    <td style="text-align: center;">
                                                                                        Other
                                                                                    </td>
                                                                                    <th style="width: 100px; background-color: #eee !important;">
                                                                                        Total
                                                                                    </th>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="text-align: left">
                                                                                        Times
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("BreakfastQty", "{0:#,0.##}") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("LunchQty", "{0:#,0.##}") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("DinnerQty", "{0:#,0.##}") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%#Eval("OtherMealQty", "{0:#,0.##}")%>
                                                                                    </td>
                                                                                    <td style="background-color: #eee; text-align: right">
                                                                                        -
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("HotelQty", "{0:#,0.##}") %>
                                                                                    </td>
                                                                                    <td style="text-align: right">
                                                                                        -
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td style="text-align: left">
                                                                                        Unit
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("BreakfastUnit", "{0:#,0.##}") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("LunchUnit", "{0:#,0.##}") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("DinnerUnit", "{0:#,0.##}") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%#Eval("OtherMealUnit", "{0:#,0.##}")%>
                                                                                    </td>
                                                                                    <td style="background-color: #eee; text-align: right">
                                                                                        -
                                                                                    </td>
                                                                                    <td style="color: Red">
                                                                                        <%# Eval("HotelUnit", "{0:#,0.##}") %>
                                                                                    </td>
                                                                                    <td style="text-align: right">
                                                                                        -
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="total-amount">
                                                                                    <td style="text-align: left">
                                                                                        Amount
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("BreakfastAmount", "{0:#,0.##}") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("LunchAmount", "{0:#,0.##}") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("DinnerAmount", "{0:#,0.##}") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%#Eval("OtherMealAmount", "{0:#,0.##}")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%# Eval("TotalAmount", "{0:#,0.##}") %>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%#Eval("HotelAmount", "{0:#,0.##}")%>
                                                                                    </td>
                                                                                    <td>
                                                                                        <%#Eval("OtherUnit", "{0:#,0.##}")%>
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
                                            <%--Edit form--
                                            <fieldset class="add-edit-form" id="AdvanceForm">
                                                <legend>Advance Information</legend>
                                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                    <ContentTemplate>
                                                        <asp:HiddenField runat="server" ID="hRequestID" />
                                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="ui-panelgrid-cell" style="width: 140px">
                                                                        <label>
                                                                            Destination<span class="required">*</span></label>
                                                                    </td>
                                                                    <td class="ui-panelgrid-cell validate-required">
                                                                        <asp:DropDownList ID="ddlDestinationLocation" Enabled="false" AutoPostBack="true"
                                                                            onchange="HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))"
                                                                            runat="server">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="ui-panelgrid-cell" style="width: 120px;">
                                                                        <label>
                                                                            From Date<span class="required">*</span></label>
                                                                    </td>
                                                                    <td class="ui-panelgrid-cell date-time-picker validate-required">
                                                                        <dx:ASPxDateEdit ID="dteFromDate" ReadOnly="true" TabIndex="4" runat="server" EditFormat="Custom"
                                                                            DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm">
                                                                            <TimeSectionProperties Visible="true">
                                                                                <TimeEditProperties EditFormatString="HH:mm" />
                                                                            </TimeSectionProperties>
                                                                        </dx:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="ui-panelgrid-cell" style="width: 110px;">
                                                                        <label>
                                                                            To Date<span class="required">*</span></label>
                                                                    </td>
                                                                    <td role="gridcell" class="ui-panelgrid-cell date-time-picker validate-required">
                                                                        <dx:ASPxDateEdit ID="dteToDate" ReadOnly="true" TabIndex="5" runat="server" EditFormat="Custom"
                                                                            DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm">
                                                                            <TimeSectionProperties Visible="true">
                                                                                <TimeEditProperties EditFormatString="HH:mm" />
                                                                            </TimeSectionProperties>
                                                                        </dx:ASPxDateEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="ui-panelgrid-cell">
                                                                        <label>
                                                                            Request</label>
                                                                    </td>
                                                                    <td class="ui-panelgrid-cell" colspan="3">
                                                                        <table class="tblRequestDetails grid-inside grid-normal" style="table-layout: fixed">
                                                                            <tr>
                                                                                <th rowspan="2">
                                                                                    Item
                                                                                </th>
                                                                                <th colspan="5">
                                                                                    Daily Allowance
                                                                                </th>
                                                                                <th rowspan="2" style="">
                                                                                    Hotel
                                                                                </th>
                                                                            </tr>
                                                                            <tr>
                                                                                <td style="text-align: center; padding: 5px !important;">
                                                                                    Breakfast
                                                                                </td>
                                                                                <td style="text-align: center; padding: 5px !important;">
                                                                                    Lunch
                                                                                </td>
                                                                                <td style="text-align: center; padding: 5px !important;">
                                                                                    Dinner
                                                                                </td>
                                                                                <td style="text-align: center; padding: 5px !important;">
                                                                                    Other
                                                                                </td>
                                                                                <th style="background-color: #eee !important; padding: 5px !important;">
                                                                                    Total
                                                                                </th>
                                                                            </tr>
                                                                            <tr class="quantity">
                                                                                <td style="text-align: left; padding: 5px !important;">
                                                                                    Times
                                                                                </td>
                                                                                <td class="td-input" id="txtBreakfastQty" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                                <td class="td-input" id="txtLunchQty" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                                <td class="td-input" id="txtDinnerQty" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                                <td class="td-input" id="txtOtherMealQty" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                                <td style="background-color: #eee; text-align: right" style="padding: 5px !important;">
                                                                                    -
                                                                                </td>
                                                                                <td class="td-input" id="txtHotelQty" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="amount">
                                                                                <td style="text-align: left; padding: 5px !important;">
                                                                                    Unit
                                                                                </td>
                                                                                <td class="td-input" id="txtBreakfastUnit" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                                <td class="td-input" id="txtLunchUnit" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                                <td class="td-input" id="txtDinnerUnit" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                                <td class="td-input" id="txtOtherMealUnit" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                                <td style="background-color: #eee; text-align: right" style="padding: 5px !important;">
                                                                                    -
                                                                                </td>
                                                                                <td class="td-input" id="txtHotelUnit" runat="server" style="padding: 5px !important;
                                                                                    color: Red">
                                                                                </td>
                                                                            </tr>
                                                                            <tr class="total-amount">
                                                                                <td style="text-align: left; background-color: #eee; font-weight: bold; padding: 5px !important;">
                                                                                    Amount
                                                                                </td>
                                                                                <td class="td-input" id="txtBreakfastAmount" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                                <td class="td-input" id="txtLunchAmount" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                                <td class="td-input" id="txtDinnerAmount" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                                <td class="td-input" id="txtOtherMealAmount" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                                <td class="cal-total-amount td-input" id="txtTotalAmount" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                                <td class="td-input hotel-amount" id="txtHotelAmount" runat="server" style="padding: 5px !important;">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td role="gridcell" class="ui-panelgrid-cell" style="vertical-align: top; padding-top: 9px;">
                                                                        <label>
                                                                            Transportation &amp; Other</label>
                                                                    </td>
                                                                    <td class="ui-panelgrid-cell spin-edit" style="vertical-align: top;">
                                                                        <dx:ASPxSpinEdit TabIndex="15" ID="txtOtherAmount" ReadOnly="true" runat="server"
                                                                            Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float" NullText="0"
                                                                            DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" HorizontalAlign="Right">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <td role="gridcell" class="ui-panelgrid-cell" style="vertical-align: top; padding-top: 9px;">
                                                                        <label>
                                                                            Other Explanation</label>
                                                                    </td>
                                                                    <td class="ui-panelgrid-cell" style="vertical-align: top;">
                                                                        <asp:TextBox ID="txtOther" ReadOnly="true" TabIndex="18" Style="width: 250px !important"
                                                                            runat="server" placeholder="Transportation, airport tax, entertainment, etc"
                                                                            TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                                        <label>
                                                                            Remark</label>
                                                                    </td>
                                                                    <td class="ui-panelgrid-cell" colspan="3">
                                                                        <asp:TextBox ID="txaRemark" ReadOnly="true" TabIndex="19" runat="server" TextMode="MultiLine"
                                                                            Rows="3" Style="width: 97% !important"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="action-pan">
                                                    <div>
                                                        <input type="button" value="Cancel" onclick="btnCancelSub_Click(this);" class="btn secondary" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                        <%--Schedule--
                                        <div class="HRTab">
                                            <%--List--
                                            <div class="ui-datatable ui-widget">
                                                <asp:UpdatePanel ID="UpdatePanel120" runat="server">
                                                    <ContentTemplate>
                                                        <div style="margin-top: 10px; border: 1px solid transparent">
                                                            <dx:ASPxGridView ID="grvBTSchedule" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                                                AutoGenerateColumns="false" Style="margin-top: 0" SettingsBehavior-AllowSort="false">
                                                                <SettingsText EmptyDataRow="No records found!" />
                                                                <Styles>
                                                                    <AlternatingRow Enabled="True">
                                                                    </AlternatingRow>
                                                                </Styles>
                                                                <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                                                <Columns>
                                                                    <dx:GridViewDataColumn Width="25px" Settings-AllowAutoFilter="False" FieldName="No"
                                                                        Caption="No" />
                                                                    <dx:GridViewDataDateColumn FieldName="Date" Caption="Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                                    <dx:GridViewBandColumn Caption="Time">
                                                                        <Columns>
                                                                            <dx:GridViewDataColumn FieldName="FromTime" Caption="From" Width="60px">
                                                                            </dx:GridViewDataColumn>
                                                                            <dx:GridViewDataColumn FieldName="ToTime" Caption="To" Width="60px">
                                                                            </dx:GridViewDataColumn>
                                                                        </Columns>
                                                                    </dx:GridViewBandColumn>
                                                                    <dx:GridViewDataColumn FieldName="WorkingArea" Caption="Working Area" />
                                                                    <dx:GridViewDataColumn FieldName="Task" Caption="Task/Remark" />
                                                                    <dx:GridViewDataTextColumn FieldName="EstimateTransportationFee" Width="120px" Caption="Transportation Fee"
                                                                        CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                                    <%--<dx:GridViewDataColumn FieldName="Request" Caption="Request to GA" />--
                                                                    <dx:GridViewDataImageColumn Width="40px">
                                                                        <DataItemTemplate>
                                                                            <asp:Button ID="btnEdit" runat="server" CssClass='grid-btn viewDetails-btn' data-id='<%# Eval("ID") %>'
                                                                                OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))"
                                                                                OnClick="btnEditSchedule_OnClick"></asp:Button>
                                                                        </DataItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    </dx:GridViewDataImageColumn>
                                                                </Columns>
                                                            </dx:ASPxGridView>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <%--Edit form--
                                            <fieldset class="add-edit-form" id="ScheduleForm">
                                                <legend>Schedule Information</legend>
                                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                    <ContentTemplate>
                                                        <asp:HiddenField runat="server" ID="hScheduleID" />
                                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                                            <tbody>
                                                                <tr>
                                                                    <td class="ui-panelgrid-cell" style="width: 110px">
                                                                        <label>
                                                                            Date<span class="required">*</span></label>
                                                                    </td>
                                                                    <td class="ui-panelgrid-cell date-time-picker validate-required">
                                                                        <dx:ASPxDateEdit runat="server" ReadOnly="true" ID="dteScheduleDate" EditFormat="Custom"
                                                                            DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy">
                                                                        </dx:ASPxDateEdit>
                                                                    </td>
                                                                    <td class="ui-panelgrid-cell" style="width: 100px">
                                                                        Time
                                                                    </td>
                                                                    <td class="ui-panelgrid-cell date-time-picker time-picker schedule-time">
                                                                        <dx:ASPxTimeEdit ID="txeFromTime" ReadOnly="true" Style="width: 100px !important;
                                                                            display: inline-block" runat="server" DisplayFormatString="HH:mm" EditFormatString="HH:mm">
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dx:ASPxTimeEdit>
                                                                        <span style="position: relative; top: -10px; padding: 0 6px;">to</span>
                                                                        <dx:ASPxTimeEdit ID="txeToTime" ReadOnly="true" Style="width: 100px !important; display: inline-block"
                                                                            runat="server" DisplayFormatString="HH:mm" EditFormatString="HH:mm">
                                                                            <ValidationSettings ErrorDisplayMode="None" />
                                                                        </dx:ASPxTimeEdit>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="ui-panelgrid-cell">
                                                                        <label>
                                                                            Working Area<span class="required">*</span></label>
                                                                    </td>
                                                                    <td class="ui-panelgrid-cell validate-required">
                                                                        <asp:TextBox ID="txtWorkingArea" ReadOnly="true" runat="server" TextMode="MultiLine"
                                                                            Rows="3"></asp:TextBox>
                                                                    </td>
                                                                    <td class="ui-panelgrid-cell">
                                                                        <label>
                                                                            Task/Remark</label>
                                                                    </td>
                                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                                        <asp:TextBox ID="txtTask" ReadOnly="true" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="ui-panelgrid-cell">
                                                                        <label>
                                                                            Transportation Fee</label>
                                                                    </td>
                                                                    <td class="ui-panelgrid-cell spin-edit">
                                                                        <dx:ASPxSpinEdit ID="spiEstimateTransportationFee" ReadOnly="true" runat="server"
                                                                            Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float" NullText="0"
                                                                            DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                                        </dx:ASPxSpinEdit>
                                                                    </td>
                                                                    <%--<td class="ui-panelgrid-cell">
                                                                        <label>
                                                                            Request to GA</label>
                                                                    </td>
                                                                    <td class="ui-panelgrid-cell schedule-request">
                                                                        <asp:CheckBox runat="server" ID="chkAirTicket" Enabled="false" Text=" " CssClass="check-button" />
                                                                        <span style="position: relative; top: -4px; padding-right: 8px">Air Ticket</span>
                                                                        <asp:CheckBox runat="server" ID="chkTrainTicket" Enabled="false" Text=" " CssClass="check-button" />
                                                                        <span style="position: relative; top: -4px; padding-right: 8px;">Train Ticket</span>
                                                                        <asp:CheckBox runat="server" ID="chkCar" Text=" " Enabled="false" CssClass="check-button" />
                                                                        <span style="position: relative; top: -4px;">Car</span>
                                                                    </td>--
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div class="action-pan">
                                                    <div>
                                                        <input type="button" value="Cancel" onclick="btnCancelSub_Click(this);" class="btn secondary" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
            </div>
            <%--Others--%>
            <div class="tab-container">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                    role="tab">
                    <span class="ui-icon"></span>Others (Optional)</h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content default-hide hide"
                    role="tabpanel">
                    <div class="HRTabControl edit-form">
                        <div class="HRTabNav">
                            <ul>
                                <li>Attachment</li>
                                <li id="tabExpenseSchedule">Schedule</li>
                                <li>Air Ticket</li>
                                <li>Invoicing</li>
                            </ul>
                            <div style="clear: both;">
                            </div>
                        </div>
                        <%--BT Schedule Information--%>
                        <div class="HRTabList" style="padding: 0 1.2em 10px; border: 1px solid #ccc; border-top: none;">
                            <%--Expense Tab--%>
                            <div class="HRTab">
                                <div class="ui-datatable ui-widget">
                                    <div class="ui-datatable-tablewrapper">
                                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                            <ContentTemplate>
                                                <table class="grid tbl-attachment">
                                                    <tr>
                                                        <th style="width: 15px">
                                                            No
                                                        </th>
                                                        <th style="width: 160px">
                                                            Attachment Name
                                                        </th>
                                                        <th>
                                                            Attachment(s) Link
                                                        </th>
                                                        <th style="width: 120px" runat="server" id="tdUploadCaption">
                                                            Upload
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            1
                                                        </td>
                                                        <td>
                                                            Business Trip Expense
                                                        </td>
                                                        <td style="padding-bottom: 3px !important; position: relative">
                                                            <div style="position: absolute; top: -1px; left: -1px; width: 100%; height: 100%;
                                                                border: 1px solid transparent">
                                                                <table style="width: 100%; height: 100%; margin: 0;">
                                                                    <tr>
                                                                        <td style="border: none !important; padding: 0 10px !important;">
                                                                            <asp:Panel runat="server" ID="panExpenseAttachments" CssClass="pan-attachment-container">
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                        <td style="text-align: center; padding: 5px 10px !important" runat="server" id="tdUpload1">
                                                            <span id="Span1" style="text-align: center; font-weight: normal; padding-right: 14px;
                                                                display: block;" class="btn inform"><i class="add"></i><span>Choose file</span>
                                                                <input type="file" name="fSchedule" class="choose-file" data-type="expense" onchange="ChooseFile(this)" />
                                                            </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 12px 10px !important;">
                                                            2
                                                        </td>
                                                        <td>
                                                            Others
                                                        </td>
                                                        <td style="padding-bottom: 3px !important">
                                                            <asp:Panel runat="server" ID="panOtherExpenseAttachments" CssClass="pan-attachment-container">
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="text-align: center" runat="server" id="tdUpload2">
                                                            <span id="btnAddOthersAttachment" style="text-align: center; font-weight: normal;
                                                                padding-right: 8px; display: block;" class="btn inform"><i class="add"></i><span>Choose
                                                                    files</span>
                                                                <input type="file" name="fOthers" class="choose-file" data-type="other-expense" multiple="multiple"
                                                                    onchange="ChooseFile(this)" />
                                                            </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            Remark
                                                        </td>
                                                        <td colspan="2" style="padding: 0 !important;" runat="server" id="tdUploadDesc">
                                                            <asp:TextBox ID="txtExpenseDescription" placeholder="Input remark" runat="server"
                                                                TextMode="MultiLine" CssClass="full-fill"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <%--Schedule--%>
                            <div class="HRTab">
                                <span id="btnAddExpenseSchedule" style="text-align: center; float: left; margin-top: 10px;
                                    margin-bottom: 4px" class="btn inform add-btn" onclick="btnAddSub_Click(this)"><i
                                        class="add"></i>Add</span>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <span runat="server" id="spanBtnExportSchedule" style="text-align: center; font-weight: normal;
                                            padding: 6px 15px 5px 5px; float: left; margin-top: 10px; margin-bottom: 4px"
                                            class="btn inform export-btn"><i class="export"></i><span>Export Schedule</span>
                                            <asp:Button ID="BtnExportSchedule" runat="server" OnClientClick="bindStartupEvents(this); HandleMessage(this); GetExportFile(this)"
                                                Text="Export" Style="text-align: center;" />
                                        </span>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div style="clear: both">
                                </div>
                                <%--List--%>
                                <div class="ui-datatable ui-widget">
                                    <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                        <ContentTemplate>
                                            <div style="margin-top: 10px; border: 1px solid transparent">
                                                <dx:ASPxGridView ID="grvBTExpenseSchedule" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                                    AutoGenerateColumns="false" Style="margin-top: 0" SettingsBehavior-AllowSort="false">
                                                    <Settings ShowFooter="true" />
                                                    <SettingsText EmptyDataRow="No records found!" />
                                                    <Styles>
                                                        <AlternatingRow Enabled="True">
                                                        </AlternatingRow>
                                                    </Styles>
                                                    <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                                    <Columns>
                                                        <dx:GridViewDataColumn Width="25px" Settings-AllowAutoFilter="False" FieldName="No"
                                                            Caption="No" />
                                                        <dx:GridViewDataDateColumn FieldName="Date" Caption="Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                        <dx:GridViewBandColumn Caption="Time">
                                                            <Columns>
                                                                <dx:GridViewDataColumn FieldName="FromTime" Caption="From" Width="60px">
                                                                </dx:GridViewDataColumn>
                                                                <dx:GridViewDataColumn FieldName="ToTime" Caption="To" Width="60px">
                                                                </dx:GridViewDataColumn>
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewDataColumn FieldName="WorkingArea" Caption="Working Area" Width="150px" />
                                                        <dx:GridViewDataColumn FieldName="Task" Caption="Task/Remark" Width="150px" />
                                                        <dx:GridViewDataTextColumn FieldName="TransportationFee" Width="120px" Caption="Transportation Fee"
                                                            CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                        <dx:GridViewDataImageColumn Width="40px">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" CssClass='<%# If(Convert.ToBoolean(Eval("EnableForm")), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("ID") %>' OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))"
                                                                    OnClick="btnEditExpenseSchedule_OnClick"></asp:Button>
                                                                <input type="button" class="grid-btn delete-btn" title="Delete" runat="server" id="btnSubConfirmDelete"
                                                                    visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>' onclick="btnDeleteScheduleClick(this)" />
                                                                <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' Visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>'
                                                                    ToolTip="Delete" runat="server" CssClass="hide" OnClick="btnDeleteSchedule_OnClick"
                                                                    OnClientClick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))">
                                                                </asp:Button>
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataImageColumn>
                                                    </Columns>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="TransportationFee" DisplayFormat="{0:#,0.##}" SummaryType="Sum" />
                                                    </TotalSummary>
                                                </dx:ASPxGridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%--Edit form--%>
                                <fieldset class="add-edit-form" id="ExpenseScheduleForm">
                                    <legend><span class="add-edit-action"></span>&nbsp;Schedule</legend>
                                    <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                        <ContentTemplate>
                                            <asp:HiddenField runat="server" ID="hExpenseScheduleID" />
                                            <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                                <tbody>
                                                    <tr>
                                                        <td class="ui-panelgrid-cell" style="width: 110px">
                                                            <label>
                                                                Date<span class="required">*</span></label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell date-time-picker validate-required">
                                                            <dx:ASPxDateEdit runat="server" ID="dteExpenseScheduleDate" ClientInstanceName="dteExpenseScheduleDate"
                                                                EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy">
                                                            </dx:ASPxDateEdit>
                                                        </td>
                                                        <td class="ui-panelgrid-cell" style="width: 100px">
                                                            Time
                                                        </td>
                                                        <td class="ui-panelgrid-cell date-time-picker time-picker schedule-time">
                                                            <dx:ASPxTimeEdit ID="txeExpenseFromTime" ClientInstanceName="txeExpenseFromTime"
                                                                Style="width: 100px !important; display: inline-block" runat="server" DisplayFormatString="HH:mm"
                                                                EditFormatString="HH:mm">
                                                                <ValidationSettings ErrorDisplayMode="None" />
                                                            </dx:ASPxTimeEdit>
                                                            <span style="position: relative; top: -10px; padding: 0 6px;">to</span>
                                                            <dx:ASPxTimeEdit ID="txeExpenseToTime" ClientInstanceName="txeExpenseToTime" Style="width: 100px !important;
                                                                display: inline-block" runat="server" DisplayFormatString="HH:mm" EditFormatString="HH:mm">
                                                                <ValidationSettings ErrorDisplayMode="None" />
                                                            </dx:ASPxTimeEdit>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="ui-panelgrid-cell">
                                                            <label>
                                                                Working Area<span class="required">*</span></label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell validate-required">
                                                            <asp:TextBox ID="txtExpenseWorkingArea" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                        </td>
                                                        <td class="ui-panelgrid-cell">
                                                            <label>
                                                                Task/Remark</label>
                                                        </td>
                                                        <td role="gridcell" class="ui-panelgrid-cell">
                                                            <asp:TextBox ID="txtExpenseTask" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="ui-panelgrid-cell">
                                                            <label>
                                                                Transportation Fee</label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell spin-edit">
                                                            <dx:ASPxSpinEdit ID="spiTransportationFee" ClientInstanceName="spiTransportationFee"
                                                                runat="server" Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float"
                                                                NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSaveSchedule" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <ul id="ExpenseScheduleSummary" class="error-summary">
                                    </ul>
                                    <div class="action-pan">
                                        <asp:UpdatePanel ID="UpdatePanel77" runat="server">
                                            <ContentTemplate>
                                                <span runat="server" id="btnShowSaveSchedule">
                                                    <input type="button" class="btn" value="Save" onclick="ValidateScheduleForm(this)" />
                                                </span>
                                                <asp:Button runat="server" ID="btnSaveSchedule" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                                <input type="button" id="btnCancelSchedule" value="Cancel" class="btn secondary btn-cancel-sub"
                                                    onclick="btnCancelSub_Click(this); ClearScheduleForm()" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </fieldset>
                            </div>
                            <%--Air ticket--%>
                            <div class="HRTab" style="margin-bottom: 10px;">
                                <span id="btnAddAirTicket" style="margin-top: 10px; text-align: center;" class="btn inform add-btn add-btn-air"
                                    onclick="btnAddSub_Click(this)"><i class="add"></i>Add</span>
                                <%--List--%>
                                <div class="ui-datatable ui-widget">
                                    <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                        <ContentTemplate>
                                            <div style="margin-top: 10px; border: 1px solid transparent">
                                                <dx:ASPxGridView ID="grvBTAirTicket" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                                    AutoGenerateColumns="false" Style="margin-top: 0" SettingsBehavior-AllowSort="false">
                                                    <SettingsText EmptyDataRow="No records found!" />
                                                    <Styles>
                                                        <AlternatingRow Enabled="True">
                                                        </AlternatingRow>
                                                    </Styles>
                                                    <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                                    <Columns>
                                                        <dx:GridViewDataColumn FieldName="No" Caption="No" Width="25px" />
                                                        <dx:GridViewDataDateColumn FieldName="TicketDate" Caption="Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                        <dx:GridViewDataColumn FieldName="AirLine" Caption="Airline" />
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
                                                        <dx:GridViewDataImageColumn Width="40px">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" CssClass='<%# If(Convert.ToBoolean(Eval("EnableForm")) AndAlso Convert.ToBoolean(Eval("AirEnable")), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("ID") %>' OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))"
                                                                    OnClick="btnEditAirTicket_OnClick"></asp:Button>
                                                                <input type="button" class="grid-btn delete-btn" title="Delete" runat="server" id="btnSubConfirmDelete"
                                                                    visible='<%# Convert.ToBoolean(Eval("EnableForm")) AndAlso Convert.ToBoolean(Eval("AirEnable"))%>'
                                                                    onclick="btnDeleteAirTicketClick(this)" />
                                                                <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' Visible='<%# Convert.ToBoolean(Eval("EnableForm")) AndAlso Convert.ToBoolean(Eval("AirEnable"))%>'
                                                                    ToolTip="Delete" runat="server" CssClass="hide" OnClick="btnDeleteAirTicket_OnClick"
                                                                    OnClientClick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))">
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
                                                </dx:ASPxGridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%--Edit form--%>
                                <fieldset class="add-edit-form" id="AirTicketForm">
                                    <legend><span class="add-edit-action"></span>&nbsp;Air Ticket</legend>
                                    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
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
                                                            <dx:ASPxDateEdit ID="dteAirDate" runat="server" ClientSideEvents-DateChanged="AirTicketDateChange"
                                                                EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy">
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
                                                            <asp:Label runat="server" ID="lblAirExrate" Style="font-weight: normal"></asp:Label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell spin-edit exrate-value" data-exrate="air-exrate">
                                                            <dx:ASPxSpinEdit ID="spiAirExrate" runat="server" Height="21px" MinValue="1" MaxValue="1000000000000"
                                                                NumberType="Float" NullText="1" HorizontalAlign="Right" DecimalPlaces="6" DisplayFormatString="{0:#,0.##}">
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
                                        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                            <ContentTemplate>
                                                <span runat="server" id="btnShowSaveAirTicket">
                                                    <input type="button" class="btn" value="Save" onclick="ValidateAirTicketForm(this)" />
                                                </span>
                                                <asp:Button runat="server" ID="btnSaveAirTicket" Text="Save" CssClass="btn hide"
                                                    OnClientClick="HandleMessage(this); HandlePartialMessageBoard(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                                <asp:Button runat="server" ID="btnCancelAirTicket" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                    OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </fieldset>
                            </div>
                            <%--Invoicing--%>
                            <div class="HRTab" style="margin-bottom: 10px;">
                                <span id="btnAddInvoice" style="margin-top: 10px; text-align: center;" class="btn inform add-btn add-btn-invoice"
                                    onclick="btnAddSub_Click(this)"><i class="add"></i>Add</span>
                                <%--List--%>
                                <div class="ui-datatable ui-widget">
                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                        <ContentTemplate>
                                            <div style="margin-top: 10px; border: 1px solid transparent">
                                                <dx:ASPxGridView ID="grvBTInvoice" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                                    AutoGenerateColumns="false" Style="margin-top: 0" SettingsBehavior-AllowSort="false">
                                                    <SettingsText EmptyDataRow="No records found!" />
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
                                                        <dx:GridViewDataColumn FieldName="STT" Caption="STT" Width="80px" />
                                                        <dx:GridViewBandColumn Caption="Invoice Information">
                                                            <Columns>
                                                                <dx:GridViewDataColumn FieldName="SerialNo" Caption="Serial No" Width="90px" />
                                                                <dx:GridViewDataColumn FieldName="InvNo" Caption="Invoice No" Width="90px" />
                                                                <dx:GridViewDataDateColumn FieldName="InvDate" Caption="Invoice Date" Width="90px"
                                                                    PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewDataColumn FieldName="Item" Caption="Item" />
                                                        <dx:GridViewDataTextColumn FieldName="NetCost" Width="90px" Caption="Net Cost" CellStyle-HorizontalAlign="Right"
                                                            PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                        <dx:GridViewDataTextColumn FieldName="VAT" Width="90px" Caption="VAT" CellStyle-HorizontalAlign="Right"
                                                            PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                        <dx:GridViewDataTextColumn FieldName="TotalAmountFormated" Width="110px" Caption="Total"
                                                            CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}"
                                                            CellStyle-BackColor="#EEEEEE" />
                                                        <dx:GridViewDataImageColumn Width="40px">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" CssClass='<%# If(Convert.ToBoolean(Eval("EnableInvForm")), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("ID") %>' OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))"
                                                                    OnClick="btnEditInvoice_OnClick"></asp:Button>
                                                                <input type="button" class="grid-btn delete-btn" title="Delete" runat="server" id="btnSubConfirmDelete"
                                                                    visible='<%# Convert.ToBoolean(Eval("EnableInvForm"))%>' onclick="btnDeleteInvoiceClick(this)" />
                                                                <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' Visible='<%# Convert.ToBoolean(Eval("EnableInvForm"))%>'
                                                                    ToolTip="Delete" runat="server" CssClass="hide" OnClientClick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))"
                                                                    OnClick="btnDeleteInvoice_OnClick"></asp:Button>
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
                                                                            Seller Name
                                                                        </th>
                                                                        <th>
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
                                    <asp:UpdatePanel ID="UpdatePanel29" runat="server">
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
                                                                float: left; width: 171px !important;"></asp:TextBox>
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
                                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                            <ContentTemplate>
                                                <span runat="server" id="btnShowSaveInvoice">
                                                    <input type="button" class="btn" value="Save" onclick="ValidateInvoiceForm(this)" />
                                                </span>
                                                <asp:Button runat="server" ID="btnSaveInvoice" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                                <asp:Button runat="server" ID="btnCancelInvoice" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                    OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </fieldset>
                            </div>
                            <%--Advance Tab--%>
                            <%--<div class="HRTab">
                                <div class="ui-datatable ui-widget">
                                    <div class="ui-datatable-tablewrapper">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <table class="grid tbl-attachment">
                                                    <tr>
                                                        <th style="width: 15px">
                                                            No
                                                        </th>
                                                        <th style="width: 160px">
                                                            Attachment Name
                                                        </th>
                                                        <th>
                                                            Attachment(s) Link
                                                        </th>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 12px 10px !important;">
                                                            1
                                                        </td>
                                                        <td>
                                                            Business Trip Approval
                                                        </td>
                                                        <td style="padding-bottom: 3px !important; position: relative">
                                                            <div style="position: absolute; top: -1px; left: -1px; width: 100%; height: 100%;
                                                                border: 1px solid transparent">
                                                                <table style="width: 100%; height: 100%; margin: 0;">
                                                                    <tr>
                                                                        <td style="border: none !important; padding: 0 10px !important;">
                                                                            <asp:Panel runat="server" ID="panRegisterAttachments" CssClass="pan-attachment-container">
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 12px 10px !important;">
                                                            2
                                                        </td>
                                                        <td>
                                                            Business Trip Schedule
                                                        </td>
                                                        <td style="padding-bottom: 3px !important; position: relative">
                                                            <div style="position: absolute; top: -1px; left: -1px; width: 100%; height: 100%;
                                                                border: 1px solid transparent">
                                                                <table style="width: 100%; height: 100%; margin: 0;">
                                                                    <tr>
                                                                        <td style="border: none !important; padding: 0 10px !important;">
                                                                            <asp:Panel runat="server" ID="panScheduleAttachments" CssClass="pan-attachment-container">
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 12px 10px !important;">
                                                            3
                                                        </td>
                                                        <td>
                                                            Others
                                                        </td>
                                                        <td style="padding-bottom: 3px !important">
                                                            <asp:Panel runat="server" ID="panOthersAttachments" CssClass="pan-attachment-container">
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            Remark
                                                        </td>
                                                        <td style="padding: 0 !important;">
                                                            <asp:TextBox ID="txtDescription" ReadOnly="true" runat="server" TextMode="MultiLine"
                                                                CssClass="full-fill"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
    <%--Action buttons--%>
    <div id="back-container" class="action-pan no-transition" style="display: none;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <span runat="server" id="spanExportBT" style="text-align: center; font-weight: normal;
                    padding: 6px 15px 5px 5px; display: block; float: right; margin-bottom: 10px;"
                    class="btn inform export-btn"><i class="export"></i><span>Export Request for Payment</span>
                    <asp:Button ID="btnExportBT" runat="server" OnClientClick="bindStartupEvents(this); HandleMessage(this); GetExportFile(this)"
                        Text="Export" Style="text-align: center;" />
                </span><span runat="server" id="spanExportSchedule1" style="text-align: center; font-weight: normal;
                    padding: 6px 15px 5px 5px; float: right; margin-right: 5px; margin-bottom: 10px"
                    class="btn inform export-btn"><i class="export"></i><span>Export Schedule</span>
                    <asp:Button ID="btnExportSchedule1" runat="server" OnClientClick="bindStartupEvents(this); HandleMessage(this); GetExportFile(this)"
                        Text="Export" Style="text-align: center;" />
                </span>
                <div style="clear: both;">
                </div>
                <span runat="server" id="btnShowSave" class="btn" style="padding-left: 6px;"><i class="save">
                </i><span>Save</span>
                    <asp:Button ID="btnFinish" runat="server" Text="Save" Style="text-align: center;"
                        CssClass="btn" OnClientClick="hidePartialForms(); $('#totalErrorSummary').hide(); bindStartupEvents(this); HandleMessage(this);" /></span>
                <span runat="server" id="btnShowSubmit" class="btn" style="padding-left: 6px;"><i
                    class="submit"></i><span>Submit</span>
                    <input type="button" value="Submit" class="btn" onclick="hidePartialForms(); ValidateSubmit(true)" /></span>
                <%--Approve/Reject--%>
                <span runat="server" id="btnShowApprove" visible="false" class="btn" style="padding-left: 6px;
                    padding-right: 20px;"><i class="approval-btn"></i><span style="padding-left: 10px"
                        runat="server" id="spanApproveLabel">Approve</span>
                    <input type="button" value="Approve" class="btn" onclick="btnApproveBTClick()" /></span>
                <asp:Button runat="server" ID="btnCheckExtInvoice" CssClass="hide" OnClientClick="HandleMessage(this); bindStartupEvents(this);" />
                <span runat="server" id="btnShowReject" visible="false" class="btn secondary" style="padding-left: 6px;">
                    <i class="reject-btn"></i><span>Reject</span>
                    <input type="button" value="Reject" class="btn" onclick="CheckOraStatus()" /></span>
                <%----%>
                <span runat="server" id="btnConfirmRecall" visible="false" class="btn attention"
                    style="padding-left: 6px;"><i class="recall"></i><span>Recall</span>
                    <input type="button" onclick="ConfirmRecall()" />
                </span>
                <asp:Button ID="btnRecall" runat="server" Visible="false" Text="Recall" Style="text-align: center;"
                    CssClass="btn attention hide" OnClientClick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]);" />
                <%----%>
                <span class="btn secondary" style="padding-left: 6px;"><i class="back"></i><span>Back</span>
                    <input type="button" id="btnConfirmCancel" onclick="ConfirmCancel(<%= If(_enable, "true", "false") %>)" />
                </span>
                <asp:Button ID="btnCancel" runat="server" Text="Back" Style="text-align: center;"
                    CssClass="btn secondary hide" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="clear: both;">
    </div>
    <%--Reject BT--%>
    <asp:UpdatePanel ID="UpdatePanel34" runat="server">
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
                                            Reject to Requester to update information</h3>
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
                                            CssClass="btn" OnClientClick="if(!checkReject()) return false; bindStartupEvents($('[id$=btnCancel]')[0]); HandleMessage($('[id$=btnCancel]')[0]);" />
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
    <%--Approve BT--%>
    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
            <div id="tabApproveMessage" runat="server" class="popup-container">
                <input type="button" id="btnInvoiceDateChange" onclick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]);"
                    class="hide" />
                <table style="width: 100%; height: 100%; margin: 0;">
                    <tr>
                        <td style="vertical-align: middle; border: none !important; padding: 0 !important;">
                            <table style="width: 100%; height: 100%; margin: 0;">
                                <tr>
                                    <td style="vertical-align: middle; border: none !important; padding: 0 !important;">
                                        <table class="grid-edit" style="width: auto !important; margin: auto; background-color: #fff;
                                            border-radius: 5px; box-shadow: 0 0 10px #fff; -webkit-box-shadow: 0 0 10px #fff;
                                            -moz-box-shadow: 0 0 10px #fff;">
                                            <tr>
                                                <td style="padding: 5px 30px 15px;" colspan="2">
                                                    <h3 style="margin: 0; padding: 5px 0 3px; background-color: #fff; border-bottom: 1px solid #999;">
                                                        Approve BT Expense Declaration (Invoice)</h3>
                                                </td>
                                            </tr>
                                            <tr>
                                                <asp:HiddenField runat="server" ID="hApproveMessage" />
                                                <td style="padding: 0px 30px 5px; color: red" id="approveMessage" runat="server"
                                                    colspan="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px 5px 0 30px;" class="date-time-picker">
                                                    <h4 style="margin: 0; padding: 5px 0 3px; background-color: #fff; text-decoration: underline;
                                                        border: none;">
                                                        Invoice Date</h4>
                                                    <dx:ASPxDateEdit ID="dteInvoiceDate" runat="server" AutoPostBack="true" ClientSideEvents-DateChanged="InvoiceDateChanged"
                                                        EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy">
                                                    </dx:ASPxDateEdit>
                                                </td>
                                                <td style="padding: 5px 30px 0 5px;" class="date-time-picker">
                                                    <h4 style="margin: 0; padding: 5px 0 3px; background-color: #fff; text-decoration: underline;
                                                        border: none;">
                                                        GL Date</h4>
                                                    <dx:ASPxDateEdit ID="dteGLDate" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                                        EditFormatString="dd-MMM-yyyy">
                                                    </dx:ASPxDateEdit>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px 5px 0 30px;">
                                                    <h4 style="margin: 0; padding: 4px 0 3px; background-color: #fff; text-decoration: underline;
                                                        border: none;">
                                                        Batch Name</h4>
                                                    <asp:DropDownList runat="server" ID="ddlBatchName">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px 30px 20px;" colspan="2">
                                                    <h4 style="margin: 0; padding: 5px 0 3px; background-color: #fff; text-decoration: underline;
                                                        border: none;">
                                                        Payment Method</h4>
                                                    <div>
                                                        <asp:RadioButton runat="server" ID="radCash" Checked="true" Text=" " CssClass="radio-button"
                                                            GroupName="PM" />
                                                        <span style="position: relative; top: -3px;">Cash (Check)</span>
                                                        <asp:RadioButton runat="server" ID="radBankTransfer" Text=" " CssClass="radio-button"
                                                            GroupName="PM" Style="margin-left: 15px;" />
                                                        <span style="position: relative; top: -3px;">Bank Transfer (Electronic)</span>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trExtensionInvoice" visible="false">
                                                <td style="padding: 10px 5px 20px 30px; position: relative; border-top: 1px solid #ccc !important;
                                                    border-bottom: 1px solid #ccc !important;" class="spin-edit">
                                                    <span style="position: relative; top: -18px; left: -5px; background-color: #fff;
                                                        padding: 0 5px; font-weight: bold;">Extension Invoice Information</span>
                                                    <h4 style="margin: -14px 0 0; padding: 5px 0 3px; background-color: #fff; text-decoration: underline;
                                                        border: none;">
                                                        Amount (USD)</h4>
                                                    <dx:ASPxSpinEdit ID="spiExtensionAmount" runat="server" MinValue="0" MaxValue="1000000000000"
                                                        NumberType="Float" NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}"
                                                        Increment="1">
                                                    </dx:ASPxSpinEdit>
                                                </td>
                                                <td style="padding: 10px 30px 20px 5px; border-top: 1px solid #ccc !important; border-bottom: 1px solid #ccc !important;"
                                                    class="spin-edit">
                                                    <h4 style="margin: 0; padding: 5px 0 3px; background-color: #fff; text-decoration: underline;
                                                        border: none;">
                                                        Exchange Rate (USD &rarr; VND)</h4>
                                                    <dx:ASPxSpinEdit ID="spiExtensionExrate" runat="server" MinValue="1" MaxValue="1000000000000"
                                                        NumberType="Float" NullText="0" DisplayFormatString="{0:#,0.##}" Increment="100">
                                                    </dx:ASPxSpinEdit>
                                                </td>
                                            </tr>
                                            <tr runat="server" id="trCreditInvoice" visible="false">
                                                <td style="padding: 10px 5px 15px 30px; position: relative; border-top: 1px solid #ccc !important;
                                                    border-bottom: 1px solid #ccc !important;" class="spin-edit">
                                                    <span style="position: relative; top: -18px; left: -5px; background-color: #fff;
                                                        padding: 0 5px; font-weight: bold;">Credit Invoice Information</span>
                                                    <h4 style="margin: -14px 0 0; padding: 5px 0 3px; background-color: #fff; text-decoration: underline;
                                                        border: none;">
                                                        Amount (USD)</h4>
                                                    <dx:ASPxSpinEdit ID="spiCreditInvoiceAmount" ReadOnly="true" runat="server" MinValue="0"
                                                        MaxValue="1000000000000" NumberType="Float" NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}"
                                                        Increment="1">
                                                    </dx:ASPxSpinEdit>
                                                </td>
                                                <td style="padding: 10px 30px 15px 5px; border-top: 1px solid #ccc !important; border-bottom: 1px solid #ccc !important;"
                                                    class="spin-edit">
                                                    <h4 style="margin: 0; padding: 5px 0 3px; background-color: #fff; text-decoration: underline;
                                                        border: none;">
                                                        Exchange Rate (USD &rarr; VND)</h4>
                                                    <dx:ASPxSpinEdit ID="spiCreditInvoiceExrate" runat="server" MinValue="1" MaxValue="1000000000000"
                                                        NumberType="Float" NullText="0" DisplayFormatString="{0:#,0.##}" Increment="100">
                                                    </dx:ASPxSpinEdit>
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
                                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" Style="text-align: center;"
                                                        CssClass="btn" OnClientClick="if(!checkApprove()) return false; bindStartupEvents($('[id$=btnCancel]')[0]); HandleMessage($('[id$=btnCancel]')[0]);" />
                                                    <asp:Button runat="server" Text="Cancel" OnClientClick="hideApproveMessage(); bindStartupEvents($('[id$=btnCancel]')[0]); HandleMessage($('[id$=btnCancel]')[0]);"
                                                        Style="margin-left: 5px;" ID="btnApproveCancel" class="btn secondary" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Submit BT--%>
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
                                            Submit to Finance Accounting to check/process</h3>
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
                                            CssClass="btn" OnClientClick="bindStartupEvents($('[id$=btnCancel]')[0]); HandleMessage($('[id$=btnCancel]')[0]); DoCheckEnableForm()" />
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
    <%--View Error--%>
    <div id="oraErrorContainer" class="no-transition" style="display: none; position: absolute;
        z-index: 1001; background-color: #fff; padding: 10px 20px; border: 1px solid red;
        border-radius: 5px; color: Red; box-shadow: 0 1px 10px #aaa; -webkit-box-shadow: 0 1px 10px #999;
        -moz-box-shadow: 0 1px 10px #aaa; border-radius: 5px; -webkit-border-radius: 5px;
        -moz-border-radius: 5px; line-height: 1.5;">
        <div id="oraErrorDetails" style="max-width: 400px;">
        </div>
        <i style="position: absolute; width: 10px; height: 10px; bottom: -10px; right: 108px;
            background: url(/images/triangle.png) center center no-repeat transparent;">
        </i>
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">

    <script src="/js/jquery/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">                
        var  _authorizedAccounts = <%= GetAuthorizedAccounts() %>   
        var id = '<%= Request.QueryString("id") %>'
        var view = id.trim().length > 0
        if(view){
            $("[id$=UpdateProgress]").show()
        }
        
        $(window).load(function() {
            ViewByID()
        })   
        
        function ViewByID() {
            if (id){
                var $btnView = $('.grid-btn.viewDetails-btn[data-id=' + id + '], .grid-btn.edit-btn[data-id=' + id + ']')
                if (!$btnView.size()) {
                    ShowErrorMessage("Access denied! System will redirect in 5 seconds...")
                    setTimeout("$('[id$=btnCancel]').click()", 5000)
                }
                else {
                    $(".btn:not(.secondary)").hide()
                    $btnView.click()
                }
            }
        }                    
    </script>

    <script src="/js/bt-expense-declaration.js" type="text/javascript"></script>

</asp:Content>
