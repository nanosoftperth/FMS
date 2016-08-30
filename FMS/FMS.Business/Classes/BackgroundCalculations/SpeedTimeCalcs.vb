﻿Imports OSIsoft.AF

Namespace BackgroundCalculations



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

            If startDate Is Nothing Then startDate = Now.AddDays(-7)

            'If startDateTime = Nothing Then startDateTime = GetStartTime()

            'Get the application from the appid provided to the method
            Dim app As FMS.Business.DataObjects.Application = FMS.Business.DataObjects.Application.GetFromAppID(appid)

            Dim geofences As List(Of FMS.Business.DataObjects.ApplicationGeoFence) = _
                        FMS.Business.DataObjects.ApplicationGeoFence.GetAllApplicationGeoFences(app.ApplicationID)

            Dim timerange As New OSIsoft.AF.Time.AFTimeRange(New Time.AFTime(startDate), New Time.AFTime(Now))

            For Each devicename As String In app.GetAllDevicesNames


                Console.WriteLine(String.Format("Device name: {0}{1}time:{2}", devicename, vbTab, Now.ToString("HH:mm:ss")))

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

                Dim logvals As Asset.AFValues = attr_log.PIPoint.RecordedValues(timerange, Data.AFBoundaryType.Inside, "", True)

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

            Return True

        End Function


    End Class

End Namespace