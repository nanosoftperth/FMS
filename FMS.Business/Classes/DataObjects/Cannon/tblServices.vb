Namespace DataObjects
    Public Class tblServices
#Region "Properties / enums"
        Public Property ServicesID As System.Guid
        Public Property Sid As Integer
        Public Property ServiceCode As String
        Public Property ServiceDescription As String
        Public Property CostOfService As System.Nullable(Of Single)
#End Region
#Region "CRUD"
        Public Shared Sub Create(Services As DataObjects.tblServices)
            Dim objService As New FMS.Business.tblService
            With objService
                .ServicesID = Guid.NewGuid
                .ServiceCode = Services.ServiceCode
                .ServiceDescription = Services.ServiceDescription
                .CostOfService = Services.CostOfService
            End With
            SingletonAccess.FMSDataContextContignous.tblServices.InsertOnSubmit(objService)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Services As DataObjects.tblServices)
            Dim objService As FMS.Business.tblService = (From c In SingletonAccess.FMSDataContextContignous.tblServices
                                                           Where c.ServicesID.Equals(Services.ServicesID)).SingleOrDefault
            With objService
                .ServiceCode = Services.ServiceCode
                .ServiceDescription = Services.ServiceDescription
                .CostOfService = Services.CostOfService
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Services As DataObjects.tblServices)
            Dim objService As FMS.Business.tblService = (From c In SingletonAccess.FMSDataContextContignous.tblServices
                                                         Where c.ServicesID.Equals(Services.ServicesID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblServices.DeleteOnSubmit(objService)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblServices)
            Dim objServices = (From c In SingletonAccess.FMSDataContextContignous.tblServices
                               Where Not c.ServiceCode Is Nothing
                            Order By c.ServiceCode
                                          Select New DataObjects.tblServices(c)).ToList
            Return objServices
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objService As FMS.Business.tblService)
            With objService
                Me.ServicesID = .ServicesID
                Me.Sid = .Sid
                Me.ServiceCode = .ServiceCode
                Me.ServiceDescription = .ServiceDescription
                Me.CostOfService = .CostOfService
            End With
        End Sub
#End Region
    End Class
End Namespace

