Public Class ProblemPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        CType(Me.Master, FMS.WEB.MainLightMaster).HeaderText = "Oops, there was a problem"

        lblProblemMessage.Text = FMS.Business.ThisSession.ProblemPageMessage

    End Sub

End Class