<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UcFileManager.ascx.vb"
    Inherits="UcFileManager" %>
<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxFileManager" TagPrefix="dx" %>
<div class="file-manager-container">
    <dx:ASPxFileManager ID="ASPxFileManager" runat="server">
        <Settings RootFolder="~\" ThumbnailFolder="~\Thumb\" />
    </dx:ASPxFileManager>
</div>
