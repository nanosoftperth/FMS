Imports FMS.Business
Imports System.Web

Public Class ThisSession

#Region "properties"

    Public Shared Property rm_DriverVehicleTimeReload As Boolean
        Get
            Return HttpContext.Current.Session("DriverVehicleTimeReload")
        End Get
        Set(value As Boolean)
            HttpContext.Current.Session("DriverVehicleTimeReload") = value
        End Set
    End Property

    Public Shared Property rm_ApplicationDriverVehicleTimes As List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime)
        Get
            Return HttpContext.Current.Session("ApplicationDriverVehicleTimes")
        End Get
        Set(value As List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime))
            HttpContext.Current.Session("ApplicationDriverVehicleTimes") = value
        End Set
    End Property


    Public Shared Property CurrentSelectedGroup As Guid
        Get
            Return HttpContext.Current.Session("CurrentSelectedGroup")
        End Get
        Set(value As Guid)
            HttpContext.Current.Session("CurrentSelectedGroup") = value
        End Set
    End Property

    Public Shared Property ApplicationVehicleID As Guid
        Get
            Return HttpContext.Current.Session("ApplicationVehicleID")
        End Get
        Set(value As Guid)
            HttpContext.Current.Session("ApplicationVehicleID") = value
        End Set
    End Property

    Public Shared Property ClientID As Integer
        Get
            Return HttpContext.Current.Session("ClientID")
        End Get
        Set(value As Integer)
            HttpContext.Current.Session("ClientID") = value
        End Set
    End Property

    Public Shared Property RunID As Integer
        Get
            Return HttpContext.Current.Session("RunID")
        End Get
        Set(value As Integer)
            HttpContext.Current.Session("RunID") = value
        End Set
    End Property

    Public Shared Property ServiceID As Integer
        Get
            Return HttpContext.Current.Session("ServiceID")
        End Get
        Set(value As Integer)
            HttpContext.Current.Session("ServiceID") = value
        End Set
    End Property
    Public Shared Property SiteID As Integer
        Get
            Return HttpContext.Current.Session("SiteID")
        End Get
        Set(value As Integer)
            HttpContext.Current.Session("SiteID") = value
        End Set
    End Property
    Public Shared Property ApplicationID As Guid
        Get
            Return HttpContext.Current.Session("ApplicationID")
        End Get
        Set(value As Guid)
            HttpContext.Current.Session("ApplicationID") = value
        End Set
    End Property

    Public Shared Property ShowReturnToUniqcoButton As Boolean
        Get
            Dim x As Object = HttpContext.Current.Session("ShowReturnToUniqcoButton")
            'if the session variable is nothing, then return false (as a default)
            Return If(x Is Nothing, False, x)

        End Get
        Set(value As Boolean)
            HttpContext.Current.Session("ShowReturnToUniqcoButton") = value
        End Set
    End Property

    Public Shared Property ReturnToUniqcoURL As String
        Get
            Dim x As Object = HttpContext.Current.Session("ReturnToUniqcoURL")
            'if the session variable is nothing, then return false (as a default)
            Return If(x Is Nothing, String.Empty, x)

        End Get
        Set(value As String)
            HttpContext.Current.Session("ReturnToUniqcoURL") = value
        End Set
    End Property

    Public Shared Property ImageType As String
        Get
            Return HttpContext.Current.Session("ImageType")
        End Get
        Set(value As String)
            HttpContext.Current.Session("ImageType") = value
        End Set
    End Property
    Public Shared Property ImageLocByByteArray As Byte()
        Get
            Return HttpContext.Current.Session("ImageLocByByteArray")
        End Get
        Set(value As Byte())
            HttpContext.Current.Session("ImageLocByByteArray") = value
        End Set
    End Property

    Public Shared Property header_logoBinary As Object
        Get
            Return HttpContext.Current.Session("header_logoBinary")
        End Get
        Set(value As Object)
            HttpContext.Current.Session("header_logoBinary") = value
        End Set
    End Property

    Public Shared Property header_companyName As String
        Get
            Return HttpContext.Current.Session("header_companyName")
        End Get
        Set(value As String)
            HttpContext.Current.Session("header_companyName") = value
        End Set
    End Property

    Public Shared Property header_applicationName As String
        Get
            Return HttpContext.Current.Session("header_applicationName")
        End Get
        Set(value As String)
            HttpContext.Current.Session("header_applicationName") = value
        End Set
    End Property

    Public Shared Property BusinessLocations As List(Of DataObjects.ApplicationLocation)
        Get
            Return HttpContext.Current.Session("BusinessLocations")
        End Get
        Set(value As List(Of DataObjects.ApplicationLocation))
            HttpContext.Current.Session("BusinessLocations") = value
        End Set
    End Property

    Public Shared Property ApplicationObject As FMS.Business.DataObjects.Application
        Get
            Return HttpContext.Current.Session("ApplicationObject")
        End Get
        Set(value As FMS.Business.DataObjects.Application)
            HttpContext.Current.Session("ApplicationObject") = value
        End Set
    End Property

    Public Shared Property User As FMS.Business.DataObjects.User
        Get
            Return HttpContext.Current.Session("User")
        End Get
        Set(value As FMS.Business.DataObjects.User)
            HttpContext.Current.Session("User") = value
        End Set
    End Property


    Public Shared Property UserID As Guid
        Get
            Return HttpContext.Current.Session("UserID")
        End Get
        Set(value As Guid)
            HttpContext.Current.Session("UserID") = value
        End Set
    End Property

    Public Shared Property CurrentExpandedRow As Guid
        Get
            Return HttpContext.Current.Session("CurrentExpandedRow")
        End Get
        Set(value As Guid)
            HttpContext.Current.Session("CurrentExpandedRow") = value
        End Set
    End Property


    Public Shared Property ProblemPageMessage As String
        Get
            Return HttpContext.Current.Session("ProblemPageMessage")
        End Get
        Set(value As String)
            HttpContext.Current.Session("ProblemPageMessage") = value
        End Set
    End Property


    Public Shared Property ApplicationName As String
        Get
            Return HttpContext.Current.Session("ApplicationName")
        End Get
        Set(value As String)
            HttpContext.Current.Session("ApplicationName") = value
        End Set
    End Property

    Public Shared Property ParameterValues As String
        Get
            Return HttpContext.Current.Session("ParameterValues")
        End Get
        Set(value As String)
            HttpContext.Current.Session("ParameterValues") = value
        End Set
    End Property

    Public Shared ReadOnly Property CachedVehicleReports As List(Of CachedVehicleReport)
        Get

            If HttpContext.Current.Session("CachedVehicleReports") Is Nothing Then _
                    HttpContext.Current.Session("CachedVehicleReports") = New List(Of CachedVehicleReport)

            Return CType(HttpContext.Current.Session("CachedVehicleReports"), List(Of CachedVehicleReport))

        End Get
    End Property
    Public Shared ReadOnly Property CachedVehicleDumpReports As List(Of CachedVehicleDumpReport)
        Get
            If HttpContext.Current.Session("CachedVehicleDumpReports") Is Nothing Then _
                    HttpContext.Current.Session("CachedVehicleDumpReports") = New List(Of CachedVehicleDumpReport)

            Return CType(HttpContext.Current.Session("CachedVehicleDumpReports"), List(Of CachedVehicleDumpReport))

        End Get
    End Property

    Public Shared Property SelectedReportName As String
        Get
            Dim x As Object = HttpContext.Current.Session("SelectedReportName")

            Return If(x Is Nothing, String.Empty, x)

        End Get
        Set(value As String)

            HttpContext.Current.Session("SelectedReportName") = value

        End Set
    End Property


    Public Shared ReadOnly Property CachedDriveroperatingHoursReports As List(Of CachedDriverOperatingHoursReport)
        Get

            If HttpContext.Current.Session("CachedDriverOperatingHoursReport") Is Nothing Then _
                    HttpContext.Current.Session("CachedDriverOperatingHoursReport") = New List(Of CachedDriverOperatingHoursReport)

            Return CType(HttpContext.Current.Session("CachedDriverOperatingHoursReport"), List(Of CachedDriverOperatingHoursReport))

        End Get
    End Property


