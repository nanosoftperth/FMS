Imports FMS.Business.ReportGeneration
Imports FMS.Business.ReportGeneration.ActivityReportLine

Namespace ReportGeneration
    Public Class VehicleActivityReportObject


        'REQUIRED COLUMNS: 'Start Time,Distance & Duration,Stop Location,Arrival Time,Idle Duration,Stop Duration,Departure Time
        Public Property VehicleActivityReportTimes As New List(Of VehicleActivityReportLine)

        Public Property TotalDistanceTravelled As Double


        Public Sub New()

        End Sub


    End Class

    Public Class VehicleActivityReportLine

        Public Property DriverName As String
        Public Property StopLocation As String
        Public Property Lat As Decimal 'BYRYAN
        Public Property Lng As Decimal 'BYRYAN
        Public Property StartTime As DateTime?
        Public Property ArrivalTime As New DateTime?
        Public Property DepartureTime As DateTime?
        Public Property IdleDuration As TimeSpan
        Public Property StopDuration As New TimeSpan
        Public Property EngineOffDuration As New TimeSpan
        Public Property DistanceKMs As Decimal

        Public ReadOnly Property Loc As Business.BackgroundCalculations.Loc
            Get
                Return New Business.BackgroundCalculations.Loc(Me.Lat, Me.Lng)
            End Get
        End Property

#Region "read only properties (mostly for formatting, dev express forms causing issues)"

        Private Const dateFormat As String = "dd/MMM/yyyy HH:mm:ss tt"
        'Private Const timeFormat As String = "dd\.hh\:mm\:ss"

        Private Function timespanFormatCust(t As TimeSpan) As String

            Dim hours As Integer = t.TotalHours
            Dim minutes As Integer = t.Minutes
            Dim seconds As Integer = t.Seconds

            Dim formatStr As String = "{0:00}:{1:00}:{2:00}"

            Return String.Format(formatStr, hours, minutes, seconds)

        End Function

        Public ReadOnly Property formatted_StartTime As String
            Get
                If StartTime.HasValue Then
                    Return StartTime.Value.ToString(dateFormat)
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public ReadOnly Property formatted_ArrivalTime As String
            Get
                If ArrivalTime.HasValue Then
                    Return ArrivalTime.Value.ToString(dateFormat)
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public ReadOnly Property formatted_DpartureTime As String
            Get
                If DepartureTime.HasValue Then
                    Return DepartureTime.Value.ToString(dateFormat)
                Else
                    Return String.Empty
                End If
            End Get
        End Property

        Public ReadOnly Property formatted_IdleDuration As String
            Get
                Return timespanFormatCust(IdleDuration)
            End Get
        End Property

        Public ReadOnly Property formatted_StopDuation As String
            Get
                Return timespanFormatCust(StopDuration)
            End Get
        End Property

        Public ReadOnly Property formatted_EngineOffFDuration As String
            Get
                Return timespanFormatCust(EngineOffDuration)
            End Get
        End Property


