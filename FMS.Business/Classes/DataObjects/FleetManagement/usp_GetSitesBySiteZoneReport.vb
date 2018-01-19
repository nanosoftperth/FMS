Namespace DataObjects
    Public Class usp_GetSitesBySiteZoneReport
#Region "Properties / enums"
        Public Property Zone As String
        Public Property Cid As Integer
        Public Property SiteName As String
        Public Property Customer As System.Nullable(Of Short)
        Public Property AddressLine1 As String
        Public Property AddressLine2 As String
        Public Property AddressLine3 As String
        Public Property Suburb As String
        Public Property PostCode As System.Nullable(Of Short)
        Public Property PostalAddressLine1 As String
        Public Property PostalAddressLine2 As String
        Public Property PostalSuburb As String
        Public Property PostalPostCode As System.Nullable(Of Short)
#End Region
#Region "Get methods"
        Public Shared Function GetSitesBySiteZone() As List(Of DataObjects.usp_GetSitesBySiteZoneReport)
            Dim objSitesBySiteZone = (From c In SingletonAccess.FMSDataContextContignous.usp_GetSitesBySiteZoneReport(ThisSession.ApplicationID)
                                      Select New DataObjects.usp_GetSitesBySiteZoneReport(c)).ToList
            Return objSitesBySiteZone
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objSitesBySiteZone As FMS.Business.usp_GetSitesBySiteZoneReportResult)
            With objSitesBySiteZone
                Me.Zone = .Zone
                Me.Cid = .Cid
                Me.SiteName = .SiteName
                Me.Customer = .Customer
                Me.AddressLine1 = .AddressLine1
                Me.AddressLine2 = .AddressLine2
                Me.AddressLine3 = .AddressLine3
                Me.Suburb = .Suburb
                Me.PostCode = .PostCode
                Me.PostalAddressLine1 = .PostalAddressLine1
                Me.PostalAddressLine2 = .PostalAddressLine2
                Me.PostalSuburb = .PostalSuburb
                Me.PostalPostCode = .PostalPostCode
            End With
        End Sub
#End Region
    End Class
End Namespace

