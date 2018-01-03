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
End Class