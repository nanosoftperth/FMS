Namespace DataObjects


    Public Class DashboardValues

#Region "property definitions & constructors"

        Public Property Parking_Break As String
        Public Property Steering As String
        Public Property Driving As String
        Public Property IFMControl As String
        Public Property CANControl As String
        Public Property AlignmentControl As String
        Public Property WarningControl As String
        Public Property StopControl As String
        Public Property SpeedControl As String
        Public Property Battery_Voltage As String

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


        Public Sub New()

        End Sub

#End Region


        Public Shared Function GetDataForDashboard(vehicleID As String) As List(Of DashboardValues)

            Dim oList As List(Of DashboardValues) = New List(Of DashboardValues)
            'Dim vehicle As DataObjects.ApplicationVehicle = DataObjects.ApplicationVehicle.GetFromDeviceID(vehicleID)
            'FMS.Business.DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID).GetAvailableCANTagsValue()

            Dim vehicle = DataObjects.ApplicationVehicle.GetFromDeviceID(vehicleID).GetAvailableCANTagsValue()

            Try
                If vehicle.Count > 0 Then

                    'If Not vehicle(0).CanValues(0).Value = Nothing Then

                    'MsgBox(vehicle(0).CanValues(0).Value.ToString())

                    Dim strDesc, strValue As String
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

                    'Dim ctr = vehicle.Count
                    Dim ctr = 9 ' for testing. will remove/remarks before deploy

                    Dim ListRow = New DashboardValues

                    For ndx As Integer = 0 To ctr - 1
                        ' Will enable when deployed to demo/live
                        'strDesc = vehicle(ndx).MessageDefinition.Description 
                        'strValue = vehicle(ndx).CanValues(0).Value
                        ' End of disable

                        ' Test data. Will remove/remarks when deployed to demo/live
                        Select Case ndx
                            Case 0
                                strDesc = "Parking Break"
                                strValue = "Parking Break ON"
                            Case 1
                                strDesc = "Battery Voltage"
                                strValue = "80.54"
                            Case 3
                                strDesc = "Fault Codes"
                                strValue = "S 1,MS 8,MS 34,MS 41,MS 42,M1 1,M1 4,M2 4,M3 2,M4 4,Canopen 3,Canopen 1,Canopen 5,Can 101,Can 203,IO 3,IO 20,IO 40,IO 41"
                            Case Else
                                strDesc = ""
                                strValue = ""
                        End Select
                        ' End of Test Data

                        Select Case strDesc
                            Case "Parking Break"
                                ListRow.Parking_Break = strValue
                                ListRow.StopControl = "ON"
                            Case "Battery Voltage"
                                ListRow.Battery_Voltage = strValue
                            Case "Fault Codes"
                                Dim fc As String() = Nothing
                                fc = strValue.Split(",")
                                Dim sfc As String

                                For Each sfc In fc
                                    Dim chrpos = sfc.IndexOf(" ")
                                    strValue = sfc.Substring(0, chrpos)

                                    Dim strSteer As String = Array.Find(arrSteer, Function(x) (x.StartsWith(sfc)))
                                    Dim strDrive As String = Array.Find(arrDrive, Function(x) (x.StartsWith(sfc)))
                                    Dim strSpeed As String = Array.Find(arrSpeed, Function(x) (x.StartsWith(sfc)))
                                    Dim strWarning As String = Array.Find(arrWarning, Function(x) (x.StartsWith(sfc)))
                                    Dim strAlign As String = Array.Find(arrAlign, Function(x) (x.StartsWith(sfc)))
                                    Dim strIFM As String = Array.Find(arrIFM, Function(x) (x.StartsWith(sfc)))
                                    Dim strCAN As String = Array.Find(arrCAN, Function(x) (x.StartsWith(sfc)))
                                    Dim strCANOPEN As String = Array.Find(arrCANOPEN, Function(x) (x.StartsWith(sfc)))

                                    If Not strSteer = Nothing Then
                                        ListRow.Steering = sfc
                                    End If

                                    If Not strDrive = Nothing Then
                                        ListRow.Driving = sfc
                                    End If

                                    If Not strIFM = Nothing Then
                                        ListRow.IFMControl = sfc
                                    End If

                                    If Not strSpeed = Nothing Then
                                        ListRow.SpeedControl = sfc
                                    End If

                                    If Not strWarning = Nothing Then
                                        ListRow.WarningControl = sfc
                                    End If

                                    If Not strCAN = Nothing Or Not strCANOPEN = Nothing Then
                                        ListRow.CANControl = sfc
                                    End If

                                    If Not strAlign = Nothing Then
                                        ListRow.AlignmentControl = sfc
                                    End If

                                Next sfc

                                ListRow.StopControl = "ON"

                        End Select


                    Next

                    oList.Add(ListRow)

                    'End If

                End If

                Return oList
                'If vehicle Is Nothing Then
                '    Return False
                'Else
                '    Return True
                'End If

            Catch ex As Exception
                'Throw ex
                'retobj.MessageDefinition = New FMS.Business.DataObjects.CAN_MessageDefinition()
                oList = New List(Of DashboardValues)
            End Try

            Return oList
            'get the value 
            'Return retobj

        End Function


    End Class


End Namespace


