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

    Public ReadOnly Property ComboBoxDisplay As String
        Get
            Return String.Format("{0} ({1})", Driver, TruckName)
        End Get
    End Property


    Public Sub New(id As String, queryDate As Date, driver As String, truckName As String)

        Me.Driver = driver
        Me.ID = id
        Me.time = queryDate

        Dim sdk As New PISDK.PISDK
        Dim srv As PISDK.Server = sdk.Servers("Windows-Vultr")

        Dim piplat As PISDK.PIPoint = srv.PIPoints(id & "_lat")
        Dim pilng As PISDK.PIPoint = srv.PIPoints(id & "_long")

        Me.TruckName = truckName

        'now that we have the correct driver and truck for the deviceID, we can grab the latitue and lonitude for this instance in time
        Try

            Dim obj As Object = piplat.Data.ArcValue(New PITimeServer.PITime With {.LocalDate = queryDate},
                                                                    PISDK.RetrievalTypeConstants.rtInterpolated).Value

            Me.lat = CStr(obj)
        Catch ex As Exception
            Dim s As String
            s = ""
        End Try

        Try
            Me.lng = pilng.Data.ArcValue(New PITimeServer.PITime With {.LocalDate = queryDate}, PISDK.RetrievalTypeConstants.rtInterpolated).Value
        Catch ex As Exception : End Try


    End Sub

    Public Function GetHeatMapLatLongForTime(starttime As Date, endtime As Date, timeperiod As TimeSpan) As List(Of LatLong)

        Dim retlst As New List(Of LatLong)
        'connect to pi
        Dim sdk As New PISDK.PISDK
        Dim srv As PISDK.Server = sdk.Servers("Windows-Vultr")

        Dim piplat As PISDK.PIPoint = srv.PIPoints(ID & "_lat")
        Dim pilng As PISDK.PIPoint = srv.PIPoints(ID & "_long")

        'create the date objects from the timeperiod
        Dim dateObjects As New List(Of Date)

        If endtime > Now Then endtime = Now


        '***** IF the timeperiod produced > than 1500 results, then increase the timeperiod *****
        Dim MAX_RESULTS_TO_CLIENT As Integer = 1500
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
                retlst.Add(New LatLong(pivalslat(i).Value, pivalslng(i).Value))
            Catch ex As Exception
            End Try
        Next

        Return retlst

    End Function

    Public Sub New()

    End Sub

    Public Shared Function GetExampleFleetNow() As List(Of Truck)
        Return GetFleetAtTime(Now.AddSeconds(-10))
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

    Friend Shared Function GetLatLongsForTruckForTimePeriod(truckID As String,
                                                            startTime As Date,
                                                            endTime As Date) As ClientServerRoundRobin_ReturnObject
        Dim retObj As New ClientServerRoundRobin_ReturnObject

        Dim trcks As List(Of Truck) = GetFleetAtTime(Now)

        Dim trck As Truck = (From t As Truck In trcks Where t.ID = truckID).Single

        retObj.DriverName = trck.Driver
        retObj.TruckName = trck.TruckName

        retObj.LatLongs = trck.GetHeatMapLatLongForTime(startTime, endTime, TimeSpan.FromSeconds(10))

        Return retObj

    End Function

    Friend Shared Function GetFleetAtTime(time As DateTime) As List(Of Truck)


        Dim lst As New List(Of Truck)

        Dim lstFromBusiness As List(Of FMS.Business.DataObjects.ApplicationVehicle) = _
                        FMS.Business.DataObjects.ApplicationVehicle.GetAllWithDrivers(ThisSession.ApplicationID, time)


        'FMS.Business.DataObjects.ApplicationVehicleDriverTime.

        For Each l As FMS.Business.DataObjects.ApplicationVehicle In lstFromBusiness
            With l

                Dim drivername As String = If(.CurrentDriver Is Nothing, "unknown driver", .CurrentDriver.NameFormatted)

                lst.Add(New Truck(.DeviceID, time, drivername, .Name))
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
    Public Property WasError As Boolean
    Public Property ErrorMessage As String
    Public Property activityViewerAutoUpdateToNow As Boolean
    Public Property activityViewer_autoincrement_selected As Boolean
    Public Property autoUpdateCurrentActivity As Boolean
    Public Property viewMachineAtTime As Boolean

    Public Property getAllTrucksAtSpecificTime As Boolean



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
Public Class GetAllGeoReferences_ReturnObject

    Public Property GeoFences As List(Of FMS.Business.DataObjects.ApplicationGeoFence)

    Public Property success As Boolean

    Public Property ErrorMessage As String


    Public Sub New()

    End Sub

End Class
