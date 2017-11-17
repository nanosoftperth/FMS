Namespace DataObjects

    Public Class CanValue

#Region "property definitions & constructors"

        Public Property RawValue As String
        Public Property Time As DateTime
        Public Property Value As Object

        Public Property IsValid As Boolean = True

        Public ReadOnly Property ValueStr As String
            Get
                Return CStr(Value)
            End Get
        End Property

        Public Sub New()

        End Sub

        

        Public Sub New(name As String, rawvalue As Double, time As DateTime, Optional isValid As Boolean = True)

            Me.RawValue = rawvalue
            Me.Time = time
            Me.IsValid = isValid

        End Sub

#End Region


    End Class


End Namespace
