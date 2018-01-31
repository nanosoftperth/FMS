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
            With New LINQtoSQLClassesDataContext
                Dim fleetRunCompletion As New FMS.Business.FleetRunCompletion
                With fleetRunCompletion
                    .RunCompletionID = Guid.NewGuid
                    .RunID = RunCompletion.RunID
                    .DriverID = RunCompletion.DriverID
                    .DID = RunCompletion.DID
                    .RunDate = RunCompletion.RunDate
                    .Notes = RunCompletion.Notes
                End With
                .FleetRunCompletions.InsertOnSubmit(fleetRunCompletion)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(RunCompletion As DataObjects.FleetRunCompletion)
            With New LINQtoSQLClassesDataContext
                Dim fleetRunCompletion As FMS.Business.FleetRunCompletion = (From i In .FleetRunCompletions
                                                                             Where i.RunCompletionID.Equals(RunCompletion.RunCompletionID)).SingleOrDefault
                With fleetRunCompletion
                    .RunCompletionID = RunCompletion.RunCompletionID
                    .RunID = RunCompletion.RunID
                    .DriverID = RunCompletion.DriverID
                    .DID = RunCompletion.DID
                    .RunDate = RunCompletion.RunDate
                    .Notes = RunCompletion.Notes
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(RunCompletion As DataObjects.FleetRunCompletion)
            With New LINQtoSQLClassesDataContext
                Dim fleetRunCompletion As FMS.Business.FleetRunCompletion = (From i In .FleetRunCompletions
                                                                             Where i.RunCompletionID.Equals(RunCompletion.RunCompletionID)).SingleOrDefault
                .FleetRunCompletions.DeleteOnSubmit(fleetRunCompletion)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.FleetRunCompletion)
            Try
                Dim fleetRunCompletions As New List(Of DataObjects.FleetRunCompletion)

                With New LINQtoSQLClassesDataContext
                    fleetRunCompletions = (From i In .FleetRunCompletions
                                           Select New DataObjects.FleetRunCompletion(i)).ToList()
                    .Dispose()
                End With

                Return fleetRunCompletions
            Catch ex As Exception
                Throw ex
            End Try
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
