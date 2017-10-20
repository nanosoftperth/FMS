Namespace DataObjects
    Public Class tblCustomerServices
#Region "Properties / enums"
        Public Property CustomerServiceID As System.Guid
        Public Property ID As Integer
        Public Property CSid As System.Nullable(Of Integer)
        Public Property CId As System.Nullable(Of Integer)
        Public Property ServiceFrequencyCode As System.Nullable(Of Short)
        Public Property ServiceUnits As System.Nullable(Of Single)
        Public Property ServicePrice As System.Nullable(Of Single)
        Public Property PerAnnumCharge As System.Nullable(Of Single)
        Public Property ServiceRun As System.Nullable(Of Short)
        Public Property ServiceComments As String
        Public Property UnitsHaveMoreThanOneRun As Boolean
        Public Property ServiceFrequency1 As System.Nullable(Of Short)
        Public Property ServiceFrequency2 As System.Nullable(Of Short)
        Public Property ServiceFrequency3 As System.Nullable(Of Short)
        Public Property ServiceFrequency4 As System.Nullable(Of Short)
        Public Property ServiceFrequency5 As System.Nullable(Of Short)
        Public Property ServiceFrequency6 As System.Nullable(Of Short)
        Public Property ServiceFrequency7 As System.Nullable(Of Short)
        Public Property ServiceFrequency8 As System.Nullable(Of Short)
        Public Property ServiceSortOrderCode As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(CustomerService As DataObjects.tblCustomerServices)
            Dim objCustomerService As New FMS.Business.tblCustomerService
            With objCustomerService
                .CustomerServiceID = Guid.NewGuid
                .CSid = CustomerService.CSid
                .CId = CustomerService.CId
                .ServiceFrequencyCode = CustomerService.ServiceFrequencyCode
                .ServiceUnits = CustomerService.ServiceUnits
                .ServicePrice = CustomerService.ServicePrice
                .PerAnnumCharge = CustomerService.PerAnnumCharge
                .ServiceRun = CustomerService.ServiceRun
                .ServiceComments = CustomerService.ServiceComments
                .UnitsHaveMoreThanOneRun = CustomerService.UnitsHaveMoreThanOneRun
                .ServiceFrequency1 = CustomerService.ServiceFrequency1
                .ServiceFrequency2 = CustomerService.ServiceFrequency2
                .ServiceFrequency3 = CustomerService.ServiceFrequency3
                .ServiceFrequency4 = CustomerService.ServiceFrequency4
                .ServiceFrequency5 = CustomerService.ServiceFrequency5
                .ServiceFrequency6 = CustomerService.ServiceFrequency6
                .ServiceFrequency7 = CustomerService.ServiceFrequency7
                .ServiceFrequency8 = CustomerService.ServiceFrequency8
                .ServiceSortOrderCode = CustomerService.ServiceSortOrderCode
            End With
            SingletonAccess.FMSDataContextContignous.tblCustomerServices.InsertOnSubmit(objCustomerService)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(CustomerService As DataObjects.tblCustomerServices)
            Dim objCustomerService As FMS.Business.tblCustomerService = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerServices
                                                           Where c.CustomerServiceID.Equals(CustomerService.CustomerServiceID)).SingleOrDefault
            With objCustomerService
                .CSid = CustomerService.CSid
                .CId = CustomerService.CId
                .ServiceFrequencyCode = CustomerService.ServiceFrequencyCode
                .ServiceUnits = CustomerService.ServiceUnits
                .ServicePrice = CustomerService.ServicePrice
                .PerAnnumCharge = CustomerService.PerAnnumCharge
                .ServiceRun = CustomerService.ServiceRun
                .ServiceComments = CustomerService.ServiceComments
                .UnitsHaveMoreThanOneRun = CustomerService.UnitsHaveMoreThanOneRun
                .ServiceFrequency1 = CustomerService.ServiceFrequency1
                .ServiceFrequency2 = CustomerService.ServiceFrequency2
                .ServiceFrequency3 = CustomerService.ServiceFrequency3
                .ServiceFrequency4 = CustomerService.ServiceFrequency4
                .ServiceFrequency5 = CustomerService.ServiceFrequency5
                .ServiceFrequency6 = CustomerService.ServiceFrequency6
                .ServiceFrequency7 = CustomerService.ServiceFrequency7
                .ServiceFrequency8 = CustomerService.ServiceFrequency8
                .ServiceSortOrderCode = CustomerService.ServiceSortOrderCode
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(CustomerService As DataObjects.tblCustomerServices)
            Dim objCustomerService As FMS.Business.tblCustomerService = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerServices
                                                         Where c.CustomerServiceID.Equals(CustomerService.CustomerServiceID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblCustomerServices.DeleteOnSubmit(objCustomerService)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblCustomerServices)
            Dim objZones = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerServices
                            Order By c.CSid
                                          Select New DataObjects.tblCustomerServices(c)).ToList
            Return objZones
        End Function
        Public Shared Function GetAllByCid(cid As Integer) As List(Of DataObjects.tblCustomerServices)
            Dim objZones = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerServices
                            Where c.CId.Equals(cid)
                            Order By c.CSid
                                          Select New DataObjects.tblCustomerServices(c)).ToList
            Return objZones
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objCustomerService As FMS.Business.tblCustomerService)
            With objCustomerService
                Me.CustomerServiceID = .CustomerServiceID
                Me.ID = .ID
                Me.CSid = .CSid
                Me.CId = .CId
                Me.ServiceFrequencyCode = .ServiceFrequencyCode
                Me.ServiceUnits = .ServiceUnits
                Me.ServicePrice = .ServicePrice
                Me.PerAnnumCharge = .PerAnnumCharge
                Me.ServiceRun = .ServiceRun
                Me.ServiceComments = .ServiceComments
                Me.UnitsHaveMoreThanOneRun = .UnitsHaveMoreThanOneRun
                Me.ServiceFrequency1 = .ServiceFrequency1
                Me.ServiceFrequency2 = .ServiceFrequency2
                Me.ServiceFrequency3 = .ServiceFrequency3
                Me.ServiceFrequency4 = .ServiceFrequency4
                Me.ServiceFrequency5 = .ServiceFrequency5
                Me.ServiceFrequency6 = .ServiceFrequency6
                Me.ServiceFrequency7 = .ServiceFrequency7
                Me.ServiceFrequency8 = .ServiceFrequency8
                Me.ServiceSortOrderCode = .ServiceSortOrderCode
            End With
        End Sub
#End Region
    End Class
End Namespace

