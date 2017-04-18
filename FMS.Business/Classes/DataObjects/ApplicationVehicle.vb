Namespace DataObjects

    Public Class ApplicationVehicle

#Region "properties"
        Public Property ApplicationVehileID As Guid
        Public Property Name As String
        Public Property Registration As String
        Public Property Notes As String
        Public Property DeviceID As String
        Public Property ApplicationID As Guid
        Public Property ApplicationImageID As Guid?
        Public Property VINNumber As String
        Public Property CurrentDriver As FMS.Business.DataObjects.ApplicationDriver
        Public Property QueryTime As Date
        Public Property CAN_Protocol_Type As String


#End Region

#Region "constructors"

        Public Sub New(av As FMS.Business.ApplicationVehicle)

            With av
                Me.ApplicationVehileID = av.ApplicationVehicleID
                Me.Name = If(String.IsNullOrEmpty(av.Name), String.Empty, av.Name)
                Me.Notes = If(String.IsNullOrEmpty(av.Notes), String.Empty, av.Notes)
                Me.Registration = If(String.IsNullOrEmpty(av.Registration), String.Empty, av.Registration)
                Me.DeviceID = av.DeviceID
                Me.ApplicationID = av.ApplicationID
                Me.VINNumber = If(String.IsNullOrEmpty(av.VINNumber), String.Empty, av.VINNumber)
                If av.ApplicationImageID Is Nothing Then
                    Dim mm = DataObjects.FleetMapMarker.GetApplicationFleetMapMarket(av.ApplicationID)
                    Me.ApplicationImageID = mm.Vehicle_ApplicationImageID
                Else
                    Me.ApplicationImageID = av.ApplicationImageID
                End If

                Me.CAN_Protocol_Type = If(String.IsNullOrEmpty(av.CAN_Protocol_Type), "j1939", av.CAN_Protocol_Type)

               
            End With

        End Sub

        ''' <summary>
        ''' for serialization only
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

#End Region

