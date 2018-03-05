Imports DevExpress.Web
Imports FMS.Business.DataObjects
Public Class RateIncreases
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub RateIncreasesGridView_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
        e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
        GetRateIncreasesUpdatingRowInserting(e, True)
    End Sub

    Protected Sub RateIncreasesGridView_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
        GetRateIncreasesUpdatingRowInserting(e, False)
    End Sub
    Protected Sub GetRateIncreasesUpdatingRowInserting(e As Object, blnInserting As Boolean)
        Dim txtRateIncreaseDescription As ASPxTextBox = TryCast(RateIncreasesGridView.FindEditFormTemplateControl("txtRateIncreaseDescription"), ASPxTextBox)
        Dim chkAnnualIncreaseApplies As ASPxCheckBox = TryCast(RateIncreasesGridView.FindEditFormTemplateControl("chkAnnualIncreaseApplies"), ASPxCheckBox)
        Dim chkAlreadyDoneThisYear As ASPxCheckBox = TryCast(RateIncreasesGridView.FindEditFormTemplateControl("chkAlreadyDoneThisYear"), ASPxCheckBox)
        e.NewValues("RateIncreaseDescription") = txtRateIncreaseDescription.Text
        e.NewValues("AnnualIncreaseApplies") = chkAnnualIncreaseApplies.Checked
        e.NewValues("AlreadyDoneThisYear") = chkAlreadyDoneThisYear.Checked
    End Sub

    Protected Sub ScheduleOfRatesGridView_BeforePerformDataSelect(sender As Object, e As EventArgs)
        FMS.Business.ThisSession.ServiceID = FMS.Business.DataObjects.tblCUAScheduleOfRates.GetServiceIdInt(CType(sender, ASPxGridView).GetMasterRowKeyValue())
    End Sub

