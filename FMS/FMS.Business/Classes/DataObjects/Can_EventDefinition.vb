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
        Public Property QueryType As String
        Public Property TextValue As String
        Public Property VehicleText As String
        Public Property MetricValue As String
#End Region

#Region "CRUD"
        Public Shared Sub Insert(eventDef As DataObjects.Can_EventDefinition)

            Dim canEventDef As New FMS.Business.CAN_EventDefinition
            With canEventDef
                .CAN_EventDefinitionID = Guid.NewGuid
                .Standard = eventDef.Metric.Split(":")(0)
                .PGN = Int(eventDef.Metric.Split(":")(1))
                .SPN = Int(eventDef.Metric.Split(":")(2))
                .TriggerConditoinQualifier = eventDef.QueryType
                .TriggerConditionText = eventDef.TextValue
                .LastDateChecked = Date.Now
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
                If eventDef.Metric.Split(":").Length.Equals(1) Then
                    .Standard = eventDef.MetricValue.Split(":")(0)
                    .PGN = Int(eventDef.MetricValue.Split(":")(1))
                    .SPN = Int(eventDef.MetricValue.Split(":")(2))
                Else
                    .Standard = eventDef.Metric.Split(":")(0)
                    .PGN = Int(eventDef.Metric.Split(":")(1))
                    .SPN = Int(eventDef.Metric.Split(":")(2))
                End If
                .TriggerConditoinQualifier = eventDef.QueryType
                .TriggerConditionText = eventDef.TextValue
                .LastDateChecked = Date.Now
                .VehicleID = eventDef.VehicleID
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(eventDef As DataObjects.Can_EventDefinition)
            Dim canEventDef As FMS.Business.CAN_EventDefinition = (From i In SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions
                                                                   Where i.CAN_EventDefinitionID = eventDef.CAN_EventDefinitionID).Single
            With canEventDef
                .CAN_EventDefinitionID = eventDef.CAN_EventDefinitionID
                .deleted = True
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetCanEventDefinition() As List(Of DataObjects.Can_EventDefinition)
            Dim objEventDefinitions = (From eventDef In SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions _
                                       Join canMessageDef In SingletonAccess.FMSDataContextContignous.CAN_MessageDefinitions On
                                       canMessageDef.Standard Equals eventDef.Standard And canMessageDef.PGN Equals eventDef.PGN _
                                       And canMessageDef.SPN Equals eventDef.SPN _
                                       Where Not eventDef.deleted.Equals(True)
                    Select New DataObjects.Can_EventDefinition() With {.CAN_EventDefinitionID = eventDef.CAN_EventDefinitionID, _
                                                                       .deleted = eventDef.deleted, .LastDateChecked = eventDef.LastDateChecked, _
                                                                       .Metric = eventDef.Standard + "-" + canMessageDef.Parameter_Group_Label, _
                                                                       .PGN = eventDef.PGN, .SPN = eventDef.SPN, .Standard = eventDef.Standard, _
                                                                       .TriggerConditionText = eventDef.TriggerConditionText, _
                                                                       .TriggerConditoinQualifier = eventDef.TriggerConditoinQualifier, _
                                                                       .VehicleID = eventDef.VehicleID, _
                                                                       .VehicleText = eventDef.VehicleID, _
                                                                       .Comparison = eventDef.TriggerConditoinQualifier + " " + eventDef.TriggerConditionText, _
                                                                       .MetricValue = canMessageDef.Standard.ToString() + " : " & canMessageDef.PGN.ToString() + " : " & canMessageDef.SPN.ToString()}).ToList()

            Return objEventDefinitions
        End Function
        Public Shared Function GetCanMessageList() As List(Of DataObjects.Can_EventDefinition.CanMessage)
            Dim objCanMess = (From i In SingletonAccess.FMSDataContextContignous.CAN_MessageDefinitions _
                             Select New DataObjects.Can_EventDefinition.CanMessage() _
                             With {.CanMetricText = i.Standard + " " + i.Parameter_Group_Label, _
                                   .CanMetricValue = i.Standard.ToString() + " : " & i.PGN.ToString() + " : " & i.SPN.ToString()}).Distinct().ToList()
            Return objCanMess
        End Function

        Public Shared Function GetCanMessageList(VehicleId As String) As List(Of DataObjects.Can_EventDefinition.CanMessage)            
            Dim objCanMess = (From i In FMS.Business.DataObjects.ApplicationVehicle.GetFromName(VehicleId).GetAvailableCANTags()
                      Select New DataObjects.Can_EventDefinition.CanMessage() _
                      With {.CanMetricText = i.Standard + " " + i.Parameter_Group_Label, _
                            .CanMetricValue = i.Standard.ToString() + " : " & i.PGN.ToString() + " : " & i.SPN.ToString()}).Distinct().ToList()
            Return objCanMess
        End Function
        Public Shared Function GetEventDefinitionList(vehicleId As String, pgn As Integer, spn As Integer, standard As String) As List(Of DataObjects.Can_EventDefinition)
            Dim EventDef = (From eDef In SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions
                            Where eDef.VehicleID.Equals(vehicleId) And eDef.PGN.Equals(pgn) And eDef.SPN.Equals(spn) And eDef.Standard.Equals(standard) And eDef.deleted.Equals(False)
                            Select New DataObjects.Can_EventDefinition(eDef)).ToList()
            Return EventDef
        End Function
        Public Shared Function GetAllCanEventDefinition() As List(Of DataObjects.Can_EventDefinition)
            Dim CanEventDefinition = (From canEvent In SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions
                                      Where canEvent.deleted.Equals(False)
                                     Select New DataObjects.Can_EventDefinition() With {.VehicleID = canEvent.VehicleID}).Distinct().ToList()
            Return CanEventDefinition
        End Function
        Public Shared Function GetCanEventDefinitionByEventDefinitionId(EventDefinitionID As Guid) As List(Of DataObjects.Can_EventDefinition)
            Dim CanEventDefinition = (From canEvent In SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions
                                      Where canEvent.deleted.Equals(False) And canEvent.CAN_EventDefinitionID.Equals(EventDefinitionID)
                                     Select New DataObjects.Can_EventDefinition(canEvent)).Distinct().ToList()
            Return CanEventDefinition
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
            Public Property CanMetricText As String
            Public Property CanMetricValue As String
        End Class


    End Class


End Namespace

