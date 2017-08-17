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







            '-------------
            ''Dim plst As PISDK.PointList = SingletonAccess.HistorianServer.GetPoints( _
            ''                                            String.Format("tag = 'can*{0}*'", deviceID))
            ''Dim cnt As Integer = plst.Count

            ''----- Get CAN_Protocol_Type from 
            'Dim vehicle As DataObjects.ApplicationVehicle = DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID)

            ''----- Search on Historoian Server with Tagname can*deviceID*. This is RAW data
            'Dim ptList As PISDK.PointList = SingletonAccess.HistorianServer.GetPoints(String.Format("tag = 'can*{0}*{1}*'", deviceID, "Zagro500"))
            'Dim cnt As Integer = ptList.Count

            'Dim lstCanMsg As New List(Of DataObjects.CAN_MessageDefinition)
            'Dim PGN_Hex As Integer

            'Try

            '    For Each pt As PISDK.PIPoint In ptList
            '        Dim ptName = pt.Name

            '        Dim pgn As Integer = ptName.Split("_").Reverse()(0)

            '        If pgn = 1602 Then
            '            PGN_Hex = Hex(1602)
            '        End If

            '        If pgn = 1346 Then
            '            PGN_Hex = Hex(1346)
            '        End If

            '        lstCanMsg.AddRange(CAN_MessageDefinition.GetForPGN(PGN_Hex, "Zagro500"))

            '    Next



            'Catch ex As Exception
            '    Throw ex
            'End Try

            Return True
        End Function

#Region "Non Static Method(s)"

        Public Function GetLCDHour(deviceID As String) As String
            Dim retobj As New CanDataPoint
            'Dim cdp As CanDataPoint = FMS.Business.DataObjects.CanDataPoint.GetPointWithData(SPN, vehicleID, standard, sd, ed)
            Dim vehicle = DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID)
            'Dim msgdef = DataObjects.Device.GetCANMessageDefinitions(deviceID)

            Dim strVehicleID = vehicle.Name
            'Dim strStandard = vehicle.CAN_Protocol_Type '----> will enable once cleared on discussion why is that on table its Zagro125
            Dim strStandard = "Zagro500"
            Dim sd = String.Format("{0:MM/dd/yyyy}", Now.AddDays(-2))
            Dim ed = String.Format("{0:MM/dd/yyyy}", Now.AddDays(1))
            'Dim sd = String.Format("{0:dd/MM/yyyy}", Now)
            'Dim ed = String.Format("{0:dd/MM/yyyy}", Now)
            Dim hrctrSPN = 13

            'get the MessageDefinition
            retobj.MessageDefinition = DataObjects.CAN_MessageDefinition.GetForSPN(hrctrSPN, strStandard)


            '----- Sample
            ' vehicleID     = RIO E-maxi L
            ' standard      = Zagro500
            ' SPN           = 13
            ' startdate     = 15/08/2017
            ' enddate       = 15/08/2017
            'demo.nanosoft.com.au/api/vehicle?vehicleID=RIO E-maxi L&standard=Zagro500&SPN=13&startdate=15/08/2017&enddate=17/08/2017

            'Dim cdp As CanDataPoint = FMS.Business.DataObjects.CanDataPoint.GetPointWithData(SPN, vehicleID, standard, sd, ed)
            Dim cdp As CanDataPoint = FMS.Business.DataObjects.CanDataPoint.GetPointWithData(hrctrSPN, strVehicleID, strStandard, sd, ed)

            'Dim JSONObj = JsonConvert.DeserializeObject(Of List(Of CanDataPoint))(cdp.ToString())

            'CanValue

            If IsNothing(cdp.CanValues) = False Then
                Dim olist As List(Of DataObjects.CanValue) = cdp.CanValues
                'olist.Sort(Function(x, y) x.Time.CompareTo(y.Time))
                'Dim oRaw1 = olist.OrderBy(Function(i) i.Time).First()

                Dim oRaw = olist.OrderByDescending(Function(o) o.Time).First()
                Dim oTime = oRaw.Time
                Dim oRawValue = oRaw.RawValue
                Dim oValue = oRaw.Value

                retobj.CanValues.CalculateValues(hrctrSPN, retobj.MessageDefinition)


                Dim objCanValues = retobj

            End If

            Return "80"
        End Function
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
                Dim ListRow = New DashboardValue
                Dim rowErrCat = New DashboardValue.clsErrCategory

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
                        Case "Parking Brake"
                            ListRow.Parking_Break = strValue

                            If strValue = "Parking Brake ON" Then
                                ListRow.StopControl = "ON"
                            Else
                                If strValue = "Parking Brake OFF" Then
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


    End Class

End Namespace

