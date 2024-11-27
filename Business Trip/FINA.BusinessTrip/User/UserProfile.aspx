<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UserProfile.aspx.vb" Inherits="UserProfile"
    MasterPageFile="~/MasterPage.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <style type="text/css">
        .profile table.grid-edit td input[readonly]
        {
            border: none !important;
            background: none;
            border-bottom: 1px solid #ccc !important;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    My Profile
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <div class="ui-accordion ui-widget ui-helper-reset ui-hidden-container profile" role="tablist">
        <%--<h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top active"
            role="tab">--%>
        <%-- <span class="ui-icon"></span>User Profile</h3>--%>
        <div class="ui-accordion-content ui-helper-reset ui-widget-content" role="tabpanel">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="panMessage">
                    </asp:Panel>
                    <table style="width: auto; margin-top: 20px;" class="grid-edit" id="tblUserProfile">
                        <tr>
                            <td rowspan="3">
                                <asp:Image Width="100" Style="box-shadow: 0 0 10px #666; border-radius: 5px;" ID="imgEmployee"
                                    runat="server" />
                            </td>
                            <td style="text-align: left; padding-left: 25px; padding-right: 8px;">
                                Employee Code:
                            </td>
                            <td>
                                <asp:TextBox ID="txtUserName" runat="server" TextMode="SingleLine" ReadOnly="True"
                                    Font-Bold="True" MaxLength="6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; padding-left: 25px; padding-right: 8px;">
                                Full Name:
                            </td>
                            <td>
                                <asp:TextBox ID="txtFullName" runat="server" TextMode="SingleLine" Font-Bold="True"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; padding-left: 25px; padding-right: 8px;">
                                Email Address:
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmailAddress" runat="server" TextMode="SingleLine" Font-Bold="True"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="vertical-align: top; padding-right: 10px; width: 50%;">
                                <fieldset style="border-radius: 10px;">
                                    <legend style="color: #234A69">Basic Information</legend>
                                    <table class="grid-edit" id="tbl2">
                                        <tr>
                                            <td>
                                                Birth Date:
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 315px !important;" ID="txtBirthdate" runat="server" TextMode="SingleLine"
                                                    Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Gender:
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 315px !important;" ID="txtGender" runat="server" TextMode="SingleLine"
                                                    Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Mobile:
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 315px !important;" ID="txtMobile" runat="server" TextMode="SingleLine"
                                                    Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Extension:
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 315px !important;" ID="txtExt" runat="server" TextMode="SingleLine"
                                                    Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td style="padding-left: 10px;">
                                <fieldset style="border-radius: 10px;">
                                    <legend style="color: #234A69">Orgchart Information</legend>
                                    <table class="grid-edit" id="Table1">
                                        <tr>
                                            <td>
                                                Branch:
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 315px !important;" ID="txtBranch" runat="server" TextMode="SingleLine"
                                                    Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Division:
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 315px !important;" ID="txtDivision" runat="server" TextMode="SingleLine"
                                                    Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Department:
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 315px !important;" ID="txtDepartment" runat="server" TextMode="SingleLine"
                                                    Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                JobBand:
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 315px !important;" ID="txtJobband" runat="server" TextMode="SingleLine"
                                                    Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                        </tr>                              
                    </table>
                   <%-- <fieldset style="border-radius: 10px; border:3px dashed #DF7676 !important"  >
                        <legend style="color: #234A69">Support Information</legend>
                        <table style="margin-top:0px">
                            <tr>
                                <td style="vertical-align: top; padding-right: 10px; width: 50%;">
                                    <table class="grid-edit" style="margin-top:0px" id="Table2">
                                        <tr>
                                            <td colspan="2" style="font-size: large; text-align: left">
                                                IT
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                User support:
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 315px !important;" ID="TextBox1" runat="server" Text="Vũ Tuấn Hiếu"
                                                    TextMode="SingleLine" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Extension:
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 315px !important;" ID="TextBox2" runat="server" Text="862 205"
                                                    TextMode="SingleLine" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Email:
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 315px !important;" ID="TextBox3" Text="syhieuvt@toyotavn.com.vn"
                                                    runat="server" TextMode="SingleLine" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="padding-left: 10px;">
                                    <table class="grid-edit" style="margin-top:0px" id="Table3">
                                        <tr>
                                            <td colspan="2" style="font-size: large; text-align: left">
                                                FINANCE
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                User support:
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 315px !important;" ID="TextBox4" runat="server" Text="Vũ Tuấn Hiếu"
                                                    TextMode="SingleLine" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Extension:
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 315px !important;" ID="TextBox5" runat="server" Text="862 205"
                                                    TextMode="SingleLine" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Email:
                                            </td>
                                            <td>
                                                <asp:TextBox Style="width: 315px !important;" ID="TextBox6" Text="syhieuvt@toyotavn.com.vn"
                                                    runat="server" TextMode="SingleLine" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </fieldset>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptPlaceHolder">
</asp:Content>
