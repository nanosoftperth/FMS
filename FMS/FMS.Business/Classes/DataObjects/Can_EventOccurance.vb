Namespace DataObjects
    Public Class Can_EventOccurance
#Region "Properties / enums"
        Public Property CAN_EventOccuranceID As System.Guid

        Public Property CAN_EventDefinitionID As System.Guid

        Public Property DateOccured As Date

        Public Property TriggerCondition As String
#End Region

#Region "CRUD"
        Public Shared Sub Create(eventOccurance As DataObjects.Can_EventOccurance)
            Dim canEventOccurance As New FMS.Business.CAN_EventOccurance
            With canEventOccurance
                .CAN_EventOccuranceID = Guid.NewGuid
                .CAN_EventDefinitionID = eventOccurance.CAN_EventDefinitionID
                .TriggerCondition = eventOccurance.TriggerCondition
                .DateOccured = eventOccurance.DateOccured
            End With
            SingletonAccess.FMSDataContextContignous.CAN_EventOccurances.InsertOnSubmit(canEventOccurance)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(eventOccurance As DataObjects.Can_EventOccurance)
            Dim canEventOccurance As FMS.Business.CAN_EventOccurance = (From i In SingletonAccess.FMSDataContextContignous.CAN_EventOccurances
                                                                        Where i.CAN_EventOccuranceID = eventOccurance.CAN_EventOccuranceID).Single
            With canEventOccurance
                .CAN_EventOccuranceID = eventOccurance.CAN_EventOccuranceID
                .CAN_EventDefinitionID = eventOccurance.CAN_EventDefinitionID
                .TriggerCondition = eventOccurance.TriggerCondition
                .DateOccured = eventOccurance.DateOccured
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(eventOccurance As DataObjects.Can_EventOccurance)
            Dim canEventOccuance As FMS.Business.CAN_EventOccurance = (From i In SingletonAccess.FMSDataContextContignous.CAN_EventOccurances
                                                                       Where i.CAN_EventOccuranceID = eventOccurance.CAN_EventOccuranceID).Single
            SingletonAccess.FMSDataContextContignous.CAN_EventOccurances.DeleteOnSubmit(canEventOccuance)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Get methods"

#End Region
    End Class
End Namespace

