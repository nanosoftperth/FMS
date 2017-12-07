Namespace DataObjects
    Public Class tblCustomerServices
#Region "Properties / enums"
        Public Property CustomerServiceID As System.Guid
        Public Property ID As Integer
        Public Property CSid As System.Nullable(Of Integer)
        Public Property CId As System.Nullable(Of Integer)
        Public Property ServiceFrequencyCode As System.Nullable(Of Short)
        Public Property ServiceFrequencySortOrder As System.Nullable(Of Integer)
        Public Property ServiceUnits As System.Nullable(Of Single)
        Public Property ServicePrice As System.Nullable(Of Single)
        Public Property PerAnnumCharge As System.Nullable(Of Single)
        Public Property ServiceRun As System.Nullable(Of Short)
        Public Property ServiceRunSortOrder As System.Nullable(Of Integer)
        Public Property ServiceComments As String
        Public Property UnitsHaveMoreThanOneRun As Boolean
        Public Property ServiceFrequency1 As System.Nullable(Of Short)
        Public Property ServiceFrequency1SortOrder As System.Nullable(Of Integer)
        Public Property ServiceFrequency2 As System.Nullable(Of Short)
        Public Property ServiceFrequency2SortOrder As System.Nullable(Of Integer)
        Public Property ServiceFrequency3 As System.Nullable(Of Short)
        Public Property ServiceFrequency3SortOrder As System.Nullable(Of Integer)
        Public Property ServiceFrequency4 As System.Nullable(Of Short)
        Public Property ServiceFrequency4SortOrder As System.Nullable(Of Integer)
        Public Property ServiceFrequency5 As System.Nullable(Of Short)
        Public Property ServiceFrequency5SortOrder As System.Nullable(Of Integer)
        Public Property ServiceFrequency6 As System.Nullable(Of Short)
        Public Property ServiceFrequency6SortOrder As System.Nullable(Of Integer)
        Public Property ServiceFrequency7 As System.Nullable(Of Short)
        Public Property ServiceFrequency7SortOrder As System.Nullable(Of Integer)
        Public Property ServiceFrequency8 As System.Nullable(Of Short)
        Public Property ServiceFrequency8SortOrder As System.Nullable(Of Integer)
        Public Property ServiceSortOrderCode As String
        Public Property ServicesSortOrder As System.Nullable(Of Integer)
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
            GetCustomerServiceID = objCustomerService.CustomerServiceID
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
        Public Shared Property GetCustomerServiceID() As Guid

        Public Shared Function GetAll() As List(Of DataObjects.tblCustomerServices)
            Dim objZones = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerServices
                            Order By c.CSid
                                          Select New DataObjects.tblCustomerServices(c)).ToList
            Return objZones
        End Function
        Public Shared Function GetAllByCid(cid As Integer) As List(Of DataObjects.tblCustomerServices)
            Dim objCustomerServices = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerServices
                            Where c.CId.Equals(cid)
                            Order By c.CSid
                                          Select New DataObjects.tblCustomerServices(c)).ToList
            Return objCustomerServices
        End Function
        Public Shared Function GetRecalculatedServices(cid As Integer) As DataObjects.ReCalculatedServices
            Dim objCustomers = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerServices
                                Where c.CId.Equals(cid))
            Dim totalPerAnnum As Double
            Dim totalServiceUnits As Double
            For Each cust In objCustomers
                If Not cust.PerAnnumCharge Is Nothing Then
                    Dim dblPerAnnum As Double = 0
                    If Double.TryParse(cust.PerAnnumCharge, dblPerAnnum) Then
                        totalPerAnnum += dblPerAnnum
                    End If
                End If
                If Not cust.ServiceUnits Is Nothing Then
                    Dim dblServiceUnits As Double = 0
                    If Double.TryParse(cust.ServiceUnits, dblServiceUnits) Then
                        totalServiceUnits += dblServiceUnits
                    End If
                End If
            Next
            Dim recalculate As New DataObjects.ReCalculatedServices()
            recalculate.PerAnnumCharge = totalPerAnnum
            recalculate.ServiceUnits = totalServiceUnits
            Return recalculate
        End Function
        Public Shared Function GetAllByCidWithSortOrders(cid As Integer) As List(Of DataObjects.tblCustomerServices)
            Dim objCustomerServices = (From c In SingletonAccess.FMSDataContextContignous.usp_GetCustomerServices
                            Where c.CId.Equals(cid)
                            Order By c.CSid
                                          Select New DataObjects.tblCustomerServices() With {.CustomerServiceID = c.CustomerServiceID, .ID = c.ID,
                                                                                             .CSid = c.CSid, .CId = c.CId, .ServiceFrequencyCode = c.ServiceFrequencyCode,
                                                                                             .ServiceUnits = c.ServiceUnits, .ServicePrice = c.ServicePrice,
                                                                                             .PerAnnumCharge = c.PerAnnumCharge, .ServiceRun = c.ServiceRun,
                                                                                             .ServiceComments = c.ServiceComments, .UnitsHaveMoreThanOneRun = c.UnitsHaveMoreThanOneRun,
                                                                                             .ServiceFrequency1 = c.ServiceFrequency1, .ServiceFrequency2 = c.ServiceFrequency2,
                                                                                             .ServiceFrequency3 = c.ServiceFrequency3, .ServiceFrequency4 = c.ServiceFrequency4,
                                                                                             .ServiceFrequency5 = c.ServiceFrequency5, .ServiceFrequency6 = c.ServiceFrequency6,
                                                                                             .ServiceFrequency7 = c.ServiceFrequency7, .ServiceFrequency8 = c.ServiceFrequency8,
                                                                                             .ServiceSortOrderCode = c.ServiceSortOrderCode, .ServicesSortOrder = c.ServicesSortOrder,
                                                                                             .ServiceFrequencySortOrder = c.ServiceFrequencySortOrder, .ServiceRunSortOrder = c.ServiceRunSortOrder,
                                                                                             .ServiceFrequency1SortOrder = c.ServiceFrequency1SortOrder, .ServiceFrequency2SortOrder = c.ServiceFrequency2SortOrder,
                                                                                             .ServiceFrequency3SortOrder = c.ServiceFrequency3SortOrder, .ServiceFrequency4SortOrder = c.ServiceFrequency4SortOrder,
                                                                                             .ServiceFrequency5SortOrder = c.ServiceFrequency5SortOrder, .ServiceFrequency6SortOrder = c.ServiceFrequency6SortOrder,
                                                                                             .ServiceFrequency7SortOrder = c.ServiceFrequency7SortOrder, .ServiceFrequency8SortOrder = c.ServiceFrequency8SortOrder}).ToList
            Return objCustomerServices
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
                Me.ServiceFrequencySortOrder = 0
                Me.ServiceUnits = .ServiceUnits
                Me.ServicePrice = .ServicePrice
                Me.PerAnnumCharge = .PerAnnumCharge
                Me.ServiceRun = .ServiceRun
                Me.ServiceRunSortOrder = 0
                Me.ServiceComments = .ServiceComments
                Me.UnitsHaveMoreThanOneRun = .UnitsHaveMoreThanOneRun
                Me.ServiceFrequency1 = .ServiceFrequency1
                Me.ServiceFrequency1SortOrder = 0
                Me.ServiceFrequency2 = .ServiceFrequency2
                Me.ServiceFrequency2SortOrder = 0
                Me.ServiceFrequency3 = .ServiceFrequency3
                Me.ServiceFrequency3SortOrder = 0
                Me.ServiceFrequency4 = .ServiceFrequency4
                Me.ServiceFrequency4SortOrder = 0
                Me.ServiceFrequency5 = .ServiceFrequency5
                Me.ServiceFrequency5SortOrder = 0
                Me.ServiceFrequency6 = .ServiceFrequency6
                Me.ServiceFrequency6SortOrder = 0
                Me.ServiceFrequency7 = .ServiceFrequency7
                Me.ServiceFrequency7SortOrder = 0
                Me.ServiceFrequency8 = .ServiceFrequency8
                Me.ServiceFrequency8SortOrder = 0
                Me.ServiceSortOrderCode = .ServiceSortOrderCode
                Me.ServicesSortOrder = 0
            End With
        End Sub
#End Region
    End Class
    Public Class ReCalculatedServices
        Public Property PerAnnumCharge As Double
        Public Property ServiceUnits As Double
    End Class
End Namespace

