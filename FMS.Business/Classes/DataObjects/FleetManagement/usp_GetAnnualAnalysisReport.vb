Namespace DataObjects
    Public Class usp_GetAnnualAnalysisReport

#Region "Properties / enums"
        Public Property ApplicationId As Guid
        Public Property Frequency As String
        Public Property CustomerName As String
        Public Property Suburb As String
        Public Property CustomerRating As System.Nullable(Of Integer)
        Public Property SiteName As String
        Public Property ServiceDescription As String
        Public Property ServiceUnits As System.Nullable(Of Double)
        Public Property ServicePrice As System.Nullable(Of Double)
        Public Property PerAnnumCharge As System.Nullable(Of Double)
        Public Property SiteContractExpiry As System.Nullable(Of Date)
        Public Property SiteCeaseReason As System.Nullable(Of Integer)
        Public Property SiteStartDate As System.Nullable(Of Date)
        Public Property CeaseReasonDescription As String
        Public Property SiteCeaseDate As System.Nullable(Of Date)
        Public Property IndustryGroup As System.Nullable(Of Integer)

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objAnnualAnalysis As FMS.Business.usp_GetAnnualAnalysisReportResult)
            With objAnnualAnalysis
                Me.ApplicationId = .ApplicationID
                Me.Frequency = .Frequency
                Me.CustomerName = .CustomerName
                Me.Suburb = .Suburb
                Me.CustomerRating = .CustomerRating
                Me.SiteName = .SiteName
                Me.ServiceDescription = .ServiceDescription
                Me.ServiceUnits = .ServiceUnits
                Me.ServicePrice = .ServicePrice
                Me.PerAnnumCharge = .PerAnnumCharge
                Me.SiteContractExpiry = .SiteContractExpiry
                Me.SiteCeaseReason = .SiteCeaseReason
                Me.SiteStartDate = .SiteStartDate
                Me.SiteCeaseDate = .SiteCeaseDate
                Me.CeaseReasonDescription = .CeaseReasonDescription
                Me.IndustryGroup = .IndustryGroup

            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAllAnnualAnalysisReport() As List(Of DataObjects.usp_GetAnnualAnalysisReport)
            SingletonAccess.FMSDataContextContignous.CommandTimeout = 180
            Dim objAnnualAnalysis = (From a In SingletonAccess.FMSDataContextContignous.usp_GetAnnualAnalysisReport()
                            Select New DataObjects.usp_GetAnnualAnalysisReport(a)).ToList
            Return objAnnualAnalysis
        End Function
        Public Shared Function GetAllAnnualAnalysisReportPerCustomerRating(Rating As Integer) As List(Of DataObjects.usp_GetAnnualAnalysisReport)
            Dim appId = ThisSession.ApplicationID
            SingletonAccess.FMSDataContextContignous.CommandTimeout = 180

            Dim objAnnualAnalysis = (From a In SingletonAccess.FMSDataContextContignous.usp_GetAnnualAnalysisReport()
                                     Where a.CustomerRating = Rating And a.ApplicationID = appId
                                     Select New DataObjects.usp_GetAnnualAnalysisReport(a)).ToList
            Return objAnnualAnalysis
        End Function

#End Region

    End Class

End Namespace


