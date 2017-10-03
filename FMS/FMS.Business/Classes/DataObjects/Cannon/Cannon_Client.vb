Namespace DataObjects
    Public Class Cannon_Client
#Region "Properties / enums"
        Public Property ClientID As System.Guid
        Public Property DocumentID As System.Nullable(Of System.Guid)
        Public Property Name As String
        Public Property Address As String
        Public Property OtherFields As String
        Public Property PhotoBinary() As Byte()
#End Region
#Region "CRUD"
        Public Shared Sub Create(Client As DataObjects.Cannon_Client)
            Dim cannonClient As New FMS.Business.Cannon_Client
            With cannonClient
                .ClientID = Guid.NewGuid
                .Name = Client.Name
                .Address = Client.Address
                .OtherFields = Client.OtherFields
                .DocumentID = Client.DocumentID
            End With
            SingletonAccess.FMSDataContextContignous.Cannon_Clients.InsertOnSubmit(cannonClient)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Client As DataObjects.Cannon_Client)
            Dim cannonClient As FMS.Business.Cannon_Client = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Clients
                                                        Where i.ClientID.Equals(Client.ClientID)).SingleOrDefault
            With cannonClient
                .ClientID = Client.ClientID
                .Name = Client.Name
                .Address = Client.Address
                .OtherFields = Client.OtherFields
                .DocumentID = Client.DocumentID
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
                             Select New DataObjects.Cannon_Client(i)).ToList()

            For Each cannonClient In cannonClients
                Dim Doc = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Documents
                           Where i.DocumentID.Equals(cannonClient.DocumentID)
                             Select New DataObjects.Cannon_Document(i)).SingleOrDefault
                If Not Doc Is Nothing Then
                    cannonClient.PhotoBinary = Doc.PhotoBinary
                End If
            Next
            Return cannonClients
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objClient As FMS.Business.Cannon_Client)
            With objClient
                Me.ClientID = .ClientID
                Me.Name = .Name
                Me.Address = .Address
                Me.OtherFields = .OtherFields
                Me.DocumentID = .DocumentID
            End With
        End Sub
#End Region
    End Class
End Namespace

