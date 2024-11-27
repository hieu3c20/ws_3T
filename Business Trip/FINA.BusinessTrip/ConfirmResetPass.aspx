<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConfirmResetPass.aspx.vb" Inherits="ConfirmResetPass" %>

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
    <h2 id="page-title" style="color:Red">NOTIFICATION – THÔNG BÁO</h2>
        <table width="100%"  cellspacing="0" cellpadding="0" border="0">
        <tbody>
            <tr>
                <td width="100%">
                    <div id="Div1">
                    </div>
                    <div align="center" id="Div2">
                        <table id="tableMainForm" cellpadding="0" cellspacing="0" border="0" style="border-collapse: collapse;
                                                    width: 100%; background:red;">
                            <tbody>
                                <tr> 
                                    <td >
                                        <form style="margin-top: 50px; margin-bottom: 0" id="formlogin" runat="server" action=""
                                        method="post">
                                        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600">
                                        </asp:ScriptManager>
                                       
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel runat="server" ID="panMessage">
                                                </asp:Panel>
                                                <table cellspacing="1" cellpadding="1" border="0" style="border-collapse: collapse;
                                                    width: 100%; background:#fff;" id="tblLoginForm">
                                                    <tbody>
                                                        <tr>
                                                        <td>
                                                            <div align="center" id="login">
                                                               <span style="margin-left:50px; margin-right:50px">
                                                               <h2>
                                                               Your password reset successfully. Please check your email to get new your password
                                                               <br />
                                                               (Mật khẩu mới đã được gửi vào Email cá nhân của Anh/Chị)
                                                               </h2>
                                                               </span>
                                                               <br /><br />
                                                              <asp:Button Style="margin-top: 6px; background-color: #fff;" CssClass="btn inform"
                                                              ID="btnCancel" Text="OK" runat="server" OnClick="btnCancel_Click" OnClientClick="HandleMessage(this)" />  
                                                              <br />
                                                              <br />
                                                            </div>
                                                          </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        </form>
                                    </td>
                                    
                                </tr>
                                
                            </tbody>
                        </table>
                    </div>
                   
    <table width="100%" height="100%" cellspacing="0" cellpadding="0" border="0" class="as">
        <tbody>
            <tr>
                <td width="100%" style="padding-bottom: 150px;">

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
                               
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</body>

</html>
