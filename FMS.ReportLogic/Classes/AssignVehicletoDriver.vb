Imports FMS.Business
Imports FMS.Business.DataObjects
Public Class AssignVehicletoDriver
    Public Property ApplicationVehicleDriverTimeID As Guid
    Public Property ApplicationID As Guid
    Public Property VehicleID As Guid?
    Public Property StartDate As Date
    Public Property EndDate As Date
    Property Notes As String
    Public Property ApplicationDriverId As Guid?
    Property DriverName As String
    Property VehicleName As String
    Public Property PassengerID As Guid?
    'Public Shared Function GetApplicationVehicleDriverTimes() As List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime)

    '    'get all of the vehicle driver entries
    '    Dim lst As List(Of FMS.Business.usp_GetVehiclesAndDriversFortimePeriodResult) = _
    '  FMS.Business.SingletonAccess.FMSDataContextNew.[usp_GetAssignedVehiclestoDrivers]( _
    '                                                   ThisSession.ApplicationID).ToList()

    '    Dim retobj As New List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime)

    '    'merge the two result sets (like a left join) and return the results to the server 
    '    For Each o As usp_GetVehiclesAndDriversFortimePeriodResult In lst

    '        Dim objToAdd As New FMS.Business.DataObjects.ApplicationVehicleDriverTime

    '        With objToAdd

    '            .ApplicationID = ThisSession.ApplicationID
    '            .VehicleID = o.ApplicationVehicleID

    '            If o.ApplicationDriverID.HasValue Then .ApplicationDriverId = o.ApplicationDriverID

    '            If o.ApplicationVehicleDriverTimeID.HasValue Then
    '                .ApplicationVehicleDriverTimeID = o.ApplicationVehicleDriverTimeID
    '            Else
    '                .ApplicationVehicleDriverTimeID = Guid.NewGuid
    '            End If

    '            .StartDate = If(o.StartDateTime.HasValue, o.StartDateTime.Value.timezoneToClient, DateAndTime.Now.timezoneToClient)
    '            .EndDate = If(o.EndDateTime.HasValue, o.EndDateTime.Value.timezoneToClient, DateAndTime.Now.timezoneToClient)

    '            .PassengerID = o.PassengerID
    '        End With

    '        retobj.Add(objToAdd)
    '    Next

    '    Return retobj



    '    If FMS.Business.ThisSession.rm_ApplicationDriverVehicleTimes Is Nothing Or FMS.Business.ThisSession.rm_DriverVehicleTimeReload Then

    '    Dim retlst As List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime) = _
    '                       FMS.Business.DataObjects.ApplicationVehicleDriverTime. _
    '                            GetAllForApplicationAndDatePeriodIncludingDuds(FMS.Business.ThisSession.ApplicationID, startdate, enddate



    '        FMS.Business.ThisSession.rm_ApplicationDriverVehicleTimes = retlst

    '        FMS.Business.ThisSession.rm_DriverVehicleTimeReload = False

    '    End If

    '    Return FMS.Business.ThisSession.rm_ApplicationDriverVehicleTimes

    'End Function

End Class
Public Class CacheAssignVehicletoDriver
    Public Property ID As Int32
    Public Property LineValies As List(Of AssignVehicletoDriver)
    Public Property LogoBinary() As Byte()
End Class
