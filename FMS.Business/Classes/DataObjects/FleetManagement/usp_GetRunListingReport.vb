Namespace DataObjects
    Public Class usp_GetRunListingReport
#Region "Properties / enums"
        Public Property RunNUmber As System.Nullable(Of Integer)
        Public Property RunNum As String
        Public Property RunDescription As String
        Public Property RunDriver As System.Nullable(Of Integer)
        Public Property DriverName As String
        Public Property MondayRun As Boolean
        Public Property TuesdayRun As Boolean
        Public Property WednesdayRun As Boolean
        Public Property ThursdayRun As Boolean
        Public Property FridayRun As Boolean
        Public Property SaturdayRun As Boolean
        Public Property SundayRun As Boolean
        Public Property InactiveRun As Boolean
        Public Property Rid As Integer
        Public Property DateOfRun As System.Nullable(Of Date)
#End Region
#Region "Get methods"
        Public Shared Function GetRunListingReport() As List(Of DataObjects.usp_GetRunListingReport)
            Try
                Dim objRunListing As New List(Of DataObjects.usp_GetRunListingReport)
                With New LINQtoSQLClassesDataContext
                    .CommandTimeout = 180
                    objRunListing = (From c In .usp_GetRunListingReport(ThisSession.ApplicationID)
                                     Select New DataObjects.usp_GetRunListingReport(c)).ToList
                    .Dispose()
                End With
                Return objRunListing
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRunListingReport As FMS.Business.usp_GetRunListingReportResult)
            With objRunListingReport
                Me.RunNum = .RunNUmber
                Me.RunNum = .RunNum
                Me.RunDescription = .RunDescription
                Me.RunDriver = .RunDriver
                Me.DriverName = .DriverName
                Me.MondayRun = .MondayRun
                Me.TuesdayRun = .TuesdayRun
                Me.WednesdayRun = .WednesdayRun
                Me.ThursdayRun = .ThursdayRun
                Me.FridayRun = .FridayRun
                Me.SaturdayRun = .SaturdayRun
                Me.SundayRun = .SundayRun
                Me.InactiveRun = .InactiveRun
                Me.Rid = .Rid
            End With
        End Sub
#End Region
    End Class
End Namespace

