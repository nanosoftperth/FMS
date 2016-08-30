
Namespace ReportGeneration


    Public Class GeoFenceReport_Simple

        Public Property DeviceID As String
        Public Property StartTime As Date
        Public Property EndTime As Date?
        Public Property GeoFence_Description As String
        Public Property Vehicle_Name As String
        Public Property GeoFence_Name As String
        Public Property Driver_Name As String
        Public Property ApplicationDriverID As Guid?
        Public Property ApplicationGeoFenceID As Guid?
        Public Property ApplicationVehicleID As Guid?
        Public Property GeoFenceDeviceCollissionID As Guid
        Public Property PK As Integer

        Public ReadOnly Property TimeTakes As TimeSpan
            Get

                Dim thisEndtime As Date = If(EndTime.HasValue, EndTime.Value, Now)
                Return thisEndtime - StartTime
            End Get
        End Property

        Public Shared Function GetReport(appid As Guid,
                                        startdate As Date,
                                        enddate As Date) As List(Of GeoFenceReport_Simple)

            Dim lst As List(Of usp_GetGeoFenceCollisionsResult) = _
                SingletonAccess.FMSDataContextContignous.usp_GetGeoFenceCollisions(appid, startdate, enddate).ToList

            
            Return (From x As usp_GetGeoFenceCollisionsResult _
                     In lst _
                     Select New GeoFenceReport_Simple() With _
                     {.DeviceID = x.DeviceID,
                      .Driver_Name = x.Driver_Name,
                      .EndTime = x.EndTime,
                      .GeoFence_Description = x.GeoFence_Description,
                      .GeoFence_Name = x.GeoFence_Name,
                      .StartTime = x.StartTime,
                      .Vehicle_Name = x.Vehicle_Name,
                      .PK = x.id,
                      .ApplicationDriverID = x.ApplicationDriverID,
                      .ApplicationGeoFenceID = x.ApplicationGeoFenceID,
                      .ApplicationVehicleID = x.ApplicationVehicleID,
                      .GeoFenceDeviceCollissionID = x.GeoFenceDeviceCollissionID
                     }).ToList


        End Function

        Public Sub New()

        End Sub


    End Class



End Namespace
