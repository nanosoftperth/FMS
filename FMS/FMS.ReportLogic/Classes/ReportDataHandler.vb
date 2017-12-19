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
    Public Shared Function GetIndustryList() As List(Of FMS.Business.DataObjects.tblIndustryGroups)
        Return FMS.Business.DataObjects.tblIndustryGroups.GetAll()
    End Function
    Public Shared Function GetZoneList() As List(Of FMS.Business.DataObjects.tbZone)
        Return FMS.Business.DataObjects.tbZone.GetAll()
    End Function
    Public Shared Function GetRunList() As List(Of FMS.Business.DataObjects.tblRuns)
        Return FMS.Business.DataObjects.tblRuns.GetTblRuns()
    End Function
    Public Shared Function GetCustomerList() As List(Of FMS.Business.DataObjects.tblCustomers)
        Return FMS.Business.DataObjects.tblCustomers.GetAllOrderByCustomerName()
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

    Public Shared Function GetIndustryListReport(IndustryID As Int32) As CacheIndustryList
        Dim rept As New CacheIndustryList
        Dim retobj = FMS.Business.DataObjects.usp_GetIndustryListReport.GetIndustryListReportByIndustryID(IndustryID).ToList()
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
        rept.Param = IIf(objList.Count > 0, retobj.FirstOrDefault().IndustryDescription, "")

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
    Public Shared Function GetContractRenewalReport(startDate As Date, endDate As Date, zoneDescription As String) As CacheContractRenewal
        Dim rept As New CacheContractRenewal

        Dim retobj As List(Of FMS.Business.DataObjects.usp_GetContractRenewalsReport)
        If Not ZoneDescription.Equals(0) Then
            retobj = FMS.Business.DataObjects.usp_GetContractRenewalsReport.GetContractRenewalsReport(startDate, endDate, zoneDescription).ToList()
        Else
            retobj = FMS.Business.DataObjects.usp_GetContractRenewalsReport.GetContractRenewalsReport(startDate, endDate).ToList()
        End If

        Dim objList As New List(Of ContractRenewal)
        For Each item In retobj
            objList.Add(New ContractRenewal() With {
                        .Customer = item.Customer, .AreaDescription = item.AreaDescription, .SiteContractExpiry = item.SiteContractExpiry,
                        .CustomerName = item.CustomerName, .SiteName = item.SiteName, .SiteStartDate = item.SiteStartDate,
                        .ContractPeriodDesc = item.ContractPeriodDesc, .SiteContactPhone = item.SiteContactPhone,
                        .CustomerContactName = item.CustomerContactName, .CustomerPhone = item.CustomerPhone,
                        .ServiceUnits = item.ServiceUnits, .PerAnnumCharge = item.PerAnnumCharge})
        Next
        rept.LineValues = objList
        rept.Param = startDate.ToShortDateString()
        rept.Param2 = endDate.ToShortDateString()
        rept.Param3 = DateTime.Now.ToShortDateString()
        Return rept
    End Function
    Public Shared Function GetServiceSummaryReport() As CacheServiceSummary
        Dim rept As New CacheServiceSummary
        Dim retobj = FMS.Business.DataObjects.usp_GetServiceSummaryReport.GetServiceSummay().ToList()
        Dim objList As New List(Of ServiceSummary)
        For Each item In retobj
            objList.Add(New ServiceSummary() With {
                        .FrequencyDescription = item.FrequencyDescription, .ServiceCode = item.ServiceCode,
                        .ServiceDescription = item.ServiceDescription, .ServiceUnits = item.ServiceUnits,
                        .SiteCeaseDate = item.SiteCeaseDate})
        Next
        rept.LineValues = objList
        Return rept
    End Function
    Public Shared Function GetLengthOfServiceReport(lengthOfService As Int32) As CacheLengthOfServices
        Dim gtYears As Integer = lengthOfService
        Dim rept As New CacheLengthOfServices
        Dim retobj = FMS.Business.DataObjects.usp_GetLengthOfServicesReport.GetLengthOfService(gtYears).ToList()
        Dim objList As New List(Of LengthOfServices)
        For Each item In retobj
            objList.Add(New LengthOfServices() With {
                        .SiteName = item.SiteName, .sitestartdate = item.sitestartdate, .Years = item.Years})
        Next
        rept.LineValues = objList
        Return rept
    End Function
    Public Shared Function GetCustomerByCustZoneReport() As CacheCustomerByCustZone
        Dim rept As New CacheCustomerByCustZone
        Dim retobj = FMS.Business.DataObjects.usp_GetCustomerByCustZone.GetCustByCustZone().ToList()
        Dim objList As New List(Of CustomerByCustZone)
        For Each item In retobj
            objList.Add(New CustomerByCustZone() With {
                        .AddressLine1 = item.AddressLine1, .AddressLine2 = item.AddressLine2, .Cid = item.Cid,
                        .CustomerContactName = item.CustomerContactName, .CustomerName = item.CustomerName,
                        .CustomerPhone = item.CustomerPhone, .PostCode = item.PostCode, .StateCode = item.StateCode,
                        .Suburb = item.Suburb, .ZoneDescription = item.ZoneDescription})
        Next
        rept.LineValues = objList
        Return rept
    End Function
    Public Shared Function GetCustomerContactDetailsReport() As CacheCustomerContactDetails
        Dim rept As New CacheCustomerContactDetails
        Dim retobj = FMS.Business.DataObjects.usp_GetCustomerContactDetailsReport.GetCustomerContactDetailsReport.ToList()
        Dim objList As New List(Of CustomerContactDetails)
        For Each item In retobj
            objList.Add(New CustomerContactDetails() With {
                        .CustomerName = item.CustomerName, .AddressLine1 = item.AddressLine1, .AddressLine2 = item.AddressLine2,
                        .AddressLine3 = item.AddressLine3, .StateDesc = item.StateDesc, .Suburb = item.Suburb, .PostCode = item.PostCode,
                        .CustomerContactName = item.CustomerContactName, .CustomerPhone = item.CustomerPhone, .CustomerMobile = item.CustomerMobile,
                        .CustomerFax = item.CustomerFax, .CustomerComments = item.CustomerComments, .CustomerAgentName = item.CustomerAgentName,
                        .CustomerRating = item.CustomerRating, .CustomerRatingDesc = item.CustomerRatingDesc})
        Next
        rept.LineValues = objList
        Return rept
    End Function
    Public Shared Function GetSiteWithNoContractsReport() As CacheSitesWithNoContracts
        Dim rept As New CacheSitesWithNoContracts
        Dim retobj = FMS.Business.DataObjects.usp_GetSitesWithNoContractsReport.GetSitesWithNoContract.ToList()
        Dim objList As New List(Of SitesWithNoContracts)
        For Each item In retobj
            objList.Add(New SitesWithNoContracts() With {
                        .CustomerName = item.CustomerName, .SiteName = item.SiteName, .SitePeriod = item.SitePeriod,
                        .SiteStartDate = item.SiteStartDate, .ContractPeriodDesc = item.ContractPeriodDesc, .SiteCeaseDate = item.SiteCeaseDate})
        Next
        rept.LineValues = objList
        Return rept
    End Function
    Public Shared Function GetInvoiceBasicCheckReport() As CacheInvoiceBasicCheck
        Dim rept As New CacheInvoiceBasicCheck
        Dim retobj = FMS.Business.DataObjects.usp_GetInvoiceBasicCheckReport.GetInvoiceBasicCheckReport.ToList()
        Dim objList As New List(Of InvoiceBasicCheck)
        For Each item In retobj
            objList.Add(New InvoiceBasicCheck() With {
                        .CustomerName = item.CustomerName, .SiteName = item.SiteName,
                        .SiteCeaseDate = item.SiteCeaseDate, .Frequency = item.Frequency,
                        .InvoiceCommencing = item.InvoiceCommencing, .MonthDescription = item.MonthDescription})
        Next
        rept.LineValues = objList
        Return rept
    End Function
    Public Shared Function GetMYOBCustomerInvoiceReport(custName As String) As CacheMYOBCustomerInvoice
        Dim rept As New CacheMYOBCustomerInvoice
        Dim retobj = FMS.Business.DataObjects.usp_GetMYOBCustomerInvoiceReport.GetMYOBCustomerInvoiceReport(custName).ToList()
        Dim objList As New List(Of MYOBCustomerInvoice)
        For Each item In retobj
            objList.Add(New MYOBCustomerInvoice() With {
                        .CustomerName = item.CustomerName, .CustomerNumber = item.CustomerNumber, .InvoiceNumber = item.InvoiceNumber,
                        .CustomerPurchaseOrderNumber = item.CustomerPurchaseOrderNumber, .Quantity = item.Quantity, .ProductCode = item.ProductCode,
                        .ProductDescription = item.ProductDescription, .InvoiceAmountExGST = item.InvoiceAmountExGST, .InvoiceAmountIncGST = item.InvoiceAmountIncGST,
                        .GSTAmount = item.GSTAmount, .SiteName = item.SiteName})
        Next
        rept.LineValues = objList
        rept.Param = custName
        Return rept
    End Function
    Public Shared Function GetRunListingReport() As CacheRunListing
        Dim rept As New CacheRunListing
        Dim retobj = FMS.Business.DataObjects.usp_GetRunListingReport.GetRunListingReport.ToList()
        Dim objList As New List(Of RunListing)
        For Each item In retobj
            objList.Add(New RunListing() With {
                        .RunNUmber = item.RunNUmber, .RunNum = item.RunNum, .RunDescription = item.RunDescription,
                        .RunDriver = item.RunDriver, .DriverName = item.DriverName, .MondayRun = item.MondayRun,
                        .TuesdayRun = item.TuesdayRun, .WednesdayRun = item.WednesdayRun, .ThursdayRun = item.ThursdayRun,
                        .FridayRun = item.FridayRun, .SaturdayRun = item.SaturdayRun, .SundayRun = item.SundayRun,
                        .InactiveRun = item.InactiveRun, .Rid = item.Rid, .DateOfRun = item.DateOfRun})
        Next
        rept.LineValues = objList
        Return rept
    End Function
    Public Shared Function GetRunListByRunNumberReport() As CacheRunListByRunNumber
        Dim rept As New CacheRunListByRunNumber
        Dim retobj = FMS.Business.DataObjects.usp_GetRunListByRunNumberReport.GetRunListingReport.ToList()
        Dim objList As New List(Of RunListByRunNumber)
        For Each item In retobj
            objList.Add(New RunListByRunNumber() With {
                        .RunNUmber = item.RunNUmber, .RunNum = item.RunNum, .RunNo = item.RunNo, .RunDescription = item.RunDescription,
                        .RunDriver = item.RunDriver, .DriverName = item.DriverName, .MondayRun = item.MondayRun,
                        .TuesdayRun = item.TuesdayRun, .WednesdayRun = item.WednesdayRun, .ThursdayRun = item.ThursdayRun,
                        .FridayRun = item.FridayRun, .SaturdayRun = item.SaturdayRun, .SundayRun = item.SundayRun,
                        .InactiveRun = item.InactiveRun, .Rid = item.Rid, .DateOfRun = item.DateOfRun})
        Next
        rept.LineValues = objList
        Return rept
    End Function
    Public Shared Function GetRunDatesReport(rid As Integer) As CacheRunDates
        Dim rept As New CacheRunDates
        Dim retobj = FMS.Business.DataObjects.usp_GetRunDates.GetRunDatesReport(rid).ToList()
        Dim objList As New List(Of RunDates)
        For Each item In retobj
            objList.Add(New RunDates() With {
                        .RID = item.RID, .DateOfRun = item.DateOfRun})
        Next
        rept.LineValues = objList
        Return rept
    End Function
    Public Shared Function GetGainsAndLossesReport(startDate As Date, endDate As Date) As CacheGainsAndLosses
        Dim rept As New CacheGainsAndLosses
        Dim retobj = FMS.Business.DataObjects.usp_GetGainsAndLossesReport.GetGainsAndLossesReport(startDate, endDate).ToList()
        Dim objList As New List(Of GainsAndLosses)
        For Each item In retobj
            objList.Add(New GainsAndLosses() With {
                        .SalesPerson = item.SalesPerson, .Site = item.Site, .EffectiveDate = item.EffectiveDate,
                        .ServiceCode = item.ServiceCode, .ServiceDescription = item.ServiceDescription, .OldServiceUnits = item.OldServiceUnits,
                        .NewServiceUnits = item.NewServiceUnits, .UnitsDiff = item.UnitsDiff, .UnitType = item.UnitType,
                        .OldPerAnnumCharge = item.OldPerAnnumCharge, .NewPerAnnumCharge = item.NewPerAnnumCharge,
                        .PADiff = item.PADiff, .PAType = item.PAType, .ChangeDate = item.ChangeDate})
        Next
        rept.LineValues = objList
        rept.Param1 = startDate
        rept.Param2 = endDate
        Return rept
    End Function
    Public Shared Function GetGainsAndLossesPerAnnumReport(startDate As Date, endDate As Date) As CacheGainsAndLossesPerAnnum
        Dim rept As New CacheGainsAndLossesPerAnnum
        Dim retobj = FMS.Business.DataObjects.usp_GetGainsAndLossesPerAnnumReport.GetGainsAndLossesPerAnnumReport(startDate, endDate).ToList()
        Dim objList As New List(Of GainsAndLossesPerAnnum)
        For Each item In retobj
            objList.Add(New GainsAndLossesPerAnnum() With {
                        .SalesPerson = item.SalesPerson, .Site = item.Site, .EffectiveDate = item.EffectiveDate,
                        .ServiceCode = item.ServiceCode, .ServiceDescription = item.ServiceDescription, .OldServiceUnits = item.OldServiceUnits,
                        .NewServiceUnits = item.NewServiceUnits, .UnitsDiff = item.UnitsDiff, .UnitType = item.UnitType,
                        .OldPerAnnumCharge = item.OldPerAnnumCharge, .NewPerAnnumCharge = item.NewPerAnnumCharge,
                        .PADiff = item.PADiff, .PAType = item.PAType, .ChangeDate = item.ChangeDate})
        Next
        rept.LineValues = objList
        rept.Param1 = startDate
        rept.Param2 = endDate
        Return rept
    End Function

    Public Shared Function GetStandardAuditReport(startDate As Date, endDate As Date) As CacheStandardAudit
        Dim rept As New CacheStandardAudit
        Dim retobj = FMS.Business.DataObjects.usp_GetStandardAuditReport.GetStandardAuditReport(startDate, endDate).ToList()
        Dim objList As New List(Of StandardAudit)
        For Each item In retobj
            objList.Add(New StandardAudit() With {
                        .Aid = item.Aid, .CSid = item.CSid, .Cid = item.Cid, .Customer = item.Customer, .Site = item.Site,
                        .OldServiceUnits = item.OldServiceUnits, .OldServicePrice = item.OldServicePrice, .OldPerAnnumCharge = item.OldPerAnnumCharge,
                        .NewServiceUnits = item.NewServiceUnits, .NewServicePrice = item.NewServicePrice, .NewPerAnnumCharge = item.NewPerAnnumCharge,
                        .ChangeReasonCode = item.ChangeReasonCode, .User = item.User, .ChangeDate = item.ChangeDate, .ChangeTime = item.ChangeTime,
                        .EffectiveDate = item.EffectiveDate, .OldContractCeasedate = item.OldContractCeasedate, .NewContractCeasedate = item.NewContractCeasedate,
                        .OldInvoiceCommencing = item.OldInvoiceCommencing, .NewInvoiceCommencing = item.NewInvoiceCommencing, .OldInvoicingFrequency = item.OldInvoicingFrequency,
                        .NewInvoicingFrequency = item.NewInvoicingFrequency, .OldContractStartDate = item.OldContractStartDate, .NewContractStartDate = item.NewContractStartDate,
                        .FieldType = item.FieldType, .OldService = item.OldService, .RevenueChangeReason = item.RevenueChangeReason, .ServiceDescription = item.ServiceDescription,
                        .ServiceCode = item.ServiceCode, .EffectiveDate1 = item.EffectiveDate1, .InvoiceCommencing = item.InvoiceCommencing, .Frequency = item.Frequency,
                        .SiteCeaseDate = item.SiteCeaseDate, .FieldType1 = item.FieldType1, .CustomerName = item.CustomerName, .InvoiceMonth1 = item.InvoiceMonth1,
                        .InvoiceMonth2 = item.InvoiceMonth2, .InvoiceMonth3 = item.InvoiceMonth3, .InvoiceMonth4 = item.InvoiceMonth4, .PurchaseOrderNumber = item.PurchaseOrderNumber,
                        .SiteCeaseDate1 = item.SiteCeaseDate1, .OldService1 = item.OldService1, .CustormerSiteCeaseDate = item.CustormerSiteCeaseDate})
        Next
        rept.LineValues = objList
        rept.Param1 = startDate
        rept.Param2 = endDate
        Return rept
    End Function
    Public Shared Function GetAuditContractReport(startDate As Date, endDate As Date) As CacheAuditContract        
        Dim rept As New CacheAuditContract
        Dim retobj = FMS.Business.DataObjects.usp_GetAuditContractReport.GetAuditContractReport(startDate, endDate).ToList()
        Dim objList As New List(Of AuditContract)
        For Each item In retobj
            objList.Add(New AuditContract() With {
                        .FieldType = item.FieldType, .ChangeDate = item.ChangeDate, .Customer = item.Customer,
                        .OldContractCeasedate = item.OldContractCeasedate, .NewContractCeasedate = item.NewContractCeasedate})
        Next
        rept.LineValues = objList
        rept.Param1 = startDate
        rept.Param2 = endDate
        Return rept
    End Function
    Public Shared Function GetAuditOfSiteDetailReport(startDate As Date, endDate As Date) As CacheAuditOfSiteDetail
        Dim rept As New CacheAuditOfSiteDetail
        Dim retobj = FMS.Business.DataObjects.usp_GetAuditOfSiteDetailReport.GetAuditOfSiteDetailReportt(startDate, endDate).ToList()
        Dim objList As New List(Of AuditOfSiteDetail)
        For Each item In retobj
            objList.Add(New AuditOfSiteDetail() With {
                        .FieldType = item.FieldType, .Customer = item.Customer, .Site = item.Site, .OldContractCeasedate = item.OldContractCeasedate,
                        .NewContractCeasedate = item.NewContractCeasedate, .OldInvoiceCommencing = item.OldInvoiceCommencing, .NewInvoiceCommencing = item.NewInvoiceCommencing,
                        .OldInvoicingFrequency = item.OldInvoicingFrequency, .NewInvoicingFrequency = item.NewInvoicingFrequency,
                        .OldContractStartDate = item.OldContractStartDate, .NewContractStartDate = item.NewContractStartDate,
                        .ChangeDate = item.ChangeDate})
        Next
        rept.LineValues = objList
        rept.Param1 = startDate
        rept.Param2 = endDate
        Return rept
    End Function
    Public Shared Function GetSitesBySiteZoneReport() As CacheSitesBySiteZone
        Dim rept As New CacheSitesBySiteZone
        Dim retobj = FMS.Business.DataObjects.usp_GetSitesBySiteZoneReport.GetSitesBySiteZone.ToList()
        Dim objList As New List(Of SitesBySiteZone)
        For Each item In retobj
            objList.Add(New SitesBySiteZone() With {
                        .Zone = item.Zone, .Cid = item.Cid, .SiteName = item.SiteName, .Customer = item.Customer, .AddressLine1 = item.AddressLine1,
                        .AddressLine2 = item.AddressLine2, .AddressLine3 = item.AddressLine3, .Suburb = item.Suburb, .PostCode = item.PostCode,
                        .PostalAddressLine1 = item.PostalAddressLine1, .PostalAddressLine2 = item.PostalAddressLine2, .PostalSuburb = item.PostalSuburb,
                        .PostalPostCode = item.PostalPostCode})
        Next
        rept.LineValues = objList
        Return rept
    End Function
    Public Shared Function GetRunValuesReport(serviceRun As String) As CacheRunValues
        Dim rept As New CacheRunValues
        Dim retobj = FMS.Business.DataObjects.usp_GetRunValuesReport.GetRunValuesReport(serviceRun).ToList()
        Dim objList As New List(Of RunValues)
        For Each item In retobj
            objList.Add(New RunValues() With {
                        .SiteName = item.SiteName, .Add = item.Add, .AddLastLine = item.AddLastLine, .ServiceDescription = item.ServiceDescription,
                        .ServiceUnits = item.ServiceUnits, .PerAnnumCharge = item.PerAnnumCharge, .ServiceRun = item.ServiceRun,
                        .RunDescription = item.RunDescription, .SiteCeaseDate = item.SiteCeaseDate})
        Next
        rept.LineValues = objList
        rept.Param = serviceRun
        Return rept
    End Function
    Public Shared Function GetRunValueSummaryReport() As CacheRunValueSummary
        Dim rept As New CacheRunValueSummary
        Dim retobj = FMS.Business.DataObjects.usp_GetRunValueSummaryReport.GetRunValueSummaryReport.ToList()
        Dim objList As New List(Of RunValueSummary)
        For Each item In retobj
            objList.Add(New RunValueSummary() With {
                        .RunDescription = item.RunDescription, .SumOfPerAnnumCharge = item.SumOfPerAnnumCharge})
        Next
        rept.LineValues = objList
        Return rept
    End Function
    Public Shared Function GetGenerateRunSheetsDetailReport() As CacheGenerateRunSheetsDetail
        Dim paramValues() As String = FMS.Business.ThisSession.ParameterValues.Split(":")
        Dim rept As New CacheGenerateRunSheetsDetail
        Dim retobj = FMS.Business.DataObjects.usp_GetGenerateRunSheetsDetail.GetGenerateRunSheetsDetail().ToList()
        Dim objList As New List(Of GenerateRunSheetsDetail)
        For Each item In retobj
            objList.Add(New GenerateRunSheetsDetail() With {
                        .SortOrder = item.SortOrder, .Cid = item.Cid, .SiteName = item.SiteName, .Add = item.Add,
                        .Suburb = item.Suburb, .SiteContactName = item.SiteContactName, .SiteContactPhone = item.SiteContactPhone,
                        .SiteContactMobile = item.SiteContactMobile, .RunDriver = item.RunDriver, .GeneralSiteServiceComments = item.GeneralSiteServiceComments,
                        .RunNumber = item.RunNumber, .DriverName = item.DriverName, .RunDescription = item.RunDescription, .Notes = item.Notes})
        Next
        rept.LineValues = objList
        rept.ParamDate = paramValues(0).ToString()
        rept.ParamDay = paramValues(1).ToString()
        Dim strForSignature As String = ""
        If paramValues(2).ToString().Equals("'true'") Then
            strForSignature = "X"
        End If
        rept.ParamSignature = strForSignature
        Return rept
    End Function
    Public Shared Function GetGenerateRunSheetsDetailSubReport() As CacheGenerateRunSheetsDetailSub
        Dim rept As New CacheGenerateRunSheetsDetailSub
        Dim retobj = FMS.Business.DataObjects.usp_GetGenerateRunSheetsDetailSub.GetGenerateRunSheetsDetailSub().ToList()
        Dim objList As New List(Of GenerateRunSheetsDetailSub)
        For Each item In retobj
            objList.Add(New GenerateRunSheetsDetailSub() With {
                        .Add = item.Add, .Cid = item.Cid, .CSid = item.CSid, .DriverName = item.DriverName, .GeneralSiteServiceComments = item.GeneralSiteServiceComments,
                        .RunDriver = item.RunDriver, .RunNumber = item.RunNumber, .ServiceComments = item.ServiceComments, .ServiceDesc = item.ServiceDesc,
                        .ServiceUnits = item.ServiceUnits, .SiteContactMobile = item.SiteContactMobile, .SiteContactName = item.SiteContactName, .SiteContactPhone = item.SiteContactPhone,
                        .SiteName = item.SiteName, .SortOrder = item.SortOrder, .Suburb = item.Suburb})
        Next
        rept.LineValues = objList
        Return rept
    End Function
    Public Shared Function GetGenerateRunSheetSummaryReport() As CacheGenerateRunSheetSummary
        Dim paramValues() As String = FMS.Business.ThisSession.ParameterValues.Split(":")
        Dim rept As New CacheGenerateRunSheetSummary
        Dim retobj = FMS.Business.DataObjects.usp_GetGenerateRunSheetSummary.GetGenerateRunSheetSummary().ToList()
        Dim objList As New List(Of GenerateRunSheetSummary)
        For Each item In retobj
            objList.Add(New GenerateRunSheetSummary() With {
                        .ServiceCode = item.ServiceCode, .ServiceDescription = item.ServiceDescription, .SumOfServiceUnits = item.SumOfServiceUnits})
        Next
        rept.LineValues = objList
        rept.ParamDate = paramValues(0).ToString()
        rept.ParamDay = paramValues(1).ToString()
        Return rept
    End Function
    Public Sub New()

    End Sub
End Class
