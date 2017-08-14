Namespace DataObjects
    Public Class Can_EventOccuranceAlert
#Region "Properties / enums"
        Public Property CAN_EventOccuranceAlertID As System.Guid
        Public Property CAN_EventOccuranceID As System.Guid
        Public Property CAN_AlertDefinition As System.Guid
        Public Property SentDate As Date
        Public Property AlertType As String
        Public Property TimePeriod As Date
        Public Property StartTime As Date
        Public Property EndTime As Date
        Public Property SubscriberNativeID As System.Guid
        Public Property EmailAddress As String
        Public Property MessageContent As String
#End Region

#Region "CRUD"
        Public Shared Sub Create(eventOccurenceAlert As DataObjects.Can_EventOccuranceAlert)
            Dim canEventOccuranceAlert As New FMS.Business.CAN_EventOccuranceAlert
            With canEventOccuranceAlert
                .CAN_EventOccuranceAlertID = Guid.NewGuid
                .CAN_EventOccuranceID = eventOccurenceAlert.CAN_EventOccuranceID
                .CAN_AlertDefinition = eventOccurenceAlert.CAN_AlertDefinition
                .SentDate = eventOccurenceAlert.SentDate
            End With
            SingletonAccess.FMSDataContextContignous.CAN_EventOccuranceAlerts.InsertOnSubmit(canEventOccuranceAlert)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(eventOccuranceAlert As DataObjects.Can_EventOccuranceAlert)
            Dim canEventOccuranceAlert As FMS.Business.CAN_EventOccuranceAlert = (From i In SingletonAccess.FMSDataContextContignous.CAN_EventOccuranceAlerts
                                                                                  Where i.CAN_EventOccuranceAlertID = eventOccuranceAlert.CAN_EventOccuranceAlertID).Single
            With canEventOccuranceAlert
                .CAN_EventOccuranceAlertID = eventOccuranceAlert.CAN_EventOccuranceAlertID
                .CAN_EventOccuranceID = eventOccuranceAlert.CAN_EventOccuranceID
                .CAN_AlertDefinition = eventOccuranceAlert.CAN_AlertDefinition
                .SentDate = eventOccuranceAlert.SentDate
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(eventOccuranceAlert As DataObjects.Can_EventOccuranceAlert)
            Dim canEventOccuranceAlert As FMS.Business.CAN_EventOccuranceAlert = (From i In SingletonAccess.FMSDataContextContignous.CAN_EventOccuranceAlerts
                                                                                  Where i.CAN_EventOccuranceAlertID = eventOccuranceAlert.CAN_EventOccuranceAlertID).Single
            SingletonAccess.FMSDataContextContignous.CAN_EventOccuranceAlerts.DeleteOnSubmit(canEventOccuranceAlert)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetCanEventOccuranceList(ApplicationId As Guid) As List(Of DataObjects.Can_EventOccuranceAlert)
            Dim objSubscribers = DataObjects.Subscriber.GetAllforApplication(ApplicationId).ToList()
            Dim objCanEventOccuranceAlert = (From eventdef In SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions
                     Join eventOcc In SingletonAccess.FMSDataContextContignous.CAN_EventOccurances On
                     eventOcc.CAN_EventDefinitionID Equals eventdef.CAN_EventDefinitionID
                     Join alertDef In SingletonAccess.FMSDataContextContignous.CAN_AlertDefinitions On
                     alertDef.CAN_EventDefinitionID Equals eventOcc.CAN_EventDefinitionID
                     Join eventOccAlert In SingletonAccess.FMSDataContextContignous.CAN_EventOccuranceAlerts On
                     eventOccAlert.CAN_EventOccuranceID Equals eventOcc.CAN_EventOccuranceID And eventOccAlert.CAN_AlertDefinition Equals alertDef.CAN_AlertDefinitionID
                     Join messageDef In SingletonAccess.FMSDataContextContignous.CAN_MessageDefinitions On
                     messageDef.Standard Equals eventdef.Standard And messageDef.PGN Equals eventdef.PGN And messageDef.SPN Equals eventdef.SPN
                     Group eventdef, eventOcc, messageDef, alertDef, eventOccAlert By eventdef.CAN_EventDefinitionID Into g = Group
                     Select New DataObjects.Can_EventOccuranceAlert() With {.AlertType = g.Min(Function(x) x.eventdef.VehicleID.Trim() & " | " & _
                                                                                            x.messageDef.Standard & " - " & _
                                                                                            x.messageDef.Parameter_Group_Label & " " & _
                                                                                            x.eventdef.TriggerConditoinQualifier.Trim() & " " & _
                                                                                            x.eventdef.TriggerConditionText.Trim() & " for time period: "), _
                                .StartTime = g.Min(Function(y) y.eventOcc.OccuredDate), .EndTime = g.Max(Function(z) z.eventOcc.OccuredDate), _
                                .SentDate = g.Max(Function(a) a.eventOccAlert.SentDate), .SubscriberNativeID = g.Max(Function(b) b.alertDef.SubscriberNativeID),
                                .MessageContent = g.Max(Function(c) c.eventOccAlert.MessageContent), .TimePeriod = g.Max(Function(d) d.alertDef.TimePeriod)}).ToList()
            For Each OccAlert In objCanEventOccuranceAlert
                Dim subscribers = objSubscribers.Where(Function(x) x.NativeID.Equals(OccAlert.SubscriberNativeID)).FirstOrDefault
                If Not subscribers Is Nothing Then
                    OccAlert.EmailAddress = subscribers.Email
                    OccAlert.AlertType = OccAlert.AlertType & OccAlert.TimePeriod.ToString("HH:mm:ss")
                End If
            Next
            Return objCanEventOccuranceAlert
        End Function
        Public Shared Function GetEventOccuranceAlertList(eventOccuranceId As Guid, alertDefId As Guid, sentDate As DateTime) As List(Of DataObjects.Can_EventOccuranceAlert)
            Dim objEventOccAlert = (From eventOccAlert In SingletonAccess.FMSDataContextContignous.CAN_EventOccuranceAlerts
                                    Where eventOccAlert.CAN_EventOccuranceID.Equals(eventOccuranceId) And eventOccAlert.CAN_AlertDefinition.Equals(alertDefId) And eventOccAlert.SentDate.Equals(sentDate)
                                   Select New DataObjects.Can_EventOccuranceAlert(eventOccAlert)).ToList()
            Return objEventOccAlert
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objEventOccuranceAlert As FMS.Business.CAN_EventOccuranceAlert)
            With objEventOccuranceAlert
                CAN_AlertDefinition = .CAN_AlertDefinition
                CAN_EventOccuranceAlertID = .CAN_EventOccuranceAlertID
                CAN_EventOccuranceID = .CAN_EventOccuranceID
                SentDate = .SentDate
            End With
        End Sub
#End Region
    End Class
End Namespace