#End Region


    Public Shared Function GetTimeZoneValues() As List(Of FMS.Business.DataObjects.Setting)

        Dim retlst As New List(Of FMS.Business.DataObjects.Setting)

        If ThisSession.ApplicationObject.TimeZone Is Nothing Then Return retlst

        Dim tz = ThisSession.ApplicationObject.TimeZone

        retlst.Add(New FMS.Business.DataObjects.Setting With {.Name = "Timezone ID", .Value = tz.ID})
        retlst.Add(New FMS.Business.DataObjects.Setting With {.Name = "Timezone Name", .Value = tz.Description})
        retlst.Add(New FMS.Business.DataObjects.Setting With {.Name = "Hours Offset", .Value = tz.Offset})
        retlst.Add(New FMS.Business.DataObjects.Setting With {.Name = "Daylight Savings Offset", .Value = tz.DST_Offset})
        retlst.Add(New FMS.Business.DataObjects.Setting With {.Name = "Actual Offset from GMT", .Value = tz.Offset + tz.DST_Offset})


        Return retlst


    End Function


End Class

Public Class CachedVehicleReport

    Public Property StartDate As DateTime
    Public Property EndDate As DateTime
    Public Property VehicleID As Guid
    Public Property LogoBinary() As Byte()


#Region "Summary Items"

    Public Property TotalStopDuration As TimeSpan
    Public Property AverageStopDuration As TimeSpan
    Public Property TotalIdleTime As TimeSpan
    Public Property NumberOfStops As Integer
    Public Property TotalDistanceTravelled As Decimal
    Public Property TotalTravelDuration As TimeSpan

    Public Property formatted_TotalStopDuration As String
    Public Property formatted_AverageStopDuration As String
    Public Property formatted_TotalDistanceTravelled As String
    Public Property formatted_TotalIdleTime As String
    Public Property formatted_NumberOfStops As String
    Public Property formatted_TotalTravelDuration As String


    Public Sub CalculateSummaries()


        For i As Integer = 0 To LineValies.Count - 1

            Dim thisLineValue As FMS.Business.ReportGeneration.VehicleActivityReportLine = LineValies(i)

            TotalStopDuration += thisLineValue.StopDuration

            TotalIdleTime += thisLineValue.IdleDuration
            NumberOfStops += 1

            TotalDistanceTravelled += thisLineValue.DistanceKMs

            If thisLineValue.StopDuration.TotalHours < 6 Then AverageStopDuration += thisLineValue.StopDuration

            If thisLineValue.ArrivalTime.HasValue AndAlso thisLineValue.StartTime.HasValue Then _
                TotalTravelDuration += (thisLineValue.ArrivalTime.Value - thisLineValue.StartTime.Value)
        Next

        If NumberOfStops > 0 Then
            AverageStopDuration = TimeSpan.FromSeconds(AverageStopDuration.TotalSeconds / NumberOfStops)
        Else
            AverageStopDuration = TimeSpan.FromSeconds(0)
        End If


        formatted_AverageStopDuration = timespanFormatCust(AverageStopDuration)
        formatted_NumberOfStops = NumberOfStops
        formatted_TotalDistanceTravelled = TotalDistanceTravelled.ToString("#.##")
        formatted_TotalIdleTime = timespanFormatCust(TotalIdleTime)
        formatted_TotalStopDuration = timespanFormatCust(TotalStopDuration)
        formatted_TotalTravelDuration = timespanFormatCust(TotalTravelDuration)


        'Try

        '    'Figure out the timezone for the vehicle
        '    'Dim offset As Double = FMS.Business.DataObjects.ApplicationVehicle.GetForID(VehicleID).GetCurrentTimeZoneOffsetFromPerth

        '    ''what was the offset applied for

        '    'For Each lv In LineValies

        '    '    If lv.ArrivalTime.HasValue Then lv.ArrivalTime = lv.ArrivalTime.Value.AddHours(offset)
        '    '    If lv.DepartureTime.HasValue Then lv.DepartureTime = lv.DepartureTime.Value.AddHours(offset)
        '    '    If lv.StartTime.HasValue Then lv.StartTime = lv.StartTime.Value.AddHours(offset)

        '    'Next

        'Catch ex As Exception

        'End Try


    End Sub
    Private Function timespanFormatCust(t As TimeSpan) As String

        'Dim hours As Integer = Math.Floor(t.TotalHours)
        'Dim minutes As Integer = Math.Floor(t.Minutes)
        'Dim seconds As Integer = Math.Floor(t.Seconds)

        'Dim formatStr As String = "{0:00}:{1:00}:{2:00}"

        'Return String.Format(formatStr, hours, minutes, seconds)

        Return t.ToString("dd\.hh\:mm\:ss")

    End Function

