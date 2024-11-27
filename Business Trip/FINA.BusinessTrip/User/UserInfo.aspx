<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UserInfo.aspx.vb" Inherits=".UserInfo"
    MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    Profile &raquo; User Info
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <style type="text/css">
        table tr.user-locked td
        {
            color: #FF6161 !important;
        }
        table tr.user-locked:hover td
        {
            color: #FF0000 !important;
        }
    </style>
    <div class="ui-accordion ui-widget ui-helper-reset ui-hidden-container" role="tablist">
        <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
            role="tab">
            <span class="ui-icon"></span>List Of User Information</h3>
        <div class="ui-accordion-content ui-helper-reset ui-widget-content" role="tabpanel">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="userid"></asp:HiddenField>
                    <dx:ASPxGridView ID="grvUserInfo" runat="server" Theme="Office2010Black" AutoGenerateColumns="False">
                        <SettingsText EmptyDataRow="No records found!" />
                        <Settings ShowFilterRow="true" ShowFilterRowMenu="true" />
                        <SettingsPager PageSize="20" NumericButtonCount="10">
                            <PageSizeItemSettings Visible="true" ShowAllItem="true" AllItemText="All" Caption="Page Size"
                                Items="20, 30, 50, 100" />
                        </SettingsPager>
                        <Styles>
                            <AlternatingRow Enabled="True">
                            </AlternatingRow>
                        </Styles>
                        <SettingsLoadingPanel Delay="0" Text="" ShowImage="false" />
                        <Columns>
                            <dx:GridViewDataColumn FieldName="RowNumber" Width="50" Caption="No" />
                            <dx:GridViewDataColumn FieldName="UserName" Width="80" Caption="User Name" />
                            <dx:GridViewDataColumn FieldName="FullName" Caption="Full Name" />                            
                            <dx:GridViewDataDateColumn Width="100" FieldName="ExpriedDate" Caption="Expired Date"
                                PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                            <dx:GridViewDataColumn FieldName="RoleTypeName" Width="100" Caption="Role Type" />
                            <dx:GridViewDataColumn FieldName="TimeKeeper" Caption="TimeKeeper" />
                            <dx:GridViewDataColumn FieldName="IsLocked" Width="50" Caption="IsLocked" />
                            <dx:GridViewDataImageColumn Caption="Edit" Width="50">
                                <DataItemTemplate>
                                    <asp:Button runat="server" ToolTip="Edit" CssClass="grid-btn edit-btn" PostBackUrl='<%# Eval("UserName", "~/User/CreateUser.aspx?uid={0}" ) %>'>
                                    </asp:Button>
                                    <asp:Button ID="Button1" ToolTip="Reset password" runat="server" CssClass="grid-btn changepass-btn"
                                        PostBackUrl='<%# Eval("UserName", "~/User/ChangePass.aspx?uid={0}" ) %>'></asp:Button>
                                </DataItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </dx:GridViewDataImageColumn>
                        </Columns>
                    </dx:ASPxGridView>
                    <div id="cangiua" class="action-pan">
                        <asp:Button CssClass="btn" OnClientClick="HandleMessage()" ID="btnCreateUser" Text="Add"
                            runat="server" PostBackUrl="~/User/CreateUser.aspx" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptPlaceHolder">
</asp:Content>
