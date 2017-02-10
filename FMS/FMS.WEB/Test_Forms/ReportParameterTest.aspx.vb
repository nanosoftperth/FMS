Public Class ReportParameterTest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Session("ApplicationID") = Guid.Parse("2E3D4974-B634-4F58-AE27-75E73A603B0A")

        'nrpTest.ReportParameter = (New ServiceVehicleReport).Parameters(0)


        Dim x As New ServiceVehicleReport

        NanoReportParamList.Report = x

        'For Each param In x.Parameters
        '    '
        '    Dim nan As NanoReportParam = LoadControl("~\Controls\NanoReportParam.ascx")


        '    sourceDIV.Controls.Add(New NanoReportParam With {.ReportParameter = param})
        'Next

       

        'sourceDIV.Controls.Add( New )


    End Sub



    Private Sub ReportParameterTest_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete

        'Dim x As New ServiceVehicleReport

        'For Each param In x.Parameters
        '    '
        '    Dim nan As NanoReportParam = LoadControl("~\Controls\NanoReportParam.ascx")

        NanoReportParamList.Report = New ServiceVehicleReport
        '    sourceDIV.Controls.Add(New NanoReportParam With {.ReportParameter = param})
        'Next
    End Sub

    'Private Sub Repeater1_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
    '    CType(e.Item.Controls("NanoReportParam1"), NanoReportParam).ReportParameter = e.Item.DataItem
    'End Sub
End Class