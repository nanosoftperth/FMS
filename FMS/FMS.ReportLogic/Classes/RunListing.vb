Public Class CacheRunListing
    Public Property LineValues As List(Of RunListing)
End Class
Public Class RunListing
    Public Property RunNUmber As System.Nullable(Of Integer)
    Public Property RunNum As String
    Public Property RunDescription As String
    Public Property RunDriver As System.Nullable(Of Short)
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
End Class
