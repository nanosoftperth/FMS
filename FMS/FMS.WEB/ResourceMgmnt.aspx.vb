Imports DevExpress.Web
Imports FMS.Business.DataObjects.FeatureListConstants
Imports System.Collections
Imports System.Collections.Generic
Imports DevExpress.Web.Data



Public Class ResourceMgmnt
    Inherits System.Web.UI.Page


#Region "odometer information"
    Private priDeviceID As String = ""

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

#End Region

    Private Sub ASPxGridView2_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles ASPxGridView2.RowInserting
        e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
    End Sub

    Private Sub ASPxGridView2_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles ASPxGridView2.RowUpdating
        e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
    End Sub
    Private Sub dgvVehicles_RowInserting(sender As Object, e As Data.ASPxDataInsertingEventArgs) Handles dgvVehicles.RowInserting
        e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
        e.NewValues("BusinessLocation") = GetBusinessLocation()
    End Sub

    Private Sub dgvVehicles_RowUpdating(sender As Object, e As Data.ASPxDataUpdatingEventArgs) Handles dgvVehicles.RowUpdating
        e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
        e.NewValues("BusinessLocation") = GetBusinessLocation()
    End Sub

    Protected Sub dgvVehicles_CustomColumnDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewColumnDisplayTextEventArgs)

        If e.Column.FieldName = "DeviceID" Then
            priDeviceID = e.Value
        End If

        If e.Column.FieldName = "BusinessLocation" Then
            Dim BussLocs As String = ""
            Dim blID As Guid

            Dim appID = FMS.Business.ThisSession.ApplicationID
            Dim vehicle = FMS.Business.DataObjects.ApplicationVehicle.GetAllWithBusinessLocation(appID, priDeviceID)

            For Each row In vehicle
                If row.BusinessLocation IsNot Nothing And row.BusinessLocation.ToString().Length > 0 Then

                    Dim blList As String() = Nothing
                    blList = row.BusinessLocation.Split("|")
                    Dim blVal As String
                    'Dim strVal As String

                    For count = 0 To blList.Length - 1
                        blVal = blList(count)

                        blID = New Guid(blVal)

                        Dim blObj = FMS.Business.DataObjects.ApplicationLocation.GetFromID(blID)

                        If (BussLocs.Length > 0) Then
                            BussLocs = BussLocs + " | " + blObj.Name
                        Else

                            BussLocs = blObj.Name
                        End If

                    Next

                End If
            Next

            e.DisplayText = If(BussLocs, String.Empty)

            'Dim text = DataProvider.GetTags().Where(Function(t) tagIDs.Contains(t.ID)).Select(Function(t) t.Name).DefaultIfEmpty().Aggregate(Function(a, b) a & ", " & b)

            'e.DisplayText = If(query, String.Empty)
        End If
    End Sub

    Protected Sub luBusinessLocation_CustomJSProperties(ByVal sender As Object, ByVal e As CustomJSPropertiesEventArgs)
        Dim grid As ASPxGridLookup = TryCast(sender, ASPxGridLookup)

        Dim start As Int32 = grid.GridView.VisibleStartIndex
        Dim [end] As Int32 = grid.GridView.VisibleStartIndex + grid.GridView.SettingsPager.PageSize

        Dim selectNumbers As Int32 = 0

        If [end] > grid.GridView.VisibleRowCount Then
            [end] = (grid.GridView.VisibleRowCount)
        Else
            [end] = ([end])
        End If

        For i As Integer = start To [end] - 1
            If grid.GridView.Selection.IsRowSelected(i) Then
                selectNumbers += 1
            End If
        Next i

        e.Properties("cpSelectedRowsOnPage") = selectNumbers
        e.Properties("cpVisibleRowCount") = grid.GridView.VisibleRowCount

    End Sub

    Protected Sub cbAll_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim chk As ASPxCheckBox = TryCast(sender, ASPxCheckBox)
        Dim container = CType(chk.NamingContainer, GridViewHeaderTemplateContainer).Grid

        chk.Checked = (container.Selection.Count = container.VisibleRowCount)

        Dim obj As Object = ""

        'Dim container = CType(lookup.NamingContainer, GridViewEditItemTemplateContainer)

        'Dim chk As ASPxCheckBox = TryCast(sender, ASPxCheckBox)
        'Dim grid As ASPxGridView = (TryCast(chk.NamingContainer, GridViewHeaderTemplateContainer)).Grid
        'chk.Checked = (grid.Selection.Count = grid.VisibleRowCount)
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
        dateEditDay.Date = currentDate
        Me.timeEditFrom.DateTime = Me.dateEditDay.Date.AddHours(5)
        Me.timeEditTo.DateTime = Me.dateEditDay.Date.AddHours(23).AddMinutes(59).AddSeconds(59)

        Dim retlst As List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime) = _
                                    FMS.Business.DataObjects.ApplicationVehicleDriverTime. _
                                            GetAllForApplicationAndDatePeriodIncludingDuds(FMS.Business.ThisSession.ApplicationID, Me.dateEditDay.Date, Me.timeEditTo.DateTime)

        Try
            pageControlMain.TabPages(3).Visible = FMS.Business.ThisSession.User.GetIfAccessToFeature(FeatureListAccess.Vehicle_and_Driver_Management__Bookings)  'ByRyan: Will determine if Application has access to Feature

        Catch ex As Exception

        End Try
    End Sub


    Public Shared Function GetApplicationVehicleDriverTimes(startdate As Date, enddate As Date) As List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime)


        If FMS.Business.ThisSession.rm_ApplicationDriverVehicleTimes Is Nothing Or FMS.Business.ThisSession.rm_DriverVehicleTimeReload Then

            Dim retlst As List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime) = _
                               FMS.Business.DataObjects.ApplicationVehicleDriverTime. _
                                    GetAllForApplicationAndDatePeriodIncludingDuds(FMS.Business.ThisSession.ApplicationID, startdate, enddate)

            FMS.Business.ThisSession.rm_ApplicationDriverVehicleTimes = retlst

            FMS.Business.ThisSession.rm_DriverVehicleTimeReload = False

        End If

        Return FMS.Business.ThisSession.rm_ApplicationDriverVehicleTimes

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
                .ApplicationID = FMS.Business.ThisSession.ApplicationID
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
                .ApplicationID = FMS.Business.ThisSession.ApplicationID

            End With

            FMS.Business.DataObjects.ApplicationVehicleDriverTime.Update(driverVehicleTime)
        Next

        FMS.Business.ThisSession.rm_DriverVehicleTimeReload = True

        e.Handled = True


    End Sub

    Private Sub dgvApplicationVehicleDriver_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles dgvApplicationVehicleDriver.CustomCallback

        Dim strs() As String = e.Parameters.Split("|")

        Dim editDate As Date = CDate(strs(0))
        Dim startTime As DateTime = CDate(strs(1).Split(" ")(1))
        Dim endtime As DateTime = CDate(strs(2).Split(" ")(1))

        Dim appID As Guid = FMS.Business.ThisSession.ApplicationID

        startTime = editDate + startTime.TimeOfDay
        endtime = editDate + endtime.TimeOfDay

        FMS.Business.ThisSession.rm_DriverVehicleTimeReload = True

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

        ab.ApplicationId = FMS.Business.ThisSession.ApplicationID

        Dim gdes = FMS.Business.DataObjects.ApplicationGeoFence.FindApplicationGeoFence(FMS.Business.ThisSession.ApplicationID, ab.GeofenceDestination)
        Dim gleave = FMS.Business.DataObjects.ApplicationGeoFence.FindApplicationGeoFence(FMS.Business.ThisSession.ApplicationID, ab.GeofenceLeave)

        'ab.GeofenceDestinationId = If(gdes Is Nothing, gdes.ApplicationGeoFenceID, CreateBookingGeofence(ab.GeofenceDestination))
        'ab.GeofenceLeaveId = If(gleave Is Nothing, gleave.ApplicationGeoFenceID, CreateBookingGeofence(ab.GeofenceLeave))

        ' Edit by Aman on 20170519
        ab.GeofenceDestinationId = If(gdes Is Nothing, CreateBookingGeofence(ab.GeofenceDestination), gdes.ApplicationGeoFenceID)
        ab.GeofenceLeaveId = If(gleave Is Nothing, CreateBookingGeofence(ab.GeofenceLeave), gleave.ApplicationGeoFenceID)

    End Sub
    Protected Sub odsBookingContact_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs)
        Dim ab = CType(e.InputParameters(0), FMS.Business.DataObjects.Contact)
        ab.ApplicationID = FMS.Business.ThisSession.ApplicationID
    End Sub
    'THIS IS FOR UPDATING DATA BEFORE INSERT
    Private Function CreateBookingGeofence(location As String) As Guid
        Dim ab = New FMS.Business.DataObjects.ApplicationGeoFence
        Dim x = FMS.Business.GoogleGeoCodeResponse.GetLatLongFromAddress(location)
        ab.ApplicationID = FMS.Business.ThisSession.ApplicationID
        ab.Name = location
        ab.Description = location
        ab.IsCircular = True
        ab.CircleRadiusMetres = 2000 'Temp: must be 2 km
        ab.CircleCentre = x.lat + "|" + x.lng
        ab.isBooking = True
        ab.UserID = FMS.Business.ThisSession.User.UserId
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

    Protected Sub pageControlMain_ActiveTabChanged(source As Object, e As TabControlEventArgs) Handles pageControlMain.ActiveTabChanged

    End Sub
    Protected Sub dgvDetailBookings_RowValidating(sender As Object, e As Data.ASPxDataValidationEventArgs)
        For Each column As GridViewColumn In dgvDetailBookings.Columns
            Dim dataColumn As GridViewDataColumn = TryCast(column, GridViewDataColumn)
            If dataColumn Is Nothing Then
                Continue For
            Else
                If dataColumn.FieldName = "ContactID" Or dataColumn.FieldName = "ArrivalTime" Or dataColumn.FieldName = "GeofenceLeave" Or dataColumn.FieldName = "GeofenceDestination" Or dataColumn.FieldName = "ApplicationDriverID" Then
                    If String.IsNullOrEmpty(Convert.ToString(e.NewValues(dataColumn.FieldName))) Then
                        e.Errors(dataColumn) = "Value can't be null."
                    End If
                End If
            End If
        Next column
    End Sub
    Private Sub AddError(ByVal errors As Dictionary(Of GridViewColumn, String), ByVal column As GridViewColumn, ByVal errorText As String)
        If errors.ContainsKey(column) Then
            Return
        End If
        errors(column) = errorText
    End Sub

