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
                                                                 PISDK.BoundaryTypeConstants.btInside)

            For Each p As PISDK.PIValue In pivds

                Try
                    Dim dbl As Double

                    If Double.TryParse(p.Value, dbl) Then

                        retobj.CanValues.Add(New CanValue With {.Time = p.TimeStamp.LocalDate,
                                                                                .RawValue = p.Value})

                    End If

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


            Public Delegate Function doCalcDelegate(ByRef cv As CanValue, msg_def As CAN_MessageDefinition) As Object

            Public Sub CalculateValues(SPN As Integer, msg_def As CAN_MessageDefinition)

                Dim calcMethod As doCalcDelegate = Nothing

                If msg_def.Standard = "Zagro" AndAlso SPN = 1 Then calcMethod = AddressOf zagro_1
                If msg_def.Standard = "j1939" AndAlso SPN = 190 Then calcMethod = AddressOf j1939_190

                For Each cv As CanValue In Me
                    calcMethod(cv, msg_def)
                Next

            End Sub


            Public Function unknown_calc(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)
                Return "not implemented standard / SPN combination"
            End Function

            Public Function zagro_1(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)

                Dim b() As Byte = BitConverter.GetBytes(cv.RawValue)

                Dim bytes = b.Reverse()

                Dim int1 As Single = Convert.ToSingle(bytes(0))
                Dim int2 As Single = Convert.ToSingle(bytes(1))

                Return int1 + (int2 * 256)

            End Function

            Public Function j1939_190(ByRef cv As CanValue, msg_def As CAN_MessageDefinition)

                Dim b() As Byte = BitConverter.GetBytes(cv.RawValue)

                Dim bytes = b.Reverse()

                Dim int1 As Single = Convert.ToSingle(bytes(0))
                Dim int2 As Single = Convert.ToSingle(bytes(1))

                Return int1 + (int2 * 256)

            End Function


        End Class


    End Class

End Namespace


