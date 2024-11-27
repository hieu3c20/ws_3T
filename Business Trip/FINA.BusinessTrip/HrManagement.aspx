<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="HrManagement.aspx.vb"
    Inherits="HrManagement" MasterPageFile="~/MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="HeadPlaceHolder">
    <link href="/js/jquery/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    HR Attendance Management
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <img src="images/inform-btn.png" style="display: none" alt="" />
    <img src="images/check-check.png" style="display: none" alt="" />
    <img src="images/check.png" style="display: none" alt="" />
    <img src="images/rad-check.png" style="display: none" alt="" />
    <img src="images/rad.png" style="display: none" alt="" />
    <span class="required"></span>
    <div class="ui-accordion ui-widget ui-helper-reset ui-hidden-container" role="tablist">
        <div id="panGetInfo" class="no-transition">
            <%--General Information--%>
            <div role="tab-container">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top active"
                    role="tab">
                    <span class="ui-icon"></span>Search Condition</h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content" role="tabpanel">
                    <span class="required" style="text-align: center"></span>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="panMessage">
                            </asp:Panel>
                            <asp:Literal runat="server" ID="lblMessage"></asp:Literal>
                            <table class="ui-panelgrid ui-widget grid-edit" role="grid" style="margin-bottom: 15px">
                                <tbody>
                                    <tr>
                                        <td class="ui-panelgrid-cell" style="width: 85px">
                                            <label>
                                                Business Trip Type</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="ddlBTType" runat="server">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hBTType" />
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Location</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="cboLocation" runat="server">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hLocation" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                From Date</label>
                                        </td>
                                        <td class="ui-panelgrid-cell date-time-picker">
                                            <dx:ASPxDateEdit ID="dtpFrom" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                                EditFormatString="dd-MMM-yyyy">
                                            </dx:ASPxDateEdit>
                                            <asp:HiddenField runat="server" ID="hFrom" />
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                To Date</label>
                                        </td>
                                        <td class="ui-panelgrid-cell date-time-picker">
                                            <dx:ASPxDateEdit ID="dtpTo" runat="server" EditFormat="Custom" DisplayFormatString="dd-MMM-yyyy"
                                                EditFormatString="dd-MMM-yyyy">
                                            </dx:ASPxDateEdit>
                                            <asp:HiddenField runat="server" ID="hTo" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Division</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="cboDivision" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hDivision" />
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Department</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="cboDept" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hDept" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Section</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="cboSection" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hSection" />
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Group/Team</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:DropDownList ID="cboGroup" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:HiddenField runat="server" ID="hGroup" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Employee Code</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtEmployeeCode" runat="server" CssClass="employee-code" data-type="int"
                                                MaxLength="6"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hEmployeeCode" />
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <label>
                                                Full Name</label>
                                        </td>
                                        <td class="ui-panelgrid-cell">
                                            <asp:TextBox ID="txtFullName" runat="server"></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hFullName" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4">
                                            <span class="btn inform" style="margin-left: 3px; top: 20px; text-align: center;">
                                                <asp:Button ID="btnSearch" CssClass="btnSearch" runat="server" Text="" OnClientClick="bindStartupEvent()" />
                                                <i class="search"></i>Search</span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <%--List of BT--%>
            <div class="tab-container">
                <h3 class="ui-accordion-header ui-helper-reset ui-state-default ui-state-active ui-corner-top active"
                    role="tab">
                    <span class="ui-icon"></span>Attendance Infomation</h3>
                <div class="ui-accordion-content ui-helper-reset ui-widget-content" role="tabpanel">
                    <div class="ui-datatable ui-widget">
                        <div class="ui-datatable-tablewrapper">
                            <div class="HRTabControl">
                                <div class="HRTabNav">
                                    <ul>
                                        <li>Pending</li>
                                        <li>HR Database</li>
                                    </ul>
                                    <div style="clear: both;">
                                    </div>
                                </div>
                                <div class="HRTabList">
                                    <div class="HRTab">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="grvBTRegister" KeyFieldName="BTRegisterID" runat="server" Theme="Office2010Black"
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
                                                        <dx:GridViewDataColumn Width="20px" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <input type="checkbox" class="chkAll" onchange="CheckAll(this)" />
                                                            </HeaderTemplate>
                                                            <DataItemTemplate>
                                                                <asp:CheckBox data-id='<%# Eval("BTRegisterID") %>' data-EmpCode='<%# Eval("EmployeeCode") %>'
                                                                    runat="server" ID="chkSelect" CssClass="chkSelect" onchange="CheckboxChecked(this)" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>
                                                        <dx:GridViewDataColumn Width="25px" FieldName="HRStatus" Caption="" Visible="false" />
                                                        <dx:GridViewDataColumn Width="25px" Settings-AllowAutoFilter="False" FieldName="No"
                                                            Caption="No" />
                                                        <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Right" Width="50px" FieldName="EmployeeCode"
                                                            Caption="Employee Code">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Full Name" Width="130px" />
                                                        <dx:GridViewBandColumn Caption="Departure">
                                                            <Columns>
                                                                <dx:GridViewDataDateColumn Width="75px" FieldName="FromDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                                <dx:GridViewDataColumn Width="50px" FieldName="FromTime" Caption="Time" />
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewBandColumn Caption="Return">
                                                            <Columns>
                                                                <dx:GridViewDataDateColumn Width="85px" FieldName="ToDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                                <dx:GridViewDataColumn Width="50px" FieldName="ToTime" Caption="Time" />
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewDataColumn FieldName="Purpose" Caption="Destination/Purpose" />
                                                        <dx:GridViewDataImageColumn Width="60px" CellStyle-HorizontalAlign="Left">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnView" runat="server" ToolTip="View" CssClass="grid-btn viewDetails-btn"
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent();"
                                                                    OnClick="btnView_OnClick"></asp:Button>
                                                                <asp:Button ID="btnApprovalNew" Visible='<%# Eval("FIStatus").ToString() <> FIStatus.rejected.ToString() %>'
                                                                    ToolTip="Approve" runat="server" CssClass="grid-btn approval-btn hide" data-id='<%# Eval("BTRegisterID") %>'
                                                                    OnClick="btnApprovalOK_OnClick" OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent();" />
                                                                <input type="button" class="grid-btn approval-btn <%# If(Eval("HRStatus").ToString() = HRStatus.rejected.ToString(), "hide", "") %>"
                                                                    onclick="confirmApproval(this)" title="Approve" />
                                                                <input type="button" class="grid-btn reject-btn <%# If(Eval("HRStatus").ToString() = HRStatus.rejected.ToString(), "hide", "") %>"
                                                                    data-id="<%# Eval("BTRegisterID") %>" title="Reject" onclick="showRejectMessage(this)"
                                                                    data-reject="0" />
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataImageColumn>
                                                    </Columns>
                                                    <%--  <SettingsDetail ShowDetailRow="true" />--%>
                                                </dx:ASPxGridView>
                                                <%--<div id="next-container" class="action-pan">
                                                    <asp:Button runat="server" Text="Approve" OnClientClick="HandleMessage(this); bindStartupEvent()"
                                                        CssClass="btnApproval hide" ID="btnApproval" />
                                                    <input type="button" onclick="confirmApproval(this)" id="btnConfirmApproval" value="Transfer"
                                                        class="btn" />
                                                </div>--%>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="HRTab">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <dx:ASPxGridView ID="grvBTSubmitted" KeyFieldName="BTRegisterID" ClientInstanceName="grvBTSubmitted"
                                                    runat="server" Theme="Office2010Black" AutoGenerateColumns="false">
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
                                                        <%--<dx:GridViewDataColumn Width="20px" HeaderStyle-HorizontalAlign="Center" CellStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <input type="checkbox" class="chkAll" onchange="CheckAll(this)" />
                                                            </HeaderTemplate>
                                                            <DataItemTemplate>
                                                                <asp:CheckBox data-id='<%# Eval("BTRegisterID") %>' runat="server" ID="chkSelect"
                                                                    CssClass="chkSelect" onchange="CheckboxChecked(this)" />
                                                            </DataItemTemplate>
                                                        </dx:GridViewDataColumn>--%>
                                                        <dx:GridViewDataColumn Width="25px" Settings-AllowAutoFilter="False" FieldName="No"
                                                            Caption="No" />
                                                        <dx:GridViewDataTextColumn CellStyle-HorizontalAlign="Right" Width="50px" FieldName="EmployeeCode"
                                                            Caption="Employee Code">
                                                        </dx:GridViewDataTextColumn>
                                                        <dx:GridViewDataColumn FieldName="EmployeeName" Caption="Full Name" Width="130px" />
                                                        <dx:GridViewBandColumn Caption="Departure">
                                                            <Columns>
                                                                <dx:GridViewDataDateColumn Width="75px" FieldName="FromDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                                <dx:GridViewDataColumn Width="50px" FieldName="FromTime" Caption="Time" />
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewBandColumn Caption="Return">
                                                            <Columns>
                                                                <dx:GridViewDataDateColumn Width="85px" FieldName="ToDate" Caption="Date" PropertiesDateEdit-DisplayFormatString="{0:dd-MMM-yyyy}" />
                                                                <dx:GridViewDataColumn Width="50px" FieldName="ToTime" Caption="Time" />
                                                            </Columns>
                                                        </dx:GridViewBandColumn>
                                                        <dx:GridViewDataColumn FieldName="Purpose" Caption="Destination/Purpose" />
                                                        <dx:GridViewDataImageColumn Width="60px">
                                                            <DataItemTemplate>
                                                                <asp:Button ID="btnView" runat="server" ToolTip="View" CssClass="grid-btn viewDetails-btn"
                                                                    data-id='<%# Eval("BTRegisterID") %>' OnClientClick="HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent();"
                                                                    OnClick="btnView_OnClick"></asp:Button>
                                                                <input type="button" class="grid-btn reject-btn" data-id="<%# Eval("BTRegisterID") %>"
                                                                    title="reject" onclick="showRejectMessage(this)" data-reject="1" />
                                                            </DataItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </dx:GridViewDataImageColumn>
                                                    </Columns>
                                                    <%--<SettingsDetail ShowDetailRow="true" />--%>
                                                </dx:ASPxGridView>
                                                <%--<div id="Div1" class="action-pan">
                                                    <asp:Button runat="server" Text="Reject" CssClass="btnReject" ID="btnReject" />
                                                </div>--%>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="clear: both;">
    </div>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <div id="tabRejectMessage" runat="server" class="popup-container">
                <asp:HiddenField runat="server" ID="hRejectBTRegisterID" />
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
                                            HR Recommendation</h3>
                                        <asp:TextBox ID="txtRejectReason" class="reject-reason" Style="width: auto !important;
                                            margin-top: 5px;" TextMode="MultiLine" Rows="5" Columns="60" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding: 0 0 15px; text-align: center;">
                                        <asp:Button ID="btnRejectOK" CssClass="btn" runat="server" Text="Reject" OnClientClick="if(!checkReject()) return false; HandleMessage($('[id$=btnSearch]')[0]); bindStartupEvent()" />
                                        <input type="button" value="Cancel" onclick="hideRejectMessage(); $('[id$=hRejectBTRegisterID]').val(''); $('[id$=txtRejectReason]').val('')"
                                            style="margin-left: 5px;" id="btnRejectCancel" class="btn secondary" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="ScriptPlaceHolder">

    <script src="/js/jquery/jquery.autocomplete.js" type="text/javascript"></script>

    <%-- Request.QueryString("btid") --%>

    <script type="text/javascript">
        var searchTimeOut
        var btid = ''
        $(document).ready(function() {
            //
            employeeCodeAutoComplete()
            //
            if (btid) {
                var $tab = GetParent($(".grid-btn.viewDetails-btn[data-id='" + btid + "']"), ".HRTab")
                if ($tab.size()) {
                    var $tabControl = $tab.parent().parent()
                    $tabControl.find(".HRTab").hide()
                    $tabControl.find(".HRTabNav li").removeClass("current")
                    $tabControl.find(".HRTabNav li:eq(" + $tab.index() + ")").addClass("current")
                    $tab.show()
                }
            }
        })

        function employeeCodeAutoComplete() {
            //            var nhlTeams = ['Anaheim Ducks', 'Atlanta Thrashers', 'Boston Bruins', 'Buffalo Sabres', 'Calgary Flames', 'Carolina Hurricanes', 'Chicago Blackhawks', 'Colorado Avalanche', 'Columbus Blue Jackets', 'Dallas Stars', 'Detroit Red Wings', 'Edmonton OIlers', 'Florida Panthers', 'Los Angeles Kings', 'Minnesota Wild', 'Montreal Canadiens', 'Nashville Predators', 'New Jersey Devils', 'New Rork Islanders', 'New York Rangers', 'Ottawa Senators', 'Philadelphia Flyers', 'Phoenix Coyotes', 'Pittsburgh Penguins', 'Saint Louis Blues', 'San Jose Sharks', 'Tampa Bay Lightning', 'Toronto Maple Leafs', 'Vancouver Canucks', 'Washington Capitals'];

            //            $('.employee-code').devbridgeAutocomplete({
            //                lookup: nhlTeams,
            //                minChars: 1,
            //                onSelect: function(suggestion) {
            //                    //$('#selection').html('You selected: ' + suggestion.value);
            //                },
            //                showNoSuggestionNotice: true,
            //                noSuggestionNotice: 'Not found',
            //                maxHeight: 150
            //            });
        }

        function bindStartupEvent() {
            $(".btnSearch").removeAttr("data-status")
            checkSearchStatus()
        }

        function checkSearchStatus() {
            if ($(".btnSearch").attr("data-status") == "done") {
                employeeCodeAutoComplete()
                SetNumberInputType()
                return
            }
            searchTimeOut = setTimeout('checkSearchStatus()', 10)
        }

        function confirmApproval(me) {
            //            if (!$("[id$=grvBTRegister]").find(":checkbox[id$=chkSelect]:checked").size()) {
            //                ShowErrorMessage("You must choose atleast 1 record. Try again!")
            //                return;
            //            }
            ShowConfirmMessage({
                message: "Do you want to approval this record?",
                OK: function() {
                    $(me).prev().click()
                }
            })
        }
        function showRejectMessage(me) {
            var $this = $(me)
            $("[id$='hRejectBTRegisterID']").val($this.attr("data-id"))
            $this.parent().parent().addClass("selected")
            $("[id$=tabRejectMessage]").stop().fadeIn(100)
            $("[id$=hRejectTypeID]").val($(me).attr("data-reject"))
            $('[id$=txtRejectReason]').focus()
        }

        function hideRejectMessage() {
            $("[id$=tabRejectMessage]").stop().fadeOut(100, function() {
                $("tr.selected").removeClass("selected")
            })
        }

        function checkReject() {
            var $txtRejectReason = $('[id$=txtRejectReason]')
            var rejectReason = $txtRejectReason.val()
            if (!rejectReason || rejectReason.trim().length === 0) {
                ShowErrorMessage("Reject recommendation is required!")
                $txtRejectReason.addClass("validate-error").focus()
                return false
            }
            else {
                $txtRejectReason.removeClass("validate-error")
                return true
            }
        }

    </script>

</asp:Content>
