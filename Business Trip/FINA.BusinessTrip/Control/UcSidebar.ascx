<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UcSidebar.ascx.vb"
    Inherits="UcSidebar" %>
<div id="sidebar">
    <div id="admin-menu">
        <div class="widget">
            <h3>
                <i class="bt-info"></i>One Day Trip</h3>
            <ul>
                <li data-tooltip="hover" title="You only use this function if you need to get money in advance for your business trips (Advance amount > 0), Otherwise you should use the BT Expense Declaration as below">
                    <a runat="server" href="~/BTOneDayDeclaration.aspx" class="input_parameter_form">BT
                        Approval <span style="color: red">(Advance Amount &gt; 0)</span></a></li>
                <li><a runat="server" href="~/BTOneDayExpenseDeclaration.aspx" class="input_parameter_form">
                    BT Expense Declaration</a></li>
            </ul>
        </div>
        <div class="widget">
            <h3>
                <i class="bt-info"></i>Overnight Trip</h3>
            <ul>
                <li><a runat="server" href="~/BTAdvanceDeclaration.aspx" class="output_parameter_form">
                    BT Approval</a></li>
                <li><a runat="server" href="~/BTExpenseDeclaration.aspx" class="input_parameter_form">
                    BT Expense Declaration</a></li>
                <li runat="server" id="liWifiPreRequest"><a runat="server" href="~/BTWifiDevicePre.aspx" class="input_parameter_form">
                    Wifi Device Pre-Request</a></li>
            </ul>
        </div>
        <%--<div class="widget">
            <h3>
                <i class="account"></i>Account Management
            </h3>
            <ul>
                <li><a href="#" class="output_parameter_form">Account Information</a></li>
                <li><a href="#" class="input_parameter_form">Change Password</a></li>
                <li><a href="#" class="input_parameter_form">Logout</a></li>
            </ul>
        </div>--%>
        <div class="widget" runat="server" id="finMenu">
            <h3>
                <i class="fin"></i>FIN Management
            </h3>
            <ul>
                <li runat="server" id="liBudgetChecking"><a href="~/FiBudgetChecking.aspx" class="output_parameter_form"
                    runat="server">Budget Checking</a></li>
                <li runat="server" id="liAdvanceMgmt"><a href="~/FiAdvanceMgmt.aspx" class="output_parameter_form"
                    runat="server">Advance Payment</a></li>
                <li runat="server" id="liExpenseMgmt"><a href="~/FiExpenseMgmt.aspx" class="output_parameter_form"
                    runat="server">Expense Payment</a></li>
                <li runat="server" id="liInvoicing"><a href="~/BTInvoicing.aspx" class="output_parameter_form"
                    runat="server">Invoicing</a></li>
                <li runat="server" id="liAirTicket"><a href="~/BTAirTicket.aspx" class="input_parameter_form"
                    runat="server">Air Ticket</a></li>
            </ul>
        </div>
        <div class="widget" runat="server" id="gaMenu">
            <h3>
                <i class="ga"></i>GA Management
            </h3>
            <ul>
                <li><a href="~/GAAdvanceMgmt.aspx" class="input_parameter_form" runat="server">GA Information</a></li>
                <li><a href="~/BTAirTicket.aspx" class="input_parameter_form" runat="server">Air Ticket</a></li>
                <li><a id="A2" href="~/BTAirTicketRequestGA.aspx" class="output_parameter_form" runat="server">
                    Air Ticket Request</a></li>
            </ul>
        </div>
        <div class="widget" runat="server" id="hrMenu" visible="false">
            <h3>
                <i class="hr"></i>HR Management
            </h3>
            <ul>
                <%--<li><a id="A1" href="~/HrManagement.aspx" class="output_parameter_form" runat="server">
                    HR Information</a></li>--%>
                <li><a id="A1" href="~/BTAirTicketRequest.aspx" class="output_parameter_form" runat="server">
                    Air Ticket Request for ICT</a></li>
            </ul>
        </div>
        <div class="widget" runat="server" id="itMenu" visible="false">
            <h3>
                <i class="it"></i>IT Management
            </h3>
            <ul>
                <%--<li><a id="A1" href="~/HrManagement.aspx" class="output_parameter_form" runat="server">
                    HR Information</a></li>--%>
                <li><a id="A3" href="~/BTWifiDevice.aspx" class="output_parameter_form" runat="server">
                    Wifi Device Request</a></li>
            </ul>
        </div>
        <div class="widget" style="position: relative;">
            <h3 style="position: absolute; z-index: 1;">
                &nbsp;
            </h3>
            <div class="calendar" data-role="calendar" data-locale='en'>
            </div>
        </div>
        <div class="widget assemply-info" style="position: relative; border: none;">
            <strong>Release information:</strong>
            <ul>
                <li>Version:
                    <asp:Label runat="server" ID="lblVersion"></asp:Label></li>
                <li>Date:
                    <asp:Label runat="server" ID="lblReleasedDate"></asp:Label></li>
            </ul>
        </div>
    </div>
</div>
