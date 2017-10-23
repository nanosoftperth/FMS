Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim x As New Uniqco_Integrator
        Dim vins As List(Of String) = {"80379", "80507"}.ToList ', "limoVIN"

        Dim username As String = "webServiceAccount"
        Dim password = "asid7@#*du@"

        'Dim y = x.CreateToken(username, password, Now.AddMonths(10))

        Dim sd As Date = CDate("20 oct 2017")

        With Now
            Dim ed As Date = New Date(.Year, .Month, .Day) 'CDate("8 jun 2016")
        End With


        Dim vinobjs As List(Of WebServices.VINNumberRequest) = _
                (From s As String In vins _
                 Select New WebServices.VINNumberRequest _
                 With {.VINNumber = s, .StartDate = Nothing, .EndDate = Nothing}).ToList

        Dim asdasd = x.GetVehicleData(vinobjs, username, password)

    End Sub

End Class