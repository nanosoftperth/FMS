Namespace DataObjects
    Public Class usp_GetRateIncreaseCuaReport
#Region "Properties / enums"
        Public Property RateIncreasesID As System.Guid
        Public Property Aid As Integer
        Public Property SiteName As String
        Public Property CustomerName As String
        Public Property CSid As System.Nullable(Of Integer)
        Public Property Units As System.Nullable(Of Short)
        Public Property OldServicePrice As System.Nullable(Of Single)
        Public Property NewServicePrice As System.Nullable(Of Single)
        Public Property OldPerAnnumCharge As System.Nullable(Of Single)
        Public Property NewPerAnnumCharge As System.Nullable(Of Single)
        Public Property CustomerID As System.Nullable(Of Integer)
        Public Property SiteID As System.Nullable(Of Integer)
        Public Property Invfreq As System.Nullable(Of Integer)
        Public Property InvStartDate As System.Nullable(Of Date)
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property ServiceDescription As String
#End Region
#Region "Get methods"
        Public Shared Function GetCuaRateIncreaseReport() As List(Of DataObjects.usp_GetRateIncreaseCuaReport)
            Try
                Dim objRunValueSummary As New List(Of DataObjects.usp_GetRateIncreaseCuaReport)
                With New LINQtoSQLClassesDataContext
                    objRunValueSummary = (From c In .usp_GetRateIncreaseCuaReport.Where(Function(x) x.ApplicationID.Equals(ThisSession.ApplicationID))
                                          Select New DataObjects.usp_GetRateIncreaseCuaReport(c)).ToList
                    .Dispose()
                End With
                Return objRunValueSummary
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRateIncreaseCuaReport As FMS.Business.usp_GetRateIncreaseCuaReportResult)
            With objRateIncreaseCuaReport
                Me.RateIncreasesID = .RateIncreasesID
                Me.Aid = .Aid
                Me.SiteName = .SiteName
                Me.CustomerName = .CustomerName
                Me.CSid = .CSid
                Me.Units = .Units
                Me.OldServicePrice = .OldServicePrice
                Me.NewServicePrice = .NewServicePrice
                Me.OldPerAnnumCharge = .OldPerAnnumCharge
                Me.NewPerAnnumCharge = .NewPerAnnumCharge
                Me.CustomerID = .CustomerID
                Me.SiteID = .SiteID
                Me.Invfreq = .Invfreq
                Me.InvStartDate = .InvStartDate
                Me.ApplicationID = .ApplicationID
                Me.ServiceDescription = .ServiceDescription
            End With
        End Sub
#End Region
    End Class
End Namespace

