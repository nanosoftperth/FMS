Namespace DataObjects

    Public Class CanValue

#Region "property definitions & constructors"

        Public Property RawValue As Double
        Public Property Time As DateTime
        Public Property Value As Object

        Public Sub New()

        End Sub

        Public Sub New(name As String, rawvalue As Double, time As DateTime)

            Me.RawValue = rawvalue
            Me.Time = time
        End Sub

#End Region


        Public Sub CalculateValue(spn As Integer, Optional desc As String = "")

            Select Case spn

                Case 578

                    Select Case desc

                        Case "Speed" : Me.Value = _578_speed(Me.RawValue)
                        Case "Parking Break"
                        Case "Horn"
                        Case "Beacon Operatio:"

                    End Select

                Case Else : Value = "not implemented"

            End Select

        End Sub


        Private Function _578_speed(val As Int64) As Object

            Dim b() As Byte = BitConverter.GetBytes(val)

            Dim bytes = b.Reverse()


            Dim int1 As Single = Convert.ToSingle(bytes(0))
            Dim int2 As Single = Convert.ToSingle(bytes(1))
            Return int1 + (int2 * 256)

        End Function


    End Class


End Namespace
