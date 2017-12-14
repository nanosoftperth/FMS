Namespace DataObjects
    Public Class usp_GetTblRuns
#Region "Properties / enums"
        Public Property RunID As System.Guid
        Public Property Rid As Integer
        Public Property RunNUmber As System.Nullable(Of Integer)
        Public Property RunDescription As String
        Public Property RunDriver As System.Nullable(Of Short)
        Public Property MondayRun As Boolean
        Public Property TuesdayRun As Boolean
        Public Property WednesdayRun As Boolean
        Public Property ThursdayRun As Boolean
        Public Property FridayRun As Boolean
        Public Property SaturdayRun As Boolean
        Public Property SundayRun As Boolean
        Public Property InactiveRun As Boolean
        Public Property InactiveRun1 As Boolean
#End Region
#Region "Get methods"
        Public Shared Function GetTblRuns(SpecificRun As String) As List(Of DataObjects.usp_GetTblRuns)
            Dim objTblRun = (From c In SingletonAccess.FMSDataContextContignous.usp_GetTblRuns(SpecificRun)
                                          Select New DataObjects.usp_GetTblRuns(c)).ToList
            Return objTblRun
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTblRuns As FMS.Business.usp_GetTblRunsResult)
            With objTblRuns
                Me.RunID = .RunID
                Me.Rid = .Rid
                Me.RunNUmber = .RunNUmber
                Me.RunDescription = .RunDescription
                Me.RunDriver = .RunDriver
                Me.MondayRun = .MondayRun
                Me.TuesdayRun = .TuesdayRun
                Me.WednesdayRun = .WednesdayRun
                Me.ThursdayRun = .ThursdayRun
                Me.FridayRun = .FridayRun
                Me.SaturdayRun = .SaturdayRun
                Me.SundayRun = .SundayRun
                Me.InactiveRun = .InactiveRun
                Me.InactiveRun1 = .InactiveRun1
            End With
        End Sub
#End Region
    End Class
End Namespace

