Namespace DataObjects
    Public Class usp_GetIncludeServiceInformationReport

#Region "Properties / enums"
        Public Property CID As System.Nullable(Of Integer)
        Public Property ServiceCode As String
        Public Property ServiceDescription As String
        Public Property ServiceUnits As System.Nullable(Of Double)
        Public Property ServicePrice As System.Nullable(Of Double)
        Public Property PerAnnumCharge As System.Nullable(Of Double)
        Public Property CSid As System.Nullable(Of Integer)
        Public Property SiteCeaseDate As System.Nullable(Of Date)
        Public Property UnitsHaveMoreThanOneRun As System.Nullable(Of Boolean)
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(IncludeServiceInformation As FMS.Business.usp_GetIncludeServiceInformationReportResult)
            With IncludeServiceInformation
                Me.CID = .Cid
                Me.ServiceCode = .ServiceCode
                Me.ServiceDescription = .ServiceDescription
                Me.ServiceUnits = .ServiceUnits
                Me.ServicePrice = .ServicePrice
                Me.PerAnnumCharge = .PerAnnumCharge
                Me.CSid = .CSid
                Me.SiteCeaseDate = .SiteCeaseDate
                Me.UnitsHaveMoreThanOneRun = .UnitsHaveMoreThanOneRun

            End With
        End Sub



#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.usp_GetIncludeServiceInformationReport)
            SingletonAccess.FMSDataContextContignous.CommandTimeout = 180
            Dim objList = (From i In SingletonAccess.FMSDataContextContignous.usp_GetIncludeServiceInformationReport()
                            Select New DataObjects.usp_GetIncludeServiceInformationReport(i)).ToList
            Return objList
        End Function
        Public Shared Function GetAllIncludeServiceInformationPerCustomer(Customer As Integer) As List(Of DataObjects.usp_GetIncludeServiceInformationReport)
            SingletonAccess.FMSDataContextContignous.CommandTimeout = 180

            Dim objList = (From s In SingletonAccess.FMSDataContextContignous.usp_GetIncludeServiceInformationReport()
                                     Where s.Customer = Customer
                            Select New DataObjects.usp_GetIncludeServiceInformationReport(s)).ToList
            Return objList
        End Function

        Public Shared Function GetAllIncludeServiceInformationPerCustomerAndCID(Customer As Integer, Optional CID As Integer = 0) As List(Of DataObjects.usp_GetIncludeServiceInformationReport)
            SingletonAccess.FMSDataContextContignous.CommandTimeout = 180
            Dim objList As New List(Of DataObjects.usp_GetIncludeServiceInformationReport)

            If (Customer = 0) Then
                If (CID = 0) Then
                    objList = (From s In SingletonAccess.FMSDataContextContignous.usp_GetIncludeServiceInformationReport()
                                    Select New DataObjects.usp_GetIncludeServiceInformationReport(s)).ToList
                End If
            Else
                If (CID = 0) Then
                    objList = (From s In SingletonAccess.FMSDataContextContignous.usp_GetIncludeServiceInformationReport()
                                    Where s.Customer = Customer
                                    Select New DataObjects.usp_GetIncludeServiceInformationReport(s)).ToList
                Else
                    objList = (From s In SingletonAccess.FMSDataContextContignous.usp_GetIncludeServiceInformationReport()
                                    Where s.Customer = Customer And s.Cid = CID
                                    Select New DataObjects.usp_GetIncludeServiceInformationReport(s)).ToList
                End If
            End If

            'Dim objSiteList = (From s In SingletonAccess.FMSDataContextContignous.usp_GetSiteListReport()
            '                         Where s.Customer = Customer
            '                Select New DataObjects.usp_GetSiteListReport(s)).ToList
            Return objList
        End Function

#End Region

    End Class

End Namespace


