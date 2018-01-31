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
            With New LINQtoSQLClassesDataContext
                Dim fleetClient As New FMS.Business.FleetClient
                With fleetClient
                    .ClientID = Guid.NewGuid
                    .CustomerID = Client.CustomerID
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
                                    Select New DataObjects.FleetClient() With {.Name = c.CustomerName, .ClientID = i.ClientID, .CustomerID = c.Cid}).ToList()
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

