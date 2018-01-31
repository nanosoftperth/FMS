Namespace DataObjects
    Public Class FleetRunClient
#Region "Properties / enums"
        Public Property RunClientID As System.Guid
        Public Property RunID As System.Nullable(Of System.Guid)
        Public Property ClientID As System.Nullable(Of System.Guid)
#End Region
#Region "CRUD"
        Public Shared Sub Create(RunClient As DataObjects.FleetRunClient)
            With New LINQtoSQLClassesDataContext
                Dim fleetRunClient As New FMS.Business.FleetRunClient
                With fleetRunClient
                    .RunClientID = Guid.NewGuid
                    .RunID = RunClient.RunID
                    .ClientID = RunClient.ClientID
                End With
                .FleetRunClients.InsertOnSubmit(fleetRunClient)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(RunClient As DataObjects.FleetRunClient)
            With New LINQtoSQLClassesDataContext
                Dim fleetRunClient As FMS.Business.FleetRunClient = (From i In .FleetRunClients
                                                                     Where i.RunClientID.Equals(RunClient.RunClientID)).SingleOrDefault
                With fleetRunClient
                    .RunClientID = RunClient.RunClientID
                    .RunID = RunClient.RunID
                    .ClientID = RunClient.ClientID
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(RunClient As DataObjects.FleetRunClient)
            With New LINQtoSQLClassesDataContext
                Dim fleetRunClient As FMS.Business.FleetRunClient = (From i In .FleetRunClients
                                                                     Where i.RunClientID.Equals(RunClient.RunClientID)).SingleOrDefault
                .FleetRunClients.DeleteOnSubmit(fleetRunClient)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.FleetRunClient)
            Try
                Dim fleetRunClients As New List(Of DataObjects.FleetRunClient)

                With New LINQtoSQLClassesDataContext
                    fleetRunClients = (From i In .FleetRunClients
                                       Select New DataObjects.FleetRunClient(i)).ToList()
                    .Dispose()
                End With

                Return fleetRunClients
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRunClient As FMS.Business.FleetRunClient)
            With objRunClient
                Me.RunClientID = .RunClientID
                Me.RunID = .RunID
                Me.ClientID = .ClientID
            End With
        End Sub
#End Region
    End Class
End Namespace

