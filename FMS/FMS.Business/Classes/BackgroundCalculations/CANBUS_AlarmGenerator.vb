Imports System.Net.Mail
Imports System.Text

Namespace BackgroundCalculations

    Public Class CANBUS_AlarmGenerator
        Public Property ApplicationID As Guid
        Public Property ApplicationName As String
        Public Property EventOccuranceId As Guid
        Public Property EventDefinitionId As Guid
        Public Property AlertDefinitionID As Guid
        Public Property OccuredDate As DateTime
        Public Property SubscriberList As List(Of DataObjects.Subscriber)
        Public Property MessageContent As String
        Public Property URL As String
        Public Shared Function ProcesCanbusAlarms(applicationId As Guid, url As String) As Boolean
            Dim objSubscribers = DataObjects.Subscriber.GetAllforApplication(applicationId).ToList()
            Dim applicationName As String = DataObjects.Application.GetFromAppID(applicationId).ApplicationName
            Dim retVal As Boolean = False
            Try
                'from SQL, get all events for the last X time (the last few days is fine)
                Dim getCanOccuranceList = DataObjects.Can_EventOccurance.GetCanbusEvenOccuranceList(Date.Now)
                For Each canOccurance In getCanOccuranceList
                    'Set properties for alarmGenerator
                    Dim propAlarmGenerator As New CANBUS_AlarmGenerator
                    propAlarmGenerator.ApplicationID = applicationId
                    propAlarmGenerator.EventOccuranceId = canOccurance.CAN_EventOccuranceID
                    propAlarmGenerator.EventDefinitionId = canOccurance.CAN_EventDefinitionID
                    propAlarmGenerator.OccuredDate = canOccurance.OccuredDate
                    propAlarmGenerator.SubscriberList = objSubscribers
                    propAlarmGenerator.ApplicationName = applicationName
                    propAlarmGenerator.URL = url
                    'filter for events which require an alarm, and that alarm has not been fired yet
                    FilterEventRequireAlarm(propAlarmGenerator)
                Next
                retVal = True
            Catch ex As Exception
                retVal = False
            End Try
            Return retVal
        End Function
        Private Shared Sub FilterEventRequireAlarm(alarmGen As CANBUS_AlarmGenerator)
            'Get GroupMembers of the list of subscriber
            Dim grpMembers As List(Of FMS.Business.GroupSubscriber) = _
                           (From x In SingletonAccess.FMSDataContextNew.GroupSubscribers).ToList

            'Get alert definition by event definition ID
            Dim objAlertDef = DataObjects.Can_AlertDefinition.GetAlertDefinitionList(alarmGen.EventDefinitionId)
            'For Each alert definition
            For Each alertDef In objAlertDef
                Dim objEventDefinition = DataObjects.Can_EventDefinition.GetCanEventDefinitionByEventDefinitionId(alertDef.CAN_EventDefinitionID).SingleOrDefault
                Dim messageDefinition = DataObjects.CAN_MessageDefinition.GetForSPNandPGNandStandard(objEventDefinition.SPN, objEventDefinition.Standard, objEventDefinition.PGN)                
                'Get event occurance alert by eventoccuranceId, alertdefinitionId and occureddate
                Dim objEventOccAlert = DataObjects.Can_EventOccuranceAlert.GetEventOccuranceAlertList(alarmGen.EventOccuranceId, alertDef.CAN_AlertDefinitionID, alarmGen.OccuredDate)
                'If eventOccuranceId, AlertDefinitionId and Occured date does not exists in EventOccuranceAlert then it will send alert email or text
                If objEventOccAlert.Count.Equals(0) Then
                    alarmGen.AlertDefinitionID = alertDef.CAN_AlertDefinitionID
                    'Get member Email Address
                    Dim memberEmail = alarmGen.SubscriberList.Where(Function(x) x.NativeID.Equals(alertDef.SubscriberNativeID)).SingleOrDefault()
                    Dim alertMessage As String = objEventDefinition.Standard.ToString.Trim() + " " + messageDefinition.Description.Trim() + " " + objEventDefinition.TriggerConditoinQualifier.Trim() + " " + objEventDefinition.TriggerConditionText.Trim()
                    Dim alertTime As String = alarmGen.OccuredDate.ToString().Split(" ")(0) + " @ " + alarmGen.OccuredDate.ToString().Split(" ")(1)

                    If alertDef.SendEmail Then
                        'Send alert mail and get Email message
                        alarmGen.MessageContent = FMS.Business.BackgroundCalculations.EmailHelper.SendAlertMail(memberEmail.Email, alarmGen.ApplicationName, memberEmail.Name, objEventDefinition.VehicleID, alertTime, alertMessage, alarmGen.URL)
                        'Create event Occurance Alert
                        CreateEventOccuranceAlert(alarmGen)
                    End If
                    If alertDef.SendText Then
                        If Not memberEmail.Mobile Is Nothing Then
                            alarmGen.MessageContent = FMS.Business.BackgroundCalculations.EmailHelper.CanbusSendSMS(memberEmail.Mobile, alarmGen.ApplicationName, memberEmail.Name, objEventDefinition.VehicleID, alertTime, alertMessage, alarmGen.URL)
                            CreateEventOccuranceAlert(alarmGen)
                        End If
                    End If
                    'Get subscriber list for the group 
                    Dim subscriberList = alarmGen.SubscriberList.Where(Function(x) x.NativeID.Equals(alertDef.SubscriberNativeID)).ToList()
                    'For each subscriber list
                    For Each subscriber In subscriberList
                        'For each group members where groupId equal to subscriber nativeId
                        For Each member In grpMembers.Where(Function(x) x.GroupID.Equals(subscriber.NativeID)).ToList()
                            'Get member Email Address
                            Dim memberEmailGroup = alarmGen.SubscriberList.Where(Function(x) x.NativeID.Equals(member.NativeID)).SingleOrDefault()
                            'If send email=true then send alert email
                            If member.SendEmail Then
                                'Get Email message
                                Dim strMessage As String = FMS.Business.BackgroundCalculations.EmailHelper.SendAlertMail(memberEmailGroup.Email, alarmGen.ApplicationName, memberEmailGroup.Name, objEventDefinition.VehicleID, alertTime, alertMessage, alarmGen.URL)
                                alarmGen.MessageContent = strMessage
                                'Create event Occurance Alert
                                CreateEventOccuranceAlert(alarmGen)
                            End If
                            If member.SendText Then
                                If Not memberEmailGroup.Mobile Is Nothing Then
                                    alarmGen.MessageContent = FMS.Business.BackgroundCalculations.EmailHelper.CanbusSendSMS(memberEmailGroup.Mobile, alarmGen.ApplicationName, memberEmailGroup.Name, objEventDefinition.VehicleID, alertTime, alertMessage, alarmGen.URL)
                                    CreateEventOccuranceAlert(alarmGen)
                                End If
                            End If
                        Next
                    Next
                End If
            Next
        End Sub
        Private Shared Sub CreateEventOccuranceAlert(alarmGen As CANBUS_AlarmGenerator)
            Dim objEventOccuranceAlert As New DataObjects.Can_EventOccuranceAlert()
            objEventOccuranceAlert.CAN_EventOccuranceID = alarmGen.EventOccuranceId
            objEventOccuranceAlert.CAN_AlertDefinition = alarmGen.AlertDefinitionID
            objEventOccuranceAlert.SentDate = alarmGen.OccuredDate
            objEventOccuranceAlert.MessageContent = alarmGen.MessageContent
            DataObjects.Can_EventOccuranceAlert.Create(objEventOccuranceAlert)
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



