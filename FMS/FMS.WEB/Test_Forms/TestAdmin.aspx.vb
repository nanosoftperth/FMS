Public Class TestAdmin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Me.cbSelectedApplication.DataSource = FMS.Business.DataObjects.Application.GetAllApplications
        'Me.cbFMApplication.DataSource = FMS.Business.DataObjects.Application.GetAllApplications
        'Me.batchComboApplications.DataSource = FMS.Business.DataObjects.Application.GetAllApplications
        Dim appID = FMS.Business.ThisSession.ApplicationID

        gvList.DataSource = FMS.Business.DataObjects.Feature.GetAllFeatures(appID)
        gvList.DataBind()

    End Sub

    Protected Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        Dim f As New FMS.Business.DataObjects.Feature

        f.Description = txtDescription.Text
        f.Name = txtFeaturename.Text


        f.FeatureID = FMS.Business.DataObjects.Feature.InsertNewFeature(f)

        Dim obj As Object = ""

        'MessageBox.Show(String.Format("Succes, new FeatureID: {0}", f.FeatureID))
    End Sub
End Class