﻿Namespace DataObjects

    Public Class CanDataPoint

#Region "properties & constructors"

        ''' <summary>
        ''' Format = CAN_DeviceID_CanStandard_PGN (eg: CAN_demo01_Zagro125_255)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TAG_STRING_FORMAT As String = "CAN_{0}_{1}_{2}"

        Public Shared Function GetTagName(deviceid As String, standard As String, pgn As Integer) As String
            Return String.Format(TAG_STRING_FORMAT, deviceid, standard, pgn)
        End Function

        Public Property MessageDefinition As DataObjects.CAN_MessageDefinition

        Public Property CanValues As New CanValueList

        Public Sub New()

        End Sub

#End Region 
        Public Shared Function GetPointWithData(SPN As Integer, vehicleid As String, _
                                                standard As String, startDate As Date, endDate As Date) As DataObjects.CanDataPoint

            Dim retobj As New CanDataPoint

            Dim vehicle As DataObjects.ApplicationVehicle = DataObjects.ApplicationVehicle.GetFromName(vehicleid)

            'get the MessageDefinition
            retobj.MessageDefinition = DataObjects.CAN_MessageDefinition.GetForSPN(SPN, standard)

            ' Format = CAN_DeviceID_CanStandard_PGN (eg: CAN_demo01_Zagro125_255)
            Dim tagName As String = DataObjects.CanDataPoint.GetTagName(vehicle.DeviceID, standard, _
                                                                                retobj.MessageDefinition.PGN)
            Try

                Dim pp As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(tagName)

                'get data from pi for the time period
                Dim pivds As PISDK.PIValues = pp.Data.RecordedValues(startDate, endDate,
                                                                     PISDK.BoundaryTypeConstants.btInside)

                For Each p As PISDK.PIValue In pivds

                    Try

                        retobj.CanValues.Add(New CanValue With {.Time = p.TimeStamp.LocalDate,
                                                                                .RawValue = p.Value})

                    Catch ex As Exception
                    End Try
                Next

                'Calculate the actual value from the raw values
                retobj.CanValues.CalculateValues(SPN, retobj.MessageDefinition)
            Catch ex As Exception
                retobj.MessageDefinition = New FMS.Business.DataObjects.CAN_MessageDefinition()
            End Try

            'get the value 
            Return retobj

        End Function

        'Public Shared Function GetPointWithDataForDashboard(vehicleid As String) As Boolean
        Public Shared Function GetPointWithDataForDashboard(vehicleID As String) As Boolean

            Dim vehicle As DataObjects.ApplicationVehicle = DataObjects.ApplicationVehicle.GetFromName(vehicleID)
           
            Try

                If vehicle Is Nothing Then
                    Return False
                Else
                    Return True
                End If

            Catch ex As Exception
                Throw ex
                'retobj.MessageDefinition = New FMS.Business.DataObjects.CAN_MessageDefinition()
            End Try

            'get the value 
            'Return retobj

        End Function

        Public Shared Function GetPointWithDataByDeviceId(SPN As Integer, deviceID As String, _
                                                standard As String, startDate As Date, endDate As Date) As DataObjects.CanDataPoint

            Dim retobj As New CanDataPoint

            Dim vehicle As DataObjects.ApplicationVehicle = DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID)

            'get the MessageDefinition
            retobj.MessageDefinition = DataObjects.CAN_MessageDefinition.GetForSPN(SPN, standard)

            ' Format = CAN_DeviceID_CanStandard_PGN (eg: CAN_demo01_Zagro125_255)
            Dim tagName As String = DataObjects.CanDataPoint.GetTagName(vehicle.DeviceID, standard, _
                                                                                retobj.MessageDefinition.PGN)
            Try

                Dim pp As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(tagName)

                'get data from pi for the time period
                Dim pivds As PISDK.PIValues = pp.Data.RecordedValues(startDate, endDate,
                                                                     PISDK.BoundaryTypeConstants.btInside)

                For Each p As PISDK.PIValue In pivds

                    Try

                        retobj.CanValues.Add(New CanValue With {.Time = p.TimeStamp.LocalDate,
                                                                                .RawValue = p.Value})

                    Catch ex As Exception
                    End Try
                Next

                'Calculate the actual value from the raw values
                retobj.CanValues.CalculateValues(SPN, retobj.MessageDefinition)
            Catch ex As Exception
                retobj.MessageDefinition = New FMS.Business.DataObjects.CAN_MessageDefinition()
            End Try

            'get the value 
            Return retobj

        End Function

        Public Shared Function GetPointWithLatestDataByDeviceId(SPN As Integer, deviceID As String, _
                                               standard As String) As DataObjects.CanDataPoint

            Dim retobj As New CanDataPoint

            Dim vehicle As DataObjects.ApplicationVehicle = DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID)

            'get the MessageDefinition
            retobj.MessageDefinition = DataObjects.CAN_MessageDefinition.GetForSPN(SPN, standard)

            ' Format = CAN_DeviceID_CanStandard_PGN (eg: CAN_demo01_Zagro125_255)
            Dim tagName As String = DataObjects.CanDataPoint.GetTagName(vehicle.DeviceID, standard, _
                                                                                retobj.MessageDefinition.PGN)

            Try

                Dim pp As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(tagName)

                'date format for demo.nanosoft.com.au is dd/MM/yyyy
                'date format for local is MM/dd/yyyy
                Dim startDate As Date = Date.Now.AddDays(1).ToString("dd/MM/yyyy")
                Dim endDate As Date = Date.Now.AddDays(-1).ToString("dd/MM/yyyy")
                Dim intCount As Integer = 1
                'get data from pi for the time period
                Dim pivds As PISDK.PIValues = pp.Data.RecordedValues(startDate, endDate,
                                                                     PISDK.BoundaryTypeConstants.btInside)

                'get the latest data from pi from current date backwards up to 30 counts/days to avoid infinity
                While pivds.Count = 0
                    intCount += 1
                    Dim eDate As Date = startDate.AddDays(-intCount).ToString("dd/MM/yyyy")
                    pivds = pp.Data.RecordedValues(startDate, eDate, PISDK.BoundaryTypeConstants.btInside)
                    If intCount = 30 Then Exit While
                End While

                For Each p As PISDK.PIValue In pivds

                    Try

                        retobj.CanValues.Add(New CanValue With {.Time = p.TimeStamp.LocalDate,
                                                                                .RawValue = p.Value})
                        'If Not SPN.Equals(5) Then Exit For
                        Exit For
                    Catch ex As Exception
                    End Try
                Next

                'Calculate the actual value from the raw values
                retobj.CanValues.CalculateValues(SPN, retobj.MessageDefinition)
            Catch ex As Exception
                retobj.MessageDefinition = New FMS.Business.DataObjects.CAN_MessageDefinition()
            End Try

            'get the value 
            Return retobj

        End Function

        Public Class CanValueList
            Inherits List(Of CanValue)

            Public Property IntHornPressed As Integer

            Public Delegate Sub doCalcDelegate(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)

            'Zagro125:
            'PGN    name                        SPN_#
            '578	Speed						1
            '578	Parking Break				2
            '578	Beacon Operation			3
            '578	Fault Codes					4
            '645	Horn						5
            '646	Pressure Values				6
            '1090	Battery Voltage				7



            Public Sub CalculateValues(SPN As Integer, msg_def As CAN_MessageDefinition)

                Dim calcMethod As doCalcDelegate = Nothing

                If msg_def.Standard = "Zagro125" Then
                    If SPN = 1 Then calcMethod = AddressOf zagro125_1
                    If SPN = 2 Then calcMethod = AddressOf zagro125_2
                    If SPN = 3 Then calcMethod = AddressOf zagro125_3
                    If SPN = 4 Then calcMethod = AddressOf zagro125_4
                    If SPN = 5 Then calcMethod = AddressOf zagro125_5
                    'If SPN = 6 Then calcMethod = AddressOf zagro125_6
                    If SPN = 7 Then calcMethod = AddressOf zagro125_7

                    If SPN >= 8 AndAlso SPN <= 11 Then calcMethod = AddressOf zagro125_pressurevals
                End If

                If msg_def.Standard = "Zagro500" Then
                    If SPN = 7 Then calcMethod = AddressOf zagro500_7
                    If SPN = 3 Then calcMethod = AddressOf zagro500_3
                    If SPN = 8 Then calcMethod = AddressOf zagro500_8
                    If SPN = 9 Then calcMethod = AddressOf zagro500_9
                    If SPN = 10 Then calcMethod = AddressOf zagro500_10
                    If SPN = 11 Then calcMethod = AddressOf zagro500_11
                    If SPN = 12 Then calcMethod = AddressOf zagro500_12
                End If

                If msg_def.Standard = "j1939" Then calcMethod = AddressOf j1939

                For Each cv As CanValue In Me
                    calcMethod(cv, msg_def)
                Next

            End Sub


            Public Sub unknown_calc(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)
                cv.Value = "not implemented standard / SPN combination"
            End Sub
