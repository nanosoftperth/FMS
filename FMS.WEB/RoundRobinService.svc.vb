Imports System.ServiceModel
Imports System.ServiceModel.Activation
Imports System.ServiceModel.Web
'Imports OSIsoft.AF.Asset
Imports System.Runtime.Serialization.Json
Imports FMS.Business

' NOTE: You can use the "Rename" command on the context menu to change the class name "DefaultService" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select DefaultService.svc or DefaultService.svc.vb at the Solution Explorer and start debugging.
<ServiceContract(Namespace:="")>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
Public Class RoundRobinService


    '***************************   DEFINITIONS FROM JS ***************************
    '****           activityViewer_SelectedVehicle          
    '****           activityViewerStartTime                 
    '****           activityViewerEndTime                   
    '****           activityViewerAutoUpdateToNow  
    '****           activityViewer_autoincrement_selected           
    '****           activityViewer_autoIncrement_seconds            
    '****           activityViewer_autoIncrement_secondsMultiplier  
    '****           //"calculated"
    '****           activityViewer_autoIncrement_secondsTotal 
    '****           var autoUpdateCurrentActivity 
    '******************************************************************************

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="activityViewer_SelectedVehicle"></param>
    ''' <param name="activityViewerStartTime"></param>
    ''' <param name="activityViewerEndTime"></param>
    ''' <param name="activityViewerAutoUpdateToNow"></param>
    ''' <param name="activityViewer_autoincrement_selected"></param>
    ''' <param name="activityViewer_autoIncrement_seconds"></param>
    ''' <param name="activityViewer_autoIncrement_secondsMultiplier"></param>
    ''' <param name="activityViewer_autoIncrement_secondsTotal"></param>
    ''' <param name="autoUpdateCurrentActivity"></param>
    ''' <param name="viewMachineAtTime"></param>
    ''' <param name="getAllTrucksAtSpecificTime"></param>
    ''' <param name="getAllTrucksAtSpecificTimeDate"></param>
    ''' <param name="isFirstCall">is this the first time that the page is calling this function? (for cache reasons)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    <OperationContract()>
<WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.WrappedRequest, ResponseFormat:=WebMessageFormat.Json)>
    Public Function ClientServerRoundRobin(activityViewer_SelectedVehicle As String,
                                           activityViewerStartTime As String,
                                           activityViewerEndTime As String,
                                           activityViewerAutoUpdateToNow As Boolean,
                                           activityViewer_autoincrement_selected As Boolean,
                                           activityViewer_autoIncrement_seconds As String,
                                           activityViewer_autoIncrement_secondsMultiplier As String,
                                           activityViewer_autoIncrement_secondsTotal As String,
                                           autoUpdateCurrentActivity As Boolean,
                                           viewMachineAtTime As Boolean,
                                           getAllTrucksAtSpecificTime As Boolean,
                                           getAllTrucksAtSpecificTimeDate As String,
                                            isFirstCall As Boolean
                                            ) As ClientServerRoundRobin_ReturnObject


        Dim retobj As New ClientServerRoundRobin_ReturnObject
        Dim waittimeInSeconds As Decimal = 0

        Dim StartTime As Date = activityViewerStartTime.getDateFromJavascriptFormat
        Dim EndTime As Date = activityViewerEndTime.getDateFromJavascriptFormat

        Try

            Select Case True

                'upate all the current trucks to what is goin on NOW()
                Case autoUpdateCurrentActivity

                    retobj = GetCurrentItemsWithPositions()
                    retobj.queryDate = Now.timezoneToClient
                    'update the current vehicle to the its progress NOW()

                Case activityViewerAutoUpdateToNow, activityViewer_autoincrement_selected, viewMachineAtTime

                    Dim selectedVehicles As List(Of String) = activityViewer_SelectedVehicle.Split(";").ToList

                    retobj = GetHeatMapCoords(selectedVehicles, StartTime, EndTime)

                Case getAllTrucksAtSpecificTime

                    retobj = GetItemsWithPositionsAtTime(getAllTrucksAtSpecificTimeDate)

                Case Else
                    'this always needs to be set or there will be an exception thrown 
                    'by jQuery when trying to parse the result
                    retobj.queryDate = Now.timezoneToClient
            End Select

            'if this is the first call to the round robin service from the page, then update the session object
            'it doesnt matter if this is called from another page in the same session. 
            If isFirstCall Then FMS.Business.ThisSession.BusinessLocations = DataObjects.ApplicationLocation.GetAll(FMS.Business.ThisSession.ApplicationID)
            retobj.BusinessLocations = FMS.Business.ThisSession.BusinessLocations

            retobj.activityViewerAutoUpdateToNow = activityViewerAutoUpdateToNow
            retobj.activityViewer_autoincrement_selected = activityViewer_autoincrement_selected
            retobj.autoUpdateCurrentActivity = autoUpdateCurrentActivity
            retobj.viewMachineAtTime = viewMachineAtTime
            retobj.getAllTrucksAtSpecificTime = getAllTrucksAtSpecificTime

            'TimeZoneHelper.AltertoHQTimeZone(retobj)

            ' If retobj.queryDate = Date.MinValue Then retobj.queryDate = Now

        Catch ex As Exception
            retobj.WasError = True
            retobj.ErrorMessage = ex.Message
        End Try

        'retobj.queryDate = retobj.queryDate.ToUniversalTime
        For Each Truck In retobj.Trucks
            Truck.IsWithCanBus = FMS.Business.DataObjects.ApplicationVehicle.GetFromDeviceID(Truck.ID).GetZagroStatus()
        Next

        Return retobj

    End Function



    <OperationContract()>
<WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.WrappedRequest, ResponseFormat:=WebMessageFormat.Json)>
    Public Function GetHeatMapCoords(selectedTruckIds As List(Of String),
                                     StartTime As Date,
                                     EndTime As Date) As ClientServerRoundRobin_ReturnObject


        Dim retObj As New ClientServerRoundRobin_ReturnObject

        Try

            'if the enddate is > than what is possible, then reset it to NOW (in the clients time-zone)
            If EndTime > Now.timezoneToClient Then EndTime = Now.timezoneToClient.AddSeconds(-5)
            retObj.queryDate = CDate(EndTime)

            'return the list of trucks
            retObj.Trucks = Truck.GetFleetAtTime(EndTime, FMS.Business.ThisSession.ApplicationID, Business.ThisSession.UserID)

            'for each truck we wish to process, find the journey lat and longs.
            For Each truckID As String In selectedTruckIds

                'find the truck in the list
                Dim foundTruck As Truck = retObj.Trucks.Where(Function(x) x.ID IsNot Nothing AndAlso x.ID.ToLower = truckID.ToLower).FirstOrDefault
                'exit the for loop if we cannot find the 
                If foundTruck Is Nothing Then Continue For

                foundTruck.ShowJourneyOnMap = True 'marker for client side processing
                foundTruck.JourneyLatLngs = foundTruck.GetHeatMapWithWeightings(StartTime, EndTime)
                'foundTruck.GetHeatMapLatLongForTime(StartTime, EndTime, TimeSpan.FromSeconds(10))
            Next

        Catch ex As Exception
            Dim x As String = ex.Message
        End Try

        Return retObj

    End Function


    <OperationContract()>
<WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.WrappedRequest, ResponseFormat:=WebMessageFormat.Json)>
    Public Function GetCurrentItemsWithPositions() As ClientServerRoundRobin_ReturnObject

        Dim retObj As New ClientServerRoundRobin_ReturnObject

        Try
            retObj.Trucks = Truck.GetFleetAtTime(Now.timezoneToClient, FMS.Business.ThisSession.ApplicationID, Business.ThisSession.UserID)
        Catch ex As Exception
            Dim x As String = ""
        End Try

        retObj.queryDate = retObj.queryDate.ToUniversalTime

        'retObj.queryDate = Now
        'TimeZoneHelper.AlterToClientTimeZone(retObj)

        Return retObj
    End Function


    <OperationContract()>
<WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.WrappedRequest, ResponseFormat:=WebMessageFormat.Json)>
    Public Function GetItemsWithPositionsAtTime(theDate As String) As ClientServerRoundRobin_ReturnObject

        Dim retObj As New ClientServerRoundRobin_ReturnObject        '

        Dim browserDate As Date = theDate.getDateFromJavascriptFormat

        Try

            theDate = browserDate
            Dim std As Date = CDate(theDate)
            retObj.Trucks = Truck.GetFleetAtTime(std, FMS.Business.ThisSession.ApplicationID, Business.ThisSession.UserID)
            retObj.queryDate = std

            Return retObj

        Catch ex As Exception
            Dim x As String = ""
        End Try

        Return Nothing
    End Function



End Class
