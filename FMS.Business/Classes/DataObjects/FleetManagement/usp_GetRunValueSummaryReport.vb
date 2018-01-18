Namespace DataObjects
    Public Class usp_GetRunValueSummaryReport
#Region "Properties / enums"
        Public Property RunDescription As String
        Public Property SumOfPerAnnumCharge As System.Nullable(Of Double)
#End Region
#Region "Get methods"
        Public Shared Function GetRunValueSummaryReport() As List(Of DataObjects.usp_GetRunValueSummaryReport)
            Dim objRunValueSummary = (From c In SingletonAccess.FMSDataContextContignous.usp_GetRunValueSummaryReport(ThisSession.ApplicationID)
                                      Select New DataObjects.usp_GetRunValueSummaryReport(c)).ToList
            Return objRunValueSummary
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRunValueSummary As FMS.Business.usp_GetRunValueSummaryReportResult)
            With objRunValueSummary
                Me.RunDescription = .RunDescription
                Me.SumOfPerAnnumCharge = .SumOfPerAnnumCharge
            End With
        End Sub
#End Region
    End Class
End Namespace

