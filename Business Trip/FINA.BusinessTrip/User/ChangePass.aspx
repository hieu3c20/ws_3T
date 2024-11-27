<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChangePass.aspx.vb" Inherits=".ChangePass"
    MasterPageFile="~/MasterPage.Master" %>
    

      
<asp:Content runat="server" ContentPlaceHolderID="TitlePlaceHolder">
    Profile &raquo; 
    <asp:Literal ID="txtMenu" runat="server"></asp:Literal>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
                  
            <asp:UpdatePanel runat="server">    
             <ContentTemplate>  
            <asp:Panel runat="server" ID="panMessage"></asp:Panel>    
            <fieldset style="border-radius: 10px;">
                <legend style="color:#234A69">User Information</legend>   
                 
            <table style="margin-left: 40px;width: auto; margin-top: 20px; text-align:right" class="grid-edit" id="tblChangePass">
                 <tr>
                    <td style="padding-right: 10px">
                        User Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" TextMode="SingleLine" 
                            Font-Bold="True" ReadOnly="True"></asp:TextBox>                        
                    </td>
                    <td style="width:150px; padding-right: 10px">
                        Divison Name:
                    </td>
                    <td>
                         <asp:TextBox ID="txtDivision" runat="server" TextMode="SingleLine" 
                                Font-Bold="True" ReadOnly="True"></asp:TextBox>  
                    </td>
                </tr>
                <tr>
                    <td style="padding-right: 10px">
                        Full Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtFullName" runat="server" TextMode="SingleLine" 
                            Font-Bold="True" ReadOnly="True"></asp:TextBox>                        
                    </td>
                    <td style="padding-right: 10px">
                        Branch Name:
                    </td>
                    <td>
                         <asp:TextBox ID="txtBranch" runat="server" TextMode="SingleLine" 
                                Font-Bold="True" ReadOnly="True"></asp:TextBox>  
                    </td>
                </tr>
                
                <tr>
                    <td style="padding-right: 10px">
                        Email:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="SingleLine" 
                            Font-Bold="True" ReadOnly="True"></asp:TextBox>                        
                    </td>
                    <td style="padding-right: 10px">
                        Department Name:
                    </td>
                    <td>
                         <asp:TextBox ID="txtDepartment" runat="server" TextMode="SingleLine" 
                                Font-Bold="True" ReadOnly="True"></asp:TextBox>  
                    </td>
                </tr>
             <%--   <% If Request.QueryString("uid") Is Nothing Then%><% End If%>--%>
              
                  <tr runat="server" id="trOldPass">
                        <td style="padding-right: 10px">
                            Current Password:
                        </td>
                        <td>
                            <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
                            
                        </td>
                        <td colspan="2" style="text-align:left; padding-left:16px">
                            <asp:Label runat="server" ID="txtMesOldPass" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
              
                <tr>
                    <td style="padding-right: 10px">
                        New Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox>
                       
                    </td>
                    <td colspan="2" style="text-align:left; padding-left:45px">
                         <asp:Label runat="server" ID="txtMesPass" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="padding-right: 10px">
                        Retype New Password:
                    </td>
                    <td>
                        <asp:TextBox ID="txtRePassword" runat="server" TextMode="Password"></asp:TextBox>
                       
                    </td>
                    <td colspan = "2" style="text-align:left" padding-left:45px>
                        <asp:Label runat="server" ID="txtMesRePass" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="trFirstChange">
                    <td>
                       
                    </td>
                    <td>                        
                        <asp:CheckBox ID="chkChangePass" runat="server" Checked="True" />  User must change password at next logon                    
                    </td>
                    <td>
                    </td>
                </tr>
                

            </table>  
            </fieldset>
                        <div class="action-pan">   
                        <asp:Button CssClass="btn" ID="btnChangePass" OnClientClick="HandleMessage(this)"
                            Text="Save" runat="server" />
                            <asp:Button style="margin-left: 5px" CssClass="btn secondary" ID="btnCancel"
                            Text="Cancel" runat="server"  />
                        </div>
            </ContentTemplate>
          </asp:UpdatePanel>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptPlaceHolder">
</asp:Content>
