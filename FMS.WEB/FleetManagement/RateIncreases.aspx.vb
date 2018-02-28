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
        EffectiveDate = objCuaRateIncrease.EffectiveDate
        ServiceCodeToUpdate = Sid
        TotCharge = 0
        TotUnits = 0

        'Delete previous rate increases from reporting table
        'Criteria = "DELETE * FROM tblRateIncreases"
        'FMS.Business.DataObjects.tblRateIncrease.DeleteAll()

        Dim SitesSqlCriteria = FMS.Business.DataObjects.tblSites.GetSitesByIndustryGroupAndCeasedateIsNull(IndustryGroupCode)

        For Each cSite In SitesSqlCriteria
            SiteId = cSite.Cid
            SiteName = cSite.SiteName
            CustomerId = cSite.Customer
            If Not cSite.Customer Is Nothing And cSite.Customer <> 0 Then
                UpdateRates(Sid, cSite.Cid, ReportOnly)
                'FMS.Business.DataObjects.tblSites.UpdateTotalUnitsAndTotalAmount(cSite.Cid, TotUnits, TotCharge)
                TotUnits = 0
                TotCharge = 0
            End If
        Next
        varLastCustomer = VarCurrentCustomer

        'DoCmd.OpenReport "repCUARateIncreases", acViewPreview
        'MsgBox vbCrLf & "Processing of rate increases complete - Thank you and have a nice day" & vbCrLf

        'GetNewRate()
        'UpdateReportFile()
        'UpdateAuditReportFile()
        Return Nothing
    End Function
    Private Shared Sub UpdateRates(csid As Integer, cid As Integer, reportOnly As Boolean)
        Dim ServiceUnits As Double
        Dim custServicesCriteria = FMS.Business.DataObjects.tblCustomerServices.GetCustomerServicesByCsidAndCid(csid, cid)
        For Each custServ In custServicesCriteria
            If custServ.ServiceUnits >= 1 And custServ.ServicePrice >= 1 Then
                SiteCSid = custServ.CSid
                ServiceUnits = custServ.ServiceUnits
                ServiceCode = custServ.CSid
                If ServiceCode = ServiceCodeToUpdate Then
                    'GetNewRate(ServiceCodeToUpdate)
                    oldserviceprice = custServ.ServicePrice
                    OldPerAnnumCharge = custServ.PerAnnumCharge
                    NewPerAnnumCharge = (NewServicePrice * custServ.ServiceUnits)
                    'UpdateReportFile()
                    TotCharge = TotCharge + NewServicePrice
                    TotUnits = TotUnits + ServiceUnits
                    If reportOnly = False And NewServicePrice <> 0 Then
                        'UpdateAuditReportFile()

                        'Update customer service entry with new values
                        'FMS.Business.DataObjects.tblCustomerServices.UpdateServicePricePerAnnumCharge(custServ.ID, NewServicePrice, NewPerAnnumCharge)
                    End If
                Else
                    TotCharge = TotCharge + oldserviceprice
                    TotUnits = TotUnits + ServiceUnits
                End If
            End If
        Next
        varLastCustomer = varCurrentCustomer
    End Sub
    Private Shared Sub GetNewRate(servCode As Integer)
        Dim cuaScheduleOfRates = FMS.Business.DataObjects.tblCUAScheduleOfRates.GetCUAScheduleOfRatesByServiceOrderbyFromUnits(servCode)
        NewServicePrice = 0
        For Each cuaSchedule In cuaScheduleOfRates
            If ServiceUnits >= cuaSchedule.FromUnits And ServiceUnits <= cuaSchedule.ToUnits Then
                NewServicePrice = cuaSchedule.UnitPrice
                Return
            End If
        Next
    End Sub
    Private Shared Sub UpdateReportFile()
        Dim rateIncrease As New FMS.Business.DataObjects.tblRateIncrease()
        With rateIncrease
            .SiteName = SiteName
            .CustomerName = CustomerName
            .CSid = SiteCSid
            .Units = ServiceUnits
            .OldServicePrice = oldserviceprice
            .NewServicePrice = NewServicePrice
            .OldPerAnnumCharge = OldPerAnnumCharge
            .NewPerAnnumCharge = NewPerAnnumCharge
        End With
        FMS.Business.DataObjects.tblRateIncrease.Create(RateIncrease)
    End Sub
    Private Shared Sub UpdateAuditReportFile()
        Dim revenueChangeAudit As New FMS.Business.DataObjects.tblRevenueChangeAudit()
        With revenueChangeAudit
            .CSid = ServiceCode
            .Cid = CustomerId
            .Customer = CustomerName
            .Site = SiteName
            .OldServiceUnits = ServiceUnits
            .OldService = ServiceCode
            .NewServiceUnits = ServiceUnits
            .OldServicePrice = oldserviceprice
            .NewServicePrice = NewServicePrice
            .User = FMS.Business.ThisSession.User.UserName 'Forms!frmmain!txtUserName
            .ChangeDate = Date.Now()
            .ChangeTime = DateTime.Now()
            .ChangeReasonCode = 13
            .OldPerAnnumCharge = OldPerAnnumCharge
            .NewPerAnnumCharge = NewPerAnnumCharge
            .EffectiveDate = EffectiveDate
        End With
        FMS.Business.DataObjects.tblRevenueChangeAudit.Create(revenueChangeAudit)
    End Sub

#Region "Properties"
    Public Shared Property EffectiveDate As Date
    Public Shared Property NewPerAnnumCharge As Double
    Public Shared Property OldPerAnnumCharge As Double
    Public Shared Property oldserviceprice As Double
    Public Shared Property CustomerName As String
    Public Shared Property ServiceUnits As Integer
    Public Shared Property VarLastCustomer As Integer
    Public Shared Property VarCurrentCustomer As Integer
    Public Shared Property CustomerId As System.Nullable(Of Integer)
    Private Shared Property SiteName As String
    Private Shared Property SiteId As Integer
    Private Shared Property ServiceCode As Integer
    Private Shared Property SiteCSid As Integer
    Private Shared Property ServiceCodeToUpdate As Integer
    Private Shared Property NewServicePrice As Double
    Private Shared Property TotCharge As Double
    Private Shared Property TotUnits As Double
#End Region
#End Region
    Public Class RateIncreaseParamenters
        Public Property ReportOnly As Boolean
        Public Property Sid As Integer
        Public Property IndustryGroupCode As Integer
        Public Property EffectiveDate As Date
    End Class
End Class
