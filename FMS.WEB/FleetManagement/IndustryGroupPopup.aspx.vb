Public Class IndustryGroupPopup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Request("aid") Is Nothing AndAlso Not Request("aid").Equals("") Then
            Dim getIndustrySortOrder = FMS.Business.DataObjects.tblIndustryGroups.GetIndustryGroupSortOrder(Request("aid"), FMS.Business.ThisSession.ApplicationID)
            If Not getIndustrySortOrder Is Nothing Then
                IndustryGroupsGridView.StartEdit(getIndustrySortOrder.IndustrySortOrder - 1)
            End If
        End If
    End Sub

    Protected Sub IndustryGroupsGridView_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
        e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
    End Sub

    Protected Sub IndustryGroupsGridView_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
        e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
    End Sub

    Protected Sub IndustryGroupsGridView_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
        e.Keys("ApplicationID") = FMS.Business.ThisSession.ApplicationID
    End Sub
End Class