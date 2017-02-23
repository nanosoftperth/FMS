Imports FMS.Business
Imports FMS.Business.DataObjects.FeatureListConstants
Imports DevExpress.Web

Public Class ReportScheduler
    Inherits System.Web.UI.Page
    Public ReadOnly Property AppVersion As String
        Get
            Return My.Settings.version
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub
    Private Sub dgvReports_CellEditorInitialize(sender As Object, e As DevExpress.Web.ASPxGridViewEditorEventArgs) Handles dgvReports.CellEditorInitialize
        'If e.Column.FieldName = "ScheduleDate" Then
        '    e.Column.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False
        'End If
    End Sub
    Private Sub dgvReports_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles dgvReports.CustomCallback
        If e.Parameters = "Oneoff" Then
            CType(dgvReports.Columns("ScheduleDate"), GridViewDataColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True
        ElseIf e.Parameters = "Daily" Then
            CType(dgvReports.Columns("ScheduleTime"), GridViewDataColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True
        ElseIf e.Parameters = "Weekly" Then
            CType(dgvReports.Columns("ScheduleTime"), GridViewDataColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True
            CType(dgvReports.Columns("DayofWeek"), GridViewDataColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True
        ElseIf e.Parameters = "Monthly" Then
            CType(dgvReports.Columns("ScheduleTime"), GridViewDataColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True
            CType(dgvReports.Columns("DayofMonth"), GridViewDataColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True
        End If
    End Sub
    Private Sub dgvReports_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles dgvReports.RowInserting
        e.NewValues.Add("ApplicationID", ThisSession.ApplicationID)
        e.NewValues.Add("Creator", Membership.GetUser.UserName)

    End Sub
    Private Sub dgvReports_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles dgvReports.RowUpdating
        e.NewValues("ApplicationID") = ThisSession.ApplicationID
    End Sub
    Private Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        'this is a little hacky, update the users timezone every time visiting the page ( and a callback or postback happens)
        'ThisSession.User.UpdatetimeZone()

        'Set the timezone for the business layer to be = to the user (the user timezone is taken from the application
        'timezone if they havent explicitly defined one)
        FMS.Business.SingletonAccess.ClientSelected_TimeZone = If(Not String.IsNullOrEmpty(ThisSession.User.TimeZone.ID), _
                                                                        ThisSession.User.TimeZone, ThisSession.ApplicationObject.TimeZone)
    End Sub
End Class