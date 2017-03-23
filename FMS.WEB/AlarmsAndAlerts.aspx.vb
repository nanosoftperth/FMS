'Imports FMS.WEB.DemosExport
Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Public Class AlarmsAndAlerts
    Inherits System.Web.UI.Page


    'Protected Sub ToolbarExport_ItemClick(ByVal source As Object, ByVal e As ExportItemClickEventArgs)
    '    Select Case e.ExportType
    '        Case DemoExportFormat.Pdf
    '            gridExport.WritePdfToResponse()
    '        Case DemoExportFormat.Xls
    '            gridExport.WriteXlsToResponse(New XlsExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
    '        Case DemoExportFormat.Xlsx
    '            gridExport.WriteXlsxToResponse(New XlsxExportOptionsEx With {.ExportType = ExportType.WYSIWYG})
    '        Case DemoExportFormat.Rtf
    '            gridExport.WriteRtfToResponse()
    '        Case DemoExportFormat.Csv
    '            gridExport.WriteCsvToResponse(New CsvExportOptionsEx() With {.ExportType = ExportType.WYSIWYG})
    '    End Select
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CType(Me.Master, FMS.WEB.MainLightMaster).HeaderText = "Geo-Fences & Alerts"

        If Not IsPostBack Then

            With Now.timezoneToClient
                Me.dateEditStartTime.Value = New Date(.Year, .Month, .Day, 0, 0, 0)
                Me.dateEditEndtime.Value = Now.timezoneToClient
            End With

        End If


    End Sub


    Protected Sub ASPxButton1_Click(sender As Object, e As EventArgs) Handles ASPxButton1.Click
        dgvGeoFences.DataBind()
    End Sub
    Private Sub odsAlerts_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsAlerts.Inserting
        CType(e.InputParameters(0), FMS.Business.DataObjects.AlertType).ApplicationID = FMS.Business.ThisSession.ApplicationID
    End Sub

    Private Sub odsAlerts_Deleting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsAlerts.Deleting
        CType(e.InputParameters(0), FMS.Business.DataObjects.AlertType).ApplicationID = FMS.Business.ThisSession.ApplicationID
    End Sub

    Private Sub odsAlerts_Updating(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsAlerts.Updating
        CType(e.InputParameters(0), FMS.Business.DataObjects.AlertType).ApplicationID = FMS.Business.ThisSession.ApplicationID
    End Sub

#Region "GROUP DATA LOGIC"

    Private Sub dgvGroups_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles dgvGroups.RowInserting
        e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
    End Sub

    Private Sub dgvGroups_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles dgvGroups.RowUpdating
        e.NewValues("ApplicationID") = FMS.Business.ThisSession.ApplicationID
    End Sub

    Private Sub dgvGroupMembers_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs) Handles dgvGroupMembers.BatchUpdate


        For Each o As DevExpress.Web.Data.ASPxDataUpdateValues In e.UpdateValues

            Dim nativeid As Guid = o.Keys("NativeID")
            Dim Email As String = o.NewValues("Email")
            Dim mobile As String = o.NewValues("Mobile")
            Dim sendEmail As Boolean = o.NewValues("SendEmail")
            Dim sendText As Boolean = o.NewValues("SendText")
            Dim groupID As Guid = FMS.Business.ThisSession.CurrentSelectedGroup

            'do something with this data here
            FMS.Business.DataObjects.Subscriber.ChangeSettingForGroup(groupID, nativeid, sendEmail, sendText)

        Next

        dgvGroupMembers.DataSourceID = Nothing
        dgvGroupMembers.DataSource = FMS.Business.DataObjects.Subscriber.GetAllForGroup(FMS.Business.ThisSession.CurrentSelectedGroup)
        dgvGroupMembers.DataBind()

        e.Handled = True

    End Sub

    Private Sub dgvPotentialGroupMembers_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles dgvGroupMembers.CustomCallback

        'bar delimited list command|value(s)
        'options add/remove/get

        Try

            Dim command As String = e.Parameters.Split("|")(0)
            Dim command_val As String = e.Parameters.Split("|")(1)

            Select Case command

                Case "get"

                    Dim keyAsGUID As Guid

                    If Not Guid.TryParse(command_val, keyAsGUID) Then
                        Exit Sub
                    End If

                    FMS.Business.ThisSession.CurrentSelectedGroup = keyAsGUID

                Case "add"

                    Dim subscriberNativeIDs As List(Of Guid) = command_val.Split(",").Select(Function(x) New Guid(x)).ToList

                    FMS.Business.DataObjects.Group.AddSubscribersByIDs(FMS.Business.ThisSession.CurrentSelectedGroup, subscriberNativeIDs)

                Case "remove"

                    Dim subscriberNativeIDs As List(Of Guid) = command_val.Split(",").Select(Function(x) New Guid(x)).ToList

                    FMS.Business.DataObjects.Group.RemoveSubscribersByIDs(FMS.Business.ThisSession.CurrentSelectedGroup, subscriberNativeIDs)

                Case Else
                    'do nothing 
            End Select

            'refresh the data in the grid view (after the above operations)
            dgvGroupMembers.DataSourceID = Nothing
            dgvGroupMembers.DataSource = FMS.Business.DataObjects.Subscriber.GetAllForGroup(FMS.Business.ThisSession.CurrentSelectedGroup)
            dgvGroupMembers.DataBind()


        Catch ex As Exception
            Throw
        End Try

    End Sub
#End Region

End Class