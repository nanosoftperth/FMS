﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace UniqcoIntegrator
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="UniqcoIntegrator.IUniqco_Integrator")>  _
    Public Interface IUniqco_Integrator
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IUniqco_Integrator/CreateToken", ReplyAction:="http://tempuri.org/IUniqco_Integrator/CreateTokenResponse")>  _
        Function CreateToken(ByVal username As String, ByVal password As String, ByVal expiry As Date) As FMS.ServiceAccess.WebServices.TokenResponse
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IUniqco_Integrator/CreateToken", ReplyAction:="http://tempuri.org/IUniqco_Integrator/CreateTokenResponse")>  _
        Function CreateTokenAsync(ByVal username As String, ByVal password As String, ByVal expiry As Date) As System.Threading.Tasks.Task(Of FMS.ServiceAccess.WebServices.TokenResponse)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IUniqco_Integrator/GetVehicleData", ReplyAction:="http://tempuri.org/IUniqco_Integrator/GetVehicleDataResponse")>  _
        Function GetVehicleData(ByVal VIN_Numbers() As FMS.ServiceAccess.WebServices.VINNumberRequest, ByVal username As String, ByVal password As String) As FMS.ServiceAccess.WebServices.GetVehicleData_Response
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/IUniqco_Integrator/GetVehicleData", ReplyAction:="http://tempuri.org/IUniqco_Integrator/GetVehicleDataResponse")>  _
        Function GetVehicleDataAsync(ByVal VIN_Numbers() As FMS.ServiceAccess.WebServices.VINNumberRequest, ByVal username As String, ByVal password As String) As System.Threading.Tasks.Task(Of FMS.ServiceAccess.WebServices.GetVehicleData_Response)
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface IUniqco_IntegratorChannel
        Inherits UniqcoIntegrator.IUniqco_Integrator, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class Uniqco_IntegratorClient
        Inherits System.ServiceModel.ClientBase(Of UniqcoIntegrator.IUniqco_Integrator)
        Implements UniqcoIntegrator.IUniqco_Integrator
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        Public Function CreateToken(ByVal username As String, ByVal password As String, ByVal expiry As Date) As FMS.ServiceAccess.WebServices.TokenResponse Implements UniqcoIntegrator.IUniqco_Integrator.CreateToken
            Return MyBase.Channel.CreateToken(username, password, expiry)
        End Function
        
        Public Function CreateTokenAsync(ByVal username As String, ByVal password As String, ByVal expiry As Date) As System.Threading.Tasks.Task(Of FMS.ServiceAccess.WebServices.TokenResponse) Implements UniqcoIntegrator.IUniqco_Integrator.CreateTokenAsync
            Return MyBase.Channel.CreateTokenAsync(username, password, expiry)
        End Function
        
        Public Function GetVehicleData(ByVal VIN_Numbers() As FMS.ServiceAccess.WebServices.VINNumberRequest, ByVal username As String, ByVal password As String) As FMS.ServiceAccess.WebServices.GetVehicleData_Response Implements UniqcoIntegrator.IUniqco_Integrator.GetVehicleData
            Return MyBase.Channel.GetVehicleData(VIN_Numbers, username, password)
        End Function
        
        Public Function GetVehicleDataAsync(ByVal VIN_Numbers() As FMS.ServiceAccess.WebServices.VINNumberRequest, ByVal username As String, ByVal password As String) As System.Threading.Tasks.Task(Of FMS.ServiceAccess.WebServices.GetVehicleData_Response) Implements UniqcoIntegrator.IUniqco_Integrator.GetVehicleDataAsync
            Return MyBase.Channel.GetVehicleDataAsync(VIN_Numbers, username, password)
        End Function
    End Class
End Namespace
