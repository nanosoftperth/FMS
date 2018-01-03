Public Class TurnOnOffAuditing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Dim oParam = FMS.Business.DataObjects.tblParameters.GetAll().Where(Function(p) p.ParId = "TurnOffAuditing").ToList()

            If (oParam.Count > 0) Then

                If oParam.FirstOrDefault.Field1 = "0" Then
                    cbxOnOff.Value = False
                Else
                    cbxOnOff.Value = True

                End If

            End If

        End If

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim val As String
        Dim message As String = ""


        If cbxOnOff.Checked = False Then
            val = "0"
            message = "Auditing OFF"
        Else
            val = "1"
            message = "Auditing ON"
        End If

        'Dim obj As New List(Of FMS.Business.DataObjects.tblParameters)
        Dim row As New FMS.Business.DataObjects.tblParameters

        row.ParId = "TurnOffAuditing"
        row.Field1 = val

        FMS.Business.DataObjects.tblParameters.Update(row)

        Dim sb As New System.Text.StringBuilder()

        sb.Append("<script type = 'text/javascript'>")
        sb.Append("window.onload=function(){")
        sb.Append("alert('")
        sb.Append(message)
        sb.Append("')};")
        sb.Append("</script>")

        ClientScript.RegisterClientScriptBlock(Me.GetType(), "alert", sb.ToString())

    End Sub
End Class