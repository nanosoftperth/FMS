
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

                Try
                    If .Standard IsNot Nothing Then Me.Standard = .Standard
                    If .PGN IsNot Nothing Then Me.PGN = .PGN
                    If .SPN IsNot Nothing Then Me.SPN = .SPN
                    If .PGN_Length IsNot Nothing Then Me.PGN_Length = .PGN_Length
                    If .Acronym IsNot Nothing Then Me.Acronym = .Acronym
                    If .Description IsNot Nothing Then Me.Description = .Description
                    If .Resolution IsNot Nothing Then Me.Resolution = .Resolution
                    If .Units IsNot Nothing Then Me.Units = .Units
                Catch ex As Exception

                    Dim msg As String = "asd"
                End Try

               

            End With

        End Sub

        Public Sub New()

        End Sub

        Public Shared Function GetForSPN(spn As Integer, _
                                        standard As String, _
                                        Optional description As String = "") _
                                                        As List(Of Business.DataObjects.CAN_MessageDefinition)

            Dim retobj As New List(Of DataObjects.CAN_MessageDefinition)



            With New LINQtoSQLClassesDataContext
                .SubmitChanges()

                If String.IsNullOrEmpty(description) Then

                    retobj = (From x In .CAN_MessageDefinitions
                          Where x.SPN.HasValue AndAlso x.SPN = spn
                          Select New DataObjects.CAN_MessageDefinition(x)).ToList
                Else
                    retobj = (From x In .CAN_MessageDefinitions
                          Where x.SPN.HasValue AndAlso x.SPN = spn AndAlso x.Description = description
                          Select New DataObjects.CAN_MessageDefinition(x)).ToList
                End If



                .Dispose()
            End With

            Return retobj

        End Function

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