<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Demo.aspx.vb" Inherits=".Demo" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <%--<asp:DropDownList runat="server" ID="test" AutoPostBack="true">
        <asp:ListItem Value="1">11</asp:ListItem>
        <asp:ListItem Value="1">12</asp:ListItem>
        <asp:ListItem Value="2">21</asp:ListItem>
        <asp:ListItem Value="2">22</asp:ListItem>
        <asp:ListItem Value="3">3</asp:ListItem>
    </asp:DropDownList>--%>
    <asp:GridView runat="server" ID="grvPassword" AutoGenerateColumns="true">
    </asp:GridView>
    </form>
</body>
</html>
