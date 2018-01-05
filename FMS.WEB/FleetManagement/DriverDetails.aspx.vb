Imports DevExpress.Web

Public Class DriverDetails
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnComment_Click(sender As Object, e As EventArgs)
        pupComment.PopupVerticalAlign = DevExpress.Web.PopupVerticalAlign.Middle
        pupComment.ShowOnPageLoad = True
    End Sub

    Protected Sub gvDriver_HtmlRowPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableRowEventArgs)
        If (e.RowType = GridViewRowType.Data) Then

        End If
    End Sub
End Class