#Region "WebMethods"
    <System.Web.Services.WebMethod()>
    Public Shared Function ProcessCuaRateIncrease(ByVal objCuaRateIncrease As RateIncreaseParamenters)
        Dim ReportOnly As Boolean = objCuaRateIncrease.ReportOnly
        Dim Sid As Integer = objCuaRateIncrease.Sid
        Dim IndustryGroupCode As Integer = objCuaRateIncrease.IndustryGroupCode
        Dim objCuaProperties As New CuaRateIncreasProperties

        With objCuaProperties
            .EffectiveDate = objCuaRateIncrease.EffectiveDate
            .ServiceCodeToUpdate = Sid
            .TotCharge = 0
            .TotUnits = 0

            'Delete previous rate increases from reporting table
            'Criteria = "DELETE * FROM tblRateIncreases"
            tblRateIncrease.DeleteAll()

            Dim SitesSqlCriteria = tblSites.GetSitesByIndustryGroupAndCeasedateIsNull(IndustryGroupCode, Sid)

            For Each cSite In SitesSqlCriteria
                .SiteId = cSite.Cid
                .SiteName = cSite.SiteName
                .CustomerId = cSite.Customer
                If Not cSite.Customer Is Nothing And cSite.Customer <> 0 Then
                    UpdateRates(objCuaProperties, ReportOnly)
                    tblSites.UpdateTotalUnitsAndTotalAmount(cSite.Cid, .TotUnits, .TotCharge)
                    .TotUnits = 0
                    .TotCharge = 0
                End If
            Next
            'VarLastCustomer = VarCurrentCustomer
        End With


        'DoCmd.OpenReport "repCUARateIncreases", acViewPreview
        'MsgBox vbCrLf & "Processing of rate increases complete - Thank you and have a nice day" & vbCrLf

        Return Nothing
    End Function
    Private Shared Sub UpdateRates(ByVal objCuaProperties As CuaRateIncreasProperties, reportOnly As Boolean)
        Dim ServiceUnits As Double
        With objCuaProperties
            Dim custServicesCriteria = tblCustomerServices.GetCustomerServicesByCsidAndCid(.ServiceCodeToUpdate, .SiteId)
            For Each custServ In custServicesCriteria
                If custServ.ServiceUnits >= 1 And custServ.ServicePrice >= 1 Then
                    .SiteCSid = custServ.CSid
                    .ServiceUnits = custServ.ServiceUnits
                    .ServiceCode = custServ.CSid
                    If .ServiceCode = .ServiceCodeToUpdate Then
                        GetNewRate(objCuaProperties)
                        .oldserviceprice = custServ.ServicePrice
                        .OldPerAnnumCharge = custServ.PerAnnumCharge
                        .NewPerAnnumCharge = (.NewServicePrice * custServ.ServiceUnits)
                        UpdateReportFile(objCuaProperties)
                        .TotCharge = .TotCharge + .NewServicePrice
                        .TotUnits = .TotUnits + ServiceUnits
                        If reportOnly = False And .NewServicePrice <> 0 Then
                            UpdateAuditReportFile(objCuaProperties)

                            'Update customer service entry with new values
                            tblCustomerServices.UpdateServicePricePerAnnumCharge(custServ.ID, .NewServicePrice, .NewPerAnnumCharge)
                        End If
                    Else
                        .TotCharge = .TotCharge + .oldserviceprice
                        .TotUnits = .TotUnits + .ServiceUnits
                    End If
                End If
            Next
        End With

        'VarLastCustomer = VarCurrentCustomer
    End Sub
    Private Shared Sub GetNewRate(ByVal objCuaProperties As CuaRateIncreasProperties)
        With objCuaProperties
            Dim cuaScheduleOfRates = tblCUAScheduleOfRates.GetCUAScheduleOfRatesByServiceOrderbyFromUnits(.ServiceCodeToUpdate)
            .NewServicePrice = 0
            For Each cuaSchedule In cuaScheduleOfRates
                If .ServiceUnits >= cuaSchedule.FromUnits And .ServiceUnits <= cuaSchedule.ToUnits Then
                    .NewServicePrice = cuaSchedule.UnitPrice
                    Return
                End If
            Next
        End With
    End Sub
    Private Shared Sub UpdateReportFile(ByVal objCuaProperties As CuaRateIncreasProperties)
        Dim rateIncrease As New tblRateIncrease()
        With rateIncrease
            .SiteName = objCuaProperties.SiteName
            .CustomerName = objCuaProperties.CustomerName
            .CSid = objCuaProperties.SiteCSid
            .Units = objCuaProperties.ServiceUnits
            .OldServicePrice = objCuaProperties.oldserviceprice
            .NewServicePrice = objCuaProperties.NewServicePrice
            .OldPerAnnumCharge = objCuaProperties.OldPerAnnumCharge
            .NewPerAnnumCharge = objCuaProperties.NewPerAnnumCharge
        End With
        tblRateIncrease.Create(rateIncrease)
    End Sub
    Private Shared Sub UpdateAuditReportFile(ByVal objCuaProperties As CuaRateIncreasProperties)
        Dim revenueChangeAudit As New tblRevenueChangeAudit()
        With revenueChangeAudit
            .CSid = objCuaProperties.ServiceCode
            .Cid = objCuaProperties.CustomerId
            .Customer = objCuaProperties.CustomerName
            .Site = objCuaProperties.SiteName
            .OldServiceUnits = objCuaProperties.ServiceUnits
            .OldService = objCuaProperties.ServiceCode
            .NewServiceUnits = objCuaProperties.ServiceUnits
            .OldServicePrice = objCuaProperties.oldserviceprice
            .NewServicePrice = objCuaProperties.NewServicePrice
            .User = FMS.Business.ThisSession.User.UserName
            .ChangeDate = Date.Now()
            .ChangeTime = DateTime.Now()
            .ChangeReasonCode = 13
            .OldPerAnnumCharge = objCuaProperties.OldPerAnnumCharge
            .NewPerAnnumCharge = objCuaProperties.NewPerAnnumCharge
            .EffectiveDate = objCuaProperties.EffectiveDate
        End With
        tblRevenueChangeAudit.Create(revenueChangeAudit)
    End Sub
