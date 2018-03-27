Namespace DataObjects
    Public Class usp_GetSitesWithNoContractsReport
#Region "Properties / enums"
        Public Property CustomerName As String
        Public Property SiteName As String
        Public Property SitePeriod As System.Nullable(Of Integer)
        Public Property SiteStartDate As System.Nullable(Of Date)
        Public Property ContractPeriodDesc As String
        Public Property SiteCeaseDate As System.Nullable(Of Date)
#End Region
#Region "Get methods"
        Public Shared Function GetSitesWithNoContract() As List(Of DataObjects.usp_GetSitesWithNoContractsReport)
            Try
                Dim objSitesWithNoContract As New List(Of DataObjects.usp_GetSitesWithNoContractsReport)
                With New LINQtoSQLClassesDataContext
                    objSitesWithNoContract = (From c In .usp_GetSitesWithNoContractsReport(ThisSession.ApplicationID)
                                              Select New DataObjects.usp_GetSitesWithNoContractsReport(c)).ToList
                    .Dispose()
                End With
                Return objSitesWithNoContract
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(SitesWithNoContractsReport As FMS.Business.usp_GetSitesWithNoContractsReportResult)
            With SitesWithNoContractsReport
                Me.CustomerName = .CustomerName
                Me.SiteName = .SiteName
                Me.SitePeriod = .SitePeriod
                Me.SiteStartDate = .SiteStartDate
                Me.ContractPeriodDesc = .ContractPeriodDesc
                Me.SiteCeaseDate = .SiteCeaseDate
            End With
        End Sub
#End Region
    End Class
End Namespace

