﻿Imports Splunk.Logging
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
Imports System.Runtime.Caching
Imports FMS.WEB.Controllers
Imports System.Globalization
Imports System.Web.Http.Results

Public Class SplunkController
    Inherits ApiController

    'Hi Cesar, we dont want to reference the other controller at all, hopefully we can remove any reference the FMS.WebAPI project at all 

    <System.Web.Http.AcceptVerbs("GET", "POST")>
    <Route("api/Splunk/SendCANValues")>
    Public Function SendCANValues(ApplicatoinName As String, StartDate As DateTime, EndDate As DateTime) As IHttpActionResult
        Try
            ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) (True)

            Dim middleware = New HttpEventCollectorResendMiddleware(100)

            'Dim ecSender = New HttpEventCollectorSender(New Uri("http://localhost:8088"),            
            'Dim ecSender = New HttpEventCollectorSender(New Uri("http://demo.nanosoft.com.au:8088"),
            'Dim ecSender = New HttpEventCollectorSender(New Uri("http://localhost:8088"),
            Dim ecSender = New HttpEventCollectorSender(New Uri("http://demo.nanosoft.com.au:8088"),
                    "6179e399-bd29-47d1-954d-ca9313079eb5",
                    Nothing,
            HttpEventCollectorSender.SendMode.Sequential,
            0,
            0,
            0,
            AddressOf middleware.Plugin
                )

            Dim strAppName As String

            Dim name As String = RegionInfo.CurrentRegion.DisplayName

            Dim currentCulture As CultureInfo
            currentCulture = CultureInfo.CurrentCulture

            Dim cultureName As CultureInfo = New CultureInfo(currentCulture.Name)
            Dim shortUsDateFormatString As String = cultureName.DateTimeFormat.ShortDatePattern

            strAppName = ApplicatoinName
            'Dim sd = Format(StartDate, shortUsDateFormatString + " 12:00:00")
            'Dim ed = Format(EndDate, shortUsDateFormatString + " 23:59:59")
            Dim sd = Format(StartDate, shortUsDateFormatString)
            Dim ed = Format(EndDate, shortUsDateFormatString)

            Dim new_cache_obj = New List(Of String)
            Dim tblSplunk As List(Of SplunkTable) = New List(Of SplunkTable)

            Dim app = Business.DataObjects.Application.GetFromApplicationName(strAppName)
            Dim vehicles = Business.DataObjects.ApplicationVehicle.GetAll(app.ApplicationID)

            For Each av As Business.DataObjects.ApplicationVehicle In vehicles

                If (Not av.CAN_Protocol_Type = "j1939") Then

                    For Each x In av.GetAvailableCANTags

                        Dim strTag = String.Format("{0}>{1}>{2}>{3}", av.Name, x.Standard, x.SPN, IIf(x.Name = Nothing, "", x.Name))
                        Dim TagValues = GetSPNValues(strTag, sd, ed)

                        For Each ntag In TagValues
                            Dim nRowSplunk As New SplunkTable

                            nRowSplunk.VehicleName = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).VehicleName
                            nRowSplunk.Standard = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).Standard
                            nRowSplunk.SPN = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).SPN
                            nRowSplunk.SPNValue = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).SPNValue
                            nRowSplunk.Description = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).Description
                            nRowSplunk.Time = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).Time

                            tblSplunk.Add(nRowSplunk)
                        Next

                    Next

                End If
            Next

            If (tblSplunk.Count > 0) Then

                Dim serializer = New JavaScriptSerializer() With {.MaxJsonLength = 819200000}
                Dim json = serializer.Serialize(tblSplunk).Replace("/", "").Replace("\", "")
                'Dim json = serializer.Serialize(tblSplunk)

                'Sanitize json before sending
                Dim sb = New StringBuilder(json)
                sb.Replace("\\\t", "\t")
                sb.Replace("\\\n", "\n")
                sb.Replace("\\\r", "\r")

                'var sb = New StringBuilder(uglyJson);
                'sb.Replace("\\\t", "\t");
                'sb.Replace("\\\n", "\n");
                'sb.Replace("\\\r", "\r");
                'Return sb.ToString();


                ecSender.Send(Guid.NewGuid.ToString, "INFO", Nothing, sb)
                ecSender.FlushAsync()
            End If

            Return Json(True)
        Catch
            Return Json(False)
        End Try
    End Function

    '<Route("api/Splunk/GetTagVals")>
    '<HttpPost()>
    '<Route("api/Splunk")>
    '<ResponseType(GetType(IHttpActionResult))>
    '<Route("api/Splunk/SendTagValues")>
    '<HttpPost>
    '<ActionName("SendTagValues")>
    '<HttpPost, Route("api/Splunk/SendTagValues")>
    <System.Web.Http.AcceptVerbs("GET", "POST")>
    <System.Web.Http.HttpGet>
    <Route("api/Splunk/SendTagValues")>
    Public Function SendTagValues(ApplicatoinName As String, StartDate As DateTime, EndDate As DateTime) As IHttpActionResult
        'Public Function SendTagValues(<FromBody> ApplicatoinName As String) As IHttpActionResult
        Try

            'i think we probably want to send something like 
            '
            'for application: uniqco 
            '
            '
            'TAG NAME           |   TIME                |   VALUE
            'x                  |  1/1/2017 00:00:01    |   2
            'x                  |  1/1/2017 00:00:02    |   2
            'x                  |  1/1/2017 00:00:03    |   2
            'x                  |  1/1/2017 00:00:04    |   2
            'Y                  |  1/1/2017 00:00:01    |   2
            'Y                  |  1/1/2017 00:00:02    |   2
            '

            ServicePointManager.ServerCertificateValidationCallback = Function(sender, certificate, chain, sslPolicyErrors) (True)

            Dim middleware = New HttpEventCollectorResendMiddleware(100)

            'Dim ecSender = New HttpEventCollectorSender(New Uri("http://localhost:8088"),            
            'Dim ecSender = New HttpEventCollectorSender(New Uri("http://localhost:8088"),
            Dim ecSender = New HttpEventCollectorSender(New Uri("http://demo.nanosoft.com.au:8088"),
                    "6179e399-bd29-47d1-954d-ca9313079eb5",
                    Nothing,
            HttpEventCollectorSender.SendMode.Sequential,
            0,
            0,
            0,
            AddressOf middleware.Plugin
                )

            'there appears to be no error delegate defined in VB ?!?  

            '--- For testing
            Dim oDashQuery = New FMS.WEB.DashboardController
            Dim strAppName As String
            'strAppName = "demo"
            'Dim sd = DateTime.Parse("09/15/2017")
            'Dim ed = Format(Now, "MM/dd/yyyy")


            Dim name As String = RegionInfo.CurrentRegion.DisplayName

            Dim currentCulture As CultureInfo
            currentCulture = CultureInfo.CurrentCulture

            Dim cultureName As CultureInfo = New CultureInfo(currentCulture.Name)
            Dim shortUsDateFormatString As String = cultureName.DateTimeFormat.ShortDatePattern

            strAppName = ApplicatoinName
            Dim sd = Format(StartDate, shortUsDateFormatString)
            Dim ed = Format(EndDate, shortUsDateFormatString)
            'Dim sd = Format(StartDate, "MM/dd/yyyy")
            'Dim ed = Format(EndDate, "MM/dd/yyyy")

            'Dim url As String = "http://myserver/method"
            'Dim content As String = "param1=1&param2=2"
            'Dim handler As New HttpClientHandler()
            'Dim httpClient As New HttpClient(handler)
            ''Dim request As New HttpRequestMessage(HttpMethod.Post, url)

            'Dim request As New HttpRequestMessage

            'With request
            '    .Headers.Add("", "")
            '    .Method()
            'End With

            'Dim application = Business.DataObjects.Application.GetFromApplicationName(strAppName)

            ''Dim request As HttpRequestMessage

            'Dim jsonString = request.Content.ReadAsStringAsync().Result

            'Dim jSerializer = New JavaScriptSerializer()

            'Dim lok As DashboardController.queryRequestType = jSerializer.Deserialize(Of DashboardController.queryRequestType)(jsonString)

            'Dim queryReturnList As New List(Of DashboardController.queryReturnType)

            'Dim startTime As Date = CDate(lok.range.from)
            'Dim endTime As Date = CDate(lok.range.to)

            ''grab the first target to determine if the result set should be timeserie or table
            'Dim isTimeserie As Boolean = lok.targets(0).type = "timeserie"

            'Dim oList As Object

            'If isTimeserie Then
            '    oList = GetTimeSeriesResultForSplunk(lok, startTime, endTime)
            'Else
            '    oList = GetTimeSeriesResultForSplunk(lok, startTime, endTime)
            'End If
            '-----------------------------------
            Dim new_cache_obj = New List(Of String)
            'Dim oListSplunk As List(Of ForSplunk) = New List(Of ForSplunk)
            Dim tblSplunk As List(Of SplunkTable) = New List(Of SplunkTable)
            'Dim TagValues As New Object

            Dim app = Business.DataObjects.Application.GetFromApplicationName(strAppName)
            Dim vehicles = Business.DataObjects.ApplicationVehicle.GetAll(app.ApplicationID)

            For Each av As Business.DataObjects.ApplicationVehicle In vehicles

                If (Not av.CAN_Protocol_Type = "j1939") Then

                    For Each x In av.GetAvailableCANTags
                        'new_cache_obj.Add(String.Format("{0}>{1}>{2}>{3}", av.Name, x.Standard, x.SPN, x.Name))
                        'vehicle>standard>spn>description
                        'If (x.Standard = "Zagro500") Then

                        'End If

                        Dim strTag = String.Format("{0}>{1}>{2}>{3}", av.Name, x.Standard, x.SPN, IIf(x.Name = Nothing, "", x.Name))
                        'Dim TagValues = GetSPNValues(strTag, sd, ed)
                        Dim TagValues = GetSPNValues(strTag, sd, ed)

                        For Each ntag In TagValues
                            'Dim nRowSplunk As New ForSplunk
                            Dim nRowSplunk As New SplunkTable
                            'Dim strVehicle = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).VehicleName
                            'Dim strStand = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).Standard
                            'Dim strSPN = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).SPN
                            'Dim strDesc = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).Description
                            'Dim dtTime = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).Time
                            'Dim strValue = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).SPNValue

                            'Dim strRec = String.Format("{0}>{1}>{2}>{3}:", strVehicle, strStand, strSPN, strDesc)

                            nRowSplunk.VehicleName = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).VehicleName
                            nRowSplunk.Standard = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).Standard
                            nRowSplunk.SPN = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).SPN
                            nRowSplunk.SPNValue = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).SPNValue
                            nRowSplunk.Description = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).Description
                            nRowSplunk.Time = DirectCast(ntag, FMS.WEB.SplunkController.SplunkTable).Time

                            'oListSplunk.Add(nRowSplunk)
                            tblSplunk.Add(nRowSplunk)
                        Next



                        'new_cache_obj.Add(String.Format("{0}>{1}>{2}>{3}", av.Name, x.Standard, x.SPN, x.Name))



                    Next

                End If
            Next


            'Dim CACHE_NAME = "CACHE_NAME_LIST" & "|" & strAppName

            'Dim cache_obj As List(Of String) = MemoryCache.Default.Get(CACHE_NAME)

            'MemoryCache.Default.Set(CACHE_NAME, new_cache_obj, New DateTimeOffset(Now.AddHours(1)))

            '--- End of Testing

            ''below I am just making a pretend list of tags
            'Dim tagList As New List(Of String)

            'For i As Integer = 0 To 1000
            '    tagList.Add(Guid.NewGuid.ToString)
            'Next



            If (tblSplunk.Count > 0) Then

                'Dim json = (New JavaScriptSerializer).Serialize(tagList)
                'Regex.Replace(yourString, "[^A-Za-z0-9\-/]", "")
                'Console.WriteLine(header.Trim( { " "c, "*"c, "."c } ))
                ' Dim json = (New JavaScriptSerializer).Serialize(tblSplunk)
                Dim serializer = New JavaScriptSerializer() With {.MaxJsonLength = 819200000}
                Dim json = serializer.Serialize(tblSplunk).Replace("/", "").Replace("\", "")
                'Dim json = (New JavaScriptSerializer).Serialize(tblSplunk).Replace("/", "").Replace("\", "")

                ecSender.Send(Guid.NewGuid.ToString, "INFO", Nothing, json)
                ecSender.FlushAsync()
            End If



        Catch ex As Exception
            Throw ex
        End Try

        Return Ok()

    End Function




    Public Function GetSPNValues(strTag As String, sd As DateTime, ed As DateTime) As Object
        Try

            Dim strs() As String = strTag.Split(">")
            Dim vehicleName As String = strs(0)
            Dim standard As String = strs(1)
            Dim spn As Integer = CInt(strs(2))
            Dim spnNAme As String = strs(3)

            Dim canDataPoint = Business.DataObjects.CanDataPoint.GetPointWithData(spn, vehicleName, standard, sd, ed)
            Dim strDescription = canDataPoint.MessageDefinition.Description

            Dim oList As List(Of SplunkTable) = New List(Of SplunkTable)
            Dim RowSplunk As New SplunkTable

            For Each nRow In canDataPoint.CanValues

                RowSplunk.VehicleName = vehicleName
                RowSplunk.Standard = standard
                RowSplunk.SPN = spn
                RowSplunk.Description = strDescription
                RowSplunk.SPNValue = nRow.ValueStr
                RowSplunk.Time = nRow.Time

                oList.Add(RowSplunk)

            Next

            Return oList

        Catch ex As Exception

        End Try


    End Function

    Public Function GetTimeSeriesResultForSplunk(lok As DashboardController.queryRequestType, startTime As Date, endTime As Date) As Object

        Dim retobj As New List(Of DashboardController.queryReturnType)

        For Each x As DashboardController.cust_target In lok.targets

            Dim qrt = New DashboardController.queryReturnType With {.target = x.target}

            'new_cache_obj.Add(String.Format("{0}>{1}>{2}>{3}", av.Name, x.Standard, x.SPN, x.Name))
            Dim strs() As String = x.target.Split(">")
            Dim vehicleName As String = strs(0)
            Dim standard As String = strs(1)
            Dim spn As Integer = CInt(strs(2))
            Dim spnNAme As String = strs(3)

            Dim canDataPoint = Business.DataObjects.CanDataPoint.GetPointWithData(spn, vehicleName, standard, startTime, endTime)

            Dim cnt As Integer = canDataPoint.CanValues.Count

            Dim dataPoints As Long(,) = New Long(cnt - 1, 1) {}

            Dim i As Integer = 0

            For Each cv In canDataPoint.CanValues

                Try
                    'Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds

                    Dim unixTimestamp = CLng(cv.Time.AddHours(-8).Subtract(CDate("01/jan/1970")).TotalSeconds) * 1000
                    Dim val As Long = cv.Value

                    If cv.Time >= startTime AndAlso cv.Time <= endTime Then
                        dataPoints(i, 0) = val
                        dataPoints(i, 1) = unixTimestamp
                    End If

                    i += 1

                Catch ex As Exception

                End Try
            Next

            qrt.datapoints = dataPoints

            retobj.Add(qrt)
        Next


        Return retobj


    End Function

#Region "Containers"
    Public Class SplunkTable
        Public Property VehicleName As String
        Public Property Standard As String
        Public Property SPN As String
        Public Property SPNValue As String
        Public Property Description As String
        Public Property Time As DateTime
    End Class

    Public Class ForSplunk
        Public Property RecStr As String
        Public Property RecVal As String
        Public Property RecTime As DateTime

    End Class

#End Region

End Class
