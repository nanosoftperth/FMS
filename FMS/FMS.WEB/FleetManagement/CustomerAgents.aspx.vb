Public Class CustomerAgents
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub CustomerAgentsGridView_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
        e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
    End Sub

    Protected Sub CustomerAgentsGridView_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
    End Sub

    Protected Sub CustomerAgentsGridView_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
        e.Keys("ApplicationID") = FMS.Business.ThisSession.ApplicationID
    End Sub
End Class