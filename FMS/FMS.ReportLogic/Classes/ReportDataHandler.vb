Imports FMS.Business
Imports FMS.Business.DataObjects
Imports System.Web
Imports DevExpress.XtraReports.UI

Public Class ReportDataHandler
    Public Shared Function GetApplicationGeoFence() As List(Of FMS.Business.DataObjects.ApplicationGeoFence)
        Return FMS.Business.DataObjects.ApplicationGeoFence.GetAllApplicationGeoFences(ThisSession.ApplicationID)
    End Function
    Public Shared Function GetThisApplicationsVehicleList() As List(Of FMS.Business.DataObjects.ApplicationVehicle)
        Return FMS.Business.DataObjects.ApplicationVehicle.GetAll(ThisSession.ApplicationID)
    End Function
    Public Shared Function GetThisApplicationLocationList() As List(Of FMS.Business.DataObjects.ApplicationLocation)
        Return FMS.Business.DataObjects.ApplicationLocation.GetAll(ThisSession.ApplicationID)
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

        Dim rept As CachedVehicleReport = Nothing

        Try

            startdate = startdate
            endDate = endDate.AddDays(1)

            If Not ThisSession.ApplicationID = Guid.Empty Then

                'get the vehicleid (guid)
                Dim vehicleID As Guid = _
                    FMS.Business.DataObjects.ApplicationVehicle.GetAll(ThisSession.ApplicationID) _
                            .Where(Function(x) x.Name.ToLower = vehicleName.ToLower).Single.ApplicationVehileID


                'Find out if the report is already in the cache
                rept = (From x In ThisSession.CachedVehicleReports _
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

            End If

        Catch ex As Exception
            Throw
        End Try


        Return rept

    End Function
    Public Shared Function GetVehicleReportValues(startdate As Date _
                                                    , endDate As Date _
                                                    , vehicleName As String _
                                                        , appID As String) As CachedVehicleReport
        Dim rept As CachedVehicleReport = Nothing

        Try
            startdate = startdate
            endDate = endDate.AddDays(1)

            'If Not ThisSession.ApplicationID = Guid.Empty Then
            ''get the vehicleid (guid)
            Dim vehicleID As Guid = _
                FMS.Business.DataObjects.ApplicationVehicle.GetAll(New Guid(appID)) _
                        .Where(Function(x) x.Name.ToLower = vehicleName.ToLower).Single.ApplicationVehileID


            'Find out if the report is already in the cache
            'rept = (From x In ThisSession.CachedVehicleReports _
            '                               Where x.EndDate = endDate _
            '                                AndAlso x.StartDate = startdate _
            '                               AndAlso x.VehicleID = vehicleID).SingleOrDefault
            'Dim GET_CAHCHED_REPORT As Boolean = True

            'MAKE the report and add it to the cache if it doesnt exist
            'If (rept Is Nothing) And (GET_CAHCHED_REPORT) Then

            Dim vehicleReportLines As List(Of FMS.Business.ReportGeneration.VehicleActivityReportLine) = _
                    FMS.Business.ReportGeneration.ReportGenerator.GetActivityReportLines_ForVehicle(startdate, endDate, vehicleID)

            rept = (New CachedVehicleReport With {.VehicleID = vehicleID _
                                                    , .StartDate = startdate _
                                                    , .EndDate = endDate _
                                                    , .LineValies = vehicleReportLines})

            rept.CalculateSummaries()

            rept.LogoBinary = FMS.Business.DataObjects.Application.GetCompanyLogo(New Guid(appID))

        Catch ex As Exception
            Throw
        End Try
        Return rept


        'startdate = startdate
        'endDate = endDate.AddDays(1)
        'Dim rept As New List(Of CachedVehicleReport)
        'Dim reptobj As New CachedVehicleReport
        'Dim vehicleReportLine As New List(Of FMS.Business.ReportGeneration.VehicleActivityReportLine)
        'If Not String.IsNullOrEmpty(vehicleName) Then
        '    Dim arrVehicle = vehicleName.Split(",")

        '    If arrVehicle.Length > 0 Then
        '        For Each vehName As String In arrVehicle

        '            'get the vehicleid (guid)
        '            Dim vehicleID As Guid = _
        '                FMS.Business.DataObjects.ApplicationVehicle.GetAll(New Guid(appID)) _
        '                        .Where(Function(x) x.Name.ToLower = vehName.ToLower).Single.ApplicationVehileID

        '            Dim GET_CAHCHED_REPORT As Boolean = True

        '            Dim vehicleReportLines As List(Of FMS.Business.ReportGeneration.VehicleActivityReportLine) = _
        '                    FMS.Business.ReportGeneration.ReportGenerator.GetActivityReportLines_ForVehicle(startdate, endDate, vehicleID)

        '            For Each Item In vehicleReportLines
        '                vehicleReportLine.Add(New FMS.Business.ReportGeneration.VehicleActivityReportLine With {.ArrivalTime = Item.ArrivalTime _
        '                                                        , .DepartureTime = Item.DepartureTime _
        '                                                        , .DistanceKMs = Item.DistanceKMs _
        '                                                        , .DriverName = Item.DriverName _
        '                                                        , .EngineOffDuration = Item.EngineOffDuration _
        '                                                        , .StopDuration = Item.StopDuration _
        '                                                        , .StopLocation = Item.StopLocation _
        '                                                        , .IdleDuration = Item.IdleDuration _
        '                                                        , .Lat = Item.Lat _
        '                                                        , .Lng = Item.Lng _
        '                                                        , .StartTime = Item.StartTime _
        '                                                       })
        '            Next


        '            'rept.Add(New CachedVehicleReport With {.VehicleID = vehicleID _
        '            '                                        , .StartDate = startdate _
        '            '                                        , .EndDate = endDate _
        '            '                                        , .LineValies = vehicleReportLines
        '            '                                      }) 
        '            'rept = (New CachedVehicleReport With {.VehicleID = vehicleID _
        '            '                                        , .StartDate = startdate _
        '            '                                        , .EndDate = endDate _
        '            '                                        , .LineValies = vehicleReportLines}) 

        '        Next
        '        reptobj = (New CachedVehicleReport With {.StartDate = startdate _
        '                                            , .EndDate = endDate _
        '                                             , .LineValies = vehicleReportLine})


        '        reptobj.CalculateSummaries()
        '    End If
        'End If

        'reptobj.LogoBinary = FMS.Business.DataObjects.Application.GetCompanyLogo(New Guid(appID))

        'Return reptobj
    End Function
    'BY RYAN FUNCTION USED TO CALL SERVICE VEHICLE REPORT 
    Public Shared Function GetDriverOperatingReportValues(startdate As Date _
                                                  , endDate As Date _
                                                  , vehicleName As String, businessLocation As String) As CachedDriverOperatingHoursReport


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
                    FMS.Business.ReportGeneration.ReportGenerator.GetDriverOperatingHours_ForVehicle(startdate, endDate, vehicleID, New Guid(businessLocation))


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


        retobj.LogoBinary = FMS.Business.DataObjects.Application.GetCompanyLogo(appID)


        'TimeZoneHelper.AltertoHQTimeZone(retobj) 'should no longer be required

        Return retobj

    End Function
    ' functions for emailing based on schedule 
    Public Shared Function GetDriverOperatingReportValue(startdate As Date _
                                                , endDate As Date _
                                                , vehicleName As String, appID As String, businessLocation As String) As CachedDriverOperatingHoursReport

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
                    FMS.Business.ReportGeneration.ReportGenerator.GetDriverOperatingHours_ForVehicle(startdate, endDate, vehicleID, New Guid(businessLocation))


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

            Dim objApplication As New FMS.Business.DataObjects.Application

            retobj.LogoBinary = FMS.Business.DataObjects.Application.GetCompanyLogo(New Guid(appID))


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
        rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary

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

        rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary

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
                                  .RoleID = item.RoleID})
        Next
        rept.LineValies = objList

        rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary
        Return rept
    End Function
    '' Report to get the drivers assigned drivers to vehicles
    Public Shared Function GetAssignedReport() As CacheAssignVehicletoDriver

        Dim rept As New CacheAssignVehicletoDriver
        'Dim retobj = FMS.Business.DataObjects.User.GetAllUsersForApplication(ThisSession.ApplicationID).ToList()
        'Dim objList As New List(Of Users)
        'For Each item In retobj
        '    objList.Add(New Users() With
        '                         {.UserName = item.UserName,
        '                          .Email = item.Email,
        '                          .Mobile = item.Mobile,
        '                          .TimeZone = item.TimeZone,
        '                          .RoleID = item.RoleID})
        'Next
        'rept.LineValies = objList

        rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary
        Return rept
    End Function
    '' to get list of Contacts
    Public Shared Function GetConactListReport() As CacheContact
        Dim rept As New CacheContact
        Dim retobj = FMS.Business.DataObjects.Contact.GetAllForApplication(ThisSession.ApplicationID).ToList()
        Dim objList As New List(Of Contact)
        For Each item In retobj
            objList.Add(New Contact() With
                                 {.Forname = item.Forname,
                                  .Surname = item.Surname,
                                  .EmailAddress = item.EmailAddress,
                                  .MobileNumber = item.MobileNumber,
                                  .CompanyName = item.CompanyName})
        Next
        rept.LineValies = objList

        rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary
        Return rept
    End Function
    Public Shared Function GetRolesListReport() As CacheRoles
        Dim rept As New CacheRoles
        Dim retobj = FMS.Business.DataObjects.Role.GetAllRolesforApplication(ThisSession.ApplicationID).ToList()
        Dim objList As New List(Of Roles)
        For Each item In retobj
            objList.Add(New Roles() With
                                 {.Name = item.Name,
                                  .Description = item.Description
                                  })
        Next
        rept.LineValies = objList

        rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary
        Return rept
    End Function
    Public Shared Function GetFeaturesAccessListReport() As CacheRole
        Dim rept As New CacheRole
        Dim retobj = FMS.Business.DataObjects.ApplicationFeatureRole.GetAllApplicationFeatureRole(ThisSession.ApplicationID)
        Dim objList As New List(Of Role)
        For Each item In retobj
            objList.Add(New Role() With
                                 {.FeatureName = item.FetaureName,
                                  .RoleName = item.RoleName
                                  })
        Next
        rept.LineValies = objList

        rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary
        Return rept
    End Function
    'To get the report for business location 
    Public Shared Function GetBusinessLocationListReport() As CacheBusinessLocation
        Dim rept As New CacheBusinessLocation
        Dim retobj = FMS.Business.DataObjects.ApplicationLocation.GetAllIncludingDefault(ThisSession.ApplicationID)
        Dim objList As New List(Of BusinessLocation)

        For Each item In retobj
            objList.Add(New BusinessLocation() With
                                 {.Name = item.Name,
                                  .Address = item.Address,
                                  .Longitude = item.Longitude,
                                  .Lattitude = item.Lattitude,
                                  .ApplicationImage = item.ApplicationImage
                                  })
        Next
        rept.LineValies = objList

        rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary
        Return rept
    End Function
    Public Shared Function GetVehicletoDriversListReport() As CacheAssignVehicletoDriver
        Dim rept As New CacheAssignVehicletoDriver
        Dim retobj = FMS.Business.DataObjects.ApplicationVehicleDriverTime.GetAllForApplicationAndDatePeriodIncludingDuds(ThisSession.ApplicationID)
        Dim objList As New List(Of AssignVehicletoDriver)

        For Each item In retobj
            objList.Add(New AssignVehicletoDriver() With
                                 {.VehicleName = item.VehicleName,
                                  .StartDate = item.StartDate,
                                  .EndDate = item.EndDate,
                                  .DriverName = item.DriverName
                                 })
        Next

        rept.LineValies = objList

        rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary
        Return rept
    End Function
    '  CacheAssignVehicletoDriver
    'to get the data for vehicle bump Report
    Public Shared Function GetVehicleDumpReportValue(startdate As Date _
                                                 , endDate As Date _
                                                 , vehicleName As String) As CachedVehicleDumpReport

        Dim rept As New CachedVehicleDumpReport()
        Try
            startdate = startdate
            endDate = endDate.AddDays(1)


            If Not ThisSession.ApplicationID = Guid.Empty Then
                'get the vehicleid (guid)
                Dim vehicleID As Guid = _
                    FMS.Business.DataObjects.ApplicationVehicle.GetAll(ThisSession.ApplicationID) _
                            .Where(Function(x) x.Name.ToLower = vehicleName.ToLower).Single.ApplicationVehileID



                '' Find out if the report is already in the cache
                'rept = (From x In ThisSession.CachedVehicleDumpReports _
                '                               Where x.EndDate = endDate _
                '                                AndAlso x.StartDate = startdate _
                '                               AndAlso x.VehicleID = vehicleID).SingleOrDefault

                'Dim GET_CAHCHED_REPORT As Boolean = True

                ''MAKE the report and add it to the cache if it doesnt exist
                'If (rept Is Nothing) And (GET_CAHCHED_REPORT) Then

                '    Dim vehicleReportLines As List(Of FMS.Business.ReportGeneration.VehicleDumpActivityReportLine) = _
                '            FMS.Business.ReportGeneration.ReportGenerator.GetActivityReportLines_ForVehicleDump(startdate, endDate, vehicleID)

                '    rept = (New CachedVehicleDumpReport With {.VehicleID = vehicleID _
                '                                            , .StartDate = startdate _
                '                                            , .EndDate = endDate _
                '                                            , .LineValies = vehicleReportLines})

                '    rept.CalculateSummaries()

                '    ThisSession.CachedVehicleDumpReports.Add(rept)
                'End If

                Dim vehicleReportLines As New List(Of FMS.Business.ReportGeneration.VehicleDumpActivityReportLine)

                vehicleReportLines.Add(New FMS.Business.ReportGeneration.VehicleDumpActivityReportLine() With
                               {.StartTime = DateTime.Now.timezoneToClient,
                                .EndTime = DateTime.Now.AddHours(12).timezoneToClient,
                                .NoofLoads = 15,
                                .AvgLoadToDump = DateTime.Now.timezoneToClient.TimeOfDay,
                                .AvgDumpToLoad = DateTime.Now.timezoneToClient.TimeOfDay,
                                .AvgLoadTime = DateTime.Now.timezoneToClient.TimeOfDay,
                                .AvgDumpTime = DateTime.Now.timezoneToClient.TimeOfDay
                               })

                rept.LineValies = vehicleReportLines

                rept.LogoBinary = ThisSession.ApplicationObject.GetLogoBinary
            End If

        Catch ex As Exception
            Throw
        End Try
        Return rept
    End Function

    Public Shared Function GetIndustryListReport() As CacheIndustryList
        Dim paramValues() As String = FMS.Business.ThisSession.ParameterValues.Split(":")
        Dim rept As New CacheIndustryList
        Dim retobj = FMS.Business.DataObjects.usp_GetIndustryListReport.GetIndustryListReportByIndustryID(paramValues(0)).ToList()
        Dim objList As New List(Of IndustryList)
        For Each item In retobj
            objList.Add(New IndustryList() With
                                 {.Aid = item.Aid, .CustomerName = item.CustomerName,
                                  .Frequency = item.Frequency, .IndustryDescription = item.IndustryDescription,
                                  .InvoiceCommencing = item.InvoiceCommencing, .MYOBCustomerNumber = item.MYOBCustomerNumber,
                                  .PerAnnumCharge = item.PerAnnumCharge, .PostCode = item.PostCode,
                                  .ServiceDescription = item.ServiceDescription, .ServicePrice = item.ServicePrice,
                                  .ServiceUnits = item.ServiceUnits, .SiteCeaseDate = item.SiteCeaseDate,
                                  .SiteName = item.SiteName, .SiteName1 = item.SiteName1, .UnitsHaveMoreThanOneRun = item.UnitsHaveMoreThanOneRun})
        Next
        rept.LineValies = objList
        rept.Param = paramValues(1)

        Return rept
    End Function
    Public Shared Function GetServiceListReport() As CacheServiceList
        Dim rept As New CacheServiceList
        Dim retobj = FMS.Business.DataObjects.tblServices.GetAllWithNull().ToList()
        Dim objList As New List(Of ServiceList)
        For Each item In retobj
            objList.Add(New ServiceList() With {
                        .CostOfService = item.CostOfService, .ServiceCode = item.ServiceCode, .ServiceDescription = item.ServiceDescription,
                        .ServicesID = item.ServicesID, .Sid = item.Sid})
        Next
        rept.LineValues = objList
        rept.Param = ""
        Return rept
    End Function
    Public Shared Function GetDriversLicenseExpiryReport() As CacheDriversLicenseExpiry
        Dim rept As New CacheDriversLicenseExpiry
        Dim retobj = FMS.Business.DataObjects.usp_GetDriversLicenseExpiryReport.GetDriversLicenseExpiryReport().ToList()
        Dim objList As New List(Of DriversLicenseExpiry)
        For Each item In retobj
            objList.Add(New DriversLicenseExpiry() With {
                        .Did = item.Did, .DriverID = item.DriverID, .DriverName = item.DriverName,
                        .DriversLicenseExpiryDate = item.DriversLicenseExpiryDate, .DriversLicenseNo = item.DriversLicenseNo,
                        .Inactive = item.Inactive, .Renewal = item.Renewal})
        Next
        rept.LineValues = objList
        rept.Param = ""
        Return rept
    End Function
    Public Sub New()

    End Sub
End Class
