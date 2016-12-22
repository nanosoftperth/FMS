Imports FMS.Business

Public Class ReportDataHandler


    Public Shared Function GetThisApplicationsVehicleList() As List(Of FMS.Business.DataObjects.ApplicationVehicle)
        Return FMS.Business.DataObjects.ApplicationVehicle.GetAll(ThisSession.ApplicationID)
    End Function


    Public Shared Function GetThisApplicationsDriverList() As List(Of FMS.Business.DataObjects.ApplicationDriver)
        Return FMS.Business.DataObjects.ApplicationDriver.GetAllDriversIncludingEveryone(ThisSession.ApplicationID)
    End Function

    ''' <summary>
    ''' IF this report has been cached in RAM , then return the cached report.
    ''' If not, then populate the session cache with the report and return that
    ''' </summary>
    Public Shared Function GetVehicleReportValues(startdate As Date _
                                                  , endDate As Date _
                                                  , vehicleName As String) As CachedVehicleReport


        startdate = startdate
        endDate = endDate.AddDays(1)


        'get the vehicleid (guid)
        Dim vehicleID As Guid = _
            FMS.Business.DataObjects.ApplicationVehicle.GetAll(ThisSession.ApplicationID) _
                    .Where(Function(x) x.Name.ToLower = vehicleName.ToLower).Single.ApplicationVehileID


        'Find out if the report is alreaedy in the cache
        Dim rept As CachedVehicleReport = (From x In ThisSession.CachedVehicleReports _
                                            Where x.EndDate = endDate _
                                            AndAlso x.StartDate = startdate _
                                            AndAlso x.VehicleID = vehicleID).SingleOrDefault

        Dim GET_CAHCHED_REPORT As Boolean = True

        'MAKE the report and add it to the cache if it doesnt exist
        If (rept Is Nothing) And (GET_CAHCHED_REPORT) Then

            Dim vehicleReportLines As List(Of FMS.Business.ReportGeneration.VehicleActivityReportLine) = _
                    FMS.Business.ReportGeneration.ReportGenerator.GetActivityReportLines_ForVehicle(startdate, endDate, vehicleID)

            rept = (New CachedVehicleReport With {.VehicleID = vehicleID _
                                                    , .StartDate = startdate _
                                                    , .EndDate = endDate _
                                                    , .LineValies = vehicleReportLines})

            'TimeZoneHelper.AltertoHQTimeZone(rept) 'should n olonger be required, don eat business layer

            rept.CalculateSummaries()

            ThisSession.CachedVehicleReports.Add(rept)

        End If

        rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary

        Return rept

    End Function

    'BY RYAN FUNCTION USED TO CALL SERVICE VEHICLE REPORT
    Public Shared Function GetServiceVehicleReportValues(startdate As Date _
                                                  , endDate As Date _
                                                  , vehicleName As String) As CachedServiceVehicleReport


        startdate = startdate
        endDate = endDate.AddDays(1)


        'get the vehicleid (guid)
        Dim vehicleID As Guid = _
            FMS.Business.DataObjects.ApplicationVehicle.GetAll(ThisSession.ApplicationID) _
                    .Where(Function(x) x.Name.ToLower = vehicleName.ToLower).Single.ApplicationVehileID


        'Find out if the report is alreaedy in the cache
        Dim rept As CachedServiceVehicleReport = (From x In ThisSession.CachedServiceVehicleReports _
                                            Where x.EndDate = endDate _
                                            AndAlso x.StartDate = startdate _
                                            AndAlso x.VehicleID = vehicleID).SingleOrDefault

        Dim GET_CAHCHED_REPORT As Boolean = True

        'MAKE the report and add it to the cache if it doesnt exist
        If (rept Is Nothing) And (GET_CAHCHED_REPORT) Then

            Dim vehicleReportLines As List(Of FMS.Business.ReportGeneration.VehicleActivityReportLine) = _
                    FMS.Business.ReportGeneration.ReportGenerator.GetActivityReportLines_ForVehicle(startdate, endDate, vehicleID)

            Dim c = New List(Of CachedServiceVehicleReportLine)

            'from vehicleReportLines, get all distinct date
            Dim ddate = (From x In vehicleReportLines Select If(x.StartTime Is Nothing, "", x.StartTime.Value.ToShortDateString())).Distinct.ToList

            'get lat long of application
            Dim apsettings = Business.DataObjects.Setting.GetSettingsForApplication_withoutImages(ThisSession.ApplicationID)
            Dim aplat = apsettings.SingleOrDefault(Function(x) x.Name = "Business_Lattitude").Value
            Dim aplog = apsettings.SingleOrDefault(Function(x) x.Name = "Business_Longitude").Value
            Dim aploc = New Business.BackgroundCalculations.Loc(aplat, aplog)
            For Each dateloop In ddate
                ' group all activities in vehicleReportLines per date
                Dim vrl = (From x In vehicleReportLines Where If(x.StartTime Is Nothing, "", x.StartTime.Value.ToShortDateString()) = dateloop Order By x.StartTime Select x).ToList
                Dim i = New CachedServiceVehicleReportLine()

                'in activity list for each date, get the first to enter the application with a 200m radius  
                Dim vrl_floc = (From x In vrl Where Business.BackgroundCalculations.GeoFenceCalcs.isPointInCircle(aploc, 200, New Business.BackgroundCalculations.Loc(x.Lat, x.Lng)) = True Select x).ToList.FirstOrDefault
                If vrl_floc IsNot Nothing Then
                    i.Arrival = vrl_floc.ArrivalTime 'get the arrival 
                End If
                'in activity list for each date, get the lastentry to enter the application with a 200m radius  
                Dim vrl_lloc = (From x In vrl Where Business.BackgroundCalculations.GeoFenceCalcs.isPointInCircle(aploc, 200, New Business.BackgroundCalculations.Loc(x.Lat, x.Lng)) = True Select x).ToList.LastOrDefault
                'get the index oof last and add 1 to have the first 'out' in application
                If vrl_lloc IsNot Nothing Then
                    Dim cl = vrl(vrl.IndexOf(vrl_lloc) + 1)
                    i.Departure = cl.StartTime 'Get departuere
                End If
                i.HomeStart = vrl.First.StartTime 'get start time from the first activity of the day
                i.HomeStart_End = vrl.Last.ArrivalTime ' get start/end time from the last activity of the day
                c.Add(i)
            Next
            'create the report source
            rept = (New CachedServiceVehicleReport With {.VehicleID = vehicleID _
                                                    , .StartDate = startdate _
                                                    , .EndDate = endDate _
                                                    , .LineValies = vehicleReportLines _
                                                    , .ServiceLineValies = c})

            'TimeZoneHelper.AltertoHQTimeZone(rept) 'should n olonger be required, don eat business layer

            'compute the summaries
            rept.CalculateSummaries()

            'put in session
            ThisSession.CachedVehicleReports.Add(rept)

        End If

        rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary

        Return rept

    End Function

    Public Shared Function GetGeoCacheReportByDriver(startDate As Date, endDate As Date, driverID As String) As ClientSide_GeoFenceReport_ByDriver

        Dim retobj As New ClientSide_GeoFenceReport_ByDriver

        startDate = startDate.timezoneToClient
        endDate = endDate.timezoneToClient.AddDays(1)

        retobj.ReportLines = FMS.Business.ReportGeneration.GeoFenceReport_Simple. _
                                                    GetReport(ThisSession.ApplicationID, startDate, endDate)

        If FMS.Business.DataObjects.ApplicationDriver.DriverREpresentingEveryone.ApplicationDriverID <> New Guid(driverID) Then

            retobj.ReportLines = retobj.ReportLines.Where(Function(c) c.ApplicationDriverID.HasValue _
                                                                      AndAlso c.ApplicationDriverID.Value = New Guid(driverID)).ToList
        End If


        retobj.CalculateSummaryValues(startDate, endDate, driverID)

        retobj.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary

        'TimeZoneHelper.AltertoHQTimeZone(retobj) 'should no longer be required

        Return retobj

    End Function

    Public Sub New()

    End Sub

End Class
