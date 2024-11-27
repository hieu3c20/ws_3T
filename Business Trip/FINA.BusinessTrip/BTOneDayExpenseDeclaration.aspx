<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BTOneDayExpenseDeclaration.aspx.vb"
    Inherits="BTOneDayExpenseDeclaration" MasterPageFile="~/MasterPage.Master" %>

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

        function BTDateChanged() {
            //$("#btnSetMealAmount").click()
            CalculateRequest()
        }

        //        function OtherDateChange() {
        //            $('[id$=ddlExpenseOtherCurrency]').change()
        //        }

        function DepartureDateChanged() {
            $("[id$=btnDepartureDateChange]").click()
        }
    </script>

</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    One Day Trip Expense Management
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
                            BTS Status: <a id="lnkFINStatus" runat="server" href="#" rel="show-more" onclick="return ShowHistory('FI')">
                            </a>
                            <asp:Label runat="server" ID="lblFINComment" Style="font-weight: normal;"></asp:Label>
                        </td>
                        <td style="font-weight: bold;" runat="server" id="tdOraStatus" visible="false">
                            <span style="padding: 0px 10px">|</span>Oracle Status: <a id="lnkOraStatus" runat="server"
                                href="#" onclick="showErrorOraMessage(this); return false"></a>
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
                                <dx:GridViewDataDateColumn Width="130px" FieldName="CreatedDate" Caption="Create Date"
                                    PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy HH:mm}" />
                                <dx:GridViewDataColumn Width="180px" FieldName="FullName" Caption="Created By" />
                                <dx:GridViewBandColumn Caption="Status">
                                    <Columns>
                                        <dx:GridViewDataColumn Width="110px" FieldName="FromStatus" Caption="From" />
                                        <dx:GridViewDataColumn Width="110px" FieldName="ToStatus" Caption="To" />
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
                            <asp:CheckBox runat="server" Enabled="false" ID="chkCredit" Text=" " CssClass="check-button"
                                Style="top: 2px;" />
                        </td>
                        <td style="padding: 0px 0px 0px 20px;">
                            <span style="position: relative; top: 1px; font-weight: bold; color: red">Currency</span>
                            <asp:DropDownList ID="ddlCurrency" Enabled="false" AutoPostBack="true" onchange="HandleMessage(this); bindStartupEvents(this)"
                                runat="server" Style="padding: 3px 5px !important; width: auto !important">
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
                        <asp:HiddenField runat="server" ID="hIsDGMAndAbove" />
                        <asp:HiddenField runat="server" ID="hIsGMAndAbove" />
                        <asp:HiddenField ID="hIsOneDayExpense" runat="server" />
                        <asp:HiddenField ID="hDeleteUsers" runat="server" />
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
                                <tr runat="server" id="trGeneral1" class="general-info">
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
                                        <asp:HiddenField runat="server" ID="hLocationID" />
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Division</label>
                                    </td>
                                    <td role="gridcell" class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtDivision" runat="server" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hDivisionID" />
                                    </td>
                                </tr>
                                <tr runat="server" id="trGeneral3" class="general-info">
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Department</label>
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtDepartment" runat="server" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hDepartmentID" />
                                    </td>
                                    <td class="ui-panelgrid-cell">
                                        <label>
                                            Section</label>
                                    </td>
                                    <td role="gridcell" class="ui-panelgrid-cell">
                                        <asp:TextBox ID="txtSection" runat="server" ReadOnly="true"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hSectionID" />
                                        <asp:HiddenField runat="server" ID="hGroup" />
                                        <asp:HiddenField runat="server" ID="hGroupID" />
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
                                        <asp:DropDownList ID="ddlBudgetName" runat="server" onchange="BudgetCodeChange(this, 'txtBudgetCode'); HandleMessage(this); bindStartupEvents(this);"
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
                                    <div class="HRTab allow-delete">
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
                                                        <dx:GridViewDataColumn Width="20px" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <input type="checkbox" class="chkAll" onchange="CheckAll(this)" />
                                                            </HeaderTemplate>
                                                            <DataItemTemplate>
                                                                <input type="checkbox" data-id='<%# Eval("BTRegisterID") %>' name="chkSelectDeleteUser"
                                                                    class="chkSelect <%# If(Provider.CommonFunction._ToInt(Eval("IsOneDayExpense")) < 1, "hide", "") %>"
                                                                    onchange="CheckboxChecked(this)" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>
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
                                                        <dx:GridViewDataImageColumn Width="40px" CellStyle-HorizontalAlign="Left">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" ToolTip="View/Edit" CssClass='<%# If(Eval("IsOwner").ToString() = "1", "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnEditBTClick(this); HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0])"
                                                                    OnClick="btnEdit_OnClick"></asp:Button>
                                                                <input type="button" runat="server" id="btnShowDelete" visible='<%# Provider.CommonFunction._ToInt(Eval("IsOneDayExpense")) = 1 AndAlso Eval("IsOwner").ToString() = "1" %>'
                                                                    class="grid-btn delete-btn" title="Delete" data-id='<%# Eval("BTRegisterID") %>'
                                                                    onclick="btnDeleteBTClick(this)" />
                                                                <asp:Button ID="btnDelete" Visible='<%# Provider.CommonFunction._ToInt(Eval("IsOneDayExpense")) = 1 AndAlso Eval("IsOwner").ToString() = "1" %>'
                                                                    data-id='<%# Eval("BTRegisterID") %>' ToolTip="Delete" runat="server" CssClass="hide"
                                                                    OnClick="btnDelete_OnClick" OnClientClick="HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0])">
                                                                </asp:Button>
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
                                        <div class="bt-legend" style="float: right; padding: 5px 0 10px;">
                                            <strong>BTS Legend:</strong> <span class="ora-done" style="padding: 0 5px; border-right: 1px solid #ccc;">
                                                Pending</span> <span class="waiting" style="padding: 0 5px;">Confirm</span>
                                        </div>
                                        <div style="clear: both">
                                        </div>
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
                                                        <dx:GridViewDataColumn Width="20px" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <input type="checkbox" class="chkAll" onchange="CheckAll(this)" />
                                                            </HeaderTemplate>
                                                            <DataItemTemplate>
                                                                <input type="checkbox" data-id='<%# Eval("BTRegisterID") %>' name="chkSelectDeleteUser"
                                                                    class="chkSelect <%# If(Provider.CommonFunction._ToInt(Eval("IsOneDayExpense")) < 1, "hide", "") %>"
                                                                    onchange="CheckboxChecked(this)" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>
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
                                                                <asp:Button ID="btnEdit" runat="server" ToolTip="View/Edit" CssClass='<%# If(Eval("IsOwner").ToString() = "1" AndAlso (Eval("FIStatus").ToString() = FIStatus.rejected.ToString() OrElse Eval("FIStatus").ToString() = FIStatus.budget_rejected.ToString()), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnEditBTClick(this); HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0])"
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
                                    <div class="HRTab allow-delete">
                                        <%-- <div class="bt-legend" style="float: right; padding: 5px 0 10px;">
                                            <strong>BTS Legend:</strong> <span class="ora-rejected" style="padding: 0 5px">Rejected</span>
                                        </div>
                                        <div style="clear: both">
                                        </div>--%>
                                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
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
                                                        <dx:GridViewDataColumn Width="20px" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <input type="checkbox" class="chkAll" onchange="CheckAll(this)" />
                                                            </HeaderTemplate>
                                                            <DataItemTemplate>
                                                                <input type="checkbox" data-id='<%# Eval("BTRegisterID") %>' name="chkSelectDeleteUser"
                                                                    class="chkSelect <%# If(Provider.CommonFunction._ToInt(Eval("IsOneDayExpense")) < 1, "hide", "") %>"
                                                                    onchange="CheckboxChecked(this)" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>
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
                                                        <dx:GridViewDataImageColumn Width="40px" CellStyle-HorizontalAlign="Left">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" ToolTip="View/Edit" CssClass='<%# If(Eval("IsOwner").ToString() = "1" AndAlso (Eval("FIStatus").ToString() = FIStatus.rejected.ToString() OrElse Eval("FIStatus").ToString() = FIStatus.budget_rejected.ToString()), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnEditBTClick(this); HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0])"
                                                                    OnClick="btnEdit_OnClick"></asp:Button>
                                                                <input type="button" runat="server" id="btnShowDelete" visible='<%# Provider.CommonFunction._ToInt(Eval("IsOneDayExpense")) = 1 AndAlso Eval("IsOwner").ToString() = "1" %>'
                                                                    class="grid-btn delete-btn" title="Delete" data-id='<%# Eval("BTRegisterID") %>'
                                                                    onclick="btnDeleteBTClick(this)" />
                                                                <asp:Button ID="btnDelete" Visible='<%# Provider.CommonFunction._ToInt(Eval("IsOneDayExpense")) = 1 AndAlso Eval("IsOwner").ToString() = "1" %>'
                                                                    data-id='<%# Eval("BTRegisterID") %>' ToolTip="Delete" runat="server" CssClass="hide"
                                                                    OnClick="btnDelete_OnClick" OnClientClick="HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0])">
                                                                </asp:Button>
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
                                                        <dx:GridViewDataColumn Width="20px" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <input type="checkbox" class="chkAll" onchange="CheckAll(this)" />
                                                            </HeaderTemplate>
                                                            <DataItemTemplate>
                                                                <input type="checkbox" data-id='<%# Eval("BTRegisterID") %>' name="chkSelectDeleteUser"
                                                                    class="chkSelect <%# If(Provider.CommonFunction._ToInt(Eval("IsOneDayExpense")) < 1, "hide", "") %>"
                                                                    onchange="CheckboxChecked(this)" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>
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
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnEditBTClick(this); HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0])"
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
            <%--Advance Request--%>
            <div class="tab-container">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top active"
                    role="tab">
                    <span class="ui-icon"></span>Expense Declaration</h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content" role="tabpanel"
                    style="position: relative">
                    <%--Expense Tab--%>
                    <div class="HRTab">
                        <%--Summary--%>
                        <div class="ui-datatable ui-widget">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <table style="margin: 0; background-color: #f9f9f9;" table-layout="fixed">
                                            <tr>
                                                <th style="border: 1px solid #ccc; padding: 5px 5px" colspan="4">
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
                                                <%--<td style="border: 1px solid #ccc; padding: 5px 5px; text-align: center">
                                                Hotel<br />
                                                (A2)
                                            </td>--%>
                                                <td style="border: 1px solid #ccc; padding: 5px 5px; text-align: center">
                                                    Moving Time Allowance<br />
                                                    (A2)
                                                </td>
                                                <td style="border: 1px solid #ccc; padding: 5px 5px; text-align: center">
                                                    Transportation &amp; Other<br />
                                                    (A3)
                                                </td>
                                                <td style="border: 1px solid #ccc; padding: 5px 5px; background-color: #ededed; color: red;
                                                    text-align: center">
                                                    Total<br />
                                                    (A) = A1 + A2 + A3
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right; padding: 5px 5px">
                                                    <asp:Label runat="server" CssClass="total-summary" ID="lblExpenseDailyAllowance"
                                                        Text="0"></asp:Label>
                                                </td>
                                                <%--<td style="text-align: right; padding: 5px 5px">
                                                <asp:Label runat="server" CssClass="total-summary" ID="lblExpenseHotelExpense" Text="0"></asp:Label>
                                            </td>--%>
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
                        <%--Edit Info--%>
                        <div class="HRTabControl edit-form" style="margin-top: 10px; margin-bottom: 10px;
                            position: relative;">
                            <div class="HRTabNav">
                                <ul>
                                    <li id="tabCommon">Allowance</li>
                                    <li>Other Expenses</li>
                                </ul>
                                <div style="clear: both;">
                                </div>
                            </div>
                            <div class="HRTabList">
                                <%--Common--%>
                                <div class="HRTab" style="position: relative;">
                                    <span id="btnAddRequest" style="margin-top: 10px; text-align: center;" class="btn inform add-btn"
                                        onclick="btnAddSub_Click(this);"><i class="add"></i>Add</span>
                                    <%--List--%>
                                    <div class="ui-datatable ui-widget">
                                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                            <ContentTemplate>
                                                <div style="margin-top: 10px; border: 1px solid transparent">
                                                    <dx:ASPxGridView ID="grvBTRequest" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                                        AutoGenerateColumns="false" Style="margin-top: 0" SettingsBehavior-AllowSort="false">
                                                        <SettingsText EmptyDataRow="No records found!" />
                                                        <Styles>
                                                            <AlternatingRow Enabled="True">
                                                            </AlternatingRow>
                                                        </Styles>
                                                        <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                                        <SettingsDetail ShowDetailRow="true" />
                                                        <Columns>
                                                            <dx:GridViewDataColumn Width="25px" Settings-AllowAutoFilter="False" FieldName="No"
                                                                Caption="No" />
                                                            <dx:GridViewDataDateColumn FieldName="oFromDate" Caption="Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                            <dx:GridViewDataColumn FieldName="FromTime" Caption="From Time" Width="80px" />
                                                            <dx:GridViewDataColumn FieldName="ToTime" Caption="To Time" Width="70px" />
                                                            <dx:GridViewDataColumn FieldName="oPurpose" Caption="Destination/Purpose" />
                                                            <dx:GridViewDataColumn FieldName="RequestGA" Caption="Request to GA" Width="100px"
                                                                CellStyle-HorizontalAlign="Center" />
                                                            <dx:GridViewDataTextColumn FieldName="EstimateFee" Width="100px" Caption="Total Amount"
                                                                CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                            <dx:GridViewDataImageColumn Width="40px">
                                                                <DataItemTemplate>
                                                                    <asp:Button ID="btnEdit" runat="server" CssClass='<%# If(Convert.ToBoolean(Eval("EnableForm")), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                        data-id='<%# Eval("ID") %>' OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0]); CheckValidate($('[id$=btnAdd]').attr('id'))"
                                                                        OnClick="btnEditRequest_OnClick"></asp:Button>
                                                                    <input id="Button1" type="button" runat="server" visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>'
                                                                        class="grid-btn delete-btn" title="Delete" onclick="btnDeleteRequestClick(this)" />
                                                                    <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' ToolTip="Delete" runat="server"
                                                                        CssClass="hide" Visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>' OnClick="btnDeleteRequest_OnClick"
                                                                        OnClientClick="HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0]); CheckValidate($('[id$=btnAdd]').attr('id'))">
                                                                    </asp:Button>
                                                                </DataItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            </dx:GridViewDataImageColumn>
                                                        </Columns>
                                                        <Templates>
                                                            <DetailRow>
                                                                <fieldset style="width: 98%; padding: 0; border: none; margin: 10px 0;">
                                                                    <table class="grid-inside" style="margin-top: 0; table-layout: fixed">
                                                                        <tr>
                                                                            <th style="" rowspan="2">
                                                                                Item
                                                                            </th>
                                                                            <th colspan="4">
                                                                                Daily Allowance
                                                                            </th>
                                                                            <th colspan="3">
                                                                                Transportation
                                                                            </th>
                                                                            <th rowspan="2">
                                                                                Moving Time Allowance
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
                                                                            <th style="background-color: #eee !important">
                                                                                Total
                                                                            </th>
                                                                            <td style="text-align: center;">
                                                                                Taxi/Car
                                                                            </td>
                                                                            <td style="text-align: center;">
                                                                                Motobike
                                                                            </td>
                                                                            <th style="background-color: #eee !important">
                                                                                Total
                                                                            </th>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding: 5px !important; text-align: left">
                                                                                Times
                                                                            </td>
                                                                            <td style="padding: 5px !important;">
                                                                                <%#Eval("BreakfastQty", "{0:#,0.##}")%>
                                                                            </td>
                                                                            <td style="padding: 5px !important;">
                                                                                <%#Eval("LunchQty", "{0:#,0.##}")%>
                                                                            </td>
                                                                            <td style="padding: 5px !important;">
                                                                                <%#Eval("DinnerQty", "{0:#,0.##}")%>
                                                                            </td>
                                                                            <td style="padding: 5px !important; background-color: #eee;">
                                                                                _
                                                                            </td>
                                                                            <td style="padding: 5px !important;">
                                                                                <%#Eval("oTaxiTime", "{0:#,0.##}")%>
                                                                            </td>
                                                                            <td style="padding: 5px !important;">
                                                                                <%#Eval("oMotobikeTime", "{0:#,0.##}")%>
                                                                            </td>
                                                                            <td style="padding: 5px !important; background-color: #eee;">
                                                                                _
                                                                            </td>
                                                                            <td style="padding: 5px !important;">
                                                                                _
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="padding: 5px !important; text-align: left">
                                                                                Fee
                                                                            </td>
                                                                            <td style="padding: 5px !important;">
                                                                                <%#Eval("BreakfastAmount", "{0:#,0.##}")%>
                                                                            </td>
                                                                            <td style="padding: 5px !important;">
                                                                                <%#Eval("LunchAmount", "{0:#,0.##}")%>
                                                                            </td>
                                                                            <td style="padding: 5px !important;">
                                                                                <%#Eval("DinnerAmount", "{0:#,0.##}")%>
                                                                            </td>
                                                                            <td style="padding: 5px !important; background-color: #eee;">
                                                                                <%#Eval("TotalAmount", "{0:#,0.##}")%>
                                                                            </td>
                                                                            <td style="padding: 5px !important;">
                                                                                <%#Eval("oTaxiAmount", "{0:#,0.##}")%>
                                                                            </td>
                                                                            <td style="padding: 5px !important;">
                                                                                <%#Eval("oMotobikeAmount", "{0:#,0.##}")%>
                                                                            </td>
                                                                            <td style="padding: 5px !important; background-color: #eee;">
                                                                                <%#Eval("TotalTransportation", "{0:#,0.##}")%>
                                                                            </td>
                                                                            <td style="padding: 5px !important;">
                                                                                <%#Eval("oMovingTimeAllowance", "{0:#,0.##}")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </fieldset>
                                                            </DetailRow>
                                                        </Templates>
                                                    </dx:ASPxGridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <%--Edit form--%>
                                    <fieldset class="add-edit-form" id="TransportationForm">
                                        <legend><span class="add-edit-action"></span>&nbsp;Payment Request</legend>
                                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                            <ContentTemplate>
                                                <asp:HiddenField runat="server" ID="hRequestID" />
                                                <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                                    <tbody>
                                                        <tr>
                                                            <td class="ui-panelgrid-cell" style="width: 120px">
                                                                <label>
                                                                    Date<span class="required">*</span></label>
                                                            </td>
                                                            <td class="ui-panelgrid-cell date-time-picker validate-required">
                                                                <dx:ASPxDateEdit TabIndex="1" ID="dteDate" ClientInstanceName="dteDate" runat="server"
                                                                    EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy"
                                                                    ClientSideEvents-DateChanged="DepartureDateChanged">
                                                                </dx:ASPxDateEdit>
                                                                <asp:Button runat="server" Text="Cancel" OnClientClick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))"
                                                                    ID="btnDepartureDateChange" class="btn secondary hide" />
                                                            </td>
                                                            <td class="ui-panelgrid-cell">
                                                                Time
                                                            </td>
                                                            <td class="ui-panelgrid-cell date-time-picker time-picker schedule-time">
                                                                <dx:ASPxTimeEdit TabIndex="2" Style="width: 100px !important; display: inline-block"
                                                                    ID="txeFromTime" runat="server" ClientInstanceName="txeFromTime" DisplayFormatString="HH:mm"
                                                                    EditFormatString="HH:mm" ClientSideEvents-DateChanged="BTDateChanged" AutoPostBack="false">
                                                                    <ValidationSettings ErrorDisplayMode="None" />
                                                                </dx:ASPxTimeEdit>
                                                                <span style="position: relative; top: -10px; padding: 0 6px;">to</span>
                                                                <dx:ASPxTimeEdit TabIndex="3" Style="width: 100px !important; display: inline-block"
                                                                    ID="txeToTime" runat="server" ClientInstanceName="txeToTime" DisplayFormatString="HH:mm"
                                                                    EditFormatString="HH:mm" ClientSideEvents-DateChanged="BTDateChanged" AutoPostBack="false">
                                                                    <ValidationSettings ErrorDisplayMode="None" />
                                                                </dx:ASPxTimeEdit>
                                                                <input type="button" id="btnSetMealAmount" class="hide" onclick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))" /><%--SetMealAmount()--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="ui-panelgrid-cell">
                                                                <label>
                                                                    Destination<span class="required">*</span></label>
                                                            </td>
                                                            <td class="ui-panelgrid-cell validate-required">
                                                                <asp:DropDownList ID="ddlToLocation" TabIndex="7" AutoPostBack="false" runat="server">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td class="ui-panelgrid-cell spin-edit" colspan="2" runat="server" id="spanMovingTime">
                                                                <asp:CheckBox runat="server" ID="chkMovingTimeAllowance" AutoPostBack="false" Text=" "
                                                                    CssClass="check-button" Style="top: 5px;" />
                                                                <label runat="server" id="lblMovingTimeTitle" style="position: relative;">
                                                                    Moving Time Allowance
                                                                </label>
                                                                <span id="spanMovingTimeAmount" style="padding-left: 5px; color: red"></span>
                                                                <asp:HiddenField runat="server" ID="hActualMovingTimeAmount" />
                                                                <asp:HiddenField runat="server" ID="hMovingTimeAmount" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="ui-panelgrid-cell">
                                                                <label>
                                                                    Request to GA</label>
                                                            </td>
                                                            <td class="ui-panelgrid-cell" colspan="3">
                                                                <asp:CheckBox runat="server" ID="chkCarRequest" Text=" " CssClass="check-button"
                                                                    Style="top: 2px;" />
                                                                <span style="position: relative; top: -3px;">Company Car</span>
                                                                <asp:CheckBox runat="server" ID="chkAirTicketRequest" Text=" " CssClass="check-button"
                                                                    Style="top: 2px; margin-left: 20px;" />
                                                                <span style="position: relative; top: -3px;">Air Ticket</span>
                                                                <asp:CheckBox runat="server" ID="chkTrainTicketRequest" Text=" " CssClass="check-button"
                                                                    Style="top: 2px; margin-left: 20px;" />
                                                                <span style="position: relative; top: -3px;">Train Ticket</span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="ui-panelgrid-cell">
                                                                Meal
                                                            </td>
                                                            <td class="ui-panelgrid-cell" colspan="3">
                                                                <asp:HiddenField runat="server" ID="hBreakfastUnit" />
                                                                <asp:HiddenField runat="server" ID="hLunchUnit" />
                                                                <asp:HiddenField runat="server" ID="hDinnerUnit" />
                                                                <asp:HiddenField runat="server" ID="hMotobikeFee" />
                                                                <div class="request-container">
                                                                    <table class="tblRequestDetails grid-inside grid-normal">
                                                                        <tr>
                                                                            <th style="width: 80px">
                                                                                Item
                                                                            </th>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox runat="server" AutoPostBack="false" onclick="CalculateRequest(true)"
                                                                                    ID="chkBreakfastAmount" Text=" " CssClass="check-button" Style="top: 2px;" />
                                                                                <span style="position: relative; top: -3px; left: -5px;">Breakfast</span>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox runat="server" AutoPostBack="false" onclick="CalculateRequest(true)"
                                                                                    ID="chkLunchAmount" Text=" " CssClass="check-button" Style="top: 2px;" />
                                                                                <span style="position: relative; top: -3px; left: -5px;">Lunch</span>
                                                                            </td>
                                                                            <td style="text-align: center">
                                                                                <asp:CheckBox runat="server" AutoPostBack="false" onclick="CalculateRequest(true)"
                                                                                    ID="chkDinnerAmount" Text=" " CssClass="check-button" Style="top: 2px;" />
                                                                                <span style="position: relative; top: -3px; left: -5px;">Dinner</span>
                                                                            </td>
                                                                            <%--<td style="text-align: center">
                                                                    Other
                                                                </td>--%>
                                                                            <th style="width: 100px; background-color: #eee !important">
                                                                                Total
                                                                            </th>
                                                                        </tr>
                                                                        <tr class="quantity hide">
                                                                            <td style="text-align: left">
                                                                                Times
                                                                            </td>
                                                                            <td class="td-input">
                                                                                <dx:ASPxSpinEdit ID="txtBreakfastQty" ClientInstanceName="txtBreakfastQty" TabIndex="8"
                                                                                    runat="server" Height="21px" MinValue="0" MaxValue="1" NumberType="Integer" NullText="0"
                                                                                    DisplayFormatString="{0:#,0.##}">
                                                                                </dx:ASPxSpinEdit>
                                                                            </td>
                                                                            <td class="td-input">
                                                                                <dx:ASPxSpinEdit ID="txtLunchQty" ClientInstanceName="txtLunchQty" runat="server"
                                                                                    TabIndex="10" Height="21px" MinValue="0" MaxValue="1" NumberType="Integer" NullText="0"
                                                                                    DisplayFormatString="{0:#,0.##}">
                                                                                </dx:ASPxSpinEdit>
                                                                            </td>
                                                                            <td class="td-input">
                                                                                <dx:ASPxSpinEdit ID="txtDinnerQty" ClientInstanceName="txtDinnerQty" runat="server"
                                                                                    TabIndex="12" Height="21px" MinValue="0" MaxValue="1" NumberType="Integer" NullText="0"
                                                                                    DisplayFormatString="{0:#,0.##}">
                                                                                </dx:ASPxSpinEdit>
                                                                            </td>
                                                                            <%--<td class="td-input">
                                                                    <dx:ASPxSpinEdit ID="txtOtherMealQty" runat="server" TabIndex="14" Height="21px"
                                                                        MinValue="0" MaxValue="1000000000000" NumberType="Integer" NullText="0" DisplayFormatString="{0:#,0.##}">
                                                                    </dx:ASPxSpinEdit>
                                                                </td>--%>
                                                                            <td style="background-color: #eee; text-align: right">
                                                                                -
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="amount hide">
                                                                            <td style="text-align: left">
                                                                                Unit
                                                                            </td>
                                                                            <td class="td-input">
                                                                                <dx:ASPxSpinEdit ID="txtBreakfastUnit" ClientInstanceName="txtBreakfastUnit" TabIndex="9"
                                                                                    runat="server" Height="21px" MinValue="0" MaxValue="1000000000000" NullText="0"
                                                                                    NumberType="Float" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" SpinButtons-ShowIncrementButtons="false"
                                                                                    ReadOnly="true">
                                                                                </dx:ASPxSpinEdit>
                                                                            </td>
                                                                            <td class="td-input">
                                                                                <dx:ASPxSpinEdit ID="txtLunchUnit" ClientInstanceName="txtLunchUnit" TabIndex="11"
                                                                                    runat="server" Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float"
                                                                                    NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" SpinButtons-ShowIncrementButtons="false"
                                                                                    ReadOnly="true">
                                                                                </dx:ASPxSpinEdit>
                                                                            </td>
                                                                            <td class="td-input">
                                                                                <dx:ASPxSpinEdit ID="txtDinnerUnit" ClientInstanceName="txtDinnerUnit" TabIndex="13"
                                                                                    runat="server" Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float"
                                                                                    NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" SpinButtons-ShowIncrementButtons="false"
                                                                                    ReadOnly="true">
                                                                                </dx:ASPxSpinEdit>
                                                                            </td>
                                                                            <%--<td class="td-input">
                                                                    <dx:ASPxSpinEdit ID="txtOtherMealUnit" TabIndex="15" runat="server" Height="21px"
                                                                        MinValue="0" MaxValue="1000000000000" NullText="0" NumberType="Float" DecimalPlaces="2"
                                                                        DisplayFormatString="{0:#,0.##}">
                                                                    </dx:ASPxSpinEdit>
                                                                </td>--%>
                                                                            <td style="background-color: #eee; text-align: right">
                                                                                -
                                                                            </td>
                                                                        </tr>
                                                                        <tr class="total-amount">
                                                                            <td style="text-align: left; background-color: #eee; font-weight: bold">
                                                                                Amount
                                                                            </td>
                                                                            <td class="td-input">
                                                                                <dx:ASPxSpinEdit ID="txtBreakfastAmount" runat="server" SpinButtons-ShowIncrementButtons="false"
                                                                                    Height="21px" NullText="0" DecimalPlaces="2" ReadOnly="true" NumberType="Float"
                                                                                    DisplayFormatString="{0:#,0.##}">
                                                                                </dx:ASPxSpinEdit>
                                                                            </td>
                                                                            <td class="td-input">
                                                                                <dx:ASPxSpinEdit ID="txtLunchAmount" runat="server" SpinButtons-ShowIncrementButtons="false"
                                                                                    Height="21px" NullText="0" DecimalPlaces="2" ReadOnly="true" NumberType="Float"
                                                                                    DisplayFormatString="{0:#,0.##}">
                                                                                </dx:ASPxSpinEdit>
                                                                            </td>
                                                                            <td class="td-input">
                                                                                <dx:ASPxSpinEdit ID="txtDinnerAmount" runat="server" SpinButtons-ShowIncrementButtons="false"
                                                                                    Height="21px" NullText="0" DecimalPlaces="2" ReadOnly="true" NumberType="Float"
                                                                                    DisplayFormatString="{0:#,0.##}">
                                                                                </dx:ASPxSpinEdit>
                                                                            </td>
                                                                            <%--<td class="td-input">
                                                                    <dx:ASPxSpinEdit ID="txtOtherMealAmount" runat="server" SpinButtons-ShowIncrementButtons="false"
                                                                        Height="21px" NullText="0" DecimalPlaces="2" ReadOnly="true" NumberType="Float"
                                                                        DisplayFormatString="{0:#,0.##}">
                                                                    </dx:ASPxSpinEdit>
                                                                </td>--%>
                                                                            <td class="cal-total-amount td-input">
                                                                                <dx:ASPxSpinEdit ID="txtTotalAmount" runat="server" SpinButtons-ShowIncrementButtons="false"
                                                                                    Height="21px" NullText="0" DecimalPlaces="2" ReadOnly="true" NumberType="Float"
                                                                                    DisplayFormatString="{0:#,0.##}">
                                                                                </dx:ASPxSpinEdit>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="ui-panelgrid-cell">
                                                                Transportation
                                                            </td>
                                                            <td class="ui-panelgrid-cell" colspan="3">
                                                                <table class="tblRequestDetails grid-inside grid-normal">
                                                                    <tr>
                                                                        <th style="text-align: center; width: 80px">
                                                                            Item
                                                                        </th>
                                                                        <td style="text-align: center">
                                                                            Taxi/Car
                                                                        </td>
                                                                        <td style="text-align: center">
                                                                            Motobike
                                                                        </td>
                                                                        <th style="width: 100px; background-color: #eee !important">
                                                                            Total
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left">
                                                                            Times
                                                                        </td>
                                                                        <td class="td-input">
                                                                            <dx:ASPxSpinEdit TabIndex="18" ID="txtTaxiQty" ClientInstanceName="txtTaxiQty" runat="server"
                                                                                Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Integer" NullText="0"
                                                                                DisplayFormatString="{0:#,0.##}">
                                                                            </dx:ASPxSpinEdit>
                                                                        </td>
                                                                        <td class="td-input motobike-qty">
                                                                            <dx:ASPxSpinEdit TabIndex="20" ID="txtMotobikeQty" ClientInstanceName="txtMotobikeQty"
                                                                                runat="server" Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Integer"
                                                                                NullText="0" DisplayFormatString="{0:#,0.##}">
                                                                            </dx:ASPxSpinEdit>
                                                                        </td>
                                                                        <td style="background-color: #eee; text-align: right">
                                                                            -
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="estimate-transportation-fee">
                                                                        <td style="text-align: left">
                                                                            Fee
                                                                        </td>
                                                                        <td class="td-input" data-type="user-pay">
                                                                            <dx:ASPxSpinEdit TabIndex="19" ID="txtTaxiFee" ClientInstanceName="txtTaxiFee" runat="server"
                                                                                Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float" NullText="0"
                                                                                DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                                            </dx:ASPxSpinEdit>
                                                                        </td>
                                                                        <td class="td-input" data-type="user-pay">
                                                                            <dx:ASPxSpinEdit TabIndex="21" ID="txtMotobikeFee" ClientInstanceName="txtMotobikeFee"
                                                                                runat="server" Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float"
                                                                                NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                                            </dx:ASPxSpinEdit>
                                                                        </td>
                                                                        <td class="cal-total-amount td-input user-pay-summary">
                                                                            <dx:ASPxSpinEdit ID="txtTotalTransport" runat="server" SpinButtons-ShowIncrementButtons="false"
                                                                                Height="21px" NullText="0" DecimalPlaces="2" ReadOnly="true" NumberType="Float"
                                                                                DisplayFormatString="{0:#,0.##}">
                                                                            </dx:ASPxSpinEdit>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="text-align: left">
                                                                            Remark
                                                                        </td>
                                                                        <td class="td-input">
                                                                            <asp:TextBox ID="txtTaxiDesc" TabIndex="22" runat="server" Style="text-align: left"></asp:TextBox>
                                                                        </td>
                                                                        <td class="td-input">
                                                                            <asp:TextBox ID="txtMotobikeDesc" TabIndex="22" runat="server" Style="text-align: left"></asp:TextBox>
                                                                        </td>
                                                                        <td style="background-color: #eee; text-align: right">
                                                                            -
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trCommonCredit">
                                                            <td class="ui-panelgrid-cell">
                                                                Credit Card
                                                            </td>
                                                            <td class="ui-panelgrid-cell spin-edit cc-edit" colspan="3">
                                                                <div style="float: left; padding-top: 3px;">
                                                                    <asp:CheckBox runat="server" AutoPostBack="false" onchange="chkCommonCCAmountChanged()"
                                                                        ID="chkCommonCCAmount" Text=" " CssClass="check-button" Style="top: 2px;" />
                                                                </div>
                                                                <dx:ASPxSpinEdit ID="spiCommonCCAmount" runat="server" ClientInstanceName="spiCommonCCAmount"
                                                                    CssClass="common-credit" Style="float: left; width: 50px !important; display: none;"
                                                                    MinValue="0" MaxValue="1000000000000" NumberType="Float" NullText="0" DecimalPlaces="2"
                                                                    HorizontalAlign="Right" DisplayFormatString="{0:#,0.##}">
                                                                </dx:ASPxSpinEdit>
                                                                <asp:Label runat="server" ID="lblCommonCCCurrency" CssClass="common-credit" Style="float: left;
                                                                    padding-top: 7px; margin-left: 5px; display: none;" Text="VND"></asp:Label>
                                                                <%--<asp:Label runat="server" ID="lblCommonCCMessage" CssClass="common-credit" Style="float: left;
                                                                    padding-top: 7px; margin-left: 10px; color: Red; display: none;"></asp:Label>--%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="ui-panelgrid-cell" style="padding-top: 11px;">
                                                                <label>
                                                                    Purpose<span class="required">*</span></label>
                                                            </td>
                                                            <td class="ui-panelgrid-cell validate-required" colspan="3">
                                                                <asp:TextBox ID="txaPurpose" TabIndex="22" TextMode="MultiLine" runat="server" Rows="3"
                                                                    Style="width: 97% !important;" ToolTip="If your business trip is for a project or an event , you need to specify what project or event. e.g. Project XXX or Support project XXX of ABC department"
                                                                    data-tooltip="all"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="ui-panelgrid-cell" style="padding-top: 11px;">
                                                                <label>
                                                                    Request Date</label>
                                                            </td>
                                                            <td class="ui-panelgrid-cell date-time-picker">
                                                                <dx:ASPxDateEdit TabIndex="23" ID="dteRequestDate" runat="server" ClientInstanceName="dteRequestDate"
                                                                    EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy">
                                                                </dx:ASPxDateEdit>
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                <td class="ui-panelgrid-cell" style="padding-top: 11px;">
                                                    <label>
                                                        Remark</label>
                                                </td>
                                                <td class="ui-panelgrid-cell" colspan="3" style="">
                                                    <asp:TextBox ID="txtTransportationArea" TabIndex="23" TextMode="MultiLine" runat="server"
                                                        Rows="3" Style="width: 97% !important;"></asp:TextBox>
                                                </td>
                                            </tr>--%>
                                                    </tbody>
                                                </table>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnSaveRequest" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <ul id="TransportationSummary" class="error-summary">
                                        </ul>
                                        <div class="action-pan">
                                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                                <ContentTemplate>
                                                    <span runat="server" id="btnShowSaveRequest">
                                                        <input type="button" class="btn" value="Save" onclick="ValidateTransportationForm(this)" />
                                                    </span>
                                                    <asp:Button runat="server" ID="btnSaveRequest" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                                    <input type="button" id="btnCancelRequest" value="Cancel" class="btn secondary btn-cancel-sub"
                                                        onclick="btnCancelSub_Click(this)" />
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
                                                            <%--<dx:GridViewDataColumn FieldName="Currency" Width="90px" Caption="Currency" />--%>
                                                            <dx:GridViewDataImageColumn Width="40px">
                                                                <DataItemTemplate>
                                                                    <asp:Button ID="btnEdit" runat="server" CssClass='<%# If(Convert.ToBoolean(Eval("EnableForm")), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                        data-id='<%# Eval("ID") %>' OnClick="btnEditExpenseOther_OnClick" OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))">
                                                                    </asp:Button>
                                                                    <input id="Button2" type="button" class="grid-btn delete-btn" title="Delete" runat="server"
                                                                        visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>' onclick="btnDeleteOtherClick(this)" />
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
                                                                    runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy">
                                                                </dx:ASPxDateEdit>
                                                                <%--ClientSideEvents-DateChanged="OtherDateChange"--%>
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
                                                                    HorizontalAlign="Right" Style="float: left">
                                                                </dx:ASPxSpinEdit>
                                                                <span style="float: left; padding-top: 7px; margin-left: 5px;">VND</span>
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
                                                                    padding-top: 7px; margin-left: 5px; display: none;" Text="VND"></asp:Label>
                                                                <%--<asp:Label runat="server" ID="lblOtherCCMessage" CssClass="other-credit" Style="float: left;
                                                                    padding-top: 7px; margin-left: 10px; color: Red; display: none;"></asp:Label>--%>
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
                </div>
            </div>
            <%--BT Others--%>
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
                                <li>Air Ticket</li>
                                <li>Invoicing</li>
                            </ul>
                            <div style="clear: both;">
                            </div>
                        </div>
                        <div class="HRTabList" style="padding: 0 1.2em; border: 1px solid #ccc; border-top: none;">
                            <%--Attachment--%>
                            <div class="HRTab" style="margin-bottom: 10px;">
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
                                                        <th style="width: 120px" runat="server" id="tdUploadCaption">
                                                            Upload
                                                        </th>
                                                    </tr>
                                                    <tr runat="server" id="trApproval">
                                                        <td>
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
                                                                            <asp:Panel runat="server" ID="panExpenseAttachments" CssClass="pan-attachment-container">
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                        <td style="text-align: center" runat="server" id="tdUpload1">
                                                            <span id="btnAddRegisterAttachment" style="text-align: center; font-weight: normal;
                                                                padding-right: 14px; display: block;" class="btn inform"><i class="add"></i><span>Choose
                                                                    file</span>
                                                                <input type="file" name="fRegister" class="choose-file" data-type="register" onchange="ChooseFile(this)" />
                                                            </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server" id="tdPaymentOrder">
                                                            2
                                                        </td>
                                                        <td>
                                                            Request for Payment
                                                        </td>
                                                        <td style="padding-bottom: 3px !important; position: relative">
                                                            <div style="position: absolute; top: -1px; left: -1px; width: 100%; height: 100%;
                                                                border: 1px solid transparent">
                                                                <table style="width: 100%; height: 100%; margin: 0;">
                                                                    <tr>
                                                                        <td style="border: none !important; padding: 0 10px !important;">
                                                                            <asp:Panel runat="server" ID="panPaymentAttachments" CssClass="pan-attachment-container">
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                        <td style="text-align: center" runat="server" id="tdUpload2">
                                                            <span id="btnAddPaymentAttachment" style="text-align: center; font-weight: normal;
                                                                padding-right: 14px; display: block;" class="btn inform"><i class="add"></i><span>Choose
                                                                    file</span>
                                                                <input type="file" name="fPayment" class="choose-file" data-type="expense" onchange="ChooseFile(this)" />
                                                            </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td runat="server" id="tdOtherOrder">
                                                            3
                                                        </td>
                                                        <td>
                                                            Others
                                                        </td>
                                                        <td style="padding-bottom: 3px !important">
                                                            <asp:Panel runat="server" ID="panOtherExpenseAttachments" CssClass="pan-attachment-container">
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="text-align: center" runat="server" id="tdUpload3">
                                                            <span id="btnAddOthersAttachment" style="text-align: center; font-weight: normal;
                                                                padding-right: 8px; display: block;" class="btn inform"><i class="add"></i><span>Choose
                                                                    files</span>
                                                                <input type="file" name="fOthers" class="choose-file" data-type="other-expense" multiple="multiple"
                                                                    onchange="ChooseFile(this)" />
                                                            </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="">
                                                            Remark
                                                        </td>
                                                        <td colspan="2" style="padding: 0 !important;" runat="server" id="tdUploadDesc">
                                                            <asp:TextBox ID="txtExpenseDescription" runat="server" TextMode="MultiLine" placeholder="Input remark"
                                                                CssClass="full-fill"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <%--Air Ticket--%>
                            <div class="HRTab" style="margin-bottom: 10px;">
                                <span id="btnAddAirTicket" style="margin-top: 10px; text-align: center;" class="btn inform add-btn add-btn-air"
                                    onclick="btnAddSub_Click(this)"><i class="add"></i>Add</span>
                                <%--List--%>
                                <div class="ui-datatable ui-widget">
                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
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
                                                            <dx:ASPxDateEdit ID="dteAirDate" runat="server" EditFormat="Custom" ClientSideEvents-DateChanged="AirTicketDateChange"
                                                                DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy">
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
                                                                SF<span class="required">*</span></label>
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
                                                <span runat="server" id="btnShowSaveAirTicket">
                                                    <input type="button" class="btn" value="Save" onclick="ValidateAirTicketForm(this)" />
                                                </span>
                                                <asp:Button runat="server" ID="btnSaveAirTicket" Text="Save" CssClass="btn hide"
                                                    OnClientClick="HandleMessage(this); HandlePartialMessageBoard(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                                <asp:Button runat="server" ID="btnCancelAirTicket" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                    OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'));" />
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
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
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
                                                        <dx:GridViewDataColumn FieldName="STT" Caption="STT" Width="90px" />
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
                                                            CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                        <dx:GridViewDataImageColumn Width="40px">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" CssClass='<%# If(Convert.ToBoolean(Eval("EnableInvForm")), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("ID") %>' OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0]); CheckValidate($('[id$=btnAdd]').attr('id'))"
                                                                    OnClick="btnEditInvoice_OnClick"></asp:Button>
                                                                <input type="button" class="grid-btn delete-btn" title="Delete" runat="server" id="btnSubConfirmDelete"
                                                                    visible='<%# Convert.ToBoolean(Eval("EnableInvForm"))%>' onclick="btnDeleteInvoiceClick(this)" />
                                                                <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' Visible='<%# Convert.ToBoolean(Eval("EnableInvForm"))%>'
                                                                    ToolTip="Delete" runat="server" CssClass="hide" OnClientClick="HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0]); CheckValidate($('[id$=btnAdd]').attr('id'))"
                                                                    OnClick="btnDeleteInvoice_OnClick"></asp:Button>
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataImageColumn>
                                                    </Columns>
                                                    <Templates>
                                                        <DetailRow>
                                                            <fieldset style="width: 98%; padding: 0; border: none; margin: 10px 0;">
                                                                <table class="tblRequestDetails grid-inside" style="table-layout: fixed;">
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
                                                            <asp:Label runat="server" ID="lblInvNetCostCurrency" Text="VND" Style="float: left;
                                                                padding-top: 7px; margin-left: 5px;"></asp:Label>
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
                                                            <asp:Label runat="server" ID="lblInvVATCurrency" Text="VND" Style="float: left; padding-top: 7px;
                                                                margin-left: 5px;"></asp:Label>
                                                        </td>
                                                        <%--<td class="ui-panelgrid-cell">
                                                <label>
                                                    Tax Rate (%)<span class="required">*</span></label>
                                            </td>
                                            <td class="ui-panelgrid-cell validate-required spin-edit tax-rate">
                                                <dx:ASPxSpinEdit ID="spiInvTaxRate" runat="server" Height="21px" MinValue="0" MaxValue="1000000000000"
                                                    NumberType="Float" NullText="0" HorizontalAlign="Right" DecimalPlaces="2" DisplayFormatString="{0:#,0}">
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
                                                            <asp:Label runat="server" ID="lblInvTotalCurrency" Text="VND" Style="float: left;
                                                                padding-top: 7px; margin-left: 5px;"></asp:Label>
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
                                                    <tr runat="server" id="trInvoiceCredit" visible="false">
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
                                                <asp:Button runat="server" ID="btnSaveInvoice" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                                <asp:Button runat="server" ID="btnCancelInvoice" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                    OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
    <%--Action buttons--%>
    <div id="next-container" class="action-pan no-transition">
        <span class="btn" style="margin-right: 6px; padding-left: 6px; display: inline-block;">
            <i class="add"></i><span>Add</span>
            <input type="button" class="btn" value="Add" id="btnAddConfirm" />
        </span><span id="btnCopyConfirm" class="btn
    hide" style="margin-right: 6px; padding-left: 6px; display: inline-block;"><i class="copy">
    </i><span>Copy</span>
            <input type="button" class="btn" value="Copy" />
        </span><span id="btnDeleteConfirm" class="btn attention hide" style="margin-right: 6px;
            padding-left: 6px; display: inline-block;"><i class="delete"></i><span>Delete</span>
            <input type="button" class="btn
    attention" value="Delete" />
        </span>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn hide" OnClientClick="bindStartupEvents(this);
    HandleMessage(this); btnAdd_Click()" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn hide" OnClientClick="bindStartupEvents(this); HandleMessage(this);" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="back-container" class="action-pan no-transition" style="display: none;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <span runat="server" id="spanExportBT" style="text-align: center; font-weight: normal;
                    padding: 6px 15px 5px 5px; float: right; margin-bottom: 10px;" class="btn inform export-btn">
                    <i class="export"></i><span>Export BT Request</span>
                    <asp:Button ID="btnExportBT" runat="server" OnClientClick="bindStartupEvents(this);
    HandleMessage(this); GetExportFile(this)" Text="Export" Style="text-align: center;" />
                </span><span runat="server" id="spanExportPayment" style="text-align: center; font-weight: normal;
                    padding: 6px 15px 5px 5px; float: right; margin-right: 4px; margin-bottom: 10px;"
                    class="btn inform
    export-btn"><i class="export"></i><span>Export Request for Payment</span>
                    <asp:Button ID="btnExportPayment" runat="server" OnClientClick="bindStartupEvents(this); HandleMessage(this);
    GetExportFile(this)" Text="Export" Style="text-align: center;" />
                </span>
                <div style="clear: both;">
                </div>
                <span runat="server" id="btnShowSave" class="btn" style="padding-left: 6px;"><i class="save">
                </i><span>Save</span>
                    <asp:Button ID="btnFinish" runat="server" Text="Save" Style="text-align: center;"
                        CssClass="btn" OnClientClick="hidePartialForms(); $('#totalErrorSummary').hide(); bindStartupEvents(this); HandleMessage(this);" /></span>
                <span runat="server" id="btnShowSubmit" class="btn" style="padding-left: 6px;"><i
                    class="submit"></i><span>Submit</span>
                    <input type="button" value="Submit" class="btn" onclick="hidePartialForms(); ValidateSubmit(true)" />
                </span><span runat="server" id="btnShowConfirmBudget" visible="false" class="btn"
                    style="padding-left: 6px;"><i class="submit"></i><span style="padding-left: 10px;">Confirm
                        Budget</span>
                    <input type="button" value="Confirm Budget" class="btn" onclick="ValidateBudget()" /></span>
                <%--Approve/Reject--%>
                <span runat="server" id="btnShowApprove" visible="false" class="btn" style="padding-left: 6px;
                    padding-right: 20px;"><i class="approval-btn"></i><span runat="server" id="spanApproveLabel"
                        style="padding-left: 10px">Approve</span>
                    <input type="button" value="Approve" class="btn" onclick="btnApproveBTClick()" /></span>
                <span runat="server" id="btnShowBudgetReject" visible="false" class="btn secondary"
                    style="padding-left: 6px;"><i class="reject-btn" style="margin-right: 8px;"></i>
                    <span>Confirm Requester</span>
                    <input type="button" value="Confirm Requester" class="btn" onclick="$('[id$=btnBudgetReject]').show(); $('[id$=btnReject]').hide(); $('[id$=btnRejectToBudget]').hide(); $('#rejectBTTitle').html('Send Email to Requester to confirm new budget'); if (ValidateRejectBudget()){showRejectMessage()}" /></span>
                <span runat="server" id="btnShowReject" visible="false" class="btn secondary" style="padding-left: 6px;">
                    <i class="reject-btn"></i><span>Reject</span>
                    <input type="button" value="Reject" class="btn" onclick="$('[id$=btnBudgetReject]').hide(); $('[id$=btnReject]').show(); $('[id$=btnRejectToBudget]').hide(); $('#rejectBTTitle').html('Reject to Requester to update information'); CheckOraStatus()" /></span>
                <span runat="server" id="btnShowRejectToBudget" visible="false" class="btn secondary"
                    style="padding-left: 6px;"><i class="reject-btn"></i><span style="padding-left: 5px">
                        Reject to Finance Budget</span>
                    <input type="button" value="Reject to Finance Budget" class="btn" onclick="$('[id$=btnBudgetReject]').hide(); $('[id$=btnReject]').hide(); $('[id$=btnRejectToBudget]').show(); $('#rejectBTTitle').html('Reject to Finance Budget to check budget'); CheckOraStatus()" /></span>
                <%----%>
                <span runat="server" id="btnConfirmRecall" visible="false" class="btn attention"
                    style="padding-left: 6px;"><i class="recall"></i><span>Recall</span>
                    <input type="button" onclick="ConfirmRecall()" />
                </span>
                <asp:Button ID="btnRecall" runat="server" Visible="false" Text="Recall" Style="text-align: center;"
                    CssClass="btn attention hide" OnClientClick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]);" />
                <%----%>
                <span class="btn secondary" style="padding-left: 6px;"><i class="back"></i><span>Back</span>
                    <input type="button" id="btnConfirmCancel" onclick="ConfirmCancel(<%= If(_enable OrElse _enableBudget, "true", "false") %>)" />
                </span>
                <asp:Button ID="btnCancel" runat="server" Text="Back" Style="text-align: center;"
                    CssClass="btn secondary hide" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div style="clear: both;">
    </div>
    <%--Add new BT--%>
    <div id="panSelectBTInfo" style="display: none; transition: none; position: fixed;
        width: 100%; height: 100%; left: 0px; top: 0px; z-index: 9000; background-color: rgba(0,
    0, 0, 0.7);">
        <div id="panSelectBTInfoContent" style="width: 525px; transition: none; margin: 200px auto;
            position: relative; padding: 10px 20px; box-shadow: rgb(255,
    255, 255) 0px 0px 10px; border-radius: 5px; opacity: 1; background-color: rgb(255,
    255, 255);">
            <div style="margin-bottom: 10px;">
                <fieldset style="padding: 10px 20px
    20px;">
                    <legend style="color: #325EA2">Input BT information</legend>
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <table class="grid-edit" style="margin: 0">
                                <tr>
                                    <td style="width: 180px; font-weight: bold;">
                                        Employee Code:<br />
                                        <asp:TextBox ID="txtSelectEmployeeCode" runat="server" MaxLength="6" Style="margin-top: 5px;"
                                            CssClass="select-employee-code"></asp:TextBox>
                                    </td>
                                    <td style="width: 180px; font-weight: bold; padding-top: 5px; padding-left: 16px">
                                        Business Trip Type:<br />
                                        <asp:DropDownList ID="ddlSelectBTType" runat="server" Style="margin-top: 5px;">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; padding-top: 5px;">
                                        Budget Name:<br />
                                        <asp:DropDownList ID="ddlSelectBudgetName" runat="server" Style="margin-top: 5px;
                                            width: 160px !important" onchange="BudgetCodeChange(this, 'txtSelectBudgetCode')">
                                        </asp:DropDownList>
                                        <span class="check-button" style="margin-left: 5px; position: relative; top: 3px;"
                                            onclick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]);">
                                            <asp:CheckBox runat="server" ID="chkSelectBudgetAll" AutoPostBack="true" />
                                            <span style="padding-top: 5px; font-weight: normal">Other</span> </span>
                                        <asp:HiddenField runat="server" ID="hSelectBudgetByEmp" />
                                    </td>
                                    <td style="font-weight: bold; padding-top: 5px; padding-left: 16px">
                                        Budget Code:<br />
                                        <asp:TextBox ID="txtSelectBudgetCode" runat="server" ReadOnly="true" Style="margin-top: 5px"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hSelectBudgetCode" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 180px; font-weight: bold;">
                                        Project Budget Code:<br />
                                        <asp:TextBox ID="txtSelectProjectBudgetCode" runat="server" Style="margin-top: 5px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </div>
            <div style="margin-left: 20px; width: 300px; color: Red;">
                <ul id="ulPreErrorMessage" class="validate-error-container">
                </ul>
            </div>
            <div style="text-align: center">
                <span id="btnSelectBTTypeOK" class="btn" style="margin-top: 10px;"><span>OK</span></span>
                <span id="btnSelectBTTypeCancel" class="btn secondary" style="margin-left: 5px; margin-top: 10px;">
                    <span>Cancel</span></span>
            </div>
        </div>
    </div>
    <%--Reject BT--%>
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
                                        <h3 id="rejectBTTitle" style="margin: 0; padding: 5px 0 3px; background-color: #fff;
                                            text-decoration: underline;">
                                        </h3>
                                        <div style="margin-top: 10px; font-weight: bold;">
                                            Recommendation:
                                        </div>
                                        <asp:TextBox ID="txtRejectReason" class="reject-reason" Style="width: auto !important;
                                            margin-top: 5px;" TextMode="MultiLine" Rows="5" Columns="60" runat="server"></asp:TextBox>
                                        <asp:HiddenField runat="server" ID="hOldBudget" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 0 0 15px; text-align: center;">
                                        <asp:Button ID="btnBudgetReject" runat="server" Visible="false" Text="Confirm Requester"
                                            Style="text-align: center;" CssClass="btn" OnClientClick="if(!checkReject())
    return false; bindStartupEvents($('[id$=btnCancel]')[0]); HandleMessage($('[id$=btnCancel]')[0]);" />
                                        <asp:Button ID="btnReject" runat="server" Visible="false" Text="Reject" Style="text-align: center;"
                                            CssClass="btn" OnClientClick="if(!checkReject())
    return false; bindStartupEvents($('[id$=btnCancel]')[0]); HandleMessage($('[id$=btnCancel]')[0]);" />
                                        <asp:Button ID="btnRejectToBudget" runat="server" Visible="false" Text="Reject to Finance Budget"
                                            Style="text-align: center;" CssClass="btn" OnClientClick="if(!checkReject())
    return false; bindStartupEvents($('[id$=btnCancel]')[0]); HandleMessage($('[id$=btnCancel]')[0]);" />
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
    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
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
                                            Approve BT Expense Declaration (Invoice)</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <asp:HiddenField runat="server" ID="hApproveMessage" />
                                    <td runat="server" style="padding: 0px 30px 5px; color: red" id="approveMessage"
                                        colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 5px 5px 0 30px;" class="date-time-picker">
                                        <h4 style="margin: 0; padding: 5px 0 3px; background-color: #fff; text-decoration: underline;
                                            border: none;">
                                            Invoice Date</h4>
                                        <dx:ASPxDateEdit ID="dteInvoiceDate" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy">
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
                                    <td style="padding: 5px 30px 15px;" colspan="2">
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
                                <tr>
                                    <td colspan="2" style="padding: 5px 30px;">
                                        <ul id="approve-summary">
                                        </ul>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 10px 0 15px; text-align: center;" colspan="2">
                                        <asp:Button ID="btnApprove" runat="server" Text="Approve" Style="text-align: center;"
                                            CssClass="btn" OnClientClick="if(!checkApprove()) return false; bindStartupEvents($('[id$=btnCancel]')[0]);
    HandleMessage($('[id$=btnCancel]')[0]);" />
                                        <asp:Button runat="server" Text="Cancel" OnClientClick="hideApproveMessage(); bindStartupEvents($('[id$=btnCancel]')[0]);
    HandleMessage($('[id$=btnCancel]')[0]);" Style="margin-left: 5px;" ID="btnApproveCancel" class="btn secondary" />
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
                                        <h3 id="submitBTTitle" style="margin: 0; padding: 5px 0 3px; background-color: #fff;
                                            border-bottom: 1px solid #999;">
                                        </h3>
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
    <%--Confirm Budget BT--%>
    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
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
                                        <asp:Button ID="btnConfirmBudget" runat="server" Text="Confirm Budget" Style="text-align: center;"
                                            CssClass="btn" OnClientClick="bindStartupEvents($('[id$=btnCancel]')[0]); HandleMessage($('[id$=btnCancel]')[0]);" />
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
    <%--Copy BT--%>
    <asp:UpdatePanel ID="UpdatePanel21" runat="server">
        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hCopyUsers" />
            <div id="tabChoosemployee" class="popup-container">
                <div style="margin: 50px 50px; padding: 0; background-color: #fff; overflow: auto;
                    border-radius: 5px; box-shadow: 0 0 10px #fff; -webkit-box-shadow: 0 0 10px #fff;
                    -moz-box-shadow: 0 0 10px #fff;">
                    <fieldset style="margin: 20px; padding: 15px;">
                        <legend>Choose Employees</legend>
                        <table style="width: 100%;">
                            <tr>
                                <td style="vertical-align: middle; border: none !important; padding: 0 0 20px !important;">
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                        <ContentTemplate>
                                            <asp:HiddenField runat="server" ID="hchooseEmpID" />
                                            <dx:ASPxGridView ID="grvChooseEmployee" runat="server" SettingsPager-Mode="ShowAllRecords"
                                                CssClass="scrolling-table grid-radio" Theme="Office2010Black" AutoGenerateColumns="false"
                                                Style="margin: auto">
                                                <SettingsText EmptyDataRow="No records found!" />
                                                <Settings ShowVerticalScrollBar="true" VerticalScrollableHeight="300" ShowFilterRow="true"
                                                    ShowFilterRowMenu="true" />
                                                <Styles>
                                                    <AlternatingRow Enabled="True">
                                                    </AlternatingRow>
                                                </Styles>
                                                <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                                <Columns>
                                                    <dx:GridViewDataColumn Width="25px" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                                        <DataItemTemplate>
                                                            <input type="checkbox" class="chkChooseEmployee" value='<%# Eval("EmployeeCode") %>'
                                                                name="employees" onchange="CheckboxChecked(this)" />
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn CellStyle-CssClass="code" Width="80px" FieldName="EmployeeCode"
                                                        Caption="Code" />
                                                    <dx:GridViewDataColumn Width="150px" FieldName="EmployeeName" Caption="Name" />
                                                    <dx:GridViewDataColumn FieldName="DivisionName" Caption="Division" />
                                                    <dx:GridViewDataColumn Width="80px" FieldName="DepartmentName" Caption="Department" />
                                                    <dx:GridViewDataColumn Width="200px" FieldName="SectionName" Caption="Section/Group/Team" />
                                                </Columns>
                                            </dx:ASPxGridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; padding-top: 10px" class="action-pan">
                                    <input type="button" value="Paste" onclick="CheckChoosedEmployess()" style="margin-left: 5px;"
                                        class="btn" />
                                    <asp:Button ID="btnCopy" OnClientClick="HandleMessage(this); bindStartupEvents(this);"
                                        CssClass="btn hide" runat="server" Text="Paste" />
                                    <input type="button" value="Cancel" onclick="hideChooseEmployee()" style="margin-left: 5px;"
                                        id="btnCancelChooseEmployee" class="btn secondary" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--View Error--%>
    <div id="oraErrorContainer" class="no-transition" style="display: none; position: absolute;
        z-index: 1001; background-color: #fff; padding: 10px 20px; border: 1px solid red;
        border-radius: 5px; color: Red; box-shadow: 0 1px 10px #aaa; -webkit-box-shadow: 0 1px 10px #999;
        -moz-box-shadow: 0 1px 10px #aaa; border-radius: 5px; -webkit-border-radius: 5px;
        -moz-border-radius: 5px; line-height: 1.5;">
        <div id="oraErrorDetails" style="max-width: 300px;">
        </div>
        <i style="position: absolute; width: 10px; height: 10px; bottom: -10px; right: 58px;
            background: url(/images/triangle.png) center center no-repeat transparent;">
        </i>
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">

    <script src="/js/jquery/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">              
        var _authorizedAccounts = <%= GetAuthorizedAccounts() %>   
        var _selectAuthorizedAccounts = <%= GetSelectAuthorizedAccounts() %>        
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

    <script src="/js/bt-oneday-expense-register.js" type="text/javascript"></script>

</asp:Content>
