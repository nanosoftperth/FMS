Imports DevExpress.Web

Public Class OdometerTest
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CType(Me.Master, FMS.WEB.MainLightMaster).HeaderText = "Odometer Test Page"
    End Sub

    Public Sub dgvDetailOdometerReadings_BeforePerformDataSelect(sender As Object, e As System.EventArgs)
        FMS.Business.ThisSession.ApplicationVehicleID = CType(sender, ASPxGridView).GetMasterRowKeyValue()
    End Sub

    Private Sub odsOdometerReadings_Deleting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsOdometerReadings.Deleting
        CType(e.InputParameters(0), FMS.Business.DataObjects.ApplicationVehicleOdometerReading).ApplicationVehicleID = FMS.Business.ThisSession.ApplicationVehicleID
    End Sub

    Private Sub odsOdometerReadings_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsOdometerReadings.Inserting
        CType(e.InputParameters(0), FMS.Business.DataObjects.ApplicationVehicleOdometerReading).ApplicationVehicleID = FMS.Business.ThisSession.ApplicationVehicleID
    End Sub

    Private Sub odsOdometerReadings_Updating(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsOdometerReadings.Updating
        CType(e.InputParameters(0), FMS.Business.DataObjects.ApplicationVehicleOdometerReading).ApplicationVehicleID = FMS.Business.ThisSession.ApplicationVehicleID
    End Sub
End Class