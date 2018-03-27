Namespace DataObjects
    Public Class usp_GetSitesAndCustomerServices
#Region "Properties / enums"
        Public Property CustomerServiceID As System.Guid
        Public Property ID As Integer
        Public Property CSid As System.Nullable(Of Integer)
        Public Property CId As System.Nullable(Of Integer)
        Public Property ServiceFrequencyCode As System.Nullable(Of Short)
        Public Property ServiceUnits As System.Nullable(Of Single)
        Public Property ServicePrice As System.Nullable(Of Single)
        Public Property PerAnnumCharge As System.Nullable(Of Single)
        Public Property ServiceRun As System.Nullable(Of Short)
        Public Property ServiceComments As String
        Public Property UnitsHaveMoreThanOneRun As Boolean
        Public Property ServiceFrequency1 As System.Nullable(Of Short)
        Public Property ServiceFrequency2 As System.Nullable(Of Short)
        Public Property ServiceFrequency3 As System.Nullable(Of Short)
        Public Property ServiceFrequency4 As System.Nullable(Of Short)
        Public Property ServiceFrequency5 As System.Nullable(Of Short)
        Public Property ServiceFrequency6 As System.Nullable(Of Short)
        Public Property ServiceFrequency7 As System.Nullable(Of Short)
        Public Property ServiceFrequency8 As System.Nullable(Of Short)
        Public Property ServiceSortOrderCode As String
        Public Property ServiceRun1 As System.Nullable(Of Short)
        Public Property SiteCeaseDate As System.Nullable(Of Date)
#End Region
#Region "Get methods"
        Public Shared Function GetSitesAndCustomerServices(rid As Integer) As List(Of DataObjects.usp_GetSitesAndCustomerServices)
            Try
                Dim objSitesAndCustomers As New List(Of DataObjects.usp_GetSitesAndCustomerServices)
                With New LINQtoSQLClassesDataContext
                    objSitesAndCustomers = (From c In .usp_GetSitesAndCustomerServices(rid, ThisSession.ApplicationID)
                                            Select New DataObjects.usp_GetSitesAndCustomerServices(c)).ToList
                    .Dispose()
                End With
                Return objSitesAndCustomers
            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objGetSitesAndCustomerServices As FMS.Business.usp_GetSitesAndCustomerServicesResult)
            With objGetSitesAndCustomerServices
                Me.CustomerServiceID = .CustomerServiceID
                Me.ID = .ID
                Me.CSid = .CSid
                Me.CId = .CId
                Me.ServiceFrequencyCode = .ServiceFrequencyCode
                Me.ServiceUnits = .ServiceUnits
                Me.ServicePrice = .ServicePrice
                Me.PerAnnumCharge = .PerAnnumCharge
                Me.ServiceRun = .ServiceRun
                Me.ServiceComments = .ServiceComments
                Me.UnitsHaveMoreThanOneRun = .UnitsHaveMoreThanOneRun
                Me.ServiceFrequency1 = .ServiceFrequency1
                Me.ServiceFrequency2 = .ServiceFrequency2
                Me.ServiceFrequency3 = .ServiceFrequency3
                Me.ServiceFrequency4 = .ServiceFrequency4
                Me.ServiceFrequency5 = .ServiceFrequency5
                Me.ServiceFrequency6 = .ServiceFrequency6
                Me.ServiceFrequency7 = .ServiceFrequency7
                Me.ServiceFrequency8 = .ServiceFrequency8
                Me.ServiceSortOrderCode = .ServiceSortOrderCode
                Me.ServiceRun1 = .ServiceRun1
                Me.SiteCeaseDate = .SiteCeaseDate
            End With
        End Sub
#End Region
    End Class
End Namespace


