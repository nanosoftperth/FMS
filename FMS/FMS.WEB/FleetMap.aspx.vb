Imports FMS.Business.DataObjects.FeatureListConstants
Imports FMS.Business
Imports DevExpress.Web

Public Class FleetMap
    Inherits System.Web.UI.Page


    Public Shared ReadOnly Property WebVersion As String
        Get
            Return My.Settings.version
        End Get
    End Property

    Private Sub FleetMap_Init(sender As Object, e As EventArgs) Handles Me.Init

        If IsPostBack Then Exit Sub

        '*******************        POPULATE THE GEO-FENCE SUMMARY GRIDVIEW AND DROP DOWN LIST      *******************

        Dim lst As List(Of FMS.Business.DataObjects.ApplicationGeoFence) = _
                                            FMS.Business.DataObjects.ApplicationGeoFence. _
                                                     GetAllApplicationGeoFences(FMS.Business.ThisSession.ApplicationID).ToList

        dgvGeoFenceSummary.DataSource = lst
        cbGeoEditGeoFences.DataSource = lst

        dgvGeoFenceSummary.DataSourceID = Nothing : dgvGeoFenceSummary.DataBind()
        cbGeoEditGeoFences.DataSourceID = Nothing : cbGeoEditGeoFences.DataBind()

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not UserAuthorisationCheck(FeatureListAccess.Fleet_Map) Then Exit Sub

        Dim clientDate As Date = Now.timezoneToClient

        ASPxDateEdit1.Date = clientDate
        heatMapStartTime.Date = New Date(clientDate.Year, clientDate.Month, clientDate.Day).AddHours(5) '5am
        heatMapEndTime.Date = clientDate

        '|============================================================================================================|
        '||loop through all the application settings and make them available client side by exposing as JS variables ||
        '||also, force-adding a few settins we want to read client side also.                                        ||
        '|============================================================================================================|

        Dim settingstoSendtoCLient As New List(Of DataObjects.Setting)

        'settingstoSendtoCLient.AddRange(ThisSession.ApplicationObject.Settings)

        Dim appLocationID As Guid = If(FMS.Business.ThisSession.User.ApplicationLocationID = Guid.Empty, _
                                       FMS.Business.ThisSession.ApplicationObject.DefaultBusinessLocationID, _
                                       FMS.Business.ThisSession.User.ApplicationLocationID)

        Dim defaultBusinessLocation As DataObjects.ApplicationLocation = DataObjects.ApplicationLocation.GetFromID(appLocationID)

        settingstoSendtoCLient.Add(New DataObjects.Setting With {.Name = "Business_Lattitude", .Value = defaultBusinessLocation.Lattitude})
        settingstoSendtoCLient.Add(New DataObjects.Setting With {.Name = "Business_Longitude", .Value = defaultBusinessLocation.Longitude})

        settingstoSendtoCLient.AddRange(GetVINNumberSettings)

        Dim str As String = "var serverSetting_{0} = '{1}';{2}"
        Dim loopStr As String = ""

        For Each s As DataObjects.Setting In settingstoSendtoCLient
            loopStr &= String.Format(str, s.Name, s.Value, vbNewLine)
        Next

        Dim userCanManageGeoFences As Boolean = FMS.Business.ThisSession.User.GetIfAccessToFeature(FeatureListAccess.Fleet_Map__GeoFence_Edit)

        'for use with javascript (if the user has access to manage geofences)
        loopStr &= String.Format(str, "Fleet_Management_Page_Manage_geofences", userCanManageGeoFences, vbNewLine)

        loopStr = String.Format("<script type=""text/javascript"">{0}{1}{0}</script>", vbNewLine, loopStr)

        If Not ClientScript.IsClientScriptBlockRegistered("1") Then ClientScript.RegisterStartupScript(Me.[GetType](), "1", loopStr)





    End Sub

    ''' <summary>
    ''' If we receive a querystring asking to show a vehicle (by VIN number)
    ''' then we will need to send those details to the client. This method
    ''' gets those values and adds them to a list of settings which is then sent to the 
    ''' client side for processing.
    ''' </summary>
    Private Function GetVINNumberSettings() As List(Of DataObjects.Setting)

        Dim retLst As New List(Of DataObjects.Setting)

        Dim vehicleautoRept_VINNumber As String = Request.QueryString("VINNumber")
        Dim vehicleautoRept_Requested As String = CStr(vehicleautoRept_VINNumber IsNot Nothing)
        Dim vehicleautoRept_VehicleID As String = String.Empty
        Dim vehicleautoRept_VehicleName As String = String.Empty
        Dim vehicleautoRept_ErrorMsg As String = String.Empty
        Dim vehicleautoRept_DeviceID As String = String.Empty

        'does not like dealing with null strings
        If vehicleautoRept_VINNumber Is Nothing Then vehicleautoRept_VINNumber = String.Empty

        If CBool(vehicleautoRept_Requested) Then
            Try

                FMS.Business.ThisSession.ShowReturnToUniqcoButton = True
                FMS.Business.ThisSession.ReturnToUniqcoURL = Request.QueryString("returnURL")

                Dim v = DataObjects.ApplicationVehicle.GetFromVINNumber(vehicleautoRept_VINNumber, FMS.Business.ThisSession.ApplicationID)

                vehicleautoRept_VehicleID = v.ApplicationVehileID.ToString
                vehicleautoRept_VehicleName = v.Name
                vehicleautoRept_DeviceID = v.DeviceID

            Catch ex As Exception
                vehicleautoRept_ErrorMsg = ex.Message
            End Try
        End If

        retLst.Add(New DataObjects.Setting With {.Name = "vehicleautoRept_VINNumber", .Value = vehicleautoRept_VINNumber})
        retLst.Add(New DataObjects.Setting With {.Name = "vehicleautoRept_Requested", .Value = vehicleautoRept_Requested})
        retLst.Add(New DataObjects.Setting With {.Name = "vehicleautoRept_VehicleID", .Value = vehicleautoRept_VehicleID})
        retLst.Add(New DataObjects.Setting With {.Name = "vehicleautoRept_VehicleName", .Value = vehicleautoRept_VehicleName})
        retLst.Add(New DataObjects.Setting With {.Name = "vehicleautoRept_ErrorMsg", .Value = vehicleautoRept_ErrorMsg})
        retLst.Add(New DataObjects.Setting With {.Name = "vehicleautoRept_DeviceID", .Value = vehicleautoRept_DeviceID})

        Return retLst

    End Function

    Private Sub dgvGeoFenceSummary_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles dgvGeoFenceSummary.CustomCallback

        dgvGeoFenceSummary.DataSource = FMS.Business.DataObjects.ApplicationGeoFence. _
                                            GetAllApplicationGeoFences(FMS.Business.ThisSession.ApplicationID)

        dgvGeoFenceSummary.DataSourceID = Nothing
        dgvGeoFenceSummary.DataBind()

    End Sub

    Private Sub cbGeoEditGeoFences_Callback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles cbGeoEditGeoFences.Callback

        Dim lst As List(Of FMS.Business.DataObjects.ApplicationGeoFence) = _
                                           FMS.Business.DataObjects.ApplicationGeoFence. _
                                                    GetAllApplicationGeoFences(FMS.Business.ThisSession.ApplicationID).ToList

        cbGeoEditGeoFences.DataSource = lst
        cbGeoEditGeoFences.DataSourceID = Nothing
        cbGeoEditGeoFences.DataBind()

    End Sub

    Private Sub ddlTrucks_Init(sender As Object, e As EventArgs) Handles ddlTrucks.Init

        '*******************        POPULATE THE TRUCK DROP DOWN LIST (FOR MULTI-SELECT)      ******************

        Dim truckLst As List(Of Truck) = Truck.GetExampleFleetNow(FMS.Business.ThisSession.ApplicationID)
        Dim lb As DevExpress.Web.ASPxListBox = ddlTrucks.FindControl("listBox")

        truckLst.Insert(0, New Truck With {.ComboBoxDisplay = "Select All"})

        lb.TextField = "ComboBoxDisplay"
        lb.ValueField = "ID"
        lb.DataSource = truckLst
    End Sub
    Protected Sub dgvVehicles_DataBound(sender As Object, e As EventArgs)

        'For i As Integer = 0 To dgvVehicles.VisibleRowCount - 1
        '    'If dgvVehicles.Selection.IsRowSelected(i) Then
        '    dgvVehicles.Selection.SelectRow(i)
        '    'End If
        'Next 

        'Dim gridView As ASPxGridView = TryCast(sender, ASPxGridView)
        'Dim i As Integer = 0
        'Do While i < gridView.VisibleRowCount
        '    'If Not Convert.ToString(gridView.KeyFieldName) = "ApplicationVehileID" Then
        '    Dim id As Integer = Convert.ToInt32(gridView.GetRowValues(i, Convert.ToString(gridView.KeyFieldName)))
        '    If id Mod 2 = 0 Then
        '        gridView.Selection.SelectRow(i)
        '    End If
        '    i += 1
        '    'End If 
        'Loop

        'For i As Integer = 0 To dgvVehicles.VisibleRowCount - 1

        'Dim keyValue As Integer = Convert.ToInt32(dgvVehicles.GetRowValues(i, dgvVehicles.KeyFieldName))
        'Dim visibleIndex As Integer = dgvVehicles.FindVisibleIndexByKeyValue(keyValue)

        'If visibleIndex = 1 Then
        '    dgvVehicles.Selection.SelectRow(visibleIndex)
        'End If
        'dgvVehicles.Selection.SetSelection(i, True)
        'Next
    End Sub
    Protected Sub dgvVehicles_PreRender(sender As Object, e As EventArgs)

        For i As Integer = 0 To dgvVehicles.VisibleRowCount - 1
            'If dgvVehicles.Selection.IsRowSelected(i) Then
            dgvVehicles.Selection.SelectRow(i)
            'End If
        Next
    End Sub
End Class