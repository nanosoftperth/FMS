Namespace DataObjects
    Public Class Cannon_Document
#Region "Properties / enums"
        Public Property DocumentID As System.Guid
        Public Property ClientID As System.Nullable(Of System.Guid)
        Public Property RunID As System.Nullable(Of System.Guid)
        Public Property Description As String
        Public Property PhotoBinary() As Byte()
        Public Property CreatedDate As System.Nullable(Of Date)
#End Region
#Region "CRUD"
        Public Shared Sub Create(Document As DataObjects.Cannon_Document)
            Dim cannonDocument As New FMS.Business.Cannon_Document
            With cannonDocument
                .DocumentID = Guid.NewGuid
                .ClientID = Document.ClientID
                .RunID = Document.RunID
                .Description = Document.Description
                .PhotoBinary = Document.PhotoBinary
                .CreatedDate = Date.Now
            End With
            SingletonAccess.FMSDataContextContignous.Cannon_Documents.InsertOnSubmit(cannonDocument)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Document As DataObjects.Cannon_Document)
            Dim cannonDocument As FMS.Business.Cannon_Document = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Documents
                                                        Where i.DocumentID.Equals(Document.DocumentID)).SingleOrDefault
            With cannonDocument
                .DocumentID = Document.DocumentID
                .ClientID = Document.ClientID
                .RunID = Document.RunID
                .Description = Document.Description
                .PhotoBinary = Document.PhotoBinary
                .CreatedDate = Date.Now
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Document As DataObjects.Cannon_Document)
            Dim DocumentID As System.Guid = Document.DocumentID
            Dim cannonDocument As FMS.Business.Cannon_Document = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Documents
                                                        Where i.DocumentID = DocumentID).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.Cannon_Documents.DeleteOnSubmit(cannonDocument)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.Cannon_Document)
            Dim cannonDocument = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Documents
                             Select New DataObjects.Cannon_Document(i)).ToList()
            Return cannonDocument
        End Function
        Public Shared Function GetAllByClient(CID As Guid) As List(Of DataObjects.Cannon_Document)
            Dim cannonDocument = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Documents
                                Where i.ClientID.Equals(CID)
                                Select New DataObjects.Cannon_Document(i)).ToList()
            Return cannonDocument
        End Function
        Public Shared Function GetAllByRun(RID As Guid) As List(Of DataObjects.Cannon_Document)
            Dim cannonDocument = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Documents
                                Where i.RunID.Equals(RID)
                                Select New DataObjects.Cannon_Document(i)).ToList()
            Return cannonDocument
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objDocument As FMS.Business.Cannon_Document)
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

