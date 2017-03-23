Imports FMS.Business.DataObjects
Imports DevExpress.Web.ASPxScheduler

Public Class Test1
    Inherits System.Web.UI.Page

    Private lastInsertedAppointmentId As Object
    Private objectInstance As CustomEventDataSource
    'Obtain the ID of the last inserted appointment from the object data source and assign it to the appointment in the ASPxScheduler storage.
    Protected Sub ASPxScheduler1_AppointmentRowInserted(ByVal sender As Object,
                                                        ByVal e As DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertedEventArgs) Handles ASPxScheduler1.AppointmentRowInserted


        Dim events As CustomEventList = TryCast(Session("CustomEventListData"), CustomEventList)

        e.KeyFieldValue = Me.objectInstance.ObtainLastInsertedId()

        If events IsNot Nothing Then

            events.Add(New FMS.Business.DataObjects.CustomEvent() With {.Id = e.KeyFieldValue, _
                                        .StartTime = e.NewValues("StartTime"), .EndTime = e.NewValues("EndTime")})
        End If


    End Sub

    Private Function GetCustomEvents() As CustomEventList
        Dim events As New CustomEventList '= TryCast(Session("CustomEventListData"), CustomEventList)

        Dim startdate As Date = ASPxScheduler1.Start
        'Dim enddate As Date = ASPxScheduler1.ran'

        For Each x In
                FMS.Business.DataObjects.CustomEvent.GetFortimeRange(FMS.Business.ThisSession.ApplicationID, Now.AddDays(-10), Now.AddDays(10))
            events.Add(x)
        Next

        If events Is Nothing Then
            events = New CustomEventList()
            Session("CustomEventListData") = events
        End If
        Return events
    End Function

    Protected Sub appointmentDataSource_ObjectCreated(sender As Object, e As ObjectDataSourceEventArgs) Handles appointmentDataSource.ObjectCreated
        Me.objectInstance = New CustomEventDataSource(GetCustomEvents())
        e.ObjectInstance = Me.objectInstance
    End Sub

    Private objectResourceInstance As CustomResourceDataSource

    Protected Sub odsResources_ObjectCreated(sender As Object, e As ObjectDataSourceEventArgs) Handles odsResources.ObjectCreated
        Me.objectResourceInstance = New CustomResourceDataSource(GetCustomResources())
        e.ObjectInstance = Me.objectResourceInstance
    End Sub

    Private Function GetCustomResources() As CustomResourceList

        'ThisSession.ApplicationID = New Guid("9B8CC16F-B045-42F8-A53E-1FAFB955232F")

        Dim resources As CustomResourceList = TryCast(Session("CustomResourceListData"), CustomResourceList)
        If resources Is Nothing Then
            resources = New CustomResourceList()

            Dim custresourcelist As List(Of FMS.Business.DataObjects.CustomResource) = _
                        FMS.Business.DataObjects.ApplicationVehicle.GetAllAsScheduleResources(FMS.Business.ThisSession.ApplicationID)

            resources.AddRange(custresourcelist)
            'ResourceHelper.FillObjectDataSourceWithResources(resources, 3)
            Session("CustomResourceListData") = resources
        End If
        Return resources
    End Function


    Protected Sub ASPxScheduler1_AppointmentFormShowing(sender As Object, e As DevExpress.Web.ASPxScheduler.AppointmentFormEventArgs) Handles ASPxScheduler1.AppointmentFormShowing

        e.Container = New DriverAppointmentFormTemplateContainer(DirectCast(sender, ASPxScheduler))

    End Sub

    Protected Sub ASPxScheduler1_BeforeExecuteCallbackCommand(sender As Object, e As SchedulerCallbackCommandEventArgs) Handles ASPxScheduler1.BeforeExecuteCallbackCommand

        If e.CommandId = SchedulerCallbackCommandId.AppointmentSave Then _
                    e.Command = New DriverAppointmentSaveCallbackCommand(DirectCast(sender, ASPxScheduler))

    End Sub
End Class