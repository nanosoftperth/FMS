Namespace DataObjects

    Public Class CanValue

#Region "property definitions & constructors"

        Public Property RawValue As String
        Public Property Time As DateTime
        Public Property Value As Object

        Public Sub New()

        End Sub

        Public Sub New(name As String, rawvalue As Double, time As DateTime)

            Me.RawValue = rawvalue
            Me.Time = time
        End Sub

#End Region


    End Class


End Namespace
