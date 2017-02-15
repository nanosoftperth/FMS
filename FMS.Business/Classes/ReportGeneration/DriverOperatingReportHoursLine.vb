
Namespace ReportGeneration


    Public Class DriverOperatingReportHoursLine

        Public Property DayDate As Date
        Public Property LeftHomeDate As Date
        Public Property HQArrival As Date
        Public Property HQLeave As Date
        Public Property ArriveHome As Date
        Public Property StopTime As TimeSpan
        Public Property TotalHours As TimeSpan
        Public Property ContainsData As Boolean = False

        Public Sub New()

        End Sub



        Public Shared Function GetTestLines() As List(Of DriverOperatingReportHoursLine)

            Dim startDate As Date = CDate("01 jan 2017")

            Dim retobj As New List(Of ReportGeneration.DriverOperatingReportHoursLine)

            For i As Integer = 1 To 20

                Dim randNum As Integer = (New Random).Next(30)

                retobj.Add(New ReportGeneration.DriverOperatingReportHoursLine With {.DayDate = startDate.AddDays(i) _
                                                                                        , .ArriveHome = .DayDate.AddHours(randNum) _
                                                                                    , .HQArrival = startDate.AddHours(9) _
                                                                                    , .HQLeave = startDate.AddHours(17) _
                                                                                    , .TotalHours = TimeSpan.FromHours(10) _
                                                                                    , .StopTime = TimeSpan.FromHours((New Random).Next(10))
                                                                                    })

            Next

            Return retobj

        End Function


        Public Shared Function GetForVehicle(vehicleID As Guid,
                                             startDate As Date, endDate As Date) As List(Of ReportGeneration.DriverOperatingReportHoursLine)

            If startDate >= endDate Then Throw New Exception("The start date must be before the end date.")

            Dim retlst As New List(Of ReportGeneration.DriverOperatingReportHoursLine)

            'get a list of days which will be a line item each in the report.
            Dim reportDays As New List(Of Date)

            Dim d As Date = startDate
            While d < endDate
                reportDays.Add(d) : d = d.AddDays(1)
            End While

            'Get the values from the vehicle report lines report, this report gives all stops done by the vehicle (takes some time)
            Dim vehicleReportLines As List(Of FMS.Business.ReportGeneration.VehicleActivityReportLine) = _
                                FMS.Business.ReportGeneration.ReportGenerator.GetActivityReportLines_ForVehicle( _
                                                                                        startDate, endDate, vehicleID)

            'get the driver of the vehicle for that time, if there is none, then use the "base" of the logged in user. 
            Dim businessLocation As DataObjects.ApplicationLocation = _
                                DataObjects.ApplicationLocation.GetLocationFromVehicle(startDate, endDate, vehicleID)

            Dim businessLocLatLng As New BackgroundCalculations.Loc(businessLocation.Lattitude, businessLocation.Longitude)

            'fiter out any values without a start time (so we can do below filtering between startdate and enddate)
            vehicleReportLines = (From x In vehicleReportLines Where x.StartTime.HasValue Select x).ToList()

            For Each loopDate As Date In reportDays

                Dim loopReptLine As New ReportGeneration.DriverOperatingReportHoursLine
                Dim loopDateStart As Date = New Date(loopDate.Year, loopDate.Month, loopDate.Day)
                Dim loopDateEnd As Date = loopDateStart.AddDays(1)

                loopReptLine.DayDate = loopDate

                'get the vehicle report lines which happened on the day were interested in
                Dim loopVehicleReportLines As List(Of VehicleActivityReportLine) = (From x In vehicleReportLines
                                                                                     Where x.StartTime >= loopDateStart _
                                                                                     And x.StartTime <= loopDateEnd).ToList()

                'get the time the driver left his house as the first entry for that date.
                'get the arrival home date as the last arrival time for that date.
                If loopVehicleReportLines.Count > 0 Then
                    loopReptLine.LeftHomeDate = loopVehicleReportLines.First.StartTime
                    loopReptLine.ArriveHome = loopVehicleReportLines(loopVehicleReportLines.Count - 1).ArrivalTime
                    loopReptLine.ContainsData = True
                End If

                Dim foundHQArrival As Boolean = False
                Dim foundHQLeave As Boolean = False

                For Each vrl In loopVehicleReportLines

                    If Not foundHQArrival Then
                        'find the "entry" location
                        foundHQArrival = BackgroundCalculations.GeoFenceCalcs.isPointInCircle(businessLocLatLng, 500, vrl.Loc)
                        If foundHQArrival Then loopReptLine.HQArrival = vrl.ArrivalTime.Value
                    End If

                    'the next entry here will show when the vehicle has left the area
                    If foundHQArrival And Not foundHQLeave Then
                        'if we're still too close to the HQ, then do nothing 
                        If BackgroundCalculations.GeoFenceCalcs.isPointInCircle(businessLocLatLng, 100, vrl.Loc) Then Continue For
                        loopReptLine.HQLeave = vrl.StartTime.Value
                        foundHQLeave = True
                    End If

                    If foundHQArrival AndAlso foundHQLeave Then
                        loopReptLine.StopTime = loopReptLine.HQLeave - loopReptLine.HQArrival
                        Exit For
                    End If

                Next

                loopReptLine.TotalHours = loopReptLine.ArriveHome - loopReptLine.LeftHomeDate
                retlst.Add(loopReptLine)

            Next

            Return retlst


        End Function


    End Class


End Namespace