Public Class OdometerTester
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        'foundVehicle.GetVehicleMovementSummary(startDate, loopEndDate)

        Dim startTime As Date = dateEditStartTime.Value
        Dim endTime As Date = dateEditEndTime.Value
        Dim deviceid As String = comboDEviceID.Value

        Dim retobjs As List(Of Business.DebugObjects.DeviceEvent) = _
                            Business.DebugObjects.DeviceEvent.GetDistanceVals(startTime, endTime, deviceid)

        Me.ASPxGridView1.DataSourceID = Nothing
        Me.ASPxGridView1.DataSource = retobjs
        Me.ASPxGridView1.DataBind()



    End Sub
End Class