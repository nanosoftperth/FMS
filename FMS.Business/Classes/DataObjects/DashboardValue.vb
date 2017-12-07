Namespace DataObjects
    Public Class DashboardValue

#Region "property definitions & constructors"

        Public Property Parking_Break As String = ""
        Public Property Steering As String = ""
        Public Property Driving As String = ""
        Public Property IFMControl As String = ""
        Public Property CANControl As String = ""
        Public Property CANOPENControl As String = ""
        Public Property AlignmentControl As String = ""
        Public Property WarningControl As String = ""
        Public Property StopControl As String = ""
        Public Property SpeedControl As String = ""
        Public Property Battery_Voltage As String = ""
        Public Property LCD_Speed As String = ""
        Public Property LCD_Driving_Mode As String = ""
        Public Property LCD_DataLogger As String = ""
        Public Property LCD_Safety As String = ""
        Public Property LCD_DriveM1 As String = ""
        Public Property LCD_DriveM2 As String = ""
        Public Property LCD_DriveM3 As String = ""
        Public Property LCD_DriveM4 As String = ""
        Public Property LCD_IO As String = ""
        Public Property LCD_Hour As String = ""
        Public Property LCD_ErrorCategory As List(Of clsErrCategory)
        Public Property LCD_WithFaultCode As String = ""

        Private priParking As String
        Private priSteering As String
        Private priDriving As String
        Private priIFM As String
        Private priCAN As String
        Private priAlignment As String
        Private priWarning As String
        Private priStop As String
        Private priSpeed As String
        Private priBattery As String

        Public ReadOnly Property ParkingBreak As String
            Get
                Return priParking
            End Get
        End Property
        Public ReadOnly Property Steer As String
            Get
                Return priSteering
            End Get
        End Property
        Public ReadOnly Property Drive As String
            Get
                Return priDriving
            End Get
        End Property
        Public ReadOnly Property IFM As String
            Get
                Return priIFM
            End Get
        End Property
        Public ReadOnly Property CAN As String
            Get
                Return priCAN
            End Get
        End Property
        Public ReadOnly Property Alignment As String
            Get
                Return priAlignment
            End Get
        End Property
        Public ReadOnly Property Warning As String
            Get
                Return priWarning
            End Get
        End Property
        Public ReadOnly Property Stop_Control As String
            Get
                Return priStop
            End Get
        End Property
        Public ReadOnly Property Speed_Control As String
            Get
                Return priSpeed
            End Get
        End Property
        Public ReadOnly Property BatteryVoltage As String
            Get
                Return priBattery
            End Get
        End Property

        Public Class clsErrCategory
            Public Err_Category As String
            Public Err_Value As String
        End Class


        Public Sub New()

        End Sub

#End Region

        Public Shared Function GetDashboardData(deviceID As String) As Boolean
            Dim oList As List(Of DashboardValue) = New List(Of DashboardValue)
            Dim oListErrCat As New List(Of DashboardValue.clsErrCategory)()

            Try
                Dim oDash As New DashboardValue

                Dim ListRow = New DashboardValue

                ListRow.LCD_Hour = oDash.GetLCDHour(deviceID)

                oList.Add(ListRow)

            Catch ex As Exception
                Throw ex
            End Try


            Return True
        End Function

