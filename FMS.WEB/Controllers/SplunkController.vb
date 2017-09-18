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

Public Class SplunkController
    Inherits ApiController

    Public Function GetEmaxiCANMessages(deviceID As String) As List(Of CAN_MessageDefinition)
        Dim oDash As New DashboardController
        Dim oSplunkAPI As New FMS.WEBAPI.Controllers.SplunkAPIController

        'Return emaxiCSNMsgLst

    End Function

End Class
