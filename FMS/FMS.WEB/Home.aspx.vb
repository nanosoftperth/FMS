Imports FMS.Business.DataObjects.FeatureListConstants

Public Class Home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserAuthorisationCheck(FeatureListAccess.Home_Page)
    End Sub

End Class