#Region "for business location logic"

    Private Function GetBusinessLocation() As Object

        Try
            Dim arrCtr As Integer = 0

            Dim column = CType(dgvVehicles.Columns("BusinessLocation"), GridViewDataColumn)
            Dim lookup = CType(dgvVehicles.FindEditRowCellTemplateControl(column, "luBusinessLocation"), ASPxGridLookup)
            Dim tags = TryCast(lookup.GridView.GetSelectedFieldValues(lookup.KeyFieldName), List(Of Object))

            Return tags

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Protected Sub BL_Lookup_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim lookup = CType(sender, ASPxGridLookup)
        Dim container = CType(lookup.NamingContainer, GridViewEditItemTemplateContainer)

        If container.Grid.IsNewRowEditing Then
            Return
        End If

        Dim BLs = CType(container.Grid.GetRowValues(container.VisibleIndex, container.Column.FieldName), String)

        If (BLs IsNot Nothing And BLs.Length > 0) Then
            Dim blList As String() = Nothing
            blList = BLs.Split("|")
            Dim blVal As String
            Dim blID As Guid

            For count = 0 To blList.Length - 1
                blVal = blList(count)

                blID = New Guid(blVal)

                lookup.GridView.Selection.SelectRowByKey(blID)

            Next
        End If



    End Sub


    Public Sub ItemRequestedByValue(ByVal source As Object, ByVal e As DevExpress.Web.ListEditItemRequestedByValueEventArgs)
        Dim value As Integer = 0
        If e.Value Is Nothing OrElse (Not Int32.TryParse(e.Value.ToString(), value)) Then
            Return
        End If

        Dim comboBox As ASPxComboBox = CType(source, ASPxComboBox)

        Dim appID = FMS.Business.ThisSession.ApplicationID
        Dim query = FMS.Business.DataObjects.ApplicationLocation.GetAllIncludingInheritFromApplication(appID)

        comboBox.DataSource = query
        comboBox.DataBind()
    End Sub

    Public Sub ItemsRequestedByFilterCondition(ByVal source As Object, ByVal e As DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs)
        Dim comboBox As ASPxComboBox = CType(source, ASPxComboBox)

        Dim skip = e.BeginIndex
        Dim take = e.EndIndex - e.BeginIndex + 1

        Dim appID = FMS.Business.ThisSession.ApplicationID
        Dim query = FMS.Business.DataObjects.ApplicationLocation.GetAllIncludingInheritFromApplication(appID)

        comboBox.DataSource = query
        comboBox.DataBind()
    End Sub

    Protected Sub GetSelectionButton_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim script As String = "<script type='text/javascript'> alert('Test...');</script>"
        ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
        'Dim grid As ASPxGridView = ASPxGridLookup1.GridView
        'Dim value As Object = grid.GetRowValues(grid.FocusedRowIndex, New String() {"ProductName"})
        'ASPxListBox1.Items.Clear()

        'If value IsNot Nothing Then
        '    ASPxListBox1.Items.Add(value.ToString())
        'End If
    End Sub


#End Region


End Class