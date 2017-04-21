Public Class CacheVehicle
    Public Property ID As Int32
    Public Property LineValies As List(Of Vehicle) 
    Public Property LogoBinary() As Byte()
End Class
Public Class Vehicle
    Public Property ApplicationVehileID As Guid
    Public Property Name As String
    Public Property Registration As String
    Public Property Notes As String
    Public Property DeviceID As String
    Public Property ApplicationID As Guid
    Public Property ApplicationImageID As Guid?
    Public Property VINNumber As String
    Public Property CurrentDriver As FMS.Business.DataObjects.ApplicationDriver
    Public Property QueryTime As Date
    Public Property CAN_Protocol_Type As String
End Class