#End Region

    Public Property LineValies As List(Of FMS.Business.ReportGeneration.VehicleActivityReportLine)

   


    Public Sub New()

        TotalStopDuration = TimeSpan.FromSeconds(0)
        AverageStopDuration = TimeSpan.FromSeconds(0)
        TotalDistanceTravelled = 0
        TotalIdleTime = TimeSpan.FromSeconds(0)
        TotalTravelDuration = TimeSpan.FromSeconds(0)

    End Sub

End Class

'BY RYAN 
'only difference is LineValies
Public Class CachedDriverOperatingHoursReport
    'Inherits CachedVehicleReport

    Public Property StartDate As DateTime
    Public Property EndDate As DateTime
    Public Property VehicleID As Guid
    Public Property BusinessLocationID As Guid
    Public Property LogoBinary() As Byte()


#Region "Summary Items"

    Public Property TotalStopDuration As TimeSpan
    Public Property AverageStopDuration As TimeSpan
    Public Property TotalIdleTime As TimeSpan
    Public Property NumberOfStops As Integer
    Public Property TotalDistanceTravelled As Decimal
    Public Property TotalTravelDuration As TimeSpan

    Public Property formatted_TotalStopDuration As String
    Public Property formatted_AverageStopDuration As String
    Public Property formatted_TotalDistanceTravelled As String
    Public Property formatted_TotalIdleTime As String
    Public Property formatted_NumberOfStops As String
    Public Property formatted_TotalTravelDuration As String


    Public Sub CalculateSummaries()


        For i As Integer = 0 To Me.VehicleActivityReportLines.Count - 1

            Dim thisLineValue As FMS.Business.ReportGeneration.VehicleActivityReportLine = Me.VehicleActivityReportLines(i)

            TotalStopDuration += thisLineValue.StopDuration
            TotalIdleTime += thisLineValue.IdleDuration
            NumberOfStops += 1

            TotalDistanceTravelled += thisLineValue.DistanceKMs

            If thisLineValue.StopDuration.TotalHours < 6 Then AverageStopDuration += thisLineValue.StopDuration

            If thisLineValue.ArrivalTime.HasValue AndAlso thisLineValue.StartTime.HasValue Then _
                TotalTravelDuration += (thisLineValue.ArrivalTime.Value - thisLineValue.StartTime.Value)
        Next

        If NumberOfStops > 0 Then
            AverageStopDuration = TimeSpan.FromSeconds(AverageStopDuration.TotalSeconds / NumberOfStops)
        Else
            AverageStopDuration = TimeSpan.FromSeconds(0)
        End If


        formatted_AverageStopDuration = timespanFormatCust(AverageStopDuration)
        formatted_NumberOfStops = NumberOfStops
        formatted_TotalDistanceTravelled = TotalDistanceTravelled.ToString("#.##")
        formatted_TotalIdleTime = timespanFormatCust(TotalIdleTime)
        formatted_TotalStopDuration = timespanFormatCust(TotalStopDuration)
        formatted_TotalTravelDuration = timespanFormatCust(TotalTravelDuration)

    End Sub

    Private Function timespanFormatCust(t As TimeSpan) As String

        Dim hours As Integer = Math.Floor(t.TotalHours)
        Dim minutes As Integer = Math.Floor(t.Minutes)
        Dim seconds As Integer = Math.Floor(t.Seconds)

        Dim formatStr As String = "{0:00}:{1:00}:{2:00}"

        Return String.Format(formatStr, hours, minutes, seconds)

    End Function

