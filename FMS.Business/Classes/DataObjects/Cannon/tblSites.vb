Namespace DataObjects
    Public Class tblSites
#Region "Properties / enums"
        Public Property SiteID As System.Guid
        Public Property Cid As Integer
        Public Property SiteName As String
        Public Property Customer As System.Nullable(Of Short)
        Public Property AddressLine1 As String
        Public Property AddressLine2 As String
        Public Property AddressLine3 As String
        Public Property AddressLine4 As String
        Public Property Suburb As String
        Public Property State As String
        Public Property PostCode As System.Nullable(Of Short)
        Public Property PhoneNo As String
        Public Property FaxNo As String
        Public Property SiteContactName As String
        Public Property SiteContactPhone As String
        Public Property SiteContactFax As String
        Public Property SiteContactMobile As String
        Public Property SiteContactEmail As String
        Public Property PostalAddressLine1 As String
        Public Property PostalAddressLine2 As String
        Public Property PostalSuburb As String
        Public Property PostalState As String
        Public Property PostalPostCode As System.Nullable(Of Short)
        Public Property SiteStartDate As System.Nullable(Of Date)
        Public Property SitePeriod As System.Nullable(Of Integer)
        Public Property SiteContractExpiry As System.Nullable(Of Date)
        Public Property SiteCeaseDate As System.Nullable(Of Date)
        Public Property SiteCeaseReason As System.Nullable(Of Integer)
        Public Property InvoiceFrequency As System.Nullable(Of Integer)
        Public Property InvoiceCommencing As System.Nullable(Of Date)
        Public Property IndustryGroup As System.Nullable(Of Short)
        Public Property PreviousSupplier As System.Nullable(Of Short)
        Public Property LostBusinessTo As System.Nullable(Of Short)
        Public Property SalesPerson As System.Nullable(Of Short)
        Public Property InitialServiceAgreementNo As String
        Public Property InvoiceMonth1 As System.Nullable(Of Integer)
        Public Property InvoiceMonth2 As System.Nullable(Of Integer)
        Public Property InvoiceMonth3 As System.Nullable(Of Integer)
        Public Property InvoiceMonth4 As System.Nullable(Of Integer)
        Public Property GeneralSiteServiceComments As String
        Public Property TotalUnits As System.Nullable(Of Double)
        Public Property TotalAmount As System.Nullable(Of Double)
        Public Property Zone As System.Nullable(Of Integer)
        Public Property SeparateInvoice As Boolean
        Public Property PurchaseOrderNumber As String
        Public Property chkSitesExcludeFuelLevy As Boolean
        Public Property cmbRateIncrease As System.Nullable(Of Short)
