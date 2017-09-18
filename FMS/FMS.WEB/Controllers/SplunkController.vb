Imports Splunk.Logging
Imports FMS.WEBAPI
'Imports NanoSplunk_WebAPI.Models
Imports System
Imports System.Collections.Generic
'Imports System.Data.Entity
'Imports System.Data.Entity.Infrastructure
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http
Imports System.Web.Http.Description
Imports FMS.Business.DataObjects
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq

Public Class SplunkController
    Inherits ApiController


    'Hi Cesar, we dont want to reference the other controller at all, hopefully we can remove any reference the FMS.WebAPI project at all 



    <HttpPost()>
    <Route("api/SplunkAPI/GeoFence")>
    <ResponseType(GetType(IHttpActionResult))>
    Public Function SendTagList(ApplicatoinName As String) As IHttpActionResult

        Try

            ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) (True)

            Dim middleware = New HttpEventCollectorResendMiddleware(100)

            Dim ecSender = New HttpEventCollectorSender(New Uri("http://demo.nanosoft.com.au:8088"),
                    "F018028D-5CFF-495F-A06C-1D3D2D80C379",
                    Nothing,
                    HttpEventCollectorSender.SendMode.Sequential,
                    0,
                    0,
                    0,
                    AddressOf middleware.Plugin
                )

            'there appears to be no error delegate defined in VB ?!?

            'below I am just making a pretend list of tags
            Dim tagList As New List(Of String)

            For i As Integer = 0 To 1000
                tagList.Add(Guid.NewGuid.ToString)
            Next


            Dim json = (New JavaScriptSerializer).Serialize(tagList)

            ecSender.Send(Guid.NewGuid.ToString, "INFO", Nothing, json)
            ecSender.FlushAsync()


        Catch ex As Exception

        End Try






    End Function



        Catch ex As Exception

        End Try




    End Function


    'Public Function GetEmaxiCANMessages(deviceID As String) As List(Of CAN_MessageDefinition)
    '    Dim oDash As New DashboardController


    '    'Return emaxiCSNMsgLst

    'End Function

End Class
