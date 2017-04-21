Namespace DataObjects

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

            'get the value 
            Return retobj

        End Function



        Public Class CanValueList
            Inherits List(Of CanValue)


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

                    If SPN >= 8 AndAlso SPN <= 11 Then calcMethod = AddressOf zagro125_pressureVals


                End If

                If msg_def.Standard = "Zagro500" Then
                    'fill in here after we get the message definitions
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
                j1939(cv, msg_def) 'uses same logic as j1939
            End Sub

            '646	Pressure Values				8,9,10,11
            Public Sub zagro125_pressurevals(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)
                j1939(cv, msg_def) 'uses same logic as j1939
            End Sub


            '645	Horn						5
            Public Sub zagro125_5(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)

                Dim i As Decimal = StringToByteArray(cv.RawValue)(0)

                i /= 2 'divide by 2 apparently?
                cv.Value = If(i Mod 2 = 0, "Horn OFF", "Horn ON")

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

                Dim b() As Byte = StringToByteArray(cv.RawValue)

                Dim startByte As Integer = msg_def.pos_start

                Dim indx As Integer = 0
                Dim runningtotal As Double = 0

                For i As Integer = 0 To 1

                    runningtotal += CInt(b(i)) * Math.Pow(256, indx)
                    indx += 1
                Next

                runningtotal /= 100

                cv.Value = runningtotal

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


        End Class


    End Class

End Namespace


