Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.XtraScheduler
Public Class DriverAppointmentFormTemplateContainer
    Inherits AppointmentFormTemplateContainer


    Public Sub New(ByVal control As ASPxScheduler)
        MyBase.New(control)
    End Sub

    Public ReadOnly Property Field1() As Double
        Get
            Dim val As Object = Appointment.CustomFields("Field1")
            Return If(val Is System.DBNull.Value, 0, System.Convert.ToDouble(val))
        End Get
    End Property
    Public ReadOnly Property Field2() As String
        Get
            Return System.Convert.ToString(Appointment.CustomFields("Field2"))
        End Get
    End Property


End Class
