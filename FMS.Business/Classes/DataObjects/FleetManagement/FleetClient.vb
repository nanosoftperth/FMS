Namespace DataObjects
    Public Class FleetClient
#Region "Properties / enums"
        Public Property ClientID As System.Guid
        Public Property Name As String
        Public Property Address As String
        Public Property CustomerID As System.Nullable(Of Integer)
        Public Property AddressLine2 As String
        Public Property Suburb As String
        Public Property PostCode As String
        Public Property CustomerContactName As String
        Public Property CustomerPhone As String
        Public Property CustomerMobile As String
        Public Property CustomerFax As String
        Public Property CustomerComments As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(Client As DataObjects.FleetClient)
            With New LINQtoSQLClassesDataContext
                Dim fleetClient As New FMS.Business.FleetClient
                With fleetClient
                    .ClientID = Guid.NewGuid
                    .CustomerID = Client.CustomerID
                    .Name = Client.Name
                    .Address = Client.Address
                    .AddressLine2 = Client.AddressLine2
                    .Suburb = Client.Suburb
                    .PostCode = Client.PostCode
                    .CustomerContactName = Client.CustomerContactName
                    .CustomerPhone = Client.CustomerPhone
                    .CustomerMobile = Client.CustomerMobile
                    .CustomerFax = Client.CustomerFax
                    .CustomerComments = Client.CustomerComments
                End With
                .FleetClients.InsertOnSubmit(fleetClient)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(Client As DataObjects.FleetClient)
            With New LINQtoSQLClassesDataContext
                Dim fleetClient As FMS.Business.FleetClient = (From i In .FleetClients
                                                               Where i.ClientID.Equals(Client.ClientID)).SingleOrDefault
                With fleetClient
                    .ClientID = Client.ClientID
                    .CustomerID = Client.CustomerID
                    .Name = Client.Name
                    .Address = Client.Address
                    .AddressLine2 = Client.AddressLine2
                    .Suburb = Client.Suburb
                    .PostCode = Client.PostCode
                    .CustomerContactName = Client.CustomerContactName
                    .CustomerPhone = Client.CustomerPhone
                    .CustomerMobile = Client.CustomerMobile
                    .CustomerFax = Client.CustomerFax
                    .CustomerComments = Client.CustomerComments
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(Client As DataObjects.FleetClient)
            With New LINQtoSQLClassesDataContext
                Dim fleetClient As FMS.Business.FleetClient = (From i In .FleetClients
                                                               Where i.ClientID.Equals(Client.ClientID)).SingleOrDefault
                .FleetClients.DeleteOnSubmit(fleetClient)
                .SubmitChanges()
                .Dispose()
            End With
            FMS.Business.DataObjects.FleetDocument.DeleteByClientID(Client.ClientID)
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.FleetClient)
            Try
                Dim fleetClients As New List(Of DataObjects.FleetClient)

                With New LINQtoSQLClassesDataContext
                    fleetClients = (From i In .FleetClients
                                    Join c In .tblCustomers On
                                  i.CustomerID Equals c.Cid
                                    Order By c.CustomerName
                                    Select New DataObjects.FleetClient() With {.Name = c.CustomerName, .ClientID = i.ClientID, .CustomerID = c.Cid,
                                        .Address = c.AddressLine1, .AddressLine2 = c.AddressLine2, .Suburb = c.Suburb, .PostCode = c.PostCode,
                                        .CustomerContactName = c.CustomerContactName, .CustomerPhone = c.CustomerPhone, .CustomerMobile = c.CustomerMobile,
                                        .CustomerFax = c.CustomerFax, .CustomerComments = c.CustomerComments}).ToList()
                    .Dispose()
                End With

                Return fleetClients

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllCustomer() As List(Of DataObjects.FleetClient)
            Try
                Dim fleetCustomers As New List(Of DataObjects.FleetClient)

                With New LINQtoSQLClassesDataContext

                    fleetCustomers = (From i In .tblCustomers
                                      Order By i.CustomerName
                                      Select New DataObjects.FleetClient() With {.Name = i.CustomerName, .Address = i.AddressLine1, .CustomerID = i.Cid}).ToList()
                    .Dispose()
                End With

                Return fleetCustomers
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllCustomerByCustomerID(ByVal Cid As Integer) As DataObjects.tblCustomers
            Try
                Return DataObjects.tblCustomers.GetACustomerByCID(Cid)
            Catch ex As Exception
                Throw ex
            End Try
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
                Me.AddressLine2 = .AddressLine2
                Me.Suburb = .Suburb
                Me.PostCode = .PostCode
                Me.CustomerContactName = .CustomerContactName
                Me.CustomerPhone = .CustomerPhone
                Me.CustomerMobile = .CustomerMobile
                Me.CustomerFax = .CustomerFax
                Me.CustomerComments = .CustomerComments
            End With
        End Sub
#End Region
    End Class
End Namespace

