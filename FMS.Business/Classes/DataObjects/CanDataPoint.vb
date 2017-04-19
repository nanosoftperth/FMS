Namespace DataObjects

    Public Class CanDataPoint

#Region "properties & constructors"

        Public Const TAG_STRING_FORMAT As String = "CAN_{0}_{1}"

        Public Property MessageDefinition As DataObjects.CAN_MessageDefinition

        Public Property CanValues As New CanValueList

        Public Sub New()

        End Sub

#End Region


        Public Shared Function GetPoint(SPN As Integer, vehicleid As String, startDate As Date, endDate As Date, _
                                                Optional description As String = "") As DataObjects.CanDataPoint

            Dim retobj As New CanDataPoint

            Dim vehicle As DataObjects.ApplicationVehicle = DataObjects.ApplicationVehicle.GetFromName(vehicleid)


            'get the MessageDefinition
            retobj.MessageDefinition = DataObjects.CAN_MessageDefinition.GetForSPN(SPN, vehicle.CAN_Protocol_Type, description).First

            'get the pi point 
            Dim tagName As String = String.Format(DataObjects.CanDataPoint.TAG_STRING_FORMAT, vehicle.DeviceID, retobj.MessageDefinition.PGN)

            Dim pp As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(tagName)

            'get data from pi for the time period
            Dim pivds As PISDK.PIValues = pp.Data.RecordedValues(startDate, endDate,
                                                                 PISDK.BoundaryTypeConstants.btInside, )

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

            Public Sub CalculateValues(SPN As Integer, msg_def As CAN_MessageDefinition)

                Dim calcMethod As doCalcDelegate = Nothing

                If msg_def.Standard = "Zagro" AndAlso SPN = 1 Then calcMethod = AddressOf zagro_1
                If msg_def.Standard = "j1939" Then calcMethod = AddressOf j1939

                For Each cv As CanValue In Me
                    calcMethod(cv, msg_def)
                Next

            End Sub


            Public Sub unknown_calc(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)
                cv.Value = "not implemented standard / SPN combination"
            End Sub

            Public Sub zagro_1(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)

                Dim rawValue As Int64 = Convert.ToInt64(cv.RawValue, 16)

                Dim b() As Byte = BitConverter.GetBytes(rawValue)

                Dim bytes = b.Reverse()

                Dim int1 As Single = Convert.ToSingle(bytes(0))
                Dim int2 As Single = Convert.ToSingle(bytes(1))

                cv.Value = int1 + (int2 * 256)

            End Sub


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


