Public Class GeoFencePropertyDisplay
    Inherits System.Web.UI.Page

    Public ReadOnly Property ApplicationGeoFenceID As Guid
        Get
            Return New Guid(Request.QueryString("ApplicationGeoFenceID"))
        End Get
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack And Membership.ApplicationName <> "/" Then Exit Sub

        'if not logged in, then redirect to the login page (applies to all pages so placed in the master page)
        If Membership.GetUser Is Nothing Then
            Response.Write("you do not have access to this page")
            Exit Sub
        End If

        'Dim uName = Membership.GetUser.UserName


        'FMS.Business.ThisSession.User = FMS.Business.DataObjects.User.GetAllUsersForApplication _
        '                (FMS.Business.ThisSession.ApplicationID).Where(Function(x) x.UserName = uName).Single

        'SET the userid in the session parameters
        'FMS.Business.ThisSession.UserID = FMS.Business.ThisSession.User.UserId
    End Sub

    Private Sub dgvGeoFenceProperty_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles dgvGeoFenceProperty.RowInserting
        e.NewValues.Add("ApplicationGeoFenceID", ApplicationGeoFenceID)
    End Sub
End Class