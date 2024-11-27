<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Report.aspx.vb" Inherits="Report"
    MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link href="/js/jquery/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    REPORT
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <img src="images/excel-32.png" style="display: none" alt="" />
    <img src="images/excel-32-hover.png" style="display: none" alt="" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="panMessage">
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hReportType" />
            <asp:HiddenField runat="server" ID="hShowClass" />
            <asp:HiddenField runat="server" ID="hReportTitle" />
            <asp:Button runat="server" Text="" ID="btnMessage" CssClass="hide" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="ui-accordion ui-widget ui-helper-reset ui-hidden-container" role="tablist">
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top active"
                role="tab">
                <span class="ui-icon"></span>Common Reports
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content" role="tabpanel">
                <div class="ui-datatable ui-widget">
                    <div class="ui-datatable-tablewrapper">
                        <table class="grid download-grid">
                            <tr>
                                <th style="width: 30px">
                                    No
                                </th>
                                <th>
                                    Report Name
                                </th>
                                <%-- <th>
                                    Description
                                </th>--%>
                                <th style="width: 50px">
                                </th>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <%=RptNo%>
                                </td>
                                <td>
                                    <a href="#" onclick="return ShowReportCondition('panReportCommon', 'Báo cáo tổng số tiền tạm ứng đã thanh toán', 'advance')">
                                        Báo cáo tổng số tiền tạm ứng đã thanh toán</a>
                                </td>
                                <td style="text-align: center">
                                    <a href="#" onclick="return ShowReportCondition('panReportCommon', 'Báo cáo tổng số tiền tạm ứng đã thanh toán', 'advance')"
                                        class="icon-32 excel" title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <%=RptNo%>
                                </td>
                                <td>
                                    <a href="#" onclick="return ShowReportCondition('panReportCommon', 'Báo cáo tổng số tiền expense thực tế đã thanh toán', 'expense')">
                                        Báo cáo tổng số tiền expense thực tế đã thanh toán</a>
                                </td>
                                <td style="text-align: center">
                                    <a href="#" onclick="return ShowReportCondition('panReportCommon', 'Báo cáo tổng số tiền expense thực tế đã thanh toán', 'expense')"
                                        class="icon-32 excel" title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <%=RptNo%>
                                </td>
                                <td>
                                    <a href="#" onclick="return ShowReportCondition('panReportCommon', 'Báo cáo các chuyến công tác đã lấy tiền tạm ứng từ công ty nhưng quá hạn settle', 'advance-clear')">
                                        Báo cáo các chuyến công tác đã lấy tiền tạm ứng từ công ty nhưng quá hạn settle</a>
                                </td>
                                <td style="text-align: center">
                                    <a href="#" onclick="return ShowReportCondition('panReportCommon', 'Báo cáo các chuyến công tác đã lấy tiền tạm ứng từ công ty nhưng quá hạn settle', 'advance-clear')"
                                        class="icon-32 excel" title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <%=RptNo%>
                                </td>
                                <td>
                                    <a href="#" onclick="return ShowReportCondition('panReportCommon', 'Báo cáo các chuyến công tác không lấy tiền tạm ứng từ công ty nhưng quá hạn settle', 'noadvance-clear')">
                                        Báo cáo các chuyến công tác không lấy tiền tạm ứng từ công ty nhưng quá hạn settle</a>
                                </td>
                                <td style="text-align: center">
                                    <a href="#" onclick="return ShowReportCondition('panReportCommon', 'Báo cáo các chuyến công tác không lấy tiền tạm ứng từ công ty nhưng quá hạn settle', 'noadvance-clear')"
                                        class="icon-32 excel" title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <%=RptNo%>
                                </td>
                                <td>
                                    <a href="#" onclick="return ShowReportCondition('panReportCommon', 'Báo cáo đối soát tổng số tiền vé máy bay', 'airticket-compare', 'airticket')">
                                        Báo cáo đối soát tổng số tiền vé máy bay</a>
                                </td>
                                <td style="text-align: center">
                                    <a href="#" onclick="return ShowReportCondition('panReportCommon', 'Báo cáo đối soát tổng số tiền vé máy bay', 'airticket-compare', 'airticket')"
                                        class="icon-32 excel" title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <%=RptNo%>
                                </td>
                                <td>
                                    <a href="#" id="lnkWifi" onclick="return ShowReportCondition('panReportCommon', 'Báo cáo thuê thiết bị phát wifi', 'wifi-device', 'wifi')">
                                        Báo cáo thuê thiết bị phát wifi</a>
                                </td>
                                <td style="text-align: center">
                                    <a href="#" onclick="return ShowReportCondition('panReportCommon', 'Báo cáo thuê thiết bị phát wifi', 'wifi-device', 'wifi')"
                                        class="icon-32 excel" title="Download"></a>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
    <%--Report condition--%>
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div id="panReportCommon" runat="server" style="display: none; transition: none;
                position: fixed; width: 100%; height: 100%; left: 0px; top: 0px; z-index: 5000;
                background-color: rgba(0, 0, 0, 0.7); overflow: auto;" class="report-condition">
                <div id="panReportCommonContent" class="report-popup" style="width: 525px; transition: none;
                    margin: 50px auto; position: relative; padding: 10px 20px; box-shadow: rgb(255, 255, 255) 0px 0px 10px;
                    border-radius: 5px; opacity: 1; background-color: rgb(255, 255, 255);" class="report-condition-content">
                    <div style="margin-bottom: 10px;">
                        <fieldset style="padding: 10px 20px 20px;">
                            <legend style="color: #325EA2; font-size: 15px" class="report-legend">Report</legend>
                            <table class="grid-edit" style="margin: 5px 0 0">
                                <tr runat="server" id="trWifiCondition" class="default-hide wifi">
                                    <td style="width: 5%; font-weight: bold;">
                                        BT Type:<br />
                                        <asp:DropDownList runat="server" ID="ddlBTType" AutoPostBack="true" onchange="BTTypeChange()" Style="margin-top: 5px;">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="font-weight: bold; padding-left: 16px">
                                        Country:<br />
                                        <asp:DropDownList runat="server" ID="ddlCountry" Style="margin-top: 5px;">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server" id="trAirTicketCondition" class="default-hide airticket">
                                    <td style="width: 5%; font-weight: bold;">
                                        Air Period:<br />
                                        <asp:DropDownList runat="server" ID="ddlAirPeriod" Style="margin-top: 5px;">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="font-weight: bold; padding-left: 16px">
                                        Supplier:<br />
                                        <asp:DropDownList runat="server" ID="ddlOraSupplier" Style="margin-top: 5px;">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 5%; font-weight: bold;" class="date-time-picker">
                                        Departure Date From:<br />
                                        <dx:ASPxDateEdit ID="dteDepartureDateFrom" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy" Style="margin-top: 5px;">
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td style="font-weight: bold; padding-left: 16px" class="date-time-picker">
                                        Departure Date To:<br />
                                        <dx:ASPxDateEdit ID="dteDepartureDateTo" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                            EditFormatString="dd-MMM-yyyy" Style="margin-top: 5px;">
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <fieldset style="border: 1px solid #ccc; margin-top: 5px">
                                            <legend>Branch</legend>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radBranchAll" Checked="true" Text=" " CssClass="radio-button" GroupName="branch" />
                                            <span style="position: relative; top: -3px;">All Branch</span>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radBranchChoose" Text=" " CssClass="radio-button" GroupName="branch" Style="margin-left: 20px;" />
                                            <span style="position: relative; top: -3px;">Choose Branch</span>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radBranchNoChoose" Text=" " CssClass="radio-button" GroupName="branch" Style="margin-left: 20px;" />
                                            <span style="position: relative; top: -3px;">Except Branch</span>
                                            <br />
                                            <asp:DropDownList runat="server" ID="ddlBranch" Style="margin-top: 5px;" Enabled="false">
                                            </asp:DropDownList>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <fieldset style="border: 1px solid #ccc; margin-top: 5px">
                                            <legend>Division</legend>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radDivAll" Checked="true" Text=" " CssClass="radio-button" GroupName="division" />
                                            <span style="position: relative; top: -3px;">All Division</span>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radDivChoose" Text=" " CssClass="radio-button" GroupName="division" Style="margin-left: 20px;" />
                                            <span style="position: relative; top: -3px;">Choose Division</span>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radDivNoChoose" Text=" " CssClass="radio-button" GroupName="division" Style="margin-left: 20px;" />
                                            <span style="position: relative; top: -3px;">Except Division</span>
                                            <br />
                                            <asp:DropDownList runat="server" ID="ddlDivision" Style="margin-top: 5px;" Enabled="false">
                                            </asp:DropDownList>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <fieldset style="border: 1px solid #ccc; margin-top: 5px">
                                            <legend>Department</legend>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radDeptAll" Checked="true" Text=" " CssClass="radio-button" GroupName="department" />
                                            <span style="position: relative; top: -3px;">All Department</span>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radDeptChoose" Text=" " CssClass="radio-button" GroupName="department" Style="margin-left: 20px;" />
                                            <span style="position: relative; top: -3px;">Choose Department</span>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radDeptNoChoose" Text=" " CssClass="radio-button" GroupName="department"
                                                Style="margin-left: 20px;" />
                                            <span style="position: relative; top: -3px;">Except Department</span>
                                            <br />
                                            <asp:DropDownList runat="server" ID="ddlDepartment" Style="margin-top: 5px;" Enabled="false">
                                            </asp:DropDownList>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <fieldset style="border: 1px solid #ccc; margin-top: 5px">
                                            <legend>Section</legend>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radSecAll" Checked="true" Text=" " CssClass="radio-button" GroupName="section" />
                                            <span style="position: relative; top: -3px;">All Section</span>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radSecChoose" Text=" " CssClass="radio-button" GroupName="section" Style="margin-left: 20px;" />
                                            <span style="position: relative; top: -3px;">Choose Section</span>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radSecNoChoose" Text=" " CssClass="radio-button" GroupName="section" Style="margin-left: 20px;" />
                                            <span style="position: relative; top: -3px;">Except Section</span>
                                            <br />
                                            <asp:DropDownList runat="server" ID="ddlSection" Style="margin-top: 5px;" Enabled="false">
                                            </asp:DropDownList>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <fieldset style="border: 1px solid #ccc; margin-top: 5px">
                                            <legend>Employee</legend>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radEmpAll" Checked="true" Text=" " CssClass="radio-button" GroupName="employee" />
                                            <span style="position: relative; top: -3px;">All Employee</span>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radEmpChoose" Text=" " CssClass="radio-button" GroupName="employee" Style="margin-left: 20px;" />
                                            <span style="position: relative; top: -3px;">Choose Employee</span>
                                            <asp:RadioButton AutoPostBack="True" onclick="bindStartupEvents()" runat="server"
                                                ID="radEmpNoChoose" Text=" " CssClass="radio-button" GroupName="employee" Style="margin-left: 20px;" />
                                            <span style="position: relative; top: -3px;">Except Employee</span>
                                            <br />
                                            <asp:TextBox ID="txtEmployeeCode" Enabled="false" runat="server" CssClass="employee-code"
                                                Style="margin-top: 5px"></asp:TextBox>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                    <div style="margin-left: 20px; width: 300px; color: Red;">
                        <ul id="ulPreErrorMessage" class="validate-error-container">
                        </ul>
                    </div>
                    <div style="text-align: center">
                        <span class="btn"><span>Report</span>
                            <asp:Button ID="btnOKCommonReport" runat="server" Text="OK" CssClass="btn" OnClientClick="bindStartupEvents(); GetExportFile()" />
                        </span><span class="btn secondary" onclick="HideReportCondition()" style="margin-left: 5px;
                            margin-top: 10px;">
                            <asp:Button ID="btnCancelCommonReport" runat="server" Text="Cancel" CssClass="btn"
                                OnClientClick="bindStartupEvents()" />
                            <span>Cancel</span></span>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">

    <script src="/js/jquery/jquery.autocomplete.js" type="text/javascript"></script>

    <script type="text/javascript">
        var _authorizedAccounts = <%= GetAuthorizedAccounts() %>        
    </script>

    <script type="text/javascript" src="/js/report.js"></script>

</asp:Content>
