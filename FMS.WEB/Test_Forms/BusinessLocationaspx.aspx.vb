Public Class BusinessLocationaspx
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ThisSession.ApplicationID = Business.DataObjects.Application.GetAllApplications.Where( _
                                        Function(x) x.ApplicationName.ToLower = "demo").Single.ApplicationID

    End Sub

End Class