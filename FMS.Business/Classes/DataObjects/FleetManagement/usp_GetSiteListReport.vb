Namespace DataObjects
    Public Class usp_GetSiteListReport

#Region "Properties / enums"

        Public Property CustomerName As String
        Public Property SiteID As System.Nullable(Of Guid)
        Public Property SiteName As String
        Public Property AddressLine1 As String
        Public Property AddressLine2 As String
        Public Property AddressLine3 As String
        Public Property AddressLine4 As String
        Public Property SubStatePC As String
        Public Property SiteContactName As String
        Public Property SiteContactPhone As String
        Public Property SiteContactFax As String
        Public Property SiteContactMobile As String
        Public Property SiteContactEmail As String
        Public Property Cid As System.Nullable(Of Integer)
        Public Property Customer As System.Nullable(Of Integer)
        Public Property PostalAddressLine1 As String
        Public Property PostalAddressLine2 As String
        Public Property PostalSubStatePC As String
        Public Property SiteCeaseDate As System.Nullable(Of Date)


#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objSiteList As FMS.Business.usp_GetSiteListReportResult)
            With objSiteList
                Me.CustomerName = .CustomerName
                Me.SiteName = .SiteName
                Me.SiteID = .SiteID
                Me.AddressLine1 = .AddressLine1
                Me.AddressLine2 = .AddressLine2
                Me.AddressLine3 = .AddressLine3
                Me.AddressLine4 = .AddressLine4
                Me.SubStatePC = .SubStatePC
                Me.SiteContactName = .SiteContactName
                Me.SiteContactPhone = .SiteContactPhone
                Me.SiteContactFax = .SiteContactFax
                Me.SiteContactMobile = .SiteContactMobile
                Me.SiteContactEmail = .SiteContactEmail
                Me.Cid = .Cid
                Me.Customer = .Customer
                Me.PostalAddressLine1 = .PostalAddressLine1
                Me.PostalAddressLine2 = .PostalAddressLine2
                Me.PostalSubStatePC = .PostalSubStatePC
                Me.SiteCeaseDate = .SiteCeaseDate

            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAllSiteListReport() As List(Of DataObjects.usp_GetSiteListReport)
            SingletonAccess.FMSDataContextContignous.CommandTimeout = 180
            Dim objSiteList = (From s In SingletonAccess.FMSDataContextContignous.usp_GetSiteListReport()
                            Select New DataObjects.usp_GetSiteListReport(s)).ToList
            Return objSiteList
        End Function
        Public Shared Function GetAllSiteListReportPerCustomer(Customer As Integer) As List(Of DataObjects.usp_GetSiteListReport)
            SingletonAccess.FMSDataContextContignous.CommandTimeout = 180

            Dim objSiteList = (From s In SingletonAccess.FMSDataContextContignous.usp_GetSiteListReport()
                                     Where s.Customer = Customer
                            Select New DataObjects.usp_GetSiteListReport(s)).ToList
            Return objSiteList
        End Function

        Public Shared Function GetAllSiteListReport(Customer As Integer, Optional CID As Integer = 0) As List(Of DataObjects.usp_GetSiteListReport)
            SingletonAccess.FMSDataContextContignous.CommandTimeout = 180
            Dim objSiteList As New List(Of DataObjects.usp_GetSiteListReport)

            If (Customer = 0) Then
                If (CID = 0) Then
                    objSiteList = (From s In SingletonAccess.FMSDataContextContignous.usp_GetSiteListReport()
                                    Select New DataObjects.usp_GetSiteListReport(s)).ToList
                End If
            Else
                If (CID = 0) Then
                    objSiteList = (From s In SingletonAccess.FMSDataContextContignous.usp_GetSiteListReport()
                                    Where s.Customer = Customer
                                    Select New DataObjects.usp_GetSiteListReport(s)).ToList
                Else
                    objSiteList = (From s In SingletonAccess.FMSDataContextContignous.usp_GetSiteListReport()
                                    Where s.Customer = Customer And s.Cid = CID
                                    Select New DataObjects.usp_GetSiteListReport(s)).ToList
                End If
            End If

            'Dim objSiteList = (From s In SingletonAccess.FMSDataContextContignous.usp_GetSiteListReport()
            '                         Where s.Customer = Customer
            '                Select New DataObjects.usp_GetSiteListReport(s)).ToList
            Return objSiteList
        End Function

#End Region

    End Class

End Namespace

