Imports OSIsoft.AF

Namespace BackgroundCalculations

    Public Class CustTimeRange

        Public Property StartTime As DateTime

        Public Property Endtime As DateTime

        Public Sub New(st As DateTime, et As DateTime)
            Me.StartTime = st
            Me.Endtime = et
        End Sub

        ''' <summary>
        ''' for serialization only
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

    End Class

    Public Class SpeedTimeCalcs

        ''' <summary>
        ''' Takes a device and deletes the speed and distance  values for a specific time period. 
        ''' The recalculates both of these. This logic is required as there is no real way for analysis to recalculate a 
        ''' specific time period of time programatically. Eventually, we want this logic to replace the AF analysis logic. 
        ''' There is a fair bit of code replication with this method and ProcessSpeedtimeVals. When we replace AF analytics, this replication 
        ''' should also be removed. 
        ''' </summary>
        Public Shared Sub RecalcSpeedAndDistValues(devicename As String, startdate As Date, endDate As Date)

            'get the device 
            Dim device = DataObjects.Device.GetFromDeviceID(devicename)

            startdate = startdate.AddHours(-8)
            endDate = endDate.AddHours(-8)

            'get the AF elements
            'Hook up to AF (this could be moved elsewhere)
            Dim myPISystem As PISystem = New PISystems().DefaultPISystem
            myPISystem.Connect()
            Dim afdb As AFDatabase = myPISystem.Databases("FMS")

            Dim afst As New OSIsoft.AF.Time.AFTime(startdate)
            Dim afet As New OSIsoft.AF.Time.AFTime(endDate)


            'Get ALL devices from AF (for all applications)
            Dim afnamedcoll As AFNamedCollection(Of Asset.AFElement) =
                OSIsoft.AF.Asset.AFElement.FindElements(afdb, Nothing, "device", _
                            AFSearchField.Template, True, AFSortField.Name, AFSortOrder.Ascending, 1000)


            Dim afe As Asset.AFElement = (From x In afnamedcoll Where x.Name = devicename).SingleOrDefault

            If afe Is Nothing Then Exit Sub

            Dim attr_DistancesinceLastVal As Asset.AFAttribute = afe.Attributes("DistanceSinceLastVal")
            Dim attr_distance As Asset.AFAttribute = afe.Attributes("distance")
            Dim attr_EngineState As Asset.AFAttribute = afe.Attributes("EngineState")
            Dim attr_Lat As Asset.AFAttribute = afe.Attributes("lat")
            Dim attr_Long As Asset.AFAttribute = afe.Attributes("long")
            Dim attr_Speed As Asset.AFAttribute = afe.Attributes("speed")
            Dim attr_TotalDistanceTravelled As Asset.AFAttribute = afe.Attributes("TotalDistanceTravelled")
            Dim attr_log As Asset.AFAttribute = afe.Attributes("log")
            Dim attr_LastGeoFenceCalc As Asset.AFAttribute = afe.Attributes("LastGeoFenceCalc")

            Dim timerange As New OSIsoft.AF.Time.AFTimeRange(afst, afet)


            Dim latVals As Asset.AFValues = attr_Lat.PIPoint.RecordedValues(timerange, Data.AFBoundaryType.Inside, "", True)
            Dim longVals As Asset.AFValues = attr_Long.PIPoint.RecordedValues(timerange, Data.AFBoundaryType.Inside, "", True)

            Dim distValstoInsert As New Asset.AFValues
            Dim speedValsToinsert As New Asset.AFValues

            For i As Integer = 1 To latVals.Count - 1

                Dim thisTime As DateTime = latVals(i).Timestamp.LocalTime
                Dim prevTime As DateTime = latVals(i - 1).Timestamp.LocalTime


                'go to the next value if there is a system state returned
                If latVals(i - 1).ValueTypeCode <> TypeCode.Double Then Continue For
                If latVals(i).ValueTypeCode <> TypeCode.Double Then Continue For
                If longVals(i - 1).ValueTypeCode <> TypeCode.Double Then Continue For
                If longVals(i).ValueTypeCode <> TypeCode.Double Then Continue For

                'get the lattitude and longitude values as decimals for calculation
                Dim Lat1 As Decimal = latVals(i - 1).Value
                Dim Lat2 As Decimal = latVals(i).Value
                Dim Lon1 As Decimal = longVals(i - 1).Value
                Dim Lon2 As Decimal = longVals(i).Value

                'calculate the distance and speed
                Dim distance As Double = DistanceCalc(Lat1, Lat2, Lon1, Lon2)
                Dim kmph As Double = distance / (thisTime - prevTime).TotalHours

                'add to their respective lists
                distValstoInsert.Add(New Asset.AFValue(distance, thisTime))
                speedValsToinsert.Add(New Asset.AFValue(kmph, thisTime))

            Next

            'DELETE the distance values whic hare already in AF 
            Dim distValuesAlreadyThere = attr_distance.PIPoint.RecordedValues(timerange, Data.AFBoundaryType.Inside, "", False, 0)
            If distValuesAlreadyThere IsNot Nothing AndAlso distValuesAlreadyThere.Count > 0 Then attr_distance.PIPoint.UpdateValues(distValuesAlreadyThere, Data.AFUpdateOption.Remove)
            'DELETE the speed values whic hare already in AF 
            Dim speedValsAlreadyThere = attr_Speed.PIPoint.RecordedValues(timerange, Data.AFBoundaryType.Inside, "", False, 0)
            If speedValsAlreadyThere IsNot Nothing AndAlso speedValsAlreadyThere.Count > 0 Then attr_Speed.PIPoint.UpdateValues(speedValsAlreadyThere, Data.AFUpdateOption.Remove)


            'to avoid timeout, the "update values" will be done in batches of 100 values at a time 
            'we can safeley assume that there are the same amount of distance and speed values.
            Dim j As Integer = 0
            Dim maxCount As Integer = distValstoInsert.Count - 1
            Dim BATCH_SIZE As Integer = 100
            Dim iterationCount As Integer = maxCount \ BATCH_SIZE

            If maxCount >= 0 Then

                While True

                    Console.WriteLine("PROCESSING BATCH {0} OF {1}", j / BATCH_SIZE, iterationCount)

                    If j > maxCount Then j = maxCount

                    Dim thisBatchCount As Integer = maxCount - j
                    If thisBatchCount > BATCH_SIZE Then thisBatchCount = BATCH_SIZE

                    If thisBatchCount = 0 Then Exit While

                    Dim distancesBatch = distValstoInsert.GetRange(j, thisBatchCount)
                    Dim speedsBatch = speedValsToinsert.GetRange(j, thisBatchCount)

                    Console.WriteLine("adding {0} distance values ", thisBatchCount)
                    attr_distance.PIPoint.UpdateValues(distancesBatch, Data.AFUpdateOption.Insert)
                    Console.WriteLine("adding {0} speed values ", thisBatchCount)
                    attr_Speed.PIPoint.UpdateValues(speedsBatch, Data.AFUpdateOption.Insert)
                    Console.WriteLine("complete....{0}{0}", vbNewLine)

                    If j >= maxCount Then Exit While

                    j += BATCH_SIZE
                End While

            End If

            Console.WriteLine("{0}recalculation complete{0}", vbNewLine)

        End Sub

        Private Shared Function DistanceCalc(lat1 As Decimal, lat2 As Decimal, lon1 As Decimal, lon2 As Decimal) As Decimal

            Dim R As Decimal = 6371

            Dim dLat As Decimal = (lat2 - lat1) * (Math.PI / 180)
            Dim dLon As Decimal = (lon2 - lon1) * (Math.PI / 180)

            Dim a As Decimal = _
                                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + _
                                Math.Cos(lat1 / (Math.PI * 180)) * Math.Cos(lat2 * (Math.PI / 180)) * _
                                Math.Sin(dLon / 2) * Math.Sin(dLon / 2)

            Dim c As Decimal = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a))

            Dim d As Decimal = R * c

            Return d

        End Function




        Public Shared Function ProcessSpeedtimeVals(appid As Guid,
                                                    Optional startDate As Date? = Nothing,
                                                    Optional isRecalc As Boolean = False) As Boolean

            'Try

            'Hook up to AF (this could be moved elsewhere)
            Dim myPISystem As PISystem = New PISystems().DefaultPISystem
            myPISystem.Connect()
            Dim afdb As AFDatabase = myPISystem.Databases("FMS")

            'Get ALL devices from AF (for all applications)
            Dim afnamedcoll As AFNamedCollection(Of Asset.AFElement) =
                OSIsoft.AF.Asset.AFElement.FindElements(afdb, Nothing, "device", _
                            AFSearchField.Template, True, AFSortField.Name, AFSortOrder.Ascending, 1000)

            If startDate Is Nothing Then startDate = Now.AddDays(-1)

            'Get the application from the appid provided to the method
            Dim app As FMS.Business.DataObjects.Application = FMS.Business.DataObjects.Application.GetFromAppID(appid)

            Dim geofences As List(Of FMS.Business.DataObjects.ApplicationGeoFence) = _
                        FMS.Business.DataObjects.ApplicationGeoFence.GetAllApplicationGeoFences(app.ApplicationID)

            'The maximum query time (this now has to be quite low as now when 
            'the vehicle is driving it produces a lot of logs (data))
            Dim maxHourQuery As Integer = 3

            'we will need to split the data range into manageable chunks for processing
            Dim loopStartDate As Date = Now
            Dim sd As Date = startDate.Value
            Dim ed As Date = startDate + TimeSpan.FromHours(maxHourQuery)

            'list ot process later
            Dim timeRanges As New List(Of CustTimeRange)

            While True

                If ed > loopStartDate Then ed = loopStartDate

                'add the timerange to the list to porcess
                timeRanges.Add(New CustTimeRange(sd, ed))

                'exit the loop if the date is > than the end date 
                If ed >= loopStartDate Then Exit While

                'alter the start and end date times 
                sd = ed
                ed = ed.AddHours(maxHourQuery)
            End While




            For Each tr As CustTimeRange In timeRanges

                Dim afst As New OSIsoft.AF.Time.AFTime(tr.StartTime)
                Dim afet As New OSIsoft.AF.Time.AFTime(tr.Endtime)

                'Console.WriteLine(String.Format("#Processing from {0} to {1} ", afst.LocalTime.ToString("dd/MMM/yyyy HH:mm:ss"), afet.LocalTime.ToString("dd/MMM/yyyy HH:mm:ss")))


                For Each devicename As String In app.GetAllDevicesNames

                    Dim timerange As New OSIsoft.AF.Time.AFTimeRange(afst, afet)

                    'Console.WriteLine(String.Format("Device name: {0}{1}time:{2}", devicename, vbTab, Now.ToString("HH:mm:ss")))

                    Dim afe As Asset.AFElement = (From x In afnamedcoll Where x.Name = devicename).SingleOrDefault

                    'if we cannot find the element in the list above, then move on (not created yet, should probably log this)
                    If afe Is Nothing Then Continue For

                    Dim attr_DistancesinceLastVal As Asset.AFAttribute = afe.Attributes("DistanceSinceLastVal")
                    Dim attr_distance As Asset.AFAttribute = afe.Attributes("distance")
                    Dim attr_EngineState As Asset.AFAttribute = afe.Attributes("EngineState")
                    Dim attr_Lat As Asset.AFAttribute = afe.Attributes("lat")
                    Dim attr_Long As Asset.AFAttribute = afe.Attributes("long")
                    Dim attr_Speed As Asset.AFAttribute = afe.Attributes("speed")
                    Dim attr_TotalDistanceTravelled As Asset.AFAttribute = afe.Attributes("TotalDistanceTravelled")
                    Dim attr_log As Asset.AFAttribute = afe.Attributes("log")
                    Dim attr_LastGeoFenceCalc As Asset.AFAttribute = afe.Attributes("LastGeoFenceCalc")

                    'Dim attr_speedCalculated As Asset.AFAttribute = afe.Attributes("speed calculated")
                    Dim logvals As New Asset.AFValues

                    Try
                        attr_log.PIPoint.RecordedValues(timerange, Data.AFBoundaryType.Inside, "", True)
                    Catch ex As Exception
                        'Console.WriteLine("Excaption caused: {0}", ex.Message)
                        Continue For
                    End Try



                    For Each piv As Asset.AFValue In logvals

                        'If this is the "started application" then we need to make the latitude and
                        'longitude to equal to the previous received values. This is becuase it
                        'takes a long time sometimes for the truck to pick up a signal on the GPS (not the 4G GPRS)
                        If piv.Value.ToString.Contains("started application") Then

                            Dim timeReceived As DateTime = piv.Timestamp.LocalTime

                            Dim latVal As Asset.AFValue = attr_Lat.PIPoint.RecordedValue(timeReceived, Data.AFRetrievalMode.Before)
                            Dim lngVal As Asset.AFValue = attr_Long.PIPoint.RecordedValue(timeReceived, Data.AFRetrievalMode.Before)

                            Dim newLatVal As New Asset.AFValue(latVal.Value, timeReceived) With {.Questionable = True}
                            Dim newLngVal As New Asset.AFValue(lngVal.Value, timeReceived) With {.Questionable = True}

                            'DANGEROUS (writing to the source data tag), marked as questionable to show that it was manually inserted
                            attr_Lat.PIPoint.UpdateValue(newLatVal, Data.AFUpdateOption.Replace)
                            attr_Long.PIPoint.UpdateValue(newLngVal, Data.AFUpdateOption.Replace)

                            'add a 0 speed here
                            Dim prevValDate As DateTime = latVal.Timestamp.LocalTime
                            Dim speed0ValPrev As New Asset.AFValue(0, prevValDate) With {.Questionable = True}
                            Dim speed0ValNow As New Asset.AFValue(0, timeReceived) With {.Questionable = True}

                            'insert the value into the speed val
                            attr_Speed.PIPoint.UpdateValue(speed0ValPrev, Data.AFUpdateOption.Replace)
                            attr_Speed.PIPoint.UpdateValue(speed0ValNow, Data.AFUpdateOption.Replace)
                        End If
                    Next


                    Dim earliestValue As Date = timerange.StartTime.LocalTime

                    If attr_Speed.GetValue.ValueTypeCode = TypeCode.Double Then
                        earliestValue = attr_Speed.GetValue.Timestamp.LocalTime
                    End If

                    If Not isRecalc Then timerange.StartTime = earliestValue


                    Dim distVals As Asset.AFValues = attr_distance.PIPoint.RecordedValues(timerange, Data.AFBoundaryType.Inside, "", True)

                    Dim firstIteration As Boolean = True
                    Dim prevAFVal As Asset.AFValue = Nothing


                    Dim valstoinsert As New Asset.AFValues

                    For Each piv As Asset.AFValue In distVals

                        If Not firstIteration Then

                            'get the distance in kms
                            Dim thisTime As DateTime = piv.Timestamp.LocalTime
                            Dim prevTime As DateTime = prevAFVal.Timestamp.LocalTime

                            'if this is a digital state, then do nothing
                            If piv.ValueTypeCode = TypeCode.Double Then

                                Dim distance As Double = piv.ValueAsDouble

                                Dim kmph As Double = distance / (thisTime - prevTime).TotalHours

                                Dim newAFValue As New Asset.AFValue(kmph, thisTime)

                                If isRecalc Or thisTime >= earliestValue Then valstoinsert.Add(newAFValue)
                                'attr_Speed.PIPoint.UpdateValue(newAFValue, Data.AFUpdateOption.Replace)

                            End If

                        End If

                        prevAFVal = piv
                        firstIteration = False
                    Next

                    If valstoinsert.Count > 0 Then attr_Speed.PIPoint.UpdateValues(valstoinsert, Data.AFUpdateOption.Replace)

                Next

            Next

            Return True

        End Function


    End Class

End Namespace