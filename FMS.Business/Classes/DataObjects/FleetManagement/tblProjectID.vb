Namespace DataObjects
    Public Class tblProjectID
#Region "Properties / enums"
#End Region
#Region "CRUD"
#Region "tblCustomer"
        Private Shared Function GetLatestCustomerID(appID As System.Guid) As FMS.Business.tblCustomer
            Return (From c In SingletonAccess.FMSDataContextContignous.tblCustomers
                    Where c.ApplicationID.Equals(appID)
                    Order By c.Cid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function CustomerIDCreate(appID As System.Guid) As Integer
            Dim objCustId As FMS.Business.tblCustomer = GetLatestCustomerID(appID)
            Dim objCustomerID As New FMS.Business.tblProjectID
            With objCustomerID
                .ProjectID = Guid.NewGuid()
                .CustomersID = objCustId.Cid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objCustomerID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objCustomerID.CustomersID
        End Function
        Private Shared Function CustomerIDUpdate(CustomerID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If CustomerID Is Nothing Then
                Dim tblCustomerCId As FMS.Business.tblCustomer = GetLatestCustomerID(appID)
                CustomerID = tblCustomerCId.Cid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.CustomersID.Equals(CustomerID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .CustomersID = CustomerID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.CustomersID
        End Function
        Public Shared Function CustomerIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objCustomer = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objCustomer Is Nothing AndAlso objCustomer.Count().Equals(0) Then
                Return CustomerIDCreate(appID)
            Else
                Return CustomerIDUpdate(objCustomer.SingleOrDefault().CustomersID, appID)
            End If
        End Function
#End Region
#Region "tblSites"
        Private Shared Function GetLatestSiteID(appID As System.Guid) As FMS.Business.tblSite
            Return (From c In SingletonAccess.FMSDataContextContignous.tblSites
                    Where c.ApplicationId.Equals(appID)
                    Order By c.Cid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function SiteIDCreate(appID As System.Guid) As Integer
            Dim tblSiteCId As FMS.Business.tblSite = GetLatestSiteID(appID)
            Dim objSiteID As New FMS.Business.tblProjectID
            With objSiteID
                .ProjectID = Guid.NewGuid()
                .SitesID = tblSiteCId.Cid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objSiteID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objSiteID.SitesID
        End Function
        Private Shared Function SiteIDUpdate(SiteID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If SiteID Is Nothing Then
                Dim tblSiteCId As FMS.Business.tblSite = GetLatestSiteID(appID)
                SiteID = tblSiteCId.Cid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.SitesID.Equals(SiteID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .SitesID = SiteID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.SitesID
        End Function
        Public Shared Function SiteIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objSites = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objSites Is Nothing AndAlso objSites.Count().Equals(0) Then
                Return SiteIDCreate(appID)
            Else
                Return SiteIDUpdate(objSites.SingleOrDefault().SitesID, appID)
            End If
        End Function
#End Region
#Region "tblIndustryGroups"
        Private Shared Function GetLatestIndustryGroupID(appID As System.Guid) As FMS.Business.tblIndustryGroup
            Return (From c In SingletonAccess.FMSDataContextContignous.tblIndustryGroups
                    Where c.ApplicationID.Equals(appID)
                    Order By c.Aid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function IndustryGroupIDCreate(appID As System.Guid) As Integer
            Dim tblIndustryGroupAId As FMS.Business.tblIndustryGroup = GetLatestIndustryGroupID(appID)
            Dim objIndustryGroupID As New FMS.Business.tblProjectID
            With objIndustryGroupID
                .ProjectID = Guid.NewGuid()
                .IndustryGroupID = tblIndustryGroupAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objIndustryGroupID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objIndustryGroupID.IndustryGroupID
        End Function
        Private Shared Function IndustryGroupIDUpdate(IndustryGroupID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If IndustryGroupID Is Nothing Then
                Dim tblIndustryGroupAId As FMS.Business.tblIndustryGroup = GetLatestIndustryGroupID(appID)
                IndustryGroupID = tblIndustryGroupAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.IndustryGroupID.Equals(IndustryGroupID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .IndustryGroupID = IndustryGroupID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.IndustryGroupID
        End Function
        Public Shared Function IndustryGroupIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objIndustryGroup = SingletonAccess.FMSDataContextContignous.tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
            If Not objIndustryGroup Is Nothing AndAlso objIndustryGroup.Count().Equals(0) Then
                Return IndustryGroupIDCreate(appID)
            Else
                Return IndustryGroupIDUpdate(objIndustryGroup.SingleOrDefault().IndustryGroupID, appID)
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
        Private Shared Function GetLatestCustomerAgentID(appID As System.Guid) As FMS.Business.tblCustomerAgent
            Return (From c In SingletonAccess.FMSDataContextContignous.tblCustomerAgents
                    Where c.ApplicationID.Equals(appID)
                    Order By c.AID Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function CustomerAgentIDCreate(appID As System.Guid) As Integer
            Dim tblCustomerAgentIId As FMS.Business.tblCustomerAgent = GetLatestCustomerAgentID(appID)
            Dim objCustomerAgentID As New FMS.Business.tblProjectID
            With objCustomerAgentID
                .ProjectID = Guid.NewGuid()
                .CustomerAgentID = tblCustomerAgentIId.AID + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objCustomerAgentID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objCustomerAgentID.CustomerAgentID
        End Function
        Private Shared Function CustomerAgentIDUpdate(CustomerAgentID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If CustomerAgentID Is Nothing Then
                Dim tblCustomerAgentIId As FMS.Business.tblCustomerAgent = GetLatestCustomerAgentID(appID)
                CustomerAgentID = tblCustomerAgentIId.AID
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.CustomerAgentID.Equals(CustomerAgentID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .CustomerAgentID = CustomerAgentID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.CustomerAgentID
        End Function
        Public Shared Function CustomerAgentIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objCustomerAgent = SingletonAccess.FMSDataContextContignous.tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
            If Not objCustomerAgent Is Nothing AndAlso objCustomerAgent.Count().Equals(0) Then
                Return CustomerAgentIDCreate(appID)
            Else
                Return CustomerAgentIDUpdate(objCustomerAgent.SingleOrDefault().CustomerAgentID, appID)
            End If
        End Function
#End Region
#Region "tblContractCeaseReasons"
        Private Shared Function GetLatestCeaseReasonID(appID As System.Guid) As FMS.Business.tblContractCeaseReason
            Return (From c In SingletonAccess.FMSDataContextContignous.tblContractCeaseReasons
                    Where c.ApplicationID.Equals(appID)
                    Order By c.Aid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function CeaseReasonIDCreate(appID As System.Guid) As Integer
            Dim tblCeaseReasonAId As FMS.Business.tblContractCeaseReason = GetLatestCeaseReasonID(appID)
            Dim objCeaseReasonID As New FMS.Business.tblProjectID
            With objCeaseReasonID
                .ProjectID = Guid.NewGuid()
                .CeaseReasonID = tblCeaseReasonAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objCeaseReasonID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objCeaseReasonID.CeaseReasonID
        End Function
        Private Shared Function CeaseReasonIDUpdate(CeaseReasonID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If CeaseReasonID Is Nothing Then
                Dim tblCeaseReasonAId As FMS.Business.tblContractCeaseReason = GetLatestCeaseReasonID(appID)
                CeaseReasonID = tblCeaseReasonAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.CeaseReasonID.Equals(CeaseReasonID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .CeaseReasonID = CeaseReasonID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.CeaseReasonID
        End Function
        Public Shared Function CeaseReasonIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objCeaseReason = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objCeaseReason Is Nothing AndAlso objCeaseReason.Count().Equals(0) Then
                Return CeaseReasonIDCreate(appID)
            Else
                Return CeaseReasonIDUpdate(objCeaseReason.SingleOrDefault().CeaseReasonID, appID)
            End If
        End Function
#End Region
#Region "tblCIRReason"
        Private Shared Function GetLatestCIRReasonID(appID As System.Guid) As FMS.Business.tblCIRReason
            Return (From c In SingletonAccess.FMSDataContextContignous.tblCIRReasons
                    Where c.ApplicationID.Equals(appID)
                    Order By c.CId Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function CIRReasonIDCreate(appID As System.Guid) As Integer
            Dim tblCIRReasonCId As FMS.Business.tblCIRReason = GetLatestCIRReasonID(appID)
            Dim objCIRReasonID As New FMS.Business.tblProjectID
            With objCIRReasonID
                .ProjectID = Guid.NewGuid()
                .CIRReasonID = tblCIRReasonCId.CId + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objCIRReasonID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objCIRReasonID.CIRReasonID
        End Function
        Private Shared Function CIRReasonIDUpdate(CIRReasonID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If CIRReasonID Is Nothing Then
                Dim tblCIRReasonCId As FMS.Business.tblCIRReason = GetLatestCIRReasonID(appID)
                CIRReasonID = tblCIRReasonCId.CId
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.CIRReasonID.Equals(CIRReasonID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .CIRReasonID = CIRReasonID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.CIRReasonID
        End Function
        Public Shared Function CIRReasonIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objCIRReason = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objCIRReason Is Nothing AndAlso objCIRReason.Count().Equals(0) Then
                Return CIRReasonIDCreate(appID)
            Else
                Return CIRReasonIDUpdate(objCIRReason.SingleOrDefault().CIRReasonID, appID)
            End If
        End Function
#End Region
#Region "tbZones"
        Private Shared Function GetLatestZoneID(appID As System.Guid) As FMS.Business.tbZone
            Return (From c In SingletonAccess.FMSDataContextContignous.tbZones
                    Where c.ApplicationID.Equals(appID)
                    Order By c.Aid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function ZoneIDCreate(appID As System.Guid) As Integer
            Dim tblZoneAId As FMS.Business.tbZone = GetLatestZoneID(appID)
            Dim objZoneID As New FMS.Business.tblProjectID
            With objZoneID
                .ProjectID = Guid.NewGuid()
                .ZoneID = tblZoneAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objZoneID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objZoneID.ZoneID
        End Function
        Private Shared Function ZoneIDUpdate(ZoneID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If ZoneID Is Nothing Then
                Dim tblZoneAId As FMS.Business.tbZone = GetLatestZoneID(appID)
                ZoneID = tblZoneAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ZoneID.Equals(ZoneID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .ZoneID = ZoneID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.ZoneID
        End Function
        Public Shared Function ZoneIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objZone = SingletonAccess.FMSDataContextContignous.tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
            If Not objZone Is Nothing AndAlso objZone.Count().Equals(0) Then
                Return ZoneIDCreate(appID)
            Else
                Return ZoneIDUpdate(objZone.SingleOrDefault().ZoneID, appID)
            End If
        End Function
#End Region
#Region "tblCustomerRating"
        Private Shared Function GetLatestCustomerRatingID(appID As System.Guid) As FMS.Business.tblCustomerRating
            Return (From c In SingletonAccess.FMSDataContextContignous.tblCustomerRatings
                    Where c.ApplicationId.Equals(appID)
                    Order By c.Rid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function CustomerRatingIDCreate(appID As System.Guid) As Integer
            Dim tblCustomerRatingRId As FMS.Business.tblCustomerRating = GetLatestCustomerRatingID(appID)
            Dim objCustomerRatingID As New FMS.Business.tblProjectID
            With objCustomerRatingID
                .ProjectID = Guid.NewGuid()
                .CustomerRatingID = tblCustomerRatingRId.Rid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objCustomerRatingID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objCustomerRatingID.CustomerRatingID
        End Function
        Private Shared Function CustomerRatingIDUpdate(CustomerRatingID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If CustomerRatingID Is Nothing Then
                Dim tblCustomerRatingRId As FMS.Business.tblCustomerRating = GetLatestCustomerRatingID(appID)
                CustomerRatingID = tblCustomerRatingRId.Rid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.CustomerRatingID.Equals(CustomerRatingID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .CustomerRatingID = CustomerRatingID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.CustomerRatingID
        End Function
        Public Shared Function CustomerRatingIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objCustomerRatingID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objCustomerRatingID Is Nothing AndAlso objCustomerRatingID.Count().Equals(0) Then
                Return CustomerRatingIDCreate(appID)
            Else
                Return CustomerRatingIDUpdate(objCustomerRatingID.SingleOrDefault().CustomerRatingID, appID)
            End If
        End Function
#End Region
#Region "tblRateIncreaseReference"
        Private Shared Function GetLatestRateIncreaseID(appID As System.Guid) As FMS.Business.tblRateIncreaseReference
            Return (From c In SingletonAccess.FMSDataContextContignous.tblRateIncreaseReferences
                    Where c.ApplicationID.Equals(appID)
                       Order By c.Aid Descending
                       Select c).FirstOrDefault()
        End Function
        Private Shared Function RateIncreaseIDCreate(appID As System.Guid) As Integer
            Dim tblRateIncreaseAId As FMS.Business.tblRateIncreaseReference = GetLatestRateIncreaseID(appID)
            Dim objRateIncreaseID As New FMS.Business.tblProjectID
            With objRateIncreaseID
                .ProjectID = Guid.NewGuid()
                .RateIncreaseID = tblRateIncreaseAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objRateIncreaseID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objRateIncreaseID.RateIncreaseID
        End Function
        Private Shared Function RateIncreaseIDUpdate(RateIncreaseID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If RateIncreaseID Is Nothing Then
                Dim tblRateIncreaseAId As FMS.Business.tblRateIncreaseReference = GetLatestRateIncreaseID(appID)
                RateIncreaseID = tblRateIncreaseAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.RateIncreaseID.Equals(RateIncreaseID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .RateIncreaseID = RateIncreaseID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.RateIncreaseID
        End Function
        Public Shared Function RateIncreaseIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objRateIncreaseID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
            If Not objRateIncreaseID Is Nothing AndAlso objRateIncreaseID.Count().Equals(0) Then
                Return RateIncreaseIDCreate(appID)
            Else
                Return RateIncreaseIDUpdate(objRateIncreaseID.SingleOrDefault().RateIncreaseID, appID)
            End If
        End Function
#End Region
#Region "tblPreviousSuppliers"
        Private Shared Function GetLatestPreviousSupplierID(appID As System.Guid) As FMS.Business.tblPreviousSupplier
            Return (From c In SingletonAccess.FMSDataContextContignous.tblPreviousSuppliers
                    Where c.ApplicationID.Equals(appID)
                    Order By c.Aid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function PreviousSupplierIDCreate(appID As System.Guid) As Integer
            Dim tblPreviousSupplierAId As FMS.Business.tblPreviousSupplier = GetLatestPreviousSupplierID(appID)
            Dim objPreviousSupplierID As New FMS.Business.tblProjectID
            With objPreviousSupplierID
                .ProjectID = Guid.NewGuid()
                .PreviousSupplierID = tblPreviousSupplierAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objPreviousSupplierID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objPreviousSupplierID.PreviousSupplierID
        End Function
        Private Shared Function PreviousSupplierIDUpdate(PreviousSupplierID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If PreviousSupplierID Is Nothing Then
                Dim tblPreviousSupplierAId As FMS.Business.tblPreviousSupplier = GetLatestPreviousSupplierID(appID)
                PreviousSupplierID = tblPreviousSupplierAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.PreviousSupplierID.Equals(PreviousSupplierID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .PreviousSupplierID = PreviousSupplierID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.PreviousSupplierID
        End Function
        Public Shared Function PreviousSupplierIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objPreviousSupplierID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objPreviousSupplierID Is Nothing AndAlso objPreviousSupplierID.Count().Equals(0) Then
                Return PreviousSupplierIDCreate(appID)
            Else
                Return PreviousSupplierIDUpdate(objPreviousSupplierID.SingleOrDefault().PreviousSupplierID, appID)
            End If
        End Function
#End Region
#Region "tblRunFortnightlyCycles"
        Private Shared Function GetLatestFortnightlyCyclesID(appID As System.Guid) As FMS.Business.tblRunFortnightlyCycle
            Return (From c In SingletonAccess.FMSDataContextContignous.tblRunFortnightlyCycles
                    Where c.ApplicationID.Equals(appID)
                    Order By c.Aid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function FortnightlyCyclesIDCreate(appID As System.Guid) As Integer
            Dim tblFortnightlyCyclesAId As FMS.Business.tblRunFortnightlyCycle = GetLatestFortnightlyCyclesID(appID)
            Dim objFortnightlyCyclesID As New FMS.Business.tblProjectID
            With objFortnightlyCyclesID
                .ProjectID = Guid.NewGuid()
                .FortnightlyCyclesID = tblFortnightlyCyclesAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objFortnightlyCyclesID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objFortnightlyCyclesID.FortnightlyCyclesID
        End Function
        Private Shared Function FortnightlyCyclesIDUpdate(FortnightlyCyclesID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If FortnightlyCyclesID Is Nothing Then
                Dim tblFortnightlyCyclesAId As FMS.Business.tblRunFortnightlyCycle = GetLatestFortnightlyCyclesID(appID)
                FortnightlyCyclesID = tblFortnightlyCyclesAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.FortnightlyCyclesID.Equals(FortnightlyCyclesID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .FortnightlyCyclesID = FortnightlyCyclesID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.FortnightlyCyclesID
        End Function
        Public Shared Function FortnightlyCyclesIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objFortnightlyCyclesID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objFortnightlyCyclesID Is Nothing AndAlso objFortnightlyCyclesID.Count().Equals(0) Then
                Return FortnightlyCyclesIDCreate(appID)
            Else
                Return FortnightlyCyclesIDUpdate(objFortnightlyCyclesID.SingleOrDefault().FortnightlyCyclesID, appID)
            End If
        End Function
#End Region
#Region "tblRevenueChangeReason"
        Private Shared Function GetLatestRevenueChangeReasonID(appID As System.Guid) As FMS.Business.tblRevenueChangeReason
            Return (From c In SingletonAccess.FMSDataContextContignous.tblRevenueChangeReasons
                    Where c.ApplicationID.Equals(appID)
                    Order By c.Rid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function RevenueChangeReasonIDCreate(appID As System.Guid) As Integer
            Dim tblRevenueChangeReasonRId As FMS.Business.tblRevenueChangeReason = GetLatestRevenueChangeReasonID(appID)
            Dim objRevenueChangeReasonID As New FMS.Business.tblProjectID
            With objRevenueChangeReasonID
                .ProjectID = Guid.NewGuid()
                .RevenueChangeReasonID = tblRevenueChangeReasonRId.Rid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objRevenueChangeReasonID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objRevenueChangeReasonID.RevenueChangeReasonID
        End Function
        Private Shared Function RevenueChangeReasonIDUpdate(RevenueChangeReasonID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If RevenueChangeReasonID Is Nothing Then
                Dim tblRevenueChangeReasonRId As FMS.Business.tblRevenueChangeReason = GetLatestRevenueChangeReasonID(appID)
                RevenueChangeReasonID = tblRevenueChangeReasonRId.Rid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.RevenueChangeReasonID.Equals(RevenueChangeReasonID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .RevenueChangeReasonID = RevenueChangeReasonID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.RevenueChangeReasonID
        End Function
        Public Shared Function RevenueChangeReasonIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objRevenueChangeReasonID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objRevenueChangeReasonID Is Nothing AndAlso objRevenueChangeReasonID.Count().Equals(0) Then
                Return RevenueChangeReasonIDCreate(appID)
            Else
                Return RevenueChangeReasonIDUpdate(objRevenueChangeReasonID.SingleOrDefault().RevenueChangeReasonID, appID)
            End If
        End Function
#End Region
#Region "tblServiceFrequency"
        Private Shared Function GetLatestFrequencyID(appID As System.Guid) As FMS.Business.tblServiceFrequency
            Return (From c In SingletonAccess.FMSDataContextContignous.tblServiceFrequencies
                    Where c.ApplicationID.Equals(appID)
                    Order By c.Fid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function FrequencyIDCreate(appID As System.Guid) As Integer
            Dim tblFrequencyFId As FMS.Business.tblServiceFrequency = GetLatestFrequencyID(appID)
            Dim objFrequencyID As New FMS.Business.tblProjectID
            With objFrequencyID
                .ProjectID = Guid.NewGuid()
                .FrequencyID = tblFrequencyFId.Fid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objFrequencyID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objFrequencyID.FrequencyID
        End Function
        Private Shared Function FrequencyIDUpdate(FrequencyID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If FrequencyID Is Nothing Then
                Dim tblFrequencyFId As FMS.Business.tblServiceFrequency = GetLatestFrequencyID(appID)
                FrequencyID = tblFrequencyFId.Fid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.FrequencyID.Equals(FrequencyID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .FrequencyID = FrequencyID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.FrequencyID
        End Function
        Public Shared Function FrequencyIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objFrequencyID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objFrequencyID Is Nothing AndAlso objFrequencyID.Count().Equals(0) Then
                Return FrequencyIDCreate(appID)
            Else
                Return FrequencyIDUpdate(objFrequencyID.SingleOrDefault().FrequencyID, appID)
            End If
        End Function
#End Region
#Region "tblServices"
        Private Shared Function GetLatestServicesID(appID As System.Guid) As FMS.Business.tblService
            Return (From c In SingletonAccess.FMSDataContextContignous.tblServices
                    Where c.ApplicationID.Equals(appID)
                    Order By c.Sid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function ServicesIDCreate(appID As System.Guid) As Integer
            Dim tblServicesSId As FMS.Business.tblService = GetLatestServicesID(appID)
            Dim objServicesID As New FMS.Business.tblProjectID
            With objServicesID
                .ProjectID = Guid.NewGuid()
                .ServicesID = tblServicesSId.Sid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objServicesID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objServicesID.ServicesID
        End Function
        Private Shared Function ServicesIDUpdate(ServicesID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If ServicesID Is Nothing Then
                Dim tblServicesSId As FMS.Business.tblService = GetLatestServicesID(appID)
                ServicesID = tblServicesSId.Sid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ServicesID.Equals(ServicesID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .ServicesID = ServicesID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.ServicesID
        End Function
        Public Shared Function ServicesIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objServicesID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
            If Not objServicesID Is Nothing AndAlso objServicesID.Count().Equals(0) Then
                Return ServicesIDCreate(appID)
            Else
                Return ServicesIDUpdate(objServicesID.SingleOrDefault().ServicesID, appID)
            End If
        End Function
#End Region
#Region "tblSalesPersons"
        Private Shared Function GetLatestSalesPersonID(appID As System.Guid) As FMS.Business.tblSalesPerson
            Return (From c In SingletonAccess.FMSDataContextContignous.tblSalesPersons
                    Where c.ApplicationID.Equals(appID)
                    Order By c.Aid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function SalesPersonIDCreate(appID As System.Guid) As Integer
            Dim tblSalesPersonAId As FMS.Business.tblSalesPerson = GetLatestSalesPersonID(appID)
            Dim objSalesPersonID As New FMS.Business.tblProjectID
            With objSalesPersonID
                .ProjectID = Guid.NewGuid()
                .SalesPersonID = tblSalesPersonAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objSalesPersonID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objSalesPersonID.SalesPersonID
        End Function
        Private Shared Function SalesPersonIDUpdate(SalesPersonID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If SalesPersonID Is Nothing Then
                Dim tblSalesPersonAId As FMS.Business.tblSalesPerson = GetLatestSalesPersonID(appID)
                SalesPersonID = tblSalesPersonAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.SalesPersonID.Equals(SalesPersonID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .SalesPersonID = SalesPersonID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.SalesPersonID
        End Function
        Public Shared Function SalesPersonIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objSalesPersonID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objSalesPersonID Is Nothing AndAlso objSalesPersonID.Count().Equals(0) Then
                Return SalesPersonIDCreate(appID)
            Else
                Return SalesPersonIDUpdate(objSalesPersonID.SingleOrDefault().SalesPersonID, appID)
            End If
        End Function
#End Region
#Region "tblFuelLevy"
        Private Shared Function GetLatestFuelLevyID(appID As System.Guid) As FMS.Business.tblFuelLevy
            Return (From c In SingletonAccess.FMSDataContextContignous.tblFuelLevies
                    Where c.ApplicationId.Equals(appID)
                    Order By c.Aid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function FuelLevyIDCreate(appID As System.Guid) As Integer
            Dim tblFuelLevyAId As FMS.Business.tblFuelLevy = GetLatestFuelLevyID(appID)
            Dim objFuelLevyID As New FMS.Business.tblProjectID
            With objFuelLevyID
                .ProjectID = Guid.NewGuid()
                .FuelLevyID = tblFuelLevyAId.Aid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objFuelLevyID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objFuelLevyID.FuelLevyID
        End Function
        Private Shared Function FuelLevyIDUpdate(FuelLevyID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If FuelLevyID Is Nothing Then
                Dim tblFuelLevyAId As FMS.Business.tblFuelLevy = GetLatestFuelLevyID(appID)
                FuelLevyID = tblFuelLevyAId.Aid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.FuelLevyID.Equals(FuelLevyID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .FuelLevyID = FuelLevyID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.FuelLevyID
        End Function
        Public Shared Function FuelLevyIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objFuelLevyID = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objFuelLevyID Is Nothing AndAlso objFuelLevyID.Count().Equals(0) Then
                Return FuelLevyIDCreate(appID)
            Else
                Return FuelLevyIDUpdate(objFuelLevyID.SingleOrDefault().FuelLevyID, appID)
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
#Region "tblSiteReSignDetails"
        Private Shared Function GetLatestSiteReSignDetailsID(appID As System.Guid) As FMS.Business.tblSiteReSignDetail
            Return (From c In SingletonAccess.FMSDataContextContignous.tblSiteReSignDetails
                    Where c.ApplicationID.Equals(appID)
                    Order By c.Cid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function SiteReSignDetailsIDCreate(appID As System.Guid) As Integer
            Dim tblSiteReSignDetailsCId As FMS.Business.tblSiteReSignDetail = GetLatestSiteReSignDetailsID(appID)
            Dim objSiteReSignDetailsID As New FMS.Business.tblProjectID
            With objSiteReSignDetailsID
                .ProjectID = Guid.NewGuid()
                .ReSignDetailsID = tblSiteReSignDetailsCId.Cid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objSiteReSignDetailsID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objSiteReSignDetailsID.ReSignDetailsID
        End Function
        Private Shared Function SiteReSignDetailsIDUpdate(ReSignDetailsID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If ReSignDetailsID Is Nothing Then
                Dim tblSiteReSignDetailsCId As FMS.Business.tblSiteReSignDetail = GetLatestSiteReSignDetailsID(appID)
                ReSignDetailsID = tblSiteReSignDetailsCId.Cid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ReSignDetailsID.Equals(ReSignDetailsID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .ReSignDetailsID = ReSignDetailsID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.ReSignDetailsID
        End Function
        Public Shared Function SiteReSignDetailsIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objSiteReSignDetails = SingletonAccess.FMSDataContextContignous.tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
            If Not objSiteReSignDetails Is Nothing AndAlso objSiteReSignDetails.Count().Equals(0) Then
                Return SiteReSignDetailsIDCreate(appID)
            Else
                Return SiteReSignDetailsIDUpdate(objSiteReSignDetails.SingleOrDefault().ReSignDetailsID, appID)
            End If
        End Function
#End Region
#Region "tblCustomerServices"
        Private Shared Function GetLatestCustomerServicesID(appID As System.Guid) As FMS.Business.tblCustomerService
            Return (From c In SingletonAccess.FMSDataContextContignous.tblCustomerServices
                    Where c.ApplicationID.Equals(appID)
                    Order By c.ID Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function CustomerServicesIDCreate(appID As System.Guid) As Integer
            Dim tblCustomerServicesId As FMS.Business.tblCustomerService = GetLatestCustomerServicesID(appID)
            Dim objCustomerServicesID As New FMS.Business.tblProjectID
            With objCustomerServicesID
                .ProjectID = Guid.NewGuid()
                .CustomerServicesID = tblCustomerServicesId.ID + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objCustomerServicesID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objCustomerServicesID.CustomerServicesID
        End Function
        Private Shared Function CustomerServicesIDUpdate(CustomerServicesID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If CustomerServicesID Is Nothing Then
                Dim tblCustomerServicesId As FMS.Business.tblCustomerService = GetLatestCustomerServicesID(appID)
                CustomerServicesID = tblCustomerServicesId.ID
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.CustomerServicesID.Equals(CustomerServicesID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .CustomerServicesID = CustomerServicesID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.CustomerServicesID
        End Function
        Public Shared Function CustomerServicesIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objCustomerServices = SingletonAccess.FMSDataContextContignous.tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
            If Not objCustomerServices Is Nothing AndAlso objCustomerServices.Count().Equals(0) Then
                Return CustomerServicesIDCreate(appID)
            Else
                Return CustomerServicesIDUpdate(objCustomerServices.SingleOrDefault().CustomerServicesID, appID)
            End If
        End Function
#End Region
#Region "tblCIRHistory"
        Private Shared Function GetLatestCIRHistoryID(appID As System.Guid) As FMS.Business.tblCIRHistory
            Return (From c In SingletonAccess.FMSDataContextContignous.tblCIRHistories
                    Where c.ApplicationID.Equals(appID)
                    Order By c.NCId Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function CIRHistoryIDCreate(appID As System.Guid) As Integer
            Dim tblCIRHistoryNCId As FMS.Business.tblCIRHistory = GetLatestCIRHistoryID(appID)
            Dim objCIRHistoryID As New FMS.Business.tblProjectID
            With objCIRHistoryID
                .ProjectID = Guid.NewGuid()
                .CIRHistoryID = tblCIRHistoryNCId.NCId + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objCIRHistoryID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objCIRHistoryID.CIRHistoryID
        End Function
        Private Shared Function CIRHistoryIDUpdate(CIRHistoryID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If CIRHistoryID Is Nothing Then
                Dim tblCIRHistoryId As FMS.Business.tblCIRHistory = GetLatestCIRHistoryID(appID)
                CIRHistoryID = tblCIRHistoryId.NCId
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.CIRHistoryID.Equals(CIRHistoryID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .CIRHistoryID = CIRHistoryID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.CIRHistoryID
        End Function
        Public Shared Function CIRHistoryIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objCIRHistory = SingletonAccess.FMSDataContextContignous.tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
            If Not objCIRHistory Is Nothing AndAlso objCIRHistory.Count().Equals(0) Then
                Return CIRHistoryIDCreate(appID)
            Else
                Return CIRHistoryIDUpdate(objCIRHistory.SingleOrDefault().CIRHistoryID, appID)
            End If
        End Function
#End Region
#Region "tblRuns"
        Private Shared Function GetLatestRunID(appID As System.Guid) As FMS.Business.tblRun
            Return (From c In SingletonAccess.FMSDataContextContignous.tblRuns
                    Where c.ApplicationID.Equals(appID)
                    Order By c.Rid Descending
                    Select c).FirstOrDefault()
        End Function
        Private Shared Function RunIDCreate(appID As System.Guid) As Integer
            Dim tblRunRid As FMS.Business.tblRun = GetLatestRunID(appID)
            Dim objRunID As New FMS.Business.tblProjectID
            With objRunID
                .ProjectID = Guid.NewGuid()
                .RunID = tblRunRid.Rid + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objRunID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objRunID.RunID
        End Function
        Private Shared Function RunIDUpdate(RunID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If RunID Is Nothing Then
                Dim tblRunId As FMS.Business.tblRun = GetLatestRunID(appID)
                RunID = tblRunId.Rid
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.RunID.Equals(RunID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .RunID = RunID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.RunID
        End Function
        Public Shared Function RunIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objRun = SingletonAccess.FMSDataContextContignous.tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
            If Not objRun Is Nothing AndAlso objRun.Count().Equals(0) Then
                Return RunIDCreate(appID)
            Else
                Return RunIDUpdate(objRun.SingleOrDefault().RunID, appID)
            End If
        End Function
#End Region
#Region "tblSiteComments"
        Private Shared Function GetLatestSiteCommentsID(appID As System.Guid) As FMS.Business.tblSiteComment
            With New LINQtoSQLClassesDataContext
                Return (From c In .tblSiteComments
                        Where c.ApplicationID.Equals(appID)
                        Order By c.Aid Descending
                        Select c).FirstOrDefault()
            End With
        End Function
        Private Shared Function SiteCommentsIDCreate(appID As System.Guid) As Integer
            With New LINQtoSQLClassesDataContext
                Dim tblSiteCommentsId As FMS.Business.tblSiteComment = GetLatestSiteCommentsID(appID)
                Dim objSiteCommentsId As New FMS.Business.tblProjectID
                With objSiteCommentsId
                    .ProjectID = Guid.NewGuid()
                    .CommentsID = tblSiteCommentsId.Aid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objSiteCommentsId)
                .SubmitChanges()
                Return objSiteCommentsId.RunID
            End With
        End Function
        Private Shared Function SiteCommentsIDUpdate(SiteCommentsID As Object, appID As System.Guid) As Integer
            With New LINQtoSQLClassesDataContext
                Dim objProject As FMS.Business.tblProjectID = Nothing
                If SiteCommentsID Is Nothing Then
                    Dim tblSiteCommentsId As FMS.Business.tblSiteComment = GetLatestSiteCommentsID(appID)
                    SiteCommentsID = tblSiteCommentsId.Aid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.RunID.Equals(SiteCommentsID) And c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .CommentsID = SiteCommentsID + 1
                End With
                .SubmitChanges()
                Return objProject.CommentsID
            End With
        End Function
        Public Shared Function SiteCommentsIDCreateOrUpdate(appID As System.Guid) As Integer
            With New LINQtoSQLClassesDataContext
                Dim objSiteComments = SingletonAccess.FMSDataContextContignous.tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                If Not objSiteComments Is Nothing AndAlso objSiteComments.Count().Equals(0) Then
                    Return SiteCommentsIDCreate(appID)
                Else
                    Return SiteCommentsIDUpdate(objSiteComments.SingleOrDefault().CommentsID, appID)
                End If
            End With
        End Function
#End Region
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
#End Region
    End Class
End Namespace

