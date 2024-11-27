<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DemoGrid.aspx.vb" Inherits=".DemoGrid" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>

    <script src="js/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>
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
            $("[id$='hTest']").val(values)
            
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

</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hTest" runat="server" />
    <div>
        <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ddlBranchEdit" Width="210px"
            runat="server" EnableAnimation="False">
            <DropDownWindowStyle BackColor="#EDEDED" />
            <DropDownWindowTemplate>
                <dx:ASPxListBox Width="100%" ID="ddlBranch" ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                    runat="server" >
                    <Border BorderStyle="None" />
                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />
<%--                    <Items>
                        <dx:ListEditItem Text="(Select all)" />
                        <dx:ListEditItem Text="Chrome" Value="1" />
                        <dx:ListEditItem Text="Firefox" Value="2" />
                        <dx:ListEditItem Text="IE" Value="3" />
                        <dx:ListEditItem Text="Opera" Value="4" />
                        <dx:ListEditItem Text="Safari" Value="5" />
                    </Items>--%>
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
        
         <asp:Button ID="btnTest" runat="server" Text="Button" />
    </div>

    </form>
</body>
</html>
