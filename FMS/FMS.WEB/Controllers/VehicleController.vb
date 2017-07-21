Imports System.Net
Imports System.Web.Http
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net.Http
Imports FMS.Business.DataObjects
Imports System.Web.Security

Namespace Controllers


    Public Class VehicleController
        Inherits ApiController

        ' GET api/vehicles
        ''' <summary>
        ''' Get vehicle values
        ''' </summary>
        ''' <returns>List of string</returns>
        Public Function [Get]() As IEnumerable(Of String)


            Return New String() {"Passed"}

            Dim userName As String = "jonathanq"
            Dim password As String = "password"
            If Membership.ValidateUser(userName, password) Then
                Return New String() {"Passed"}
            Else
                Return New String() {"FAILED"}
            End If
        End Function

        ' GET api/vehicles/5
        ''' <summary>
        ''' Get available CAN tags by vehicleID
        ''' </summary>
        ''' <param name="vehicleID">The Vehicle Id</param>
        ''' <returns>List of Can message definition</returns>
        Public Function [Get](vehicleID As String) As List(Of CAN_MessageDefinition)

            Return FMS.Business.DataObjects.ApplicationVehicle.GetFromName(vehicleID).GetAvailableCANTags()

        End Function

        ' GET api/vehicles/GetCanMessge/5
        ''' <summary>
        ''' Get available CAN tags by deviceID
        ''' </summary>
        ''' <param name="deviceID">The device Id</param>
        ''' <returns>List of Can message definition</returns>
        <HttpGet>
        Public Function GetCanMessage(deviceID As String) As List(Of CAN_MessageDefinition)
            Return FMS.Business.DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID).GetAvailableCANTags()
        End Function

        ' GET api/vehicles/GetDashboardData/5
        ''' <summary>
        ''' Get available CAN tags by deviceID for dashboard
        ''' </summary>
        ''' <param name="DashVehicleID">The device Id</param>
        ''' <returns>List of dashboard values</returns>
        <HttpGet>
        Public Function GetDashboardData(DashVehicleID As String) As List(Of DashboardValues)

            'Dim tagvalues = FMS.Business.DataObjects.ApplicationVehicle.GetFromDeviceID(DashVehicleID).GetAvailableCANTagsValue()
            'Dim tagvalues = FMS.Business.DataObjects.DashboardValues.GetDataForDashboard(DashVehicleID)
            'Return FMS.Business.DataObjects.CanDataPoint.GetPointWithDataForDashboard(DashVehicleID)
            Return FMS.Business.DataObjects.DashboardValues.GetDataForDashboard(DashVehicleID)

            'Dim objDashValues = New List(Of DashboardValues)


        End Function

        ' GET api/vehicles/GetCanMessageValue/5
        ''' <summary>
        ''' Get available CAN Message Value by deviceID
        ''' </summary>
        ''' <param name="deviceID">The device Id</param>
        ''' <returns>List of Can values and message definition</returns>
        <HttpGet>
        Public Function GetCanMessageValue(deviceID As String) As List(Of CanValueMessageDefinition)
            Return FMS.Business.DataObjects.ApplicationVehicle.GetFromDeviceID(deviceID).GetAvailableCANTagsValue()
        End Function

        ''' <summary>
        ''' Get CAN data point by vehicleID, standard, SPN, startdate and enddate
        ''' </summary>
        ''' <param name="vehicleID">The vehicle Id</param>
        ''' <param name="standard">The standard field</param>
        ''' <param name="SPN">The SPN field</param>
        ''' <param name="startdate">The startdate for the time period</param>
        ''' <param name="enddate">The enddate for the time period</param>
        ''' <returns>CanDataPoint</returns>
        
        Public Function [Get](vehicleID As String, standard As String, SPN As Integer, startdate As String, enddate As String) As CanDataPoint
            'the standard of canbus is infered from the vehicleID.

            Dim sd As DateTime = DateTime.Parse(startdate)
            Dim ed As DateTime = DateTime.Parse(enddate)


            Dim cdp As CanDataPoint = FMS.Business.DataObjects.CanDataPoint.GetPointWithData(SPN, vehicleID, standard, sd, ed)

            Return cdp
        End Function

        ''' <summary>
        ''' Get CAN data point by deviceID, standard, SPN, startdate and enddate
        ''' </summary>
        ''' <param name="deviceID"></param>
        ''' <param name="standard"></param>
        ''' <param name="SPN"></param>
        ''' <param name="startdate"></param>
        ''' <param name="enddate"></param>
        ''' <returns>CanDataPoint</returns>
        ''' <remarks></remarks>
        <HttpGet>
        Public Function GetCanMessage(deviceID As String, standard As String, SPN As Integer, startdate As String, enddate As String) As CanDataPoint
            'the standard of canbus is infered from the vehicleID.

            Dim sd As DateTime = DateTime.Parse(startdate)
            Dim ed As DateTime = DateTime.Parse(enddate)


            Dim cdp As CanDataPoint = FMS.Business.DataObjects.CanDataPoint.GetPointWithDataByDeviceId(SPN, deviceID, standard, sd, ed)

            Return cdp
        End Function

        ' POST api/vehicles
        ''' <summary>
        ''' Post vehicles
        ''' </summary>
        ''' <param name="value">Values posted</param>
        Public Sub Post(<FromBody> value As String)
        End Sub

        ' PUT api/vehicles/5
        ''' <summary>
        ''' Put vehicles
        ''' </summary>
        ''' <param name="id">by Id</param>
        ''' <param name="value">String values</param>
        Public Sub Put(id As Integer, <FromBody> value As String)
        End Sub

        ' DELETE api/vehicles/5
        ''' <summary>
        ''' Delete vehicle by Id
        ''' </summary>
        ''' <param name="id">Vehicle Id</param>
        Public Sub Delete(id As Integer)
        End Sub
    End Class


End Namespace