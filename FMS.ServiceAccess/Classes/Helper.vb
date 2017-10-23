Imports FMS.ServiceAccess
Imports FMS.Business
Imports FMS.ServiceAccess.WebServices


Public Class Helper

    ''' <summary>
    ''' TODO: need to make this method smaller, getting far too big.
    ''' </summary>
    ''' <param name="vnr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetVehicleData(vnr As VINNumberRequest) As VINNumberResponse

        'create the return object, with the correct VIN number, startdate and enddate
        Dim retobj As New VINNumberResponse With {.VINNumber = vnr.VINNumber}

        'we will search for the application and vehicle objects from the DB in the below loop
        Dim foundVehicle As FMS.Business.DataObjects.ApplicationVehicle = Nothing
        Dim foundApplication As Business.DataObjects.Application = Nothing

        'search through all the company names for uniqco and search for the vehicle with the correct VIN
        'TODO: This could be done a lot more efficiently
        For Each companyName As String In My.Settings.COMPANY_LIST_UNDER_UNIQCO.Split(",").ToList

            Dim loopApp = FMS.Business.DataObjects.Application.GetFromApplicationName(companyName)
            Dim loopVehicle = FMS.Business.DataObjects.ApplicationVehicle.GetFromVINNumber(vnr.VINNumber, loopApp.ApplicationID)

            If loopVehicle IsNot Nothing Then
                foundVehicle = loopVehicle
                foundApplication = loopApp
            End If
        Next

        'if we could not find the vehicle or application , then ERR
        If foundVehicle Is Nothing OrElse foundApplication Is Nothing Then
            retobj.ErrorMessage &= If(foundVehicle Is Nothing, "could not find the vehicle with VIN " & vnr.VINNumber & vbNewLine, "")
            retobj.ErrorMessage &= If(foundApplication Is Nothing, "could not find the related application for vehicle with VIN " & vnr.VINNumber & vbNewLine, "")
            retobj.WasError = True
            Return retobj
        End If

        '#############################################################################      TIMEZONE SET HERE #######
        'set the cached timezone to that of the application we found 
        Business.SingletonAccess.ClientSelected_TimeZone = foundApplication.TimeZone

        'a list of the dates which we want to query 
        'we want to query midnight for every night which is in between the start and end dates
        Dim queryDateList As New List(Of Date)

        'if either the start or end date are null, then return midnight (in the client timezone)
        'the previous night.
        If Not vnr.StartDate.HasValue Or Not vnr.EndDate.HasValue Then

            With DataObjects.TimeZone.GetCurrentClientTime()
                queryDateList.Add(New Date(.Year, .Month, .Day))
            End With
        Else

            'below code creates a date for every midnight between two dates (st & et)
            '-------X|-------|X-------X-------X-------X-------X-------X
            '-------X---|----X-------X-----|-X-------X-------X-------X
            '-----|--X-------X-------X-------X-------X-------X----|---X

            Dim sd As Date = vnr.StartDate.Value
            Dim ed As Date = vnr.EndDate.Value

            'the beginin of the first day and just before the very end of the last day
            sd = New Date(sd.Year, sd.Month, sd.Day)
            ed = New Date(ed.Year, ed.Month, ed.Day).AddDays(+1).AddSeconds(-1)

            While sd <= ed
                sd = sd.AddDays(1)
                If sd <= ed Then queryDateList.Add(sd)
            End While
        End If

        'for EngineoursOn we will need to get the total time for the first iteration
        Dim earliestDate As Date = queryDateList.Min()

        Dim odoReading = (From o In foundVehicle.GetOdometerReadings
                          Where o.TimeReadingTaken <= earliestDate
                          Order By o.TimeReadingTaken).LastOrDefault

        'get the "engine hours on" value for when the first odometer reading (within the start & end date) 
        Dim engineHoursOn As TimeSpan = foundVehicle.GetVehicleMovementSummary(CDate("01 jan 2016"), odoReading.TimeReadingTaken).EngineHoursOn

        'for each date which we want to process:
        For Each loopEndDate As Date In queryDateList.OrderBy(Function(x) x)

            Dim dailyReading As New DailyReading With {.ReadingDate = loopEndDate.AddSeconds(-1)} 'so we get 01/01/2016 23:59:59

            'grab the last odometer reading that was within our time range
            odoReading = (From o In foundVehicle.GetOdometerReadings _
                               Where o.TimeReadingTaken <= loopEndDate _
                               Order By o.TimeReadingTaken).LastOrDefault

            Dim startDate As Date = odoReading.TimeReadingTaken

            'get the totalized data between the start and end date
            With foundVehicle.GetVehicleMovementSummary(startDate, loopEndDate)

                dailyReading.Kilometers = .KilometersTravelled + odoReading.OdometerReading

                If foundVehicle.Name.ToLower.Contains("grant") Then dailyReading.Kilometers = _
                                                    (.KilometersTravelled + odoReading.OdometerReading)

                dailyReading.Enginehours = (engineHoursOn + .EngineHoursOn).TotalHours
            End With


            '============================================================================================
            '|    this is a hard-coded fix for the Emaxi's. There was no time to make this genericised. |
            '|    the data needs to come from the CANbus value from the emaxi                           |
            '|    uses the same logic as the vehicle controllers method "GetCanMessageMessage"          |
            '============================================================================================

            If foundVehicle.DeviceID.ToLower.Contains("emaxi") Then

                Try

                    'below should be constants, or part of the vehicles definitino in SQL 
                    Dim standard As String = "Zagro500"
                    Dim spn As Integer = 13

                    Dim dt As Date = loopEndDate
                    Dim deviceid As String = foundVehicle.DeviceID

                    Dim canVal As Business.DataObjects.CanValue = _
                            Business.DataObjects.CanDataPoint.GetCANMessageForTime(deviceid, standard, spn, dt)


                    Dim engineHrs As String = canVal.ValueStr.Split(":")(0)
                    Dim engineMins As String = canVal.ValueStr.Split(":")(1)
                    Dim engineMinsAsDecimal As Integer = (CDec(engineMins) / 60) * 100

                    dailyReading.Enginehours = CDec(String.Format("{0}.{1}", engineHrs, engineMinsAsDecimal))

                Catch ex As Exception

                    retobj.ErrorMessage = String.Format("EXCEPTION CAUSED{0}{1}", vbNewLine, ex.Message)
                    retobj.WasError = True
                End Try

            End If

            retobj.DailyReadings.Add(dailyReading)

        Next

        Return retobj

    End Function


End Class

