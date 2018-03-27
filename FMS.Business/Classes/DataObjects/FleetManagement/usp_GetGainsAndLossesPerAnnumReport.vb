Namespace DataObjects
    Public Class usp_GetGainsAndLossesPerAnnumReport
#Region "Properties / enums"
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
#End Region
#Region "Get methods"
        Public Shared Function GetGainsAndLossesPerAnnumReport(sDate As Date, eDate As Date) As List(Of DataObjects.usp_GetGainsAndLossesPerAnnumReport)
            Try
                Dim objGainsAndLossesPerAnnum As New List(Of DataObjects.usp_GetGainsAndLossesPerAnnumReport)
                With New LINQtoSQLClassesDataContext
                    .CommandTimeout = 180
                    objGainsAndLossesPerAnnum = (From c In .usp_GetGainsAndLossesPerAnnumReport(sDate, eDate, ThisSession.ApplicationID)
                                                 Select New DataObjects.usp_GetGainsAndLossesPerAnnumReport(c)).ToList
                    .Dispose()
                End With
                Return objGainsAndLossesPerAnnum
            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objGainsAndLossesPerAnnum As FMS.Business.usp_GetGainsAndLossesPerAnnumReportResult)
            With objGainsAndLossesPerAnnum
                Me.SalesPerson = .SalesPerson
                Me.Site = .Site
                Me.EffectiveDate = .EffectiveDate
                Me.ServiceCode = .ServiceCode
                Me.ServiceDescription = .ServiceDescription
                Me.OldServiceUnits = .OldServiceUnits
                Me.NewServiceUnits = .NewServiceUnits
                Me.UnitsDiff = .UnitsDiff
                Me.UnitType = .UnitType
                Me.OldPerAnnumCharge = .OldPerAnnumCharge
                Me.NewPerAnnumCharge = .NewPerAnnumCharge
                Me.PADiff = .PADiff
                Me.PAType = .PAType
                Me.ChangeDate = .ChangeDate
            End With
        End Sub
#End Region
    End Class
End Namespace
