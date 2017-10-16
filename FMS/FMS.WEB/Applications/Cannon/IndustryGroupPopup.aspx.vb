Public Class IndustryGroupPopup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        IndustryGroupsGridView.StartEdit(30)

    End Sub

End Class