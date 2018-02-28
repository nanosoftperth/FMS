Namespace DataObjects
    Public Class usp_GetRunCompletionForLogReport

#Region "Properties / enums"
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property RunCompletionID As System.Guid
        Public Property RunID As System.Guid
        Public Property DriverID As System.Nullable(Of System.Guid)
        Public Property DID As System.Nullable(Of Integer)
        Public Property RunDate As System.Nullable(Of Date)
        Public Property Notes As String
        Public Property RunNumber As System.Nullable(Of Integer)
        Public Property RunDescription As String
        Public Property DriverName As String
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(obj As FMS.Business.usp_GetRunCompletionForLogReportResult)
            With obj
                Me.ApplicationID = .ApplicationID
                Me.RunCompletionID = .RunCompletionID
                Me.DriverID = .DriverID
                Me.RunID = .RunID
                Me.DID = .Did
                Me.RunDate = .RunDate
                Me.Notes = .Notes
                Me.RunNumber = .RunNUmber
                Me.RunDescription = .RunDescription
                Me.DriverName = .DriverName
            End With
        End Sub
#End Region

#Region "Get methods"

        Public Shared Function GetAll() As List(Of DataObjects.usp_GetRunCompletionForLogReport)
            Try
                Dim obj As New List(Of DataObjects.usp_GetRunCompletionForLogReport)

                With New LINQtoSQLClassesDataContext
                    obj = (From r In .usp_GetRunCompletionForLogReport
                           Order By r.RunDate
                           Select New DataObjects.usp_GetRunCompletionForLogReport(r)).ToList
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


