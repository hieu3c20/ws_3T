Imports System.Web
Imports System.Web.Services
Imports Provider
Imports System.IO

Public Class imagehandle
    Implements System.Web.IHttpHandler, IRequiresSessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "image/jpeg"
        context.Response.Expires = 0
        context.Response.Buffer = True
        context.Response.Clear()

        Dim NewsId As String = context.Session("UserName")
        Dim objUser As New tbl_UsersInfo()

        objUser = UserProvider.tbl_User_GetUserInfo_ByUserName(NewsId)
        Dim data As Byte() = Nothing
        If objUser IsNot Nothing Then
            data = DirectCast(objUser.Photograph, Byte())            
        End If
        If data Is Nothing OrElse data.Length = 0 Then
            Dim imgPath As String = context.Server.MapPath(If(NewsId = "000000", "~/images/guard.png", "~/images/check.png"))
            Dim f As New FileInfo(imgPath)
            Dim rd As New BinaryReader(f.OpenRead())
            data = rd.ReadBytes(CInt(f.Length))
            f.Refresh()
        End If
        context.Response.BinaryWrite(data)
        context.Response.End()
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class