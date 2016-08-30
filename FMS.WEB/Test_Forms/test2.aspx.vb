Imports DevExpress.XtraCharts.Web
Imports DevExpress.XtraCharts

Public Class test2
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' ThisSession.ApplicationID = New Guid("9B8CC16F-B045-42F8-A53E-1FAFB955232F")

        If Not IsPostBack Then
            deTimeFrom.Value = New Date(Now.Year, Now.Month, Now.Day)
            deTimeTo.Value = New Date(Now.Year, Now.Month, Now.Day).AddDays(1).AddSeconds(-1)

            ' Me.chartSpeed.DataSource = Nothing
            ' Me.chartSpeed.DataSourceID = Nothing

        End If


    End Sub
  
End Class