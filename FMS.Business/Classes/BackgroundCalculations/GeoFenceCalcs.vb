Imports OSIsoft.AF

Namespace BackgroundCalculations

    Public Class GeoFenceCalcs

        Public Shared ReadOnly Property GEOFENCE_COLLISSION_VALID_REPORT_TIME As TimeSpan
            Get
                Return TimeSpan.FromHours(6)
            End Get
        End Property


        Public Shared Sub ProcessGeoFenceCollissionAlerts(appid As Guid, startDate As Date)

            startDate = startDate.AddHours(-12) 'for "alert if left geofence for more than X" queries
            Dim applicationName As String = DataObjects.Application.GetFromAppID(appid).ApplicationName

            'Get all the geofences from the last time this application was ran 
            Dim geofencecollissions As List(Of DataObjects.GeoFenceDeviceCollision) = _
                                DataObjects.GeoFenceDeviceCollision.GetAllForApplication(appid, startDate)

            If geofencecollissions.Count = 0 Then Exit Sub

            'get all the alerts sent for the same time period
            'TODO: we should filter this by time , this will become slow eventually
            Dim alertTypeOccurances As List(Of DataObjects.AlertTypeOccurance) = DataObjects.AlertTypeOccurance.GetAllForApplication(appid)

            'get all the alert defenitions for the application            
            Dim alertTypes = DataObjects.AlertType.GetAllForApplicationIncludingOpenBookings(appid)

            'get all the subscribers
            Dim subscribers As List(Of DataObjects.Subscriber) = FMS.Business.DataObjects.Subscriber.GetAllforApplication(appid)

            'get all drivers
            Dim drivers As List(Of DataObjects.ApplicationDriver) = DataObjects.ApplicationDriver.GetAllDriversIncludingEveryone(appid)


            'REAPLACE THE CODE BELOW AND ADD IT TO THE BELOW LOOP WITH THE NEW STORED PROCEDURE
            'get all the geofence collissions
            'Dim geoReportRslts As List(Of ReportGeneration.GeoFenceReport_Simple) = _
            '                           ReportGeneration.GeoFenceReport_Simple.GetReport(appid, startDate, Now)

            'for each alert type, get all of the geofence collissions whch have not already been processed
            For Each alertDefn In alertTypes


                'Finds geofence collissions which have not been processed yet for this alert type 
                Dim geoReportRslts As List(Of ReportGeneration.AlertTypeUnprocessedCollission_Report) = _
                                                ReportGeneration.AlertTypeUnprocessedCollission_Report.GetForAlertType( _
                                                                                    alertDefn.ApplicationAlertTypeID, startDate)


                'get the geo-fence COLLISIONS which we are interested in 
                Dim results = geoReportRslts.Where(Function(x) x.ApplicationGeoFenceID = alertDefn.GeoFenceId).ToList

                Dim alertDriver As DataObjects.ApplicationDriver = drivers.Where( _
                                                                Function(d) d.ApplicationDriverID = alertDefn.DriverID).SingleOrDefault

                'if the driver has been deleted, then we cannot process this alert
                If alertDriver Is Nothing Then Continue For

                For Each rslt As ReportGeneration.AlertTypeUnprocessedCollission_Report In results

                    'determine what type of geo-fence result happened
                    Dim timeperiod_mins As Decimal = alertDefn.Time_Period_mins

                    If Not rslt.ApplicationDriverID.HasValue Then rslt.DriverName = "unknown"

                    'check that we are looking for the correct driver
                    Dim isCorrectDriver As Boolean = alertDriver.RepresentsEveryone

                    If Not isCorrectDriver AndAlso rslt.ApplicationDriverID.HasValue Then
                        isCorrectDriver = rslt.ApplicationDriverID.Value = alertDriver.ApplicationDriverID
                    End If

                    'IF the alert defenition has a booking refrerence, then grab that booking so we can grab the arrival time
                    Dim booking As DataObjects.ApplicationBooking = If(alertDefn.BookingID.HasValue, _
                                                                       DataObjects.ApplicationBooking.GetFromID(alertDefn.BookingID.Value), Nothing)


                    'if this is a booking but the booking has since been deleted, then ignore forever (a bit inefficient)
                    If alertDefn.isBooking AndAlso booking Is Nothing Then Continue For

                    'here we want to check that the booking is expected to happen within the next 2 hours. 
                    'If not then we want to ignore for now. This avoide people placing booking for a few days in 
                    'the future and then an alert being fired immediatley
                    If booking IsNot Nothing AndAlso booking.ArrivalTime.Value > Now.AddHours(+2) Then Continue For

                    'DEPENDING IF THIS ALERT WAS FIRED FOR AN "ENTRY" OR "EXIT" EVENT
                    Select Case alertDefn.Action

                        Case DataObjects.AlertType.ActionType.Enters

                            Dim timeInGeoFence As TimeSpan = rslt.TimeTakes
                            Dim timeRequiredToBeInGeoFence As TimeSpan = TimeSpan.FromMinutes(alertDefn.Time_Period_mins)

                            'find out if the geofence was for long enough to fire the event
                            Dim wasInGeoFenceLongEngough As Boolean = timeInGeoFence >= timeRequiredToBeInGeoFence

                            'If the "start-time" has been fired longer than 6 hours ago, then we do not want to fire an alert.
                            Dim withinValidTimeFrame As Boolean = (rslt.StartTime + timeRequiredToBeInGeoFence) > Now - GEOFENCE_COLLISSION_VALID_REPORT_TIME

                            If wasInGeoFenceLongEngough AndAlso isCorrectDriver AndAlso withinValidTimeFrame Then

                                'BY RYAN: if booking, mark as sent to ensure that booking is only ever fired ONCE
                                If booking IsNot Nothing AndAlso alertDefn.isSent = False Then

                                    alertDefn.isSent = True
                                    DataObjects.AlertType.Update(alertDefn)

                                End If

                                ProcessAlertInstances(rslt, alertDefn, alertTypeOccurances, subscribers, applicationName, alertDefn.Action)

                            End If

                        Case DataObjects.AlertType.ActionType.Leaves

                            'if this is a "user has left geofence" action type, then the driver
                            'has to have actually left the geo-fence meaning that there must be a defined endtime.
                            If rslt.EndTime Is Nothing Then Continue For

                            Dim timeOutsideOfGeoFence As TimeSpan = Now - rslt.EndTime
                            Dim timeRequiredToBeOutsideOfGeoFence As TimeSpan = TimeSpan.FromMinutes(alertDefn.Time_Period_mins)

                            Dim hasBeenOutsideForLongEnough As Boolean = timeOutsideOfGeoFence >= timeRequiredToBeOutsideOfGeoFence

                            'If the "end-time" has been fired longer than X hours ago, then we do not want to fire an alert.
                            Dim withinValidTimeFrame As Boolean = (rslt.EndTime + timeRequiredToBeOutsideOfGeoFence) > Now - GEOFENCE_COLLISSION_VALID_REPORT_TIME


                            If hasBeenOutsideForLongEnough AndAlso isCorrectDriver AndAlso withinValidTimeFrame Then

                                'BY RYAN: if booking, mark as sent to ensure that booking is only ever fired ONCE
                                If alertDefn.isBooking And Not alertDefn.isSent Then
                                    alertDefn.isSent = True
                                    DataObjects.AlertType.Update(alertDefn)
                                End If
                                ProcessAlertInstances(rslt, alertDefn, alertTypeOccurances, subscribers, applicationName, alertDefn.Action)
                            End If

                    End Select
                Next
            Next

        End Sub

        Private Shared Sub ProcessAlertInstances(rslt As ReportGeneration.AlertTypeUnprocessedCollission_Report,
                                                         alertDefn As DataObjects.AlertType,
                                                         alertTypeOccurances As List(Of DataObjects.AlertTypeOccurance),
                                                         subscribers As List(Of DataObjects.Subscriber),
                                                         applicationName As String,
                                                         actnType As DataObjects.AlertType.ActionType)


            'find out if there have already been alerts send out for this geo-fence collision
            Dim alreadyFiredAlert = alertTypeOccurances _
                                    .Where(Function(a) a.GeoFenceCollisionID = rslt.GeoFenceCollissoinID _
                                                And a.AlertTypeID = alertDefn.ApplicationAlertTypeID).Count > 0

            Dim thisSubscriber As DataObjects.Subscriber = _
                    subscribers.Where(Function(s) s.NativeID = alertDefn.SubscriberNativeID).SingleOrDefault()


            Dim emailAndTextList = getEmailListFromSubscriber(thisSubscriber, subscribers)

            If Not alreadyFiredAlert Then



                'create the new alert occurance, then send the email/text
                Dim newAlertTypeOccurance As New DataObjects.AlertTypeOccurance

                With newAlertTypeOccurance

                    .AlertTypeID = alertDefn.ApplicationAlertTypeID
                    .AlertTypeOccuranceID = Guid.NewGuid
                    .DateSent = Now
                    .Emails = emailAndTextList.Emails
                    .GeoFenceCollisionID = rslt.GeoFenceCollissoinID
                    .SubscriberNativeID = thisSubscriber.NativeID
                    .SubscriberTypeName = thisSubscriber.NameFormatted
                    .SubscriberTypeStr = thisSubscriber.SubscriberType_Str
                    .Texts = emailAndTextList.Texts
                    .ApplicationGeoFenceID = rslt.ApplicationGeoFenceID
                    .ApplicationGeoFenceName = rslt.GeoFence_Name
                    .DriverName = If(String.IsNullOrEmpty(rslt.DriverName), "Unknown", rslt.DriverName)
                End With


                'send the emails, this returns nothing if there was an exception (dodgy)
                If Not String.IsNullOrEmpty(newAlertTypeOccurance.Emails) Then

                    If alertDefn.isBooking Then


                        newAlertTypeOccurance.MessageContent = BackgroundCalculations.EmailHelper _
                            .SendCarBookingEmail(newAlertTypeOccurance.Emails,
                                                 applicationName,
                                                 thisSubscriber.Name,
                                                 rslt.Driver_FristName,
                                                 rslt.Vehicle_Name,
                                                 rslt.Driver_PhoneNumber, actnType)

                    Else

                        Dim enterOrLeaveTime As Date = If(actnType = DataObjects.AlertType.ActionType.Enters, rslt.StartTime, rslt.EndTime)

                        newAlertTypeOccurance.MessageContent = BackgroundCalculations.EmailHelper _
                            .SendEmail(newAlertTypeOccurance.Emails, applicationName, rslt.DriverName, rslt.GeoFence_Name, enterOrLeaveTime, actnType)

                    End If

                End If

                'send SMS to subscribers with SMS messages
                If Not String.IsNullOrEmpty(newAlertTypeOccurance.Texts) Then _
                    newAlertTypeOccurance.MessageContent &= BackgroundCalculations.EmailHelper _
                        .SendSMS(newAlertTypeOccurance.Texts, applicationName, rslt.DriverName, rslt.GeoFence_Name, rslt.StartTime, actnType)

                'if the email / texts were sent correctly (without exception) , then log the result in the DB
                If Not String.IsNullOrEmpty(newAlertTypeOccurance.MessageContent) Then DataObjects.AlertTypeOccurance.Create(newAlertTypeOccurance)


            End If

        End Sub

        Public Class EmailsAndTextAddresses

            Public Property Emails As String
            Public Property Texts As String

            Public Sub New()

            End Sub

            Public Sub New(e As String, t As String)
                Emails = e
                Texts = t
            End Sub

        End Class

        Public Shared Function getEmailListFromSubscriber(s As DataObjects.Subscriber, subs As List(Of DataObjects.Subscriber)) As EmailsAndTextAddresses

            Dim retobj As New EmailsAndTextAddresses

            If s.SubscriberType <> DataObjects.Subscriber.SubscriberType_Enum.Group Then _
                                                        Return New EmailsAndTextAddresses(s.Email, s.Mobile)

            'if we are looking at a group, then get the subscribers and build the email address list 
            Dim groupSubscriberList As List(Of DataObjects.GroupSubscriber) = DataObjects.Group.GetSubscribers(s.NativeID)

            For Each groupSubscriber As DataObjects.GroupSubscriber In groupSubscriberList

                Dim thisSubscriber As DataObjects.Subscriber = subs.Where(Function(x) x.NativeID = groupSubscriber.NativeID).FirstOrDefault

                If thisSubscriber IsNot Nothing Then
                    If groupSubscriber.SendEmail Then retobj.Emails &= String.Format("{0}{1}", If(String.IsNullOrEmpty(retobj.Emails), "", ";"), thisSubscriber.Email)
                    If groupSubscriber.SendText Then retobj.Texts &= String.Format("{0}{1}", If(String.IsNullOrEmpty(retobj.Texts), "", ";"), thisSubscriber.Mobile)
                End If

            Next

            Return retobj

        End Function

        ''' <summary>
        ''' Processes the geofence collisons, return the earliest date for us 
        ''' to calcluate the new geo-fence alerts
        ''' </summary>
        ''' <returns>Returns the earliest data which any device still had no data for </returns>
        ''' <remarks></remarks>
        Public Shared Function ProcessGeoFenceCollisions(appid As Guid, Optional startDate As Date? = Nothing) As Date

            'Try
            'get the earliest date which we are calculating so that we can perform the geo-fence alert check
            Dim EarliestStartDate As DateTime = Now

            'if we are forcing a new start date, then this is debug mode, so we must delete the already existing geofemce collision alerts
            'WE HAVE TO DO THIS OR ELSE WE WILL GENERATE A DUPLICATE GEO-FENCE ALERT EVERY TIME THIS IS RAN
            Dim forceDelete As Boolean = startDate IsNot Nothing

            'Hook up to AF (this could be moved elsewhere)
            Dim myPISystem As PISystem = New PISystems().DefaultPISystem
            myPISystem.Connect()
            Dim afdb As AFDatabase = myPISystem.Databases("FMS")

            'Get ALL devices from AF (for all applications)
            Dim afnamedcoll As AFNamedCollection(Of Asset.AFElement) =
                OSIsoft.AF.Asset.AFElement.FindElements(afdb, Nothing, "device", _
                            AFSearchField.Template, True, AFSortField.Name, AFSortOrder.Ascending, 1000)


            'If startDateTime = Nothing Then startDateTime = GetStartTime()

            'Get the application from the appid provided to the method
            Dim app As FMS.Business.DataObjects.Application = FMS.Business.DataObjects.Application.GetFromAppID(appid)

            Dim geofences As List(Of FMS.Business.DataObjects.ApplicationGeoFence) = _
                        FMS.Business.DataObjects.ApplicationGeoFence.GetAllApplicationGeoFences(app.ApplicationID)

            For Each devicename As String In app.GetAllDevicesNames

                Dim afe As Asset.AFElement = (From x In afnamedcoll Where x.Name = devicename).SingleOrDefault

                'if we cannot find the element in the list above, then move on (not created yet, should probably log this)
                If afe Is Nothing Then Continue For

                'Dim attr_DistancesinceLastVal As Asset.AFAttribute = afe.Attributes("DistanceSinceLastVal")
                'Dim attr_EngineState As Asset.AFAttribute = afe.Attributes("EngineState")
                Dim attr_Lat As Asset.AFAttribute = afe.Attributes("lat")
                Dim attr_Long As Asset.AFAttribute = afe.Attributes("long")
                Dim attr_Speed As Asset.AFAttribute = afe.Attributes("speed")
                'Dim attr_TotalDistanceTravelled As Asset.AFAttribute = afe.Attributes("TotalDistanceTravelled")
                Dim attr_LastGeoFenceCalc As Asset.AFAttribute = afe.Attributes("LastGeoFenceCalc")


                Dim thisLoopStartDate As Date

                If startDate IsNot Nothing Then
                    thisLoopStartDate = startDate.Value
                    'TODO: we should delete all the geo-fence data for this device from thie start date here
                Else

                    Dim afv As OSIsoft.AF.Asset.AFValue = attr_LastGeoFenceCalc.GetValue
                    'if we dont have an overriding startDate AND there is no good value for 
                    'the last query, then delete 
                    thisLoopStartDate = If(afv.IsGood, afv.Timestamp.LocalTime, Now.AddMonths(-1))
                End If

                'get the earliest date which we are calculating so that we can perform the geo-fence alert check
                If thisLoopStartDate <= EarliestStartDate Then EarliestStartDate = thisLoopStartDate

                'delete records we are about to reprocess
                If forceDelete Then FMS.Business.DataObjects.GeoFenceDeviceCollision.DeleteForTimeRange(devicename, thisLoopStartDate, Now)

                Dim endDateOverall As Date = Now
                Dim startDateOverall As Date = thisLoopStartDate
                Dim startAndEndDates As New List(Of KeyValuePair(Of Date, Date))
                Dim loopTimePeriod As TimeSpan = TimeSpan.FromDays(1)

                Dim e As Date = startDateOverall
                Dim s As Date = startDateOverall

                While e < endDateOverall

                    e += loopTimePeriod

                    If e > endDateOverall Then e = endDateOverall

                    startAndEndDates.Add(New KeyValuePair(Of Date, Date)(s, e))

                    s = e
                End While

                For Each kvp As KeyValuePair(Of Date, Date) In startAndEndDates.OrderBy(Function(x) x.Key).ToList

                    Dim st As New OSIsoft.AF.Time.AFTime(kvp.Key)
                    Dim et As New OSIsoft.AF.Time.AFTime(kvp.Value)
                    Dim tr As New OSIsoft.AF.Time.AFTimeRange(st, et)

                    Dim dateStrFormat As String = "dd/MMM/yyyy HH:mm:ss"

                    'Console.WriteLine("preforming calculation for ""{0}"" from {1} to {2} ", devicename, st.ToString(dateStrFormat), et.ToString(dateStrFormat))

                    'Dim st As New OSIsoft.AF.Time.AFTime(thisLoopStartDate)
                    'Dim et As New OSIsoft.AF.Time.AFTime(Now.AddSeconds(-5))
                    'Dim tr As New OSIsoft.AF.Time.AFTimeRange(st, et)

                    'get recorded values
                    Dim latvals As Asset.AFValues = attr_Lat.PIPoint.RecordedValues(tr, Data.AFBoundaryType.Inside, Nothing, False)
                    Dim longVals As Asset.AFValues = attr_Long.PIPoint.RecordedValues(tr, Data.AFBoundaryType.Inside, Nothing, False)

                    If Math.Abs(latvals.Count - longVals.Count) > 1 Then
                        Throw New Exception("unexpected results retreived from AF")
                    End If

                    Dim iMax As Integer = Math.Min(latvals.Count, longVals.Count)

                    If iMax = 0 Then Continue For

                    'get the open geofence collisions for this device
                    Dim openGeoFenceCollisions As List(Of FMS.Business.DataObjects.GeoFenceDeviceCollision) = _
                                FMS.Business.DataObjects.GeoFenceDeviceCollision.GetAllWithoutEndDates(app.ApplicationID, devicename) _
                               .ToList

                    ' .Where(Function(x) x.DeviceID = devicename And x.EndTime Is Nothing
                    'for each polygon, look at each point 
                    For Each geofence As FMS.Business.DataObjects.ApplicationGeoFence In geofences

                        'Console.WriteLine("geofence name " & geofence.Name)

                        Dim lst As List(Of Loc) = _
                                geofence.ApplicationGeoFenceSides.Select( _
                                            Function(x) New Loc(CDbl(x.Latitude), CDbl(x.Longitude))).ToList


                        For i As Integer = 0 To iMax - 1

                            Dim lat As Double = latvals(i).ValueAsDouble
                            Dim lng As Double = longVals(i).ValueAsDouble
                            Dim l As New Loc(lat, lng)

                            'is the device already known to be in this geofence?
                            Dim alreadyHaveOpenCollision As Boolean = _
                            openGeoFenceCollisions.Where(Function(x) x.ApplicationGeoFenceID _
                                                             = geofence.ApplicationGeoFenceID).Count > 0

                            'do the calc here
                            Dim found As Boolean

                            If geofence.IsCircular Then
                                found = isPointInCircle(geofence, l)
                            Else
                                found = isPointInPolygon(lst, l)
                            End If


                            If found Then

                                Dim geofencename As String = geofence.Name
                                Dim time As Date = latvals(i).Timestamp.LocalTime
                                Dim deviceid As String = devicename


                                'if so, then do nothing,
                                'if it is not, then create a new geofence and add it to the list
                                If Not alreadyHaveOpenCollision Then

                                    Dim gfdc As New FMS.Business.DataObjects.GeoFenceDeviceCollision
                                    gfdc.ApplicationID = app.ApplicationID
                                    gfdc.ApplicationGeoFenceID = geofence.ApplicationGeoFenceID
                                    gfdc.StartTime = time
                                    gfdc.EndTime = Nothing
                                    gfdc.DeviceID = deviceid

                                    gfdc.GeoFenceDeviceCollissionID = _
                                        FMS.Business.DataObjects.GeoFenceDeviceCollision.Create(gfdc)

                                    'add this new open collision to the list
                                    openGeoFenceCollisions.Add(gfdc)
                                Else
                                    'do nothing
                                End If
                            Else

                                'if there is already an open collision ,then "close it"
                                'then remove this from the in memory collection
                                If alreadyHaveOpenCollision Then

                                    Dim time As Date = latvals(i).Timestamp.LocalTime

                                    Dim lstCols As List(Of FMS.Business.DataObjects.GeoFenceDeviceCollision) = _
                                                openGeoFenceCollisions.Where( _
                                                    Function(x) _
                                                            x.ApplicationGeoFenceID = geofence.ApplicationGeoFenceID _
                                                        And x.DeviceID = devicename _
                                                        And x.EndTime Is Nothing _
                                                        And x.StartTime <= kvp.Key _
                                                ).ToList

                                    For Each gdc As FMS.Business.DataObjects.GeoFenceDeviceCollision In lstCols

                                        gdc.EndTime = time
                                        FMS.Business.DataObjects.GeoFenceDeviceCollision.Update(gdc)
                                        openGeoFenceCollisions.Remove(gdc)
                                    Next

                                End If

                            End If

                        Next
                    Next

                    'store the amount of calcs done at the latest lat/long time which was processed
                    attr_LastGeoFenceCalc.SetValue(New Asset.AFValue(latvals.Count, latvals(iMax - 1).Timestamp.LocalTime))
                Next

            Next

            'Catch ex As Exception
            '    Return False
            'End Try

            myPISystem.Disconnect()
            myPISystem.Dispose()

            Return EarliestStartDate

        End Function

        Public Shared Function isPointInCircle(geofence As DataObjects.ApplicationGeoFence, point As Loc) As Boolean

            Dim lat As Decimal = geofence.CircleCentre.Split("|")(0)
            Dim lng As Decimal = geofence.CircleCentre.Split("|")(1)

            Dim circleCentre As Loc = New Loc(lat, lng)
            Dim circleRadius As Decimal = geofence.CircleRadiusMetres

            Dim distanceFromCentre As Decimal = CoordDistanceM(circleCentre, point)

            Return distanceFromCentre <= circleRadius

        End Function
        'BY RYAN
        Public Shared Function isPointInCircle(center As Loc, radius As Decimal, point As Loc) As Boolean

            Dim lat As Decimal = center.lt
            Dim lng As Decimal = center.lg

            Dim circleCentre As Loc = New Loc(lat, lng)
            Dim circleRadius As Decimal = radius

            Dim distanceFromCentre As Decimal = CoordDistanceM(circleCentre, point)

            Return distanceFromCentre <= circleRadius

        End Function

        Public Shared Function CoordDistanceM(Pos1 As Loc, Pos2 As Loc) As Double

            Dim R As Double = 6371

            Dim dLat As Double = toRadian(Pos2.lt - Pos1.lt)

            Dim dLon As Double = toRadian(Pos2.lg - Pos1.lg)

            Dim a As Double = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(toRadian(Pos1.lt)) * Math.Cos(toRadian(Pos2.lt)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2)

            Dim c As Double = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)))

            Dim result As Double = R * c

            Return result * 1000

        End Function

        Private Shared Function toRadian(val As Double) As Double

            Return (Math.PI / 180) * val

        End Function


        Public Shared Function isPointInPolygon(poly As List(Of Loc), point As Loc) As Boolean

            Dim i As Integer
            Dim j As Integer = poly.Count - 1
            Dim c As Boolean = False


            ' for (i = 0, j = poly.Count - 1; i < poly.Count; j = i++)

            Dim firstIteration As Boolean = True

            For i = 0 To poly.Count - 1


                If ((((poly(i).lt <= point.lt) And (point.lt < poly(j).lt)) _
                        Or ((poly(j).lt <= point.lt) And (point.lt < poly(i).lt))) _
                        And (point.lg < (poly(j).lg - poly(i).lg) * (point.lt - poly(i).lt) _
                            / (poly(j).lt - poly(i).lt) + poly(i).lg)) Then

                    c = Not c

                End If

                If firstIteration Then
                    firstIteration = False
                    j = 0
                Else
                    j += 1
                End If

            Next


            Return c

        End Function

    End Class

    Public Class Loc

        Public Property lt As Double
        Public lg As Double

        Public Sub New(lt As Double, lg As Double)

            Me.lt = lt
            Me.lg = lg
        End Sub
    End Class

End Namespace

