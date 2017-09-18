Imports FMS.WEBAPI

Public Class SplunkTest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Unnamed1_Click(sender As Object, e As EventArgs)
        Dim oSplunkAPI As New FMS.WEBAPI.Controllers.SplunkAPIController

        Try
            Dim ThisDate = DateTime.Parse(txtDate.Text)
            Dim ThisDevID = txtVehicle.Text
            oSplunkAPI.SendGeoFenceDeviceCollision(ThisDevID, ThisDate)


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class