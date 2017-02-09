Public Class ReportParameterTest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Session("ApplicationID") = Guid.Parse("2E3D4974-B634-4F58-AE27-75E73A603B0A")

        nrpTest.ReportParameter = (New ServiceVehicleReport).Parameters(0)


    End Sub

End Class