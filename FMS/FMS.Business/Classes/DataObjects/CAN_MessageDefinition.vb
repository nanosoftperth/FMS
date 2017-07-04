﻿
Namespace DataObjects

    Public Class CAN_MessageDefinition


        Public Property Standard As String
        Public Property PGN As String
        Public Property SPN As Integer

        Public Property Acronym As String
        Public Property Description As String
        Public Property Resolution As String
        Public Property Units As String
        Public Property offset As Integer
        Public Property pos As String
        Public Property SPN_Length As String
        Public Property PGN_Length As Integer

        Public Property Data_Range As String

        Private _pos_start As Decimal
        Private _pos_end As Decimal
        Private _Resolution_Multiplier As Decimal

        Public ReadOnly Property Resolution_Multiplier As Decimal
            Get
                Return _Resolution_Multiplier
            End Get
        End Property


        Public ReadOnly Property pos_start As Decimal
            Get
                Return _pos_start
            End Get
        End Property

        Public ReadOnly Property pos_end As Decimal
            Get
                Return _pos_end
            End Get
        End Property


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
                    If .Offset IsNot Nothing Then Me.offset = .Offset
                    If .pos IsNot Nothing Then Me.pos = .pos
                    If .SPN_length IsNot Nothing Then Me.SPN_Length = .SPN_length
                    If .Units IsNot Nothing Then Me.Units = .Units

                    If .Data_Range IsNot Nothing Then Me.Data_Range = .Data_Range

                    'calculate position start and end 
                    If Not String.IsNullOrEmpty(Me.pos) Then
                        Me._pos_start = Decimal.Parse(Me.pos.Split("-")(0).Trim)
                        If Me.pos.Contains("-") Then Me._pos_end = Decimal.Parse(Me.pos.Split("-")(1).Trim)

                        If pos_end = 0 Then _pos_end = pos_start + (CInt(SPN_Length / 8) - 1)
                    End If

                    If Not String.IsNullOrEmpty(Me.Resolution) Then Me._Resolution_Multiplier = Decimal.Parse(Me.Resolution.Split(" "c)(0))

                Catch ex As Exception

                    Dim msg As String = "asd"
                End Try



            End With

        End Sub

        Public Sub New()

        End Sub

        Public Shared Function GetForPGN(pgn As Integer, _
                                         standard As String) _
                                                         As List(Of Business.DataObjects.CAN_MessageDefinition)

            Dim retobj As New List(Of DataObjects.CAN_MessageDefinition)



            With New LINQtoSQLClassesDataContext
                .SubmitChanges()

                retobj = (From x In .CAN_MessageDefinitions
                          Where x.PGN.HasValue AndAlso x.PGN = pgn
                          Select New DataObjects.CAN_MessageDefinition(x)).ToList


                .Dispose()
            End With

            Return retobj


        End Function

        Public Shared Function GetForSPN(spn As Integer, standard As String) As Business.DataObjects.CAN_MessageDefinition

            Dim retobj As DataObjects.CAN_MessageDefinition

            With New LINQtoSQLClassesDataContext

                .SubmitChanges()


                retobj = (From x In .CAN_MessageDefinitions
                      Where x.SPN.HasValue AndAlso x.SPN = spn AndAlso x.Standard = standard
                      Select New DataObjects.CAN_MessageDefinition(x)).FirstOrDefault

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