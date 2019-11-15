Imports Microsoft.VisualBasic
#Region "#usings_form_aspxcs"
Imports DevExpress.XtraScheduler
'Imports DevExpress.Web
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports System.Collections.Generic
Imports DevExpress.XtraScheduler.Localization
Imports System.Collections
Imports System
Imports DXWebApplication1

#End Region ' #usings_form_aspxcs

Partial Public Class DriverAppointmentForm
    Inherits SchedulerFormControl
    Public ReadOnly Property CanShowReminders() As Boolean
        Get
            Return (CType(Parent, AppointmentFormTemplateContainer)).Control.Storage.EnableReminders
        End Get
    End Property
    Public ReadOnly Property ResourceSharing() As Boolean
        Get
            Return (CType(Parent, AppointmentFormTemplateContainer)).Control.Storage.ResourceSharing
        End Get
    End Property
    Public ReadOnly Property ResourceDataSource() As IEnumerable
        Get
            Return (CType(Parent, AppointmentFormTemplateContainer)).ResourceDataSource
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        'PrepareChildControls();
        tbSubject.Focus()
    End Sub

    Public Overrides Sub DataBind()
        MyBase.DataBind()
        Dim container As DriverAppointmentFormTemplateContainer = CType(Parent, DriverAppointmentFormTemplateContainer)

        Dim apt As Appointment = container.Appointment
        'edtLabel.SelectedIndex = apt.LabelId
        'edtStatus.SelectedIndex = apt.StatusId

        PopulateResourceEditors(apt, container)

        AppointmentRecurrenceForm1.Visible = container.ShouldShowRecurrence

        If container.Appointment.HasReminder Then
            'cbReminder.Value = container.Appointment.Reminder.TimeBeforeStart.ToString()
            'chkReminder.Checked = True
        Else
            'cbReminder.ClientEnabled = False
        End If

        btnOk.ClientSideEvents.Click = container.SaveHandler
        btnCancel.ClientSideEvents.Click = container.CancelHandler
        btnDelete.ClientSideEvents.Click = container.DeleteHandler

        'btnDelete.Enabled = !container.IsNewAppointment;
    End Sub
    Private Sub PopulateResourceEditors(ByVal apt As Appointment, ByVal container As AppointmentFormTemplateContainer)
        'If ResourceSharing Then
        '    Dim edtMultiResource As ASPxListBox = TryCast(ddResource.FindControl("edtMultiResource"), ASPxListBox)
        '    If edtMultiResource Is Nothing Then
        '        Return
        '    End If
        '    SetListBoxSelectedValues(edtMultiResource, apt.ResourceIds)
        '    Dim multiResourceString As List(Of String) = GetListBoxSeletedItemsText(edtMultiResource)
        '    Dim stringResourceNone As String = SchedulerLocalizer.GetString(SchedulerStringId.Caption_ResourceNone)
        '    ddResource.Value = stringResourceNone
        '    If multiResourceString.Count > 0 Then
        '        ddResource.Value = String.Join(", ", multiResourceString.ToArray())
        '    End If
        '    ddResource.JSProperties.Add("cp_Caption_ResourceNone", stringResourceNone)
        'Else
        '    If (Not Object.Equals(apt.ResourceId, Resource.Empty.Id)) Then
        '        edtResource.Value = apt.ResourceId.ToString()
        '    Else
        '        edtResource.Value = SchedulerIdHelper.EmptyResourceId
        '    End If
        'End If
    End Sub
    Private Function GetListBoxSeletedItemsText(ByVal listBox As ASPxListBox) As List(Of String)
        'Dim result As New List(Of String)()
        'For Each editItem As ListEditItem In listBox.Items
        '    If editItem.Selected Then
        '        result.Add(editItem.Text)
        '    End If
        'Next editItem
        'Return result
    End Function
    Private Sub SetListBoxSelectedValues(ByVal listBox As ASPxListBox, ByVal values As IEnumerable)
        'listBox.Value = Nothing
        'For Each value As Object In values
        '    Dim item As ListEditItem = listBox.Items.FindByValue(value.ToString())
        '    If item IsNot Nothing Then
        '        item.Selected = True
        '    End If
        'Next value
    End Sub
    Protected Overrides Sub PrepareChildControls()
        Dim container As DriverAppointmentFormTemplateContainer = CType(Parent, DriverAppointmentFormTemplateContainer)
        Dim control As ASPxScheduler = container.Control

        AppointmentRecurrenceForm1.EditorsInfo = New EditorsInfo(control, control.Styles.FormEditors, control.Images.FormEditors, control.Styles.Buttons)
        MyBase.PrepareChildControls()
    End Sub
#Region "#getchildeditors"
    Protected Overrides Function GetChildEditors() As ASPxEditBase()
        ' Required to implement client-side functionality
        Dim edits() As ASPxEditBase = {}
        Return edits
    End Function
#End Region ' #getchildeditors
    Protected Overrides Function GetChildButtons() As ASPxButton()
        Dim buttons() As ASPxButton = {btnOk, btnCancel, btnDelete}
        Return buttons
    End Function
End Class