#Region "Zagro 125K" 'Contains all the Zagro 125K methods. We will need to add for 500K when we get the logic.

            '1090	Battery Voltage				7
            Public Sub zagro125_7(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)
                cv.Value = GetJ1939(cv, msg_def) / 100 'uses same logic as j1939
            End Sub

            '646	Pressure Values				8,9,10,11
            Public Sub zagro125_pressurevals(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)
                cv.Value = GetJ1939(cv, msg_def) / 10000 * 250 'uses same logic as j1939

            End Sub


            '645	Horn						5
            Public Sub zagro125_5(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)

                Dim i As Decimal = StringToByteArray(cv.RawValue)(0)

                i /= 2 'divide by 2 apparently?
                cv.Value = If(i Mod 2 = 0, "Horn OFF", "Horn ON")
                'If (i Mod 2 > 0) Then
                '    IntHornPressed += 1
                'End If
                'cv.Value = IntHornPressed
            End Sub

            '578	Fault Codes					4
            Public Sub zagro125_4(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)

                Dim b() As Byte = StringToByteArray(cv.RawValue)

                Dim startByte As Integer = msg_def.pos_start

                Dim indx As Integer = 0
                Dim runningtotal As Double = 0

                For i As Integer = 5 To 4

                    runningtotal += CInt(b(i)) * Math.Pow(256, indx)
                    indx += 1
                Next

                Dim faultMode As Integer = CInt(b(3))

                cv.Value = String.Format("FaultCode:{0}, FaultMode:{1}", runningtotal, faultMode)

            End Sub

            '578	Beacon Operation			3
            Public Sub zagro125_3(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)

                Dim i As Decimal = CInt(StringToByteArray(cv.RawValue)(6))
                cv.Value = If(i Mod 2 = 0, "Beacon is OFF", "Beacon is ON")

            End Sub

            '578	Parking Break				2
            Public Sub zagro125_2(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)

                Dim i As Decimal = CInt(StringToByteArray(cv.RawValue)(7))
                cv.Value = If(i Mod 2 = 0, "Parking Break OFF", "Parking Break ON")

            End Sub


            '578	Speed						1
            Public Sub zagro125_1(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)
                Dim J1939Value = GetJ1939(cv, msg_def)
                '32768 fix value from the excel file CANBUS_Analysis_125KbData
                If J1939Value < 32768 Then
                    cv.Value = J1939Value / 100
                Else
                    cv.Value = -Convert.ToInt32(Right(Hex(-J1939Value), 4), 16) / 100
                End If
            End Sub
