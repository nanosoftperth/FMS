Public Class CacheGenerateRunSheetsDetail
    Public Property LineValues As List(Of GenerateRunSheetsDetail)
    Public Property ParamDate As String
    Public Property ParamDay As String
    Public Property ParamSignature As String
End Class
Public Class GenerateRunSheetsDetail
    Public Property SortOrder As String
    Public Property Cid As System.Nullable(Of Integer)
    Public Property SiteName As String
    Public Property Add As String
    Public Property Suburb As String
    Public Property SiteContactName As String
    Public Property SiteContactPhone As String
    Public Property SiteContactMobile As String
    Public Property RunDriver As System.Nullable(Of Integer)
    Public Property GeneralSiteServiceComments As String
    Public Property RunNumber As System.Nullable(Of Integer)
    Public Property DriverName As String
    Public Property RunDescription As String
    Public Property Notes As String
End Class