#End Region
#Region "Process Update Rate Increase"
    <System.Web.Services.WebMethod()>
    Public Shared Function ProcessRateIncrease(percentageIncrease As String, reportOnly As String)
        Dim dblPercentage As Double = Convert.ToDouble(percentageIncrease)
        Dim blnReportOnly As Boolean = Convert.ToBoolean(reportOnly)
        Dim objCuaProperties As New CuaRateIncreasProperties

        With objCuaProperties
            .TotCharge = 0
            .TotUnits = 0

            tblRateIncrease.DeleteAll()

            Dim SitesSqlCriteria = tblSites.GetAllSitesSiteCeaseDateIsNull()

            For Each cSite In SitesSqlCriteria
                .SiteId = cSite.Cid
                .SiteName = cSite.SiteName
                .CustomerId = cSite.Customer
                If Not cSite.Customer Is Nothing Then
                    EstablishRateIncreaseWarranted(objCuaProperties, cSite.Customer, cSite.Cid)
                    If .ProcessThisOne Then
                        ProcessUpdateRates(objCuaProperties, dblPercentage, .SiteId, blnReportOnly)
                        tblSites.UpdateTotalUnitsAndTotalAmount(cSite.Cid, .TotUnits, .TotCharge)
                    End If
                End If
                .TotUnits = 0
                .TotCharge = 0
            Next
            'VarLastCustomer = VarCurrentCustomer
        End With

        Return Nothing
    End Function
    Private Shared Sub ProcessUpdateRates(objCuaProperties As CuaRateIncreasProperties, dblPercentage As Double, siteId As Integer, blnReportOnly As Boolean)
        Dim CustomerUpdateRates = tblCustomerServices.GetAllByCid(siteId)
        With objCuaProperties
            For Each cust In CustomerUpdateRates
                If cust.ServiceUnits >= 1 And cust.ServicePrice >= 1 Then
                    Dim custCsid As Integer = 0
                    If cust.CSid IsNot Nothing Then
                        custCsid = cust.CSid
                    End If
                    .SiteCSid = custCsid
                    .ServiceUnits = cust.ServiceUnits
                    .ServiceCode = custCsid
                    .oldserviceprice = cust.ServicePrice
                    Dim dblServicePrice As Double = (cust.ServicePrice * (dblPercentage / 100) + cust.ServicePrice)
                    .NewServicePrice = Math.Round(dblServicePrice, 2)
                    If cust.PerAnnumCharge IsNot Nothing Then
                        .OldPerAnnumCharge = cust.PerAnnumCharge
                    Else
                        .OldPerAnnumCharge = 0
                    End If
                    .NewPerAnnumCharge = (.NewServicePrice * cust.ServiceUnits)
                    .TotCharge = .TotCharge + .NewPerAnnumCharge
                    .TotUnits = .TotUnits + .ServiceUnits
                    UpdateReportFileRates(objCuaProperties)
                    If blnReportOnly = False Then
                        UpdateAuditReportFileRates(objCuaProperties)
                        tblCustomerServices.UpdateServicePricePerAnnumCharge(cust.ID, .NewServicePrice, .NewPerAnnumCharge)
                    End If
                End If
            Next
        End With
    End Sub
    Private Shared Sub EstablishRateIncreaseWarranted(objCuaProperties As CuaRateIncreasProperties, customerId As Integer, siteId As Integer)
        'Process customer details
        With objCuaProperties
            .CustomerAlreadyDone = False
            .CustomerRateIncrease = False
            Dim customers = tblCustomers.GetCustomerByCustomerID(customerId)
            .ProcessThisOne = True

            If customers.Count Then
            Else
                Return
            End If

            For Each cust In customers
                .CustomerName = cust.CustomerName
                .CustomerRateIncrease = cust.cmbRateIncrease
                If Not .CustomerRateIncrease Is Nothing And .CustomerRateIncrease <> 0 Then
                    'CustomerAlreadyDone = DLookup("AlreadyDoneThisYear", "tblRateIncreaseReference", "Aid = " & CustomerRateIncrease)
                    Dim RateIncreateReference = tblRateIncreaseReference.GetRateIncreaseReferenceAlreadyDoneThisYear(.CustomerRateIncrease)
                    .CustomerAlreadyDone = RateIncreateReference.AlreadyDoneThisYear
                    If Not .CustomerAlreadyDone Then
                        'CustomerAnnualIncrease = DLookup("AnnualIncreaseApplies", "tblRateIncreaseReference", "Aid = " & CustomerRateIncrease)
                        .CustomerAnnualIncrease = RateIncreateReference.AnnualIncreaseApplies
                        If Not .CustomerAnnualIncrease Then
                            .ProcessThisOne = False
                            Return
                        End If
                    Else
                        .ProcessThisOne = False
                        Return
                    End If
                End If
            Next

            'Process site details

            .SiteAlreadyDone = False
            .SiteRateIncrease = False

            Dim Sites = tblSites.GetAllBySiteID(siteId)

            .ProcessThisOne = True

            If Sites IsNot Nothing Then
                .SiteRateIncrease = Sites.cmbRateIncrease
                If .SiteRateIncrease IsNot Nothing And .SiteRateIncrease <> 0 Then
                    'SiteAlreadyDone = DLookup("AlreadyDoneThisYear", "tblRateIncreaseReference", "Aid = " & SiteRateIncrease)
                    Dim RateIncreateReferenceForSites = tblRateIncreaseReference.GetRateIncreaseReferenceAlreadyDoneThisYear(.SiteRateIncrease)
                    .SiteAlreadyDone = RateIncreateReferenceForSites.AlreadyDoneThisYear
                    If Not .SiteAlreadyDone Then
                        .SiteAnnualIncrease = RateIncreateReferenceForSites.AnnualIncreaseApplies
                        If Not .SiteAnnualIncrease Then
                            .ProcessThisOne = False
                            Return
                        End If
                    Else
                        .ProcessThisOne = False
                        Return
                    End If
                Else
                    .ProcessThisOne = False
                End If
            End If
        End With
        Return
    End Sub
    Private Shared Sub UpdateReportFileRates(ByVal objCuaProperties As CuaRateIncreasProperties)
        Dim rateIncrease As New tblRateIncrease()
        With rateIncrease
            .SiteName = objCuaProperties.SiteName
            .CustomerName = objCuaProperties.CustomerName
            .CSid = objCuaProperties.SiteCSid
            .OldServicePrice = objCuaProperties.oldserviceprice
            .NewServicePrice = objCuaProperties.NewServicePrice
            .OldPerAnnumCharge = objCuaProperties.OldPerAnnumCharge
            .NewPerAnnumCharge = objCuaProperties.NewPerAnnumCharge
        End With
        tblRateIncrease.Create(rateIncrease)
    End Sub
    Private Shared Sub UpdateAuditReportFileRates(ByVal objCuaProperties As CuaRateIncreasProperties)
        Dim revenueChangeAudit As New tblRevenueChangeAudit()
        With revenueChangeAudit
            .CSid = objCuaProperties.ServiceCode
            .Cid = objCuaProperties.CustomerId
            .Customer = objCuaProperties.CustomerName
            .Site = objCuaProperties.SiteName
            .OldServiceUnits = objCuaProperties.ServiceUnits
            .OldService = objCuaProperties.ServiceCode
            .NewServiceUnits = objCuaProperties.ServiceUnits
            .OldServicePrice = objCuaProperties.oldserviceprice
            .NewServicePrice = objCuaProperties.NewServicePrice
            .User = FMS.Business.ThisSession.User.UserName
            .ChangeDate = Date.Now()
            .ChangeTime = DateTime.Now()
            .ChangeReasonCode = 13
            .OldPerAnnumCharge = objCuaProperties.OldPerAnnumCharge
            .NewPerAnnumCharge = objCuaProperties.NewPerAnnumCharge
            .EffectiveDate = objCuaProperties.EffectiveDate
        End With
        tblRevenueChangeAudit.Create(revenueChangeAudit)
    End Sub
