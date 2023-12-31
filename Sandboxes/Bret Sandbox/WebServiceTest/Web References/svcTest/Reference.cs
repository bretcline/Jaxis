﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.1.
// 
#pragma warning disable 1591

namespace WebServiceTest.svcTest {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    // CODEGEN: The optional WSDL extension element 'PolicyReference' from namespace 'http://schemas.xmlsoap.org/ws/2004/09/policy' was not handled.
    // CODEGEN: The optional WSDL extension element 'PolicyReference' from namespace 'http://schemas.xmlsoap.org/ws/2004/09/policy' was not handled.
    // CODEGEN: The optional WSDL extension element 'PolicyReference' from namespace 'http://schemas.xmlsoap.org/ws/2004/09/policy' was not handled.
    // CODEGEN: The optional WSDL extension element 'PolicyReference' from namespace 'http://schemas.xmlsoap.org/ws/2004/09/policy' was not handled.
    // CODEGEN: The optional WSDL extension element 'PolicyReference' from namespace 'http://schemas.xmlsoap.org/ws/2004/09/policy' was not handled.
    // CODEGEN: The optional WSDL extension element 'PolicyReference' from namespace 'http://schemas.xmlsoap.org/ws/2004/09/policy' was not handled.
    // CODEGEN: The optional WSDL extension element 'PolicyReference' from namespace 'http://schemas.xmlsoap.org/ws/2004/09/policy' was not handled.
    // CODEGEN: The optional WSDL extension element 'PolicyReference' from namespace 'http://schemas.xmlsoap.org/ws/2004/09/policy' was not handled.
    // CODEGEN: The optional WSDL extension element 'PolicyReference' from namespace 'http://schemas.xmlsoap.org/ws/2004/09/policy' was not handled.
    // CODEGEN: The optional WSDL extension element 'EndpointReference' from namespace 'http://www.w3.org/2005/08/addressing' was not handled.
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WSHttpBinding_IPourEngineConfig", Namespace="http://tempuri.org/")]
    public partial class PourEngineConfig : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback AddUPCDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback RemoveUPCDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback StartEventOperationCompleted;
        
        private System.Threading.SendOrPostCallback StopEventOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public PourEngineConfig() {
            this.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;
            this.Url = global::WebServiceTest.Properties.Settings.Default.WebServiceTest_svcTest_PourEngineConfig;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event AddUPCDataCompletedEventHandler AddUPCDataCompleted;
        
        /// <remarks/>
        public event RemoveUPCDataCompletedEventHandler RemoveUPCDataCompleted;
        
        /// <remarks/>
        public event StartEventCompletedEventHandler StartEventCompleted;
        
        /// <remarks/>
        public event StopEventCompletedEventHandler StopEventCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IPourEngineConfig/AddUPCData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void AddUPCData([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] UPCData data) {
            this.Invoke("AddUPCData", new object[] {
                        data});
        }
        
        /// <remarks/>
        public void AddUPCDataAsync(UPCData data) {
            this.AddUPCDataAsync(data, null);
        }
        
        /// <remarks/>
        public void AddUPCDataAsync(UPCData data, object userState) {
            if ((this.AddUPCDataOperationCompleted == null)) {
                this.AddUPCDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddUPCDataOperationCompleted);
            }
            this.InvokeAsync("AddUPCData", new object[] {
                        data}, this.AddUPCDataOperationCompleted, userState);
        }
        
