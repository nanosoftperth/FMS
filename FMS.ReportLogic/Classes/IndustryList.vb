Public Class CacheIndustryList
    Public Property Param As String
    Public Property LineValies As List(Of IndustryList)
End Class
Public Class IndustryList
    Public Property CustomerName As String
    Public Property SiteName As String
    Public Property SiteCeaseDate As System.Nullable(Of Date)
    Public Property ServiceUnits As System.Nullable(Of Single)
    Public Property ServiceDescription As String
    Public Property ServicePrice As System.Nullable(Of Single)
    Public Property PerAnnumCharge As System.Nullable(Of Single)
    Public Property SiteName1 As String
    Public Property Aid As Integer
    Public Property IndustryDescription As String
    Public Property UnitsHaveMoreThanOneRun As Boolean
    Public Property Frequency As String
    Public Property InvoiceCommencing As System.Nullable(Of Date)
    Public Property PostCode As System.Nullable(Of Short)
    Public Property MYOBCustomerNumber As String
End Class
