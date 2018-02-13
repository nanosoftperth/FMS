Namespace DataObjects
    Public Class FleetRun
#Region "Properties / enums"
        Public Property RunID As System.Guid
        Public Property Rid As System.Nullable(Of Integer)
        Public Property RunNumber As System.Nullable(Of Integer)
        Public Property RunDriver As System.Nullable(Of Short)
        Public Property RunName As String
#End Region

#Region "CRUD"
        Public Shared Sub Create(Run As DataObjects.FleetRun)
            With New LINQtoSQLClassesDataContext
                Dim fleetRun As New FMS.Business.FleetRun
                With fleetRun
                    .RunID = Guid.NewGuid
                    .Rid = Run.Rid
                    .RunNumber = Run.RunNumber
                    .RunDriver = Run.RunDriver
                    .RunName = Run.RunName
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
                    .Rid = Run.Rid
                    .RunNumber = Run.RunNumber
                    .RunDriver = Run.RunDriver
                    .RunName = Run.RunName
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
                                 Select New DataObjects.FleetRun(i)).ToList
                    .Dispose()
                End With

                Return fleetRuns

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetRunList() As List(Of DataObjects.tblRuns)
            Try
                Return DataObjects.tblRuns.GetTblRuns()
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetRunListById(ByVal Rid As Integer) As DataObjects.tblRuns
            Try
                Return DataObjects.tblRuns.GetTblRunByRId(Rid)
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
                Me.Rid = .Rid
                Me.RunNumber = .RunNumber
                Me.RunDriver = .RunDriver
                Me.RunName = .RunName
            End With
        End Sub
#End Region
    End Class
End Namespace

