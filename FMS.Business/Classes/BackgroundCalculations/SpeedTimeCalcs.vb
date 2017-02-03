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