#End Region

#Region "Zagro 500K"
            Public Sub zagro500_7(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)
                cv.Value = GetJ1939(cv, msg_def) / 100
            End Sub

            Public Sub zagro500_3(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)
                Dim i As Array = cv.RawValue.ToCharArray
                Dim val As String = i(2).ToString + i(3).ToString
                cv.Value = IIf(val.Equals("06"), "Diagonal mode road", IIf(val.Equals("08"), "Circle mode road", IIf(val.Equals("10"), "rail mode", IIf(val.Equals("00"), "Undefined", "Stationary"))))
            End Sub
            Public Sub zagro500_8(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)
                cv.Value = GetMotorTemperature(cv, msg_def)
            End Sub
            Public Sub zagro500_9(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)
                cv.Value = GetMotorTemperature(cv, msg_def)
            End Sub
            Public Sub zagro500_10(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)
                cv.Value = GetMotorTemperature(cv, msg_def)
            End Sub
            Public Sub zagro500_11(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)
                cv.Value = GetMotorTemperature(cv, msg_def)
            End Sub
            Public Sub zagro500_12(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)
                Dim byte0 As Object = GetFaultCodes(cv, msg_def, 0, 1, 0)
                Dim byte1 As Object = GetFaultCodes(cv, msg_def, 2, 3, 1)
                Dim byte2 As Object = GetFaultCodes(cv, msg_def, 4, 5, 2)
                Dim byte3 As Object = GetFaultCodes(cv, msg_def, 6, 7, 3)
                Dim byte4 As Object = GetFaultCodes(cv, msg_def, 8, 9, 4)
                Dim byte5 As Object = GetFaultCodes(cv, msg_def, 10, 11, 5)
                Dim byte6 As Object = GetFaultCodes(cv, msg_def, 12, 13, 6)
                Dim byte7 As Object = GetFaultCodes(cv, msg_def, 14, 15, 7)
                Dim cvVal As String = IIf(Not byte0.Equals(""), byte0 & ",", "") + IIf(Not byte1.Equals(""), byte1 & ",", "") + _
                    IIf(Not byte2.Equals(""), byte2 & ",", "") + IIf(Not byte3.Equals(""), byte3 & ",", "") + _
                    IIf(Not byte4.Equals(""), byte4 & ",", "") + If(Not byte5.Equals(""), byte5 & ",", "") + _
                    IIf(Not byte6.Equals(""), byte6 & ",", "") + IIf(Not byte7.Equals(""), byte7 & ",", "")
                cv.Value = cvVal.Trim(",")

            End Sub
