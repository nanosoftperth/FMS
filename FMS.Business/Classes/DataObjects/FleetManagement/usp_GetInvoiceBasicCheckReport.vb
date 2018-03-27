Namespace DataObjects
    Public Class usp_GetInvoiceBasicCheckReport
#Region "Properties / enums"
        Public Property CustomerName As String
        Public Property SiteName As String
        Public Property SiteCeaseDate As System.Nullable(Of Date)
        Public Property Frequency As String
        Public Property InvoiceCommencing As System.Nullable(Of Date)
        Public Property MonthDescription As String
#End Region
#Region "Get methods"
        Public Shared Function GetInvoiceBasicCheckReport() As List(Of DataObjects.usp_GetInvoiceBasicCheckReport)
            Try
                Dim objInvoiveBasicCheckReport As New List(Of DataObjects.usp_GetInvoiceBasicCheckReport)
                With New LINQtoSQLClassesDataContext
                    objInvoiveBasicCheckReport = (From c In .usp_GetInvoiceBasicCheckReport(ThisSession.ApplicationID)
                                                  Select New DataObjects.usp_GetInvoiceBasicCheckReport(c)).ToList
                    .Dispose()
                End With
                Return objInvoiveBasicCheckReport
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objInvoiceBasicCheck As FMS.Business.usp_GetInvoiceBasicCheckReportResult)
            With objInvoiceBasicCheck
                Me.CustomerName = .CustomerName
                Me.SiteName = .SiteName
                Me.SiteCeaseDate = .SiteCeaseDate
                Me.Frequency = .Frequency
                Me.InvoiceCommencing = .InvoiceCommencing
                Me.MonthDescription = .MonthDescription
            End With
        End Sub
#End Region
    End Class
End Namespace


