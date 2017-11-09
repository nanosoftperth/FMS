Namespace DataObjects
    Public Class usp_GetIndustryListReport
#Region "Properties / enums"
        Public Property CustomerName As String
        Public Property SiteName As String
        Public Property SiteCeaseDate As System.Nullable(Of Date)
        Public Property ServiceUnits As System.Nullable(Of Single)
        Public Property ServiceDescription As String
        Public Property ServicePrice As System.Nullable(Of Single)
        Public Property PerAnnumCharge As System.Nullable(Of Single)
        Public Property SiteName1 As String
        Public Property Aid As Integer
        Public Property IndustryDescription As String
        Public Property UnitsHaveMoreThanOneRun As Boolean
        Public Property Frequency As String
        Public Property InvoiceCommencing As System.Nullable(Of Date)
        Public Property PostCode As System.Nullable(Of Short)
        Public Property MYOBCustomerNumber As String
#End Region
#Region "Get methods"
        Public Shared Function GetIndustryListReportByIndustryID(industryID As Integer) As List(Of DataObjects.usp_GetIndustryListReport)
            Dim IndustryListReport = (From c In SingletonAccess.FMSDataContextContignous.usp_GetIndustryListReport(industryID)
                                        Select New DataObjects.usp_GetIndustryListReport(c)).ToList
            Return IndustryListReport
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(IndustryListReport As FMS.Business.usp_GetIndustryListReportResult)
            With IndustryListReport
                Me.CustomerName = .CustomerName
                Me.SiteName = .SiteName
                Me.SiteCeaseDate = .SiteCeaseDate
                Me.ServiceUnits = .ServiceUnits
                Me.ServiceDescription = .ServiceDescription
                Me.ServicePrice = .ServicePrice
                Me.PerAnnumCharge = .PerAnnumCharge
                Me.SiteName1 = .SiteName1
                Me.Aid = .Aid
                Me.IndustryDescription = .IndustryDescription
                Me.UnitsHaveMoreThanOneRun = .UnitsHaveMoreThanOneRun
                Me.Frequency = .Frequency
                Me.InvoiceCommencing = .InvoiceCommencing
                Me.PostCode = .PostCode
                Me.MYOBCustomerNumber = .MYOBCustomerNumber
            End With
        End Sub
#End Region
    End Class
End Namespace