#End Region

    Public Property LineValies As List(Of Business.ReportGeneration.DriverOperatingReportHoursLine)
    Public Property VehicleActivityReportLines As List(Of Business.ReportGeneration.VehicleActivityReportLine)

End Class

Public Class CachedDriverOperatingHoursReportLine

    Public Property HomeStart As DateTime?
    Public Property Arrival As DateTime?
    Public Property Departure As DateTime?
    Public Property HomeStart_End As DateTime?
    Public ReadOnly Property formatted_Departure As String
        Get
            Return If(Departure Is Nothing, "", Departure.Value.ToShortTimeString() + formatTimeSpan(Arrival, Departure))
        End Get
    End Property
    Public ReadOnly Property formatted_HomeStart_End As String
        Get
            Return If(HomeStart_End Is Nothing, "", HomeStart_End.Value.ToShortTimeString() + formatTimeSpan(Departure, HomeStart_End))
        End Get
    End Property
    Private Function formatTimeSpan(d1 As DateTime?, d2 As DateTime?) As String
        If d1 Is Nothing Then Return ""
        If d2 Is Nothing Then Return ""
        Dim min = d2.Value.Subtract(d1.Value).TotalMinutes
        If min < 60 Then
            Return " (" + min.ToString("##") + "mins)"
        Else
            Dim hr = d2.Value.Subtract(d1.Value).Hours
            Dim hrmin = min - (hr * 60)
            Return " (" + hr.ToString + ":" + hrmin.ToString("##") + "hrs)"
        End If

        Return ""
    End Function

