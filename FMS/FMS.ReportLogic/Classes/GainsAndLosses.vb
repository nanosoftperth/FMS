Public Class CacheGainsAndLosses
    Public Property Param1 As String
    Public Property Param2 As String
    Public Property LineValues As List(Of GainsAndLosses)
End Class
Public Class GainsAndLosses
    Public Property SalesPerson As String
    Public Property Site As String
    Public Property EffectiveDate As System.Nullable(Of Date)
    Public Property ServiceCode As String
    Public Property ServiceDescription As String
    Public Property OldServiceUnits As System.Nullable(Of Double)
    Public Property NewServiceUnits As System.Nullable(Of Double)
    Public Property UnitsDiff As System.Nullable(Of Double)
    Public Property UnitType As String
    Public Property OldPerAnnumCharge As System.Nullable(Of Double)
    Public Property NewPerAnnumCharge As System.Nullable(Of Double)
    Public Property PADiff As System.Nullable(Of Double)
    Public Property PAType As String
    Public Property ChangeDate As System.Nullable(Of Date)
End Class
