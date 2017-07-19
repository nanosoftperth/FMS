Namespace DataObjects
    Public Class Can_EventOccuranceAlert
#Region "Properties / enums"
        Public Property CAN_EventOccuranceAlertID As System.Guid

        Public Property CAN_EventOccuranceID As System.Guid

        Public Property CAN_AlertDefinition As System.Guid

        Public Property DateSent As Date
#End Region

#Region "CRUD"
        Public Shared Sub Create(eventOccurenceAlert As DataObjects.Can_EventOccuranceAlert)
            Dim canEventOccuranceAlert As New FMS.Business.CAN_EventOccuranceAlert
            With canEventOccuranceAlert
                .CAN_EventOccuranceAlertID = Guid.NewGuid
                .CAN_EventOccuranceID = eventOccurenceAlert.CAN_EventOccuranceID
                .CAN_AlertDefinition = eventOccurenceAlert.CAN_AlertDefinition
                .DateSent = eventOccurenceAlert.DateSent
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
                .DateSent = eventOccuranceAlert.DateSent
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

#End Region
    End Class
End Namespace