#End Region
#Region "CRUD"
        Public Shared Sub Create(Site As DataObjects.tblSites)
            Dim objSite As New FMS.Business.tblSite
            With objSite
                .SiteID = Guid.NewGuid
                .Cid = GetLastIDUsed() + 1
                .SiteName = Site.SiteName
                .Customer = Site.Customer
                .AddressLine1 = Site.AddressLine1
                .AddressLine2 = Site.AddressLine2
                .AddressLine3 = Site.AddressLine3
                .AddressLine4 = Site.AddressLine4
                .Suburb = Site.Suburb
                .State = Site.State
                .PostCode = Site.PostCode
                .PhoneNo = Site.PhoneNo
                .FaxNo = Site.FaxNo
                .SiteContactName = Site.SiteContactName
                .SiteContactPhone = Site.SiteContactPhone
                .SiteContactFax = Site.SiteContactFax
                .SiteContactMobile = Site.SiteContactMobile
                .SiteContactEmail = Site.SiteContactEmail
                .PostalAddressLine1 = Site.PostalAddressLine1
                .PostalAddressLine2 = Site.PostalAddressLine2
                .PostalSuburb = Site.PostalSuburb
                .PostalState = Site.PostalState
                .PostalPostCode = Site.PostalPostCode
                .SiteStartDate = Site.SiteStartDate
                .SitePeriod = Site.SitePeriod
                .SiteContractExpiry = Site.SiteContractExpiry
                .SiteCeaseDate = Site.SiteCeaseDate
                .SiteCeaseReason = Site.SiteCeaseReason
                .InvoiceFrequency = Site.InvoiceFrequency
                .InvoiceCommencing = Site.InvoiceCommencing
                .IndustryGroup = Site.IndustryGroup
                .PreviousSupplier = Site.PreviousSupplier
                .LostBusinessTo = Site.LostBusinessTo
                .SalesPerson = Site.SalesPerson
                .InitialServiceAgreementNo = Site.InitialServiceAgreementNo
                .InvoiceMonth1 = Site.InvoiceMonth1
                .InvoiceMonth2 = Site.InvoiceMonth2
                .InvoiceMonth3 = Site.InvoiceMonth3
                .InvoiceMonth4 = Site.InvoiceMonth4
                .GeneralSiteServiceComments = Site.GeneralSiteServiceComments
                .TotalUnits = Site.TotalUnits
                .TotalAmount = Site.TotalAmount
                .Zone = Site.Zone
                .SeparateInvoice = Site.SeparateInvoice
                .PurchaseOrderNumber = Site.PurchaseOrderNumber
                .chkSitesExcludeFuelLevy = Site.chkSitesExcludeFuelLevy
                .cmbRateIncrease = Site.cmbRateIncrease
            End With
            SingletonAccess.FMSDataContextContignous.tblSites.InsertOnSubmit(objSite)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Site As DataObjects.tblSites)
            Dim objSite As FMS.Business.tblSite = (From c In SingletonAccess.FMSDataContextContignous.tblSites
                                                           Where c.SiteID.Equals(Site.SiteID)).SingleOrDefault
            With objSite
                .SiteName = Site.SiteName
                .Customer = Site.Customer
                .AddressLine1 = Site.AddressLine1
                .AddressLine2 = Site.AddressLine2
                .AddressLine3 = Site.AddressLine3
                .AddressLine4 = Site.AddressLine4
                .Suburb = Site.Suburb
                .State = Site.State
                .PostCode = Site.PostCode
                .PhoneNo = Site.PhoneNo
                .FaxNo = Site.FaxNo
                .SiteContactName = Site.SiteContactName
                .SiteContactPhone = Site.SiteContactPhone
                .SiteContactFax = Site.SiteContactFax
                .SiteContactMobile = Site.SiteContactMobile
                .SiteContactEmail = Site.SiteContactEmail
                .PostalAddressLine1 = Site.PostalAddressLine1
                .PostalAddressLine2 = Site.PostalAddressLine2
                .PostalSuburb = Site.PostalSuburb
                .PostalState = Site.PostalState
                .PostalPostCode = Site.PostalPostCode
                .SiteStartDate = Site.SiteStartDate
                .SitePeriod = Site.SitePeriod
                .SiteContractExpiry = Site.SiteContractExpiry
                .SiteCeaseDate = Site.SiteCeaseDate
                .SiteCeaseReason = Site.SiteCeaseReason
                .InvoiceFrequency = Site.InvoiceFrequency
                .InvoiceCommencing = Site.InvoiceCommencing
                .IndustryGroup = Site.IndustryGroup
                .PreviousSupplier = Site.PreviousSupplier
                .LostBusinessTo = Site.LostBusinessTo
                .SalesPerson = Site.SalesPerson
                .InitialServiceAgreementNo = Site.InitialServiceAgreementNo
                .InvoiceMonth1 = Site.InvoiceMonth1
                .InvoiceMonth2 = Site.InvoiceMonth2
                .InvoiceMonth3 = Site.InvoiceMonth3
                .InvoiceMonth4 = Site.InvoiceMonth4
                .GeneralSiteServiceComments = Site.GeneralSiteServiceComments
                .TotalUnits = Site.TotalUnits
                .TotalAmount = Site.TotalAmount
                .Zone = Site.Zone
                .SeparateInvoice = Site.SeparateInvoice
                .PurchaseOrderNumber = Site.PurchaseOrderNumber
                .chkSitesExcludeFuelLevy = Site.chkSitesExcludeFuelLevy
                .cmbRateIncrease = Site.cmbRateIncrease
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Site As DataObjects.tblSites)
            Dim objSite As FMS.Business.tblSite = (From c In SingletonAccess.FMSDataContextContignous.tblSites
                                                         Where c.SiteID.Equals(Site.SiteID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblSites.DeleteOnSubmit(objSite)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Private Shared Function GetLastIDUsed() As Integer
            Dim objSite = SingletonAccess.FMSDataContextContignous.tblSites.Count
            Return objSite
        End Function
        Public Shared Function GetAll() As List(Of DataObjects.tblSites)
            Dim objSites = (From c In SingletonAccess.FMSDataContextContignous.tblSites
                            Order By c.SiteName
                            Select New DataObjects.tblSites(c)).ToList
            Return objSites
        End Function
        Public Shared Function GetAllByCustomer(cust As Integer) As List(Of DataObjects.tblSites)
            Dim objSites = (From c In SingletonAccess.FMSDataContextContignous.tblSites
                            Where c.Customer.Equals(cust) And (Not c.Customer Is Nothing And Not c.Customer.Equals(0))
                            Order By c.SiteName
                            Select New DataObjects.tblSites(c)).ToList
            Return objSites
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTblSite As FMS.Business.tblSite)
            With objTblSite
                Me.SiteID = .SiteID
                Me.Cid = .Cid
                Me.SiteName = .SiteName
                Me.Customer = .Customer
                Me.AddressLine1 = .AddressLine1
                Me.AddressLine2 = .AddressLine2
                Me.AddressLine3 = .AddressLine3
                Me.AddressLine4 = .AddressLine4
                Me.Suburb = .Suburb
                Me.State = .State
                Me.PostCode = .PostCode
                Me.PhoneNo = .PhoneNo
                Me.FaxNo = .FaxNo
                Me.SiteContactName = .SiteContactName
                Me.SiteContactPhone = .SiteContactPhone
                Me.SiteContactFax = .SiteContactFax
                Me.SiteContactMobile = .SiteContactMobile
                Me.SiteContactEmail = .SiteContactEmail
                Me.PostalAddressLine1 = .PostalAddressLine1
                Me.PostalAddressLine2 = .PostalAddressLine2
                Me.PostalSuburb = .PostalSuburb
                Me.PostalState = .PostalState
                Me.PostalPostCode = .PostalPostCode
                Me.SiteStartDate = .SiteStartDate
                Me.SitePeriod = .SitePeriod
                Me.SiteContractExpiry = .SiteContractExpiry
                Me.SiteCeaseDate = .SiteCeaseDate
                Me.SiteCeaseReason = .SiteCeaseReason
                Me.InvoiceFrequency = .InvoiceFrequency
                Me.InvoiceCommencing = .InvoiceCommencing
                Me.IndustryGroup = .IndustryGroup
                Me.PreviousSupplier = .PreviousSupplier
                Me.LostBusinessTo = .LostBusinessTo
                Me.SalesPerson = .SalesPerson
                Me.InitialServiceAgreementNo = .InitialServiceAgreementNo
                Me.InvoiceMonth1 = .InvoiceMonth1
                Me.InvoiceMonth2 = .InvoiceMonth2
                Me.InvoiceMonth3 = .InvoiceMonth3
                Me.InvoiceMonth4 = .InvoiceMonth4
                Me.GeneralSiteServiceComments = .GeneralSiteServiceComments
                Me.TotalUnits = .TotalUnits
                Me.TotalAmount = .TotalAmount
                Me.Zone = .Zone
                Me.SeparateInvoice = .SeparateInvoice
                Me.PurchaseOrderNumber = .PurchaseOrderNumber
                Me.chkSitesExcludeFuelLevy = .chkSitesExcludeFuelLevy
                Me.cmbRateIncrease = .cmbRateIncrease
            End With
        End Sub
#End Region
    End Class
End Namespace

