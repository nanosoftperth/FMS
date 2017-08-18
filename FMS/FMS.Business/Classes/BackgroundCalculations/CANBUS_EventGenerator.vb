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
            Dim AvailableCanTags = appVehicle.GetAvailableCANTags()
            For Each availCanTag In AvailableCanTags
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

            For Each eventList In eventDefList
                Dim objCanOccurance As New DataObjects.Can_EventOccurance
                If Not eventList.TriggerConditoinQualifier.ToUpper.Equals("LIKE") Then
                    Dim dblValue As Double
                    objCanOccurance.OccuredDate = lastTimeValid
                    If Double.TryParse(eventList.TriggerConditionText.Trim, dblValue) AndAlso Double.TryParse(canBusValue, dblCanValue) Then
                        Select Case eventList.TriggerConditoinQualifier.Trim
                            Case "<"
                                If dblValue < dblCanValue Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                            Case ">"
                                If dblValue > dblCanValue Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                            Case "<="
                                If dblValue <= dblCanValue Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                            Case ">="
                                If dblValue >= dblCanValue Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                            Case "!="
                                If dblValue <> dblCanValue Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                            Case "="
                                If dblValue = dblCanValue Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                        End Select
                    Else
                        Select Case eventList.TriggerConditoinQualifier.ToUpper.Trim
                            Case "LIKE"
                                If canBusValue.ToUpper.Trim().Contains(eventList.TriggerConditionText.ToUpper.Trim()) Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                            Case "="
                                If canBusValue.ToUpper.Trim().Contains(eventList.TriggerConditionText.ToUpper.Trim()) Then
                                    blnRetValue = SetCanOccurance(objCanOccurance, eventList)
                                End If
                        End Select
                    End If
                End If
            Next
            Return blnRetValue
        End Function
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
