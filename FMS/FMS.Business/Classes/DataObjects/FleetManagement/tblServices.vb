Namespace DataObjects
    Public Class tblServices
#Region "Properties / enums"
        Public Property ServicesID As System.Guid
        Public Property Sid As Integer
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property ServiceCode As String
        Public Property ServiceDescription As String
        Public Property CostOfService As System.Nullable(Of Single)
#End Region
#Region "CRUD"
        Public Shared Sub Create(Services As DataObjects.tblServices)
            Dim objService As New FMS.Business.tblService
            With objService
                .ServicesID = Guid.NewGuid
                .Sid = tblProjectID.ServicesIDCreateOrUpdate()
                .ApplicationID = Services.ApplicationID
                .ServiceCode = Services.ServiceCode
                .ServiceDescription = Services.ServiceDescription
                .CostOfService = Services.CostOfService
            End With
            SingletonAccess.FMSDataContextContignous.tblServices.InsertOnSubmit(objService)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Services As DataObjects.tblServices)
            Dim objService As FMS.Business.tblService = (From c In SingletonAccess.FMSDataContextContignous.tblServices
                                                           Where c.ServicesID.Equals(Services.ServicesID) And c.ApplicationID.Equals(Services.ApplicationID)).SingleOrDefault
            With objService
                .ServiceCode = Services.ServiceCode
                .ServiceDescription = Services.ServiceDescription
                .CostOfService = Services.CostOfService
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Services As DataObjects.tblServices)
            Dim objService As FMS.Business.tblService = (From c In SingletonAccess.FMSDataContextContignous.tblServices
                                                         Where c.ServicesID.Equals(Services.ServicesID) And c.ApplicationID.Equals(Services.ApplicationID)).SingleOrDefault
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
        Public Shared Function GetAllByApplicationID(appID As System.Guid) As List(Of DataObjects.tblServices)
            Dim objServices = (From c In SingletonAccess.FMSDataContextContignous.tblServices
                               Where Not c.ServiceCode Is Nothing And c.ApplicationID.Equals(appID)
                               Order By c.ServiceCode
                                          Select New DataObjects.tblServices(c)).ToList
            Return objServices
        End Function
        Public Shared Function GetAllWithNull() As List(Of DataObjects.tblServices)
            Dim objServices = (From c In SingletonAccess.FMSDataContextContignous.tblServices
                            Order By c.ServiceDescription
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
                Me.ApplicationID = .ApplicationID
                Me.ServiceCode = .ServiceCode
                Me.ServiceDescription = .ServiceDescription
                Me.CostOfService = .CostOfService
            End With
        End Sub
#End Region
    End Class
End Namespace

