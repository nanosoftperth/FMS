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

    Protected Sub btnCloseComments_Click(sender As Object, e As EventArgs)
        pupComment.ShowOnPageLoad = False
    End Sub

    Protected Sub btnAllocate_Click(sender As Object, e As EventArgs)
        pupAllocate.PopupVerticalAlign = DevExpress.Web.PopupVerticalAlign.Middle
        pupAllocate.ShowOnPageLoad = True
    End Sub

    Protected Sub btnCloseAllocate_Click(sender As Object, e As EventArgs)
        pupAllocate.ShowOnPageLoad = False
    End Sub

    Protected Sub btnChangeRun_Click(sender As Object, e As EventArgs)
        Dim intFromDriver = Me.cboDriverFrom.Value
        Dim intToDriver = Me.cboDriverTo.Value

        FMS.Business.DataObjects.tblRuns.ChangeRun(intFromDriver, intToDriver)

        Dim message As String = "Run Changed."
        Dim sb As New System.Text.StringBuilder()

        sb.Append("<script type = 'text/javascript'>")
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")
        sb.Append("</script>")

        ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", sb.ToString())

        pupAllocate.ShowOnPageLoad = False

    End Sub
End Class