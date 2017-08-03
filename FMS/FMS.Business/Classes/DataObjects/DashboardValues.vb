Imports System.Web

Namespace DataObjects


    Public Class DashboardValues

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

        Public Shared Function WithReturnValue(deviceID As String) As Integer
            Dim vehicle = DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID).GetAvailableCANTagsValue()

            If vehicle.Count > 0 Then
                Return vehicle.Count
            Else
                Return 0
            End If

        End Function

        Public Shared Function GetDataForDashboardSimulator() As List(Of DashboardValues)
            Dim oList As List(Of DashboardValues) = New List(Of DashboardValues)



        End Function
        Public Shared Function GetDataForDashboard(deviceID As String) As List(Of DashboardValues)
            Dim oList As List(Of DashboardValues) = New List(Of DashboardValues)

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


            If vehicle.Count > 0 Then
                Dim ListRow = New DashboardValues
                Dim mainLoopCtr = vehicle.Count
                Dim strDesc, strValue As String

                For ndx As Integer = 0 To mainLoopCtr - 1
                    strDesc = vehicle(ndx).MessageDefinition.Description

                    If vehicle(ndx).CanValues.Count = 0 Then
                        If (strDesc = "Fault Codes") Then
                            strValue = "None"
                            ListRow.LCD_WithFaultCode = strValue
                        End If

                        Continue For

                    Else
                        strValue = vehicle(ndx).CanValues(0).Value
                    End If

                    Select Case strDesc
                        Case "Parking Break"
                            ListRow.Parking_Break = strValue

                            If strValue = "Parking Break ON" Then
                                ListRow.StopControl = "ON"
                            Else
                                If strValue = "Parking Break OFF" Then
                                    ListRow.StopControl = "OFF"
                                End If
                            End If

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

                        Case "Fault Codes"
                            Dim fc As String() = Nothing
                            fc = strValue.Split(",")
                            'Dim sfc As String

                    End Select

                Next
                oList.Add(ListRow)
            Else
                oList = New List(Of DashboardValues)
            End If

            Return oList

        End Function

        'Public Shared Function GetDataForDashboard(deviceID As String) As List(Of DashboardValues)

        '    'Dim oList As List(Of DashboardValues) = New List(Of DashboardValues)

        '    'Dim vehicle = DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID).GetAvailableCANTagsValue()
        '    ''Dim vehicle = FMS.Business.DataObjects.

        '    'If vehicle.Count > 0 Then
        '    '    Dim ListRow = New DashboardValues
        '    '    Dim mainLoopCtr = vehicle.Count
        '    '    Dim strDesc, strValue As String

        '    '    For ndx As Integer = 0 To mainLoopCtr - 1
        '    '        strDesc = vehicle(ndx).MessageDefinition.Description

        '    '        If vehicle(ndx).CanValues.Count = 0 Then
        '    '            Continue For
        '    '        Else
        '    '            strValue = vehicle(ndx).CanValues(0).Value
        '    '        End If

        '    '        Select Case strDesc
        '    '            Case "Parking Break"
        '    '                ListRow.Parking_Break = strValue

        '    '                If strValue = "Parking Break ON" Then
        '    '                    ListRow.StopControl = "ON"
        '    '                Else
        '    '                    If strValue = "Parking Break OFF" Then
        '    '                        ListRow.StopControl = "OFF"
        '    '                    End If
        '    '                End If

        '    '            Case "Battery Voltage"
        '    '                If Not strValue = Nothing Then
        '    '                    ListRow.Battery_Voltage = strValue
        '    '                Else
        '    '                    If ListRow.Battery_Voltage.Length = 0 Then
        '    '                        ListRow.Battery_Voltage = "0"
        '    '                    End If
        '    '                End If

        '    '            Case "Speed"
        '    '                ListRow.LCD_Speed = strValue

        '    '            Case "Driving Mode"
        '    '                ListRow.LCD_Driving_Mode = strValue

        '    '        End Select

        '    '    Next

        '    'Else
        '    '    oList = New List(Of DashboardValues)
        '    'End If

        '    'Return oList
        '    ' -----------------------

        '    'Dim oList As List(Of DashboardValues) = New List(Of DashboardValues)
        '    ''Dim vehicle As DataObjects.ApplicationVehicle = DataObjects.ApplicationVehicle.GetFromDeviceID(vehicleID)

        '    ''FMS.Business.DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID).GetAvailableCANTagsValue()

        '    'Dim vehicle = DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID).GetAvailableCANTagsValue()

        '    'Try
        '    '    If vehicle.Count > 0 Then

        '    '        'If Not vehicle(0).CanValues(0).Value = Nothing Then

        '    '        'MsgBox(vehicle(0).CanValues(0).Value.ToString())

        '    '        Dim strDesc, strValue As String
        '    '        Dim arrSteer() As String = {"MS 1", "MS 11", "MS 12", "MS 14", "MS 21", "MS 22", "MS 23",
        '    '                                    "MS 31", "MS 32", "MS 34", "MS 41", "MS 42"}
        '    '        Dim arrDrive() As String = {"M1 1", "M1 2", "M1 4", "M2 1", "M2 2", "M2 4",
        '    '                                    "M3 1", "M3 2", "M3 4", "M4 1", "M4 2", "M4 4"}
        '    '        Dim arrSpeed() As String = {"M1 3", "M2 3", "M3 3", "M4 3", "MS 7", "MS 8", "MS 9", "MS 9", "IO 4"}
        '    '        Dim arrWarning() As String = {"M1 3", "M1 5", "M1 6", "M2 3", "M2 5", "M2 6",
        '    '                                    "M3 3", "M3 5", "M3 6", "M4 3", "M4 5", "M4 6",
        '    '                                    "MS 6", "MS 7", "MS 8", "MS 9",
        '    '                                    "IO 1", "IO 3", "IO 4", "IO 8", "IO 30", "IO 32", "IO 33", "IO 34",
        '    '                                    "IO 35", "IO 40", "IO 41", "IO 71", "S 4", "S 5",
        '    '                                    "Canopen 1", "Canopen 2", "Canopen 3", "Canopen 4", "Canopen 6",
        '    '                                    "Canopen 7", "Canopen 8"}
        '    '        Dim arrAlign() As String = {"IO 11", "IO 12", "IO 13", "IO 14", "IO 41"}
        '    '        Dim arrIFM() As String = {"M1 1", "M2 1", "M3 1", "M4 1"}
        '    '        Dim arrCAN() As String = {"Can 1", "Can 2", "Can 3", "Can 4", "Can 5", "Can 6", "Can 7", "Can 8",
        '    '                                "Can 31", "Can 101", "Can 102", "Can 103", "Can 104", "Can 105", "Can 106",
        '    '                                "Can 107", "Can 108", "Can 131", "Can 205", "Can 206", "Can 207", "Can 208"}
        '    '        Dim arrCANOPEN() As String = {"Canopen 5"}
        '    '        Dim arrDataLogger() As String = {"IO 30"}
        '    '        Dim arrSafety() As String = {"S 1", "S 2", "S 3", "S 4", "S 5", "S 10"}
        '    '        Dim arrDrive_M1() As String = {"M1 1", "M1 2", "M1 3", "M1 4", "M1 5", "M1 6"}
        '    '        Dim arrDrive_M2() As String = {"M2 1", "M2 2", "M2 3", "M2 4", "M2 5", "M2 6"}
        '    '        Dim arrDrive_M3() As String = {"M3 1", "M3 2", "M3 3", "M3 4", "M3 5", "M3 6"}
        '    '        Dim arrDrive_M4() As String = {"M4 1", "M4 2", "M4 3", "M4 4", "M4 5", "M4 6"}
        '    '        Dim arrIO() As String = {"IO 1", "IO 2", "IO 3", "IO 4", "IO 8", "IO 11",
        '    '                                "IO 12", "IO 13", "IO 14", "IO 20", "IO 30", "IO 32",
        '    '                                "IO 33", "IO 34", "IO 35", "IO 40", "IO 41", "IO 71"}

        '    '        Dim ctr = vehicle.Count
        '    '        'Dim ctr = 9 ' ----- for testing. will remove/remarks before deploy

        '    '        Dim ListRow = New DashboardValues

        '    '        For ndx As Integer = 0 To ctr - 1
        '    '            ' ----- Will enable when deployed to demo/live
        '    '            strDesc = vehicle(ndx).MessageDefinition.Description

        '    '            If vehicle(ndx).CanValues.Count = 0 Then
        '    '                Continue For
        '    '            End If
        '    '            strValue = vehicle(ndx).CanValues(0).Value
        '    '            ' ----- End of disable

        '    '            ' ----- Test data. Will remove/remarks when deployed to demo/live
        '    '            'Select Case ndx
        '    '            '    Case 0
        '    '            '        strDesc = "Parking Break"
        '    '            '        strValue = "Parking Break ON"
        '    '            '    Case 1
        '    '            '        strDesc = "Battery Voltage"
        '    '            '        strValue = "80.54"
        '    '            '    Case 2
        '    '            '        strDesc = "Speed"
        '    '            '        strValue = "-9.01"
        '    '            '    Case 3
        '    '            '        strDesc = "Driving Mode"
        '    '            '        strValue = "Rail mode road"
        '    '            '    Case 8
        '    '            '        strDesc = "Fault Codes"
        '    '            '        'strValue = "S 1,MS 8,MS 34,MS 41,MS 42,M1 1,M1 4,M2 4,M3 2,M4 4,Canopen 3,Canopen 1,Canopen 5,Can 101,Can 203,IO 3,IO 20,IO 30,IO 40,IO 41"
        '    '            '        strValue = "IO 35"
        '    '            '    Case Else
        '    '            '        strDesc = ""
        '    '            '        strValue = ""
        '    '            'End Select
        '    '            ' ----- End of Test Data

        '    '            Select Case strDesc
        '    '                Case "Parking Break"
        '    '                    ListRow.Parking_Break = strValue

        '    '                    If strValue = "Parking Break ON" Then
        '    '                        ListRow.StopControl = "ON"
        '    '                    Else
        '    '                        If strValue = "Parking Break OFF" Then
        '    '                            ListRow.StopControl = "OFF"
        '    '                        End If
        '    '                    End If

        '    '                Case "Battery Voltage"
        '    '                    If Not strValue = Nothing Then
        '    '                        ListRow.Battery_Voltage = strValue
        '    '                    Else
        '    '                        If ListRow.Battery_Voltage.Length = 0 Then
        '    '                            ListRow.Battery_Voltage = "0"
        '    '                        End If
        '    '                    End If

        '    '                Case "Speed"
        '    '                    ListRow.LCD_Speed = strValue
        '    '                Case "Driving Mode"
        '    '                    ListRow.LCD_Driving_Mode = strValue
        '    '                Case "Fault Codes"
        '    '                    Dim fc As String() = Nothing
        '    '                    fc = strValue.Split(",")
        '    '                    Dim sfc As String

        '    '                    For Each sfc In fc
        '    '                        Dim chrpos = sfc.IndexOf(" ")
        '    '                        strValue = sfc.Substring(0, chrpos)

        '    '                        Dim strSteer As String = Array.Find(arrSteer, Function(x) (x.StartsWith(sfc)))
        '    '                        Dim strDrive As String = Array.Find(arrDrive, Function(x) (x.StartsWith(sfc)))
        '    '                        Dim strSpeed As String = Array.Find(arrSpeed, Function(x) (x.StartsWith(sfc)))
        '    '                        Dim strWarning As String = Array.Find(arrWarning, Function(x) (x.StartsWith(sfc)))
        '    '                        Dim strAlign As String = Array.Find(arrAlign, Function(x) (x.StartsWith(sfc)))
        '    '                        Dim strIFM As String = Array.Find(arrIFM, Function(x) (x.StartsWith(sfc)))
        '    '                        Dim strCAN As String = Array.Find(arrCAN, Function(x) (x.StartsWith(sfc)))
        '    '                        Dim strCANOPEN As String = Array.Find(arrCANOPEN, Function(x) (x.StartsWith(sfc)))
        '    '                        Dim strDataLogger As String = Array.Find(arrDataLogger, Function(x) (x.StartsWith(sfc)))
        '    '                        Dim strSafety As String = Array.Find(arrSafety, Function(x) (x.StartsWith(sfc)))
        '    '                        Dim strDrvM1 As String = Array.Find(arrDrive_M1, Function(x) (x.StartsWith(sfc)))
        '    '                        Dim strDrvM2 As String = Array.Find(arrDrive_M2, Function(x) (x.StartsWith(sfc)))
        '    '                        Dim strDrvM3 As String = Array.Find(arrDrive_M3, Function(x) (x.StartsWith(sfc)))
        '    '                        Dim strDrvM4 As String = Array.Find(arrDrive_M4, Function(x) (x.StartsWith(sfc)))
        '    '                        Dim strIO As String = Array.Find(arrIO, Function(x) (x.StartsWith(sfc)))

        '    '                        If Not strSteer = Nothing Then
        '    '                            ListRow.Steering = sfc
        '    '                        End If

        '    '                        If Not strDrive = Nothing Then
        '    '                            ListRow.Driving = sfc
        '    '                        End If

        '    '                        If Not strIFM = Nothing Then
        '    '                            ListRow.IFMControl = sfc
        '    '                        End If

        '    '                        If Not strSpeed = Nothing Then
        '    '                            ListRow.SpeedControl = sfc
        '    '                        End If

        '    '                        If Not strWarning = Nothing Then
        '    '                            ListRow.WarningControl = sfc
        '    '                        End If

        '    '                        If Not strCAN = Nothing Then
        '    '                            ListRow.CANControl = sfc
        '    '                        End If

        '    '                        If Not strCANOPEN = Nothing Then
        '    '                            ListRow.CANOPENControl = sfc
        '    '                        End If

        '    '                        If Not strAlign = Nothing Then
        '    '                            ListRow.AlignmentControl = sfc
        '    '                        End If

        '    '                        If Not strDataLogger = Nothing Then
        '    '                            ListRow.LCD_DataLogger = sfc
        '    '                        End If

        '    '                        If Not strSafety = Nothing Then
        '    '                            ListRow.LCD_Safety = sfc
        '    '                        End If

        '    '                        If Not strSafety = Nothing Then
        '    '                            ListRow.LCD_Safety = sfc
        '    '                        End If

        '    '                        If Not strDrvM1 = Nothing Then
        '    '                            ListRow.LCD_DriveM1 = sfc
        '    '                        End If

        '    '                        If Not strDrvM2 = Nothing Then
        '    '                            ListRow.LCD_DriveM2 = sfc
        '    '                        End If

        '    '                        If Not strDrvM3 = Nothing Then
        '    '                            ListRow.LCD_DriveM3 = sfc
        '    '                        End If

        '    '                        If Not strDrvM4 = Nothing Then
        '    '                            ListRow.LCD_DriveM4 = sfc
        '    '                        End If

        '    '                        If Not strIO = Nothing Then
        '    '                            ListRow.LCD_IO = sfc
        '    '                        End If

        '    '                    Next sfc

        '    '            End Select


        '    '        Next


        '    '        oList.Add(ListRow)

        '    '        'End If

        '    '    End If

        '    '    'Return oList
        '    '    'If vehicle Is Nothing Then
        '    '    '    Return False
        '    '    'Else
        '    '    '    Return True
        '    '    'End If

        '    'Catch ex As Exception
        '    '    'Throw ex
        '    '    'retobj.MessageDefinition = New FMS.Business.DataObjects.CAN_MessageDefinition()
        '    '    oList = New List(Of DashboardValues)
        '    'End Try


        '    'Return oList
        '    ''get the value 
        '    ''Return retobj

        'End Function


    End Class


End Namespace



