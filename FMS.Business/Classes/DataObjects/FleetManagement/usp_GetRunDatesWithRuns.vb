Namespace DataObjects

    Public Class usp_GetRunDatesWithRuns
#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property DateOfRun As Date
        Public Property Rid As Integer
        Public Property RunDescription As String


#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(obj As FMS.Business.usp_GetRunDatesWithRunsResult)
            With obj
                Me.ApplicationId = .ApplicationID
                Me.Rid = .Rid
                Me.RunDescription = .RunDescription
                Me.DateOfRun = .DateOfRun

            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll(TransDate As Date) As List(Of DataObjects.usp_GetRunDatesWithRuns)
            Try
                Dim appId = ThisSession.ApplicationID
                Dim obj As New List(Of DataObjects.usp_GetRunDatesWithRuns)

                With New LINQtoSQLClassesDataContext
                    obj = (From r In .usp_GetRunDatesWithRuns
                           Where r.DateOfRun = TransDate
                           Order By r.RunDescription
                           Select New DataObjects.usp_GetRunDatesWithRuns(r)).ToList()
                    .Dispose()
                End With

                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function

#End Region


    End Class

End Namespace


