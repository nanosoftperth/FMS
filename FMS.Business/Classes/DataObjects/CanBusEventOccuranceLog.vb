Namespace DataObjects
    Public Class CanBusEventOccuranceLog
#Region "PROPERTIES"
        Public CanBusEventOccuranceLogId As System.Guid
        Public CanEventDefinitionId As System.Nullable(Of System.Guid)
        Public CanValue As String
        Public LogDate As DateTime
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(obj As FMS.Business.CanBusEventOccuranceLog)
            With obj
                Me.CanBusEventOccuranceLogId = .CanBusEventOccuranceLogId
                Me.CanEventDefinitionId = .CanEventDefinitionId
                Me.CanValue = .CanValue
                Me.LogDate = .LogDate
            End With
        End Sub
#End Region
#Region "CRUD"
        Public Shared Sub Create(cbEventOccuranceLog As DataObjects.CanBusEventOccuranceLog)
            Dim canOccuranceLog As New FMS.Business.CanBusEventOccuranceLog
            Try
                With canOccuranceLog
                    .CanBusEventOccuranceLogId = Guid.NewGuid
                    .CanEventDefinitionId = cbEventOccuranceLog.CanEventDefinitionId
                    .CanValue = cbEventOccuranceLog.CanValue
                    .LogDate = cbEventOccuranceLog.LogDate
                End With
                SingletonAccess.FMSDataContextContignous.CanBusEventOccuranceLogs.InsertOnSubmit(canOccuranceLog)
                SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Catch ex As Exception
            End Try
        End Sub
#End Region
#Region "GET Methods"
        Public Shared Function GetCanBusEventOccuranceLatestLog(cbEventDefinitionId As Guid) As List(Of DataObjects.CanBusEventOccuranceLog)
            Dim objCanBusEventOccuranceLog = (From logs In SingletonAccess.FMSDataContextNew.CanBusEventOccuranceLogs
                                            Where logs.CanEventDefinitionId.Equals(cbEventDefinitionId)
                                            Order By logs.LogDate Descending
                                            Select New DataObjects.CanBusEventOccuranceLog(logs)).ToList
            Return objCanBusEventOccuranceLog
        End Function
#End Region
    End Class
End Namespace
