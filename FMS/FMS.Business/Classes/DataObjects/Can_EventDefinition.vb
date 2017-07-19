Namespace DataObjects
    Public Class Can_EventDefinition
#Region "Properties / enums"
        Public Property CAN_EventDefinitionID As System.Guid

        Public Property Standard As String

        Public Property PGN As System.Nullable(Of Double)

        Public Property SPN As System.Nullable(Of Double)

        Public Property TriggerConditoinQualifier As String

        Public Property TriggerConditionText As String

        Public Property LastDateChecked As System.Nullable(Of Date)

        Public Property deleted As Boolean

#End Region

#Region "CRUD"
        Public Shared Sub Create(eventDef As DataObjects.Can_EventDefinition)
            Dim canEventDef As New FMS.Business.CAN_EventDefinition
            With canEventDef
                .CAN_EventDefinitionID = Guid.NewGuid
                .Standard = eventDef.Standard
                .PGN = eventDef.PGN
                .SPN = eventDef.SPN
                .TriggerConditoinQualifier = eventDef.TriggerConditoinQualifier
                .TriggerConditionText = eventDef.TriggerConditionText
                .LastDateChecked = eventDef.LastDateChecked
                .deleted = eventDef.deleted
            End With

            SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions.InsertOnSubmit(canEventDef)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(eventDef As DataObjects.Can_EventDefinition)
            Dim canEventDef As FMS.Business.CAN_EventDefinition = (From i In SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions
                                                                   Where i.CAN_EventDefinitionID = eventDef.CAN_EventDefinitionID).Single
            With canEventDef
                .CAN_EventDefinitionID = eventDef.CAN_EventDefinitionID
                .Standard = eventDef.Standard
                .PGN = eventDef.PGN
                .SPN = eventDef.SPN
                .TriggerConditoinQualifier = eventDef.TriggerConditoinQualifier
                .TriggerConditionText = eventDef.TriggerConditionText
                .LastDateChecked = eventDef.LastDateChecked
                .deleted = eventDef.deleted
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(eventDef As DataObjects.Can_EventDefinition)
            Dim canEventDef As FMS.Business.CAN_EventDefinition = (From i In SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions
                                                                   Where i.CAN_EventDefinitionID = eventDef.CAN_EventDefinitionID).Single
            SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions.DeleteOnSubmit(canEventDef)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Get methods"

#End Region
    End Class
End Namespace

