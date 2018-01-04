Namespace DataObjects
    Public Class tblDrivers
#Region "Properties / enums"
        Public Property ApplicationId As Guid
        Public Property DriverID As Guid
        Public Property Did As Integer
        Public Property DriverName As String
        Public Property DriversLicenseNo As String
        Public Property DriversLicenseExpiryDate As System.Nullable(Of Date)
        Public Property Inactive As Boolean
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

#Region "CRUD"
        Public Shared Sub Create(dvr As DataObjects.tblDrivers)
            Dim appID = ThisSession.ApplicationID

            Dim obj As New FMS.Business.tblDriver
            With obj
                .ApplicationId = appID
                .DriverName = dvr.DriverName
                .DriversLicenseNo = dvr.DriversLicenseNo
                .DriversLicenseExpiryDate = dvr.DriversLicenseExpiryDate
                .Inactive = dvr.Inactive

            End With
            SingletonAccess.FMSDataContextContignous.tblDrivers.InsertOnSubmit(obj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(dvr As DataObjects.tblDrivers)
            Dim appID = ThisSession.ApplicationID

            Dim obj As FMS.Business.tblDriver = (From d In SingletonAccess.FMSDataContextContignous.tblDrivers
                                                 Where d.DriverID.Equals(dvr.DriverID) And d.ApplicationId = appID).SingleOrDefault
            With obj
                .DriverName = dvr.DriverName
                .DriversLicenseNo = dvr.DriversLicenseNo
                .DriversLicenseExpiryDate = dvr.DriversLicenseExpiryDate
                .Inactive = dvr.Inactive

            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(dvr As DataObjects.tblDrivers)
            Dim appID = ThisSession.ApplicationID

            Dim obj As FMS.Business.tblDriver = (From d In SingletonAccess.FMSDataContextContignous.tblDrivers
                                                 Where d.DriverID.Equals(dvr.DriverID) And d.ApplicationId.Equals(appID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblDrivers.DeleteOnSubmit(obj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblDrivers)
            Dim getDrivers = (From d In SingletonAccess.FMSDataContextContignous.tblDrivers
                              Order By d.DriverName
                              Select New DataObjects.tblDrivers(d)).ToList
            Return getDrivers
        End Function

        Public Shared Function GetAllPerApplication() As List(Of DataObjects.tblDrivers)
            Dim appID = ThisSession.ApplicationID

            Dim getDrivers = (From d In SingletonAccess.FMSDataContextContignous.tblDrivers
                              Where d.ApplicationId = appID
                              Order By d.DriverName
                              Select New DataObjects.tblDrivers(d)).ToList
            Return getDrivers
        End Function
#End Region

    End Class

End Namespace
