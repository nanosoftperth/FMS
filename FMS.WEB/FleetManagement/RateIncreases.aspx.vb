Imports DevExpress.Web

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
            FMS.Business.DataObjects.tblRateIncrease.DeleteAll()

            Dim SitesSqlCriteria = FMS.Business.DataObjects.tblSites.GetSitesByIndustryGroupAndCeasedateIsNull(IndustryGroupCode, Sid)

            For Each cSite In SitesSqlCriteria
                .SiteId = cSite.Cid
                .SiteName = cSite.SiteName
                .CustomerId = cSite.Customer
                If Not cSite.Customer Is Nothing And cSite.Customer <> 0 Then
                    UpdateRates(objCuaProperties, ReportOnly)
                    FMS.Business.DataObjects.tblSites.UpdateTotalUnitsAndTotalAmount(cSite.Cid, .TotUnits, .TotCharge)
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
            Dim custServicesCriteria = FMS.Business.DataObjects.tblCustomerServices.GetCustomerServicesByCsidAndCid(.ServiceCodeToUpdate, .SiteId)
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
                            FMS.Business.DataObjects.tblCustomerServices.UpdateServicePricePerAnnumCharge(custServ.ID, .NewServicePrice, .NewPerAnnumCharge)
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
            Dim cuaScheduleOfRates = FMS.Business.DataObjects.tblCUAScheduleOfRates.GetCUAScheduleOfRatesByServiceOrderbyFromUnits(.ServiceCodeToUpdate)
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
        Dim rateIncrease As New FMS.Business.DataObjects.tblRateIncrease()
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
        FMS.Business.DataObjects.tblRateIncrease.Create(rateIncrease)
    End Sub
    Private Shared Sub UpdateAuditReportFile(ByVal objCuaProperties As CuaRateIncreasProperties)
        Dim revenueChangeAudit As New FMS.Business.DataObjects.tblRevenueChangeAudit()
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
        FMS.Business.DataObjects.tblRevenueChangeAudit.Create(revenueChangeAudit)
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

            'FMS.Business.DataObjects.tblRateIncrease.DeleteAll()

            Dim SitesSqlCriteria = FMS.Business.DataObjects.tblSites.GetAllSitesSiteCeaseDateIsNull()

            For Each cSite In SitesSqlCriteria
                .SiteId = cSite.Cid
                .SiteName = cSite.SiteName
                .CustomerId = cSite.Customer
                If Not cSite.Customer Is Nothing Then
                    EstablishRateIncreaseWarranted(objCuaProperties, cSite.Cid)
                    FMS.Business.DataObjects.tblSites.UpdateTotalUnitsAndTotalAmount(cSite.Cid, .TotUnits, .TotCharge)
                    .TotUnits = 0
                    .TotCharge = 0
                End If
            Next
            'VarLastCustomer = VarCurrentCustomer
        End With

        Return Nothing
    End Function
    Private Shared Sub ProcessUpdateRates()

    End Sub
    Private Shared Sub EstablishRateIncreaseWarranted(objCuaProperties As CuaRateIncreasProperties, customerID As Integer)

        With objCuaProperties
            .CustomerAlreadyDone = False
            .CustomerRateIncrease = False
            Dim customers = FMS.Business.DataObjects.tblCustomers.GetCustomerByCustomerID(customerID)
            .ProcessThisOne = True

            For Each cust In customers
                .CustomerName = cust.CustomerName
                .CustomerRateIncrease = cust.cmbRateIncrease
                If Not .CustomerRateIncrease Is Nothing And .CustomerRateIncrease <> 0 Then
                    'CustomerAlreadyDone = DLookup("AlreadyDoneThisYear", "tblRateIncreaseReference", "Aid = " & CustomerRateIncrease)
                End If
            Next

        End With


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
        Public Property CustomerRateIncrease As Object
    End Class
    Public Class RateIncreaseParamenters
        Public Property ReportOnly As Boolean
        Public Property Sid As Integer
        Public Property IndustryGroupCode As Integer
        Public Property EffectiveDate As Date
    End Class
End Class
