Namespace DataObjects
    Public Class usp_GetGenerateRunSheetSummary
#Region "Properties / enums"
        Public Property ServiceCode As String
        Public Property ServiceDescription As String
        Public Property SumOfServiceUnits As System.Nullable(Of Double)
#End Region
#Region "Get methods"
        Public Shared Function GetGenerateRunSheetSummary() As List(Of DataObjects.usp_GetGenerateRunSheetSummary)
            Try
                Dim objGenerateRunSheetSummary As New List(Of DataObjects.usp_GetGenerateRunSheetSummary)
                With New LINQtoSQLClassesDataContext
                    objGenerateRunSheetSummary = (From c In .usp_GetGenerateRunSheetSummary
                                                  Select New DataObjects.usp_GetGenerateRunSheetSummary(c)).ToList
                    .Dispose()
                End With
                Return objGenerateRunSheetSummary
            Catch ex As Exception
                Throw ex
            End Try
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

