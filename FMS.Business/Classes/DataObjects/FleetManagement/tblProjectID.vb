Namespace DataObjects
    Public Class tblProjectID
#Region "Properties / enums"
#End Region
#Region "CRUD"
#Region "tblCustomer"
        Private Shared Function GetLatestCustomerID(appID As System.Guid) As FMS.Business.tblCustomer
            Dim cust As FMS.Business.tblCustomer
            With New LINQtoSQLClassesDataContext
                cust = (From c In .tblCustomers
                        Where c.ApplicationID.Equals(appID)
                        Order By c.Cid Descending
                        Select c).FirstOrDefault()
                .Dispose()
            End With
            Return cust
        End Function
        Private Shared Function CustomerIDCreate(appID As System.Guid) As Integer
            Dim objCustId As FMS.Business.tblCustomer = GetLatestCustomerID(appID)
            Dim objCustomerID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objCustomerID
                    .ProjectID = Guid.NewGuid()
                    .CustomersID = objCustId.Cid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objCustomerID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objCustomerID.CustomersID
        End Function
        Private Shared Function CustomerIDUpdate(CustomerID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If CustomerID Is Nothing Then
                    Dim tblCustomerCId As FMS.Business.tblCustomer = GetLatestCustomerID(appID)
                    CustomerID = tblCustomerCId.Cid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .CustomersID = CustomerID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With

            Return objProject.CustomersID
        End Function
        Public Shared Function CustomerIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objCustomer As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objCustomer = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objCustomer Is Nothing AndAlso objCustomer.Count().Equals(0) Then
                Return CustomerIDCreate(appID)
            Else
                Return CustomerIDUpdate(objCustomer.SingleOrDefault().CustomersID, appID)
            End If
        End Function
#End Region
#Region "tblSites"
        Private Shared Function GetLatestSiteID(appID As System.Guid) As FMS.Business.tblSite
            Dim site As FMS.Business.tblSite
            With New LINQtoSQLClassesDataContext
                site = (From c In .tblSites
                        Where c.ApplicationId.Equals(appID)
                        Order By c.Cid Descending
                        Select c).FirstOrDefault()
                .Dispose()
            End With
            Return site
        End Function
        Private Shared Function SiteIDCreate(appID As System.Guid) As Integer
            Dim tblSiteCId As FMS.Business.tblSite = GetLatestSiteID(appID)
            Dim objSiteID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objSiteID
                    .ProjectID = Guid.NewGuid()
                    .SitesID = tblSiteCId.Cid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objSiteID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objSiteID.SitesID
        End Function
        Private Shared Function SiteIDUpdate(SiteID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If SiteID Is Nothing Then
                    Dim tblSiteCId As FMS.Business.tblSite = GetLatestSiteID(appID)
                    SiteID = tblSiteCId.Cid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .SitesID = SiteID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With
            Return objProject.SitesID
        End Function
        Public Shared Function SiteIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objSites As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objSites = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objSites Is Nothing AndAlso objSites.Count().Equals(0) Then
                Return SiteIDCreate(appID)
            Else
                Return SiteIDUpdate(objSites.SingleOrDefault().SitesID, appID)
            End If
        End Function
#End Region
#Region "tblIndustryGroups"
        Private Shared Function GetLatestIndustryGroupID(appID As System.Guid) As FMS.Business.tblIndustryGroup
            Dim industryGroup As FMS.Business.tblIndustryGroup
            With New LINQtoSQLClassesDataContext
                industryGroup = (From c In .tblIndustryGroups
                                 Where c.ApplicationID.Equals(appID)
                                 Order By c.Aid Descending
                                 Select c).FirstOrDefault()
                .Dispose()
            End With
            Return industryGroup
        End Function
        Private Shared Function IndustryGroupIDCreate(appID As System.Guid) As Integer
            Dim tblIndustryGroupAId As FMS.Business.tblIndustryGroup = GetLatestIndustryGroupID(appID)
            Dim objIndustryGroupID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objIndustryGroupID
                    .ProjectID = Guid.NewGuid()
                    .IndustryGroupID = tblIndustryGroupAId.Aid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objIndustryGroupID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objIndustryGroupID.IndustryGroupID
        End Function
        Private Shared Function IndustryGroupIDUpdate(IndustryGroupID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If IndustryGroupID Is Nothing Then
                    Dim tblIndustryGroupAId As FMS.Business.tblIndustryGroup = GetLatestIndustryGroupID(appID)
                    IndustryGroupID = tblIndustryGroupAId.Aid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .IndustryGroupID = IndustryGroupID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With

            Return objProject.IndustryGroupID
        End Function
        Public Shared Function IndustryGroupIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objIndustryGroup As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objIndustryGroup = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objIndustryGroup Is Nothing AndAlso objIndustryGroup.Count().Equals(0) Then
                Return IndustryGroupIDCreate(appID)
            Else
                Return IndustryGroupIDUpdate(objIndustryGroup.SingleOrDefault().IndustryGroupID, appID)
            End If
        End Function
#End Region
#Region "tblInvoicingFrequency"
        Private Shared Function GetLatestInvoiceFrequencyID() As FMS.Business.tblInvoicingFrequency
            Dim invoicingFrequency As FMS.Business.tblInvoicingFrequency
            With New LINQtoSQLClassesDataContext
                invoicingFrequency = (From c In .tblInvoicingFrequencies
                                      Where c.ApplicationId.Equals(ThisSession.ApplicationID)
                                      Order By c.IId Descending
                                      Select c).FirstOrDefault()
                .Dispose()
            End With
            Return invoicingFrequency
        End Function
        Private Shared Function InvoiceFrequencyIDCreate() As Integer
            Dim tblInvoiceFrequencyIId As FMS.Business.tblInvoicingFrequency = GetLatestInvoiceFrequencyID()
            Dim objInvoiceFrequencyID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objInvoiceFrequencyID
                    .ProjectID = Guid.NewGuid()
                    .InvoiceFrequencyID = tblInvoiceFrequencyIId.IId + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objInvoiceFrequencyID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objInvoiceFrequencyID.InvoiceFrequencyID
        End Function
        Private Shared Function InvoiceFrequencyIDUpdate(InvoiceFrequencyID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If InvoiceFrequencyID Is Nothing Then
                    Dim tblInvoiceFrequencyIId As FMS.Business.tblInvoicingFrequency = GetLatestInvoiceFrequencyID()
                    InvoiceFrequencyID = tblInvoiceFrequencyIId.IId
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(ThisSession.ApplicationID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                End If

                With objProject
                    .InvoiceFrequencyID = InvoiceFrequencyID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With
            Return objProject.InvoiceFrequencyID
        End Function
        Public Shared Function InvoiceFrequencyIDCreateOrUpdate() As Integer
            Dim objInvoiceFrequency As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objInvoiceFrequency = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(ThisSession.ApplicationID)).ToList()
                .Dispose()
            End With
            If Not objInvoiceFrequency Is Nothing AndAlso objInvoiceFrequency.Count().Equals(0) Then
                Return InvoiceFrequencyIDCreate()
            Else
                Return InvoiceFrequencyIDUpdate(objInvoiceFrequency.SingleOrDefault().InvoiceFrequencyID)
            End If
        End Function
#End Region
#Region "tblCustomerAgent"
        Private Shared Function GetLatestCustomerAgentID(appID As System.Guid) As FMS.Business.tblCustomerAgent
            Dim custAgent As FMS.Business.tblCustomerAgent
            With New LINQtoSQLClassesDataContext
                custAgent = (From c In .tblCustomerAgents
                             Where c.ApplicationID.Equals(appID)
                             Order By c.AID Descending
                             Select c).FirstOrDefault()
                .Dispose()
            End With
            Return custAgent
        End Function
        Private Shared Function CustomerAgentIDCreate(appID As System.Guid) As Integer
            Dim tblCustomerAgentIId As FMS.Business.tblCustomerAgent = GetLatestCustomerAgentID(appID)
            Dim objCustomerAgentID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objCustomerAgentID
                    .ProjectID = Guid.NewGuid()
                    .CustomerAgentID = tblCustomerAgentIId.AID + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objCustomerAgentID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objCustomerAgentID.CustomerAgentID
        End Function
        Private Shared Function CustomerAgentIDUpdate(CustomerAgentID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If CustomerAgentID Is Nothing Then
                    Dim tblCustomerAgentIId As FMS.Business.tblCustomerAgent = GetLatestCustomerAgentID(appID)
                    CustomerAgentID = tblCustomerAgentIId.AID
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .CustomerAgentID = CustomerAgentID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With

            Return objProject.CustomerAgentID
        End Function
        Public Shared Function CustomerAgentIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objCustomerAgent As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objCustomerAgent = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objCustomerAgent Is Nothing AndAlso objCustomerAgent.Count().Equals(0) Then
                Return CustomerAgentIDCreate(appID)
            Else
                Return CustomerAgentIDUpdate(objCustomerAgent.SingleOrDefault().CustomerAgentID, appID)
            End If
        End Function
#End Region
#Region "tblContractCeaseReasons"
        Private Shared Function GetLatestCeaseReasonID(appID As System.Guid) As FMS.Business.tblContractCeaseReason
            Dim contractCeaseReason As FMS.Business.tblContractCeaseReason
            With New LINQtoSQLClassesDataContext
                contractCeaseReason = (From c In .tblContractCeaseReasons
                                       Where c.ApplicationID.Equals(appID)
                                       Order By c.Aid Descending
                                       Select c).FirstOrDefault()
                .Dispose()
            End With
            Return contractCeaseReason
        End Function
        Private Shared Function CeaseReasonIDCreate(appID As System.Guid) As Integer
            Dim tblCeaseReasonAId As FMS.Business.tblContractCeaseReason = GetLatestCeaseReasonID(appID)
            Dim objCeaseReasonID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objCeaseReasonID
                    .ProjectID = Guid.NewGuid()
                    .CeaseReasonID = tblCeaseReasonAId.Aid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objCeaseReasonID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objCeaseReasonID.CeaseReasonID
        End Function
        Private Shared Function CeaseReasonIDUpdate(CeaseReasonID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If CeaseReasonID Is Nothing Then
                    Dim tblCeaseReasonAId As FMS.Business.tblContractCeaseReason = GetLatestCeaseReasonID(appID)
                    CeaseReasonID = tblCeaseReasonAId.Aid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .CeaseReasonID = CeaseReasonID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With

            Return objProject.CeaseReasonID
        End Function
        Public Shared Function CeaseReasonIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objCeaseReason As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objCeaseReason = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
            End With
            If Not objCeaseReason Is Nothing AndAlso objCeaseReason.Count().Equals(0) Then
                Return CeaseReasonIDCreate(appID)
            Else
                Return CeaseReasonIDUpdate(objCeaseReason.SingleOrDefault().CeaseReasonID, appID)
            End If
        End Function
#End Region
#Region "tblCIRReason"
        Private Shared Function GetLatestCIRReasonID(appID As System.Guid) As FMS.Business.tblCIRReason
            Dim cirReason As FMS.Business.tblCIRReason
            With New LINQtoSQLClassesDataContext
                cirReason = (From c In .tblCIRReasons
                             Where c.ApplicationID.Equals(appID)
                             Order By c.CId Descending
                             Select c).FirstOrDefault()
                .Dispose()
            End With
            Return cirReason
        End Function
        Private Shared Function CIRReasonIDCreate(appID As System.Guid) As Integer
            Dim tblCIRReasonCId As FMS.Business.tblCIRReason = GetLatestCIRReasonID(appID)
            Dim objCIRReasonID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objCIRReasonID
                    .ProjectID = Guid.NewGuid()
                    .CIRReasonID = tblCIRReasonCId.CId + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objCIRReasonID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objCIRReasonID.CIRReasonID
        End Function
        Private Shared Function CIRReasonIDUpdate(CIRReasonID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If CIRReasonID Is Nothing Then
                    Dim tblCIRReasonCId As FMS.Business.tblCIRReason = GetLatestCIRReasonID(appID)
                    CIRReasonID = tblCIRReasonCId.CId
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .CIRReasonID = CIRReasonID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With
            Return objProject.CIRReasonID
        End Function
        Public Shared Function CIRReasonIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objCIRReason As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objCIRReason = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objCIRReason Is Nothing AndAlso objCIRReason.Count().Equals(0) Then
                Return CIRReasonIDCreate(appID)
            Else
                Return CIRReasonIDUpdate(objCIRReason.SingleOrDefault().CIRReasonID, appID)
            End If
        End Function
#End Region
#Region "tbZones"
        Private Shared Function GetLatestZoneID(appID As System.Guid) As FMS.Business.tbZone
            Dim zone As FMS.Business.tbZone
            With New LINQtoSQLClassesDataContext
                zone = (From c In .tbZones
                        Where c.ApplicationID.Equals(appID)
                        Order By c.Aid Descending
                        Select c).FirstOrDefault()
                .Dispose()
            End With
            Return zone
        End Function
        Private Shared Function ZoneIDCreate(appID As System.Guid) As Integer
            Dim tblZoneAId As FMS.Business.tbZone = GetLatestZoneID(appID)
            Dim objZoneID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objZoneID
                    .ProjectID = Guid.NewGuid()
                    .ZoneID = tblZoneAId.Aid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objZoneID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objZoneID.ZoneID
        End Function
        Private Shared Function ZoneIDUpdate(ZoneID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If ZoneID Is Nothing Then
                    Dim tblZoneAId As FMS.Business.tbZone = GetLatestZoneID(appID)
                    ZoneID = tblZoneAId.Aid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .ZoneID = ZoneID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With
            Return objProject.ZoneID
        End Function
        Public Shared Function ZoneIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objZone As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objZone = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objZone Is Nothing AndAlso objZone.Count().Equals(0) Then
                Return ZoneIDCreate(appID)
            Else
                Return ZoneIDUpdate(objZone.SingleOrDefault().ZoneID, appID)
            End If
        End Function
#End Region
#Region "tblCustomerRating"
        Private Shared Function GetLatestCustomerRatingID(appID As System.Guid) As FMS.Business.tblCustomerRating
            Dim custRating As FMS.Business.tblCustomerRating
            With New LINQtoSQLClassesDataContext
                custRating = (From c In .tblCustomerRatings
                              Where c.ApplicationId.Equals(appID)
                              Order By c.Rid Descending
                              Select c).FirstOrDefault()
                .Dispose()
            End With
            Return custRating
        End Function
        Private Shared Function CustomerRatingIDCreate(appID As System.Guid) As Integer
            Dim tblCustomerRatingRId As FMS.Business.tblCustomerRating = GetLatestCustomerRatingID(appID)
            Dim objCustomerRatingID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objCustomerRatingID
                    .ProjectID = Guid.NewGuid()
                    .CustomerRatingID = tblCustomerRatingRId.Rid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objCustomerRatingID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objCustomerRatingID.CustomerRatingID
        End Function
        Private Shared Function CustomerRatingIDUpdate(CustomerRatingID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If CustomerRatingID Is Nothing Then
                    Dim tblCustomerRatingRId As FMS.Business.tblCustomerRating = GetLatestCustomerRatingID(appID)
                    CustomerRatingID = tblCustomerRatingRId.Rid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If
                With objProject
                    .CustomerRatingID = CustomerRatingID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With

            Return objProject.CustomerRatingID
        End Function
        Public Shared Function CustomerRatingIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objCustomerRatingID As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objCustomerRatingID = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objCustomerRatingID Is Nothing AndAlso objCustomerRatingID.Count().Equals(0) Then
                Return CustomerRatingIDCreate(appID)
            Else
                Return CustomerRatingIDUpdate(objCustomerRatingID.SingleOrDefault().CustomerRatingID, appID)
            End If
        End Function
#End Region
#Region "tblRateIncreaseReference"
        Private Shared Function GetLatestRateIncreaseID(appID As System.Guid) As FMS.Business.tblRateIncreaseReference
            Dim rateIncreaseRef As FMS.Business.tblRateIncreaseReference
            With New LINQtoSQLClassesDataContext
                rateIncreaseRef = (From c In .tblRateIncreaseReferences
                                   Where c.ApplicationID.Equals(appID)
                                   Order By c.Aid Descending
                                   Select c).FirstOrDefault()
                .Dispose()
            End With
            Return rateIncreaseRef
        End Function
        Private Shared Function RateIncreaseIDCreate(appID As System.Guid) As Integer
            Dim tblRateIncreaseAId As FMS.Business.tblRateIncreaseReference = GetLatestRateIncreaseID(appID)
            Dim objRateIncreaseID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objRateIncreaseID
                    .ProjectID = Guid.NewGuid()
                    .RateIncreaseID = tblRateIncreaseAId.Aid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objRateIncreaseID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objRateIncreaseID.RateIncreaseID
        End Function
        Private Shared Function RateIncreaseIDUpdate(RateIncreaseID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If RateIncreaseID Is Nothing Then
                    Dim tblRateIncreaseAId As FMS.Business.tblRateIncreaseReference = GetLatestRateIncreaseID(appID)
                    RateIncreaseID = tblRateIncreaseAId.Aid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .RateIncreaseID = RateIncreaseID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With
            Return objProject.RateIncreaseID
        End Function
        Public Shared Function RateIncreaseIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objRateIncreaseID As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objRateIncreaseID = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objRateIncreaseID Is Nothing AndAlso objRateIncreaseID.Count().Equals(0) Then
                Return RateIncreaseIDCreate(appID)
            Else
                Return RateIncreaseIDUpdate(objRateIncreaseID.SingleOrDefault().RateIncreaseID, appID)
            End If
        End Function
#End Region
#Region "tblPreviousSuppliers"
        Private Shared Function GetLatestPreviousSupplierID(appID As System.Guid) As FMS.Business.tblPreviousSupplier
            Dim prevSupplier As FMS.Business.tblPreviousSupplier
            With New LINQtoSQLClassesDataContext
                prevSupplier = (From c In .tblPreviousSuppliers
                                Where c.ApplicationID.Equals(appID)
                                Order By c.Aid Descending
                                Select c).FirstOrDefault()
                .Dispose()
            End With
            Return prevSupplier
        End Function
        Private Shared Function PreviousSupplierIDCreate(appID As System.Guid) As Integer
            Dim tblPreviousSupplierAId As FMS.Business.tblPreviousSupplier = GetLatestPreviousSupplierID(appID)
            Dim objPreviousSupplierID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objPreviousSupplierID
                    .ProjectID = Guid.NewGuid()
                    .PreviousSupplierID = tblPreviousSupplierAId.Aid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objPreviousSupplierID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objPreviousSupplierID.PreviousSupplierID
        End Function
        Private Shared Function PreviousSupplierIDUpdate(PreviousSupplierID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If PreviousSupplierID Is Nothing Then
                    Dim tblPreviousSupplierAId As FMS.Business.tblPreviousSupplier = GetLatestPreviousSupplierID(appID)
                    PreviousSupplierID = tblPreviousSupplierAId.Aid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .PreviousSupplierID = PreviousSupplierID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With
            Return objProject.PreviousSupplierID
        End Function
        Public Shared Function PreviousSupplierIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objPreviousSupplierID As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objPreviousSupplierID = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objPreviousSupplierID Is Nothing AndAlso objPreviousSupplierID.Count().Equals(0) Then
                Return PreviousSupplierIDCreate(appID)
            Else
                Return PreviousSupplierIDUpdate(objPreviousSupplierID.SingleOrDefault().PreviousSupplierID, appID)
            End If
        End Function
#End Region
#Region "tblRunFortnightlyCycles"
        Private Shared Function GetLatestFortnightlyCyclesID(appID As System.Guid) As FMS.Business.tblRunFortnightlyCycle
            Dim runFort As FMS.Business.tblRunFortnightlyCycle
            With New LINQtoSQLClassesDataContext
                runFort = (From c In .tblRunFortnightlyCycles
                           Where c.ApplicationID.Equals(appID)
                           Order By c.Aid Descending
                           Select c).FirstOrDefault()
                .Dispose()
            End With
            Return runFort
        End Function
        Private Shared Function FortnightlyCyclesIDCreate(appID As System.Guid) As Integer
            Dim tblFortnightlyCyclesAId As FMS.Business.tblRunFortnightlyCycle = GetLatestFortnightlyCyclesID(appID)
            Dim objFortnightlyCyclesID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objFortnightlyCyclesID
                    .ProjectID = Guid.NewGuid()
                    .FortnightlyCyclesID = tblFortnightlyCyclesAId.Aid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objFortnightlyCyclesID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objFortnightlyCyclesID.FortnightlyCyclesID
        End Function
        Private Shared Function FortnightlyCyclesIDUpdate(FortnightlyCyclesID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If FortnightlyCyclesID Is Nothing Then
                    Dim tblFortnightlyCyclesAId As FMS.Business.tblRunFortnightlyCycle = GetLatestFortnightlyCyclesID(appID)
                    FortnightlyCyclesID = tblFortnightlyCyclesAId.Aid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .FortnightlyCyclesID = FortnightlyCyclesID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With

            Return objProject.FortnightlyCyclesID
        End Function
        Public Shared Function FortnightlyCyclesIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objFortnightlyCyclesID As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objFortnightlyCyclesID = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objFortnightlyCyclesID Is Nothing AndAlso objFortnightlyCyclesID.Count().Equals(0) Then
                Return FortnightlyCyclesIDCreate(appID)
            Else
                Return FortnightlyCyclesIDUpdate(objFortnightlyCyclesID.SingleOrDefault().FortnightlyCyclesID, appID)
            End If
        End Function
#End Region
#Region "tblRevenueChangeReason"
        Private Shared Function GetLatestRevenueChangeReasonID(appID As System.Guid) As FMS.Business.tblRevenueChangeReason
            Dim revChange As FMS.Business.tblRevenueChangeReason
            With New LINQtoSQLClassesDataContext
                revChange = (From c In .tblRevenueChangeReasons
                             Where c.ApplicationID.Equals(appID)
                             Order By c.Rid Descending
                             Select c).FirstOrDefault()
                .Dispose()
            End With
            Return revChange
        End Function
        Private Shared Function RevenueChangeReasonIDCreate(appID As System.Guid) As Integer
            Dim tblRevenueChangeReasonRId As FMS.Business.tblRevenueChangeReason = GetLatestRevenueChangeReasonID(appID)
            Dim objRevenueChangeReasonID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objRevenueChangeReasonID
                    .ProjectID = Guid.NewGuid()
                    .RevenueChangeReasonID = tblRevenueChangeReasonRId.Rid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objRevenueChangeReasonID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objRevenueChangeReasonID.RevenueChangeReasonID
        End Function
        Private Shared Function RevenueChangeReasonIDUpdate(RevenueChangeReasonID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If RevenueChangeReasonID Is Nothing Then
                    Dim tblRevenueChangeReasonRId As FMS.Business.tblRevenueChangeReason = GetLatestRevenueChangeReasonID(appID)
                    RevenueChangeReasonID = tblRevenueChangeReasonRId.Rid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .RevenueChangeReasonID = RevenueChangeReasonID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With
            Return objProject.RevenueChangeReasonID
        End Function
        Public Shared Function RevenueChangeReasonIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objRevenueChangeReasonID As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objRevenueChangeReasonID = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objRevenueChangeReasonID Is Nothing AndAlso objRevenueChangeReasonID.Count().Equals(0) Then
                Return RevenueChangeReasonIDCreate(appID)
            Else
                Return RevenueChangeReasonIDUpdate(objRevenueChangeReasonID.SingleOrDefault().RevenueChangeReasonID, appID)
            End If
        End Function
#End Region
#Region "tblServiceFrequency"
        Private Shared Function GetLatestFrequencyID(appID As System.Guid) As FMS.Business.tblServiceFrequency
            Dim serviceFreq As FMS.Business.tblServiceFrequency
            With New LINQtoSQLClassesDataContext
                serviceFreq = (From c In .tblServiceFrequencies
                               Where c.ApplicationID.Equals(appID)
                               Order By c.Fid Descending
                               Select c).FirstOrDefault()
                .Dispose()
            End With
            Return serviceFreq
        End Function
        Private Shared Function FrequencyIDCreate(appID As System.Guid) As Integer
            Dim tblFrequencyFId As FMS.Business.tblServiceFrequency = GetLatestFrequencyID(appID)
            Dim objFrequencyID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objFrequencyID
                    .ProjectID = Guid.NewGuid()
                    .FrequencyID = tblFrequencyFId.Fid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objFrequencyID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objFrequencyID.FrequencyID
        End Function
        Private Shared Function FrequencyIDUpdate(FrequencyID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If FrequencyID Is Nothing Then
                    Dim tblFrequencyFId As FMS.Business.tblServiceFrequency = GetLatestFrequencyID(appID)
                    FrequencyID = tblFrequencyFId.Fid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .FrequencyID = FrequencyID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With
            Return objProject.FrequencyID
        End Function
        Public Shared Function FrequencyIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objFrequencyID As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objFrequencyID = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objFrequencyID Is Nothing AndAlso objFrequencyID.Count().Equals(0) Then
                Return FrequencyIDCreate(appID)
            Else
                Return FrequencyIDUpdate(objFrequencyID.SingleOrDefault().FrequencyID, appID)
            End If
        End Function
#End Region
#Region "tblServices"
        Private Shared Function GetLatestServicesID(appID As System.Guid) As FMS.Business.tblService
            Dim serv As FMS.Business.tblService
            With New LINQtoSQLClassesDataContext
                serv = (From c In .tblServices
                        Where c.ApplicationID.Equals(appID)
                        Order By c.Sid Descending
                        Select c).FirstOrDefault()
                .Dispose()
            End With
            Return serv
        End Function
        Private Shared Function ServicesIDCreate(appID As System.Guid) As Integer
            Dim tblServicesSId As FMS.Business.tblService = GetLatestServicesID(appID)
            Dim objServicesID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objServicesID
                    .ProjectID = Guid.NewGuid()
                    .ServicesID = tblServicesSId.Sid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objServicesID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objServicesID.ServicesID
        End Function
        Private Shared Function ServicesIDUpdate(ServicesID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If ServicesID Is Nothing Then
                    Dim tblServicesSId As FMS.Business.tblService = GetLatestServicesID(appID)
                    ServicesID = tblServicesSId.Sid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .ServicesID = ServicesID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With

            Return objProject.ServicesID
        End Function
        Public Shared Function ServicesIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objServicesID As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objServicesID = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objServicesID Is Nothing AndAlso objServicesID.Count().Equals(0) Then
                Return ServicesIDCreate(appID)
            Else
                Return ServicesIDUpdate(objServicesID.SingleOrDefault().ServicesID, appID)
            End If
        End Function
#End Region
#Region "tblSalesPersons"
        Private Shared Function GetLatestSalesPersonID(appID As System.Guid) As FMS.Business.tblSalesPerson
            Dim sales As FMS.Business.tblSalesPerson
            With New LINQtoSQLClassesDataContext
                sales = (From c In .tblSalesPersons
                         Where c.ApplicationID.Equals(appID)
                         Order By c.Aid Descending
                         Select c).FirstOrDefault()
                .Dispose()
            End With
            Return sales
        End Function
        Private Shared Function SalesPersonIDCreate(appID As System.Guid) As Integer
            Dim tblSalesPersonAId As FMS.Business.tblSalesPerson = GetLatestSalesPersonID(appID)
            Dim objSalesPersonID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objSalesPersonID
                    .ProjectID = Guid.NewGuid()
                    .SalesPersonID = tblSalesPersonAId.Aid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objSalesPersonID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objSalesPersonID.SalesPersonID
        End Function
        Private Shared Function SalesPersonIDUpdate(SalesPersonID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If SalesPersonID Is Nothing Then
                    Dim tblSalesPersonAId As FMS.Business.tblSalesPerson = GetLatestSalesPersonID(appID)
                    SalesPersonID = tblSalesPersonAId.Aid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .SalesPersonID = SalesPersonID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With

            Return objProject.SalesPersonID
        End Function
        Public Shared Function SalesPersonIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objSalesPersonID As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objSalesPersonID = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objSalesPersonID Is Nothing AndAlso objSalesPersonID.Count().Equals(0) Then
                Return SalesPersonIDCreate(appID)
            Else
                Return SalesPersonIDUpdate(objSalesPersonID.SingleOrDefault().SalesPersonID, appID)
            End If
        End Function
#End Region
#Region "tblFuelLevy"
        Private Shared Function GetLatestFuelLevyID(appID As System.Guid) As FMS.Business.tblFuelLevy
            Dim fuel As FMS.Business.tblFuelLevy
            With New LINQtoSQLClassesDataContext
                fuel = (From c In .tblFuelLevies
                        Where c.ApplicationId.Equals(appID)
                        Order By c.Aid Descending
                        Select c).FirstOrDefault()
                .Dispose()
            End With
            Return fuel
        End Function
        Private Shared Function FuelLevyIDCreate(appID As System.Guid) As Integer
            Dim tblFuelLevyAId As FMS.Business.tblFuelLevy = GetLatestFuelLevyID(appID)
            Dim objFuelLevyID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objFuelLevyID
                    .ProjectID = Guid.NewGuid()
                    .FuelLevyID = tblFuelLevyAId.Aid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objFuelLevyID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objFuelLevyID.FuelLevyID
        End Function
        Private Shared Function FuelLevyIDUpdate(FuelLevyID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If FuelLevyID Is Nothing Then
                    Dim tblFuelLevyAId As FMS.Business.tblFuelLevy = GetLatestFuelLevyID(appID)
                    FuelLevyID = tblFuelLevyAId.Aid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .FuelLevyID = FuelLevyID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With

            Return objProject.FuelLevyID
        End Function
        Public Shared Function FuelLevyIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objFuelLevyID As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objFuelLevyID = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objFuelLevyID Is Nothing AndAlso objFuelLevyID.Count().Equals(0) Then
                Return FuelLevyIDCreate(appID)
            Else
                Return FuelLevyIDUpdate(objFuelLevyID.SingleOrDefault().FuelLevyID, appID)
            End If
        End Function
#End Region
#Region "tblzGenerateRunSheets"
        Private Shared Function GetLatestRunSheetID() As FMS.Business.tblzGenerateRunSheet
            Dim generateRun As FMS.Business.tblzGenerateRunSheet
            With New LINQtoSQLClassesDataContext
                generateRun = (From c In .tblzGenerateRunSheets
                               Order By c.Aid Descending
                               Select c).FirstOrDefault()
                .Dispose()
            End With
            Return generateRun
        End Function
        Private Shared Function RunSheetIDCreate() As Integer
            Dim tblRunSheetAId As FMS.Business.tblzGenerateRunSheet = GetLatestRunSheetID()
            Dim objRunSheetID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objRunSheetID
                    .ProjectID = Guid.NewGuid()
                    .RunSheetID = tblRunSheetAId.Aid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objRunSheetID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objRunSheetID.RunSheetID
        End Function
        Private Shared Function RunSheetIDUpdate(RunSheetID As Object) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If RunSheetID Is Nothing Then
                    Dim tblRunSheetAId As FMS.Business.tblzGenerateRunSheet = GetLatestRunSheetID()
                    RunSheetID = tblRunSheetAId.Aid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(ThisSession.ApplicationID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                End If

                With objProject
                    .RunSheetID = RunSheetID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With
            Return objProject.RunSheetID
        End Function
        Public Shared Function RunSheetIDCreateOrUpdate() As Integer
            Dim objRunSheetID As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objRunSheetID = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(ThisSession.ApplicationID)).ToList()
                .Dispose()
            End With
            If Not objRunSheetID Is Nothing AndAlso objRunSheetID.Count().Equals(0) Then
                Return RunSheetIDCreate()
            Else
                Return RunSheetIDUpdate(objRunSheetID.SingleOrDefault().RunSheetID)
            End If
        End Function
#End Region
#Region "tblSiteReSignDetails"
        Private Shared Function GetLatestSiteReSignDetailsID(appID As System.Guid) As FMS.Business.tblSiteReSignDetail
            Dim siteResign As FMS.Business.tblSiteReSignDetail
            With New LINQtoSQLClassesDataContext
                siteResign = (From c In .tblSiteReSignDetails
                              Where c.ApplicationID.Equals(appID)
                              Order By c.Cid Descending
                              Select c).FirstOrDefault()
                .Dispose()
            End With
            Return siteResign
        End Function
        Private Shared Function SiteReSignDetailsIDCreate(appID As System.Guid) As Integer
            Dim tblSiteReSignDetailsCId As FMS.Business.tblSiteReSignDetail = GetLatestSiteReSignDetailsID(appID)
            Dim objSiteReSignDetailsID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objSiteReSignDetailsID
                    .ProjectID = Guid.NewGuid()
                    .ReSignDetailsID = tblSiteReSignDetailsCId.Cid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objSiteReSignDetailsID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objSiteReSignDetailsID.ReSignDetailsID
        End Function
        Private Shared Function SiteReSignDetailsIDUpdate(ReSignDetailsID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If ReSignDetailsID Is Nothing Then
                    Dim tblSiteReSignDetailsCId As FMS.Business.tblSiteReSignDetail = GetLatestSiteReSignDetailsID(appID)
                    ReSignDetailsID = tblSiteReSignDetailsCId.Cid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .ReSignDetailsID = ReSignDetailsID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With

            Return objProject.ReSignDetailsID
        End Function
        Public Shared Function SiteReSignDetailsIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objSiteReSignDetails As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objSiteReSignDetails = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objSiteReSignDetails Is Nothing AndAlso objSiteReSignDetails.Count().Equals(0) Then
                Return SiteReSignDetailsIDCreate(appID)
            Else
                Return SiteReSignDetailsIDUpdate(objSiteReSignDetails.SingleOrDefault().ReSignDetailsID, appID)
            End If
        End Function
#End Region
#Region "tblCustomerServices"
        Private Shared Function GetLatestCustomerServicesID(appID As System.Guid) As FMS.Business.tblCustomerService
            Dim custService As FMS.Business.tblCustomerService
            With New LINQtoSQLClassesDataContext
                custService = (From c In .tblCustomerServices
                               Where c.ApplicationID.Equals(appID)
                               Order By c.ID Descending
                               Select c).FirstOrDefault()
                .Dispose()
            End With
            Return custService
        End Function
        Private Shared Function CustomerServicesIDCreate(appID As System.Guid) As Integer
            Dim tblCustomerServicesId As FMS.Business.tblCustomerService = GetLatestCustomerServicesID(appID)
            Dim objCustomerServicesID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objCustomerServicesID
                    .ProjectID = Guid.NewGuid()
                    .CustomerServicesID = tblCustomerServicesId.ID + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objCustomerServicesID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objCustomerServicesID.CustomerServicesID
        End Function
        Private Shared Function CustomerServicesIDUpdate(CustomerServicesID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If CustomerServicesID Is Nothing Then
                    Dim tblCustomerServicesId As FMS.Business.tblCustomerService = GetLatestCustomerServicesID(appID)
                    CustomerServicesID = tblCustomerServicesId.ID
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .CustomerServicesID = CustomerServicesID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With

            Return objProject.CustomerServicesID
        End Function
        Public Shared Function CustomerServicesIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objCustomerServices As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objCustomerServices = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objCustomerServices Is Nothing AndAlso objCustomerServices.Count().Equals(0) Then
                Return CustomerServicesIDCreate(appID)
            Else
                Return CustomerServicesIDUpdate(objCustomerServices.SingleOrDefault().CustomerServicesID, appID)
            End If
        End Function
#End Region
#Region "tblCIRHistory"
        Private Shared Function GetLatestCIRHistoryID(appID As System.Guid) As FMS.Business.tblCIRHistory
            Dim CirHistory As FMS.Business.tblCIRHistory
            With New LINQtoSQLClassesDataContext
                CirHistory = (From c In .tblCIRHistories
                              Where c.ApplicationID.Equals(appID)
                              Order By c.NCId Descending
                              Select c).FirstOrDefault()
                .Dispose()
            End With
            Return CirHistory
        End Function
        Private Shared Function CIRHistoryIDCreate(appID As System.Guid) As Integer
            Dim tblCIRHistoryNCId As FMS.Business.tblCIRHistory = GetLatestCIRHistoryID(appID)
            Dim objCIRHistoryID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objCIRHistoryID
                    .ProjectID = Guid.NewGuid()
                    .CIRHistoryID = tblCIRHistoryNCId.NCId + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objCIRHistoryID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objCIRHistoryID.CIRHistoryID
        End Function
        Private Shared Function CIRHistoryIDUpdate(CIRHistoryID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing

            With New LINQtoSQLClassesDataContext
                If CIRHistoryID Is Nothing Then
                    Dim tblCIRHistoryId As FMS.Business.tblCIRHistory = GetLatestCIRHistoryID(appID)
                    CIRHistoryID = tblCIRHistoryId.NCId
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .CIRHistoryID = CIRHistoryID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With

            Return objProject.CIRHistoryID
        End Function
        Public Shared Function CIRHistoryIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objCIRHistory As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objCIRHistory = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objCIRHistory Is Nothing AndAlso objCIRHistory.Count().Equals(0) Then
                Return CIRHistoryIDCreate(appID)
            Else
                Return CIRHistoryIDUpdate(objCIRHistory.SingleOrDefault().CIRHistoryID, appID)
            End If
        End Function
#End Region
#Region "tblRuns"
        Private Shared Function GetLatestRunID(appID As System.Guid) As FMS.Business.tblRun
            Dim run As FMS.Business.tblRun
            With New LINQtoSQLClassesDataContext
                run = (From c In .tblRuns
                       Where c.ApplicationID.Equals(appID)
                       Order By c.Rid Descending
                       Select c).FirstOrDefault()
                .Dispose()
            End With
            Return run
        End Function
        Private Shared Function RunIDCreate(appID As System.Guid) As Integer
            Dim tblRunRid As FMS.Business.tblRun = GetLatestRunID(appID)
            Dim objRunID As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objRunID
                    .ProjectID = Guid.NewGuid()
                    .RunID = tblRunRid.Rid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objRunID)
                .SubmitChanges()
                .Dispose()
            End With
            Return objRunID.RunID
        End Function
        Private Shared Function RunIDUpdate(RunID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If RunID Is Nothing Then
                    Dim tblRunId As FMS.Business.tblRun = GetLatestRunID(appID)
                    RunID = tblRunId.Rid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .RunID = RunID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With

            Return objProject.RunID
        End Function
        Public Shared Function RunIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objRun As List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objRun = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objRun Is Nothing AndAlso objRun.Count().Equals(0) Then
                Return RunIDCreate(appID)
            Else
                Return RunIDUpdate(objRun.SingleOrDefault().RunID, appID)
            End If
        End Function
#End Region
#Region "tblSiteComments"
        Private Shared Function GetLatestSiteCommentsID(appID As System.Guid) As FMS.Business.tblSiteComment
            Dim siteComment As FMS.Business.tblSiteComment
            With New LINQtoSQLClassesDataContext
                siteComment = (From c In .tblSiteComments
                               Where c.ApplicationID.Equals(appID)
                               Order By c.Aid Descending
                               Select c).FirstOrDefault()
                .Dispose()
            End With
            Return siteComment
        End Function
        Private Shared Function SiteCommentsIDCreate(appID As System.Guid) As Integer
            Dim tblSiteCommentsId As FMS.Business.tblSiteComment = GetLatestSiteCommentsID(appID)
            Dim objSiteCommentsId As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objSiteCommentsId
                    .ProjectID = Guid.NewGuid()
                    .CommentsID = tblSiteCommentsId.Aid + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objSiteCommentsId)
                .SubmitChanges()
                .Dispose()
            End With
            Return objSiteCommentsId.RunID
        End Function
        Private Shared Function SiteCommentsIDUpdate(SiteCommentsID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If SiteCommentsID Is Nothing Then
                    Dim tblSiteCommentsId As FMS.Business.tblSiteComment = GetLatestSiteCommentsID(appID)
                    SiteCommentsID = tblSiteCommentsId.Aid
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .CommentsID = SiteCommentsID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With
            Return objProject.CommentsID
        End Function
        Public Shared Function SiteCommentsIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objSiteComments As New List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objSiteComments = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objSiteComments Is Nothing AndAlso objSiteComments.Count().Equals(0) Then
                Return SiteCommentsIDCreate(appID)
            Else
                Return SiteCommentsIDUpdate(objSiteComments.SingleOrDefault().CommentsID, appID)
            End If
        End Function
#End Region
#Region "tblCUAScheduleOfRates"
        Private Shared Function GetLatestRatesID(appID As System.Guid) As FMS.Business.tblCUAScheduleOfRate
            Dim CUAScheduleOfRates As FMS.Business.tblCUAScheduleOfRate
            With New LINQtoSQLClassesDataContext
                CUAScheduleOfRates = (From c In .tblCUAScheduleOfRates
                                      Where c.ApplicationID.Equals(appID)
                                      Order By c.RatesAutoId Descending
                                      Select c).FirstOrDefault()
                .Dispose()
            End With
            Return CUAScheduleOfRates
        End Function
        Private Shared Function RatesIDCreate(appID As System.Guid) As Integer
            Dim tblCUAScheduleOfRatesId As FMS.Business.tblCUAScheduleOfRate = GetLatestRatesID(appID)
            Dim objProjectId As New FMS.Business.tblProjectID
            With New LINQtoSQLClassesDataContext
                With objProjectId
                    .ProjectID = Guid.NewGuid()
                    .RatesID = tblCUAScheduleOfRatesId.RatesAutoId + 1
                End With
                .tblProjectIDs.InsertOnSubmit(objProjectId)
                .SubmitChanges()
                .Dispose()
            End With
            Return objProjectId.RatesID
        End Function
        Private Shared Function RatesIDUpdate(RatesID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            With New LINQtoSQLClassesDataContext
                If RatesID Is Nothing Then
                    Dim tblRatesID As FMS.Business.tblCUAScheduleOfRate = GetLatestRatesID(appID)
                    RatesID = tblRatesID.RatesAutoId
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).FirstOrDefault()
                Else
                    objProject = (From c In .tblProjectIDs
                                  Where c.ApplicationID.Equals(appID)).SingleOrDefault
                End If

                With objProject
                    .RatesID = RatesID + 1
                End With
                .SubmitChanges()
                .Dispose()
            End With
            Return objProject.RatesID
        End Function
        Public Shared Function RatesIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objProject As New List(Of FMS.Business.tblProjectID)
            With New LINQtoSQLClassesDataContext
                objProject = .tblProjectIDs.Where(Function(x) x.ApplicationID.Equals(appID)).ToList()
                .Dispose()
            End With
            If Not objProject Is Nothing AndAlso objProject.Count().Equals(0) Then
                Return RatesIDCreate(appID)
            Else
                Return RatesIDUpdate(objProject.SingleOrDefault().RatesID, appID)
            End If
        End Function
#End Region
#Region "tblDrivers"
        Public Shared Function DriverIDCreateOrUpdate(appID As System.Guid) As Integer
            Dim objDriver = SingletonAccess.FMSDataContextContignous.tblProjectIDs.ToList()
            If Not objDriver Is Nothing AndAlso objDriver.Count().Equals(0) Then
                Return DriverIDCreate(appID)
            Else
                Return DriverIDUpdate(objDriver.SingleOrDefault().Did, appID)
            End If
        End Function

        Private Shared Function GetLatestDriverID(appID As System.Guid) As FMS.Business.tblDriver

            Return (From d In SingletonAccess.FMSDataContextContignous.tblDrivers
                    Where d.ApplicationId.Equals(appID)
                    Order By d.Did Descending
                    Select d).FirstOrDefault()
        End Function
        Private Shared Function DriverIDCreate(appID As System.Guid) As Integer
            Dim objDvrId As FMS.Business.tblDriver = GetLatestDriverID(appID)
            Dim objDriverID As New FMS.Business.tblProjectID
            With objDriverID
                .ProjectID = Guid.NewGuid()
                .Did = objDvrId.Did + 1
            End With
            SingletonAccess.FMSDataContextContignous.tblProjectIDs.InsertOnSubmit(objDriverID)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objDriverID.Did
        End Function
        Private Shared Function DriverIDUpdate(DriverID As Object, appID As System.Guid) As Integer
            Dim objProject As FMS.Business.tblProjectID = Nothing
            If DriverID Is Nothing Then
                Dim tblDriverDid As FMS.Business.tblDriver = GetLatestDriverID(appID)
                DriverID = tblDriverDid.Did
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.ApplicationID.Equals(appID)).FirstOrDefault()
            Else
                objProject = (From c In SingletonAccess.FMSDataContextContignous.tblProjectIDs
                              Where c.Did.Equals(DriverID) And c.ApplicationID.Equals(appID)).SingleOrDefault
            End If

            With objProject
                .Did = DriverID + 1
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return objProject.Did
        End Function

#End Region
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
#End Region
    End Class
End Namespace