#End Region

            Public Shared Function StringToByteArray(hex As String) As Byte()
                Return Enumerable.Range(0, hex.Length).Where(Function(x) x Mod 2 = 0).[Select](Function(x) Convert.ToByte(hex.Substring(x, 2), 16)).ToArray()
            End Function


            Public Sub j1939(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)

                Dim b() As Byte = StringToByteArray(cv.RawValue)

                Dim startByte As Integer = msg_def.pos_start

                Dim indx As Integer = 0
                Dim runningtotal As Double = 0

                For i As Integer = msg_def.pos_start - 1 To msg_def.pos_end - 1

                    runningtotal += CInt(b(i)) * Math.Pow(256, indx)
                    indx += 1
                Next

                runningtotal = msg_def.offset + (runningtotal * msg_def.Resolution_Multiplier)

                cv.Value = runningtotal

            End Sub

            Public Function GetJ1939(ByVal cv As CanValue, msg_def As CAN_MessageDefinition) As Object

                Dim b() As Byte = StringToByteArray(cv.RawValue)

                Dim startByte As Integer = msg_def.pos_start

                Dim indx As Integer = 0
                Dim runningtotal As Double = 0
                Dim startTotal As Double = 0

                For i As Integer = msg_def.pos_start - 1 To msg_def.pos_end - 1
                    If indx.Equals(0) Then
                        startTotal = CInt(b(i))
                    Else
                        runningtotal += startTotal + (CInt(b(i)) * Math.Pow(256, indx))
                    End If

                    indx += 1
                Next

                Return runningtotal
            End Function

            Public Function GetMotorTemperature(ByVal cv As CanValue, msg_def As CAN_MessageDefinition) As Object
                Dim objValue As Object = ""
                Dim b() As Byte = StringToByteArray(cv.RawValue)
                Dim i As Array = cv.RawValue.ToCharArray
                Dim convertedValue As Integer
                Dim tempList = GetTemperatureList()
                convertedValue = b(4) * 2
                Dim intListCount As Integer = tempList.Count
                For iVal As Integer = 0 To intListCount - 2
                    If convertedValue >= tempList(iVal).Key And convertedValue <= tempList(iVal + 1).Key Then
                        Dim outTempValue As Integer
                        Dim outTempValue2 As Integer
                        Dim outTempValue3 As Integer
                        Integer.TryParse(tempList(iVal + 1).Value.Split(",")(0), outTempValue2)
                        Integer.TryParse(tempList(iVal).Value.Split(",")(1), outTempValue)
                        Integer.TryParse(tempList(iVal + 1).Value.Split(",")(2), outTempValue3)
                        objValue = (outTempValue + ((convertedValue - tempList(iVal).Key) / outTempValue2) * outTempValue3) / 10
                        Exit For
                    End If
                Next
                Return IIf(objValue.Equals(""), 0, objValue)
            End Function
            Public Function GetFaultCodes(ByVal cv As CanValue, msg_def As CAN_MessageDefinition, bitStart As Integer, bitEnd As Integer, byteCol As String) As Object
                Dim i As Array = cv.RawValue.ToCharArray
                Dim bit0 = Convert.ToString(Convert.ToInt32(i(bitStart), 16), 2).PadLeft(4, "0")
                Dim bit1 = Convert.ToString(Convert.ToInt32(i(bitEnd), 16), 2).PadLeft(4, "0")
                Dim byteVal As String = StrReverse((bit0 + bit1).ToString())
                Dim faultCodeValues As String = ""
                Dim intCounter As Integer = 1
                For Each bVal As Char In byteVal.ToCharArray
                    If Convert.ToInt16(bVal.ToString()) Then
                        Dim faultList = GetFaultCodeList().Where(Function(xy) xy.byteCode.Equals(byteCol)).ToList()
                        Dim faultCode = faultList.Select(Function(blist) blist.byteList.Where(Function(xy) xy.Key = intCounter)).ToList()

                        If Not faultCode(0)(0).Value.ToString().Equals("") Then
                            faultCodeValues &= faultCode(0)(0).Value.ToString + ","
                        End If

                    End If
                    intCounter += 1
                Next
                Return faultCodeValues.Trim(",")
            End Function

            Public Shared Function GetTemperatureList() As List(Of KeyValuePair(Of Integer, String))
                Return New List(Of KeyValuePair(Of Integer, String)) _
                    From
                    {
                        New KeyValuePair(Of Integer, String)(97, "0,-500,0"),
                        New KeyValuePair(Of Integer, String)(127, "31,-200,300"),
                        New KeyValuePair(Of Integer, String)(150, "23,0,200"),
                        New KeyValuePair(Of Integer, String)(174, "24,200,200"),
                        New KeyValuePair(Of Integer, String)(203, "29,400,200"),
                        New KeyValuePair(Of Integer, String)(234, "31,600,200"),
                        New KeyValuePair(Of Integer, String)(268, "34,800,200"),
                        New KeyValuePair(Of Integer, String)(305, "37,1000,200"),
                        New KeyValuePair(Of Integer, String)(344, "39,1200,200"),
                        New KeyValuePair(Of Integer, String)(386, "42,1400,200"),
                        New KeyValuePair(Of Integer, String)(431, "45,1600,200"),
                        New KeyValuePair(Of Integer, String)(467, "36,1750,150")
                        }
            End Function

            Public Shared Function GetFaultCodeList() As List(Of FaultCodes)
                Dim lstFaultCodes As New List(Of FaultCodes)

                lstFaultCodes.Add(New FaultCodes() With {.byteCode = 0, .byteList = New List(Of KeyValuePair(Of Integer, String)) _
                                                                        From {
                                                                                New KeyValuePair(Of Integer, String)(1, "S 1"),
                                                                                New KeyValuePair(Of Integer, String)(2, "S 4"),
                                                                                New KeyValuePair(Of Integer, String)(3, "S 20"),
                                                                                New KeyValuePair(Of Integer, String)(4, "S 21"),
                                                                                New KeyValuePair(Of Integer, String)(5, ""),
                                                                                New KeyValuePair(Of Integer, String)(6, ""),
                                                                                New KeyValuePair(Of Integer, String)(7, ""),
                                                                                New KeyValuePair(Of Integer, String)(8, "Can 31")
                                                                            }
                                                                        })
                lstFaultCodes.Add(New FaultCodes() With {.byteCode = 1, .byteList = New List(Of KeyValuePair(Of Integer, String)) _
                                                                        From {
                                                                                New KeyValuePair(Of Integer, String)(1, "Canopen 1"),
                                                                                New KeyValuePair(Of Integer, String)(2, "Canopen 2"),
                                                                                New KeyValuePair(Of Integer, String)(3, "Canopen 3"),
                                                                                New KeyValuePair(Of Integer, String)(4, "Canopen 4"),
                                                                                New KeyValuePair(Of Integer, String)(5, "Canopen 5"),
                                                                                New KeyValuePair(Of Integer, String)(6, "Canopen 6"),
                                                                                New KeyValuePair(Of Integer, String)(7, "Canopen 7"),
                                                                                New KeyValuePair(Of Integer, String)(8, "Canopen 8")
                                                                            }
                                                                        })
                lstFaultCodes.Add(New FaultCodes() With {.byteCode = 2, .byteList = New List(Of KeyValuePair(Of Integer, String)) _
                                                                        From {
                                                                                New KeyValuePair(Of Integer, String)(1, "Can  1, Can  101"),
                                                                                New KeyValuePair(Of Integer, String)(2, "Can  2, Can  102"),
                                                                                New KeyValuePair(Of Integer, String)(3, "Can  3, Can  103"),
                                                                                New KeyValuePair(Of Integer, String)(4, "Can  4, Can  104"),
                                                                                New KeyValuePair(Of Integer, String)(5, "Can  5, Can  105"),
                                                                                New KeyValuePair(Of Integer, String)(6, "Can  6, Can  106"),
                                                                                New KeyValuePair(Of Integer, String)(7, "Can  7, Can  107"),
                                                                                New KeyValuePair(Of Integer, String)(8, "Can  8, Can  108")
                                                                            }
                                                                        })
                lstFaultCodes.Add(New FaultCodes() With {.byteCode = 3, .byteList = New List(Of KeyValuePair(Of Integer, String)) _
                                                                        From {
                                                                                New KeyValuePair(Of Integer, String)(1, "Can  201"),
                                                                                New KeyValuePair(Of Integer, String)(2, "Can  202"),
                                                                                New KeyValuePair(Of Integer, String)(3, "Can  203"),
                                                                                New KeyValuePair(Of Integer, String)(4, "Can  204"),
                                                                                New KeyValuePair(Of Integer, String)(5, "Can  205"),
                                                                                New KeyValuePair(Of Integer, String)(6, "Can  206"),
                                                                                New KeyValuePair(Of Integer, String)(7, "Can  207"),
                                                                                New KeyValuePair(Of Integer, String)(8, "Can  208")
                                                                            }
                                                                        })
                lstFaultCodes.Add(New FaultCodes() With {.byteCode = 4, .byteList = New List(Of KeyValuePair(Of Integer, String)) _
                                                                        From {
                                                                                New KeyValuePair(Of Integer, String)(1, "MS  1"),
                                                                                New KeyValuePair(Of Integer, String)(2, "MS  11"),
                                                                                New KeyValuePair(Of Integer, String)(3, "MS  12"),
                                                                                New KeyValuePair(Of Integer, String)(4, "MS  14"),
                                                                                New KeyValuePair(Of Integer, String)(5, "MS  21"),
                                                                                New KeyValuePair(Of Integer, String)(6, "MS  22"),
                                                                                New KeyValuePair(Of Integer, String)(7, "MS  23"),
                                                                                New KeyValuePair(Of Integer, String)(8, "MS  31")
                                                                            }
                                                                        })
                lstFaultCodes.Add(New FaultCodes() With {.byteCode = 5, .byteList = New List(Of KeyValuePair(Of Integer, String)) _
                                                                        From {
                                                                                New KeyValuePair(Of Integer, String)(1, "MS  32"),
                                                                                New KeyValuePair(Of Integer, String)(2, "MS  34"),
                                                                                New KeyValuePair(Of Integer, String)(3, "MS  41"),
                                                                                New KeyValuePair(Of Integer, String)(4, "MS  42"),
                                                                                New KeyValuePair(Of Integer, String)(5, "M1  1"),
                                                                                New KeyValuePair(Of Integer, String)(6, "M1  2"),
                                                                                New KeyValuePair(Of Integer, String)(7, "M1  4"),
                                                                                New KeyValuePair(Of Integer, String)(8, "M2  1")
                                                                            }
                                                                        })
                lstFaultCodes.Add(New FaultCodes() With {.byteCode = 6, .byteList = New List(Of KeyValuePair(Of Integer, String)) _
                                                                        From {
                                                                                New KeyValuePair(Of Integer, String)(1, "M2  2"),
                                                                                New KeyValuePair(Of Integer, String)(2, "M2  4"),
                                                                                New KeyValuePair(Of Integer, String)(3, "M3  1"),
                                                                                New KeyValuePair(Of Integer, String)(4, "M3  2"),
                                                                                New KeyValuePair(Of Integer, String)(5, "M3  4"),
                                                                                New KeyValuePair(Of Integer, String)(6, "M4  1"),
                                                                                New KeyValuePair(Of Integer, String)(7, "M4  2"),
                                                                                New KeyValuePair(Of Integer, String)(8, "M4  4")
                                                                            }
                                                                        })
                lstFaultCodes.Add(New FaultCodes() With {.byteCode = 7, .byteList = New List(Of KeyValuePair(Of Integer, String)) _
                                                                        From {
                                                                                New KeyValuePair(Of Integer, String)(1, "IO 1"),
                                                                                New KeyValuePair(Of Integer, String)(2, "IO 4"),
                                                                                New KeyValuePair(Of Integer, String)(3, "IO 20"),
                                                                                New KeyValuePair(Of Integer, String)(4, "IO 40"),
                                                                                New KeyValuePair(Of Integer, String)(5, ""),
                                                                                New KeyValuePair(Of Integer, String)(6, ""),
                                                                                New KeyValuePair(Of Integer, String)(7, ""),
                                                                                New KeyValuePair(Of Integer, String)(8, "")
                                                                            }
                                                                        })

                Return lstFaultCodes

            End Function

            Public Class FaultCodes
                Public Property byteCode As String
                Public Property byteList As New List(Of KeyValuePair(Of Integer, String))
            End Class

        End Class

        Public Class CanBusFaultDefinition
            Public Shared Function GetFaultCodeList() As List(Of KeyValuePair(Of String, String))
                Return New List(Of KeyValuePair(Of String, String)) _
                    From {
                        New KeyValuePair(Of String, String)("S 1", "E,Emergency stop"),
                        New KeyValuePair(Of String, String)("S 4", "E,Limit end position. Acknowledgment by key switch"),
                        New KeyValuePair(Of String, String)("S 4", "W,Incorrect start after and active faults. First joystick in the rest position"),
                        New KeyValuePair(Of String, String)("Can 31", "E,Timeout vehicle plc CR0020 A12 (from V4.0.8)"),
                        New KeyValuePair(Of String, String)("Canopen 1", "W,Timeout position transmitter B1,not for E-Maxi S, not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Canopen 2", "W,Timeout position transmitter B2,not for E-Maxi S, not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Canopen 3", "W,Timeout position transmitter B3,not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Canopen 4", "W,Timeout position transmitter B4,not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Canopen 5", "E,Timeout radio control A11"),
                        New KeyValuePair(Of String, String)("Canopen 6", "W,Timeout hydraulic module CR2032 A10,not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Canopen 7", "W,Timeout WBA module CR2032 A40,only option 'WBA'"),
                        New KeyValuePair(Of String, String)("Canopen 8", "W,Timeout additional hydraulic module CR2032 A37,only option 'addional hydraulic'"),
                        New KeyValuePair(Of String, String)("Can 1", "E,Timeout AC Inverter A1 (drive)"),
                        New KeyValuePair(Of String, String)("Can 2", "E,Timeout AC Inverter A2 (drive)"),
                        New KeyValuePair(Of String, String)("Can 3", "E,Timeout AC Inverter A3 (drive)"),
                        New KeyValuePair(Of String, String)("Can 4", "E,Timeout AC Inverter A4 (drive)"),
                        New KeyValuePair(Of String, String)("Can 5", "E,Timeout AC Inverter A5 (steering),not for E-Maxi S,not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Can 6", "E,Timeout AC Inverter A6 (steering),not for E-Maxi S,not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Can 7", "E,Timeout AC Inverter A7 (steering),not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Can 8", "E,Timeout AC Inverter A8 (steering),not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Can 8", "E,Timeout vehicle plc CR0020 A12 (till V4.0.8)"),
                        New KeyValuePair(Of String, String)("Can 101", "E,Timeout AC Inverter A1 (drive) <br> no message reception since emergency stop or switching on"),
                        New KeyValuePair(Of String, String)("Can 102", "E,Timeout AC Inverter A2 (drive) <br> no message reception since emergency stop or switching on"),
                        New KeyValuePair(Of String, String)("Can 103", "E,Timeout AC Inverter A3 (drive) <br> no message reception since emergency stop or switching on"),
                        New KeyValuePair(Of String, String)("Can 104", "E,Timeout AC Inverter A4 (drive) <br> no message reception since emergency stop or switching on"),
                        New KeyValuePair(Of String, String)("Can 105", "E,Timeout AC Inverter A5 (steering) <br> no message reception since emergency stop or switching on,not for E-Maxi S, not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Can 106", "E,Timeout AC Inverter A6 (steering) <br> no message reception since emergency stop or switching on,not for E-Maxi S, not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Can 107", "E,Timeout AC Inverter A7 (steering) <br> no message reception since emergency stop or switching on,not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Can 108", "E,Timeout AC Inverter A8 (steering) <br> no message reception since emergency stop or switching on,not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Can 205", "E,Timeout boot up message AC Inverter A5,not for E-Maxi S,not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Can 206", "E,Timeout boot up message AC Inverter A6,not for E-Maxi S,not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Can 207", "E,Timeout boot up message AC Inverter A7,not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("Can 208", "E,Timeout boot up message AC Inverter A8,not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("MS 1", "E,no steering control is online"),
                        New KeyValuePair(Of String, String)("MS 11", "E,Error position transmitter B1,not for E-Maxi S"),
                        New KeyValuePair(Of String, String)("MS 12", "E,Error AC inverter A5,not for E-Maxi S"),
                        New KeyValuePair(Of String, String)("MS 14", "E,Position discrepancy between axle 1 and 4 is too large. <br> Reset via emergency stop,not for E-Maxi S"),
                        New KeyValuePair(Of String, String)("MS 21", "E,Error position transmitter B2,not for E-Maxi S"),
                        New KeyValuePair(Of String, String)("MS 22", "E,Error AC inverter A6,not for E-Maxi S"),
                        New KeyValuePair(Of String, String)("MS 23", "E,Position discrepancy between axle 2 and 3 is too large. <br> Reset via emergency stop,not for E-Maxi S"),
                        New KeyValuePair(Of String, String)("MS 31", "E,Error position transmitter B3"),
                        New KeyValuePair(Of String, String)("MS 32", "E,Error AC inverter A7"),
                        New KeyValuePair(Of String, String)("MS 34", "E,Position discrepancy between axle 3 and 4 is too large. <br> Reset via emergency stop,only E-Maxi S"),
                        New KeyValuePair(Of String, String)("MS 41", "E,Error position transmitter B4"),
                        New KeyValuePair(Of String, String)("MS 42", "E,Error AC inverter A8"),
                        New KeyValuePair(Of String, String)("M1 1", "E,Drive/Engine axle 1.Wants to drive but no pulse signals (encoder?). <br> Reset via emergency stop"),
                        New KeyValuePair(Of String, String)("M1 2", "E,Drive/Engine axle 1.Main contactor of the drive is not activated. <br> (see logbook Zapi)"),
                        New KeyValuePair(Of String, String)("M1 4", "E,Drive/Engine axle 1.Temperature of the drive engine Mx is above break-off shaft."),
                        New KeyValuePair(Of String, String)("M2 1", "E,Drive/Engine axle 2.Wants to drive but no pulse signals (encoder?). <br> Reset via emergency stop"),
                        New KeyValuePair(Of String, String)("M2 2", "E,Drive/Engine axle 2.Main contactor of the drive is not activated. <br> (see logbook Zapi)"),
                        New KeyValuePair(Of String, String)("M2 4", "E,Drive/Engine axle 2.Temperature of the drive engine Mx is above break-off shaft."),
                        New KeyValuePair(Of String, String)("M3 1", "E,Drive/Engine axle 3.Wants to drive but no pulse signals (encoder?). <br> Reset via emergency stop"),
                        New KeyValuePair(Of String, String)("M3 2", "E,Drive/Engine axle 3.Main contactor of the drive is not activated. <br> (see logbook Zapi)"),
                        New KeyValuePair(Of String, String)("M3 4", "E,Drive/Engine axle 3.Temperature of the drive engine Mx is above break-off shaft."),
                        New KeyValuePair(Of String, String)("M4 1", "E,Drive/Engine axle 4.Wants to drive but no pulse signals (encoder?). <br> Reset via emergency stop"),
                        New KeyValuePair(Of String, String)("M4 2", "E,Drive/Engine axle 4.Main contactor of the drive is not activated. <br> (see logbook Zapi)"),
                        New KeyValuePair(Of String, String)("M4 4", "E,Drive/Engine axle 4.Temperature of the drive engine Mx is above break-off shaft."),
                        New KeyValuePair(Of String, String)("IO 1", "W,Cable break battery supervision."),
                        New KeyValuePair(Of String, String)("IO 4", "W,Battery empty <br> speed reduction"),
                        New KeyValuePair(Of String, String)("IO 20", "E,Invalid steering program,not for E-Maxi with option 'no steering'"),
                        New KeyValuePair(Of String, String)("IO 40", "W,Cable break current sensor.")
                        }
            End Function
        End Class
    End Class

End Namespace


