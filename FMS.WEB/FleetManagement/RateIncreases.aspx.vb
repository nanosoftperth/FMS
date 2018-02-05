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
End Class