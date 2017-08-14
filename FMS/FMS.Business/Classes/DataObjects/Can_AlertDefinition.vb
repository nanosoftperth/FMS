Namespace DataObjects
    Public Class Can_AlertDefinition
#Region "Properties / enums"
        Private dateValue As String
        Public Property CanAlertDefinitionIDUnique As String
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
            Dim RetEventType As System.Guid
            Dim RetSubscriberID As System.Guid
            If Not alertDef.EventType Is Nothing Or Not alertDef.MessageDestination Is Nothing Then               
                With canAlertDef
                    .CAN_AlertDefinitionID = Guid.NewGuid
                    If Guid.TryParse(alertDef.EventType.ToString(), RetEventType) Then
                        .CAN_EventDefinitionID = RetEventType
                    End If
                    Dim intCount As Integer = 0
                    If Guid.TryParse(alertDef.MessageDestination.ToString(), RetSubscriberID) Then
                        .SubscriberNativeID = RetSubscriberID
                        intCount = (From x In SingletonAccess.FMSDataContextNew.GroupSubscribers).ToList.Where(Function(x) x.GroupID.Equals(RetSubscriberID)).Count()
                    End If
                    If intCount.Equals(0) Then
                        .SendEmail = alertDef.SendEmail
                        .SendText = alertDef.SendText
                    End If
                    .TimePeriod = alertDef.TimePeriod.AddYears(Date.Now.Year - 1)
                End With

                SingletonAccess.FMSDataContextContignous.CAN_AlertDefinitions.InsertOnSubmit(canAlertDef)
                SingletonAccess.FMSDataContextContignous.SubmitChanges()
            End If
        End Sub
        Public Shared Sub Update(alertDef As DataObjects.Can_AlertDefinition)
            Dim AlertDefinitionID As System.Guid = Guid.Parse(alertDef.CanAlertDefinitionIDUnique.Split(":")(1).ToString())
            Dim SubscriberID As System.Guid = Guid.Parse(alertDef.CanAlertDefinitionIDUnique.Split(":")(2).ToString())
            Dim canAlertDef As FMS.Business.CAN_AlertDefinition = (From i In SingletonAccess.FMSDataContextContignous.CAN_AlertDefinitions
                                                                   Where i.CAN_AlertDefinitionID = AlertDefinitionID).Single
            Dim RetEventType As System.Guid
            Dim RetSubscriberID As System.Guid            
            
            With canAlertDef
                .CAN_AlertDefinitionID = AlertDefinitionID
                If Guid.TryParse(alertDef.EventType.ToString(), RetEventType) Then
                    .CAN_EventDefinitionID = RetEventType
                End If

                Dim intCount As Integer = 0
                If Guid.TryParse(alertDef.MessageDestination.ToString(), RetSubscriberID) Then
                    .SubscriberNativeID = RetSubscriberID
                    intCount = (From x In SingletonAccess.FMSDataContextNew.GroupSubscribers).ToList.Where(Function(x) x.GroupID.Equals(RetSubscriberID)).Count()
                Else
                    intCount = (From x In SingletonAccess.FMSDataContextNew.GroupSubscribers).ToList.Where(Function(x) x.GroupID.Equals(SubscriberID)).Count()
                End If

                If intCount.Equals(0) Then
                    .SendEmail = alertDef.SendEmail
                    .SendText = alertDef.SendText
                End If
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(alertDef As DataObjects.Can_AlertDefinition)
            Dim AlertDefinitionID As System.Guid = Guid.Parse(alertDef.CanAlertDefinitionIDUnique.Split(":")(1).ToString())
            Dim canAlertDef As FMS.Business.CAN_AlertDefinition = (From i In SingletonAccess.FMSDataContextContignous.CAN_AlertDefinitions
                                                                   Where i.CAN_AlertDefinitionID = AlertDefinitionID).Single
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
                                             eventDef.TriggerConditionText.Trim(), .TimePeriod = alertDef.TimePeriod, .SubscriberNativeID = alertDef.SubscriberNativeID, .CAN_AlertDefinitionID = alertDef.CAN_AlertDefinitionID, .CAN_EventDefinitionID = alertDef.CAN_EventDefinitionID, .SendEmail = alertDef.SendEmail, .SendText = alertDef.SendText}).ToList()
            Dim subsAlertDefGroups = (From canAlert In objCanAlertDefinitions
                                   Join subs In objSubscribers On subs.NativeID Equals canAlert.SubscriberNativeID
                                   Join gMembers In grpMembers On gMembers.GroupID Equals canAlert.SubscriberNativeID
                                   Select New DataObjects.Can_AlertDefinition() With {.EventType = canAlert.EventType, .TimePeriod = canAlert.TimePeriod,
                                       .MessageDestination = subs.NameFormatted, .SendEmail = canAlert.SendEmail, .SendText = canAlert.SendText, .SubscriberNativeID = canAlert.SubscriberNativeID,
                                        .CAN_AlertDefinitionID = canAlert.CAN_AlertDefinitionID, .CAN_EventDefinitionID = canAlert.CAN_EventDefinitionID}).ToList()
            Dim subsAlertDefNoGroups = (From canAlert In objCanAlertDefinitions
                                 Join subs In objSubscribers On subs.NativeID Equals canAlert.SubscriberNativeID
                                 Select New DataObjects.Can_AlertDefinition() With {.EventType = canAlert.EventType, .TimePeriod = canAlert.TimePeriod,
                                     .MessageDestination = subs.NameFormatted, .SendEmail = canAlert.SendEmail, .SendText = canAlert.SendText, .SubscriberNativeID = canAlert.SubscriberNativeID,
                                        .CAN_AlertDefinitionID = canAlert.CAN_AlertDefinitionID, .CAN_EventDefinitionID = canAlert.CAN_EventDefinitionID}).ToList()
            For Each grpAlertDef In grpMembers
                For Each searchedMembers In subsAlertDefNoGroups
                    If searchedMembers.SubscriberNativeID.Equals(grpAlertDef.GroupID) Then
                        subsAlertDefNoGroups.Remove(searchedMembers)
                        Exit For
                    End If
                Next
            Next
            Dim lstAlertDef = Enumerable.Union(subsAlertDefGroups, subsAlertDefNoGroups).ToList()
            For Each AlertDef In lstAlertDef
                AlertDef.CanAlertDefinitionIDUnique = Guid.NewGuid.ToString() & ":" & AlertDef.CAN_AlertDefinitionID.ToString() & ":" & AlertDef.SubscriberNativeID.ToString()
            Next
            Return lstAlertDef
        End Function
        Public Shared Function GetEventDefintionList() As List(Of DataObjects.Can_AlertDefinition.CanAlertDefintionTextValue)
            Dim objCanAlertDefinitions = (From eventDef In SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions
                                       Join messageDef In SingletonAccess.FMSDataContextContignous.CAN_MessageDefinitions On
                                       eventDef.Standard Equals messageDef.Standard And eventDef.PGN Equals messageDef.PGN And eventDef.SPN Equals messageDef.SPN
                                       Where eventDef.deleted.Equals(False)
                                       Select New DataObjects.Can_AlertDefinition.CanAlertDefintionTextValue() With {.FieldText = eventDef.VehicleID & " | " & _
                                           messageDef.Standard & " - " & _
                                           messageDef.Parameter_Group_Label & " " & _
                                           eventDef.TriggerConditoinQualifier.Trim() & " " & _
                                           eventDef.TriggerConditionText.Trim(), .FieldValue = eventDef.CAN_EventDefinitionID}).ToList()
            Return objCanAlertDefinitions
        End Function
        Public Shared Function GetSubscribersList(applicationId As Guid) As List(Of DataObjects.Can_AlertDefinition.CanAlertDefintionTextValue)
            Dim objSubscribers = (From subs In DataObjects.Subscriber.GetAllforApplication(applicationId)
                                 Select New DataObjects.Can_AlertDefinition.CanAlertDefintionTextValue() With {.FieldText = subs.NameFormatted, .FieldValue = subs.NativeID}).ToList()
            Return objSubscribers
        End Function
        Public Shared Function GetAlertDefinitionList(eventDefId As Guid) As List(Of DataObjects.Can_AlertDefinition)
            Dim objAlertDefList = (From alertDef In SingletonAccess.FMSDataContextContignous.CAN_AlertDefinitions
                                   Where alertDef.CAN_EventDefinitionID.Equals(eventDefId)
                                  Select New DataObjects.Can_AlertDefinition(alertDef)).ToList()
            Return objAlertDefList
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
        Public Class CanAlertDefintionTextValue
            Public Property FieldText As String
            Public Property FieldValue As Guid
        End Class
    End Class
End Namespace

