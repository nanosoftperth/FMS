Namespace DataObjects

    Public Class CanDataPoint

#Region "properties & constructors"

        Public Const TAG_STRING_FORMAT As String = "CAN_{0}_{1}"

        Public Property MessageDefinition As DataObjects.CAN_MessageDefinition

        Public Property CanValues As New CanValueList

        Public Sub New()

        End Sub

#End Region


        Public Class CanValueList
            Inherits List(Of CanValue)

            Public Sub CalculateValues(SPN As Integer)

                For Each cv As CanValue In Me
                    cv.CalculateValue(SPN)
                Next

            End Sub


        End Class


    End Class

End Namespace


