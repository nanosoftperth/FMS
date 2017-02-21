Imports System.Net

Public Class QueryParser
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Dim url As String = Request.QueryString("URL")

        Dim http As New WebClient

        Dim resp As String = http.DownloadString(url)


        Page.Response.Write(resp)

    End Sub

End Class