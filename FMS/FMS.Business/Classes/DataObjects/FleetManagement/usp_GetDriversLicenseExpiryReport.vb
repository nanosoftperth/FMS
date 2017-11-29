Namespace DataObjects
    Public Class usp_GetDriversLicenseExpiryReport
#Region "Properties / enums"
        Public Property DriverID As System.Guid
        Public Property Did As Integer
        Public Property DriverName As String
        Public Property DriversLicenseNo As String
        Public Property DriversLicenseExpiryDate As System.Nullable(Of Date)
        Public Property Inactive As Boolean
        Public Property Renewal As String
#End Region
#Region "Get methods"

        Public Shared Function GetDriversLicenseExpiryReport() As List(Of DataObjects.usp_GetDriversLicenseExpiryReport)
            Dim lstGetDriversLicenseExpiryReport = (From d In SingletonAccess.FMSDataContextContignous.usp_GetDriversLicenseExpiryReport
                                    Order By d.DriverName
                                 Select New DataObjects.usp_GetDriversLicenseExpiryReport(d)).ToList()
            Return lstGetDriversLicenseExpiryReport
        End Function

#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(GetDriversLicenseExpiry As FMS.Business.usp_GetDriversLicenseExpiryReportResult)
            With GetDriversLicenseExpiry
                Me.DriverID = .DriverID
                Me.Did = .Did
                Me.DriverName = .DriverName
                Me.DriversLicenseNo = .DriversLicenseNo
                Me.DriversLicenseExpiryDate = .DriversLicenseExpiryDate
                Me.Inactive = .Inactive
                Me.Renewal = .Renewal
            End With
        End Sub
#End Region
    End Class
End Namespace

