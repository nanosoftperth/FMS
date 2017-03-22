Imports FMS.Business
Imports FMS.Business.ReportGeneration


Public Class ReportsGenerators
    Public Shared Function GetVehicleSpeedAndDistance_ForGraph(vehicleID As Guid,
                                                                  startDate As Date,
                                                                  endDate As Date) As VehicleSpeedRetObj

        startDate = startDate.timezoneToPerth
        endDate = endDate.timezoneToPerth

        Dim retobj As New VehicleSpeedRetObj

        Dim dateObjects As New List(Of Date)
        If endDate > Now Then endDate = Now


        'get the vehicle
        Dim vehicle As DataObjects.ApplicationVehicle = DataObjects.ApplicationVehicle.GetForID(vehicleID)

        'GET THE DATA FOR SPEED
        Dim tagName = vehicle.DeviceID & "_speed"

        Dim lastGoodValue As Date = SingletonAccess.HistorianServer.PIPoints(tagName).Data.Snapshot().TimeStamp.LocalDate

        Dim pitStart As New PITimeServer.PITime With {.LocalDate = startDate}
        Dim pitEnd As New PITimeServer.PITime With {.LocalDate = lastGoodValue}


        Dim pivs As PISDK.PIValues = SingletonAccess.HistorianServer.PIPoints(tagName).Data.InterpolatedValues(pitStart, pitEnd, 75)

        '.Data.RecordedValues(pitStart, pitEnd, PISDK.BoundaryTypeConstants.btOutside)
        '.Data.RecordedValues(pitStart, pitEnd)

        retobj.SpeedVals = TimeSeriesFloat.getForSpeedVals(pivs, False, True)

        'GET THE DATA FOR DISTANCE
        Dim tagnameDist As String = vehicle.DeviceID & "_DistanceSinceLastVal"

        Dim firstiteration As Boolean = True
        Dim prevVal As PISDK.PIValue = Nothing
        Dim distVals As New List(Of TimeSeriesFloat)

        Dim pit As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(tagnameDist)
        'Dim totalizer As Decimal = 0
        Dim startpitime As New PITimeServer.PITime

        Dim recordedvals As PISDK.PIValues = pit.Data.RecordedValues(pitStart, pitEnd)

        distVals = TimeSeriesFloat.gettValsFromPIVals(recordedvals)
        Dim retdistances As New List(Of TimeSeriesFloat)

        For Each tsv In retobj.SpeedVals

            Dim newval As New TimeSeriesFloat With {.DateVal = tsv.DateVal}

            Dim newVal_Val As Double = (From x In distVals
                                        Where x.DateVal <= tsv.DateVal _
                                        And x.Val >= 0.007
                                        Select x.Val).Sum

            newval.Val = newVal_Val

            retdistances.Add(newval)

        Next

        retobj.DistanceVals = retdistances

        retobj.VehicleName = vehicle.Name

        'i mean, this "should" be moved somewhere else 
        For Each x In retobj.DistanceVals : x.DateVal = x.DateVal.timezoneToClient : Next
        For Each x In retobj.Latitudes : x.DateVal = x.DateVal.timezoneToClient : Next
        For Each x In retobj.Longitudes : x.DateVal = x.DateVal.timezoneToClient : Next
        For Each x In retobj.SpeedVals : x.DateVal = x.DateVal.timezoneToClient : Next

        Return retobj
    End Function
End Class
