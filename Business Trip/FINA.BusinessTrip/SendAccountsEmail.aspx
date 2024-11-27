<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SendAccountsEmail.aspx.vb"
    Inherits="SendAccountsEmail" MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="HeadPlaceHolder">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    Send Account Email
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <span id="btnShowSend" class="btn" style="padding-left: 6px;"><i class="submit"></i>
                <span>Send</span>
                <asp:Button runat="server" ID="btnSend" /></span>
            <asp:Label runat="server" ID="lblMessage"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="clear: both;">
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">
</asp:Content>
