Namespace DataObjects
    Public Class FleetRunCompletion
#Region "Properties / enums"
        Public Property RunCompletionID As System.Guid
        Public Property RunID As System.Guid
        Public Property DriverID As System.Nullable(Of System.Guid)
        Public Property DID As System.Nullable(Of Integer)
        Public Property RunDate As System.Nullable(Of Date)
        Public Property Notes As String
#End Region

#Region "CRUD"
        Public Shared Sub Create(RunCompletion As DataObjects.FleetRunCompletion)
            Dim fleetRunCompletion As New FMS.Business.FleetRunCompletion
            With fleetRunCompletion
                .RunCompletionID = Guid.NewGuid
                .RunID = RunCompletion.RunID
                .DriverID = RunCompletion.DriverID
                .DID = RunCompletion.DID
                .RunDate = RunCompletion.RunDate
                .Notes = RunCompletion.Notes
            End With
            SingletonAccess.FMSDataContextContignous.FleetRunCompletions.InsertOnSubmit(fleetRunCompletion)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(RunCompletion As DataObjects.FleetRunCompletion)
            Dim fleetRunCompletion As FMS.Business.FleetRunCompletion = (From i In SingletonAccess.FMSDataContextContignous.FleetRunCompletions
                                                                            Where i.RunCompletionID = RunCompletion.RunCompletionID).SingleOrDefault
            With fleetRunCompletion
                .RunCompletionID = RunCompletion.RunCompletionID
                .RunID = RunCompletion.RunID
                .DriverID = RunCompletion.DriverID
                .DID = RunCompletion.DID
                .RunDate = RunCompletion.RunDate
                .Notes = RunCompletion.Notes
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(RunCompletion As DataObjects.FleetRunCompletion)
            Dim RunCompletionId As System.Guid = RunCompletion.RunCompletionID
            Dim fleetRunCompletion As FMS.Business.FleetRunCompletion = (From i In SingletonAccess.FMSDataContextContignous.FleetRunCompletions
                                                        Where i.RunCompletionID = RunCompletionId).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.FleetRunCompletions.DeleteOnSubmit(fleetRunCompletion)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.FleetRunCompletion)
            Dim fleetRunCompletion = (From i In SingletonAccess.FMSDataContextContignous.FleetRunCompletions
                             Select New DataObjects.FleetRunCompletion(i)).ToList()
            Return fleetRunCompletion
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objFleetRunCompletion As FMS.Business.FleetRunCompletion)
            With objFleetRunCompletion
                Me.RunCompletionID = .RunCompletionID
                Me.RunID = .RunID
                Me.DriverID = .DriverID
                Me.DID = .DID
                Me.RunDate = .RunDate
                Me.Notes = .Notes
            End With
        End Sub
#End Region
    End Class
End Namespace
