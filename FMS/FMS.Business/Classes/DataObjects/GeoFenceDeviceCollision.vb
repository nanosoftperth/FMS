Namespace DataObjects
    Public Class GeoFenceDeviceCollision

#Region "properties"
        Public Property GeoFenceDeviceCollissionID As Guid
        Public Property ApplicationID As Guid
        Public Property DeviceID As String
        Public Property StartTime As DateTime
        Public Property EndTime As DateTime?
        Public Property ApplicationGeoFenceID As Guid

#End Region


#Region "constructors"

        Public Sub New(x As FMS.Business.GeoFenceDeviceCollission)

            With x

                Me.GeoFenceDeviceCollissionID = .GeoFenceDeviceCollissionID
                Me.ApplicationID = .ApplicationID
                Me.DeviceID = .DeviceID
                Me.StartTime = If(.StartTime.HasValue, .StartTime.Value.timezoneToClient, Nothing)
                Me.EndTime = x.EndTime.timezoneToClient
                Me.ApplicationGeoFenceID = .ApplicationGeoFenceID

            End With

        End Sub

        ''' <summary>
        ''' serialization only
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

#End Region

#Region "CRUD"

        Public Shared Function Create(x As FMS.Business.DataObjects.GeoFenceDeviceCollision) As Guid

            Dim o As FMS.Business.GeoFenceDeviceCollission = New FMS.Business.GeoFenceDeviceCollission

            o.ApplicationID = x.ApplicationID
            o.GeoFenceDeviceCollissionID = Guid.NewGuid
            o.DeviceID = x.DeviceID
            o.StartTime = x.StartTime.timezoneToPerth
            o.ApplicationGeoFenceID = x.ApplicationGeoFenceID

            If x.EndTime IsNot Nothing Then o.EndTime = x.EndTime.Value.timezoneToPerth

            SingletonAccess.FMSDataContextContignous.GeoFenceDeviceCollissions.InsertOnSubmit(o)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

            Return o.GeoFenceDeviceCollissionID

        End Function

        Public Shared Sub Update(x As FMS.Business.DataObjects.GeoFenceDeviceCollision)

            Dim o As FMS.Business.GeoFenceDeviceCollission = _
                        SingletonAccess.FMSDataContextContignous _
                             .GeoFenceDeviceCollissions.Where(Function(y) _
                                            x.GeoFenceDeviceCollissionID = y.GeoFenceDeviceCollissionID).ToList.Single

            o.ApplicationID = x.ApplicationID
            o.GeoFenceDeviceCollissionID = x.GeoFenceDeviceCollissionID
            o.DeviceID = x.DeviceID
            o.StartTime = x.StartTime.timezoneToPerth
            o.EndTime = If(x.EndTime.HasValue, x.EndTime.Value.timezoneToPerth, Nothing)

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Delete(x As FMS.Business.DataObjects.GeoFenceDeviceCollision)

            Dim o As FMS.Business.GeoFenceDeviceCollission = _
            SingletonAccess.FMSDataContextContignous _
                 .GeoFenceDeviceCollissions.Where(Function(y) _
                                x.GeoFenceDeviceCollissionID = x.GeoFenceDeviceCollissionID).ToList.Single

            SingletonAccess.FMSDataContextContignous.GeoFenceDeviceCollissions.DeleteOnSubmit(o)

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub DeleteForTimeRange(deviceid As String, starttime As Date, endtime As Date)

            starttime = starttime.timezoneToPerth
            endtime = endtime.timezoneToPerth

            SingletonAccess.FMSDataContextNew.usp_deleteGeoFenceCollisions(deviceid, starttime, endtime)
        End Sub

#End Region

#Region "STATIC gets and sets"


        Public Shared Function GetAllForApplication(appid As Guid, startDate As Date) As List(Of DataObjects.GeoFenceDeviceCollision)


            Dim retobj As New List(Of DataObjects.GeoFenceDeviceCollision)

            retobj = (From y In SingletonAccess.FMSDataContextNew.GeoFenceDeviceCollissions _
                        Where y.ApplicationID = appid _
                        And y.StartTime >= startDate _
                        Select New DataObjects.GeoFenceDeviceCollision(y)).ToList

            Return retobj

        End Function

        Public Shared Function GetAllForApplication(appid As Guid) As List(Of DataObjects.GeoFenceDeviceCollision)


            Dim retobj As New List(Of DataObjects.GeoFenceDeviceCollision)

            retobj = (From y In SingletonAccess.FMSDataContextNew.GeoFenceDeviceCollissions _
                        Where y.ApplicationID = appid
                        Select New DataObjects.GeoFenceDeviceCollision).ToList

            Return retobj

        End Function

        Public Shared Function GetAllWithoutEndDates(appid As Guid) As List(Of DataObjects.GeoFenceDeviceCollision)

            Dim retobj As New List(Of DataObjects.GeoFenceDeviceCollision)

            retobj = (From y In SingletonAccess.FMSDataContextNew.GeoFenceDeviceCollissions _
                        Where y.ApplicationID = appid _
                        And y.EndTime Is Nothing
                        Select New DataObjects.GeoFenceDeviceCollision(y)).ToList

            Return retobj

        End Function


        Public Shared Function GetAllWithoutEndDates(appid As Guid, deviceid As String) As List(Of DataObjects.GeoFenceDeviceCollision)

            Dim retobj As New List(Of DataObjects.GeoFenceDeviceCollision)

            retobj = (From y In SingletonAccess.FMSDataContextNew.GeoFenceDeviceCollissions _
                        Where y.ApplicationID = appid _
                        And y.EndTime Is Nothing _
                        And y.DeviceID = deviceid _
                        Select New DataObjects.GeoFenceDeviceCollision(y)).ToList

            Return retobj

        End Function

#End Region

    End Class


End Namespace

