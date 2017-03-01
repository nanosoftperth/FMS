Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
'Imports DevExpress.Web.ASPxEditors
Imports DevExpress.Web.ASPxCallbackPanel
Imports DevExpress.Web
Imports FMS.Business.DataObjects

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
        'Dim cbp As UserControl = CType(LoadControl("~/CallbackPanel.ascx"), UserControl)
        'cbp.ID = "cpUserControl"
        'Dim button As ASPxButton = New ASPxButton
        'button.ID = "Button1"
        'button.Text = "Click Me"
        'button.AutoPostBack = True
        'cbp.Controls.Add(button)  
    End Sub
    Private Sub dgvReports_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles dgvReports.CustomCallback
        'Dim panel As Object = CType(dgvReports.FindEditRowCellTemplateControl(dgvReports.DataColumns("Schedule"), "callbackREportEdit"), ASPxCallbackPanel)
        'Dim cp As ASPxCallbackPanel = CType(dgvReports.FindEditFormTemplateControl("callbackREportEdit"), ASPxCallbackPanel)
        'Dim ctrl As UserControl = cp.FindControl("NanoReportParamList")
        'lblParameterName()
        'For Each control In cp.Controls
        'Next
        'Dim lbl As ASPxTextBox = CType(ctrl.FindControl("t"), ASPxTextBox)
        'Dim str As String = lbl.Text
        ' For Each item In ctrl.Controls
        ' Dim st = item
        'For Each control As ASPxComboBox In cp.Controls
        '    Dim txt As ASPxComboBox = TryCast(control, ASPxComboBox)
        '    If txt IsNot Nothing Then
        '        Dim ID = DirectCast(txt, ASPxComboBox).ID
        '    End If
        'Next control
        '  Dim ID = DirectCast(st, System.Web.UI.LiteralControl).ID
        '  Next
        'Dim btn As ASPxComboBox = CType(ctrl.FindControl(), ASPxComboBox)
        ' Dim btn As ASPxComboBox = CType(ctrl.FindControl("ctl00_ctl00_MainPane_Content_ASPxRoundPanel1_MainContent_ASPxPageControl1_dgvReports_efnew_callbackREportEdit_NanoReportParamList_412a19314b2d4c39b0df34ed0d398c8d_StartDate_I"), ASPxComboBox)

        'Dim pageControl As ASPxCallbackPanel = CType(dgvReports.FindEditRowCellTemplateControl("callbackREportEdit"), ASPxCallbackPanel)
        'Dim ctrl As Control = ASPxCallbackPanel.FindControl("callbackREportEdit")
        'Dim panel As ASPxCallbackPanel = CType(sender, ASPxCallbackPanel)
        'Dim ctrl As Control = panel.FindControl("callbackREportEdit")
        'ThisSession.SelectedReportName = e.Parameters
        'If e.Parameters = "Oneoff" Then
        '    CType(dgvReports.Columns("ScheduleDate"), GridViewDataColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True
        'ElseIf e.Parameters = "Daily" Then
        '    CType(dgvReports.Columns("ScheduleTime"), GridViewDataColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True
        'ElseIf e.Parameters = "Weekly" Then
        '    CType(dgvReports.Columns("ScheduleTime"), GridViewDataColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True
        '    CType(dgvReports.Columns("DayofWeek"), GridViewDataColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True
        'ElseIf e.Parameters = "Monthly" Then
        '    CType(dgvReports.Columns("ScheduleTime"), GridViewDataColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True
        '    CType(dgvReports.Columns("DayofMonth"), GridViewDataColumn).EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True
        'End If
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
    Protected Sub ReportType_SelectedIndexChanged(sender As Object, e As EventArgs)
        ThisSession.SelectedReportName = "VehicleReport"
    End Sub
    <System.Web.Services.WebMethod()> _
    Public Shared Function setReportParameter(ByVal StartDate As String, ByVal EndDate As String, ByVal Vehicle As String, ByVal StartDateSpecific As String, ByVal EndDateSpecific As String) As String
        ReportParam.StartDate = StartDate
        ReportParam.EndDate = EndDate
        ReportParam.Vehicle = Vehicle 
        ReportParam.StartDateSpecific = StartDateSpecific
        ReportParam.EndDateSpecific = EndDateSpecific
        ReportParam.Driver = Vehicle
        Return ""
    End Function
End Class