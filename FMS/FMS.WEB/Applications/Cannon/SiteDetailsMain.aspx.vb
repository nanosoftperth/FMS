Imports DevExpress.Web
Public Class SiteDetailsMain
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Protected Sub SiteDetailsGridView_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs)
        'GetCustomersRowUpdatingRowInserting(e, False)
    End Sub

    Protected Sub SiteDetailsGridView_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs)
        'GetCustomersRowUpdatingRowInserting(e, True)
    End Sub

    Protected Sub ResignHistoryGridView_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs)
        Dim hdnSiteCid As ASPxTextBox = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("hdnSiteCid"), ASPxTextBox)
        e.NewValues("SiteCId") = hdnSiteCid.Text
    End Sub

    Protected Sub ResignHistoryGridView_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs)
        Dim hdnSiteCid As ASPxTextBox = TryCast(SiteDetailsGridView.FindEditFormTemplateControl("hdnSiteCid"), ASPxTextBox)
        e.NewValues("SiteCId") = hdnSiteCid.Text
    End Sub
End Class