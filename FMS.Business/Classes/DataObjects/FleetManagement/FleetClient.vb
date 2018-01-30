Namespace DataObjects
    Public Class FleetClient
#Region "Properties / enums"
        Public Property ClientID As System.Guid
        Public Property Name As String
        Public Property Address As String
        Public Property CustomerID As System.Nullable(Of Integer)
#End Region
#Region "CRUD"
        Public Shared Sub Create(Client As DataObjects.FleetClient)
            Dim fleetClient As New FMS.Business.FleetClient
            With fleetClient
                .ClientID = Guid.NewGuid
                .CustomerID = Client.CustomerID
            End With
            SingletonAccess.FMSDataContextContignous.FleetClients.InsertOnSubmit(fleetClient)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Client As DataObjects.FleetClient)
            Dim fleetClient As FMS.Business.FleetClient = (From i In SingletonAccess.FMSDataContextContignous.FleetClients
                                                        Where i.ClientID.Equals(Client.ClientID)).SingleOrDefault
            With fleetClient
                .ClientID = Client.ClientID
                .CustomerID = Client.CustomerID
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Client As DataObjects.FleetClient)
            Dim ClientID As System.Guid = Client.ClientID
            Dim fleetClient As FMS.Business.FleetClient = (From i In SingletonAccess.FMSDataContextContignous.FleetClients
                                                        Where i.ClientID = ClientID).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.FleetClients.DeleteOnSubmit(fleetClient)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            FMS.Business.DataObjects.FleetDocument.DeleteByClientID(ClientID)
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.FleetClient)
            Dim fleetClients = (From i In SingletonAccess.FMSDataContextContignous.FleetClients
                                Join c In SingletonAccess.FMSDataContextContignous.tblCustomers On
                                 i.CustomerID Equals c.Cid
                                Order By c.CustomerName
                                Select New DataObjects.FleetClient() With {.Name = c.CustomerName, .ClientID = i.ClientID, .CustomerID = c.Cid}).ToList()
            Return fleetClients
        End Function
        Public Shared Function GetAllCustomer() As List(Of DataObjects.FleetClient)
            Dim fleetCustomers = (From i In SingletonAccess.FMSDataContextContignous.tblCustomers
                                  Order By i.CustomerName
                                  Select New DataObjects.FleetClient() With {.Name = i.CustomerName, .Address = i.AddressLine1, .CustomerID = i.Cid}).ToList()
            Return fleetCustomers
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objClient As FMS.Business.FleetClient)
            With objClient
                Me.ClientID = .ClientID
                Me.CustomerID = .CustomerID
                Me.Name = .Name
                Me.Address = .Address
            End With
        End Sub
#End Region
    End Class
End Namespace

