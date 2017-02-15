Namespace ReportGeneration

    Public Class ActivityReportLine

        ''' <summary>
        ''' The minimum amount of time a vehicle can be stopped before 
        ''' it is classed as having actually STOPPED.
        ''' This needs to be short enough to detect stopping at the shops
        ''' but long enough to not detect stopping at a set of lights!
        ''' </summary>
        Public Const STOP_TIME_MIN_MINUTES As Integer = 5

        Public Enum ActivityReportLineType
            Stopped_EngineOff = 0
            Stoipped_Idling = 1
            GeoFenceEntry = 2
            GeoFenceExit = 3
            Travelling = 4
        End Enum

        Public Property Distance As Decimal
        Public Property Lat As Decimal
        Public Property Lng As Decimal
        Public Property ActivityReportType As ActivityReportLineType
        Public Property Description As String
        Public Property StartTime As DateTime
        Public Property EndTime As DateTime?
        Public Property ID As Integer
        Public Property DeleteMe As Boolean = False
        Public Property LineItemPic() As Byte()

        Public Sub New()

        End Sub

        Public Shared Function GetFromSpeedAndtimeStr(startDate As Date,
                                                    endDate As Date,
                                                    vehicleID As String) As List(Of ActivityReportLine)

            Return GetFromSpeedAndtime(startDate, endDate, New Guid(vehicleID))

        End Function

        Friend Shared Function GetFromSpeedAndtime(startDate As Date,
                                                    endDate As Date,
                                                    vehicleID As Guid) As List(Of ActivityReportLine)

            startDate = startDate.timezoneToPerth
            endDate = endDate.timezoneToPerth

            Dim retlst As New List(Of ActivityReportLine)
            'get all the speed and distances
            Dim vsro As VehicleSpeedRetObj = ReportGenerator.GetVehicleSpeedAndDistance(vehicleID, startDate, endDate)
            Dim isFirstIteration As Boolean = False
            Dim currActivityReportLine As New ActivityReportLine

            If vsro.SpeedVals.Count = 0 Then Return retlst
            Dim timesinceLastChange As TimeSpan = TimeSpan.FromSeconds(1)

            '#################################################################################################################
            '################## THIS FIRST ITERATION WILL DETERMINE WHEN THE VEHICLE IS MOVING OR           ##################
            '################## RESTING,  THE SECOND ITERATION WILL REMOVE SOME OF THE "RESTING"            ################## 
            '################## ACTIVITYREPORTLINES AS THEY WILL BE WAITING AT LIGHTS OR SOMETHING SIMILLAR ##################
            '#################################################################################################################
            'this is a 0 based collection, but we want to start on the second item
            For i As Integer = 1 To vsro.SpeedVals.Count - 1

                Dim speedobj As TimeSeriesFloat = vsro.SpeedVals(i)
                Dim prevValSpeed As TimeSeriesFloat = vsro.SpeedVals(i - 1)

                Dim timeBetweenThisEventAndLast As TimeSpan = speedobj.DateVal - prevValSpeed.DateVal

                Dim activitySinceLastVal As ActivityReportLine.ActivityReportLineType = GetActionFromTwoSpeedEvents(prevValSpeed, speedobj)

                'if this is the first iteration 
                If i = 1 Then
                    currActivityReportLine.ActivityReportType = activitySinceLastVal
                    currActivityReportLine.StartTime = prevValSpeed.DateVal
                End If

                'if the report type has changed
                If activitySinceLastVal <> currActivityReportLine.ActivityReportType Then

                    currActivityReportLine.EndTime = prevValSpeed.DateVal
                    retlst.Add(currActivityReportLine)

                    currActivityReportLine = New ActivityReportLine
                    currActivityReportLine.ActivityReportType = activitySinceLastVal
                    currActivityReportLine.StartTime = prevValSpeed.DateVal
                End If

            Next

            If currActivityReportLine.EndTime Is Nothing Then retlst.Add(currActivityReportLine)

            '###############################################################
            '##################    SECOND  ITERATION                    ####
            '##################    THIS REMOVES ALL FALSE IDLING        ####
            '###############################################################
            For j As Integer = 1 To retlst.Count - 1

                Dim thisActivityReportLine As ActivityReportLine = retlst(j)
                Dim prevActivityREportLine As ActivityReportLine = retlst(j - 1)
                Dim nextActivityREportLine As ActivityReportLine = If(j < retlst.Count - 1, retlst(j + 1), Nothing)

                If nextActivityREportLine Is Nothing Then Exit For

                'were only interested in when the activity is "stopped" (of any variety)
                If thisActivityReportLine.ActivityReportType = ActivityReportLineType.Travelling Then Continue For

                Dim lengthOfThisAcitvity As TimeSpan = thisActivityReportLine.EndTime - thisActivityReportLine.StartTime

                If lengthOfThisAcitvity.TotalMinutes < STOP_TIME_MIN_MINUTES AndAlso thisActivityReportLine.ActivityReportType = ActivityReportLineType.Stoipped_Idling Then

                    'if this activity is "idling", we cannot know if the previous activity was actually travelling as 
                    'we may have marked things as deleted
                    thisActivityReportLine.DeleteMe = True
                    'nextActivityREportLine.StartTime = prevActivityREportLine.EndTime
                End If

            Next

            '|-------------s-------||-----t-----||------t-----||----s---||---s---||---t---|

            retlst = (From x In retlst Where x.DeleteMe = False).ToList

            'remove joining "travel" sections
            For i As Integer = 1 To retlst.Count - 1

                'if this is "travel" AND the previous is "travel", then 
                'amend this and change previous to delete
                Dim prev As ActivityReportLine = retlst(i - 1)
                Dim current As ActivityReportLine = retlst(i)

                If current.ActivityReportType = ActivityReportLineType.Travelling And _
                                        prev.ActivityReportType = ActivityReportLineType.Travelling Then

                    retlst(i - 1).DeleteMe = True
                    retlst(i).StartTime = retlst(i - 1).StartTime
                End If
            Next

            retlst = (From x In retlst Where x.DeleteMe = False).ToList

            For i As Integer = 1 To retlst.Count - 1
                Dim prev As ActivityReportLine = retlst(i - 1)
                retlst(i).StartTime = prev.EndTime
            Next



            '###############################################################
            '##################    THIRD  ITERATION                     ####
            '##################    ADDS THE DISTANCES AND LAT/LONGS     ####
            '###############################################################
            For i As Integer = 0 To retlst.Count - 1

                Dim x = retlst(i)

                Dim startTime As Date = x.StartTime
                Dim endTime As Date = If(x.EndTime.HasValue, x.EndTime.Value, endDate)

                If x.ActivityReportType = ActivityReportLineType.Travelling Then _
                                            x.Distance = (From y In vsro.TimeSpansWithVals
                                                            Where y.EndDate > startTime _
                                                              AndAlso y.EndDate <= endTime _
                                                                Select y.distance).Sum

                Dim tswv As TimeSpanWithVals = vsro.TimeSpansWithVals.Where(Function(o) o.EndDate = endTime).FirstOrDefault

                If tswv IsNot Nothing Then
                    x.Lat = tswv.end_lat
                    x.Lng = tswv.end_long
                End If

                'deals with the "last" value
                If tswv Is Nothing AndAlso i = retlst.Count - 1 Then

                    If x.ActivityReportType = ActivityReportLineType.Stoipped_Idling Then x.ActivityReportType = ActivityReportLineType.Stopped_EngineOff

                    x.Lat = vsro.TimeSpansWithVals.Last.end_lat
                    x.Lng = vsro.TimeSpansWithVals.Last.end_long

                    x.EndTime = If(endTime > Now, Now, endTime)

                End If

            Next



            Return retlst

        End Function

        Private Shared Function GetActionFromTwoSpeedEvents(prevAction As TimeSeriesFloat, currAction As TimeSeriesFloat) As ActivityReportLine.ActivityReportLineType

            Dim timebetweenvalues As TimeSpan = currAction.DateVal - prevAction.DateVal
            Dim speed As Double = currAction.Val

            If speed < 20 Then

                If timebetweenvalues.TotalSeconds > 30 Then
                    Return ActivityReportLineType.Stopped_EngineOff
                Else
                    Return ActivityReportLineType.Stoipped_Idling
                End If
            Else
                Return ActivityReportLineType.Travelling
            End If

        End Function

    End Class


End Namespace


