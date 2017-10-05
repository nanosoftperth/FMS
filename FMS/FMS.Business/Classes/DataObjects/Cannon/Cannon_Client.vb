Namespace DataObjects
    Public Class Cannon_Client
#Region "Properties / enums"
        Public Property ClientID As System.Guid
        Public Property Name As String
        Public Property Address As String
        Public Property CustomerID As System.Nullable(Of Integer)
#End Region
#Region "CRUD"
        Public Shared Sub Create(Client As DataObjects.Cannon_Client)
            Dim cannonClient As New FMS.Business.Cannon_Client
            With cannonClient
                .ClientID = Guid.NewGuid
                .CustomerID = Client.CustomerID
                '.Name = Client.Name
                '.Address = Client.Address                
            End With
            SingletonAccess.FMSDataContextContignous.Cannon_Clients.InsertOnSubmit(cannonClient)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Client As DataObjects.Cannon_Client)
            Dim cannonClient As FMS.Business.Cannon_Client = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Clients
                                                        Where i.ClientID.Equals(Client.ClientID)).SingleOrDefault
            With cannonClient
                .ClientID = Client.ClientID
                .CustomerID = Client.CustomerID
                '.Name = Client.Name
                '.Address = Client.Address                
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Client As DataObjects.Cannon_Client)
            Dim ClientID As System.Guid = Client.ClientID
            Dim cannonClient As FMS.Business.Cannon_Client = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Clients
                                                        Where i.ClientID = ClientID).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.Cannon_Clients.DeleteOnSubmit(cannonClient)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.Cannon_Client)
            Dim cannonClients = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Clients
                                 Join c In SingletonAccess.FMSDataContextContignous.tblCustomers On
                                 i.CustomerID Equals c.Cid
                                Select New DataObjects.Cannon_Client() With {.Name = c.CustomerName, .ClientID = i.ClientID, .CustomerID = c.Cid}).ToList()
            Return cannonClients
        End Function
        Public Shared Function GetAllCustomer() As List(Of DataObjects.Cannon_Client)
            Dim cannonCustomers = (From i In SingletonAccess.FMSDataContextContignous.tblCustomers
                                   Select New DataObjects.Cannon_Client() With {.Name = i.CustomerName, .Address = i.AddressLine1, .CustomerID = i.Cid}).ToList()
            Return cannonCustomers
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objClient As FMS.Business.Cannon_Client)
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

