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
        Public Property VehicleID As String
        Public Property Metric As String
        Public Property Comparison As String
        Public Property TextValueField As String
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
                .VehicleID = eventDef.VehicleID
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
                .VehicleID = eventDef.VehicleID
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
        Public Shared Function GetCanEventDefinition() As List(Of DataObjects.Can_EventDefinition)
            Dim objEventDefinitions = (From eventDef In SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions _
                                       Join canMessageDef In SingletonAccess.FMSDataContextContignous.CAN_MessageDefinitions On
                                       canMessageDef.Standard Equals eventDef.Standard And canMessageDef.PGN Equals eventDef.PGN _
                                       And canMessageDef.SPN Equals eventDef.SPN
                    Select New DataObjects.Can_EventDefinition() With {.CAN_EventDefinitionID = eventDef.CAN_EventDefinitionID, _
                                                                       .deleted = eventDef.deleted, .LastDateChecked = eventDef.LastDateChecked, _
                                                                       .Metric = eventDef.Standard + "-" + canMessageDef.Parameter_Group_Label, _
                                                                       .PGN = eventDef.PGN, .SPN = eventDef.SPN, .Standard = eventDef.Standard, _
                                                                       .TriggerConditionText = eventDef.TriggerConditionText, _
                                                                       .TriggerConditoinQualifier = eventDef.TriggerConditoinQualifier, _
                                                                       .VehicleID = eventDef.VehicleID, _
                                                                       .Comparison = eventDef.TriggerConditoinQualifier + " " + eventDef.TriggerConditionText, _
                                                                       .TextValueField = ""}).ToList()

            Return objEventDefinitions
        End Function
        Public Shared Function GetCanMessageList() As List(Of DataObjects.Can_EventDefinition.CanMessage)
            Dim objCanMess = (From i In SingletonAccess.FMSDataContextContignous.CAN_MessageDefinitions _
                             Select New DataObjects.Can_EventDefinition.CanMessage() With {.CanMetric = i.Standard + " " + i.Parameter_Group_Label}).Distinct().ToList()
            Return objCanMess
        End Function
        
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objEventDef As FMS.Business.CAN_EventDefinition)
            With objEventDef
                CAN_EventDefinitionID = .CAN_EventDefinitionID
                Standard = .Standard
                PGN = .PGN
                SPN = .SPN
                TriggerConditoinQualifier = .TriggerConditoinQualifier
                TriggerConditionText = .TriggerConditionText
                LastDateChecked = .LastDateChecked
                deleted = .deleted
                VehicleID = .VehicleID
            End With
        End Sub
#End Region

        Public Class CanMessage
            Public Property CanMetric As String
        End Class
        
    End Class


End Namespace

