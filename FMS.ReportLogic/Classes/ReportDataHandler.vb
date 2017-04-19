Imports FMS.Business
Imports FMS.Business.DataObjects
Imports System.Web
Imports DevExpress.XtraReports.UI


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
    Public Shared Function GetVehicleReportValue(startdate As Date _
                                                  , endDate As Date _
                                                  , vehicleName As String) As CachedVehicleReport

        startdate = startdate
        endDate = endDate.AddDays(1)


        'get the vehicleid (guid)
        Dim vehicleID As Guid = _
            FMS.Business.DataObjects.ApplicationVehicle.GetAll(ThisSession.ApplicationID) _
                    .Where(Function(x) x.Name.ToLower = vehicleName.ToLower).Single.ApplicationVehileID


        'Find out if the report is already in the cache
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

            rept.CalculateSummaries()

            ThisSession.CachedVehicleReports.Add(rept)


        End If

        rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary

        Return rept

    End Function
    Public Shared Function GetVehicleReportValues(startdate As Date _
                                                    , endDate As Date _
                                                    , vehicleName As String, appID As String) As CachedVehicleReport

        startdate = startdate
        endDate = endDate.AddDays(1)


        'Dim appID As Guid
        'appID = New Guid("176225F3-3AC6-404C-B191-0B4F69CC651A")

        'get the vehicleid (guid)
        Dim vehicleID As Guid = _
            FMS.Business.DataObjects.ApplicationVehicle.GetAll(New Guid(appID)) _
                    .Where(Function(x) x.Name.ToLower = vehicleName.ToLower).Single.ApplicationVehileID


        'Find out if the report is already in the cache
        'Dim rept As CachedVehicleReport = (From x In ThisSession.CachedVehicleReports _
        '                                    Where x.EndDate = endDate _
        '                                    AndAlso x.StartDate = startdate _
        '                                  AndAlso x.VehicleID = vehicleID).SingleOrDefault
        Dim rept As CachedVehicleReport
        Dim GET_CAHCHED_REPORT As Boolean = True

        'MAKE the report and add it to the cache if it doesnt exist
        If (rept Is Nothing) And (GET_CAHCHED_REPORT) Then

            Dim vehicleReportLines As List(Of FMS.Business.ReportGeneration.VehicleActivityReportLine) = _
                    FMS.Business.ReportGeneration.ReportGenerator.GetActivityReportLines_ForVehicle(startdate, endDate, vehicleID)

            rept = (New CachedVehicleReport With {.VehicleID = vehicleID _
                                                    , .StartDate = startdate _
                                                    , .EndDate = endDate _
                                                    , .LineValies = vehicleReportLines})

            rept.CalculateSummaries() 
            'ThisSession.CachedVehicleReports.Add(rept)

        End If

        'rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary 
        Return rept 
    End Function
      
    'BY RYAN FUNCTION USED TO CALL SERVICE VEHICLE REPORT 
    Public Shared Function GetDriverOperatingReportValues(startdate As Date _
                                                  , endDate As Date _
                                                  , vehicleName As String) As CachedDriverOperatingHoursReport


        startdate = startdate
        endDate = endDate.AddDays(1)

        'get the vehicleid (guid)
        Dim vehicleID As Guid = _
            FMS.Business.DataObjects.ApplicationVehicle.GetAll(ThisSession.ApplicationID) _
                    .Where(Function(x) x.Name.ToLower = vehicleName.ToLower).Single.ApplicationVehileID


        'Find out if the report is alreaedy in the cache 
        Dim rept As CachedDriverOperatingHoursReport = (From x In ThisSession.CachedDriveroperatingHoursReports _
                                                            Where x.EndDate = endDate _
                                                            AndAlso x.StartDate = startdate _
                                                            AndAlso x.VehicleID = vehicleID).SingleOrDefault

        Dim GET_CAHCHED_REPORT As Boolean = True

        'MAKE the report and add it to the cache if it doesnt exist
        If (rept Is Nothing) And (GET_CAHCHED_REPORT) Then

            Dim driverOperatingReport As FMS.Business.ReportGeneration.DriverOperatingReportHoursLine.DriverOperatingReportHours = _
                    FMS.Business.ReportGeneration.ReportGenerator.GetDriverOperatingHours_ForVehicle(startdate, endDate, vehicleID)


            rept = (New CachedDriverOperatingHoursReport With {.VehicleID = vehicleID _
                                                    , .StartDate = startdate _
                                                    , .EndDate = endDate _
                                                    , .LineValies = driverOperatingReport.LineValues _
                                                    , .VehicleActivityReportLines = driverOperatingReport.VehicleActivityReportLines
                                                    })

            'compute the summaries
            rept.CalculateSummaries()

            'put in session
            ThisSession.CachedDriveroperatingHoursReports.Add(rept)

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
    Public Shared Function GetGeoCacheReportByDrivers(startDate As Date, endDate As Date, driverID As String, appID As Guid) As ClientSide_GeoFenceReport_ByDriver

        Dim retobj As New ClientSide_GeoFenceReport_ByDriver

        startDate = startDate.timezoneToClient
        endDate = endDate.timezoneToClient.AddDays(1)

        retobj.ReportLines = FMS.Business.ReportGeneration.GeoFenceReport_Simple. _
                                                    GetReport(appID, startDate, endDate)

        If FMS.Business.DataObjects.ApplicationDriver.DriverREpresentingEveryone.ApplicationDriverID <> New Guid(driverID) Then

            retobj.ReportLines = retobj.ReportLines.Where(Function(c) c.ApplicationDriverID.HasValue _
                                                                      AndAlso c.ApplicationDriverID.Value = New Guid(driverID)).ToList
        End If

        retobj.CalculateSummaryValues(startDate, endDate, driverID)

        'retobj.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary

        'TimeZoneHelper.AltertoHQTimeZone(retobj) 'should no longer be required
         
        Return retobj

    End Function

    ' functions for emailing based on schedule 
    Public Shared Function GetDriverOperatingReportValue(startdate As Date _
                                                , endDate As Date _
                                                , vehicleName As String, appID As String) As CachedDriverOperatingHoursReport

        startdate = startdate
        endDate = endDate.AddDays(1)

        'get the vehicleid (guid)
        Dim vehicleID As Guid = _
            FMS.Business.DataObjects.ApplicationVehicle.GetAll(New Guid(appID)) _
                    .Where(Function(x) x.Name.ToLower = vehicleName.ToLower).Single.ApplicationVehileID


        'Find out if the report is alreaedy in the cache
        Dim rept As CachedDriverOperatingHoursReport
        'Dim rept As CachedDriverOperatingHoursReport = (From x In ThisSession.CachedDriveroperatingHoursReports _
        '                                                    Where x.EndDate = endDate _
        '                                                    AndAlso x.StartDate = startdate _
        '                                                    AndAlso x.VehicleID = vehicleID).SingleOrDefault

        Dim GET_CAHCHED_REPORT As Boolean = True

        'MAKE the report and add it to the cache if it doesnt exist
        If (rept Is Nothing) And (GET_CAHCHED_REPORT) Then

            Dim driverOperatingReport As FMS.Business.ReportGeneration.DriverOperatingReportHoursLine.DriverOperatingReportHours = _
                    FMS.Business.ReportGeneration.ReportGenerator.GetDriverOperatingHours_ForVehicle(startdate, endDate, vehicleID)


            rept = (New CachedDriverOperatingHoursReport With {.VehicleID = vehicleID _
                                                    , .StartDate = startdate _
                                                    , .EndDate = endDate _
                                                    , .LineValies = driverOperatingReport.LineValues _
                                                    , .VehicleActivityReportLines = driverOperatingReport.VehicleActivityReportLines
                                                    })

            'compute the summaries
            rept.CalculateSummaries()

            'put in session
            'ThisSession.CachedDriveroperatingHoursReports.Add(rept)

        End If

        'rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary

        Return rept

    End Function
    Public Shared Function GetGeoCacheReportByDriverforEmail(startDate As Date, endDate As Date, driverID As String, appID As String) As ClientSide_GeoFenceReport_ByDriver

        Dim retobj As New ClientSide_GeoFenceReport_ByDriver

        startDate = startDate.timezoneToClient
        endDate = endDate.timezoneToClient.AddDays(1)
         
        Dim driveID As String = FMS.Business.DataObjects.ApplicationDriver.GetDriverID(driverID)
         
         
        If Not String.IsNullOrEmpty(driveID) Then
            retobj.ReportLines = FMS.Business.ReportGeneration.GeoFenceReport_Simple. _
                                            GetReport(New Guid(appID), startDate, endDate)

            If FMS.Business.DataObjects.ApplicationDriver.DriverREpresentingEveryone.ApplicationDriverID <> New Guid(driveID) Then

                retobj.ReportLines = retobj.ReportLines.Where(Function(c) c.ApplicationDriverID.HasValue _
                                                                          AndAlso c.ApplicationDriverID.Value = New Guid(driveID)).ToList
            End If


            retobj.CalculateSummaryValues(startDate, endDate, driveID)

            'retobj.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary

            'TimeZoneHelper.AltertoHQTimeZone(retobj) 'should no longer be required
        End If


        Return retobj

    End Function
    ''Get List  

    Public Shared Function GetVehicleReport() As CacheVehicle 
        Dim rept As New CacheVehicle
        Dim retobj = FMS.Business.DataObjects.ApplicationVehicle.GetAll(ThisSession.ApplicationID).ToList()

        Dim objList As New List(Of Vehicle)
        For Each item In retobj
            objList.Add(New Vehicle() With
                                 {.Name = item.Name,
                                  .VINNumber = item.VINNumber, .Registration = item.Registration, .Notes = item.Notes, .DeviceID = item.DeviceID, .ApplicationImageID = item.ApplicationImageID})
        Next
        rept.LineValies = objList 

        Return rept
    End Function

    Public Shared Function GetFDriverListReport() As CacheDriver
        Dim rept As New CacheDriver
        Dim retobj = FMS.Business.DataObjects.ApplicationDriver.GetAllDrivers(ThisSession.ApplicationID).ToList() 
        Dim objList As New List(Of Driver)
        For Each item In retobj
            objList.Add(New Driver() With
                                 {.FirstName = item.FirstName,
                                  .Surname = item.Surname, .PhoneNumber = item.PhoneNumber, .EmailAddress = item.EmailAddress, .PhotoBinary = item.PhotoBinary, .Notes = item.Notes})
        Next
        rept.LineValies = objList

        Return rept
    End Function
    Public Shared Function GetUsersListReport() As CacheUsers
        Dim rept As New CacheUsers
        Dim retobj = FMS.Business.DataObjects.User.GetAllUsersForApplication(ThisSession.ApplicationID).ToList()
        Dim objList As New List(Of Users)
        For Each item In retobj
            objList.Add(New Users() With
                                 {.UserName = item.UserName,
                                  .Email = item.Email,
                                  .Mobile = item.Mobile,
                                  .TimeZone = item.TimeZone,
                                  .RoleID = item.RoleID
                                  })
        Next
        rept.LineValies = objList

        Return rept
    End Function

    Public Sub New()

    End Sub
End Class
