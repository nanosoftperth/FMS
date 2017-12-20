Public Class CacheGenerateRunSheetSummary
    Public Property LineValues As List(Of GenerateRunSheetSummary)
    Public Property ParamDate As String
    Public Property ParamDay As String
    Public Property ParamMessage As String
End Class
Public Class GenerateRunSheetSummary
    Public Property ServiceCode As String
    Public Property ServiceDescription As String
    Public Property SumOfServiceUnits As System.Nullable(Of Double)
End Class
