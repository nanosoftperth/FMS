Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler

Public Class DriverAppointmentFormController
    Inherits AppointmentFormController

    Public Sub New(ByVal control As ASPxScheduler, ByVal apt As Appointment)
        MyBase.New(control, apt)
    End Sub
    Public Property Field1() As Double
        Get
            Return CDbl(EditedAppointmentCopy.CustomFields("Field1"))
        End Get
        Set(ByVal value As Double)
            EditedAppointmentCopy.CustomFields("Field1") = value
        End Set
    End Property
    Public Property Field2() As String
        Get
            Return CStr(EditedAppointmentCopy.CustomFields("Field2"))
        End Get
        Set(ByVal value As String)
            EditedAppointmentCopy.CustomFields("Field2") = value
        End Set
    End Property
    Private Property SourceField1() As Double
        Get
            Return CDbl(SourceAppointment.CustomFields("Field1"))
        End Get
        Set(ByVal value As Double)
            SourceAppointment.CustomFields("Field1") = value
        End Set
    End Property
    Private Property SourceField2() As String
        Get
            Return CStr(SourceAppointment.CustomFields("Field2"))
        End Get
        Set(ByVal value As String)
            SourceAppointment.CustomFields("Field2") = value
        End Set
    End Property

End Class
