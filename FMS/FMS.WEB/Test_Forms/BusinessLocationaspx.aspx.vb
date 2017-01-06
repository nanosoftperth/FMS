Public Class BusinessLocationaspx
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ThisSession.ApplicationID = Business.DataObjects.Application.GetAllApplications.Where( _
                                        Function(x) x.ApplicationName.ToLower = "demo").Single.ApplicationID

    End Sub

    Private Sub dgvBusinessLocations_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles dgvBusinessLocations.RowInserting
        e.NewValues("ApplicationID") = ThisSession.ApplicationID
    End Sub

    Private Sub dgvBusinessLocations_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles dgvBusinessLocations.RowUpdating
        e.NewValues("ApplicationID") = ThisSession.ApplicationID
    End Sub
End Class