#Region "Non Static Method(s)"

        Public Function GetLCDHour(deviceID As String) As String
            Dim hrctrSPN = 13

            'Dim vehicle = FMS.Business.DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID).GetAvailableCANTagsValueForDash()
            'Dim strStandard = vehicle.CAN_Protocol_Type '----> will enable once standard is change on DB's applicationvehicle table
            Dim strStandard = "Zagro500"

            Dim cdp As CanDataPoint = FMS.Business.DataObjects.CanDataPoint.GetPointWithLatestDataByDeviceId(hrctrSPN, deviceID, strStandard)

            Try

                If IsNothing(cdp.CanValues) = False Then
                    Dim olist As List(Of DataObjects.CanValue) = cdp.CanValues

                    '---- use only to test for value while on breakpoint
                    Dim oTime = olist(0).Time
                    Dim oRawValue = olist(0).RawValue
                    Dim oValue = olist(0).Value
                    '---- end of test

                    Return olist(0).Value
                Else
                    Return ""
                End If

            Catch ex As Exception
                Return ""
            End Try


        End Function

        Public Function GetLEDValue(deviceID As String) As String
            Dim hrctrSPN = 14

            'Dim vehicle = FMS.Business.DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID).GetAvailableCANTagsValueForDash()
            'Dim strStandard = vehicle.CAN_Protocol_Type '----> will enable once standard is change on DB's applicationvehicle table
            Dim strStandard = "Zagro500"

            Dim cdp As CanDataPoint = FMS.Business.DataObjects.CanDataPoint.GetPointWithLatestDataByDeviceId(hrctrSPN, deviceID, strStandard)

            Try

                If IsNothing(cdp.CanValues) = False Then
                    Dim olist As List(Of DataObjects.CanValue) = cdp.CanValues

                    '---- use only to test for value while on breakpoint
                    Dim oTime = olist(0).Time
                    Dim oRawValue = olist(0).RawValue
                    Dim oValue = olist(0).Value
                    '---- end of test

                    Return olist(0).Value
                Else
                    Return ""
                End If

            Catch ex As Exception
                Return ""
            End Try


        End Function

        Public Function GetStopValue(deviceID As String) As String
            Dim hrctrSPN = 15

            'Dim vehicle = FMS.Business.DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID).GetAvailableCANTagsValueForDash()
            'Dim strStandard = vehicle.CAN_Protocol_Type '----> will enable once standard is change on DB's applicationvehicle table
            Dim strStandard = "Zagro500"

            Dim cdp As CanDataPoint = FMS.Business.DataObjects.CanDataPoint.GetPointWithLatestDataByDeviceId(hrctrSPN, deviceID, strStandard)

            Try

                If IsNothing(cdp.CanValues) = False Then
                    Dim olist As List(Of DataObjects.CanValue) = cdp.CanValues

                    '---- use only to test for value while on breakpoint
                    Dim oTime = olist(0).Time
                    Dim oRawValue = olist(0).RawValue
                    Dim oValue = olist(0).Value
                    '---- end of test

                    Return olist(0).Value
                Else
                    Return ""
                End If

            Catch ex As Exception
                Return ""
            End Try


        End Function

        'Public Function GetStandardList() As list(of CAN_MessageDefinition) as DataObjects.CAN_MessageDefinition 



