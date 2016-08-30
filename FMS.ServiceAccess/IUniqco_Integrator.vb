Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IIntegrator" in both code and config file together.
<ServiceContract()>
Public Interface IUniqco_Integrator


    <OperationContract()>
    Function CreateToken(username As String, password As String, expiry As Date) As WebServices.TokenResponse

    <OperationContract>
    Function GetVehicleData(VIN_Numbers As List(Of WebServices.VINNumberRequest),
                            username As String, password As String) As WebServices.GetVehicleData_Response


End Interface
