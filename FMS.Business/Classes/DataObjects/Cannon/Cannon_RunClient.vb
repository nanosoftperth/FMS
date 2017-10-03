Namespace DataObjects
    Public Class Cannon_RunClient
#Region "Properties / enums"
        Public Property RunClientID As System.Guid
        Public Property RunID As System.Nullable(Of System.Guid)
        Public Property ClientID As System.Nullable(Of System.Guid)
#End Region
#Region "CRUD"
        Public Shared Sub Create(RunClient As DataObjects.Cannon_RunClient)
            Dim cannonRunClient As New FMS.Business.Cannon_RunClient
            With cannonRunClient
                .RunClientID = Guid.NewGuid
                .RunID = RunClient.RunID
                .ClientID = RunClient.ClientID
            End With
            SingletonAccess.FMSDataContextContignous.Cannon_RunClients.InsertOnSubmit(cannonRunClient)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(RunClient As DataObjects.Cannon_RunClient)
            Dim cannonRunClient As FMS.Business.Cannon_RunClient = (From i In SingletonAccess.FMSDataContextContignous.Cannon_RunClients
                                                        Where i.RunClientID.Equals(RunClient.RunClientID)).SingleOrDefault
            With cannonRunClient
                .RunClientID = RunClient.RunClientID
                .RunID = RunClient.RunID
                .ClientID = RunClient.ClientID
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(RunClient As DataObjects.Cannon_RunClient)
            Dim RunClientID As System.Guid = RunClient.RunClientID
            Dim cannonRunClient As FMS.Business.Cannon_RunClient = (From i In SingletonAccess.FMSDataContextContignous.Cannon_RunClients
                                                        Where i.RunClientID = RunClientID).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.Cannon_RunClients.DeleteOnSubmit(cannonRunClient)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.Cannon_RunClient)
            Dim cannonRunClients = (From i In SingletonAccess.FMSDataContextContignous.Cannon_RunClients
                             Select New DataObjects.Cannon_RunClient(i)).ToList()
            Return cannonRunClients
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRunClient As FMS.Business.Cannon_RunClient)
            With objRunClient
                Me.RunClientID = .RunClientID
                Me.RunID = .RunID
                Me.ClientID = .ClientID
            End With
        End Sub
#End Region
    End Class
End Namespace

