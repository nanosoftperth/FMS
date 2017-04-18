Namespace DataObjects

    Public Class CanDataPoint

#Region "properties & constructors"

        Public Const TAG_STRING_FORMAT As String = "CAN_{0}_{1}"

        Public Property MessageDefinition As DataObjects.CAN_MessageDefinition

        Public Property CanValues As New CanValueList

        Public Sub New()

        End Sub

#End Region


        Public Shared Function GetPoint(SPN As Integer, vehicleid As String, _
                                                Optional description As String = "") As DataObjects.CanDataPoint

            Dim retobj As New CanDataPoint

            Dim vehicle As DataObjects.ApplicationVehicle = DataObjects.ApplicationVehicle.GetFromName(vehicleid)


            'get the MessageDefinition
            retobj.MessageDefinition = DataObjects.CAN_MessageDefinition.GetForSPN(SPN, vehicle.CAN_Protocol_Type, description).First

            'get the pi point 
            Dim tagName As String = String.Format(DataObjects.CanDataPoint.TAG_STRING_FORMAT, vehicle.DeviceID, SPN)

            Dim pp As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(tagName)

            'get the last 10 hours worth of values
            Dim pivds As PISDK.PIValues = pp.Data.RecordedValues(Now.AddHours(-10), Now,
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
            retobj.CanValues.CalculateValues(SPN, description)

            'get the value 
            Return retobj

        End Function



        Public Class CanValueList
            Inherits List(Of CanValue)

            Public Sub CalculateValues(SPN As Integer, Optional desc As String = "")

                For Each cv As CanValue In Me
                    cv.CalculateValue(SPN, desc)
                Next

            End Sub


        End Class


    End Class

End Namespace


