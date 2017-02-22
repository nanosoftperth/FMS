Imports System.Web
Imports System.Web.Services
Imports System.Net
Imports Newtonsoft.Json.Linq

Public Class QueryParser1
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim url As String = context.Request.QueryString("URL")
        Dim path As String = context.Request.QueryString("path")

        Dim http As New WebClient

        Dim resp As String = http.DownloadString(url)
        Dim jo As New JObject
        Dim obj = JObject.Parse(resp)

        Dim t As String = obj.SelectToken(path).ToString()

        context.Response.ContentType = "text/plain"
        context.Response.Write(t)

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class