#End Region

        ''' <summary>
        ''' internal property used whilst doing calculations, please ignore outside of this context
        ''' </summary>
        Friend Property IsNew As Boolean = True

        Friend Shared Function GetFromSpeedTimes(speedTimes As List(Of ActivityReportLine)) As List(Of VehicleActivityReportLine)

            Dim count As Integer = speedTimes.Count
            Dim retobjs As New List(Of VehicleActivityReportLine)
            Dim loopVARL As New VehicleActivityReportLine

            'Starttime is the "beginning of  travelling"
            'Distance is the distance of the travelling
            'Arrival time is end of the travelling time
            'Idle Duration is the time, when stopped, idling
            'departure time is the start time of the "next" travelling event (outside of this one)

            '(t:travfelling, si:stopped(idling), ss(stopped, no engine)
            '   -----t---si---ss----si---ss--si---t 
            '   ----|----------------------------|-------

            For i As Integer = 1 To speedTimes.Count - 2

                Dim s_prev As ActivityReportLine = speedTimes(i - 1)
                Dim s_current As ActivityReportLine = speedTimes(i)
                Dim s_next As ActivityReportLine = speedTimes(i + 1)

                Dim timeInStatus As TimeSpan = s_current.EndTime - s_prev.StartTime
                Dim startNewLoopVarlAfterThis As Boolean = False

                'I have moved the if/then/else logic out of the statement for easier reading.
                Dim wasPreviouslyTravelling As Boolean = s_prev.ActivityReportType = ActivityReportLineType.Travelling ' Or i = 1
                Dim isCurrentlyStopped As Boolean = s_current.ActivityReportType = ActivityReportLineType.Stoipped_Idling Or s_current.ActivityReportType = ActivityReportLineType.Stopped_EngineOff
                Dim isfirstLoopAndFirstValWasStopped As Boolean = (i = 1) AndAlso (s_prev.ActivityReportType = ActivityReportLineType.Stoipped_Idling Or s_prev.ActivityReportType = ActivityReportLineType.Stopped_EngineOff)

                'if this is the first time weve stopped, then find out the address
                'if (in the very first place) we were already stopped, get that address
                If wasPreviouslyTravelling And isCurrentlyStopped Then
                    loopVARL.StopLocation = FMS.Business.GoogleGeoCodeResponse.GetForLatLong(s_current.Lat, s_current.Lng)
                    'BY RYAN
                    loopVARL.Lat = s_current.Lat
                    loopVARL.Lng = s_current.Lng
                End If


                If isfirstLoopAndFirstValWasStopped Then
                    loopVARL.StopLocation = FMS.Business.GoogleGeoCodeResponse.GetForLatLong(s_prev.Lat, s_prev.Lng)
                    'BY RYAN
                    loopVARL.Lat = s_prev.Lat
                    loopVARL.Lng = s_prev.Lng
                End If

                'If we are stopped, then add to the correct total
                Select Case s_current.ActivityReportType
                    Case ActivityReportLineType.Stoipped_Idling
                        loopVARL.IdleDuration += timeInStatus
                        loopVARL.StopDuration += timeInStatus
                    Case ActivityReportLineType.Stopped_EngineOff
                        loopVARL.StopDuration += timeInStatus
                        loopVARL.EngineOffDuration += timeInStatus
                    Case Else
                        'do nothing 
                End Select

                'If the previous value is a "travelling" one, then from this we can derive the 
                'arrival time and the start time
                If s_prev.ActivityReportType = ActivityReportLineType.Travelling Then

                    'Need to determine the distance travelled here 
                    loopVARL.DistanceKMs = s_prev.Distance

                    loopVARL.ArrivalTime = s_prev.EndTime
                    loopVARL.StartTime = s_prev.StartTime
                End If

                'if the next value is travelling, then that is when we leave, so this is the departure time.
                'Also, this is when we should get the address of our latlongs from google API
                If s_next.ActivityReportType = ActivityReportLineType.Travelling Then
                    'set the departure time
                    loopVARL.DepartureTime = s_next.StartTime

                    'Add the object, then set the current object to NEW (as in to start again)
                    retobjs.Add(loopVARL)
                    loopVARL = New VehicleActivityReportLine

                Else

                    If i = speedTimes.Count - 2 Then

                        If s_next.ActivityReportType = ActivityReportLineType.Stopped_EngineOff _
                                                Or s_next.ActivityReportType = ActivityReportLineType.Stoipped_Idling Then

                            loopVARL.StartTime = s_current.StartTime
                            loopVARL.StopLocation = FMS.Business.GoogleGeoCodeResponse.GetForLatLong(s_next.Lat, s_next.Lng)
                            'BY RYAN
                            loopVARL.Lat = s_next.Lat
                            loopVARL.Lng = s_next.Lng
                            loopVARL.ArrivalTime = s_next.StartTime
                            loopVARL.DepartureTime = s_next.EndTime

                            loopVARL.DistanceKMs = s_current.Distance

                            loopVARL.StopDuration = loopVARL.DepartureTime - loopVARL.ArrivalTime

                            retobjs.Add(loopVARL)

                        End If

                    End If

                End If
            Next

            'HACK: unsure why these numbers do not add up using the method above.
            'For Each varl As ReportGeneration.VehicleActivityReportLine In retobjs

            '    Try

            '        Dim ratio As Decimal = varl.IdleDuration.TotalMilliseconds / varl.StopDuration.TotalMilliseconds

            '        varl.StopDuration = varl.DepartureTime.Value - varl.ArrivalTime.Value
            '        varl.IdleDuration = TimeSpan.FromMilliseconds(varl.StopDuration.TotalMilliseconds * ratio)

            '    Catch ex As Exception

            '    End Try
            'Next

            Return retobjs

        End Function

        Public Sub New()

            StopDuration = TimeSpan.FromSeconds(0)
            IdleDuration = TimeSpan.FromSeconds(0)
            EngineOffDuration = TimeSpan.FromSeconds(0)

        End Sub


    End Class

End Namespace
