Public Class test3
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        If Not IsPostBack Then

            With Now
                Me.dateEditStartTime.Value = New Date(.Year, .Month, .Day, 0, 0, 0)
                Me.dateEditEndtime.Value = Now
            End With


            CType(Me.Master, FMS.WEB.MainLightMaster).HeaderText = "Report Generation"

        End If



    End Sub

    Protected Sub ASPxButton1_Click(sender As Object, e As EventArgs) Handles ASPxButton1.Click
        dgvGeoFences.DataBind()
    End Sub
End Class