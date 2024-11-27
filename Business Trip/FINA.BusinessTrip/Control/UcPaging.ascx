<%@ Control Language="vb" AutoEventWireup="false" CodeFile="UcPaging.ascx.vb" Inherits="FINA.BusinessTrip.UcPaging" %>

<asp:HiddenField ID="_TongSoBanGhi" runat="server" />
<asp:HiddenField ID="_CurrentPage" runat="server" />
<asp:HiddenField ID="_PageSize" runat="server" />
<asp:HiddenField ID="_SoBanGhiTrongPage" runat="server" />
<asp:HiddenField ID="_DefaultShowPageSize" runat="server" />
<asp:HiddenField ID="_ShowPageSize" runat="server" />

 <div class="ddladmin">

        <div class="dataTables_info" id="dataTables-1_info" role="alert" aria-live="polite" aria-relevant="all"> 
                <asp:PlaceHolder runat="server" ID="plhShowing" Visible="false">Showing <asp:Label runat="server" ID="lblBanGhiFrom" /> to <asp:Label runat="server" ID="lblBanGhiTo" /> of total <asp:Label runat="server" ID="lblTongSoBanGhi" /> entries</asp:PlaceHolder>
        </div>
        <div class="dataTables_paginate paging_simple_numbers" id="dataTables-1_paginate">
           
            <asp:LinkButton ID="_previous" runat="server" CssClass="paginate_button previous" 
                        aria-controls="dataTables-1" tabindex="0" 
                        OnCommand="ControlCommand" CommandName="_previous" >Previous
            </asp:LinkButton>
            <span>
                    <asp:Repeater ID="rptCurrentPage" runat="server" >
                        <ItemTemplate>
                                <a class='paginate_button <%#Eval("CurrentCss") %>' href='<%#Eval("Href") %>' tabindex='<%#Eval("Value") %>' ><%#Eval("Text")%></a>
                        </ItemTemplate>
                    </asp:Repeater>
            </span>
            <asp:LinkButton ID="_next" runat="server" CssClass="paginate_button previous" 
                        aria-controls="dataTables-1" tabindex="0" 
                        OnCommand="ControlCommand" CommandName="_next" >Next
            </asp:LinkButton>
        </div>
        
        <asp:Label ID="lblMessage" runat=server />
        
</div>