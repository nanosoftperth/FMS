Namespace DataObjects

    Public Class usp_GetUnAssignedRuns

#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property Rid As Integer
        Public Property DateOfRun As Date
        Public Property RunNUmber As Integer
        Public Property RunDescription As String
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(obj As FMS.Business.usp_GetUnAssignedRunsResult)
            With obj
                Me.ApplicationId = .ApplicationID
                Me.Rid = .Rid
                Me.DateOfRun = .DateOfRun
                Me.RunNUmber = .RunNUmber
                Me.RunDescription = .RunDescription
            End With
        End Sub
#End Region

#Region "Get methods"

        Public Shared Function GetAllPerApplication(StartDate As Date, EndDate As Date) As List(Of DataObjects.usp_GetUnAssignedRuns)
            Try
                Dim appId = ThisSession.ApplicationID
                Dim obj As New List(Of DataObjects.usp_GetUnAssignedRuns)
                With New LINQtoSQLClassesDataContext
                    obj = (From r In .usp_GetUnAssignedRuns(appId, StartDate, EndDate)
                           Order By r.RunDescription
                           Select New DataObjects.usp_GetUnAssignedRuns(r)).ToList
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

