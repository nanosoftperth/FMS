Public Class Group
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub dgvGroups_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles dgvGroups.RowInserting
        e.NewValues("ApplicationID") = ThisSession.ApplicationID
    End Sub

    Private Sub dgvGroups_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles dgvGroups.RowUpdating
        e.NewValues("ApplicationID") = ThisSession.ApplicationID
    End Sub

    Private Sub dgvGroupMembers_BatchUpdate(sender As Object, e As DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs) Handles dgvGroupMembers.BatchUpdate


        For Each o As DevExpress.Web.Data.ASPxDataUpdateValues In e.UpdateValues

            Dim nativeid As Guid = o.Keys("NativeID")
            Dim Email As String = o.NewValues("Email")
            Dim mobile As String = o.NewValues("Mobile")
            Dim sendEmail As Boolean = o.NewValues("SendEmail")
            Dim sendText As Boolean = o.NewValues("SendText")
            Dim groupID As Guid = ThisSession.CurrentSelectedGroup

            'do something with this data here
            FMS.Business.DataObjects.Subscriber.ChangeSettingForGroup(groupID, nativeid, sendEmail, sendText)

        Next

        dgvGroupMembers.DataSourceID = Nothing
        dgvGroupMembers.DataSource = FMS.Business.DataObjects.Subscriber.GetAllForGroup(ThisSession.CurrentSelectedGroup)
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

                    ThisSession.CurrentSelectedGroup = keyAsGUID

                Case "add"

                    Dim subscriberNativeIDs As List(Of Guid) = command_val.Split(",").Select(Function(x) New Guid(x)).ToList

                    FMS.Business.DataObjects.Group.AddSubscribersByIDs(ThisSession.CurrentSelectedGroup, subscriberNativeIDs)

                Case "remove"

                    Dim subscriberNativeIDs As List(Of Guid) = command_val.Split(",").Select(Function(x) New Guid(x)).ToList

                    FMS.Business.DataObjects.Group.RemoveSubscribersByIDs(ThisSession.CurrentSelectedGroup, subscriberNativeIDs)

                Case Else
                    'do nothing 
            End Select

            'refresh the data in the grid view (after the above operations)
            dgvGroupMembers.DataSourceID = Nothing
            dgvGroupMembers.DataSource = FMS.Business.DataObjects.Subscriber.GetAllForGroup(ThisSession.CurrentSelectedGroup)
            dgvGroupMembers.DataBind()


        Catch ex As Exception
            Throw
        End Try

    End Sub


    Private Sub dgvPotentialGroupMembers_DataBinding(sender As Object, e As EventArgs) Handles dgvPotentialGroupMembers.DataBinding

    End Sub
End Class