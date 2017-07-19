Namespace DataObjects
    Public Class Can_AlertDefinition
#Region "Properties / enums"
        Public Property CAN_AlertDefinitionID As System.Guid

        Public Property CAN_EventDefinitionID As System.Guid

        Public Property SubscriberNativeID As System.Guid

        Public Property SendEmail As Boolean

        Public Property SendText As Boolean
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

#End Region
    End Class
End Namespace

