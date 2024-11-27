<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BTAdvanceDeclaration.aspx.vb"
    Inherits="BTAdvanceDeclaration" MasterPageFile="~/MasterPage.Master" %>

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

        function RequestDateChanged() {
            //$("#btnRequestDateChange").click()
            CalculateRequest()
        }
        var currentDepartureDate
        function DepartureDateChanged() {
            if ($("[id$=grvBTRequest]").find("tr.dxgvDataRow_Office2010Black").size()) {
                ShowErrorMessage("Advance request existed, can not change departure date!")
                departureDate.SetDate(currentDepartureDate)
            }
            else {
                currentDepartureDate = departureDate.GetDate()                
                $("[id$=btnDepartureDateChange]").click()
            }
        }
    </script>

</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    Overnight Trip Advance Management
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
                                        <asp:DropDownList ID="ddlBudgetName" runat="server" onchange="BudgetCodeChange(this, 'txtBudgetCode'); HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))"
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
                    <span class="ui-icon"></span>Advance Information</h3>
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
                                        <li>Cancelled</li>
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
                                                                    class="chkSelect" onchange="CheckboxChecked(this)" />
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
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnEditBTClick(this); HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0]); bindRequestAdvance($('[id$=btnAdd]')[0])"
                                                                    OnClick="btnEdit_OnClick"></asp:Button>
                                                                <input type="button" runat="server" id="btnShowDelete" visible='<%# Eval("IsOwner").ToString() = "1" %>'
                                                                    class="grid-btn delete-btn" title="Delete" data-id='<%# Eval("BTRegisterID") %>'
                                                                    onclick="btnDeleteBTClick(this)" />
                                                                <asp:Button ID="btnDelete" Visible='<%# Eval("IsOwner").ToString() = "1" %>' data-id='<%# Eval("BTRegisterID") %>'
                                                                    ToolTip="Delete" runat="server" CssClass="hide" OnClick="btnDelete_OnClick" OnClientClick="HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0])">
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
                                                Pending</span> <span class="waiting" style="padding: 0 5px">Confirm</span>
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
                                                                    class="chkSelect" onchange="CheckboxChecked(this)" />
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
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnEditBTClick(this); HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0]); bindRequestAdvance($('[id$=btnAdd]')[0])"
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
                                        <%--<div class="bt-legend" style="float: right; padding: 5px 0 10px;">
                                            <strong>BTS Legend:</strong> <span class="ora-rejected" style="padding: 0 5px">Rejected</span>
                                        </div>
                                        <div style="clear: both">
                                        </div>--%>
                                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
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
                                                                    class="chkSelect" onchange="CheckboxChecked(this)" />
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
                                                        <dx:GridViewDataImageColumn Width="40px">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" ToolTip="View/Edit" CssClass='<%# If(Eval("IsOwner").ToString() = "1" AndAlso (Eval("FIStatus").ToString() = FIStatus.rejected.ToString() OrElse Eval("FIStatus").ToString() = FIStatus.budget_rejected.ToString()), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnEditBTClick(this); HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0]); bindRequestAdvance($('[id$=btnAdd]')[0])"
                                                                    OnClick="btnEdit_OnClick"></asp:Button>
                                                                <input type="button" runat="server" id="btnShowDelete" visible='<%# Eval("IsOwner").ToString() = "1" %>'
                                                                    class="grid-btn delete-btn" title="Delete" data-id='<%# Eval("BTRegisterID") %>'
                                                                    onclick="btnDeleteBTClick(this)" />
                                                                <asp:Button ID="btnDelete" Visible='<%# Eval("IsOwner").ToString() = "1" %>' data-id='<%# Eval("BTRegisterID") %>'
                                                                    ToolTip="Delete" runat="server" CssClass="hide" OnClick="btnDelete_OnClick" OnClientClick="HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0])">
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
                                                                    class="chkSelect" onchange="CheckboxChecked(this)" />
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
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnEditBTClick(this); HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0]); bindRequestAdvance($('[id$=btnAdd]')[0])"
                                                                    OnClick="btnEdit_OnClick"></asp:Button>
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataImageColumn>
                                                    </Columns>
                                                </dx:ASPxGridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <%--Cancelled BT--%>
                                    <div class="HRTab">
                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="grvBTCancelled" KeyFieldName="BTRegisterID" runat="server" Theme="Office2010Black"
                                                    AutoGenerateColumns="false">
                                                    <SettingsText EmptyDataRow="No records found!" />
                                                    <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
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
                                                                <asp:CheckBox data-id='<%# Eval("BTRegisterID") %>' data-EmpCode='<%# Eval("EmployeeCode") %>'
                                                                    runat="server" ID="chkSelect" CssClass="chkSelect" onchange="CheckboxChecked(this)" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn> --%>
                                                        <dx:GridViewDataColumn Width="25px" FieldName="FIStatus" Caption="" Visible="false" />
                                                        <dx:GridViewDataColumn Width="25px" CellStyle-CssClass="id" Settings-AllowAutoFilter="False"
                                                            FieldName="BTRegisterID" Caption="ID" />
                                                        <dx:GridViewDataTextColumn CellStyle-CssClass="code" CellStyle-HorizontalAlign="Right"
                                                            Width="50px" FieldName="EmployeeCode" Caption="Employee Code">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Full Name" Width="130px" />
                                                        <dx:GridViewBandColumn Caption="Departure">
                                                            <Columns>
                                                                <dx:GridViewDataDateColumn Width="75px" FieldName="FromDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
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
                                                        <dx:GridViewDataImageColumn Width="1px" CellStyle-HorizontalAlign="Left">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnView" runat="server" ToolTip="View" CssClass="grid-btn viewDetails-btn"
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnEditBTClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvents()"
                                                                    OnClick="btnEdit_OnClick"></asp:Button>
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataImageColumn>
                                                    </Columns>
                                                    <%--  <SettingsDetail ShowDetailRow="true" />--%>
                                                </dx:ASPxGridView>
                                                <%--<div id="next-container" visible="false" class="action-pan hide">
                                                    <asp:Button runat="server" Text="Approve" OnClientClick="HandleMessage(this)" CssClass="btnApproval hide"
                                                        ID="btnApproval" />
                                                    <input type="button" onclick="confirmApproval(this)" id="btnConfirmApproval" value="Approval"
                                                        class="btn" />
                                                </div>--%>
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
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <table class="ui-panelgrid ui-widget grid-edit" role="grid">
                                <tbody>
                                    <tr>
                                        <td class="ui-panelgrid-cell validate-required" colspan="2" style="padding-bottom: 10px">
                                            <asp:CheckBox runat="server" ID="chkShowNoRequestAdvance" Text=" " CssClass="check-button"
                                                onchange="ConfirmNoRequest()" />
                                            <label style="position: relative; top: -5px; color: Red;">
                                                No request advance (Advance amount = 0)
                                            </label>
                                            <asp:CheckBox runat="server" ID="chkNoRequestAdvance" Text=" " CssClass="hide" AutoPostBack="true"
                                                onchange="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id')); doRequestAdvance();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell" style="width: 120px">
                                            <label>
                                                Departure Date<span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell date-time-picker validate-required">
                                            <dx:ASPxDateEdit ID="dteDepartureDate" runat="server" ClientInstanceName="departureDate"
                                                EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm"
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
                                            <dx:ASPxDateEdit ID="dteReturnDate" runat="server" ClientInstanceName="returnDate"
                                                EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm">
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
                                            <asp:DropDownList ID="ddlDestinationCountry" Enabled="false" runat="server">
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
                                            <asp:TextBox ID="txtPurpose" runat="server" TextMode="MultiLine" Rows="3" Style="width: 97% !important"
                                                ToolTip="If your business trip is for a project or an event , you need to specify what project or event. e.g. Project XXX or Support project XXX of ABC department"
                                                data-tooltip="all"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell" style="vertical-align: top">
                                            <label>
                                                Request to GA</label>
                                        </td>
                                        <td class="ui-panelgrid-cell" colspan="3" style="vertical-align: top">
                                            <asp:CheckBox runat="server" ID="chkRequestAirTicket" Text=" " CssClass="check-button"
                                                onchange="onRequestAirTicketChanged(true)" />
                                            <span style="position: relative; top: -4px; padding-right: 8px">Air Ticket</span>
                                            <asp:CheckBox runat="server" ID="chkRequestTrainTicket" Text=" " CssClass="check-button" />
                                            <span style="position: relative; top: -4px; padding-right: 8px;">Train Ticket</span>
                                            <asp:CheckBox runat="server" ID="chkRequestCar" Text=" " CssClass="check-button" />
                                            <span style="position: relative; top: -4px; padding-right: 8px;">Car</span>
                                            <%--<asp:CheckBox runat="server" ID="chkRequestWifi" Visible="false" Text=" " CssClass="check-button" />
                                            <span runat="server" id="lblRequestWifi" visible="false" style="position: relative;
                                                top: -4px;">Wifi Device </span>
                                            <div visible="false" style="padding-top: 10px; font-style: italic;" runat="server"
                                                id="panWifiNote">
                                                * Note: The wifi device must be returned to IT Dept immediately when a requestor
                                                come back to office</div>--%>
                                            <asp:HiddenField runat="server" ID="hRequestGA" />
                                        </td>
                                    </tr>
                                    <tr class="expected-air" style="display: none">
                                        <td class="ui-panelgrid-cell">
                                        </td>
                                        <td class="ui-panelgrid-cell date-time-picker">
                                            Expected Departure Time:
                                            <dx:ASPxDateEdit ID="dteExpectedDepartureTime" runat="server" EditFormat="Custom"
                                                DisplayFormatString="dd-MMM-yyyy HH:mm" ClientInstanceName="expectedDepartureTime"
                                                EditFormatString="dd-MMM-yyyy HH:mm">
                                                <TimeSectionProperties Visible="true">
                                                    <TimeEditProperties EditFormatString="HH:mm" />
                                                </TimeSectionProperties>
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td class="ui-panelgrid-cell" colspan="2">
                                            Expected Departure Flight No / Remark:<br />
                                            <asp:TextBox ID="txtExpectedDepartureFlightNo" runat="server" Style="width: 95% !important;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="expected-air" style="display: none">
                                        <td class="ui-panelgrid-cell">
                                        </td>
                                        <td class="ui-panelgrid-cell date-time-picker">
                                            Expected Return Time:
                                            <dx:ASPxDateEdit ID="dteExpectedReturnTime" runat="server" ClientInstanceName="expectedReturnTime"
                                                EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm">
                                                <TimeSectionProperties Visible="true">
                                                    <TimeEditProperties EditFormatString="HH:mm" />
                                                </TimeSectionProperties>
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td class="ui-panelgrid-cell" colspan="2">
                                            Expected Return Flight No / Remark:<br />
                                            <asp:TextBox ID="txtExpectedReturnFlightNo" runat="server" Style="width: 95% !important;"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <%--Advance Request--%>
            <div class="tab-container">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                    role="tab">
                    <span class="ui-icon"></span>Advance Payment</h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content default-hide hide"
                    role="tabpanel" style="position: relative">
                    <%--Summary--%>
                    <div class="ui-datatable ui-widget">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div>
                                    <%--<fieldset style="background-color: #f9f9f9; padding: 10px; margin-top: 0;">
                                        <legend style="color: #325EA2; font-size: 1.2em">Summary</legend>--%>
                                    <table style="background-color: #f9f9f9; margin: 0;">
                                        <%--<tr>
                                            <th style="border: 1px solid #ccc; padding: 5px 5px" colspan="5">
                                                Advance Request
                                            </th>
                                            <th style="border: 1px solid #ccc; padding: 5px 5px" rowspan="2">
                                                Request to GA
                                            </th>
                                        </tr>--%>
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
                                                <asp:Label runat="server" CssClass="total-summary" ID="lblTotalAdvance" Text="0"
                                                    data-tooltip="all" title=""></asp:Label>
                                            </td>
                                            <%--<td style="padding: 5px 5px">
                                                <asp:Label runat="server" CssClass="total-summary" ID="lblRequestGA"></asp:Label>
                                            </td>--%>
                                        </tr>
                                    </table>
                                    <%-- </fieldset>--%>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <%--Advance Info--%>
                    <%--Global Request--%>
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hMovingTime" />
                            <asp:HiddenField runat="server" ID="hOtherAmount" />
                            <asp:HiddenField runat="server" ID="hFirstTime" />
                            <div style="float: right; margin-top: 10px;">
                                <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="width: 750px;
                                    margin-top: 7px;">
                                    <tbody>
                                        <tr>
                                            <td style="width: auto">
                                                &nbsp;
                                            </td>
                                            <%--Moving Time Allowance--%>
                                            <td runat="server" id="spanMovingTime" class="ui-panelgrid-cell" style="text-align: right;
                                                width: 250px; padding-top: 2px; padding-right: 2px">
                                                <asp:CheckBox runat="server" ID="chkMovingTimeAllowance" AutoPostBack="false" Text=" "
                                                    CssClass="check-button" onchange="CalculateSummary()" />
                                                <label style="position: relative; top: -5px;">
                                                    Moving Time Allowance <span runat="server" id="spanMovingTimeAmount" style="color: red">
                                                    </span>
                                                </label>
                                            </td>
                                            <%--First Time Oversea--%>
                                            <td runat="server" id="spanFirstTimeOversea" class="ui-panelgrid-cell" style="text-align: right;
                                                width: 210px; padding-top: 2px; padding-right: 2px">
                                                <asp:CheckBox runat="server" ID="chkFirstTimeOversea" AutoPostBack="false" Text=" "
                                                    CssClass="check-button" onchange="CalculateSummary()" /><label style="position: relative;
                                                        top: -5px;">
                                                        First Time Oversea <span runat="server" id="spanFirstAmount" style="color: red">
                                                        </span>
                                                    </label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <span id="btnAddOneDay" style="margin-top: 15px; text-align: center;" class="btn inform add-btn"
                        onclick="btnAddSub_Click(this)"><i class="add"></i>Add</span>
                    <%--List--%>
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
                                            <dx:GridViewDataColumn FieldName="Other" Caption="Other Explanation / Remark" />
                                            <dx:GridViewDataImageColumn Width="40px">
                                                <DataItemTemplate>
                                                    <asp:Button ID="btnEdit" runat="server" CssClass='<%# If(Convert.ToBoolean(Eval("EnableForm")), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                        data-id='<%# Eval("RequestID") %>' OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0]); CheckValidate($('[id$=btnAdd]').attr('id'))"
                                                        OnClick="btnEditRequest_OnClick"></asp:Button>
                                                    <input type="button" runat="server" visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>'
                                                        class="grid-btn delete-btn" title="Delete" onclick="btnDeleteRequestClick(this)" />
                                                    <asp:Button ID="btnDelete" data-id='<%# Eval("RequestID") %>' Visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>'
                                                        ToolTip="Delete" runat="server" CssClass="hide" OnClick="btnDeleteRequest_OnClick"
                                                        OnClientClick="HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0]); CheckValidate($('[id$=btnAdd]').attr('id'))">
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
                                                            <td style="color: red">
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
                    <%--Edit form--%>
                    <fieldset class="add-edit-form" id="AdvanceForm">
                        <legend><span class="add-edit-action"></span>&nbsp;Advance Information</legend>
                        <asp:UpdatePanel runat="server">
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
                                                <asp:DropDownList ID="ddlDestinationLocation" AutoPostBack="false" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ui-panelgrid-cell" style="width: 160px;">
                                                <label>
                                                    From Date<span class="required">*</span></label>
                                            </td>
                                            <td class="ui-panelgrid-cell date-time-picker validate-required">
                                                <dx:ASPxDateEdit ID="dteFromDate" TabIndex="4" ClientInstanceName="dteFromDate" runat="server"
                                                    EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm"
                                                    AutoPostBack="false" ClientSideEvents-DateChanged="RequestDateChanged">
                                                    <TimeSectionProperties Visible="true">
                                                        <TimeEditProperties EditFormatString="HH:mm" />
                                                    </TimeSectionProperties>
                                                </dx:ASPxDateEdit>
                                            </td>
                                            <td class="ui-panelgrid-cell" style="width: 100px;">
                                                <label>
                                                    To Date<span class="required">*</span></label>
                                            </td>
                                            <td role="gridcell" class="ui-panelgrid-cell date-time-picker validate-required">
                                                <dx:ASPxDateEdit ID="dteToDate" TabIndex="5" ClientInstanceName="dteToDate" runat="server"
                                                    EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy HH:mm" EditFormatString="dd-MMM-yyyy HH:mm"
                                                    AutoPostBack="false" ClientSideEvents-DateChanged="RequestDateChanged">
                                                    <TimeSectionProperties Visible="true">
                                                        <TimeEditProperties EditFormatString="HH:mm" />
                                                    </TimeSectionProperties>
                                                </dx:ASPxDateEdit>
                                                <input type="button" id="btnRequestDateChange" class="hide" onclick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]); CheckValidate($('[id$=btnCancel]').attr('id'))" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="ui-panelgrid-cell">
                                                <label>
                                                    Request</label>
                                            </td>
                                            <td class="ui-panelgrid-cell" colspan="3">
                                                <asp:HiddenField runat="server" ID="hBreakfastUnit" />
                                                <asp:HiddenField runat="server" ID="hLunchUnit" />
                                                <asp:HiddenField runat="server" ID="hDinnerUnit" />
                                                <asp:HiddenField runat="server" ID="hOtherMealUnit" />
                                                <asp:HiddenField runat="server" ID="hHotelUnit" />
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
                                                        <td style="text-align: center">
                                                            Breakfast
                                                        </td>
                                                        <td style="text-align: center">
                                                            Lunch
                                                        </td>
                                                        <td style="text-align: center">
                                                            Dinner
                                                        </td>
                                                        <td style="text-align: center">
                                                            Other
                                                        </td>
                                                        <th style="background-color: #eee !important">
                                                            Total
                                                        </th>
                                                    </tr>
                                                    <tr class="quantity">
                                                        <td style="text-align: left; color: Red">
                                                            Times
                                                        </td>
                                                        <td class="td-input">
                                                            <dx:ASPxSpinEdit TabIndex="6" ID="txtBreakfastQty" ClientInstanceName="breakfastQty"
                                                                runat="server" Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float"
                                                                DecimalPlaces="2" NullText="0" DisplayFormatString="{0:#,0.##}" ForeColor="Red">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                        <td class="td-input">
                                                            <dx:ASPxSpinEdit TabIndex="8" ID="txtLunchQty" ClientInstanceName="lunchQty" runat="server"
                                                                Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Integer" NullText="0"
                                                                DisplayFormatString="{0:#,0.##}" ForeColor="Red">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                        <td class="td-input">
                                                            <dx:ASPxSpinEdit TabIndex="10" ID="txtDinnerQty" ClientInstanceName="dinnerQty" runat="server"
                                                                Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Integer" NullText="0"
                                                                DisplayFormatString="{0:#,0.##}" ForeColor="Red">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                        <td class="td-input">
                                                            <dx:ASPxSpinEdit TabIndex="12" ID="txtOtherMealQty" ClientInstanceName="otherMealQty"
                                                                runat="server" Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Integer"
                                                                NullText="0" DisplayFormatString="{0:#,0.##}" ForeColor="Red">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                        <td style="background-color: #eee; text-align: right">
                                                            -
                                                        </td>
                                                        <td class="td-input">
                                                            <dx:ASPxSpinEdit ID="txtHotelQty" runat="server" ClientInstanceName="hotelQty" Height="21px"
                                                                MinValue="0" MaxValue="1000000000000" NumberType="Integer" TabIndex="14" NullText="0"
                                                                DisplayFormatString="{0:#,0.##}" ForeColor="Red">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                    <tr class="amount">
                                                        <td style="text-align: left">
                                                            Unit
                                                        </td>
                                                        <td class="td-input">
                                                            <dx:ASPxSpinEdit TabIndex="7" ID="txtBreakfastUnit" ClientInstanceName="breakfastUnit"
                                                                runat="server" Height="21px" MinValue="0" MaxValue="1000000000000" NullText="0"
                                                                NumberType="Float" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" SpinButtons-ShowIncrementButtons="false"
                                                                ReadOnly="true">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                        <td class="td-input">
                                                            <dx:ASPxSpinEdit TabIndex="9" ID="txtLunchUnit" ClientInstanceName="lunchUnit" runat="server"
                                                                Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float" NullText="0"
                                                                DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" SpinButtons-ShowIncrementButtons="false"
                                                                ReadOnly="true">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                        <td class="td-input">
                                                            <dx:ASPxSpinEdit TabIndex="11" ID="txtDinnerUnit" ClientInstanceName="dinnerUnit"
                                                                runat="server" Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float"
                                                                NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" SpinButtons-ShowIncrementButtons="false"
                                                                ReadOnly="true">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                        <td class="td-input">
                                                            <dx:ASPxSpinEdit TabIndex="13" ID="txtOtherMealUnit" ClientInstanceName="otherMealUnit"
                                                                runat="server" Height="21px" MinValue="0" MaxValue="1000000000000" NullText="0"
                                                                NumberType="Float" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" SpinButtons-ShowIncrementButtons="false"
                                                                ReadOnly="true">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                        <td style="background-color: #eee; text-align: right">
                                                            -
                                                        </td>
                                                        <td class="td-input">
                                                            <dx:ASPxSpinEdit TabIndex="15" ID="txtHotelUnit" ClientInstanceName="hotelUnit" runat="server"
                                                                Height="21px" MinValue="0" ForeColor="Red" MaxValue="1000000000000" NumberType="Float"
                                                                NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                            </dx:ASPxSpinEdit>
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
                                                        <td class="td-input">
                                                            <dx:ASPxSpinEdit ID="txtOtherMealAmount" runat="server" SpinButtons-ShowIncrementButtons="false"
                                                                Height="21px" NullText="0" DecimalPlaces="2" ReadOnly="true" NumberType="Float"
                                                                DisplayFormatString="{0:#,0.##}">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                        <td class="cal-total-amount td-input">
                                                            <dx:ASPxSpinEdit ID="txtTotalAmount" runat="server" SpinButtons-ShowIncrementButtons="false"
                                                                Height="21px" NullText="0" DecimalPlaces="2" ReadOnly="true" NumberType="Float"
                                                                DisplayFormatString="{0:#,0.##}">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                        <td class="td-input hotel-amount">
                                                            <dx:ASPxSpinEdit ID="txtHotelAmount" runat="server" SpinButtons-ShowIncrementButtons="false"
                                                                Height="21px" NullText="0" DecimalPlaces="2" ReadOnly="true" NumberType="Float"
                                                                DisplayFormatString="{0:#,0.##}">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td role="gridcell" class="ui-panelgrid-cell">
                                                <label>
                                                    Transportation &amp; Other</label>
                                            </td>
                                            <td class="ui-panelgrid-cell spin-edit" style="vertical-align: top">
                                                <dx:ASPxSpinEdit TabIndex="15" ID="txtOtherAmount" ClientInstanceName="otherAmount"
                                                    runat="server" Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float"
                                                    NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}" HorizontalAlign="Right"
                                                    Style="float: left;">
                                                </dx:ASPxSpinEdit>
                                                <asp:Label runat="server" ID="lblTransportationAndOtherCurrency" Style="float: left;
                                                    padding-top: 7px; margin-left: 5px;"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td role="gridcell" class="ui-panelgrid-cell">
                                                <label>
                                                    Other Explanation / Remark</label>
                                            </td>
                                            <td class="ui-panelgrid-cell" colspan="3">
                                                <asp:TextBox ID="txtOther" TabIndex="18" Style="width: 97% !important" runat="server"
                                                    placeholder="Transportation, airport tax, entertainment, etc" TextMode="MultiLine"
                                                    Rows="3"></asp:TextBox>
                                            </td>
                                            <%--<td role="gridcell" class="ui-panelgrid-cell">
                                                <label>
                                                    Remark</label>
                                            </td>
                                            <td class="ui-panelgrid-cell" colspan="3">
                                                <asp:TextBox ID="txaRemark" TabIndex="19" runat="server" TextMode="MultiLine" Rows="3"
                                                    Style="width: 97% !important"></asp:TextBox>
                                            </td>--%>
                                        </tr>
                                    </tbody>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSaveRequest" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <ul id="AdvanceSummary" class="error-summary">
                        </ul>
                        <div class="action-pan">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <span runat="server" id="btnShowSaveRequest">
                                        <input type="button" class="btn" value="Save" onclick="ValidateAdvanceForm(this)" />
                                    </span>
                                    <asp:Button runat="server" ID="btnSaveRequest" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); HandlePartialMessageBoard(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                    <input type="button" id="btnCancelRequest" value="Cancel" class="btn secondary btn-cancel-sub"
                                        onclick="btnCancelSub_Click(this); ClearRequestForm();" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </fieldset>
                </div>
            </div>
            <%--BT Other--%>
            <div class="tab-container">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                    role="tab">
                    <span class="ui-icon"></span>Others (Optional)</h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content default-hide hide"
                    role="tabpanel">
                    <div class="HRTabControl edit-form" style="margin-top: 10px; margin-bottom: 10px;">
                        <div class="HRTabNav">
                            <ul>
                                <li>Attachment</li>
                                <li id="tabSchedule">Schedule</li>
                                <li>Air Ticket</li>
                                <li>Wifi Device</li>
                            </ul>
                            <div style="clear: both;">
                            </div>
                        </div>
                        <div class="HRTabList">
                            <%--Attachment--%>
                            <div class="HRTab" style="position: relative;">
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
                                                    <tr>
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
                                                                            <asp:Panel runat="server" ID="panRegisterAttachments" CssClass="pan-attachment-container">
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
                                                        <td>
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
                                                        <td style="text-align: center" runat="server" id="tdUpload2">
                                                            <span id="btnAddScheduleAttachment" style="text-align: center; font-weight: normal;
                                                                padding-right: 14px; display: block;" class="btn inform"><i class="add"></i><span>Choose
                                                                    file</span>
                                                                <input type="file" name="fSchedule" class="choose-file" data-type="schedule" onchange="ChooseFile(this)" />
                                                            </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            3
                                                        </td>
                                                        <td>
                                                            Others
                                                        </td>
                                                        <td style="padding-bottom: 3px !important">
                                                            <asp:Panel runat="server" ID="panOthersAttachments" CssClass="pan-attachment-container">
                                                            </asp:Panel>
                                                        </td>
                                                        <td style="text-align: center" runat="server" id="tdUpload3">
                                                            <span id="btnAddOthersAttachment" style="text-align: center; font-weight: normal;
                                                                padding-right: 8px; display: block;" class="btn inform"><i class="add"></i><span>Choose
                                                                    files</span>
                                                                <input type="file" name="fOthers" class="choose-file" data-type="other" multiple="multiple"
                                                                    onchange="ChooseFile(this)" />
                                                            </span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="">
                                                            Remark
                                                        </td>
                                                        <td colspan="2" style="padding: 0 !important;" runat="server" id="tdUploadDesc">
                                                            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" placeholder="Input remark"
                                                                CssClass="full-fill"></asp:TextBox>
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
                                <span id="btnAddSchedule" style="text-align: center; float: left; margin-top: 10px;
                                    margin-bottom: 4px" class="btn inform add-btn" onclick="btnAddSub_Click(this)"><i
                                        class="add"></i>Add</span>
                                <asp:UpdatePanel ID="UpdatePanel19" runat="server">
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
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <div style="margin-top: 10px; border: 1px solid transparent">
                                                <dx:ASPxGridView ID="grvBTSchedule" runat="server" KeyFieldName="ID" Theme="Office2010Black"
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
                                                        <dx:GridViewDataTextColumn FieldName="EstimateTransportationFee" Width="120px" Caption="Transportation Fee"
                                                            CellStyle-HorizontalAlign="Right" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                        <%--<dx:GridViewDataColumn FieldName="Request" Caption="Request to GA" />--%>
                                                        <dx:GridViewDataImageColumn Width="40px">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" CssClass='<%# If(Convert.ToBoolean(Eval("EnableForm")), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("ID") %>' OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0]); CheckValidate($('[id$=btnAdd]').attr('id'))"
                                                                    OnClick="btnEditSchedule_OnClick"></asp:Button>
                                                                <input type="button" class="grid-btn delete-btn" title="Delete" runat="server" id="btnSubConfirmDelete"
                                                                    visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>' onclick="btnDeleteScheduleClick(this)" />
                                                                <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' Visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>'
                                                                    ToolTip="Delete" runat="server" CssClass="hide" OnClick="btnDeleteSchedule_OnClick"
                                                                    OnClientClick="HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0]); CheckValidate($('[id$=btnAdd]').attr('id'))">
                                                                </asp:Button>
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataImageColumn>
                                                    </Columns>
                                                    <TotalSummary>
                                                        <dx:ASPxSummaryItem FieldName="EstimateTransportationFee" DisplayFormat="{0:#,0.##}"
                                                            SummaryType="Sum" />
                                                    </TotalSummary>
                                                </dx:ASPxGridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%--Edit form--%>
                                <fieldset class="add-edit-form" id="ScheduleForm">
                                    <legend><span class="add-edit-action"></span>&nbsp;Schedule</legend>
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
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
                                                            <dx:ASPxDateEdit runat="server" ID="dteScheduleDate" ClientInstanceName="dteScheduleDate"
                                                                EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy">
                                                            </dx:ASPxDateEdit>
                                                        </td>
                                                        <td class="ui-panelgrid-cell" style="width: 100px">
                                                            Time
                                                        </td>
                                                        <td class="ui-panelgrid-cell date-time-picker time-picker schedule-time">
                                                            <dx:ASPxTimeEdit ID="txeFromTime" ClientInstanceName="txeFromTime" Style="width: 100px !important;
                                                                display: inline-block" runat="server" DisplayFormatString="HH:mm" EditFormatString="HH:mm">
                                                                <ValidationSettings ErrorDisplayMode="None" />
                                                            </dx:ASPxTimeEdit>
                                                            <span style="position: relative; top: -10px; padding: 0 6px;">to</span>
                                                            <dx:ASPxTimeEdit ID="txeToTime" ClientInstanceName="txeToTime" Style="width: 100px !important;
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
                                                            <asp:TextBox ID="txtWorkingArea" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                        </td>
                                                        <td class="ui-panelgrid-cell">
                                                            <label>
                                                                Task/Remark</label>
                                                        </td>
                                                        <td role="gridcell" class="ui-panelgrid-cell">
                                                            <asp:TextBox ID="txtTask" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="ui-panelgrid-cell">
                                                            <label>
                                                                Transportation Fee</label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell spin-edit">
                                                            <dx:ASPxSpinEdit ID="spiEstimateTransportationFee" ClientInstanceName="estimateTransportationFee"
                                                                runat="server" Height="21px" MinValue="0" MaxValue="1000000000000" NumberType="Float"
                                                                NullText="0" DecimalPlaces="2" DisplayFormatString="{0:#,0.##}">
                                                            </dx:ASPxSpinEdit>
                                                        </td>
                                                        <%--<td class="ui-panelgrid-cell">
                                                            <label>
                                                                Request to GA</label>
                                                        </td>
                                                        <td class="ui-panelgrid-cell schedule-request">
                                                            <asp:CheckBox runat="server" ID="chkAirTicket" Text=" " CssClass="check-button" />
                                                            <span style="position: relative; top: -4px; padding-right: 8px">Air Ticket</span>
                                                            <asp:CheckBox runat="server" ID="chkTrainTicket" Text=" " CssClass="check-button" />
                                                            <span style="position: relative; top: -4px; padding-right: 8px;">Train Ticket</span>
                                                            <asp:CheckBox runat="server" ID="chkCar" Text=" " CssClass="check-button" />
                                                            <span style="position: relative; top: -4px;">Car</span>
                                                        </td>--%>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSaveSchedule" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <ul id="ScheduleSummary" class="error-summary">
                                    </ul>
                                    <div class="action-pan">
                                        <asp:UpdatePanel ID="UpdatePanel77" runat="server">
                                            <ContentTemplate>
                                                <span runat="server" id="btnShowSaveSchedule">
                                                    <input type="button" class="btn" value="Save" onclick="ValidateScheduleForm(this)" />
                                                </span>
                                                <asp:Button runat="server" ID="btnSaveSchedule" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                                <input type="button" id="btnCancelSchedule" value="Cancel" class="btn secondary btn-cancel-sub"
                                                    onclick="btnCancelSub_Click(this); ClearScheduleForm();" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </fieldset>
                            </div>
                            <%--Air ticket--%>
                            <div class="HRTab">
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
                                                                            VAT / TAX
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
                                                        <td class="ui-panelgrid-cell spin-edit air-net-payment validate-required">
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
                                                    OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </fieldset>
                            </div>
                            <%--Wifi device--%>
                            <div class="HRTab">
                                <span id="btnAddWifiDevice" style="text-align: center; float: left; margin-top: 10px;
                                    margin-bottom: 4px" class="btn inform add-btn" onclick="btnAddSub_Click(this)"><i
                                        class="add"></i>Add</span>
                                <%-- <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                    <ContentTemplate>
                                        <span runat="server" id="spanBtnExportWifiDevice" style="text-align: center; font-weight: normal;
                                            padding: 6px 15px 5px 5px; float: left; margin-top: 10px; margin-bottom: 4px"
                                            class="btn inform export-btn"><i class="export"></i><span>Export Wifi Device Form</span>
                                            <asp:Button ID="btnExportWifiDevice" runat="server" OnClientClick="bindStartupEvents(this); HandleMessage(this); GetExportFile(this)"
                                                Text="Export" Style="text-align: center;" />
                                        </span>
                                    </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                <div style="clear: both">
                                </div>
                                <%--List--%>
                                <div class="ui-datatable ui-widget">
                                    <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                        <ContentTemplate>
                                            <div style="margin-top: 10px; border: 1px solid transparent">
                                                <dx:ASPxGridView ID="grvWifiDevice" runat="server" KeyFieldName="ID" Theme="Office2010Black"
                                                    AutoGenerateColumns="false" Style="margin-top: 0" SettingsBehavior-AllowSort="false">
                                                    <%--<Settings ShowFooter="true" />--%>
                                                    <SettingsText EmptyDataRow="No records found!" />
                                                    <Styles>
                                                        <AlternatingRow Enabled="True">
                                                        </AlternatingRow>
                                                    </Styles>
                                                    <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                                    <Columns>
                                                        <dx:GridViewDataColumn Width="25px" Settings-AllowAutoFilter="False" FieldName="No"
                                                            Caption="No" />
                                                        <dx:GridViewDataColumn FieldName="Country" Caption="Country" Width="150px" />
                                                        <dx:GridViewDataDateColumn FieldName="FromDate" Caption="From Date" Width="90px"
                                                            PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                        <dx:GridViewDataDateColumn FieldName="ToDate" Caption="To Date" Width="90px" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                        <dx:GridViewDataImageColumn Width="40px">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnEdit" runat="server" CssClass='<%# If(Convert.ToBoolean(Eval("EnableForm")), "grid-btn edit-btn", "grid-btn viewDetails-btn") %>'
                                                                    data-id='<%# Eval("ID") %>' OnClientClick="btnEditRequestClick(this); HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0]); CheckValidate($('[id$=btnAdd]').attr('id'))"
                                                                    OnClick="btnEditWifiDevice_OnClick"></asp:Button>
                                                                <input type="button" class="grid-btn delete-btn" title="Delete" runat="server" id="btnSubConfirmDelete"
                                                                    visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>' onclick="btnDeleteWifiDeviceClick(this)" />
                                                                <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' Visible='<%# Convert.ToBoolean(Eval("EnableForm"))%>'
                                                                    ToolTip="Delete" runat="server" CssClass="hide" OnClick="btnDeleteWifiDevice_OnClick"
                                                                    OnClientClick="HandleMessage($('[id$=btnAdd]')[0]); bindStartupEvents($('[id$=btnAdd]')[0]); CheckValidate($('[id$=btnAdd]').attr('id'))">
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
                                                <span runat="server" id="btnShowSaveWifiDevice">
                                                    <input type="button" class="btn" value="Save" onclick="ValidateWifiForm(this)" />
                                                </span>
                                                <asp:Button runat="server" ID="btnSaveWifiDevice" Text="Save" CssClass="btn hide"
                                                    OnClientClick="HandleMessage(this); bindStartupEvents(this); CheckValidate($(this).attr('id'))" />
                                                <input type="button" id="btnCancelWifiDevice" value="Cancel" class="btn secondary btn-cancel-sub"
                                                    onclick="btnCancelSub_Click(this); ClearWifiDeviceForm();" />
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
        </span><span id="btnCopyConfirm" class="btn hide" style="margin-right: 6px; padding-left: 6px;
            display: inline-block;"><i class="copy"></i><span>Copy</span>
            <input type="button" class="btn" value="Copy" />
        </span><span id="btnDeleteConfirm" class="btn attention hide" style="margin-right: 6px;
            padding-left: 6px; display: inline-block;"><i class="delete"></i><span>Delete</span>
            <input type="button" class="btn attention" value="Delete" />
        </span>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn hide" OnClientClick="bindStartupEvents(this); HandleMessage(this); btnAdd_Click(); bindRequestAdvance(this)" />
                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn hide" OnClientClick="bindStartupEvents(this); HandleMessage(this);" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="back-container" class="action-pan no-transition" style="display: none;">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <span runat="server" id="spanExportBT" style="text-align: center; font-weight: normal;
                    padding: 6px 15px 5px 5px; display: block; float: right; margin-bottom: 10px;"
                    class="btn inform export-btn"><i class="export"></i><span>Export BT Request</span>
                    <asp:Button ID="btnExportBT" runat="server" OnClientClick="bindStartupEvents(this); HandleMessage(this); GetExportFile(this)"
                        Text="Export" Style="text-align: center;" />
                </span><span runat="server" id="spanExportSchedule1" style="text-align: center; font-weight: normal;
                    padding: 6px 15px 5px 5px; float: right; margin-right: 5px; margin-bottom: 10px"
                    class="btn inform export-btn"><i class="export"></i><span>Export Schedule</span>
                    <asp:Button ID="btnExportSchedule1" runat="server" OnClientClick="bindStartupEvents(this); HandleMessage(this); GetExportFile(this)"
                        Text="Export" Style="text-align: center;" />
                </span>
                <div style="clear: both">
                </div>
                <span runat="server" id="btnShowSave" class="btn" style="margin-right: 6px; padding-left: 6px;">
                    <i class="save"></i><span>Save</span>
                    <asp:Button ID="btnFinish" runat="server" Text="Save" Style="text-align: center;"
                        CssClass="btn" OnClientClick="hidePartialForms(); $('#totalErrorSummary').hide(); bindStartupEvents(this); HandleMessage(this);" />
                </span><span runat="server" id="btnShowSubmit" class="btn" style="padding-left: 6px;">
                    <i class="submit"></i><span>Submit</span>
                    <input type="button" value="Submit" class="btn" onclick="hidePartialForms(); ValidateSubmit(true)" /></span>
                <span runat="server" id="btnShowConfirmBudget" visible="false" class="btn" style="padding-left: 6px;">
                    <i class="submit"></i><span style="padding-left: 10px;">Confirm Budget</span>
                    <input type="button" value="Confirm Budget" class="btn" onclick="ValidateBudget()" /></span>
                <%--Approve/Reject--%>
                <span runat="server" id="btnShowApprove" visible="false" class="btn" style="padding-left: 6px;
                    padding-right: 20px;"><i class="approval-btn"></i><span style="padding-left: 10px"
                        runat="server" id="spanApproveLabel">Approve</span>
                    <input type="button" value="Approve" class="btn" onclick="btnApproveBTClick()" /></span>
                <span runat="server" id="btnShowBudgetReject" visible="false" class="btn secondary"
                    style="padding-left: 6px;"><i class="reject-btn" style="margin-right: 8px;"></i>
                    <span>Confirm Requester</span>
                    <input type="button" value="Confirm Requester" class="btn" onclick="$('[id$=btnBudgetReject]').show(); $('[id$=btnReject]').hide(); $('[id$=btnRejectToBudget]').hide(); $('#rejectBTTitle').html('Send Email to Requester to confirm new budget'); if (ValidateRejectBudget()){showRejectMessage()}" /></span>
                <span runat="server" id="btnShowReject" visible="false" class="btn secondary" style="padding-left: 6px;">
                    <i class="reject-btn"></i><span>Reject</span>
                    <input type="button" value="Reject" class="btn" onclick="$('[id$=btnBudgetReject]').hide(); $('[id$=btnReject]').show(); $('[id$=btnRejectToBudget]').hide(); $('#rejectBTTitle').html('Reject to Requester to update information'); CheckOraStatus()" /></span>
                <span runat="server" id="btnShowRejectToBudget" visible="false" class="btn secondary"
                    style="padding-left: 6px;"><i class="reject-btn"></i><span style="padding-left: 5px;">
                        Reject to Finance Budget</span>
                    <input type="button" value="Reject to Finance Budget" class="btn" onclick="$('[id$=btnBudgetReject]').hide(); $('[id$=btnReject]').hide(); $('[id$=btnRejectToBudget]').show(); $('#rejectBTTitle').html('Reject to Finance Budget to check budget'); CheckOraStatus()" /></span>
                <%----%>
                <span runat="server" id="btnShowResetAdvance" visible="false" class="btn attention"
                    style="padding-left: 6px;" data-tooltip="hover" data-tooltip-width="auto" title="Advance amount = 0">
                    <i class="eraser"></i><span style="padding-left: 5px;">Clear Advance Request</span>
                    <input type="button" value="Clear Advance Request" class="btn" onclick="ConfirmClearRequest()" /></span>
                <asp:Button ID="btnResetAdvance" runat="server" Visible="false" Text="Clear Advance Request"
                    Style="text-align: center;" CssClass="btn attention hide" OnClientClick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]);" />
                <span runat="server" id="btnShowCancelBT" visible="false" class="btn attention" style="padding-left: 6px;"
                    data-tooltip="hover" data-tooltip-width="auto" title="User cancelled the trip"><i
                        class="cancel"></i><span style="padding-left: 5px;">Cancel BT</span>
                    <input type="button" value="Cancel BT" class="btn" onclick="ConfirmClearBT()" /></span>
                <asp:Button ID="btnCancelBT" runat="server" Visible="false" Text="Cancel BT" Style="text-align: center;"
                    CssClass="btn attention hide" OnClientClick="HandleMessage($('[id$=btnCancel]')[0]); bindStartupEvents($('[id$=btnCancel]')[0]);" />
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
    <%--Popup Forms--%>
    <%--Add new BT--%>
    <div id="panSelectBTInfo" style="display: none; transition: none; position: fixed;
        width: 100%; height: 100%; left: 0px; top: 0px; z-index: 9000; background-color: rgba(0, 0, 0, 0.7);">
        <div id="panSelectBTInfoContent" style="width: 525px; transition: none; margin: 200px auto;
            position: relative; padding: 10px 20px; box-shadow: rgb(255, 255, 255) 0px 0px 10px;
            border-radius: 5px; opacity: 1; background-color: rgb(255, 255, 255);">
            <div style="margin-bottom: 10px;">
                <fieldset style="padding: 10px 20px 20px;">
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
                                        <asp:DropDownList ID="ddlSelectBTType" runat="server" Style="margin-top: 5px;" AutoPostBack="true"
                                            onchange="HandleMessage(this); bindStartupEvents(this);">
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
                                        <asp:HiddenField runat="server" ID="hSelectBudgetName" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; padding-top: 5px;">
                                        Country:<br />
                                        <asp:DropDownList ID="ddlSelectDestinationCountry" runat="server" Style="margin-top: 5px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="font-weight: bold; padding-top: 5px; padding-left: 16px">
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
                                            Style="text-align: center;" CssClass="btn" OnClientClick="if(!checkReject()) return false; bindStartupEvents($('[id$=btnCancel]')[0]); HandleMessage($('[id$=btnCancel]')[0]);" />
                                        <asp:Button ID="btnReject" runat="server" Visible="false" Text="Reject" Style="text-align: center;"
                                            CssClass="btn" OnClientClick="if(!checkReject()) return false; bindStartupEvents($('[id$=btnCancel]')[0]); HandleMessage($('[id$=btnCancel]')[0]);" />
                                        <asp:Button ID="btnRejectToBudget" runat="server" Visible="false" Text="Reject to Finance Budget"
                                            Style="text-align: center;" CssClass="btn" OnClientClick="if(!checkReject()) return false; bindStartupEvents($('[id$=btnCancel]')[0]); HandleMessage($('[id$=btnCancel]')[0]);" />
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
    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
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
                                            Approve BT Advance Request (Invoice)</h3>
                                    </td>
                                </tr>
                                <tr>
                                    <asp:HiddenField runat="server" ID="hApproveMessage" />
                                    <td style="padding: 0 30px 5px; color: red" runat="server" id="approveMessage" colspan="2">
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
                                            CssClass="btn" OnClientClick="if(!checkApprove()) return false; bindStartupEvents($('[id$=btnCancel]')[0]); HandleMessage($('[id$=btnCancel]')[0]);" />
                                        <asp:Button runat="server" Text="Cancel" OnClientClick="hideApproveMessage(); bindStartupEvents($('[id$=btnCancel]')[0]); HandleMessage($('[id$=btnCancel]')[0]);"
                                            Style="margin-left: 5px;" ID="btnApproveCancel" class="btn secondary" />
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
                                            Submit to Finance Accouting &amp; GA to check/process</h3>
                                        <div style="margin-top: 10px; font-weight: bold;">
                                            Comment:
                                        </div>
                                        <asp:TextBox ID="txtConfirmComment" class="reject-reason" Style="width: auto !important;
                                            margin-top: 5px;" TextMode="MultiLine" Rows="5" Columns="65" runat="server"></asp:TextBox>
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

    <script src="/js/bt-approval-register.js" type="text/javascript"></script>

</asp:Content>
