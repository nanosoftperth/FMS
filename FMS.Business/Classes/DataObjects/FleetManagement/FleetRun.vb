Namespace DataObjects
    Public Class FleetRun
#Region "Properties / enums"
        Public Property RunID As System.Guid
        Public Property RunName As String
        Public Property KeyNumber As String
#End Region

#Region "CRUD"
        Public Shared Sub Create(Run As DataObjects.FleetRun)
            With New LINQtoSQLClassesDataContext
                Dim fleetRun As New FMS.Business.FleetRun
                With fleetRun
                    .RunID = Guid.NewGuid
                    .RunName = Run.RunName
                    .KeyNumber = Run.KeyNumber
                End With
                .FleetRuns.InsertOnSubmit(fleetRun)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(Run As DataObjects.FleetRun)
            With New LINQtoSQLClassesDataContext
                Dim fleetRun As FMS.Business.FleetRun = (From i In .FleetRuns
                                                         Where i.RunID.Equals(Run.RunID)).SingleOrDefault
                With fleetRun
                    .RunID = Run.RunID
                    .RunName = Run.RunName
                    .KeyNumber = Run.KeyNumber
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(Run As DataObjects.FleetRun)
            With New LINQtoSQLClassesDataContext
                Dim fleetRun As FMS.Business.FleetRun = (From i In .FleetRuns
                                                         Where i.RunID.Equals(Run.RunID)).SingleOrDefault
                .FleetRuns.DeleteOnSubmit(fleetRun)
                .SubmitChanges()
                .Dispose()
            End With
            FMS.Business.DataObjects.FleetDocument.DeleteByRunID(Run.RunID)
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.FleetRun)
            Try
                Dim fleetRuns As New List(Of DataObjects.FleetRun)

                With New LINQtoSQLClassesDataContext

                    fleetRuns = (From i In .FleetRuns
                                 Order By i.RunName
                                 Select New DataObjects.FleetRun(i)).ToList
                    .Dispose()
                End With

                Return fleetRuns

            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub

        Public Sub New(objFleetRun As FMS.Business.FleetRun)
            With objFleetRun
                Me.RunID = .RunID
                Me.RunName = .RunName
                Me.KeyNumber = .KeyNumber
            End With
        End Sub
#End Region
    End Class
End Namespace

