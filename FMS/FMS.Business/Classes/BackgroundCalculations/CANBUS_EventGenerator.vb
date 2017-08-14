Imports FMS.Business

Namespace BackgroundCalculations
    Public Class CANBUS_EventGenerator
        Public Shared Function ProcessCanbusEvents(applicationId As Guid) As Boolean
            Dim retVal As Boolean = False
            Try
                Dim applicationVehicles = DataObjects.ApplicationVehicle.GetAll(applicationId)
                For Each vehicle In applicationVehicles 'for each vehicle 
                    GetEachSPN(vehicle.DeviceID) 'for each SPN 
                Next
                retVal = True
            Catch ex As Exception
                retVal = False
            End Try
            Return retVal
        End Function
        Private Shared Sub GetEachSPN(strDeviceId As String)
            Dim appVehicle As New DataObjects.ApplicationVehicle()
            appVehicle.DeviceID = strDeviceId
            Dim AvailableCanTags = appVehicle.GetAvailableCANTags()
            For Each availCanTag In AvailableCanTags
                'get the last time that valid data was obtained for that SPN
                Dim lastTimeValid As DateTime = DataObjects.CanBusLogs.GetLastTimeValidData(strDeviceId, availCanTag.PGN, availCanTag.SPN, availCanTag.Standard)
                'read the new SPN data
                Dim CanData = DataObjects.CanDataPoint.GetPointWithLatestDataByDeviceId(Date.Now, lastTimeValid, availCanTag.SPN, strDeviceId, availCanTag.Standard)
                'get vehicle by device id
                Dim getVehicleByDeviceId = DataObjects.ApplicationVehicle.GetFromDeviceID(strDeviceId)
                If Not CanData.CanValues.Count.Equals(0) AndAlso Not lastTimeValid.ToString().Equals(CanData.CanValues(0).Time.ToString()) Then
                    'generate events from this new data
                    GenerateEventsFromThisNewData(getVehicleByDeviceId.Name, availCanTag.PGN, availCanTag.SPN, availCanTag.Standard, CanData.CanValues(0).Value)
                    ' Save the last time that valid data was obtained for that SPN
                    SaveTheLastTimeValidData(CanData, strDeviceId, availCanTag.SPN, availCanTag.Standard)
                End If
            Next
        End Sub
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
        Private Shared Sub GenerateEventsFromThisNewData(vehicleId As String, pgn As Integer, spn As Integer, standard As String, canBusValue As String)
            Dim eventDefList = DataObjects.Can_EventDefinition.GetEventDefinitionList(vehicleId, pgn, spn, standard)
            Dim dblCanValue As Double

            For Each eventList In eventDefList
                Dim objCanOccurance As New DataObjects.Can_EventOccurance
                If Not eventList.TriggerConditoinQualifier.ToUpper.Equals("LIKE") Then
                    Dim dblValue As Double
                    If Double.TryParse(eventList.TriggerConditionText.Trim, dblValue) AndAlso Double.TryParse(canBusValue, dblCanValue) Then
                        Select Case eventList.TriggerConditoinQualifier.Trim
                            Case "<"
                                If dblValue < dblCanValue Then
                                    SetCanOccurance(objCanOccurance, eventList)
                                End If
                            Case ">"
                                If dblValue > dblCanValue Then
                                    SetCanOccurance(objCanOccurance, eventList)
                                End If
                            Case "<="
                                If dblValue <= dblCanValue Then
                                    SetCanOccurance(objCanOccurance, eventList)
                                End If
                            Case ">="
                                If dblValue >= dblCanValue Then
                                    SetCanOccurance(objCanOccurance, eventList)
                                End If
                            Case "!="
                                If dblValue <> dblCanValue Then
                                    SetCanOccurance(objCanOccurance, eventList)
                                End If
                            Case "="
                                If dblValue = dblCanValue Then
                                    SetCanOccurance(objCanOccurance, eventList)
                                End If
                        End Select
                    Else
                        Select Case eventList.TriggerConditoinQualifier.Trim
                            Case "Like"
                                If canBusValue.Trim().Contains(eventList.TriggerConditionText.Trim()) Then
                                    SetCanOccurance(objCanOccurance, eventList)
                                End If
                            Case "="
                                If canBusValue.Trim().Contains(eventList.TriggerConditionText.Trim()) Then
                                    SetCanOccurance(objCanOccurance, eventList)
                                End If
                        End Select
                    End If
                End If
                CreateCanEventOccurance(objCanOccurance)
            Next
        End Sub
        Private Shared Function CreateCanEventOccurance(canOccurance As DataObjects.Can_EventOccurance) As Boolean
            Try
                DataObjects.Can_EventOccurance.Create(canOccurance)
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Private Shared Sub SetCanOccurance(ByRef objOccurance As DataObjects.Can_EventOccurance, ByVal eventList As DataObjects.Can_EventDefinition)
            objOccurance.CAN_EventDefinitionID = eventList.CAN_EventDefinitionID
            objOccurance.TriggerCondition = eventList.TriggerConditoinQualifier.Trim & " " & eventList.TriggerConditionText.Trim
            objOccurance.OccuredDate = Date.Now
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
