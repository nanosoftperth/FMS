Imports System.ServiceModel

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IIntegrator" in both code and config file together.
<ServiceContract()>
Public Interface IUniqco_Integrator

    <OperationContract()>
    Function GetCurrentDateTest() As Date

    <OperationContract()>
    Function GetMileageForVehicle(VIN_Number As String, userName As String, password As String) As WebServices.VehicleDistanceData





End Interface
