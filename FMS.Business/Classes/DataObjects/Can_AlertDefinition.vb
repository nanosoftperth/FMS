Namespace DataObjects
    Public Class Can_AlertDefinition
#Region "Properties / enums"
        Private dateValue As String
        Public Property CAN_AlertDefinitionID As System.Guid
        Public Property CAN_EventDefinitionID As System.Guid
        Public Property SubscriberNativeID As System.Guid
        Public Property SendEmail As Boolean
        Public Property SendText As Boolean
        Public Property TimePeriod() As DateTime
            Get
                Return dateValue
            End Get
            Set(value As DateTime)
                dateValue = value.ToString("HH:mm:ss")
                ValueDate = dateValue
            End Set
        End Property
        Public Property ValueDate As String
        Public Property EventType As String
        Public Property MessageDestination As String
#End Region

#Region "CRUD"
        Public Shared Sub Create(alertDef As DataObjects.Can_AlertDefinition)
            Dim canAlertDef As New FMS.Business.CAN_AlertDefinition
            With canAlertDef
                .CAN_AlertDefinitionID = Guid.NewGuid
                .CAN_EventDefinitionID = alertDef.CAN_EventDefinitionID
                .SendEmail = alertDef.SendEmail
                .SendText = alertDef.SendText
                .SubscriberNativeID = alertDef.SubscriberNativeID
                .TimePeriod = alertDef.TimePeriod
            End With

            SingletonAccess.FMSDataContextContignous.CAN_AlertDefinitions.InsertOnSubmit(canAlertDef)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(alertDef As DataObjects.Can_AlertDefinition)
            Dim canAlertDef As FMS.Business.CAN_AlertDefinition = (From i In SingletonAccess.FMSDataContextContignous.CAN_AlertDefinitions
                                                                   Where i.CAN_AlertDefinitionID = alertDef.CAN_AlertDefinitionID).Single
            With canAlertDef
                .CAN_AlertDefinitionID = alertDef.CAN_AlertDefinitionID
                .CAN_EventDefinitionID = alertDef.CAN_EventDefinitionID
                .SendEmail = alertDef.SendEmail
                .SendText = alertDef.SendText
                .SubscriberNativeID = alertDef.SubscriberNativeID
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(alertDef As DataObjects.Can_AlertDefinition)
            Dim canAlertDef As FMS.Business.CAN_AlertDefinition = (From i In SingletonAccess.FMSDataContextContignous.CAN_AlertDefinitions
                                                                   Where i.CAN_AlertDefinitionID = alertDef.CAN_AlertDefinitionID).Single
            SingletonAccess.FMSDataContextContignous.CAN_AlertDefinitions.DeleteOnSubmit(canAlertDef)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAllForGroupAlertSubscriber(ApplicationId As Guid) As List(Of DataObjects.Subscriber)
            Dim allSubscribers As List(Of DataObjects.Subscriber) = DataObjects.Subscriber.GetAllforApplication(ApplicationId)
            Return allSubscribers
        End Function

        Public Shared Function GetCanAlertDefinitionList(applicationId As Guid) As List(Of DataObjects.Can_AlertDefinition)
            Dim grpMembers As List(Of FMS.Business.GroupSubscriber) = _
                            (From x In SingletonAccess.FMSDataContextNew.GroupSubscribers).ToList
            Dim objSubscribers = DataObjects.Subscriber.GetAllforApplication(applicationId).ToList()
            Dim objCanAlertDefinitions = (From eventDef In SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions
                                         Join messageDef In SingletonAccess.FMSDataContextContignous.CAN_MessageDefinitions On
                                         eventDef.Standard Equals messageDef.Standard And eventDef.PGN Equals messageDef.PGN And eventDef.SPN Equals messageDef.SPN
                                         Join alertDef In SingletonAccess.FMSDataContextContignous.CAN_AlertDefinitions On
                                         alertDef.CAN_EventDefinitionID Equals eventDef.CAN_EventDefinitionID
                                         Select New DataObjects.Can_AlertDefinition() With {.EventType = eventDef.VehicleID & " | " & _
                                             messageDef.Standard & " - " & _
                                             messageDef.Parameter_Group_Label & " " & _
                                             eventDef.TriggerConditoinQualifier.Trim() & " " & _
                                             eventDef.TriggerConditionText.Trim(), .TimePeriod = alertDef.TimePeriod, .SubscriberNativeID = alertDef.SubscriberNativeID}).ToList()
            Dim subsAlertDef = (From canAlert In objCanAlertDefinitions
                               Join subs In objSubscribers On subs.NativeID Equals canAlert.SubscriberNativeID
                               Join gMembers In grpMembers On gMembers.GroupID Equals canAlert.SubscriberNativeID
                               Select New DataObjects.Can_AlertDefinition() With {.EventType = canAlert.EventType, .TimePeriod = canAlert.TimePeriod,
                                   .MessageDestination = subs.NameFormatted, .SendEmail = subs.SendEmail, .SendText = subs.SendText, .SubscriberNativeID = canAlert.SubscriberNativeID}).ToList()
            

            Return subsAlertDef
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objAlertDef As FMS.Business.CAN_AlertDefinition)
            With objAlertDef
                Me.CAN_AlertDefinitionID = .CAN_AlertDefinitionID
                Me.CAN_EventDefinitionID = .CAN_EventDefinitionID
                Me.SendEmail = .SendEmail
                Me.SendText = .SendText
                Me.SubscriberNativeID = .SubscriberNativeID
                Me.TimePeriod = .TimePeriod
            End With
        End Sub
#End Region
    End Class
End Namespace

