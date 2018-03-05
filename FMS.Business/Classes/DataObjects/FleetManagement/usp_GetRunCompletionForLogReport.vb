Namespace DataObjects
    Public Class usp_GetRunCompletionForLogReport
#Region "Properties / enums"
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property RunCompletionID As System.Nullable(Of System.Guid)
        Public Property RunID As System.Nullable(Of System.Guid)
        Public Property DriverID As System.Nullable(Of System.Guid)
        Public Property RunDate As Date
        Public Property Notes As String
        Public Property RunNumber As System.Nullable(Of Integer)
        Public Property RunDescription As String
        Public Property DID As System.Nullable(Of Integer)
        Public Property DriverName As String
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRun As FMS.Business.usp_GetRunCompletionForLogReportResult)
            With objRun
                Me.ApplicationID = .ApplicationID
                Me.RunCompletionID = .RunCompletionID
                Me.RunID = .RunID
                Me.DriverID = .DriverID
                Me.RunDate = .RunDate
                Me.Notes = .Notes
                Me.RunNumber = .RunNUmber
                Me.RunDescription = .RunDescription
                Me.DID = .Did
                Me.DriverName = DriverName

            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.usp_GetRunCompletionForLogReport)
            Try
                Dim fleetRunCompletions As New List(Of DataObjects.usp_GetRunCompletionForLogReport)

                With New LINQtoSQLClassesDataContext
                    fleetRunCompletions = (From i In .usp_GetRunCompletionForLogReport
                                           Select New DataObjects.usp_GetRunCompletionForLogReport(i)).ToList()
                    .Dispose()
                End With

                Return fleetRunCompletions
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region


    End Class
End Namespace

