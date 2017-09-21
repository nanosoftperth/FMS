Imports FMS.WEBAPI
Imports System.Net.Http

Public Class SplunkTest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Unnamed1_Click(sender As Object, e As EventArgs)
        'Dim oSplunkAPI As New WEBAPI.Controllers.SplunkAPIController
        Dim oSplunk As New SplunkController
        Dim instance As New HttpRequestMessage

        Try

            oSplunk.SendTagValues("uniqco")
            'With instance
            '    .Content = 
            'End With



            'Dim ThisDate = DateTime.Parse(txtDate.Text)
            'Dim ThisDevID = txtVehicle.Text
            'oSplunkAPI.SendGeoFenceDeviceCollision(ThisDevID, ThisDate)




        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class