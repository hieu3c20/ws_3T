<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MasterData.aspx.vb" Inherits="MasterData"
    MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <style type="text/css">
        table.dxgvTable_Office2010Black
        {
            table-layout: fixed;
        }
    </style>

    <script type="text/javascript">
        // Long them  
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
            $("[id$='hTitleIDs']").val(values)

        }

        function SynchronizeListBoxValues(dropDown, args) {
            checkListBox.UnselectAll();
            var texts = dropDown.GetText().split(textSeparator);
            var values = GetValuesByTexts(texts);
            checkListBox.SelectValues(values);
            UpdateSelectAllItemState();
            UpdateText(); // for remove non-existing texts    
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    Master Data Management
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <img src="images/inform-btn.png" style="display: none" alt="" />
    <img src="images/check-check.png" style="display: none" alt="" />
    <img src="images/check.png" style="display: none" alt="" />
    <img src="images/rad-check.png" style="display: none" alt="" />
    <img src="images/rad.png" style="display: none" alt="" />
    <%--Message Panel--%>
    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="panMessage">
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Master Data--%>
    <div class="ui-accordion ui-widget ui-helper-reset ui-hidden-container" role="tablist">
        <%--System Parameter--%>
        <div runat="server" id="divSystemParameter" class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                role="tab">
                <span class="ui-icon"></span>System Parameters</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <span id="btnAddOneDay" style="margin-top: 10px; text-align: center;" onclick="btnAddSub_Click(this)"
                    class="btn inform add-btn"><i class="add"></i>Add</span>
                <div class="ui-datatable ui-widget">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <dx:ASPxGridView ID="grvSystem" runat="server" Style="table-layout: fixed;" Theme="Office2010Black"
                                AutoGenerateColumns="false">
                                <SettingsText EmptyDataRow="No records found!" />
                                <Settings ShowFilterRow="true" />
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
                                    <dx:GridViewDataColumn CellStyle-CssClass="id" Width="50px" FieldName="ID" Caption="ID"
                                        Settings-AllowAutoFilter="False" />
                                    <dx:GridViewDataColumn Width="130px" FieldName="Code" Caption="Code" />
                                    <dx:GridViewDataColumn Width="180px" FieldName="Value" Caption="Display Value" />
                                    <dx:GridViewDataColumn FieldName="Text" Caption="Display Text" />
                                    <dx:GridViewDataColumn Width="80px" FieldName="Status" Caption="Status" />
                                    <dx:GridViewDataImageColumn Width="40px" Caption="">
                                        <DataItemTemplate>
                                            <asp:Button ID="btnEditSystem" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                data-id='<%# Eval("ID") %>' OnClientClick="btnEditClick(this, 'hSystemID'); HandleMessage($('[id$=btnSaveSystem]')[0]); bindStartupEvents($('[id$=btnSaveSystem]')[0]);"
                                                OnClick="btnEditSystem_OnClick"></asp:Button>
                                            <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("ID") %>' onclick="btnDeleteClick(this, 'hSystemID')" />
                                            <asp:Button ID="btnDeleteSystem" data-id='<%# Eval("ID") %>' ToolTip="Delete" runat="server"
                                                CssClass="hide" OnClick="btnDeleteSystem_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveSystem]')[0]); bindStartupEvents($('[id$=btnSaveSystem]')[0])">
                                            </asp:Button>
                                        </DataItemTemplate>
                                    </dx:GridViewDataImageColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <fieldset class="add-edit-form">
                    <legend><span class="add-edit-action">Add</span> System Parameter</legend>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hSystemID" />
                            <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                <tbody>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Code <span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtCodeSys" runat="server" Style="width: 210px !important"></asp:TextBox>
                                        </td>
                                        <td role="gridcell" class="ui-panelgrid-cell">
                                            <label>
                                                Display Value <span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtValueSys" runat="server" Style="width: 210px  !important"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Display Text <span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtTextSys" runat="server" Style="width: 210px !important" TextMode="MultiLine"
                                                Rows="5"></asp:TextBox>
                                        </td>
                                        <td role="gridcell" class="ui-panelgrid-cell">
                                            <label>
                                                Display Order <span class="required">*</span></label>
                                        </td>
                                        <td class="spin-edit ui-panelgrid-cell">
                                            <dx:ASPxSpinEdit ID="speOrder" runat="server" Number="0" MinValue="0" NumberType="Integer"
                                                MaxValue="50">
                                            </dx:ASPxSpinEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="padding-left: 9px;">
                                            <span class="check-button" style="top: 2px;">
                                                <asp:CheckBox ID="chkActive" runat="server" Checked="True" />
                                                <span></span></span>Active
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="action-pan">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:Button runat="server" ID="btnSaveSystem" Text="Save" CssClass="btn" OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                <asp:Button runat="server" ID="btnCancelSystem" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                    OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </fieldset>
            </div>
        </div>
        <%--Destination--%>
        <div runat="server" id="divDestination" class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                role="tab">
                <span class="ui-icon"></span>Country &amp; Destination</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="HRTabControl">
                    <div class="HRTabNav">
                        <ul>
                            <li>Country Group</li>
                            <li>Country</li>
                            <li>Destination Group</li>
                            <li>Destination (City or Province)</li>
                        </ul>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <div class="HRTabList" style="padding: 10px; border: 1px solid #ccc; border-top: none;">
                        <%--Country Group--%>
                        <div class="HRTab no-transition">
                            <span id="Span13" style="text-align: center;" onclick="btnAddSub_Click(this)" class="btn inform add-btn">
                                <i class="add"></i>Add</span>
                            <div class="ui-datatable ui-widget">
                                <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="grvCountryGroup" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Width="50px" FieldName="ID" Caption="ID"
                                                    Settings-AllowAutoFilter="False" />
                                                <dx:GridViewDataColumn Width="150px" FieldName="GroupName" Caption="Group Name" />
                                                <dx:GridViewDataColumn FieldName="Description" Caption="Description" />
                                                <dx:GridViewDataColumn Width="80px" FieldName="Status" Caption="Status" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                            data-id='<%# Eval("ID") %>' OnClientClick="btnEditClick(this, 'hCountryGroupID'); HandleMessage($('[id$=btnSaveCountryGroup]')[0]); bindStartupEvents($('[id$=btnSaveCountryGroup]')[0])"
                                                            OnClick="btnEditCountryGroup_OnClick"></asp:Button>
                                                        <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("ID") %>' onclick="btnDeleteClick(this, 'hCountryGroupID')" />
                                                        <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' ToolTip="Delete" runat="server"
                                                            CssClass="hide" OnClick="btnDeleteCountryGroup_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveCountryGroup]')[0]); bindStartupEvents($('[id$=btnSaveCountryGroup]')[0])">
                                                        </asp:Button>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <fieldset class="add-edit-form">
                                <legend><span class="add-edit-action"></span>&nbsp;Country Group Info</legend>
                                <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hCountryGroupID" />
                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                            <tbody>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Name <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtCountryGroupName" runat="server" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Description</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" rowspan="2">
                                                        <asp:TextBox ID="txtCountryGroupDes" runat="server" TextMode="MultiLine" Rows="3"
                                                            Style="width: 340px !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td style="padding-left: 9px;">
                                                        <span class="check-button" style="top: 2px;">
                                                            <asp:CheckBox ID="ChkCountryGroup" runat="server" Checked="True" />
                                                            <span></span></span>Active
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <ul id="ulmCountryGroupMessage" style="padding-left: 120px" class="error-summary">
                                </ul>
                                <div class="action-pan">
                                    <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                                        <ContentTemplate>
                                            <input type="button" value="Save" class="btn" onclick="check_mCountryGroup(this)" />
                                            <asp:Button runat="server" ID="btnSaveCountryGroup" Text="Save" CssClass="btn hide"
                                                OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                            <asp:Button runat="server" ID="btnCancelCountryGroup" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                        </div>
                        <%--Country--%>
                        <div class="HRTab no-transition">
                            <span id="Span16" style="text-align: center;" onclick="btnAddSub_Click(this)" class="btn inform add-btn">
                                <i class="add"></i>Add</span>
                            <div class="ui-datatable ui-widget">
                                <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="grvCountry" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Width="50px" FieldName="ID" Caption="ID"
                                                    Settings-AllowAutoFilter="False" />
                                                <dx:GridViewDataColumn FieldName="GroupName" Caption="Group" />
                                                <dx:GridViewDataColumn FieldName="Code" Caption="Country Code" />
                                                <dx:GridViewDataColumn FieldName="Name" Caption="Country Name" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                            data-id='<%# Eval("ID") %>' OnClientClick="btnEditClick(this, 'hCountryID'); HandleMessage($('[id$=btnSaveCountry]')[0]); bindStartupEvents($('[id$=btnSaveCountry]')[0])"
                                                            OnClick="btnEditCountry_OnClick"></asp:Button>
                                                        <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("ID") %>' onclick="btnDeleteClick(this, 'hCountryID')" />
                                                        <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' ToolTip="Delete" runat="server"
                                                            CssClass="hide" OnClick="btnDeleteCountry_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveCountry]')[0]); bindStartupEvents($('[id$=btnSaveCountry]')[0])">
                                                        </asp:Button>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <fieldset class="add-edit-form">
                                <legend><span class="add-edit-action"></span>&nbsp;Country Info</legend>
                                <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hCountryID" />
                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                            <tbody>
                                                <tr>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Group</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:DropDownList ID="ddlCountryGroup" runat="server" Style="width: 233px !important">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Code <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtCountryCode" runat="server" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Name <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" rowspan="2">
                                                        <asp:TextBox ID="txtCountryName" runat="server" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <ul id="ulCountryMessage" style="padding-left: 120px" class="error-summary">
                                </ul>
                                <div class="action-pan">
                                    <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                                        <ContentTemplate>
                                            <input type="button" value="Save" class="btn" onclick="check_Country(this)" />
                                            <asp:Button runat="server" ID="btnSaveCountry" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                            <asp:Button runat="server" ID="btnCancelCountry" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                        </div>
                        <%--Destination Group--%>
                        <div class="HRTab no-transition">
                            <span id="Span6" style="text-align: center;" onclick="btnAddSub_Click(this)" class="btn inform add-btn">
                                <i class="add"></i>Add</span>
                            <div class="ui-datatable ui-widget">
                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="grvDesGroup" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Width="50px" FieldName="GroupID" Caption="ID"
                                                    Settings-AllowAutoFilter="False" />
                                                <dx:GridViewDataColumn Width="150px" FieldName="GroupName" Caption="Group Name" />
                                                <dx:GridViewDataColumn FieldName="Note" Caption="Description" />
                                                <dx:GridViewDataColumn Width="80px" FieldName="Status" Caption="Status" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                            data-id='<%# Eval("GroupID") %>' OnClientClick="btnEditClick(this, 'hDestinationGroupID'); HandleMessage($('[id$=btnSaveDesGroup]')[0]); bindStartupEvents($('[id$=btnSaveDesGroup]')[0])"
                                                            OnClick="btnEditDesGroup_OnClick"></asp:Button>
                                                        <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("GroupID") %>'
                                                            onclick="btnDeleteClick(this, 'hDestinationGroupID')" />
                                                        <asp:Button ID="btnDelete" data-id='<%# Eval("GroupID") %>' ToolTip="Delete" runat="server"
                                                            CssClass="hide" OnClick="btnDeleteDesGroup_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveDesGroup]')[0]); bindStartupEvents($('[id$=btnSaveDesGroup]')[0])">
                                                        </asp:Button>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <fieldset class="add-edit-form">
                                <legend><span class="add-edit-action"></span>&nbsp;Destination Group Info</legend>
                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hDestinationGroupID" />
                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                            <tbody>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Name <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtDesGroupName" runat="server" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Description</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" rowspan="2">
                                                        <asp:TextBox ID="txtDesGroupDescription" runat="server" TextMode="MultiLine" Rows="3"
                                                            Style="width: 340px !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td style="padding-left: 9px;">
                                                        <span class="check-button" style="top: 2px;">
                                                            <asp:CheckBox ID="chkGroupStatus" runat="server" Checked="True" />
                                                            <span></span></span>Active
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <ul id="ulmDesGroupMessage" style="padding-left: 120px" class="error-summary">
                                </ul>
                                <div class="action-pan">
                                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                        <ContentTemplate>
                                            <input type="button" value="Save" class="btn" onclick="check_mDestinationGroup(this)" />
                                            <asp:Button runat="server" ID="btnSaveDesGroup" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                            <asp:Button runat="server" ID="btnCancelDesGroup" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                        </div>
                        <%--Destination (City or province)--%>
                        <div class="HRTab no-transition">
                            <span id="Span3" style="text-align: center;" onclick="btnAddSub_Click(this)" class="btn inform add-btn">
                                <i class="add"></i>Add</span>
                            <div class="ui-datatable ui-widget">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="grvDestination" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Width="50px" FieldName="DestinationID"
                                                    Caption="ID" Settings-AllowAutoFilter="False" />
                                                <dx:GridViewDataColumn Width="150px" FieldName="DestinationName" Caption="Destination Name" />
                                                <dx:GridViewDataColumn FieldName="Country" Width="150px" Caption="Country" />
                                                <dx:GridViewDataColumn FieldName="GroupName" Width="150px" Caption="Group" />
                                                <dx:GridViewDataColumn FieldName="Note" Caption="Description" />
                                                <dx:GridViewDataColumn FieldName="Status" Caption="Status" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                            data-id='<%# Eval("DestinationID") %>' OnClientClick="btnEditClick(this, 'hDestinationID'); HandleMessage($('[id$=btnSaveDestination]')[0]); bindStartupEvents($('[id$=btnSaveDestination]')[0])"
                                                            OnClick="btnEditDestination_OnClick"></asp:Button>
                                                        <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("DestinationID") %>'
                                                            onclick="btnDeleteClick(this, 'hDestinationID')" />
                                                        <asp:Button ID="btnDelete" data-id='<%# Eval("DestinationID") %>' ToolTip="Delete"
                                                            runat="server" CssClass="hide" OnClick="btnDeleteDestination_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveDestination]')[0]); bindStartupEvents($('[id$=btnSaveDestination]')[0])">
                                                        </asp:Button>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <fieldset class="add-edit-form">
                                <legend><span class="add-edit-action"></span>&nbsp;Destination Info</legend>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hDestinationID" />
                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                            <tbody>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Country <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:DropDownList ID="cboCountry" runat="server" Style="width: 233px !important">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Group <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:DropDownList ID="ddlDesGroup" runat="server" Style="width: 233px !important">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Destination <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtaDesName" runat="server" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Description</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" rowspan="2">
                                                        <asp:TextBox ID="txtaDesNote" runat="server" TextMode="MultiLine" Rows="3" Style="width: 340px !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td style="padding-left: 9px;">
                                                        <span class="check-button" style="top: 2px;">
                                                            <asp:CheckBox ID="chkDestination" runat="server" Checked="True" />
                                                            <span></span></span>Active
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <ul id="ulmCountryMessage" style="padding-left: 120px" class="error-summary">
                                </ul>
                                <div class="action-pan">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                        <ContentTemplate>
                                            <input type="button" value="Save" class="btn" onclick="check_mCountry(this)" />
                                            <asp:Button runat="server" ID="btnSaveDestination" Text="Save" CssClass="btn hide"
                                                OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                            <asp:Button runat="server" ID="btnCancelDestination" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--Title Group--%>
        <div runat="server" id="divTitleGroup" class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                role="tab">
                <span class="ui-icon"></span>Title Group</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <span id="Span7" style="margin-top: 10px; text-align: center;" onclick="btnAddSub_Click(this)"
                    class="btn inform add-btn"><i class="add"></i>Add</span>
                <div class="ui-datatable ui-widget">
                    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hTitleIDs" runat="server" />
                            <dx:ASPxGridView ID="grvTitleGroup" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                    <dx:GridViewDataColumn CellStyle-CssClass="id" Width="50px" FieldName="TitleGroupID"
                                        Caption="ID" Settings-AllowAutoFilter="False" />
                                    <dx:GridViewDataColumn Width="150px" FieldName="Name" Caption="Name" />
                                    <dx:GridViewDataColumn Width="150px" FieldName="GroupTitle" Caption="Group Title Name" />
                                    <dx:GridViewDataColumn FieldName="Note" Caption="Description" />
                                    <dx:GridViewDataColumn Width="80px" FieldName="Status" Caption="Status" />
                                    <dx:GridViewDataImageColumn Width="40px">
                                        <DataItemTemplate>
                                            <asp:Button ID="btnEdit" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                data-id='<%# Eval("TitleGroupID") %>' OnClientClick="btnEditClick(this, 'hTitleGroupID'); HandleMessage($('[id$=btnSaveTitleGroup]')[0]); bindStartupEvents($('[id$=btnSaveTitleGroup]')[0])"
                                                OnClick="btnEditTitleGroup_OnClick"></asp:Button>
                                            <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("TitleGroupID") %>'
                                                onclick="btnDeleteClick(this, 'hTitleGroupID')" />
                                            <asp:Button ID="btnDelete" data-id='<%# Eval("TitleGroupID") %>' ToolTip="Delete"
                                                runat="server" CssClass="hide" OnClick="btnDeleteTitleGroup_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveTitleGroup]')[0]); bindStartupEvents($('[id$=btnSaveTitleGroup]')[0])">
                                            </asp:Button>
                                        </DataItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </dx:GridViewDataImageColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <fieldset class="add-edit-form">
                    <legend><span class="add-edit-action"></span>&nbsp;Title Group Info</legend>
                    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hTitleGroupID" />
                            <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                <tbody>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Name <span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtTitleGroupName" runat="server" Style="width: 210px !important"></asp:TextBox>
                                        </td>
                                        <td role="gridcell" class="ui-panelgrid-cell">
                                            <label>
                                                Titles <span class="required">*</span></label>
                                        </td>
                                        <td class="ddl-mutiselect" style="padding-left: 10px">
                                            <asp:HiddenField ID="hGroupTitle" runat="server" />
                                            <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ddlTitle" Width="210px"
                                                runat="server" EnableAnimation="False">
                                                <DropDownWindowStyle BackColor="#EDEDED" />
                                                <DropDownWindowTemplate>
                                                    <dx:ASPxListBox Width="100%" ID="ddlTitle" ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
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
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="padding-left: 9px;">
                                            <span class="check-button" style="top: 2px;">
                                                <asp:CheckBox ID="chkTitleGroup" runat="server" Checked="True" />
                                                <span></span></span>Active
                                        </td>
                                        <td role="gridcell" class="ui-panelgrid-cell">
                                            <label>
                                                Description</label>
                                        </td>
                                        <td class="ui-panelgrid-cell" rowspan="2">
                                            <asp:TextBox ID="txtTitleGroupNote" runat="server" TextMode="MultiLine" Rows="3"
                                                Style="width: 340px !important"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <ul id="ulmTitleGroupMessage" style="padding-left: 120px" class="error-summary">
                    </ul>
                    <div class="action-pan">
                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                            <ContentTemplate>
                                <input type="button" value="Save" class="btn" onclick="check_mTitleGroup(this)" />
                                <asp:Button runat="server" ID="btnSaveTitleGroup" Text="Save" CssClass="btn hide"
                                    OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                <asp:Button runat="server" ID="btnCancelTitleGroup" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                    OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </fieldset>
            </div>
        </div>
        <%--Business Trip Policy--%>
        <div runat="server" id="divBTPolicy" class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                role="tab">
                <span class="ui-icon"></span>Business Trip Policy</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="HRTabControl">
                    <div class="HRTabNav">
                        <ul>
                            <li>Advance &amp; Expense Policy</li>
                            <li>Moving Time Allowance</li>
                        </ul>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <div class="HRTabList" style="padding: 10px; border: 1px solid #ccc; border-top: none;">
                        <%--Advance & Expense Policy--%>
                        <div class="HRTab no-transition">
                            <span id="Span4" style="text-align: center;" onclick="btnAddSub_Click(this)" class="btn inform add-btn">
                                <i class="add"></i>Add</span>
                            <div class="ui-datatable ui-widget">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="grvExpense" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
                                            <SettingsText EmptyDataRow="No records found!" />
                                            <Settings ShowFilterRow="true" />
                                            <SettingsPager PageSize="20" NumericButtonCount="10">
                                                <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All" Caption="Page Size"
                                                    Items="10, 20, 30, 50, 100" />
                                            </SettingsPager>
                                            <Styles>
                                                <AlternatingRow Enabled="True">
                                                </AlternatingRow>
                                            </Styles>
                                            <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                                            <Columns>
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Width="30px" FieldName="ExpenseID"
                                                    Caption="ID" Settings-AllowAutoFilter="False" />
                                                <dx:GridViewDataColumn Width="70px" FieldName="Title" Caption="Title" />
                                                <dx:GridViewDataColumn Width="70px" FieldName="BTType" Caption="BT Type" />
                                                <dx:GridViewBandColumn Caption="Meal">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="Breakfast" Caption="Breakfast" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                        <dx:GridViewDataTextColumn FieldName="Lunch" Caption="Lunch" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                        <dx:GridViewDataTextColumn FieldName="Dinner" Caption="Diner" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                        <dx:GridViewDataTextColumn FieldName="OtherMeal" Caption="Other" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                    </Columns>
                                                </dx:GridViewBandColumn>
                                                <dx:GridViewDataTextColumn Width="50px" FieldName="Hotel" Caption="Hotel" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                <dx:GridViewBandColumn Caption="Transportation">
                                                    <Columns>
                                                        <dx:GridViewDataTextColumn FieldName="Motobike" Caption="Motobike" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                        <dx:GridViewDataTextColumn FieldName="Transportation" Caption="Other" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                    </Columns>
                                                </dx:GridViewBandColumn>
                                                <dx:GridViewDataTextColumn FieldName="Other" Caption="Other" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                <dx:GridViewDataTextColumn FieldName="Currency" Caption="Currency" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                <dx:GridViewDataDateColumn FieldName="EffectiveDate" Caption="Effective Date"
                                                    PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                            data-id='<%# Eval("ExpenseID") %>' OnClientClick="btnEditClick(this, 'hExpense'); HandleMessage($('[id$=btnSaveExpense]')[0]); bindStartupEvents($('[id$=btnSaveExpense]')[0])"
                                                            OnClick="btnEditExpense_OnClick"></asp:Button>
                                                        <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("ExpenseID") %>'
                                                            onclick="btnDeleteClick(this, 'hExpense')" />
                                                        <asp:Button ID="btnDelete" data-id='<%# Eval("ExpenseID") %>' ToolTip="Delete" runat="server"
                                                            CssClass="hide" OnClick="btnDeleteExpense_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveExpense]')[0]); bindStartupEvents($('[id$=btnSaveExpense]')[0])">
                                                        </asp:Button>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <fieldset class="add-edit-form">
                                <legend><span class="add-edit-action">Add</span> Expense Payment</legend>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hExpense" />
                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                            <tbody>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Title Group<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:DropDownList ID="ddlJobBand" runat="server" AutoPostBack="true" Style="width: 233px !important">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Titles</label>
                                                    </td>
                                                    <td style="padding-left: 10px">
                                                        <asp:TextBox ID="txtGroupTitle" runat="server" ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <%--<td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Destination Group <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:DropDownList ID="ddlDestinationGroup" runat="server" AutoPostBack="true" Style="width: 233px !important">
                                                        </asp:DropDownList>
                                                    </td>--%>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            BT Type <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:DropDownList ID="ddlExpenseBTType" runat="server" AutoPostBack="true" Style="width: 233px !important">
                                                            <asp:ListItem Value="" Text=""></asp:ListItem>
                                                            <asp:ListItem Value="d" Text="Domestic"></asp:ListItem>
                                                            <asp:ListItem Value="o" Text="Overseas"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Currency</label>
                                                    </td>
                                                    <td class="spin-edit ui-panelgrid-cell">
                                                        <asp:DropDownList ID="ddlCurrency" Enabled="false" runat="server" Style="width: 233px !important">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Breakfast
                                                        </label>
                                                    </td>
                                                    <td class="spin-edit ui-panelgrid-cell">
                                                        <dx:ASPxSpinEdit ID="txtExpBreakfast" DisplayFormatString="{0:#,0.##}" runat="server"
                                                            MinValue="0" NullText="0" NumberType="Integer" MaxValue="99999999999999999" Increment="1">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Lunch
                                                        </label>
                                                    </td>
                                                    <td class="spin-edit ui-panelgrid-cell">
                                                        <dx:ASPxSpinEdit ID="txtExpLunch" DisplayFormatString="{0:#,0.##}" runat="server"
                                                            MinValue="0" NullText="0" NumberType="Integer" MaxValue="99999999999999999" Increment="1">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Dinner</label>
                                                    </td>
                                                    <td class="spin-edit ui-panelgrid-cell">
                                                        <dx:ASPxSpinEdit ID="txtExpDinner" DisplayFormatString="{0:#,0.##}" runat="server"
                                                            MinValue="0" NullText="0" NumberType="Integer" MaxValue="99999999999999999" Increment="1">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Other Meal
                                                        </label>
                                                    </td>
                                                    <td class="spin-edit ui-panelgrid-cell">
                                                        <dx:ASPxSpinEdit ID="txtExpOtherMeal" DisplayFormatString="{0:#,0.##}" runat="server"
                                                            MinValue="0" NullText="0" NumberType="Integer" MaxValue="99999999999999999" Increment="1">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Hotel</label>
                                                    </td>
                                                    <td class="spin-edit ui-panelgrid-cell">
                                                        <dx:ASPxSpinEdit ID="txtExpHotel" DisplayFormatString="{0:#,0.##}" runat="server"
                                                            MinValue="0" NullText="0" NumberType="Integer" MaxValue="99999999999999999" Increment="1">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Transportation</label>
                                                    </td>
                                                    <td class="spin-edit ui-panelgrid-cell">
                                                        <dx:ASPxSpinEdit ID="txtExpTransport" DisplayFormatString="{0:#,0.##}" runat="server"
                                                            MinValue="0" NullText="0" NumberType="Integer" MaxValue="99999999999999999" Increment="1">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Motobike</label>
                                                    </td>
                                                    <td class="spin-edit ui-panelgrid-cell">
                                                        <dx:ASPxSpinEdit ID="txtExpenseMotobike" DisplayFormatString="{0:#,0.##}" runat="server"
                                                            MinValue="0" NullText="0" NumberType="Integer" MaxValue="99999999999999999" Increment="1">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Other</label>
                                                    </td>
                                                    <td class="spin-edit ui-panelgrid-cell">
                                                        <dx:ASPxSpinEdit ID="txtExpOther" DisplayFormatString="{0:#,0.##}" runat="server"
                                                            MinValue="0" NullText="0" NumberType="Integer" MaxValue="99999999999999999" Increment="1">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Note</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtExpNote" runat="server" TextMode="MultiLine" Rows="3" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Effective Date <span class="required">*</span></label>
                                                    </td>
                                                    <td style="padding: 5px 5px 0 10px;" class="date-time-picker">
                                                        <dx:ASPxDateEdit ID="dteExpenseEffectiveDate" runat="server" EditFormat="Custom"
                                                            DisplayFormatString="dd-MMM-yyyy" EditFormatString="dd-MMM-yyyy">
                                                        </dx:ASPxDateEdit>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <ul id="ulmExpMessage" style="padding-left: 145px" class="error-summary">
                                </ul>
                                <div class="action-pan">
                                    <asp:UpdatePanel ID="UpdatePanel55" runat="server">
                                        <ContentTemplate>
                                            <input type="button" value="Save" class="btn" onclick="check_mExpense(this)" />
                                            <asp:Button runat="server" ID="btnSaveExpense" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                            <asp:Button runat="server" ID="btnCancelExpense" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                        </div>
                        <%--Moving Time Allowance--%>
                        <div class="HRTab no-transition">
                            <span id="Span14" style="text-align: center;" onclick="btnAddSub_Click(this)" class="btn inform add-btn">
                                <i class="add"></i>Add</span>
                            <div class="ui-datatable ui-widget">
                                <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="grvAllowance" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Settings-AllowAutoFilter="False" Width="50px"
                                                    FieldName="ID" Caption="ID" />
                                                <dx:GridViewDataColumn Width="150px" FieldName="CountryGroup" Caption="Country Group" />
                                                <dx:GridViewDataColumn Width="150px" FieldName="Amount" Caption="Amount" />
                                                <dx:GridViewDataColumn Width="150px" FieldName="Currency" Caption="Currency" />
                                                <dx:GridViewDataColumn Width="150px" FieldName="Description" Caption="Description" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEditAllowance" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                            data-id='<%# Eval("ID") %>' OnClientClick="btnEditClick(this, 'hAllowanceID'); HandleMessage($('[id$=btnSaveAllowance]')[0]); bindStartupEvents($('[id$=btnSaveAllowance]')[0])"
                                                            OnClick="btnEditAllowance_OnClick"></asp:Button>
                                                        <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("ID") %>' onclick="btnDeleteClick(this, 'hAllowanceID')" />
                                                        <asp:Button ID="btnDeleteAllowance" data-id='<%# Eval("ID") %>' ToolTip="Delete"
                                                            runat="server" CssClass="hide" OnClick="btnDeleteAllowance_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveAllowance]')[0]); bindStartupEvents($('[id$=btnSaveAllowance]')[0])">
                                                        </asp:Button>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <fieldset class="add-edit-form">
                                <legend><span class="add-edit-action"></span>&nbsp;Moving Time Allowance Info</legend>
                                <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hAllowanceID" />
                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                            <tbody>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            CountryGroup <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:DropDownList ID="ddlAllowanceCountryGroup" runat="server" Style="width: 233px !important">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Currency <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:DropDownList ID="ddlAllowanceCurrency" runat="server" Style="width: 233px !important">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Amount <span class="required">*</span></label>
                                                    </td>
                                                    <td class="spin-edit ui-panelgrid-cell">
                                                        <dx:ASPxSpinEdit ID="speAllowanceAmount" DisplayFormatString="{0:#,0.#}" runat="server"
                                                            MinValue="0" NullText="0" NumberType="Float" MaxValue="99999999999999999" Increment="1">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Description</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" rowspan="2">
                                                        <asp:TextBox ID="txtAllowanceDescription" runat="server" TextMode="MultiLine" Rows="3"
                                                            Style="width: 340px !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <ul id="ulmAllowanceMessage" style="padding-left: 120px" class="error-summary">
                                </ul>
                                <div class="action-pan">
                                    <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                                        <ContentTemplate>
                                            <input type="button" value="Save" class="btn" onclick="check_mAllowance(this)" />
                                            <asp:Button runat="server" ID="btnSaveAllowance" Text="Save" CssClass="btn hide"
                                                OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                            <asp:Button runat="server" ID="btnCancelAllowance" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--BUDGET--%>
        <div runat="server" id="divBudget" class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                role="tab">
                <span class="ui-icon"></span>Budget</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="HRTabControl">
                    <div class="HRTabNav">
                        <ul>
                            <li>Budget Information</li>
                            <li>Budget P.I.C</li>
                        </ul>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <div class="HRTabList" style="padding: 10px; border: 1px solid #ccc; border-top: none;">
                        <%--Budget Information--%>
                        <div class="HRTab no-transition">
                            <span id="Span2" style="margin-top: 0px; text-align: center;" onclick="btnAddSub_Click(this)"
                                class="btn inform add-btn"><i class="add"></i>Add</span>
                            <%-- --%>
                            <span id="Span15" style="margin-top: 0px; text-align: center;" onclick="$('[id$=btnGetOraBudget]').click()"
                                class="btn inform add-btn"><i class="add"></i>Get Oracle Data</span>
                            <%--<span id="Span1" style="margin-top: 10px;
                            text-align: center;" onclick="showImportMessage(this)" class="btn inform add-btn">
                            <i class="add"></i>Import Excel</span>--%>
                            <div class="ui-datatable ui-widget">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Button runat="server" CssClass="btn hide" Text="Get Oracle Data" ID="btnGetOraBudget"
                                            OnClientClick="HandleMessage(this); bindStartupEvents(this);" />
                                        <dx:ASPxGridView ID="grvBudget" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
                                            <SettingsText EmptyDataRow="No records found!" />
                                            <Settings ShowFilterRow="true" />
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
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Settings-AllowAutoFilter="False" Width="50px"
                                                    FieldName="BudgetID" Caption="ID" />
                                                <dx:GridViewDataColumn Width="200px" FieldName="BudgetCode" Caption="Budget Code" />
                                                <dx:GridViewDataColumn FieldName="BudgetName" Caption="Budget Name" />
                                                <dx:GridViewDataTextColumn FieldName="Amount" Caption="Amount" PropertiesTextEdit-DisplayFormatString="{0:#,0.##}" />
                                                <dx:GridViewDataColumn FieldName="Org" Caption="Org" />
                                                <dx:GridViewDataColumn FieldName="Department" Caption="HR Department" />
                                                <dx:GridViewDataColumn FieldName="Year" Caption="Year" />
                                                <%--<dx:GridViewDataColumn FieldName="Description" Caption="Description" />--%>
                                                <dx:GridViewDataColumn FieldName="Status" Caption="Status" />
                                                <dx:GridViewDataImageColumn Width="50px" Caption="">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEditBudget" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                            data-id='<%# Eval("BudgetID") %>' OnClientClick="btnEditClick(this, 'hBudget'); HandleMessage($('[id$=btnSaveBudget]')[0]); bindStartupEvents($('[id$=btnSaveBudget]')[0])"
                                                            OnClick="btnEditBudget_OnClick"></asp:Button>
                                                        <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("BudgetID") %>'
                                                            onclick="btnDeleteClick(this, 'hBudget')" />
                                                        <asp:Button ID="btnDeleteBudget" data-id='<%# Eval("BudgetID") %>' ToolTip="Delete"
                                                            runat="server" CssClass="hide" OnClick="btnDeleteBudget_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveBudget]')[0]); bindStartupEvents($('[id$=btnSaveBudget]')[0])">
                                                        </asp:Button>
                                                    </DataItemTemplate>
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <fieldset class="add-edit-form">
                                <legend><span class="add-edit-action">Add</span> Budget Information</legend>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hBudget" />
                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                            <tbody>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Budget Code <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtBudgetCode" runat="server" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Budget Name <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtBudgetName" runat="server" Style="width: 210px  !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Amount <span class="required">*</span></label>
                                                    </td>
                                                    <td class="spin-edit ui-panelgrid-cell">
                                                        <dx:ASPxSpinEdit ID="txtBudgetAmount" DisplayFormatString="{0:#,0.##}" runat="server"
                                                            MinValue="0" NullText="0" NumberType="Integer" MaxValue="99999999999999999" Increment="1">
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            HR Department <span class="required">*</span></label>
                                                    </td>
                                                    <td class="spin-edit ui-panelgrid-cell">
                                                        <asp:DropDownList ID="ddlDept" runat="server" Style="width: 233px !important">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell" style="vertical-align: top; padding-top: 10px;">
                                                        <label>
                                                            Type</label>
                                                    </td>
                                                    <td class="spin-edit ui-panelgrid-cell" style="vertical-align: top">
                                                        <asp:DropDownList ID="ddlBGType" runat="server" Style="width: 233px !important">
                                                            <asp:ListItem Value=""></asp:ListItem>
                                                            <asp:ListItem Value="d">Domestic</asp:ListItem>
                                                            <asp:ListItem Value="o">Overseas</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell" style="vertical-align: top; padding-top: 10px;"
                                                        rowspan="2">
                                                        <label>
                                                            Description</label>
                                                    </td>
                                                    <td class="spin-edit ui-panelgrid-cell" rowspan="2">
                                                        <asp:TextBox ID="txtBudgetDes" runat="server" TextMode="MultiLine" Rows="3" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Org <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtOrg" runat="server" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <span class="check-button" style="top: 2px;">
                                                            <asp:CheckBox ID="chkBudgetIsExecutive" runat="server" Checked="false" />
                                                            <span></span></span>Is Executive <span class="check-button" style="top: 2px; margin-left: 20px;">
                                                                <asp:CheckBox ID="chkBudgetActive" runat="server" Checked="True" />
                                                                <span></span></span>Active
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <ul id="ulmBudMessage" style="padding-left: 145px" class="error-summary">
                                </ul>
                                <div class="action-pan">
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                        <ContentTemplate>
                                            <input type="button" value="Save" class="btn" onclick="check_mBudget(this)" />
                                            <asp:Button runat="server" ID="btnSaveBudget" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                            <asp:Button runat="server" ID="btnCancelBudget" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                            <%--<div id="tabImportMessage" class="popup-container">
                                <asp:HiddenField runat="server" ID="hImportExcelID" />
                                <asp:HiddenField ID="hRejectTypeID" runat="server" />
                                <table style="width: 100%; height: 100%; margin: 0;">
                                    <tr>
                                        <td style="vertical-align: middle; border: none !important; padding: 0 !important;">
                                            <table class="grid-edit" style="width: auto !important; margin: auto; background-color: #fff;
                                                border-radius: 5px; box-shadow: 0 0 10px #fff; -webkit-box-shadow: 0 0 10px #fff;
                                                -moz-box-shadow: 0 0 10px #fff;">
                                                <tr>
                                                    <td style="padding: 5px 30px 15px;">
                                                        <h3 style="margin: 0; padding: 5px 0 3px; background-color: #fff; text-decoration: underline;">
                                                            Import Excel</h3>
                                                        <input type="file" name="fImportExcelBudget" id="fImportExcelBudget" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 0 0 15px; text-align: center;">
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <input type="button" value="Import" onclick="ImportExcelBudget()" id="btnImportBudget"
                                                                    class="btn" />
                                                                <input type="button" value="Cancel" onclick="hideImportMessage()" style="margin-left: 5px;"
                                                                    id="btnImportCancel" class="btn secondary" />
                                                                <asp:Button runat="server" ID="btnLoadBudget" CssClass="hide" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>--%>
                        </div>
                        <%--Budget PIC--%>
                        <div class="HRTab no-transition">
                            <span id="Span17" style="text-align: center;" onclick="btnAddSub_Click(this)" class="btn inform add-btn">
                                <i class="add"></i>Add</span>
                            <div class="ui-datatable ui-widget">
                                <asp:UpdatePanel ID="UpdatePanel46" runat="server">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="grvBudgetPIC" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Width="50px" FieldName="ID" Caption="ID"
                                                    Settings-AllowAutoFilter="False" />
                                                <dx:GridViewDataColumn FieldName="Org" Caption="Org" />
                                                <dx:GridViewDataColumn FieldName="PICName" Caption="P.I.C Name" />
                                                <dx:GridViewDataColumn FieldName="PICEmail" Caption="P.I.C Email" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                            data-id='<%# Eval("ID") %>' OnClientClick="btnEditClick(this, 'hBudgetPIC'); HandleMessage($('[id$=btnSaveBudgetPIC]')[0]); bindStartupEvents($('[id$=btnSaveBudgetPIC]')[0])"
                                                            OnClick="btnEditBudgetPIC_OnClick"></asp:Button>
                                                        <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("ID") %>' onclick="btnDeleteClick(this, 'hBudgetPIC')" />
                                                        <asp:Button ID="btnDelete" data-id='<%# Eval("ID") %>' ToolTip="Delete" runat="server"
                                                            CssClass="hide" OnClick="btnDeleteBudgetPIC_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveBudgetPIC]')[0]); bindStartupEvents($('[id$=btnSaveBudgetPIC]')[0])">
                                                        </asp:Button>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <fieldset class="add-edit-form">
                                <legend><span class="add-edit-action"></span>&nbsp;Budget P.I.C Info</legend>
                                <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hBudgetPIC" />
                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                            <tbody>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Org<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtBudgetPICOrg" runat="server" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            P.I.C Name</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtBudgetPICName" runat="server" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            P.I.C Email<span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtBudgetPICEmail" runat="server" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <ul id="ulmBudgetPICMessage" style="padding-left: 120px" class="error-summary">
                                </ul>
                                <div class="action-pan">
                                    <asp:UpdatePanel ID="UpdatePanel48" runat="server">
                                        <ContentTemplate>
                                            <input type="button" value="Save" class="btn" onclick="check_budgetPIC(this)" />
                                            <asp:Button runat="server" ID="btnSaveBudgetPIC" Text="Save" CssClass="btn hide"
                                                OnClientClick="HandleMessage(this); bindStartupEvents(this);" />
                                            <asp:Button runat="server" ID="btnCancelBudgetPIC" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--Invoicing--%>
        <div runat="server" id="divInvoicing" class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                role="tab">
                <span class="ui-icon"></span>Invoicing</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="HRTabControl">
                    <div class="HRTabNav">
                        <ul>
                            <li>Invoice Item</li>
                            <li>Invoice Seller</li>
                        </ul>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <div class="HRTabList" style="padding: 10px; border: 1px solid #ccc; border-top: none;">
                        <%--Invoice Item--%>
                        <div class="HRTab no-transition">
                            <span id="Span5" style="text-align: center;" onclick="btnAddSub_Click(this)" class="btn inform add-btn">
                                <i class="add"></i>Add</span>
                            <div class="ui-datatable ui-widget">
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="grvInvoiceItem" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Settings-AllowAutoFilter="False" Width="50px"
                                                    FieldName="InvoiceItemID" Caption="ID" />
                                                <dx:GridViewDataColumn Width="150px" FieldName="ItemName" Caption="Item Name" />
                                                <dx:GridViewDataColumn FieldName="Note" Caption="Description" />
                                                <dx:GridViewDataColumn Width="80px" FieldName="Status" Caption="Status" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEdit" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                            data-id='<%# Eval("InvoiceItemID") %>' OnClientClick="btnEditClick(this, 'hInvoiceItemID'); HandleMessage($('[id$=btnSaveItem]')[0]); bindStartupEvents($('[id$=btnSaveItem]')[0])"
                                                            OnClick="btnEditItem_OnClick"></asp:Button>
                                                        <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("InvoiceItemID") %>'
                                                            onclick="btnDeleteClick(this, 'hInvoiceItemID')" />
                                                        <asp:Button ID="btnDelete" data-id='<%# Eval("InvoiceItemID") %>' ToolTip="Delete"
                                                            runat="server" CssClass="hide" OnClick="btnDeleteItem_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveItem]')[0]); bindStartupEvents($('[id$=btnSaveItem]')[0])">
                                                        </asp:Button>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <fieldset class="add-edit-form">
                                <legend><span class="add-edit-action"></span>&nbsp;Invoice Item Info</legend>
                                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hInvoiceItemID" />
                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                            <tbody>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Item Name <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtInvoiceItemName" runat="server" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Description</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" rowspan="2">
                                                        <asp:TextBox ID="txtInvoiceItemNote" runat="server" TextMode="MultiLine" Rows="3"
                                                            Style="width: 340px !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td style="padding-left: 9px;">
                                                        <span class="check-button" style="top: 2px;">
                                                            <asp:CheckBox ID="chkInvItem" runat="server" Checked="True" />
                                                            <span></span></span>Active
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <ul id="ulmInvoiceItemMessage" style="padding-left: 120px" class="error-summary">
                                </ul>
                                <div class="action-pan">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                        <ContentTemplate>
                                            <input type="button" value="Save" class="btn" onclick="check_mInvoiceItem(this)" />
                                            <asp:Button runat="server" ID="btnSaveItem" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                            <asp:Button runat="server" ID="btnCancelItem" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                        </div>
                        <%--Invoice Seller--%>
                        <div class="HRTab no-transition">
                            <span id="Span12" style="text-align: center;" onclick="btnAddSub_Click(this)" class="btn inform add-btn">
                                <i class="add"></i>Add</span>
                            <div class="ui-datatable ui-widget">
                                <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="grvCompany" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Settings-AllowAutoFilter="False" Width="50px"
                                                    FieldName="ID" Caption="ID" />
                                                <dx:GridViewDataColumn Width="150px" FieldName="Name" Caption="Seller Name" />
                                                <dx:GridViewDataColumn Width="150px" FieldName="TaxCode" Caption="Tax Code" />
                                                <dx:GridViewDataColumn FieldName="Description" Caption="Description" />
                                                <dx:GridViewDataColumn Width="80px" FieldName="Status" Caption="Status" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEditCompanyName" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                            data-id='<%# Eval("ID") %>' OnClientClick="btnEditClick(this, 'hCompanyID'); HandleMessage($('[id$=btnSaveCompany]')[0]); bindStartupEvents($('[id$=btnSaveCompany]')[0])"
                                                            OnClick="btnEditCompanyName_OnClick"></asp:Button>
                                                        <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("ID") %>' onclick="btnDeleteClick(this, 'hCompanyID')" />
                                                        <asp:Button ID="btnDeleteCompanyName" data-id='<%# Eval("ID") %>' ToolTip="Delete"
                                                            runat="server" CssClass="hide" OnClick="btnDeleteCompanyName_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveCompany]')[0]); bindStartupEvents($('[id$=btnSaveCompany]')[0])">
                                                        </asp:Button>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <fieldset class="add-edit-form">
                                <legend><span class="add-edit-action"></span>&nbsp;Seller Info</legend>
                                <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hCompanyID" />
                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                            <tbody>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Seller Name <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtCompanyName" runat="server" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Tax Code <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtCompanyTaxCode" runat="server" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td style="padding-left: 9px;">
                                                        <span class="check-button" style="top: 2px;">
                                                            <asp:CheckBox ID="chkCompanyCheck" runat="server" Checked="True" />
                                                            <span></span></span>Active
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Description</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" rowspan="2">
                                                        <asp:TextBox ID="txtCompanyDescription" runat="server" TextMode="MultiLine" Rows="3"
                                                            Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <ul id="ulmCompanyNameMessage" style="padding-left: 120px" class="error-summary">
                                </ul>
                                <div class="action-pan">
                                    <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                                        <ContentTemplate>
                                            <input type="button" value="Save" class="btn" onclick="check_mCompanyName()" />
                                            <asp:Button runat="server" ID="btnSaveCompany" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                            <asp:Button runat="server" ID="btnCancelCompany" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--Air Ticket--%>
        <div runat="server" id="divAirTicket" class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                role="tab">
                <span class="ui-icon"></span>Air Ticket</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="HRTabControl">
                    <div class="HRTabNav">
                        <ul>
                            <li>Oracle Supplier</li>
                            <li>Air Period</li>
                        </ul>
                        <div style="clear: both;">
                        </div>
                    </div>
                    <div class="HRTabList" style="padding: 10px; border: 1px solid #ccc; border-top: none;">
                        <%--Oracle Supplier--%>
                        <div class="HRTab no-transition">
                            <span id="Span8" style="text-align: center;" onclick="btnAddSub_Click(this)" class="btn inform add-btn">
                                <i class="add"></i>Add</span>
                            <div class="ui-datatable ui-widget">
                                <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hOraSupplierID" />
                                        <dx:ASPxGridView ID="grvSupplier" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                                <dx:GridViewDataColumn Width="50px" FieldName="ID" Caption="ID" CellStyle-CssClass="id"
                                                    Settings-AllowAutoFilter="False" />
                                                <dx:GridViewDataColumn Width="500px" FieldName="SupplierName" Caption="Supplier Name" />
                                                <dx:GridViewDataColumn FieldName="Oralink" Caption="Oralink" />
                                                <dx:GridViewDataColumn Width="80px" FieldName="Status" Caption="Status" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <%--<asp:Button ID="btnEditSupplier" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                data-id='<%# Eval("ID") %>' OnClientClick="btnEditClick(this); HandleMessage($('[id$=btnSaveSupplier]')[0])"
                                                OnClick="btnEditSupplier_OnClick"></asp:Button>--%>
                                                        <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("ID") %>' onclick="btnDeleteClick(this, 'hOraSupplierID')" />
                                                        <asp:Button ID="btnDeleteSuplier" data-id='<%# Eval("ID") %>' ToolTip="Delete" runat="server"
                                                            CssClass="hide" OnClick="btnDeleteSupplier_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveSupplier]')[0]); bindStartupEvents($('[id$=btnSaveSupplier]')[0])">
                                                        </asp:Button>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <fieldset class="add-edit-form">
                                <legend>Search Oracle Supplier</legend>
                                <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hSelectSuppliers" />
                                        <table style="width: 100%; margin: 15px 0;" class="grid-edit">
                                            <tr>
                                                <td class="ui-panelgrid-cell">
                                                    <label>
                                                        Supplier Name</label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSupplierName" runat="server"></asp:TextBox>
                                                </td>
                                                <td class="ui-panelgrid-cell">
                                                    <label>
                                                        Supplier Type</label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtSupplierType" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSearchSupplier" CssClass="btn" runat="server" Text="Search" OnClientClick="HandleMessage(this); bindStartupEvents(this);" />
                                                </td>
                                            </tr>
                                        </table>
                                        <dx:ASPxGridView ID="grvGetDataOraSupplier" runat="server" Theme="Office2010Black"
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
                                                <dx:GridViewDataColumn Width="50px" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <input type="checkbox" class="chkAll" onchange="CheckAll(this)" />
                                                    </HeaderTemplate>
                                                    <DataItemTemplate>
                                                        <input type="checkbox" class="chkSelect" name="chkSelectSupplier" />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn CellStyle-CssClass="supplierNo" Width="100" FieldName="SEGMENT1"
                                                    Caption="Supplier No" />
                                                <dx:GridViewDataColumn CellStyle-CssClass="supplierName" FieldName="VENDOR_NAME"
                                                    Caption="Supplier Name" />
                                                <dx:GridViewDataColumn Width="150" FieldName="VENDOR_TYPE_LOOKUP_CODE" Caption="Type" />
                                                <dx:GridViewDataColumn Width="150" FieldName="VAT_REGISTRATION_NUM" Caption="Code" />
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <ul id="ulOraSupplierSummary" style="padding-left: 120px" class="error-summary">
                                </ul>
                                <div class="action-pan">
                                    <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                                        <ContentTemplate>
                                            <input type="button" value="Save" class="btn" onclick="checkOraSupplier()" />
                                            <asp:Button runat="server" ID="btnSaveSupplier" Text="Save" CssClass="btn hide" OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                            <asp:Button runat="server" ID="btnCancelSupplier" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                        </div>
                        <%--Air Period--%>
                        <div class="HRTab no-transition">
                            <span id="Span9" style="text-align: center;" onclick="btnAddSub_Click(this)" class="btn inform add-btn">
                                <i class="add"></i>Add</span>
                            <div class="ui-datatable ui-widget">
                                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                    <ContentTemplate>
                                        <dx:ASPxGridView ID="grvAirPeriod" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                                <dx:GridViewDataColumn CellStyle-CssClass="id" Settings-AllowAutoFilter="False" Width="50px"
                                                    FieldName="ID" Caption="ID" />
                                                <dx:GridViewDataColumn Width="150px" FieldName="Name" Caption="Period Name" />
                                                <dx:GridViewDataColumn FieldName="Description" Caption="Description" />
                                                <dx:GridViewDataColumn Width="80px" FieldName="Status" Caption="Status" />
                                                <dx:GridViewDataImageColumn Width="40px">
                                                    <DataItemTemplate>
                                                        <asp:Button ID="btnEditAirPeriod" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                            data-id='<%# Eval("ID") %>' OnClientClick="btnEditClick(this, 'hAirPeriodID'); HandleMessage($('[id$=btnSaveAirPeriod]')[0]); bindStartupEvents($('[id$=btnSaveAirPeriod]')[0])"
                                                            OnClick="btnEditAirPeriod_OnClick"></asp:Button>
                                                        <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("ID") %>' onclick="btnDeleteClick(this, 'hAirPeriodID')" />
                                                        <asp:Button ID="btnDeleteAirPeriod" data-id='<%# Eval("ID") %>' ToolTip="Delete"
                                                            runat="server" CssClass="hide" OnClick="btnDeleteAirPeriod_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveAirPeriod]')[0]); bindStartupEvents($('[id$=btnSaveAirPeriod]')[0])">
                                                        </asp:Button>
                                                    </DataItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </dx:GridViewDataImageColumn>
                                            </Columns>
                                        </dx:ASPxGridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <fieldset class="add-edit-form">
                                <legend><span class="add-edit-action"></span>&nbsp;Air Period Info</legend>
                                <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                    <ContentTemplate>
                                        <asp:HiddenField runat="server" ID="hAirPeriodID" />
                                        <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                            <tbody>
                                                <tr>
                                                    <td class="ui-panelgrid-cell">
                                                        <label>
                                                            Air Period <span class="required">*</span></label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell">
                                                        <asp:TextBox ID="txtAirName" runat="server" Style="width: 210px !important"></asp:TextBox>
                                                    </td>
                                                    <td role="gridcell" class="ui-panelgrid-cell">
                                                        <label>
                                                            Description</label>
                                                    </td>
                                                    <td class="ui-panelgrid-cell" rowspan="2">
                                                        <asp:TextBox ID="txtAirDescription" runat="server" TextMode="MultiLine" Rows="3"
                                                            Style="width: 340px !important"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td style="padding-left: 9px;">
                                                        <span class="check-button" style="top: 2px;">
                                                            <asp:CheckBox ID="chkAirActive" runat="server" Checked="True" />
                                                            <span></span></span>Active
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <ul id="ulmAirPeriodMessage" style="padding-left: 120px" class="error-summary">
                                </ul>
                                <div class="action-pan">
                                    <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                        <ContentTemplate>
                                            <input type="button" value="Save" class="btn" onclick="check_mAirPeriod()" />
                                            <asp:Button runat="server" ID="btnSaveAirPeriod" Text="Save" CssClass="btn hide"
                                                OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                            <asp:Button runat="server" ID="btnCancelAirPeriod" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                                OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--Oracle Invoice Batch Name--%>
        <div runat="server" id="divOraInvBatchName" class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                role="tab">
                <span class="ui-icon"></span>Oracle Invoice Batch Name</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <span id="Span10" style="margin-top: 10px; text-align: center;" onclick="btnAddSub_Click(this)"
                    class="btn inform add-btn"><i class="add"></i>Add</span>
                <div class="ui-datatable ui-widget">
                    <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                        <ContentTemplate>
                            <dx:ASPxGridView ID="grvBatchName" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                    <dx:GridViewDataColumn CellStyle-CssClass="id" Settings-AllowAutoFilter="False" Width="50px"
                                        FieldName="ID" Caption="ID" />
                                    <dx:GridViewDataColumn Width="150px" FieldName="BatchName" Caption="Batch Name" />
                                    <dx:GridViewDataColumn FieldName="Description" Caption="Description" />
                                    <dx:GridViewDataColumn Width="80px" FieldName="Status" Caption="Status" />
                                    <dx:GridViewDataImageColumn Width="40px">
                                        <DataItemTemplate>
                                            <asp:Button ID="btnEditBatchName" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                data-id='<%# Eval("ID") %>' OnClientClick="btnEditClick(this, 'hBatchNameID'); HandleMessage($('[id$=btnSaveBatchName]')[0]); bindStartupEvents($('[id$=btnSaveBatchName]')[0])"
                                                OnClick="btnEditBatchName_OnClick"></asp:Button>
                                            <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("ID") %>' onclick="btnDeleteClick(this, 'hBatchNameID')" />
                                            <asp:Button ID="btnDeleteBatchName" data-id='<%# Eval("ID") %>' ToolTip="Delete"
                                                runat="server" CssClass="hide" OnClick="btnDeleteBatchName_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveBatchName]')[0]); bindStartupEvents($('[id$=btnSaveBatchName]')[0])">
                                            </asp:Button>
                                        </DataItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </dx:GridViewDataImageColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <fieldset class="add-edit-form">
                    <legend><span class="add-edit-action"></span>&nbsp;Batch Name Info</legend>
                    <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hBatchNameID" />
                            <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                <tbody>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Batch Name <span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtBatchName" runat="server" Style="width: 210px !important"></asp:TextBox>
                                        </td>
                                        <td role="gridcell" class="ui-panelgrid-cell">
                                            <label>
                                                Description</label>
                                        </td>
                                        <td class="ui-panelgrid-cell" rowspan="2">
                                            <asp:TextBox ID="txtBatchNameDescription" runat="server" TextMode="MultiLine" Rows="3"
                                                Style="width: 340px !important"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="padding-left: 9px;">
                                            <span class="check-button" style="top: 2px;">
                                                <asp:CheckBox ID="chkBatchNameActive" runat="server" Checked="True" />
                                                <span></span></span>Active
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <ul id="ulmBatchNameMessage" style="padding-left: 120px" class="error-summary">
                    </ul>
                    <div class="action-pan">
                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                            <ContentTemplate>
                                <input type="button" value="Save" class="btn" onclick="check_mBatchName(this)" />
                                <asp:Button runat="server" ID="btnSaveBatchName" Text="Save" CssClass="btn hide"
                                    OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                <asp:Button runat="server" ID="btnCancelBatchName" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                    OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </fieldset>
            </div>
        </div>
        <%--Daily Exchange Rates--%>
        <div runat="server" id="divDailyExrate" class="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                role="tab">
                <span class="ui-icon"></span>Daily Exchange Rates</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <span id="Span11" style="margin-top: 10px; text-align: center;" onclick="btnAddSub_Click(this)"
                    class="btn inform add-btn"><i class="add"></i>Add</span>
                <%-- --%>
                <span id="Span1" style="margin-top: 10px; text-align: center;" onclick="$('[id$=btnGetOraExrate]').click()"
                    class="btn inform add-btn"><i class="add"></i>Get Oracle Data</span>
                <div class="ui-datatable ui-widget">
                    <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                        <ContentTemplate>
                            <asp:Button runat="server" CssClass="btn hide" Text="Get Oracle Data" ID="btnGetOraExrate"
                                OnClientClick="HandleMessage(this); bindStartupEvents(this);" />
                            <dx:ASPxGridView ID="grvDailyRate" runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                    <dx:GridViewDataColumn CellStyle-CssClass="id" Settings-AllowAutoFilter="False" Width="50px"
                                        FieldName="ID" Caption="ID" />
                                    <dx:GridViewDataColumn Width="150px" FieldName="FROM_CURRENCY" Caption="From Currency" />
                                    <dx:GridViewDataColumn Width="150px" FieldName="TO_CURRENCY" Caption="To Currency" />
                                    <dx:GridViewDataDateColumn Width="150px" FieldName="CONVERSION_DATE" Caption="Conversion Date"
                                        PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                    <dx:GridViewDataColumn FieldName="CONVERSION_RATE" Caption="Conversion Rate" />
                                    <dx:GridViewDataColumn Width="80px" FieldName="Status" Caption="Status" />
                                    <dx:GridViewDataImageColumn Width="40px">
                                        <DataItemTemplate>
                                            <asp:Button ID="btnEditDailyRate" runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn"
                                                data-id='<%# Eval("ID") %>' OnClientClick="btnEditClick(this, 'hDailyRateID'); HandleMessage($('[id$=btnSaveDailyRate]')[0]); bindStartupEvents($('[id$=btnSaveDailyRate]')[0])"
                                                OnClick="btnEditDailyRate_OnClick"></asp:Button>
                                            <input type="button" class="grid-btn delete-btn" data-id='<%# Eval("ID") %>' onclick="btnDeleteClick(this, 'hDailyRateID')" />
                                            <asp:Button ID="btnDeleteDailyRate" data-id='<%# Eval("ID") %>' ToolTip="Delete"
                                                runat="server" CssClass="hide" OnClick="btnDeleteDailyRate_OnClick" OnClientClick="HandleMessage($('[id$=btnSaveDailyRate]')[0]); bindStartupEvents($('[id$=btnSaveDailyRate]')[0])">
                                            </asp:Button>
                                        </DataItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </dx:GridViewDataImageColumn>
                                </Columns>
                            </dx:ASPxGridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <fieldset class="add-edit-form">
                    <legend><span class="add-edit-action"></span>&nbsp;Daily Rates Info</legend>
                    <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hDailyRateID" />
                            <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                <tbody>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                From Currency <span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="ddlFromCurrency" runat="server" Style="width: 233px !important">
                                            </asp:DropDownList>
                                        </td>
                                        <td role="gridcell" class="ui-panelgrid-cell">
                                            <label>
                                                To Currency <span class="required">*</span></label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="ddlToCurrency" runat="server" Style="width: 233px !important">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Conversion Date <span class="required">*</span></label>
                                        </td>
                                        <td style="padding: 5px 5px 0 10px;" class="date-time-picker">
                                            <dx:ASPxDateEdit ID="dteConversionDate" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                                EditFormatString="dd-MMM-yyyy">
                                            </dx:ASPxDateEdit>
                                        </td>
                                        <td role="gridcell" class="ui-panelgrid-cell">
                                            <label>
                                                Conversion Rate</label>
                                        </td>
                                        <td class="spin-edit ui-panelgrid-cell">
                                            <dx:ASPxSpinEdit ID="speConversionRate" DisplayFormatString="{0:#,0.#####################}"
                                                runat="server" MinValue="0" NullText="0" NumberType="Float" MaxValue="99999999999999999"
                                                Increment="1">
                                            </dx:ASPxSpinEdit>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="padding-left: 9px;">
                                            <span class="check-button" style="top: 2px;">
                                                <asp:CheckBox ID="chkDailyRateActive" runat="server" Checked="True" />
                                                <span></span></span>Active
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <ul id="ulmDailyRateMessage" style="padding-left: 120px" class="error-summary">
                    </ul>
                    <div class="action-pan">
                        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                            <ContentTemplate>
                                <input type="button" value="Save" class="btn" onclick="check_mDailyRate(this)" />
                                <asp:Button runat="server" ID="btnSaveDailyRate" Text="Save" CssClass="btn hide"
                                    OnClientClick="HandleMessage(this); bindStartupEvents(this); HandlePartialMessageBoard(this);" />
                                <asp:Button runat="server" ID="btnCancelDailyRate" Text="Cancel" CssClass="btn secondary btn-cancel-sub"
                                    OnClientClick="btnCancelSub_Click(this); HandleMessage(this); bindStartupEvents(this);" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">

    <script src="/js/jquery/jquery.autocomplete.js" type="text/javascript"></script>

    <script src="/js/master-data.js" type="text/javascript"></script>

</asp:Content>
