﻿Imports FMS.Business

Public Class Test
    Inherits System.Web.UI.Page

    Public ReadOnly Property AppVersion As String
        Get
            Return My.Settings.version
        End Get
    End Property

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load



        '############           EXIT THE SUB HERE IF THIS IS A CALL/POST BACK      #############
        If IsPostBack Or IsCallback Then Exit Sub

        Dim thisapp As DataObjects.Application = DataObjects.Application.GetFromAppID(ThisSession.ApplicationID)

        Dim Settings As List(Of DataObjects.Setting) = DataObjects.Setting.GetSettingsForApplication(thisapp.ApplicationName)
        Dim logoBytes() As Byte = DataObjects.Setting.GetLogoForApplication(thisapp.ApplicationName)

        Me.imgCompanylogo.ContentBytes = logoBytes

        dgvTimezoneSettings.DataSourceID = Nothing
        dgvTimezoneSettings.DataSource = ThisSession.GetTimeZoneValues
        dgvTimezoneSettings.DataBind()

        cboPossibleTimeZones.Value = ThisSession.ApplicationObject.TimeZone.ID

    End Sub

    Protected Sub ASPxButton1_Click(sender As Object, e As EventArgs) Handles ASPxButton1.Click

        If Me.imgCompanylogo.ContentBytes Is Nothing Then Exit Sub

        Dim logoBytes() As Byte = Me.imgCompanylogo.ContentBytes

        Dim thisapp As DataObjects.Application = _
                                  DataObjects.Application.GetFromAppID(ThisSession.ApplicationID)

        Dim s As DataObjects.Setting = DataObjects.Setting.GetSettingsForApplication(thisapp.ApplicationName) _
                                                                        .Where(Function(itm) itm.Name = "Logo").Single

        s.ValueObj = Me.imgCompanylogo.ContentBytes
        DataObjects.Setting.Update(s)
    End Sub


    Private Sub odsUsers_Updating(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsUsers.Updating
        CType(e.InputParameters(0), FMS.Business.DataObjects.User).ApplicationID = ThisSession.ApplicationID
    End Sub

    Private Sub odsRolesRoles_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsRolesRoles.Inserting
        CType(e.InputParameters(0), FMS.Business.DataObjects.Role).ApplicationID = ThisSession.ApplicationID
    End Sub

    Private Sub odsRolesRoles_Updating(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsRolesRoles.Updating
        CType(e.InputParameters(0), FMS.Business.DataObjects.Role).ApplicationID = ThisSession.ApplicationID
    End Sub

    Protected Sub dgvRolesUsers_BeforePerformDataSelect(sender As Object, e As EventArgs)
        ThisSession.CurrentExpandedRow = DirectCast(sender, DevExpress.Web.ASPxGridView).GetMasterRowKeyValue
    End Sub

    Private Sub dgvRoleAccessToFeatures_RowInserting(sender As Object, e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles dgvRoleAccessToFeatures.RowInserting
        e.NewValues.Add("ApplicationID", ThisSession.ApplicationID)
    End Sub

    Private Sub dgvSettings_RowUpdating(sender As Object, e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles dgvSettings.RowUpdating
        e.NewValues.Add("ApplicationID", ThisSession.ApplicationID)
    End Sub

    Private Sub dgvTimezoneSettings_CustomCallback(sender As Object, e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles dgvTimezoneSettings.CustomCallback

        Dim timeZoneID As String = e.Parameters

        Dim tz As Business.DataObjects.TimeZone

        If String.IsNullOrEmpty(timeZoneID) Then
            tz = ThisSession.ApplicationObject.GetTimezoneFromLatLong()
        Else
            tz = FMS.Business.DataObjects.TimeZone.GettimeZoneFromID(timeZoneID)
        End If

        ThisSession.ApplicationObject.SaveTimeZone(tz)

        dgvTimezoneSettings.DataSourceID = Nothing
        dgvTimezoneSettings.DataSource = ThisSession.GetTimeZoneValues
        dgvTimezoneSettings.DataBind()

    End Sub

    Private Sub Page_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        'this is a little hacky, update the users timezone every time visiting the page ( and a callback or postback happens)
        ThisSession.User.UpdatetimeZone()

        'Set the timezone for the business layer to be = to the user (the user timezone is taken from the application
        'timezone if they havent explicitly defined one)
        FMS.Business.SingletonAccess.ClientSelected_TimeZone = If(Not String.IsNullOrEmpty(ThisSession.User.TimeZone.ID), _
                                                                        ThisSession.User.TimeZone, ThisSession.ApplicationObject.TimeZone)
    End Sub

    Protected Sub odsUsers_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs)
        'BY RYAN
        Dim agp = Membership.GeneratePassword(9, 0)
        'TODO: email must be unique per application and not empty
        Dim x = CType(e.InputParameters(0), FMS.Business.DataObjects.User)
        x.ApplicationID = ThisSession.ApplicationID
        Dim user As MembershipUser = Membership.CreateUser(x.UserName, agp, x.Email)
        x.UserId = user.ProviderUserKey

        'send an email with agp
        If x.SendEmailtoUserWithDefPass Then
            BackgroundCalculations.EmailHelper.SendEmailUserCreated(x.Email, ThisSession.ApplicationName, x.UserName, agp)
            dgvUsers.JSProperties("cpHasInserted") = x.Email

        End If
    End Sub

    Protected Sub dgvUsers_BeforeGetCallbackResult(sender As Object, e As EventArgs)

        Dim x = CType(sender, DevExpress.Web.ASPxGridView)
        If Not x.IsNewRowEditing Then
            Dim c = CType(x.Columns("SendEmailtoUserWithDefPass"), DevExpress.Web.GridViewDataColumn)
            c.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False
        End If
    End Sub

    Protected Sub dgvUsers_InitNewRow(sender As Object, e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs)
        e.NewValues("SendEmailtoUserWithDefPass") = True
    End Sub
End Class