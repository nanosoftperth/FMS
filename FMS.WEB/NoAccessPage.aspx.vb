Public Class NoAccessPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CType(Me.Master, FMS.WEB.MainLightMaster).HeaderText = "Access Restrictions"

        Me.literalMainMessage.Text = Request.QueryString("additionalMessage")

    End Sub

End Class