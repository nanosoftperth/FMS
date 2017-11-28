Namespace DataObjects

    Public Class ApplicationVehicle

#Region "properties"
        Public Property ApplicationVehileID As Guid
        Public Property Name As String
        Public Property Registration As String
        Public Property Notes As String
        Public Property DeviceID As String
        Public Property ApplicationID As Guid
        Public Property ApplicationLocationID As Guid ' for business location
        Public Property BusinessLocation As Object ' for business location
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
                Me.BusinessLocation = FormatBussLocation(av.BusinessLocation)


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

                Dim protocols As String = "j1939,Zagro125,Zagro500,CANopen - EN 50325-4,EnergyBus - CiA 454"

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

                .BusinessLocation = FormatBussLocation(av.BusinessLocation)

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

                .BusinessLocation = FormatBussLocation(av.BusinessLocation)
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




        Public Function GetAvailableCANTags() As List(Of DataObjects.CAN_MessageDefinition)

            Return DataObjects.Device.GetCANMessageDefinitions(DeviceID)

        End Function

        Public Function GetZagroStatus() As Boolean
            Dim plst As PISDK.PointList = SingletonAccess.HistorianServer.GetPoints( _
                                                       String.Format("tag = 'can*{0}*'", DeviceID))
            Dim retValue As Boolean = False
            For Each p As PISDK.PIPoint In plst

                Try
                    Dim ppname As String = p.Name
                    If ppname.ToUpper.IndexOf("ZAGRO") > 0 AndAlso Not ppname.Split("_").Reverse()(0).Equals("0") Then
                        retValue = True
                        Exit For
                    End If
                Catch ex As Exception
                    Dim msg As String = ex.Message
                End Try
            Next
            Return retValue
        End Function
        Public Function GetAvailableCANTagsValue() As List(Of DataObjects.CanValueMessageDefinition)

            Dim lst As New List(Of DataObjects.CAN_MessageDefinition)
            Dim lstNew As New List(Of DataObjects.CanValueMessageDefinition)

            'search for all the pi points which exist for the device
            Dim plst As PISDK.PointList = SingletonAccess.HistorianServer.GetPoints( _
                                                        String.Format("tag = 'can*{0}*'", DeviceID))
            Dim cnt As Integer = plst.Count

            For Each p As PISDK.PIPoint In plst

                Try

                    Dim ppname As String = p.Name

                    'get the spn number
                    'dim replacestr as string = string.format("can_{0}_", deviceid)
                    'dim pgn as integer = ppname.replace(replacestr, "")
                    Dim pgn As Integer = ppname.Split("_").Reverse()(0)
                    Dim protocolType As String = ppname.Split("_")(2)

                    lst.AddRange(CAN_MessageDefinition.GetForPGN(pgn, protocolType))

                Catch ex As Exception
                    Dim msg As String = ex.Message
                End Try
            Next

            For Each canmessdef As CAN_MessageDefinition In lst.Where(Function(x) x.Standard.ToLower.Equals("zagro125") Or x.Standard.ToLower.Equals("zagro500")).Distinct().ToList()
                Dim canValue As New CanValueMessageDefinition()
                Dim PointWithData = FMS.Business.DataObjects.CanDataPoint.GetPointWithLatestDataByDeviceId(canmessdef.SPN, DeviceID, canmessdef.Standard)
                canValue.MessageDefinition = canmessdef
                canValue.CanValues = PointWithData.CanValues
                lstNew.Add(canValue)
            Next
            'Use group by standard and spn to eliminate duplicate
            Return lstNew.ToList().ToList().GroupBy(Function(xx) New With {Key xx.MessageDefinition.Standard, Key xx.MessageDefinition.PGN, Key xx.MessageDefinition.SPN}).ToList().Select(Function(xxx) xxx.First()).ToList()

        End Function

        Public Function GetAvailableCANTagsValueForDash() As List(Of DataObjects.CanValueMessageDefinition)

            Dim lst As New List(Of DataObjects.CAN_MessageDefinition)
            Dim lstNew As New List(Of DataObjects.CanValueMessageDefinition)

            'search for all the pi points which exist for the device
            Dim plst As PISDK.PointList = SingletonAccess.HistorianServer.GetPoints( _
                                                        String.Format("tag = 'can*{0}*'", DeviceID))
            Dim cnt As Integer = plst.Count

            For Each p As PISDK.PIPoint In plst

                Try

                    Dim ppname As String = p.Name

                    'get the spn number
                    'dim replacestr as string = string.format("can_{0}_", deviceid)
                    'dim pgn as integer = ppname.replace(replacestr, "")
                    Dim pgn As Integer = ppname.Split("_").Reverse()(0)
                    lst.AddRange(CAN_MessageDefinition.GetForPGN(pgn, Me.CAN_Protocol_Type))

                Catch ex As Exception
                    Dim msg As String = ex.Message
                End Try
            Next

            'For Each canmessdef As CAN_MessageDefinition In lst.Where(Function(x) x.Standard.ToLower.Equals("zagro125") _
            '                                                              Or x.Standard.ToLower.Equals("zagro500") _
            '                                                              Or x.Standard.ToLower.Equals("j1939") _
            '                                                              Or x.Standard.ToLower.Equals("NANO1000")).Distinct().ToList()
            For Each canmessdef As CAN_MessageDefinition In lst.Where(Function(x) x.Standard.ToLower.Equals("zagro125") _
                                                                          Or x.Standard.ToLower.Equals("zagro500") _
                                                                          Or x.Standard.ToLower.Equals("j1939")).Distinct().ToList()
                Dim canValue As New CanValueMessageDefinition()
                Dim PointWithData = FMS.Business.DataObjects.CanDataPoint.GetPointWithLatestDataByDeviceId(canmessdef.SPN, DeviceID, canmessdef.Standard)
                canValue.MessageDefinition = canmessdef
                canValue.CanValues = PointWithData.CanValues
                lstNew.Add(canValue)
            Next
            'Use group by standard and spn to eliminate duplicate
            Return lstNew.ToList().ToList().GroupBy(Function(xx) New With {Key xx.MessageDefinition.Standard, Key xx.MessageDefinition.PGN, Key xx.MessageDefinition.SPN}).ToList().Select(Function(xxx) xxx.First()).ToList()

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


            With New FMS.Business.LINQtoSQLClassesDataContext()

                Return (From x In .ApplicationVehicles _
                         Where x.Name = name
                         Select New DataObjects.ApplicationVehicle(x)).FirstOrDefault

            End With


            'Return (From x In SingletonAccess.FMSDataContextContignous.ApplicationVehicles _
            '        Where x.Name = name _
            '        Select New DataObjects.ApplicationVehicle(x)).ToList.FirstOrDefault()

        End Function

        Public Shared Function GetFromDeviceID(deviceID As String) As ApplicationVehicle

            Return (From x In SingletonAccess.FMSDataContextContignous.ApplicationVehicles _
                    Where x.DeviceID = deviceID _
                    Select New DataObjects.ApplicationVehicle(x)).ToList.FirstOrDefault()

        End Function

        Public Shared Function GetAll(appplicationID As Guid) As List(Of ApplicationVehicle)

            If appplicationID = Guid.Empty Then Return Nothing

            Dim retobj As Object = SingletonAccess.FMSDataContextNew.ApplicationVehicles.Where(Function(y) y.ApplicationID = appplicationID).OrderBy(Function(m) m.DeviceID).Select( _
                                                                            Function(x) New DataObjects.ApplicationVehicle(x)).ToList

            Return retobj

        End Function

        ' For Business Location
        Public Shared Function GetAllWithBusinessLocation(appplicationID As Guid, deviceID As String) As List(Of ApplicationVehicle)

            If appplicationID = Guid.Empty Then Return Nothing

            Dim retobj As Object = SingletonAccess.FMSDataContextNew.ApplicationVehicles.Where(Function(y) y.ApplicationID = appplicationID And y.DeviceID = deviceID).OrderBy(Function(m) m.DeviceID).Select( _
                                                                            Function(x) New DataObjects.ApplicationVehicle(x)).ToList

            Return retobj

        End Function



        Public Function GetCurrentTimeZoneOffsetFromPerth() As Double

            Try

                Dim latlng As KeyValuePair(Of Decimal, Decimal) = _
                                FMS.Business.DataObjects.Device.GetFromDeviceID(DeviceID).GetLatLongs(Now)

                Dim tzr As GoogleAPI.TimeZoneResponse = FMS.Business.GoogleAPI.GetCurrentTimeZoneOffset(latlng.Key, latlng.Value)

                Dim tz As DataObjects.TimeZone = New DataObjects.TimeZone(tzr)

                Return tz.Offset_FromPerthToHQ

            Catch ex As Exception
                'nop
            End Try

            Return 0

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
        Public Shared Function GetApplicationsVehicleList(appID As Guid) As List(Of FMS.Business.DataObjects.ApplicationVehicle)


            Dim ObjList As New List(Of DataObjects.ApplicationVehicle)
            Dim resultString = FMS.Business.DataObjects.ApplicationVehicle.GetAll(appID)


            ObjList.Add(New FMS.Business.DataObjects.ApplicationVehicle() With
                                 {.Name = "Select All"})

            For Each listItem In resultString

                ObjList.Add(New FMS.Business.DataObjects.ApplicationVehicle() With
                                  {.Name = listItem.Name})

            Next

            Return ObjList
            '  Return FMS.Business.DataObjects.ApplicationVehicle.GetAll(ThisSession.ApplicationID)
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


        Public Shared Function FormatBussLocation(businesslocation As Object) As String
            Try
                Dim strValue As String = ""

                If (businesslocation IsNot Nothing) Then

                    Dim objType = businesslocation.GetType()

                    If (objType.Name() = "List`1") Then
                        Dim oCtr = DirectCast(businesslocation.Count, Integer)
                        Dim blValue As Guid
                        'Dim strValue As String = ""
                        '(New System.Collections.Generic.Mscorlib_CollectionDebugView(Of Object)(businesslocation)).Items(0)
                        If oCtr > 0 Then

                            For Each element As Object In businesslocation
                                ' Avoid Nothing elements.
                                If element IsNot Nothing Then
                                    blValue = element

                                    If blValue.ToString() <> "00000000-0000-0000-0000-000000000000" Then
                                        If strValue.Length > 0 Then
                                            strValue = strValue + "|" + blValue.ToString()
                                        Else
                                            strValue = blValue.ToString()
                                        End If
                                    End If

                                End If
                            Next

                        End If
                    Else
                        strValue = businesslocation.ToString()
                    End If

                End If

                Return strValue

            Catch ex As Exception
                Throw ex
            End Try
        End Function


#End Region



    End Class

End Namespace

