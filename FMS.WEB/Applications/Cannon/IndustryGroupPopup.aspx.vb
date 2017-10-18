Public Class IndustryGroupPopup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim getIndustrySortOrder = FMS.Business.DataObjects.tblIndustryGroups.GetIndustryGroupSortOrder(Request("aid"))
        IndustryGroupsGridView.StartEdit(getIndustrySortOrder.IndustrySortOrder - 1)
    End Sub

End Class