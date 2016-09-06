Namespace DebugObjects

    Public Class DeviceEvent

        Public Property DeviceID As String
        Public Property StartTime As DateTime
        Public Property Speed As Decimal
        Public Property Endtime As DateTime
        Public Property Distance As Decimal
        Public Property Cumulativedistance As Decimal
        Public Shared Property ApplicationID As Guid?
        Public Shared Property TruckName As String


        Public Sub New()

        End Sub

        Public Shared Function GetDistanceVals() As List(Of DeviceEvent)

            Dim retlst As New List(Of DeviceEvent)

            Return retlst
        End Function

        Public Shared Function GetDistanceVals(starttime As Date, endtime As Date, deviceid As String) As List(Of DeviceEvent)

            'foundVehicle.GetVehicleMovementSummary(startDate, loopEndDate)
            Dim device = Business.DataObjects.Device.GetFromDeviceID(deviceid)

            'get the application associated with the truck
            Dim app = Business.DataObjects.Application.GetFromAppID(device.ApplicatinID)

            'get all the vehicles associated with that truck
            Dim vehicles = Business.DataObjects.ApplicationVehicle.GetAll(app.ApplicationID)

            Dim foundVehicle = vehicles.Where(Function(x) x.DeviceID = device.DeviceID).Single

            Dim vehicleMovementSummry = foundVehicle.GetVehicleMovementSummary(starttime, endtime)

            Dim distVals = ReportGeneration.ReportGenerator.GetVehicleSpeedAndDistance(foundVehicle.ApplicationVehileID, starttime, endtime)


            Dim retobj As New List(Of DeviceEvent)
            Dim cumulativeDist As Decimal = 0

            For Each distVal In distVals.TimeSpansWithVals.OrderBy(Function(x) x.EndDate).ToList

                cumulativeDist += distVal.distance

                Dim objToAdd As New DeviceEvent

                With objToAdd
                    .Cumulativedistance = cumulativeDist
                    .DeviceID = deviceid
                    .Distance = distVal.distance
                    .StartTime = distVal.StartDate
                    .Endtime = distVal.EndDate
                    .Speed = distVal.speed
                End With

                retobj.Add(objToAdd)

            Next

            Return retobj

        End Function

    End Class

End Namespace