End Class

'Get report content for vehicle dump details 
Public Class CachedVehicleDumpReport

    Public Property StartDate As DateTime
    Public Property EndDate As DateTime
    Public Property VehicleID As Guid
    Public Property LogoBinary() As Byte()


#Region "Summary Items"

    Public Property TotalStopDuration As TimeSpan
    Public Property AverageStopDuration As TimeSpan
    Public Property TotalIdleTime As TimeSpan
    Public Property NumberOfStops As Integer
    Public Property TotalDistanceTravelled As Decimal
    Public Property TotalTravelDuration As TimeSpan

    Public Property formatted_TotalStopDuration As String
    Public Property formatted_AverageStopDuration As String
    Public Property formatted_TotalDistanceTravelled As String
    Public Property formatted_TotalIdleTime As String
    Public Property formatted_NumberOfStops As String
    Public Property formatted_TotalTravelDuration As String


    Public Sub CalculateSummaries()


        For i As Integer = 0 To LineValies.Count - 1

            Dim thisLineValue As FMS.Business.ReportGeneration.VehicleDumpActivityReportLine = LineValies(i)

            TotalStopDuration += thisLineValue.StopDuration

            TotalIdleTime += thisLineValue.IdleDuration
            NumberOfStops += 1

            TotalDistanceTravelled += thisLineValue.DistanceKMs

            If thisLineValue.StopDuration.TotalHours < 6 Then AverageStopDuration += thisLineValue.StopDuration

            If thisLineValue.ArrivalTime.HasValue AndAlso thisLineValue.StartTime.HasValue Then _
                TotalTravelDuration += (thisLineValue.ArrivalTime.Value - thisLineValue.StartTime.Value)
        Next

        If NumberOfStops > 0 Then
            AverageStopDuration = TimeSpan.FromSeconds(AverageStopDuration.TotalSeconds / NumberOfStops)
        Else
            AverageStopDuration = TimeSpan.FromSeconds(0)
        End If


        formatted_AverageStopDuration = timespanFormatCust(AverageStopDuration)
        formatted_NumberOfStops = NumberOfStops
        formatted_TotalDistanceTravelled = TotalDistanceTravelled.ToString("#.##")
        formatted_TotalIdleTime = timespanFormatCust(TotalIdleTime)
        formatted_TotalStopDuration = timespanFormatCust(TotalStopDuration)
        formatted_TotalTravelDuration = timespanFormatCust(TotalTravelDuration)





    End Sub

    Private Function timespanFormatCust(t As TimeSpan) As String

        'Dim hours As Integer = Math.Floor(t.TotalHours)
        'Dim minutes As Integer = Math.Floor(t.Minutes)
        'Dim seconds As Integer = Math.Floor(t.Seconds)

        'Dim formatStr As String = "{0:00}:{1:00}:{2:00}"

        'Return String.Format(formatStr, hours, minutes, seconds)

        Return t.ToString("dd\.hh\:mm\:ss")

    End Function

#End Region

    Public Property LineValies As List(Of FMS.Business.ReportGeneration.VehicleDumpActivityReportLine)

    Public Sub New()

        TotalStopDuration = TimeSpan.FromSeconds(0)
        AverageStopDuration = TimeSpan.FromSeconds(0)
        TotalDistanceTravelled = 0
        TotalIdleTime = TimeSpan.FromSeconds(0)
        TotalTravelDuration = TimeSpan.FromSeconds(0)

    End Sub

End Class
