Namespace DataObjects
    Public Class tblCustomers
#Region "Properties / enums"
        Public Property CustomerID As System.Guid
        Public Property Cid As Integer
        Public Property CustomerName As String
        Public Property AddressLine1 As String
        Public Property AddressLine2 As String
        Public Property State As System.Nullable(Of Integer)
        Public Property Suburb As String
        Public Property PostCode As String
        Public Property CustomerContactName As String
        Public Property CustomerPhone As String
        Public Property CustomerMobile As String
        Public Property CustomerFax As String
        Public Property CustomerComments As String
        Public Property CustomerAgentName As String
        Public Property CustomerRating As System.Nullable(Of Short)
        Public Property Zone As System.Nullable(Of Integer)
        Public Property MYOBCustomerNumber As String
        Public Property CustomerValue As System.Nullable(Of Double)
        Public Property InactiveCustomer As Boolean
        Public Property CustomerCommencementDate As System.Nullable(Of Date)
        Public Property chkCustomerExcludeFuelLevy As Boolean
        Public Property cmbRateIncrease As System.Nullable(Of Short)
#End Region
#Region "CRUD"
        Public Shared Sub Create(customer As DataObjects.tblCustomers)
            Dim objCustomer As New FMS.Business.tblCustomer
            With objCustomer
                .Cid = GetLastIDUsed() + 1
                .CustomerName = customer.CustomerName
                .AddressLine1 = customer.AddressLine1
                .AddressLine2 = customer.AddressLine2
                .State = customer.State
                .Suburb = customer.Suburb
                .PostCode = customer.PostCode
                .CustomerContactName = customer.CustomerContactName
                .CustomerPhone = customer.CustomerPhone
                .CustomerMobile = customer.CustomerMobile
                .CustomerFax = customer.CustomerFax
                .CustomerComments = customer.CustomerComments
                .CustomerAgentName = customer.CustomerAgentName
                .CustomerRating = customer.CustomerRating
                .Zone = customer.Zone
                .MYOBCustomerNumber = customer.MYOBCustomerNumber
                .CustomerValue = customer.CustomerValue
                .InactiveCustomer = customer.InactiveCustomer
                .CustomerCommencementDate = customer.CustomerCommencementDate
                .chkCustomerExcludeFuelLevy = customer.chkCustomerExcludeFuelLevy
                .cmbRateIncrease = customer.cmbRateIncrease
            End With
            SingletonAccess.FMSDataContextContignous.tblCustomers.InsertOnSubmit(objCustomer)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(customer As DataObjects.tblCustomers)
            Dim objCustomer As FMS.Business.tblCustomer = (From c In SingletonAccess.FMSDataContextContignous.tblCustomers
                                                           Where c.Cid.Equals(customer.Cid)).SingleOrDefault
            With objCustomer
                .CustomerID = customer.CustomerID
                .CustomerName = customer.CustomerName
                .AddressLine1 = customer.AddressLine1
                .AddressLine2 = customer.AddressLine2
                .State = customer.State
                .Suburb = customer.Suburb
                .PostCode = customer.PostCode
                .CustomerContactName = customer.CustomerContactName
                .CustomerPhone = customer.CustomerPhone
                .CustomerMobile = customer.CustomerMobile
                .CustomerFax = customer.CustomerFax
                .CustomerComments = customer.CustomerComments
                .CustomerAgentName = customer.CustomerAgentName
                .CustomerRating = customer.CustomerRating
                .Zone = customer.Zone
                .MYOBCustomerNumber = customer.MYOBCustomerNumber
                .CustomerValue = customer.CustomerValue
                .InactiveCustomer = customer.InactiveCustomer
                .CustomerCommencementDate = customer.CustomerCommencementDate
                .chkCustomerExcludeFuelLevy = customer.chkCustomerExcludeFuelLevy
                .cmbRateIncrease = customer.cmbRateIncrease
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Customer As DataObjects.tblCustomers)
            Dim objCustomer As FMS.Business.tblCustomer = (From c In SingletonAccess.FMSDataContextContignous.tblCustomers
                                                           Where c.CustomerID.Equals(Customer.CustomerID) And c.Cid.Equals(Customer.Cid)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblCustomers.DeleteOnSubmit(objCustomer)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Private Shared Function GetLastIDUsed() As Integer
            Dim objCustomer = (From c In SingletonAccess.FMSDataContextContignous.tblCustomers
                               Order By c.Cid Descending
                               Select New DataObjects.tblCustomers(c)).First()
            Return objCustomer.Cid
        End Function
        Public Shared Function GetAll() As List(Of DataObjects.tblCustomers)

            Dim objCustomer = (From c In SingletonAccess.FMSDataContextContignous.tblCustomers
                               Select New DataObjects.tblCustomers(c)).ToList
            Return objCustomer
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTblCustomer As FMS.Business.tblCustomer)
            With objTblCustomer
                Me.CustomerID = .CustomerID
                Me.Cid = .Cid
                Me.CustomerName = .CustomerName
                Me.AddressLine1 = .AddressLine1
                Me.AddressLine2 = .AddressLine2
                Me.State = .State
                Me.Suburb = .Suburb
                Me.PostCode = .PostCode
                Me.CustomerContactName = .CustomerContactName
                Me.CustomerPhone = .CustomerPhone
                Me.CustomerMobile = .CustomerMobile
                Me.CustomerFax = .CustomerFax
                Me.CustomerComments = .CustomerComments
                Me.CustomerAgentName = .CustomerAgentName
                Me.CustomerRating = .CustomerRating
                Me.Zone = .Zone
                Me.MYOBCustomerNumber = .MYOBCustomerNumber
                Me.CustomerValue = .CustomerValue
                Me.InactiveCustomer = .InactiveCustomer
                Me.CustomerCommencementDate = .CustomerCommencementDate
                Me.chkCustomerExcludeFuelLevy = .chkCustomerExcludeFuelLevy
                Me.cmbRateIncrease = .cmbRateIncrease
            End With
        End Sub
#End Region
    End Class
End Namespace

