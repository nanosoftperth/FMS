Namespace DataObjects
    Public Class usp_GetServiceSummaryReport
#Region "Properties / enums"
        Public Property FrequencyDescription As String
        Public Property ServiceDescription As String
        Public Property ServiceCode As String
        Public Property ServiceUnits As System.Nullable(Of Double)
        Public Property SiteCeaseDate As System.Nullable(Of Date)
#End Region
#Region "Get methods"
        Public Shared Function GetServiceSummay() As List(Of DataObjects.usp_GetServiceSummaryReport)
            Try
                Dim objServiceSummary As New List(Of DataObjects.usp_GetServiceSummaryReport)
                With New LINQtoSQLClassesDataContext
                    objServiceSummary = (From c In .usp_GetServiceSummaryReport(ThisSession.ApplicationID)
                                         Select New DataObjects.usp_GetServiceSummaryReport(c)).ToList
                    .Dispose()
                End With
                Return objServiceSummary
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(ServiceSummaryReport As FMS.Business.usp_GetServiceSummaryReportResult)
            With ServiceSummaryReport
                Me.FrequencyDescription = .FrequencyDescription
                Me.ServiceDescription = .ServiceDescription
                Me.ServiceCode = .ServiceCode
                Me.ServiceUnits = .ServiceUnits
                Me.SiteCeaseDate = .SiteCeaseDate
            End With
        End Sub
#End Region
    End Class
End Namespace


