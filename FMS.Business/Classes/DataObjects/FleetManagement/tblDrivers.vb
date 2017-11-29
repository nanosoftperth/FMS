Namespace DataObjects
    Public Class tblDrivers
#Region "Properties / enums"
        Public Property Did As Integer
        Public Property DriverName As String
        Public Property DriversLicenseNo As String
        Public Property DriversLicenseExpiryDate As System.Nullable(Of Date)
        Public Property Inactive As Boolean
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblDrivers)
            Dim getDrivers = (From d In SingletonAccess.FMSDataContextContignous.tblDrivers
                              Select New DataObjects.tblDrivers(d)).ToList
            Return getDrivers
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTblDrivers As FMS.Business.tblDriver)
            With objTblDrivers
                Me.Did = .Did
                Me.DriverName = .DriverName
                Me.DriversLicenseExpiryDate = .DriversLicenseExpiryDate
                Me.DriversLicenseNo = .DriversLicenseNo
                Me.Inactive = .Inactive
            End With
        End Sub
#End Region
    End Class

End Namespace
