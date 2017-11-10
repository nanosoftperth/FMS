Namespace DataObjects
    Public Class FleetRunClient
#Region "Properties / enums"
        Public Property RunClientID As System.Guid
        Public Property RunID As System.Nullable(Of System.Guid)
        Public Property ClientID As System.Nullable(Of System.Guid)
#End Region
#Region "CRUD"
        Public Shared Sub Create(RunClient As DataObjects.FleetRunClient)
            Dim fleetRunClient As New FMS.Business.FleetRunClient
            With fleetRunClient
                .RunClientID = Guid.NewGuid
                .RunID = RunClient.RunID
                .ClientID = RunClient.ClientID
            End With
            SingletonAccess.FMSDataContextContignous.FleetRunClients.InsertOnSubmit(fleetRunClient)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(RunClient As DataObjects.FleetRunClient)
            Dim fleetRunClient As FMS.Business.FleetRunClient = (From i In SingletonAccess.FMSDataContextContignous.FleetRunClients
                                                        Where i.RunClientID.Equals(RunClient.RunClientID)).SingleOrDefault
            With fleetRunClient
                .RunClientID = RunClient.RunClientID
                .RunID = RunClient.RunID
                .ClientID = RunClient.ClientID
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(RunClient As DataObjects.FleetRunClient)
            Dim RunClientID As System.Guid = RunClient.RunClientID
            Dim fleetRunClient As FMS.Business.FleetRunClient = (From i In SingletonAccess.FMSDataContextContignous.FleetRunClients
                                                        Where i.RunClientID = RunClientID).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.FleetRunClients.DeleteOnSubmit(fleetRunClient)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.FleetRunClient)
            Dim fleetRunClients = (From i In SingletonAccess.FMSDataContextContignous.FleetRunClients
                             Select New DataObjects.FleetRunClient(i)).ToList()
            Return fleetRunClients
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

