Namespace DataObjects
    Public Class usp_GetRunsForAssignment
#Region "Properties / enums"
        Public Property UniqueID As String
        Public Property Rid As System.Nullable(Of Integer)
        Public Property RunNUmber As System.Nullable(Of Integer)
        Public Property RunDescription As String
        Public Property DriverId As System.Nullable(Of Integer)
        Public Property DriverName As String
        Public Property DateOfRun As System.Nullable(Of Date)
#End Region
#Region "CRUD"
        Public Shared Sub Create(GetRunsForAssignment As DataObjects.usp_GetRunsForAssignment)
            With New LINQtoSQLClassesDataContext

                .Dispose()

            End With
        End Sub
        Public Shared Sub Update(GetRunsForAssignment As DataObjects.usp_GetRunsForAssignment)
            Dim runDates As New tblRunDates()
            Dim Rid As Integer = GetRunsForAssignment.UniqueID.Split("-")(1)
            GetRunsForAssignment.Rid = Rid
            Dim runDatesByRunID = tblRunDates.GetRunDatesByRunID(GetRunsForAssignment.Rid)
            Dim blnRunDates As Boolean = True
            If GetRunsForAssignment.DateOfRun Is Nothing Then
                Exit Sub
            End If
            runDates.Rid = GetRunsForAssignment.Rid
            runDates.DateOfRun = GetRunsForAssignment.DateOfRun
            runDates.Driver = GetRunsForAssignment.DriverId
            For Each rdates In runDatesByRunID
                If rdates.DateOfRun.Equals(GetRunsForAssignment.DateOfRun) Then
                    runDates.DRid = rdates.DRid
                    blnRunDates = False
                    Exit For
                End If
            Next
            If Not blnRunDates Then
                tblRunDates.UpdateByRunIDAndDrid(runDates)
            Else
                tblRunDates.Create(runDates)
            End If
        End Sub
        Public Shared Sub Delete(GetRunsForAssignment As DataObjects.usp_GetRunsForAssignment)
            With New LINQtoSQLClassesDataContext

                .Dispose()
            End With

        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.usp_GetRunsForAssignment)
            Try
                Dim objGetRunsForAssignment As New List(Of DataObjects.usp_GetRunsForAssignment)

                With New LINQtoSQLClassesDataContext
                    objGetRunsForAssignment = (From c In .usp_GetRunsForAssignment
                                               Order By c.RunDescription
                                               Select New DataObjects.usp_GetRunsForAssignment(c)).ToList
                    .Dispose()
                End With

                Return objGetRunsForAssignment

            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objGetRunsForAssignment As FMS.Business.usp_GetRunsForAssignmentResult)
            With objGetRunsForAssignment
                Me.UniqueID = .UniqueID
                Me.Rid = .Rid
                Me.RunNUmber = .RunNUmber
                Me.RunDescription = .RunDescription
                Me.DriverId = .DriverId
                Me.DriverName = .DriverName
                Me.DateOfRun = .DateOfRun
            End With
        End Sub
#End Region
    End Class
End Namespace