#End Region

        Public Shared Function WithReturnValue(deviceID As String) As Integer
            Dim vehicle = DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID).GetAvailableCANTagsValue()

            If vehicle.Count > 0 Then
                Return vehicle.Count
            Else
                Return 0
            End If

        End Function

        Public Shared Function GetDataForDashboard(deviceID As String) As List(Of DashboardValue)
            Dim oList As List(Of DashboardValue) = New List(Of DashboardValue)
            Dim oListErrCat As New List(Of DashboardValue.clsErrCategory)()
            Dim oDash As New DashboardValue

            Dim vehicle = DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID).GetAvailableCANTagsValue()

            Dim arrSteer() As String = {"MS 1", "MS 11", "MS 12", "MS 14", "MS 21", "MS 22", "MS 23",
                                        "MS 31", "MS 32", "MS 34", "MS 41", "MS 42"}
            Dim arrDrive() As String = {"M1 1", "M1 2", "M1 4", "M2 1", "M2 2", "M2 4",
                                        "M3 1", "M3 2", "M3 4", "M4 1", "M4 2", "M4 4"}
            Dim arrSpeed() As String = {"M1 3", "M2 3", "M3 3", "M4 3", "MS 7", "MS 8", "MS 9", "MS 9", "IO 4"}
            Dim arrWarning() As String = {"M1 3", "M1 5", "M1 6", "M2 3", "M2 5", "M2 6",
                                        "M3 3", "M3 5", "M3 6", "M4 3", "M4 5", "M4 6",
                                        "MS 6", "MS 7", "MS 8", "MS 9",
                                        "IO 1", "IO 3", "IO 4", "IO 8", "IO 30", "IO 32", "IO 33", "IO 34",
                                        "IO 35", "IO 40", "IO 41", "IO 71", "S 4", "S 5",
                                        "Canopen 1", "Canopen 2", "Canopen 3", "Canopen 4", "Canopen 6",
                                        "Canopen 7", "Canopen 8"}
            Dim arrAlign() As String = {"IO 11", "IO 12", "IO 13", "IO 14", "IO 41"}
            Dim arrIFM() As String = {"M1 1", "M2 1", "M3 1", "M4 1"}
            Dim arrCAN() As String = {"Can 1", "Can 2", "Can 3", "Can 4", "Can 5", "Can 6", "Can 7", "Can 8",
                                    "Can 31", "Can 101", "Can 102", "Can 103", "Can 104", "Can 105", "Can 106",
                                    "Can 107", "Can 108", "Can 131", "Can 205", "Can 206", "Can 207", "Can 208"}
            Dim arrCANOPEN() As String = {"Canopen 5"}
            Dim arrDataLogger() As String = {"IO 30"}
            Dim arrSafety() As String = {"S 1", "S 2", "S 3", "S 4", "S 5", "S 10"}
            Dim arrDrive_M1() As String = {"M1 1", "M1 2", "M1 3", "M1 4", "M1 5", "M1 6"}
            Dim arrDrive_M2() As String = {"M2 1", "M2 2", "M2 3", "M2 4", "M2 5", "M2 6"}
            Dim arrDrive_M3() As String = {"M3 1", "M3 2", "M3 3", "M3 4", "M3 5", "M3 6"}
            Dim arrDrive_M4() As String = {"M4 1", "M4 2", "M4 3", "M4 4", "M4 5", "M4 6"}
            Dim arrIO() As String = {"IO 1", "IO 2", "IO 3", "IO 4", "IO 8", "IO 11",
                                    "IO 12", "IO 13", "IO 14", "IO 20", "IO 30", "IO 32",
                                    "IO 33", "IO 34", "IO 35", "IO 40", "IO 41", "IO 71"}
            Dim arrAC() As String = {"ACE2 A1", "ACE2 A2", "ACE2 A3", "ACE2 A4",
                                    "EPS A1", "EPS A2", "EPS A3", "EPS A4", "for Testing"}
            

            If vehicle.Count > 0 Then
                Dim ListRow = New DashboardValue
                Dim rowErrCat = New DashboardValue.clsErrCategory

                Dim mainLoopCtr = vehicle.Count
                Dim strDesc As String = ""
                Dim strValue As String = ""

                For ndx As Integer = 0 To mainLoopCtr - 1
                    strDesc = vehicle(ndx).MessageDefinition.Description

                    If vehicle(ndx).CanValues.Count = 0 Then
                        If (strDesc = "Fault Codes") Then
                            strValue = "None"
                            ListRow.LCD_WithFaultCode = strValue
                        End If

                        Continue For

                    Else
                        If (strDesc <> "AC Inverter") Then
                            strValue = vehicle(ndx).CanValues(0).Value
                        End If

                    End If

                    Select Case strDesc

                        Case "Stop"
                            Dim StpValue = oDash.GetStopValue(deviceID)

                            If StpValue = "1" Then
                                ListRow.StopControl = "On"
                            Else
                                ListRow.StopControl = "Off"
                            End If

                        Case "LED"
                            'B6.2   - Stop
                            'B7.0   - Parking brake
                            'B7.1   - Steering
                            'B7.2   - Drive unit
                            'B7.3   - Ifm controller
                            'B7.4   - Can bus
                            'B7.5   - Service
                            'B7.6   – Warning
                            'B7.7   - Speedlimiter

                            Dim LEDValue = oDash.GetLEDValue(deviceID)
                            Dim pos = 7
                            Dim bPos As Integer = 0


                            For count = 0 To LEDValue.Length - 1

                                'LED	0000 01 01
                                Dim bval = LEDValue.Substring(pos, 1)

                                If count = 0 And bval = "1" Then
                                    ListRow.Parking_Break = "Parking Brake ON"
                                ElseIf count = 0 And bval = "0" Then
                                    ListRow.Parking_Break = "Parking Brake OFF"
                                End If

                                If count = 1 And bval = "1" Then
                                    ListRow.Steering = "Err"
                                End If

                                If count = 2 And bval = "1" Then
                                    ListRow.Driving = "Err"
                                End If

                                If count = 3 And bval = "1" Then
                                    ListRow.IFMControl = "Err"
                                End If

                                If count = 4 And bval = "1" Then
                                    ListRow.CANControl = "Err"
                                End If

                                If count = 5 And bval = "1" Then
                                    ListRow.AlignmentControl = "Err"
                                End If

                                If count = 6 And bval = "1" Then
                                    ListRow.WarningControl = "Err"
                                End If

                                If count = 7 And bval = "1" Then
                                    ListRow.SpeedControl = "Err"
                                End If

                                pos = pos - 1

                            Next

                        Case "Battery Voltage"
                            If Not strValue = Nothing Then
                                ListRow.Battery_Voltage = strValue
                            Else
                                If ListRow.Battery_Voltage.Length = 0 Then
                                    ListRow.Battery_Voltage = "0"
                                End If
                            End If

                        Case "Speed"
                            ListRow.LCD_Speed = strValue

                        Case "Driving Mode"
                            ListRow.LCD_Driving_Mode = strValue
                        Case "Hour Counter"
                            'ListRow.LCD_Hour = strValue
                            ListRow.LCD_Hour = oDash.GetLCDHour(deviceID)

                        Case "Fault Codes"
                            If strValue = Nothing Then
                                Continue For
                            End If

                            Dim fc As String() = Nothing
                            fc = strValue.Split(",")
                            Dim sfc As String
                            Dim strVal As String

                            For count = 0 To fc.Length - 1
                                sfc = fc(count)

                                Dim valErrCat As String = ""
                                Dim valErrCode As String = ""
                                Dim chrpos = sfc.IndexOf(" ")

                                If chrpos > -1 Then
                                    Dim sLen = sfc.Length - chrpos
                                    strVal = sfc.Substring(chrpos, sLen).Trim()

                                    Dim strSteer As String = Array.Find(arrSteer, Function(x) (x.StartsWith(sfc)))
                                    Dim strDrive As String = Array.Find(arrDrive, Function(x) (x.StartsWith(sfc)))
                                    Dim strSpeed As String = Array.Find(arrSpeed, Function(x) (x.StartsWith(sfc)))
                                    Dim strWarning As String = Array.Find(arrWarning, Function(x) (x.StartsWith(sfc)))
                                    Dim strAlign As String = Array.Find(arrAlign, Function(x) (x.StartsWith(sfc)))
                                    Dim strIFM As String = Array.Find(arrIFM, Function(x) (x.StartsWith(sfc)))
                                    Dim strCAN As String = Array.Find(arrCAN, Function(x) (x.StartsWith(sfc)))
                                    Dim strCANOPEN As String = Array.Find(arrCANOPEN, Function(x) (x.StartsWith(sfc)))
                                    Dim strDataLogger As String = Array.Find(arrDataLogger, Function(x) (x.StartsWith(sfc)))
                                    Dim strSafety As String = Array.Find(arrSafety, Function(x) (x.StartsWith(sfc)))
                                    Dim strDrvM1 As String = Array.Find(arrDrive_M1, Function(x) (x.StartsWith(sfc)))
                                    Dim strDrvM2 As String = Array.Find(arrDrive_M2, Function(x) (x.StartsWith(sfc)))
                                    Dim strDrvM3 As String = Array.Find(arrDrive_M3, Function(x) (x.StartsWith(sfc)))
                                    Dim strDrvM4 As String = Array.Find(arrDrive_M4, Function(x) (x.StartsWith(sfc)))
                                    Dim strIO As String = Array.Find(arrIO, Function(x) (x.StartsWith(sfc)))
                                    Dim strAC As String = Array.Find(arrAC, Function(x) (x.StartsWith(sfc)))

                                    If Not strSteer = Nothing Then
                                        valErrCat = "Steering"
                                        valErrCode = strVal
                                    End If

                                    If Not strSafety = Nothing Then
                                        valErrCat = "Safety"
                                        valErrCode = strVal
                                    End If

                                    If Not strDrvM1 = Nothing Then
                                        valErrCat = "DriveM1"
                                        valErrCode = strVal
                                    End If

                                    If Not strDrvM2 = Nothing Then
                                        valErrCat = "DriveM2"
                                        valErrCode = strVal
                                    End If

                                    If Not strDrvM3 = Nothing Then
                                        valErrCat = "DriveM3"
                                        valErrCode = strVal
                                    End If

                                    If Not strDrvM4 = Nothing Then
                                        valErrCat = "DriveM4"
                                        valErrCode = strVal
                                    End If

                                    If Not strCAN = Nothing Then
                                        valErrCat = "CAN"
                                        valErrCode = strVal
                                        ListRow.CANControl = strValue
                                    End If

                                    If Not strCANOPEN = Nothing Then
                                        valErrCat = "CANOPEN"
                                        valErrCode = strVal
                                        ListRow.CANControl = strValue
                                    End If

                                    If Not strIO = Nothing Then
                                        valErrCat = "InOut"
                                        valErrCode = strVal
                                    End If

                                    oListErrCat.Add(New DashboardValue.clsErrCategory() With { _
                                                         .Err_Category = valErrCat, _
                                                         .Err_Value = valErrCode _
                                                    })

                                End If


                            Next

                            ListRow.LCD_ErrorCategory = oListErrCat

                    End Select

                Next

                oList.Add(ListRow)
            Else
                oList = New List(Of DashboardValue)
            End If

            Return oList

        End Function

        Public Class DashCANMsgDef
            Public Property ID As String
            Public Property Standard As String

        End Class
    End Class

End Namespace

