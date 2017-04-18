Namespace DataObjects

    Public Class CanValue

#Region "property definitions & constructors"

        Public Property RawValue As Int64
        Public Property Time As DateTime
        Public Property Value As Object

        Public Sub New()

        End Sub

        Public Sub New(name As String, rawvalue As Double, time As DateTime)

            Me.RawValue = rawvalue
            Me.Time = time
        End Sub

#End Region


        Public Sub CalculateValue(spn As Integer, standard As String)

            Select Case spn

                Case 1 : Me.Value = _zagro_1(Me.RawValue)
                Case 2 : Me.Value = _zagro_1(Me.RawValue)
                Case 3 : Me.Value = _zagro_1(Me.RawValue)
                Case 4 : Me.Value = _zagro_1(Me.RawValue)
                Case 5 : Me.Value = _zagro_1(Me.RawValue)
                Case 6 : Me.Value = _zagro_1(Me.RawValue)
                Case 7 : Me.Value = _zagro_1(Me.RawValue)


                Case 190 : Me.Value = _zagro_1(Me.RawValue)

                Case Else : Value = "not implemented"

            End Select

        End Sub

        Private Function _190(val As Int64) As Object

            Return False

        End Function

        Private Function _zagro_1(val As Int64) As Object

            Dim b() As Byte = BitConverter.GetBytes(val)

            Dim bytes = b.Reverse()


            Dim int1 As Single = Convert.ToSingle(bytes(0))
            Dim int2 As Single = Convert.ToSingle(bytes(1))
            Return int1 + (int2 * 256)

        End Function


    End Class


End Namespace
