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
        Public Property Technician As System.Nullable(Of Boolean)
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTblDrivers As FMS.Business.tblDriver)
            With objTblDrivers
                Me.ApplicationId = .ApplicationId
                Me.DriverID = .DriverID
                Me.Did = .Did
                Me.DriverName = .DriverName
                Me.DriversLicenseExpiryDate = .DriversLicenseExpiryDate
                Me.DriversLicenseNo = .DriversLicenseNo
                Me.Inactive = .Inactive
                Me.Technician = .Technician
            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(dvr As DataObjects.tblDrivers)
            Dim appID = ThisSession.ApplicationID

            Dim obj As New FMS.Business.tblDriver
            With obj
                .ApplicationId = appID
                .DriverID = Guid.NewGuid()
                .DriverName = dvr.DriverName
                .DriversLicenseNo = dvr.DriversLicenseNo
                .DriversLicenseExpiryDate = dvr.DriversLicenseExpiryDate
                .Inactive = dvr.Inactive
                .Technician = dvr.Technician

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
                .Technician = dvr.Technician

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
                              Where d.Inactive.Equals(True) And d.ApplicationId = ThisSession.ApplicationID
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
        Public Shared Function GetAllPerApplicationMinusInActive() As List(Of DataObjects.tblDrivers)
            Dim appID = ThisSession.ApplicationID

            Dim getDrivers = (From d In SingletonAccess.FMSDataContextContignous.tblDrivers
                              Where d.ApplicationId = appID And d.Inactive = 0
                              Order By d.DriverName
                              Select New DataObjects.tblDrivers(d)).ToList
            Return getDrivers
        End Function

        Public Shared Function GetTechnicianPerApplicationMinusInActive() As List(Of DataObjects.tblDrivers)
            Dim appID = ThisSession.ApplicationID

            Dim getDrivers = (From d In SingletonAccess.FMSDataContextContignous.tblDrivers
                              Where d.ApplicationId = appID _
                                  And d.Technician = True _
                                  And d.Inactive = False
                              Order By d.DriverName
                              Select New DataObjects.tblDrivers(d)).ToList
            Return getDrivers
        End Function
#End Region

    End Class

End Namespace
