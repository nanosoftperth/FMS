Imports FMS.Business.DataObjects.CanDataPoint

Namespace DataObjects
    Public Class DataLoggerReport
        Public Property DeviceId As String
        Public Property Standard As String
        Public Property Spn As Integer
        Public Property StartDate As Date
        Public Property EndDate As Date

        Public Shared Function Get7502DataLogger(deviceid As String, startDate As Date, endDate As Date) As CanValue
            'Dim param As New DataLoggerReport
            'param.DeviceId = deviceid
            'param.Standard = "Zagro125"
            'param.Spn = 1
            'param.StartDate = startDate
            'param.EndDate = endDate
            'Dim speedList = GetDataLogger(param) 'pgn:578, spn:1, speed

            'Dim param1 As New DataLoggerReport
            'param1.DeviceId = deviceid
            'param1.Standard = "Zagro125"
            'param1.Spn = 2
            'param1.StartDate = startDate
            'param1.EndDate = endDate
            'Dim brakeList = GetDataLogger(param1) 'pgn:578, spn:2, parking brake

            'Dim param2 As New DataLoggerReport
            'param2.DeviceId = deviceid
            'param2.Standard = "Zagro125"
            'param2.Spn = 5
            'param2.StartDate = startDate
            'param2.EndDate = endDate
            'Dim hornList = GetDataLogger(param2) 'pgn:645, spn:5, horn

            Dim loggerData As List(Of String) = GetDeviceDataLogger(deviceid, startDate, endDate)
            Dim lstSpeed As New List(Of String)
            Dim lstHeadlights As New List(Of String)
            For Each Data As String In loggerData
                If Data.IndexOf("Current vehicle Speed is") > 0 Then
                    lstSpeed.Add(Data)
                End If
                If Data.IndexOf("Headlights") > 0 Then
                    lstHeadlights.Add(Data)
                End If
            Next
            Return Nothing
        End Function
        Private Shared Function GetDeviceDataLogger(DeviceID As String, sDate As Date, eDate As Date) As List(Of String)
            Dim lstRetVal As New List(Of String)
            Try
                Dim startDate As Date = sDate
                Dim endDate As Date = eDate

                Dim pipName As String = DeviceID & "_log"

                Dim pit As New PITimeServer.PITime With {.LocalDate = Now}

                Dim pp As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(pipName)

                Dim pivds = SingletonAccess.HistorianServer.PIPoints(pipName).Data.RecordedValues(startDate, endDate, PISDK.BoundaryTypeConstants.btInside)


                For Each p As PISDK.PIValue In pivds
                    lstRetVal.Add(p.Value.ToString())
                Next
            Catch ex As Exception
                lstRetVal.Add(ex.Message.ToString)
            End Try
            Return lstRetVal
        End Function
        Private Shared Function GetDataLogger(reportParam As DataLoggerReport) As List(Of CanValue)
            Dim lstCanDataPoint As New List(Of CanValue)
            Dim retobj As New CanDataPoint

            Dim vehicle As DataObjects.ApplicationVehicle = DataObjects.ApplicationVehicle.GetFromDeviceID(reportParam.DeviceId)

            'get the MessageDefinition
            retobj.MessageDefinition = DataObjects.CAN_MessageDefinition.GetForSPN(reportParam.Spn, reportParam.Standard)

            ' Format = CAN_DeviceID_CanStandard_PGN (eg: CAN_demo01_Zagro125_255)
            Dim tagName As String = DataObjects.CanDataPoint.GetTagName(vehicle.DeviceID, reportParam.Standard,
                                                                                retobj.MessageDefinition.PGN)

            Try

                Dim pp As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(tagName)

                'Dim startDate As Date = Date.Now.AddDays(1).ToShortDateString
                'Dim endDate As Date = Date.Now.AddDays(-365).ToShortDateString
                Dim startDate As Date = reportParam.StartDate
                Dim endDate As Date = reportParam.EndDate
                Dim intCount As Integer = 1
                'get data from pi for the time period
                Dim pivds As PISDK.PIValues = pp.Data.RecordedValues(startDate, endDate,
                                                                     PISDK.BoundaryTypeConstants.btInside)
                'get the latest data from pi from current date backwards until it gets data
                While pivds.Count = 0
                    intCount += 1
                    Dim eDate As Date = startDate.AddDays(-intCount).ToShortDateString
                    pivds = pp.Data.RecordedValues(startDate, eDate, PISDK.BoundaryTypeConstants.btInside)
                End While

                For Each p As PISDK.PIValue In pivds
                    Try
                        retobj.CanValues.Add(New CanValue With {.Time = p.TimeStamp.LocalDate,
                                                                                .RawValue = p.Value})
                        'Calculate the actual value from the raw values
                        retobj.CanValues.CalculateValues(reportParam.Spn, retobj.MessageDefinition)

                    Catch ex As Exception
                    End Try
                Next
            Catch ex As Exception
                retobj.MessageDefinition = New FMS.Business.DataObjects.CAN_MessageDefinition()
            End Try

            Return retobj.CanValues
        End Function

    End Class
End Namespace

