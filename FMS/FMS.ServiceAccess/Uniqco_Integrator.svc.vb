' NOTE: You can use the "Rename" command on the context menu to change the class name "Integrator" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select Integrator.svc or Integrator.svc.vb at the Solution Explorer and start debugging.

Imports FMS.Business
Imports System.IO
Imports System.Xml.Serialization

Public Class Uniqco_Integrator
    Implements IUniqco_Integrator


    Public Const COMPANY_LIST_UNDER_UNIQCO As String = "uniqco" 'to expand: "uniqco,ppjs,example3,etc"

    Public Function CreateToken(username As String, password As String, expiry As Date) _
                                            As WebServices.TokenResponse Implements IUniqco_Integrator.CreateToken

        'limoVIN, grantsVIN (as tests)
        Dim retobj As New WebServices.TokenResponse

        Try

            'check authentication
            Membership.ApplicationName = "uniqco"
            Dim app = FMS.Business.DataObjects.Application.GetFromApplicationName("uniqco")

            retobj.SuccessLogin = Membership.ValidateUser(username, password)

            'if not authenticated, then return an object saying so
            If Not retobj.SuccessLogin Then
                retobj.ErrorMessage = "The user is not authenticated, please try a different username/password combination"
                Exit Try
            End If

            'Search through all the company names for uniqco and search for the vehicle with the correct VIN
            'TODO: This could be done a lot more efficiently
            For Each companyName As String In COMPANY_LIST_UNDER_UNIQCO.Split(",").ToList

                Dim t As New FMS.Business.DataObjects.AuthenticationToken
                Dim a As DataObjects.Application = DataObjects.Application.GetFromApplicationName(companyName)

                With t
                    .ApplicationID = a.ApplicationID
                    .ExpiryDate = expiry ' Now.AddHours(1)
                    .StartDate = Now
                    .TokenID = Guid.NewGuid
                    .UserID = a.GetAdminUser.UserId
                End With

                retobj.TokenID = DataObjects.AuthenticationToken.Create(t)

                Dim urlStrBase = "http://{0}.nanosoft.com.au/FleetMap.aspx?VinNumber={1}&token={2}&returnURL={3}"

                retobj.URL = String.Format(urlStrBase, a.ApplicationName, "{0}", retobj.TokenID.ToString, "{1}")

            Next

        Catch ex As Exception
            retobj.ErrorMessage = ex.Message & vbNewLine & ex.StackTrace
        End Try

        Return retobj

    End Function

    Public Function GetVehicleData(VIN_Numbers As List(Of WebServices.VINNumberRequest), _
                                   username As String, password As String) As WebServices.GetVehicleData_Response Implements IUniqco_Integrator.GetVehicleData


        'limoVIN, grantsVIN (as tests)
        Dim retobj As New WebServices.GetVehicleData_Response

        'ClientTimeYesterday (cty for shorthand)
        Dim cty As Date = DataObjects.TimeZone.GetCurrentClientTime

        Try
            'check authentication
            Membership.ApplicationName = "uniqco"

            Dim app = FMS.Business.DataObjects.Application.GetFromApplicationName("uniqco")

            retobj.WasAuthenticated = Membership.ValidateUser(username, password)

            'if not authenticated, then return an object saying so
            If Not retobj.WasAuthenticated Then
                retobj.ReturnMessage = "The user is not authenticated, please try a different username/password combination"
                retobj.WasSuccess = False
                Exit Try
            End If

            For Each vnr As WebServices.VINNumberRequest In VIN_Numbers

                Dim vd = Helper.GetVehicleData(vnr)

                retobj.VINNumberResponses.Add(vd)
            Next

            retobj.WasSuccess = True

        Catch ex As Exception

            retobj.ReturnMessage = String.Format("EXCEPTION CAUSED: {0}{1}{2}", ex.Message, vbNewLine, ex.StackTrace)
        End Try


        'log the request
        Try

            Dim xmls As New XmlSerializer(VIN_Numbers.GetType)
            Dim sr As New StringWriter()

            xmls.Serialize(sr, VIN_Numbers)

            Dim vinNumbersSerialized As String = sr.ToString

            sr = New StringWriter
            xmls = New XmlSerializer(retobj.GetType)

            xmls.Serialize(sr, retobj)

            Dim respSerialized As String = sr.ToString

            Business.DataObjects.WebServiceLogLine.Create("uniqco", username, _
                                                          "GetVehicleData", vinNumbersSerialized, respSerialized)

        Catch ex As Exception

        End Try

        Return retobj
    End Function

End Class
