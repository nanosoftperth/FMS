Imports FMS.WEBAPI
Imports System.Net.Http

Public Class SplunkTest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Unnamed1_Click(sender As Object, e As EventArgs)
        Dim oSplunkAPI As New WEBAPI.Controllers.SplunkAPIController
        Dim oSplunk As New SplunkController
        Dim instance As New HttpRequestMessage

        Try

            'oSplunk.SendTagValues("uniqco")
            'With instance
            '    .Content = 
            'End With



            Dim StartDate = DateTime.Parse(txtStartDate.Text)
            Dim Enddate = DateTime.Parse(txtEndDate.Text)
            Dim ThisDevID = txtVehicle.Text

            'oSplunk.SendTagValues(ThisDate, )
            'oSplunkAPI.SendGeoFenceDeviceCollision(ThisDevID, ThisDate)
            oSplunk.SendTagValues("demo", StartDate, Enddate)




        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub TestGetall_Click(sender As Object, e As EventArgs)

        Dim appid = FMS.Business.ThisSession.ApplicationID
        Dim oAppVehicle = FMS.Business.DataObjects.ApplicationVehicle.GetAll_Draft1(appid)


        Dim obj As Object = ""
    End Sub
End Class