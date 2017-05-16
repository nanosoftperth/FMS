
Namespace ReportGeneration

#Region "ret objs"

    Public Class VehicleSpeedRetObj

        Public Property VehicleName As String
        Public Property SpeedVals As New List(Of TimeSeriesFloat)
        Public Property DistanceVals As New List(Of TimeSeriesFloat)
        Public Property Latitudes As New List(Of TimeSeriesFloat)
        Public Property Longitudes As New List(Of TimeSeriesFloat)


        Public Property TimeSpansWithVals As New List(Of TimeSpanWithVals)

        Public Sub New()

        End Sub

    End Class

    Public Class TimeSpanWithVals

        Public Property start_lat As Decimal
        Public Property start_long As Decimal
        Public Property end_lat As Decimal
        Public Property end_long As Decimal
        Public Property speed As Decimal
        Public Property distance As Decimal
        Public Property StartDate As Date
        Public Property EndDate As Date

        Public Sub New()

        End Sub

    End Class

    Public Class TimeSeriesFloat

        Public Property DateVal As DateTime
        Public Property Val As Decimal



        Public Shared Function getForSpeedVals(pivs As PISDK.PIValues,
                                                 Optional totalize As Boolean = False,
                                                 Optional changeNegativeToZeros As Boolean = False)

            Dim retobj As New List(Of TimeSeriesFloat)

            Dim valTot As Double = 0

            Dim prevVal As TimeSeriesFloat = Nothing

            For Each pival As PISDK.PIValue In pivs

                Try

                    Dim val As Double = pival.Value

                    If changeNegativeToZeros AndAlso val < 0 Then val = 0
                    If val < 5 Then val = 0

                    retobj.Add(New TimeSeriesFloat(pival.TimeStamp.LocalDate, val))

                    ' prevVal = New TimeSeriesFloat(pival.TimeStamp.LocalDate, val)

                Catch ex As Exception

                End Try
            Next

            Return retobj

        End Function


        Public Shared Function gettValsFromPIValsAsDict(pivs As PISDK.PIValues,
                                                 Optional totalize As Boolean = False,
                                                 Optional changeNegativeToZeros As Boolean = False,
                                                 Optional ignoreDuplicates As Boolean = True) As Dictionary(Of Date, Decimal)


            Dim datesAlreadyThere As New Dictionary(Of Date, Decimal)

            Dim valTot As Double = 0
            For Each pival As PISDK.PIValue In pivs

                Try

                    Dim thisDate As Date = pival.TimeStamp.LocalDate

                    If ignoreDuplicates AndAlso datesAlreadyThere.ContainsKey(thisDate) Then Continue For

                    Dim val As Double = pival.Value

                    If changeNegativeToZeros AndAlso val < 0 Then val = 0
                    valTot += val

                    If totalize Then val = valTot


                    datesAlreadyThere.Add(thisDate, val)

                Catch ex As Exception

                End Try
            Next

            Return datesAlreadyThere

        End Function

        Public Shared Function gettValsFromPIVals(pivs As PISDK.PIValues,
                                                  Optional totalize As Boolean = False,
                                                  Optional changeNegativeToZeros As Boolean = False,
                                                  Optional ignoreDuplicates As Boolean = True) As List(Of TimeSeriesFloat)

            Dim retobj As New List(Of TimeSeriesFloat)

            Dim datesAlreadyThere As New Dictionary(Of Date, Decimal)

            Dim valTot As Double = 0
            For Each pival As PISDK.PIValue In pivs

                Try

                    Dim thisDate As Date = pival.TimeStamp.LocalDate

                    If ignoreDuplicates AndAlso datesAlreadyThere.ContainsKey(thisDate) Then Continue For

                    Dim val As Double = pival.Value

                    If changeNegativeToZeros AndAlso val < 0 Then val = 0
                    valTot += val

                    If totalize Then val = valTot

                    retobj.Add(New TimeSeriesFloat(pival.TimeStamp.LocalDate, val))

                    If ignoreDuplicates Then datesAlreadyThere.Add(thisDate, val)

                Catch ex As Exception

                End Try
            Next

            Return retobj

        End Function

        Public Sub New(d As Date, v As Decimal)
            Me.DateVal = d
            Me.Val = v
        End Sub

        Public Sub New()

        End Sub
    End Class


