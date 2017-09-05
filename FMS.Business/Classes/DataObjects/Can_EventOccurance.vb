Namespace DataObjects
    Public Class Can_EventOccurance
#Region "Properties / enums"
        Public Property CAN_EventOccuranceID As System.Guid
        Public Property CAN_EventDefinitionID As System.Guid
        Public Property OccuredDate As Date
        Public Property FinishedDate As Nullable(Of Date)
        Public Property TriggerCondition As String
        Public Property EventType As String
        Public Property StartTime As Date
        Public Property EndTime As Nullable(Of Date)
#End Region

#Region "CRUD"
        Public Shared Sub Create(eventOccurance As DataObjects.Can_EventOccurance)
            Dim canEventOccurance As New FMS.Business.CAN_EventOccurance
            With canEventOccurance
                .CAN_EventOccuranceID = Guid.NewGuid
                .CAN_EventDefinitionID = eventOccurance.CAN_EventDefinitionID
                .TriggerCondition = eventOccurance.TriggerCondition
                .OccuredDate = eventOccurance.OccuredDate
                '.FinishedDate = eventOccurance.FinishedDate
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
                '.OccuredDate = eventOccurance.OccuredDate
                .FinishedDate = eventOccurance.FinishedDate
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
        Public Shared Function GetCanEventOccuranceList(SearchParam As String) As List(Of DataObjects.Can_EventOccurance)

            If SearchParam Is Nothing Then
                SearchParam = ""
            End If

            Dim objGetCanEventOccurance = (From eventOcc In SingletonAccess.FMSDataContextContignous.CAN_EventOccurances
                                           Join eventDef In SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions On
                                           eventDef.CAN_EventDefinitionID Equals eventOcc.CAN_EventDefinitionID
                                           Join messageDef In SingletonAccess.FMSDataContextContignous.CAN_MessageDefinitions On
                                           messageDef.Standard Equals eventDef.Standard And messageDef.PGN Equals eventDef.PGN And messageDef.SPN Equals eventDef.SPN
                                           Where eventDef.Standard.Contains(SearchParam) Or messageDef.Parameter_Group_Label.Contains(SearchParam) Or _
                                           eventDef.TriggerConditionText.Contains(SearchParam) Or eventDef.TriggerConditoinQualifier.Contains(SearchParam)
                                           Select New DataObjects.Can_EventOccurance() With {.EventType = eventDef.VehicleID.Trim() & " | " & _
                                                                                                            messageDef.Standard & " - " & _
                                                                                                            messageDef.Parameter_Group_Label & " " & _
                                                                                                            eventDef.TriggerConditoinQualifier.Trim() & " " & _
                                                                                                            eventDef.TriggerConditionText.Trim(), _
                                                       .StartTime = eventOcc.OccuredDate, .EndTime = eventOcc.FinishedDate}).ToList()

            Return objGetCanEventOccurance
        End Function
        Public Shared Function GetCanEventOccuranceList() As List(Of DataObjects.Can_EventOccurance)
            Dim objGetCanEventOccurance = (From eventOcc In SingletonAccess.FMSDataContextContignous.CAN_EventOccurances
                                                        Join eventDef In SingletonAccess.FMSDataContextContignous.CAN_EventDefinitions On
                                                        eventDef.CAN_EventDefinitionID Equals eventOcc.CAN_EventDefinitionID
                                                        Join messageDef In SingletonAccess.FMSDataContextContignous.CAN_MessageDefinitions On
                                                        messageDef.Standard Equals eventDef.Standard And messageDef.PGN Equals eventDef.PGN And messageDef.SPN Equals eventDef.SPN
                                                        Select New DataObjects.Can_EventOccurance() With {.EventType = eventDef.VehicleID.Trim() & " | " & _
                                                                                                                                messageDef.Standard & " - " & _
                                                                                                                                messageDef.Parameter_Group_Label & " " & _
                                                                                                                                eventDef.TriggerConditoinQualifier.Trim() & " " & _
                                                                                                                                eventDef.TriggerConditionText.Trim(), _
                                                                    .StartTime = eventOcc.OccuredDate, .EndTime = eventOcc.FinishedDate}).ToList()

            Return objGetCanEventOccurance
        End Function
        Public Shared Function GetCanEventOccuranceFinishedDate(cbEventDefinitionId As Guid) As DataObjects.Can_EventOccurance
            Dim objGetOccuranceFinishedDate = (From eventOcc In SingletonAccess.FMSDataContextContignous.CAN_EventOccurances
                                              Where eventOcc.CAN_EventDefinitionID.Equals(cbEventDefinitionId)
                                              Order By eventOcc.OccuredDate Descending
                                              Select New DataObjects.Can_EventOccurance() With {.CAN_EventOccuranceID = eventOcc.CAN_EventOccuranceID,
                                                                                                .CAN_EventDefinitionID = eventOcc.CAN_EventDefinitionID,
                                                                                                .TriggerCondition = eventOcc.TriggerCondition,
                                                                                                .OccuredDate = eventOcc.OccuredDate,
                                                                                                .FinishedDate = eventOcc.FinishedDate}).FirstOrDefault
            Return objGetOccuranceFinishedDate
        End Function
        Public Shared Function GetCanbusEvenOccuranceList(DateOccured As DateTime) As List(Of DataObjects.Can_EventOccurance)
            Dim retList = (From canbusEvent In SingletonAccess.FMSDataContextContignous.CAN_EventOccurances
                          Where canbusEvent.OccuredDate >= DateOccured.AddDays(-7) And canbusEvent.OccuredDate <= DateOccured
                          Select New DataObjects.Can_EventOccurance(canbusEvent)).ToList()
            Return retList
        End Function
        Public Shared Function GetEventOccuranceByOccuranceId(OccuranceId As Guid) As DataObjects.Can_EventOccurance
            Dim retValue = From canbusEvent In SingletonAccess.FMSDataContextContignous.CAN_EventOccurances
                           Where canbusEvent.CAN_EventOccuranceID.Equals(OccuranceId)
                           Select New DataObjects.Can_EventOccurance(canbusEvent)
            Return retValue
        End Function
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objEventOccurance As FMS.Business.CAN_EventOccurance)
            With objEventOccurance
                CAN_EventDefinitionID = .CAN_EventDefinitionID
                CAN_EventOccuranceID = .CAN_EventOccuranceID
                OccuredDate = .OccuredDate
                FinishedDate = .FinishedDate
                TriggerCondition = .TriggerCondition
            End With
        End Sub
#End Region
    End Class
End Namespace

