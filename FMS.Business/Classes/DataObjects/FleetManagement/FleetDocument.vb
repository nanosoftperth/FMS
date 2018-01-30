Namespace DataObjects
    Public Class FleetDocument
#Region "Properties / enums"
        Public Property DocumentID As System.Guid
        Public Property ClientID As System.Nullable(Of System.Guid)
        Public Property RunID As System.Nullable(Of System.Guid)
        Public Property Description As String
        Public Property PhotoBinary() As Byte()
        Public Property CreatedDate As System.Nullable(Of Date)
#End Region
#Region "CRUD"
        Public Shared Sub Create(Document As DataObjects.FleetDocument)
            Dim fleetDocument As New FMS.Business.FleetDocument
            With fleetDocument
                .DocumentID = Guid.NewGuid
                .ClientID = Document.ClientID
                .RunID = Document.RunID
                .Description = Document.Description
                .PhotoBinary = Document.PhotoBinary
                .CreatedDate = Date.Now
            End With
            SingletonAccess.FMSDataContextContignous.FleetDocuments.InsertOnSubmit(fleetDocument)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Document As DataObjects.FleetDocument)
            Dim fleetDocument As FMS.Business.FleetDocument = (From i In SingletonAccess.FMSDataContextContignous.FleetDocuments
                                                        Where i.DocumentID.Equals(Document.DocumentID)).SingleOrDefault
            With fleetDocument
                .DocumentID = Document.DocumentID
                .ClientID = Document.ClientID
                .RunID = Document.RunID
                .Description = Document.Description
                .PhotoBinary = Document.PhotoBinary
                .CreatedDate = Date.Now
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Document As DataObjects.FleetDocument)
            Dim DocumentID As System.Guid = Document.DocumentID
            Dim fleetDocument As FMS.Business.FleetDocument = (From i In SingletonAccess.FMSDataContextContignous.FleetDocuments
                                                               Where i.DocumentID = DocumentID).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.FleetDocuments.DeleteOnSubmit(fleetDocument)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub DeleteByRunID(RunID As Guid)
            Dim DocRunID As System.Guid = RunID
            Dim fleetDocs = (From i In SingletonAccess.FMSDataContextContignous.FleetDocuments
                             Where i.RunID = DocRunID).ToList()
            For Each fleetDoc In fleetDocs
                Dim fleetDocument As FMS.Business.FleetDocument = fleetDoc
                SingletonAccess.FMSDataContextContignous.FleetDocuments.DeleteOnSubmit(fleetDocument)
                SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Next
        End Sub
        Public Shared Sub DeleteByClientID(ClientID As Guid)
            Dim DocClientID As System.Guid = ClientID
            Dim fleetDocs = (From i In SingletonAccess.FMSDataContextContignous.FleetDocuments
                             Where i.ClientID = DocClientID).ToList()
            For Each fleetDoc In fleetDocs
                Dim fleetDocument As FMS.Business.FleetDocument = fleetDoc
                SingletonAccess.FMSDataContextContignous.FleetDocuments.DeleteOnSubmit(fleetDocument)
                SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Next
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.FleetDocument)
            Dim fleetDocument = (From i In SingletonAccess.FMSDataContextContignous.FleetDocuments
                                 Order By i.Description
                                 Select New DataObjects.FleetDocument(i)).ToList()
            Return fleetDocument
        End Function
        Public Shared Function GetAllByClient(CID As Guid) As List(Of DataObjects.FleetDocument)
            Dim fleetDocument = (From i In SingletonAccess.FMSDataContextContignous.FleetDocuments
                                 Order By i.Description
                                 Where i.ClientID.Equals(CID)
                                 Select New DataObjects.FleetDocument(i)).ToList()
            Return fleetDocument
        End Function
        Public Shared Function GetAllByRun(RID As Guid) As List(Of DataObjects.FleetDocument)
            Dim fleetDocument = (From i In SingletonAccess.FMSDataContextContignous.FleetDocuments
                                 Order By i.Description
                                 Where i.RunID.Equals(RID)
                                 Select New DataObjects.FleetDocument(i)).ToList()
            Return fleetDocument
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objDocument As FMS.Business.FleetDocument)
            With objDocument
                Me.DocumentID = .DocumentID
                Me.ClientID = .ClientID
                Me.RunID = .RunID
                Me.Description = .Description
                Me.PhotoBinary = .PhotoBinary.ToArray
                Me.CreatedDate = .CreatedDate
            End With
        End Sub
#End Region
    End Class
End Namespace

