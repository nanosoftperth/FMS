Public Class Debug
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'of the first time the page is loaded
        If Not IsPostBack Then
            Me.dateStart.Value = Now.timezoneToClient.AddHours(-1) 'New Date(Now.Year, Now.Month, Now.Day)
            Me.dateEnd.Value = Now.timezoneToClient
            Me.ASPxGridView1.DataSourceID = Nothing
        End If

        'below run if first time page is loaded OR if the button is pressed
        Session("asd") = FMS.Business.DataObjects.LogEntry.GetAllBetweenDates( _
                                Me.dateStart.Value, Me.dateEnd.Value, FMS.Business.ThisSession.ApplicationID)

        Me.ASPxGridView1.DataSource = Session("asd")
        ASPxGridView1.DataBind()

        CType(Me.Master, FMS.WEB.MainLightMaster).HeaderText = "Feature Requests"

    End Sub

    Protected Sub ASPxButton1_Click(sender As Object, e As EventArgs) Handles ASPxButton1.Click
        'logic in the page load method
    End Sub


    Private Sub ASPxGridView1_CustomSummaryCalculate1(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs) Handles ASPxGridView1.CustomSummaryCalculate


        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize Then

            Dim list As List(Of Business.DataObjects.LogEntry) = ASPxGridView1.DataSource
            e.TotalValue = String.Format("device count: {0}", (From y In list Select y.DeviceID).Distinct.Count)

        End If

    End Sub
End Class