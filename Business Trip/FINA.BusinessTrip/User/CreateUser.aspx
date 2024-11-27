<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CreateUser.aspx.vb" Inherits=".CreateUser"
    MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <style type="text/css">
        .formatColumn
        {
            width: 150px;
            padding-right: 10px;
            text-align: right;
        }
        .formatColumn0
        {
            width: 80px;
            padding-right: 10px;
            text-align: right;
        }
        table.rdlFilterEmp
        {
            margin: 0;
        }
        .rdlFilterEmp td
        {
            padding: 0 10px 0 0;
        }
    </style>

    <script type="text/javascript">
        // <![CDATA[
        var textSeparator = ";";
        function GetSelectedItemsText(items) {
            var texts = [];
            for (var i = 0; i < items.length; i++)
                if (items[i].index != 0)
                texts.push(items[i].text);
            return texts.join(textSeparator);
        }
        function GetValuesByTexts(texts) {
            var actualValues = [];
            var item;
            for (var i = 0; i < texts.length; i++) {
                item = checkListBox.FindItemByText(texts[i]);
                if (item != null)
                    actualValues.push(item.value);
            }
            return actualValues;
        }
        function OnListBoxSelectionChanged(listBox, args) {
            if (args.index == 0)
                args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
            UpdateSelectAllItemState();
            UpdateText();
        }
        function UpdateSelectAllItemState() {
            IsAllSelected() ? checkListBox.SelectIndices([0]) : checkListBox.UnselectIndices([0]);
        }
        function IsAllSelected() {
            var selectedDataItemCount = checkListBox.GetItemCount() - (checkListBox.GetItem(0).selected ? 0 : 1);
            return checkListBox.GetSelectedItems().length == selectedDataItemCount;
        }
        function UpdateText() {
            var selectedItems = checkListBox.GetSelectedItems();
            var texts = GetSelectedItemsText(selectedItems)
            checkComboBox.SetText(texts);
            texts = texts.split(textSeparator);
            var values = GetValuesByTexts(texts);
            $("[id$='hBranch']").val(values)

        }
        function SynchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = GetValuesByTexts(texts);
            checkListBox.SelectValues(values);
            UpdateSelectAllItemState();
            UpdateText(); // for remove non-existing texts    
        }

        // ]]>
    </script>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    Profile &raquo; Create User
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <%--Message Panel--%>
    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="panMessage">
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="ui-accordion ui-widget ui-helper-reset ui-hidden-container" role="tablist">
        <div class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active active ui-corner-top"
                role="tab">
                <span class="ui-icon"></span>User Infomation</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content" role="tabpanel">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="htxtUserName" runat="server" />
                        <fieldset style="border-radius: 10px;">
                            <legend>User Infomation </legend>
                            <table style="margin-left: 80px; width: auto; margin-top: 5px" class="grid-edit"
                                id="tblCreateUser">
                                <tr>
                                    <td style="padding-left: 89px" colspan="4">
                                        &nbsp;<asp:Label runat="server" ID="txtInvalidUser" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formatColumn0">
                                        User Name:
                                    </td>
                                    <td style="width: 80px !important">
                                        <asp:TextBox ID="txtUserName" ReadOnly="true" runat="server" TextMode="SingleLine"
                                            Style="width: 111px !important" Font-Bold="True" MaxLength="6"></asp:TextBox>
                                        <span runat="server" id="btnSearchEmpInfo" style="margin-left: 3px; text-align: center;
                                            margin-top: -11px; display: inline-block; position: relative; top: 11px; padding-bottom: 5px;"
                                            onclick="showChooseEmployee(this)" class="btn inform add-btn"><i class="search">
                                            </i><span runat="server" id="loadText">Load</span></span>
                                        <%-- <span class="btn inform" id="btnSearchEmpInfo" runat="server" style="margin-left: 3px;
                                            text-align: center; margin-top: -11px; display: inline-block; position: relative;   
                                            top: 11px; padding-bottom: 5px;"><asp:Button ID="btnGetUserInfo" CssClass="btnSearch" runat="server" Text="Load"  OnClientClick="showChooseEmployeeMessage(this);" />
                                            <i class="search"></i><span runat="server" id="loadText">Load</span></span>--%>
                                        <asp:Button ID="btnGetUserInfo" CssClass="btnSearch hide" runat="server" Text="Load" />
                                    </td>
                                    <td class="formatColumn">
                                        Full Name:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFullName" runat="server" TextMode="SingleLine" Font-Bold="True"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formatColumn0" style="vertical-align: middle">
                                        Branch Management:
                                    </td>
                                    <td class="ddl-mutiselect">
                                        <asp:HiddenField ID="hBranch" runat="server" />
                                        <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ddlBranch" Width="210px"
                                            runat="server" EnableAnimation="False">
                                            <DropDownWindowStyle BackColor="#EDEDED" />
                                            <DropDownWindowTemplate>
                                                <dx:ASPxListBox Width="100%" ID="ddlBranch" ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                                                    runat="server">
                                                    <Border BorderStyle="None" />
                                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
                                                    <ClientSideEvents SelectedIndexChanged="OnListBoxSelectionChanged" />
                                                </dx:ASPxListBox>
                                                <table style="width: 100%" cellspacing="0" cellpadding="4">
                                                    <tr>
                                                        <td align="right">
                                                            <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close">
                                                                <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </DropDownWindowTemplate>
                                            <ClientSideEvents TextChanged="SynchronizeListBoxValues" DropDown="SynchronizeListBoxValues" />
                                        </dx:ASPxDropDownEdit>
                                    </td>
                                    <td class="formatColumn">
                                        Division:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDivision" runat="server" TextMode="SingleLine" Font-Bold="True"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formatColumn0">
                                        Department:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDepartment" runat="server" TextMode="SingleLine" Font-Bold="True"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td class="formatColumn">
                                        Section:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSection" runat="server" TextMode="SingleLine" Font-Bold="True"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formatColumn0">
                                        Email:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmailAddress" runat="server" TextMode="SingleLine" Font-Bold="True"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td class="formatColumn">
                                        Title:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTitle" runat="server" TextMode="SingleLine" Font-Bold="True"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="hide_pass">
                                    <td class="formatColumn0">
                                        <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:Label runat="server" ID="txtMesPass" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </td>
                                    <td class="formatColumn">
                                        <asp:Label ID="lblRePassword" runat="server" Text="RePassword:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRePassword" runat="server" TextMode="Password"></asp:TextBox>
                                        <asp:Label runat="server" ID="txtMesRePass" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="formatColumn0">
                                        Role Type:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRoleType" runat="server">
                                        </asp:DropDownList>
                                        <asp:Label runat="server" ID="txtMesRoleType" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </td>
                                    <%--<td class="formatColumn">
                                        Role Level:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRoleLevel" runat="server" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:Label runat="server" ID="txtMesRoleLevel" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </td>--%>
                                    <td>
                                    </td>
                                    <td>
                                        <span id="Span1" style="position: relative; text-align: center; margin: 0"
                                            onclick="showPopupMapUserID(this)" class="btn inform add-btn"><i class="add"></i>
                                            Map Oracle User</span> <span id="Span2" style="position: relative; margin: 0;
                                                text-align: center; padding: 6px 7px 6px 8px; color: Red" class="btn inform add-btn">Remove
                                                Map
                                                <asp:Button runat="server" ID="btnClearOraMap" />
                                            </span>
                                    </td>
                                </tr>
                                <tr runat="server" id="hChangePass">
                                    <td>
                                    </td>
                                    <td colspan="3">
                                        <span class="check-button" style="top: 2px;">
                                            <asp:CheckBox ID="chkChangePass" runat="server" Checked="True" />
                                            <span></span></span>User must change password at next logon
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <span class="check-button" style="top: 2px;">
                                            <asp:CheckBox ID="chkAccount" runat="server" />
                                            <span></span></span>Account is disabled
                                    </td>
                                    <td class="formatColumn0">
                                        Oracle User:
                                    </td>
                                    <td style="padding: 0;">
                                        <asp:TextBox ID="txtMapUserID" runat="server" TextMode="SingleLine" ReadOnly="true"
                                            Style="width: 62px !important; margin: 0;"></asp:TextBox>
                                        <asp:TextBox runat="server" ID="txtMapUserNameOra" TextMode="SingleLine" ReadOnly="true"
                                            Style="width: 123px !important; margin: 0;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="hide">
                                    <td>
                                    </td>
                                    <td>
                                        <span class="check-button" style="top: 2px;">
                                            <asp:CheckBox ID="chkIsCreditCard" runat="server" />
                                            <span></span></span>Credit Card
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <fieldset style="border-radius: 10px;">
                            <legend>Employees Management List</legend>
                            <div style="text-align: Left; margin-top: 10px">
                                <span id="btnAdd" style="margin-top: 10px; text-align: center;" onclick="showChooseEmployeeMessage(this)"
                                    class="btn inform add-btn"><i class="add"></i>Add</span> <span style="margin-top: 10px;
                                        text-align: center;" class="btn inform add-btn" onclick="showMessageRemove()"><i
                                            class="remove"></i>Remove</span>
                                <asp:Button ID="btnReject" CssClass="btn secondary hide" runat="server" Text="Remove" />
                            </div>
                            <dx:ASPxGridView ID="grvTimeSheet" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                            <input type="checkbox" class="chkSelect" name="chkRemoveEmployee" value='<%# Eval("EmployeeCode")%>' />
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Width="10px" FieldName="No" Caption="No" />
                                    <dx:GridViewDataColumn Width="50px" FieldName="EmployeeCode" Caption="Code" />
                                    <dx:GridViewDataColumn Width="90px" FieldName="EmployeeName" Caption="Name" />
                                    <dx:GridViewDataColumn Width="150px" FieldName="DivisionName" Caption="Division" />
                                    <dx:GridViewDataColumn Width="120px" FieldName="DepartmentName" Caption="Department" />
                                    <dx:GridViewDataColumn Width="150px" FieldName="SectionName" Caption="Section/Group/Team" />
                                    <%-- <dx:GridViewDataColumn FieldName="Note" Caption="Description" />--%>
                                </Columns>
                            </dx:ASPxGridView>
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="action-pan">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Button CssClass="btn" OnClientClick="HandleMessage(this)" ID="btnCreateUser"
                                Text="Save" runat="server" />
                            <asp:Button Style="margin-left: 5px" CssClass="btn secondary" ID="btnCancel" Text="Cancel"
                                runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div id="tabChooseEmployee" class="popup-container">
        <asp:HiddenField runat="server" ID="hRejectBTRegisterID" />
        <asp:HiddenField ID="hRejectTypeID" runat="server" />
        <div style="margin: 150px 50px 50px; padding: 10px; background-color: #fff; overflow: auto;
            max-height: 80%; border-radius: 5px; box-shadow: 0 0 10px #fff; -webkit-box-shadow: 0 0 10px #fff;
            -moz-box-shadow: 0 0 10px #fff;">
            <fieldset>
                <legend>Choose Employee </legend>
                <table style="width: 100%; margin: 0">
                    <tr>
                        <td class="radio-button" style="padding: 5px 0 10px 15px;">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:RadioButtonList CssClass="rdlFilterEmp" ID="rdlFilterEmp" RepeatDirection="Horizontal"
                                        RepeatLayout="Table" runat="server" AutoPostBack="True">
                                        <asp:ListItem>All</asp:ListItem>
                                        <asp:ListItem>Division</asp:ListItem>
                                        <asp:ListItem Selected="True">Department</asp:ListItem>
                                    </asp:RadioButtonList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle; border: none !important; padding: 0 !important;">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <dx:ASPxGridView ID="grvChoose" runat="server" SettingsPager-Mode="ShowAllRecords"
                                        CssClass="scrolling-table" Theme="Office2010Black" AutoGenerateColumns="false"
                                        Width="96%" Style="margin: auto">
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
                                                <HeaderTemplate>
                                                    <input type="checkbox" class="chkAll" onchange="CheckAll(this)" />
                                                </HeaderTemplate>
                                                <DataItemTemplate>
                                                    <input type="checkbox" class="chkSelect" name="chkSelectEmployee" value='<%# Eval("EmployeeCode") + "/" + Eval("EmployeeName") %>' />
                                                </DataItemTemplate>
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Width="80px" FieldName="EmployeeCode" Caption="Code" />
                                            <dx:GridViewDataColumn Width="120px" FieldName="EmployeeName" Caption="Name" />
                                            <dx:GridViewDataColumn Width="150px" FieldName="DivisionName" Caption="Division" />
                                            <dx:GridViewDataColumn Width="150px" FieldName="DepartmentName" Caption="Department" />
                                            <dx:GridViewDataColumn FieldName="SectionName" Caption="Section/Group/Team" />
                                        </Columns>
                                    </dx:ASPxGridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; padding-top: 10px">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnChooseOK" OnClientClick="hideRejectMessage()" CssClass="btn" runat="server"
                                        Text="Choose" />
                                    <input type="button" value="Cancel" onclick="hideRejectMessage()" style="margin-left: 5px;"
                                        id="btnRejectCancel" class="btn secondary" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
    </div>
    <%--    choose popup MapUserID--%>
    <div id="tabShowPopupMapUserId" class="popup-container">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <div style="margin: 80px 50px; padding: 10px; background-color: #fff; overflow: auto;
                    max-height: 80%; border-radius: 5px; box-shadow: 0 0 10px #fff; -webkit-box-shadow: 0 0 10px #fff;
                    -moz-box-shadow: 0 0 10px #fff;">
                    <fieldset>
                        <legend>Search Employee</legend>
                        <table style="width: 100%; margin: 0" class="grid-edit">
                            <tr>
                                <td class="ui-panelgrid-cell">
                                    <label style="margin-right: -6px">
                                        UserName:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMapUserName" runat="server" TextMode="SingleLine" Font-Bold="True"></asp:TextBox>
                                </td>
                                <td class="ui-panelgrid-cell">
                                    <label style="margin-right: -20px; margin-left: 40px">
                                        Description:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMapUserDescription" runat="server" TextMode="SingleLine" Font-Bold="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnMapUserSearch" CssClass="btn" runat="server" Text="Search" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset>
                        <legend>Choose Employee in Oracle </legend>
                        <table>
                            <tr>
                                <td style="vertical-align: middle; border: none !important; padding: 0 !important;">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:HiddenField runat="server" ID="hchooseEmpMapOraID" />
                                            <asp:HiddenField runat="server" ID="hchooseEmpMapOraName" />
                                            <dx:ASPxGridView ID="grvMapUser" runat="server" SettingsPager-Mode="ShowAllRecords"
                                                CssClass="scrolling-table grid-radio" Theme="Office2010Black" AutoGenerateColumns="false"
                                                Width="96%" Style="margin: auto">
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
                                                            <input type="radio" value='<%# Eval("USER_ID") %>-<%# Eval("USER_NAME") %>' name="rdChooseEmployee"
                                                                cssclass="rdSelect" onchange="CheckMapUserID(this)" />
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Width="150px" FieldName="USER_ID" Caption="Code" />
                                                    <dx:GridViewDataColumn Width="220px" FieldName="USER_NAME" Caption="Name" />
                                                    <dx:GridViewDataColumn FieldName="DESCRIPTION" Caption="Division" />
                                                </Columns>
                                            </dx:ASPxGridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; padding-top: 10px">
                                    <asp:Button ID="btnChooseEmpOra" OnClientClick="HandleMessage(this); HideEmployeeMapOra()"
                                        CssClass="btn" runat="server" Text="Choose" />
                                    <input type="button" value="Cancel" onclick="HideEmployeeMapOra()" style="margin-left: 5px;"
                                        id="Button5" class="btn secondary" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--  Choose Employee--%>
    <div id="tabChoosemployee" class="popup-container">
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <div style="margin: 80px 50px; padding: 10px; background-color: #fff; overflow: auto;
                    max-height: 80%; border-radius: 5px; box-shadow: 0 0 10px #fff; -webkit-box-shadow: 0 0 10px #fff;
                    -moz-box-shadow: 0 0 10px #fff;">
                    <fieldset>
                        <legend>Search Employee</legend>
                        <table style="width: 100%; margin: 0" class="grid-edit">
                            <tr>
                                <td class="ui-panelgrid-cell">
                                    <label style="margin-right: -6px">
                                        Employee Code:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmpCodeSearch" runat="server" TextMode="SingleLine" Font-Bold="True"></asp:TextBox>
                                </td>
                                <td class="ui-panelgrid-cell">
                                    <label style="margin-right: -20px; margin-left: 40px">
                                        Employee Name:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmpNameSearch" runat="server" TextMode="SingleLine" Font-Bold="True"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearchEmployee" CssClass="btn" runat="server" Text="Search" />
                                </td>
                            </tr>
                            <tr>
                                <td class="ui-panelgrid-cell">
                                    <label>
                                        Division</label>
                                </td>
                                <td class="ui-panelgrid-cell" style="width: 200px">
                                    <asp:DropDownList ID="cboDivision" runat="server" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hDivision" />
                                </td>
                                <td class="ui-panelgrid-cell">
                                    <label style="margin-right: -20px; margin-left: 40px">
                                        Department</label>
                                </td>
                                <td class="ui-panelgrid-cell">
                                    <asp:DropDownList ID="cboDept" runat="server">
                                    </asp:DropDownList>
                                    <asp:HiddenField runat="server" ID="hDept" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <fieldset>
                        <legend>Choose Employee </legend>
                        <table>
                            <tr>
                                <td style="vertical-align: middle; border: none !important; padding: 0 !important;">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:HiddenField runat="server" ID="hchooseEmpID" />
                                            <dx:ASPxGridView ID="grvChooseEmployee" runat="server" SettingsPager-Mode="ShowAllRecords"
                                                CssClass="scrolling-table grid-radio" Theme="Office2010Black" AutoGenerateColumns="false"
                                                Width="96%" Style="margin: auto">
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
                                                            <input type="radio" value='<%# Eval("EmployeeCode") %>' name="rdChooseEmployee" cssclass="rdSelect"
                                                                onchange="CheckRdChecked(this)" />
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                    <dx:GridViewDataColumn Width="80px" FieldName="EmployeeCode" Caption="Code" />
                                                    <dx:GridViewDataColumn Width="120px" FieldName="EmployeeName" Caption="Name" />
                                                    <dx:GridViewDataColumn Width="150px" FieldName="DivisionName" Caption="Division" />
                                                    <dx:GridViewDataColumn Width="150px" FieldName="DepartmentName" Caption="Department" />
                                                    <dx:GridViewDataColumn FieldName="SectionName" Caption="Section/Group/Team" />
                                                </Columns>
                                            </dx:ASPxGridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; padding-top: 10px">
                                    <asp:Button ID="btnChooseEmp" OnClientClick="HandleMessage(this); hideChooseEmployee()"
                                        CssClass="btn" runat="server" Text="Choose" />
                                    <input type="button" value="Cancel" onclick="hideChooseEmployee()" style="margin-left: 5px;"
                                        id="Button2" class="btn secondary" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="ScriptContent" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">

    <script src="/js/jquery/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">

        function showChooseEmployeeMessage(me) {
            if ($("[id$='btnGetUserInfo']").val() == 'Load') {
                ShowErrorMessage("Please choose employee!")
                return
            }
            var $this = $(me)
            $("#tabChooseEmployee").stop().fadeIn(100)
        }

        function showPopupMapUserID(me) {
            var $this = $(me)
            $("[id$='hchooseEmpMapOraID']").val($this.attr("data-id"))
            $("#tabShowPopupMapUserId").stop().fadeIn(100)
            //            $("[id$=hUserMapID]").val($(me).attr("data-reject"))
        }


        function showMessageRemove() {
            ShowConfirmMessage({
                message: "Do you want to remove these records?",
                OK: function() {
                    $("[id$=btnReject]").click()
                }
            })
        }

        function hideRejectMessage() {

            $("[id $='hRejectBTRegisterID']").val("")
            $("#tabChooseEmployee").stop().fadeOut(100, function() {
                // $("tr.selected").removeClass("selected")
            })
        }


        function showChooseEmployee(me) {
            var $this = $(me)
            $("[id$='hChooseEmployeeID']").val($this.attr("data-id"))
            //$this.parent().parent().addClass("selected")
            $("#tabChoosemployee").stop().fadeIn(100)
            $("[id$=hChooseEmployeeID]").val($(me).attr("data-reject"))
        }


        function hideChooseEmployee() {

            $("[id $='hChooseEmployeeID']").val("")
            $("#tabChoosemployee").stop().fadeOut(100, function() {
            })
        }

        // hide Employee in Oracle
        function HideEmployeeMapOra() {
            //            $("[id $='hChooseEmpOraID']").val("")
            $("#tabShowPopupMapUserId").stop().fadeOut(100, function() {
                // $("tr.selected").removeClass("selected")
            })
        }

        function CheckRdChecked(me) {
            $(".grid-radio tr.selected").removeClass("selected")
            var $radio = $(me)
            if (!$radio.is(":radio")) {
                $radio = $(me).find(":radio")

            }
            var checked = $radio.prop("checked")
            var $itemRow = $(me).parent().parent()
            if (checked) {
                $("[id$=hchooseEmpID]").val($radio.val())
                $itemRow.addClass("selected")
            }
            //  alert($("[id$=hchooseEmpID]").val())
        }

        function CheckMapUserID(me) {
            $(".grid-radio tr.selected").removeClass("selected")
            var $radio = $(me)
            if (!$radio.is(":radio")) {
                $radio = $(me).find(":radio")
            }
            var checked = $radio.prop("checked")
            var $itemRow = $(me).parent().parent()
            if (checked) {
                var obj = $radio.val()
                $("[id$=hchooseEmpMapOraID]").val(obj.split('-')[0])
                $("[id$=hchooseEmpMapOraName]").val(obj.split('-')[1])
                $itemRow.addClass("selected")
            }
            //  alert($("[id$=hchooseEmpID]").val())
        }

    </script>

</asp:Content>
