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
Public Class DefaultService

    <OperationContract()>
<WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.WrappedRequest, ResponseFormat:=WebMessageFormat.Json)>
    Public Function DefalutserverInstantiate() As String
        Return Now.ToString
    End Function

    <OperationContract()>
<WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.WrappedRequest, ResponseFormat:=WebMessageFormat.Json)>
    Public Function SaveDefaultBusinessLocation(newBusinessLocation As String) As String

        Dim id As Guid = Guid.Parse(newBusinessLocation)
        ThisSession.ApplicationObject.SaveDefaultBusinessLocation(id)

        Return Now.ToString
    End Function

    <OperationContract()>
<WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.WrappedRequest, ResponseFormat:=WebMessageFormat.Json)>
    Public Function SetSelectedReport(ReportName As String) As String

        ThisSession.SelectedReportName = ReportName

        Return "message from server"

    End Function


    'JAVASCRIPT CALLER
    'param.colour = selectedItem.colour;
    'param.desc = selectedItem.desc;
    'param.id = selectedItem.id;
    'param.name = selectedItem.name;
    'param.latlngs = selectedItem.latlngs;
    'param.circleCentre = selectedItem.circleCentre;
    'param.circleRadius = selectedItem.circleRadius;
    'param.isCircle = selectedItem.isCircle;
    <OperationContract()>
<WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.WrappedRequest, ResponseFormat:=WebMessageFormat.Json)>
    Public Function EditGeoFence(colour As String _
                                 , desc As String _
                                 , id As String _
                                 , name As String _
                                 , latlngs As String _
                                 , circleCentre As String _
                                 , circleRadius As String _
                                 , isCircle As String) As DeleteGeoFence_ReturnObject

        Dim retObj As New DeleteGeoFence_ReturnObject With {.wasSuccess = True}
        Dim sideCount As Integer = 0

        Try
            'get the geofence
            Dim geofence As FMS.Business.DataObjects.ApplicationGeoFence = _
                       FMS.Business.DataObjects.ApplicationGeoFence.GetApplicationGeoFence(New Guid(id))

            'create new sides for the geo-fence
            Dim sides As New List(Of FMS.Business.DataObjects.ApplicationGeoFenceSide)

            If Not String.IsNullOrEmpty(latlngs) Then

                For Each s As String In latlngs.Split("|")

                    Dim fenceside As New FMS.Business.DataObjects.ApplicationGeoFenceSide

                    Dim strarr() As String = s.Split(",")
                    Dim latlng As LatLong = New LatLong With {.Lat = strarr(0), .Lng = strarr(1)}

                    fenceside.Latitude = latlng.Lat
                    fenceside.Longitude = latlng.Lng

                    sides.Add(fenceside)
                Next
            End If

            sideCount = sides.Count

            'set the geo-fence parameters
            With geofence
                .Colour = colour
                .Description = desc
                .Name = name
                .IsCircular = CBool(isCircle)
                .CircleRadiusMetres = circleRadius
                .CircleCentre = circleCentre
            End With

            'update the DB
            FMS.Business.DataObjects.ApplicationGeoFence.Update(geofence)

            If sides.Count > 0 Then geofence.UpdateGeoFenceSides(sides)

            If geofence.IsCircular Then

                retObj.MessageFromServer = String.Format("Successfuly updated circular geo-fence ""{0}"" with a radius of {1:0}m ", geofence.Name, geofence.CircleRadiusMetres)
            Else

                retObj.MessageFromServer = String.Format("Successfuly updated geo-fence ""{0}"" with {1} sides ", geofence.Name, sides.Count)
            End If

        Catch ex As Exception

            retObj.wasSuccess = False
            retObj.MessageFromServer = String.Format("ERROR FROM SERVER:{0}{1}{0}{2}", _
                                                     vbNewLine, ex.Message, ex.StackTrace)
        End Try


        Return retObj

    End Function

    <OperationContract()>
<WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.WrappedRequest, ResponseFormat:=WebMessageFormat.Json)>
    Public Function DeleteGeoFence(ApplicationGeoFenceID As String) As DeleteGeoFence_ReturnObject

        Dim retobj As New DeleteGeoFence_ReturnObject With {.wasSuccess = True}

        Try

            Dim id As New Guid(ApplicationGeoFenceID)

            Dim appgeofence As FMS.Business.DataObjects.ApplicationGeoFence = _
                            FMS.Business.DataObjects.ApplicationGeoFence.GetApplicationGeoFence(id)

            FMS.Business.DataObjects.ApplicationGeoFence.Delete(appgeofence)

            retobj.MessageFromServer = "Successful deletion of " & appgeofence.Name

        Catch ex As Exception

            retobj.wasSuccess = False
            retobj.MessageFromServer = String.Format("ERROR FROM SERVER:{0}{1}{0}{2}", _
                                                     vbNewLine, ex.Message, ex.StackTrace)

        End Try

        Return retobj

    End Function


    'param.NorthWestLat = bounds.H.j
    'param.NorthWestLong = bounds.j.j
    'param.SoutEasttLat = bounds.H.H;
    'param.SoutEasttLong = bounds.j.H;

    'NorthWestLat As String, NorthWestLong As String,
    'SoutEasttLat As String, SoutEasttLong As String

    <OperationContract()>
<WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.WrappedRequest, ResponseFormat:=WebMessageFormat.Json)>
    Public Function SendDriverMessage(SendEmail As Boolean, SendText As Boolean, _
                                      MsgToSend As String, DriverID As String) As SendDriverMessage_ReturnObject

        Dim retobj As New SendDriverMessage_ReturnObject

        Try

            Dim did As Guid = Guid.Parse(DriverID)

            Dim driver = Business.DataObjects.ApplicationDriver.GetDriverFromID(did)

            Dim fromStr As String = String.Format("Message sent from nanosoft-FMS by {0} ", ThisSession.User.UserName)


            If driver Is Nothing Then
                retobj.ReturnString = "There is no driver assigned to the vehicle. No message has been sent."
            Else
                If SendEmail Then Business.BackgroundCalculations.EmailHelper.SendEmail(driver.EmailAddress, fromStr, MsgToSend)
                If SendText Then Business.BackgroundCalculations.EmailHelper.SendSMS(driver.PhoneNumber, fromStr & ": " & MsgToSend)

                retobj.ReturnString = "Messages sent successfully"
            End If

        Catch ex As Exception
            retobj.ReturnString = String.Format("EXCEPTOIN CAUSED{0}{1}{0}{2}", vbNewLine, ex.Message, String.Empty)
        End Try

        Return retobj

    End Function

    <OperationContract()>
<WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.WrappedRequest, ResponseFormat:=WebMessageFormat.Json)>
    Public Function GetLatestMessage(DeviceID As String) As SendDriverMessage_ReturnObject

        Dim retval As String

        Try
            retval = Business.DataObjects.Device.GetLastLogEntryForDeviceID(DeviceID)

            'retval = retval.Replace(",", "\n").Replace(",", vbNewLine).Replace(",", "\n").Replace(",", "\n").Replace(",", "\n").Replace(",", "\n").Replace(",", "\n")

        Catch ex As Exception
            retval = String.Format("EXCEPTION:{0}{1}{2}", ex.Message, vbNewLine, ex.StackTrace)
        End Try

        Return New SendDriverMessage_ReturnObject With {.ReturnString = retval}

    End Function


    <OperationContract()>
<WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.WrappedRequest, ResponseFormat:=WebMessageFormat.Json)>
    Public Function GetAllGeoFences() As GetAllGeoReferences_ReturnObject

        Dim retobj As New GetAllGeoReferences_ReturnObject

        Try

            Dim thisappid As Guid = ThisSession.ApplicationID

            retobj.GeoFences = FMS.Business.DataObjects.ApplicationGeoFence.GetAllApplicationGeoFences(thisappid)

        Catch ex As Exception
            retobj.success = False
            retobj.ErrorMessage = String.Format("{0}{1}{3}", ex.Message, vbNewLine, ex.StackTrace)
        End Try

        Return retobj

    End Function

    <OperationContract()>
<WebInvoke(Method:="POST", BodyStyle:=WebMessageBodyStyle.WrappedRequest, ResponseFormat:=WebMessageFormat.Json)>
    Public Function SaveNewPolygonToDB(LatLongs As String,
                                       Name As String,
                                       Description As String,
                                       Colour As String,
                                       CircleLatLngStr As String,
                                       CircleRadius As String,
                                       CircleOrPolygon As String) As SaveNewPolygonToDB_ReturnObject

        Dim retobj As New SaveNewPolygonToDB_ReturnObject

        Try
            Dim geofence As New FMS.Business.DataObjects.ApplicationGeoFence

            geofence.Name = Name
            geofence.Description = Description
            geofence.Colour = Colour
            geofence.ApplicationID = ThisSession.ApplicationID
            geofence.UserID = ThisSession.User.UserId
            geofence.CircleCentre = CircleLatLngStr
            geofence.CircleRadiusMetres = CircleRadius
            geofence.IsCircular = CircleOrPolygon.ToLower = "circle"

            For Each s As String In LatLongs.Split("|")

                Dim fenceside As New FMS.Business.DataObjects.ApplicationGeoFenceSide

                Dim strarr() As String = s.Split(",")
                Dim latlng As LatLong = New LatLong With {.Lat = strarr(0), .Lng = strarr(1)}

                fenceside.Latitude = latlng.Lat
                fenceside.Longitude = latlng.Lng

                geofence.ApplicationGeoFenceSides.Add(fenceside)
            Next

            'Check if there is already a geofence with that name
            'by ryan
            Dim alreadyExists As Boolean = FMS.Business.DataObjects.ApplicationGeoFence.IfApplicationGeoFencesAlreadyExist(ThisSession.ApplicationID, Name)

            retobj.IsCircular = geofence.IsCircular

            If Not alreadyExists Then
                retobj.NewGeoFenceID = FMS.Business.DataObjects.ApplicationGeoFence.Create(geofence).ToString
                retobj.wasSuccess = True
                retobj.MessageFromServer = "Successful creation of geofence!"
            Else
                retobj.wasSuccess = False
                retobj.MessageFromServer = String.Format("The geo-fence ""{0}"" already exists." & _
                            "{1}Please use a different name or edit the existing geo-fence using the edit tab.", Name, vbNewLine)
            End If

        Catch ex As Exception
            retobj.wasSuccess = False
            Dim vbNewLine = ""
            retobj.MessageFromServer = String.Format("ERROR at Server:{0}{1}{0}{2}", vbNewLine, ex.Message, ex.StackTrace)
        End Try

        Return retobj
    End Function


End Class
