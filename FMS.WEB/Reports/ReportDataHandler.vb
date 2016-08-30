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
