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
    Public Shared Function ProcessCuaRateIncrease(ByVal param1 As String)
        UpdateRates()
        GetNewRate()
        UpdateReportFile()
        UpdateAuditReportFile()
        Return Nothing
    End Function
    Private Shared Sub UpdateRates()
    End Sub
    Private Shared Sub GetNewRate()
    End Sub
    Private Shared Sub UpdateReportFile()
    End Sub
    Private Shared Sub UpdateAuditReportFile()
    End Sub
#End Region
End Class