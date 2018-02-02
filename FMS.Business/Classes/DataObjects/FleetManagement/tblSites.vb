Namespace DataObjects
    Public Class tblSites
#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property SiteID As System.Guid
        Public Property Cid As Integer
        Public Property SiteName As String
        Public Property Customer As System.Nullable(Of Short)
        Public Property CustomerSortOrder As System.Nullable(Of Integer)
        Public Property AddressLine1 As String
        Public Property AddressLine2 As String
        Public Property AddressLine3 As String
        Public Property AddressLine4 As String
        Public Property Suburb As String
        Public Property State As String
        Public Property StateSortOrder As System.Nullable(Of Integer)
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
        Public Property InitialContractPeriodSortOrder As System.Nullable(Of Integer)
        Public Property SiteContractExpiry As System.Nullable(Of Date)
        Public Property SiteCeaseDate As System.Nullable(Of Date)
        Public Property SiteCeaseReason As System.Nullable(Of Integer)
        Public Property ContractCeaseReasonsSortOrder As System.Nullable(Of Integer)
        Public Property InvoiceFrequency As System.Nullable(Of Integer)
        Public Property InvoicingFrequencySortOrder As System.Nullable(Of Integer)
        Public Property InvoiceCommencing As System.Nullable(Of Date)
        Public Property InvoiceCommencingString As String

        Public Property IndustryGroup As System.Nullable(Of Short)
        Public Property IndustrySortOrder As System.Nullable(Of Integer)
        Public Property PreviousSupplier As System.Nullable(Of Short)
        Public Property PreviousSupplierSortOrder As System.Nullable(Of Integer)
        Public Property LostBusinessTo As System.Nullable(Of Short)
        Public Property LostBusinessToSortOrder As System.Nullable(Of Integer)
        Public Property SalesPerson As System.Nullable(Of Short)
        Public Property SalesPersonSortOrder As System.Nullable(Of Integer)
        Public Property InitialServiceAgreementNo As String
        Public Property InvoiceMonth1 As System.Nullable(Of Integer)
        Public Property InvoiceMonth2 As System.Nullable(Of Integer)
        Public Property InvoiceMonth3 As System.Nullable(Of Integer)
        Public Property InvoiceMonth4 As System.Nullable(Of Integer)
        Public Property GeneralSiteServiceComments As String
        Public Property TotalUnits As System.Nullable(Of Double)
        Public Property TotalAmount As System.Nullable(Of Double)
        Public Property Zone As System.Nullable(Of Integer)
        Public Property ZoneSortOrder As System.Nullable(Of Integer)
        Public Property SeparateInvoice As Boolean
        Public Property PurchaseOrderNumber As String
        Public Property chkSitesExcludeFuelLevy As Boolean
        Public Property cmbRateIncrease As System.Nullable(Of Short)
        Public Property cmbRateIncreaseSortOrder As System.Nullable(Of Integer)
        Public Property CustomerName As String
        Public Property CustomerRating As System.Nullable(Of Short)
        Public Property CustomerRatingDesc As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(Site As DataObjects.tblSites)
            With New LINQtoSQLClassesDataContext
                Dim appId = ThisSession.ApplicationID
                Dim objSite As New FMS.Business.tblSite
                With objSite
                    .ApplicationId = appId
                    .SiteID = Guid.NewGuid
                    .Cid = tblProjectID.SiteIDCreateOrUpdate(appId)
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
                .tblSites.InsertOnSubmit(objSite)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(Site As DataObjects.tblSites)
            With New LINQtoSQLClassesDataContext
                Dim objSite As FMS.Business.tblSite = (From c In .tblSites
                                                       Where c.Cid.Equals(Site.Cid) And c.ApplicationId.Equals(ThisSession.ApplicationID)).SingleOrDefault
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
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(Site As DataObjects.tblSites)
            With New LINQtoSQLClassesDataContext
                Dim objSite As FMS.Business.tblSite = (From c In .tblSites
                                                       Where c.Cid.Equals(Site.Cid) And c.ApplicationId.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblSites.DeleteOnSubmit(objSite)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblSites)
            Try
                Dim objSites As New List(Of DataObjects.tblSites)
                With New LINQtoSQLClassesDataContext
                    objSites = (From c In .tblSites
                                Where c.ApplicationId.Equals(ThisSession.ApplicationID)
                                Order By c.SiteName
                                Select New DataObjects.tblSites(c)).ToList

                    .Dispose()
                End With
                Return objSites
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllByCustomer(cust As Integer) As List(Of DataObjects.tblSites)
            Try
                Dim objSites As New List(Of DataObjects.tblSites)
                With New LINQtoSQLClassesDataContext
                    objSites = (From c In .tblSites
                                Where c.Customer.Equals(cust) And (Not c.Customer Is Nothing And Not c.Customer.Equals(0) And c.ApplicationId.Equals(ThisSession.ApplicationID))
                                Order By c.SiteName
                                Select New DataObjects.tblSites(c)).ToList
                    .Dispose()
                End With
                Return objSites
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllBySiteID(cid As Integer) As DataObjects.tblSites
            Dim objSites = (From s In DataObjects.tblSites.GetAllWithZoneSortOrder()
                            Where s.Cid.Equals(cid) And s.ApplicationId.Equals(ThisSession.ApplicationID)
                            Order By s.SiteName
                            Select New DataObjects.tblSites() With {.SiteID = s.SiteID, .Cid = s.Cid, .SiteName = s.SiteName, .Customer = s.Customer, .AddressLine1 = s.AddressLine1,
                                                                    .AddressLine2 = s.AddressLine2, .AddressLine3 = s.AddressLine3, .AddressLine4 = s.AddressLine4, .Suburb = s.Suburb,
                                                                    .State = s.State, .PostCode = s.PostCode, .PhoneNo = s.PhoneNo, .FaxNo = s.FaxNo, .SiteContactName = s.SiteContactName,
                                                                    .SiteContactPhone = s.SiteContactPhone, .SiteContactFax = s.SiteContactFax, .SiteContactMobile = s.SiteContactMobile,
                                                                    .SiteContactEmail = s.SiteContactEmail, .PostalAddressLine1 = s.PostalAddressLine1, .PostalAddressLine2 = s.PostalAddressLine2,
                                                                    .PostalSuburb = s.PostalSuburb, .PostalState = s.PostalState, .PostalPostCode = s.PostalPostCode, .SiteStartDate = s.SiteStartDate,
                                                                    .SitePeriod = s.SitePeriod, .SiteContractExpiry = s.SiteContractExpiry, .SiteCeaseDate = s.SiteCeaseDate, .SiteCeaseReason = s.SiteCeaseReason,
                                                                    .InvoiceFrequency = s.InvoiceFrequency, .InvoiceCommencing = s.InvoiceCommencing, .IndustryGroup = s.IndustryGroup, .PreviousSupplier = s.PreviousSupplier,
                                                                    .LostBusinessTo = s.LostBusinessTo, .SalesPerson = s.SalesPerson, .InitialServiceAgreementNo = s.InitialServiceAgreementNo, .InvoiceMonth1 = s.InvoiceMonth1,
                                                                    .InvoiceMonth2 = s.InvoiceMonth2, .InvoiceMonth3 = s.InvoiceMonth3, .InvoiceMonth4 = s.InvoiceMonth4,
                                                                    .GeneralSiteServiceComments = s.GeneralSiteServiceComments, .TotalUnits = s.TotalUnits, .TotalAmount = s.TotalAmount, .Zone = s.Zone,
                                                                    .ZoneSortOrder = s.ZoneSortOrder, .SeparateInvoice = s.SeparateInvoice, .PurchaseOrderNumber = s.PurchaseOrderNumber, .chkSitesExcludeFuelLevy = s.chkSitesExcludeFuelLevy,
                                                                    .cmbRateIncrease = s.cmbRateIncrease, .StateSortOrder = s.StateSortOrder, .CustomerSortOrder = s.CustomerSortOrder,
                                                                    .IndustrySortOrder = s.IndustrySortOrder, .PreviousSupplierSortOrder = s.PreviousSupplierSortOrder, .SalesPersonSortOrder = s.SalesPersonSortOrder,
                                                                    .InitialContractPeriodSortOrder = s.InitialContractPeriodSortOrder, .ContractCeaseReasonsSortOrder = s.ContractCeaseReasonsSortOrder,
                                                                    .LostBusinessToSortOrder = s.LostBusinessToSortOrder, .InvoicingFrequencySortOrder = s.InvoicingFrequencySortOrder,
                                                                    .cmbRateIncreaseSortOrder = s.cmbRateIncreaseSortOrder, .CustomerName = s.CustomerName, .CustomerRating = s.CustomerRating,
                                                                    .CustomerRatingDesc = s.CustomerRatingDesc}).SingleOrDefault
            Return objSites
        End Function
        Public Shared Function GetAllWithZoneSortOrder() As List(Of DataObjects.tblSites)
            Try
                Dim objSites As New List(Of DataObjects.tblSites)
                With New LINQtoSQLClassesDataContext
                    objSites = (From s In .usp_GetSites(ThisSession.ApplicationID)
                                Select New DataObjects.tblSites() With {.SiteID = s.SiteID, .Cid = s.Cid, .SiteName = s.SiteName, .Customer = s.Customer, .AddressLine1 = s.AddressLine1,
                                                                            .AddressLine2 = s.AddressLine2, .AddressLine3 = s.AddressLine3, .AddressLine4 = s.AddressLine4, .Suburb = s.Suburb,
                                                                            .State = s.State, .PostCode = s.PostCode, .PhoneNo = s.PhoneNo, .FaxNo = s.FaxNo, .SiteContactName = s.SiteContactName,
                                                                            .SiteContactPhone = s.SiteContactPhone, .SiteContactFax = s.SiteContactFax, .SiteContactMobile = s.SiteContactMobile,
                                                                            .SiteContactEmail = s.SiteContactEmail, .PostalAddressLine1 = s.PostalAddressLine1, .PostalAddressLine2 = s.PostalAddressLine2,
                                                                            .PostalSuburb = s.PostalSuburb, .PostalState = s.PostalState, .PostalPostCode = s.PostalPostCode, .SiteStartDate = s.SiteStartDate,
                                                                            .SitePeriod = s.SitePeriod, .SiteContractExpiry = s.SiteContractExpiry, .SiteCeaseDate = s.SiteCeaseDate, .SiteCeaseReason = s.SiteCeaseReason,
                                                                            .InvoiceFrequency = s.InvoiceFrequency, .InvoiceCommencing = s.InvoiceCommencing, .IndustryGroup = s.IndustryGroup, .PreviousSupplier = s.PreviousSupplier,
                                                                            .LostBusinessTo = s.LostBusinessTo, .SalesPerson = s.SalesPerson, .InitialServiceAgreementNo = s.InitialServiceAgreementNo, .InvoiceMonth1 = s.InvoiceMonth1,
                                                                            .InvoiceMonth2 = s.InvoiceMonth2, .InvoiceMonth3 = s.InvoiceMonth3, .InvoiceMonth4 = s.InvoiceMonth4,
                                                                            .GeneralSiteServiceComments = s.GeneralSiteServiceComments, .TotalUnits = s.TotalUnits, .TotalAmount = s.TotalAmount, .Zone = s.Zone,
                                                                            .ZoneSortOrder = s.ZoneSortOrder, .SeparateInvoice = s.SeparateInvoice, .PurchaseOrderNumber = s.PurchaseOrderNumber, .chkSitesExcludeFuelLevy = s.chkSitesExcludeFuelLevy,
                                                                            .cmbRateIncrease = s.cmbRateIncrease, .StateSortOrder = s.StateSortOrder, .CustomerSortOrder = s.CustomerSortOrder,
                                                                            .IndustrySortOrder = s.IndustrySortOrder, .PreviousSupplierSortOrder = s.PreviousSupplierSortOrder, .SalesPersonSortOrder = s.SalesPersonSortOrder,
                                                                            .InitialContractPeriodSortOrder = s.InitialContractPeriodSortOrder, .ContractCeaseReasonsSortOrder = s.ContractCeaseReasonsSortOrder,
                                                                            .LostBusinessToSortOrder = s.LostBusinessToSortOrder, .InvoicingFrequencySortOrder = s.InvoicingFrequencySortOrder,
                                                                            .cmbRateIncreaseSortOrder = s.cmbRateIncreaseSortOrder, .CustomerName = s.CustomerName, .CustomerRating = s.CustomerRating,
                                                                            .CustomerRatingDesc = s.CustomerRatingDesc}).ToList
                    .Dispose()
                End With
                Return objSites
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        ' For Report - CAN 18
        Public Shared Function GetAllByPostalCode(postcode As Integer) As List(Of DataObjects.tblSites)
            Dim appId = ThisSession.ApplicationID
            Dim lstSites = (From s In SingletonAccess.FMSDataContextNew.tblSites
                            Where s.PostCode = postcode And s.ApplicationId = appId
                            Select New DataObjects.tblSites(s)).ToList()

            Dim objSites As New List(Of DataObjects.tblSites)

            For Each site In lstSites
                Dim row As New DataObjects.tblSites

                row.SiteID = site.SiteID
                row.SiteName = site.SiteName
                row.AddressLine1 = site.AddressLine1 + " " + site.AddressLine2
                row.Suburb = site.Suburb
                row.PostCode = site.PostCode
                row.SiteContactName = site.SiteContactName
                row.SiteContactPhone = site.SiteContactPhone

                objSites.Add(row)

            Next

            Return objSites
        End Function

        Public Shared Function GetAllOrderyBySuburb() As List(Of DataObjects.tblSites)
            Dim appId = ThisSession.ApplicationID
            Dim objSites = (From c In SingletonAccess.FMSDataContextContignous.tblSites
                            Where c.ApplicationId = appId
                            Order By c.Suburb
                            Select New DataObjects.tblSites(c)).ToList
            Return objSites
        End Function

        Public Shared Function GetAllPerApplication() As List(Of DataObjects.tblSites)
            Dim AppId = ThisSession.ApplicationID

            Dim objSites = (From c In SingletonAccess.FMSDataContextContignous.tblSites
                            Where c.ApplicationId = AppId
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
                Me.ApplicationId = .ApplicationId
                Me.SiteID = .SiteID
                Me.Cid = .Cid
                Me.SiteName = .SiteName
                Me.Customer = .Customer
                Me.CustomerSortOrder = 0
                Me.AddressLine1 = .AddressLine1
                Me.AddressLine2 = .AddressLine2
                Me.AddressLine3 = .AddressLine3
                Me.AddressLine4 = .AddressLine4
                Me.Suburb = .Suburb
                Me.State = .State
                Me.StateSortOrder = 0
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
                Me.InitialContractPeriodSortOrder = 0
                Me.SiteContractExpiry = .SiteContractExpiry
                Me.SiteCeaseDate = .SiteCeaseDate
                Me.SiteCeaseReason = .SiteCeaseReason
                Me.ContractCeaseReasonsSortOrder = 0
                Me.InvoiceFrequency = .InvoiceFrequency
                Me.InvoicingFrequencySortOrder = 0
                Me.InvoiceCommencing = .InvoiceCommencing
                Me.IndustryGroup = .IndustryGroup
                Me.IndustrySortOrder = 0
                Me.PreviousSupplier = .PreviousSupplier
                Me.PreviousSupplierSortOrder = 0
                Me.LostBusinessTo = .LostBusinessTo
                Me.LostBusinessToSortOrder = 0
                Me.SalesPerson = .SalesPerson
                Me.SalesPersonSortOrder = 0
                Me.InitialServiceAgreementNo = .InitialServiceAgreementNo
                Me.InvoiceMonth1 = .InvoiceMonth1
                Me.InvoiceMonth2 = .InvoiceMonth2
                Me.InvoiceMonth3 = .InvoiceMonth3
                Me.InvoiceMonth4 = .InvoiceMonth4
                Me.GeneralSiteServiceComments = .GeneralSiteServiceComments
                Me.TotalUnits = .TotalUnits
                Me.TotalAmount = .TotalAmount
                Me.Zone = .Zone
                Me.ZoneSortOrder = 0
                Me.SeparateInvoice = .SeparateInvoice
                Me.PurchaseOrderNumber = .PurchaseOrderNumber
                Me.chkSitesExcludeFuelLevy = .chkSitesExcludeFuelLevy
                Me.cmbRateIncrease = .cmbRateIncrease
                Me.cmbRateIncreaseSortOrder = 0
            End With
        End Sub
#End Region
    End Class
End Namespace

