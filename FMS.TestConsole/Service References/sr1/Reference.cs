﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FMS.TestConsole.sr1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="sr1.IUniqco_Integrator")]
    public interface IUniqco_Integrator {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUniqco_Integrator/CreateToken", ReplyAction="http://tempuri.org/IUniqco_Integrator/CreateTokenResponse")]
        FMS.ServiceAccess.WebServices.TokenResponse CreateToken(string username, string password, System.DateTime expiry);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUniqco_Integrator/CreateToken", ReplyAction="http://tempuri.org/IUniqco_Integrator/CreateTokenResponse")]
        System.Threading.Tasks.Task<FMS.ServiceAccess.WebServices.TokenResponse> CreateTokenAsync(string username, string password, System.DateTime expiry);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUniqco_Integrator/GetVehicleData", ReplyAction="http://tempuri.org/IUniqco_Integrator/GetVehicleDataResponse")]
        FMS.ServiceAccess.WebServices.GetVehicleData_Response GetVehicleData(FMS.ServiceAccess.WebServices.VINNumberRequest[] VIN_Numbers, string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUniqco_Integrator/GetVehicleData", ReplyAction="http://tempuri.org/IUniqco_Integrator/GetVehicleDataResponse")]
        System.Threading.Tasks.Task<FMS.ServiceAccess.WebServices.GetVehicleData_Response> GetVehicleDataAsync(FMS.ServiceAccess.WebServices.VINNumberRequest[] VIN_Numbers, string username, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUniqco_IntegratorChannel : FMS.TestConsole.sr1.IUniqco_Integrator, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Uniqco_IntegratorClient : System.ServiceModel.ClientBase<FMS.TestConsole.sr1.IUniqco_Integrator>, FMS.TestConsole.sr1.IUniqco_Integrator {
        
        public Uniqco_IntegratorClient() {
        }
        
        public Uniqco_IntegratorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Uniqco_IntegratorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Uniqco_IntegratorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Uniqco_IntegratorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public FMS.ServiceAccess.WebServices.TokenResponse CreateToken(string username, string password, System.DateTime expiry) {
            return base.Channel.CreateToken(username, password, expiry);
        }
        
        public System.Threading.Tasks.Task<FMS.ServiceAccess.WebServices.TokenResponse> CreateTokenAsync(string username, string password, System.DateTime expiry) {
            return base.Channel.CreateTokenAsync(username, password, expiry);
        }
        
        public FMS.ServiceAccess.WebServices.GetVehicleData_Response GetVehicleData(FMS.ServiceAccess.WebServices.VINNumberRequest[] VIN_Numbers, string username, string password) {
            return base.Channel.GetVehicleData(VIN_Numbers, username, password);
        }
        
        public System.Threading.Tasks.Task<FMS.ServiceAccess.WebServices.GetVehicleData_Response> GetVehicleDataAsync(FMS.ServiceAccess.WebServices.VINNumberRequest[] VIN_Numbers, string username, string password) {
            return base.Channel.GetVehicleDataAsync(VIN_Numbers, username, password);
        }
    }
}
