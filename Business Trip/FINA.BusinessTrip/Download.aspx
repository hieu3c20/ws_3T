<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Download.aspx.vb" Inherits="Download"
    MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="HeadPlaceHolder">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    DOWNLOAD
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <img src="images/download.png" style="display: none" alt="" />
    <img src="images/download-hover.png" style="display: none" alt="" />
    <div class="ui-accordion ui-widget ui-helper-reset ui-hidden-container" role="tablist">
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top active"
                role="tab">
                <span class="ui-icon"></span>List of Guideline &amp; Policy
            </h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content" role="tabpanel">
                <div class="ui-datatable ui-widget">
                    <div class="ui-datatable-tablewrapper">
                        <table class="grid download-grid">
                            <tr>
                                <th style="width: 30px">
                                    No
                                </th>
                                <th style="width: 350px">
                                    Document Name
                                </th>
                                <th>
                                    Description
                                </th>
                                <th style="width: 50px">
                                </th>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    1
                                </td>
                                <td>
                                    <a href="/Download/BusinessPolicy_EN.pdf" target="_blank">Business Trip Policy (EN)</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/BusinessPolicy_EN.pdf" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    2
                                </td>
                                <td>
                                    <a href="/Download/BusinessPolicy_Annex_I_(EN).pdf" target="_blank">Business Trip Policy
                                        - Annex I (EN)</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/BusinessPolicy_Annex_I_(EN).pdf" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    3
                                </td>
                                <td>
                                    <a href="/Download/BusinessPolicy_Annex_II_(EN).pdf" target="_blank">Business Trip Policy
                                        - Annex II (EN)</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/BusinessPolicy_Annex_II_(EN).pdf" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    4
                                </td>
                                <td>
                                    <a href="/Download/BusinessPolicy_VN.pdf" target="_blank">Business Trip Policy (VN)</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/BusinessPolicy_VN.pdf" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    5
                                </td>
                                <td>
                                    <a href="/Download/BusinessPolicy_Annex_I_(VN).pdf" target="_blank">Business Trip Policy
                                        - Phụ lục I (VN)</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/BusinessPolicy_Annex_I_(VN).pdf" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    6
                                </td>
                                <td>
                                    <a href="/Download/BusinessPolicy_Annex_II_(VN).pdf" target="_blank">Business Trip Policy
                                        - Phụ lục II (VN)</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/BusinessPolicy_Annex_II_(VN).pdf" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>                            
                            <tr>
                                <td style="text-align: right">
                                    7
                                </td>
                                <td>
                                    <a href="/Download/Document/BTS_UserManual_EN.docx" target="_blank">BTS UserManual (EN)</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/Document/BTS_UserManual_EN.docx" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    8
                                </td>
                                <td>
                                    <a href="/Download/Document/BTS_UserManual_JP.docx" target="_blank">BTS UserManual (JP)</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/Document/BTS_UserManual_JP.docx" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    9
                                </td>
                                <td>
                                    <a href="/Download/Document/BTS_UserManual_VN.docx" target="_blank">BTS UserManual (VN)</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/Document/BTS_UserManual_VN.docx" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    10
                                </td>
                                <td>
                                    <a href="/Download/Document/Business_Trip_Procedure_EN.PDF" target="_blank">Business Trip Procedure (EN)</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/Document/Business_Trip_Procedure_EN.PDF" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    11
                                </td>
                                <td>
                                    <a href="/Download/Document/Business_Trip_Procedure_VN.PDF" target="_blank">Business Trip Procedure (VN)</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/Document/Business_Trip_Procedure_VN.PDF" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    12
                                </td>
                                <td>
                                    <a href="/Download/Meal_allowance_decision.pdf" target="_blank">Meal Allowance Decision</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/Meal_allowance_decision.pdf" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div role="tab-container">
            <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top"
                role="tab">
                <span class="ui-icon"></span>List of Form Template</h3>
            <div class="ui-accordion-content ui-helper-reset ui-widget-content hide" role="tabpanel">
                <div class="ui-datatable ui-widget">
                    <div class="ui-datatable-tablewrapper">
                        <table class="grid download-grid">
                            <tr>
                                <th style="width: 30px">
                                    No
                                </th>
                                <th style="width: 350px">
                                    Document Name
                                </th>
                                <th>
                                    Description
                                </th>
                                <th style="width: 50px">
                                </th>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    1
                                </td>
                                <td>
                                    <a href="/Download/overnight_bt_template.xlt" target="_blank">Business Trip Approval
                                        (Over Night Trip - Domestic)</a>
                                </td>
                                <td>
                                    Active: 01 July 2015
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/overnight_bt_template.xlt" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    2
                                </td>
                                <td>
                                    <a href="/Download/overnight_bt_international_template.xlt" target="_blank">Business
                                        Trip Approval (Over Night Trip - Oversea)</a>
                                </td>
                                <td>
                                    Active: 01 July 2015
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/overnight_bt_international_template.xlt" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    3
                                </td>
                                <td>
                                    <a href="/Download/one_day_bt_template.xlt" target="_blank">Business Trip Approval (One
                                        Day Trip)</a>
                                </td>
                                <td>
                                    Active: 01 July 2015
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/one_day_bt_template.xlt" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    4
                                </td>
                                <td>
                                    <a href="/Download/bt-schedule-template.xlt" target="_blank">Business Trip Schedule</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/bt-schedule-template.xlt" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    5
                                </td>
                                <td>
                                    <a href="/Download/bt_expense_template.xlt" target="_blank">Business Trip Expense Form
                                        Revised (Domestic)</a>
                                </td>
                                <td>
                                    Active: 01 July 2015
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/bt_expense_template.xlt" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    6
                                </td>
                                <td>
                                    <a href="/Download/bt_expense_international_template.xlt" target="_blank">Business Trip
                                        Expense Form Revised (Oversea)</a>
                                </td>
                                <td>
                                    Active: 01 July 2015
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/bt_expense_international_template.xlt" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    7
                                </td>
                                <td>
                                    <a href="/Download/car_usage_request.xls" target="_blank">Car Usage Request</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/car_usage_request.xls" target="_blank" class="icon-32 download"
                                        title="Download"></a>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    8
                                </td>
                                <td>
                                    <a href="/Download/Document/Account_Delegation_Registration_Form.doc" target="_blank">Account Delegation Registration Form</a>
                                </td>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <a href="/Download/Document/Account_Delegation_Registration_Form.doc" target="_blank" class="icon-32 download"
                                        title="Download"></a>
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
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">
</asp:Content>
