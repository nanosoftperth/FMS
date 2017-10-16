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
End Class