<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FiAdvanceMgmt.aspx.vb"
    Inherits="FiAdvanceMgmt" MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="HeadContent" runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link href="/js/jquery/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    FI Advance management
</asp:Content>
<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <%--Preload images--%>
    <img src="images/inform-btn.png" style="display: none" alt="" />
    <img src="images/check-check.png" style="display: none" alt="" />
    <img src="images/check.png" style="display: none" alt="" />
    <img src="images/rad-check.png" style="display: none" alt="" />
    <img src="images/rad.png" style="display: none" alt="" />
    <img src="images/triangle.png" style="display: none" alt="" />
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
                            <asp:HiddenField ID="hIsReApprove" runat="server" />
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
                                        <li>No Advance</li>
                                        <li>Completed</li>
                                        <li>Cancelled</li>
                                    </ul>
                                    <div style="clear: both;">
                                    </div>
                                </div>
                                <div class="HRTabList">
                                    <div class="HRTab">
                                        <div class="bt-legend" style="float: right; padding: 5px 0 10px;">
                                            <strong>BTS Legend:</strong> <span class="ora-not-found" style="padding: 0 5px; border-right: 1px solid #ccc;">
                                                Pending</span> <span class="ora-done" style="padding: 0 5px; border-right: 1px solid #ccc;">
                                                    Budget Checked</span> <span class="waiting" style="padding: 0 5px;">Confirm</span>
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
                                                        <dx:GridViewDataImageColumn Width="60px" CellStyle-HorizontalAlign="Left">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnView" runat="server" ToolTip="View" CssClass="grid-btn viewDetails-btn"
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnViewBTClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent()"
                                                                    OnClick="btnView_OnClick"></asp:Button>
                                                                <input type="button" class="grid-btn approval-btn <%# If(Eval("FIStatus").ToString() <> FIStatus.checked.ToString(), "hide", "") %>"
                                                                    onclick="$('[id$=btnApprove]').val('Approve').attr('title', 'Approve'); $('[id$=hIsReApprove]').val('F'); confirmApproval(this)"
                                                                    data-id='<%# Eval("BTRegisterID") %>' data-budget-code='<%# Eval("BudgetCodeID") %>'
                                                                    data-employee-code='<%# Eval("EmployeeCode") %>' title="Approve" />
                                                                <input type="button" class="grid-btn reject-btn <%# If(Eval("FIStatus").ToString() <> FIStatus.checked.ToString(), "hide", "") %>"
                                                                    data-id='<%# Eval("BTRegisterID") %>' title="Reject" onclick="CheckOraStatus(this)"
                                                                    data-reject="0" />
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
                                            <strong>BTS Legend:</strong> <span class="ora-rejected" style="padding: 0 5px">Rejected</span>
                                        </div>
                                        <div style="clear: both">
                                        </div>--%>
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
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
                                    <%--No advance BT--%>
                                    <div class="HRTab">
                                        <%--<div class="bt-legend" style="float: right; padding: 5px 0 10px;">
                                            <strong>BTS Legend:</strong> <span class="ora-done" style="padding: 0 5px;">Completed</span>
                                        </div>
                                        <div style="clear: both">
                                        </div>--%>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="grvBTNoAdvance" KeyFieldName="BTRegisterID" runat="server" Theme="Office2010Black"
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
                                                        <dx:GridViewDataImageColumn Width="40px" CellStyle-HorizontalAlign="Left">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnView" runat="server" ToolTip="View" CssClass="grid-btn viewDetails-btn"
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnViewBTClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent()"
                                                                    OnClick="btnView_OnClick"></asp:Button>
                                                                <input type="button" class="grid-btn reject-btn" data-id='<%# Eval("BTRegisterID") %>'
                                                                    title="Reject" onclick="CheckOraStatus(this)" data-reject="0" />
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
                                        <div style="float: left; padding: 5px 0 10px; font-style: italic; color: #999;" class="ora-last-update">
                                        </div>
                                        <div class="ora-legend" style="float: right; padding: 5px 0 10px;">
                                            <strong>Oracle Legend:</strong> <span class="ora-new" style="padding: 0 5px; border-right: 1px solid #ccc;">
                                                No Oracle Invoice</span> <span class="ora-rejected" style="padding: 0 5px; border-right: 1px solid #ccc;">
                                                    Error</span> <span class="ora-done" style="padding: 0 5px; border-right: 1px solid #ccc;">
                                                        Done</span> <span class="ora-paid" style="padding: 0 5px; border-right: 1px solid #ccc;">
                                                            Paid</span> <span class="ora-not-found" style="padding: 0 5px;">N/A</span>
                                        </div>
                                        <div style="clear: both">
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="grvBTSubmitted" KeyFieldName="BTRegisterID" ClientInstanceName="grvBTSubmitted"
                                                    runat="server" Theme="Office2010Black" AutoGenerateColumns="false" CssClass="ora-status-grid">
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
                                                                <asp:CheckBox data-id='<%# Eval("BTRegisterID") %>' runat="server" ID="chkSelect"
                                                                    CssClass="chkSelect" onchange="CheckboxChecked(this)" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>--%>
                                                        <dx:GridViewDataColumn Width="25px" Settings-AllowAutoFilter="False" CellStyle-CssClass="id"
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
                                                        <dx:GridViewDataImageColumn Width="80px" CellStyle-HorizontalAlign="Left">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnView" runat="server" ToolTip="View" CssClass="grid-btn viewDetails-btn"
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="btnViewBTClick(this); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent()"
                                                                    OnClick="btnView_OnClick"></asp:Button>
                                                                <input type="button" class="grid-btn approval-btn hide" onclick="$('[id$=btnApprove]').val('Re-Approve').attr('title', 'Re-Approve'); $('[id$=hIsReApprove]').val('T'); confirmApproval(this)"
                                                                    data-id='<%# Eval("BTRegisterID") %>' data-budget-code='<%# Eval("BudgetCodeID") %>'
                                                                    data-employee-code='<%# Eval("EmployeeCode") %>' title="Re-Approve" />
                                                                <input type="button" class="grid-btn reject-btn" data-id='<%# Eval("BTRegisterID") %>'
                                                                    title="reject" onclick="CheckOraStatus(this)" data-reject="1" />
                                                                <input type="button" class="grid-btn ora-error-btn hide" data-id='<%# Eval("BTRegisterID") %>'
                                                                    title="Error" onclick="showErrorOraMessage(this)" data-reject="0" data-message='<%# Eval("ReasonError") %>' />
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataImageColumn>
                                                    </Columns>
                                                    <%--<SettingsDetail ShowDetailRow="true" />--%>
                                                </dx:ASPxGridView>
                                                <%--<div id="Div1" class="action-pan">
                                                    <asp:Button runat="server" Text="Reject" CssClass="btnReject" ID="btnReject" />
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
    <%--reject bt--%>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <div id="tabRejectMessage" runat="server" class="popup-container">
                <asp:HiddenField runat="server" ID="hRejectBTRegisterID" />
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
                                            Reject to Requester to update information
                                        </h3>
                                        <div style="margin-top: 10px; font-weight: bold;">
                                            Recommendation:
                                        </div>
                                        <asp:TextBox ID="txtRejectReason" class="reject-reason" Style="width: auto !important;
                                            margin-top: 5px;" TextMode="MultiLine" Rows="5" Columns="60" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 0 0 15px; text-align: center;">
                                        <asp:Button ID="btnRejectOK" CssClass="btn" runat="server" Text="Reject" OnClientClick="if(!checkReject()) return false; HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent();" />
                                        <input type="button" value="Cancel" onclick="hideRejectMessage(); $('[id$=hRejectBTRegisterID]').val(''); $('[id$=txtRejectReason]').val('')"
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
    <%--approve bt--%>
    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <div id="tabApproveMessage" runat="server" class="popup-container">
                <asp:HiddenField runat="server" ID="hApproveBTRegisterID" />
                <asp:HiddenField runat="server" ID="hApproveEmployeeCode" />
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
                                    <td style="padding: 5px 30px;" colspan="2">
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
                                        <asp:Button ID="btnApprove" ToolTip="Approve" runat="server" CssClass="btn" Text="Approve"
                                            OnClientClick="if(!checkApprove()) return false; HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent();" />
                                        <asp:Button runat="server" Text="Cancel" OnClientClick="hideApproveMessage(); HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent();"
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
    <%--View Error--%>
    <div id="oraErrorContainer" class="no-transition" style="display: none; position: absolute;
        right: 20px; z-index: 1000; background-color: #fff; padding: 10px 20px; border: 1px solid red;
        border-radius: 5px; color: Red; box-shadow: 0 1px 10px #aaa; -webkit-box-shadow: 0 1px 10px #999;
        -moz-box-shadow: 0 1px 10px #aaa; border-radius: 5px; -webkit-border-radius: 5px;
        -moz-border-radius: 5px; line-height: 1.5;">
        <div id="oraErrorDetails" style="max-width: 300px;">
        </div>
        <i style="position: absolute; width: 10px; height: 10px; bottom: -10px; right: 22px;
            background: url(/images/triangle.png) center center no-repeat transparent;">
        </i>
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">

    <script src="/js/jquery/jquery.autocomplete.js" type="text/javascript"></script>

    <%-- Request.QueryString("btid") --%>

    <script type="text/javascript">
        var btid = ''
    </script>

    <script src="/js/fi-advance-mgmt.js" type="text/javascript"></script>

</asp:Content>
