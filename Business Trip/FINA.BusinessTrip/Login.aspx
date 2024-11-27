<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Login" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>    
    <title>Business Trip Online System</title>
    <link rel="stylesheet" type="text/css" href="CSS/Login.css">
    <link rel="stylesheet" type="text/css" href="CSS/Button.css">
    <link rel="shortcut icon" href="/images/icon.ico" />

    <script src="/js/jquery/jquery-1.11.1.min.js" type="text/javascript"></script>

    <script src="/js/jquery/datepicker.js" type="text/javascript"></script>

    <script src="/js/application.js" type="text/javascript"></script>

</head>
<body bottommargin="0" rightmargin="0" leftmargin="0" topmargin="0">
    <h2 id="page-title" style="display: none;">
        Login</h2>
    <table width="100%" height="100%" cellspacing="0" cellpadding="0" border="0" class="as">
        <tbody>
            <tr>
                <td width="100%" style="padding-bottom: 150px;">
                    <div id="line_x">
                    </div>
                    <div align="center" id="login">
                        <table id="tableMainForm" cellpadding="0" cellspacing="0" border="0">
                            <tbody>
                                <tr>
                                    <td style="line-height: 100%;" colspan="3">
                                        <img border="0" src="images/bts-login.png" alt="bts-login">
                                    </td>
                                </tr>
                                <tr>
                                    <td width="1" style="line-height: 100%; background: url(images/si3.jpg) center repeat-y">
                                        <%-- <img style="width:1px; height:100%;" border="0" src="images/si3.jpg"/>--%>
                                    </td>
                                    <td style="/* width: 24px; */background: url(/images/bts-login-sub.png) center center no-repeat;">
                                        <form style="margin-top: 0; margin-bottom: 0" id="formlogin" runat="server" action=""
                                        method="post">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600">
                                        </asp:ScriptManager>
                                        <asp:UpdateProgress ID="UpdateProgress" runat="server" DisplayAfter="0">
                                            <ProgressTemplate>
                                                <div style="position: fixed; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(255, 255, 255, 0.3);
                                                    z-index: 10000;">
                                                </div>
                                                <div style="position: fixed; top: 0; right: 0; width: 32px; height: 32px; padding: 5px;
                                                    background: url(/images/ajax-loading.gif) center center no-repeat; background-color: rgba(0, 0, 0, 0.6);
                                                    z-index: 10001; box-shadow: 0 0 10px #666; -webkit-box-shadow: 0 0 10px #666;
                                                    -moz-box-shadow: 0 0 10px #666; border-radius: 30px; -webkit-border-radius: 30px;
                                                    -moz-border-radius: 30px;">
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel runat="server" ID="panMessage">
                                                </asp:Panel>
                                                <table cellspacing="1" cellpadding="1" border="0" style="border-collapse: collapse;
                                                    width: 100%;" id="tblLoginForm">
                                                    <tbody>
                                                   
                                                        <tr>
                                                            <td colspan="2" style="text-align: center">
                                                                &nbsp;
                                                                <asp:Label Style="color: Red; margin-top: 3px" ID="txtMessage" runat="server" Text=""></asp:Label>
                                                            </td>
                                                        </tr>
                                                         
                                                        <tr>
                                                            <td width="138" style="line-height: 100%; color: Black">
                                                                <p align="right">
                                                                    Employee Code:
                                                                </p>
                                                            </td>
                                                            <td style="line-height: 100%;">
                                                                <asp:TextBox CssClass="loginInput" data-type="int" ID="txtUserName" runat="server"
                                                                    MaxLength="6"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="138" style="line-height: 100%; padding-top: 5px; color: Black">
                                                                <p align="right" style="margin-top: 0px">
                                                                    Password:
                                                                </p>
                                                            </td>
                                                            <td style="line-height: 100%; padding-top: 5px;">
                                                                <asp:TextBox CssClass="loginInput" ID="txtPassword" autocomplete="off" runat="server"
                                                                    TextMode="Password"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="138" style="line-height: 100%;">
                                                                &nbsp;
                                                            </td>
                                                            <td style="line-height: 100%; padding-top: 10px;">
                                                                <span class="btn inform" style="padding-left: 6px;"><i class="login" style="margin-right: 10px;">
                                                                </i><span>Login</span>
                                                                    <asp:Button Style="margin-top: 6px; background-color: #fff;" CssClass="btn inform"
                                                                        ID="btnLogin" Text="Login" runat="server" OnClick="btnLogin_Click" OnClientClick="HandleMessage(this)" />  
                                                                </span>
                                                              <span style="padding-left: 6px;">
                                                                     <a href="ResetPass.aspx">Reset Password</a> 
                                                                </span>
                                                            </td> 
                                                            
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        </form>
                                    </td>
                                    <td width="1" style="line-height: 100%; background: url(images/si4.jpg) center repeat-y">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="line-height: 100%;" colspan="3">
                                        <img border="0" src="images/si2.png" alt="si2" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div id="info">
                        <table width="100%" cellspacing="0" cellpadding="0" border="0" id="table4">
                            <tbody>
                              
                                <tr>
                                    <td class="col1">
                                        <div align="right">
                                            <table cellspacing="0" cellpadding="0" border="0" id="copyright">
                                                <tbody>
                                                    <tr>
                                                        <td style="padding-left: 16px">
                                                            <b>TMV BUSINESS TRIP ONLINE SYSTEM</b>
                                                            <br>
                                                            <img src="images/red-arrow.png" style="margin-left: 10px" />
                                                            Best screen resolution: 1024x768
                                                            <br>
                                                                <img src="images/red-arrow.png" style="margin-left: 10px" />
                                                                Best browser: Firefox and IE 10+</br>
                                                            <b>Version 2015</b>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </td>
                                    <td style="width: 1px">
                                        <img vspace="1" hspace="1" border="0" align="left" src="images/toyota.png" alt="st">
                                    </td>
                                    <td class="col2">
                                        <b>TOYOTA MOTOR VIETNAM CO., LTD</b>
                                        <br>
                                        <b>Address:</b> Head Quarter: Phuc Thang Ward, Phuc Yen Town, Vinh Phuc Province
                                        <br />
                                        <b>Tel</b>: (0211) 3 868100-112
                                        <br />
                                        <b>Fax</b>: (0211) 3 868117
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 13px" class="col1">
                                    </td>
                                    <td style="height: 13px" class="col2">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</body>
<asp:literal runat="server" id="StartupJS"></asp:literal>
</html>
