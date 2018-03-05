Imports DevExpress.Web
Imports FMS.Business.DataObjects.FeatureListConstants
Imports System.Collections
Imports System.Collections.Generic
Imports DevExpress.Web.Data

Public Class UserSecurity
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub cboUserGroup_Init(sender As Object, e As EventArgs)
        Dim Lookup = CType(sender, ASPxComboBox)
        Dim container = CType(Lookup.NamingContainer, GridViewEditItemTemplateContainer)

        If container.Grid.IsNewRowEditing Then
            Return
        End If

        Dim strGroup = CType(container.Grid.GetRowValues(container.VisibleIndex, "UserGroup"), String)

        Lookup.Text = strGroup

    End Sub

    Protected Sub txtPassword_Init(sender As Object, e As EventArgs)
        Dim Lookup = CType(sender, ASPxTextBox)
        Dim container = CType(Lookup.NamingContainer, GridViewEditItemTemplateContainer)

        If container.Grid.IsNewRowEditing Then
            Return
        End If

        Dim strPassword = CType(container.Grid.GetRowValues(container.VisibleIndex, "UserPassword"), String)

        Lookup.Text = strPassword

    End Sub

    Protected Sub Grid_RowInserting(sender As Object, e As ASPxDataInsertingEventArgs)

        Dim colGroup = CType(Grid.Columns("UserGroup"), GridViewDataColumn)
        Dim strGroup = CType(Grid.FindEditRowCellTemplateControl(colGroup, "cboUserGroup"), ASPxComboBox)

        e.NewValues("UserGroup") = strGroup.Value

    End Sub

    Protected Sub Grid_RowUpdating(sender As Object, e As ASPxDataUpdatingEventArgs)
        'Dim strPassword = CType(sender.GetRowValues(sender, "UserPassword"), String)
        'Dim colPassword = CType(Grid.Columns("UserPassword"), GridViewDataColumn)
        'Dim strPassword = CType(Grid.FindEditRowCellTemplateControl(colPassword, "txtPassword"), ASPxTextBox)
        'object keyValue = ASPxGridView1.GetRowValues(ASPxGridView1.EditingRowVisibleIndex, "Oid");
        'Dim colUName = Grid.GetRowValues(Grid.EditingRowVisibleIndex, "UserPassword")
        Dim obj As Object = e.NewValues("txtPassword")

        Dim uname = e.NewValues("txtUserName")
        Dim newNome = e.NewValues("UserPassword")

        Dim colGroup = CType(Grid.Columns("UserGroup"), GridViewDataColumn)
        Dim strGroup = CType(Grid.FindEditRowCellTemplateControl(colGroup, "cboUserGroup"), ASPxComboBox)

        'e.NewValues("UserPassword") = strPassword.Text
        e.NewValues("UserGroup") = strGroup.Value

        '===========================
        'Dim tags = TryCast(lookup.GridView.GetSelectedFieldValues(lookup.KeyFieldName), List(Of Object))

        'Dim tags = TryCast(lookup.GridView.GetSelectedFieldValues(lookup.KeyFieldName), List(Of Object))

        'Dim Lookup = CType(sender, ASPxTextBox)
        'Dim container = CType(Lookup.NamingContainer, GridViewEditItemTemplateContainer)
        'Dim strPassword = CType(container.Grid.GetRowValues(container.VisibleIndex, "UserPassword"), String)

        'e.NewValues("BusinessLocation") = 

    End Sub

    Protected Sub Grid_StartRowEditing(sender As Object, e As ASPxStartRowEditingEventArgs)
        Grid.SettingsText.PopupEditFormCaption = "Edit User"
    End Sub

    Protected Sub Grid_InitNewRow(sender As Object, e As ASPxDataInitNewRowEventArgs)
        Grid.SettingsText.PopupEditFormCaption = "Create New User"
        e.NewValues("Administrator") = False
    End Sub



    'Private Sub dgvVehicles_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs) Handles dgvVehicles.RowUpdating
    '    e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
    '    e.NewValues("BusinessLocation") = GetBusinessLocation()
    'End Sub
End Class