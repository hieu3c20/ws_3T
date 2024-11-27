<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UcHeader.ascx.vb" Inherits="UcHeader" %>
<div id="header">
    <div id="logo">
        <img src="/images/logo-toyota.png" alt="logo" />
    </div>
    <div id="text-header">
        <img src="/images/text-header.png" alt="tmv-business-trip" />
    </div>
    <div id="top-menu">
        <div id="account">
            <ul>
                <li><i class="user-icon btnAccount"></i><a runat="server" href="~/User/UserProfile.aspx">
                    <asp:Label ID="lblFullName" runat="server" Text=""></asp:Label></a> –
                    <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                    <%--<i class="hamburger-icon"></i>--%>
                    <div id="account-menu" class="no-transition">
                        <ul>
                            <li class="imgProfile"><a href="~/User/UserProfile.aspx" runat="server">Profile</a></li>
                            <li class="imgUserInfo" runat="server" id="liAccountMenu"><a href="~/User/UserInfo.aspx"
                                runat="server">Account Management</a></li>
                            <li class="imgChangePass"><a href="~/User/ChangePass.aspx" runat="server">Change Password</a></li>
                            <li class="imgLogout"><a class="btnLogout" runat="server" href="~/Logout.aspx">Logout</a></li>
                        </ul>
                    </div>
                </li>
            </ul>
        </div>
    </div>
    <div class="navBar">
        <ul class="ul_nav">
            <li><a href="/BTOneDayExpenseDeclaration.aspx" class="money">One Day Trip</a>
                <div class="divider">
                </div>
            </li>
            <li><a href="/BTExpenseDeclaration.aspx" class="moneyregister">Overnight Trip </a>
                <div class="divider">
                </div>
            </li>
            <li runat="server" id="masterMenu"><a href="~/MasterData.aspx" runat="server" class="masterdata">
                Master Data</a><div class="divider">
                </div>
            </li>
            <li runat="server" id="reportMenu"><a href="~/Report.aspx" runat="server" class="report">
                Reports</a>
                <div class="divider">
                </div>
            </li>
            <li><a href="/FAQ.aspx" class="faq" style='color: Red'>FAQ</a><div class="divider">
            </div>
            </li>
            <li><a href="/Download.aspx" class="download">Download</a></li>
        </ul>
    </div>
    <div class="quick-link-bar">
        <i class="quick-link"></i>
        <ul class="quick-link-submenu">
            <li><a href="http://tmv-net.com.vn/" target="_blank">Tmv-net</a> </li>
            <li><a href="http://www.lexus.com.vn" target="_blank">Lexus</a></li>
            <li><a href="http://www.toyota.com.vn/" target="_blank">Toyota</a> </li>
            <li><a href="http://www.toyotavn.com.vn" target="_blank">Toyotavn</a></li>
        </ul>
    </div>
</div>
