' NOTE: You can use the "Rename" command on the context menu to change the class name "WebService_Uniqco" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select WebService_Uniqco.svc or WebService_Uniqco.svc.vb at the Solution Explorer and start debugging.
Public Class WebService_Uniqco
    Implements IWebService_Uniqco

    Public Const COMPANY_LIST_UNDER_UNIQCO As String = "uniqco" 'to expand: "uniqco,ppjs,example3,etc"

    Public Function GetCurrentDateTest() As Date Implements IWebService_Uniqco.GetCurrentDateTest
        Return Now
    End Function


    Public Function GetMileageForVehicle(VIN_Number As String, userName As String, password As String) As WebServices.VehicleDistanceData Implements IWebService_Uniqco.GetMileageForVehicle

        'limoVIN, grantsVIN (as tests)
        Dim retobj As New WebServices.VehicleDistanceData

        Try

            'check authentication
            Membership.ApplicationName = "uniqco"
            Dim app = FMS.Business.DataObjects.Application.GetFromApplicationName("uniqco")
            retobj.WasAuthenticated = Membership.ValidateUser(userName, password)


            'if not authenticated, then return an object saying so
            If Not retobj.WasAuthenticated Then
                retobj.ReturnMessage = "The user is not authenticated, please try a different username/password combination"
                retobj.WasSuccess = False
                Exit Try
            End If

            Dim foundVehicle As FMS.Business.DataObjects.ApplicationVehicle = Nothing

            'search through all the company names for uniqco and saerch for the vehicle with the correct VIN
            'TODO: This could be done a lot more efficiently
            For Each companyName As String In COMPANY_LIST_UNDER_UNIQCO.Split(",").ToList

                Dim loopApp = FMS.Business.DataObjects.Application.GetFromApplicationName(companyName)
                Dim loopVehicle = FMS.Business.DataObjects.ApplicationVehicle.GetFromVINNumber(VIN_Number, loopApp.ApplicationID)

                If loopVehicle IsNot Nothing Then foundVehicle = loopVehicle
            Next


            If foundVehicle IsNot Nothing Then

                Dim odoReading = foundVehicle.GetOdometerReadings.OrderBy(Function(x) x.TimeReadingTaken).LastOrDefault

                If odoReading Is Nothing Then
                    retobj.ReturnMessage = "there was no ""start"" odometer reading"
                    retobj.WasSuccess = False
                    Exit Try
                End If

                retobj.LastOdometerReading = New WebServices.OdometerReading With {.DateOfReading = odoReading.TimeReadingTaken _
                                                                                  , .Kilometers = odoReading.OdometerReading}

                With foundVehicle.GetVehicleMovementSummary(retobj.LastOdometerReading.DateOfReading, Now)
                    retobj.DistanceSinceLastOdometerReading_km = .KilometersTravelled
                    retobj.EnginehoursOn = .EngineHoursOn
                End With

                retobj.CalcluatedDistance = retobj.LastOdometerReading.Kilometers + retobj.DistanceSinceLastOdometerReading_km
                retobj.WasSuccess = True



            Else
                retobj.ReturnMessage = "there was no vehicle with that VIN number"
                retobj.WasSuccess = False
            End If


        Catch ex As Exception

            retobj.ReturnMessage = String.Format("EXCAPTION CAUSED: {0}{1}{2}", ex.Message, vbNewLine, ex.StackTrace)
        End Try

        Return retobj

    End Function

End Class
