Namespace DataObjects
    Public Class tblProjectID
#Region "Properties / enums"
#End Region
#Region "CRUD"
#Region "tblCustomer"
        Private Shared Function GetLatestCustomerID() As FMS.Business.tblCustomer
            Return (From c In SingletonAccess.FMSDataContextContignous.tblCustomers
                       Order By c.Cid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function CustomerIDCreate() As Integer
            Dim objCustId = (From c In SingletonAccess.FMSDataContextContignous.tblCustomers
                       Order By c.Cid Descending
                       Select c).FirstOrDefault()
            Dim objCustomerID As New FMS.Business.tblProjectID
            With objCustomerID
                .ProjectID = Guid.NewGuid()
                .CustomersID = objCustId.Cid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objCustomerID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objCustomerID.CustomersID
        End Function
        Private Shared Function CustomerIDUpdate(CustomerID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If CustomerID Is Nothing Then
                Dim tblCustomerCId As FMS.Business.tblCustomer = GetLatestCustomerID()
                CustomerID = tblCustomerCId.Cid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.CustomersID.Equals(CustomerID)).SingleOrDefault
            End If

            With objProject
                .CustomersID = CustomerID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.CustomersID
        End Function
        Public Shared Function CustomerIDCreateOrUpdate() As Integer
            Dim objCustomer = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objCustomer Is Nothing AndAlso objCustomer.Count().Equals(0) Then
                Return CustomerIDCreate()
            Else
                Return CustomerIDUpdate(objCustomer.SingleOrDefault().CustomersID)
            End If
        End Function
#End Region
#Region "tblSites"
        Private Shared Function GetLatestSiteID() As FMS.Business.tblSite
            Return (From c In SingletonAccess.FMSDataContextContignous.tblSites
                       Order By c.Cid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function SiteIDCreate() As Integer
            Dim tblSiteCId As FMS.Business.tblSite = GetLatestSiteID()
            Dim objSiteID As New FMS.Business.tblProjectID
            With objSiteID
                .ProjectID = Guid.NewGuid()
                .SitesID = tblSiteCId.Cid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objSiteID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objSiteID.SitesID
        End Function
        Private Shared Function SiteIDUpdate(SiteID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If SiteID Is Nothing Then
                Dim tblSiteCId As FMS.Business.tblSite = GetLatestSiteID()
                SiteID = tblSiteCId.Cid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.SitesID.Equals(SiteID)).SingleOrDefault
            End If

            With objProject
                .SitesID = SiteID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.SitesID
        End Function
        Public Shared Function SiteIDCreateOrUpdate() As Integer
            Dim objSites = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objSites Is Nothing AndAlso objSites.Count().Equals(0) Then
                Return SiteIDCreate()
            Else
                Return SiteIDUpdate(objSites.SingleOrDefault().SitesID)
            End If
        End Function
#End Region
#Region "tblIndustryGroups"
        Private Shared Function GetLatestIndustryGroupID() As FMS.Business.tblIndustryGroup
            Return (From c In SingletonAccess.FMSDataContextContignous.tblIndustryGroups
                       Order By c.Aid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function IndustryGroupIDCreate() As Integer
            Dim tblIndustryGroupAId As FMS.Business.tblIndustryGroup = GetLatestIndustryGroupID()
            Dim objIndustryGroupID As New FMS.Business.tblProjectID
            With objIndustryGroupID
                .ProjectID = Guid.NewGuid()
                .IndustryGroupID = tblIndustryGroupAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objIndustryGroupID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objIndustryGroupID.IndustryGroupID
        End Function
        Private Shared Function IndustryGroupIDUpdate(IndustryGroupID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If IndustryGroupID Is Nothing Then
                Dim tblIndustryGroupAId As FMS.Business.tblIndustryGroup = GetLatestIndustryGroupID()
                IndustryGroupID = tblIndustryGroupAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.IndustryGroupID.Equals(IndustryGroupID)).SingleOrDefault
            End If

            With objProject
                .IndustryGroupID = IndustryGroupID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.IndustryGroupID
        End Function
        Public Shared Function IndustryGroupIDCreateOrUpdate() As Integer
            Dim objIndustryGroup = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objIndustryGroup Is Nothing AndAlso objIndustryGroup.Count().Equals(0) Then
                Return IndustryGroupIDCreate()
            Else
                Return IndustryGroupIDUpdate(objIndustryGroup.SingleOrDefault().IndustryGroupID)
            End If
        End Function
#End Region
#Region "tblInvoicingFrequency"
        Private Shared Function GetLatestInvoiceFrequencyID() As FMS.Business.tblInvoicingFrequency
            Return (From c In SingletonAccess.FMSDataContextContignous.tblInvoicingFrequencies
                       Order By c.IId Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function InvoiceFrequencyIDCreate() As Integer
            Dim tblInvoiceFrequencyIId As FMS.Business.tblInvoicingFrequency = GetLatestInvoiceFrequencyID()
            Dim objInvoiceFrequencyID As New FMS.Business.tblProjectID
            With objInvoiceFrequencyID
                .ProjectID = Guid.NewGuid()
                .InvoiceFrequencyID = tblInvoiceFrequencyIId.IId + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objInvoiceFrequencyID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objInvoiceFrequencyID.InvoiceFrequencyID
        End Function
        Private Shared Function InvoiceFrequencyIDUpdate(InvoiceFrequencyID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If InvoiceFrequencyID Is Nothing Then
                Dim tblInvoiceFrequencyIId As FMS.Business.tblInvoicingFrequency = GetLatestInvoiceFrequencyID()
                InvoiceFrequencyID = tblInvoiceFrequencyIId.IId
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.InvoiceFrequencyID.Equals(InvoiceFrequencyID)).SingleOrDefault
            End If

            With objProject
                .InvoiceFrequencyID = InvoiceFrequencyID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.InvoiceFrequencyID
        End Function
        Public Shared Function InvoiceFrequencyIDCreateOrUpdate() As Integer
            Dim objInvoiceFrequency = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objInvoiceFrequency Is Nothing AndAlso objInvoiceFrequency.Count().Equals(0) Then
                Return InvoiceFrequencyIDCreate()
            Else
                Return InvoiceFrequencyIDUpdate(objInvoiceFrequency.SingleOrDefault().InvoiceFrequencyID)
            End If
        End Function
#End Region
#Region "tblCustomerAgent"
        Private Shared Function GetLatestCustomerAgentID() As FMS.Business.tblCustomerAgent
            Return (From c In SingletonAccess.FMSDataContextContignous.tblCustomerAgents
                       Order By c.AID Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function CustomerAgentIDCreate() As Integer
            Dim tblCustomerAgentIId As FMS.Business.tblCustomerAgent = GetLatestCustomerAgentID()
            Dim objCustomerAgentID As New FMS.Business.tblProjectID
            With objCustomerAgentID
                .ProjectID = Guid.NewGuid()
                .CustomerAgentID = tblCustomerAgentIId.AID + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objCustomerAgentID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objCustomerAgentID.CustomerAgentID
        End Function
        Private Shared Function CustomerAgentIDUpdate(CustomerAgentID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If CustomerAgentID Is Nothing Then
                Dim tblCustomerAgentIId As FMS.Business.tblCustomerAgent = GetLatestCustomerAgentID()
                CustomerAgentID = tblCustomerAgentIId.AID
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.CustomerAgentID.Equals(CustomerAgentID)).SingleOrDefault
            End If

            With objProject
                .CustomerAgentID = CustomerAgentID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.CustomerAgentID
        End Function
        Public Shared Function CustomerAgentIDCreateOrUpdate() As Integer
            Dim objCustomerAgent = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objCustomerAgent Is Nothing AndAlso objCustomerAgent.Count().Equals(0) Then
                Return CustomerAgentIDCreate()
            Else
                Return CustomerAgentIDUpdate(objCustomerAgent.SingleOrDefault().CustomerAgentID)
            End If
        End Function
#End Region
#Region "tblContractCeaseReasons"
        Private Shared Function GetLatestCeaseReasonID() As FMS.Business.tblContractCeaseReason
            Return (From c In SingletonAccess.FMSDataContextContignous.tblContractCeaseReasons
                       Order By c.Aid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function CeaseReasonIDCreate() As Integer
            Dim tblCeaseReasonAId As FMS.Business.tblContractCeaseReason = GetLatestCeaseReasonID()
            Dim objCeaseReasonID As New FMS.Business.tblProjectID
            With objCeaseReasonID
                .ProjectID = Guid.NewGuid()
                .CeaseReasonID = tblCeaseReasonAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objCeaseReasonID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objCeaseReasonID.CeaseReasonID
        End Function
        Private Shared Function CeaseReasonIDUpdate(CeaseReasonID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If CeaseReasonID Is Nothing Then
                Dim tblCeaseReasonAId As FMS.Business.tblContractCeaseReason = GetLatestCeaseReasonID()
                CeaseReasonID = tblCeaseReasonAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.CeaseReasonID.Equals(CeaseReasonID)).SingleOrDefault
            End If

            With objProject
                .CeaseReasonID = CeaseReasonID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.CeaseReasonID
        End Function
        Public Shared Function CeaseReasonIDCreateOrUpdate() As Integer
            Dim objCeaseReason = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objCeaseReason Is Nothing AndAlso objCeaseReason.Count().Equals(0) Then
                Return CeaseReasonIDCreate()
            Else
                Return CeaseReasonIDUpdate(objCeaseReason.SingleOrDefault().CeaseReasonID)
            End If
        End Function
#End Region
#Region "tblCIRReason"
        Private Shared Function GetLatestCIRReasonID() As FMS.Business.tblCIRReason
            Return (From c In SingletonAccess.FMSDataContextContignous.tblCIRReasons
                       Order By c.CId Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function CIRReasonIDCreate() As Integer
            Dim tblCIRReasonCId As FMS.Business.tblCIRReason = GetLatestCIRReasonID()
            Dim objCIRReasonID As New FMS.Business.tblProjectID
            With objCIRReasonID
                .ProjectID = Guid.NewGuid()
                .CIRReasonID = tblCIRReasonCId.CId + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objCIRReasonID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objCIRReasonID.CIRReasonID
        End Function
        Private Shared Function CIRReasonIDUpdate(CIRReasonID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If CIRReasonID Is Nothing Then
                Dim tblCIRReasonCId As FMS.Business.tblCIRReason = GetLatestCIRReasonID()
                CIRReasonID = tblCIRReasonCId.CId
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.CIRReasonID.Equals(CIRReasonID)).SingleOrDefault
            End If

            With objProject
                .CIRReasonID = CIRReasonID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.CIRReasonID
        End Function
        Public Shared Function CIRReasonIDCreateOrUpdate() As Integer
            Dim objCIRReason = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objCIRReason Is Nothing AndAlso objCIRReason.Count().Equals(0) Then
                Return CIRReasonIDCreate()
            Else
                Return CIRReasonIDUpdate(objCIRReason.SingleOrDefault().CIRReasonID)
            End If
        End Function
#End Region
#Region "tbZones"
        Private Shared Function GetLatestZoneID() As FMS.Business.tbZone
            Return (From c In SingletonAccess.FMSDataContextContignous.tbZones
                       Order By c.Aid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function ZoneIDCreate() As Integer
            Dim tblZoneAId As FMS.Business.tbZone = GetLatestZoneID()
            Dim objZoneID As New FMS.Business.tblProjectID
            With objZoneID
                .ProjectID = Guid.NewGuid()
                .ZoneID = tblZoneAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objZoneID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objZoneID.ZoneID
        End Function
        Private Shared Function ZoneIDUpdate(ZoneID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If ZoneID Is Nothing Then
                Dim tblZoneAId As FMS.Business.tbZone = GetLatestZoneID()
                ZoneID = tblZoneAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.ZoneID.Equals(ZoneID)).SingleOrDefault
            End If

            With objProject
                .ZoneID = ZoneID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.ZoneID
        End Function
        Public Shared Function ZoneIDCreateOrUpdate() As Integer
            Dim objZone = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objZone Is Nothing AndAlso objZone.Count().Equals(0) Then
                Return ZoneIDCreate()
            Else
                Return ZoneIDUpdate(objZone.SingleOrDefault().ZoneID)
            End If
        End Function
#End Region
#Region "tblCustomerRating"
        Private Shared Function GetLatestCustomerRatingID() As FMS.Business.tblCustomerRating
            Return (From c In SingletonAccess.FMSDataContextContignous.tblCustomerRatings
                       Order By c.Rid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function CustomerRatingIDCreate() As Integer
            Dim tblCustomerRatingRId As FMS.Business.tblCustomerRating = GetLatestCustomerRatingID()
            Dim objCustomerRatingID As New FMS.Business.tblProjectID
            With objCustomerRatingID
                .ProjectID = Guid.NewGuid()
                .CustomerRatingID = tblCustomerRatingRId.Rid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objCustomerRatingID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objCustomerRatingID.CustomerRatingID
        End Function
        Private Shared Function CustomerRatingIDUpdate(CustomerRatingID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If CustomerRatingID Is Nothing Then
                Dim tblCustomerRatingRId As FMS.Business.tblCustomerRating = GetLatestCustomerRatingID()
                CustomerRatingID = tblCustomerRatingRId.Rid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.CustomerRatingID.Equals(CustomerRatingID)).SingleOrDefault
            End If

            With objProject
                .CustomerRatingID = CustomerRatingID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.CustomerRatingID
        End Function
        Public Shared Function CustomerRatingIDCreateOrUpdate() As Integer
            Dim objCustomerRatingID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objCustomerRatingID Is Nothing AndAlso objCustomerRatingID.Count().Equals(0) Then
                Return CustomerRatingIDCreate()
            Else
                Return CustomerRatingIDUpdate(objCustomerRatingID.SingleOrDefault().CustomerRatingID)
            End If
        End Function
#End Region
#Region "tblRateIncreaseReference"
        Private Shared Function GetLatestRateIncreaseID() As FMS.Business.tblRateIncreaseReference
            Return (From c In SingletonAccess.FMSDataContextContignous.tblRateIncreaseReferences
                       Order By c.Aid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function RateIncreaseIDCreate() As Integer
            Dim tblRateIncreaseAId As FMS.Business.tblRateIncreaseReference = GetLatestRateIncreaseID()
            Dim objRateIncreaseID As New FMS.Business.tblProjectID
            With objRateIncreaseID
                .ProjectID = Guid.NewGuid()
                .RateIncreaseID = tblRateIncreaseAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objRateIncreaseID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objRateIncreaseID.RateIncreaseID
        End Function
        Private Shared Function RateIncreaseIDUpdate(RateIncreaseID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If RateIncreaseID Is Nothing Then
                Dim tblRateIncreaseAId As FMS.Business.tblRateIncreaseReference = GetLatestRateIncreaseID()
                RateIncreaseID = tblRateIncreaseAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.RateIncreaseID.Equals(RateIncreaseID)).SingleOrDefault
            End If

            With objProject
                .RateIncreaseID = RateIncreaseID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.RateIncreaseID
        End Function
        Public Shared Function RateIncreaseIDCreateOrUpdate() As Integer
            Dim objRateIncreaseID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objRateIncreaseID Is Nothing AndAlso objRateIncreaseID.Count().Equals(0) Then
                Return RateIncreaseIDCreate()
            Else
                Return RateIncreaseIDUpdate(objRateIncreaseID.SingleOrDefault().RateIncreaseID)
            End If
        End Function
#End Region
#Region "tblPreviousSuppliers"
        Private Shared Function GetLatestPreviousSupplierID() As FMS.Business.tblPreviousSupplier
            Return (From c In SingletonAccess.FMSDataContextContignous.tblPreviousSuppliers
                       Order By c.Aid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function PreviousSupplierIDCreate() As Integer
            Dim tblPreviousSupplierAId As FMS.Business.tblPreviousSupplier = GetLatestPreviousSupplierID()
            Dim objPreviousSupplierID As New FMS.Business.tblProjectID
            With objPreviousSupplierID
                .ProjectID = Guid.NewGuid()
                .PreviousSupplierID = tblPreviousSupplierAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objPreviousSupplierID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objPreviousSupplierID.PreviousSupplierID
        End Function
        Private Shared Function PreviousSupplierIDUpdate(PreviousSupplierID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If PreviousSupplierID Is Nothing Then
                Dim tblPreviousSupplierAId As FMS.Business.tblPreviousSupplier = GetLatestPreviousSupplierID()
                PreviousSupplierID = tblPreviousSupplierAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.PreviousSupplierID.Equals(PreviousSupplierID)).SingleOrDefault
            End If

            With objProject
                .PreviousSupplierID = PreviousSupplierID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.PreviousSupplierID
        End Function
        Public Shared Function PreviousSupplierIDCreateOrUpdate() As Integer
            Dim objPreviousSupplierID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objPreviousSupplierID Is Nothing AndAlso objPreviousSupplierID.Count().Equals(0) Then
                Return PreviousSupplierIDCreate()
            Else
                Return PreviousSupplierIDUpdate(objPreviousSupplierID.SingleOrDefault().PreviousSupplierID)
            End If
        End Function
#End Region
#Region "tblRunFortnightlyCycles"
        Private Shared Function GetLatestFortnightlyCyclesID() As FMS.Business.tblRunFortnightlyCycle
            Return (From c In SingletonAccess.FMSDataContextContignous.tblRunFortnightlyCycles
                       Order By c.Aid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function FortnightlyCyclesIDCreate() As Integer
            Dim tblFortnightlyCyclesAId As FMS.Business.tblRunFortnightlyCycle = GetLatestFortnightlyCyclesID()
            Dim objFortnightlyCyclesID As New FMS.Business.tblProjectID
            With objFortnightlyCyclesID
                .ProjectID = Guid.NewGuid()
                .FortnightlyCyclesID = tblFortnightlyCyclesAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objFortnightlyCyclesID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objFortnightlyCyclesID.FortnightlyCyclesID
        End Function
        Private Shared Function FortnightlyCyclesIDUpdate(FortnightlyCyclesID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If FortnightlyCyclesID Is Nothing Then
                Dim tblFortnightlyCyclesAId As FMS.Business.tblRunFortnightlyCycle = GetLatestFortnightlyCyclesID()
                FortnightlyCyclesID = tblFortnightlyCyclesAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.FortnightlyCyclesID.Equals(FortnightlyCyclesID)).SingleOrDefault
            End If

            With objProject
                .FortnightlyCyclesID = FortnightlyCyclesID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.FortnightlyCyclesID
        End Function
        Public Shared Function FortnightlyCyclesIDCreateOrUpdate() As Integer
            Dim objFortnightlyCyclesID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objFortnightlyCyclesID Is Nothing AndAlso objFortnightlyCyclesID.Count().Equals(0) Then
                Return FortnightlyCyclesIDCreate()
            Else
                Return FortnightlyCyclesIDUpdate(objFortnightlyCyclesID.SingleOrDefault().FortnightlyCyclesID)
            End If
        End Function
#End Region
#Region "tblRevenueChangeReason"
        Private Shared Function GetLatestRevenueChangeReasonID() As FMS.Business.tblRevenueChangeReason
            Return (From c In SingletonAccess.FMSDataContextContignous.tblRevenueChangeReasons
                       Order By c.Rid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function RevenueChangeReasonIDCreate() As Integer
            Dim tblRevenueChangeReasonRId As FMS.Business.tblRevenueChangeReason = GetLatestRevenueChangeReasonID()
            Dim objRevenueChangeReasonID As New FMS.Business.tblProjectID
            With objRevenueChangeReasonID
                .ProjectID = Guid.NewGuid()
                .RevenueChangeReasonID = tblRevenueChangeReasonRId.Rid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objRevenueChangeReasonID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objRevenueChangeReasonID.RevenueChangeReasonID
        End Function
        Private Shared Function RevenueChangeReasonIDUpdate(RevenueChangeReasonID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If RevenueChangeReasonID Is Nothing Then
                Dim tblRevenueChangeReasonRId As FMS.Business.tblRevenueChangeReason = GetLatestRevenueChangeReasonID()
                RevenueChangeReasonID = tblRevenueChangeReasonRId.Rid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.RevenueChangeReasonID.Equals(RevenueChangeReasonID)).SingleOrDefault
            End If

            With objProject
                .RevenueChangeReasonID = RevenueChangeReasonID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.RevenueChangeReasonID
        End Function
        Public Shared Function RevenueChangeReasonIDCreateOrUpdate() As Integer
            Dim objRevenueChangeReasonID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objRevenueChangeReasonID Is Nothing AndAlso objRevenueChangeReasonID.Count().Equals(0) Then
                Return RevenueChangeReasonIDCreate()
            Else
                Return RevenueChangeReasonIDUpdate(objRevenueChangeReasonID.SingleOrDefault().RevenueChangeReasonID)
            End If
        End Function
#End Region
#Region "tblServiceFrequency"
        Private Shared Function GetLatestFrequencyID() As FMS.Business.tblServiceFrequency
            Return (From c In SingletonAccess.FMSDataContextContignous.tblServiceFrequencies
                       Order By c.Fid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function FrequencyIDCreate() As Integer
            Dim tblFrequencyFId As FMS.Business.tblServiceFrequency = GetLatestFrequencyID()
            Dim objFrequencyID As New FMS.Business.tblProjectID
            With objFrequencyID
                .ProjectID = Guid.NewGuid()
                .FrequencyID = tblFrequencyFId.Fid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objFrequencyID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objFrequencyID.FrequencyID
        End Function
        Private Shared Function FrequencyIDUpdate(FrequencyID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If FrequencyID Is Nothing Then
                Dim tblFrequencyFId As FMS.Business.tblServiceFrequency = GetLatestFrequencyID()
                FrequencyID = tblFrequencyFId.Fid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.FrequencyID.Equals(FrequencyID)).SingleOrDefault
            End If

            With objProject
                .FrequencyID = FrequencyID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.FrequencyID
        End Function
        Public Shared Function FrequencyIDCreateOrUpdate() As Integer
            Dim objFrequencyID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objFrequencyID Is Nothing AndAlso objFrequencyID.Count().Equals(0) Then
                Return FrequencyIDCreate()
            Else
                Return FrequencyIDUpdate(objFrequencyID.SingleOrDefault().FrequencyID)
            End If
        End Function
#End Region
#Region "tblServices"
        Private Shared Function GetLatestServicesID() As FMS.Business.tblService
            Return (From c In SingletonAccess.FMSDataContextContignous.tblServices
                       Order By c.Sid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function ServicesIDCreate() As Integer
            Dim tblServicesSId As FMS.Business.tblService = GetLatestServicesID()
            Dim objServicesID As New FMS.Business.tblProjectID
            With objServicesID
                .ProjectID = Guid.NewGuid()
                .ServicesID = tblServicesSId.Sid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objServicesID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objServicesID.ServicesID
        End Function
        Private Shared Function ServicesIDUpdate(ServicesID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If ServicesID Is Nothing Then
                Dim tblServicesSId As FMS.Business.tblService = GetLatestServicesID()
                ServicesID = tblServicesSId.Sid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.ServicesID.Equals(ServicesID)).SingleOrDefault
            End If

            With objProject
                .ServicesID = ServicesID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.ServicesID
        End Function
        Public Shared Function ServicesIDCreateOrUpdate() As Integer
            Dim objServicesID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objServicesID Is Nothing AndAlso objServicesID.Count().Equals(0) Then
                Return ServicesIDCreate()
            Else
                Return ServicesIDUpdate(objServicesID.SingleOrDefault().ServicesID)
            End If
        End Function
#End Region
#Region "tblSalesPersons"
        Private Shared Function GetLatestSalesPersonID() As FMS.Business.tblSalesPerson
            Return (From c In SingletonAccess.FMSDataContextContignous.tblSalesPersons
                       Order By c.Aid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function SalesPersonIDCreate() As Integer
            Dim tblSalesPersonAId As FMS.Business.tblSalesPerson = GetLatestSalesPersonID()
            Dim objSalesPersonID As New FMS.Business.tblProjectID
            With objSalesPersonID
                .ProjectID = Guid.NewGuid()
                .SalesPersonID = tblSalesPersonAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objSalesPersonID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objSalesPersonID.SalesPersonID
        End Function
        Private Shared Function SalesPersonIDUpdate(SalesPersonID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If SalesPersonID Is Nothing Then
                Dim tblSalesPersonAId As FMS.Business.tblSalesPerson = GetLatestSalesPersonID()
                SalesPersonID = tblSalesPersonAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.SalesPersonID.Equals(SalesPersonID)).SingleOrDefault
            End If

            With objProject
                .SalesPersonID = SalesPersonID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.SalesPersonID
        End Function
        Public Shared Function SalesPersonIDCreateOrUpdate() As Integer
            Dim objSalesPersonID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objSalesPersonID Is Nothing AndAlso objSalesPersonID.Count().Equals(0) Then
                Return SalesPersonIDCreate()
            Else
                Return SalesPersonIDUpdate(objSalesPersonID.SingleOrDefault().SalesPersonID)
            End If
        End Function
#End Region
#Region "tblFuelLevy"
        Private Shared Function GetLatestFuelLevyID() As FMS.Business.tblFuelLevy
            Return (From c In SingletonAccess.FMSDataContextContignous.tblFuelLevies
                       Order By c.Aid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function FuelLevyIDCreate() As Integer
            Dim tblFuelLevyAId As FMS.Business.tblFuelLevy = GetLatestFuelLevyID()
            Dim objFuelLevyID As New FMS.Business.tblProjectID
            With objFuelLevyID
                .ProjectID = Guid.NewGuid()
                .FuelLevyID = tblFuelLevyAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objFuelLevyID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objFuelLevyID.FuelLevyID
        End Function
        Private Shared Function FuelLevyIDUpdate(FuelLevyID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If FuelLevyID Is Nothing Then
                Dim tblFuelLevyAId As FMS.Business.tblFuelLevy = GetLatestFuelLevyID()
                FuelLevyID = tblFuelLevyAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.FuelLevyID.Equals(FuelLevyID)).SingleOrDefault
            End If

            With objProject
                .FuelLevyID = FuelLevyID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.FuelLevyID
        End Function
        Public Shared Function FuelLevyIDCreateOrUpdate() As Integer
            Dim objFuelLevyID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objFuelLevyID Is Nothing AndAlso objFuelLevyID.Count().Equals(0) Then
                Return FuelLevyIDCreate()
            Else
                Return FuelLevyIDUpdate(objFuelLevyID.SingleOrDefault().FuelLevyID)
            End If
        End Function
#End Region
#Region "tblzGenerateRunSheets"
        Private Shared Function GetLatestRunSheetID() As FMS.Business.tblzGenerateRunSheet
            Return (From c In SingletonAccess.FMSDataContextContignous.tblzGenerateRunSheets
                       Order By c.Aid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function RunSheetIDCreate() As Integer
            Dim tblRunSheetAId As FMS.Business.tblzGenerateRunSheet = GetLatestRunSheetID()
            Dim objRunSheetID As New FMS.Business.tblProjectID
            With objRunSheetID
                .ProjectID = Guid.NewGuid()
                .RunSheetID = tblRunSheetAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objRunSheetID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objRunSheetID.RunSheetID
        End Function
        Private Shared Function RunSheetIDUpdate(RunSheetID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If RunSheetID Is Nothing Then
                Dim tblRunSheetAId As FMS.Business.tblzGenerateRunSheet = GetLatestRunSheetID()
                RunSheetID = tblRunSheetAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                                                  Where c.RunSheetID.Equals(RunSheetID)).SingleOrDefault
            End If

            With objProject
                .RunSheetID = RunSheetID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.RunSheetID
        End Function
        Public Shared Function RunSheetIDCreateOrUpdate() As Integer
            Dim objRunSheetID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objRunSheetID Is Nothing AndAlso objRunSheetID.Count().Equals(0) Then
                Return RunSheetIDCreate()
            Else
                Return RunSheetIDUpdate(objRunSheetID.SingleOrDefault().RunSheetID)
            End If
        End Function
#End Region
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
#End Region
    End Class
End Namespace

