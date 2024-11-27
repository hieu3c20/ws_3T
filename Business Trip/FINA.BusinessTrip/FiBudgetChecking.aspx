<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FiBudgetChecking.aspx.vb"
    Inherits="FiBudgetChecking" MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="HeadContent" runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link href="/js/jquery/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    FI Budget Checking
</asp:Content>
<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <img src="images/inform-btn.png" style="display: none" alt="" />
    <img src="images/check-check.png" style="display: none" alt="" />
    <img src="images/check.png" style="display: none" alt="" />
    <img src="images/rad-check.png" style="display: none" alt="" />
    <img src="images/rad.png" style="display: none" alt="" />
    <span class="required"></span>
    <div class="ui-accordion ui-widget ui-helper-reset ui-hidden-container" role="tablist">
        <div id="panGetInfo" class="no-transition">
            <%--Search form--%>
            <div role="tab-container">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top active"
                    role="tab">
                    <span class="ui-icon"></span>Search Condition</h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content" role="tabpanel">
                    <span class="required" style="text-align: center"></span>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hItemID" runat="server" />
                            <asp:Panel runat="server" ID="panMessage">
                            </asp:Panel>
                            <asp:Literal runat="server" ID="lblMessage"></asp:Literal>
                            <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                <tbody>
                                    <tr>
                                        <td class="ui-panelgrid-cell" style="width: 120px">
                                            <label>
                                                Business Trip Type</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="ddlBTType" runat="server">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hBTType" />
                                        </td>
                                        <td class="ui-panelgrid-cell" style="width: 120px">
                                            <label>
                                                Location</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="cboLocation" runat="server">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hLocation" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                From Date</label>
                                        </td>
                                        <td class="ui-panelgrid-cell date-time-picker">
                                            <dx:ASPxDateEdit ID="dtpFrom" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                                EditFormatString="dd-MMM-yyyy">
                                            </dx:ASPxDateEdit>
                                            <asp:HiddenField runat="server" ID="hFrom" />
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                To Date</label>
                                        </td>
                                        <td class="ui-panelgrid-cell date-time-picker">
                                            <dx:ASPxDateEdit ID="dtpTo" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                                EditFormatString="dd-MMM-yyyy">
                                            </dx:ASPxDateEdit>
                                            <asp:HiddenField runat="server" ID="hTo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Division</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="cboDivision" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hDivision" />
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Department</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="cboDept" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hDept" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Section</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="cboSection" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hSection" />
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Group/Team</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="cboGroup" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hGroup" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Employee Code</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="employee-code" data-type="int"
                                                MaxLength="6"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hEmployeeCode" />
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Full Name</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hFullName" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                BT No/Invoice No</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtBTNo" runat="server"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hBTNo" />
                                        </td>
                                        <%--<td class="ui-panelgrid-cell">
                                            <label>
                                                Oracle Status</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="ddlOraStatus" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hOraStatus" />
                                        </td>--%>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                BTS Status</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="ddlBTSStatus" runat="server" AutoPostBack="false">
                                                <asp:ListItem Value="" Text="All"></asp:ListItem>
                                                <asp:ListItem Value="pending" Text="Pending"></asp:ListItem>
                                                <asp:ListItem Value="rejected" Text="Rejected"></asp:ListItem>
                                                <asp:ListItem Value="checked" Text="Budget Checked"></asp:ListItem>
                                                <asp:ListItem Value="completed" Text="Completed"></asp:ListItem>
                                                <asp:ListItem Value="cancelled" Text="Cancelled"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hBTSStatus" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <span class="btn inform" style="margin-left: 3px; top: 20px; text-align: center;">
                                                <asp:Button ID="btnSearch" CssClass="btnSearch" runat="server" Text="" OnClientClick="bindStartupEvent(); HandleMessage(this)" />
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
            <div class="tab-container">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top active"
                    role="tab">
                    <span class="ui-icon"></span>Advance Infomation</h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content" role="tabpanel">
                    <div class="ui-datatable ui-widget">
                        <div class="ui-datatable-tablewrapper">
                            <div class="HRTabControl">
                                <div class="HRTabNav">
                                    <ul>
                                        <li>Pending</li>
                                        <li>Rejected</li>
                                        <li>Budget Checked</li>
                                        <li>Completed</li>
                                        <li>Cancelled</li>
                                    </ul>
                                    <div style="clear: both;">
                                    </div>
                                </div>
                                <div class="HRTabList">
                                    <%--Pending BT--%>
                                    <div class="HRTab">
                                        <div class="bt-legend" style="float: right; padding: 5px 0 10px;">
                                            <strong>BTS Legend:</strong> <span class="ora-done" style="padding: 0 5px; border-right: 1px solid #ccc;">
                                                Pending</span> <span class="waiting" style="padding: 0 5px;">Confirm</span>
                                        </div>
                                        <div style="clear: both">
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="grvBTRegister" KeyFieldName="BTRegisterID" runat="server" Theme="Office2010Black"
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
                                                        <dx:GridViewDataColumn Width="25px" FieldName="BudgetChecked" Caption="" Visible="false" />
                                                        <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                                            FieldName="BTRegisterID" Caption="ID" />
                                                        <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Right" Width="50px" FieldName="EmployeeCode"
                                                            Caption="Employee Code">
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
                                                        <dx:GridViewDataImageColumn Width="20px" CellStyle-HorizontalAlign="Left">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnView" runat="server" ToolTip="View" CssClass="grid-btn viewDetails-btn"
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnViewBTClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent()"
                                                                    OnClick="btnView_OnClick"></asp:Button>
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
                                    <%--Rejected BT--%>
                                    <div class="HRTab">
                                        <%--<div class="bt-legend" style="float: right; padding: 5px 0 10px;">
                                            <strong>BTS Legend:</strong> <span class="ora-rejected" style="padding: 0 5px">BT Rejected</span>
                                        </div>
                                        <div style="clear: both">
                                        </div>--%>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="grvBTRejected" KeyFieldName="BTRegisterID" runat="server" Theme="Office2010Black"
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
                                                        <dx:GridViewDataColumn Width="25px" FieldName="BudgetChecked" Caption="" Visible="false" />
                                                        <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                                            FieldName="BTRegisterID" Caption="ID" />
                                                        <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Right" Width="50px" FieldName="EmployeeCode"
                                                            Caption="Employee Code">
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
                                                        <dx:GridViewDataImageColumn Width="20px" CellStyle-HorizontalAlign="Left">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnView" runat="server" ToolTip="View" CssClass="grid-btn viewDetails-btn"
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnViewBTClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent()"
                                                                    OnClick="btnView_OnClick"></asp:Button>
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
                                    <%--Checked BT--%>
                                    <div class="HRTab">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="grvBTChecked" KeyFieldName="BTRegisterID" runat="server" Theme="Office2010Black"
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
                                                        <dx:GridViewDataColumn Width="25px" FieldName="BudgetChecked" Caption="" Visible="false" />
                                                        <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                                            FieldName="BTRegisterID" Caption="ID" />
                                                        <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Right" Width="50px" FieldName="EmployeeCode"
                                                            Caption="Employee Code">
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
                                                        <dx:GridViewDataImageColumn Width="20px" CellStyle-HorizontalAlign="Left">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnView" runat="server" ToolTip="View" CssClass="grid-btn viewDetails-btn"
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnViewBTClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent()"
                                                                    OnClick="btnView_OnClick"></asp:Button>
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
                                    <%--Completed BT--%>
                                    <div class="HRTab">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="grvBTCompleted" KeyFieldName="BTRegisterID" runat="server" Theme="Office2010Black"
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
                                                        <dx:GridViewDataColumn Width="25px" FieldName="BudgetChecked" Caption="" Visible="false" />
                                                        <dx:GridViewDataColumn CellStyle-CssClass="id" Width="25px" Settings-AllowAutoFilter="False"
                                                            FieldName="BTRegisterID" Caption="ID" />
                                                        <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Right" Width="50px" FieldName="EmployeeCode"
                                                            Caption="Employee Code">
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
                                                        <dx:GridViewDataImageColumn Width="20px" CellStyle-HorizontalAlign="Left">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnView" runat="server" ToolTip="View" CssClass="grid-btn viewDetails-btn"
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnViewBTClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent()"
                                                                    OnClick="btnView_OnClick"></asp:Button>
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
                                    <%--Cancelled BT--%>
                                    <div class="HRTab">
                                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
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
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnViewBTClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent()"
                                                                    OnClick="btnView_OnClick"></asp:Button>
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
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">

    <script src="/js/jquery/jquery.autocomplete.js" type="text/javascript"></script>

    <%-- Request.QueryString("btid") --%>

    <script type="text/javascript">
        var btid = ''
    </script>

    <script src="/js/fi-budget-checking.js" type="text/javascript"></script>

</asp:Content>
