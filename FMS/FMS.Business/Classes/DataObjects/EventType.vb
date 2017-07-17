Namespace DataObjects
    Public Class EventType

#Region "SHARED FUNCTIONS"

        Public Shared Function GetAllVehicleNames(ByVal applicationId As Guid) As Object
            Dim retobj As Object = SingletonAccess.FMSDataContextNew.ApplicationVehicles.Where(Function(y) y.ApplicationID = applicationId).OrderBy(Function(m) m.DeviceID).Select( _
                                                                            Function(x) New DataObjects.ApplicationVehicle(x)).Select(Function(col) col.Name).ToList()
            Return retobj
        End Function
        Public Shared Function GetAllForVehicleSPN(ByVal VehicleName As String) As List(Of DataObjects.CAN_MessageDefinition)
            Return DataObjects.ApplicationVehicle.GetFromName(VehicleName).GetAvailableCANTags()
        End Function
#End Region
#Region "CRUD"

#End Region
    End Class
End Namespace

