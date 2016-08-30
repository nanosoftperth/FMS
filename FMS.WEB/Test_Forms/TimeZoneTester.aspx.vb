Public Class TimeZoneTester
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsCallback Then Exit Sub

        CType(Me.Master, FMS.WEB.MainLightMaster).HeaderText = "Time-Zone Test Page"

        dgvTimezoneSettings.DataSourceID = Nothing
        dgvTimezoneSettings.DataSource = ThisSession.GetTimeZoneValues
        dgvTimezoneSettings.DataBind()

        cboPossibleTimeZones.Value = ThisSession.ApplicationObject.TimeZone.ID

    End Sub

    Private Sub dgvTimezoneSettings_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles dgvTimezoneSettings.CustomCallback

        Dim timeZoneID As String = e.Parameters

        Dim tz As Business.DataObjects.TimeZone

        If String.IsNullOrEmpty(timeZoneID) Then
            tz = ThisSession.ApplicationObject.GetTimezoneFromLatLong()
        Else
            tz = FMS.Business.DataObjects.TimeZone.GettimeZoneFromID(timeZoneID)
        End If

        ThisSession.ApplicationObject.SaveTimeZone(tz)

        dgvTimezoneSettings.DataSourceID = Nothing
        dgvTimezoneSettings.DataSource = ThisSession.GetTimeZoneValues
        dgvTimezoneSettings.DataBind()

    End Sub
End Class