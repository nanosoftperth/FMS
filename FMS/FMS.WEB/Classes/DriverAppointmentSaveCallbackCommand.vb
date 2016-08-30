Imports DevExpress.Web
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler

Public Class DriverAppointmentSaveCallbackCommand
    Inherits AppointmentFormSaveCallbackCommand

    Public Sub New(ByVal control As ASPxScheduler)
        MyBase.New(control)
    End Sub
    Protected Friend Shadows ReadOnly Property Controller() As DriverAppointmentFormController
        Get
            Return CType(MyBase.Controller, DriverAppointmentFormController)
        End Get
    End Property

    Protected Overrides Sub AssignControllerValues()
        Dim tbField1 As ASPxTextBox = CType(FindControlByID("tbField1"), ASPxTextBox)
        ' Dim memField2 As ASPxMemo = CType(FindControlByID("memField2"), ASPxMemo)
        'Controller.Field1 = System.Convert.ToDouble(tbField1.Text)
        'Controller.Field2 = memField2.Text
        MyBase.AssignControllerValues()
    End Sub
    Protected Overrides Function CreateAppointmentFormController(ByVal apt As Appointment) As AppointmentFormController
        Return New DriverAppointmentFormController(Control, apt)
    End Function
End Class