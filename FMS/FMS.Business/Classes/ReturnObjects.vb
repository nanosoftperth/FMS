Imports System.Runtime.Serialization



<Serializable()>
Public Class GetHeatMapCoords_ReturnObject

    Public Property TruckName As String
    Public Property DriverName As String
    Public Property DeviceID As String
    Public Property LatLongs As List(Of Truck.LatLong)
    Public Property Trucks As New List(Of Truck)

    Public Property WasError As Boolean

    Public Property ErrorMessage As String

    Public Sub New()

    End Sub

End Class

<Serializable()>
Public Class SaveNewPolygonToDB_ReturnObject

    Public Property wasSuccess As Boolean
    Public Property ErrorMessage As String
    Public Property MessageFromServer As String
    Public Property IsCircular As Boolean
    Public Property NewGeoFenceID As String
    Public Property savedObj As FMS.Business.DataObjects.ApplicationGeoFence

    Public Sub New()

    End Sub

End Class

<Serializable()>
Public Class GetListOfItemsWithPositions_ReturnObject

    Public Property Trucks As New List(Of Truck)


    ''' <summary>
    ''' for serialization purposes only 
    ''' </summary>
    Public Sub New()
        Throw New Exception("should no longer be using object: GetListOfItemsWithPositions_ReturnObject")
    End Sub

End Class


Public Class Truck

    ''' <summary>
    ''' Used client side to determine if we want to view the vehicles journey
    ''' </summary>
    Public Property ShowJourneyOnMap As Boolean = False


    Public Property DriverID As Guid


    ''' <summary>
    ''' Does nothave to be populated, however; can store a particular journey in lat/long coordinates
    ''' </summary>
    Public Property JourneyLatLngs As List(Of Business.Truck.LatLong)


    Public Property lat As String
    Public Property lng As String

    ''' <summary>
    ''' ID is the DeviceID
    ''' this is what is used in the client side as the PK
    ''' anythin else can be changed cynamically (truck, driver AND the latitude and longitude)
    ''' </summary>
    Public Property ID As String
    Public Property TruckName As String
    Public Property time As String
    Public Property Driver As String
    Public Property IsWithCanBus As Boolean
    Public Property ApplicationImageID As String

    Private Property __comboBoxDisplay As String

    Public Property ComboBoxDisplay As String
        Get

            If String.IsNullOrEmpty(__comboBoxDisplay) Then Return String.Format("{0} ({1})", Driver, TruckName)

            Return __comboBoxDisplay
        End Get
        Set(value As String)
            __comboBoxDisplay = value
        End Set
    End Property


    Public Sub New(id As String, queryDate As Date, driver As String, truckName As String, applicationImageID As Guid)

        Me.Driver = driver
        Me.ID = id
        Me.time = queryDate
        Me.TruckName = truckName
        Me.ApplicationImageID = applicationImageID.ToString
        'dont try and get the lat long combo if the deviceID was not supplied
        If String.IsNullOrEmpty(id) Then Exit Sub

        'now that we have the correct driver and truck for the deviceID, we can grab the latitue and lonitude for this instance in time
        Try

            Dim piplat As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(id & "_lat")

            Dim pilng As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(id & "_long")

            Me.lat = piplat.Data.ArcValue(New PITimeServer.PITime With {.LocalDate = queryDate},
                                                                    PISDK.RetrievalTypeConstants.rtInterpolated).Value
            Me.lng = pilng.Data.ArcValue(New PITimeServer.PITime With {.LocalDate = queryDate},
                                                                    PISDK.RetrievalTypeConstants.rtInterpolated).Value

        Catch ex As Exception
            Dim exceptionmsg As String = ex.Message
        End Try

    End Sub

    Public Function GetHeatMapLatLongForTime(starttime As Date, endtime As Date, timeperiod As TimeSpan) As List(Of LatLong)

        Dim retlst As New List(Of LatLong)
        'connect to pi

        starttime = starttime.timezoneToPerth
        endtime = endtime.timezoneToPerth

        Dim piplat As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(ID & "_lat")
        Dim pilng As PISDK.PIPoint = SingletonAccess.HistorianServer.PIPoints(ID & "_long")

        'create the date objects from the timeperiod
        Dim dateObjects As New List(Of Date)

        If endtime > Now Then endtime = Now


        '***** IF the timeperiod produced > than 1500 results, then increase the timeperiod *****
        Dim MAX_RESULTS_TO_CLIENT As Integer = 250000
        Dim totalseconds As Integer = (endtime - starttime).TotalSeconds

        Dim suggestedTimeperiod As TimeSpan = TimeSpan.FromSeconds(totalseconds / MAX_RESULTS_TO_CLIENT)

        If timeperiod < suggestedTimeperiod Then timeperiod = suggestedTimeperiod


        While True
            If starttime < endtime Then
                dateObjects.Add(starttime)
                starttime = starttime + timeperiod
            Else
                Exit While
            End If
        End While

        Dim tp As New PITimeServer.TimeIntervals
        Dim arr() As Date = dateObjects.ToArray

        Dim pivalslat As PISDK.PIValues = piplat.Data.TimedValues(arr, , , False)
        Dim pivalslng As PISDK.PIValues = pilng.Data.TimedValues(arr, , , False)

        For i As Integer = 1 To pivalslat.Count
            Try

                If Not (pivalslat(i).Value.GetType().Name = "__ComObject" OrElse pivalslng(i).Value.GetType().Name = "__ComObject") Then
                    retlst.Add(New LatLong(pivalslat(i).Value, pivalslng(i).Value))
                End If
            Catch ex As Exception
                Dim s As String = ex.Message
            End Try
        Next

        Return retlst

    End Function

    Public Sub New()

    End Sub

    Public Shared Function GetExampleFleetNow(appid As Guid) As List(Of Truck)

        Return GetFleetAtTime(Now.timezoneToClient.AddSeconds(-10), appid, Business.ThisSession.UserID)
    End Function

    Public Class LatLong

        Public Property Lat As String
        Public Property Lng As String

        Public Sub New(lt As String, lg As String)

            Me.Lat = lt
            Me.Lng = lg

        End Sub

        Public Sub New()

        End Sub
    End Class

    Public Shared Function GetLatLongsForTruckForTimePeriod_oldmethod(truckID As String,
                                                            startTime As Date,
                                                            endTime As Date,
                                                            applicationid As Guid) As ClientServerRoundRobin_ReturnObject

        Dim retObj As New ClientServerRoundRobin_ReturnObject

        Dim trcks As List(Of Truck) = GetFleetAtTime(Now, applicationid, Business.ThisSession.UserID)

        Dim trck As Truck = (From t As Truck In trcks Where t.ID = truckID).First

        retObj.DriverName = trck.Driver
        retObj.TruckName = trck.TruckName

        retObj.LatLongs = trck.GetHeatMapLatLongForTime(startTime, endTime, TimeSpan.FromSeconds(10))

        Return retObj

    End Function

    Public Shared Function GetFleetAtTime(time As DateTime, applicationid As Guid, userid As Guid) As List(Of Truck)


        Dim lst As New List(Of Truck)

        Dim lstFromBusiness As List(Of FMS.Business.DataObjects.ApplicationVehicle) = _
                        FMS.Business.DataObjects.ApplicationVehicle.GetAllWithDrivers(applicationid, time, userid)

        time = time.timezoneToPerth()

        Dim app As DataObjects.Application = DataObjects.Application.GetFromAppID(applicationid)

        Dim default_lat As String = app.Settings.Where(Function(x) x.Name.ToLower = "business_lattitude").Single.Value
        Dim default_lng As String = app.Settings.Where(Function(x) x.Name.ToLower = "business_longitude").Single.Value

        'if there is nothing to process, then just return the empty list 
        If lstFromBusiness Is Nothing Then Return lst

        For Each l As FMS.Business.DataObjects.ApplicationVehicle In lstFromBusiness
            With l

                Dim drivername As String = If(.CurrentDriver Is Nothing, "unknown driver", .CurrentDriver.NameFormatted)
                Dim driverid As Guid = If(.CurrentDriver Is Nothing, Guid.Empty, .CurrentDriver.ApplicationDriverID)

                Dim t As Truck = New Truck(.DeviceID, time, drivername, .Name, .ApplicationImageID)

                t.DriverID = driverid

                If String.IsNullOrEmpty(t.ID) Then
                    t.lat = default_lat
                    t.lng = default_lng
                End If

                lst.Add(t)
            End With
        Next

        Return lst

    End Function