#End Region


    Public Class ReportGenerator


        Public Shared Function GetActivityReportLines_ForVehicle_str(startDate As Date,
                                                                endDate As Date,
                                                                vehicleID As String) As List(Of VehicleActivityReportLine)

            'we will not do the timezone change here as it is done in the method we are wrapping  below
            Return GetActivityReportLines_ForVehicle(startDate, endDate, New Guid(vehicleID))

        End Function

        Public Shared Function GetDriverOperatingHours_ForVehicle(startDate As Date,
                                                                endDate As Date,
                                                                vehicleID As Guid?, businessLocation As Guid?) As ReportGeneration.DriverOperatingReportHoursLine.DriverOperatingReportHours

            'Return ReportGeneration.DriverOperatingReportHoursLine.GetTestLines()
            Return ReportGeneration.DriverOperatingReportHoursLine.GetForVehicle(vehicleID, startDate, endDate, businessLocation)

        End Function

        Public Shared Function GetActivityReportLines_ForVehicle(startDate As Date,
                                                                 endDate As Date,
                                                                 vehicleID As Guid?) As List(Of VehicleActivityReportLine)


            startDate = startDate.timezoneToPerth
            endDate = endDate.timezoneToPerth

            'basically, run the GetActivityReportLines_ForDevice for each period the driver was driving that vehicle
            'make sure though that there is a seperate line item when there is vehicle change.
            'REQUIRED COLUMNS: 'Start Time,Distance & Duration,Stop Location,Arrival Time,Idle Duration,Stop Duration,Departure Time
            Dim speedTimes As List(Of ActivityReportLine) = ActivityReportLine.GetFromSpeedAndtime(startDate, endDate, vehicleID)

            Dim lst As List(Of VehicleActivityReportLine) = _
                            FMS.Business.ReportGeneration.VehicleActivityReportLine.GetFromSpeedTimes(speedTimes)

            'change the timezone to the client timezone (for each entry)
            For Each l In lst
                l.ArrivalTime = l.ArrivalTime.timezoneToClient
                l.DepartureTime = l.DepartureTime.timezoneToClient
                l.StartTime = l.StartTime.timezoneToClient

                If l.DepartureTime.HasValue AndAlso l.ArrivalTime.HasValue Then _
                                l.StopDuration = l.DepartureTime.Value - l.ArrivalTime.Value 
            Next

            ' Edited by Aman on 20170516 to validate minimum stop duration (5 Min)             
            lst.RemoveAll(Function(x) x.StopDuration < TimeSpan.FromMinutes(5))
         

            'start time (earliest irst)
            Return lst.OrderBy(Function(u) u.StartTime).ToList

        End Function

        'Friend Shared Function GetVehicleSpeedAndDistance_ForGraph(vehicleID As Guid,
        '                                                           startDate As Date,
        '                                                           endDate As Date) As VehicleSpeedRetObj

        '    startDate = startDate.timezoneToPerth
        '    endDate = endDate.timezoneToPerth

        '    Dim retobj As New VehicleSpeedRetObj

        '    Dim dateObjects As New List(Of Date)
        '    If endDate > Now Then endDate = Now


        '    'get the vehicle
        '    Dim vehicle As DataObjects.ApplicationVehicle = DataObjects.ApplicationVehicle.GetForID(vehicleID)

        '    'GET THE DATA FOR SPEED
        '    Dim tagName = vehicle.DeviceID & "_speed"

        '    Dim lastGoodValue As Date = SingletonAccess.HistorianServer.PIPoints(tagName).Data.Snapshot().TimeStamp.LocalDate

        '    Dim pitStart As New PITimeServer.PITime With {.LocalDate = startDate}
        '    Dim pitEnd As New PITimeServer.PITime With {.LocalDate = lastGoodValue}


        '    Dim pivs As PISDK.PIValues = SingletonAccess.HistorianServer.PIPoints(tagName).Data.InterpolatedValues(pitStart, pitEnd, 75)

        '    '.Data.RecordedValues(pitStart, pitEnd, PISDK.BoundaryTypeConstants.btOutside)
        '    '.Data.RecordedValues(pitStart, pitEnd)

        '    retobj.SpeedVals = TimeSeriesFloat.getForSpeedVals(pivs, False, True)

        '    'GET THE DATA FOR DISTANCE
        '    Dim tagnameDist As String = vehicle.DeviceID & "_DistanceSinceLastVal"

        '    Dim firstiteration As Boolean = True
        '    Dim prevVal As PISDK.PIValue = Nothing
        '    Dim distVals As New List(Of TimeSeriesFloat)

        '    Dim pit As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(tagnameDist)
        '    'Dim totalizer As Decimal = 0
        '    Dim startpitime As New PITimeServer.PITime

        '    Dim recordedvals As PISDK.PIValues = pit.Data.RecordedValues(pitStart, pitEnd)

        '    distVals = TimeSeriesFloat.gettValsFromPIVals(recordedvals)
        '    Dim retdistances As New List(Of TimeSeriesFloat)

        '    For Each tsv In retobj.SpeedVals

        '        Dim newval As New TimeSeriesFloat With {.DateVal = tsv.DateVal}

        '        Dim newVal_Val As Double = (From x In distVals
        '                                    Where x.DateVal <= tsv.DateVal _
        '                                    And x.Val >= 0.007
        '                                    Select x.Val).Sum

        '        newval.Val = newVal_Val

        '        retdistances.Add(newval)

        '    Next

        '    retobj.DistanceVals = retdistances

        '    retobj.VehicleName = vehicle.Name

        '    'i mean, this "should" be moved somewhere else 
        '    For Each x In retobj.DistanceVals : x.DateVal = x.DateVal.timezoneToClient : Next
        '    For Each x In retobj.Latitudes : x.DateVal = x.DateVal.timezoneToClient : Next
        '    For Each x In retobj.Longitudes : x.DateVal = x.DateVal.timezoneToClient : Next
        '    For Each x In retobj.SpeedVals : x.DateVal = x.DateVal.timezoneToClient : Next

        '    Return retobj
        'End Function

        Friend Shared Function GetVehicleSpeedAndDistance(vehicleID As Guid,
                                                               startDate As Date,
                                                               endDate As Date) As VehicleSpeedRetObj

            Dim retobj As New VehicleSpeedRetObj

            Dim dateObjects As New List(Of Date)
            If endDate > Now Then endDate = Now


            'get the vehicle
            Dim vehicle As DataObjects.ApplicationVehicle = DataObjects.ApplicationVehicle.GetForID(vehicleID)

            retobj.VehicleName = vehicle.Name

            'GET THE DATA FOR SPEED & DISTANCE
            Dim speedTagName = vehicle.DeviceID & "_speed"
            Dim distTagName As String = vehicle.DeviceID & "_Distance"
            Dim latTagName As String = vehicle.DeviceID & "_lat"
            Dim longTagName As String = vehicle.DeviceID & "_long"

            Dim lastGoodValue As Date = SingletonAccess.HistorianServer.PIPoints(speedTagName).Data.Snapshot().TimeStamp.LocalDate

            If lastGoodValue > endDate Then lastGoodValue = endDate

            Dim pitStart As New PITimeServer.PITime With {.LocalDate = startDate}
            Dim pitEnd As New PITimeServer.PITime With {.LocalDate = lastGoodValue}

            Dim speed_pivs As PISDK.PIValues = SingletonAccess.HistorianServer.PIPoints(speedTagName).Data.RecordedValues(pitStart, pitEnd, PISDK.BoundaryTypeConstants.btInside, )
            Dim dist_pivs As PISDK.PIValues = SingletonAccess.HistorianServer.PIPoints(distTagName).Data.RecordedValues(pitStart, pitEnd, PISDK.BoundaryTypeConstants.btInside)
            Dim lat_pivs As PISDK.PIValues = SingletonAccess.HistorianServer.PIPoints(latTagName).Data.RecordedValues(pitStart, pitEnd, PISDK.BoundaryTypeConstants.btInside)
            Dim lng_pivs As PISDK.PIValues = SingletonAccess.HistorianServer.PIPoints(longTagName).Data.RecordedValues(pitStart, pitEnd, PISDK.BoundaryTypeConstants.btInside)


            retobj.SpeedVals = TimeSeriesFloat.getForSpeedVals(speed_pivs, False, True)
            'get the other value as dictionaries for speedy lookups
            Dim DistanceValsDict As Dictionary(Of Date, Decimal) = TimeSeriesFloat.gettValsFromPIValsAsDict(dist_pivs, False, True)
            Dim LatitudesDict As Dictionary(Of Date, Decimal) = TimeSeriesFloat.gettValsFromPIValsAsDict(lat_pivs, False, False)
            Dim LongitudesDict As Dictionary(Of Date, Decimal) = TimeSeriesFloat.gettValsFromPIValsAsDict(lng_pivs, False, False)

            For i As Integer = 0 To retobj.SpeedVals.Count - 1

                Try
                    Dim curr_SpeedVal As TimeSeriesFloat = retobj.SpeedVals(i)
                    Dim x As New TimeSpanWithVals

                    x.speed = curr_SpeedVal.Val
                    x.EndDate = curr_SpeedVal.DateVal

                    'not happy about this but was causing issues in very few instances
                    If Not LatitudesDict.ContainsKey(curr_SpeedVal.DateVal) Then Continue For
                    If Not LongitudesDict.ContainsKey(curr_SpeedVal.DateVal) Then Continue For

                    'get the lat and long values
                    x.distance = If(DistanceValsDict.ContainsKey(curr_SpeedVal.DateVal), DistanceValsDict.Item(curr_SpeedVal.DateVal), 0)

                    x.end_lat = LatitudesDict.Item(curr_SpeedVal.DateVal)
                    x.end_long = LongitudesDict.Item(curr_SpeedVal.DateVal)

                    If i > 0 Then
                        Dim prev_SpeedVal As TimeSeriesFloat = retobj.SpeedVals(i - 1)
                        x.StartDate = prev_SpeedVal.DateVal

                        If Not LatitudesDict.ContainsKey(prev_SpeedVal.DateVal) OrElse Not LongitudesDict.ContainsKey(prev_SpeedVal.DateVal) Then
                            Continue For
                        End If


                        x.start_lat = LatitudesDict.Item(prev_SpeedVal.DateVal)
                        x.start_long = LongitudesDict.Item(prev_SpeedVal.DateVal)
                    End If

                    retobj.TimeSpansWithVals.Add(x)


                Catch ex As Exception
                    Throw
                End Try

            Next
            Return retobj
        End Function 
    End Class

End Namespace