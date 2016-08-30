Public Class debugger
    Inherits System.Web.UI.Page



    Public Function GetVals() As List(Of FMS.Business.DataObjects.LogEntry)

        If Me.comboApplications Is Nothing Then Return Nothing

        Dim appid As Guid = Me.comboApplications.SelectedItem.Value
        'Dim appid As Guid = comboa

        Return FMS.Business.DataObjects.LogEntry.GetAllBetweenDates(Now.AddMinutes(-10), Now, appid).OrderBy(Function(x) x.Time)

    End Function


    Private Const SESSION_VAR_appid = "adsi8asdia32h"

    Protected Sub ASPxButton1_Click(sender As Object, e As EventArgs) Handles ASPxButton1.Click

        Dim appid As Guid = Me.comboApplications.SelectedItem.Value

        ASPxGridView1.DataSourceID = Nothing
        ASPxGridView1.DataSource = FMS.Business.DataObjects.LogEntry.GetAllBetweenDates(Now.AddMinutes(-10), Now, appid).OrderByDescending(Function(x) x.Time)
        ASPxGridView1.DataBind()

        Session(SESSION_VAR_appid) = appid
    End Sub

    Private Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        ' FMS.Business.SingletonAccess.ClientSelected_TimeZone = (From x In FMS.Business.DataObjects.TimeZone.GetMSoftTZones).ToList.First
    End Sub


    Private Sub ASPxGridView1_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles ASPxGridView1.CustomCallback


        If Session(SESSION_VAR_appid) Is Nothing Then Exit Sub

        Dim appid As Guid = Session(SESSION_VAR_appid)

        ASPxGridView1.DataSource = FMS.Business.DataObjects.LogEntry.GetAllBetweenDates(Now.AddMinutes(-10), Now, appid).OrderByDescending(Function(x) x.Time)
        ASPxGridView1.DataBind()

    End Sub


End Class