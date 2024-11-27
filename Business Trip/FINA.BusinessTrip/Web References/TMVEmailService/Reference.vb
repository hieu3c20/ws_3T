﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50727.8662
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.8662.
'
Namespace TMVEmailService
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.8662"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="EmailServiceSoap", [Namespace]:="http://toyotavn.com.vn/")>  _
    Partial Public Class EmailService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private SendEmailOperationCompleted As System.Threading.SendOrPostCallback
        
        Private DeleteFileServerOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.My.MySettings.Default.FINA_BusinessTrip_TMVEmailService_EmailService
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event SendEmailCompleted As SendEmailCompletedEventHandler
        
        '''<remarks/>
        Public Event DeleteFileServerCompleted As DeleteFileServerCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://toyotavn.com.vn/SendEmail", RequestNamespace:="http://toyotavn.com.vn/", ResponseNamespace:="http://toyotavn.com.vn/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function SendEmail(ByVal from As String, ByVal [to] As String, ByVal cc As String, ByVal bcc As String, ByVal subject As String, ByVal body As String, ByVal attachments As String, ByVal connectionString As String) As Boolean
            Dim results() As Object = Me.Invoke("SendEmail", New Object() {from, [to], cc, bcc, subject, body, attachments, connectionString})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub SendEmailAsync(ByVal from As String, ByVal [to] As String, ByVal cc As String, ByVal bcc As String, ByVal subject As String, ByVal body As String, ByVal attachments As String, ByVal connectionString As String)
            Me.SendEmailAsync(from, [to], cc, bcc, subject, body, attachments, connectionString, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub SendEmailAsync(ByVal from As String, ByVal [to] As String, ByVal cc As String, ByVal bcc As String, ByVal subject As String, ByVal body As String, ByVal attachments As String, ByVal connectionString As String, ByVal userState As Object)
            If (Me.SendEmailOperationCompleted Is Nothing) Then
                Me.SendEmailOperationCompleted = AddressOf Me.OnSendEmailOperationCompleted
            End If
            Me.InvokeAsync("SendEmail", New Object() {from, [to], cc, bcc, subject, body, attachments, connectionString}, Me.SendEmailOperationCompleted, userState)
        End Sub
        
        Private Sub OnSendEmailOperationCompleted(ByVal arg As Object)
            If (Not (Me.SendEmailCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent SendEmailCompleted(Me, New SendEmailCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://toyotavn.com.vn/DeleteFileServer", RequestNamespace:="http://toyotavn.com.vn/", ResponseNamespace:="http://toyotavn.com.vn/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function DeleteFileServer(ByVal fileName As String) As Boolean
            Dim results() As Object = Me.Invoke("DeleteFileServer", New Object() {fileName})
            Return CType(results(0),Boolean)
        End Function
        
        '''<remarks/>
        Public Overloads Sub DeleteFileServerAsync(ByVal fileName As String)
            Me.DeleteFileServerAsync(fileName, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub DeleteFileServerAsync(ByVal fileName As String, ByVal userState As Object)
            If (Me.DeleteFileServerOperationCompleted Is Nothing) Then
                Me.DeleteFileServerOperationCompleted = AddressOf Me.OnDeleteFileServerOperationCompleted
            End If
            Me.InvokeAsync("DeleteFileServer", New Object() {fileName}, Me.DeleteFileServerOperationCompleted, userState)
        End Sub
        
        Private Sub OnDeleteFileServerOperationCompleted(ByVal arg As Object)
            If (Not (Me.DeleteFileServerCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent DeleteFileServerCompleted(Me, New DeleteFileServerCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.8662")>  _
    Public Delegate Sub SendEmailCompletedEventHandler(ByVal sender As Object, ByVal e As SendEmailCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.8662"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class SendEmailCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.8662")>  _
    Public Delegate Sub DeleteFileServerCompletedEventHandler(ByVal sender As Object, ByVal e As DeleteFileServerCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.8662"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class DeleteFileServerCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Boolean
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Boolean)
            End Get
        End Property
    End Class
End Namespace