End Class


<Serializable()> _
Public Class ClientServerRoundRobin_ReturnObject

    Public Property queryDate As DateTime
    Public Property TruckName As String
    Public Property DriverName As String
    Public Property DeviceID As String
    Public Property LatLongs As List(Of Truck.LatLong)
    Public Property Trucks As New List(Of Truck)

    Public Property BusinessLocations As List(Of DataObjects.ApplicationLocation)

    Public Property WasError As Boolean
    Public Property ErrorMessage As String
    Public Property activityViewerAutoUpdateToNow As Boolean
    Public Property activityViewer_autoincrement_selected As Boolean
    Public Property autoUpdateCurrentActivity As Boolean
    Public Property viewMachineAtTime As Boolean
    Public Property getAllTrucksAtSpecificTime As Boolean


    Public Property vehicleViewer As Boolean

    ''' <summary>
    ''' for serialization only
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

    End Sub

End Class

<Serializable()>
Public Class DeleteGeoFence_ReturnObject

    Public Property wasSuccess As Boolean
    Public Property MessageFromServer As String

    Public Sub New()

    End Sub
End Class

<Serializable()>
Public Class SendDriverMessage_ReturnObject


    Public Property ReturnString As String

    Public Property Lat As String

    Public Property Lng As String

    Public Sub New()

    End Sub

End Class

<Serializable()>
Public Class GetAllGeoReferences_ReturnObject

    Public Property GeoFences As List(Of FMS.Business.DataObjects.ApplicationGeoFence)

    Public Property success As Boolean

    Public Property ErrorMessage As String


    Public Sub New()

    End Sub

End Class
