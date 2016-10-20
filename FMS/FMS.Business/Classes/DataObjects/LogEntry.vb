
Namespace DataObjects

    Public Class LogEntry

        Public Property ApplicationName As String = "not implemented yet"
        Public Property DeviceID As String
        Public Property TruckName As String
        Public Property Message As String
        Public Property Time As Date



        Public Shared Function GetAllBetweenDates(startdtae As Date, enddate As Date, _
                                                            applicationid As Guid) As List(Of DataObjects.LogEntry)


            startdtae = startdtae.timezoneToPerth
            enddate = enddate.timezoneToPerth

            Dim sdk As New PISDK.PISDK
            Dim piserver As PISDK.Server = sdk.Servers.DefaultServer
            Dim pip As PISDK.PIPoint = piserver.PIPoints("MessagesFromDevices")


            Dim pitStart As New PITimeServer.PITime With {.LocalDate = startdtae}
            Dim pitEnd As New PITimeServer.PITime With {.LocalDate = enddate}

            Dim pips As PISDK.PIValues = pip.Data.RecordedValues(pitStart, pitEnd)

            Dim retlst As New List(Of LogEntry)

            For Each piv As PISDK.PIValue In pips

                Dim le As New LogEntry


                Dim pivStr As String = piv.Value.ToString

                'KH469: Exception first loopTimed out before valid 'GPRMC'.waiting 120 seconds before retry
                'TPG15: time:17/Mar/2016 09:13:56,lat:-32.239331666666665,lng115.82121333333335

                With le

                    Dim devicename As String = pivStr.Split(":")(0)

                    .DeviceID = devicename
                    .Message = pivStr.Replace(devicename & ":", "")
                    .Time = piv.TimeStamp.LocalDate.timezoneToClient

                End With

                retlst.Add(le)

            Next

            Return (From r In retlst Order By r.Time Descending).ToList

        End Function


        Public Sub New()

        End Sub

    End Class



End Namespace
