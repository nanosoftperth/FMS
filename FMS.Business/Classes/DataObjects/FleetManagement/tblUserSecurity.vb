Namespace DataObjects
    Public Class tblUserSecurity

#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property usersecID As System.Guid
        Public Property txtUserName As String
        Public Property Administrator As System.Nullable(Of Boolean)
        Public Property UserPassword As String
        Public Property UserGroup As String
        Public Property lblCustomerDetails As System.Nullable(Of Boolean)
        Public Property lblSites As System.Nullable(Of Boolean)
        Public Property lblReports As System.Nullable(Of Boolean)
        Public Property lblOtherProcesses As System.Nullable(Of Boolean)
        Public Property lblMaintenance As System.Nullable(Of Boolean)
        Public Property cmdServices As System.Nullable(Of Boolean)
        Public Property Toggle41 As System.Nullable(Of Boolean)
        Public Property Toggle42 As System.Nullable(Of Boolean)
        Public Property CmdIndustryGroups As System.Nullable(Of Boolean)
        Public Property CmdInvoicingFrequency As System.Nullable(Of Boolean)
        Public Property CmdPubHolReg As System.Nullable(Of Boolean)
        Public Property Command50 As System.Nullable(Of Boolean)
        Public Property CmdSalesPersons As System.Nullable(Of Boolean)
        Public Property CmdCeaseReasons As System.Nullable(Of Boolean)
        Public Property CmdPreviousSuppliers As System.Nullable(Of Boolean)
        Public Property CmdCIRReasons As System.Nullable(Of Boolean)
        Public Property CmdCycles As System.Nullable(Of Boolean)
        Public Property CmdTurnOffAuditing As System.Nullable(Of Boolean)
        Public Property CmdAuditChangeReason As System.Nullable(Of Boolean)
        Public Property CmdAreas As System.Nullable(Of Boolean)
        Public Property Command55 As System.Nullable(Of Boolean)
        Public Property cmdUserSecurity As System.Nullable(Of Boolean)
        Public Property CmdContractRenewalReport As System.Nullable(Of Boolean)
        Public Property CmdQuickViewBySuburb As System.Nullable(Of Boolean)
        Public Property CmdAuditReport As System.Nullable(Of Boolean)
        Public Property CmdProductsReport As System.Nullable(Of Boolean)
        Public Property CmdRunReport As System.Nullable(Of Boolean)
        Public Property Command13 As System.Nullable(Of Boolean)
        Public Property CmdCustomerSummary As System.Nullable(Of Boolean)
        Public Property CmdSiteReport As System.Nullable(Of Boolean)
        Public Property Command9 As System.Nullable(Of Boolean)
        Public Property Command10 As System.Nullable(Of Boolean)
        Public Property CmdSalesSummaryDislocations As System.Nullable(Of Boolean)
        Public Property CmdDrivesLicenseExpiry As System.Nullable(Of Boolean)
        Public Property Command14 As System.Nullable(Of Boolean)
        Public Property cmdServiceSummary As System.Nullable(Of Boolean)
        Public Property cmdRunValue As System.Nullable(Of Boolean)
        Public Property cmdInvoicing As System.Nullable(Of Boolean)
        Public Property cmdLengthOfService As System.Nullable(Of Boolean)
        Public Property cmdRunValue2 As System.Nullable(Of Boolean)
        Public Property cmdSitesWithNoContract As System.Nullable(Of Boolean)


