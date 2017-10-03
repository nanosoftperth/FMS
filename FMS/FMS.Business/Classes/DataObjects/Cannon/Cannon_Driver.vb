Namespace DataObjects
    Public Class Cannon_Driver
#Region "Properties / enums"
        Public Property DriverID As System.Guid
        Public Property Name As String
        Public Property OtherFields As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(Driver As DataObjects.Cannon_Driver)
            Dim cannonDriver As New FMS.Business.Cannon_Driver
            With cannonDriver
                .DriverID = Guid.NewGuid
                .Name = Driver.Name
                .OtherFields = Driver.OtherFields
            End With
            SingletonAccess.FMSDataContextContignous.Cannon_Drivers.InsertOnSubmit(cannonDriver)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Driver As DataObjects.Cannon_Driver)
            Dim cannonDriver As FMS.Business.Cannon_Driver = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Drivers
                                                        Where i.DriverID.Equals(Driver.DriverID)).SingleOrDefault
            With cannonDriver
                .DriverID = Driver.DriverID
                .Name = Driver.Name
                .OtherFields = Driver.OtherFields
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Driver As DataObjects.Cannon_Driver)
            Dim DriverID As System.Guid = Driver.DriverID
            Dim cannonDriver As FMS.Business.Cannon_Driver = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Drivers
                                                        Where i.DriverID = DriverID).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.Cannon_Drivers.DeleteOnSubmit(cannonDriver)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.Cannon_Driver)
            Dim cannonDrivers = (From i In SingletonAccess.FMSDataContextContignous.Cannon_Drivers
                             Select New DataObjects.Cannon_Driver(i)).ToList()
            Return cannonDrivers
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objDriver As FMS.Business.Cannon_Driver)
            With objDriver
                Me.DriverID = .DriverID
                Me.Name = .Name
                Me.OtherFields = .OtherFields
            End With
        End Sub
#End Region
    End Class
End Namespace

