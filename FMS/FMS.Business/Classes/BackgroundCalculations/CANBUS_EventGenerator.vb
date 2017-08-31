Imports FMS.Business

Namespace BackgroundCalculations
    Public Class CANBUS_EventGenerator
        Public Shared Function ProcessCanbusEvents(applicationId As Guid) As Boolean
            Dim retVal As Boolean = False
            Try
                Dim AllCanEventDefinition = DataObjects.Can_EventDefinition.GetAllCanEventDefinition()
                For Each canEvent In AllCanEventDefinition 'get all vehicles in can_eventdefinition
                    GetEachSPN(DataObjects.ApplicationVehicle.GetFromName(canEvent.VehicleID, applicationId).DeviceID, canEvent.VehicleID) 'for each SPN 
                Next
                retVal = True
            Catch ex As Exception
                retVal = False
            End Try
            Return retVal
        End Function
        Private Shared Sub GetEachSPN(strDeviceId As String, strVehicleName As String)
            Dim appVehicle As New DataObjects.ApplicationVehicle()
            appVehicle.DeviceID = strDeviceId
            Dim intCount As Integer = 0
            Dim AvailableCanTags = appVehicle.GetAvailableCANTags()
            'Use group by standard and spn to eliminate duplicate
            Dim removeDuplicates = AvailableCanTags.ToList().GroupBy(Function(xx) New With {Key xx.Standard, Key xx.PGN, Key xx.SPN}).ToList().Select(Function(xxx) xxx.First()).ToList()
            For Each availCanTag In removeDuplicates
                intCount += 1
                'get the last time that valid data was obtained for that SPN
                Dim lastTimeValid As DateTime = DataObjects.CanBusLogs.GetLastTimeValidData(strDeviceId, availCanTag.PGN, availCanTag.SPN, availCanTag.Standard)
                'read the new SPN data
                Dim CanData = DataObjects.CanDataPoint.GetPointWithLatestDataByDeviceId(Date.Now, lastTimeValid, availCanTag.SPN, strDeviceId, availCanTag.Standard)
                If Not CanData.CanValues.Count.Equals(0) AndAlso Not lastTimeValid.ToString().Equals(CanData.CanValues(0).Time.ToString()) Then
                    Dim blnGeneratedEvents As Boolean = False
                    'generate events from this new data
                    blnGeneratedEvents = GenerateEventsFromThisNewData(strVehicleName, availCanTag.PGN, availCanTag.SPN, availCanTag.Standard, CanData.CanValues(0).Value, lastTimeValid)
                    If blnGeneratedEvents Then
                        ' Save the last time that valid data was obtained for that SPN
                        SaveTheLastTimeValidData(CanData, strDeviceId, availCanTag.SPN, availCanTag.Standard)
                    End If
                End If
            Next
        End Sub
        Private Shared Function GenerateEventsFromThisNewData(vehicleId As String, pgn As Integer, spn As Integer, standard As String, canBusValue As String, lastTimeValid As Date) As Boolean
            Dim eventDefList = DataObjects.Can_EventDefinition.GetEventDefinitionList(vehicleId, pgn, spn, standard)
            Dim dblCanValue As Double
            Dim blnRetValue As Boolean = False
            Dim objCbEventOccuranceLog As New DataObjects.CanBusEventOccuranceLog()

            For Each eventList In eventDefList
                Dim objCanOccurance As New DataObjects.Can_EventOccurance
                'If Not eventList.TriggerConditoinQualifier.ToUpper.Equals("LIKE") Then
                Dim dblValue As Double
                objCanOccurance.OccuredDate = lastTimeValid
                objCanOccurance.CAN_EventOccuranceID = eventList.CAN_EventDefinitionID
                Dim getLastEventValue = DataObjects.CanBusEventOccuranceLog.GetCanBusEventOccuranceLogs(eventList.CAN_EventDefinitionID).FirstOrDefault()
                Dim previousCanValue As Double = 0
                Dim previousCanValueStr As String = String.Empty
                If Double.TryParse(eventList.TriggerConditionText.Trim, dblValue) AndAlso Double.TryParse(canBusValue, dblCanValue) Then

                    If Not getLastEventValue Is Nothing Then
                        previousCanValue = CDbl(getLastEventValue.CanValue)
                    End If
                    Select Case eventList.TriggerConditoinQualifier.Trim
                        Case "<"
                            If dblCanValue < dblValue Then
                                If previousCanValue <> dblCanValue And (getLastEventValue Is Nothing OrElse previousCanValue > dblValue) Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                            End If
                        Case ">"
                            If dblCanValue > dblValue Then
                                If previousCanValue <> dblCanValue And (getLastEventValue Is Nothing OrElse previousCanValue < dblValue) Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                            End If
                        Case "<="
                            If dblCanValue <= dblValue Then
                                If previousCanValue <> dblCanValue And (getLastEventValue Is Nothing OrElse previousCanValue >= dblValue) Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                            End If
                        Case ">="
                            If dblCanValue >= dblValue Then
                                If previousCanValue <> dblCanValue And (getLastEventValue Is Nothing OrElse previousCanValue <= dblValue) Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                            End If
                        Case "!="
                            If dblCanValue <> dblValue Then
                                If previousCanValue <> dblCanValue And (getLastEventValue Is Nothing OrElse previousCanValue <> dblValue) Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                            End If
                        Case "="
                            If dblCanValue = dblValue Then
                                If previousCanValue <> dblCanValue And (getLastEventValue Is Nothing OrElse previousCanValue = dblValue) Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                            End If
                    End Select
                    SaveCanBusEventOccuranceLogs(objCanOccurance.CAN_EventOccuranceID, dblCanValue.ToString(), lastTimeValid)
                Else
                    If Not getLastEventValue Is Nothing Then
                        previousCanValueStr = getLastEventValue.CanValue
                    End If
                    Select Case eventList.TriggerConditoinQualifier.ToUpper.Trim
                        Case "LIKE"
                            If canBusValue.ToUpper.Trim().Contains(eventList.TriggerConditionText.ToUpper.Trim()) Then
                                If previousCanValueStr.ToUpper.ToString().Trim <> canBusValue.ToUpper.Trim And
                                    (getLastEventValue Is Nothing OrElse previousCanValueStr.ToUpper.ToString().Trim.Contains(eventList.TriggerConditionText.ToUpper.Trim())) Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                            End If
                        Case "="
                            If canBusValue.ToUpper.Trim() = eventList.TriggerConditionText.ToUpper.Trim() Then
                                If previousCanValueStr.ToUpper.ToString().Trim <> canBusValue.ToUpper.Trim And
                                    (getLastEventValue Is Nothing OrElse previousCanValueStr.ToUpper.ToString().Trim = eventList.TriggerConditionText.ToUpper.Trim()) Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                            End If
                    End Select
                    SaveCanBusEventOccuranceLogs(objCanOccurance.CAN_EventOccuranceID, canBusValue, lastTimeValid)
                End If

                'End If
            Next
            Return blnRetValue
        End Function
        Private Shared Sub SaveCanBusEventOccuranceLogs(EventDefinitionId As Guid, canBusValue As String, lastTimeValid As DateTime)
            Try
                Dim objCanBusEventOccuranceLog As New DataObjects.CanBusEventOccuranceLog()
                objCanBusEventOccuranceLog.CanEventDefinitionId = EventDefinitionId
                objCanBusEventOccuranceLog.CanValue = canBusValue
                objCanBusEventOccuranceLog.LogDate = lastTimeValid
                DataObjects.CanBusEventOccuranceLog.Create(objCanBusEventOccuranceLog)
            Catch ex As Exception
            End Try
        End Sub
        Private Shared Function SetCanOccurance(ByVal objOccurance As DataObjects.Can_EventOccurance, ByVal eventList As DataObjects.Can_EventDefinition) As Boolean
            Try
                objOccurance.CAN_EventDefinitionID = eventList.CAN_EventDefinitionID
                objOccurance.TriggerCondition = eventList.TriggerConditoinQualifier.Trim & " " & eventList.TriggerConditionText.Trim
                objOccurance.FinishedDate = Date.Now
                DataObjects.Can_EventOccurance.Create(objOccurance)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Private Shared Sub SaveTheLastTimeValidData(cdt As DataObjects.CanDataPoint, strDeviceId As String, intSPN As Integer, strStandard As String)
            If Not cdt.CanValues.Count.Equals(0) Then
                Dim cbl As New DataObjects.CanBusLogs
                cbl.DeviceId = strDeviceId
                cbl.PGN = cdt.MessageDefinition.PGN
                cbl.SPN = intSPN
                cbl.Standard = strStandard
                cbl.DateLog = cdt.CanValues(0).Time
                DataObjects.CanBusLogs.Create(cbl)
            End If
        End Sub

        '1. for each vehicle 

        '2. for each SPN 

        '3. get the last time that valid data was obtained for that SPN

        '4. read the new SPN data

        '5. generte events from this new data


        ' for ( vehicle v in vehicles){
        '   for ( spn in vehicle.SPNs){
        '
        '       date lastProcessedDate =  vehicle.getLastProcessedDateForSPN(spn.ID);
        '
        '       List<values> lst =  spn.GetData(lastProcessedDate,Datetime.Now()) // spn.GetData(<<startDate>>,<<EndDate>>)       
        '
        '       for ( value v in lst) {
        '           //process value and generate event if neccessary ( in SQL)
        '       }
        '       
        '       //if everything was ok
        '       vehicle.SaveSPNLastProcessedDate(lst.Last().EventTime);
        '
        '   }
        '}
    End Class
End Namespace
