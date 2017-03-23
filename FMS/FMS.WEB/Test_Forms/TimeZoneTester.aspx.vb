Public Class TimeZoneTester
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsCallback Then Exit Sub

        CType(Me.Master, FMS.WEB.MainLightMaster).HeaderText = "Time-Zone Test Page"

        dgvTimezoneSettings.DataSourceID = Nothing
        dgvTimezoneSettings.DataSource = FMS.Business.ThisSession.GetTimeZoneValues
        dgvTimezoneSettings.DataBind()

        cboPossibleTimeZones.Value = FMS.Business.ThisSession.ApplicationObject.TimeZone.ID

    End Sub

    Private Sub dgvTimezoneSettings_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles dgvTimezoneSettings.CustomCallback

        Dim timeZoneID As String = e.Parameters

        Dim tz As Business.DataObjects.TimeZone

        If String.IsNullOrEmpty(timeZoneID) Then
            tz = FMS.Business.ThisSession.ApplicationObject.GetTimezoneFromLatLong()
        Else
            tz = FMS.Business.DataObjects.TimeZone.GettimeZoneFromID(timeZoneID)
        End If

        FMS.Business.ThisSession.ApplicationObject.SaveTimeZone(tz)

        dgvTimezoneSettings.DataSourceID = Nothing
        dgvTimezoneSettings.DataSource = FMS.Business.ThisSession.GetTimeZoneValues
        dgvTimezoneSettings.DataBind()

    End Sub
End Class