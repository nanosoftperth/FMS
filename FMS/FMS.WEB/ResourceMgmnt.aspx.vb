Imports DevExpress.Web
Imports FMS.Business.DataObjects.FeatureListConstants

Public Class ResourceMgmnt
    Inherits System.Web.UI.Page


#Region "odometer information"


    Public Sub dgvDetailOdometerReadings_BeforePerformDataSelect(sender As Object, e As System.EventArgs)
        ThisSession.ApplicationVehicleID = CType(sender, ASPxGridView).GetMasterRowKeyValue()
    End Sub

    Private Sub odsOdometerReadings_Deleting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsOdometerReadings.Deleting
        CType(e.InputParameters(0), FMS.Business.DataObjects.ApplicationVehicleOdometerReading).ApplicationVehicleID = ThisSession.ApplicationVehicleID
    End Sub

    Private Sub odsOdometerReadings_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsOdometerReadings.Inserting
        CType(e.InputParameters(0), FMS.Business.DataObjects.ApplicationVehicleOdometerReading).ApplicationVehicleID = ThisSession.ApplicationVehicleID
    End Sub

    Private Sub odsOdometerReadings_Updating(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsOdometerReadings.Updating
        CType(e.InputParameters(0), FMS.Business.DataObjects.ApplicationVehicleOdometerReading).ApplicationVehicleID = ThisSession.ApplicationVehicleID
    End Sub

#End Region

    Private Sub ASPxGridView2_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles ASPxGridView2.RowInserting
        e.NewValues("ApplicationID") = ThisSession.ApplicationID
    End Sub

    Private Sub ASPxGridView2_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles ASPxGridView2.RowUpdating
        e.NewValues("ApplicationID") = ThisSession.ApplicationID
    End Sub
    Private Sub dgvVehicles_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs) Handles dgvVehicles.RowInserting
        e.NewValues("ApplicationID") = ThisSession.ApplicationID
    End Sub

    Private Sub dgvVehicles_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs) Handles dgvVehicles.RowUpdating
        e.NewValues("ApplicationID") = ThisSession.ApplicationID
    End Sub


    Protected Sub hyperLinkNew_Init(sender As Object, e As EventArgs)
        Dim hyperLink As ASPxHyperLink = TryCast(sender, ASPxHyperLink)
        Dim container As GridViewDataRowTemplateContainer = TryCast(hyperLink.NamingContainer, GridViewDataRowTemplateContainer)

        hyperLink.NavigateUrl = [String].Format("javascript:{0}.AddNewRow()", container.Grid.ClientInstanceName)
    End Sub
    Protected Sub hyperLinkEdit_Init(sender As Object, e As EventArgs)
        Dim hyperLink As ASPxHyperLink = TryCast(sender, ASPxHyperLink)
        Dim container As GridViewDataRowTemplateContainer = TryCast(hyperLink.NamingContainer, GridViewDataRowTemplateContainer)

        hyperLink.NavigateUrl = [String].Format("javascript:{0}.StartEditRow({1})", container.Grid.ClientInstanceName, container.VisibleIndex)
    End Sub
    Protected Sub hyperLinkDelete_Init(sender As Object, e As EventArgs)
        Dim hyperLink As ASPxHyperLink = TryCast(sender, ASPxHyperLink)
        Dim container As GridViewDataRowTemplateContainer = TryCast(hyperLink.NamingContainer, GridViewDataRowTemplateContainer)

        hyperLink.NavigateUrl = [String].Format("javascript:{0}.DeleteRow({1})", container.Grid.ClientInstanceName, container.VisibleIndex)
    End Sub


    Private Sub ResourceMgmnt_Load(sender As Object, e As EventArgs) Handles Me.Load

        CType(Me.Master, FMS.WEB.MainLightMaster).HeaderText = "Resource management"

        UserAuthorisationCheck(FeatureListAccess.Vehicle_and_Driver_Management__Read_Only)

        If IsPostBack Then Exit Sub

        Dim currentDate As Date = Now.timezoneToClient

        Me.dateEditDay.Date = New Date(currentDate.Year, currentDate.Month, currentDate.Day)
        Me.timeEditFrom.DateTime = Me.dateEditDay.Date.AddHours(5)
        Me.timeEditTo.DateTime = Me.dateEditDay.Date.AddHours(23).AddMinutes(59).AddSeconds(59)

        Dim retlst As List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime) = _
                                    FMS.Business.DataObjects.ApplicationVehicleDriverTime. _
                                            GetAllForApplicationAndDatePeriodIncludingDuds(ThisSession.ApplicationID, Me.dateEditDay.Date, Me.timeEditTo.DateTime)

    End Sub


    Public Shared Function GetApplicationVehicleDriverTimes(startdate As Date, enddate As Date) As List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime)


        If ThisSession.rm_ApplicationDriverVehicleTimes Is Nothing Or ThisSession.rm_DriverVehicleTimeReload Then

            Dim retlst As List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime) = _
                               FMS.Business.DataObjects.ApplicationVehicleDriverTime. _
                                    GetAllForApplicationAndDatePeriodIncludingDuds(ThisSession.ApplicationID, startdate, enddate)

            ThisSession.rm_ApplicationDriverVehicleTimes = retlst

            ThisSession.rm_DriverVehicleTimeReload = False

        End If

        Return ThisSession.rm_ApplicationDriverVehicleTimes

    End Function

    Private Sub dgvApplicationVehicleDriver_BatchUpdate(sender As Object,
                                                        e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs) _
                                                                            Handles dgvApplicationVehicleDriver.BatchUpdate

        'DELETE ANYTHING MARKED FOR DELETION
        For Each o As DevExpress.Web.Data.ASPxDataDeleteValues In e.DeleteValues

            Dim ApplicationVehicleDriverTimeID As Guid = o.Keys("ApplicationVehicleDriverTimeID")

            'Dim VehicleID As Guid = o.Keys("VehicleID")
            ' Dim ApplicationDriverId As Guid = o.Keys("ApplicationDriverId")

            Dim obj As New FMS.Business.DataObjects.ApplicationVehicleDriverTime With {.ApplicationVehicleDriverTimeID = ApplicationVehicleDriverTimeID}
            FMS.Business.DataObjects.ApplicationVehicleDriverTime.Delete(obj)
        Next

        'INSERT THE NEW VALUES
        For Each o As DevExpress.Web.Data.ASPxDataInsertValues In e.InsertValues

            Dim startdate As Date = o.NewValues("StartDate")
            Dim EndDate As Date = o.NewValues("EndDate")
            Dim VehicleID As Guid = o.NewValues("VehicleID")
            Dim ApplicationDriverID As Guid = o.NewValues("ApplicationDriverId")
            Dim PassengerID As Guid = o.NewValues("PassengerID")


            Dim driverVehicleTime As New FMS.Business.DataObjects.ApplicationVehicleDriverTime

            With driverVehicleTime
                .StartDate = startdate
                .EndDate = EndDate
                .VehicleID = VehicleID
                .ApplicationDriverId = ApplicationDriverID
                .PassengerID = PassengerID
                .ApplicationID = ThisSession.ApplicationID
            End With

            FMS.Business.DataObjects.ApplicationVehicleDriverTime.Insert(driverVehicleTime)

        Next

        'UPDATE AND CHANGED VALUES
        For Each o As DevExpress.Web.Data.ASPxDataUpdateValues In e.UpdateValues

            Dim startdate As Date = o.NewValues("StartDate")
            Dim EndDate As Date = o.NewValues("EndDate")
            Dim VehicleID As Guid = o.NewValues("VehicleID")
            Dim ApplicationDriverID As Guid = o.NewValues("ApplicationDriverId")
            Dim ApplicationVehicleDriverTimeID As Guid = o.Keys("ApplicationVehicleDriverTimeID")
            Dim passengerID As Guid = o.NewValues("PassengerID")

            Dim driverVehicleTime As New FMS.Business.DataObjects.ApplicationVehicleDriverTime

            With driverVehicleTime
                .StartDate = startdate
                .EndDate = EndDate
                .VehicleID = VehicleID
                If ApplicationDriverID <> Guid.Empty Then .ApplicationDriverId = ApplicationDriverID
                If passengerID <> Guid.Empty Then .PassengerID = passengerID

                .ApplicationVehicleDriverTimeID = ApplicationVehicleDriverTimeID
                .ApplicationID = ThisSession.ApplicationID

            End With

            FMS.Business.DataObjects.ApplicationVehicleDriverTime.Update(driverVehicleTime)
        Next

        ThisSession.rm_DriverVehicleTimeReload = True

        e.Handled = True


    End Sub

    Private Sub dgvApplicationVehicleDriver_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles dgvApplicationVehicleDriver.CustomCallback

        Dim strs() As String = e.Parameters.Split("|")

        Dim editDate As Date = CDate(strs(0))
        Dim startTime As DateTime = CDate(strs(1).Split(" ")(1))
        Dim endtime As DateTime = CDate(strs(2).Split(" ")(1))

        Dim appID As Guid = ThisSession.ApplicationID

        startTime = editDate + startTime.TimeOfDay
        endtime = editDate + endtime.TimeOfDay

        ThisSession.rm_DriverVehicleTimeReload = True

        Dim retlst As List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime) = GetApplicationVehicleDriverTimes(startTime, endtime)

        dgvApplicationVehicleDriver.DataSourceID = Nothing
        dgvApplicationVehicleDriver.DataSource = retlst
        dgvApplicationVehicleDriver.DataBind()

    End Sub


    Private Sub dgvApplicationVehicleDriver_CustomErrorText(sender As Object, e As ASPxGridViewCustomErrorTextEventArgs) Handles dgvApplicationVehicleDriver.CustomErrorText

        If e.ErrorText = "Nullable object must have a value." Then e.ErrorText = "You cannot have a passenger without a driver."
    End Sub



    Protected Sub odsBooking_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs)
        Dim ab = CType(e.InputParameters(0), FMS.Business.DataObjects.ApplicationBooking)
        ab.ApplicationId = ThisSession.ApplicationID
        Dim gdes = FMS.Business.DataObjects.ApplicationGeoFence.FindApplicationGeoFence(ThisSession.ApplicationID, ab.GeofenceDestination)
        Dim gleave = FMS.Business.DataObjects.ApplicationGeoFence.FindApplicationGeoFence(ThisSession.ApplicationID, ab.GeofenceLeave)
        If gdes IsNot Nothing Then
            ab.GeofenceDestinationId = gdes.ApplicationGeoFenceID
        Else
            ab.GeofenceDestinationId = CreateBookingGeofence(ab.GeofenceDestination)
        End If
        If gleave IsNot Nothing Then
            ab.GeofenceLeaveId = gdes.ApplicationGeoFenceID
        Else
            ab.GeofenceLeaveId = CreateBookingGeofence(ab.GeofenceLeave)
        End If
    End Sub

    Protected Sub odsBookingContact_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs)
        Dim ab = CType(e.InputParameters(0), FMS.Business.DataObjects.Contact)
        ab.ApplicationID = ThisSession.ApplicationID
    End Sub
    'THIS IS FOR UPDATING DATA BEFORE INSERT
    Private Function CreateBookingGeofence(location As String) As Guid
        Dim ab = New FMS.Business.DataObjects.ApplicationGeoFence
        Dim x = FMS.Business.GoogleGeoCodeResponse.GetLatLongFromAddress(location)
        ab.ApplicationID = ThisSession.ApplicationID
        ab.Description = location
        ab.IsCircular = True
        ab.CircleRadiusMetres = 2000 'Temp: must be 2 km
        ab.CircleCentre = x.lat + "|" + x.lng
        ab.isBooking = True
        ab.UserID = ThisSession.User.UserId
        ab.DateCreated = DateTime.Now

        ab.ApplicationGeoFenceSides.Add(New Business.DataObjects.ApplicationGeoFenceSide() With {
                                        .Latitude = x.lat,
                                        .Longitude = x.lng
                                        })
        Return FMS.Business.DataObjects.ApplicationGeoFence.Create(ab)
    End Function

    'Protected Sub Unnamed_ItemRequestedByValue(sender As Object, e As DevExpress.Web.ListEditItemRequestedByValueEventArgs)
    '    Dim id As Guid
    '    If e.Value Is Nothing Or Not Guid.TryParse(e.Value.ToString, id) Then
    '        Return
    '    End If
    '    Dim cbx = CType(sender, ASPxComboBox)

    '    cbx.DataSource = FMS.Business.DataObjects.Contact.GetAllForApplication(ThisSession.ApplicationID).Where(Function(x) x.ContactID = id).ToList
    '    cbx.DataBind()

    'End Sub

    'Protected Sub Unnamed_ItemsRequestedByFilterCondition(sender As Object, e As DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs)
    '    Dim cbx = CType(sender, ASPxComboBox)
    '    Dim skip = e.BeginIndex
    '    Dim take = e.EndIndex - e.BeginIndex + 1

    '    cbx.DataSource = FMS.Business.DataObjects.Contact.GetAllForApplication(ThisSession.ApplicationID).Where(Function(x) x.NameFormatted.StartsWith(e.Filter)).Skip(skip).Take(take).ToList
    '    cbx.DataBind()
    'End Sub

End Class