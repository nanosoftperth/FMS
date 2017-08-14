Namespace BackgroundCalculations

    Public Class CANBUS_AlarmGenerator
        Public Shared Function ProcesCanbusAlarms() As Boolean
            Dim retVal As Boolean = False
            Try
                Dim getCanOccuranceList = DataObjects.Can_EventOccurance.GetCanbusEvenOccuranceList(Date.Now)
                For Each canOccurance In getCanOccuranceList
                    FilterEventRequireAlarm(canOccurance.CAN_EventOccuranceID, canOccurance.CAN_EventDefinitionID, canOccurance.OccuredDate)
                Next
                retVal = True
            Catch ex As Exception
                retVal = False
            End Try
            Return retVal
        End Function
        Private Shared Sub FilterEventRequireAlarm(eventOccuranceId As Guid, eventDefinitionId As Guid, occuredDate As DateTime)
            Dim objAlertDef = DataObjects.Can_AlertDefinition.GetAlertDefinitionList(eventDefinitionId)
            For Each alertDef In objAlertDef
                Dim objEventOccAlert = DataObjects.Can_EventOccuranceAlert.GetEventOccuranceAlertList(eventOccuranceId, alertDef.CAN_AlertDefinitionID, occuredDate)
                If objEventOccAlert.Count.Equals(0) Then

                End If
            Next
        End Sub

        'generate alarms from the events which are stored in SQL


        '
        '   from SQL, get all events for the last X time (the last few days is fine)
        '   
        '   filter for events which require an alarm, and that alarm has not been fired yet
        '
        '   send alarm (email/text/groupo etc)



    End Class

End Namespace