#Region "CRUD"

        Public Class CanStandard
            Public Property Name As String
            Public Property ID As String

            Public Shared Function GetAllCANPRotocols() As List(Of CanStandard)

                Dim protocols As String = "j1939,Zagro,CANopen - EN 50325-4,EnergyBus - CiA 454"

                Return (From x In protocols.Split(","c) Select New CanStandard With {.ID = x, .Name = x}).ToList

            End Function

            Public Sub New()

            End Sub
        End Class

       

        Public Shared Sub Create(av As FMS.Business.DataObjects.ApplicationVehicle)

            Dim contextAppVecle As New FMS.Business.ApplicationVehicle

            With contextAppVecle
                .ApplicationVehicleID = Guid.NewGuid
                .Name = av.Name
                .Notes = av.Notes
                .Registration = av.Registration
                .DeviceID = av.DeviceID
                .ApplicationID = av.ApplicationID
                .VINNumber = av.VINNumber
                .ApplicationImageID = av.ApplicationImageID
                .CAN_Protocol_Type = av.CAN_Protocol_Type

            End With

            SingletonAccess.FMSDataContextContignous.ApplicationVehicles.InsertOnSubmit(contextAppVecle)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Update(av As FMS.Business.DataObjects.ApplicationVehicle)

            Dim currObj As FMS.Business.ApplicationVehicle = (From i In SingletonAccess.FMSDataContextContignous.ApplicationVehicles
                                                              Where i.ApplicationVehicleID = av.ApplicationVehileID).Single


            With currObj
                '.ApplicationVehicleID = Guid.NewGuid
                .Name = av.Name
                .Notes = av.Notes
                .Registration = av.Registration
                .DeviceID = av.DeviceID
                .ApplicationID = av.ApplicationID
                .VINNumber = av.VINNumber

                .CAN_Protocol_Type = av.CAN_Protocol_Type

                .ApplicationImageID = av.ApplicationImageID
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub


        Public Shared Sub Delete(av As FMS.Business.DataObjects.ApplicationVehicle)

            Dim currObj As FMS.Business.ApplicationVehicle = (From i In SingletonAccess.FMSDataContextContignous.ApplicationVehicles
                                                             Where i.ApplicationVehicleID = av.ApplicationVehileID).Single

            SingletonAccess.FMSDataContextContignous.ApplicationVehicles.DeleteOnSubmit(currObj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

#End Region

#Region "CANbus specific"


        Public Function GetAvailableCANTags(deviceid As String, standard As String) As List(Of DataObjects.CAN_MessageDefinition)

            Dim lst As New List(Of DataObjects.CAN_MessageDefinition)

            'search for all the pi points which exist for the device
            Dim plst As PISDK.PointList = SingletonAccess.HistorianServer.GetPoints( _
                                                        String.Format("tag = 'CAN*{0}*'", deviceid))

            Dim cnt As Integer = plst.Count

            For Each p As PISDK.PIPoint In plst

                Try

                    Dim ppName As String = p.Name

                    'get the SPN number
                    Dim replaceStr As String = String.Format("CAN_{0}_", deviceid)
                    Dim spn As Integer = ppName.Replace(replaceStr, "")

                    lst.AddRange(CAN_MessageDefinition.GetForPGN(spn, standard))

                Catch ex As Exception

                    Dim msg As String = ex.Message
                End Try
            Next

            Return lst

        End Function


#End Region

#Region "Selectors"

        Public Shared Function GetFromVINNumber(vinNumber As String, appid As Guid) As ApplicationVehicle

            Return GetAll(appid).Where(Function(x) x.VINNumber.ToLower.Trim = vinNumber.ToLower.Trim).SingleOrDefault

        End Function

        Public Shared Function GetFromName(name As String, appid As Guid) As ApplicationVehicle

            Return GetAll(appid).Where(Function(x) x.Name.ToLower.Trim = name.ToLower.Trim).SingleOrDefault

        End Function

        Public Shared Function GetFromName(name As String) As ApplicationVehicle

            Return (From x In SingletonAccess.FMSDataContextContignous.ApplicationVehicles _
                    Where x.Name = name _
                    Select New DataObjects.ApplicationVehicle(x)).ToList.FirstOrDefault()

        End Function

        Public Shared Function GetAll(appplicationID As Guid) As List(Of ApplicationVehicle)

            Dim retobj As Object = SingletonAccess.FMSDataContextNew.ApplicationVehicles.Where(Function(y) y.ApplicationID = appplicationID).OrderBy(Function(m) m.DeviceID).Select( _
                                                                            Function(x) New DataObjects.ApplicationVehicle(x)).ToList

            Return retobj

        End Function

        Public Shared Function GetForID(Id As Guid) As ApplicationVehicle

            Return SingletonAccess.FMSDataContextNew.ApplicationVehicles.Where(
                            Function(y) y.ApplicationVehicleID = Id).Select( _
                            Function(x) New DataObjects.ApplicationVehicle(x)).Single

        End Function

        Public Shared Function GetAllWithDrivers(appid As Guid, querydate As Date) As List(Of ApplicationVehicle)

            querydate = querydate.timezoneToPerth

            Dim vehicles As List(Of DataObjects.ApplicationVehicle) = GetAll(appid)

            Dim drivers As List(Of usp_GetVehiclesAndDriversFortimePeriodResult) = _
                    SingletonAccess.FMSDataContextNew.usp_GetVehiclesAndDriversFortimePeriod(appid, querydate, querydate).ToList

            For Each itm As DataObjects.ApplicationVehicle In vehicles

                'get the application vehicleID
                Dim driver As usp_GetVehiclesAndDriversFortimePeriodResult = _
                                    (From i In drivers Where i.ApplicationVehicleID = itm.ApplicationVehileID).FirstOrDefault

                If driver.ApplicationDriverID.HasValue Then _
                        itm.CurrentDriver = DataObjects.ApplicationDriver.GetDriverFromID(driver.ApplicationDriverID)

                itm.QueryTime = querydate.timezoneToClient

            Next

            Return vehicles

        End Function


        ''' <summary>
        ''' For the outlook like 
        ''' schedule  control
        ''' </summary>
        ''' <param name="appid"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetAllAsScheduleResources(appid As Guid) As List(Of CustomResource)


            Dim retobjs As New List(Of CustomResource)
            Dim vehicles As List(Of DataObjects.ApplicationVehicle) = GetAll(appid)


            For Each v As DataObjects.ApplicationVehicle In vehicles

                retobjs.Add(New CustomResource(v.ApplicationVehileID, v.Name))
            Next

            Return retobjs

        End Function

#End Region

#Region "not static methods"

        Public Function GetOdometerReadings() As List(Of ApplicationVehicleOdometerReading)

            Return ApplicationVehicleOdometerReading.GetForVehicleID(Me.ApplicationVehileID)

        End Function

        Public Class VehicleMovementSummary

            Public Property StartDate As Date
            Public Property EndDate As Date
            Public Property KilometersTravelled As Decimal
            Public Property EngineHoursOn As TimeSpan
            Public Sub New()

            End Sub
        End Class

        Public Function GetVehicleMovementSummary(startDate As Date, endDate As Date) As VehicleMovementSummary

            Dim retobj As New VehicleMovementSummary With {.StartDate = startDate, .EndDate = endDate}

            startDate = startDate.timezoneToPerth
            endDate = endDate.timezoneToPerth

            Dim speedAnddistObj As ReportGeneration.VehicleSpeedRetObj = _
                    ReportGeneration.ReportGenerator.GetVehicleSpeedAndDistance(Me.ApplicationVehileID, startDate, endDate)

            Dim distanceTravelled As Decimal = 0
            Dim engineHours As New TimeSpan(0)

            For Each tsf In speedAnddistObj.TimeSpansWithVals

                'if were moving quicker than x km/h , then log it, if not, then this is classed as noise
                If tsf.speed >= 10 Then

                    distanceTravelled += tsf.distance

                    Dim thisTimespan As TimeSpan = TimeSpan.FromDays(0)

                    If tsf.EndDate <> DateTime.MinValue AndAlso tsf.StartDate <> DateTime.MinValue Then
                        thisTimespan = tsf.EndDate - tsf.StartDate
                    End If

                    engineHours += thisTimespan
                End If

            Next

            retobj.KilometersTravelled = distanceTravelled
            retobj.EngineHoursOn = TimeSpan.FromHours(engineHours.TotalHours)

            Return retobj

        End Function


#End Region

    End Class

End Namespace

