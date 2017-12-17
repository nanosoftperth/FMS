Namespace DataObjects
    Public Class usp_GetGenerateRunSheetSummary
#Region "Properties / enums"
        Public Property ServiceCode As String
        Public Property ServiceDescription As String
        Public Property SumOfServiceUnits As System.Nullable(Of Double)
#End Region
#Region "Get methods"
        Public Shared Function GetGenerateRunSheetSummary() As List(Of DataObjects.usp_GetGenerateRunSheetSummary)
            Dim objGenerateRunSheetSummary = (From c In SingletonAccess.FMSDataContextContignous.usp_GetGenerateRunSheetSummary
                            Select New DataObjects.usp_GetGenerateRunSheetSummary(c)).ToList
            Return objGenerateRunSheetSummary
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objGenerateRunSheetSummary As FMS.Business.usp_GetGenerateRunSheetSummaryResult)
            With objGenerateRunSheetSummary
                Me.ServiceCode = .ServiceCode
                Me.ServiceDescription = .ServiceDescription
                Me.SumOfServiceUnits = .SumOfServiceUnits
            End With
        End Sub
#End Region
    End Class
End Namespace

