Namespace DataObjects
    Public Class usp_GetContractRenewalsReport
#Region "Properties / enums"
        Public Property Customer As System.Nullable(Of Short)
        Public Property AreaDescription As String
        Public Property SiteContractExpiry As System.Nullable(Of Date)
        Public Property CustomerName As String
        Public Property SiteName As String
        Public Property SiteStartDate As System.Nullable(Of Date)
        Public Property ContractPeriodDesc As String
        Public Property SiteContactPhone As String
        Public Property CustomerContactName As String
        Public Property CustomerPhone As String
        Public Property ServiceUnits As System.Nullable(Of Double)
        Public Property PerAnnumCharge As System.Nullable(Of Double)
#End Region
#Region "Get methods"
        Public Shared Function GetContractRenewalsReport(StartDate As Date, EndDate As Date) As List(Of DataObjects.usp_GetContractRenewalsReport)
            Dim ContractRenewalsReport = (From c In SingletonAccess.FMSDataContextContignous.usp_GetContractRenewalsReport(StartDate, EndDate, ThisSession.ApplicationID)
                                          Order By c.AreaDescription
                                          Select New DataObjects.usp_GetContractRenewalsReport(c)).ToList
            Return ContractRenewalsReport
        End Function
        Public Shared Function GetContractRenewalsReport(StartDate As Date, EndDate As Date, Zone As String) As List(Of DataObjects.usp_GetContractRenewalsReport)
            Dim ContractRenewalsReport = (From c In SingletonAccess.FMSDataContextContignous.usp_GetContractRenewalsReport(StartDate, EndDate, ThisSession.ApplicationID)
                                          Where Not c.AreaDescription Is Nothing AndAlso c.AreaDescription.Equals(Zone)
                                          Order By c.AreaDescription
                                          Select New DataObjects.usp_GetContractRenewalsReport(c)).ToList
            Return ContractRenewalsReport
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(ContractRenewalReport As FMS.Business.usp_GetContractRenewalsReportResult)
            With ContractRenewalReport
                Me.Customer = .Customer
                Me.AreaDescription = .AreaDescription
                Me.SiteContractExpiry = .SiteContractExpiry
                Me.CustomerName = .CustomerName
                Me.SiteName = .SiteName
                Me.SiteStartDate = .SiteStartDate
                Me.ContractPeriodDesc = .ContractPeriodDesc
                Me.SiteContactPhone = .SiteContactPhone
                Me.CustomerContactName = .CustomerContactName
                Me.CustomerPhone = .CustomerPhone
                Me.ServiceUnits = .ServiceUnits
                Me.PerAnnumCharge = .PerAnnumCharge
            End With
        End Sub
#End Region
    End Class
End Namespace

