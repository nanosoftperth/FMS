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
        Public Shared Function GetCanBusEventOccuranceLogs(cbEventDefinitionId As Guid) As List(Of DataObjects.CanBusEventOccuranceLog)
            Dim objCanBusEventOccuranceLog = (From logs In SingletonAccess.FMSDataContextNew.CanBusEventOccuranceLogs
                                             Where logs.CanEventDefinitionId.Equals(cbEventDefinitionId)
                                             Group logs By logs.CanEventDefinitionId Into g = Group
                                             Order By g.Max(Function(_Date) _Date.LogDate)
                                             Select New DataObjects.CanBusEventOccuranceLog() With {.CanValue = g.Max(Function(cbValue) cbValue.CanValue),
                                                                                                    .CanEventDefinitionId = g.Max(Function(cbOccuranceId) cbOccuranceId.CanEventDefinitionId),
                                                                                                    .LogDate = g.Max(Function(cbDate) cbDate.LogDate)}).ToList
            Return objCanBusEventOccuranceLog
        End Function
#End Region
    End Class
End Namespace
