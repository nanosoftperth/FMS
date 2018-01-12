Imports System.Globalization

Namespace DataObjects
    Public Class usp_GetGainAndLossesBySalesPersonReport

#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property AreaDescription As String
        Public Property Suburb As String
        Public Property SalesPerson As String
        Public Property SiteName As String
        Public Property SiteStartDate As System.Nullable(Of Date)
        Public Property SiteCeaseDate As System.Nullable(Of Date)
        Public Property Status As String
        Public Property ServiceDescription As String
        Public Property ServiceUnits As System.Nullable(Of Double)
        Public Property ServicePrice As System.Nullable(Of Double)
        Public Property PerAnnumCharge As System.Nullable(Of Double)


#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTbl As FMS.Business.usp_GetGainAndLossesBySalesPersonReportResult)

            With objTbl
                Me.ApplicationId = .ApplicationId
                Me.AreaDescription = .AreaDescription
                Me.Suburb = .Suburb
                Me.SalesPerson = .SalesPerson
                Me.SiteName = .SiteName
                Me.SiteStartDate = .SiteStartDate
                Me.SiteCeaseDate = .SiteCeaseDate
                Me.Status = .status
                Me.ServiceDescription = .ServiceDescription
                Me.ServiceUnits = .ServiceUnits
                Me.ServicePrice = .ServicePrice
                Me.PerAnnumCharge = .PerAnnumCharge

            End With
        End Sub
#End Region

#Region "Get methods"

        Public Shared Function GetAllPerApplication() As List(Of DataObjects.usp_GetGainAndLossesBySalesPersonReport)
            Dim appId = ThisSession.ApplicationID

            Dim obj = (From g In SingletonAccess.FMSDataContextContignous.usp_GetGainAndLossesBySalesPersonReport
                       Where g.ApplicationId = appId
                       Order By g.AreaDescription
                       Select New DataObjects.usp_GetGainAndLossesBySalesPersonReport(g)).ToList()
            Return obj

        End Function

        Public Shared Function GetAllPerApplicationWithStartEndDate(StartDate As Date, EndDate As Date) As List(Of DataObjects.usp_GetGainAndLossesBySalesPersonReport)
            Dim appId = ThisSession.ApplicationID

            Dim sDate As Date
            Dim eDate As Date

            Dim culture As CultureInfo = CultureInfo.CurrentCulture

            If (culture.Name = "en-AU") Then
                sDate = StartDate.ToString("MM/dd/yyyy")
                eDate = EndDate.ToString("MM/dd/yyyy")
            Else
                sDate = StartDate.ToString("dd/MM/yyyy")
                eDate = EndDate.ToString("dd/MM/yyyy")
            End If

            Dim obj = (From g In SingletonAccess.FMSDataContextContignous.usp_GetGainAndLossesBySalesPersonReport
                       Where g.ApplicationId = appId And
                           (g.SiteStartDate >= sDate And g.SiteStartDate <= eDate) Or
                           (g.SiteCeaseDate >= sDate And g.SiteCeaseDate <= eDate)
                       Order By g.AreaDescription
                       Select New DataObjects.usp_GetGainAndLossesBySalesPersonReport(g)).ToList()



            Return obj

        End Function


#End Region

    End Class

End Namespace