        private void OnAddUPCDataOperationCompleted(object arg) {
            if ((this.AddUPCDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddUPCDataCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IPourEngineConfig/RemoveUPCData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void RemoveUPCData([System.Xml.Serialization.XmlElementAttribute(IsNullable=true)] UPCData data) {
            this.Invoke("RemoveUPCData", new object[] {
                        data});
        }
        
        /// <remarks/>
        public void RemoveUPCDataAsync(UPCData data) {
            this.RemoveUPCDataAsync(data, null);
        }
        
        /// <remarks/>
        public void RemoveUPCDataAsync(UPCData data, object userState) {
            if ((this.RemoveUPCDataOperationCompleted == null)) {
                this.RemoveUPCDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRemoveUPCDataOperationCompleted);
            }
            this.InvokeAsync("RemoveUPCData", new object[] {
                        data}, this.RemoveUPCDataOperationCompleted, userState);
        }
        
        private void OnRemoveUPCDataOperationCompleted(object arg) {
            if ((this.RemoveUPCDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RemoveUPCDataCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IPourEngineConfig/StartEvent", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void StartEvent(int eventID, [System.Xml.Serialization.XmlIgnoreAttribute()] bool eventIDSpecified, [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)] [System.Xml.Serialization.XmlArrayItemAttribute(Namespace="http://schemas.datacontract.org/2004/07/Jaxis.Readers.Identec")] UPCData[] upcData) {
            this.Invoke("StartEvent", new object[] {
                        eventID,
                        eventIDSpecified,
                        upcData});
        }
        
        /// <remarks/>
        public void StartEventAsync(int eventID, bool eventIDSpecified, UPCData[] upcData) {
            this.StartEventAsync(eventID, eventIDSpecified, upcData, null);
        }
        
        /// <remarks/>
        public void StartEventAsync(int eventID, bool eventIDSpecified, UPCData[] upcData, object userState) {
            if ((this.StartEventOperationCompleted == null)) {
                this.StartEventOperationCompleted = new System.Threading.SendOrPostCallback(this.OnStartEventOperationCompleted);
            }
            this.InvokeAsync("StartEvent", new object[] {
                        eventID,
                        eventIDSpecified,
                        upcData}, this.StartEventOperationCompleted, userState);
        }
        
        private void OnStartEventOperationCompleted(object arg) {
            if ((this.StartEventCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.StartEventCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IPourEngineConfig/StopEvent", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void StopEvent(int eventID, [System.Xml.Serialization.XmlIgnoreAttribute()] bool eventIDSpecified) {
            this.Invoke("StopEvent", new object[] {
                        eventID,
                        eventIDSpecified});
        }
        
        /// <remarks/>
        public void StopEventAsync(int eventID, bool eventIDSpecified) {
            this.StopEventAsync(eventID, eventIDSpecified, null);
        }
        
        /// <remarks/>
        public void StopEventAsync(int eventID, bool eventIDSpecified, object userState) {
            if ((this.StopEventOperationCompleted == null)) {
                this.StopEventOperationCompleted = new System.Threading.SendOrPostCallback(this.OnStopEventOperationCompleted);
            }
            this.InvokeAsync("StopEvent", new object[] {
                        eventID,
                        eventIDSpecified}, this.StopEventOperationCompleted, userState);
        }
        
        private void OnStopEventOperationCompleted(object arg) {
            if ((this.StopEventCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.StopEventCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.datacontract.org/2004/07/Jaxis.Readers.Identec")]
    public partial class UPCData {
        
        private short amountInBottleField;
        
        private bool amountInBottleFieldSpecified;
        
        private short bottleSizeField;
        
        private bool bottleSizeFieldSpecified;
        
        private float nozzleDiameterField;
        
        private bool nozzleDiameterFieldSpecified;
        
        private string tagIDField;
        
        private ArrayOfKeyValueOfintdoubleKeyValueOfintdouble[] viscocityByTemperatureField;
        
        /// <remarks/>
        public short AmountInBottle {
            get {
                return this.amountInBottleField;
            }
            set {
                this.amountInBottleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AmountInBottleSpecified {
            get {
                return this.amountInBottleFieldSpecified;
            }
            set {
                this.amountInBottleFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public short BottleSize {
            get {
                return this.bottleSizeField;
            }
            set {
                this.bottleSizeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BottleSizeSpecified {
            get {
                return this.bottleSizeFieldSpecified;
            }
            set {
                this.bottleSizeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        public float NozzleDiameter {
            get {
                return this.nozzleDiameterField;
            }
            set {
                this.nozzleDiameterField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NozzleDiameterSpecified {
            get {
                return this.nozzleDiameterFieldSpecified;
            }
            set {
                this.nozzleDiameterFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string TagID {
            get {
                return this.tagIDField;
            }
            set {
                this.tagIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(IsNullable=true)]
        [System.Xml.Serialization.XmlArrayItemAttribute("KeyValueOfintdouble", Namespace="http://schemas.microsoft.com/2003/10/Serialization/Arrays", IsNullable=false)]
        public ArrayOfKeyValueOfintdoubleKeyValueOfintdouble[] ViscocityByTemperature {
            get {
                return this.viscocityByTemperatureField;
            }
            set {
                this.viscocityByTemperatureField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
    public partial class ArrayOfKeyValueOfintdoubleKeyValueOfintdouble {
        
        private int keyField;
        
        private double valueField;
        
        /// <remarks/>
        public int Key {
            get {
                return this.keyField;
            }
            set {
                this.keyField = value;
            }
        }
        
        /// <remarks/>
        public double Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void AddUPCDataCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void RemoveUPCDataCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void StartEventCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void StopEventCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591