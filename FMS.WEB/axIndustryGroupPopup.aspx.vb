Public Class axIndustryGroupPopup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Request("aid") Is Nothing AndAlso Not Request("aid").Equals("") Then
            Dim getIndustrySortOrder = FMS.Business.DataObjects.tblIndustryGroups.GetIndustryGroupSortOrder(Request("aid"))
            IndustryGroupsGridView.StartEdit(getIndustrySortOrder.IndustrySortOrder - 1)
        End If
    End Sub

End Class