Public Class Debug
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'of the first time the page is loaded
        If Not IsPostBack Then
            Me.dateStart.Value = Now.AddHours(-1) 'New Date(Now.Year, Now.Month, Now.Day)
            Me.dateEnd.Value = Now
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
    'Private Sub ASPxGridView1_OnCustomUnboundColumnData(sender As Object, e As DevExpress.Web.ASPxGridViewColumnDataEventArgs) Handles ASPxGridView1.CustomUnboundColumnData


    '    If e.Column.FieldName = "DeviceID" Then

    '        e.Value = 10
    '    End If


    '    '  if(e.Column.FieldName == "Total") {
    '    '    decimal price = (decimal)e.GetListSourceFieldValue("UnitPrice");
    '    '    int quantity = Convert.ToInt32(e.GetListSourceFieldValue("Quantity"));
    '    '    e.Value = price * quantity;
    '    '}


    'End Sub
     
    Protected Sub ASPxGridView1_CustomUnboundColumnData1(sender As Object, e As DevExpress.Web.ASPxGridViewColumnDataEventArgs)
        If e.Column.FieldName = "DeviceID" Then 
            e.Value = 10
        End If
    End Sub


    Protected Sub ASPxGridView1_CustomSummaryCalculate(sender As Object, e As DevExpress.Data.CustomSummaryEventArgs)

    End Sub
End Class