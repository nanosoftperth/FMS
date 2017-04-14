
Namespace DataObjects

    Public Class CAN_MessageDefinition


        Public Property Standard As String
        Public Property PGN As String
        Public Property SPN As Integer
        Public Property PGN_Length As Integer
        Public Property Acronym As String
        Public Property Description As String
        Public Property Resolution As String
        Public Property Units As String


        Public Sub New(x As Business.CAN_MessageDefinition)

            With x

                Me.Standard = .Standard
                Me.PGN = .Standard
                Me.SPN = .Standard
                Me.PGN_Length = .Standard
                Me.Acronym = .Standard
                Me.Description = .Standard
                Me.Resolution = .Standard
                Me.Units = .Standard

            End With

        End Sub

        Public Sub New()

        End Sub

        Public Shared Function GetAll() As List(Of Business.DataObjects.CAN_MessageDefinition)

            Dim retobj As New List(Of DataObjects.CAN_MessageDefinition)

            With New LINQtoSQLClassesDataContext
                .SubmitChanges()
                retobj = (From x In .CAN_MessageDefinitions
                          Select New DataObjects.CAN_MessageDefinition(x)).ToList

                .Dispose()
            End With

            Return retobj

        End Function


        

    End Class


End Namespace