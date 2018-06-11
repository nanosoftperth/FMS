Imports System.Globalization
Imports FMS.Business.DataObjects.CanDataPoint
Imports OSIsoft.AF

Namespace DataObjects
    Public Class DataLoggerReport
        Public Property DeviceId As String
        Public Property Standard As String
        Public Property Spn As Integer
        Public Property StartDate As Date
        Public Property EndDate As Date
        Public Shared Function GetLatLongLog(deviceid As String, startDate As Date, endDate As Date) As List(Of DevicePositionLatLong)
            Dim lstDPLatLong As New List(Of DevicePositionLatLong)
            Dim myPISystem As PISystem = New PISystems().DefaultPISystem
            Try
                myPISystem.Connect()
                Dim afdb As AFDatabase = myPISystem.Databases("FMS")

                Dim afnamedcoll As AFNamedCollection(Of Asset.AFElement) =
                    OSIsoft.AF.Asset.AFElement.FindElements(afdb, Nothing, "device",
                                AFSearchField.Template, True, AFSortField.Name, AFSortOrder.Ascending, 1000)

                Dim afe As Asset.AFElement = (From x In afnamedcoll Where x.Name = deviceid).SingleOrDefault
                Dim attr_Lat As Asset.AFAttribute = afe.Attributes("lat")
                Dim attr_Long As Asset.AFAttribute = afe.Attributes("long")
                Dim st As New OSIsoft.AF.Time.AFTime(startDate.ToString("MM/dd/yyyy hh:mm:ss tt"), CultureInfo.InvariantCulture)
                Dim et As New OSIsoft.AF.Time.AFTime(endDate.ToString("MM/dd/yyyy hh:mm:ss tt"), CultureInfo.InvariantCulture)
                Dim tr As New OSIsoft.AF.Time.AFTimeRange(st, et)
                Dim latvals As Asset.AFValues = attr_Lat.PIPoint.RecordedValues(tr, Data.AFBoundaryType.Inside, Nothing, False)
                Dim longVals As Asset.AFValues = attr_Long.PIPoint.RecordedValues(tr, Data.AFBoundaryType.Inside, Nothing, False)

                Dim intLatLongCount As Integer = Math.Max(latvals.Count, longVals.Count)
                For counter As Integer = 0 To intLatLongCount - 1
                    Dim dpLatLong As New DevicePositionLatLong
                    dpLatLong.DeviceName = deviceid
                    dpLatLong.Latitude = latvals(counter).Value
                    dpLatLong.Longitude = longVals(counter).Value
                    dpLatLong.LatitudeDateTime = latvals(counter).Timestamp
                    dpLatLong.LongtitudeDateTime = longVals(counter).Timestamp
                    lstDPLatLong.Add(dpLatLong)
                Next
                Return lstDPLatLong
            Catch ex As Exception
                Return Nothing
            End Try
        End Function
        Public Shared Function Get7502DataLogger(deviceid As String, startDate As Date, endDate As Date) As List(Of SpeedReportFields)
            Dim param0 As New DataLoggerReport
            param0.DeviceId = deviceid
            param0.Standard = "Zagro125"
            param0.Spn = 1
            param0.StartDate = startDate
            param0.EndDate = endDate
            Dim speedList = GetDataLogger(param0) 'pgn:578, spn:1, speed

            Dim dtLogger As New List(Of SpeedReportFields)
            Dim currentValue As Integer
            Dim dtTimeA As DateTime
            Dim dtTimeCurrent As DateTime
            For d As Integer = 0 To speedList.Count
                Dim objSpeed As New SpeedReportFields
                Try
                    dtTimeA = speedList(d + 1).Time.ToShortTimeString
                    dtTimeCurrent = speedList(d).Time.ToShortTimeString
                    objSpeed.Description = "Speed"
                    currentValue = speedList(d).Value
                    objSpeed.ValueX = speedList(d).Value
                    objSpeed.DateRawX = speedList(d).Time
                    objSpeed.DateX = speedList(d).Time.ToString("dd/MM/yyyy HH:mm:ss")
                    dtLogger.Add(objSpeed)
                Catch ex As Exception
                End Try
            Next
            Return dtLogger
        End Function
        Public Shared Function GetSpeedDataLogger(deviceid As String, startDate As Date, endDate As Date) As List(Of SpeedFields)
            Dim param0 As New DataLoggerReport
            If Not deviceid.Equals("") Then


                param0.DeviceId = deviceid
                param0.Standard = "Zagro125"
                param0.Spn = 1
                param0.StartDate = startDate
                param0.EndDate = endDate
                Dim speedList = GetDataLogger(param0) 'pgn:578, spn:1, speed

                Dim dtLogger As New List(Of SpeedFields)
                Dim currentValue As Integer
                Dim dtTimeA As DateTime
                Dim dtTimeCurrent As DateTime
                For d As Integer = 0 To speedList.Count
                    Dim objSpeed As New SpeedFields
                    Try
                        dtTimeA = speedList(d + 1).Time.ToShortTimeString
                        dtTimeCurrent = speedList(d).Time.ToShortTimeString
                        objSpeed.Description = "Speed"
                        currentValue = speedList(d).Value
                        objSpeed.Value = speedList(d).Value
                        objSpeed.SpeedDateTime = speedList(d).Time
                        dtLogger.Add(objSpeed)
                    Catch ex As Exception
                    End Try
                Next
                Return dtLogger
            Else
                Return Nothing
            End If
        End Function

        Public Shared Function GetReportDirection(deviceid As String, startDate As Date, endDate As Date) As List(Of ReportFields)
            Dim dtLogger As New List(Of ReportFields)

            GetHeadlight(dtLogger, deviceid, startDate, endDate)
            GetDirection(dtLogger, deviceid, startDate, endDate)
            GetServiceBrake(dtLogger, deviceid, startDate, endDate)
            GetParkingBrake(dtLogger, deviceid, startDate, endDate)
            GetHorn(dtLogger, deviceid, startDate, endDate)
            GetVigilanceTimeOut(dtLogger, deviceid, startDate, endDate)
            GetRoadRailEngaged(dtLogger, deviceid, startDate, endDate)

            Return dtLogger
        End Function
        Private Shared Function GetDeviceDataLogger(DeviceID As String, sDate As Date, eDate As Date) As List(Of String)
            Dim lstRetVal As New List(Of String)
            Try
                Dim startDate As Date = sDate
                Dim endDate As Date = eDate

                Dim pipName As String = DeviceID & "_log"

                Dim pit As New PITimeServer.PITime With {.LocalDate = Now}

                Dim pp As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(pipName)

                Dim pivds = SingletonAccess.HistorianServer.PIPoints(pipName).Data.RecordedValues(startDate, endDate, PISDK.BoundaryTypeConstants.btInside)

                For Each p As PISDK.PIValue In pivds
                    lstRetVal.Add(p.Value.ToString())
                Next
            Catch ex As Exception
                lstRetVal.Add(ex.Message.ToString)
            End Try
            Return lstRetVal
        End Function
        Private Shared Function GetDataLogger(reportParam As DataLoggerReport) As List(Of CanValue)
            Dim lstCanDataPoint As New List(Of CanValue)
            Dim retobj As New CanDataPoint

            Dim vehicle As DataObjects.ApplicationVehicle = DataObjects.ApplicationVehicle.GetFromDeviceID(reportParam.DeviceId)

            'get the MessageDefinition
            retobj.MessageDefinition = DataObjects.CAN_MessageDefinition.GetForSPN(reportParam.Spn, reportParam.Standard)

            ' Format = CAN_DeviceID_CanStandard_PGN (eg: CAN_demo01_Zagro125_255)
            Dim tagName As String = DataObjects.CanDataPoint.GetTagName(vehicle.DeviceID, reportParam.Standard,
                                                                                retobj.MessageDefinition.PGN)

            Try

                Dim pp As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(tagName)

                Dim startDate As Date = reportParam.StartDate
                Dim endDate As Date = reportParam.EndDate
                Dim intCount As Integer = 1
                'get data from pi for the time period
                Dim pivds As PISDK.PIValues = pp.Data.RecordedValues(startDate, endDate,
                                                                     PISDK.BoundaryTypeConstants.btInside)
                For Each p As PISDK.PIValue In pivds
                    Try
                        retobj.CanValues.Add(New CanValue With {.Time = p.TimeStamp.LocalDate,
                                                                                .RawValue = p.Value})
                        'Calculate the actual value from the raw values
                        retobj.CanValues.CalculateValues(reportParam.Spn, retobj.MessageDefinition)

                    Catch ex As Exception
                    End Try
                Next
            Catch ex As Exception
                retobj.MessageDefinition = New FMS.Business.DataObjects.CAN_MessageDefinition()
            End Try

            Return retobj.CanValues
        End Function
        Private Shared Sub GetDirection(ByRef dtLogger As List(Of ReportFields), deviceid As String, startDate As Date, endDate As Date)
            Dim param0 As New DataLoggerReport
            param0.DeviceId = deviceid
            param0.Standard = "Zagro125"
            param0.Spn = 1
            param0.StartDate = startDate
            param0.EndDate = endDate
            Dim speedList = GetDataLogger(param0) 'pgn:578, spn:1,
            Dim blnDirectionAddedOn As Boolean = False
            Dim blnDirectionAddedOff As Boolean = False
            Dim inta As Integer
            Dim intb As Integer
            Dim dblback As Double
            Dim dblforward As Double
            Dim addBack As Boolean = False
            Dim addForward As Boolean = False
            Dim reportForward As New ReportFields
            Dim reportBack As New ReportFields
            For d As Integer = 0 To speedList.Count
                Try
                    reportForward.Description = "Direction"
                    reportBack.Description = "Direction"
                    If speedList(d).Value <> 0 Then
                        If speedList(d).Value < 0 Then
                            inta = inta + 1
                            dblback = speedList(d + 1).Value
                            If addBack Then
                                dtLogger.Add(reportForward)
                                intb = 0
                                addBack = False
                                blnDirectionAddedOff = False
                                reportForward = New ReportFields
                            End If
                            If dblback > 0 Or dblback = 0 Then
                                reportBack = New ReportFields
                                reportBack.Direction = "backward-" & inta.ToString()
                                'reportBack.Direction = "backward"
                                reportBack.Value = inta 'backward
                                addForward = True
                                blnDirectionAddedOn = True
                            End If
                        End If

                        If speedList(d).Value > 0 Then
                            intb = intb + 1
                            dblforward = speedList(d + 1).Value
                            If addForward Then
                                dtLogger.Add(reportBack)
                                inta = 0
                                addForward = False
                                blnDirectionAddedOn = False
                                reportBack = New ReportFields
                            End If
                            If dblforward < 0 Or dblforward = 0 Then
                                reportForward = New ReportFields
                                reportForward.Direction = "forward-" & intb.ToString()
                                'reportForward.Direction = "forward"
                                reportForward.Value = intb 'forward
                                addBack = True
                                blnDirectionAddedOff = True
                            End If
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
            If Not blnDirectionAddedOn And inta > 0 Then
                reportBack = New ReportFields
                reportBack.Description = "Direction"
                reportBack.Direction = "forward On-" & inta.ToString()
                reportBack.Value = inta 'On
                dtLogger.Add(reportBack)
            End If
            If Not blnDirectionAddedOff And intb > 0 Then
                reportForward = New ReportFields
                reportForward.Description = "Direction"
                reportForward.Direction = "forward Off-" & intb.ToString()
                reportForward.Value = intb  'Off
                dtLogger.Add(reportForward)
            End If
        End Sub
        Private Shared Sub GetHeadlight(ByRef dtLogger As List(Of ReportFields), deviceid As String, startDate As Date, endDate As Date)
            Dim loggerData As List(Of String) = GetDeviceDataLogger(deviceid, startDate, endDate)
            Dim blnHeadlightOn As Boolean
            Dim blnHeadlightOff As Boolean
            Dim blnHeadlightAddedOn As Boolean = False
            Dim blnHeadlightAddedOff As Boolean = False
            Dim intHeadlightOn As Integer
            Dim intHeadlightOff As Integer
            Dim addHeadlightOn As Boolean = False
            Dim addHeadlightOff As Boolean = False
            Dim strHeadlightOn As String
            Dim strHeadlightOff As String
            Dim reportHeadlightOn As New ReportFields
            Dim reportHeadlightOff As New ReportFields
            For a As Integer = 0 To loggerData.Count
                Try
                    reportHeadlightOn.Description = "Headlight On Off"
                    reportHeadlightOff.Description = reportHeadlightOn.Description
                    If loggerData(a).IndexOf("Headlights") > 0 Then
                        intHeadlightOn = intHeadlightOn + 1
                        strHeadlightOn = loggerData(a + 1)
                        If blnHeadlightOff Then
                            dtLogger.Add(reportHeadlightOff)
                            intHeadlightOff = 0
                            blnHeadlightOff = False
                            blnHeadlightAddedOff = False
                            reportHeadlightOff = New ReportFields
                        End If
                        If strHeadlightOn.IndexOf("Headlights") < 0 Then
                            reportHeadlightOn = New ReportFields
                            reportHeadlightOn.Direction = "Headlight On-" & intHeadlightOn.ToString()
                            reportHeadlightOn.Value = intHeadlightOn 'On
                            blnHeadlightOn = True
                            blnHeadlightAddedOn = True
                        End If
                    Else
                        intHeadlightOff = intHeadlightOff + 1
                        strHeadlightOff = loggerData(a + 1)
                        If blnHeadlightOn Then
                            dtLogger.Add(reportHeadlightOn)
                            intHeadlightOn = 0
                            blnHeadlightOn = False
                            blnHeadlightAddedOn = False
                            reportHeadlightOn = New ReportFields
                        End If
                        If strHeadlightOff.IndexOf("Headlights") > 0 Then
                            reportHeadlightOff = New ReportFields
                            reportHeadlightOff.Direction = "Headlight Off-" & intHeadlightOff.ToString()
                            reportHeadlightOff.Value = intHeadlightOff  'Off
                            blnHeadlightOff = True
                            blnHeadlightAddedOff = True
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
            If Not blnHeadlightAddedOn And intHeadlightOn > 0 Then
                reportHeadlightOn = New ReportFields
                reportHeadlightOn.Description = "Headlight On Off"
                reportHeadlightOn.Direction = "Headlight On-" & intHeadlightOn.ToString()
                reportHeadlightOn.Value = intHeadlightOn 'On
                dtLogger.Add(reportHeadlightOn)
            End If
            If Not blnHeadlightAddedOff And intHeadlightOff > 0 Then
                reportHeadlightOff = New ReportFields
                reportHeadlightOff.Description = "Headlight On Off"
                reportHeadlightOff.Direction = "Headlight Off-" & intHeadlightOff.ToString()
                reportHeadlightOff.Value = intHeadlightOff  'Off
                dtLogger.Add(reportHeadlightOff)
            End If
        End Sub
        Private Shared Sub GetServiceBrake(ByRef dtLogger As List(Of ReportFields), deviceid As String, startDate As Date, endDate As Date)
            Dim blnBrakeAddedOn As Boolean = False
            Dim blnBrakeAddedOff As Boolean = False
            Dim blnBrakeOn As Boolean
            Dim blnBrakeOff As Boolean
            Dim strBrakeOn As String
            Dim strBrakeOff As String
            Dim intServiceParkingOn As Integer
            Dim intServiceParkingOff As Integer
            Dim param1 As New DataLoggerReport
            param1.DeviceId = deviceid
            param1.Standard = "Zagro125"
            param1.Spn = 2
            param1.StartDate = startDate
            param1.EndDate = endDate
            Dim brakeList = GetDataLogger(param1) 'pgn:578, spn:2, parking brake
            Dim reportServiceBrakeOn As New ReportFields
            Dim reportServiceBrakeOff As New ReportFields
            For d As Integer = 0 To brakeList.Count
                Try
                    reportServiceBrakeOn.Description = "Service Brake On Off"
                    reportServiceBrakeOff.Description = reportServiceBrakeOn.Description
                    If brakeList(d).Value = "Parking Brake ON" Then
                        intServiceParkingOn = intServiceParkingOn + 1
                        strBrakeOn = brakeList(d + 1).Value
                        If blnBrakeOff Then
                            dtLogger.Add(reportServiceBrakeOff)
                            intServiceParkingOff = 0
                            blnBrakeOff = False
                            blnBrakeAddedOff = False
                            reportServiceBrakeOff = New ReportFields
                        End If
                        If strBrakeOn = "Parking Brake OFF" Then
                            reportServiceBrakeOn = New ReportFields
                            reportServiceBrakeOn.Direction = "Service Brake On-" & intServiceParkingOn.ToString()
                            reportServiceBrakeOn.Value = intServiceParkingOn 'On
                            blnBrakeOn = True
                            blnBrakeAddedOn = True
                        End If
                    End If
                    If brakeList(d).Value = "Parking Brake OFF" Then
                        intServiceParkingOff = intServiceParkingOff + 1
                        strBrakeOff = brakeList(d + 1).Value
                        If blnBrakeOn Then
                            dtLogger.Add(reportServiceBrakeOn)
                            intServiceParkingOn = 0
                            blnBrakeOn = False
                            blnBrakeAddedOn = False
                            reportServiceBrakeOn = New ReportFields
                        End If
                        If strBrakeOff = "Parking Brake ON" Then
                            reportServiceBrakeOff = New ReportFields
                            reportServiceBrakeOff.Direction = "Service Brake Off-" & intServiceParkingOff.ToString()
                            reportServiceBrakeOff.Value = intServiceParkingOff  'Off
                            blnBrakeOff = True
                            blnBrakeAddedOff = True
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
            If Not blnBrakeAddedOn And intServiceParkingOn > 0 Then
                reportServiceBrakeOn = New ReportFields
                reportServiceBrakeOn.Description = "Service Brake On Off"
                reportServiceBrakeOn.Direction = "Service Brake On-" & intServiceParkingOn.ToString()
                reportServiceBrakeOn.Value = intServiceParkingOn 'On
                dtLogger.Add(reportServiceBrakeOn)
            End If
            If Not blnBrakeAddedOff And intServiceParkingOff > 0 Then
                reportServiceBrakeOff = New ReportFields
                reportServiceBrakeOff.Description = "Service Brake On Off"
                reportServiceBrakeOff.Direction = "Service Brake Off-" & intServiceParkingOff.ToString()
                reportServiceBrakeOff.Value = intServiceParkingOff  'Off
                dtLogger.Add(reportServiceBrakeOff)
            End If
        End Sub
        Private Shared Sub GetParkingBrake(ByRef dtLogger As List(Of ReportFields), deviceid As String, startDate As Date, endDate As Date)
            Dim blnBrakeAddedOn1 As Boolean = False
            Dim blnBrakeAddedOff1 As Boolean = False
            Dim blnBrakeOn1 As Boolean
            Dim blnBrakeOff1 As Boolean
            Dim strBrakeOn1 As String
            Dim strBrakeOff1 As String
            Dim intServiceParkingOn1 As Integer
            Dim intServiceParkingOff1 As Integer
            Dim param11 As New DataLoggerReport
            param11.DeviceId = deviceid
            param11.Standard = "Zagro125"
            param11.Spn = 2
            param11.StartDate = startDate
            param11.EndDate = endDate
            Dim brakeList1 = GetDataLogger(param11) 'pgn:578, spn:2, parking brake
            Dim reportServiceBrakeOn1 As New ReportFields
            Dim reportServiceBrakeOff1 As New ReportFields

            For d As Integer = 0 To brakeList1.Count
                Try
                    reportServiceBrakeOn1.Description = "Park Brake On Off"
                    reportServiceBrakeOff1.Description = reportServiceBrakeOn1.Description
                    If brakeList1(d).Value = "Parking Brake ON" Then
                        intServiceParkingOn1 = intServiceParkingOn1 + 1
                        strBrakeOn1 = brakeList1(d + 1).Value
                        If blnBrakeOff1 Then
                            dtLogger.Add(reportServiceBrakeOff1)
                            intServiceParkingOff1 = 0
                            blnBrakeOff1 = False
                            blnBrakeAddedOff1 = False
                            reportServiceBrakeOff1 = New ReportFields
                        End If
                        If strBrakeOn1 = "Parking Brake OFF" Then
                            reportServiceBrakeOn1 = New ReportFields
                            reportServiceBrakeOn1.Direction = "Park Brake On-" & intServiceParkingOn1.ToString()
                            reportServiceBrakeOn1.Value = intServiceParkingOn1 'On
                            blnBrakeOn1 = True
                            blnBrakeAddedOn1 = True
                        End If
                    End If
                    If brakeList1(d).Value = "Parking Brake OFF" Then
                        intServiceParkingOff1 = intServiceParkingOff1 + 1
                        strBrakeOff1 = brakeList1(d + 1).Value
                        If blnBrakeOn1 Then
                            dtLogger.Add(reportServiceBrakeOn1)
                            intServiceParkingOn1 = 0
                            blnBrakeOn1 = False
                            blnBrakeAddedOn1 = False
                            reportServiceBrakeOn1 = New ReportFields
                        End If
                        If strBrakeOff1 = "Parking Brake ON" Then
                            reportServiceBrakeOff1 = New ReportFields
                            reportServiceBrakeOff1.Direction = "Park Brake Off-" & intServiceParkingOff1.ToString()
                            reportServiceBrakeOff1.Value = intServiceParkingOff1  'Off
                            blnBrakeOff1 = True
                            blnBrakeAddedOff1 = True
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
            If Not blnBrakeAddedOn1 And intServiceParkingOn1 > 0 Then
                reportServiceBrakeOn1 = New ReportFields
                reportServiceBrakeOn1.Description = "Park Brake On Off"
                reportServiceBrakeOn1.Direction = "Park Brake On-" & intServiceParkingOn1.ToString()
                reportServiceBrakeOn1.Value = intServiceParkingOn1 'On
                dtLogger.Add(reportServiceBrakeOn1)
            End If
            If Not blnBrakeAddedOff1 And intServiceParkingOff1 > 0 Then
                reportServiceBrakeOff1 = New ReportFields
                reportServiceBrakeOff1.Description = "Park Brake On Off"
                reportServiceBrakeOff1.Direction = "Park Brake Off-" & intServiceParkingOff1.ToString()
                reportServiceBrakeOff1.Value = intServiceParkingOff1  'Off
                dtLogger.Add(reportServiceBrakeOff1)
            End If
        End Sub
        Private Shared Sub GetHorn(ByRef dtLogger As List(Of ReportFields), deviceid As String, startDate As Date, endDate As Date)
            Dim blnHornAddedOn As Boolean = False
            Dim blnHornAddedOff As Boolean = False
            Dim blnHornOn As Boolean
            Dim blnHornOff As Boolean
            Dim strHornOn As String
            Dim strHornOff As String
            Dim intHornOn As Integer
            Dim intHornOff As Integer
            Dim param4 As New DataLoggerReport
            param4.DeviceId = deviceid
            param4.Standard = "Zagro125"
            param4.Spn = 5
            param4.StartDate = startDate
            param4.EndDate = endDate
            Dim hornList = GetDataLogger(param4) 'pgn:645, spn:5, horn
            Dim reportHornOn As New ReportFields
            Dim reportHornOff As New ReportFields
            For d As Integer = 0 To hornList.Count
                Try
                    reportHornOn.Description = "Horn Activation"
                    reportHornOff.Description = reportHornOn.Description
                    If hornList(d).Value = "Horn ON" Then
                        intHornOn = intHornOn + 1
                        strHornOn = hornList(d + 1).Value
                        If blnHornOff Then
                            dtLogger.Add(reportHornOff)
                            intHornOff = 0
                            blnHornOff = False
                            blnHornAddedOff = False
                            reportHornOff = New ReportFields
                        End If
                        If strHornOn = "Horn OFF" Then
                            reportHornOn = New ReportFields
                            reportHornOn.Direction = "Horn On-" & intHornOn.ToString()
                            reportHornOn.Value = intHornOn 'On
                            blnHornOn = True
                            blnHornAddedOn = True
                        End If
                    End If
                    If hornList(d).Value = "Horn OFF" Then
                        intHornOff = intHornOff + 1
                        strHornOff = hornList(d + 1).Value
                        If blnHornOn Then
                            dtLogger.Add(reportHornOn)
                            intHornOn = 0
                            blnHornOn = False
                            blnHornAddedOn = False
                            reportHornOn = New ReportFields
                        End If
                        If strHornOff = "Horn ON" Then
                            reportHornOff = New ReportFields
                            reportHornOff.Direction = "Horn Off-" & intHornOff.ToString()
                            reportHornOff.Value = intHornOff  'Off
                            blnHornOff = True
                            blnHornAddedOff = True
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
            If Not blnHornAddedOn And intHornOn > 0 Then
                reportHornOn = New ReportFields
                reportHornOn.Description = "Horn Activation"
                reportHornOn.Direction = "Horn On-" & intHornOn.ToString()
                reportHornOn.Value = intHornOn 'On
                dtLogger.Add(reportHornOn)
            End If
            If Not blnHornAddedOff And intHornOff > 0 Then
                reportHornOff = New ReportFields
                reportHornOff.Description = "Horn Activation"
                reportHornOff.Direction = "Horn Off-" & intHornOff.ToString()
                reportHornOff.Value = intHornOff  'Off
                dtLogger.Add(reportHornOff)
            End If
        End Sub
        Private Shared Sub GetVigilanceTimeOut(ByRef dtLogger As List(Of ReportFields), deviceid As String, startDate As Date, endDate As Date)
            Dim blnVigilanceAddedOn As Boolean = False
            Dim blnVigilanceAddedOff As Boolean = False
            Dim blnVigilanceOn As Boolean
            Dim blnVigilanceOff As Boolean
            Dim strVigilanceOn As String
            Dim strVigilanceOff As String
            Dim intVigilanceOn As Integer
            Dim intVigilanceOff As Integer
            Dim param5 As New DataLoggerReport
            param5.DeviceId = deviceid
            param5.Standard = "Zagro125"
            param5.Spn = 3
            param5.StartDate = startDate
            param5.EndDate = endDate
            Dim vigilanceTimeOut = GetDataLogger(param5) 'pgn:578, spn:3, Beacon Operation
            Dim reportVigilanceOn As New ReportFields
            Dim reportVigilanceOff As New ReportFields
            For d As Integer = 0 To vigilanceTimeOut.Count
                Try
                    reportVigilanceOn.Description = "Vigilance Time Out"
                    reportVigilanceOff.Description = reportVigilanceOn.Description
                    If vigilanceTimeOut(d).Value = "Beacon is ON" Then
                        intVigilanceOn = intVigilanceOn + 1
                        strVigilanceOn = vigilanceTimeOut(d + 1).Value
                        If blnVigilanceOff Then
                            dtLogger.Add(reportVigilanceOff)
                            intVigilanceOff = 0
                            blnVigilanceOff = False
                            blnVigilanceAddedOff = False
                            reportVigilanceOff = New ReportFields
                        End If
                        If strVigilanceOn = "Beacon is OFF" Then
                            reportVigilanceOn = New ReportFields
                            reportVigilanceOn.Direction = "Beacon is On-" & intVigilanceOn.ToString()
                            reportVigilanceOn.Value = intVigilanceOn 'On
                            blnVigilanceOn = True
                            blnVigilanceAddedOn = True
                        End If
                    End If
                    If vigilanceTimeOut(d).Value = "Beacon is OFF" Then
                        intVigilanceOff = intVigilanceOff + 1
                        strVigilanceOff = vigilanceTimeOut(d + 1).Value
                        If blnVigilanceOn Then
                            dtLogger.Add(reportVigilanceOn)
                            intVigilanceOn = 0
                            blnVigilanceOn = False
                            blnVigilanceAddedOn = False
                            reportVigilanceOn = New ReportFields
                        End If
                        If strVigilanceOff = "Beacon is ON" Then
                            reportVigilanceOff = New ReportFields
                            reportVigilanceOff.Direction = "Beacon is Off-" & intVigilanceOff.ToString()
                            reportVigilanceOff.Value = intVigilanceOff  'Off
                            blnVigilanceOff = True
                            blnVigilanceAddedOff = True
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
            If Not blnVigilanceAddedOn And intVigilanceOn > 0 Then
                reportVigilanceOn = New ReportFields
                reportVigilanceOn.Description = "Vigilance Time Out"
                reportVigilanceOn.Direction = "Beacon is On-" & intVigilanceOn.ToString()
                reportVigilanceOn.Value = intVigilanceOn 'On
                dtLogger.Add(reportVigilanceOn)
            End If
            If Not blnVigilanceAddedOff And intVigilanceOff > 0 Then
                reportVigilanceOff = New ReportFields
                reportVigilanceOff.Description = "Vigilance Time Out"
                reportVigilanceOff.Direction = "Beacon is Off-" & intVigilanceOff.ToString()
                reportVigilanceOff.Value = intVigilanceOff  'Off
                dtLogger.Add(reportVigilanceOff)
            End If
        End Sub
        Private Shared Sub GetRoadRailEngaged(ByRef dtLogger As List(Of ReportFields), deviceid As String, startDate As Date, endDate As Date)
            Dim blnAddedOn As Boolean = False
            Dim blnAddedOff As Boolean = False
            Dim blnRoadRailOn As Boolean
            Dim blnRoadRailOff As Boolean
            Dim intRoadRailValueOn1 As Integer
            Dim intRoadRailValueOn2 As Integer
            Dim intRoadRailValueOff1 As Integer
            Dim intRoadRailValueOff2 As Integer
            Dim intRoadRailOn As Integer
            Dim intRoadRailOff As Integer
            Dim param2 As New DataLoggerReport
            param2.DeviceId = deviceid
            param2.Standard = "Zagro125"
            param2.Spn = 8
            param2.StartDate = startDate
            param2.EndDate = endDate
            Dim param3 As New DataLoggerReport
            param3.DeviceId = deviceid
            param3.Standard = "Zagro125"
            param3.Spn = 9
            param3.StartDate = startDate
            param3.EndDate = endDate
            Dim pressureValues1 = GetDataLogger(param2) 'pgn:646, spn:8, (PressureValues1)Road Rail Engaged
            Dim pressureValues2 = GetDataLogger(param3) 'pgn:646, spn:9, (PressureValues2)Road Rail Engaged
            Dim reportRoadRailOn As New ReportFields
            Dim reportRoadRailOff As New ReportFields
            For d As Integer = 0 To pressureValues1.Count
                Try
                    reportRoadRailOn.Description = "Road Rail Engaged"
                    reportRoadRailOff.Description = reportRoadRailOn.Description
                    If pressureValues1(d).Value >= 60 And pressureValues2(d).Value >= 60 Then
                        intRoadRailOn = intRoadRailOn + 1
                        intRoadRailValueOn1 = pressureValues1(d + 1).Value
                        intRoadRailValueOn2 = pressureValues2(d + 1).Value
                        If blnRoadRailOff Then
                            dtLogger.Add(reportRoadRailOff)
                            intRoadRailOff = 0
                            blnRoadRailOff = False
                            blnAddedOff = False
                            reportRoadRailOff = New ReportFields
                        End If
                        If intRoadRailValueOn1 < 60 And intRoadRailValueOn2 < 60 Then
                            reportRoadRailOn = New ReportFields
                            reportRoadRailOn.Direction = "Road Rail Engaged On-" & intRoadRailOn.ToString()
                            reportRoadRailOn.Value = intRoadRailOn 'On
                            blnRoadRailOn = True
                            blnAddedOn = True
                        End If
                    Else
                        intRoadRailOff = intRoadRailOff + 1
                        intRoadRailValueOff1 = pressureValues1(d + 1).Value
                        intRoadRailValueOff2 = pressureValues2(d + 1).Value
                        If blnRoadRailOn Then
                            dtLogger.Add(reportRoadRailOn)
                            intRoadRailOn = 0
                            blnRoadRailOn = False
                            blnAddedOn = False
                            reportRoadRailOn = New ReportFields
                        End If
                        If intRoadRailValueOff1 >= 60 And intRoadRailValueOff2 >= 60 Then
                            reportRoadRailOff = New ReportFields
                            reportRoadRailOff.Direction = "Road Rail Engaged Off-" & intRoadRailOff.ToString()
                            reportRoadRailOff.Value = intRoadRailOff  'Off
                            blnRoadRailOff = True
                            blnAddedOff = True
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next
            If Not blnAddedOn And intRoadRailOn > 0 Then
                reportRoadRailOn = New ReportFields
                reportRoadRailOn.Description = "Road Rail Engaged"
                reportRoadRailOn.Direction = "Road Rail Engaged On-" & intRoadRailOn.ToString()
                reportRoadRailOn.Value = intRoadRailOn 'On
                dtLogger.Add(reportRoadRailOn)
            End If
            If Not blnAddedOff And intRoadRailOff > 0 Then
                reportRoadRailOff = New ReportFields
                reportRoadRailOff.Description = "Road Rail Engaged"
                reportRoadRailOff.Direction = "Road Rail Engaged Off-" & intRoadRailOff.ToString()
                reportRoadRailOff.Value = intRoadRailOff  'Off
                dtLogger.Add(reportRoadRailOff)
            End If
        End Sub
    End Class
    Public Class DataLogger7502
        Public Property Id As Integer
        Public Property Speed As Double
        Public Property Direction As ReportFields
        Public Property ServiceBrake As String
        Public Property ParkBrake As String
        Public Property Horn As String
        Public Property VigilanceTimeOut As String
        Public Property HeadLight As String
        Public Property RoadRailEngaged As String
    End Class
    Public Class DataLogger7502Report
        Public Property Direction As List(Of ReportFields)
        'Public Property ServiceBrake As String
    End Class

    Public Class ReportFields
        Public Property Description As String
        Public Property Direction As String
        Public Property Value As Integer
    End Class
    Public Class SpeedReportFields
        Public Property Description As String
        Public Property ValueX As Integer
        Public Property DateX As DateTime
        Public Property DateRawX As DateTime
    End Class
    Public Class SpeedFields
        Public Property Description As String
        Public Property Value As Integer
        Public Property SpeedDateTime As DateTime
    End Class
    Public Class DevicePositionLatLong
        Public Property DeviceName As String
        Public Property Latitude As Decimal
        Public Property Longitude As Decimal
        Public Property LatitudeDateTime As DateTime
        Public Property LongtitudeDateTime As DateTime
    End Class
End Namespace