#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTbl As FMS.Business.tblUserSecurity)
            With objTbl
                Me.ApplicationId = .ApplicationId
                Me.usersecID = .usersecID
                Me.txtUserName = .txtUserName
                Me.Administrator = .Administrator
                Me.UserPassword = .UserPassword
                Me.UserGroup = .UserGroup
                Me.lblCustomerDetails = .lblCustomerDetails
                Me.lblSites = .lblSites
                Me.lblMaintenance = .lblMaintenance
                Me.lblReports = .lblReports
                Me.lblOtherProcesses = .lblOtherProcesses
                Me.cmdServices = .cmdServices
                Me.Toggle41 = .Toggle41
                Me.Toggle42 = .Toggle42
                Me.CmdIndustryGroups = .CmdIndustryGroups
                Me.CmdInvoicingFrequency = .CmdInvoicingFrequency
                Me.CmdPubHolReg = .CmdPubHolReg
                Me.Command50 = .Command50
                Me.CmdSalesPersons = .CmdSalesPersons
                Me.CmdCeaseReasons = .CmdCeaseReasons
                Me.CmdPreviousSuppliers = .CmdPreviousSuppliers
                Me.CmdCycles = .CmdCycles
                Me.CmdTurnOffAuditing = .CmdTurnOffAuditing
                Me.CmdAuditChangeReason = .CmdAuditChangeReason
                Me.CmdAreas = .CmdAreas
                Me.Command55 = .Command55
                Me.CmdContractRenewalReport = .CmdContractRenewalReport
                Me.CmdQuickViewBySuburb = .CmdQuickViewBySuburb
                Me.CmdAuditReport = .CmdAuditReport
                Me.CmdProductsReport = .CmdProductsReport
                Me.CmdRunReport = .CmdRunReport
                Me.Command13 = .Command13
                Me.CmdCustomerSummary = .CmdCustomerSummary
                Me.CmdSiteReport = .CmdSiteReport
                Me.Command9 = .Command9
                Me.Command10 = .Command10
                Me.CmdSalesSummaryDislocations = .CmdSalesSummaryDislocations
                Me.CmdDrivesLicenseExpiry = .CmdDrivesLicenseExpiry
                Me.Command14 = .Command14
                Me.cmdServiceSummary = .cmdServiceSummary
                Me.cmdRunValue = .cmdRunValue
                Me.cmdInvoicing = .cmdInvoicing
                Me.cmdLengthOfService = .cmdLengthOfService
                Me.cmdRunValue2 = .cmdRunValue2
                Me.cmdSitesWithNoContract = .cmdSitesWithNoContract
            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(sec As DataObjects.tblUserSecurity)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID

                Dim obj As New FMS.Business.tblUserSecurity
                With obj
                    .ApplicationId = appID
                    .usersecID = Guid.NewGuid
                    .txtUserName = sec.txtUserName
                    .Administrator = sec.Administrator
                    .UserPassword = sec.UserPassword
                    .UserGroup = sec.UserGroup
                    .lblCustomerDetails = IIf(sec.lblCustomerDetails.HasValue, sec.lblCustomerDetails, False)
                    .lblSites = IIf(sec.lblSites.HasValue, sec.lblSites, False)
                    .lblMaintenance = IIf(sec.lblMaintenance.HasValue, sec.lblMaintenance, False)
                    .lblReports = IIf(sec.lblReports.HasValue, sec.lblReports, False)
                    .lblOtherProcesses = IIf(sec.lblOtherProcesses.HasValue, sec.lblOtherProcesses, False)
                    .cmdServices = IIf(sec.cmdServices.HasValue, sec.cmdServices, False)
                    .Toggle41 = IIf(sec.Toggle41.HasValue, sec.Toggle41, False)
                    .Toggle42 = IIf(sec.Toggle42.HasValue, sec.Toggle42, False)
                    .CmdIndustryGroups = IIf(sec.CmdIndustryGroups.HasValue, sec.CmdIndustryGroups, False)
                    .CmdInvoicingFrequency = IIf(sec.CmdInvoicingFrequency.HasValue, sec.CmdInvoicingFrequency, False)
                    .CmdPubHolReg = IIf(sec.CmdPubHolReg.HasValue, sec.CmdPubHolReg, False)
                    .CmdSalesPersons = IIf(sec.CmdSalesPersons.HasValue, sec.CmdSalesPersons, False)
                    .CmdCeaseReasons = IIf(sec.CmdCeaseReasons.HasValue, sec.CmdCeaseReasons, False)
                    .CmdCIRReasons = IIf(sec.CmdCIRReasons.HasValue, sec.CmdCIRReasons, False)
                    .CmdCycles = IIf(sec.CmdCycles.HasValue, sec.CmdCycles, False)
                    .CmdTurnOffAuditing = IIf(sec.CmdTurnOffAuditing.HasValue, sec.CmdTurnOffAuditing, False)
                    .CmdAuditChangeReason = IIf(sec.CmdAuditChangeReason.HasValue, sec.CmdAuditChangeReason, False)
                    .CmdAreas = IIf(sec.CmdAreas.HasValue, sec.CmdAreas, False)
                    .Command55 = IIf(sec.Command55.HasValue, sec.Command55, False)
                    .CmdContractRenewalReport = IIf(sec.CmdContractRenewalReport.HasValue, sec.CmdContractRenewalReport, False)
                    .CmdQuickViewBySuburb = IIf(sec.CmdQuickViewBySuburb.HasValue, sec.CmdQuickViewBySuburb, False)
                    .CmdAuditReport = IIf(sec.CmdAuditReport.HasValue, sec.CmdAuditReport, False)
                    .CmdProductsReport = IIf(sec.CmdProductsReport.HasValue, sec.CmdProductsReport, False)
                    .CmdRunReport = IIf(sec.CmdRunReport.HasValue, sec.CmdRunReport, False)
                    .Command13 = IIf(sec.Command13.HasValue, sec.Command13, False)
                    .CmdCustomerSummary = IIf(sec.CmdCustomerSummary.HasValue, sec.CmdCustomerSummary, False)
                    .CmdSiteReport = IIf(sec.CmdSiteReport.HasValue, sec.CmdSiteReport, False)
                    .Command9 = IIf(sec.Command9.HasValue, sec.Command9, False)
                    .Command10 = IIf(sec.Command10.HasValue, sec.Command10, False)
                    .CmdSalesSummaryDislocations = IIf(sec.CmdSalesSummaryDislocations.HasValue, sec.CmdSalesSummaryDislocations, False)
                    .CmdDrivesLicenseExpiry = IIf(sec.CmdDrivesLicenseExpiry.HasValue, sec.CmdDrivesLicenseExpiry, False)
                    .Command14 = IIf(sec.Command14.HasValue, sec.Command14, False)
                    .cmdServiceSummary = IIf(sec.cmdServiceSummary.HasValue, sec.cmdServiceSummary, False)
                    .cmdRunValue = IIf(sec.cmdRunValue.HasValue, sec.cmdRunValue, False)
                    .cmdInvoicing = IIf(sec.cmdInvoicing.HasValue, sec.cmdInvoicing, False)
                    .cmdLengthOfService = IIf(sec.cmdLengthOfService.HasValue, sec.cmdLengthOfService, False)
                    .cmdRunValue2 = IIf(sec.cmdRunValue2.HasValue, sec.cmdRunValue2, False)
                    .cmdSitesWithNoContract = IIf(sec.cmdSitesWithNoContract.HasValue, sec.cmdSitesWithNoContract, False)
                End With

                .tblUserSecurities.InsertOnSubmit(obj)
                .SubmitChanges()
                .Dispose()

            End With

        End Sub

        Public Shared Sub Update(sec As DataObjects.tblUserSecurity)
            With New LINQtoSQLClassesDataContext

                Dim appID = ThisSession.ApplicationID

                Dim obj As FMS.Business.tblUserSecurity = (From u In .tblUserSecurities
                                                           Where u.usersecID.Equals(sec.usersecID) And u.ApplicationId = appID).SingleOrDefault
                With obj
                    .txtUserName = sec.txtUserName
                    .Administrator = sec.Administrator
                    .UserPassword = sec.UserPassword
                    .UserGroup = sec.UserGroup
                    .lblCustomerDetails = IIf(sec.lblCustomerDetails.HasValue, sec.lblCustomerDetails, False)
                    .lblSites = IIf(sec.lblSites.HasValue, sec.lblSites, False)
                    .lblMaintenance = IIf(sec.lblMaintenance.HasValue, sec.lblMaintenance, False)
                    .lblReports = IIf(sec.lblReports.HasValue, sec.lblReports, False)
                    .lblOtherProcesses = IIf(sec.lblOtherProcesses.HasValue, sec.lblOtherProcesses, False)
                    .cmdServices = IIf(sec.cmdServices.HasValue, sec.cmdServices, False)
                    .Toggle41 = IIf(sec.Toggle41.HasValue, sec.Toggle41, False)
                    .Toggle42 = IIf(sec.Toggle42.HasValue, sec.Toggle42, False)
                    .CmdIndustryGroups = IIf(sec.CmdIndustryGroups.HasValue, sec.CmdIndustryGroups, False)
                    .CmdInvoicingFrequency = IIf(sec.CmdInvoicingFrequency.HasValue, sec.CmdInvoicingFrequency, False)
                    .CmdPubHolReg = IIf(sec.CmdPubHolReg.HasValue, sec.CmdPubHolReg, False)
                    .CmdSalesPersons = IIf(sec.CmdSalesPersons.HasValue, sec.CmdSalesPersons, False)
                    .CmdCeaseReasons = IIf(sec.CmdCeaseReasons.HasValue, sec.CmdCeaseReasons, False)
                    .CmdCIRReasons = IIf(sec.CmdCIRReasons.HasValue, sec.CmdCIRReasons, False)
                    .CmdCycles = IIf(sec.CmdCycles.HasValue, sec.CmdCycles, False)
                    .CmdTurnOffAuditing = IIf(sec.CmdTurnOffAuditing.HasValue, sec.CmdTurnOffAuditing, False)
                    .CmdAuditChangeReason = IIf(sec.CmdAuditChangeReason.HasValue, sec.CmdAuditChangeReason, False)
                    .CmdAreas = IIf(sec.CmdAreas.HasValue, sec.CmdAreas, False)
                    .Command55 = IIf(sec.Command55.HasValue, sec.Command55, False)
                    .CmdContractRenewalReport = IIf(sec.CmdContractRenewalReport.HasValue, sec.CmdContractRenewalReport, False)
                    .CmdQuickViewBySuburb = IIf(sec.CmdQuickViewBySuburb.HasValue, sec.CmdQuickViewBySuburb, False)
                    .CmdAuditReport = IIf(sec.CmdAuditReport.HasValue, sec.CmdAuditReport, False)
                    .CmdProductsReport = IIf(sec.CmdProductsReport.HasValue, sec.CmdProductsReport, False)
                    .CmdRunReport = IIf(sec.CmdRunReport.HasValue, sec.CmdRunReport, False)
                    .Command13 = IIf(sec.Command13.HasValue, sec.Command13, False)
                    .CmdCustomerSummary = IIf(sec.CmdCustomerSummary.HasValue, sec.CmdCustomerSummary, False)
                    .CmdSiteReport = IIf(sec.CmdSiteReport.HasValue, sec.CmdSiteReport, False)
                    .Command9 = IIf(sec.Command9.HasValue, sec.Command9, False)
                    .Command10 = IIf(sec.Command10.HasValue, sec.Command10, False)
                    .CmdSalesSummaryDislocations = IIf(sec.CmdSalesSummaryDislocations.HasValue, sec.CmdSalesSummaryDislocations, False)
                    .CmdDrivesLicenseExpiry = IIf(sec.CmdDrivesLicenseExpiry.HasValue, sec.CmdDrivesLicenseExpiry, False)
                    .Command14 = IIf(sec.Command14.HasValue, sec.Command14, False)
                    .cmdServiceSummary = IIf(sec.cmdServiceSummary.HasValue, sec.cmdServiceSummary, False)
                    .cmdRunValue = IIf(sec.cmdRunValue.HasValue, sec.cmdRunValue, False)
                    .cmdInvoicing = IIf(sec.cmdInvoicing.HasValue, sec.cmdInvoicing, False)
                    .cmdLengthOfService = IIf(sec.cmdLengthOfService.HasValue, sec.cmdLengthOfService, False)
                    .cmdRunValue2 = IIf(sec.cmdRunValue2.HasValue, sec.cmdRunValue2, False)
                    .cmdSitesWithNoContract = IIf(sec.cmdSitesWithNoContract.HasValue, sec.cmdSitesWithNoContract, False)

                    '.txtUserName = sec.txtUserName
                    '.Administrator = sec.Administrator
                    '.UserPassword = sec.UserPassword
                    '.UserGroup = sec.UserGroup
                    '.lblCustomerDetails = sec.lblCustomerDetails
                    '.lblSites = sec.lblSites
                    '.lblMaintenance = sec.lblMaintenance
                    '.lblReports = sec.lblReports
                    '.lblOtherProcesses = sec.lblOtherProcesses
                    '.cmdServices = sec.cmdServices
                    '.Toggle41 = sec.Toggle41
                    '.Toggle42 = sec.Toggle42
                    '.CmdIndustryGroups = sec.CmdIndustryGroups
                    '.CmdInvoicingFrequency = sec.CmdInvoicingFrequency
                    '.CmdPubHolReg = sec.CmdPubHolReg
                    '.CmdSalesPersons = sec.CmdSalesPersons
                    '.CmdCeaseReasons = sec.CmdCeaseReasons
                    '.CmdCycles = sec.CmdCycles
                    '.CmdTurnOffAuditing = sec.CmdTurnOffAuditing
                    '.CmdAuditChangeReason = sec.CmdAuditChangeReason
                    '.CmdAreas = sec.CmdAreas
                    '.Command55 = sec.Command55
                    '.CmdContractRenewalReport = sec.CmdContractRenewalReport
                    '.CmdQuickViewBySuburb = sec.CmdQuickViewBySuburb
                    '.CmdAuditReport = sec.CmdAuditReport
                    '.CmdProductsReport = sec.CmdProductsReport
                    '.CmdRunReport = sec.CmdRunReport
                    '.Command13 = sec.Command13
                    '.CmdCustomerSummary = sec.CmdCustomerSummary
                    '.CmdSiteReport = sec.CmdSiteReport
                    '.Command9 = sec.Command9
                    '.Command10 = sec.Command10
                    '.CmdSalesSummaryDislocations = .CmdSalesSummaryDislocations
                    '.CmdDrivesLicenseExpiry = .CmdDrivesLicenseExpiry
                    '.Command14 = sec.Command14
                    '.cmdServiceSummary = sec.cmdServiceSummary
                    '.cmdRunValue = sec.cmdRunValue
                    '.cmdInvoicing = sec.cmdInvoicing
                    '.cmdLengthOfService = sec.cmdLengthOfService
                    '.cmdRunValue2 = sec.cmdRunValue2
                    '.cmdSitesWithNoContract = sec.cmdSitesWithNoContract
                End With

                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Delete(sec As DataObjects.tblUserSecurity)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID

                Dim obj As FMS.Business.tblUserSecurity = (From u In .tblUserSecurities
                                                           Where u.usersecID.Equals(sec.usersecID) And u.ApplicationId.Equals(appID)).SingleOrDefault
                .tblUserSecurities.DeleteOnSubmit(obj)
                .SubmitChanges()
                .Dispose()
            End With

        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblUserSecurity)
            Try
                Dim obj As New List(Of DataObjects.tblUserSecurity)

                With New LINQtoSQLClassesDataContext
                    obj = (From u In .tblUserSecurities
                           Order By u.txtUserName
                           Select New DataObjects.tblUserSecurity(u)).ToList
                End With

                Return obj

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllPerUserName(UserName As String) As List(Of DataObjects.tblUserSecurity)
            Try
                Dim obj As New List(Of DataObjects.tblUserSecurity)

                With New LINQtoSQLClassesDataContext

                    obj = (From u In .tblUserSecurities
                           Select New DataObjects.tblUserSecurity(u)).ToList

                End With

                Return obj
            Catch ex As Exception
                Throw ex
            End Try
        End Function




        'Public Shared Function GetAll() As List(Of DataObjects.tblUserSecurity)
        '    Dim obj = (From u In SingletonAccess.FMSDataContextContignous.tblUserSecurities
        '               Order By u.txtUserName
        '               Select New DataObjects.tblUserSecurity(u)).ToList

        '    Return obj
        'End Function
        'Public Shared Function GetAllPerUserName(UserName As String) As List(Of DataObjects.tblUserSecurity)
        '    Dim obj = (From u In SingletonAccess.FMSDataContextContignous.tblUserSecurities
        '               Select New DataObjects.tblUserSecurity(u)).ToList

        '    Return obj
        'End Function



#End Region

    End Class

End Namespace


