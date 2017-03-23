
Imports FMS.Business.DataObjects.FeatureListConstants
Public Class Contacts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CType(Me.Master, FMS.WEB.MainLightMaster).HeaderText = "Contacts"

        Me.UserAuthorisationCheck(FeatureListAccess.Contact_Management__View)
    End Sub




#Region "dgvContacts event handlers"

    Private Sub dgvConteact_CustomErrorText(sender As Object, e As DevExpress.Web.ASPxGridViewCustomErrorTextEventArgs) Handles dgvConteact.CustomErrorText
        e.ErrorText = e.Exception.Message
    End Sub

    Private Sub dgvConteact_RowDeleting(sender As Object, e As DevExpress.Web.Data.ASPxDataDeletingEventArgs) Handles dgvConteact.RowDeleting
        If Not FMS.Business.ThisSession.User.GetIfAccessToFeature(FeatureListAccess.Contact_Management__Edit) Then
            Throw New Exception("You do not have ""edit"" access to this page.")
            e.Cancel = True
        End If
    End Sub

    Private Sub dgvConteact_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles dgvConteact.RowInserting

        If Not FMS.Business.ThisSession.User.GetIfAccessToFeature(FeatureListAccess.Contact_Management__Edit) Then
            Throw New Exception("You do not have ""edit"" access to this page.")
            e.Cancel = True
        End If

        e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
    End Sub

    Private Sub dgvConteact_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles dgvConteact.RowUpdating

        If Not FMS.Business.ThisSession.User.GetIfAccessToFeature(FeatureListAccess.Contact_Management__Edit) Then
            Throw New Exception("You do not have ""edit"" access to this page.")
            e.Cancel = True
        End If

        e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
    End Sub

#End Region

End Class