#End Region
    Public Class CuaRateIncreasProperties
        Public Property EffectiveDate As Date
        Public Property NewPerAnnumCharge As Double
        Public Property OldPerAnnumCharge As Double
        Public Property oldserviceprice As Double
        Public Property CustomerName As String
        Public Property ServiceUnits As Integer
        Public Property VarLastCustomer As Integer
        Public Property VarCurrentCustomer As Integer
        Public Property CustomerId As System.Nullable(Of Integer)
        Public Property SiteName As String
        Public Property SiteId As Integer
        Public Property ServiceCode As Integer
        Public Property SiteCSid As Integer
        Public Property ServiceCodeToUpdate As Integer
        Public Property NewServicePrice As Double
        Public Property TotCharge As Double
        Public Property TotUnits As Double
        Public Property ProcessThisOne As Boolean
        Public Property CustomerAlreadyDone As Boolean
        Public Property CustomerAnnualIncrease As Boolean
        Public Property SiteAlreadyDone As Boolean
        Public Property SiteAnnualIncrease As Boolean
        Public Property SiteRateIncrease As Object
        Public Property CustomerRateIncrease As Object
    End Class
    Public Class RateIncreaseParamenters
        Public Property ReportOnly As Boolean
        Public Property Sid As Integer
        Public Property IndustryGroupCode As Integer
        Public Property EffectiveDate As Date
    End Class
End Class
