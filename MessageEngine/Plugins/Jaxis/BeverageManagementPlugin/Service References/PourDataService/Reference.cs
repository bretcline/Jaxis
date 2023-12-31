﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Jaxis.BeverageManagement.Plugin.PourDataService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PourDataService.IPourEngineService")]
    public interface IPourEngineService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPourEngineService/Ping", ReplyAction="http://tempuri.org/IPourEngineService/PingResponse")]
        bool Ping();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPourEngineService/PushCalcPourData", ReplyAction="http://tempuri.org/IPourEngineService/PushCalcPourDataResponse")]
        void PushCalcPourData(Jaxis.MessageLibrary.CalcPour data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPourEngineService/PushPourData", ReplyAction="http://tempuri.org/IPourEngineService/PushPourDataResponse")]
        void PushPourData(Jaxis.Inventory.Data.DataPour data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPourEngineService/PushTagActivity", ReplyAction="http://tempuri.org/IPourEngineService/PushTagActivityResponse")]
        void PushTagActivity(Jaxis.Inventory.Data.DataTagActivity data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPourEngineService/PushTagAlert", ReplyAction="http://tempuri.org/IPourEngineService/PushTagAlertResponse")]
        void PushTagAlert(Jaxis.Inventory.Data.DataTagAlert data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPourEngineService/PushDeviceAlert", ReplyAction="http://tempuri.org/IPourEngineService/PushDeviceAlertResponse")]
        void PushDeviceAlert(Jaxis.Inventory.Data.DataDeviceAlert data);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPourEngineService/PushTagMove", ReplyAction="http://tempuri.org/IPourEngineService/PushTagMoveResponse")]
        void PushTagMove(Jaxis.Inventory.Data.DataTagMove ticket);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPourEngineService/PushActivityLog", ReplyAction="http://tempuri.org/IPourEngineService/PushActivityLogResponse")]
        void PushActivityLog(Jaxis.Inventory.Data.DataActivityLog ticket);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPourEngineService/PushTicket", ReplyAction="http://tempuri.org/IPourEngineService/PushTicketResponse")]
        void PushTicket(Jaxis.Inventory.Data.DataPOSTicket ticket);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPourEngineServiceChannel : Jaxis.BeverageManagement.Plugin.PourDataService.IPourEngineService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PourEngineServiceClient : System.ServiceModel.ClientBase<Jaxis.BeverageManagement.Plugin.PourDataService.IPourEngineService>, Jaxis.BeverageManagement.Plugin.PourDataService.IPourEngineService {
        
        public PourEngineServiceClient() {
        }
        
        public PourEngineServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PourEngineServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PourEngineServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PourEngineServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Ping() {
            return base.Channel.Ping();
        }
        
        public void PushCalcPourData(Jaxis.MessageLibrary.CalcPour data) {
            base.Channel.PushCalcPourData(data);
        }
        
        public void PushPourData(Jaxis.Inventory.Data.DataPour data) {
            base.Channel.PushPourData(data);
        }
        
        public void PushTagActivity(Jaxis.Inventory.Data.DataTagActivity data) {
            base.Channel.PushTagActivity(data);
        }
        
        public void PushTagAlert(Jaxis.Inventory.Data.DataTagAlert data) {
            base.Channel.PushTagAlert(data);
        }
        
        public void PushDeviceAlert(Jaxis.Inventory.Data.DataDeviceAlert data) {
            base.Channel.PushDeviceAlert(data);
        }
        
        public void PushTagMove(Jaxis.Inventory.Data.DataTagMove ticket) {
            base.Channel.PushTagMove(ticket);
        }
        
        public void PushActivityLog(Jaxis.Inventory.Data.DataActivityLog ticket) {
            base.Channel.PushActivityLog(ticket);
        }
        
        public void PushTicket(Jaxis.Inventory.Data.DataPOSTicket ticket) {
            base.Channel.PushTicket(ticket);
        }
    }
}
